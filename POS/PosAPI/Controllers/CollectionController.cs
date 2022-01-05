using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    //[Authorize]
    public class CollectionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCollectionList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllCollectionMasterRequest();

                int lmt = 0, ofset = 0;
                int.TryParse(limit, out lmt);
                int.TryParse(offset, out ofset);
                lmt = lmt <= 0 ? 10 : lmt;
                ofset = ofset > 0 ? (ofset - 1) * lmt : 0;
                RequestData.Limit = lmt.ToString();
                RequestData.Offset = ofset.ToString();
                //RequestData.Limit = limit == null || limit == ""  ? "10" : limit;
                //RequestData.Offset = offset == null || offset == "" ? "0" : offset;
                RequestData.SearchString = searchString == null ? "" : searchString;
                RequestData.IsActive = isActive == null || isActive == "" ? "1" : isActive.ToLower() == "true" || isActive == "1" ? "1" : "0";



                SelectAllCollectionMasterResponse response = null;
                var ResponseData = new CollectionMasterBLL();
                response = ResponseData.API_SelectALL(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCollectionList()
        {
            try
            {
                var RequestData = new SelectAllCollectionMasterRequest();
                RequestData.ShowInActiveRecords = true;
                SelectAllCollectionMasterResponse response = null;
                var ResponseData = new CollectionMasterBLL();
                response = ResponseData.SelectAllCollectionMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetCollectionByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDCollectionMasterRequest();
                RequestData.ID = ID;
                SelectByIDCollectionMasterResponse response = null;
                var ResponseData = new CollectionMasterBLL();
                response = ResponseData.SelectByIDCollectionMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostCollection(CollectionMasterTypes _objCollection)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveCollectionMasterRequest();
                RequestData.CollectionMasterTypesRecord = new CollectionMasterTypes();
                RequestData.CollectionMasterTypesRecord = _objCollection;
                RequestData.CollectionMasterTypesRecord.CreateOn = DateTime.Now;
                RequestData.CollectionMasterTypesRecord.CreateBy = UserID;
                RequestData.CollectionMasterTypesRecord.SCN = 0;
                SaveCollectionMasterResponse response = null;
                var ResponseData = new CollectionMasterBLL();
                response = ResponseData.SaveCollectionMaster(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PutCollection(CollectionMasterTypes _objCollection)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new UpdateCollectionMasterRequest();
                RequestData.CollectionMasterTypesData = new CollectionMasterTypes();
                RequestData.CollectionMasterTypesData = _objCollection;
                RequestData.CollectionMasterTypesData.UpdateOn = DateTime.Now;
                RequestData.CollectionMasterTypesData.SCN = 0;
                RequestData.CollectionMasterTypesData.UpdateBy = UserID;
                UpdateCollectionMasterResponse response = null;
                var ResponseData = new CollectionMasterBLL();
                response = ResponseData.UpdateCollectionMaster(RequestData);
                //return Ok(response);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
