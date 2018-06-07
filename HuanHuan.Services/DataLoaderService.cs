using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HuanHuan.Core.Interfaces;
using HuanHuan.Domain;
using HuanHuan.Domain.DomainModel;
using HuanHuan.Services.Interfaces;
using data = HuanHuan.Data;
using HuanHuan.Core.Extensions;

namespace HuanHuan.Services
{
    public class DataLoaderService : IDataLoaderService
    {
        // == Fields 
        private readonly IFileParserFactory _fileParserFactory;
        private readonly IRepository<data.Stock> _stockRepository;
        private readonly IRepository<data.Securities> _securitiesRepository;

        // == Ctor
        public DataLoaderService(IFileParserFactory fileParserFactory,
            IRepository<data.Stock> stockRepository,
            IRepository<data.Securities> securitiesRepository)
        {
            if (fileParserFactory == null)
                throw new ArgumentNullException(nameof(fileParserFactory));

            if (stockRepository == null)
                throw new ArgumentNullException(nameof(stockRepository));

            if (securitiesRepository == null)
                throw new ArgumentNullException(nameof(securitiesRepository));

            _fileParserFactory = fileParserFactory;
            _stockRepository = stockRepository;
            _securitiesRepository = securitiesRepository;
        }

        // == Public methods
        public OperationResult<T> LoadFromFile<T>(string filepath, DataType requestDataType)
        {
            if (!File.Exists(filepath))
                return new OperationResult<T>(false, new[] { "Invalid File type" });
            // == Resolve file type
            if (requestDataType == DataType.Undefined)
                return new OperationResult<T>(false, new[] { "Invalid Data type" });

            // == Get parser
            var fileParser = _fileParserFactory.GetFileParser<T>(requestDataType);
            if (fileParser == null)
                return new OperationResult<T>(false, new[] { "Failed to parse file" });

            // == Parse
            var dataToLoad = fileParser.Parse(filepath);
            if (!dataToLoad?.Any() ?? false)
                return new OperationResult<T>(false, new[] { "0 records found in the file. No data loaded" });

            // == Save data
            var saveResult = SaveData<T>(dataToLoad);
            return new OperationResult<T>(saveResult.OperationSucceeded, saveResult.Errors, saveResult.Info);
        }


        private OperationResult SaveData<T>(IEnumerable<T> dtos)
        {
            if (typeof(T) == typeof(Stock))
            {
                var dataToSave = dtos.OfType<Stock>().Select(x => x.MapToEntity());
                var res = _stockRepository.SaveMany(dataToSave);

                return new OperationResult<Stock>(res.OperationSucceeded, res.Errors, res.Info);
            }

            if (typeof(T) == typeof(Securities))
            {
                var dataToSave = dtos.OfType<Securities>().Select(x => x.MapToEntity());
                var res = _securitiesRepository.SaveMany(dataToSave);

                return new OperationResult<Stock>(res.OperationSucceeded, res.Errors, res.Info);
            }

            return new OperationResult(false);
        }
    }
}
