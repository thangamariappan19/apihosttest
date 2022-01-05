using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseBrandMasterDAL : BaseDAL
    {
       public abstract SelectBrandLookUpResponse SelectBrandLookUp(SelectBrandLookUpRequest ObjRequest);
        public abstract SelectAllBrandResponse API_SelectALL(SelectAllBrandRequest requestData);
        public abstract SelectBrandLookUpResponse API_SelectBrandMasterLookUp(SelectBrandLookUpRequest requestData);
    }
}
