using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BasePosMasterDAL:BaseDAL
    {
        public abstract SelectPosMasterLookUpResponse SelectPosMasterLookUp(SelectPosMasterLookUpRequest RequestObj);
        public abstract SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest RequestObj);
        public abstract SelectStoreGroupLookUpResponse SelectStoreGroupLookUp(SelectStoreGroupLookUpRequest RequestObj);

        public abstract SelectStoreMasterLookUpResponse SelectStoreBasedOnStoreGroupandCountryMasterLookUp(SelectStoreMasterLookUpRequest RequestObj);
        public abstract SelectAllPosMasterResponse API_SelectALL(SelectAllPosMasterRequest requestData);
    }
}
