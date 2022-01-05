using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPromotions;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class PromotionsMasterPresenter
    {
        IPromotionsView _IPromotionsView;
        PromotionsMasterBLL _PromotionsMasterBLL = new PromotionsMasterBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();
        CustomerGroupBLL _CustomerGroupBLL = new CustomerGroupBLL();
        AFSegamationMasterBLL _AFSegamationMasterBLL = new AFSegamationMasterBLL();
        YearBLL _YearBLL = new YearBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        SubBrandBLL _SubBrandBLL = new SubBrandBLL();
        SeasonBLL _SeasonBLL = new SeasonBLL();
        ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();
        ProductSubGroupBLL _ProductSubGroupBLL = new ProductSubGroupBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        public PromotionsMasterPresenter(IPromotionsView ViewObj)
        {
            _IPromotionsView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IPromotionsView.PromotionCode.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Promotion Code is missing Please Enter it.";
            }
           
            else if (_IPromotionsView.PromotionName.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Promotion is missing Please Enter it.";
            }
            else if (_IPromotionsView.Type.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Type is missing Please Enter it.";
            }          
          
            else if (_IPromotionsView.Discount == string.Empty)
            {
                _IPromotionsView.Message = "Discount is missing Please Enter it.";
            }
            else if (_IPromotionsView.DiscountValue == 0.0)
            {
                _IPromotionsView.Message = "Discount Value is missing Please Enter it.";
            }        
        
            else if (_IPromotionsView.StoreCommonUtil.Count==0)
            {
                _IPromotionsView.Message = "Store or Store Group is missing Please Enter it.";
            }
            else if (_IPromotionsView.CustomerCommonUtil.Count == 0)
            {
                _IPromotionsView.Message = "Customer Or Customer Group is missing Please Enter it.";
            }
            else if (_IPromotionsView.ProductCommonUtil.Count == 0)
            {
                _IPromotionsView.Message = "Products Type  is missing Please Enter it.";
            }
            else if (_IPromotionsView.BuyItemCommonUtil.Count == 0)
            {
                _IPromotionsView.Message = "Buy Item Type  is missing Please Enter it.";
            }
            else if (_IPromotionsView.GetItemCommonUtil.Count == 0)
            {
                _IPromotionsView.Message = "Get Item Type  is missing Please Enter it.";
            }
            else if (_IPromotionsView.PriceListID == 0)
            {
                _IPromotionsView.Message = "Please Select Price List";
            }
            
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public bool UpdateIsValidForm()
        {
            bool objBool = false;
           

             if (_IPromotionsView.PromotionName.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Promotion is missing Please Enter it.";
            }
            else if (_IPromotionsView.Type.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Type is missing Please Enter it.";
            }

            else if (_IPromotionsView.Discount == string.Empty)
            {
                _IPromotionsView.Message = "Discount is missing Please Enter it.";
            }
            else if (_IPromotionsView.DiscountValue == 0.0)
            {
                _IPromotionsView.Message = "Discount Value is missing Please Enter it.";
            }

          
            else if (_IPromotionsView.PriceListID == 0)
            {
                _IPromotionsView.Message = "Please Select Price List";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SavePromotions()
        {
            try
            {
                if (IsValidForm())
                {
                var RequestData = new SavePromotionsRequest();
                RequestData.PromotionsRecord = new PromotionsMaster();
                    RequestData.PromotionsRecord.ID = _IPromotionsView.ID;
                    RequestData.PromotionsRecord.PromotionCode = _IPromotionsView.PromotionCode;
                    RequestData.PromotionsRecord.PromotionName = _IPromotionsView.PromotionName;
                    RequestData.PromotionsRecord.Type = _IPromotionsView.Type;
                    RequestData.PromotionsRecord.PriceListID = _IPromotionsView.PriceListID;
                    RequestData.PromotionsRecord.StartDate = _IPromotionsView.StartDate;
                    RequestData.PromotionsRecord.EndDate = _IPromotionsView.EndDate;
                    RequestData.PromotionsRecord.MinBillAmount = _IPromotionsView.MinBillAmount;
                    RequestData.PromotionsRecord.MinQuantity = _IPromotionsView.MinQuantity;
                    RequestData.PromotionsRecord.Discount = _IPromotionsView.Discount;
                    RequestData.PromotionsRecord.DiscountValue = _IPromotionsView.DiscountValue;
                    RequestData.PromotionsRecord.AllowMultiPromotion = _IPromotionsView.AllowMultiPromotion;
                    RequestData.PromotionsRecord.LowestValue = _IPromotionsView.LowestValue;
                    RequestData.PromotionsRecord.ExculdeDiscountItems = _IPromotionsView.ExculdeDiscountItems;
                    RequestData.PromotionsRecord.Prompt = _IPromotionsView.Prompt;
                    RequestData.PromotionsRecord.Color = _IPromotionsView.Colors;
                    RequestData.PromotionsRecord.BuyOptionalCount = _IPromotionsView.BuyOptionalCount;
                    RequestData.PromotionsRecord.GetOptionalCount = _IPromotionsView.GetOptionalCount;
                    RequestData.PromotionsRecord.GetItematFixedPrice = _IPromotionsView.GetItematFixedPrice;
                   
                    RequestData.PromotionsRecord.CreateBy = _IPromotionsView.UserID;
                    RequestData.PromotionsRecord.CreateOn = DateTime.Now;
                    RequestData.PromotionsRecord.Active = _IPromotionsView.Active;
                    //RequestData.PromotionsRecord.Franchise = _IPromotionsView.Franchise;
                    RequestData.PromotionsRecord.SCN = _IPromotionsView.SCN;

                    RequestData.StoreTypeList = _IPromotionsView.StoreCommonUtil;
                    RequestData.CustomerTypeList = _IPromotionsView.CustomerCommonUtil;
                    RequestData.ProductTypeList = _IPromotionsView.ProductCommonUtil;
                    RequestData.BuyItemTypeList = _IPromotionsView.BuyItemCommonUtil;
                    RequestData.GetItemTypeList = _IPromotionsView.GetItemCommonUtil;   
                    var ResponseData = _PromotionsMasterBLL.SavePromotions(RequestData);
                    _IPromotionsView.Message = ResponseData.DisplayMessage;
                    _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPromotionsView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public void UpdatePromotions()
        {
            try
            {
                if (UpdateIsValidForm())
                {
                    var RequestData = new SavePromotionsRequest();
                    RequestData.PromotionsRecord = new PromotionsMaster();
                    RequestData.PromotionsRecord.ID = _IPromotionsView.ID;
                    RequestData.PromotionsRecord.PromotionCode = _IPromotionsView.PromotionCode;
                    RequestData.PromotionsRecord.PromotionName = _IPromotionsView.PromotionName;
                    RequestData.PromotionsRecord.Type = _IPromotionsView.Type;
                    RequestData.PromotionsRecord.PriceListID = _IPromotionsView.PriceListID;
                    RequestData.PromotionsRecord.StartDate = _IPromotionsView.StartDate;
                    RequestData.PromotionsRecord.EndDate = _IPromotionsView.EndDate;
                    RequestData.PromotionsRecord.MinBillAmount = _IPromotionsView.MinBillAmount;
                    RequestData.PromotionsRecord.MinQuantity = _IPromotionsView.MinQuantity;
                    RequestData.PromotionsRecord.Discount = _IPromotionsView.Discount;
                    RequestData.PromotionsRecord.DiscountValue = _IPromotionsView.DiscountValue;
                    RequestData.PromotionsRecord.AllowMultiPromotion = _IPromotionsView.AllowMultiPromotion;
                    RequestData.PromotionsRecord.LowestValue = _IPromotionsView.LowestValue;
                    RequestData.PromotionsRecord.ExculdeDiscountItems = _IPromotionsView.ExculdeDiscountItems;
                    RequestData.PromotionsRecord.Prompt = _IPromotionsView.Prompt;
                    RequestData.PromotionsRecord.Color = _IPromotionsView.Colors;
                    RequestData.PromotionsRecord.BuyOptionalCount = _IPromotionsView.BuyOptionalCount;
                    RequestData.PromotionsRecord.GetOptionalCount = _IPromotionsView.GetOptionalCount;
                    RequestData.PromotionsRecord.GetItematFixedPrice = _IPromotionsView.GetItematFixedPrice;

                    RequestData.PromotionsRecord.CreateBy = _IPromotionsView.UserID;
                    RequestData.PromotionsRecord.CreateOn = DateTime.Now;
                    RequestData.PromotionsRecord.Active = _IPromotionsView.Active;
                    //RequestData.PromotionsRecord.Franchise = _IPromotionsView.Franchise;
                    RequestData.PromotionsRecord.SCN = _IPromotionsView.SCN;

                    RequestData.StoreTypeList = _IPromotionsView.StoreCommonUtil;
                    RequestData.CustomerTypeList = _IPromotionsView.CustomerCommonUtil;
                    RequestData.ProductTypeList = _IPromotionsView.ProductCommonUtil;
                    RequestData.BuyItemTypeList = _IPromotionsView.BuyItemCommonUtil;
                    RequestData.GetItemTypeList = _IPromotionsView.GetItemCommonUtil;
                    var ResponseData = _PromotionsMasterBLL.SavePromotions(RequestData);
                    _IPromotionsView.Message = ResponseData.DisplayMessage;
                    _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPromotionsView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void SelectPromotionsRecord()
        {
            try
            {
                var RequestData = new SelectByPromotionsIDRequest();
                RequestData.ID = _IPromotionsView.ID;
                var ResponseData = _PromotionsMasterBLL.SelectPromotionsRecord(RequestData);
                _IPromotionsView.PromotionCode = ResponseData.PromotionsRecord.PromotionCode;
                _IPromotionsView.PromotionName = ResponseData.PromotionsRecord.PromotionName;
                _IPromotionsView.Active = ResponseData.PromotionsRecord.Active;
                _IPromotionsView.Type = ResponseData.PromotionsRecord.Type;
                _IPromotionsView.PriceListID = ResponseData.PromotionsRecord.PriceListID;
                _IPromotionsView.StartDate = ResponseData.PromotionsRecord.StartDate;
                _IPromotionsView.EndDate = ResponseData.PromotionsRecord.EndDate;
                _IPromotionsView.MinBillAmount = ResponseData.PromotionsRecord.MinBillAmount;
                _IPromotionsView.MinQuantity = ResponseData.PromotionsRecord.MinQuantity;
                _IPromotionsView.Discount = ResponseData.PromotionsRecord.Discount;
                _IPromotionsView.DiscountValue = ResponseData.PromotionsRecord.DiscountValue;
                _IPromotionsView.AllowMultiPromotion = ResponseData.PromotionsRecord.AllowMultiPromotion;
                _IPromotionsView.LowestValue = ResponseData.PromotionsRecord.LowestValue;
                _IPromotionsView.ExculdeDiscountItems = ResponseData.PromotionsRecord.ExculdeDiscountItems;
                _IPromotionsView.Prompt = ResponseData.PromotionsRecord.Prompt;
                _IPromotionsView.PriceListID = ResponseData.PromotionsRecord.PriceListID;
           
                _IPromotionsView.BuyOptionalCount = ResponseData.PromotionsRecord.BuyOptionalCount;
                _IPromotionsView.GetOptionalCount = ResponseData.PromotionsRecord.GetOptionalCount;
                _IPromotionsView.GetItematFixedPrice = ResponseData.PromotionsRecord.GetItematFixedPrice;              
                _IPromotionsView.SCN = ResponseData.PromotionsRecord.SCN;
                _IPromotionsView.Colors = ResponseData.PromotionsRecord.Color;
                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IPromotionsView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IPromotionsView.Message = ResponseData.DisplayMessage;
                }
                _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void SelectDetails()
        {
            try
            {
                var RequestData = new SelectByPromotionIDStoreDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.ID = _IPromotionsView.ID;
                RequestData.Type = _IPromotionsView.StoreType;
                RequestData.DetailsType = _IPromotionsView.DetailsTypeName;
                var ResponseData = _PromotionsMasterBLL.PromotionsWithStoreDetails(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.StoreCommonUtil = ResponseData.DetailsRecord;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IPromotionsView.StoreCommonUtil = new List<CommonUtil>();
                }

                _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SelectDetailsByAll()
        {
            try
            {
                var RequestData = new SelectByPromotionIDStoreDetailsRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.ID = _IPromotionsView.ID;
                RequestData.Type = "";
                RequestData.DetailsType = _IPromotionsView.DetailsTypeName;
                var ResponseData = _PromotionsMasterBLL.PromotionsWithStoreDetails(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CommonAllDetailsCommonUtil = ResponseData.DetailsRecord;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IPromotionsView.CommonAllDetailsCommonUtil = new List<CommonUtil>();
                }

                _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetStoreGroupMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStoreGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StoreGroupBLL.SelectStoreGroupMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.StoreGroupList = ResponseData.StoreGroupMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetPriceLookUp()
        {
            var RequestData = new SelectPriceListLookUPRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPromotionsView.PriceListLookUp = ResponseData.PriceListTypeData;
            }
        }

        public void GetStoreMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.StoreMasterList = ResponseData.StoreMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCustomerMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCustomerMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CustomerMasterBLL.SelectCustomerMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CustomerMasterList = ResponseData.CustomerMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetCustomerGroupMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCustomerGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CustomerGroupBLL.SelectCustomerGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CustomerGroupMasterList = ResponseData.CustomerGroupMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSegamationLookUp()
        {
            try
            {
                var RequestData = new SelectAFsegmentationLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _AFSegamationMasterBLL.SelectSegmentationLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.SegamationMasterList = ResponseData.AFSegmentationMaster;
                    
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void GetYearLookUp()
        {
            try
            {
                var RequestData = new SelectYearLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _YearBLL.YearLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.YearList = ResponseData.YearList;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void GetSeasonLookUp()
        {
            try
            {
                var RequestData = new SelectSeasonLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _SeasonBLL.SelectSeasonLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.SeasonList = ResponseData.SeasonList;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void GetBrandCodeLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.BrandList = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void GetSubBrandLookUp()
        {
            try
            {
                var RequestData = new SelectSubBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
               // RequestData.BrandID = _IPromotionsView.BrandID;
                var ResponseData = _SubBrandBLL.SubBrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.SubBrandMasterList = ResponseData.SubBrandList;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }
        public void GetProductGroupLookUp()
        {
            try
            {
                var RequestData = new SelectProductGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _ProductGroupBLL.ProductGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.ProductGroupList = ResponseData.ProductGroupList;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void GetProductSubGroupLookUp()
        {
            try
            {
                var RequestData = new SelectProductSubGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                //RequestData.ProductGroupID = _IPromotionsView.ProductGroupID;
                var ResponseData = _ProductSubGroupBLL.ProductSubGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.ProductSubGroupList = ResponseData.ProductSubGroupList;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
    }
    public class PromotionsListPresenter
    {
        PromotionsMasterBLL _PromotionsMasterBLL = new PromotionsMasterBLL();
        IPromotionsCollectionView _IPromotionsCollectionView;
        public PromotionsListPresenter(IPromotionsCollectionView ViewObj)
        {
            _IPromotionsCollectionView = ViewObj;
        }
        public void GetPromotionsList()
        {
            try
            {
                var RequestData = new SelectAllPromotionsRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllPromotionsResponse();
                ResponseData = _PromotionsMasterBLL.SelectAllPromotionsRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsCollectionView.PromotionsList = ResponseData.PromotionsList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

    }
}
