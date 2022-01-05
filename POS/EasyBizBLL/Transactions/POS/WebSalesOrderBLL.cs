using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizFactory;
using EasyBizRequest.Transactions.POS.WebOrderSalesRequest;
using EasyBizResponse.Transactions.POS.WebOrderSalesResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Transactions.POS
{
    public class WebSalesOrderBLL
    {

        public WebSalesOrderResponse SaveOrderSalesRecord(WebSalesOrderRequest objRequest)
        {
            WebSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseNonTradingItemStockDAL = objFactory.GetDALRepository().GetWebSalesOrderDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var objNonTradingItemRequest = new WebSalesOrderHeader();
                    objNonTradingItemRequest = (WebSalesOrderHeader)objRequest.RequestDynamicData;
                    objRequest.WebOrderHeaderList = objNonTradingItemRequest;
                    //objRequest.WebSalesHeaderDetails = objNonTradingItemRequest;
                    //objRequest.TransactionLogList = objNonTradingItemRequest.TransactionLogList;
                }
                objResponse = (WebSalesOrderResponse)objBaseNonTradingItemStockDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    //objRequest.DocumentType = Enums.DocumentType.NONTRADINGITEMDISTRIBUTION;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.POS.WebSalesOrderBLL", "SaveOrderSalesRecord");
                }
            }
            catch (Exception ex)
            {
                objResponse = new WebSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "NonTradingItemStock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public WebSalesOrderResponse SelectAllOrderRecords(WebSalesOrderRequest objRequest)
        {
            var _WebOrderSales = new WebSalesOrderBLL();
            WebSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetWebSalesOrderDAL();
                objResponse = (WebSalesOrderResponse)objBaseBrandDAL.SelectAll(objRequest);                
            }

            catch (Exception ex)
            {
                objResponse = new WebSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Order Sales");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public WebSalesOrderResponse SelectOrderRecordsByID(WebSalesOrderRequest objRequest)
        {
            var _WebOrderSales = new WebSalesOrderBLL();
            WebSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseBrandDAL = objFactory.GetDALRepository().GetWebSalesOrderDAL();
                objResponse = (WebSalesOrderResponse)objBaseBrandDAL.SelectByIDs(objRequest);
                if (objResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
                {
                    var OrderSalesList = new List<WebSalesOrderDetails>();
                    foreach (WebSalesOrderHeader objBrand in objResponse.WebSalesOrderHeader)
                    {
                        var objSelectSubBrandListForCategoryRequest = new WebSalesOrderRequest();
                        objSelectSubBrandListForCategoryRequest.DocumentIDs = Convert.ToString(objBrand.ID);
                        //objSelectSubBrandListForCategoryRequest.ShowInActiveRecords = true;
                        var objSelectSubBrandListForCategoryResponse = new WebSalesOrderResponse();
                        objSelectSubBrandListForCategoryResponse = _WebOrderSales.SalesOrderDetails(objSelectSubBrandListForCategoryRequest);
                        if (objSelectSubBrandListForCategoryResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objBrand.StoreOrderDetails = objSelectSubBrandListForCategoryResponse.WebSalesOrderDetails;
                        }
                        else
                        {
                            objBrand.StoreOrderDetails = new List<WebSalesOrderDetails>();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                objResponse = new WebSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Order Sales");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public WebSalesOrderResponse SalesOrderDetails(WebSalesOrderRequest objRequest)
        {
            WebSalesOrderResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseSubBrandDAL = objFactory.GetDALRepository().GetWebSalesOrderDAL();
                if (objRequest.DocumentIDs != "")
                {
                    if (!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        int Doc_Id;
                        int.TryParse(objRequest.DocumentIDs, out Doc_Id);
                        objRequest.DocumentNumber = Convert.ToString(Doc_Id);
                    }
                }
                objResponse = (WebSalesOrderResponse)objBaseSubBrandDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new WebSalesOrderResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Sub Brand");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
    }
}
