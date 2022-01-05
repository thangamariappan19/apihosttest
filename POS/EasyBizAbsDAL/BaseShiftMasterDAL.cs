using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.ShiftResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseShiftMasterDAL : BaseDAL
    {
        public abstract SelectByCountryIDResponse SelectCountryByID(SelectByCountryIDRequest ObjRequest);

        public abstract SelectShiftLookUpResponse SelectShiftLookUp(SelectShiftLookUpRequest ObjRequest);

        public abstract SelectShiftListForCategoryResponse SelectShiftListByCountry(SelectShiftListForCategoryRequest RequestObj);
    }
}
