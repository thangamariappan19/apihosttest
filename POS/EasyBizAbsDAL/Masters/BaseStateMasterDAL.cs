using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.StateMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseStateMasterDAL : BaseDAL
    {
        public abstract SelectStateLookUpResponse SelectStateLookUp(SelectStateLookUpRequest ObjRequest);

        public abstract SelectStateAloneLookUPResponse SelectStateAloneLookup(SelectStateAloneLookUPRequest objRequest);
        public abstract SelectAllStateResponse API_SelectAll(SelectAllStateRequest objRequest);
    }
}
