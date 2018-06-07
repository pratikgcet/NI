using System.Data.Entity;
using data = HuanHuan.Data;

namespace HuanHuan.Core
{
    public class SecuritiesRepository : GenericRepository<data.Securities>
    {
        public SecuritiesRepository(string connectionStringConfigSettingName)
            : base(connectionStringConfigSettingName)
        {
            //
        }

    }
}