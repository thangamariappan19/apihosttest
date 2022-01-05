using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Tailoring
{
	[DataContract]
	[Serializable]
	public class TailoringOrder: BaseType
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public string DocumentNo { get; set; }
		[DataMember]
		public DateTime DocumentDate { get; set; }
		[DataMember]
		public string StoreCode { get; set; }
		[DataMember]
		public string CustomerCode { get; set; }
		[DataMember]
		public DateTime ExpectedDeliveryDate { get; set; }
		 
		public string OrderStatus { get; set; }
		public string TailoringUnitCode { get; set; }
		public bool DeliveredToTailor { get; set; }
		public List<TailoringOrderDetails> TailoringOrderDetailsList { get; set; }
		
	}
}
