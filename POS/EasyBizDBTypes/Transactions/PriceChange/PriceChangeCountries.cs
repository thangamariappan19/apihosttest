using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.PriceChange
{
	public class PriceChangeCountries
	{
		public int ID { get; set; }
		public int HeaderID { get; set; }
		public bool Select { get; set; }
		public int CountryID { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public bool PricePointApplicable { get; set; }
		public int CurrencyID { get; set; }
		public string CurrencyCode { get; set; }
	}
}
