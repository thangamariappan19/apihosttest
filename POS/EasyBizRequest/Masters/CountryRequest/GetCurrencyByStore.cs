using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CountryRequest
{
    [DataContract]
    [Serializable]
   public class GetCurrencyByStoreRequest : BaseRequestType
    {
       [DataMember]
       public int ID { get; set; }
    }
}
