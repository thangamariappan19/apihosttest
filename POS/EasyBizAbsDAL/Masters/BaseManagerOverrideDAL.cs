using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.ManagerOverrideResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseManagerOverrideDAL : BaseDAL
    {
       public abstract SelectManagerOverrideLookUpResponse SelectManagerOverrideLookUp(SelectManagerOverrideLookUpRequest ObjRequest);
        public abstract SelectAllManagerOverrideResponse API_SelectAllManagerOverride(SelectAllManagerOverrideRequest objRequest);
    }
}
