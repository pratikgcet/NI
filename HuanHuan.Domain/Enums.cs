using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanHuan.Domain
{
    public enum DataType
    {
        StockPrice,
        Securities,
        Undefined
    }

    public enum FileType
    {
        Undefined,
        Csv,
        Xml,
        Json
    }
}
