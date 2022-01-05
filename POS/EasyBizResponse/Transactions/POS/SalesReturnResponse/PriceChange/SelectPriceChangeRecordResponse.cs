using EasyBizDBTypes.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.PriceChange
{
	[DataContract]
	[Serializable]
	public class SelectPriceChangeRecordResponse : BaseResponseType
	{
		public EasyBizDBTypes.Transactions.PriceChange.PriceChange PriceChangeRecord { get; set; }
		public List<PriceChangeDetails> PriceChangeDetailsList { get; set; }
		public List<PriceChangeCountries> PriceChangeCountriesList { get; set; }
	}
}
