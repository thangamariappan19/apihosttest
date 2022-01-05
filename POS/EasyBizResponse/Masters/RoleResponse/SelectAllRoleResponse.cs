using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Masters.RoleResponse
{
    [DataContract]
    [Serializable]
   public class SelectAllRoleResponse:BaseResponseType
    {
        [DataMember]
        public List<RoleMaster> RoleMasterList { get; set; }
    }
}
