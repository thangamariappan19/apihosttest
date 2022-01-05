using EasyBizAbsDAL.Common;
using EasyBizRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseStoreGroupMasterDAL : BaseDAL
    {
        public abstract SelectStoreGroupLookUpResponse SelectStoreGroupLookUp(SelectStoreGroupLookUpRequest ObjRequest);
        public abstract SelectAllStoreGroupDetailsResponse SelectAllStoreGroupDetails(SelectAllStoreGroupDetailsRequest ObjRequest);
        public abstract SelectStoreGroupDetailsResponse SelectStoreGroupDetails(SelectStoreGroupDetailsRequest ObjRequest);
        public abstract SelectAllStoreGroupResponse API_SelectALL(SelectAllStoreGroupRequest requestData);
    }
}
