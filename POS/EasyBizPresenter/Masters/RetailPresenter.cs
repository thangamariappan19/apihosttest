using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IRetailSettings;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizResponse.Masters.RetailSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class RetailPresenter
    {
        IRetailView _IRetailView;
        RetailSettingsBLL _RetailSettingsBLL = new RetailSettingsBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        public RetailPresenter(IRetailView ViewObj)
        {
            _IRetailView = ViewObj;
        }        
        public void SelectRetailSettings()
        {
            var RequestData = new SelectByRetailIDRequest();
            RequestData.ID = _IRetailView.ID;
            var ResponseData = _RetailSettingsBLL.SelectRetailRecord(RequestData);
            _IRetailView.RetailCode = ResponseData.RetailRecord.RetailCode;
            _IRetailView.RetailName = ResponseData.RetailRecord.RetailName;
            _IRetailView.PriceLowerLimit = ResponseData.RetailRecord.PriceLowerLimit;
            _IRetailView.PriceUpperLimit = ResponseData.RetailRecord.PriceUpperLimit;
            _IRetailView.RowforScan = ResponseData.RetailRecord.RowforScan;
            _IRetailView.ChangeAmountCurrency = ResponseData.RetailRecord.ChangeAmountCurrency;
            _IRetailView.RefundPromotinal = ResponseData.RetailRecord.RefundPromotinal;
            _IRetailView.PrintParked = ResponseData.RetailRecord.PrintParked;
            _IRetailView.DeleteParkedDayEnd = ResponseData.RetailRecord.DeleteParkedDayEnd;
            _IRetailView.ChangeSaleEmployee = ResponseData.RetailRecord.ChangeSaleEmployee;
            _IRetailView.QuickComplete = ResponseData.RetailRecord.QuickComplete;
            _IRetailView.LoginDiffDate = ResponseData.RetailRecord.LoginDiffDate;
            _IRetailView.LogVoidedTransaction = ResponseData.RetailRecord.LogVoidedTransaction;
            _IRetailView.MaxLinesPerTransaction = ResponseData.RetailRecord.MaxLinesPerTransaction;
            _IRetailView.MaxDiscountPercentage = ResponseData.RetailRecord.MaxDiscountPercentage;
            _IRetailView.DefaultTransMode = ResponseData.RetailRecord.DefaultTransMode;
            _IRetailView.AllowRefundToExchanged = ResponseData.RetailRecord.AllowRefundToExchangedItems;
            //_IRetailView.MaxLineDiscountPercentage = ResponseData.RetailRecord.MaxLineDiscountPercentage;
            _IRetailView.MaxDiscountAmt = ResponseData.RetailRecord.MaxDiscountAmt;
            //_IRetailView.MaxLieDiscountAmt = ResponseData.RetailRecord.MaxLieDiscountAmt;
            _IRetailView.Active = ResponseData.RetailRecord.Active;           
            _IRetailView.AllowSalesForNegativeStock = ResponseData.RetailRecord.AllowSalesForNegativeStock;
            _IRetailView.AllowSalesForZeroPrice = ResponseData.RetailRecord.AllowSalesForZeroPrice;
            _IRetailView.IsCreditCardDetailsMandatory = ResponseData.RetailRecord.IsCreditCardDetailsMandatory;
            _IRetailView.AllowMultiplePromotions = ResponseData.RetailRecord.AllowMultiplePromotions;
            _IRetailView.AllowWNPromotions = ResponseData.RetailRecord.AllowWNPromotions;
            _IRetailView.EnableFingerPrint = ResponseData.RetailRecord.EnableFingerPrint;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IRetailView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IRetailView.Message = ResponseData.DisplayMessage;
            }

            _IRetailView.ProcessStatus = ResponseData.StatusCode;


        }

        public class RetailListPresenter
        {
            IRetailCollectionView _IRetailCollectionView;
            RetailSettingsBLL _RetailSettingsBLL = new RetailSettingsBLL();

            public RetailListPresenter(IRetailCollectionView ViewObj)
            {
                _IRetailCollectionView = ViewObj;
            }

            public void GetRetailList()
            {
                var RequestData = new SelectAllRetailRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllRetailResponse();
                ResponseData = _RetailSettingsBLL.SelectAllRetail(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IRetailCollectionView.RetailList = ResponseData.RetailList;


                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {

                }
            }
        }

        public void SaveRetail()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveRetailRequest();
                    RequestData.RetailRecord = new RetailSettingsType();
                    RequestData.RetailRecord.ID = _IRetailView.ID;
                    RequestData.RetailRecord.RetailCode = _IRetailView.RetailCode;
                    RequestData.RetailRecord.RetailName = _IRetailView.RetailName;
                    RequestData.RetailRecord.DefaultTransMode = _IRetailView.DefaultTransMode;
                    RequestData.RetailRecord.PriceLowerLimit = _IRetailView.PriceLowerLimit;
                    RequestData.RetailRecord.PriceUpperLimit = _IRetailView.PriceUpperLimit;
                    RequestData.RetailRecord.RowforScan = _IRetailView.RowforScan;
                    RequestData.RetailRecord.ChangeAmountCurrency = _IRetailView.ChangeAmountCurrency;
                    RequestData.RetailRecord.RefundPromotinal = _IRetailView.RefundPromotinal;
                    RequestData.RetailRecord.PrintParked = _IRetailView.PrintParked;
                    RequestData.RetailRecord.DeleteParkedDayEnd = _IRetailView.DeleteParkedDayEnd;
                    RequestData.RetailRecord.ChangeSaleEmployee = _IRetailView.ChangeSaleEmployee;
                    RequestData.RetailRecord.QuickComplete = _IRetailView.QuickComplete;
                    RequestData.RetailRecord.LoginDiffDate = _IRetailView.LoginDiffDate;
                    RequestData.RetailRecord.LogVoidedTransaction = _IRetailView.LogVoidedTransaction;
                    RequestData.RetailRecord.MaxLinesPerTransaction = _IRetailView.MaxLinesPerTransaction;
                    RequestData.RetailRecord.MaxDiscountPercentage = _IRetailView.MaxDiscountPercentage;
                    //RequestData.RetailRecord.MaxLineDiscountPercentage = _IRetailView.MaxLineDiscountPercentage;
                    RequestData.RetailRecord.MaxDiscountAmt = _IRetailView.MaxDiscountAmt;                   
                    //RequestData.RetailRecord.MaxLieDiscountAmt = _IRetailView.MaxLieDiscountAmt;

                    RequestData.RetailRecord.AllowRefundToExchangedItems = _IRetailView.AllowRefundToExchanged;
                    RequestData.RetailRecord.Active = _IRetailView.Active;
                    RequestData.RetailRecord.AllowSalesForNegativeStock = _IRetailView.AllowSalesForNegativeStock;
                    RequestData.RetailRecord.AllowSalesForZeroPrice = _IRetailView.AllowSalesForZeroPrice;
                    RequestData.RetailRecord.IsCreditCardDetailsMandatory = _IRetailView.IsCreditCardDetailsMandatory;
                    RequestData.RetailRecord.AllowMultiplePromotions = _IRetailView.AllowMultiplePromotions;
                    RequestData.RetailRecord.AllowWNPromotions = _IRetailView.AllowWNPromotions;
                    RequestData.RetailRecord.CreateBy = _IRetailView.UserID;
                    RequestData.RetailRecord.CreateOn = DateTime.Now;
                    // RequestData.RetailRecord.LoginDiffDate = _IRetailView.SCN;


                    var ResponseData = _RetailSettingsBLL.SaveRetail(RequestData);

                    _IRetailView.Message = ResponseData.DisplayMessage;
                    _IRetailView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IRetailView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidForm()
        {
            bool objBool = false;
            if (_IRetailView.RetailCode.Trim() == string.Empty)
            {
                _IRetailView.Message = "Retail Code is missing Please Enter it.";
            }
            else if (_IRetailView.RetailName.Trim() == string.Empty)
            {
                _IRetailView.Message = " Retail Name is Missing.";
            }
            else if (_IRetailView.ChangeAmountCurrency == 0)
            {
                _IRetailView.Message = "Please Select Change Amount Currency";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void DeleteRetail()
        {
            try
            {
                var RequestData = new DeleteRetailRequest();
                RequestData.ID = -_IRetailView.ID;
                var ResponseData = _RetailSettingsBLL.DeleteRetail(RequestData);
                _IRetailView.Message = ResponseData.DisplayMessage;
                _IRetailView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectCurrencyLookUp()
        {
            var RequestData = new SelectCurrencyLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IRetailView.CurrencyLookup = ResponseData.CurrencyMasterList;
            }
        }
    }
}

