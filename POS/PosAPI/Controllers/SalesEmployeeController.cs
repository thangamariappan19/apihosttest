using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SalesEmployeeController : ApiController
    {
        public IHttpActionResult GetEmployeeMasterList(int StoreID)
        {
            try
            {
                var _EmployeeMasterBLL = new EmployeeMasterBLL();
                var RequestData = new SelectAllEmployeeMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                RequestData.StoreID = StoreID;
                var response = new SelectAllEmployeeMasterResponse();
                response = _EmployeeMasterBLL.SelectSalesEmployeeForPOS(RequestData);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}