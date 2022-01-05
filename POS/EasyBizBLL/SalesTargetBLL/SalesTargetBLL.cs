using EasyBizAbsDAL.SalesTarget;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizFactory;
using EasyBizRequest.SalesTargetRequest;
using EasyBizResponse.SalesTargetResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.SalesTargetBLL
{
   public class SalesTargetBLL
    {
       public SalestargetHistoryResponse SelectSalesHistory(SalestargetHistoryRequest objRequest)
       {
           SalestargetHistoryResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {

               var objBaseSalesTargetDAL = objFactory.GetDALRepository().GetBaseSalesTargetDAL();
               objResponse = (SalestargetHistoryResponse)objBaseSalesTargetDAL.HistorySalesTarget(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SalestargetHistoryResponse();
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectByIDSalesTargetResponse SelectByIDSalesTargetHeader(SelectByIDSalesTargetRequest objRequest)
       {
           SelectByIDSalesTargetResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseSalesTargetDAL = objFactory.GetDALRepository().GetBaseSalesTargetDAL();

               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               }

               objResponse = (SelectByIDSalesTargetResponse)objBaseSalesTargetDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByIDSalesTargetResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Target Header");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectSalesTargetDetailsResponse SelectByIDAFSegamationDetils(SelectSalesTargetDetailsRequest objRequest)
       {
           SelectSalesTargetDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseSalesTargetDAL = objFactory.GetDALRepository().GetBaseSalesTargetDAL();
               objResponse = (SelectSalesTargetDetailsResponse)objBaseSalesTargetDAL.SelectSalesTargetDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectSalesTargetDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Target Details");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SaveSalesTargetResponse SaveDocumentNumberingMaster(SaveSalesTargetRequest objRequest)
       {
           SaveSalesTargetResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;

               BaseSalesTargetDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetBaseSalesTargetDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ObjSalesTargetHeader = new SalesTargetHeader();
                   ObjSalesTargetHeader = (SalesTargetHeader)objRequest.RequestDynamicData;
                   objRequest.DocumentTypeID = ObjSalesTargetHeader.DocumentTypeID;
                   objRequest.SalesTargetHeaderRecord = ObjSalesTargetHeader;
                   objRequest.SalestargetDetailsList = ObjSalesTargetHeader.SalestargetDetails;
               }
               objResponse = (SaveSalesTargetResponse)objBaseDocumentNumberingMasterDAL.InsertRecord(objRequest);
               if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               {
                   objRequest.RequestFrom = objRequest.RequestFrom;
                   objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                   objRequest.DocumentType = Enums.DocumentType.SALESTARGET;
                   objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                   //objRequest.BaseIntegrateStoreID = objRequest.SalesTargetHeaderRecord.StoreID;

                   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.SalesTargetBLL.SalesTargetBLL", "SaveDocumentNumberingMaster");
               }
           }
           catch (Exception ex)
           {
               objResponse = new SaveSalesTargetResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectAllSalesTargetResponse SelectAllDocumentNumberingMaster(SelectAllSalesTargetRequest objRequest)
       {
           SelectAllSalesTargetResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               BaseSalesTargetDAL objBaseDocumentNumberingMasterDAL = objFactory.GetDALRepository().GetBaseSalesTargetDAL();
               objResponse = (SelectAllSalesTargetResponse)objBaseDocumentNumberingMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {

               objResponse = new SelectAllSalesTargetResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Type");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
    }
}
