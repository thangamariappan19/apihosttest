using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.Stocks.StockReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Stocks
{
    public class StockReturnDAL : BaseStockReturnDAL
    {
        SqlConnection _ConnectionObj;
        SqlConnection cnn;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStockReturnRequest)RequestObj;
            var ResponseData = new SaveStockReturnResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateStockReturn", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StockReturnHeaderRecord.ID;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.StockReturnHeaderRecord.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.StockReturnHeaderRecord.DocumentDate);

                SqlParameter TotalQuantity = _CommandObj.Parameters.Add("@TotalQuantity", SqlDbType.Int);
                TotalQuantity.Direction = ParameterDirection.Input;
                TotalQuantity.Value = RequestData.StockReturnHeaderRecord.TotalQuantity;

                SqlParameter ToWareHouseID = _CommandObj.Parameters.Add("@ToWareHouseID", SqlDbType.Int);
                ToWareHouseID.Direction = ParameterDirection.Input;
                ToWareHouseID.Value = RequestData.StockReturnHeaderRecord.ToWareHouseID;

                SqlParameter ToWareHouseCode = _CommandObj.Parameters.Add("@ToWareHouseCode", SqlDbType.NVarChar);
                ToWareHouseCode.Direction = ParameterDirection.Input;
                ToWareHouseCode.Value = RequestData.StockReturnHeaderRecord.ToWareHouseCode;

                SqlParameter FromStoreID = _CommandObj.Parameters.Add("@FromStoreID", SqlDbType.Int);
                FromStoreID.Direction = ParameterDirection.Input;
                FromStoreID.Value = RequestData.StockReturnHeaderRecord.FromStoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StockReturnHeaderRecord.StoreCode;

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.StockReturnHeaderRecord.Status;                

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StockReturnHeaderRecord.CreateBy;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.StockReturnHeaderRecord.Remarks;

                SqlParameter ReturnType = _CommandObj.Parameters.Add("@ReturnType", SqlDbType.VarChar);
                ReturnType.Direction = ParameterDirection.Input;
                ReturnType.Value = RequestData.StockReturnHeaderRecord.ReturnType;

                SqlParameter BinCode = _CommandObj.Parameters.Add("@BinCode", SqlDbType.VarChar);
                BinCode.Direction = ParameterDirection.Input;
                BinCode.Value = RequestData.StockReturnHeaderRecord.BinCode;

                SqlParameter RunningNo = _CommandObj.Parameters.Add("@RunningNo", SqlDbType.Int);
                RunningNo.Direction = ParameterDirection.Input;
                RunningNo.Value = RequestData.RunningNo;

                SqlParameter DocumentNumberingID = _CommandObj.Parameters.Add("@DocumentNumberingID", SqlDbType.Int);
                DocumentNumberingID.Direction = ParameterDirection.Input;
                DocumentNumberingID.Value = RequestData.DetailID;

                SqlParameter StockReturnDetails = _CommandObj.Parameters.Add("@StockReturnDetails", SqlDbType.Xml);
                StockReturnDetails.Direction = ParameterDirection.Input;
                StockReturnDetails.Value = StockReturnDetailMasterXML(RequestData.StockReturnDetailsList);
                //StockReturnDetails.Value = StockReturnDetails.ToString().Replace("&", "&#38;");

                SqlParameter TransactionLogDetails = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLogDetails.Direction = ParameterDirection.Input;
                TransactionLogDetails.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 10);
                ID2.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "StockReturn");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "StockReturn");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReturn");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReturn");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public string TransactionLogDetailMasterXML(List<TransactionLog> TransactionLogDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (TransactionLog objTransactionLogDetailMasterDetails in TransactionLogDetailMasterList)
            {
                sSql.Append("<TransactionLogDetailsData>");
                sSql.Append("<ID>" + objTransactionLogDetailMasterDetails.ID + "</ID>");
                sSql.Append("<TransactionType>" + objTransactionLogDetailMasterDetails.TransactionType + "</TransactionType>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.BusinessDate) + "</BusinessDate>");
                sSql.Append("<ActualDateTime>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ActualDateTime) + "</ActualDateTime>");               
                sSql.Append("<DocumentID>" + (objTransactionLogDetailMasterDetails.DocumentID) + "</DocumentID>");                           
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
                sSql.Append("<Tag_Id>" + (objTransactionLogDetailMasterDetails.Tag_Id) + "</Tag_Id>");
                sSql.Append("</TransactionLogDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        } 
        public string StockReturnDetailMasterXML(List<StockReturnDetails> StockReturnDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StockReturnDetails objStockReturnDetailMasterDetails in StockReturnDetailMasterList)
            {
                sSql.Append("<StockReturnDetailsData>");
                sSql.Append("<ID>" + objStockReturnDetailMasterDetails.StockReturnDetailID + "</ID>");
                sSql.Append("<HeaderID>" + objStockReturnDetailMasterDetails.HeaderID + "</HeaderID>");
                sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString( objStockReturnDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objStockReturnDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                //sSql.Append("<ReasonID>" + objStockReturnDetailMasterDetails.ReasonID + "</ReasonID>");
                sSql.Append("<SKUID>" + (objStockReturnDetailMasterDetails.SKUID) + "</SKUID>");
                sSql.Append("<StyleCode>" + (objStockReturnDetailMasterDetails.StyleCode) + "</StyleCode>");

                sSql.Append("<SKUName>" + objStockReturnDetailMasterDetails.SKUName + "</SKUName>");
                sSql.Append("<Brand>" + objStockReturnDetailMasterDetails.Brand + "</Brand>");
                sSql.Append("<Color>" + objStockReturnDetailMasterDetails.Color + "</Color>");
                sSql.Append("<Size>" + (objStockReturnDetailMasterDetails.Size) + "</Size>");
                sSql.Append("<BarCode>" + (objStockReturnDetailMasterDetails.BarCode) + "</BarCode>");
                sSql.Append("<Tag_Id>" + (objStockReturnDetailMasterDetails.Tag_Id) + "</Tag_Id>");

                sSql.Append("<SKUCode>" + objStockReturnDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<FromStoreID>" + objStockReturnDetailMasterDetails.FromStoreID + "</FromStoreID>");
                sSql.Append("<Quantity>" + objStockReturnDetailMasterDetails.Quantity + "</Quantity>");
                sSql.Append("<FromStoreID>" + (objStockReturnDetailMasterDetails.FromStoreID) + "</FromStoreID>");
                sSql.Append("<Remarks>" + (objStockReturnDetailMasterDetails.Remarks) + "</Remarks>");
                sSql.Append("<BinCode>" + (objStockReturnDetailMasterDetails.BinCode) + "</BinCode>");
                sSql.Append("</StockReturnDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
            //return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
        }   

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockReturnRecord = new StockReturnHeader();
            var RequestData = (DeleteStockReturnRequest)RequestObj;
            var ResponseData = new DeleteStockReturnResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from StockReturnDetails where HeaderID={0}; Delete from StockReturnHeader where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Stock Return");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Stock Return");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockReturnRecord = new StockReturnHeader();
            var RequestData = (SelectByStockReturnIDRequest)RequestObj;
            var ResponseData = new SelectByStockReturnIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from StockReturnHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReturn = new StockReturnHeader();
                        objStockReturn.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReturn.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReturn.ToWareHouseID = objReader["ToWareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["ToWareHouseID"]) : 0;
                            
                        objStockReturn.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReturn.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;                            
                        objStockReturn.Status = Convert.ToString(objReader["Status"]);
                        objStockReturn.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockReturn.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStockReturn.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReturn.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockReturn.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockReturn.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockReturn.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReturn.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStockReturn.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        objStockReturn.ReturnType = Convert.ToString(objReader["ReturnType"]);
                        objStockReturn.ToWareHouseCode = objReader["ToWareHouseCode"] != DBNull.Value ? Convert.ToString(objReader["ToWareHouseCode"]) : "";

                        objStockReturn.StockReturnDetailsList = new List<StockReturnDetails>();

                        SelectByStockReturnDetailsRequest objSelectByStockReturnDetailsRequest = new SelectByStockReturnDetailsRequest();
                        SelectByStockReturnDetailsResponse objSelectByStockReturnDetailsResponse = new SelectByStockReturnDetailsResponse();
                        objSelectByStockReturnDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByStockReturnDetailsRequest.DocumentNumber = objStockReturn.DocumentNo;
                        objSelectByStockReturnDetailsRequest.DocumentDate = objStockReturn.DocumentDate;
                        objSelectByStockReturnDetailsRequest.FromOrToStoreID = objStockReturn.FromStoreID;
                        objSelectByStockReturnDetailsRequest.FromOrToStoreCode = objStockReturn.StoreCode;

                        objSelectByStockReturnDetailsResponse = SelectByStockReturnDetails(objSelectByStockReturnDetailsRequest);
                        if (objSelectByStockReturnDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockReturn.StockReturnDetailsList = objSelectByStockReturnDetailsResponse.StockReturnDetailsRecord;
                            objStockReturn.TransactionLogList = objSelectByStockReturnDetailsResponse.TransactionLogList;
                        }

                        ResponseData.StockReturnHeaderRecord = objStockReturn;
                        ResponseData.ResponseDynamicData = objStockReturn;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReturn");
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockReturnList = new List<StockReturnHeader>();
            var RequestData = (SelectAllStockReturnRequest)RequestObj;
            var ResponseData = new SelectAllStockReturnResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sQuery = "Select * from StockReturnHeader with(NoLock) Where Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Fromstoreid ='" + RequestData.StoreID + "'";
                }
                else
                {
                    sQuery = "Select * from StockReturnHeader with(NoLock) where storeid='" + RequestData.StoreID + "'";
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReturn = new StockReturnHeader();
                        objStockReturn.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReturn.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReturn.ToWareHouseID = objReader["ToWareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["ToWareHouseID"]) : 0;
                        objStockReturn.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReturn.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockReturn.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;                          
                            
                        objStockReturn.Status = Convert.ToString(objReader["Status"]);
                        objStockReturn.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReturn.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockReturn.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockReturn.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockReturn.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReturn.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StockReturnList.Add(objStockReturn);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReturnHeaderList = StockReturnList;
                    ResponseData.ResponseDynamicData = StockReturnList;
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectByStockReturnDetailsResponse SelectByStockReturnDetails(SelectByStockReturnDetailsRequest ObjRequest)
        {
            var TransactionLogList = new List<TransactionLog>();
            var StockReturnDetailMasterList = new List<StockReturnDetails>();
            var RequestData = (SelectByStockReturnDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockReturnDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select *,sk.StyleCode from StockReturnDetails SRD inner join SKUMaster SK on SRD.SKUCode=sk.SKUCode ");
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
                        var objStockReturnDetailMaster = new StockReturnDetails();
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
                        StockReturnDetailMasterList.Add(objStockReturnDetailMaster);

                        TransactionLog objTransactionType = new TransactionLog();
                        objTransactionType.ID = 0;
                        objTransactionType.TransactionType = "StockReturn";
                        objTransactionType.BusinessDate = RequestData.DocumentDate;
                        objTransactionType.ActualDateTime = DateTime.Now;
                        objTransactionType.DocumentID = objStockReturnDetailMaster.HeaderID;
                        objTransactionType.StyleCode = objStockReturnDetailMaster.StyleCode;
                        objTransactionType.SKUCode = objStockReturnDetailMaster.SKUCode;
                        objTransactionType.CountryID = RequestData.FromOrToCountryID;
                        objTransactionType.StoreID = RequestData.FromOrToStoreID;
                        objTransactionType.InQty = 0;
                        objTransactionType.OutQty = objStockReturnDetailMaster.Quantity;
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
                    ResponseData.StockReturnDetailsRecord = StockReturnDetailMasterList;
                    ResponseData.TransactionLogList = TransactionLogList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReturn");
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


        public override SaveStockReturnResponse Saveint_stock(SaveStockReturnRequest ObjRequest)
        {
            var RequestData = (SaveStockReturnRequest)ObjRequest;
            var ResponseData = new SaveStockReturnResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon1 = new MsSqlCommon();
            var sqlCommon = new MsSqlCommon();
            string connetionString;

            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];

                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _RequestFrom = RequestData.RequestFrom;

                    _CommandObj = new SqlCommand("Insertint_stockReturn", cnn);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    //_CommandObj.CommandTimeout = 300000;

                    SqlParameter int_stock = _CommandObj.Parameters.Add("@int_stockReturn", SqlDbType.Xml);
                    int_stock.Direction = ParameterDirection.Input;
                    int_stock.Value = int_stockXML(RequestData.int_stockreturnList);

                    SqlParameter TLIDs = _CommandObj.Parameters.Add("@TLIDs", SqlDbType.VarChar, 10);
                    TLIDs.Direction = ParameterDirection.Output;

                    SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                    StatusCode.Direction = ParameterDirection.Output;

                    SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                    StatusMsg.Direction = ParameterDirection.Output;

                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();
                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = "Records saved into CentralUnit database !.";
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = TLIDs.Value.ToString();
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = "Records are not saved into CentralUnit.Please save the records manually !.";
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit databse not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "int_stock");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }    
            return ResponseData;
        }

        public string int_stockXML(List<int_stockreturn> int_stockreturnList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockreturn objint_stockrequestTypes in int_stockreturnList)
            {
                sSql.Append("<int_stockreturnData>");
                sSql.Append("<ID>" + objint_stockrequestTypes.ID + "</ID>");
                sSql.Append("<DocNum>" + objint_stockrequestTypes.DocNum + "</DocNum>");
                sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objint_stockrequestTypes.DocDate) + "</DocDate>");                             
                sSql.Append("<FromLocation>" + objint_stockrequestTypes.FromLocation + "</FromLocation>");
                sSql.Append("<ToLocation>" + objint_stockrequestTypes.ToLocation + "</ToLocation>");               
                sSql.Append("<ItemCode>" + (objint_stockrequestTypes.SKUCode) + "</ItemCode>");
                sSql.Append("<ItemName>" + (objint_stockrequestTypes.SKUName) + "</ItemName>");
                sSql.Append("<BarCode>" + (objint_stockrequestTypes.BarCode) + "</BarCode>");
                sSql.Append("<Quantity>" + (objint_stockrequestTypes.Quantity) + "</Quantity>");
                sSql.Append("<Remarks>" + (objint_stockrequestTypes.Remarks) + "</Remarks>");
                sSql.Append("<Flag>" + (objint_stockrequestTypes.Flag) + "</Flag>");

                sSql.Append("</int_stockreturnData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public bool IsPinging(string SqlConString)
        {
            return true;
            //bool objBool = false;
            //try
            //{
            //    SqlConnectionStringBuilder SQLConBuilder = new SqlConnectionStringBuilder(SqlConString);
            //    var DataSource = SQLConBuilder.DataSource.Split('\\');
            //    string StoreIP = string.Empty;
            //    if (DataSource.Length > 0)
            //    {
            //        StoreIP = Convert.ToString(DataSource[0]);
            //    }
            //    if (StoreIP != null && StoreIP != string.Empty)
            //    {
            //        Ping myPing = new Ping();
            //        PingReply reply = myPing.Send(StoreIP, 60);
            //        if (reply != null)
            //        {
            //            if (reply.Status == IPStatus.Success)
            //            {
            //                objBool = true;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    objBool = false;
            //}
            //return objBool;
        }
        private string ReturnCloseList(string StoreCode)
        {
            var DocumentNos = string.Empty;
            var _DocumentNos = string.Empty;
            DataSet _DataSet = new DataSet();           
            StringBuilder sSql = new StringBuilder();           
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                SqlConnection _SqlConnection = new SqlConnection(connetionString);
                SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter("Select * from int_stockreturn where FromLocation='" + StoreCode + "' and Flag=1", _SqlConnection);
                _SqlDataAdapter.Fill(_DataSet);

                if(_DataSet.Tables.Count >0 && _DataSet.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow ObjDataRow in _DataSet.Tables[0].Rows)
                    {
                        string DocNum = Convert.ToString(ObjDataRow["DocNum"]);
                        DocumentNos = DocumentNos + "'" + DocNum + "',";
                    }                    
                    if(DocumentNos != string.Empty)
                    {
                        _DocumentNos = DocumentNos.Remove(DocumentNos.Length - 1);                        
                    }
                }
            }
            catch
            {
                _DocumentNos = null;
            }
            return _DocumentNos;
        }
        public override UpdateStockReturnResponse CloseOpenDocuments(UpdateStockReturnRequest RequestData)
        {
            var ResponseData = new UpdateStockReturnResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                string DocumentNos = ReturnCloseList(RequestData.StoreCode);
                if (DocumentNos != null && DocumentNos != string.Empty)
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;

                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand("Update StockReturnHeader set Status='Closed' where StoreID=" + RequestData.StoreID + " and DocumentNo in(" + DocumentNos + ")", _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    _CommandObj.ExecuteNonQuery();
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.UpdateRecordFailed;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectAllStockReturnResponse GetStockReturnHeaderReport(SelectAllStockReturnRequest objRequest)
        {
            var StockReturnDetailsList = new List<StockReturnHeader>();
            var RequestData = (SelectAllStockReturnRequest)objRequest;
            var ResponseData = new SelectAllStockReturnResponse();
            SqlDataReader objReader;
            //new
            string StoreDBConnection = null;
            string EncyptConnection = null;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select * from DBConnections where StoreID = 2");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StoreDBConnection = objReader["ConnString_Integration"] != DBNull.Value ? Convert.ToString(objReader["ConnString_Integration"]) : string.Empty;
                        EncyptConnection = objReader["ConnectionString"] != DBNull.Value ? Convert.ToString(objReader["ConnectionString"]) : string.Empty;
                    }
                }
                sqlCommon.CloseConnection(_ConnectionObj);
                _ConnectionString = EncyptConnection;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Get_StockReturnHeader", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter FromDate = _CommandObj.Parameters.Add("@FromDate", SqlDbType.Date);
                FromDate.Direction = ParameterDirection.Input;
                FromDate.Value = RequestData.FromDate.ToString("yyyy-MM-dd");

                SqlParameter ToDate = _CommandObj.Parameters.Add("@ToDate", SqlDbType.Date);
                ToDate.Direction = ParameterDirection.Input;
                ToDate.Value = RequestData.ToDate.ToString("yyyy-MM-dd");

                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReturnHeader();
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        objStockReceipt.ToWareHouseCode = objReader["ToWareHouseCode"] != DBNull.Value ? Convert.ToString(objReader["ToWareHouseCode"]) : "";
                        objStockReceipt.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReceipt.Remarks = objReader["UserName"] != DBNull.Value ? Convert.ToString(objReader["UserName"]) : "";

                        StockReturnDetailsList.Add(objStockReceipt);
                    }
                    ResponseData.StockReturnHeaderList = StockReturnDetailsList;
                    ResponseData.ResponseDynamicData = StockReturnDetailsList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt");
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

        public override SelectAllStockReturnResponse GetStockReturnDetailsReport(SelectAllStockReturnRequest objRequest)
        {
            var StockReturnDetailsList = new List<StockReturnDetails>();
            var RequestData = (SelectAllStockReturnRequest)objRequest;
            var ResponseData = new SelectAllStockReturnResponse();
            SqlDataReader objReader;
            /*string StoreDBConnection = null;
            string EncyptConnection = null;*/
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                /*sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select * from DBConnections where StoreID = 2");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StoreDBConnection = objReader["ConnString_Integration"] != DBNull.Value ? Convert.ToString(objReader["ConnString_Integration"]) : string.Empty;
                        EncyptConnection = objReader["ConnectionString"] != DBNull.Value ? Convert.ToString(objReader["ConnectionString"]) : string.Empty;
                    }
                }
                sqlCommon.CloseConnection(_ConnectionObj);
                _ConnectionString = EncyptConnection;*/

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Get_StockReturnDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter FromDate = _CommandObj.Parameters.Add("@FromDate", SqlDbType.Date);
                FromDate.Direction = ParameterDirection.Input;
                FromDate.Value = RequestData.FromDate.ToString("yyyy-MM-dd");

                SqlParameter ToDate = _CommandObj.Parameters.Add("@ToDate", SqlDbType.Date);
                ToDate.Direction = ParameterDirection.Input;
                ToDate.Value = RequestData.ToDate.ToString("yyyy-MM-dd");

                _CommandObj.CommandType = CommandType.StoredProcedure;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReturnDetails();
                        /* for Document no*/
                        objStockReceipt.Brand = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        /* for from storecode*/
                        objStockReceipt.Remarks = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        /* for To WareHouse*/
                        objStockReceipt.SKUName = objReader["ToWareHouseCode"] != DBNull.Value ? Convert.ToString(objReader["ToWareHouseCode"]) : "";
                        objStockReceipt.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
                        objStockReceipt.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                        //objStockReceipt.ReceivedQuantity = objReader["ReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQuantity"]) : 0;
                        objStockReceipt.CreateOn = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                       // objStockReceipt.CreateBy = objReader["UserName"] != DBNull.Value ? Convert.ToInt32(objReader["UserName"]) : 0;
                        //objStockReceipt.Discrepancies = objReader["Discrepancies"] != DBNull.Value ? Convert.ToInt32(objReader["Discrepancies"]) : 0;

                        StockReturnDetailsList.Add(objStockReceipt);
                    }
                    ResponseData.StockReturnDetailsList = StockReturnDetailsList;
                    ResponseData.ResponseDynamicData = StockReturnDetailsList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt");
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

        public override SelectAllStockReturnResponse API_SelectALL(SelectAllStockReturnRequest objRequest)
        {
            var StockReturnList = new List<StockReturnHeader>();
            var RequestData = (SelectAllStockReturnRequest)objRequest;
            var ResponseData = new SelectAllStockReturnResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //string sQuery = string.Empty;
                var sQuery = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    sQuery = "Select * from StockReturnHeader with(NoLock) Where Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Fromstoreid ='" + RequestData.StoreID + "'";
                //}
                //else
                //{
                //    sQuery = "Select * from StockReturnHeader with(NoLock) where storeid='" + RequestData.StoreID + "'";
                //}

                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sQuery.Append("Select * from StockReturnHeader with(NoLock) Where Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Fromstoreid ='" + RequestData.StoreID + "'");
                    //sQuery = "Select * from StockReturnHeader with(NoLock) Where Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Fromstoreid ='" + RequestData.StoreID + "'";
                }
                else
                {
                    sQuery.Append("Select ID, DocumentNo, DocumentDate, Status, Active, RC.TOTAL_CNT [RecordCount] ");
                    sQuery.Append("from StockReturnHeader with (nolock) ");
                    sQuery.Append("LEFT JOIN(Select  count(SR.ID) As TOTAL_CNT From StockReturnHeader SR with(NoLock) ");
                    sQuery.Append("where SR.Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and SR.StoreID = " + RequestData.StoreID + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or SR.DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or SR.DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or SR.DocumentDate like  isnull('%" + RequestData.SearchString + "%','') ");
                    }
                    sQuery.Append("or SR.Status like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                    sQuery.Append("where Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and StoreID = " + RequestData.StoreID + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or DocumentDate like  isnull('%" + RequestData.SearchString + "%','') ");
                    }
                    sQuery.Append("or Status like isnull('%" + RequestData.SearchString + "%','')) ");
                    sQuery.Append("order by ID asc ");
                    sQuery.Append("offset " + RequestData.Offset + " rows ");
                    sQuery.Append("fetch first " + RequestData.Limit + " rows only");

                   // sQuery = "Select ID, DocumentNo, DocumentDate, Status, Active, RecordCount = COUNT(*) OVER() " +
                   //"from StockReturnHeader " +
                   //"where Active = " + RequestData.IsActive + " " +
                   //"and StoreID = " + RequestData.StoreID + " " +
                   //    "and (isnull('" + RequestData.SearchString + "','') = '' " +
                   //        "or DocumentNo = isnull('" + RequestData.SearchString + "','') " +
                   //        "or DocumentDate = isnull('" + RequestData.SearchString + "','') " +
                   //        "or Status = isnull('" + RequestData.SearchString + "','')) " +
                   //"order by ID asc " +
                   //"offset " + RequestData.Offset + " rows " +
                   //"fetch first " + RequestData.Limit + " rows only";
                }

                _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReturn = new StockReturnHeader();
                        objStockReturn.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objStockReturn.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        //objStockReturn.ToWareHouseID = objReader["ToWareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["ToWareHouseID"]) : 0;
                        objStockReturn.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        //objStockReturn.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockReturn.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;

                        objStockReturn.Status = Convert.ToString(objReader["Status"]);
                        //objStockReturn.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockReturn.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockReturn.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockReturn.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStockReturn.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReturn.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StockReturnList.Add(objStockReturn);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.StockReturnHeaderList = StockReturnList;
                    ResponseData.ResponseDynamicData = StockReturnList;
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
    }
}
