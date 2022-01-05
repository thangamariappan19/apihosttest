using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Reports.DayWiseTransaction
{
    [DataContract]
    [Serializable]
    public class InvoicePaymentTransaction : BaseType
    {
        [DataMember]
        public string PaymentType { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public String  ApprovalNumber { get; set; }
    }
}
