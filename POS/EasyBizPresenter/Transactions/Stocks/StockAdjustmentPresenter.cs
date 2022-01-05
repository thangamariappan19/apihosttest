using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Stocks;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizIView.Transactions.IStockAdjustment;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizRequest.Transactions.Stocks.StockStaging;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.ScaleMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Transactions.Stocks.StockStaging;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;

namespace EasyBizPresenter.Transactions.Stocks
{
   public class StockAdjustmentPresenter
    {
       IStockAdjustmentView _IStockAdjustmentView;
       StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
       TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
       StockAdjustmentBLL _StockAdjustmentBLL = new StockAdjustmentBLL();
       DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();

       int _RunningNo;
       int _DetailID;
       public StockAdjustmentPresenter(IStockAdjustmentView ViewObj)
        {
            _IStockAdjustmentView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_IStockAdjustmentView.DocumentNumber == "")
           {
               _IStockAdjustmentView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
           }     
           else if (_IStockAdjustmentView.DocumentDate == null)
           {
               _IStockAdjustmentView.Message = "DocumentDate is missing Please Enter it.";
           }          

           else
           {
               objBool = true;
           }
           return objBool;
       }       
       public void GetStyleLookUp()
       {
           try
           {
               var RequestData = new SelectStyleLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _StyleMasterBLL.SelectStyleLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   //_IStockAdjustmentView.StyleMasterLookUp = ResponseData.StyleMasterList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetDocumentNumber()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.STOCKADJUSTMENT;                
                RequestData.StoreID = _IStockAdjustmentView.StoreID;
                RequestData.StoreCode = _IStockAdjustmentView.StoreCode;
                RequestData.CountryID = _IStockAdjustmentView.CountryID;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNumber = string.Empty;
                    DocumentNumber = DocumentNumber.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _IStockAdjustmentView.DocumentNumber = DocumentNumber;
                 
                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public void UpdateRunningNo()
       {
           try
           {
               UpdateRunningNumRequest objUpdateRunningNumRequest = new UpdateRunningNumRequest();
               UpdateRunningNumResponse objUpdateRunningNumResponse = new UpdateRunningNumResponse();

               objUpdateRunningNumRequest.RunningNo = _RunningNo;
               objUpdateRunningNumRequest.DetailID = _DetailID;
               objUpdateRunningNumRequest.StoreID = _IStockAdjustmentView.StoreID;
               objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       //public void SelectStyleWithScaleDetails()
       //{
       //    try
       //    {
       //        var RequestData = new SelectScaleDetailsRequest();
       //        RequestData.ShowInActiveRecords = true;
       //        RequestData.ID = _IStockAdjustmentView.StyleID;
       //        RequestData.StyleCode = _IStockAdjustmentView.StyleCode;
       //        var ResponseData = _StyleMasterBLL.SelectStyleWithScaleRecord(RequestData);

       //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
       //        {
       //            _IStockAdjustmentView.StyleWithScaleDetailList = ResponseData.ScaleDetailMasterRecord;
       //        }
       //        else
       //        {
       //            _IStockAdjustmentView.Message = ResponseData.DisplayMessage;
       //            _IStockAdjustmentView.ProcessStatus = ResponseData.StatusCode;

       //            var ScaleList = new List<ScaleDetailMaster>();
       //            _IStockAdjustmentView.StyleWithScaleDetailList = ScaleList;
                   
       //        }
       //    }
       //    catch(Exception ex)
       //    {
       //        throw ex;
       //    }
          
       //}
       public void SelectStyleWithScaleDetails()
       {
           try
           {
               var RequestData = new SelectStyleWithScaleforStockRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.ID = _IStockAdjustmentView.StyleID;
               RequestData.StyleCode = _IStockAdjustmentView.StyleCode;

               var ResponseData = _StyleMasterBLL.SelectStyleWithScaleRecordForStock(RequestData);

               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IStockAdjustmentView.StyleWithScaleDetailList = ResponseData.ScaleDetailMasterRecordForStock;
               }
               else
               {                  
                   var ScaleList = new List<ScaleDetailMaster>();
                   _IStockAdjustmentView.StyleWithScaleDetailList = ScaleList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public void SelectStyleWithColorDetails()
       {
           try
           {
               var RequestData = new SelectColorDetailsRequest();
               var ResponseData = new SelectColorDetailsResponse();
               RequestData.ShowInActiveRecords = true;
               RequestData.ID = _IStockAdjustmentView.StyleID;
               RequestData.StyleCode = _IStockAdjustmentView.StyleCode;

               ResponseData = _StyleMasterBLL.SelectStyleWithColorDetailsRecord(RequestData);

               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IStockAdjustmentView.StyleWithColorList = ResponseData.StyleWithColorDetailsRecord;
               }
               else
               {
                   //_IStockAdjustmentView.Message = ResponseData.DisplayMessage;
                   //_IStockAdjustmentView.ProcessStatus = ResponseData.StatusCode;

                   var ColorList = new List<ColorMaster>();
                   _IStockAdjustmentView.StyleWithColorList = ColorList;
               }
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
       public void GetStockList()
       {
           try
           {
               var RequestData = new GetStockByStyleCodeRequest();
               var ResponseData = new GetStockByStyleCodeResponse();

               RequestData.StyleCode = _IStockAdjustmentView.StyleCode;
               RequestData.StockWiseName = "All";
               ResponseData = _TransactionLogBLL.GetStockByStyleCode(RequestData);

               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IStockAdjustmentView.StockList = ResponseData.StockList;
               }
               else
               {
                   var StockList = new List<TransactionLog>();
                   _IStockAdjustmentView.StockList = new List<TransactionLog>();
                   _IStockAdjustmentView.StockList = StockList;
               }
           }
           catch(Exception ex)
           {
               throw ex;
           }
           
       }
       public void SaveStockAdjustemnt()
       {
           try
           {
               var RequestData = new SaveStockAdjustmentRequest();
               var ResponseData = new SaveStockAdjustmentResponse();
               RequestData.RequestedByUserID = _IStockAdjustmentView.UserID;
               RequestData.StockAdjustmentRecord = _IStockAdjustmentView.StockAdjustmentRecord;
               RequestData.TransactionLogList = _IStockAdjustmentView.TransactionLogList;
               ResponseData = _StockAdjustmentBLL.SaveStockAdjustment(RequestData);
               _IStockAdjustmentView.Message = ResponseData.DisplayMessage;
               _IStockAdjustmentView.HeaderID = ResponseData.IDs;
               _IStockAdjustmentView.ProcessStatus = ResponseData.StatusCode;

               if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   UpdateRunningNo();
               }
               
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }


       public void SaveTransactionsLog()
       {
           var RequestData = new SaveTransactionLogRequest();
           RequestData.StoreID = _IStockAdjustmentView.StoreID;
           RequestData.StoreCode = _IStockAdjustmentView.StoreCode;
           RequestData.TransactionLogList = _IStockAdjustmentView.TransactionLogList;
           var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);

       }      
    }



   public class StockAdjustmentPresenterList
   {

       StockAdjustmentBLL _StockAdjustmentBLL = new StockAdjustmentBLL();
       IStockAdjustmentCollectionView _IStockAdjustmentCollectionView;
       public StockAdjustmentPresenterList(IStockAdjustmentCollectionView ViewObj)
       {
           _IStockAdjustmentCollectionView = ViewObj;
       }
       public void GetStockAdustmentList()
       {
            try
           {

               var RequestData = new GetAllStockAdjustmentRecordRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new GetAllStockAdjustmentRecordResponse();
               ResponseData = _StockAdjustmentBLL.SelectStockAdjustment(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IStockAdjustmentCollectionView.StockAdjustmentHeaderList = ResponseData.StockAdjustmentList;
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
