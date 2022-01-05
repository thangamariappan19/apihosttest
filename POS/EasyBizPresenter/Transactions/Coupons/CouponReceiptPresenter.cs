using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Coupens;
using EasyBizBLL.Transactions.CouponReceipt;
using EasyBizBLL.Transactions.CouponTransfer;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizIView.Transactions.ICoupons;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Transactions.CouponReceipt;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse.Transactions.CouponReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Coupons
{
    public class CouponReceiptPresenter
    {
        IcouponReceiptHeader _IcouponReceiptHeader;
        CountryBLL _CountryBLL = new CountryBLL();
        CouponTransferBLL _CouponTransferBLL = new CouponTransferBLL();
        CouponMasterBLL _CouponMasterBLL = new CouponMasterBLL();
        CouponreceiptBLL _CouponreceiptBLL = new CouponreceiptBLL();
        public CouponReceiptPresenter(IcouponReceiptHeader ViewObj)
        {
            _IcouponReceiptHeader = ViewObj;
        }
        //public void GetCountryLookUp()
        //{
        //    try
        //    {
        //        var RequestData = new SelectCountryLookUpRequest();
        //        RequestData.ShowInActiveRecords = false;
        //        var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IcouponReceiptHeader.CountryLookUp = ResponseData.CountryMasterList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void GetCouponLookUp()
        {
            try
            {
                var RequestData = new SelectCouponMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CouponMasterBLL.SelectCouponMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IcouponReceiptHeader.CouponLookUp = ResponseData.CouponMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveCouponReceipt()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCouponReceiptRequest();
                    RequestData.CouponReceiptHeaderRecord = new CouponReceiptHeader();
                    RequestData.CouponReceiptDetailsList = _IcouponReceiptHeader.CouponReceiptDetailsList;
                    RequestData.CouponReceiptHeaderRecord.ID = _IcouponReceiptHeader.ID;
                    RequestData.CouponReceiptHeaderRecord.CouponID = _IcouponReceiptHeader.CouponID;
                    RequestData.CouponReceiptHeaderRecord.CouponCode = _IcouponReceiptHeader.CouponCode;
                    RequestData.CouponReceiptHeaderRecord.Active = _IcouponReceiptHeader.Active;
                    RequestData.CouponReceiptHeaderRecord.CurrentLocation = _IcouponReceiptHeader.CurrentLocation;
                    RequestData.CouponReceiptHeaderRecord.CreateBy = _IcouponReceiptHeader.UserID;
                    RequestData.CouponReceiptHeaderRecord.CreateOn = DateTime.Now;
                    RequestData.CouponReceiptHeaderRecord.SCN = _IcouponReceiptHeader.SCN;
                    var ResponseData = _CouponreceiptBLL.SaveCouponReceipt(RequestData);
                    _IcouponReceiptHeader.Message = ResponseData.DisplayMessage;
                    _IcouponReceiptHeader.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IcouponReceiptHeader.ProcessStatus = Enums.OpStatusCode.GeneralError;
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

            RequestData.CouponTransactionList = _IcouponReceiptHeader.CouponTransactionList;
            var ResponseData = _CouponreceiptBLL.SaveCouponTransactionLog(RequestData);

        }
        public bool IsValidForm()
        {
            bool objBool = false;

            if (_IcouponReceiptHeader.CouponReceiptDetailsList.Count == 0)
            {
                _IcouponReceiptHeader.Message = "CouponReceiptDetails is missing Please Select it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SelectCouponReceiptHeaderRecord()
        {
            try
            {
                var RequestData = new SelectByIDCouponReceiptRequest();
                RequestData.ID = _IcouponReceiptHeader.ID;
                var ResponseData = _CouponreceiptBLL.SelectCouponReceiptRecord(RequestData);
                _IcouponReceiptHeader.ID = ResponseData.CouponReceiptHeaderRecord.ID;
                _IcouponReceiptHeader.CouponID = ResponseData.CouponReceiptHeaderRecord.CouponID;
                _IcouponReceiptHeader.CurrentLocation = ResponseData.CouponReceiptHeaderRecord.CurrentLocation;
                _IcouponReceiptHeader.Active = ResponseData.CouponReceiptHeaderRecord.Active;
                _IcouponReceiptHeader.SCN = ResponseData.CouponReceiptHeaderRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IcouponReceiptHeader.Message = ResponseData.DisplayMessage;
                }

                _IcouponReceiptHeader.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCouponReceiptDetails()
        {
            var RequestData = new SelectByCouponReceiptDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IcouponReceiptHeader.ID;
            var ResponseData = _CouponreceiptBLL.SelectCouponReceiptDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IcouponReceiptHeader.CouponReceiptDetailsList = ResponseData.CouponReceiptDetailsRecord;
            }
            else
            {
                _IcouponReceiptHeader.Message = ResponseData.DisplayMessage;
                _IcouponReceiptHeader.ProcessStatus = ResponseData.StatusCode;
            }
        }
    }
   public class CouponReceiptListPresenter
   {
       IcouponReceiptList _IcouponReceiptList;
       CouponreceiptBLL _CouponreceiptBLL = new CouponreceiptBLL();
       public CouponReceiptListPresenter(IcouponReceiptList ViewObj)
        {
            _IcouponReceiptList = ViewObj;
        }
       public void GetCouponReceiptlist()
       {
           try
           {
               var RequestData = new SelectAllCouponReceiptRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllCouponReceiptResponse();
               ResponseData = _CouponreceiptBLL.SelectAllCouponReceipt(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IcouponReceiptList.CouponReceiptHeaderList = ResponseData.CouponReceiptHeaderList;
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
