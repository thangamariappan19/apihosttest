using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ArmadaCollectionsMasterRequest;
using EasyBizResponse.Masters.ArmadaCollectionsMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseArmadaCollectionMasterDAL : BaseDAL
    {
        public abstract SelectArmadaCollectionLookUpResponse SelectArmadaCollectionLookUp(SelectArmadaCollectionLookUpRequest ObjRequest);
    }
}
