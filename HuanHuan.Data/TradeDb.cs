using System.ComponentModel.DataAnnotations;

namespace HuanHuan.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TradeDb : DbContext
    {
        // Your context has been configured to use a 'TradeDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HuanHuan.Data.TradeDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TradeDb' 
        // connection string in the application configuration file.
        public TradeDb()
            : base("name=TradeDb")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Stock> StocksSet { get; set; }
        public virtual DbSet<Securities> SecuritiesSet { get; set; }
    }

    public class Stock
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal? Price { get; set; }
    }

    public class Securities
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string SecuritiesType { get; set; }       
    }
}