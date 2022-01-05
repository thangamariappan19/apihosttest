using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Promotions;
//using EasyBizBLL.Transactions.Promotions.DiscountMasterBLL;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizIView.Transactions.IPromotion.IDiscountMaster;
using EasyBizIView.Transactions.IPromotion.IFamilyDiscount;
//using EasyBizIView.Transactions.IPromotion.IFamilyDiscount;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.DiscountMasterRequest;
//using EasyBizRequest.Transactions.Promotions.FamilyDiscount;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.Promotions.DiscountMasterResponse;
//using EasyBizResponse.Transactions.Promotions.FamilyDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Promotion.DiscountMasterPresenter
{
    public class DiscountMasterPresenter
    {
        IDiscountMasterView _IDiscountMasterView;

        CustomerGroupBLL _CustomerGroupMasterBLL = new CustomerGroupBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        DiscountMasterBLL _DiscountMasterBLL = new DiscountMasterBLL();

        public DiscountMasterPresenter(IDiscountMasterView ViewObj)
        {

            _IDiscountMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDiscountMasterView.CustomerGroupID == 0)
            {
                _IDiscountMasterView.Message = "Select CustomerGroup";
            }
            else if (_IDiscountMasterView.CountryIDs == "")
            {
                _IDiscountMasterView.Message = "Select Country";
            }
            else if (_IDiscountMasterView.StoreIDs == "")
            {
                _IDiscountMasterView.Message = "Select Store";
            }
            else if (_IDiscountMasterView.DiscountType == string.Empty)
            {
                _IDiscountMasterView.Message = "Select DiscountType";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void GetCustomerGroupLookUp()
        {
            try
            {
                var RequestData = new SelectCustomerGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CustomerGroupMasterBLL.SelectCustomerGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDiscountMasterView.CustomerGroupNameLookUp = ResponseData.CustomerGroupMasterList;
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
                    _IDiscountMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetStoreLookUp()
        {
            try
            {
                

                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryIDs = _IDiscountMasterView.CountryIDs;
                var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDiscountMasterView.StoreMasterLookUp = ResponseData.StoreMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStoreBrandMapping()
        {
            try
            {
                var RequestData = new StoreBrandMapRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.StoreIDs = _IDiscountMasterView.StoreIDs;
                var ResponseData = new StoreBrandMapResponse();
                ResponseData = _StoreMasterBLL.GetStoreBrandMappingDetails(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDiscountMasterView.StoreBrandMapList = ResponseData.StoreBrandMapList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveDiscountMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveDiscountMasterRequest();
                    RequestData.EmployeeDiscountDetailList = _IDiscountMasterView.EmployeeDiscountDetailsList;
                    RequestData.FamilyDiscountDetailList = _IDiscountMasterView.FamilyDiscountDetailsList;
                    RequestData.DiscountMasterRecord = new DiscountMasterTypes();
                   
                        RequestData.DiscountMasterRecord.ID = _IDiscountMasterView.ID;
                        RequestData.DiscountMasterRecord.CustomerGroupID = _IDiscountMasterView.CustomerGroupID;
                        RequestData.DiscountMasterRecord.CustomerGroupCode = _IDiscountMasterView.CustomerGroupCode;
                        RequestData.DiscountMasterRecord.CountryIDs = _IDiscountMasterView.CountryIDs;
                        RequestData.DiscountMasterRecord.CountryCodes = _IDiscountMasterView.CountryCodes;
                        RequestData.DiscountMasterRecord.StoreIDs = _IDiscountMasterView.StoreIDs;
                        RequestData.DiscountMasterRecord.StoreCodes = _IDiscountMasterView.StoreCodes;
                        RequestData.DiscountMasterRecord.DiscountType = _IDiscountMasterView.DiscountType;


                        RequestData.DiscountMasterRecord.CreateBy = _IDiscountMasterView.UserID;
                        RequestData.DiscountMasterRecord.CreateOn = DateTime.Now;                      
                        RequestData.DiscountMasterRecord.SCN = _IDiscountMasterView.SCN;
                    
                        var ResponseData = _DiscountMasterBLL.SaveDiscountMaster(RequestData);

                        _IDiscountMasterView.Message = ResponseData.DisplayMessage;
                        _IDiscountMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IDiscountMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAllMappingRecord()
        {
            try
            {
                var RequestData = new SelectAllDiscountMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CustomerGroupID = _IDiscountMasterView.CustomerGroupID;
                RequestData.CustomerGroupCode = _IDiscountMasterView.CustomerGroupCode;    
                var ResponseData = new SelectAllDiscountMasterResponse();
                ResponseData = _DiscountMasterBLL.SelectAllMappingRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDiscountMasterView.EmployeeDiscountDetailsList = ResponseData.EmployeeDiscountDetailList;
                    _IDiscountMasterView.FamilyDiscountDetailsList = ResponseData.FamilyDiscountDetailList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetDiscountRecord()
        {
            var _DiscountMasterBLL = new DiscountMasterBLL();
            var RequestData = new SelectAllDiscountMasterRequest();
            RequestData.CustomerGroupID = _IDiscountMasterView.CustomerGroupID;
            RequestData.CustomerGroupCode = _IDiscountMasterView.CustomerGroupCode;
            var ResponseData = new SelectAllDiscountMasterResponse();
            try
            {               
                ResponseData = _DiscountMasterBLL.SelectAllMappingRecords(RequestData);
                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDiscountMasterView.DiscountMasterRecord = ResponseData.DiscountMasterRecord;
                }                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _DiscountMasterBLL = null;
                RequestData = null;
                ResponseData = null;
            }
        }
    }
    public class DiscountMasterPresenterList
    {

        IDiscountMasterCollectionViewList _IDiscountMasterCollectionViewList;

        DiscountMasterBLL _DiscountMasterBLL = new DiscountMasterBLL();


        public DiscountMasterPresenterList(IDiscountMasterCollectionViewList ViewObj)
        {

            _IDiscountMasterCollectionViewList = ViewObj;
        }
    }
}

