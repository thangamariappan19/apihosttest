using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using EasyBizResponse.Masters.CollectionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCollectionMasterDAL : BaseDAL
    {
        public abstract SelectCollectionLookUpResponse SelectCollectionLookUp(SelectCollectionLookUpRequest ObjRequest);
        public abstract SelectAllCollectionMasterResponse API_SelectALL(SelectAllCollectionMasterRequest requestData);
    }
}
