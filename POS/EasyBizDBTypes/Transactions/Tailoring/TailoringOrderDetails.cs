using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.Tailoring
{
	public class TailoringOrderDetails: BaseType
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public int TailoringOrderID { get; set; }
		[DataMember]
		public string SKUCode { get; set; }
		[DataMember]
		public int OrderQuantity { get; set; }
		[DataMember]
		public string TailoringRemarks { get; set; }
		[DataMember]
		public int AtTailor { get; set; }
		[DataMember]
		public int ReceivedFromTailor { get; set; }
        [DataMember]
        public int ReceivedTailor { get; set; }
		[DataMember]
		public int DeliveredQuantity { get; set; }
        [DataMember]
        public int AlreadyDeliveredQuantity { get; set; }
		[DataMember]
		public DateTime CustomerDeliveryDate { get; set; }
		[DataMember]
		public DateTime TailorDeliveryDate { get; set; }
		[DataMember]
		public string OrderStatus { get; set; }		
		public List<SKUMasterTypes> SKUMasterList { get; set; }

	}
}
