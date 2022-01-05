using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizResponse.Masters.ReasonMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   
   
    public abstract class BaseReasonMasterDAL : BaseDAL
    {
        public abstract SelectReasonMasterLookUpResponse SelectReasonMasterLookUp(SelectReasonMasterLookUpRequest RequestObj);
        public abstract SelectReasonMasterLookUpResponse API_SelectReasonMasterLookUp(SelectReasonMasterLookUpRequest objRequest);

        public abstract SelectAllReasonMasterResponse API_SelectALL(SelectAllReasonMasterRequest requestData);
    }
}
