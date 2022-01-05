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
    public class SelectCityLookUPRequest : BaseRequestType
    {
        [DataMember]
        public int StateID { get; set; }
    }
}
