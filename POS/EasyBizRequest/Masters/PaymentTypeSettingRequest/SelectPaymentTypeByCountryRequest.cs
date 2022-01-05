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
   public class SelectPaymentTypeByCountryRequest : BaseRequestType
    {

       [DataMember]
       public int CountryID { get; set; }
       [DataMember]
       public string PaymentCode { get; set; }
    }
}
