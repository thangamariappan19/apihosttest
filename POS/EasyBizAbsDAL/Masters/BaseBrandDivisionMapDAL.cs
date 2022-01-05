using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseBrandDivisionMapDAL : BaseDAL
    {
       public abstract SelectBrandDivisionMapLookUpResponse SelectBrandDivisionMapLookUp(SelectBrandDivisionLookUpRequest ObjRequest);

       public abstract SelectBrandDivListforCategoryResponse SelectBrandDivisionListByBrand(SelectBrandDivListforCategoryRequest RequestObj);
    }
}
