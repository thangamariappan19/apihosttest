using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Transactions.POS
{
    [DataContract]
    [Serializable]
   public class GiftvoucherDetail:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime ApplicationDate { get; set; }
        [DataMember]
        public int InvoiceHeaderID { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string  GiftvoucherCode {get;set;}
        [DataMember]
        public Decimal Amount { get;set; }
        [DataMember]
        public string PayMentMode { get; set; }
    }
}
