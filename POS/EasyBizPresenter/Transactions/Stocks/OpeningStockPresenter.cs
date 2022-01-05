using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Transactions.IOpeningStock;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Transactions.Stocks.OpeningStock;
using EasyBizBLL.Transactions.Stocks;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.Stocks.StockRequest;

namespace EasyBizPresenter.Transactions.Stocks
{
   public class OpeningStockPresenter
    {
       IOpeningStockView _IOpeningStockView;
       DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
       SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
       StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
       OpeningStockBLL _OpeningStockBLL = new OpeningStockBLL();
       TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
       int _RunningNo;
       int _DetailID;
       public OpeningStockPresenter(IOpeningStockView ViewObj)
        {
            _IOpeningStockView = ViewObj;
        }
       public void SaveOpeningStock()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveOpeningStockRequest();
                   RequestData.OpeningStockHeaderRecord = new OpeningStockHeader();
                   RequestData.OpeningStockDetailsList = _IOpeningStockView.OpeningStockDetailsList;
                   RequestData.TransactionLogList = _IOpeningStockView.TransactionLogList;
                   RequestData.OpeningStockHeaderRecord.ID = _IOpeningStockView.ID;
                   RequestData.OpeningStockHeaderRecord.DocumentNo = _IOpeningStockView.DocumentNo;
                   RequestData.OpeningStockHeaderRecord.DocumentDate = _IOpeningStockView.DocumentDate;
                   RequestData.OpeningStockHeaderRecord.TotalQuantity = _IOpeningStockView.TotalQuantity;
                   RequestData.OpeningStockHeaderRecord.StoreID = _IOpeningStockView.StoreID;
                   RequestData.OpeningStockHeaderRecord.StoreCode = _IOpeningStockView.StoreCode;
                   RequestData.OpeningStockHeaderRecord.Remarks = _IOpeningStockView.Remarks;
                   RequestData.OpeningStockHeaderRecord.CreateBy = _IOpeningStockView.UserID;
                   RequestData.OpeningStockHeaderRecord.CreateOn = DateTime.Now;
                   RequestData.OpeningStockHeaderRecord.SCN = _IOpeningStockView.SCN;
                   var ResponseData = _OpeningStockBLL.SaveOpeningStock(RequestData);
                   _IOpeningStockView.Message = ResponseData.DisplayMessage;
                   _IOpeningStockView.ProcessStatus = ResponseData.StatusCode;

