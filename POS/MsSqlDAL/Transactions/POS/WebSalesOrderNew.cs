using EasyBizAbsDAL.Masters;
using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Transactions.POS.WebOrderSalesRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Transactions.POS.WebOrderSalesResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.POS
{
    public class WebSalesOrderNew : BaseWebSalesOrderDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlTransaction transaction = null;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        Enums.Enum_Order_Status _Order_Status;
        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            string status = "";
            string documentNo = "";
            var RequestData = (WebSalesOrderRequest)RequestObj;
            var ResponseData = new WebSalesOrderResponse();
            var sqlCommon = new MsSqlCommon();
            List<WebSalesOrderDetails> _SalesOrderDetails = new List<WebSalesOrderDetails>();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                foreach (WebSalesOrderHeader _orderSalesHeader in RequestData.WebSalesHeaderDetails)
                {
                    status = _orderSalesHeader.StatusCode;
                    documentNo = _orderSalesHeader.DocumentNo;
                    _SalesOrderDetails = _orderSalesHeader.StoreOrderDetails;
                }
                //sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateOrderSalesStatus", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.DocumentNos;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = documentNo;

                SqlParameter OrderStatus = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                OrderStatus.Direction = ParameterDirection.Input;
                OrderStatus.Value = status;

                if (status == "CLOSED")
                {
                    int Status = (int)Enums.Enum_Order_Status.CLOSED;
                    SqlParameter OrderStatusID = _CommandObj.Parameters.Add("@StatusID", SqlDbType.Int);
                    OrderStatusID.Direction = ParameterDirection.Input;
                    OrderStatusID.Value = Status;
                }
                else if(status== "NOSTOCK")
                {
                    int Status = (int)Enums.Enum_Order_Status.NOSTOCK;
                    SqlParameter OrderStatusID = _CommandObj.Parameters.Add("@StatusID", SqlDbType.Int);
                    OrderStatusID.Direction = ParameterDirection.Input;
                    OrderStatusID.Value = Status;
                }

                SqlParameter SalesOrderDetails = _CommandObj.Parameters.Add("@SalesOrderDetails", SqlDbType.Xml);
                SalesOrderDetails.Direction = ParameterDirection.Input;
                SalesOrderDetails.Value = SalesOrderDetailMasterXML(_SalesOrderDetails);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Order Fulfillment");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Order Fulfillment");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Order Fulfillment");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        private string SalesOrderDetailMasterXML(List<WebSalesOrderDetails> salesOrderDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (WebSalesOrderDetails objSalesOrderDetailsDetails in salesOrderDetailsList)
            {
                sSql.Append("<SalesOrderDetailsData>");
                sSql.Append("<ID>" + objSalesOrderDetailsDetails.ID + "</ID>");
                sSql.Append("<SalesOrderID>" + objSalesOrderDetailsDetails.HeaderID + "</SalesOrderID>");
                sSql.Append("<BarCode>" + objSalesOrderDetailsDetails.BarCode + "</BarCode>");
                sSql.Append("<SKUCode>" + (objSalesOrderDetailsDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<Qty>" + (objSalesOrderDetailsDetails.IssuedQty) + "</Qty>");
                sSql.Append("<Price>" + objSalesOrderDetailsDetails.Price + "</Price>");
                sSql.Append("</SalesOrderDetailsData>");
            }
            return sSql.ToString();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var DocumentList = new List<WebSalesOrderHeader>();
            var RequestData = (WebSalesOrderRequest)RequestObj;
            var ResponseData = new WebSalesOrderResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from BrandMaster with(NoLock) where Active='{0}'";
                string sSql = "Select * from StoreOrderHeader where (StatusCode='OPEN' or StatusCode='READ') and (StatusID=1 or StatusID='5') order by DocumentNo asc ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandMaster = new WebSalesOrderHeader();
                        objBrandMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandMaster.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objBrandMaster.DocumentDate = Convert.ToDateTime(objReader["DocumentDate"]);
                        objBrandMaster.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                        //objBrandMaster.CustomerPhoneNo = Convert.ToString(objReader["CustomerPhoneNo"]);
                        //objBrandMaster.TotalQty = Convert.ToInt32(objReader["TotalQty"]);
                        //objBrandMaster.BrandType = Convert.ToString(objReader["BrandType"]);
                        //objBrandMaster.Remarks = Convert.ToString(objReader["Remarks"]); */
                         objBrandMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                         objBrandMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                         objBrandMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                         objBrandMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; 
                         //objBrandMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                         //objBrandMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                         DocumentList.Add(objBrandMaster);
                     }
                     ResponseData.StatusCode = Enums.OpStatusCode.Success;
                     ResponseData.DocumentNoLookup = DocumentList;
                     ResponseData.ResponseDynamicData = DocumentList;
                 }
                 else
                 {
                     ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                     ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Settings");
                 }
             }
             catch (Exception ex)
             {
                 ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                 ResponseData.DisplayMessage = ex.Message;
             }
             finally
             {
                 sqlCommon.CloseConnection(_ConnectionObj);
             }
             return ResponseData;
         }

         public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
         {
             var DocumentList = new List<WebSalesOrderHeader>();
             var RequestData = (WebSalesOrderRequest)RequestObj;
             var ResponseData = new WebSalesOrderResponse();
             SqlDataReader objReader;
             var sqlCommon = new MsSqlCommon();
             try
             {
                 _ConnectionString = RequestData.ConnectionString;
                 _RequestFrom = RequestData.RequestFrom;

                 sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                 //string sSql = "Select * from BrandMaster with(NoLock) where Active='{0}'";
                 string sSql = "Select * from StoreOrderHeader where (StatusCode='OPEN' or StatusCode='READ') and (StatusID=1 or StatusID='5') AND ID=" + RequestObj.DocumentIDs ;
                 _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                 _CommandObj.CommandType = CommandType.Text;
                 objReader = _CommandObj.ExecuteReader();
                 if (objReader.HasRows)
                 {
                     while (objReader.Read())
                     {
                         var objBrandMaster = new WebSalesOrderHeader();
                         objBrandMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                         objBrandMaster.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                         objBrandMaster.DocumentDate = Convert.ToDateTime(objReader["DocumentDate"]);
                         objBrandMaster.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                         objBrandMaster.PhoneNo = Convert.ToString(objReader["CustomerPhoneNo"]);
                         objBrandMaster.TotalOrderQty = Convert.ToInt32(objReader["TotalQty"]);
                         objBrandMaster.StatusID = Convert.ToInt32(objReader["StatusID"]);
                         objBrandMaster.StatusCode = Convert.ToString(objReader["StatusCode"]);
                         /*objBrandMaster.BrandType = Convert.ToString(objReader["BrandType"]);
                         objBrandMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                         /*objBrandMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                         objBrandMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                         objBrandMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                         objBrandMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                         //objBrandMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                         objBrandMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;*/
                         DocumentList.Add(objBrandMaster);


                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WebSalesOrderHeader = DocumentList;
                    ResponseData.ResponseDynamicData = DocumentList;
                    UpdateRecord(RequestObj);
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Settings");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var BrandList = new List<WebSalesOrderDetails>();
            var RequestData = (WebSalesOrderRequest)RequestObj;
            var ResponseData = new WebSalesOrderResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from BrandMaster with(NoLock) where Active='{0}'";
                string sSql = "Select * from StoreOrderDetails where HeaderID ='" + RequestObj.DocumentNumber + "' order by ID  asc ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandMaster = new WebSalesOrderDetails();
                        objBrandMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objBrandMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objBrandMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objBrandMaster.OrderQty = Convert.ToInt32(objReader["OrderQty"]);
                        objBrandMaster.Price = Convert.ToDecimal(objReader["Price"]);
                        /*objBrandMaster.CustomerPhoneNo = Convert.ToString(objReader["CustomerPhoneNo"]);
                        objBrandMaster.TotalQty = Convert.ToInt32(objReader["TotalQty"]);*/
                        /*objBrandMaster.BrandType = Convert.ToString(objReader["BrandType"]);
                        objBrandMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objBrandMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objBrandMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objBrandMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objBrandMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;*/
                        //objBrandMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objBrandMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BrandList.Add(objBrandMaster);


                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WebSalesOrderDetails = BrandList;
                    ResponseData.ResponseDynamicData = BrandList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Settings");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var DocumentList = new List<WebSalesOrderHeader>();
            var RequestData = (WebSalesOrderRequest)RequestObj;
            var ResponseData = new WebSalesOrderResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                int Status = (int)Enums.Enum_Order_Status.READ;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from BrandMaster with(NoLock) where Active='{0}'";
                string sSql = "Update StoreOrderHeader set StatusCode='READ', StatusID="+Status+ " where ID=" + RequestObj.DocumentIDs;
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.ExecuteReader();
                _ConnectionObj.Close();
                _ConnectionObj.Open();
                SqlCommand sqlCommand = new SqlCommand("Update StoreOrderDetails set StatusCode='READ', StatusID=" + Status + " where HeaderID=" + RequestObj.DocumentIDs, _ConnectionObj);
                sqlCommand.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
    }
}
