using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Pricing;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.PriceListResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PosAPI.Controllers
{
    public class WNPromotionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetWNPromotionsList(string limit, string offset, string isActive, string searchString)
        {
            try
            {
                var RequestData = new SelectAllWNPromotionRequest();

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



                SelectAllWNPromotionResponse response = null;
                var ResponseData = new WNPromotionBLL();
                response = ResponseData.API_SelectALLWN(RequestData);
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
        public IHttpActionResult Get()
        {
            try
            {
                var RequestData = new SelectAllWNPromotionRequest();
                SelectAllWNPromotionResponse response = null;
                var ResponseData = new WNPromotionBLL();
                response = ResponseData.SelectAllWNPromotion(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Select by ID - Edit
        public IHttpActionResult GetWNPromotion(int ID)
        {
            try
            {
                var RequestData = new SelectWNPromotionByIDRequest();
                RequestData.ID = ID;
                SelectWNPromotionByIDResponse response = null;
                var ResponseData = new WNPromotionBLL();
                response = ResponseData.SelectWNPromotionRecord(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        

        public IHttpActionResult PostWNPromotion(WNPromotion request)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var myClaim = principal.Claims.Where(x => x.Type == "UserID").SingleOrDefault().Value;
                int UserID = myClaim != null ? int.Parse(myClaim) : 0;
                var RequestData = new SaveWNPromotionRequest();
                RequestData.WNPromotionData = new WNPromotion();
                RequestData.WNPromotionData = request;
                if(request.ID==0)
                {
                    RequestData.Mode = 1;
                }
                else
                {
                    RequestData.Mode = 0;
                }
                RequestData.WNPromotionData.CreateBy = UserID;
                SaveWNPromotionResponse response = null;
                var ResponseData = new WNPromotionBLL();
                response = ResponseData.SaveWNPromotion(RequestData);
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


        public IHttpActionResult PutWNPromotion(WNPromotion _objWNPromotionMaster)
        {
            var styleCodeFound = true;
            var ErrorMessage = false;
            var PromotionDetailsList = new List<WNPromotionDetails>();

            try
            {

                string StyleCode = "";
         
                var FilterWNPromotionDetailsList = new List<WNPromotionDetails>();

                var Country = string.Empty;

                FilterWNPromotionDetailsList = _objWNPromotionMaster.WNPromotionDetailsList.Where(x => x.CountryID == _objWNPromotionMaster.DefaultCountryID).ToList();


                if (FilterWNPromotionDetailsList != null && FilterWNPromotionDetailsList.Count > 0)
                {
                    foreach (WNPromotionDetails oWNPromotionDetails in FilterWNPromotionDetailsList)
                    {
                        var objWNPromotionDetails = new WNPromotionDetails();

                        var BrandData = new BrandMaster();

                        StyleCode = oWNPromotionDetails.StyleCode; //Convert.ToString(objDR["StyleCode"]);

                        if (StyleCode != null && StyleCode.Trim() != string.Empty)
                        {
                            Decimal NowPrice = oWNPromotionDetails.NowPrice; //objDR["NowPrice"] != DBNull.Value ? Convert.ToDecimal(objDR["NowPrice"]) : 0;
                            Decimal Discount = oWNPromotionDetails.Discount; //objDR["Discount"] != DBNull.Value ? Convert.ToDecimal(objDR["Discount"]) : 0;

                           var StyleList = SelectStyleList(StyleCode);
        
                           if (StyleList.StyleList != null) { 
                            var StyleData = StyleList.StyleList.Where(x => x.StyleCode.ToUpper().Trim() == StyleCode.ToUpper().Trim()).FirstOrDefault();
                            objWNPromotionDetails.ID = 0;


                            Decimal NearByRoundOff = 0;
                            var CountryList = SelectCountryList();
                            if (CountryList.CountryMasterList.Where(x => x.ID == _objWNPromotionMaster.DefaultCountryID).FirstOrDefault() != null)
                            {
                                Country = CountryList.CountryMasterList.Where(x => x.ID == _objWNPromotionMaster.DefaultCountryID).FirstOrDefault().CountryName;
                                NearByRoundOff = CountryList.CountryMasterList.Where(x => x.ID == _objWNPromotionMaster.DefaultCountryID).FirstOrDefault().NearByRoundOff;
                            }

                            objWNPromotionDetails.CountryID = _objWNPromotionMaster.DefaultCountryID;
                            objWNPromotionDetails.Country = Country;

                            objWNPromotionDetails.NowPrice = NowPrice;
                            objWNPromotionDetails.StyleCode = StyleCode;
                            objWNPromotionDetails.WNPromotionID = _objWNPromotionMaster.ID;

                            if (StyleData != null)
                            {
                                
                                BrandData = SelectBrandList().BrandList.Where(x => x.ID == StyleData.BrandID).FirstOrDefault();
                                objWNPromotionDetails.BrandID = StyleData.BrandID;
                                if (BrandData != null)
                                {
                                    objWNPromotionDetails.Brand = BrandData.BrandName;
                                }
                                objWNPromotionDetails.StyleID = StyleData.ID;
                                objWNPromotionDetails.Status = "Ok";
                            }
                            else
                            {
                                objWNPromotionDetails.BrandID = 0;
                                objWNPromotionDetails.CountryID = 1;
                                objWNPromotionDetails.Brand = string.Empty;
                                objWNPromotionDetails.StyleID = 0;
                                objWNPromotionDetails.Status = "Not Ok";
                                objWNPromotionDetails.ErrorMsg = "Invalid Style.";
                                objWNPromotionDetails.NowPrice = 0;
                                ErrorMessage = true;
                            }

                            //ApplicationState.SetValue("WNPromotionView", "SalePriceListID", DefaultPriceListID);
                             var StylePricingList = SelectStylePricingList(_objWNPromotionMaster.PriceListID, oWNPromotionDetails.StyleCode);

                            var PriceData = StylePricingList.SalePriceListTypeData.Where(x => x.PriceListID == _objWNPromotionMaster.PriceListID && x.StyleCode.Trim().ToUpper() == StyleCode.Trim().ToUpper()).FirstOrDefault();
                            if (PriceData != null)
                            {
                                if (PriceData.Price > 0)
                                {
                                    objWNPromotionDetails.WasPrice = PriceData.Price;
                                    if (_objWNPromotionMaster.UploadType == "Discount")
                                    {
                                        if (Discount > 0)
                                        {
                                            Decimal DiscountAmount = Math.Round(((PriceData.Price / 100) * Discount), 2);

                                            Decimal TempNowPrice = (PriceData.Price - DiscountAmount);
                                            Decimal RoundOffValue = RoundUpToNearest(TempNowPrice, NearByRoundOff);

                                            objWNPromotionDetails.NowPrice = RoundOffValue; //(PriceData.Price - DiscountAmount);
                                            if (RoundOffValue > 0)
                                            {
                                                objWNPromotionDetails.Discount = Discount;
                                            }
                                            else
                                            {
                                                objWNPromotionDetails.Discount = 0;
                                            }
                                        }
                                        else
                                        {
                                            objWNPromotionDetails.NowPrice = PriceData.Price;
                                            objWNPromotionDetails.Discount = 0;
                                        }
                                    }
                                    else if (_objWNPromotionMaster.UploadType == "Now Price")
                                    {
                                        if (NowPrice > 0)
                                        {
                                            Decimal RoundOffValue = RoundUpToNearest(NowPrice, NearByRoundOff);
                                            objWNPromotionDetails.NowPrice = RoundOffValue;

                                            if (RoundOffValue > 0)
                                            {
                                                Decimal DifferntValue = PriceData.Price - RoundOffValue; //PriceData.Price - NowPrice;
                                                objWNPromotionDetails.Discount = Math.Round(((DifferntValue / PriceData.Price) * 100), 2);
                                            }
                                            else
                                            {
                                                objWNPromotionDetails.Discount = 0;
                                            }
                                        }
                                        else
                                        {
                                            objWNPromotionDetails.NowPrice = 0;
                                            objWNPromotionDetails.Discount = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    objWNPromotionDetails.Status = "Not Ok";
                                    objWNPromotionDetails.ErrorMsg = objWNPromotionDetails.ErrorMsg + "Price not available.";
                                    objWNPromotionDetails.NowPrice = 0;
                                    ErrorMessage = true;
                                   // IsValid = false;
                                }
                            }
                            else
                            {
                                objWNPromotionDetails.Status = "Not Ok";
                                objWNPromotionDetails.ErrorMsg = objWNPromotionDetails.ErrorMsg + "Price details not available.";
                                //IsValid = false;
                                objWNPromotionDetails.NowPrice = 0;
                                ErrorMessage = true;
                            }
                            var TempWNPromotionDetails = new WNPromotionDetails();
                            if (PromotionDetailsList != null)
                            {
                                TempWNPromotionDetails = PromotionDetailsList.Where(x => x.StyleCode.Trim().ToUpper() == StyleCode.Trim().ToUpper() && x.CountryID == _objWNPromotionMaster.DefaultCountryID).FirstOrDefault();
                                if (TempWNPromotionDetails == null)
                                {
                                    PromotionDetailsList.Add(objWNPromotionDetails);
                                }
                                else
                                {
                                    PromotionDetailsList.Where(x => x.StyleCode.Trim().ToUpper() == StyleCode.Trim().ToUpper() && x.CountryID == _objWNPromotionMaster.DefaultCountryID).ToList().ForEach(y => y.NowPrice = objWNPromotionDetails.NowPrice);
                                    PromotionDetailsList.Where(x => x.StyleCode.Trim().ToUpper() == StyleCode.Trim().ToUpper() && x.CountryID == _objWNPromotionMaster.DefaultCountryID).ToList().ForEach(y => y.Discount = objWNPromotionDetails.Discount);
                                    PromotionDetailsList.Where(x => x.StyleCode.Trim().ToUpper() == StyleCode.Trim().ToUpper() && x.CountryID == _objWNPromotionMaster.DefaultCountryID).ToList().ForEach(y => y.Status = objWNPromotionDetails.Status);
                                    PromotionDetailsList.Where(x => x.StyleCode.Trim().ToUpper() == StyleCode.Trim().ToUpper() && x.CountryID == _objWNPromotionMaster.DefaultCountryID).ToList().ForEach(y => y.ErrorMsg = objWNPromotionDetails.ErrorMsg);
                                }
                            }
                            }
                            else
                            {
                                objWNPromotionDetails.StyleCode = StyleCode;
                                objWNPromotionDetails.CountryID = 1;
                                objWNPromotionDetails.BrandID = 0;
                                objWNPromotionDetails.Brand = string.Empty;
                                objWNPromotionDetails.StyleID = 0;
                                objWNPromotionDetails.Status = "Not Ok";
                                objWNPromotionDetails.ErrorMsg = "Invalid Style.";
                                objWNPromotionDetails.NowPrice = 0;
                                PromotionDetailsList.Add(objWNPromotionDetails);
                                styleCodeFound = false;
                            }
                        }
                    }
                }

                List<string> CountryIds = new List<string>();
                CountryIds = _objWNPromotionMaster.Countries.ToString().Split(',').ToList();

                foreach (object objCountryId in CountryIds)
                {
                    int CountryID = Convert.ToInt32(objCountryId);
                    Decimal dNearByRoundOff = 0;
                    Decimal NearByRoundOff = 0;
                    if (CountryID != _objWNPromotionMaster.DefaultCountryID)
                    {
                        Country = SelectCountryList().CountryMasterList.Where(x => x.ID == CountryID).FirstOrDefault().CountryName;

                        dNearByRoundOff = SelectCountryList().CountryMasterList.Where(x => x.ID == CountryID).FirstOrDefault().NearByRoundOff;
                        NearByRoundOff = Convert.ToDecimal(dNearByRoundOff);
                        if (styleCodeFound == true)
                        {
                            if (ErrorMessage == false)
                            {
                                var data = (ValidateOtherCountriesUploadData(CountryID, Country, NearByRoundOff, _objWNPromotionMaster));

                                foreach (var item in data)
                                {
                                    PromotionDetailsList.Add(item);
                                }
                            }
                        }

                        
                    }
                }
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(PromotionDetailsList);
        }

        private List<WNPromotionDetails> ValidateOtherCountriesUploadData(int CountryID, string Country, Decimal NearByRoundOff, WNPromotion _objWNPromotionMaster)
        {
           // var _UIProcess = new UIProcess();

            var objWNPromotionDetailsList = new List<WNPromotionDetails>();
            objWNPromotionDetailsList = _objWNPromotionMaster.WNPromotionDetailsList.Where(x => x.CountryID == _objWNPromotionMaster.DefaultCountryID).ToList(); ;

            var PromotionDetailsList = new List<WNPromotionDetails>();

            foreach (WNPromotionDetails TempWNPromotionDetails in objWNPromotionDetailsList)
            {
                var objWNPromotionDetails = new WNPromotionDetails();

                objWNPromotionDetails.ID = 0;
                objWNPromotionDetails.CountryID = CountryID;
                objWNPromotionDetails.Country = Country;

                objWNPromotionDetails.StyleCode = TempWNPromotionDetails.StyleCode;
                objWNPromotionDetails.WNPromotionID = TempWNPromotionDetails.WNPromotionID;
                objWNPromotionDetails.BrandID = TempWNPromotionDetails.BrandID;
                objWNPromotionDetails.Brand = TempWNPromotionDetails.Brand;
                objWNPromotionDetails.StyleID = TempWNPromotionDetails.StyleID;


                if (_objWNPromotionMaster.PricePointApplicable == true)
                {
                    var PriceListData = SelectPriceList().PriceListTypeData.Where(x => x.CountryID == CountryID).FirstOrDefault();

                    if (PriceListData != null)
                    {
                        //ApplicationState.SetValue("WNPromotionView", "SalePriceListID", PriceListData.ID);


                       var StyleCode = TempWNPromotionDetails.StyleCode;

                        //_WNPromotionPresenter.GetStylePricingList();


                        var objPricePoint = new PricePoint();
                        objPricePoint = SelectPricePointList().PricePointList.Where(x => x.BrandID == TempWNPromotionDetails.BrandID && x.Active == true).FirstOrDefault();

                        if (objPricePoint != null && objPricePoint.PricePointRangeList != null && objPricePoint.PricePointRangeList.Count > 0)
                        {
                            var StylePricingData = SelectStylePricingList(PriceListData.ID, StyleCode).SalePriceListTypeData.Where(x => x.CountryID == CountryID && x.PriceCategory.ToUpper() == "SALES" && x.StyleCode.Trim().ToUpper() == TempWNPromotionDetails.StyleCode.Trim().ToUpper()).FirstOrDefault();
                            if (StylePricingData != null)
                            {
                                objWNPromotionDetails.WasPrice = StylePricingData.Price;
                                Decimal DiscountedPrice = objWNPromotionDetails.WasPrice - ((objWNPromotionDetails.WasPrice * TempWNPromotionDetails.Discount) / 100);

                                int CurrencyID = StylePricingData.PriceListCurrency;
                                PricePointRange DefaultRange = new PricePointRange();
                                DefaultRange = objPricePoint.PricePointRangeList.Where(x => x.RangeFrom <= Convert.ToDecimal(DiscountedPrice) && x.RangeTo >= Convert.ToDecimal(DiscountedPrice) && x.CurrencyID == CurrencyID).FirstOrDefault();
                                if (DefaultRange != null)
                                {
                                    Decimal RoundOffValue = RoundUpToNearest(DefaultRange.Price, NearByRoundOff);
                                    objWNPromotionDetails.NowPrice = RoundOffValue;
                                    if (RoundOffValue > 0)
                                    {
                                        objWNPromotionDetails.Discount = TempWNPromotionDetails.Discount;
                                    }
                                    else
                                    {
                                        objWNPromotionDetails.Discount = 0;
                                    }
                                    objWNPromotionDetails.Status = "Ok";
                                }
                            }
                            else
                            {
                                objWNPromotionDetails.Status = "Not Ok";
                                objWNPromotionDetails.ErrorMsg = "Style pricing not available";
                                //IsValidDatas = false;
                                objWNPromotionDetails.NowPrice = 0;
                                objWNPromotionDetails.Discount = 0;
                            }
                        }
                        else
                        {
                            objWNPromotionDetails.Status = "Not Ok";
                            objWNPromotionDetails.ErrorMsg = "Price point not available";
                           // IsValidDatas = false;
                            objWNPromotionDetails.NowPrice = 0;
                            objWNPromotionDetails.Discount = 0;
                        }
                    }
                    else
                    {
                        objWNPromotionDetails.Status = "Not Ok";
                        objWNPromotionDetails.ErrorMsg = "Price List not available";
                       // IsValidDatas = false;
                        objWNPromotionDetails.NowPrice = 0;
                        objWNPromotionDetails.Discount = 0;
                    }
                }
                else
                {

                    var PriceListData = SelectPriceList().PriceListTypeData.Where(x => x.CountryID == CountryID).FirstOrDefault();

                    if (PriceListData != null)
                    {
                        //ApplicationState.SetValue("WNPromotionView", "SalePriceListID", PriceListData.ID);

                        //PriceListID = PriceListData.ID;
                        var StyleCode = TempWNPromotionDetails.StyleCode;

                        

                        var StylePricingData = SelectStylePricingList(PriceListData.ID, StyleCode ).SalePriceListTypeData.Where(x => x.CountryID == CountryID && x.PriceCategory.ToUpper() == "SALES" && x.PriceType.Trim().ToUpper() == "RETAIL PRICE" && x.StyleCode.Trim().ToUpper() == TempWNPromotionDetails.StyleCode.Trim().ToUpper()).FirstOrDefault();
                        if (StylePricingData != null)
                        {
                            objWNPromotionDetails.WasPrice = StylePricingData.Price;
                            Decimal DiscountAmount = (StylePricingData.Price / 100) * (Math.Round(TempWNPromotionDetails.Discount, 2));

                            Decimal TempNowPrice = (StylePricingData.Price - DiscountAmount);
                            Decimal RoundValue = RoundUpToNearest(TempNowPrice, NearByRoundOff); //(StylePricingData.Price - DiscountAmount);

                            objWNPromotionDetails.NowPrice = RoundValue;
                            if (RoundValue > 0)
                            {
                                objWNPromotionDetails.Discount = Math.Round(TempWNPromotionDetails.Discount, 2);
                            }
                            else
                            {
                                objWNPromotionDetails.Discount = 0;
                            }
                            objWNPromotionDetails.Status = "Ok";
                        }
                        else
                        {
                            objWNPromotionDetails.Status = "Not Ok";
                            objWNPromotionDetails.ErrorMsg = "Style pricing not available";
                            //IsValidDatas = false;
                            objWNPromotionDetails.NowPrice = 0;
                            objWNPromotionDetails.Discount = 0;
                        }
                    }
                    else
                    {
                        objWNPromotionDetails.Status = "Not Ok";
                        objWNPromotionDetails.ErrorMsg = "Price List not available";
                        //IsValidDatas = false;
                        objWNPromotionDetails.NowPrice = 0;
                        objWNPromotionDetails.Discount = 0;
                    }
                }
                objWNPromotionDetails.CreateBy = 0;
                PromotionDetailsList.Add(objWNPromotionDetails);
                //ApplicationState.SetValue("WNPromotionView", "SalePriceListID", 0);
            }

            return PromotionDetailsList;

           // var oWNPromotionDetailsList = new List<WNPromotionDetails>();
           //oWNPromotionDetailsList = _objWNPromotionMaster.WNPromotionDetailsList;

            //oWNPromotionDetailsList.AddRange(PromotionDetailsList);

            // WNPromotionDetailsList = WNPromotionDeepCopyCreator.WNPromotionDetailListDeepCopy(oWNPromotionDetailsList);

        }
        private Decimal RoundUpToNearest(object passednumber, object roundto)
        {
            if (Convert.ToDecimal(roundto) == 0)
            {
                return Convert.ToDecimal(passednumber);
            }
            else
            {
                return Math.Round(Convert.ToDecimal(passednumber), Convert.ToInt32(roundto));
                //Decimal dFloor = Math.Ceiling(Convert.ToDecimal(passednumber) / Convert.ToDecimal(roundto)) * Convert.ToDecimal(roundto);
                //return Convert.ToDecimal(dFloor);
            }
        }

        private SelectSalePriceListLookupResponse SelectStylePricingList(int PriceListID, string StyleCode)
        {
            try
            {
                var _PriceListBLL = new PriceListBLL();
                var RequestData = new SelectSalePriceListLookupRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.SalePriceListID = PriceListID;
                RequestData.stylecode = StyleCode;
                RequestData.Type = "Sales";
                var ResponseData = _PriceListBLL.SalePriceListLookUp(RequestData);
                return ResponseData;
                //SalePriceListTypeData
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SelectAllPricePointResponse SelectPricePointList()
        {
            try
            {
                var _PricePointBLL = new PricePointBLL();
                var RequestData = new SelectAllPricePointRequest();
                var ResponseData = new SelectAllPricePointResponse();
                ResponseData = _PricePointBLL.GetPricePointList(RequestData);
                // if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                // {
                return ResponseData;
               // }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SelectPriceListLookUPResponse SelectPriceList()
        {
            try
            {
                var _PriceListBLL = new PriceListBLL();
                var RequestData = new SelectPriceListLookUPRequest();
                var ResponseData = new SelectPriceListLookUPResponse();
                RequestData.Type = "WNPROMOTION";
                ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                return ResponseData;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private SelectAllStyleResponse SelectStyleList(string StyleCode)
        {
            var ResponseData = new SelectAllStyleResponse();
            try
            {
                var _StyleMasterBLL = new StyleMasterBLL();
                var RequestData = new SelectAllStyleRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StyleCode = StyleCode;
                //var ResponseData = new SelectAllStyleResponse();
                ResponseData = _StyleMasterBLL.SelectAllStyleRecord(RequestData);
                //if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                //{
                //    return ResponseData;
                //}

                return ResponseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SelectAllCountryResponse SelectCountryList()
        {
            var ResponseData = new SelectAllCountryResponse();
            try
            {
                var _CountryBLL = new CountryBLL();
                var RequestData = new SelectAllCountryRequest();

                ResponseData = _CountryBLL.SelectAllCountryMaster(RequestData);
                //if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                //{
                //    return ResponseData;
                //}
                return ResponseData;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SelectBrandLookUpResponse SelectBrandList()
        {
            try
            {
                var _BrandBLL = new BrandBLL();
                var RequestData = new SelectBrandLookUpRequest();
                var ResponseData = new SelectBrandLookUpResponse();
                ResponseData = _BrandBLL.BrandLookUp(RequestData);
                //if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                //{
                //    return ResponseData;
                //}
                return ResponseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
