using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest
{
    [DataContract]
    [Serializable]
   public class SelectByInvoiceNoCashDetailsRequest:BaseRequestType
    {
        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public long InvoiceHeaderID { get; set; }
    }
}
