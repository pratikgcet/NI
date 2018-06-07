using data = HuanHuan.Data;

namespace HuanHuan.Core
{
    public class StockRepository : GenericRepository<data.Stock>
    {
        public StockRepository(string connectionStringConfigSettingName)
            : base(connectionStringConfigSettingName)
        {
            //
        }
    }
}
