using EasyBizBLL.Masters;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizResponse.Masters.DocumentTypeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class DocumentTypeController : ApiController
    {
        public IHttpActionResult GetDocumentType()
        {
            try
            {
                SelectDocumentLookUpRequest RequestData = new SelectDocumentLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                SelectDocumentLookUpResponse response = null;
                var ResponseData = new DocumentTypeBLL();
                response = ResponseData.SelectDocumentLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
