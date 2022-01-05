using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.CardDetails
{
    [DataContract]
    [Serializable]
   public class SelectCreditCardDetailsByInvoiceNoResponse:BaseResponseType
    {
        public List<PaymentDetail> InvoiceNoCreditCardDetails { get; set; }
    }
}
