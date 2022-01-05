using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.API_SalesOrderRequest;
using EasyBizResponse.Transactions.POS.API_SalesOrderResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class API_SalesOrderBLL
    {
        public API_SaveSalesOrderResponse SaveSalesOrder(API_SaveSalesOrderRequest objRequest)
        {
            API_SaveSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetAPISalesOrderDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objSalesOrder = new API_SalesOrderHeader();
                    objSalesOrder = (API_SalesOrderHeader)objRequest.RequestDynamicData;
                    objRequest.SalesOrderHeaderRecord = objSalesOrder;
                    objRequest.SalesOrderDetailsList = objSalesOrder.SalesOrderDetailsList;
                }
                objResponse = (API_SaveSalesOrderResponse)objBaseSalesOrderDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.SALESORDER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.SalesOrderBLL", "SaveSalesOrder");
                }
            }
            catch (Exception ex)
            {
                objResponse = new API_SaveSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public API_SelectALLSalesOrderResponse SelectAllSalesRecord(API_SelectAllSalesOrderRequest objRequest)
        {
            API_SelectALLSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetAPISalesOrderDAL();
                objResponse = (API_SelectALLSalesOrderResponse)objBaseSalesOrderDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new API_SelectALLSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public API_SelectBySalesOrderIDResponse SelectSalesOrderRecord(API_SelectBySalesOrderIDRequest objRequest)
        {
            API_SelectBySalesOrderIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSalesOrderDAL = objFactory.GetDALRepository().GetAPISalesOrderDAL();
                objResponse = (API_SelectBySalesOrderIDResponse)objBaseSalesOrderDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new API_SelectBySalesOrderIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SalesOrder");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
