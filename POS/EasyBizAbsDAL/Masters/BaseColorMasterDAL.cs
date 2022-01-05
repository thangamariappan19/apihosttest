using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizResponse.Masters.ColorMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseColorMasterDAL : BaseDAL
    {
        public abstract SelectColorLookUpResponse SelectColorLookUp(SelectColorLookUpRequest ObjRequest);
        public abstract SelectAllColorResponse API_SelectALL(SelectAllColorRequest requestData);
    }
}
