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
	public class SelectByIDPriceChangeResponse : BaseResponseType
	{
		[DataMember]
		public EasyBizDBTypes.Transactions.PriceChange.PriceChange PriceChangeRecord { get; set; }
	}
}
