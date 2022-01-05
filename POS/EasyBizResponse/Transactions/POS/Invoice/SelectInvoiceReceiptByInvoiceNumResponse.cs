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
    public class SelectInvoiceReceiptByInvoiceNumResponse : BaseResponseType
    {
        [DataMember]
        public List<InvoiceReceiptTypes> InvoiceList { get; set; }
        [DataMember]
        public List<InvoiceSubReceiptTypes> InvoiceSubReceiptTList { get; set; }
        [DataMember]
        public List<ApprovalNumReceipt> ApprovalNumReceiptList { get; set; }
    }
}
