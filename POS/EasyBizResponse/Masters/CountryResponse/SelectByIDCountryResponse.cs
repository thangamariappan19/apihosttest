using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.CountryResponse
{
    [DataContract]
    [Serializable]
   public class SelectByIDCountryResponse:BaseResponseType
    {
        [DataMember]
        public CountryMaster CountryMasterRecord { get; set; }
    }
}
