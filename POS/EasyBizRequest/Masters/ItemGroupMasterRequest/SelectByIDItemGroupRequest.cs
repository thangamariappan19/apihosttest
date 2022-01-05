using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.ItemGroupMasterRequest
{
    [DataContract]
    [Serializable]
    public class SelectByIDItemGroupRequest : BaseRequestType
    {
        [DataMember]
        public int ID { get; set; }
    }
}
