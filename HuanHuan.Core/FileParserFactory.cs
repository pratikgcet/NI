using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuanHuan.Core.Interfaces;
using HuanHuan.Domain;

namespace HuanHuan.Core
{
    public class FileParserFactory : IFileParserFactory
    {
        public IFileParser<T> GetFileParser<T>(DataType fileType)
        {
            return null;
        }
    }
}
