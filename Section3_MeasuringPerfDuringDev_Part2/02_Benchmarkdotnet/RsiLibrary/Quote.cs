using System;
using System.Collections.Generic;
using System.Text;

namespace _02_03_Demo1_PerfTips
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A simple quote to store a value with a date
    /// </summary>
    public class Quote
    {
        public DateTime Date { get; set; }
        public Decimal Value { get; set; }

        public Quote(DateTime date, Decimal value)
        {
            Date = date;
            Value = value;
        }

        public Quote()
        {

        }
    }

}
