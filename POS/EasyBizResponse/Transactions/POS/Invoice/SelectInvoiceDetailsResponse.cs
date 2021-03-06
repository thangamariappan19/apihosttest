using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.Invoice
{
    [Serializable]
    [DataContract]
   public class SelectInvoiceDetailsResponse : BaseResponseType
    {

        [DataMember]
        public List<InvoiceDetails> InvoiceDetailData { get; set; }
    }
}
