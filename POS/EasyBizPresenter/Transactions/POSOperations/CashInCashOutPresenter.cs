using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POSOperations;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.PaymentDetails;
using EasyBizIView.Transactions.IPaymentDetails.ICashInCashOut;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizRequest.Transactions.PaymentDetails;
using EasyBizResponse.Masters.ReasonMasterResponse;
using EasyBizResponse.Transactions.PaymentDetails.CashInCashOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POSOperations
{
   public class CashInCashOutPresenter
    {
        ICashInCashOutView _ICashInCashOutView;
        CashInCashOutBLL _CashInCashOutBLL = new CashInCashOutBLL();
        ReasonMasterBLL _ReasonMasterBLL = new ReasonMasterBLL(); 
        public CashInCashOutPresenter(ICashInCashOutView ViewObj)
        {
            _ICashInCashOutView = ViewObj;
        }
         public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICashInCashOutView.DocumentDate == null)
            {
                _ICashInCashOutView.Message = "DocumentDate is missing Please Enter it.";
            }
           
            else if (_ICashInCashOutView.CashInCashOutDetailsList == null)
            {
                _ICashInCashOutView.Message = "CashInCashOutDetails is missing Please Select it.";
            }

            else if (_ICashInCashOutView.Total == 0)
            {
                _ICashInCashOutView.Message = "Line Details is invalid";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveCashInCashOut()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCashInCashOutRequest();
                    RequestData.CashInCashOutMasterRecord = new CashInCashOutMaster();
                    if (_ICashInCashOutView.CashInCashOutDetailsList.Count !=0)
                    {
                        RequestData.CashInCashOutDetailsList = _ICashInCashOutView.CashInCashOutDetailsList;
                        RequestData.CashInCashOutMasterRecord.ID = _ICashInCashOutView.ID;                        
                        RequestData.CashInCashOutMasterRecord.Total = _ICashInCashOutView.Total;
                        RequestData.CashInCashOutMasterRecord.DocumentDate = _ICashInCashOutView.DocumentDate;
                        RequestData.CashInCashOutMasterRecord.CreateBy = _ICashInCashOutView.UserID;
                        RequestData.CashInCashOutMasterRecord.CreateOn = DateTime.Now;
                        RequestData.CashInCashOutMasterRecord.Active = true;
                        RequestData.CashInCashOutMasterRecord.StoreID = _ICashInCashOutView.StoreID;
                        RequestData.CashInCashOutMasterRecord.StoreCode = _ICashInCashOutView.StoreCode;
                        RequestData.CashInCashOutMasterRecord.POSID = _ICashInCashOutView.POSID;
                        RequestData.CashInCashOutMasterRecord.POSCode = _ICashInCashOutView.POSCode;
                        RequestData.CashInCashOutMasterRecord.ShiftID = _ICashInCashOutView.ShiftID;
                        RequestData.CashInCashOutMasterRecord.ShiftCode = _ICashInCashOutView.ShiftCode;
                        RequestData.CashInCashOutMasterRecord.SCN = _ICashInCashOutView.SCN;
                        var ResponseData = _CashInCashOutBLL.SaveCashInCashOut(RequestData);
                        _ICashInCashOutView.Message = ResponseData.DisplayMessage;
                        _ICashInCashOutView.ProcessStatus = ResponseData.StatusCode;
                    }
                    else
                    {
                        _ICashInCashOutView.Message = "Line Details is invalid";
                    }
                   
                }
                else
                {
                    _ICashInCashOutView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        public void SelectCashInCashOutMasterRecord()
        {
            try
            {
                var RequestData = new SelectByCashInCashOutIDRequest();
                RequestData.ID = _ICashInCashOutView.ID;
                var ResponseData = _CashInCashOutBLL.SelectCashInCashOutRecord(RequestData);
                _ICashInCashOutView.ID = ResponseData.CashInCashOutMasterRecord.ID;
                _ICashInCashOutView.Total = ResponseData.CashInCashOutMasterRecord.Total;
                _ICashInCashOutView.DocumentDate = ResponseData.CashInCashOutMasterRecord.DocumentDate;  
                          
                _ICashInCashOutView.SCN = ResponseData.CashInCashOutMasterRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICashInCashOutView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ICashInCashOutView.Message = ResponseData.DisplayMessage;
                }
                _ICashInCashOutView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectCashInCashOutDetails()
        {
            SelectCashInCashOutDetailsRequest RequestData = new SelectCashInCashOutDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _ICashInCashOutView.ID;
            SelectCashInCashOutDetailsResponse ResponseData = _CashInCashOutBLL.SelectAllStoreGroupDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICashInCashOutView.CashInCashOutDetailsList = ResponseData.CashInCashOutDetailsRecord;
            }
            else
            {
                _ICashInCashOutView.Message = ResponseData.DisplayMessage;
                _ICashInCashOutView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void DeleteCashInCashOut()
        {
            try
            {
                var RequestData = new DeleteCashInCashOutRequest();
                RequestData.ID = _ICashInCashOutView.ID;
                var ResponseData = _CashInCashOutBLL.DeleteCashInCashOut(RequestData);
                _ICashInCashOutView.Message = ResponseData.DisplayMessage;
                _ICashInCashOutView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectReasonLookup()
        {
            var RequestData = new SelectReasonMasterLookUpRequest();

            var ResponseData = _ReasonMasterBLL.SelectReasonMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ICashInCashOutView.ReasonMasterList = ResponseData.ReasonMasterList;
            }
            else
            {
                _ICashInCashOutView.Message = ResponseData.DisplayMessage;
                _ICashInCashOutView.ProcessStatus = ResponseData.StatusCode;
            }
        }
    }
   public class CashInCashOutListPresenter
   {
       ICashInCashOutCollectionView _ICashInCashOutCollectionView;
       CashInCashOutBLL _CashInCashOutBLL = new CashInCashOutBLL();

       public CashInCashOutListPresenter(ICashInCashOutCollectionView ViewObj)
       {
           _ICashInCashOutCollectionView = ViewObj;
       }

       public void GetCashInCashOutlist()
       {
           try
           {
               var RequestData = new SelectAllCashInCashOutRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllCashInCashOutResponse();
               ResponseData = _CashInCashOutBLL.SelectAllCashInCashOut (RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICashInCashOutCollectionView.CashInCashOutMasterList = ResponseData.CashInCashOutMasterList;
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
       public void GetCashInlist()
       {
           try
           {
               var RequestData = new SelectAllCashInCashOutRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllCashInCashOutResponse();
               RequestData.Type = "CashIn";
               ResponseData = _CashInCashOutBLL.SelectAllCashInCashOut(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICashInCashOutCollectionView.CashInCashOutMasterList = ResponseData.CashInCashOutMasterList;
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
       public void GetCashOutlist()
       {
           try
           {
               var RequestData = new SelectAllCashInCashOutRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllCashInCashOutResponse();
               RequestData.Type = "CashOut";
               ResponseData = _CashInCashOutBLL.SelectAllCashInCashOut(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ICashInCashOutCollectionView.CashInCashOutMasterList = ResponseData.CashInCashOutMasterList;
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
