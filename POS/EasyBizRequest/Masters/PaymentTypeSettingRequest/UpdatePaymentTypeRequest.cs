using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PaymentTypeSettingRequest
{
   
    
    [DataContract]
    [Serializable]
    public class UpdatePaymentTypeRequest : BaseRequestType
    {
        [DataMember]

        public PaymentTypeMasterType PaymentTypeMasterData { get; set; }
    }
}
