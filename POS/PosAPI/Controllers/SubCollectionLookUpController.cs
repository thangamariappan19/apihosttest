using EasyBizBLL.Masters;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizResponse.Masters.SubCollectionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class SubCollectionLookUpController : ApiController
    {
        public IHttpActionResult GetSubCollectionLookUp(int CollectionID)
        {
            try
            {
                var RequestData = new SelectSubCollectionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CollectionID = CollectionID;
                SelectSubCollectionLookUpResponse response = null;
                var ResponseData = new SubCollectionBLL();
                response = ResponseData.SelectAllLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
