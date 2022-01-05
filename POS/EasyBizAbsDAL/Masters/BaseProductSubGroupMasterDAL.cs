using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseProductSubGroupMasterDAL : BaseDAL
    {
        public abstract SelectProductSubGroupLookUpResponse SelectProductSubGroupLookUp(SelectProductSubGroupLookUpRequest ObjRequest);
        public abstract SelectProductGroupListForProductSubGroupResponse SelectProductSubGroupListByProductGroup(SelectProductGroupListForProductSubGroupRequest RequestObj);
    }
}
