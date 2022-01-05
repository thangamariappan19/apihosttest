using EasyBizDBTypes.Transactions.PriceChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.PriceChange
{
	[DataContract]
	[Serializable]
	public class SavePriceChangeRequest : BaseRequestType
	{
		[DataMember]
		public EasyBizDBTypes.Transactions.PriceChange.PriceChange PriceChangeRecord { get; set; }
		[DataMember]
		public List<PriceChangeDetails> PriceChangeDetailsList { get; set; }
		[DataMember]
		public List<PriceChangeCountries> PriceChangeCountriesList { get; set; } 
	}
}
