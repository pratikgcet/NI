using System;
using System.Data.Entity;

namespace HuanHuan.Core
{
    public class BaseRepository
    {
        private DbContext _dataContext;
        private readonly string _connectionStringConfigSettingName;

        protected DbContext DataContext
        {
            get
            {
                if (_dataContext == null)                
                    _dataContext = new DbContext(_connectionStringConfigSettingName);

                return _dataContext;
            }
        }

        // == Ctor
        protected BaseRepository(string connectionStringConfigSettingName)
        {
            if(string.IsNullOrWhiteSpace(connectionStringConfigSettingName))
                throw new ArgumentNullException(nameof(connectionStringConfigSettingName));

            _connectionStringConfigSettingName = connectionStringConfigSettingName;
        }
    }
}