using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.CityMasterRequest
{
    [DataContract]
    [Serializable]
    public class UpdateCityRequest : BaseRequestType
    {
        [DataMember]
        public CityMaster CityRecord { get; set; }
    }
}
