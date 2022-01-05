using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesOrder;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.SalesOrder;
using EasyBizResponse.Transactions.POS.SalesOrder;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
   public class SalesOrderBLL
    {

        public SaveSalesOrderReponse SaveSalesOrder(SaveSalesOrderRequest objRequest)
        {
            SaveSalesOrderReponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetSalesOrderDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objSalesOrder = new SalesOrderHeader();
                    objSalesOrder = (SalesOrderHeader)objRequest.RequestDynamicData;
                    objRequest.SalesOrderHeaderRecord = objSalesOrder;
                    objRequest.SalesOrderDetailsList = objSalesOrder.SalesOrderDetailsList;
                }
                objResponse = (SaveSalesOrderReponse)objBaseSalesOrderDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SALESORDER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.SalesOrderBLL", "SaveSalesOrder");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveSalesOrderReponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public DeleteSalesOrderResponse DeleteSalesOrder(DeleteSalesOrderRequest objRequest)
        {
            DeleteSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetSalesOrderDAL();
                objResponse = (DeleteSalesOrderResponse)objBaseSalesOrderDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SALESORDER;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.SalesOrderBLL", "DeleteSalesOrder");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllSalesOrderResponse SelectAllSalesRecord(SelectAllSalesOrderRequest objRequest)
        {
            SelectAllSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetSalesOrderDAL();
                objResponse = (SelectAllSalesOrderResponse)objBaseSalesOrderDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectBySalesOrderIDResponse SelectSalesOrderRecord(SelectBySalesOrderIDRequest objRequest)
        {
            SelectBySalesOrderIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetSalesOrderDAL();
                objResponse = (SelectBySalesOrderIDResponse)objBaseSalesOrderDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectBySalesOrderIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
