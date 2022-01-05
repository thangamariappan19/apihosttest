using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizResponse.Masters.ExchangeRatesResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class ExchangeRatesBLL
    {
        public SaveExchangeRatesResponse SaveExchangeRates(SaveExchangeRatesRequest objRequest)
        {
            SaveExchangeRatesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 11.09.2018
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                var objBaseExchangeRatesDAL = objFactory.GetDALRepository().GetBaseExchangeRatesDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    //var objExchangeRates = new ExchangeRates();
                    //objExchangeRates = (ExchangeRates)objRequest.RequestDynamicData;
                    //objRequest.ExchangeRatesDate = objExchangeRates.ExchangeRateDate;
                    //objRequest.ExchangeRateslist = objExchangeRates.ExchangeRateslist;

                    var objExchangeRates = new List<ExchangeRates>();
                    objExchangeRates.AddRange(objRequest.RequestDynamicData);
                    objRequest.ExchangeRatesDate = objExchangeRates.FirstOrDefault().ExchangeRateDate;
                    objRequest.ExchangeRateslist = objExchangeRates;
                }
                objResponse = (SaveExchangeRatesResponse)objBaseExchangeRatesDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = Enums.RequestFrom.MainServer;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                //    objRequest.DocumentType = Enums.DocumentType.EXCHANGERATE;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.ExchangeRatesBLL", "SaveExchangeRates");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveExchangeRatesResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ExchangeRates");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllExchangeRatesResponse API_SelectALL(SelectAllExchangeRatesRequest requestData)
        {
            SelectAllExchangeRatesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExchangeRatesDAL = objFactory.GetDALRepository().GetBaseExchangeRatesDAL();
                objResponse = (SelectAllExchangeRatesResponse)objBaseExchangeRatesDAL.API_SelectALL(requestData);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllExchangeRatesResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ExchangeRates Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllExchangeRatesResponse SelectAllExchangeRatesRecords(SelectAllExchangeRatesRequest objRequest)
        {
            SelectAllExchangeRatesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExchangeRatesDAL = objFactory.GetDALRepository().GetBaseExchangeRatesDAL();
                objResponse = (SelectAllExchangeRatesResponse)objBaseExchangeRatesDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllExchangeRatesResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ExchangeRates Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectCurrecnyExchangeRatesResponse SelectAllExchangeRatesCurrencyRecords(SelectCurrecnyExchangeRatesRequest objRequest)
        {
            SelectCurrecnyExchangeRatesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExchangeRatesDAL = objFactory.GetDALRepository().GetBaseExchangeRatesDAL();
                objResponse = (SelectCurrecnyExchangeRatesResponse)objBaseExchangeRatesDAL.API_SelectCurrencyALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCurrecnyExchangeRatesResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ExchangeRates Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectByIDExchangeRatesResponse SelectExchangeRatesRecord(SelectByIDExchangeRatesRequest objRequest)
        {
            SelectByIDExchangeRatesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                // Changed by Senthamil @ 11.09.2018
                var objBaseExchangeRatesDAL = objFactory.GetDALRepository().GetBaseExchangeRatesDAL();
                if(string.IsNullOrEmpty(objRequest.ExchangeRatesCode))
                {
                    if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        objRequest.ExchangeRatesCode = objRequest.DocumentIDs;
                    }
                }
                objResponse = (SelectByIDExchangeRatesResponse)objBaseExchangeRatesDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDExchangeRatesResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ExchangeRates");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SelectCurrecnyExchangeRatesResponse SelectCurrenctExchangeRates(SelectCurrecnyExchangeRatesRequest objRequest)
        {
            SelectCurrecnyExchangeRatesResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseExchangeRatesDAL = objFactory.GetDALRepository().GetBaseExchangeRatesDAL();
                objResponse = (SelectCurrecnyExchangeRatesResponse)objBaseExchangeRatesDAL.SelectCurrecnyExchangeRates(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectCurrecnyExchangeRatesResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "CurrecnyExchangeRates");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
    }
}
