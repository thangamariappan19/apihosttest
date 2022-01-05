using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizResponse.Masters.PrevilegesResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BasePrevilegesDAL : BaseDAL
    {
        public abstract SelectRoleLookupResponse SelectRoleLookUp(SelectRoleLookupRequest ObjRequest);
        public abstract GetScreenNamesResponse SelectPOSScreenNameLookUp(GetScreenNamesRequest ObjRequest);
        public abstract SelectPrevilegesLookUpResponse SelectUserPrivilagesLookUp(SelectPrevilegesLookUpRequest ObjRequest);
        
    }
}
