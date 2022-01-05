using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PaymentModeMaterRequest
{
    [DataContract]
    [Serializable]
    public class UpdatePaymentModeMasterRequest:BaseRequestType
    {
        [DataMember]
        public PaymentModeTypes PaymentModeMasterData { get; set; }
        public Enums.OpStatusCode StatusCode { get; set; }
    }
}
