using EasyBizDBTypes.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.Tailoring
{
	[DataContract]
	[Serializable]
	public class SaveTailoringOrderRequest:BaseRequestType
	{
		[DataMember]
		public TailoringOrder TailoringOrderHeaderRecord { get; set; }
		[DataMember]
		public List<TailoringOrderDetails> TailoringOrderDetailsList { get; set; }
	}
}
