using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizResponse.Masters.RoleResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseRoleDAL:BaseDAL
    {
       public abstract SelectRoleMasterLookUpResponse SelectRoleLookUp(SelectRoleMasterLookUpRequest ObjRequest);
        public abstract SelectAllRoleResponse SelectAllRoleMasterLookUp(SelectAllRoleRequest objRequest);
        public abstract SelectAllRoleResponse API_SelectALL(SelectAllRoleRequest requestData);
    }
}
