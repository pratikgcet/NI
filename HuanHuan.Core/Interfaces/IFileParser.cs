using System.Collections.Generic;

namespace HuanHuan.Core.Interfaces
{
    public interface IFileParser<T>
    {
        IEnumerable<T> Parse(string filepath);
    }
}