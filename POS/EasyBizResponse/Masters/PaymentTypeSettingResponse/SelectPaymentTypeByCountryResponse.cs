using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.PaymentTypeSettingResponse
{
    [Serializable]
    [DataContract]
   public class SelectPaymentTypeByCountryResponse : BaseResponseType
    {
        [DataMember]
        public PaymentTypeMasterType PaymentTypeCountryRecord { get; set; }
        [DataMember]
        public List<PaymentTypeMasterType> PaymentDetailsList { get; set; }
       
    }
}
