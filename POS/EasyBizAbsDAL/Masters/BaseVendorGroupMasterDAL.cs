using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseVendorGroupMasterDAL : BaseDAL
    {
        public abstract SelectVendorGroupLookUpResponse SelectVendorGroupLookUp(SelectVendorGroupLookUpRequest RequestObj);
    }
}
