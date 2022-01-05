using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizRequest.Masters.RoleRequest
{
    [DataContract]
    [Serializable]
    public class SaveRoleRequest : BaseRequestType
    {
        [DataMember]
        public RoleMaster RoleMasterData { get; set; }
    }
}
