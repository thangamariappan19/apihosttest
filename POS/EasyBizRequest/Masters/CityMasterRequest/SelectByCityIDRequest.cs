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
    public class SelectByCityIDRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
