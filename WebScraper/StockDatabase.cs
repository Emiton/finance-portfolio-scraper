using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public class StockDatabase : DbContext
    {
        public DbSet<StockObject> StockObjects { get; set; }
    }
}
