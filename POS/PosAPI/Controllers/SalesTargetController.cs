using EasyBizBLL.SalesTargetBLL;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizRequest.SalesTargetRequest;
using EasyBizResponse.SalesTargetResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SalesTargetController : ApiController
    {
        public IHttpActionResult GetSalesTargetList()
        {
            try
            {
                var RequestData = new SelectAllSalesTargetRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllSalesTargetResponse response = null;
                var ResponseData = new SalesTargetBLL();
                response = ResponseData.SelectAllDocumentNumberingMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostSalesTarget(SalesTargetHeader _objsalesTarget)
        {
            try
            {
                var RequestData = new SaveSalesTargetRequest();
                
                RequestData.SalesTargetHeaderRecord = new SalesTargetHeader();
                RequestData.SalesTargetHeaderRecord = _objsalesTarget;
                RequestData.SalestargetDetailsList = _objsalesTarget.SalestargetDetails;
                RequestData.SalesTargetHeaderRecord.CreateOn = DateTime.Now;
                RequestData.SalesTargetHeaderRecord.SCN = 0;
                SaveSalesTargetResponse response = null;
                var ResponseData = new SalesTargetBLL();
                response = ResponseData.SaveDocumentNumberingMaster(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
