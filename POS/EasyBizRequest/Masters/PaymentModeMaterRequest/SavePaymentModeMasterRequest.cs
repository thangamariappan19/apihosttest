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
    public class SavePaymentModeMasterRequest: BaseRequestType
    {
        [DataMember]
        public PaymentModeTypes PaymentModeTypesData { get; set; }
    }
}
