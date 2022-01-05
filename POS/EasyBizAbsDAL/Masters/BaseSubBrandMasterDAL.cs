using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseSubBrandMasterDAL : BaseDAL
    {
        public abstract  SelectSubBrandLookUpResponse SelectSubBrandLookUp(SelectSubBrandLookUpRequest ObjRequest);
        public abstract SelectSubBrandListForCategoryResponse SelectSubBrandListByBrand(SelectSubBrandListForCategoryRequest RequestObj);
        public abstract SelectAllSubBrandResponse API_SelectALL(SelectAllSubBrandRequest requestData);
    }
}
