using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Coupens;
using EasyBizBLL.Transactions.CouponReceipt;
using EasyBizBLL.Transactions.CouponTransfer;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizIView.Transactions.ICoupons;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Coupons
{
    public class CouponTransferPresenter
    {
        ICouponTransferView _ICouponTransferView;
        CountryBLL _CountryBLL = new CountryBLL();
        CouponMasterBLL _CouponMasterBLL = new CouponMasterBLL();
        EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();    
        CouponTransferBLL _CouponTransferBLL =new CouponTransferBLL();
        CouponreceiptBLL _CouponreceiptBLL = new CouponreceiptBLL();
        public CouponTransferPresenter(ICouponTransferView ViewObj)
        {
            _ICouponTransferView = ViewObj;
        }
        public void GetCountryLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponTransferView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetSerialNumber()
        {
            try
            {
                var RequestData = new GetSerialNumberRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CouponCode = _ICouponTransferView.CouponCode;
                RequestData.FromSerialNum = _ICouponTransferView.FromSerialNum;
                RequestData.ToSerialNum = _ICouponTransferView.ToSerialNum;
                var ResponseData = _CouponreceiptBLL.SelectCouponSerialNum(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponTransferView.CouponReceiptDetailsRecord = ResponseData.CouponReceiptDetailsRecord;
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
                var RequestData = new SelectCouponMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectCouponMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponTransferView.CouponLookUp = ResponseData.CouponMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStoreLookUp()
        {
            var RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _ICouponTransferView.FromCountryID;
            var ResponseData = _EmployeeMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICouponTransferView.StoreMasterLookUp = ResponseData.StoreMasterList;
            }
        }
        public void SaveCouponTransfer()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCouponTransferRequest();
                    RequestData.CouponTransferRecord = new CouponTransferMaster();
                    RequestData.CouponReceiptDetailsList = _ICouponTransferView.CouponReceiptDetailsRecord;
                    RequestData.CouponTransferRecord.ID = _ICouponTransferView.ID;
                    RequestData.CouponTransferRecord.CouponID = _ICouponTransferView.CouponID;
                    RequestData.CouponTransferRecord.CouponCode = _ICouponTransferView.CouponCode;
                    RequestData.CouponTransferRecord.FromCountryID = _ICouponTransferView.FromCountryID;
                    RequestData.CouponTransferRecord.FromCountryCode = _ICouponTransferView.FromCountryCode;
                    RequestData.CouponTransferRecord.ToStoreID = _ICouponTransferView.ToStoreID;
                    RequestData.CouponTransferRecord.ToStoreCode = _ICouponTransferView.ToStoreCode;
                    RequestData.CouponTransferRecord.FromSerialNum = _ICouponTransferView.FromSerialNum;
                    RequestData.CouponTransferRecord.ToSerialNum = _ICouponTransferView.ToSerialNum;
                    RequestData.CouponTransferRecord.Active = _ICouponTransferView.Active;
                    RequestData.CouponTransferRecord.Fromloaction = _ICouponTransferView.Fromloaction;

                    RequestData.CouponTransferRecord.CreateBy = _ICouponTransferView.UserID;
                    RequestData.CouponTransferRecord.CreateOn = DateTime.Now;                    
                    RequestData.CouponTransferRecord.SCN = _ICouponTransferView.SCN;
                    var ResponseData = _CouponTransferBLL.SaveCouponTransfer(RequestData);
                    _ICouponTransferView.Message = ResponseData.DisplayMessage;
                    _ICouponTransferView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ICouponTransferView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveCouponTransactionsLog()
        {
            var RequestData = new SaveCouponTransactionRequest();

            RequestData.CouponTransactionList = _ICouponTransferView.CouponTransactionList;
            var ResponseData = _CouponTransferBLL.SaveCouponTransactionLog(RequestData);

        }
        public void GetCouponTransferRecord()
        {
            try
            {
                var RequestData = new SelectByIDCouponTransferRequest();
                RequestData.ID = _ICouponTransferView.ID;
                var ResponseData = _CouponTransferBLL.SelectCouponTransferRecord(RequestData);
                _ICouponTransferView.CouponID = ResponseData.CouponTransferMasterRecord.CouponID;                
                _ICouponTransferView.FromCountryID = ResponseData.CouponTransferMasterRecord.FromCountryID;
                _ICouponTransferView.FromSerialNum = ResponseData.CouponTransferMasterRecord.FromSerialNum;
                _ICouponTransferView.ToStoreID = ResponseData.CouponTransferMasterRecord.ToStoreID;
                _ICouponTransferView.ToSerialNum = ResponseData.CouponTransferMasterRecord.ToSerialNum;
                _ICouponTransferView.Active = ResponseData.CouponTransferMasterRecord.Active;
                _ICouponTransferView.Fromloaction = ResponseData.CouponTransferMasterRecord.Fromloaction;
                _ICouponTransferView.SCN = ResponseData.CouponTransferMasterRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICouponTransferView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ICouponTransferView.Message = ResponseData.DisplayMessage;
                }
                _ICouponTransferView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCouponTransferDetails()
        {
            var RequestData = new SelectByCouponTransferDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _ICouponTransferView.ID;
            var ResponseData = _CouponTransferBLL.SelectCouponTransferDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICouponTransferView.CouponReceiptDetailsRecord = ResponseData.CouponTransferDetailsRecord;
            }
            else
            {
                _ICouponTransferView.Message = ResponseData.DisplayMessage;
                _ICouponTransferView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICouponTransferView.CouponCode.Trim() == string.Empty)
            {
                _ICouponTransferView.Message = "Coupon Code is missing Please Enter it.";
            }           
            else if (_ICouponTransferView.FromCountryCode.Trim() == string.Empty)
            {
                _ICouponTransferView.Message = "FromCountry is missing Please Enter it.";
            }
            else if (_ICouponTransferView.ToStoreCode.Trim() == string.Empty)
            {
                _ICouponTransferView.Message = "ToStoreCode is missing Please Enter it.";
            }
            
            else if (_ICouponTransferView.FromSerialNum.Trim() == string.Empty)
            {
                _ICouponTransferView.Message = "FromSerialNum is missing Please Enter it.";
            }
            else if (_ICouponTransferView.ToSerialNum.Trim() == string.Empty)
            {
                _ICouponTransferView.Message = "ToSerialNum is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
    }
    public class CouponTransferListPresenter
    {

        CouponTransferBLL _CouponTransferBLL = new CouponTransferBLL();
        ICouponTransferList _ICouponTransferList;
        public CouponTransferListPresenter(ICouponTransferList ViewObj)
        {
            _ICouponTransferList = ViewObj;
        }
        public void GetCouponTransferList()
        {
            try
            {
                var RequestData = new SelectAllCouponTransferRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCouponTransferResponse();
                ResponseData = _CouponTransferBLL.SelectAllCouponTransfer(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICouponTransferList.CouponTransferMasterList = ResponseData.CouponTransferMasterList;
                }
                else
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
