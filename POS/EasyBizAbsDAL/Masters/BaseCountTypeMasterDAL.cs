using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CountTypeMasterRequest;
using EasyBizResponse.Masters.CountTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   
    public abstract class BaseCountTypeMasterDAL : BaseDAL
    {
        public abstract SelectCountTypeMasterLookUpResponse SelectCountTypeMasterLookUp(SelectCountTypeMasterLookUpRequest RequestObj);
    }
}
