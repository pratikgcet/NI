using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HuanHuan.Core.Interfaces;
using HuanHuan.Domain;

namespace HuanHuan.Core
{
    public abstract class GenericRepository<T> : BaseRepository, IRepository<T>  where T: class
    {
        // == ctor
        protected GenericRepository(string connectionStringConfigSettingName)
            : base(connectionStringConfigSettingName)
        {
            //
        }

        // == Protected methods
        protected virtual object[] GetKey(T dto)
        {
            if (dto == null)
                return new object[0];

            var keys = dto.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(KeyAttribute)))
                .ToArray();

            return keys;
        }
        protected virtual OperationResult<T> DoSave(T dto)
        {
            try
            {
                var keys = GetKey(dto);
                if (!keys.Any())
                    return new Domain.OperationResult<T>(false, new[] { "Underlying entity is invalid. No key defined. Save cancelled" });

                var existingEntity = this.FindByKey(keys);
                if (existingEntity == null)
                {
                    // add new entity
                    DataContext.Set<T>().Add(dto);
                }
                else
                {
                    // Update
                    DataContext.Entry(existingEntity).CurrentValues.SetValues(dto);

                    // Note :  this update does not update the whole object graph
                    //         this logic must be implemented separately
                }

                return new Domain.OperationResult<T>(true);
            }
            catch (DBConcurrencyException ex)
            {
                return new Domain.OperationResult<T>(false, new[] { "Concurrency error", ex.Message });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new Domain.OperationResult<T>(false, new[] { "Update concurrency error", ex.Message });
            }
            catch (Exception ex)
            {
                return new Domain.OperationResult<T>(false, new[] { "Error", ex.Message });
            }
        }

        // == IRepository<T> implementation methods
        public virtual OperationResult<T> DeleteByKey(object key)
        {
            var existingEnt = this.FindByKey(key);
            if(existingEnt == null)
                return new Domain.OperationResult<T>(false, new [] { "Entity not found with the specified key. Delete cancelled"});

            try
            {
                DataContext.Set<T>().Remove(existingEnt);
                DataContext.SaveChanges();
                return new Domain.OperationResult<T>(true);
            }
            catch (Exception ex)
            {
                return new Domain.OperationResult<T>(false, new[] { "Error", ex.Message });
            }
        }
        public virtual T FindByKey(object key)
        {
            return DataContext.Set<T>()?.Find(key);
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return DataContext.Set<T>()?.Where(filter);
        }
        public virtual OperationResult<T> Save(T dto)
        {
            if (dto == null)
                return new Domain.OperationResult<T>(false, new[] { "dto is null. Save cancelled" });

            try
            {
                var saveRes = DoSave(dto);

                if (saveRes.OperationSucceeded)
                {
                    DataContext.SaveChanges();
                }

                return new OperationResult<T>(true);
            }
            catch (Exception ex)
            {
                return new Domain.OperationResult<T>(false, new[] { "Error", ex.Message });
            }
        }
        public virtual OperationResult<IEnumerable<T>> SaveMany(IEnumerable<T> dtos)
        {
            try
            {
                // Save all
                var compoundResult = dtos.Select(this.DoSave);

                // get error entities
                var errors = compoundResult.Where(x => !x.OperationSucceeded);

                // get saved entities
                var saved = compoundResult.Where(x => x.OperationSucceeded);

                // create single final result object
                var errorMsgs = new[] {$"Records with errors : {errors.Count()}"}.Concat(errors.SelectMany(x => x.Errors));
                var succeededMsgs = new[] {$"Records saved successfully : {saved.Count()}"};

                // Save transaction
                DataContext.SaveChanges();

                return new OperationResult<IEnumerable<T>>(!errors.Any(), errorMsgs, succeededMsgs, dtos);
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<T>>(false, new [] {"Critical Error", ex.Message});
            }
        }
    }
}