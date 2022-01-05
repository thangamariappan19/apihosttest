using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseProductGroupDAL:BaseDAL
    {
       public abstract SelectProductGroupLookUpResponse SelectProductGroupLookUp(SelectProductGroupLookUpRequest ObjRequest);
        public abstract SelectAllProductGroupResponse API_SelectALL(SelectAllProductGroupRequest requestData);
    }
}
