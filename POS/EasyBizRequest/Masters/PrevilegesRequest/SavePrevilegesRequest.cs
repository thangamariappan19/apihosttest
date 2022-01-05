using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.PrevilegesRequest
{
    [Serializable]
    [DataContract]
    public class SavePrevilegesRequestt : BaseRequestType
    {
        [DataMember]
        public UserPrivilagesTypes UserPrivilagesData { get; set; }
    }
}
