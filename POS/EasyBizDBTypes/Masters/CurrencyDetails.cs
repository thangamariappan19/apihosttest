using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace EasyBizDBTypes.Masters
{
	[DataContract]
	[Serializable]
	public class CurrencyDetails
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public int CurrencyID { get; set; }
		[DataMember]
		public string CurrencyCode { get; set; }
		[DataMember]
		public decimal CurrencyValue { get; set; }
        [DataMember]
        public decimal PaymentValue { get; set; }
        [DataMember]
        public decimal TotalValue { get; set; }
		[DataMember]
		public string Remarks { get; set; }
	}
}
