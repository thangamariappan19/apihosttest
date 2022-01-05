using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.Invoice
{
    [DataContract]
    [Serializable]
    public class GetSearchInvoiceHeaderDetailsResponse : BaseResponseType
    {
        [DataMember]
        public InvoiceHeader InvoiceHeaderDetailsList { get; set; }
        [DataMember]
        public List<PaymentDetail> PaymentList { get; set; }

    }
}
