using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuanHuan.Domain;

namespace HuanHuan.Services.Interfaces
{
    public interface IDataLoaderService
    {
        OperationResult<T> LoadFromFile<T>(string filepath, DataType requestDataType);
    }
}
