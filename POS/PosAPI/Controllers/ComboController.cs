using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizRequest.Masters.ComboOfferRequest;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Masters.ComboOfferResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizDBTypes.Masters;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizResponse.Masters.PriceListResponse;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class ComboController : ApiController
    {
        [HttpGet]

        public IHttpActionResult GetComboOfferList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllComboOfferRequest();

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


                RequestData.ShowInActiveRecords = true;
                SelectAllComboOfferResponse response = null;
                var ResponseData = new ComboOfferMasterBLL();
                response = ResponseData.SelectAllComboOfferRecords(RequestData);
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

        public IHttpActionResult SaveComboOffer(SaveComboOfferRequest ComboOfferView)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SaveComboOfferRequest();
                RequestData.ComboOfferRecord = new ComboOfferMaster();
                RequestData.ComboOfferRecord.ComboOfferDetailsList = ComboOfferView.ComboOfferRecord.ComboOfferDetailsList;
                RequestData.ComboOfferRecord.ID = ComboOfferView.ComboOfferRecord.ID;
                RequestData.ComboOfferRecord.DocumentDate = ComboOfferView.ComboOfferRecord.DocumentDate;
                RequestData.ComboOfferRecord.ProductBarcode = ComboOfferView.ComboOfferRecord.ProductBarcode;
                RequestData.ComboOfferRecord.ProductSKUCode = ComboOfferView.ComboOfferRecord.ProductSKUCode;
                //RequestData.ComboOfferRecord.ProductStylecode = _IComboOfferView.ProductStylecode;
                RequestData.ComboOfferRecord.CreateBy = UserID;
                RequestData.ComboOfferRecord.CreateOn = DateTime.Now;
                RequestData.ComboOfferRecord.Active = ComboOfferView.ComboOfferRecord.Active;
                RequestData.ComboOfferRecord.SCN = 1;//_IComboOfferView.SCN;
                RequestData.CPOStyleDetailsRecords = ComboOfferView.CPOStyleDetailsRecords;
                RequestData.PriceListTypes = ComboOfferView.PriceListTypes;
                SaveComboOfferResponse response = null;
                var ResponseData = new ComboOfferMasterBLL();
                response = ResponseData.SaveComboOffer(RequestData);
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

        public IHttpActionResult GetAllSKUDetails(string itemcode)
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = itemcode;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.MainServer;
                //RequestData.StoreIDs = storeid;
                SelectSKUDetailsResponse response = null;
                var ResponseData = new SKUMasterBLL();
                //response = ResponseData.SelectAllSKUMaster(RequestData);
                response = ResponseData.SelectSKUDetails(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetPriceListData()
        {
            try
            {
                var RequestData = new SelectAllPriceListRequest();
                SelectAllPriceListResponse response = null;
                var ResponseData = new PriceListBLL();
                response = ResponseData.SelectAllPriceList(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetComboOffer(int StoreID)
        {
            try
            {
                var RequestData = new SelectAllComboOfferRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreID = StoreID;
                SelectAllComboOfferResponse  response = null;
                var ResponseData = new ComboOfferMasterBLL();
                response = ResponseData.SelectAllComboOffer(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetComboOfferByID(int ID)
        {
            try
            {
                var RequestData = new SelectByComboOfferIDRequest();
                RequestData.ID = ID;
                //RequestData.SKUcode = SKUCode;
                SelectByComboOfferIDResponse response = null;
                var ResponseData = new ComboOfferMasterBLL();
                response = ResponseData.SelectComboOfferRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

}
