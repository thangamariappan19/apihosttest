using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IExchangeRate;
using EasyBizPresenter.Common;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizResponse.Masters.ExchangeRatesResponse;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class ExchangeRatePresenter
    {
        IExchangeRateView _IExchangeRateView;
        ExchangeRatesBLL _ExchangeRatesBLL = new ExchangeRatesBLL();
        public ExchangeRatePresenter(IExchangeRateView ViewObj)
        {
            _IExchangeRateView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IExchangeRateView.ExchangeRatesCode.Trim() == string.Empty)
            {
                _IExchangeRateView.Message = " Code is missing Please Enter it.";
            }
            else if (_IExchangeRateView.ExchangeRatesCode.Length > 8)
            {
                _IExchangeRateView.Message = " Please Enter Valid Code.";
            }
          
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void GetCurrencyList()
        {
            var _CurrencyBLL = new CurrencyBLL();
            var RequestData = new SelectAllCurrencyRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _CurrencyBLL.SelectAllCurrencyMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IExchangeRateView.CurrencyList = ResponseData.CurrencyMasterList;
            }
        }
        public void SelectExchangeRatesRecord()
        {
            try
            {
                var RequestData = new SelectByIDExchangeRatesRequest();
                RequestData.ExchangeRatesCode = _IExchangeRateView.ExchangeRatesCode;
                var ResponseData = _ExchangeRatesBLL.SelectExchangeRatesRecord(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {

                    _IExchangeRateView.ExchangeRatesList = ResponseData.ExchangeRatesList;

                    List<CurrencyMaster> CurrencyList = _IExchangeRateView.CurrencyList;
                    int BaseCurrencyID = ResponseData.ExchangeRatesList.FirstOrDefault().BaseCurrencyID;
                    var FilteredCurrencyList = new List<CurrencyMaster>();
                    FilteredCurrencyList = CurrencyList.Where(x => x.ID != BaseCurrencyID).ToList();


                    List<ExpandoObject> ExpandoObjectList = new List<ExpandoObject>();
                   

                    List<ExchangeRates> GroupExchangeRatesList=new List<ExchangeRates>();
                    GroupExchangeRatesList = ResponseData.ExchangeRatesList.GroupBy(x => new { x.ExchangeRateDate, x.BaseCurrencyID }).Select(grp => grp.First()).ToList();

                    foreach(ExchangeRates objExchangeRates in GroupExchangeRatesList)
                    {
                        dynamic expando = new ExpandoObject();
                        expando.ID = objExchangeRates.ID;
                        expando.ExchangeRateDate = objExchangeRates.ExchangeRateDate;
                        foreach (CurrencyMaster objCurrencyMaster in FilteredCurrencyList)
                        {
                            var ExchangeRatesList = new List<ExchangeRates>();
                            ExchangeRatesList = ResponseData.ExchangeRatesList.Where(x => x.BaseCurrencyID == BaseCurrencyID && x.ExchangeRateDate == objExchangeRates.ExchangeRateDate && x.TargetCurrencyID == objCurrencyMaster.ID).ToList();
                            foreach (ExchangeRates TempExchangeRates in ExchangeRatesList)
                            {
                                DynamicProperty.AddProperty(expando, "Currency-" + objCurrencyMaster.ID, objCurrencyMaster.ID);
                                DynamicProperty.AddProperty(expando, objCurrencyMaster.ID + "-InternationalCode", objCurrencyMaster.InternationalCode);
                                DynamicProperty.AddProperty(expando, objCurrencyMaster.InternationalCode, TempExchangeRates.ExchangeAmount);

                                
                            }

                        }
                        ExpandoObjectList.Add(expando);
                    }
                    _IExchangeRateView.ExchangeExpandoList = ExpandoObjectList;
                }
                else
                {
                    _IExchangeRateView.Message = ResponseData.DisplayMessage;
                }
                _IExchangeRateView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public void SaveExchangeRate()
        {
            try
            {               
                    var RequestData = new SaveExchangeRatesRequest();
                    RequestData.ExchangeRatesDate = _IExchangeRateView.ExchangeRateDate;
                    RequestData.ExchangeRateslist = _IExchangeRateView.ExchangeRatesList;
                    var ResponseData = _ExchangeRatesBLL.SaveExchangeRates(RequestData);
                    _IExchangeRateView.Message = ResponseData.DisplayMessage;
                    _IExchangeRateView.ProcessStatus = ResponseData.StatusCode;
               
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
    public class ExchangeRateListPresenter
    {
        IExchangeRateListView _IExchangeRateListView;
        ExchangeRatesBLL _ExchangeRatesBLL = new ExchangeRatesBLL();

        public ExchangeRateListPresenter(IExchangeRateListView ViewObj)
        {
            _IExchangeRateListView = ViewObj;
        }

        public void GetExchangeRatesList()
        {
            try
            {
                var RequestData = new SelectAllExchangeRatesRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllExchangeRatesResponse();
                ResponseData = _ExchangeRatesBLL.SelectAllExchangeRatesRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IExchangeRateListView.ExchangeRatesList = ResponseData.ExchangeRatesList;
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
        public void GetExchangeRateByBusinessDate()
        {
            try
            {
                var _ExchangeRatesBLL = new ExchangeRatesBLL();
                var RequestData = new SelectCurrecnyExchangeRatesRequest();
                RequestData.Exchangedate = _IExchangeRateListView.BusinessDate;
                var ResponseData = _ExchangeRatesBLL.SelectCurrenctExchangeRates(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IExchangeRateListView.ExchangeRatesList = ResponseData.CurrencyExchangeRatesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 
}
