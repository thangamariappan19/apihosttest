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
    public class StockAdjustmentTransactionDAL : BaseStockAdjustmentTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override StockAdjustmentTransactionResponse SelectAll(StockAdjustmentTransactionRequest RequestObj)
        {
            var StockReceiptList = new List<StockAdjustmentHeaderTransaction>();
            var RequestData = (StockAdjustmentTransactionRequest)RequestObj;
            var ResponseData = new StockAdjustmentTransactionResponse();
            string StoreName = "";
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = @"Select a.*,empc.EmployeeName,SM.StoreName from StockAdjustmentHeader as a left join EmployeeMaster empc on a.CreateBy = empc.ID left join StoreMaster SM on a.StoreID = SM.ID  Where cast(Documentdate as date) between '" + RequestData.FromDate.ToString("yyyy/MM/dd") + "' and '" + RequestData.ToDate.ToString("yyyy/MM/dd") + "'  and a.storeid ='" + RequestData.StoreID + "' order by a.documentnumber,a.documentdate";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objStockAdjustment = new StockAdjustmentHeaderTransaction();
                        objStockAdjustment.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;                   
                        objStockAdjustment.DocumentNo = Convert.ToString(objReader["DocumentNumber"]);
                        objStockAdjustment.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockAdjustment.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? (Convert.ToDateTime(objReader["DocumentDate"])).ToString("dd/MM/yyyy") : "";
                        objStockAdjustment.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : "";
                        StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : "";
                        objStockAdjustment.CreatedBy = objReader["EmployeeName"] != DBNull.Value ? Convert.ToString(objReader["EmployeeName"]) : "";
                       
                        StockReceiptList.Add(objStockAdjustment);
                    }
                    ResponseData.FromDate = RequestData.FromDate;
                    ResponseData.ToDate = RequestData.ToDate;
                    ResponseData.storename = StoreName;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockAdjustmentHeaderTransactionList = StockReceiptList;
                    ResponseData.ResponseDynamicData = StockReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Adjustment");
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



        public override StockAdjustmentTransactionResponse SelectDetailAdjustment(StockAdjustmentTransactionRequest ObjRequest)
        {
            var StockReturnDetailMasterList = new List<StockAdjustmentDetailTransaction>();
            var RequestData = (StockAdjustmentTransactionRequest)ObjRequest;
            var ResponseData = new StockAdjustmentTransactionResponse();
            string StoreName = "";
            String AdjustmentNumber = "";
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
             
                sSql.Append("Select a.*,b.*,empc.EmployeeName,SM.StoreName from StockAdjustmentHeader as a left join EmployeeMaster empc on a.CreateBy = empc.ID left join StoreMaster SM on a.StoreID = SM.ID inner join stockadjustmentdetails as b on a.id = b.SAHID ");
                sSql.Append("where  a.id=" + RequestData.ID + " ");
                sSql.Append("order by a.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockAdjustmentDetailMaster = new StockAdjustmentDetailTransaction();
                        objStockAdjustmentDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockAdjustmentDetailMaster.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
                        objStockAdjustmentDetailMaster.SystemQuantity = objReader["SystemQuantity"] != DBNull.Value ? Convert.ToString(objReader["SystemQuantity"]) : "";
                        objStockAdjustmentDetailMaster.PhysicalQuantity = objReader["PhysicalQuantity"] != DBNull.Value ? Convert.ToString(objReader["PhysicalQuantity"]) : "";
                        objStockAdjustmentDetailMaster.DifferenceQty = objReader["DifferenceQty"] != DBNull.Value ? Convert.ToString(objReader["DifferenceQty"]) : ""; 
                        StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : "";
                        AdjustmentNumber = objReader["DocumentNumber"] != DBNull.Value ? Convert.ToString(objReader["DocumentNumber"]) : "";
                        StockReturnDetailMasterList.Add(objStockAdjustmentDetailMaster);
                    }
                    ResponseData.storename = StoreName;
                    ResponseData.AdjustmentNumber = AdjustmentNumber;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockAdjustmentDetailsTransactionList = StockReturnDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Adjustment");
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

        public override StockAdjustmentTransactionResponse SelectAllReport(StockAdjustmentTransactionRequest RequestObj)
        {

            var StockReturnList = new List<StockAdjustmentHeaderTransaction>();
            var RequestData = (StockAdjustmentTransactionRequest)RequestObj;
            var ResponseData = new StockAdjustmentTransactionResponse();
            DataTable objDataTable = new DataTable();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("StockAdjustmentTransaction", _ConnectionObj);
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
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Adjustment");
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Stock Adjustment");
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