                   if (_IOpeningStockView.ProcessStatus == Enums.OpStatusCode.Success)
                   {
                       _IOpeningStockView.OpeningHeaderID = Convert.ToInt32(ResponseData.IDs);
                       UpdateRunningNo();
                   }
                   else
                   {
                       _IOpeningStockView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SaveTransactionsLog()
       {
           var RequestData = new SaveTransactionLogRequest();

           RequestData.TransactionLogList = _IOpeningStockView.TransactionLogList;
           var ResponseData = _TransactionLogBLL.SaveTransactionLog(RequestData);

       }
       public void UpdateRunningNo()
       {
           try
           {
               UpdateRunningNumRequest objUpdateRunningNumRequest = new UpdateRunningNumRequest();
               UpdateRunningNumResponse objUpdateRunningNumResponse = new UpdateRunningNumResponse();

               objUpdateRunningNumRequest.RunningNo = _RunningNo;
               objUpdateRunningNumRequest.DetailID = _DetailID;
               objUpdateRunningNumRequest.StoreID = _IOpeningStockView.StoreID;
               objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public bool IsValidForm()
       {
           bool objBool = false;
          
               if (_IOpeningStockView.DocumentNo == "")
               {
                   _IOpeningStockView.Message = "DocumentNo is missing.Please Make a entry in Document Numbering Screen.";
               }
               else if (_IOpeningStockView.DocumentDate == null)
               {
                   _IOpeningStockView.Message = "DocumentDate is missing Please Enter it.";
               }
               else if (_IOpeningStockView.OpeningStockDetailsList.Count == 0)
               {
                   _IOpeningStockView.Message = "OpeningStockDetails is missing Please Select it.";
               }              
               else
               {
                   objBool = true;
               }
               return objBool;                   
          
       }
       public void SelectDocumentNumberingRecord()
       {
           try
           {
               var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
               RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
               RequestData.DocumentTypeID = (int)Enums.DocumentType.OPENINGSTOCK;
               RequestData.StoreID = _IOpeningStockView.StoreID;
               RequestData.StoreCode = _IOpeningStockView.StoreCode;
               var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   string DocumentNo = string.Empty;
                   DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                   _IOpeningStockView.DocumentNo = DocumentNo;

                   _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                   _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetStoreMasterLookUP()
       {
           SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
           RequestData.ShowInActiveRecords = false;
           RequestData.StoreID = _IOpeningStockView.StoreID;
           RequestData.StoreCode = _IOpeningStockView.StoreCode;
           SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStorename(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IOpeningStockView.StoreMasterRecord = ResponseData.StoreMasterData;
           }
       }


       public void SelectStockRequestHeaderRecord()
       {
           try
           {
               var RequestData = new SelectByIDOpeningStockHeaderRequest();
               RequestData.ID = _IOpeningStockView.ID;
               var ResponseData = _OpeningStockBLL.SelectStockRequestRecord(RequestData);
               _IOpeningStockView.ID = ResponseData.OpeningStockHeaderRecord.ID;
               _IOpeningStockView.TotalQuantity = ResponseData.OpeningStockHeaderRecord.TotalQuantity;
               _IOpeningStockView.DocumentDate = ResponseData.OpeningStockHeaderRecord.DocumentDate;
               _IOpeningStockView.DocumentNo = ResponseData.OpeningStockHeaderRecord.DocumentNo;
               _IOpeningStockView.Remarks = ResponseData.OpeningStockHeaderRecord.Remarks;
               _IOpeningStockView.SCN = ResponseData.OpeningStockHeaderRecord.SCN;

               if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _IOpeningStockView.Message = ResponseData.DisplayMessage;
               }
               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _IOpeningStockView.Message = ResponseData.DisplayMessage;
               }
               _IOpeningStockView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SelectStockRequestDetails()
       {
           SelectByOpeningStockDetailsRequest RequestData = new SelectByOpeningStockDetailsRequest();          
           RequestData.ID = _IOpeningStockView.ID;
           var ResponseData = _OpeningStockBLL.SelectOpeningStockDetails(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IOpeningStockView.OpeningStockDetailsList = ResponseData.OpeningStockDetailsRecord;
           }
           else
           {
               _IOpeningStockView.Message = ResponseData.DisplayMessage;
               _IOpeningStockView.ProcessStatus = ResponseData.StatusCode;
           }
       }
       public void GetSKU()
       {
           try
           {
               var RequestData = new SelectAllSKUMasterRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.Count = 1;
               RequestData.SearchString = _IOpeningStockView.SKUSearchString;
               RequestData.Mode = "SALES";
               RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
               var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IOpeningStockView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
               }
               else
               {
                   _IOpeningStockView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }

   public class OpeningStockCollectionPresenter
   {
       IOpeningStockCollectionView _IOpeningStockCollectionView;
       OpeningStockBLL _OpeningStockBLL = new OpeningStockBLL();
       public OpeningStockCollectionPresenter(IOpeningStockCollectionView ViewObj)
       {
           _IOpeningStockCollectionView = ViewObj;
       }

       public void GetOpeningStocklist()
       {
           try
           {
               var RequestData = new SelectAllOpeningStockRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllOpeningStockResponse();
               ResponseData = _OpeningStockBLL.SelectAllOpeningStock(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IOpeningStockCollectionView.OpeningStockHeaderList = ResponseData.OpeningStockHeaderList;
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
