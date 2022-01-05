using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Pricing;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizIView.Transactions.IPromotion;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.PriceListResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Promotion
{
    public class WNPromotionPresenter
    {
        IWNPromotionView _IWNPromotionView;
        WNPromotionBLL _WNPromotionBLL = new WNPromotionBLL();
        public WNPromotionPresenter(IWNPromotionView ViewObj)
        {
            _IWNPromotionView = ViewObj;
        }
        public void GetPricePointList()
        {
            try
            {
                var _PricePointBLL = new PricePointBLL();
                var RequestData = new SelectAllPricePointRequest();
                var ResponseData = new SelectAllPricePointResponse();
                ResponseData = _PricePointBLL.GetPricePointList(RequestData);
                if(ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.PricePointList = ResponseData.PricePointList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void GetPriceList()
        {
            try
            {
                var _PriceListBLL = new PriceListBLL();
                var RequestData = new SelectPriceListLookUPRequest();
                var ResponseData = new SelectPriceListLookUPResponse();
                RequestData.Type = "WNPROMOTION";
                ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.PriceList = ResponseData.PriceListTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }                 
        }
        public void GetStylePricingList()
        {
            try
            {
                var _PriceListBLL = new PriceListBLL();
                var RequestData = new SelectSalePriceListLookupRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.SalePriceListID = _IWNPromotionView.SalePriceListID;
                RequestData.stylecode = _IWNPromotionView.StyleCode;
                RequestData.Type = "Sales";
                var ResponseData = _PriceListBLL.SalePriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.StylePricingList = ResponseData.SalePriceListTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
        public void GetCountryList()
        { 
            try
            {                
                var _CountryBLL = new CountryBLL();
                var RequestData = new SelectAllCountryRequest();
                var ResponseData = new SelectAllCountryResponse();
                ResponseData = _CountryBLL.SelectAllCountryMaster(RequestData);
                if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.CountryList = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStyleList()
        { 
            try
            {
                var _StyleMasterBLL = new StyleMasterBLL();
                var RequestData = new SelectAllStyleRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.StyleCode = _IWNPromotionView.StyleCode;
                var ResponseData = new SelectAllStyleResponse();
                ResponseData = _StyleMasterBLL.SelectAllStyleRecord(RequestData);
                if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.StyleList = ResponseData.StyleList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetBrandList()
        {             
            try
            {
                var _BrandBLL = new BrandBLL();
                var RequestData = new SelectBrandLookUpRequest();
                var ResponseData = new SelectBrandLookUpResponse();
                ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.BrandList = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSkuList()
        {
            try
            {
                var _SKUMasterBLL = new SKUMasterBLL();
                var RequestData = new SelectAllSKUMasterRequest();
                var ResponseData = new SelectAllSKUMasterResponse();
                ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    //_IWNPromotionView.SKUList = ResponseData.SKUMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveWNPromotionData()
        {
            try
            {
                var RequestData = new SaveWNPromotionRequest();
                var ResponseData = new SaveWNPromotionResponse();

                RequestData.WNPromotionData = new WNPromotion();
                RequestData.Mode = _IWNPromotionView.Mode;

                var objWNPromotion = new WNPromotion();
                objWNPromotion.Active = _IWNPromotionView.Active;
                objWNPromotion.Countries = _IWNPromotionView.Countries;
                objWNPromotion.CreateBy = _IWNPromotionView.UserID;
                objWNPromotion.EndDate = _IWNPromotionView.ToDate;
                objWNPromotion.ID = _IWNPromotionView.WNPromotionID;
                objWNPromotion.PriceListID = _IWNPromotionView.DefaultPriceListID;
                objWNPromotion.DefaultCountryID = _IWNPromotionView.DefaultCountryID;
                objWNPromotion.PricePointApplicable = _IWNPromotionView.PricePointApplicable;
                objWNPromotion.PromotionCode = _IWNPromotionView.PromotionCode;
                objWNPromotion.PromotionName = _IWNPromotionView.PromotionName;
                objWNPromotion.SCN = _IWNPromotionView.SCN;
                objWNPromotion.StartDate = _IWNPromotionView.FromDate;
                objWNPromotion.CreateBy = _IWNPromotionView.UserID;
                objWNPromotion.UploadType = _IWNPromotionView.UploadType;
                objWNPromotion.WNPromotionDetailsList = _IWNPromotionView.WNPromotionDetailsList;


                RequestData.WNPromotionData = objWNPromotion;
                ResponseData = _WNPromotionBLL.SaveWNPromotion(RequestData);

                _IWNPromotionView.ProcessStatus = ResponseData.StatusCode;

                _IWNPromotionView.Message = ResponseData.DisplayMessage;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetWNPromotionRecord()
        { 
            var _WNPromotionBLL = new WNPromotionBLL();
            try
            {
                var RequestData = new SelectWNPromotionByIDRequest();
                var ResponseData = new SelectWNPromotionByIDResponse();
                RequestData.ID = _IWNPromotionView.WNPromotionID;
                ResponseData = _WNPromotionBLL.SelectWNPromotionRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IWNPromotionView.WNPromotionData = ResponseData.WNPromotionRecord;
                }
                else
                {
                    _IWNPromotionView.WNPromotionData = new WNPromotion();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class WNPromotionCollectionPresenter
    {
        IWNPromotionCollectionView _IWNPromotionCollectionView;
        public WNPromotionCollectionPresenter(IWNPromotionCollectionView ViewObj)
        {
            _IWNPromotionCollectionView = ViewObj;
        }
        public void GetWNPromotionList()
        {
            var _WNPromotionBLL = new WNPromotionBLL();
            try
            {                
                var RequestData = new SelectAllWNPromotionRequest();
                var ResponseData = new SelectAllWNPromotionResponse();
                ResponseData = _WNPromotionBLL.SelectAllWNPromotion(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IWNPromotionCollectionView.WNPromotionList = ResponseData.WNPromotionList;
                }
                else
                {
                    _IWNPromotionCollectionView.WNPromotionList = new List<WNPromotion>();
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
