using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizResponse.Masters.ProductLineMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class ProductLineController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProductLineList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllProductLineMasterRequest();

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



                SelectAllProductLineMasterResponse response = null;
                var ResponseData = new ProductLineMasterBLL();
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

        //select all
        public IHttpActionResult GetProductLineData()
        {
            try
            {
                var RequestData = new SelectAllProductLineMasterRequest();
                SelectAllProductLineMasterResponse response = null;
                var ResponseData = new ProductLineMasterBLL();
                response = ResponseData.SelectAllProductLineMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetProductLineDataByID(int ID)
        {
            try
            {
                var RequestData = new SelectByIDProductLineMasterRequest();
                RequestData.ID = ID;
                SelectByIDProductLineMasterResponse response = null;
                var ResponseData = new ProductLineMasterBLL();
                response = ResponseData.SelectProductLineMasterRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostProductLineMaster(ProductLineMaster _objProductLineMaster)
        {
            try
            {
                var RequestData = new SaveProductLineMasterRequest();
                RequestData.ProductLineMasterData = new ProductLineMaster();
                RequestData.ProductLineMasterData = _objProductLineMaster;
                SaveProductLineMasterResponse response = null;
                var ResponseData = new ProductLineMasterBLL();
                response = ResponseData.SaveProductLineMaster(RequestData);
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

        public IHttpActionResult PutProductLineMaster(ProductLineMaster _objProductLineMaster)
        {
            try
            {
                var RequestData = new UpdateProductLineMasterRequest();
                RequestData.ProductLineMasterData = new ProductLineMaster();
                RequestData.ProductLineMasterData = _objProductLineMaster;
                UpdateProductLineMasterResponse response = null;
                var ResponseData = new ProductLineMasterBLL();
                response = ResponseData.UpdateProductLineMaster(RequestData);
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
