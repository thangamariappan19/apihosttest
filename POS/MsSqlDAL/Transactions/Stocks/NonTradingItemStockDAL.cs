using EasyBizAbsDAL.Transactions.NonTradingItemStock;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.NonTradingItemStock;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Stocks
{
    public class NonTradingItemStockDAL : BaseNonTradingItemStockDAL
    {
        SqlConnection _ConnectionObj;
        SqlConnection cnn;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        string _CentralUnitConnectionString;
        Enums.RequestFrom _RequestFrom1;

        
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
            var RequestData = (SaveNonTradingItemRequest)RequestObj;
            var ResponseData = new SaveNonTradingItemResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertNonTradingStock", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.NonTradingItemRecord.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateTimeString(RequestData.NonTradingItemRecord.DocumentDate);

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.NonTradingItemRecord.CountryID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.NonTradingItemRecord.StoreID;

                SqlParameter ReceivedType = _CommandObj.Parameters.Add("@ReceivedType", SqlDbType.VarChar);
                ReceivedType.Direction = ParameterDirection.Input;
                ReceivedType.Value = RequestData.NonTradingItemRecord.ReceivedType;

                SqlParameter TransactionType = _CommandObj.Parameters.Add("@TransactionType", SqlDbType.VarChar);
                TransactionType.Direction = ParameterDirection.Input;
                TransactionType.Value = RequestData.NonTradingItemRecord.TransactionType;

                SqlParameter ReceivedQty = _CommandObj.Parameters.Add("@TotalQty", SqlDbType.Int);
                ReceivedQty.Direction = ParameterDirection.Input;
                ReceivedQty.Value = RequestData.NonTradingItemRecord.ReceivedQty;

                SqlParameter CreatedOn = _CommandObj.Parameters.Add("@CreatedOn", SqlDbType.DateTime);
                CreatedOn.Direction = ParameterDirection.Input;
                CreatedOn.Value = DateTime.Now;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@CreatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.NonTradingItemRecord.CreateBy;   

                SqlParameter EmployeeID = _CommandObj.Parameters.Add("@EmployeeID", SqlDbType.Int);
                EmployeeID.Direction = ParameterDirection.Input;
                EmployeeID.Value = RequestData.NonTradingItemRecord.EmployeeID;

                SqlParameter EmployeeName = _CommandObj.Parameters.Add("@EmployeeName", SqlDbType.NVarChar);
                EmployeeName.Direction = ParameterDirection.Input;
                EmployeeName.Value = RequestData.NonTradingItemRecord.EmployeeName;

                SqlParameter EmpCode = _CommandObj.Parameters.Add("@EmployeeCode", SqlDbType.NVarChar);
                EmpCode.Direction = ParameterDirection.Input;
                EmpCode.Value = RequestData.NonTradingItemRecord.EmployeeCode;


                SqlParameter RefDocumentNo = _CommandObj.Parameters.Add("@RefDocumentNo", SqlDbType.VarChar);
                RefDocumentNo.Direction = ParameterDirection.Input;
                RefDocumentNo.Value = RequestData.NonTradingItemRecord.RefDocumentNo;

                //SqlParameter ReturnQty = _CommandObj.Parameters.Add("@ReturnQty", SqlDbType.Int);
                //ReturnQty.Direction = ParameterDirection.Input;
                //ReturnQty.Value = RequestData.NonTradingItemRecord.ReturnQty;

                //SqlParameter SKUCode = _CommandObj.Parameters.Add("@SKUCode", SqlDbType.NVarChar);
                //SKUCode.Direction = ParameterDirection.Input;
                //SKUCode.Value = RequestData.NonTradingItemRecord.SKUCode;

                //SqlParameter BarCode = _CommandObj.Parameters.Add("@BarCode", SqlDbType.NVarChar);
                //BarCode.Direction = ParameterDirection.Input;
                //BarCode.Value = RequestData.NonTradingItemRecord.BarCode;

                _CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);

                SqlParameter NonTradingStockList = _CommandObj.Parameters.Add("@NonTradingDetails",SqlDbType.Xml);
                NonTradingStockList.Direction = ParameterDirection.Input;
                NonTradingStockList.Value = NonTradingStockDetailsXML(RequestData.NonTradingStockDetailsList);

                SqlParameter NonTradingTransactionLogList = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                NonTradingTransactionLogList.Direction = ParameterDirection.Input;
                NonTradingTransactionLogList.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "NonTradingItemStock");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "NonTradingItemStock");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "NonTradingItemStock");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "NonTradingItemStock");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public string NonTradingStockDetailsXML(List<NonTradingStockDetailsTypes> nonTradingItemStocks)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlcommon = new MsSqlCommon();
            foreach(NonTradingStockDetailsTypes objNonTradingStockDetails in nonTradingItemStocks)
            {
                sSql.Append("<NonTradingDetails>");
                sSql.Append("<SerialNo>" + objNonTradingStockDetails.SerialNo +"</SerialNo>");
                sSql.Append("<SKUID>" + objNonTradingStockDetails.SKUID + "</SKUID>");
                sSql.Append("<SKUCode>" + objNonTradingStockDetails.SKUCode + "</SKUCode>");
                sSql.Append("<BarCode>" + objNonTradingStockDetails.BarCode + "</BarCode>");
                sSql.Append("<ReceivedQty>" + objNonTradingStockDetails.ReceivedQty + "</ReceivedQty>");
                sSql.Append("<ReturnQty>" + objNonTradingStockDetails.ReturnQty + "</ReturnQty>");
                sSql.Append("<CreatedBy>" + objNonTradingStockDetails.CreateBy + "</CreatedBy>");
                sSql.Append("<StoreID>" + objNonTradingStockDetails.StoreID + "</StoreID>");
                sSql.Append("</NonTradingDetails>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public string TransactionLogDetailMasterXML(List<TransactionLog> TransactionLogList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (TransactionLog objTransactionLogDetailMasterDetails in TransactionLogList)
            {
                sSql.Append("<TransactionLogDetailsData>");
                //sSql.Append("<ID>" + objTransactionLogDetailMasterDetails.ID + "</ID>");
                sSql.Append("<TransactionType> Non-Trading Items</TransactionType>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.BusinessDate) + "</BusinessDate>");
                sSql.Append("<ActualDateTime>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ActualDateTime) + "</ActualDateTime>");
                sSql.Append("<StyleCode>" + objTransactionLogDetailMasterDetails.StyleCode + "</StyleCode>");
                sSql.Append("<SKUCode>" + objTransactionLogDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<InQty>" + objTransactionLogDetailMasterDetails.InQty + "</InQty>");
                sSql.Append("<OutQty>" + objTransactionLogDetailMasterDetails.OutQty + "</OutQty>");
                sSql.Append("<TransactionPrice>" + objTransactionLogDetailMasterDetails.TransactionPrice + "</TransactionPrice>");
                sSql.Append("<Currency>" + (objTransactionLogDetailMasterDetails.Currency) + "</Currency>");
                sSql.Append("<ExchangeRate>" + (objTransactionLogDetailMasterDetails.ExchangeRate) + "</ExchangeRate>");
                sSql.Append("<DocumentPrice>" + (objTransactionLogDetailMasterDetails.DocumentPrice) + "</DocumentPrice>");
                sSql.Append("<UserID>" + (objTransactionLogDetailMasterDetails.UserID) + "</UserID>");
                sSql.Append("<CountryID>" + (objTransactionLogDetailMasterDetails.CountryID) + "</CountryID>");
                sSql.Append("<CountryCode>" + (objTransactionLogDetailMasterDetails.CountryCode) + "</CountryCode>");
                sSql.Append("<StoreID>" + (objTransactionLogDetailMasterDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objTransactionLogDetailMasterDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<DocumentNo>" + (objTransactionLogDetailMasterDetails.DocumentNo) + "</DocumentNo>");
                sSql.Append("</TransactionLogDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var NonTradingStockList = new List<NonTradingStockHeaderTypes>();
            var RequestData = (SelectALLNonTradingStockRequest)RequestObj;
            var ResponseData = new SelectALLNonTradingStockResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    sQuery = "Select * from StockReturnHeader with(NoLock) Where Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Fromstoreid ='" + RequestData.StoreID + "'";
                //}
                //sQuery = "Select NTH.ID,NTH.DocumentNo,NTH.DocumentDate,MAX(NTH.EmployeeName) as EmployeeName,MAX(NTH.Employeecode) as Employeecode,MAX(NTH.TransactionType) as TransactionType,SUM(NTD.ReceivedQty) as ReceivedQty,SUM(NTD.ReturnQty) as ReturnQty from [NonTradingStockDetails] NTD JOIN[NonTradingStockHeader] NTH ON NTD.NonTradingHeaderID = NTH.ID where TransactionType='Issue' group by NTH.ID,NTH.DocumentNo,NTH.DocumentDate--,NTH.EmployeeName order by DocumentNo desc";
                sQuery = "Select NTH.ID,NTH.DocumentNo,NTH.DocumentDate,MAX(NTH.EmployeeName) as EmployeeName,MAX(NTH.Employeecode) as Employeecode,MAX(NTH.TransactionType) as TransactionType,SUM(NTD.ReceivedQty) as ReceivedQty,SUM(NTD.ReturnQty) as ReturnQty from[NonTradingStockDetails] NTD JOIN[NonTradingStockHeader] NTH ON NTD.NonTradingHeaderID = NTH.ID  group by NTH.ID,NTH.DocumentNo,NTH.DocumentDate--,NTH.EmployeeName order by DocumentNo desc";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objNonTradingStock = new NonTradingStockHeaderTypes();  
                        objNonTradingStock.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objNonTradingStock.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objNonTradingStock.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objNonTradingStock.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objNonTradingStock.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objNonTradingStock.ReceivedQty = objReader["ReceivedQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQty"]) : 0;
                        objNonTradingStock.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;    
                        objNonTradingStock.TransactionType = Convert.ToString(objReader["TransactionType"]);
                        //objStockReturn.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockReturn.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockReturn.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockReturn.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStockReturn.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objStockReturn.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        NonTradingStockList.Add(objNonTradingStock);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.NonTradingStockHeaderList = NonTradingStockList;
                    ResponseData.ResponseDynamicData = NonTradingStockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReturn Master");
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
            throw new NotImplementedException();
        }       

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var StockReturnRecord = new NonTradingStockHeaderTypes();
            var RequestData = (SelectByNonTradingHeaderIDRequest)RequestObj;
            var ResponseData = new SelectByNonTradingStockIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("Select * from NonTradingStockHeader NTH JOIN NonTradingStockDetails NTD ON NTH.ID = NTD.NonTradingHeaderID ");
                if (RequestData.ID != 0)
                {
                    sSql.Append("where NTH.ID = " + RequestData.ID + "");
                }
                else
                {
                    sSql.Append("where DocumentNo = '" + RequestData.DocumentNo + "' and ISNULL(NTD.ReturnQty,0) = 0");
                }
                //sSql.Append("Select * from NonTradingStockHeader with(NoLock)");
                //if (RequestData.ID != 0)
                //{
                //    sSql.Append("where ID = " + RequestData.ID + "");                  
                //}
                //else
                //{
                //    sSql.Append("where DocumentNo = '" + RequestData.DocumentNo + "'");
                //}
                //sSql = string.Format(sSql, RequestData.ID, RequestData.DocumentNo);
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from NonTradingStockHeader with(NoLock) where ID='{0}' or DocumentNo = {0} ";              
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReturn = new NonTradingStockHeaderTypes();
                        objStockReturn.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReturn.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReturn.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReturn.TransactionType = Convert.ToString(objReader["TransactionType"]);
                        objStockReturn.ReceivedType = Convert.ToString(objReader["ReceivedType"]);
                        objStockReturn.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objStockReturn.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        objStockReturn.NonTradingStockDetailsList = new List<NonTradingStockDetailsTypes>();
                        //SelectByNonTradingHeaderIDRequest objSelectByStockReturnDetailsRequest = new SelectByNonTradingHeaderIDRequest();
                        ////SelectByNonTradingStockIDResponse objSelectByStockReturnDetailsResponse = new SelectByNonTradingStockIDResponse();
                        //objSelectByStockReturnDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSelectByStockReturnDetailsRequest.DocumentNumber = objStockReturn.DocumentNo;
                        //objSelectByStockReturnDetailsRequest.DocumentDate = objStockReturn.DocumentDate;

                        SelectByNonTraddingDetailsIDRequest detailRequest = new SelectByNonTraddingDetailsIDRequest();
                        SelectByNonTradingDetailsIDResponse detailResponse = new SelectByNonTradingDetailsIDResponse();
                        detailRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        detailRequest.Mode = "Edit";
                        detailResponse = SelectByNonTradingStockDetails(detailRequest);
                        if (detailResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockReturn.NonTradingStockDetailsList = detailResponse.NonTradingStockDetailsRecord;
                            //objStockReturn.TransactionLogList = objSelectByStockReturnDetailsResponse.NonTradingStockList;
                        }

                        //if (objSelectByStockReturnDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objStockReturn.NonTradingStockDetailsList = objSelectByStockReturnDetailsResponse.NonTradingStockDetailsList;
                        //    objStockReturn.TransactionLogList = objSelectByStockReturnDetailsResponse.NonTradingStockList;
                        //}
                        ResponseData.NonTradingStockHeaderRecord = objStockReturn;
                        ResponseData.ResponseDynamicData = objStockReturn;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "NonTradingItem");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }      

        public override SelectByNonTradingDetailsIDResponse SelectByNonTradingStockDetails(SelectByNonTraddingDetailsIDRequest ObjRequest)
        {
            var TransactionLogList = new List<TransactionLog>();
            var StockReturnDetailMasterList = new List<NonTradingStockDetailsTypes>();
            var RequestData = (SelectByNonTraddingDetailsIDRequest)ObjRequest;
            var ResponseData = new SelectByNonTradingDetailsIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("Select * from NonTradingStockDetails ");
                if (RequestData.Mode == "Edit")
                {
                    sSql.Append("where NonTradingHeaderID = " + RequestData.ID + " order by SerialNo");
                }
                else
                {
                    sSql.Append("where NonTradingHeaderID = " + RequestData.ID + " and ISNULL(ReturnQty,0) = 0 order by SerialNo");
                }
                //var sSql = new StringBuilder();
                //sSql.Append("Select * from NonTradingStockDetails ");
                //sSql.Append("where NonTradingHeaderID = " + RequestData.ID + " order by SerialNo");
                ////sSql.Append("order by SRD.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReturnDetailMaster = new NonTradingStockDetailsTypes();
                        objStockReturnDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReturnDetailMaster.SerialNo=objReader["SerialNo"] != DBNull.Value ? Convert.ToInt32(objReader["SerialNo"]) : 0;
                        objStockReturnDetailMaster.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        objStockReturnDetailMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objStockReturnDetailMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objStockReturnDetailMaster.ReceivedQty = objReader["ReceivedQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQty"]) : 0;
                        if(objStockReturnDetailMaster.ReceivedQty==0)
                            objStockReturnDetailMaster.ReceivedQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
                        objStockReturnDetailMaster.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
                        StockReturnDetailMasterList.Add(objStockReturnDetailMaster);

                        TransactionLog objTransactionType = new TransactionLog();
                        objTransactionType.ID = 0;
                        objTransactionType.TransactionType = "StockReturn";
                        objTransactionType.BusinessDate = RequestData.DocumentDate;
                        objTransactionType.ActualDateTime = DateTime.Now;
                        objTransactionType.DocumentID = objStockReturnDetailMaster.ID;
                        objTransactionType.SKUCode = objStockReturnDetailMaster.SKUCode;
                        objTransactionType.CountryID = RequestData.FromOrToCountryID;
                        objTransactionType.StoreID = RequestData.FromOrToStoreID;
                        objTransactionType.InQty = 0;
                        objTransactionType.OutQty = 0;
                        objTransactionType.TransactionPrice = 0;
                        objTransactionType.Currency = 0;
                        objTransactionType.ExchangeRate = 0;
                        objTransactionType.DocumentPrice = 0;
                        objTransactionType.UserID = 0;
                        objTransactionType.DocumentNo = Convert.ToString(RequestData.DocumentNumber);
                        objTransactionType.StoreID = RequestData.FromOrToStoreID;
                        objTransactionType.StoreCode = Convert.ToString(RequestData.FromOrToStoreCode);
                        objTransactionType.CountryID = RequestData.FromOrToCountryID;
                        objTransactionType.CountryCode = String.Empty;

                        TransactionLogList.Add(objTransactionType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.NonTradingStockDetailsRecord = StockReturnDetailMasterList;
                    ResponseData.TransactionLogList = TransactionLogList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "NonTradingItem");
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

        public override SelectALLNonTradingStockResponse API_SelectALL(SelectALLNonTradingStockRequest objRequest)
        {
            var NonTradingStockList = new List<NonTradingStockHeaderTypes>();
            var RequestData = (SelectALLNonTradingStockRequest)objRequest;
            var ResponseData = new SelectALLNonTradingStockResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                ///string sQuery = string.Empty;
                var sQuery = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    sQuery = "Select * from StockReturnHeader with(NoLock) Where Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Fromstoreid ='" + RequestData.StoreID + "'";
                //}
                //sQuery = "Select NTH.ID,NTH.DocumentNo,NTH.DocumentDate,MAX(NTH.EmployeeName) as EmployeeName,MAX(NTH.Employeecode) as Employeecode,MAX(NTH.TransactionType) as TransactionType,SUM(NTD.ReceivedQty) as ReceivedQty,SUM(NTD.ReturnQty) as ReturnQty from [NonTradingStockDetails] NTD JOIN[NonTradingStockHeader] NTH ON NTD.NonTradingHeaderID = NTH.ID where TransactionType='Issue' group by NTH.ID,NTH.DocumentNo,NTH.DocumentDate--,NTH.EmployeeName order by DocumentNo desc";
                //sQuery = "Select NTH.ID,NTH.DocumentNo,NTH.DocumentDate,MAX(NTH.EmployeeName) as EmployeeName,MAX(NTH.Employeecode) as Employeecode,MAX(NTH.TransactionType) as TransactionType,SUM(NTD.ReceivedQty) as ReceivedQty,SUM(NTD.ReturnQty) as ReturnQty from[NonTradingStockDetails] NTD JOIN[NonTradingStockHeader] NTH ON NTD.NonTradingHeaderID = NTH.ID  group by NTH.ID,NTH.DocumentNo,NTH.DocumentDate--,NTH.EmployeeName order by DocumentNo desc";

                sQuery.Append("Select NTH.ID,NTH.DocumentNo,NTH.DocumentDate,MAX(NTH.EmployeeName) as EmployeeName,MAX(NTH.TransactionType) as TransactionType,SUM(NTD.ReceivedQty) as ReceivedQty,SUM(NTD.ReturnQty) as ReturnQty,RC.TOTAL_CNT [RecordCount] ");
                sQuery.Append("from[NonTradingStockDetails] NTD with(NoLock) JOIN[NonTradingStockHeader] NTH with(NoLock) ON NTD.NonTradingHeaderID = NTH.ID ");
                sQuery.Append("LEFT JOIN(Select  count(NTD1.ID) As TOTAL_CNT From NonTradingStockDetails NTD1  with(NoLock) ");
                sQuery.Append("JOIN[NonTradingStockHeader] NTH1 with(NoLock) ON NTD1.NonTradingHeaderID = NTH1.ID ");
                sQuery.Append("where (isnull('" + RequestData.SearchString + "','') = '' ");
                sQuery.Append("or NTH1.DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                if (RequestData.SearchString.Contains("-"))
                {
                    sQuery.Append("or NTH1.DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                }
                sQuery.Append("or EmployeeName like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or TransactionType like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or ReceivedQty like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or ReturnQty like isnull('%" + RequestData.SearchString + "%','')))  As RC ON 1 = 1 ");
                //sQuery.Append("group by NTH1.ID,NTH1.DocumentNo,NTH1.DocumentDate order by DocumentNo asc ");

                sQuery.Append("where (isnull('" + RequestData.SearchString + "','') = '' ");
                sQuery.Append("or NTH.DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                if (RequestData.SearchString.Contains("-"))
                {
                    sQuery.Append("or NTH.DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                }
                sQuery.Append("or EmployeeName like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or TransactionType like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or ReceivedQty like isnull('%" + RequestData.SearchString + "%','') ");
                sQuery.Append("or ReturnQty like isnull('%" + RequestData.SearchString + "%','')) ");
                sQuery.Append("group by NTH.ID,NTH.DocumentNo,NTH.DocumentDate,RC.TOTAL_CNT  order by DocumentNo asc ");
                sQuery.Append("offset " + RequestData.Offset + " rows ");
                sQuery.Append("fetch first " + RequestData.Limit + " rows only");

                //sQuery = "Select NTH.ID,NTH.DocumentNo,NTH.DocumentDate,MAX(NTH.EmployeeName) as EmployeeName,MAX(NTH.TransactionType) as TransactionType,SUM(NTD.ReceivedQty) as ReceivedQty,SUM(NTD.ReturnQty) as ReturnQty,RecordCount = COUNT(*) OVER() " +
                //  "from[NonTradingStockDetails] NTD JOIN[NonTradingStockHeader] NTH ON NTD.NonTradingHeaderID = NTH.ID " +                  
                //      "where (isnull('" + RequestData.SearchString + "','') = '' " +
                //          "or NTH.DocumentNo = isnull('" + RequestData.SearchString + "','') " +
                //          //"or ,NTH.DocumentDate = isnull('" + RequestData.SearchString + "','') " +
                //          "or EmployeeName = isnull('" + RequestData.SearchString + "','') " +
                //          "or TransactionType = isnull('" + RequestData.SearchString + "','')) " +
                //  //"or ReceivedQty = isnull('" + RequestData.SearchString + "','') " +
                //  //"or ReturnQty = isnull('" + RequestData.SearchString + "','')) " +
                //  "group by NTH.ID,NTH.DocumentNo,NTH.DocumentDate order by DocumentNo asc " +
                //  "offset " + RequestData.Offset + " rows " +
                //  "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objNonTradingStock = new NonTradingStockHeaderTypes();
                        objNonTradingStock.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objNonTradingStock.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objNonTradingStock.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objNonTradingStock.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                        //objNonTradingStock.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                        objNonTradingStock.ReceivedQty = objReader["ReceivedQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQty"]) : 0;
                        objNonTradingStock.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
                        objNonTradingStock.TransactionType = Convert.ToString(objReader["TransactionType"]);
                        //objStockReturn.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockReturn.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockReturn.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockReturn.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStockReturn.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objStockReturn.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        NonTradingStockList.Add(objNonTradingStock);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.NonTradingStockHeaderList = NonTradingStockList;
                    //ResponseData.ResponseDynamicData = NonTradingStockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Non Trading Stock Distribution");
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
    }
}
