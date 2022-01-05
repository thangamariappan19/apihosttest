using EasyBizDBTypes.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Tailoring
{
	[DataContract]
	[Serializable]
	public class SelectTailoringOrderDetailsResponse:BaseResponseType
	{
		[DataMember]
		public List<TailoringOrderDetails> TailoringOrderDetailsList { get; set; }
	}
}
