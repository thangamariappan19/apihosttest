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
    public class StockReceiptTransactionDAL : BaseStockReceiptTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override StockReceiptTransactionResponse SelectAll(StockReceiptTransactionRequest RequestObj)
        {
            var StockReceiptList = new List<StockReceiptHeaderTransaction>();
            var RequestData = (StockReceiptTransactionRequest)RequestObj;
            var ResponseData = new StockReceiptTransactionResponse();
            string StoreName = "";
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select SRH.*,cm.countryname,sm.storename from StockReceiptHeader SRH left join countrymaster cm on SRH.FromCountryId=cm.id left join storemaster sm on SRH.FromStoreId=sm.id Where SRH.Documentdate between '" + RequestData.FromDate.ToString("yyyy/MM/dd") + "' and '" + RequestData.ToDate.ToString("yyyy/MM/dd") + "' and SRH.FromStoreID ='" + RequestData.StoreID + "' order by SRH.DocumentNo,SRH.DocumentDate";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                       
                        var objStockReceipt = new StockReceiptHeaderTransaction();
                        objStockReceipt.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReceipt.FromWareHouseID = objReader["FromWareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["FromWareHouseID"]) : 0;
                        objStockReceipt.FromWarehouseCode = Convert.ToString(objReader["FromWarehouseCode"]);
                        objStockReceipt.Fromwarehousename = Convert.ToString(objReader["Fromwarehousename"]);
                        objStockReceipt.StockRequestDocumentNo = Convert.ToString(objReader["StockRequestDocumentNo"]);
                        objStockReceipt.StockRequestID = objReader["StockRequestID"] != DBNull.Value ? Convert.ToInt32(objReader["StockRequestID"]) : 0;
                        objStockReceipt.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReceipt.TotalReceivedQuantity = objReader["TotalReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalReceivedQuantity"]) : 0;
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? (Convert.ToDateTime(objReader["DocumentDate"])).ToString("dd/MM/yyyy") : "";
                        objStockReceipt.Status = Convert.ToString(objReader["Status"]);
                        objStockReceipt.Type = objReader["Type"] != DBNull.Value ? Convert.ToBoolean(objReader["Type"]) : false;
                        StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : ""; 
                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objStockReceipt.StoreName = Convert.ToString(objReader["StoreName"]);
                        }
                        StockReceiptList.Add(objStockReceipt);
                    }
                    ResponseData.FromDate = RequestData.FromDate;
                    ResponseData.ToDate = RequestData.ToDate;
                    ResponseData.storename = StoreName;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptHeaderTransactionList = StockReceiptList;
                    ResponseData.ResponseDynamicData = StockReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Receipt");
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


        public override StockReceiptTransactionResponse SelectDetailReceipt(StockReceiptTransactionRequest ObjRequest)
        {
            var StockReceiptDetailMasterList = new List<StockReceiptDetailTransaction>();
            var RequestData = (StockReceiptTransactionRequest)ObjRequest;
            var ResponseData = new StockReceiptTransactionResponse();
            string StoreName = "";
            String ReceiptNumber = "";
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select StockReceiptDetails.*,stockreceiptheader.StockRequestDocumentNo,storemaster.storename from StockReceiptDetails left join stockreceiptheader on StockReceiptDetails.headerid = stockreceiptheader.id left join storemaster on storemaster.id = stockreceiptheader.fromstoreid");
                sSql.Append(" where  HeaderID=" + RequestData.ID + " ");
                sSql.Append("order by id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceiptDetailMaster = new StockReceiptDetailTransaction();
                        objStockReceiptDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReceiptDetailMaster.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        Convert.ToInt32(objReader["HeaderID"]);
                        objStockReceiptDetailMaster.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        Convert.ToInt32(objReader["SKUID"]);
                        objStockReceiptDetailMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);

                        objStockReceiptDetailMaster.SKUName = Convert.ToString(objReader["SKUName"]);
                        objStockReceiptDetailMaster.Brand = Convert.ToString(objReader["Brand"]);
                        objStockReceiptDetailMaster.Color = Convert.ToString(objReader["Color"]);
                        objStockReceiptDetailMaster.Size = Convert.ToString(objReader["Size"]);
                        objStockReceiptDetailMaster.BarCode = Convert.ToString(objReader["BarCode"]);

                        objStockReceiptDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceiptDetailMaster.ReceivedQuantity = objReader["ReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQuantity"]) : 0;
                        objStockReceiptDetailMaster.TransferQuantity = objReader["TransferQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TransferQuantity"]) : 0;
                        objStockReceiptDetailMaster.RequestQuantity = objReader["RequestQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["RequestQuantity"]) : 0;
                        objStockReceiptDetailMaster.DifferenceQuantity = objReader["DifferenceQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DifferenceQuantity"]) : 0;
                        objStockReceiptDetailMaster.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objStockReceiptDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceiptDetailMaster.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";
                        ReceiptNumber = objReader["StockRequestDocumentNo"] != DBNull.Value ? Convert.ToString(objReader["StockRequestDocumentNo"]) : "";
                        objStockReceiptDetailMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        StockReceiptDetailMasterList.Add(objStockReceiptDetailMaster);
                    }
                    ResponseData.storename = StoreName;
                    ResponseData.ReceiptNumber = ReceiptNumber;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptDetailsTransactionList = StockReceiptDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Receipt");
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

        public override StockReceiptTransactionResponse SelectAllReport(StockReceiptTransactionRequest RequestObj)
        {

            var StockReceiptDetailMasterList = new List<StockReceiptDetailTransaction>();
            var RequestData = (StockReceiptTransactionRequest)RequestObj;
            var ResponseData = new StockReceiptTransactionResponse();
            DataTable objDataTable = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("StockReceiptHeaderTransaction", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter FromDate = _CommandObj.Parameters.Add("@FromDate", SqlDbType.VarChar);
                FromDate.Direction = ParameterDirection.Input;
                FromDate.Value = sqlCommon.GetSQLServerDateString(RequestData.FromDate);

                SqlParameter ToDate = _CommandObj.Parameters.Add("@ToDate", SqlDbType.VarChar);
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
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Receipt");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Receipt");
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
