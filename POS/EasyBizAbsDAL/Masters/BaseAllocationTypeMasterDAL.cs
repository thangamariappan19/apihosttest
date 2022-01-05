using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.AllocationTypeMasterRequest;
using EasyBizResponse.Masters.AllocationTypeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseAllocationTypeMasterDAL:BaseDAL
    {
        public abstract SelectAllocationTypeMasterLookUpResponse SelectAllocationTypeMasterLookUp(SelectAllocationTypeMasterLookUpRequest RequestObj);
    }
}
