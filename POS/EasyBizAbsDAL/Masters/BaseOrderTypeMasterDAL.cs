using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.OrderTypeMasterRequest;
using EasyBizResponse.Masters.OrderTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseOrderTypeMasterDAL : BaseDAL
    {
        public abstract SelectOrderTypeMasterLookUpResponse SelectOrderTypeMasterLookUp(SelectOrderTypeMasterLookUpRequest RequestObj);
    }


}
