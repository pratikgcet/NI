using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HuanHuan.Domain;
using HuanHuan.Domain.DomainModel;
using HuanHuan.Services.Interfaces;

namespace HuanHuan.Controllers
{
    public class DataLoadController : ApiController
    {
        private readonly IDataLoaderService _dataLoaderService;

        public DataLoadController(IDataLoaderService dataLoaderService)
        {
           // handle dataLoaderService null ref
            _dataLoaderService = dataLoaderService;
        }

        public void Load(string filepath, DataType dataType)
        {
            try
            {
                if (dataType == DataType.StockPrice)
                    _dataLoaderService.LoadFromFile<Stock>(filepath);

                else if (dataType == DataType.Securities)
                    _dataLoaderService.LoadFromFile<Securities>(filepath);
            }
            catch (Exception ex)
            {
                // Log
            }
           
        }
    }
}
