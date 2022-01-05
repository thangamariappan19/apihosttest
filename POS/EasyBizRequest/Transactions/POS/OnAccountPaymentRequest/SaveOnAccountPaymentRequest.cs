using EasyBizDBTypes.Transactions.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Transactions.POS.OnAccountPaymentRequest
{
    [DataContract]
    [Serializable]
    public class SaveOnAccountPaymentRequest : BaseRequestType
    {
        [DataMember]
        public OnAccountPayment OnAccountPaymentRecord { get; set; }       
    }
}
