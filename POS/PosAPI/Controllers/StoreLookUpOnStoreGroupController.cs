using EasyBizBLL.Masters;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class StoreLookUpOnStoreGroupController : ApiController
    {
        //store MASter Lookup Based on StoreGrroup ID
        public IHttpActionResult GetStoreLookupOnStoreGroup(int StoreGroupID)
        {
            try
            {
                SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StoreGroupID = Convert.ToInt32(StoreGroupID);
                SelectStoreMasterLookUpResponse response = null;
                var ResponseData = new PosMasterBLL();
                response = ResponseData.SelectStoreMasterLookUp(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
