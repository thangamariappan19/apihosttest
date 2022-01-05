using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.RequestTypeMasterRequest;
using EasyBizResponse.Masters.RequestTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseRequestTypeMasterDAL:BaseDAL
    {
        public abstract SelectRequestTypeMasterLookUpResponse SelectRequestTypeMasterLookUp(SelectRequestTypeMasterLookUpRequest RequestObj);
    }
}
