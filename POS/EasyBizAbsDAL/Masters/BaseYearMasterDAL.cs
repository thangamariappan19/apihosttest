using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.YearMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
     public abstract class BaseYearMasterDAL: BaseDAL
    {
         public abstract SelectYearLookUpResponse SelectYearLookUp(SelectYearLookUpRequest RequestObj);
        public abstract SelectAllYearResponse API_SelectALL(SelectAllYearRequest requestData);
    }
}
