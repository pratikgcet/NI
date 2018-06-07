using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using CsvHelper;
using HuanHuan.Core.Interfaces;
using HuanHuan.Domain.DomainModel;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace HuanHuan.Core
{
    public class StockPriceDataFileParser : BaseFileParser<Stock>, IFileParser<Stock>//,IValidator
    {
        private List<Stock> stocks;
        public StockPriceDataFileParser()
        {
            stocks = new List<Stock>();

        }
        public IEnumerable<Stock> Parse(string filepath)
        {
            return base.DoParse(filepath);
        }

        protected override IEnumerable<Stock> ReadDataFromCsv(string filepath)
        {

            using (TextReader reader = File.OpenText(@"/Users/bart/Downloads/Work.csv"))
            {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ";";
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    Stock Record = csv.GetRecord<Stock>();
                    if (Validate(Record))
                        stocks.Add(Record);
                }
            }

            return stocks;
        }

        private bool Validate(Stock record)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerable<Stock> ReadDataFromJson(string filepath)
        {
            string jsonString = File.ReadAllText(filepath);

            var records = new JavaScriptSerializer().Deserialize<IEnumerable<Stock>>(jsonString);
            foreach (var st in records)
            {
                if (Validate(st))
                    stocks.Add(st);
            }

            return stocks;

        }

        protected override IEnumerable<Stock> ReadDataFromXml(string filepath)
        {
            XElement xml = XElement.Load(filepath);
            foreach (XElement child in xml.Elements())
            {
                //XElement date = child.Element("date");
                //XElement close = child.Element("close");
                //XElement name = child.Element("name");

                //var date = date?.Value;
                //string val = row?.Value;
                // Process child XElement
            }

            return null;
        }
    }
}