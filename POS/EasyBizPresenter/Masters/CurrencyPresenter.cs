using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.Currency;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizResponse.Masters.CurrencyResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class CurrencyPresenter
    {

        ICurrencyView _ICurrencyMasterView;
        CurrencyBLL _CurrencyMasterBLL = new CurrencyBLL();

        public CurrencyPresenter(ICurrencyView ViewObj)
        {
            _ICurrencyMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICurrencyMasterView.CurrencyCode.Trim() == string.Empty)
            {
                _ICurrencyMasterView.Message = "Currency Code is missing Please Enter it.";
            }
            else if (_ICurrencyMasterView.CurrencyCode.Length > 8)
            {
                _ICurrencyMasterView.Message = " Please Enter Vail Code.";
            }
            else if (_ICurrencyMasterView.CurrencyName.Trim() == string.Empty)
            {
                _ICurrencyMasterView.Message = "Currency Name is missing Please Enter it. ";
            }
            else if (_ICurrencyMasterView.CurrencySymbol.Trim() == string.Empty)
            {
                _ICurrencyMasterView.Message = "Currency Symbol is missing Please Enter it. ";
            }
            else if (_ICurrencyMasterView.InternationalCode.Trim() == string.Empty)
            {
                _ICurrencyMasterView.Message = "International Code is missing Please Enter it.";
            }
            else if (_ICurrencyMasterView.DecimalPlaces == null)
            {
                _ICurrencyMasterView.Message = "Decimal value is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveCurrencyMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveCurrencyRequest();
                RequestData.CurrencyMasterData = new CurrencyMaster();

                RequestData.CurrencyMasterData.ID = _ICurrencyMasterView.ID;
                RequestData.CurrencyMasterData.CurrencyCode = _ICurrencyMasterView.CurrencyCode;
                RequestData.CurrencyMasterData.CurrencyName = _ICurrencyMasterView.CurrencyName;
                RequestData.CurrencyMasterData.CurrencySymbol = _ICurrencyMasterView.CurrencySymbol;
                RequestData.CurrencyMasterData.InternationalCode = _ICurrencyMasterView.InternationalCode;
                RequestData.CurrencyMasterData.DecimalPlaces = _ICurrencyMasterView.DecimalPlaces;
                RequestData.CurrencyMasterData.CreateBy = _ICurrencyMasterView.UserID;
                RequestData.CurrencyMasterData.CreateOn = DateTime.Now;
                RequestData.CurrencyMasterData.Active = _ICurrencyMasterView.Active;
                RequestData.CurrencyMasterData.SCN = _ICurrencyMasterView.SCN;
                RequestData.CurrencyMasterData.CurrencyType = _ICurrencyMasterView.CurrencyType;
                RequestData.CurrencyMasterData.MRoundValue = _ICurrencyMasterView.MRoundValue;
                RequestData.CurrencyMasterData.InterDescription = _ICurrencyMasterView.InterDescription;
                RequestData.CurrencyMasterData.HundredthName = _ICurrencyMasterView.HundredthName;
                RequestData.CurrencyMasterData.English = _ICurrencyMasterView.English;
                RequestData.CurrencyMasterData.EngHundredthName = _ICurrencyMasterView.EngHundredthName;
				RequestData.CurrencyMasterData.CurrencyDetailsList = _ICurrencyMasterView.CurrencyDetailsList;

                var ResponseData = _CurrencyMasterBLL.SaveCurrencyMaster(RequestData);

                _ICurrencyMasterView.Message = ResponseData.DisplayMessage;
                _ICurrencyMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ICurrencyMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateCurrencyMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateCurrencyRequest();
                RequestData.CurrencyMasterData = new CurrencyMaster();
                RequestData.CurrencyMasterData.ID = _ICurrencyMasterView.ID;
                RequestData.CurrencyMasterData.CurrencyCode = _ICurrencyMasterView.CurrencyCode;
                RequestData.CurrencyMasterData.CurrencyName = _ICurrencyMasterView.CurrencyName;
                RequestData.CurrencyMasterData.CurrencySymbol = _ICurrencyMasterView.CurrencySymbol;
                RequestData.CurrencyMasterData.InternationalCode = _ICurrencyMasterView.InternationalCode;
                RequestData.CurrencyMasterData.DecimalPlaces = _ICurrencyMasterView.DecimalPlaces;
                RequestData.CurrencyMasterData.UpdateBy = _ICurrencyMasterView.UserID;
                RequestData.CurrencyMasterData.UpdateOn = DateTime.Now;
                RequestData.CurrencyMasterData.Active = _ICurrencyMasterView.Active;
                RequestData.CurrencyMasterData.SCN = _ICurrencyMasterView.SCN;
                RequestData.CurrencyMasterData.MRoundValue = _ICurrencyMasterView.MRoundValue;
                RequestData.CurrencyMasterData.InterDescription = _ICurrencyMasterView.InterDescription;
                RequestData.CurrencyMasterData.HundredthName = _ICurrencyMasterView.HundredthName;
                RequestData.CurrencyMasterData.English = _ICurrencyMasterView.English;
                RequestData.CurrencyMasterData.EngHundredthName = _ICurrencyMasterView.EngHundredthName; 
                RequestData.CurrencyMasterData.CurrencyType = _ICurrencyMasterView.CurrencyType;
				RequestData.CurrencyMasterData.CurrencyDetailsList = _ICurrencyMasterView.CurrencyDetailsList;

                var ResponseData = _CurrencyMasterBLL.UpdateCurrencyMaster(RequestData);

                _ICurrencyMasterView.Message = ResponseData.DisplayMessage;
                _ICurrencyMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _ICurrencyMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteCurrencyMaster()
        {
            try
            {
            var RequestData = new DeleteCurrencyRequest();
            RequestData.ID = -_ICurrencyMasterView.ID;
            var ResponseData = _CurrencyMasterBLL.DeleteCurrencyMaster(RequestData);
            _ICurrencyMasterView.Message = ResponseData.DisplayMessage;
            _ICurrencyMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCurrencyMaster()
        {
            var RequestData = new SelectByIDCurrencyRequest();
            RequestData.ID = _ICurrencyMasterView.ID;
            var ResponseData = _CurrencyMasterBLL.SelectCurrencyMaster(RequestData);

            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICurrencyMasterView.CurrencyCode = ResponseData.CurrencyMasterRecord.CurrencyCode;
                _ICurrencyMasterView.CurrencyName = ResponseData.CurrencyMasterRecord.CurrencyName;
                _ICurrencyMasterView.CurrencySymbol = ResponseData.CurrencyMasterRecord.CurrencySymbol;
                _ICurrencyMasterView.InternationalCode = ResponseData.CurrencyMasterRecord.InternationalCode;
                _ICurrencyMasterView.DecimalPlaces = ResponseData.CurrencyMasterRecord.DecimalPlaces;
                _ICurrencyMasterView.CurrencyType = ResponseData.CurrencyMasterRecord.CurrencyType;
                _ICurrencyMasterView.MRoundValue = ResponseData.CurrencyMasterRecord.MRoundValue;
                _ICurrencyMasterView.InterDescription = ResponseData.CurrencyMasterRecord.InterDescription;
                _ICurrencyMasterView.HundredthName = ResponseData.CurrencyMasterRecord.HundredthName;
                _ICurrencyMasterView.English = ResponseData.CurrencyMasterRecord.English;
                _ICurrencyMasterView.EngHundredthName = ResponseData.CurrencyMasterRecord.EngHundredthName;
                _ICurrencyMasterView.Active = ResponseData.CurrencyMasterRecord.Active;
				_ICurrencyMasterView.CurrencyDetailsList = ResponseData.CurrencyMasterRecord.CurrencyDetailsList;
            }
            else
            {
                _ICurrencyMasterView.Message = ResponseData.DisplayMessage;
            }
            _ICurrencyMasterView.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectAllCurrencyMaster()
        {
            var RequestData = new SelectAllCurrencyRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _CurrencyMasterBLL.SelectAllCurrencyMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICurrencyMasterView.CurrencyMasterList = ResponseData.CurrencyMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _ICurrencyMasterView.Message = ResponseData.DisplayMessage;
            }
        }
    }

    public class CurrencyListPresenter
    {
        ICurrencyList _ICurrencyMasterList;
        CurrencyBLL _CurrencyMasterBLL = new CurrencyBLL();

        public CurrencyListPresenter(ICurrencyList ViewObj)
        {
            _ICurrencyMasterList = ViewObj;
        }
        public void GetCurrencyMasterList()
        {
            var RequestData = new SelectAllCurrencyRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllCurrencyResponse();
            ResponseData = _CurrencyMasterBLL.SelectAllCurrencyMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICurrencyMasterList.ICurrencyMasterList = ResponseData.CurrencyMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {

            }
        }
    }
}

