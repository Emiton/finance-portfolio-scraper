using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    /// <summary>
    ///     This class will hold all of the data for a given stock
    /// </summary>
    public class StockObject
    {
        [Key]
        [Column(Order = 1)]
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string ValueChange { get; set; }
        public string PercentChange { get; set; }
        public string Currency { get; set; }
        // Time of the market at the time the data was collected
        public string MarketTime { get; set; }
        public string Volume { get; set; }
        // Average volume over the last 3 months
        public string AverageVolume3M { get; set; }
        public string MarketCap { get; set; }
        // Time at which the stock was scraped
        [Key]
        [Column(Order = 2)]
        public string ScrapeTime { get; set; }
    }
}
