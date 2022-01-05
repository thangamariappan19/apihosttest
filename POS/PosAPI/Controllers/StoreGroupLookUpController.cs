using EasyBizBLL.Masters;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizResponse.Masters.StoreGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StoreGroupLookUpController : ApiController
    {

        public IHttpActionResult GetStoreGroupLookupOnStore(int CountryID)
        {
            try
            {
                SelectStoreGroupLookUpRequest RequestData = new SelectStoreGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = CountryID;
                SelectStoreGroupLookUpResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SelectStoreGroupMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
