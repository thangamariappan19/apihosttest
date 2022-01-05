using EasyBizAbsDAL.Reports.DayWiseTransaction;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Reports.DayWiseTransaction;
using EasyBizRequest.Reports.DayWiseTransactionRequest;
using EasyBizResponse.Reports.DayWiseTransactionResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MsSqlDAL.Reports.DayWiseTransaction
{
    public class StockReturnTransactionDAL : BaseStockReturnTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override StockReturnTransactionResponse SelectAll(StockReturnTransactionRequest RequestObj)
        {
            var StockReceiptList = new List<StockReturnHeaderTransaction>();
            var RequestData = (StockReturnTransactionRequest)RequestObj;
            var ResponseData = new StockReturnTransactionResponse();
            string StoreName = "";
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = @"Select StockReturnHeader.*,warehousemaster.warehousename,storemaster.storename from StockReturnHeader left join storemaster on storemaster.id = stockreturnheader.Fromstoreid left join warehousemaster on StockReturnHeader.towarehouseid = warehousemaster.id  Where Documentdate between '" + RequestData.FromDate.ToString("yyyy/MM/dd") + "' and '" + RequestData.ToDate.ToString("yyyy/MM/dd") + "'  and Fromstoreid ='" + RequestData.StoreID + "' order by StockReturnHeader.DocumentNo,StockReturnHeader.DocumentDate";
                           
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objStockReturn = new StockReturnHeaderTransaction();
                        objStockReturn.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReturn.ToWareHouse = objReader["warehousename"] != DBNull.Value ? Convert.ToString(objReader["warehousename"]) : "";                       
                        objStockReturn.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReturn.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReturn.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockReturn.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? (Convert.ToDateTime(objReader["DocumentDate"])).ToString("dd/MM/yyyy") : "";
                        objStockReturn.Status = Convert.ToString(objReader["Status"]);
                        StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : "";
                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objStockReturn.StoreName = Convert.ToString(objReader["StoreName"]);
                        }
                        StockReceiptList.Add(objStockReturn);
                    }
                    ResponseData.FromDate = RequestData.FromDate;
                    ResponseData.ToDate = RequestData.ToDate;
                    ResponseData.storename = StoreName;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReturnHeaderTransactionList = StockReceiptList;
                    ResponseData.ResponseDynamicData = StockReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Return");
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

      

        public override StockReturnTransactionResponse SelectDetailReturn(StockReturnTransactionRequest ObjRequest)
        {
            var StockReturnDetailMasterList = new List<StockReturnDetailTransaction>();
            var RequestData = (StockReturnTransactionRequest)ObjRequest;
            var ResponseData = new StockReturnTransactionResponse();
            string StoreName = "";
            String ReturnNumber = "";
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select *,sk.StyleCode,storemaster.storename,stockreturnheader.DocumentNo from StockReturnDetails SRD  left join stockreturnheader on stockreturnheader.id = SRD.HeaderID   left join storemaster on storemaster.id = stockreturnheader.Fromstoreid  inner join SKUMaster SK on SRD.SKUCode=sk.SKUCode ");
                sSql.Append("where  SRD.HeaderID=" + RequestData.ID + " ");
                sSql.Append("order by SRD.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReturnDetailMaster = new StockReturnDetailTransaction();
                        objStockReturnDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReturnDetailMaster.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objStockReturnDetailMaster.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        objStockReturnDetailMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objStockReturnDetailMaster.SKUName = Convert.ToString(objReader["SKUName"]);
                        objStockReturnDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStockReturnDetailMaster.Brand = Convert.ToString(objReader["Brand"]);
                        objStockReturnDetailMaster.Color = Convert.ToString(objReader["Color"]);
                        objStockReturnDetailMaster.Size = Convert.ToString(objReader["Size"]);
                        objStockReturnDetailMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objStockReturnDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReturnDetailMaster.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                        objStockReturnDetailMaster.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objStockReturnDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReturnDetailMaster.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        objStockReturnDetailMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";
                        ReturnNumber = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : "";
                        StockReturnDetailMasterList.Add(objStockReturnDetailMaster);
                    }
                    ResponseData.storename = StoreName;
                    ResponseData.ReturnNumber = ReturnNumber;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReturnDetailsTransactionList = StockReturnDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Return");
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

        public override StockReturnTransactionResponse SelectAllReport(StockReturnTransactionRequest RequestObj)
        {

            var StockReturnList = new List<StockReturnHeaderTransaction>();
            var RequestData = (StockReturnTransactionRequest)RequestObj;
            var ResponseData = new StockReturnTransactionResponse();
            DataTable objDataTable = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("StockReturnHeaderTransaction", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter FromDate = _CommandObj.Parameters.Add("@FromDate", SqlDbType.DateTime);
                FromDate.Direction = ParameterDirection.Input;
                FromDate.Value = sqlCommon.GetSQLServerDateString(RequestData.FromDate);

                SqlParameter ToDate = _CommandObj.Parameters.Add("@ToDate", SqlDbType.DateTime);
                ToDate.Direction = ParameterDirection.Input;
                ToDate.Value = sqlCommon.GetSQLServerDateString(RequestData.ToDate);

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StoreID;


                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlDataReader objReader;

                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    objDataTable.Load(objReader);
                }

                if (objDataTable.Rows.Count > 0)
                {
                    ResponseData.ReportDataTable = objDataTable;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.ReportDataTable = new DataTable();
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Return");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Return");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }
        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            return null;
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
