using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Coupens;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ICouponMaster;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.CouponMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class CouponMasterPresenter
    {
        ICouponMasterView _ICouponMasterView;
        CouponMasterBLL _CouponMasterBLL = new CouponMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
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

        public CouponMasterPresenter(ICouponMasterView ViewObj)
        {
            _ICouponMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICouponMasterView.StartDate > _ICouponMasterView.EndDate)
            {
                _ICouponMasterView.Message = "Please give To date is above or from date.";
            }
            else if (_ICouponMasterView.CouponCode.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Coupon Code is missing.Please Enter it.";
            }
            else if (_ICouponMasterView.Coupondescription.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Enter Coupon Description";
            }
            else if (_ICouponMasterView.BarCode.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Enter BarCode";
            }
            else if (_ICouponMasterView.Country.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Select Country";
            }
            else if (_ICouponMasterView.CouponType.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Select Coupon Type";
            }
            else if (_ICouponMasterView.StartDate == null)
            {
                _ICouponMasterView.Message = "Please Give Start Date";
            }
            else if (_ICouponMasterView.EndDate == null)
            {
                _ICouponMasterView.Message = "Please Give End Date";
            }
            else if (_ICouponMasterView.DiscountType.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Select Discount Type";
            }
            else if (_ICouponMasterView.StoreCommonUtil == null)
            {
                _ICouponMasterView.Message = "Store List is empty";
            }
            else if (_ICouponMasterView.CustomerCommonUtil == null)
            {
                _ICouponMasterView.Message = "Customer List is empty";
            }
            else if (_ICouponMasterView.TotalMasterCommonUtil == null)
            {
                _ICouponMasterView.Message = "Applicable list is empty";
            }
           
            //else if ( _ICouponMasterView.StoreCommonUtil.Count==0)
            //{
            //    _ICouponMasterView.Message = "Please Give Store Details";
            //}
            //else if (_ICouponMasterView.CustomerCommonUtil.Count == 0)
            //{
            //    _ICouponMasterView.Message = "Please Give Customer Details";
            //}
            //else if (_ICouponMasterView.TotalMasterCommonUtil.Count == 0)
            //{
            //    _ICouponMasterView.Message = "Please Give Product Details";
            //}
       
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public bool IsValidFormUpdate()
        {
            bool objBool = false;
            if (_ICouponMasterView.CouponCode.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "CouponCode is missing Please Enter it.";
            }
            else if (_ICouponMasterView.Coupondescription.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Give Description";
            }
            else if (_ICouponMasterView.BarCode.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Give BarCode";
            }
            else if (_ICouponMasterView.Country.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Give Country";
            }
            else if (_ICouponMasterView.CouponType.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Give Coupon Type";
            }
            else if (_ICouponMasterView.StartDate == null)
            {
                _ICouponMasterView.Message = "Please Give Start Date";
            }
            else if (_ICouponMasterView.EndDate == null)
            {
                _ICouponMasterView.Message = "Please Give End Date";
            }
            else if (_ICouponMasterView.DiscountType.Trim() == string.Empty)
            {
                _ICouponMasterView.Message = "Please Give Discount Type";
            }
            else if (_ICouponMasterView.DiscountType == "Percentage")
            {
                _ICouponMasterView.Message = "Please Give Value";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveCouponMaster()
        {
            if (IsValidForm())
            {
                var RequestData = new SaveCouponMasterRequest();
                RequestData.CouponMasterData = new CouponMaster();              
                RequestData.CouponMasterData.ID = _ICouponMasterView.ID;
                RequestData.CouponMasterData.CouponCode = _ICouponMasterView.CouponCode;
                RequestData.CouponMasterData.Coupondescription = _ICouponMasterView.Coupondescription;
                RequestData.CouponMasterData.BarCode = _ICouponMasterView.BarCode;
                RequestData.CouponMasterData.Country = _ICouponMasterView.Country;
                RequestData.CouponMasterData.CouponType = _ICouponMasterView.CouponType;
                RequestData.CouponMasterData.StartDate = _ICouponMasterView.StartDate;
                RequestData.CouponMasterData.EndDate = _ICouponMasterView.EndDate;
                RequestData.CouponMasterData.DiscountType = _ICouponMasterView.DiscountType;
                RequestData.CouponMasterData.DiscountValue = _ICouponMasterView.DiscountValue;
                RequestData.CouponMasterData.IssuableAtPOS = _ICouponMasterView.IssuableAtPOS;
                RequestData.CouponMasterData.Serial = _ICouponMasterView.Serial;
                RequestData.CouponMasterData.CreateBy = 1;
                RequestData.CouponMasterData.Remarks = _ICouponMasterView.Remarks;
                RequestData.CouponMasterData.Active = _ICouponMasterView.Active;
                RequestData.CouponMasterData.CouponStoreType = _ICouponMasterView.CouponStoreType;
                RequestData.CouponMasterData.CouponCustomerType = _ICouponMasterView.CouponCustomerType;
                RequestData.CouponMasterData.CouponSerialCode = _ICouponMasterView.CouponSerialCode;
                RequestData.CouponMasterData.Issuedstatus = _ICouponMasterView.Issuedstatus;
                RequestData.CouponMasterData.PhysicalStore = _ICouponMasterView.PhysicalStore;
                RequestData.CouponMasterData.Remainingamount = _ICouponMasterView.Remainingamount;
                RequestData.CouponMasterData.Redeemedstatus = _ICouponMasterView.Redeemedstatus;

                RequestData.StoreCommonUtilData = _ICouponMasterView.StoreCommonUtil;
                RequestData.CustomerCommonUtilData = _ICouponMasterView.CustomerCommonUtil;
                RequestData.TotalMasterCommonUtilData = _ICouponMasterView.TotalMasterCommonUtil;

                SaveCouponMasterResponse ResponseData = _CouponMasterBLL.SaveCouponMaster(RequestData);
                _ICouponMasterView.Message = ResponseData.DisplayMessage;
                _ICouponMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ICouponMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void SelectDetails()
        {
            try
            {
                var RequestData = new SelectCouponStoreDetailsRequest();
                int Count = Enum.GetNames(typeof(Enums.SpecialPriceRecordType)).Length;
                for (int i = 1; i <= Count; i++)
                {
                    RequestData.ShowInActiveRecords = false;
                    RequestData.CouponID = _ICouponMasterView.ID;
                    RequestData.DetailsType = (Enums.SpecialPriceRecordType)Enum.ToObject(typeof(Enums.SpecialPriceRecordType), i);

                    var ResponseData = _CouponMasterBLL.SelectCouponStoreDetails(RequestData);

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        if ((int)Enums.SpecialPriceRecordType.Store == i)
                        {
                            _ICouponMasterView.StoreCommonUtil = ResponseData.StoreCommonUtil;
                        }
                        else if ((int)Enums.SpecialPriceRecordType.Customer == i)
                        {
                            _ICouponMasterView.CustomerCommonUtil = ResponseData.StoreCommonUtil;
                        }
                        else if ((int)Enums.SpecialPriceRecordType.Category == i)
                        {
                            _ICouponMasterView.TotalMasterCommonUtil = ResponseData.StoreCommonUtil;
                        }                        
                    }
                    else
                    {
                        _ICouponMasterView.ProcessStatus = ResponseData.StatusCode;
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void UpdateCouponMaster()
        {

            if (IsValidFormUpdate())
            {
                var RequestData = new SaveCouponMasterRequest();
                RequestData.CouponMasterData = new CouponMaster();
                RequestData.CouponMasterData.ID = _ICouponMasterView.ID;
                RequestData.CouponMasterData.CouponCode = _ICouponMasterView.CouponCode;
                RequestData.CouponMasterData.Coupondescription = _ICouponMasterView.Coupondescription;
                RequestData.CouponMasterData.BarCode = _ICouponMasterView.BarCode;
                RequestData.CouponMasterData.Country = _ICouponMasterView.Country;
                RequestData.CouponMasterData.CouponType = _ICouponMasterView.CouponType;
                RequestData.CouponMasterData.StartDate = _ICouponMasterView.StartDate;
                RequestData.CouponMasterData.EndDate = _ICouponMasterView.EndDate;
                RequestData.CouponMasterData.DiscountType = _ICouponMasterView.DiscountType;
                RequestData.CouponMasterData.DiscountValue = _ICouponMasterView.DiscountValue;
                RequestData.CouponMasterData.IssuableAtPOS = _ICouponMasterView.IssuableAtPOS;
                RequestData.CouponMasterData.Serial = _ICouponMasterView.Serial;
                RequestData.CouponMasterData.CreateBy = 1;
                RequestData.CouponMasterData.Remarks = _ICouponMasterView.Remarks;
                RequestData.CouponMasterData.Active = _ICouponMasterView.Active;
                RequestData.CouponMasterData.CouponStoreType = _ICouponMasterView.CouponStoreType;
                RequestData.CouponMasterData.CouponCustomerType = _ICouponMasterView.CouponCustomerType;
                RequestData.CouponMasterData.CouponSerialCode = _ICouponMasterView.CouponSerialCode;
                RequestData.CouponMasterData.Issuedstatus = _ICouponMasterView.Issuedstatus;
                RequestData.CouponMasterData.PhysicalStore = _ICouponMasterView.PhysicalStore;
                RequestData.CouponMasterData.Remainingamount = _ICouponMasterView.Remainingamount;
                RequestData.CouponMasterData.Redeemedstatus = _ICouponMasterView.Redeemedstatus;

                RequestData.StoreCommonUtilData = _ICouponMasterView.StoreCommonUtil;
                RequestData.CustomerCommonUtilData = _ICouponMasterView.CustomerCommonUtil;
                RequestData.TotalMasterCommonUtilData = _ICouponMasterView.TotalMasterCommonUtil;

                SaveCouponMasterResponse ResponseData = _CouponMasterBLL.SaveCouponMaster(RequestData);
                _ICouponMasterView.Message = ResponseData.DisplayMessage;
                _ICouponMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ICouponMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteCouponMaster()
        {

            var RequestData = new DeleteCouponMasterRequest();
            RequestData.ID = _ICouponMasterView.ID;
            var ResponseData = _CouponMasterBLL.DeleteCouponMaster(RequestData);
            _ICouponMasterView.Message = ResponseData.DisplayMessage;
            _ICouponMasterView.ProcessStatus = ResponseData.StatusCode;
        }

        public void SelectCouponMasterRecord()
        {


            var RequestData = new SelectByIDCouponMasterRequest();
            RequestData.ID = _ICouponMasterView.ID;

            var ResponseData = _CouponMasterBLL.SelectCouponMasterRecord(RequestData);
            _ICouponMasterView.CouponCode = ResponseData.CouponMasterRecord.CouponCode;
            _ICouponMasterView.Coupondescription = ResponseData.CouponMasterRecord.Coupondescription;
            _ICouponMasterView.BarCode = ResponseData.CouponMasterRecord.BarCode;
            _ICouponMasterView.Country = ResponseData.CouponMasterRecord.Country;
            _ICouponMasterView.CouponType = ResponseData.CouponMasterRecord.CouponType;
            _ICouponMasterView.StartDate = ResponseData.CouponMasterRecord.StartDate;
             _ICouponMasterView.EndDate = ResponseData.CouponMasterRecord.EndDate;
            _ICouponMasterView.DiscountType = ResponseData.CouponMasterRecord.DiscountType;
            _ICouponMasterView.DiscountValue = ResponseData.CouponMasterRecord.DiscountValue;
            _ICouponMasterView.IssuableAtPOS = ResponseData.CouponMasterRecord.IssuableAtPOS;
            _ICouponMasterView.Serial = ResponseData.CouponMasterRecord.Serial;              
            _ICouponMasterView.SCN = ResponseData.CouponMasterRecord.SCN;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _ICouponMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _ICouponMasterView.Message = ResponseData.DisplayMessage;
            }

            _ICouponMasterView.ProcessStatus = ResponseData.StatusCode;
        }


        public void SelectCouponListMasterRecord()
        {

            var RequestData = new SelectCouponCouponListDetailsRequest();
            RequestData.CouponID = _ICouponMasterView.ID;

            var ResponseData = _CouponMasterBLL.SelectCouponMasterList(RequestData);        
            _ICouponMasterView.CouponSerialCode = ResponseData.CouponMasterListDetails.CouponSerialCode;
            _ICouponMasterView.Issuedstatus = ResponseData.CouponMasterListDetails.Issuedstatus;
            _ICouponMasterView.PhysicalStore = ResponseData.CouponMasterListDetails.PhysicalStore;
            _ICouponMasterView.Remainingamount = ResponseData.CouponMasterListDetails.Remainingamount;
            _ICouponMasterView.Redeemedstatus = ResponseData.CouponMasterListDetails.Redeemedstatus;
           

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _ICouponMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _ICouponMasterView.Message = ResponseData.DisplayMessage;
            }

            _ICouponMasterView.ProcessStatus = ResponseData.StatusCode;
        }



      
        public void SelectCouponStoreMaster()
        {
            try
            {
                var RequestData = new SelectCouponStoreDetailsRequest();
                RequestData.CouponID = _ICouponMasterView.ID;
                RequestData.CouponStoreType = _ICouponMasterView.CouponStoreType;
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectCouponStoreDetails(RequestData);
               
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _ICouponMasterView.StoreCommonUtil = ResponseData.StoreCommonUtil;
                    }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SelectCouponCustomerMaster()
        {
            try
            {
                var RequestData = new SelectCouponCustomerDetailsRequest();
                RequestData.CouponID = _ICouponMasterView.ID;
                RequestData.CouponCustomerType = _ICouponMasterView.CouponCustomerType;
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectCouponCustomerDetails(RequestData);
               
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.CustomerCommonUtil = ResponseData.CustomerCommonUtil;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SelectCouponProductCategoryTypes()
        {
            try
            {
                var RequestData = new SelectCouponProductCategoryDetailsRequest();
                RequestData.CouponID = _ICouponMasterView.ID;
                RequestData.CouponProductCategoryType = _ICouponMasterView.CouponProductType;
                RequestData.ShowInActiveRecords = false;

                var ResponseData = _CouponMasterBLL.SelectCouponMasterProductTypeBLL(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.TotalMasterCommonUtil = ResponseData.ProductCategoryCommonUtil;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void SelectCouponStoreMasterDetails()
        {
            try
            {
                var RequestData = new SelectCouponStoreDetailsRequest();
                RequestData.CouponID = _ICouponMasterView.ID;
                RequestData.CouponStoreType = "";
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectCouponStoreDetails(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.StoreCommonUtilDetails = ResponseData.StoreCommonUtil;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SelectCouponCustomerMasterDetails()
        {
            try
            {
                var RequestData = new SelectCouponCustomerDetailsRequest();
                RequestData.CouponID = _ICouponMasterView.ID;
                RequestData.CouponCustomerType ="";
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectCouponCustomerDetails(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.CustomerCommonUtilDetails = ResponseData.CustomerCommonUtil;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void SelectCouponProductCategoryTypesDetails()
        {
            try
            {
                var RequestData = new SelectCouponProductCategoryDetailsRequest();
                RequestData.CouponID = _ICouponMasterView.ID;
                RequestData.CouponProductCategoryType = "";
                RequestData.ShowInActiveRecords = false;

                var ResponseData = _CouponMasterBLL.SelectCouponMasterProductTypeBLL(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.TotalMasterCommonUtilDetails = ResponseData.ProductCategoryCommonUtil;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




      

        public void GetCuntryMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
                }
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
                    _ICouponMasterView.StoreGroupMasterLookUp = ResponseData.StoreGroupMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    _ICouponMasterView.StoreMasterList = ResponseData.StoreMasterList;
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
                    _ICouponMasterView.CustomerMasterList = ResponseData.CustomerMasterList;
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
                    _ICouponMasterView.CustomerGroupMasterList = ResponseData.CustomerGroupMasterList;
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
                    _ICouponMasterView.SegamationMasterList = ResponseData.AFSegmentationMaster;

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
                    _ICouponMasterView.YearList = ResponseData.YearList;
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
                    _ICouponMasterView.SeasonList = ResponseData.SeasonList;
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
                    _ICouponMasterView.BrandList = ResponseData.BrandList;
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
                //RequestData.BrandID = _IPromotionsView.BrandID;
                var ResponseData = _SubBrandBLL.SubBrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponMasterView.SubBrandMasterList = ResponseData.SubBrandList;
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
                    _ICouponMasterView.ProductGroupList = ResponseData.ProductGroupList;
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
                    _ICouponMasterView.ProductSubGroupList = ResponseData.ProductSubGroupList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class CouponMasterListPresenter
    {

        CouponMasterBLL _CouponMasterBLL = new CouponMasterBLL();

        ICouponMasterList _ICouponMasterList;

        public CouponMasterListPresenter(ICouponMasterList ViewObj)
        {
            _ICouponMasterList = ViewObj;
        }

        public void GetCouponMasterList()
        {

            var RequestData = new SelectAllCouponMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllCouponMasterResponse();
            ResponseData = _CouponMasterBLL.SelectAllCouponMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICouponMasterList.CouponMasterList = ResponseData.CouponMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }

}

