using System.Collections.Generic;
using System.IO;
using System.Xml;
using HuanHuan.Core.Interfaces;
using HuanHuan.Domain.DomainModel;

namespace HuanHuan.Core
{
    public class SecuritiesDataFileParser : BaseFileParser<Securities>, IFileParser<Securities>
    {
        public IEnumerable<Securities> Parse(string filepath)
        {
            return base.DoParse(filepath);
        }

        protected override IEnumerable<Securities> ReadDataFromCsv(string filepath)
        {
            throw new System.NotImplementedException();
        }
        protected override IEnumerable<Securities> ReadDataFromJson(string filepath)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerable<Securities> ReadDataFromXml(string filepath)
        {
            throw new System.NotImplementedException();
        }
    }
}