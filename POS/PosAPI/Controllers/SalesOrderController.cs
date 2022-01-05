using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.POS;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Transactions.POS.API_SalesOrderRequest;
using EasyBizResponse.Transactions.POS.API_SalesOrderResponse;
using EasyBizDBTypes.Transactions.POS.API_SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizBLL.Transactions.TransactionLogs;

namespace PosAPI.Controllers
{
    public class SalesOrderController : ApiController
    {

        public IHttpActionResult GetAllSalesOrder(int StoreID)
        {
            try
            {
                var RequestData = new API_SelectAllSalesOrderRequest();
                RequestData.StoreID = StoreID;
                API_SelectALLSalesOrderResponse response = null;
                var ResponseData = new API_SalesOrderBLL();
                response = ResponseData.SelectAllSalesRecord(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult GetSalesOrderByID(int StoreID, int HeaderID)
        {
            try
            {
                var RequestData = new API_SelectBySalesOrderIDRequest();
                RequestData.StoreID = StoreID;
                RequestData.ID = HeaderID;
                API_SelectBySalesOrderIDResponse response = null;
                var ResponseData = new API_SalesOrderBLL();
                response = ResponseData.SelectSalesOrderRecord(RequestData);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult PostSalesOrder(API_SalesOrderHeader ObjSalesOrder)
        {
            try
            {
                var RequestData = new API_SaveSalesOrderRequest();
                RequestData.SalesOrderHeaderRecord = new API_SalesOrderHeader();
                RequestData.SalesOrderHeaderRecord.ID = ObjSalesOrder.ID;
                RequestData.SalesOrderHeaderRecord.DocumentNo = ObjSalesOrder.DocumentNo;
                RequestData.SalesOrderHeaderRecord.DocumentDate = ObjSalesOrder.DocumentDate;
                RequestData.SalesOrderHeaderRecord.DeliveryDate = ObjSalesOrder.DeliveryDate;
                RequestData.SalesOrderHeaderRecord.TotalQty = ObjSalesOrder.TotalQty;
                RequestData.SalesOrderHeaderRecord.PickedQty = ObjSalesOrder.PickedQty;
                RequestData.SalesOrderHeaderRecord.TotalAmount = ObjSalesOrder.TotalAmount;
                RequestData.SalesOrderHeaderRecord.DiscountType = ObjSalesOrder.DiscountType;
                RequestData.SalesOrderHeaderRecord.DiscountValue = ObjSalesOrder.DiscountValue;
                RequestData.SalesOrderHeaderRecord.NetAmount = ObjSalesOrder.NetAmount;
                RequestData.SalesOrderHeaderRecord.OrderStatus = ObjSalesOrder.OrderStatus;
                RequestData.SalesOrderHeaderRecord.PaymentStatus = ObjSalesOrder.PaymentStatus;
                RequestData.SalesOrderHeaderRecord.CustomerCode = ObjSalesOrder.CustomerCode;
                RequestData.SalesOrderHeaderRecord.SalesOrderDetailsList = ObjSalesOrder.SalesOrderDetailsList;
                RequestData.SalesOrderHeaderRecord.PaymentList = ObjSalesOrder.PaymentList;
                API_SaveSalesOrderResponse response = null;
                var ResponseData = new API_SalesOrderBLL();
                response = ResponseData.SaveSalesOrder(RequestData);
                return Ok(response);
                }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult GetProductSearch(string SearchValue, int StoreID, int Mode)
        {
            try
            {
                GetProductDescSearchRequest RequestData = new GetProductDescSearchRequest();
                RequestData.SearchString = SearchValue;
                RequestData.Storeid = StoreID;

                GetProductDescSearchResponse response = null;
                var ResponseData = new TransactionLogBLL();
                response = ResponseData.GetProductDescSearch(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        /*public IHttpActionResult PutSalesOrder(API_SalesOrderHeader ObjSalesOrder)
        {
            try
            {
                var RequestData = new API_SaveSalesOrderRequest();
                RequestData.SalesOrderHeaderRecord.ID = ObjSalesOrder.ID;
                RequestData.SalesOrderHeaderRecord.DocumentNo = ObjSalesOrder.DocumentNo;
                RequestData.SalesOrderHeaderRecord.DocumentDate = ObjSalesOrder.DocumentDate;
                RequestData.SalesOrderHeaderRecord.DeliveryDate = ObjSalesOrder.DeliveryDate;
                RequestData.SalesOrderHeaderRecord.TotalQty = ObjSalesOrder.TotalQty;
                RequestData.SalesOrderHeaderRecord.PickedQty = ObjSalesOrder.PickedQty;
                RequestData.SalesOrderHeaderRecord.TotalAmount = ObjSalesOrder.TotalAmount;
                RequestData.SalesOrderHeaderRecord.DiscountType = ObjSalesOrder.DiscountType;
                RequestData.SalesOrderHeaderRecord.DiscountValue = ObjSalesOrder.DiscountValue;
                RequestData.SalesOrderHeaderRecord.NetAmount = ObjSalesOrder.NetAmount;
                RequestData.SalesOrderHeaderRecord.OrderStatus = ObjSalesOrder.OrderStatus;
                RequestData.SalesOrderHeaderRecord.PaymentStatus = ObjSalesOrder.PaymentStatus;
                RequestData.SalesOrderHeaderRecord.CustomerCode = ObjSalesOrder.CustomerCode;
                RequestData.SalesOrderHeaderRecord.SalesOrderDetailsList = ObjSalesOrder.SalesOrderDetailsList;
                RequestData.SalesOrderHeaderRecord.PaymentList = ObjSalesOrder.PaymentList;
                API_SaveSalesOrderResponse response = null;
                var ResponseData = new API_SalesOrderBLL();
                response = ResponseData.SaveSalesOrder(RequestData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }*/
    }
}
