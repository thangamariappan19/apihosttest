using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using PosAPI.Modules;
using PosAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizBLL.Common;
using EasyBizDBTypes.Masters;
using System.Security.Claims;

namespace PosAPI.Controllers
{
    public class DocumentNumberingController : ApiController
    {
        //[Authorize]
        //public IHttpActionResult GetDocumentNumber(int countryid,int stateid,int storeid,string storecode, Enums.RequestFrom RequestFrom, Enums.DocumentType DocumentType)
        //{
        //    try
        //    {
        //        var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();

        //        //RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
        //        //RequestData.DocumentTypeID = (int)Enums.DocumentType.SALES;

        //        RequestData.RequestFrom = RequestFrom;
        //        RequestData.DocumentTypeID = (int)DocumentType;
        //        RequestData.CountryID = countryid;
        //        RequestData.StateID = stateid;
        //        RequestData.StoreID = storeid;
        //        RequestData.StoreCode = storecode;
        //        SelectDocumentNumberingBillNoDetailsResponse response = null;
        //        //var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);
        //        var ResponseDate = new DocumentNumberingBLL();
        //        response = ResponseDate.GetDocumentNoDetail(RequestData);
        //        if (response.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            string BillNo = string.Empty;
        //            BillNo = BillNo.ToDocumentNo(response.DocumentNumberingBillNoDetailsRecord.Prefix, response.DocumentNumberingBillNoDetailsRecord.Suffix, response.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, response.DocumentNumberingBillNoDetailsRecord.StartNumber, response.DocumentNumberingBillNoDetailsRecord.EndNumber, response.DocumentNumberingBillNoDetailsRecord.RunningNo);
        //            //response = codu
        //            response.DocumentNo = BillNo;
        //        }

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}



        //public IHttpActionResult GetDocumentNumber(int countryid, int stateid, int storeid, string storecode, Enums.RequestFrom RequestFrom, Enums.DocumentType DocumentType)
        //{
        //    try
        //    {
        //        var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();

        //        //RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
        //        //RequestData.DocumentTypeID = (int)Enums.DocumentType.SALES;

        //        RequestData.RequestFrom = RequestFrom;
        //        RequestData.DocumentTypeID = (int)DocumentType;
        //        RequestData.CountryID = countryid;
        //        RequestData.StateID = stateid;
        //        RequestData.StoreID = storeid;
        //        RequestData.StoreCode = storecode;
        //        SelectDocumentNumberingBillNoDetailsResponse response = null;
        //        //var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);
        //        var ResponseDate = new DocumentNumberingBLL();
        //        response = ResponseDate.GetDocumentNoDetail(RequestData);
        //        if (response.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            string BillNo = string.Empty;
        //            BillNo = BillNo.ToDocumentNo(response.DocumentNumberingBillNoDetailsRecord.Prefix, response.DocumentNumberingBillNoDetailsRecord.Suffix, response.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, response.DocumentNumberingBillNoDetailsRecord.StartNumber, response.DocumentNumberingBillNoDetailsRecord.EndNumber, response.DocumentNumberingBillNoDetailsRecord.RunningNo);
        //            //response = codu
        //            response.DocumentNo = BillNo;
        //            LoginModule _Log = new LoginModule();
        //            response.BusinessDate = _Log.BusinessDate(storeid);
        //        }

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}


        public IHttpActionResult GetDocumentNumber(int storeid, int DocumentTypeID, string business_date)
        {
            try
            {
                DateTime BusinessDate;

                DateTime.TryParseExact(business_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InstalledUICulture,
                    System.Globalization.DateTimeStyles.None, out BusinessDate);
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();

                //RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                //RequestData.DocumentTypeID = (int)Enums.DocumentType.SALES;

                RequestData.DocumentDate = BusinessDate;
                //RequestData.RequestFrom = RequestFrom;
                RequestData.DocumentTypeID = (int)DocumentTypeID;
                //RequestData.CountryID = countryid;
                //RequestData.StateID = stateid;
                RequestData.StoreID = storeid;
                //RequestData.StoreCode = storecode;
                SelectDocumentNumberingBillNoDetailsResponse response = null;
                //var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);
                var ResponseData = new DocumentNumberingBLL();
                response = ResponseData.GetDocumentNo(RequestData);
                if (response.StatusCode == Enums.OpStatusCode.Success)
                {
                    //string BillNo = string.Empty;
                    //BillNo = BillNo.ToDocumentNo(response.DocumentNumberingBillNoDetailsRecord.Prefix, response.DocumentNumberingBillNoDetailsRecord.Suffix, response.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, response.DocumentNumberingBillNoDetailsRecord.StartNumber, response.DocumentNumberingBillNoDetailsRecord.EndNumber, response.DocumentNumberingBillNoDetailsRecord.RunningNo);
                    //response = codu
                    response.DocumentNo = response.DocumentNumberingBillNoDetailsRecord.Prefix;
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

        [HttpGet]
        public IHttpActionResult GetdocumentNumberingList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllDocumentNumberingMasterRequest();

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



                SelectAllDocumentNumberingMasterResponse response = null;
                var ResponseData = new DocumentNumberingBLL();
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

        //public IHttpActionResult GetdocumentNumberingList()
        //{
        //    try
        //    {
        //        var RequestData = new SelectAllDocumentNumberingMasterRequest();
        //        RequestData.ShowInActiveRecords = true;
        //        SelectAllDocumentNumberingMasterResponse response = null;
        //        var ResponseData = new DocumentNumberingBLL();
        //        response = ResponseData.SelectAllDocumentNumberingMaster(RequestData);
        //        return Ok(response);
        //    }
        //    catch(Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        public IHttpActionResult GetdocumentNumberingList(int ID)
        {
            try
            {
                var RequestData = new SelectByIDDocumentNumberingMasterRequest();
                RequestData.ID = ID;
                SelectByIDDocumentNumberingMasterResponse response = null;
                var ResponseData = new DocumentNumberingBLL();
                response = ResponseData.SelectDocumentNumberingMaster(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostDocumentNumbering(DocumentNumberingMaster _objDocumentNumber)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;

                var RequestData = new SaveDocumentNumberingMasterRequest();
                RequestData.DocumentNumberingMasterRecord = new DocumentNumberingMaster();
                RequestData.DocumentNumberingMasterRecord = _objDocumentNumber;
                RequestData.DocumentNumberingMasterRecord.CreateBy = UserID;
                RequestData.DocumentNumberingDetailsList = _objDocumentNumber.DocumentNumberingDetails;
                SaveDocumentNumberingMasterResponse response = null;
                var ResponseData = new DocumentNumberingBLL();
                response = ResponseData.SaveDocumentNumberingMaster(RequestData);
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