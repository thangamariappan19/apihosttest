using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.FreightMasterRequest;
using EasyBizResponse.Masters.FreightMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseFreightMasterDAL : BaseDAL
    {
        public abstract SelectFreightMasterLookUpResponse SelectFreightMasterLookUp(SelectFreightMasterLookUpRequest RequestObj);
    }
}
