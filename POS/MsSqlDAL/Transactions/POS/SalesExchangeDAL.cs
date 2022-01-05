using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
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
    public class SalesExchangeDAL : BaseSalesExchangeDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSalesExchangeRequest)RequestObj;
            var ResponseData = new SaveSalesExchangeResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateSalesExchange", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.RequestFrom == Enums.RequestFrom.SyncService)
                {
                    _CommandObj.Parameters.AddWithValue("@ID", 0);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@ID", RequestData.SalesExchangeHeaderRecord.ID);
                }

                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.SalesExchangeHeaderRecord.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@DocumentNo", RequestData.SalesExchangeHeaderRecord.DocumentNo);
                _CommandObj.Parameters.AddWithValue("@DocumentDate", RequestData.SalesExchangeHeaderRecord.DocumentDate);
                _CommandObj.Parameters.AddWithValue("@SalesInvoiceNumber", RequestData.SalesExchangeHeaderRecord.SalesInvoiceNumber);   
                _CommandObj.Parameters.AddWithValue("@ExchangeWithOutInvoiceNo", RequestData.SalesExchangeHeaderRecord.ExchangeWithOutInvoiceNo);
                _CommandObj.Parameters.AddWithValue("@TotalExchangeQty", RequestData.SalesExchangeHeaderRecord.TotalExchangeQty);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.SalesExchangeHeaderRecord.CountryID);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.SalesExchangeHeaderRecord.StoreID);
                _CommandObj.Parameters.AddWithValue("@PosID", RequestData.SalesExchangeHeaderRecord.PosID);
                _CommandObj.Parameters.AddWithValue("@ExchangeMode", RequestData.SalesExchangeHeaderRecord.ExchangeMode);                
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.SalesExchangeHeaderRecord.CountryCode);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.SalesExchangeHeaderRecord.StoreCode);
                _CommandObj.Parameters.AddWithValue("@PosCode", RequestData.SalesExchangeHeaderRecord.POSCode);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.SalesExchangeHeaderRecord.CreateBy);
                _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.SalesExchangeHeaderRecord.CashierID);
                _CommandObj.Parameters.AddWithValue("@CreditSales", RequestData.SalesExchangeHeaderRecord.CreditSales);

                _CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);

                SqlParameter SalesExchangeDetails = _CommandObj.Parameters.Add("@SalesExchangeDetails", SqlDbType.Xml);
                SalesExchangeDetails.Direction = ParameterDirection.Input;
                SalesExchangeDetails.Value = SalesExchangeDetailXML(RequestData.SalesExchangeHeaderRecord.SalesExchangeDetailList,RequestData.RequestFrom);

                SqlParameter ReturnExchangeDetails = _CommandObj.Parameters.Add("@ReturnExchangeDetails", SqlDbType.Xml);
                ReturnExchangeDetails.Direction = ParameterDirection.Input;
                ReturnExchangeDetails.Value = ReturnExchangeXML(RequestData.SalesExchangeHeaderRecord.ReturnExchangeDetailList);

                var TransactionLog = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLog.Direction = ParameterDirection.Input;
                if (RequestData.TransactionLogList != null && RequestData.TransactionLogList.Count > 0)
                {
                    TransactionLog.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);
                }
                else
                {
                    TransactionLog.Value = string.Empty;
                }

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar,10);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.SalesExchangeHeaderRecord.SalesExchangeDetailList != null && RequestData.SalesExchangeHeaderRecord.SalesExchangeDetailList.Count > 0)
                {
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();
                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sales Exchange");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = Convert.ToInt32(ID2.Value).ToString();
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                        ResponseData.ExceptionMessage = Convert.ToString(StatusMsg.Value);
                        ResponseData.StackTrace = Convert.ToString(StatusMsg.Value);
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = "Detail list not found on " + RequestData.SalesExchangeHeaderRecord.DocumentNo;
                    ResponseData.ExceptionMessage = "Detail list not found on " + RequestData.SalesExchangeHeaderRecord.DocumentNo;
                    ResponseData.StackTrace = "Detail list not found on " + RequestData.SalesExchangeHeaderRecord.DocumentNo;
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = Convert.ToString(ex.Message);
                ResponseData.ExceptionMessage = Convert.ToString(ex.Message);
                ResponseData.StackTrace = Convert.ToString(ex.StackTrace);
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public string SalesExchangeDetailXML(List<SalesExchangeDetail> SalesExchangeDetailList, Enums.RequestFrom RequestFrom)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (SalesExchangeDetail objSalesExchangeDetail in SalesExchangeDetailList)
            {
                if ((objSalesExchangeDetail.Qty > 0 && objSalesExchangeDetail.IsExchange) || (objSalesExchangeDetail.Qty > 0 && RequestFrom == Enums.RequestFrom.SyncService))
                {
                    sSql.Append("<SalesExchangeDetail>");                   
                    sSql.Append("<ID>0</ID>");
                    sSql.Append("<SalesExchangeID>" + (objSalesExchangeDetail.SalesExchangeID) + "</SalesExchangeID>");
                    sSql.Append("<CountryID>" + (objSalesExchangeDetail.CountryID) + "</CountryID>");
                    sSql.Append("<StoreID>" + objSalesExchangeDetail.StoreID + "</StoreID>");
                    sSql.Append("<PosID>" + objSalesExchangeDetail.PosID + "</PosID>");
                    sSql.Append("<InvoiceDetailID>" + (objSalesExchangeDetail.InvoiceDetailID) + "</InvoiceDetailID>");
                    sSql.Append("<InvoiceSerialNo>" + (objSalesExchangeDetail.InvoiceSerialNo) + "</InvoiceSerialNo>");
                    sSql.Append("<SKUID>" + (objSalesExchangeDetail.SKUID) + "</SKUID>");
                    sSql.Append("<SKUCode>" + objSalesExchangeDetail.SKUCode + "</SKUCode>");
                    sSql.Append("<StyleCode>" + objSalesExchangeDetail.StyleCode + "</StyleCode>");
                    sSql.Append("<Qty>" + objSalesExchangeDetail.Qty + "</Qty>");
                    sSql.Append("<CountryCode>" + objSalesExchangeDetail.CountryCode + "</CountryCode>");
                    sSql.Append("<StoreCode>" + objSalesExchangeDetail.StoreCode + "</StoreCode>");
                    sSql.Append("<PosCode>" + objSalesExchangeDetail.POSCode + "</PosCode>");
                    sSql.Append("<SellingPricePerQty>" + objSalesExchangeDetail.SellingPricePerQty + "</SellingPricePerQty>");
                    sSql.Append("<CreateBy>" + objSalesExchangeDetail.CreateBy + "</CreateBy>");
                    sSql.Append("<TaxID>" + objSalesExchangeDetail.TaxID + "</TaxID>");
                    sSql.Append("<TaxAmount>" + objSalesExchangeDetail.TaxAmount + "</TaxAmount>");
                    sSql.Append("<Tag_Id>" + objSalesExchangeDetail.Tag_Id + "</Tag_Id>");
                    sSql.Append("</SalesExchangeDetail>");
                }
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
            //return sSql.ToString().Replace("&", "&#38;");
           // return sSql.ToString();
        }
        public string ReturnExchangeXML(List<SalesExchangeDetail> SalesExchangeDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (SalesExchangeDetail objSalesExchangeDetail in SalesExchangeDetailList)
            {
                if (objSalesExchangeDetail.ExchangeQty > 0)
                {
                    sSql.Append("<ReturnExchangeDetail>");
                    sSql.Append("<ExchangeDetailID>" + (objSalesExchangeDetail.ID) + "</ExchangeDetailID>");
                    sSql.Append("<InvoiceSerialNo>" + objSalesExchangeDetail.InvoiceSerialNo + "</InvoiceSerialNo>");
                    sSql.Append("<SKUCode>" + objSalesExchangeDetail.SKUCode + "</SKUCode>");
                    sSql.Append("<ExchangeSKU>" + objSalesExchangeDetail.ExchangeSKU + "</ExchangeSKU>");
                    sSql.Append("<InvoiceDetailID>" + (objSalesExchangeDetail.InvoiceDetailID) + "</InvoiceDetailID>");                   
                    sSql.Append("<ExchangeQty>" + objSalesExchangeDetail.ExchangeQty + "</ExchangeQty>");
                    sSql.Append("<Tag_Id>" + objSalesExchangeDetail.Tag_Id + "</Tag_Id>");
                    sSql.Append("</ReturnExchangeDetail>");
                }
            }
            //return sSql.ToString();

            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
        }
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SalesExchangeHeaderRecord = new SalesExchangeHeader();
            var RequestData = (SelectSalesExchangeRecordRequest)RequestObj;
            var ResponseData = new SelectSalesExchangeRecordResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                   
                string sSql = "Select* from SalesExchangeHeader with(NoLock) ";

                if (RequestData.DocumentNos != null && RequestData.DocumentNos != string.Empty)
                {
                    sSql = sSql + " where DocumentNo='" + RequestData.DocumentNos + "'";
                }
                else if (RequestData.SalesExchangeDocumentNo != null && RequestData.SalesExchangeDocumentNo != string.Empty)
                {
                    sSql = sSql + " where DocumentNo='" + RequestData.SalesExchangeDocumentNo + "'";
                }
                else if (RequestData.SalesExchangeID > 0)
                {                  
                    sSql = sSql + " where ID=" + RequestData.SalesExchangeID;
                }
                else
                {                   
                    sSql = sSql + " where ID=" + Convert.ToInt64(RequestData.DocumentIDs);
                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesExchangeHeader = new SalesExchangeHeader();
                        objSalesExchangeHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesExchangeHeader.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objSalesExchangeHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesExchangeHeader.SalesInvoiceNumber = Convert.ToString(objReader["SalesInvoiceNumber"]);
                        objSalesExchangeHeader.InvoiceHeaderID = objReader["InvoiceHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["InvoiceHeaderID"]) : 0;
                        objSalesExchangeHeader.ExchangeWithOutInvoiceNo = objReader["ExchangeWithOutInvoiceNo"] != DBNull.Value ? Convert.ToBoolean(objReader["ExchangeWithOutInvoiceNo"]) : false;
                        objSalesExchangeHeader.TotalExchangeQty = objReader["TotalExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalExchangeQty"]) : 0;
                        objSalesExchangeHeader.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSalesExchangeHeader.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objSalesExchangeHeader.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) : 0;
                        objSalesExchangeHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesExchangeHeader.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSalesExchangeHeader.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSalesExchangeHeader.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSalesExchangeHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        objSalesExchangeHeader.ExchangeMode = objReader["ExchangeMode"] != DBNull.Value ? Convert.ToString(objReader["ExchangeMode"]) : string.Empty;
                        objSalesExchangeHeader.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objSalesExchangeHeader.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objSalesExchangeHeader.POSCode = objReader["POSCode"] != DBNull.Value ? Convert.ToString(objReader["POSCode"]) : string.Empty;
                        objSalesExchangeHeader.CashierID = objReader["CashierID"] != DBNull.Value ? Convert.ToInt32(objReader["CashierID"]) : 0;
                        objSalesExchangeHeader.CreditSales = objReader["CreditSales"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditSales"]) : false;

                        objSalesExchangeHeader.SalesExchangeDetailList = new List<SalesExchangeDetail>();
                        objSalesExchangeHeader.ReturnExchangeDetailList = new List<SalesExchangeDetail>();

                        var objSelectAllSalesExchangeDetailRequest = new SelectAllSalesExchangeDetailRequest();
                        var objSelectAllSalesExchangeDetailResponse = new SelectAllSalesExchangeDetailResponse();

                        objSelectAllSalesExchangeDetailRequest.SalesExchangeID = objSalesExchangeHeader.ID;
                        objSelectAllSalesExchangeDetailRequest.ConnectionString = RequestData.ConnectionString;

                        objSelectAllSalesExchangeDetailResponse = SelectAllSalesExchangeDetailList(objSelectAllSalesExchangeDetailRequest);
                        if (objSelectAllSalesExchangeDetailResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesExchangeHeader.SalesExchangeDetailList = objSelectAllSalesExchangeDetailResponse.SalesExchangeDetailList;
                            objSalesExchangeHeader.ReturnExchangeDetailList = objSelectAllSalesExchangeDetailResponse.ReturnExchangeDetailList;
                        }
                        if (RequestData.RequestFrom == Enums.RequestFrom.SyncService && RequestData.FromOrToStoreID == 0)
                        {
                            objSalesExchangeHeader.TransactionLogList = TransactionLogList(objSalesExchangeHeader.StoreID, objSalesExchangeHeader.ID, RequestData);
                        }
                        else
                        {
                            objSalesExchangeHeader.TransactionLogList = new List<TransactionLog>();
                        }

                        SalesExchangeHeaderRecord = objSalesExchangeHeader;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesExchangeHeaderRecord = SalesExchangeHeaderRecord;
                    ResponseData.ResponseDynamicData = SalesExchangeHeaderRecord;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SalesExchangeList = new List<SalesExchangeHeader>();
            var RequestData = (SelectAllSalesExchangeRequest)RequestObj;
            var ResponseData = new SelectAllSalesExchangeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = string.Empty;
                //string sSql = "Select * from AgentMaster with(NoLock) where Active='{0}'";
                
                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {                 
                    sSql = "Select SEH.*,cm.countryname,sm.storename,pm.posname from SalesExchangeHeader SEH with(NoLock) left join countrymaster cm  with(NoLock) on SEH.countryid=cm.id left join storemaster sm with(NoLock) on SEH.storeid=sm.id left join posmaster pm with(NoLock) on SEH.posid=pm.id Where SEH.Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SEH.storeid ='" + RequestData.StoreID + "'";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else
                {
                    sSql = "Select * from SalesExchangeHeader with(NoLock) ";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
               
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesExchangeHeader = new SalesExchangeHeader();
                        objSalesExchangeHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesExchangeHeader.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objSalesExchangeHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesExchangeHeader.SalesInvoiceNumber = Convert.ToString(objReader["SalesInvoiceNumber"]);
                        objSalesExchangeHeader.InvoiceHeaderID = objReader["InvoiceHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["InvoiceHeaderID"]) : 0;
                        objSalesExchangeHeader.ExchangeWithOutInvoiceNo = objReader["ExchangeWithOutInvoiceNo"] != DBNull.Value ? Convert.ToBoolean(objReader["InvoiceHeaderID"]) : false;
                        objSalesExchangeHeader.TotalExchangeQty = objReader["TotalExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalExchangeQty"]) : 0;
                        objSalesExchangeHeader.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSalesExchangeHeader.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objSalesExchangeHeader.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) : 0;
                        objSalesExchangeHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesExchangeHeader.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSalesExchangeHeader.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSalesExchangeHeader.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSalesExchangeHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        objSalesExchangeHeader.ExchangeMode = objReader["ExchangeMode"] != DBNull.Value ? Convert.ToString(objReader["ExchangeMode"]) : string.Empty;
                        objSalesExchangeHeader.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objSalesExchangeHeader.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objSalesExchangeHeader.POSCode = objReader["POSCode"] != DBNull.Value ? Convert.ToString(objReader["POSCode"]) : string.Empty;
                        objSalesExchangeHeader.CashierID = objReader["CashierID"] != DBNull.Value ? Convert.ToInt32(objReader["CashierID"]) : 0;
                        objSalesExchangeHeader.CreditSales = objReader["CreditSales"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditSales"]) : false;


                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objSalesExchangeHeader.StoreName = Convert.ToString(objReader["StoreName"]);
                            objSalesExchangeHeader.POSName = Convert.ToString(objReader["posName"]);
                            objSalesExchangeHeader.CountryName = Convert.ToString(objReader["CountryName"]);
                        }
                        objSalesExchangeHeader.SalesExchangeDetailList = new List<SalesExchangeDetail>();
                        objSalesExchangeHeader.ReturnExchangeDetailList = new List<SalesExchangeDetail>();

                        var objSelectAllSalesExchangeDetailRequest = new SelectAllSalesExchangeDetailRequest();
                        var objSelectAllSalesExchangeDetailResponse = new SelectAllSalesExchangeDetailResponse();

                        objSelectAllSalesExchangeDetailRequest.SalesExchangeID = objSalesExchangeHeader.ID;
                        objSelectAllSalesExchangeDetailResponse = SelectAllSalesExchangeDetailList(objSelectAllSalesExchangeDetailRequest);
                        if (objSelectAllSalesExchangeDetailResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesExchangeHeader.SalesExchangeDetailList = objSelectAllSalesExchangeDetailResponse.SalesExchangeDetailList;
                            objSalesExchangeHeader.ReturnExchangeDetailList = objSelectAllSalesExchangeDetailResponse.ReturnExchangeDetailList;
                        }

                        SalesExchangeList.Add(objSalesExchangeHeader);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesExchangeList = SalesExchangeList;
                    ResponseData.ResponseDynamicData = SalesExchangeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
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

        public override SelectAllSalesExchangeDetailResponse SelectAllSalesExchangeDetailList(SelectAllSalesExchangeDetailRequest RequestObj)
        {
            var SalesExchangeDetailList = new List<SalesExchangeDetail>();
            var ReturnExchangeDetailList = new List<SalesExchangeDetail>();

            var RequestData = (SelectAllSalesExchangeDetailRequest)RequestObj;
            var ResponseData = new SelectAllSalesExchangeDetailResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
               
                StringBuilder sSql = new StringBuilder();

                sSql.Append("Select sed.*,seh.DocumentNo,seh.SalesInvoiceNumber,seh.CreditSales from  SalesExchangeHeader  seh with(NoLock) ");
                sSql.Append("join SalesExchangeDetail sed with(NoLock) on seh.ID = sed.SalesExchangeID");

                if (RequestData.InvoiceNo != null && RequestData.InvoiceNo.Trim() != string.Empty)
                {
                    if (RequestData.Mode == "SalesInvoice")
                    {
                        sSql.Append(" where seh.SalesInvoiceNumber='" + RequestData.InvoiceNo.Trim() + "'");
                    }
                    else
                    {
                        sSql.Append(" where seh.DocumentNo='" + RequestData.InvoiceNo.Trim() + "'");
                    }
                }
                else if (RequestData.SalesExchangeID > 0)
                {
                    sSql.Append( " where sed.SalesExchangeID=" + RequestData.SalesExchangeID);
                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        //CreateBy,CreateOn,UpdateBy,UpdateOn,Active
                        var objSalesExchangeDetail = new SalesExchangeDetail();

                        objSalesExchangeDetail.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesExchangeDetail.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : string.Empty;
                        objSalesExchangeDetail.SalesExchangeID = objReader["SalesExchangeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesExchangeID"]) : 0;
                        objSalesExchangeDetail.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSalesExchangeDetail.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objSalesExchangeDetail.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) : 0;
                        objSalesExchangeDetail.InvoiceDetailID = objReader["InvoiceDetailID"] != DBNull.Value ? Convert.ToInt32(objReader["InvoiceDetailID"]) : 0;
                        objSalesExchangeDetail.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        objSalesExchangeDetail.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objSalesExchangeDetail.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objSalesExchangeDetail.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objSalesExchangeDetail.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objSalesExchangeDetail.POSCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objSalesExchangeDetail.InvoiceSerialNo = objReader["SerialNo"] != DBNull.Value ? Convert.ToInt32(objReader["SerialNo"]) : 0;

                        objSalesExchangeDetail.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objSalesExchangeDetail.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;

                        objSalesExchangeDetail.CreditSales = objReader["CreditSales"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditSales"]) : false;
                        objSalesExchangeDetail.SalesInvoiceNumber = objReader["SalesInvoiceNumber"] != DBNull.Value ? Convert.ToString(objReader["SalesInvoiceNumber"]) : string.Empty;


                        objSalesExchangeDetail.SellingPricePerQty = objReader["SellingPricePerQty"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPricePerQty"]) : 0;

                        if (RequestData.Mode == "SalesInvoice" || RequestData.Mode == "ReturnInvoice")
                        {
                            objSalesExchangeDetail.IsExchanged = true; 
                            objSalesExchangeDetail.EnableCell = false;
                            objSalesExchangeDetail.ExchangeRemarks = "Already Exchanged Item.";
                            objSalesExchangeDetail.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                            //objSalesExchangeDetail.ExchangeQty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;

                            //objSalesExchangeDetail.IsExchanged = objReader["IsExchanged"] != DBNull.Value ? Convert.ToBoolean(objReader["IsExchanged"]) : false;
                            objSalesExchangeDetail.ExchangedQty = objReader["NewExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["NewExchangeQty"]) : 0; // Already exchanged qty
                            objSalesExchangeDetail.IsReturned = objReader["IsReturned"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturned"]) : false;
                            objSalesExchangeDetail.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;

                            string ReturnRemarks = string.Empty;
                            if (objSalesExchangeDetail.ExchangedQty > 0)
                            {
                                ReturnRemarks = objSalesExchangeDetail.ExchangedQty + " items are exchanged this sales.";
                            }
                            if (objSalesExchangeDetail.ReturnQty > 0)
                            {
                                ReturnRemarks = ReturnRemarks + objSalesExchangeDetail.ReturnQty + " items are already returned this sales.";
                            }
                            objSalesExchangeDetail.ExchangeRemarks = ReturnRemarks;

                        }
                        else
                        {
                            objSalesExchangeDetail.IsExchanged = objReader["IsExchanged"] != DBNull.Value ? Convert.ToBoolean(objReader["IsExchanged"]) : false;
                            objSalesExchangeDetail.ExchangedQty = objReader["NewExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["NewExchangeQty"]) : 0; // Already exchanged qty
                            objSalesExchangeDetail.IsReturned = objReader["IsReturned"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturned"]) : false;
                            objSalesExchangeDetail.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;
 
                            objSalesExchangeDetail.EnableCell = true;
                            objSalesExchangeDetail.ExchangeRemarks = string.Empty;
                            objSalesExchangeDetail.ExchangeQty = 0; // we can update through entry
                            objSalesExchangeDetail.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0; // Its an equal to sales Qty

                            string ReturnRemarks = string.Empty;
                            if (objSalesExchangeDetail.ExchangedQty > 0)
                            {
                                ReturnRemarks = objSalesExchangeDetail.ExchangedQty + " items are exchanged this sales.";
                            }
                            if (objSalesExchangeDetail.ReturnQty > 0)
                            {
                                ReturnRemarks = ReturnRemarks + objSalesExchangeDetail.ReturnQty + " items are already returned this sales.";
                            }
                            objSalesExchangeDetail.ExchangeRemarks = ReturnRemarks;
                        }
                        objSalesExchangeDetail.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesExchangeDetail.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSalesExchangeDetail.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSalesExchangeDetail.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSalesExchangeDetail.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;

                        SalesExchangeDetailList.Add(objSalesExchangeDetail);

                        var TempReturnExchangeDetailList = new List<SalesExchangeDetail>();
                        TempReturnExchangeDetailList = GetReturnExchangeDetailList(RequestData, objSalesExchangeDetail.DocumentNo, objSalesExchangeDetail.InvoiceDetailID , objSalesExchangeDetail.InvoiceSerialNo);
                        if(TempReturnExchangeDetailList != null && TempReturnExchangeDetailList.Count > 0)
                        {
                            ReturnExchangeDetailList.AddRange(TempReturnExchangeDetailList);
                        }
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesExchangeDetailList = SalesExchangeDetailList;
                    ResponseData.ReturnExchangeDetailList = ReturnExchangeDetailList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
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
        public List<SalesExchangeDetail> GetReturnExchangeDetailList(SelectAllSalesExchangeDetailRequest RequestData, string DocumentNo, int InvoiceDetailID , int InvoiceSerialNo)
        {
            var ReturnExchangeDetailList = new List<SalesExchangeDetail>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sbSql = new StringBuilder();
                string sSql = string.Empty;

                sbSql.Append("Select * from ReturnExchangeDetail with(NoLock) where ExchangeDocumentNo='{0}' and InvoiceDetailID={1}");

                sSql = string.Format(sbSql.ToString(), DocumentNo, InvoiceDetailID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objReturnExchangeDetail = new SalesExchangeDetail();

                        objReturnExchangeDetail.DocumentNo = Convert.ToString(objReader["ExchangeDocumentNo"]);
                        objReturnExchangeDetail.ID = objReader["ExchangeDetailID"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeDetailID"]) : 0;
                        objReturnExchangeDetail.InvoiceDetailID = objReader["InvoiceDetailID"] != DBNull.Value ? Convert.ToInt32(objReader["InvoiceDetailID"]) : 0;
                        objReturnExchangeDetail.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
                        objReturnExchangeDetail.SKUCode = objReader["ReturnSKU"] != DBNull.Value ? Convert.ToString(objReader["ReturnSKU"]) : string.Empty;
                        objReturnExchangeDetail.ExchangeSKU = objReader["ExchangeSKU"] != DBNull.Value ? Convert.ToString(objReader["ExchangeSKU"]) : string.Empty;
                        objReturnExchangeDetail.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
                        objReturnExchangeDetail.InvoiceSerialNo = InvoiceSerialNo;
                        ReturnExchangeDetailList.Add(objReturnExchangeDetail);
                    }
                }
                return ReturnExchangeDetailList;
            }
            catch(Exception ex)
            {
                return new List<SalesExchangeDetail>();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
        }
        public override SelectExchangeByInvoiceNumResponse GetExchangeReceipt(SelectExchangeByInvoiceNumRequest RequestObj)
        {
            var ExchangeReceiptList = new List<ExchangeReceipt>();
            var RequestData = (SelectExchangeByInvoiceNumRequest)RequestObj;
            var ResponseData = new SelectExchangeByInvoiceNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetExchangeDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNum);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objExchangeReceipt = new ExchangeReceipt();

                        objExchangeReceipt.Currency = objReader["Currency"].ToString();
                        objExchangeReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objExchangeReceipt.ShopName = objReader["ShopName"].ToString();
                        objExchangeReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                        objExchangeReceipt.SKUCode = objReader["SKUCode"].ToString();
                        objExchangeReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;                  
                        objExchangeReceipt.SalesInvoice = objReader["SalesInvoice"].ToString();
                        objExchangeReceipt.POSName = objReader["POSName"].ToString();
                        objExchangeReceipt.Cashier = objReader["Cashier"].ToString();
                        objExchangeReceipt.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;                       
                        objExchangeReceipt.Footer = objReader["Footer"].ToString();
                        objExchangeReceipt.ArabicDetails = objReader["ArabicDetails"].ToString();
                        objExchangeReceipt.Date = objReader["Date"] != DBNull.Value ? Convert.ToDateTime(objReader["Date"]) : DateTime.Now;
                        objExchangeReceipt.Time = objReader["Time"] != DBNull.Value ? Convert.ToDateTime(objReader["Time"]) : DateTime.Now;
                        objExchangeReceipt.SalesInvoice1 = objReader["SalesInvoice"].ToString();
                        objExchangeReceipt.CustomerName = objReader["CustomerName"].ToString();

                        ExchangeReceiptList.Add(objExchangeReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ExchangeReceiptList = ExchangeReceiptList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Exchange Print");
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
                sSql.Append("<Tag_Id>" + (objTransactionLogDetailMasterDetails.Tag_Id) + "</Tag_Id>");
                sSql.Append("</TransactionLogDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }
        public List<TransactionLog> TransactionLogList(int StoreID, long DocumentID, SelectSalesExchangeRecordRequest RequestData)
        {
            var _TransactionLogList = new List<TransactionLog>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from TransactionLog with(NoLock) where StoreID={0} and DocumentID={1} and TransactionType='SalesExchange'";
                sSql = string.Format(sSql, StoreID, DocumentID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTransactionLog.TransactionType = objReader["TransactionType"] != DBNull.Value ? Convert.ToString(objReader["TransactionType"]) : string.Empty;
                        objTransactionLog.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objTransactionLog.ActualDateTime = objReader["ActualDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ActualDateTime"]) : DateTime.Now;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.InQty = objReader["InQty"] != DBNull.Value ? Convert.ToInt32(objReader["InQty"]) : 0;
                        objTransactionLog.OutQty = objReader["OutQty"] != DBNull.Value ? Convert.ToInt32(objReader["OutQty"]) : 0;
                        objTransactionLog.TransactionPrice = objReader["TransactionPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["TransactionPrice"]) : 0;
                        objTransactionLog.Currency = objReader["Currency"] != DBNull.Value ? Convert.ToDecimal(objReader["Currency"]) : 0;
                        objTransactionLog.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;
                        objTransactionLog.DocumentPrice = objReader["DocumentPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["DocumentPrice"]) : 0;
                        objTransactionLog.UserID = objReader["UserID"] != DBNull.Value ? Convert.ToInt32(objReader["UserID"]) : 0;
                        objTransactionLog.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objTransactionLog.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objTransactionLog.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objTransactionLog.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        _TransactionLogList.Add(objTransactionLog);
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<TransactionLog>();
            }
            return _TransactionLogList;
        }

        public override GetExchangeOrSalesResponse GetSalesOrExchangeList(SelectAllInvoiceRequest RequestObj)
        {
            var InvoiceHeaderList = new List<InvoiceHeader>();
            var SalesxchangeDetailList = new List<SalesExchangeDetail>();

            var RequestData = (SelectAllInvoiceRequest)RequestObj;
            var ResponseData = new GetExchangeOrSalesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                StringBuilder sSql = new StringBuilder();

                sSql.Append("SELECT *,um.CustomerName,um.PhoneNumber FROM InvoiceHeader ih with(NoLock) ");
                 sSql.Append("join Customermaster um with(NoLock) on ih.CustomerCode=um.CustomerCode Where InvoiceNo='" + RequestData.SearchString + "'");
               

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objInvoiceHeaderTypes = new InvoiceHeader();

                        objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
                        objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
                        objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
                        objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;

                        //objInvoiceHeaderTypes.SubTotalWithOutDiscount = objReader["SubTotalWithOutDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithOutDiscount"]) : 0;

                        objInvoiceHeaderTypes.TotalDiscountType = objReader["TotalDiscountType"] != DBNull.Value ? Convert.ToString(objReader["TotalDiscountType"]) : string.Empty;
                        objInvoiceHeaderTypes.TotalDiscountAmount = objReader["TotalDiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscountAmount"]) : 0;
                        objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
                        objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
                        objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
                        objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
                        objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
                        objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
                        objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

                        objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
                        objInvoiceHeaderTypes.Active = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objInvoiceHeaderTypes.Active = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objInvoiceHeaderTypes.CreateOn = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateOn = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;

                        objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                        objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
                        objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;

                        // "Senthamil_Changes"
                        {
                            objInvoiceHeaderTypes.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponCode = objReader["RedeemCouponCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponLineNo = objReader["RedeemCouponLineNo"] != DBNull.Value ? Convert.ToInt32(objReader["RedeemCouponLineNo"]) : 0;
                            objInvoiceHeaderTypes.RedeemCouponSerialCode = objReader["RedeemCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountType = objReader["RedeemCouponDiscountType"] != DBNull.Value ? Convert.ToString(objReader["RedeemCouponDiscountType"]) : string.Empty;
                            objInvoiceHeaderTypes.RedeemCouponDiscountValue = objReader["RedeemCouponDiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemCouponDiscountValue"]) : 0;
                            objInvoiceHeaderTypes.RedeemValue = objReader["RedeemValue"] != DBNull.Value ? Convert.ToDecimal(objReader["RedeemValue"]) : 0;
                            objInvoiceHeaderTypes.IssuedCouponCode = objReader["IssuedCouponCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponCode"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponLineNo = objReader["IssuedCouponLineNo"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponLineNo"]) : string.Empty;
                            objInvoiceHeaderTypes.IssuedCouponSerialCode = objReader["IssuedCouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["IssuedCouponSerialCode"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
                            objInvoiceHeaderTypes.CustomerMobileNo = objReader["CustomerMobileNo"] != DBNull.Value ? Convert.ToString(objReader["CustomerMobileNo"]) : string.Empty;
                        }


                        InvoiceHeaderList.Add(objInvoiceHeaderTypes);

                    }
                    ResponseData.InvoiceHeaderList = InvoiceHeaderList;
                    ResponseData.ResponseDynamicData = InvoiceHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    if(ResponseData.InvoiceHeaderList.Count <1)
                    {

                        sSql.Append("Select sed.*,seh.DocumentNo,seh.SalesInvoiceNumber,seh.CreditSales from  SalesExchangeHeader  seh with(NoLock) ");
                        sSql.Append("join SalesExchangeDetail sed with(NoLock) on seh.ID = sed.SalesExchangeID");
                        sSql.Append(" where seh.DocumentNo='" + RequestData.SearchString.Trim() + "'");

                        _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                        _CommandObj.CommandType = CommandType.Text;
                        objReader = _CommandObj.ExecuteReader();
                        if (objReader.HasRows)
                        {
                            while (objReader.Read())
                            {

                                //CreateBy,CreateOn,UpdateBy,UpdateOn,Active
                                var objSalesExchangeDetail = new SalesExchangeDetail();

                                objSalesExchangeDetail.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                                objSalesExchangeDetail.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : string.Empty;
                                objSalesExchangeDetail.SalesExchangeID = objReader["SalesExchangeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesExchangeID"]) : 0;
                                objSalesExchangeDetail.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                                objSalesExchangeDetail.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                                objSalesExchangeDetail.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) : 0;
                                objSalesExchangeDetail.InvoiceDetailID = objReader["InvoiceDetailID"] != DBNull.Value ? Convert.ToInt32(objReader["InvoiceDetailID"]) : 0;
                                objSalesExchangeDetail.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                                objSalesExchangeDetail.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                                objSalesExchangeDetail.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                                objSalesExchangeDetail.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                                objSalesExchangeDetail.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                                objSalesExchangeDetail.POSCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                                objSalesExchangeDetail.InvoiceSerialNo = objReader["SerialNo"] != DBNull.Value ? Convert.ToInt32(objReader["SerialNo"]) : 0;

                                objSalesExchangeDetail.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                                objSalesExchangeDetail.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;

                                objSalesExchangeDetail.CreditSales = objReader["CreditSales"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditSales"]) : false;
                                objSalesExchangeDetail.SalesInvoiceNumber = objReader["SalesInvoiceNumber"] != DBNull.Value ? Convert.ToString(objReader["SalesInvoiceNumber"]) : string.Empty;


                                objSalesExchangeDetail.SellingPricePerQty = objReader["SellingPricePerQty"] != DBNull.Value ? Convert.ToDecimal(objReader["SellingPricePerQty"]) : 0;

                                
                              
                                    objSalesExchangeDetail.IsExchanged = objReader["IsExchanged"] != DBNull.Value ? Convert.ToBoolean(objReader["IsExchanged"]) : false;
                                    objSalesExchangeDetail.ExchangedQty = objReader["NewExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["NewExchangeQty"]) : 0; // Already exchanged qty
                                    objSalesExchangeDetail.IsReturned = objReader["IsReturned"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturned"]) : false;
                                    objSalesExchangeDetail.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;

                                    objSalesExchangeDetail.EnableCell = true;
                                    objSalesExchangeDetail.ExchangeRemarks = string.Empty;
                                    objSalesExchangeDetail.ExchangeQty = 0; // we can update through entry
                                    objSalesExchangeDetail.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0; // Its an equal to sales Qty

                                    string ReturnRemarks = string.Empty;
                                    if (objSalesExchangeDetail.ExchangedQty > 0)
                                    {
                                        ReturnRemarks = objSalesExchangeDetail.ExchangedQty + " items are exchanged this sales.";
                                    }
                                    if (objSalesExchangeDetail.ReturnQty > 0)
                                    {
                                        ReturnRemarks = ReturnRemarks + objSalesExchangeDetail.ReturnQty + " items are already returned this sales.";
                                    }
                                    objSalesExchangeDetail.ExchangeRemarks = ReturnRemarks;
                               
                                objSalesExchangeDetail.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                                objSalesExchangeDetail.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                                objSalesExchangeDetail.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                                objSalesExchangeDetail.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                                objSalesExchangeDetail.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;

                                SalesxchangeDetailList.Add(objSalesExchangeDetail);                               
                            }
                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                            ResponseData.SalesExchangeDetailList = SalesxchangeDetailList;
                        }
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Exchange");
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
