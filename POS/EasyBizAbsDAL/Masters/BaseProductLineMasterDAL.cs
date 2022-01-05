using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizResponse.Masters.ProductLineMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseProductLineMasterDAL : BaseDAL
    {
        public abstract SelectProductLineLookUpResponse SelectProductLineLookUp(SelectProductLineLookUpRequest ObjRequest);
        public abstract SelectAllProductLineMasterResponse API_SelectALL(SelectAllProductLineMasterRequest requestData);
    }
}
