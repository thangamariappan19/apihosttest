using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizResponse.Masters.ScaleMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseScaleMasterDAL : BaseDAL
    {
        
        public abstract SelectScaleLookUpResponse SelectScaleLookUp(SelectScaleLookUpRequest ObjRequest);
        public abstract SelectAllScaleDetailsResponse SelectAllScaleDetails(SelectAllScaleDetailsRequest ObjRequest);
        public abstract SelectScaleDetailsResponse SelectScaleDetails(SelectScaleDetailsRequest ObjRequest);
        public abstract SelectScaleDetailsResponse SelectScaleBrandDetails(SelectScaleDetailsRequest ObjRequest);
        public abstract SelectScaleDetailsLookUpResponse SelectScaleDetailsLookUp(SelectScaleDetailsLookUpRequest ObjRequest);
        public abstract SelectAllScaleResponse API_SelectALL(SelectAllScaleRequest requestData);
    }
}
