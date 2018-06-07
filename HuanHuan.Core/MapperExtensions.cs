using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dom = HuanHuan.Domain.DomainModel;
using data = HuanHuan.Data;


namespace HuanHuan.Core.Extensions
{
    public static class MapperExtensions
    {
        #region Domain To Data Model

        public static data.Securities MapToEntity(this dom.Securities domDto)
        {
            return new data.Securities(); // TODO:
        }

        public static data.Stock MapToEntity(this dom.Stock domDto)
        {
            return new data.Stock(); // TODO:
        }

        #endregion

        #region Data to Domain model

        public static dom.Securities MapToDomain(this data.Securities dataDto)
        {
            return new dom.Securities(); // TODO:
        }

        public static dom.Stock MapToDomain(this data.Stock dataDto)
        {
            return new dom.Stock(); // TODO:
        }

        #endregion
    }
}
