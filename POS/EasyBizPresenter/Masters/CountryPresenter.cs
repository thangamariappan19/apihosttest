using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.Country;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.CountryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class CountryPresenter
    {
        ICountryView _ICountryView;
        CountryBLL _CountryBLL = new CountryBLL();
        LanguageBLL _LanguageBLL = new LanguageBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        TaxBLL _TaxBLL = new TaxBLL();
        public CountryPresenter(ICountryView ViewObj)
        {
            _ICountryView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICountryView.CountryCode.Trim() == string.Empty)
            {
                _ICountryView.Message = "Country Code is missing Please Enter it.";
            }
            else if (_ICountryView.CountryCode.Length > 8)
            {
                _ICountryView.Message = " Please Enter Vail Code.";
            }
            else if (_ICountryView.CountryName.Trim() == string.Empty)
            {
                _ICountryView.Message = "Country Name is missing Please Enter it. ";
            }
            else if (_ICountryView.LanguageName.Trim() == string.Empty)
            {
                _ICountryView.Message = "Language Name is missing Please Enter it. ";
            }
            else if (_ICountryView.CurrencyID==0)
            {
                _ICountryView.Message = "Currency is missing Please Enter it. ";
            }
            else if (_ICountryView.TaxID == 0)
            {
                _ICountryView.Message = "Tax-ID is missing Please Enter it. ";
            }
            else if (_ICountryView.DecimalDigit == null)
            {
                _ICountryView.Message = "Decimal Digit is missing Please Enter it.";
            }
            else if (_ICountryView.DecimalPlaces == null)
            {
                _ICountryView.Message = "Decimal value is missing Please Enter it.";
            }
            else if (_ICountryView.DateFormat == null)
            {
                _ICountryView.Message = "Date Format is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveCountryMaster()
        {
            try
        
          {
            if (IsValidForm())
            {
                var RequestData = new SaveCountryRequest();
                RequestData.CountryMasterData = new CountryMaster();

                RequestData.CountryMasterData.ID = _ICountryView.ID;
                RequestData.CountryMasterData.CountryCode = _ICountryView.CountryCode;
                RequestData.CountryMasterData.CountryName = _ICountryView.CountryName;
                RequestData.CountryMasterData.LanguageName = _ICountryView.LanguageName;
                RequestData.CountryMasterData.DecimalDigit = _ICountryView.DecimalDigit;
                RequestData.CountryMasterData.DecimalPlaces = _ICountryView.DecimalPlaces;
                RequestData.CountryMasterData.DateFormat = _ICountryView.DateFormat;
                RequestData.CountryMasterData.DateSeparator = _ICountryView.DateSeparator;
                RequestData.CountryMasterData.NearByRoundOff = _ICountryView.NearByRoundOff;
                RequestData.CountryMasterData.TaxID = _ICountryView.TaxID;
                RequestData.CountryMasterData.NegativeSign = _ICountryView.NegativeSign;
                RequestData.CountryMasterData.CurrencySeparator = _ICountryView.CurrencySeparator;
                RequestData.CountryMasterData.Currency = _ICountryView.Currency;
                RequestData.CountryMasterData.CurrencyID = _ICountryView.CurrencyID;
                RequestData.CountryMasterData.EmailID = _ICountryView.EmailID;
                RequestData.CountryMasterData.CreditLimitCheck = _ICountryView.CreditLimitCheck;
                RequestData.CountryMasterData.AllowMultipleTransaction = _ICountryView.AllowMultipleTransaction;
                RequestData.CountryMasterData.AllowPartialReceiving = _ICountryView.AllowPartialReceiving;
                RequestData.CountryMasterData.AllowSaleAndRedemption = _ICountryView.AllowSaleAndRedemption;
                RequestData.CountryMasterData.CurrencyCode = _ICountryView.CurrencyCode;
                RequestData.CountryMasterData.TaxCode = _ICountryView.TaxCode;
                RequestData.CountryMasterData.Active = _ICountryView.Active;
                RequestData.CountryMasterData.OrginCountry = _ICountryView.OrginCountry;
                RequestData.CountryMasterData.POSTitle = _ICountryView.POSTitle;
                RequestData.CountryMasterData.PromotionRoundOff = _ICountryView.PromotionRoundOff;
                RequestData.CountryMasterData.CreateBy = _ICountryView.UserID;               
                RequestData.CountryMasterData.CreateOn = DateTime.Now;               
                RequestData.CountryMasterData.SCN = _ICountryView.SCN;

                var ResponseData = _CountryBLL.SaveCountryMaster(RequestData);

                _ICountryView.Message = ResponseData.DisplayMessage;
                _ICountryView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ICountryView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateCountryMaster()
       
        {
            try
        
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateCountryRequest();
                RequestData.CountryMasterData = new CountryMaster();

                RequestData.CountryMasterData.ID = _ICountryView.ID;
                RequestData.CountryMasterData.CountryCode = _ICountryView.CountryCode;
                RequestData.CountryMasterData.CountryName = _ICountryView.CountryName;
                RequestData.CountryMasterData.LanguageName = _ICountryView.LanguageName;
                RequestData.CountryMasterData.DecimalDigit = _ICountryView.DecimalDigit;
                RequestData.CountryMasterData.DecimalPlaces = _ICountryView.DecimalPlaces;
                RequestData.CountryMasterData.DateFormat = _ICountryView.DateFormat;
                RequestData.CountryMasterData.DateSeparator = _ICountryView.DateSeparator;
                RequestData.CountryMasterData.NegativeSign = _ICountryView.NegativeSign;
                RequestData.CountryMasterData.NearByRoundOff = _ICountryView.NearByRoundOff;
                RequestData.CountryMasterData.TaxID = _ICountryView.TaxID;
                RequestData.CountryMasterData.CurrencySeparator = _ICountryView.CurrencySeparator;
                RequestData.CountryMasterData.Currency = _ICountryView.Currency;
                RequestData.CountryMasterData.CurrencyID = _ICountryView.CurrencyID;
                RequestData.CountryMasterData.EmailID = _ICountryView.EmailID;
                RequestData.CountryMasterData.CreditLimitCheck = _ICountryView.CreditLimitCheck;
                RequestData.CountryMasterData.AllowMultipleTransaction = _ICountryView.AllowMultipleTransaction;
                RequestData.CountryMasterData.AllowPartialReceiving = _ICountryView.AllowPartialReceiving;
                RequestData.CountryMasterData.AllowSaleAndRedemption = _ICountryView.AllowSaleAndRedemption;
                RequestData.CountryMasterData.CurrencyCode = _ICountryView.CurrencyCode;
                RequestData.CountryMasterData.TaxCode = _ICountryView.TaxCode;
                RequestData.CountryMasterData.Active = _ICountryView.Active;
                RequestData.CountryMasterData.OrginCountry = _ICountryView.OrginCountry;
                RequestData.CountryMasterData.POSTitle = _ICountryView.POSTitle;
                RequestData.CountryMasterData.PromotionRoundOff = _ICountryView.PromotionRoundOff;
                RequestData.CountryMasterData.UpdateBy = _ICountryView.UserID;
                RequestData.CountryMasterData.UpdateOn = DateTime.Now;                
                RequestData.CountryMasterData.SCN = _ICountryView.SCN;
                var ResponseData = _CountryBLL.UpdateCountryMaster(RequestData);

                _ICountryView.Message = ResponseData.DisplayMessage;
                _ICountryView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ICountryView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        }
        public void DeleteCountryMaster()
        {
            try
       
        {
            var RequestData = new DeleteCountryRequest();
            RequestData.ID = -_ICountryView.ID;
            var ResponseData = _CountryBLL.DeleteCountryMaster(RequestData);
            _ICountryView.Message = ResponseData.DisplayMessage;
            _ICountryView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCountryMaster()
        {
            var RequestData = new SelectByIDCountryRequest();
            RequestData.ID = _ICountryView.ID;
            var ResponseData = _CountryBLL.SelectCountryMaster(RequestData);
            _ICountryView.CountryCode = ResponseData.CountryMasterRecord.CountryCode;
            _ICountryView.CountryName = ResponseData.CountryMasterRecord.CountryName;
            _ICountryView.LanguageName = ResponseData.CountryMasterRecord.LanguageName;
            _ICountryView.DecimalDigit = ResponseData.CountryMasterRecord.DecimalDigit;
            _ICountryView.DecimalPlaces = ResponseData.CountryMasterRecord.DecimalPlaces;
            _ICountryView.DateFormat = ResponseData.CountryMasterRecord.DateFormat;
            _ICountryView.TaxID = ResponseData.CountryMasterRecord.TaxID;
            _ICountryView.NearByRoundOff = ResponseData.CountryMasterRecord.NearByRoundOff;
            _ICountryView.DateSeparator = ResponseData.CountryMasterRecord.DateSeparator;
            _ICountryView.NegativeSign = ResponseData.CountryMasterRecord.NegativeSign;
            _ICountryView.CurrencySeparator = ResponseData.CountryMasterRecord.CurrencySeparator;
            _ICountryView.CurrencyID = ResponseData.CountryMasterRecord.CurrencyID;
            _ICountryView.EmailID = ResponseData.CountryMasterRecord.EmailID;
            _ICountryView.CreditLimitCheck = ResponseData.CountryMasterRecord.CreditLimitCheck;
            _ICountryView.AllowMultipleTransaction = ResponseData.CountryMasterRecord.AllowMultipleTransaction;
            _ICountryView.Active = ResponseData.CountryMasterRecord.Active;
            _ICountryView.AllowPartialReceiving = ResponseData.CountryMasterRecord.AllowPartialReceiving;
            _ICountryView.AllowSaleAndRedemption = ResponseData.CountryMasterRecord.AllowSaleAndRedemption;
            _ICountryView.OrginCountry = ResponseData.CountryMasterRecord.OrginCountry;
            _ICountryView.POSTitle = ResponseData.CountryMasterRecord.POSTitle;
            _ICountryView.PromotionRoundOff = ResponseData.CountryMasterRecord.PromotionRoundOff;
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _ICountryView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _ICountryView.Message = ResponseData.DisplayMessage;
            }

            _ICountryView.ProcessStatus = ResponseData.StatusCode;
        }
        public void GetLanguageLookUp()
        {
            var RequestData = new SelectLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _LanguageBLL.SelectLookUpLanguage(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICountryView.LanguageLookUp = ResponseData.LanguageMasterList;
            }
        }
        public void SelectCurrencyLookUp()
        {
            var RequestData = new SelectCurrencyLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICountryView.CurrencyLookup = ResponseData.CurrencyMasterList;
            }
        }
        public void GetTaxLookUp()
        {
            var RequestData = new SelectTaxLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _TaxBLL.SelectTaxLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICountryView.TaxMasterLookUp = ResponseData.TaxList;
            }
        }
        public void SelectAllCountryMaster()
        {
            var RequestData = new SelectAllCountryRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _CountryBLL.SelectAllCountryMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICountryView.CountryMasterList = ResponseData.CountryMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _ICountryView.Message = ResponseData.DisplayMessage;
            }
        }
    }
}

public class CountryMasterListPresenter
{
    ICountryList _ICountryList;
    CountryBLL _CountryBLL = new CountryBLL();

    public CountryMasterListPresenter(ICountryList ViewObj)
    {
        _ICountryList = ViewObj;
    }
    public void GetCountryMasterList()
    {
        var RequestData = new SelectAllCountryRequest();
        RequestData.ShowInActiveRecords = true;
        var ResponseData = new SelectAllCountryResponse();
        ResponseData = _CountryBLL.SelectAllCountryMaster(RequestData);
        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        {
            _ICountryList.CountryMasterList = ResponseData.CountryMasterList;
        }
        else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
        {

        }
    }
}

