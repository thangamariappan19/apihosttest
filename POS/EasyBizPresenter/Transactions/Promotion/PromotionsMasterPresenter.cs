using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Coupens;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPromotions;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
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
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        public PromotionsMasterPresenter(IPromotionsView ViewObj)
        {
            _IPromotionsView = ViewObj;
        }
        //public void GetCommonCustomerList()
        //{
        //    List<LookUp> CommonUtilList = new List<LookUp>();
        //    CommonUtilList.AddRange(GetCustomerList());
        //    CommonUtilList.AddRange(GetCustomerGroupList());
        //    _IPromotionsView.CommonCustomerLookUpList = CommonUtilList;
        //}


        public bool IsValidForm()
        {
            bool objBool = false;
            Decimal BuyTotalQty = 0;
            Decimal GetTotalQty = 0;

            if (_IPromotionsView.PromotionWithBuyItemList == null || _IPromotionsView.PromotionWithBuyItemList.Count == 0)
            {
               
            }    
            else 
            {
                BuyTotalQty = _IPromotionsView.PromotionWithBuyItemList.Sum(x => x.Quantity);
            }
            if (_IPromotionsView.PromotionWithGetItemList == null || _IPromotionsView.PromotionWithGetItemList.Count == 0)
            {
                
            }  
            else
            {
                GetTotalQty = _IPromotionsView.PromotionWithGetItemList.Sum(x => x.Quantity);
            }
             if (_IPromotionsView.PromotionCode.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Promotion Code is missing Please Enter it.";
            }
            else if (_IPromotionsView.PromotionName.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Promotion Name is missing Please Enter it.";
            }
            else if (_IPromotionsView.Type.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Type is missing Please Enter it.";
            }
             else if(_IPromotionsView.Colors.Trim() == string.Empty )
             {
                 _IPromotionsView.Message = "Color is Missing.Please Select it";
             }
            else if (_IPromotionsView.Type.Trim().ToLower() == "fixed price" && _IPromotionsView.GetItematFixedPrice <= 0)
            {
                _IPromotionsView.Message = "Please Enter Get Items At Fixed Price";
            }    
            else if (_IPromotionsView.PromotionWithStoreList == null)
            {
                _IPromotionsView.Message = "store list is empty";
            }            
                  
            //else if (_IPromotionsView.BuyOptionalCount > BuyTotalQty)
            //{
            //    _IPromotionsView.Message = "Buy Item Optional count and Buy Item List total Quantity must be same or greater !";
            //}
            //else if (_IPromotionsView.GetOptionalCount > GetTotalQty)
            //{
            //    _IPromotionsView.Message = "Get Item Optional count and Get Item List total Quantity must be same or greater !";
            //}            
            else if (_IPromotionsView.PromotionType.Trim() == string.Empty)
            {
                _IPromotionsView.Message = "Promotion Type is missing Please select it.";
            }
            //else if (_IPromotionsView.Type.Trim() == "Bonus Buys" && _IPromotionsView.PromotionType.Trim() == "Combo Offer")
            //{
            //    _IPromotionsView.Message = "Combo Offer is not applicable for Bonus Buys !";
            //}
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

            //else if (_IPromotionsView.Discount == string.Empty)
            //{
            //    _IPromotionsView.Message = "Discount is missing Please Enter it.";
            //}
             else if (_IPromotionsView.DiscountValue == Convert.ToDecimal(0) && _IPromotionsView.Discount != string.Empty)
            {
                _IPromotionsView.Message = "Discount Value is missing Please Enter it.";
            }
            //else if (_IPromotionsView.PriceListID == 0)
            //{
            //    _IPromotionsView.Message = "Please Select Price List";
            //}

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
                    RequestData.PromotionsRecord.PromotionType = _IPromotionsView.PromotionType;
                    RequestData.PromotionsRecord.AppliedType = _IPromotionsView.AppliedType;
                    RequestData.PromotionsRecord.Type = _IPromotionsView.Type;
                    //RequestData.PromotionsRecord.PriceListID = _IPromotionsView.PriceListID;
                    RequestData.PromotionsRecord.StartDate = _IPromotionsView.StartDate;
                    RequestData.PromotionsRecord.EndDate = _IPromotionsView.EndDate;
                    RequestData.PromotionsRecord.MinBillAmount = _IPromotionsView.MinBillAmount;
                    RequestData.PromotionsRecord.MinQuantity = _IPromotionsView.MinQuantity;
                    RequestData.PromotionsRecord.Discount = _IPromotionsView.Discount;
                    RequestData.PromotionsRecord.DiscountValue = _IPromotionsView.DiscountValue;
                    RequestData.PromotionsRecord.AllowMultiPromotion = _IPromotionsView.AllowMultiPromotion;
                    RequestData.PromotionsRecord.LowestValue = _IPromotionsView.LowestValue;
                    RequestData.PromotionsRecord.LowestValueWithGroup = _IPromotionsView.LowestValueWithGroup;
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

                    RequestData.StoreTypeList = _IPromotionsView.PromotionWithStoreList;
                    RequestData.CustomerTypeList = _IPromotionsView.PromotionWithCustomerList;
                    RequestData.ProductTypeList = _IPromotionsView.PromotionWithProductList;
                    RequestData.BuyItemTypeList = _IPromotionsView.PromotionWithBuyItemList;
                    RequestData.GetItemTypeList = _IPromotionsView.PromotionWithGetItemList;   
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
                throw ex;
            }
        }
        public void GetStyleLookUp()
        {
            try
            {
                var RequestData = new SelectStyleLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StyleMasterBLL.SelectStyleLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.StyleMasterList = ResponseData.StyleMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCouponLookUp()
        {
            try
            {
                var _CouponMasterBLL = new CouponMasterBLL();
                var RequestData = new SelectAllCouponMasterRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectAllCouponMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CouponMasterList = ResponseData.CouponMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CouponMasterBLL
        //SelectCouponMasterLookUpResponse SelectCouponMasterLookUp(SelectCouponMasterLookUpRequest objRequest)
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
                    RequestData.PromotionsRecord.PromotionType = _IPromotionsView.PromotionType;
                    RequestData.PromotionsRecord.AppliedType = _IPromotionsView.AppliedType;
                    RequestData.PromotionsRecord.Type = _IPromotionsView.Type;
                    //RequestData.PromotionsRecord.PriceListID = _IPromotionsView.PriceListID;
                    RequestData.PromotionsRecord.StartDate = _IPromotionsView.StartDate;
                    RequestData.PromotionsRecord.EndDate = _IPromotionsView.EndDate;
                    RequestData.PromotionsRecord.MinBillAmount = _IPromotionsView.MinBillAmount;
                    RequestData.PromotionsRecord.MinQuantity = _IPromotionsView.MinQuantity;
                    RequestData.PromotionsRecord.Discount = _IPromotionsView.Discount;
                    RequestData.PromotionsRecord.DiscountValue = _IPromotionsView.DiscountValue;
                    RequestData.PromotionsRecord.AllowMultiPromotion = _IPromotionsView.AllowMultiPromotion;
                    RequestData.PromotionsRecord.LowestValue = _IPromotionsView.LowestValue;
                    RequestData.PromotionsRecord.LowestValueWithGroup = _IPromotionsView.LowestValueWithGroup;
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

                    //RequestData.StoreTypeList = _IPromotionsView.StoreCommonUtil;
                    //RequestData.CustomerTypeList = _IPromotionsView.CustomerCommonUtil;
                    //RequestData.ProductTypeList = _IPromotionsView.ProductCommonUtil;
                    //RequestData.BuyItemTypeList = _IPromotionsView.BuyItemCommonUtil;
                    //RequestData.GetItemTypeList = _IPromotionsView.GetItemCommonUtil;
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
                throw ex;
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
                _IPromotionsView.PromotionType = ResponseData.PromotionsRecord.PromotionType;
                _IPromotionsView.AppliedType = ResponseData.PromotionsRecord.AppliedType;
                _IPromotionsView.Active = ResponseData.PromotionsRecord.Active;
                _IPromotionsView.Type = ResponseData.PromotionsRecord.Type;
                //_IPromotionsView.PriceListID = ResponseData.PromotionsRecord.PriceListID;
                _IPromotionsView.StartDate = ResponseData.PromotionsRecord.StartDate;
                _IPromotionsView.EndDate = ResponseData.PromotionsRecord.EndDate;
                _IPromotionsView.MinBillAmount = ResponseData.PromotionsRecord.MinBillAmount;
                _IPromotionsView.MinQuantity = ResponseData.PromotionsRecord.MinQuantity;
                _IPromotionsView.Discount = ResponseData.PromotionsRecord.Discount;
                _IPromotionsView.DiscountValue = ResponseData.PromotionsRecord.DiscountValue;
                _IPromotionsView.AllowMultiPromotion = ResponseData.PromotionsRecord.AllowMultiPromotion;
                _IPromotionsView.LowestValue = ResponseData.PromotionsRecord.LowestValue;
                _IPromotionsView.LowestValueWithGroup = ResponseData.PromotionsRecord.LowestValueWithGroup;
                _IPromotionsView.ExculdeDiscountItems = ResponseData.PromotionsRecord.ExculdeDiscountItems;
                _IPromotionsView.Prompt = ResponseData.PromotionsRecord.Prompt;
                //_IPromotionsView.PriceListID = ResponseData.PromotionsRecord.PriceListID;
           
                _IPromotionsView.BuyOptionalCount = ResponseData.PromotionsRecord.BuyOptionalCount;
                _IPromotionsView.GetOptionalCount = ResponseData.PromotionsRecord.GetOptionalCount;
                _IPromotionsView.GetItematFixedPrice = ResponseData.PromotionsRecord.GetItematFixedPrice;              
                _IPromotionsView.SCN = ResponseData.PromotionsRecord.SCN;
                _IPromotionsView.Colors = ResponseData.PromotionsRecord.Color;

                _IPromotionsView.PromotionWithStoreList = ResponseData.PromotionsRecord.StoreList;
                _IPromotionsView.PromotionWithCustomerList = ResponseData.PromotionsRecord.CustomerList;
                _IPromotionsView.PromotionWithProductList = ResponseData.PromotionsRecord.ProductTypeList;
                _IPromotionsView.PromotionWithBuyItemList = ResponseData.PromotionsRecord.BuyItemTypeList;
                _IPromotionsView.PromotionWithGetItemList = ResponseData.PromotionsRecord.GetItemTypeList;


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
                throw ex;
            }
        }
        public void SelectDetails()
        {
            try
            {
                var RequestData = new SelectByPromotionIDStoreDetailsRequest();
                int Count = Enum.GetNames(typeof(Enums.PromotionRecordType)).Length;
                for (int i = 1; i <= Count; i++)
                {
                    RequestData.ShowInActiveRecords = false;
                    RequestData.ID = _IPromotionsView.ID;
                    RequestData.DetailsType = (Enums.PromotionRecordType)Enum.ToObject(typeof(Enums.PromotionRecordType), i);
                   
                    var ResponseData = _PromotionsMasterBLL.PromotionsWithStoreDetails(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        if ((int)Enums.PromotionRecordType.Store == i)
                        {
                            _IPromotionsView.PromotionWithStoreList = ResponseData.DetailsRecord;
                        }
                        else if ((int)Enums.PromotionRecordType.Customer == i)
                        {
                            _IPromotionsView.PromotionWithCustomerList= ResponseData.DetailsRecord;
                        }
                        else if ((int)Enums.PromotionRecordType.Category == i)
                        {
                            _IPromotionsView.PromotionWithProductList = ResponseData.DetailsRecord;
                        }
                        else if ((int)Enums.PromotionRecordType.BuyItem == i)
                        {
                            _IPromotionsView.PromotionWithBuyItemList = ResponseData.DetailsRecord;
                        }
                        else if ((int)Enums.PromotionRecordType.GetItem == i)
                        {
                            _IPromotionsView.PromotionWithGetItemList = ResponseData.DetailsRecord;
                        }
                    }
                    else 
                    {
                        _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
                    }

                    
                }
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
                //RequestData.DetailsType = _IPromotionsView.DetailsTypeName;
                var ResponseData = _PromotionsMasterBLL.PromotionsWithStoreDetails(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    //_IPromotionsView.CommonAllDetailsCommonUtil = ResponseData.DetailsRecord;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    //_IPromotionsView.CommonAllDetailsCommonUtil = new List<CommonUtil>();
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
                RequestData.CountryID = _IPromotionsView.CountryID;
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

        public void GetCustomerList()
        {
            List<LookUp> LookUpList = new List<LookUp>();
            try
            {
                var RequestData = new SelectCustomerMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CustomerMasterBLL.SelectCustomerMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CustomerMasterList = ResponseData.CustomerMasterList;
                    //LookUpList = (from c in ResponseData.CustomerMasterList
                    //                  select new LookUp { TypeID = (int)Enums.CustomerDetailsTypes.Customer, DocumentID = c.ID, DocumentCode = Convert.ToString(c.CustomerCode), DocumentName = c.CustomerName }).ToList();
                   

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public void GetCustomerGroupList()
        {
            List<LookUp> LookUpList = new List<LookUp>();
            try
            {
                var RequestData = new SelectCustomerGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CustomerGroupBLL.SelectCustomerGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CustomerGroupMasterList = ResponseData.CustomerGroupMasterList;
                    //LookUpList = (from c in ResponseData.CustomerGroupMasterList
                    //                  select new LookUp { TypeID = (int)Enums.CustomerDetailsTypes.CustomerGroup, DocumentID = c.ID, DocumentCode = Convert.ToString(c.GroupCode), DocumentName = c.GroupName }).ToList();
                    
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
            }
        }
        public void GetCountryLookUp()
        {
            try
            {
                var _CountryBLL = new CountryBLL();
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CountryList = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    _IPromotionsView.SubBrandList = ResponseData.SubBrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
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
                throw ex;
            }
        }

        public void DeletePromotions()
        {
            try
            {
                var RequestData = new DeletePromotionsRequest();
                RequestData.ID = _IPromotionsView.ID;
                var ResponseData = _PromotionsMasterBLL.DeletePromotions(RequestData);
                _IPromotionsView.Message = ResponseData.DisplayMessage;
                _IPromotionsView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCustomer()
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Source = "Sales";
                //RequestData.ID = _IPromotionsView.CustomerID;
                //RequestData.CustomerInfo = _IPromotionsView.CustomerSearchString;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = new SelectAllCustomerMasterResponse();

                ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionsView.CustomerMasterList = ResponseData.CustomerMasterData;
                }
                else
                {
                    var CustomerList = new List<CustomerMaster>();
                    _IPromotionsView.CustomerMasterList = CustomerList;
                }
            }
            catch (Exception ex)
            {
                var CustomerList = new List<CustomerMaster>();
                _IPromotionsView.CustomerMasterList = CustomerList;
                throw ex;
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
                throw ex;
            }
        }

    }
}
