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
	public class SaveDespatchToTailorRequest : BaseRequestType
	{
		[DataMember]
		public List<TailoringOrder> TailoringOrderList { get; set; }
		[DataMember]
		public string TailoringUnitCode { get; set; } 
		[DataMember]
		public DateTime TailorDeliveryDate { get; set; }
	}
}
