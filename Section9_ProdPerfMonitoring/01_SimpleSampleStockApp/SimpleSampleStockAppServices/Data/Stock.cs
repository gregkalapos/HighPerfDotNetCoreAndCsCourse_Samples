using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSampleStockAppServices.Data
{
    public class Stock
    {
        [Key]
        public string Name { get; set; }

        public string Symbol{ get; set; }

        public string Currency { get; set; }
    }
}
