using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PaymentTypeSettingResponse
{
    [DataContract]
    [Serializable]
   public class SelectPaymentTypeLookUpResponse : BaseResponseType
    {
        [DataMember]
        public List<PaymentTypeMasterType> PaymentTypeList { get; set; }
    }
}
