using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [Serializable]
    [DataContract]
    public class CustomerViewTransactionTypes
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public DateTime CreateOn { get; set; }
        [DataMember]
        public string InvoiceNo { get; set; }
        [DataMember]
        public decimal NetAmount { get; set; }
        [DataMember]
        public decimal ReceivedAmount { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public decimal DueAmount { get; set; }
        [DataMember]
        public decimal ReturnAmount { get; set; }
    }
}