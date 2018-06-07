using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HuanHuan.Domain;

namespace HuanHuan.Core.Interfaces
{
    public interface IRepository<T>
    {
        T FindByKey(object key);
        IQueryable<T> Get(Expression<Func<T, bool>> filter);
        OperationResult<T> Save(T dto);
        OperationResult<IEnumerable<T>> SaveMany(IEnumerable<T> dtos);
        OperationResult<T> DeleteByKey(object key);
    }
}