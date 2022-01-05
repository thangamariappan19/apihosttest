using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse
{
    [DataContract]
    [Serializable]
   public class SelectByInvoiceNoCashDetailsResponse:BaseResponseType
    {
        [DataMember]
        public List<PaymentDetail> InvoiceNoCashDetails { get; set; }
    }
}
