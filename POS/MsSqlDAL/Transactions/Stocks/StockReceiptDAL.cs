using EasyBizAbsDAL.Transactions.StockReceipt;
using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
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
    public class StockReceiptDAL : BaseStockReceiptDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlConnection cnn;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        String STKIDS;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStockReceiptRequest)RequestObj;
            var ResponseData = new SaveStockReceiptResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                if (RequestData.StockReceiptHeaderRecord == null)
                {
                    RequestData.StockReceiptHeaderRecord = RequestData.RequestDynamicData;
                }

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateStockReceipt", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StockReceiptHeaderRecord.ID;

                SqlParameter StockRequestID = _CommandObj.Parameters.Add("@StockRequestID", SqlDbType.Int);
                StockRequestID.Direction = ParameterDirection.Input;
                StockRequestID.Value = RequestData.StockReceiptHeaderRecord.StockRequestID;

                SqlParameter StockRequestDocumentNo = _CommandObj.Parameters.Add("@StockRequestDocumentNo", SqlDbType.NVarChar);
                StockRequestDocumentNo.Direction = ParameterDirection.Input;
                StockRequestDocumentNo.Value = RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.StockReceiptHeaderRecord.DocumentNo;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.StockReceiptHeaderRecord.Remarks;

                SqlParameter WithOutBaseDoc = _CommandObj.Parameters.Add("@WithOutBaseDoc", SqlDbType.NVarChar);
                WithOutBaseDoc.Direction = ParameterDirection.Input;
                WithOutBaseDoc.Value = RequestData.StockReceiptHeaderRecord.WithOutBaseDoc;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.Date);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateTimeString(RequestData.StockReceiptHeaderRecord.DocumentDate);

                SqlParameter TotalQuantity = _CommandObj.Parameters.Add("@TotalQuantity", SqlDbType.Int);
                TotalQuantity.Direction = ParameterDirection.Input;
                TotalQuantity.Value = RequestData.StockReceiptHeaderRecord.TotalQuantity;

                SqlParameter TotalReceivedQuantity = _CommandObj.Parameters.Add("@TotalReceivedQuantity", SqlDbType.Int);
                TotalReceivedQuantity.Direction = ParameterDirection.Input;
                TotalReceivedQuantity.Value = RequestData.StockReceiptHeaderRecord.TotalReceivedQuantity;


                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StockReceiptHeaderRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StockReceiptHeaderRecord.StoreCode;

                SqlParameter Type = _CommandObj.Parameters.Add("@Type", SqlDbType.Bit);
                Type.Direction = ParameterDirection.Input;
                Type.Value = RequestData.StockReceiptHeaderRecord.Type;

                SqlParameter StockRequestStatus = _CommandObj.Parameters.Add("@StockRequestStatus", SqlDbType.NVarChar);
                StockRequestStatus.Direction = ParameterDirection.Input;
                StockRequestStatus.Value = RequestData.StockReceiptHeaderRecord.StockRequestStatus;
            

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.StockReceiptHeaderRecord.Status;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StockReceiptHeaderRecord.CreateBy;


                SqlParameter StockReceiptDetails = _CommandObj.Parameters.Add("@StockReceiptDetails", SqlDbType.Xml);
                StockReceiptDetails.Direction = ParameterDirection.Input;
                StockReceiptDetails.Value = StockReceiptDetailMasterXML(RequestData.StockReceiptDetailsList);


                SqlParameter TransactionLogDetails = _CommandObj.Parameters.Add("@TransactionLogDetails", SqlDbType.Xml);
                TransactionLogDetails.Direction = ParameterDirection.Input;
                TransactionLogDetails.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter BinLogDetails = _CommandObj.Parameters.Add("@BinLogDetails", SqlDbType.Xml);
                BinLogDetails.Direction = ParameterDirection.Input;
                BinLogDetails.Value = BinLogDetailXML(RequestData.BinLogList);

                //SqlParameter RFIDTagList = _CommandObj.Parameters.Add("@RFIDTagList", SqlDbType.Xml);
                //RFIDTagList.Direction = ParameterDirection.Input;
                //RFIDTagList.Value = RFIDTagXml(RequestData.RFIDTagList);

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "StockReceipt");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                    STKIDS = ResponseData.IDs;


                    //SaveStockReceiptRequest objRequest = new SaveStockReceiptRequest();
                    //SaveStockReceiptResponse objSaveStockReceiptResponse = new SaveStockReceiptResponse();
                    //objSaveStockReceiptResponse = Update_ConfirmTransfer(objRequest);

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "StockReceipt");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string TransactionLogDetailMasterXML(List<TransactionLog> TransactionLogList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (TransactionLog objTransactionLogDetailMasterDetails in TransactionLogList)
            {
                if (objTransactionLogDetailMasterDetails.InQty != 0 && objTransactionLogDetailMasterDetails.OutQty == 0)
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

                    sSql.Append("</TransactionLogDetailsData>");
                }
                else if (objTransactionLogDetailMasterDetails.InQty == 0 && objTransactionLogDetailMasterDetails.OutQty != 0)
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

                    sSql.Append("</TransactionLogDetailsData>");
                }

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }  
        
        public string StockReceiptDetailMasterXML(List<StockReceiptDetails> StockReceiptDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StockReceiptDetails objStockReceiptDetailMasterDetails in StockReceiptDetailMasterList)
            {
                sSql.Append("<StockReceiptDetailsData>");
                sSql.Append("<ID>" + objStockReceiptDetailMasterDetails.ID + "</ID>");
                sSql.Append("<HeaderID>" + objStockReceiptDetailMasterDetails.HeaderID + "</HeaderID>");
                sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString(objStockReceiptDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objStockReceiptDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                //sSql.Append("<ReasonID>" + objStockReceiptDetailMasterDetails.ReasonID + "</ReasonID>");
                sSql.Append("<SKUID>" + (objStockReceiptDetailMasterDetails.SKUID) + "</SKUID>");
                sSql.Append("<DocumentNo>" + (objStockReceiptDetailMasterDetails.DocumentNo) + "</DocumentNo>");
                sSql.Append("<SKUName>" + (objStockReceiptDetailMasterDetails.SKUName) + "</SKUName>");
                sSql.Append("<SKUCode>" + objStockReceiptDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<Brand>" + (objStockReceiptDetailMasterDetails.Brand) + "</Brand>");
                sSql.Append("<Color>" + (objStockReceiptDetailMasterDetails.Color) + "</Color>");
                sSql.Append("<Size>" + objStockReceiptDetailMasterDetails.Size + "</Size>");
                sSql.Append("<BarCode>" + objStockReceiptDetailMasterDetails.BarCode + "</BarCode>");
                sSql.Append("<FromStoreID>" + objStockReceiptDetailMasterDetails.FromStoreID + "</FromStoreID>");
                sSql.Append("<RequestQuantity>" + objStockReceiptDetailMasterDetails.RequestQuantity + "</RequestQuantity>");
                sSql.Append("<TransferQuantity>" + objStockReceiptDetailMasterDetails.TransferQuantity + "</TransferQuantity>");
                sSql.Append("<ReceivedQuantity>" + objStockReceiptDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                sSql.Append("<DifferenceQuantity>" + objStockReceiptDetailMasterDetails.DifferenceQuantity + "</DifferenceQuantity>");
                sSql.Append("<Remarks>" + (objStockReceiptDetailMasterDetails.Remarks) + "</Remarks>");
                sSql.Append("</StockReceiptDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
        }

        public string BinLogDetailXML(List<BinLogTypes> BinLogDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (BinLogTypes objBinLogDetail in BinLogDetailList)
            {
                sSql.Append("<BinLogDetailsData>");
                sSql.Append("<ID>" + objBinLogDetail.ID + "</ID>");
                sSql.Append("<SKUCode>" + objBinLogDetail.SKUCode + "</SKUCode>");
                sSql.Append("<BarCode>" + objBinLogDetail.BarCode + "</BarCode>");
                sSql.Append("<RFID>" + objBinLogDetail.RFID + "</RFID>");
                sSql.Append("<Quantity>" + objBinLogDetail.Quantity + "</Quantity>");
                sSql.Append("<Status>" + objBinLogDetail.Status + "</Status>");
                sSql.Append("<Remarks>" + objBinLogDetail.Remarks + "</Remarks>");
                sSql.Append("<Active>" + objBinLogDetail.Active + "</Active>");
                sSql.Append("<StoreID>" + objBinLogDetail.StoreID + "</StoreID>");
                sSql.Append("<StoreCode>" + objBinLogDetail.StoreCode + "</StoreCode>");
                sSql.Append("<CreateBy>" + objBinLogDetail.CreateBy + "</CreateBy>");
                sSql.Append("<UpdateBy>" + objBinLogDetail.UpdateBy + "</UpdateBy>");
                sSql.Append("<BinID>" + objBinLogDetail.BinID + "</BinID>");
                sSql.Append("<BinCode>" + objBinLogDetail.BinCode + "</BinCode>");
                sSql.Append("<BinSubLevelCode>" + objBinLogDetail.BinSubLevelCode + "</BinSubLevelCode>");

                sSql.Append("</BinLogDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }
        public string RFIDTagXml(List<TagIdItemDetails> RFIDList)
        {
            var sqlCommon = new MsSqlCommon();
            StringBuilder sSql = new StringBuilder();
            var RFIDInList = RFIDList.Where(x => x.ItemStatus == "In").ToList();
            if (RFIDInList != null && RFIDInList.Count > 0)
            {
                foreach (var objTagIdItemDetails in RFIDInList)
                {
                    sSql.Append("<RFIDTagDetail>");
                    sSql.Append("<DocumentNo>" + objTagIdItemDetails.DocumentNo + "</DocumentNo>");
                    sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objTagIdItemDetails.DocumentDate) + "</DocumentDate>");
                    sSql.Append("<Itemcode>" + objTagIdItemDetails.Itemcode + "</Itemcode>");
                    sSql.Append("<Tag_Id>" + objTagIdItemDetails.Tag_ID + "</Tag_Id>");                    
                    sSql.Append("</RFIDTagDetail>");
                }
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockReceiptRecord = new StockReceiptHeader();
            var RequestData = (DeleteStockReceiptRequest)RequestObj;
            var ResponseData = new DeleteStockReceiptResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from StockReceiptDetails where HeaderID={0} ; Delete from StockReceiptHeader where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Stock Receipt");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Stock Receipt");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockReceiptRecord = new StockReceiptHeader();
            var RequestData = (SelectByStockReceiptIDRequest)RequestObj;
            var ResponseData = new SelectByStockReceiptIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.ID != 0)
                {
                    string sSql = "Select * from StockReceiptHeader with(NoLock) where ID='{0}' ";
                    if (RequestData.ID > 0)
                    {
                        sSql = string.Format(sSql, RequestData.ID);
                        _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    }
                    else
                    {
                        long DocumentID = Convert.ToInt64(RequestData.DocumentIDs);
                        sSql = string.Format(sSql, DocumentID);
                        _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    }
                }
                else if (RequestData.DocumentNumber != null && RequestData.DocumentNumber != string.Empty)
                {
                    string sSql = "Select * from StockReceiptHeader with(NoLock) where DocumentNo='{0}' ";
                    sSql = string.Format(sSql, RequestData.DocumentNumber);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                else
                {
                    string sSql = "Select * from StockReceiptHeader with(NoLock) where StockRequestDocumentNo='{0}' ";
                    sSql = string.Format(sSql, RequestData.StockRequestDocumentNo);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReceiptHeader();
                        objStockReceipt.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objStockReceipt.FromWareHouseID = objReader["FromWareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["FromWareHouseID"]) : 0;
                        objStockReceipt.FromWarehouseCode = Convert.ToString(objReader["FromWarehouseCode"]);
                        objStockReceipt.Fromwarehousename = Convert.ToString(objReader["Fromwarehousename"]);
                        objStockReceipt.StockRequestID = objReader["StockRequestID"] != DBNull.Value ? Convert.ToInt32(objReader["StockRequestID"]) : 0;
                        objStockReceipt.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReceipt.TotalReceivedQuantity = objReader["TotalReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalReceivedQuantity"]) : 0;
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockReceipt.DataFrom = Convert.ToString(objReader["DataFrom"]);
                        objStockReceipt.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.Status = Convert.ToString(objReader["Status"]);
                        objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReceipt.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockReceipt.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockReceipt.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockReceipt.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReceipt.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStockReceipt.Type = objReader["Type"] != DBNull.Value ? Convert.ToBoolean(objReader["Type"]) : false;
                        objStockReceipt.Type = objReader["Type"] != DBNull.Value ? Convert.ToBoolean(objReader["Type"]) : false;
                        objStockReceipt.fromApplication = objReader["fromApplication"] != DBNull.Value ? Convert.ToBoolean(objReader["fromApplication"]) : false;
                        objStockReceipt.WithOutBaseDoc = objReader["WithOutBaseDoc"] != DBNull.Value ? Convert.ToBoolean(objReader["WithOutBaseDoc"]) : false;
                        objStockReceipt.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objStockReceipt.StockRequestDocumentNo = objReader["StockRequestDocumentNo"] != DBNull.Value ? Convert.ToString(objReader["StockRequestDocumentNo"]) : "";

                        objStockReceipt.StockRequestDocumentNo = Convert.ToString(objReader["StockRequestDocumentNo"]);
                        objStockReceipt.ReceivedType = objReader["ReceivedType"] != DBNull.Value ? Convert.ToString(objReader["ReceivedType"]) : string.Empty;

                        objStockReceipt.StockReceiptDetailsList = new List<StockReceiptDetails>();   
                        objStockReceipt.RFIDList = new List<TagIdItemDetails>();

                        SelectByStockReceiptDetailsRequest objSelectByStockReceiptDetailsRequest = new SelectByStockReceiptDetailsRequest();
                        SelectByStockReceiptDetailsResponse objSelectByStockReceiptDetailsResponse = new SelectByStockReceiptDetailsResponse();
                        objSelectByStockReceiptDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByStockReceiptDetailsRequest.DocumentNumber = Convert.ToString(objReader["DocumentNo"]);
                        objSelectByStockReceiptDetailsRequest.FromOrToCountryID = 0;
                        objSelectByStockReceiptDetailsRequest.FromOrToStoreCode = Convert.ToString(objReader["StoreCode"]);
                        objSelectByStockReceiptDetailsRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectByStockReceiptDetailsResponse = SelectByStockReceiptDetails(objSelectByStockReceiptDetailsRequest);
                        if (objSelectByStockReceiptDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockReceipt.StockReceiptDetailsList = objSelectByStockReceiptDetailsResponse.StockReceiptDetailsRecord;
                            objStockReceipt.TransactionLogList = objSelectByStockReceiptDetailsResponse.TransactionLogList;
                        }

                        if (objStockReceipt.ReceivedType.Trim().ToUpper() == "RFID")
                        {
                            var objSelectTagIDListRequest = new SelectTagIDListRequest();
                            objSelectTagIDListRequest.DocumentNo = objStockReceipt.DocumentNo;
                            objSelectTagIDListRequest.ConnectionString = RequestData.ConnectionString;
                            var objSelectTagIDListResponse = new SelectTagIDListResponse();
                            objSelectTagIDListResponse = SelectTagIDList(objSelectTagIDListRequest);
                            if (objSelectTagIDListResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objStockReceipt.RFIDList = objSelectTagIDListResponse.RFIDList;
                            }
                        }

                        ResponseData.StockReceiptHeaderRecord = objStockReceipt;
                        ResponseData.ResponseDynamicData = objStockReceipt;
                    }
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockReceiptList = new List<StockReceiptHeader>();
            var RequestData = (SelectAllStockReceiptRequest)RequestObj;
            var ResponseData = new SelectAllStockReceiptResponse();
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
                    sQuery = "Select SRH.*,cm.countryname,sm.storename from StockReceiptHeader SRH left join countrymaster cm on SRH.FromCountryId=cm.id left join storemaster sm on SRH.storeid=sm.id Where SRH.Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SRH.storeid ='" + RequestData.StoreID + "' and SRH.status='Open'";
                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else
                {
                    sQuery = "Select * from StockReceiptHeader ";
                    if (RequestData.FromOrToStoreCode != null && RequestData.FromOrToStoreCode != string.Empty)
                    {
                        sQuery = sQuery + " where StoreCode='" + RequestData.FromOrToStoreCode + "'" + " AND status='Open' ";
                    }
                    else
                    {
                        sQuery = sQuery + " where status='Open' ";
                    }
                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReceiptHeader();
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
                        objStockReceipt.DataFrom = Convert.ToString(objReader["DataFrom"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.Status = Convert.ToString(objReader["Status"]);
                        objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReceipt.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockReceipt.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockReceipt.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockReceipt.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReceipt.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStockReceipt.WithOutBaseDoc = objReader["WithOutBaseDoc"] != DBNull.Value ? Convert.ToBoolean(objReader["WithOutBaseDoc"]) : false;
                        objStockReceipt.Type = objReader["Type"] != DBNull.Value ? Convert.ToBoolean(objReader["Type"]) : false;
                        objStockReceipt.fromApplication = objReader["fromApplication"] != DBNull.Value ? Convert.ToBoolean(objReader["fromApplication"]) : false;

                        //if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                        //{
                        //    objStockReceipt.ReceivedType = Convert.ToString(objReader["ReceivedType"]);
                        //}

                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objStockReceipt.StoreName = Convert.ToString(objReader["StoreName"]);
                        }

                        objStockReceipt.ReceivedType = objReader["ReceivedType"] != DBNull.Value ? Convert.ToString(objReader["ReceivedType"]) : string.Empty;

                        objStockReceipt.StockReceiptDetailsList = new List<StockReceiptDetails>();
                        objStockReceipt.RFIDList = new List<TagIdItemDetails>();

                        SelectByStockReceiptDetailsRequest objSelectByStockReceiptDetailsRequest = new SelectByStockReceiptDetailsRequest();
                        SelectByStockReceiptDetailsResponse objSelectByStockReceiptDetailsResponse = new SelectByStockReceiptDetailsResponse();
                        objSelectByStockReceiptDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByStockReceiptDetailsRequest.DocumentNumber = Convert.ToString(objReader["DocumentNo"]);
                        objSelectByStockReceiptDetailsRequest.FromOrToCountryID = 0;
                        objSelectByStockReceiptDetailsRequest.FromOrToStoreCode = Convert.ToString(objReader["StoreCode"]);
                        objSelectByStockReceiptDetailsRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectByStockReceiptDetailsResponse = SelectByStockReceiptDetails(objSelectByStockReceiptDetailsRequest);
                        if (objSelectByStockReceiptDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockReceipt.StockReceiptDetailsList = objSelectByStockReceiptDetailsResponse.StockReceiptDetailsRecord;                            
                            objStockReceipt.TransactionLogList = new List<TransactionLog>();
                            // No Need for Web Api
                            //objStockReceipt.TransactionLogList = objSelectByStockReceiptDetailsResponse.TransactionLogList;
                        }

                        if (objStockReceipt.ReceivedType.Trim().ToUpper() == "RFID")
                        {
                            var objSelectTagIDListRequest = new SelectTagIDListRequest();
                            objSelectTagIDListRequest.DocumentNo = objStockReceipt.DocumentNo;
                            objSelectTagIDListRequest.ConnectionString = RequestData.ConnectionString;
                            var objSelectTagIDListResponse = new SelectTagIDListResponse();
                            objSelectTagIDListResponse = SelectTagIDList(objSelectTagIDListRequest);
                            if (objSelectTagIDListResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objStockReceipt.RFIDList = objSelectTagIDListResponse.RFIDList;
                            }
                        }

                        StockReceiptList.Add(objStockReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptHeaderList = StockReceiptList;
                    ResponseData.ResponseDynamicData = StockReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt Master");
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

        public override SelectByStockReceiptDetailsResponse SelectByStockReceiptDetails(SelectByStockReceiptDetailsRequest ObjRequest)
        {
            var TransactionLogList = new List<TransactionLog>();
            var StockReceiptDetailMasterList = new List<StockReceiptDetails>();
            var RequestData = (SelectByStockReceiptDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockReceiptDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from StockReceiptDetails ");
                sSql.Append("where  HeaderID=" + RequestData.ID + " ");
                sSql.Append("order by id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceiptDetailMaster = new StockReceiptDetails();
                        objStockReceiptDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReceiptDetailMaster.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;                       
                        objStockReceiptDetailMaster.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;                       
                        objStockReceiptDetailMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);

                        objStockReceiptDetailMaster.SKUName = Convert.ToString(objReader["SKUName"]);
                        objStockReceiptDetailMaster.Brand = Convert.ToString(objReader["Brand"]);
                        objStockReceiptDetailMaster.Color = Convert.ToString(objReader["Color"]);
                        objStockReceiptDetailMaster.Size = Convert.ToString(objReader["Size"]);
                        objStockReceiptDetailMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objStockReceiptDetailMaster.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceiptDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceiptDetailMaster.OldReceivedQuantity = objReader["ReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQuantity"]) : 0;
                        objStockReceiptDetailMaster.TransferQuantity = objReader["TransferQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TransferQuantity"]) : 0;
                        objStockReceiptDetailMaster.RequestQuantity = objReader["RequestQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["RequestQuantity"]) : 0;
                        objStockReceiptDetailMaster.DifferenceQuantity = objReader["DifferenceQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DifferenceQuantity"]) : 0;
                        objStockReceiptDetailMaster.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objStockReceiptDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceiptDetailMaster.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        objStockReceiptDetailMaster.Remarks = Convert.ToString(objReader["Remarks"]);

                        StockReceiptDetailMasterList.Add(objStockReceiptDetailMaster);

                        TransactionLog objTransactionType = new TransactionLog();
                        objTransactionType.ID = 0;
                        objTransactionType.TransactionType = "StockReceipt";
                        objTransactionType.BusinessDate = objStockReceiptDetailMaster.DocumentDate;
                        objTransactionType.ActualDateTime = DateTime.Now;
                        objTransactionType.DocumentID = objStockReceiptDetailMaster.HeaderID;                        
                        objTransactionType.StyleCode = string.Empty;
                        objTransactionType.SKUCode = objStockReceiptDetailMaster.SKUCode;
                        objTransactionType.CountryID = ObjRequest.FromOrToCountryID;
                        objTransactionType.StoreID = objStockReceiptDetailMaster.FromStoreID;
                        objTransactionType.InQty = objStockReceiptDetailMaster.OldReceivedQuantity;
                        objTransactionType.OutQty = 0;
                        objTransactionType.TransactionPrice = 0;
                        objTransactionType.Currency = 0;
                        objTransactionType.ExchangeRate = 0;
                        objTransactionType.DocumentPrice = 0;
                        objTransactionType.UserID = 0;
                        objTransactionType.DocumentNo = Convert.ToString(ObjRequest.DocumentNumber);
                        objTransactionType.StoreID = objStockReceiptDetailMaster.FromStoreID;
                        objTransactionType.StoreCode = Convert.ToString(ObjRequest.FromOrToStoreCode);
                        objTransactionType.CountryID = ObjRequest.FromOrToCountryID;
                        objTransactionType.CountryCode = String.Empty;

                        TransactionLogList.Add(objTransactionType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptDetailsRecord = StockReceiptDetailMasterList;
                    ResponseData.TransactionLogList = TransactionLogList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt");
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

        public override SaveStockReceiptResponse Saveint_stockreceipt(SaveStockReceiptRequest ObjRequest)
        {
            var RequestData = (SaveStockReceiptRequest)ObjRequest;
            var ResponseData = new SaveStockReceiptResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Insertint_stockreceipt", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                //_CommandObj.CommandTimeout = 300000;



                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StockReceiptHeaderRecord.ID;

                SqlParameter StockRequestID = _CommandObj.Parameters.Add("@StockRequestID", SqlDbType.Int);
                StockRequestID.Direction = ParameterDirection.Input;
                StockRequestID.Value = RequestData.StockReceiptHeaderRecord.StockRequestID;

                SqlParameter StockRequestDocumentNo = _CommandObj.Parameters.Add("@StockRequestDocumentNo", SqlDbType.NVarChar);
                StockRequestDocumentNo.Direction = ParameterDirection.Input;
                StockRequestDocumentNo.Value = RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.StockReceiptHeaderRecord.DocumentNo;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.StockReceiptHeaderRecord.Remarks;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.Date);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateTimeString(RequestData.StockReceiptHeaderRecord.DocumentDate);

                SqlParameter TotalQuantity = _CommandObj.Parameters.Add("@TotalQuantity", SqlDbType.Int);
                TotalQuantity.Direction = ParameterDirection.Input;
                TotalQuantity.Value = RequestData.StockReceiptHeaderRecord.TotalQuantity;

                SqlParameter TotalReceivedQuantity = _CommandObj.Parameters.Add("@TotalReceivedQuantity", SqlDbType.Int);
                TotalReceivedQuantity.Direction = ParameterDirection.Input;
                TotalReceivedQuantity.Value = RequestData.StockReceiptHeaderRecord.TotalReceivedQuantity;


                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StockReceiptHeaderRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StockReceiptHeaderRecord.StoreCode;

                SqlParameter Type = _CommandObj.Parameters.Add("@Type", SqlDbType.Bit);
                Type.Direction = ParameterDirection.Input;
                Type.Value = RequestData.StockReceiptHeaderRecord.Type;

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.StockReceiptHeaderRecord.Status;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StockReceiptHeaderRecord.CreateBy;


                SqlParameter int_stockreceiptList = _CommandObj.Parameters.Add("@int_stockreceiptList", SqlDbType.Xml);
                int_stockreceiptList.Direction = ParameterDirection.Input;
                int_stockreceiptList.Value = int_stockreceiptListXML(RequestData.int_stockreceiptList);

                //SqlParameter TLIDs = _CommandObj.Parameters.Add("@TLIDs", SqlDbType.VarChar, 10);
                //TLIDs.Direction = ParameterDirection.Output;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "StockReceipt");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReceipt");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string int_stockreceiptListXML(List<int_stockreceipt> int_stockreceiptList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockreceipt objTransactionLogDetailMasterDetails in int_stockreceiptList)
            {
                sSql.Append("<TransactionLogDetailsData>");

                sSql.Append("<ID>" + objTransactionLogDetailMasterDetails.ID + "</ID>");

                sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocDate) + "</DocDate>");
                //sSql.Append("<DelDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DelDate) + "</DelDate>"); 
                // sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                sSql.Append("<SKUCode>" + (objTransactionLogDetailMasterDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<SKUName>" + (objTransactionLogDetailMasterDetails.SKUName) + "</SKUName>");
                sSql.Append("<Brand>" + (objTransactionLogDetailMasterDetails.Brand) + "</Brand>");
                sSql.Append("<Color>" + (objTransactionLogDetailMasterDetails.Color) + "</Color>");
                sSql.Append("<Size>" + (objTransactionLogDetailMasterDetails.Size) + "</Size>");
                sSql.Append("<FromStoreID>" + (objTransactionLogDetailMasterDetails.FromStoreID) + "</FromStoreID>");
                sSql.Append("<RequestedQty>" + objTransactionLogDetailMasterDetails.RequestQuantity + "</RequestedQty>");
                sSql.Append("<ReceivedQuantity>" + objTransactionLogDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                sSql.Append("<BarCode>" + objTransactionLogDetailMasterDetails.BarCode + "</BarCode>");
                sSql.Append("<TranasferedQty>" + objTransactionLogDetailMasterDetails.TransferQuantity + "</TranasferedQty>");
                sSql.Append("<DifferenceQuantity>" + objTransactionLogDetailMasterDetails.DifferenceQuantity + "</DifferenceQuantity>");
                //sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");               
                sSql.Append("<Remarks>" + (objTransactionLogDetailMasterDetails.Remarks) + "</Remarks>");

                sSql.Append("</TransactionLogDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }


        public override SaveStockReceiptResponse Update_ConfirmTransfer(SaveStockReceiptRequest ObjRequest)
        {
            var RequestData = (SaveStockReceiptRequest)ObjRequest;

            var ResponseData = new SaveStockReceiptResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    // _RequestFrom = RequestData.RequestFrom;

                    _CommandObj = new SqlCommand("UpdateStockreceiptandConfirmTransfer", cnn);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    //_CommandObj.CommandTimeout = 300000;

                    SqlParameter StockRequestDocumentNo = _CommandObj.Parameters.Add("@StockRequestDocumentNo", SqlDbType.NVarChar);
                    StockRequestDocumentNo.Direction = ParameterDirection.Input;
                    StockRequestDocumentNo.Value = RequestData.StockReceiptHeaderRecord.StockRequestDocumentNo;

                    SqlParameter int_stockreceiptList = _CommandObj.Parameters.Add("@TransactionLogDetailsData", SqlDbType.Xml);
                    int_stockreceiptList.Direction = ParameterDirection.Input;
                    int_stockreceiptList.Value = InsertConfirmTransfer(RequestData.int_stockreceiptList);

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
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Transfer Confirm");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.WMSIDs = ID2.Value.ToString();
                        ResponseData.IDs = STKIDS;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Transfer Confirm");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit database not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            //finally
            //{
            //    sqlCommon1.CloseConnection(cnn);
            //    sqlCommon.CloseConnection(_ConnectionObj);
            //}

            return ResponseData;
        }
        public string InsertConfirmTransfer(List<int_stockreceipt> int_stockreceiptList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockreceipt objTransactionLogDetailMasterDetails in int_stockreceiptList)
            {
                if (objTransactionLogDetailMasterDetails.ReceivedQuantity != 0)
                {
                    sSql.Append("<TransactionLogDetailsData>");
                    sSql.Append("<DocNum>" + objTransactionLogDetailMasterDetails.DocNum + "</DocNum>");
                    sSql.Append("<BaseDocNum>" + objTransactionLogDetailMasterDetails.BasDocNum + "</BaseDocNum>");
                    sSql.Append("<WMSReqKey>" + objTransactionLogDetailMasterDetails.WMSReqKey + "</WMSReqKey>");
                    sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocDate) + "</DocDate>");
                    sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");
                    sSql.Append("<ToLocation>" + objTransactionLogDetailMasterDetails.ToLocation + "</ToLocation>");
                    sSql.Append("<StkRecDocNum>" + objTransactionLogDetailMasterDetails.StkRecDocNum + "</StkRecDocNum>");

                    sSql.Append("<SKUCode>" + (objTransactionLogDetailMasterDetails.SKUCode) + "</SKUCode>");
                    sSql.Append("<SKUName>" + (objTransactionLogDetailMasterDetails.SKUName) + "</SKUName>");
                    sSql.Append("<BarCode>" + objTransactionLogDetailMasterDetails.BarCode + "</BarCode>");
                    sSql.Append("<RequestQuantity>" + objTransactionLogDetailMasterDetails.RequestQuantity + "</RequestQuantity>");
                    sSql.Append("<TransferQuantity>" + objTransactionLogDetailMasterDetails.TransferQuantity + "</TransferQuantity>");
                    sSql.Append("<ReceivedQuantity>" + objTransactionLogDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                    sSql.Append("<Remarks>" + (objTransactionLogDetailMasterDetails.Remarks) + "</Remarks>");

                    sSql.Append("</TransactionLogDetailsData>");
                }
               
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override SaveStockReceiptResponse saveintStockReceipt(SaveStockReceiptRequest ObjRequest)
        {
             var RequestData = (SaveStockReceiptRequest)ObjRequest;

            var ResponseData = new SaveStockReceiptResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _CommandObj = new SqlCommand("Insertint_stockDetails", cnn);
                    _CommandObj.CommandType = CommandType.StoredProcedure; 

                    SqlParameter int_stockreceiptList = _CommandObj.Parameters.Add("@TransactionLogDetailsData", SqlDbType.Xml);
                    int_stockreceiptList.Direction = ParameterDirection.Input;
                    int_stockreceiptList.Value = InsertStockreceipt(RequestData.int_stockreceiptList);

                    SqlParameter Int_ConfirmTransferList = _CommandObj.Parameters.Add("@Int_ConfirmTransfer", SqlDbType.Xml);
                    Int_ConfirmTransferList.Direction = ParameterDirection.Input;
                    Int_ConfirmTransferList.Value = Int_ConfirmTransfer(RequestData.int_stockreceiptList);

                    SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar, int.MaxValue);
                    DocumentNo.Direction = ParameterDirection.Output;

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
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "TransactionLog");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = TLIDs.Value.ToString();
                        ResponseData.DocumentNo = DocumentNo.Value.ToString();

                        UpdateStockReceiptDetailsFlag(ResponseData.DocumentNo);

                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit database not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            //finally
            //{
            //    sqlCommon1.CloseConnection(cnn);
            //    sqlCommon.CloseConnection(_ConnectionObj);
            //}

            return ResponseData;
        }

        private void UpdateStockReceiptDetailsFlag(string DocumentNo)
        {

            MsSqlCommon sqlCommon = new MsSqlCommon();
            SqlDataReader objReader;
            try
            {

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "update StockReceiptDetails set IsFlaged=1 where DocumentNo in ('" + DocumentNo + "')";
                sSql = string.Format(sSql);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                sqlCommon.CloseConnection(cnn);
            }
        }

        public string InsertStockreceipt(List<int_stockreceipt> int_stockreceiptList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockreceipt objTransactionLogDetailMasterDetails in int_stockreceiptList)
            {
                sSql.Append("<TransactionLogDetailsData>");
                sSql.Append("<DocNum>" + objTransactionLogDetailMasterDetails.DocNum + "</DocNum>");
                sSql.Append("<BaseDocNum>" + objTransactionLogDetailMasterDetails.BasDocNum + "</BaseDocNum>");
                sSql.Append("<WMSReqKey>" + objTransactionLogDetailMasterDetails.WMSReqKey + "</WMSReqKey>");               
                sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocDate) + "</DocDate>");
                sSql.Append("<DelDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DelDate) + "</DelDate>");
                sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");
                sSql.Append("<ToLocation>" + objTransactionLogDetailMasterDetails.ToLocation + "</ToLocation>");
               
                sSql.Append("<SKUCode>" + (objTransactionLogDetailMasterDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<SKUName>" + (objTransactionLogDetailMasterDetails.SKUName) + "</SKUName>");
                sSql.Append("<BarCode>" + objTransactionLogDetailMasterDetails.BarCode + "</BarCode>");       
                sSql.Append("<RequestQuantity>" + objTransactionLogDetailMasterDetails.RequestQuantity + "</RequestQuantity>");
                sSql.Append("<ReceivedQuantity>" + objTransactionLogDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                sSql.Append("<Remarks>" + (objTransactionLogDetailMasterDetails.Remarks) + "</Remarks>");
             
                //sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");               
                

                sSql.Append("</TransactionLogDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }
        public string Int_ConfirmTransfer(List<int_stockreceipt> int_stockreceiptList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockreceipt objTransactionLogDetailMasterDetails in int_stockreceiptList)
            {
                if (objTransactionLogDetailMasterDetails.ReceivedQuantity != 0)
                {
                    sSql.Append("<Int_ConfirmTransferData>");
                    sSql.Append("<DocNum>" + objTransactionLogDetailMasterDetails.DocNum + "</DocNum>");
                    sSql.Append("<BaseDocNum>" + objTransactionLogDetailMasterDetails.BasDocNum + "</BaseDocNum>");
                    sSql.Append("<WMSReqKey>" + objTransactionLogDetailMasterDetails.WMSReqKey + "</WMSReqKey>");             
                    sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocDate) + "</DocDate>");                  
                    sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");
                    sSql.Append("<ToLocation>" + objTransactionLogDetailMasterDetails.ToLocation + "</ToLocation>");
                    sSql.Append("<StkRecDocNum>" + objTransactionLogDetailMasterDetails.StkRecDocNum + "</StkRecDocNum>");

                    sSql.Append("<SKUCode>" + (objTransactionLogDetailMasterDetails.SKUCode) + "</SKUCode>");
                    sSql.Append("<SKUName>" + (objTransactionLogDetailMasterDetails.SKUName) + "</SKUName>");
                    sSql.Append("<BarCode>" + objTransactionLogDetailMasterDetails.BarCode + "</BarCode>");
                    sSql.Append("<RequestQuantity>" + objTransactionLogDetailMasterDetails.RequestQuantity + "</RequestQuantity>");
                    sSql.Append("<TransferQuantity>" + objTransactionLogDetailMasterDetails.TransferQuantity + "</TransferQuantity>");
                    sSql.Append("<ReceivedQuantity>" + objTransactionLogDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                    sSql.Append("<Remarks>" + (objTransactionLogDetailMasterDetails.Remarks) + "</Remarks>");

                    //sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");               


                    sSql.Append("</Int_ConfirmTransferData>");
                }

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override SelectByStockReceiptDetailsResponse SelectByStockReceiptTransactionDetails(SelectByStockReceiptDetailsRequest ObjRequest)
        {
            var StockReceiptHeaderList = new List<StockReceiptHeader>();
            var RequestData = (SelectByStockReceiptDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockReceiptDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from StockReceiptHeader SRH inner join  TransactionLog TL on SRH.DocumentNo=TL.DocumentNo ");
                sSql.Append("where  tl.DocumentNo='" + RequestData.DocumentNumber + "' ");               
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReceiptHeader();
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
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.Status = Convert.ToString(objReader["Status"]);
                        objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReceipt.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockReceipt.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockReceipt.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockReceipt.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReceipt.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStockReceipt.WithOutBaseDoc = objReader["WithOutBaseDoc"] != DBNull.Value ? Convert.ToBoolean(objReader["WithOutBaseDoc"]) : false;
                        objStockReceipt.Type = objReader["Type"] != DBNull.Value ? Convert.ToBoolean(objReader["Type"]) : false;
                        objStockReceipt.fromApplication = objReader["fromApplication"] != DBNull.Value ? Convert.ToBoolean(objReader["fromApplication"]) : false;
                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objStockReceipt.StoreName = Convert.ToString(objReader["StoreName"]);
                        }
                        StockReceiptHeaderList.Add(objStockReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptHeaderList = StockReceiptHeaderList;
                    ResponseData.ResponseDynamicData = StockReceiptHeaderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt Master");
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

        public override SelectAllStockReceiptResponse SelectAllStockReceiptWms(SelectAllStockReceiptRequest ObjRequest)
        {
            List<string> DocNoList = new List<string>();
            var int_stockreceiptList = new List<int_stockreceipt>();
            var RequestData = (SelectAllStockReceiptRequest)ObjRequest;
            var ResponseData = new SelectAllStockReceiptResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];

                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _RequestFrom = RequestData.RequestFrom;

                    string sSql = "Select * from int_stockreceipt where  isnull(flag,0)=0 and ToLocation='" + RequestData.StoreCode + "'";
                    sSql = string.Format(sSql);

                    _CommandObj = new SqlCommand(sSql, cnn);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInt_ConfirmTransfer = new int_stockreceipt();
                            objInt_ConfirmTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objInt_ConfirmTransfer.DocNum = Convert.ToString(objReader["DocNum"]);
                            objInt_ConfirmTransfer.DocDate = objReader["DocDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocDate"]) : DateTime.Now;
                            objInt_ConfirmTransfer.DelDate = objReader["DelDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DelDate"]) : DateTime.Now;
                            objInt_ConfirmTransfer.LineId = objReader["LineId"] != DBNull.Value ? Convert.ToInt32(objReader["LineId"]) : 0;
                            objInt_ConfirmTransfer.FromLocation = Convert.ToString(objReader["FromLocation"]);
                            objInt_ConfirmTransfer.ToLocation = Convert.ToString(objReader["ToLocation"]);
                            objInt_ConfirmTransfer.StoreCode = Convert.ToString(objReader["ToLocation"]);
                            objInt_ConfirmTransfer.SKUCode = Convert.ToString(objReader["ItemCode"]);
                            objInt_ConfirmTransfer.SKUName = Convert.ToString(objReader["ItemName"]);
                            objInt_ConfirmTransfer.BarCode = Convert.ToString(objReader["BarCode"]);
                            objInt_ConfirmTransfer.RequestQuantity = objReader["ReqQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReqQty"]) : 0;
                            objInt_ConfirmTransfer.TransferQuantity = objReader["TfrdQty"] != DBNull.Value ? Convert.ToInt32(objReader["TfrdQty"]) : 0;
                            objInt_ConfirmTransfer.Remarks = Convert.ToString(objReader["Rermarks"]);
                            objInt_ConfirmTransfer.Basedocument = Convert.ToString(objReader["Basedocument"]);
                            objInt_ConfirmTransfer.BaseDocKey = Convert.ToString(objReader["BaseDocKey"]);
                            objInt_ConfirmTransfer.BasDocNum = Convert.ToString(objReader["BasDocNum"]);
                            objInt_ConfirmTransfer.WMSReqKey = objReader["WMSReqKey"] != DBNull.Value ? Convert.ToInt32(objReader["WMSReqKey"]) : 0;
                            objInt_ConfirmTransfer.Flag = objReader["Flag"] != DBNull.Value ? Convert.ToBoolean(objReader["Flag"]) : false;
                            objInt_ConfirmTransfer.DocType = Convert.ToString(objReader["DocType"]);

                            int_stockreceiptList.Add(objInt_ConfirmTransfer);

                            if (objInt_ConfirmTransfer.DocType.Trim().ToUpper() == "RFID")
                            {
                                DocNoList.Add(objInt_ConfirmTransfer.DocNum.Trim().ToUpper());
                            }
                        }                       

                        string StrDocNumbers = string.Empty;
                        string DocNumbers = string.Empty;
                        var DistinctDocNoList = DocNoList.Distinct();
                        if (DistinctDocNoList != null)
                        {
                            foreach (string str in DistinctDocNoList)
                            {
                                StrDocNumbers =  StrDocNumbers + ",'" + str + "'";
                            }
                            if (StrDocNumbers.Length > 0)
                            {
                                DocNumbers = StrDocNumbers.Remove(0,1);
                            }
                        }
                        if (DocNumbers != string.Empty)
                        {
                            ResponseData.RFIDTagList = TagList(DocNumbers);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.int_stockreceiptRecord = int_stockreceiptList;
                        ResponseData.ResponseDynamicData = int_stockreceiptList;

                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = "No Stock Receipt from CentralUnit Database !.";
                    }
                    
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit database not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ResponseData;
        }
        public List<TagIdItemDetails> TagList(string DocNumbers)
        {
            List<TagIdItemDetails> _TagList = new List<TagIdItemDetails>();          
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];

                cnn = new SqlConnection(connetionString);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                string sSql = "Select distinct t1.*,t2.DocDate from Int_StockReceipt_TagId  t1 with(nolock) join Int_StockReceipt t2 ";
                sSql = sSql + "with(nolock) on t1.DocNum=t2.DocNum where t1.DocNum in('"+ DocNumbers + "') and isnull(t2.flag,0)=0 ";
              

                _CommandObj = new SqlCommand(sSql, cnn);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var _TagIdItemDetails = new TagIdItemDetails();
                        _TagIdItemDetails.DocumentNo = objReader["DocNum"] != DBNull.Value ? Convert.ToString(objReader["DocNum"]) : string.Empty;
                        _TagIdItemDetails.DocumentDate = objReader["DocDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocDate"]) : DateTime.Now;
                        _TagIdItemDetails.Itemcode = objReader["Itemcode"] != DBNull.Value ? Convert.ToString(objReader["Itemcode"]) : string.Empty;
                        _TagIdItemDetails.Tag_ID = objReader["Tag_id"] != DBNull.Value ? Convert.ToString(objReader["Tag_id"]) : string.Empty;
                        _TagIdItemDetails.ItemStatus = "In"; //Hard Coded for First time insert from WMS to Store_pos DB.ItemStatus not inserted in first time
                        _TagList.Add(_TagIdItemDetails);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return _TagList;
        }


        public override SaveStockReceiptResponse SaveStockReceiptlistWms(SaveStockReceiptRequest ObjRequest)
        {
            var RequestData = (SaveStockReceiptRequest)ObjRequest;

            var ResponseData = new SaveStockReceiptResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
           // string connetionString;
            try
            {

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;             

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Insertstock_receiptDetailslistwms", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter int_stockreceiptList = _CommandObj.Parameters.Add("@StockReceiptHeaderListWms", SqlDbType.Xml);
                int_stockreceiptList.Direction = ParameterDirection.Input;
                int_stockreceiptList.Value = InsertStockReceiptHeaderListWms(RequestData.StockReceiptHeaderListWms);

                SqlParameter Int_ConfirmTransferList = _CommandObj.Parameters.Add("@StockReceiptDetailsListWms", SqlDbType.Xml);
                Int_ConfirmTransferList.Direction = ParameterDirection.Input;
                Int_ConfirmTransferList.Value = InsertStockReceiptDetailsListWms(RequestData.StockReceiptDetailsListWms);

                SqlParameter RFIDList = _CommandObj.Parameters.Add("@RFIDList", SqlDbType.Xml);
                RFIDList.Direction = ParameterDirection.Input;
                if (RequestData.RFIDTagList != null && RequestData.RFIDTagList.Count > 0)
                {
                    RFIDList.Value = RFIDTagXml(RequestData.RFIDTagList);
                }
                else
                {
                    RFIDList.Value = DBNull.Value;
                }

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar,int.MaxValue);
                DocumentNo.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "TransactionLog");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DocumentNo = DocumentNo.Value.ToString();
            

                  UpdateIntStockReceiptWms(ResponseData.DocumentNo , RequestData.StoreCode);
                  UpdateIntStockReceiptHeader(ResponseData.DocumentNo);

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        private void UpdateIntStockReceiptHeader(string DocumentNo)
        {

            MsSqlCommon sqlCommon = new MsSqlCommon();
            SqlDataReader objReader;
            try
            {

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "update StockReceiptHeader set IsFlaged=1 where DocumentNo in (" + DocumentNo + ")";
                sSql = string.Format(sSql);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                sqlCommon.CloseConnection(cnn);
            }
        }
        private void UpdateIntStockReceiptWms(string DocumentNo, string StoreCode)
        {
            MsSqlCommon sqlCommon1 = new MsSqlCommon();
            string connetionString;
            SqlDataReader objReader;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    string sSql = "update Int_StockReceipt set Flag=1 where ToLocation='" + StoreCode + "' and DocNum in ('" + DocumentNo + "')";
                    sSql = string.Format(sSql);

                    _CommandObj = new SqlCommand(sSql, cnn);
                    _CommandObj.CommandType = CommandType.Text;
                    _CommandObj.ExecuteNonQuery();
                    cnn.Close();
                }
                else
                {
                    throw new Exception("CentralUnit databse not coneected !.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
  

        public string InsertStockReceiptHeaderListWms(List<StockReceiptHeader> StockReceiptHeaderListWms)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StockReceiptHeader objTransactionLogDetailMasterDetails in StockReceiptHeaderListWms)
            {
                sSql.Append("<StockReceiptHeaderListWms>");
                sSql.Append("<StockRequestDocumentNo>" + objTransactionLogDetailMasterDetails.StockRequestDocumentNo + "</StockRequestDocumentNo>");
                sSql.Append("<StockRequestID>" + objTransactionLogDetailMasterDetails.StockRequestID + "</StockRequestID>");
                sSql.Append("<DocumentNo>" + objTransactionLogDetailMasterDetails.DocumentNo + "</DocumentNo>");
                sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                sSql.Append("<Status>" + objTransactionLogDetailMasterDetails.Status + "</Status>");
                sSql.Append("<Active>" + objTransactionLogDetailMasterDetails.Active + "</Active>");
                sSql.Append("<CreateBy>" + objTransactionLogDetailMasterDetails.CreateBy + "</CreateBy>");

                sSql.Append("<CreateOn>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.CreateOn) + "</CreateOn>");
                sSql.Append("<UpdateBy>" + (objTransactionLogDetailMasterDetails.UpdateBy) + "</UpdateBy>");
                sSql.Append("<UpdateOn>" +sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.UpdateOn) + "</UpdateOn>");
                sSql.Append("<SCN>" + objTransactionLogDetailMasterDetails.SCN + "</SCN>");
                sSql.Append("<TotalReceivedQuantity>" + objTransactionLogDetailMasterDetails.TotalReceivedQuantity + "</TotalReceivedQuantity>");
                sSql.Append("<StoreID>" + (objTransactionLogDetailMasterDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objTransactionLogDetailMasterDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<WithOutBaseDoc>" + objTransactionLogDetailMasterDetails.WithOutBaseDoc + "</WithOutBaseDoc>");
                sSql.Append("<DataFrom>" + (objTransactionLogDetailMasterDetails.DataFrom) + "</DataFrom>");
                sSql.Append("<IsFlaged>" + (objTransactionLogDetailMasterDetails.IsFlaged) + "</IsFlaged>");
                sSql.Append("<ReceivedType>" + (objTransactionLogDetailMasterDetails.ReceivedType) + "</ReceivedType>");
                //sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");              


                sSql.Append("</StockReceiptHeaderListWms>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public string InsertStockReceiptDetailsListWms(List<StockReceiptDetails> StockReceiptDetailsListWms)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StockReceiptDetails objTransactionLogDetailMasterDetails in StockReceiptDetailsListWms)
            {
                sSql.Append("<StockReceiptDetailsListWms>");
                sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                sSql.Append("<SKUID>" + objTransactionLogDetailMasterDetails.SKUID + "</SKUID>");
                sSql.Append("<SKUCode>" + objTransactionLogDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<SKUName>" + objTransactionLogDetailMasterDetails.SKUName + "</SKUName>");
                sSql.Append("<Brand>" + objTransactionLogDetailMasterDetails.Brand + "</Brand>");
                sSql.Append("<Color>" + objTransactionLogDetailMasterDetails.Color + "</Color>");
                sSql.Append("<Size>" + objTransactionLogDetailMasterDetails.Size + "</Size>");

                sSql.Append("<FromStoreID>" + (objTransactionLogDetailMasterDetails.FromStoreID) + "</FromStoreID>");
                sSql.Append("<RequestQuantity>" + (objTransactionLogDetailMasterDetails.RequestQuantity) + "</RequestQuantity>");
                sSql.Append("<TransferQuantity>" + objTransactionLogDetailMasterDetails.TransferQuantity + "</TransferQuantity>");
                sSql.Append("<ReceivedQuantity>" + objTransactionLogDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                sSql.Append("<DifferenceQuantity>" + objTransactionLogDetailMasterDetails.DifferenceQuantity + "</DifferenceQuantity>");
                sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                sSql.Append("<BarCode>" + (objTransactionLogDetailMasterDetails.BarCode) + "</BarCode>");
                sSql.Append("<DocumentNo>" + (objTransactionLogDetailMasterDetails.DocumentNo) + "</DocumentNo>");
                sSql.Append("<IsFlaged>" + (objTransactionLogDetailMasterDetails.IsFlaged) + "</IsFlaged>");
                //sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");               


                sSql.Append("</StockReceiptDetailsListWms>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }


        public override SelectAllStockReceiptResponse SelectAllStockReceiptWmsFlagCheck(SelectAllStockReceiptRequest RequestObj)
        {
            var StockReceiptList = new List<StockReceiptHeader>();
            var RequestData = (SelectAllStockReceiptRequest)RequestObj;
            var ResponseData = new SelectAllStockReceiptResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);            
                    sQuery = "Select DocumentNo from StockReceiptHeader where isnull(IsFlaged,0)=0 ";
                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReceiptHeader();

                       
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                                          
                        StockReceiptList.Add(objStockReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptHeaderListwmsFlag = StockReceiptList;
                    ResponseData.ResponseDynamicData = StockReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt Master");
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

        public override SaveStockReceiptResponse SaveStockReceiptlistWmsFlagCheck(SaveStockReceiptRequest ObjRequest)
        {
            var RequestData = (SaveStockReceiptRequest)ObjRequest;

            var ResponseData = new SaveStockReceiptResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
               
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _CommandObj = new SqlCommand("UpdateIntStockReceiptFlag", cnn);
                    _CommandObj.CommandType = CommandType.StoredProcedure;


                    SqlParameter int_stockreceiptList = _CommandObj.Parameters.Add("@StockReceiptHeaderListWms", SqlDbType.Xml);
                    int_stockreceiptList.Direction = ParameterDirection.Input;
                    int_stockreceiptList.Value = StockReceiptHeaderListFlagCheck(RequestData.StockReceiptHeaderListWmsFlagCheck);


                    SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar, int.MaxValue);
                    DocumentNo.Direction = ParameterDirection.Output;

                    SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                    StatusCode.Direction = ParameterDirection.Output;

                    SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                    StatusMsg.Direction = ParameterDirection.Output;

                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();
                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "TransactionLog");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.DocumentNo = DocumentNo.Value.ToString();


                        UpdateIntStockReceiptHeaderFlag(ResponseData.DocumentNo);

                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit database not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            //finally
            //{
            //    sqlCommon.CloseConnection(_ConnectionObj);
            //}

            return ResponseData;
        }


        private void UpdateIntStockReceiptHeaderFlag(string DocumentNo)
        {
            
            MsSqlCommon sqlCommon = new MsSqlCommon();         
            SqlDataReader objReader;
            try
            {
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "update StockReceiptHeader set IsFlaged=1 where DocumentNo in (" + DocumentNo + ")";
                sSql = string.Format(sSql);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }
        public string StockReceiptHeaderListFlagCheck(List<StockReceiptHeader> StockReceiptHeaderListWmsFlagCheck)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StockReceiptHeader objStockReceiptDetailMasterDetails in StockReceiptHeaderListWmsFlagCheck)
            {
                sSql.Append("<StockReceiptHeadListFlagCheckData>");
                sSql.Append("<DocumentNo>" + objStockReceiptDetailMasterDetails.DocumentNo + "</DocumentNo>");
                //sSql.Append("<HeaderID>" + objStockReceiptDetailMasterDetails.HeaderID + "</HeaderID>");
                //sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString(objStockReceiptDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                //sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objStockReceiptDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                ////sSql.Append("<ReasonID>" + objStockReceiptDetailMasterDetails.ReasonID + "</ReasonID>");
                //sSql.Append("<SKUID>" + (objStockReceiptDetailMasterDetails.SKUID) + "</SKUID>");
                //sSql.Append("<SKUName>" + (objStockReceiptDetailMasterDetails.SKUName) + "</SKUName>");
                //sSql.Append("<SKUCode>" + objStockReceiptDetailMasterDetails.SKUCode + "</SKUCode>");
                //sSql.Append("<Brand>" + (objStockReceiptDetailMasterDetails.Brand) + "</Brand>");
                //sSql.Append("<Color>" + (objStockReceiptDetailMasterDetails.Color) + "</Color>");
                //sSql.Append("<Size>" + objStockReceiptDetailMasterDetails.Size + "</Size>");
                //sSql.Append("<BarCode>" + objStockReceiptDetailMasterDetails.BarCode + "</BarCode>");
                //sSql.Append("<FromStoreID>" + objStockReceiptDetailMasterDetails.FromStoreID + "</FromStoreID>");
                //sSql.Append("<RequestQuantity>" + objStockReceiptDetailMasterDetails.RequestQuantity + "</RequestQuantity>");
                //sSql.Append("<TransferQuantity>" + objStockReceiptDetailMasterDetails.TransferQuantity + "</TransferQuantity>");
                //sSql.Append("<ReceivedQuantity>" + objStockReceiptDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                //sSql.Append("<DifferenceQuantity>" + objStockReceiptDetailMasterDetails.DifferenceQuantity + "</DifferenceQuantity>");
                //sSql.Append("<Remarks>" + (objStockReceiptDetailMasterDetails.Remarks) + "</Remarks>");
                sSql.Append("</StockReceiptHeadListFlagCheckData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
        }

        public override SelectAllStockReceiptDetailsResponse SelectAllStockReceiptDetailsForFlaglist(SelectAllStockReceiptDetailsRequest RequestObj)
        {
            var StockReceiptDetailsList = new List<StockReceiptDetails>();
            var RequestData = (SelectAllStockReceiptDetailsRequest)RequestObj;
            var ResponseData = new SelectAllStockReceiptDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select srd.* from StockReceiptDetails srd join StockReceiptHeader sh on srd.HeaderID=sh.ID where srd.IsFlaged=0 and sh.Status='Closed' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceiptDetails = new StockReceiptDetails();


                        objStockReceiptDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockReceiptDetails.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objStockReceiptDetails.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        objStockReceiptDetails.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objStockReceiptDetails.SKUName = Convert.ToString(objReader["SKUName"]);
                        objStockReceiptDetails.Brand = Convert.ToString(objReader["Brand"]);
                        objStockReceiptDetails.Color = Convert.ToString(objReader["Color"]);
                        objStockReceiptDetails.Size = Convert.ToString(objReader["Size"]);
                        objStockReceiptDetails.BarCode = Convert.ToString(objReader["BarCode"]);
                        objStockReceiptDetails.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceiptDetails.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceiptDetails.ReceivedQuantity = objReader["ReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQuantity"]) : 0;
                        objStockReceiptDetails.TransferQuantity = objReader["TransferQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TransferQuantity"]) : 0;
                        objStockReceiptDetails.RequestQuantity = objReader["RequestQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["RequestQuantity"]) : 0;
                        objStockReceiptDetails.DifferenceQuantity = objReader["DifferenceQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DifferenceQuantity"]) : 0;
                        objStockReceiptDetails.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objStockReceiptDetails.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceiptDetails.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        objStockReceiptDetails.Remarks = Convert.ToString(objReader["Remarks"]);                       
                        StockReceiptDetailsList.Add(objStockReceiptDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockReceiptDetailswmsFlag = StockReceiptDetailsList;
                    ResponseData.ResponseDynamicData = StockReceiptDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt Master");
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

        public override SaveStockReceiptListWmsConfirmTransferResponse SaveStockReceiptListWmsConfirmTransfer(SaveStockReceiptListWmsConfirmTransferRequest objRequest)
        {
           var RequestData = (SaveStockReceiptListWmsConfirmTransferRequest)objRequest;

            var ResponseData = new SaveStockReceiptListWmsConfirmTransferResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];

                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _CommandObj = new SqlCommand("[InserteStockreceiptandConfirmTransferupdateflag]", cnn);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    //_CommandObj.CommandTimeout = 300000;

                    SqlParameter Int_ConfirmTransferList = _CommandObj.Parameters.Add("@Int_ConfirmTransfer", SqlDbType.Xml);
                    Int_ConfirmTransferList.Direction = ParameterDirection.Input;
                    Int_ConfirmTransferList.Value = Int_ConfirmTransferFlag(RequestData.int_stockreceiptConfirmTransfer);

                    SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar, 500);
                    DocumentNo.Direction = ParameterDirection.Output;

                    SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                    StatusCode.Direction = ParameterDirection.Output;

                    SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                    StatusMsg.Direction = ParameterDirection.Output;

                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();
                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "TransactionLog");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.DocumentNo = DocumentNo.Value.ToString();

                        UpdateStockReceiptDetailsFlag(ResponseData.DocumentNo);

                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit database not connected !.";
                }

            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            return ResponseData;
        }
        public string Int_ConfirmTransferFlag(List<int_stockreceipt> int_stockreceiptConfirmTransfer)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockreceipt objTransactionLogDetailMasterDetails in int_stockreceiptConfirmTransfer)
            {
                if (objTransactionLogDetailMasterDetails.ReceivedQuantity != 0)
                {
                    sSql.Append("<Int_ConfirmTransferData>");
                    sSql.Append("<DocNum>" + objTransactionLogDetailMasterDetails.DocNum + "</DocNum>");                
                  
                    sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.DocDate) + "</DocDate>");
                    sSql.Append("<FromLocation>" + objTransactionLogDetailMasterDetails.FromLocation + "</FromLocation>");
                    sSql.Append("<ToLocation>" + objTransactionLogDetailMasterDetails.ToLocation + "</ToLocation>");                
                    sSql.Append("<SKUCode>" + (objTransactionLogDetailMasterDetails.SKUCode) + "</SKUCode>");
                    sSql.Append("<SKUName>" + (objTransactionLogDetailMasterDetails.SKUName) + "</SKUName>");
                    sSql.Append("<BarCode>" + objTransactionLogDetailMasterDetails.BarCode + "</BarCode>");
                    sSql.Append("<RequestQuantity>" + objTransactionLogDetailMasterDetails.RequestQuantity + "</RequestQuantity>");                
                    sSql.Append("<ReceivedQuantity>" + objTransactionLogDetailMasterDetails.ReceivedQuantity + "</ReceivedQuantity>");
                   
                    sSql.Append("</Int_ConfirmTransferData>");
                }

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

        public override SelectTagIDListResponse SelectTagIDList(SelectTagIDListRequest ObjRequest)
        {
            var TagIdItemDetailsList = new List<TagIdItemDetails>();
            var RequestData = (SelectTagIDListRequest)ObjRequest;
            var ResponseData = new SelectTagIDListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select * from TAGID_ITEM_DETAILS Where DocumentNo='" + RequestData.DocumentNo + "'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTagIdItemDetails = new TagIdItemDetails();
                        objTagIdItemDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTagIdItemDetails.Tag_ID = objReader["Tag_ID"] != DBNull.Value ? Convert.ToString(objReader["Tag_ID"]) : string.Empty;
                        objTagIdItemDetails.Itemcode = objReader["Itemcode"] != DBNull.Value ? Convert.ToString(objReader["Itemcode"]) : string.Empty;
                        objTagIdItemDetails.Description = objReader["Description"] != DBNull.Value ? Convert.ToString(objReader["Description"]) : string.Empty;
                        objTagIdItemDetails.Supplier_barcode = objReader["Supplier_barcode"] != DBNull.Value ? Convert.ToString(objReader["Supplier_barcode"]) : string.Empty;
                        objTagIdItemDetails.Orjwan_Barcode = objReader["Orjwan_Barcode"] != DBNull.Value ? Convert.ToString(objReader["Orjwan_Barcode"]) : string.Empty;
                        objTagIdItemDetails.Brand_code = objReader["Brand_code"] != DBNull.Value ? Convert.ToString(objReader["Brand_code"]) : string.Empty;
                        objTagIdItemDetails.Design_code = objReader["Design_code"] != DBNull.Value ? Convert.ToString(objReader["Design_code"]) : string.Empty;
                        objTagIdItemDetails.Department_code = objReader["Department_code"] != DBNull.Value ? Convert.ToString(objReader["Department_code"]) : string.Empty;
                        objTagIdItemDetails.Product_code = objReader["Product_code"] != DBNull.Value ? Convert.ToString(objReader["Product_code"]) : string.Empty;                       
                        objTagIdItemDetails.Color_code = objReader["Color_code"] != DBNull.Value ? Convert.ToString(objReader["Color_code"]) : string.Empty;
                        objTagIdItemDetails.Size_code = objReader["Size_code"] != DBNull.Value ? Convert.ToString(objReader["Size_code"]) : string.Empty;
                        objTagIdItemDetails.Color_Name = objReader["Color_Name"] != DBNull.Value ? Convert.ToString(objReader["Color_Name"]) : string.Empty;
                        objTagIdItemDetails.Scale = objReader["Scale"] != DBNull.Value ? Convert.ToString(objReader["Scale"]) : string.Empty;
                        objTagIdItemDetails.Season = objReader["Season"] != DBNull.Value ? Convert.ToString(objReader["Season"]) : string.Empty;
                        objTagIdItemDetails.Theme = objReader["Theme"] != DBNull.Value ? Convert.ToString(objReader["Theme"]) : string.Empty;
                        objTagIdItemDetails.Department = objReader["Department"] != DBNull.Value ? Convert.ToString(objReader["Department"]) : string.Empty;
                        objTagIdItemDetails.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                        objTagIdItemDetails.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : string.Empty;
                        objTagIdItemDetails.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objTagIdItemDetails.ItemStatus = objReader["ItemStatus"] != DBNull.Value ? Convert.ToString(objReader["ItemStatus"]) : string.Empty;
                        TagIdItemDetailsList.Add(objTagIdItemDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.RFIDList = TagIdItemDetailsList;
                    ResponseData.ResponseDynamicData = TagIdItemDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Stock Receipt-RFID Tag List");
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

        public override SelectAllStockReceiptResponse GetStockReceiptHeaderReport(SelectAllStockReceiptRequest objRequest)
        {
            var StockReceiptDetailsList = new List<StockReceiptHeader>();
            var RequestData = (SelectAllStockReceiptRequest)objRequest;
            var ResponseData = new SelectAllStockReceiptResponse();
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
                _CommandObj = new SqlCommand("Get_APIStockReceiptHeader", _ConnectionObj);
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
                        var objStockReceipt = new StockReceiptHeader();
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]): "";
                        objStockReceipt.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockReceipt.TotalReceivedQuantity = objReader["TotalReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalReceivedQuantity"]) : 0;
                        objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                       //objStockReceipt.Fromwarehousename = objReader["Fromwarehousename"] != DBNull.Value ? Convert.ToString(objReader["Fromwarehousename"]) : "";
                        objStockReceipt.DataFrom = objReader["UserName"] != DBNull.Value ? Convert.ToString(objReader["UserName"]) : "";
                        objStockReceipt.Discrepancies = objReader["Discrepancies"] != DBNull.Value ? Convert.ToInt32(objReader["Discrepancies"]) : 0;
                        
                        StockReceiptDetailsList.Add(objStockReceipt);
                    }
                    ResponseData.StockReceiptHeaderList = StockReceiptDetailsList;
                    ResponseData.ResponseDynamicData = StockReceiptDetailsList;
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

        public override SelectAllStockReceiptResponse GetStockReceiptDetailsReport(SelectAllStockReceiptRequest objRequest)
        {
            var StockReceiptDetailsList = new List<StockReceiptDetails>();
            var RequestData = (SelectAllStockReceiptRequest)objRequest;
            var ResponseData = new SelectAllStockReceiptResponse();
            SqlDataReader objReader;
            string StoreDBConnection = null;
            string EncyptConnection = null;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //StringBuilder sSql = new StringBuilder();
                //sSql.Append("Select * from DBConnections where StoreID = 2");

                //_CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                //_CommandObj.CommandType = CommandType.Text;
                //objReader = _CommandObj.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    while (objReader.Read())
                //    {
                //        StoreDBConnection = objReader["ConnString_Integration"] != DBNull.Value ? Convert.ToString(objReader["ConnString_Integration"]) : string.Empty;
                //        EncyptConnection = objReader["ConnectionString"] != DBNull.Value ? Convert.ToString(objReader["ConnectionString"]) : string.Empty;
                //    }
                //}
                //sqlCommon.CloseConnection(_ConnectionObj);
                //_ConnectionString = EncyptConnection;

                //sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Get_StockReceiptDetails", _ConnectionObj);
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
                        var objStockReceipt = new StockReceiptDetails();
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.Remarks = objReader["FromStoreID"] != DBNull.Value ? Convert.ToString(objReader["FromStoreID"]) : "";
                        objStockReceipt.Brand= objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        //objStockReceipt.FromStoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        objStockReceipt.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : "";
                        objStockReceipt.TransferQuantity = objReader["TransferQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TransferQuantity"]) : 0;
                        objStockReceipt.ReceivedQuantity = objReader["ReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQuantity"]) : 0;
                        objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockReceipt.Color = objReader["UserName"] != DBNull.Value ? Convert.ToString(objReader["UserName"]) : "";
                        objStockReceipt.Discrepancies = objReader["Discrepancies"] != DBNull.Value ? Convert.ToInt32(objReader["Discrepancies"]) : 0;

                        StockReceiptDetailsList.Add(objStockReceipt);
                    }
                    ResponseData.StockReceiptDetailsList = StockReceiptDetailsList;
                    ResponseData.ResponseDynamicData = StockReceiptDetailsList;
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

        public override SelectAllStockReceiptResponse API_SelectALL(SelectAllStockReceiptRequest objRequest)
        {
            var StockReceiptList = new List<StockReceiptHeader>();
            var RequestData = (SelectAllStockReceiptRequest)objRequest;
            var ResponseData = new SelectAllStockReceiptResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sQuery = new StringBuilder();


                //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    sQuery = "Select SRH.*,cm.countryname,sm.storename from StockReceiptHeader SRH left join countrymaster cm on SRH.FromCountryId=cm.id left join storemaster sm on SRH.storeid=sm.id Where SRH.Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SRH.storeid ='" + RequestData.StoreID + "' and SRH.status='Open'";
                //    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                //    _CommandObj.CommandType = CommandType.Text;
                //}
                //else
                //{
                //    sQuery = "Select * from StockReceiptHeader ";
                //    if (RequestData.FromOrToStoreCode != null && RequestData.FromOrToStoreCode != string.Empty)
                //    {
                //        sQuery = sQuery + " where StoreCode='" + RequestData.FromOrToStoreCode + "'" + " AND status='Open' ";
                //    }
                //    else
                //    {
                //        sQuery = sQuery + " where status='Open' ";
                //    }
                //    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                //    _CommandObj.CommandType = CommandType.Text;
                //}               

                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sQuery.Append("Select SRH.*,cm.countryname,sm.storename from StockReceiptHeader SRH with (nolock) left join countrymaster cm (nolock) on SRH.FromCountryId=cm.id left join storemaster sm (nolock) on SRH.storeid=sm.id Where SRH.Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SRH.storeid ='" + RequestData.StoreID + "' and SRH.status='Open'");
                    _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else
                {
                    sQuery.Append("Select ID, DocumentNo, DocumentDate, Status, Remarks, Active, RC.TOTAL_CNT [RecordCount] ");
                    sQuery.Append("from StockReceiptHeader (nolock) ");
                    sQuery.Append("LEFT JOIN(Select  count(SH.ID) As TOTAL_CNT From StockReceiptHeader SH with(NoLock) ");
                    sQuery.Append("where SH.Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or SH.DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");

                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or SH.DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                    }

                    sQuery.Append("or SH.Status like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or SH.Remarks like isnull('%" + RequestData.SearchString + "%','')) ");
                    if (RequestData.FromOrToStoreCode != null && RequestData.FromOrToStoreCode != string.Empty)
                    {
                        
                        sQuery.Append(" and SH.StoreCode ='" + RequestData.FromOrToStoreCode + "'" + " AND SH.Status='Open' ) As RC ON 1 = 1 ");
                    }
                    else
                    {
                        
                        sQuery.Append(" and SH.Status='Open') As RC ON 1 = 1  ");
                    }

                    sQuery.Append("where Active = " + RequestData.IsActive + " ");
                    sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sQuery.Append("or DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");

                    if (RequestData.SearchString.Contains("-"))
                    {
                        sQuery.Append("or DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                    }

                    sQuery.Append("or Status like isnull('%" + RequestData.SearchString + "%','') ");
                    sQuery.Append("or Remarks like isnull('%" + RequestData.SearchString + "%','')) ");

                    //sQuery = "Select ID, DocumentNo, DocumentDate, Status, Remarks, Active, RecordCount = COUNT(*) OVER() " +
                    //"from StockReceiptHeader " +
                    //"where Active = " + RequestData.IsActive + " " +
                    //    "and (isnull('" + RequestData.SearchString + "','') = '' " +
                    //    "or DocumentNo = isnull('" + RequestData.SearchString + "','') " +
                    //    "or DocumentDate = isnull('" + RequestData.SearchString + "','') " +
                    //    "or Status = isnull('" + RequestData.SearchString + "','') " +
                    //    "or Remarks = isnull('" + RequestData.SearchString + "','')) ";

                    if (RequestData.FromOrToStoreCode != null && RequestData.FromOrToStoreCode != string.Empty)
                    {
                        //sQuery = sQuery + " and StoreCode='" + RequestData.FromOrToStoreCode + "'" + " AND Status='Open' ";
                        sQuery.Append(" and StoreCode = '" + RequestData.FromOrToStoreCode + "'" + " AND Status='Open' ");
                    }
                    else
                    {
                       // sQuery = sQuery + " and Status='Open' ";
                        sQuery.Append(" and Status='Open' ");
                    }

                    sQuery.Append("order by ID asc ");
                    sQuery.Append("offset " + RequestData.Offset + " rows ");
                    sQuery.Append("fetch first " + RequestData.Limit + " rows only");


                    //sQuery = sQuery + "order by ID asc " +
                    //"offset " + RequestData.Offset + " rows " +
                    //"fetch first " + RequestData.Limit + " rows only";

                    _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new StockReceiptHeader();
                        objStockReceipt.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objStockReceipt.FromWareHouseID = objReader["FromWareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["FromWareHouseID"]) : 0;
                        //objStockReceipt.FromWarehouseCode = Convert.ToString(objReader["FromWarehouseCode"]);
                        //objStockReceipt.Fromwarehousename = Convert.ToString(objReader["Fromwarehousename"]);
                        //objStockReceipt.StockRequestDocumentNo = Convert.ToString(objReader["StockRequestDocumentNo"]);
                        //objStockReceipt.StockRequestID = objReader["StockRequestID"] != DBNull.Value ? Convert.ToInt32(objReader["StockRequestID"]) : 0;
                        //objStockReceipt.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        //objStockReceipt.TotalReceivedQuantity = objReader["TotalReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalReceivedQuantity"]) : 0;
                        objStockReceipt.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockReceipt.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objStockReceipt.DataFrom = Convert.ToString(objReader["DataFrom"]);
                        objStockReceipt.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockReceipt.Status = Convert.ToString(objReader["Status"]);
                        //objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockReceipt.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockReceipt.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockReceipt.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStockReceipt.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockReceipt.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objStockReceipt.WithOutBaseDoc = objReader["WithOutBaseDoc"] != DBNull.Value ? Convert.ToBoolean(objReader["WithOutBaseDoc"]) : false;
                        //objStockReceipt.Type = objReader["Type"] != DBNull.Value ? Convert.ToBoolean(objReader["Type"]) : false;
                        //objStockReceipt.fromApplication = objReader["fromApplication"] != DBNull.Value ? Convert.ToBoolean(objReader["fromApplication"]) : false;

                        //if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                        //{
                        //    objStockReceipt.ReceivedType = Convert.ToString(objReader["ReceivedType"]);
                        //}

                        //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        //{
                        //    objStockReceipt.StoreName = Convert.ToString(objReader["StoreName"]);
                        //}

                        //objStockReceipt.ReceivedType = objReader["ReceivedType"] != DBNull.Value ? Convert.ToString(objReader["ReceivedType"]) : string.Empty;

                        //objStockReceipt.StockReceiptDetailsList = new List<StockReceiptDetails>();
                        //objStockReceipt.RFIDList = new List<TagIdItemDetails>();

                        //SelectByStockReceiptDetailsRequest objSelectByStockReceiptDetailsRequest = new SelectByStockReceiptDetailsRequest();
                        //SelectByStockReceiptDetailsResponse objSelectByStockReceiptDetailsResponse = new SelectByStockReceiptDetailsResponse();
                        //objSelectByStockReceiptDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSelectByStockReceiptDetailsRequest.DocumentNumber = Convert.ToString(objReader["DocumentNo"]);
                        //objSelectByStockReceiptDetailsRequest.FromOrToCountryID = 0;
                        //objSelectByStockReceiptDetailsRequest.FromOrToStoreCode = Convert.ToString(objReader["StoreCode"]);
                        //objSelectByStockReceiptDetailsRequest.ConnectionString = RequestData.ConnectionString;

                        //objSelectByStockReceiptDetailsResponse = SelectByStockReceiptDetails(objSelectByStockReceiptDetailsRequest);
                        //if (objSelectByStockReceiptDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objStockReceipt.StockReceiptDetailsList = objSelectByStockReceiptDetailsResponse.StockReceiptDetailsRecord;
                        //    objStockReceipt.TransactionLogList = new List<TransactionLog>();
                        //    // No Need for Web Api
                        //    //objStockReceipt.TransactionLogList = objSelectByStockReceiptDetailsResponse.TransactionLogList;
                        //}

                        //if (objStockReceipt.ReceivedType.Trim().ToUpper() == "RFID")
                        //{
                        //    var objSelectTagIDListRequest = new SelectTagIDListRequest();
                        //    objSelectTagIDListRequest.DocumentNo = objStockReceipt.DocumentNo;
                        //    objSelectTagIDListRequest.ConnectionString = RequestData.ConnectionString;
                        //    var objSelectTagIDListResponse = new SelectTagIDListResponse();
                        //    objSelectTagIDListResponse = SelectTagIDList(objSelectTagIDListRequest);
                        //    if (objSelectTagIDListResponse.StatusCode == Enums.OpStatusCode.Success)
                        //    {
                        //        objStockReceipt.RFIDList = objSelectTagIDListResponse.RFIDList;
                        //    }
                        //}

                        StockReceiptList.Add(objStockReceipt);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.StockReceiptHeaderList = StockReceiptList;
                    ResponseData.ResponseDynamicData = StockReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReceipt Master");
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
