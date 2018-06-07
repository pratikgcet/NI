using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuanHuan.Domain;

namespace HuanHuan.Core.Interfaces
{
    public interface IFileParserFactory
    {
        IFileParser<T> GetFileParser<T>(DataType dataType);
    }
}
