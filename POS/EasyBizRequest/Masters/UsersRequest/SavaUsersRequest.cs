using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Masters;

namespace EasyBizRequest.Masters.UsersRequest
{
    [DataContract]
    [Serializable]
    public class SavaUsersRequest :BaseRequestType
    {
        [DataMember]
        public Users UsersRecord { get; set; }
    }
}
