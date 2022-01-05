using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizResponse.Masters.DivisionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseDivisionMasterDAL : BaseDAL
    {
        public abstract SelectDivisionLookUpResponse SelectDivisionLookUp(SelectDivisionLookUpRequest ObjRequest);

        public abstract SelectAllDivisionResponse API_SelectALL(SelectAllDivisionRequest requestData);
    }
}
