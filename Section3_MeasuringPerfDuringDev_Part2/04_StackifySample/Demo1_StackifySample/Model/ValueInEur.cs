using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1_StackifySample.Model
{
	public class ValueInEur {
		public String CurrencyName { get; set; }
		public Decimal ValueEur { get; set; }

		public Decimal ValueInOrigCurrency { get; set; }
	}
}
