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
	public class SelectPriceChangeDetailsResponse : BaseResponseType
	{
		[DataMember]
		public List<PriceChangeDetails> PriceChangeDetailsList { get; set; }
	}
}
