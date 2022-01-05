using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.POS.OnAccountPaymentResponse
{
    [DataContract]
    [Serializable]
    public class GetOnAccountPaymentPendingResponse :BaseResponseType
    {
        [DataMember]
        public List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList { get; set; }
    }
}
