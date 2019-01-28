﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public class StockObject
    {
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public double ValueChange { get; set; }
        public double PercentChange { get; set; }
        public string Currency { get; set; }
        // Time of the market at the time the data was collected
        public string MarketTime { get; set; }
        public string Volume { get; set; }
        // Average volume over the last 3 months
        public string AverageVolume3M { get; set; }
        public string MarketCap { get; set; }
    }
}