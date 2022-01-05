using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.EmployeeDiscountInfoRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.EmployeeDiscountInfoResponse;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseEmployeeMasterDAL:BaseDAL
    {
       public abstract SelectEmployeeLookUpResponse SelectCountryLookUp(SelectEmployeeLookUpRequest ObjRequest);

       public abstract SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest objRequest);

       public abstract SelectEmployeeDiscountInfoResponseByCustCode SelectEmployeediscountInfoByCustCode(SelectEmployeeDiscountInfoByCustCode objRequest);

       public abstract SelectEmployeeLookUpResponse GetEmployeeByStore(GetEmployeeByStoreRequest objRequest);

        public abstract SelectAllEmployeeMasterResponse SelectSalesEmployeeForPOS(SelectAllEmployeeMasterRequest objRequest);
        public abstract SelectAllEmployeeMasterResponse API_SelectALL(SelectAllEmployeeMasterRequest requestData);
        public abstract SelectAllEmployeeMasterResponse API_SelectFilterData(SelectCountryStoreFilterEmployeeMaster requestData);

    }
}
