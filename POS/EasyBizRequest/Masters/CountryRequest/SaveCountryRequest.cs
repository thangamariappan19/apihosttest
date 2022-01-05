using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CountryRequest
{
    [Serializable]
    [DataContract]
   public class SaveCountryRequest:BaseRequestType
    {
        [DataMember]
        public CountryMaster CountryMasterData { get; set; }
    }
}
