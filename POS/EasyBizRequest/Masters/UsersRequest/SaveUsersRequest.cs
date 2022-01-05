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
    public class SaveUsersRequest :BaseRequestType
    {
        [DataMember]
        public UsersSettings UsersRecord { get; set; }
    }
}
