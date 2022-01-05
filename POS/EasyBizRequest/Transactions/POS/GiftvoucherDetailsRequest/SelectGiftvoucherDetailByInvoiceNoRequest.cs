using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.GiftvoucherDetailsRequest
{
    [DataContract]
    [Serializable]
   public class SelectGiftvoucherDetailByInvoiceNoRequest:BaseRequestType
    {
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
