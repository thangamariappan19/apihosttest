using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.AgentMasterRequest;
using EasyBizResponse.Masters.AgentMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseAgentMasterDAL : BaseDAL
    {
        public abstract SelectAgentLookUpResponse SelectAgentLookUp(SelectAgentLookUpRequest ObjRequest);
    }
}
