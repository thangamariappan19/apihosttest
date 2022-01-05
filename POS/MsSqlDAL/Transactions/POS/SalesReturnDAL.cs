using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.Sales_Return;
using EasyBizRequest.Transactions.POS.SalesReturnRequest;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
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
   public class SalesReturnDAL : BaseSalesReturnHeaderDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override SelectSalesReturnDetailsByIDResponse SelectByIDSalesReturnDetails(SelectSalesReturnDetailsByIDRequest ObjRequest)
        {
            var SalesReturnDetailList = new List<SalesReturnDetail>();
            var RequestData = (SelectSalesReturnDetailsByIDRequest)ObjRequest;
            var ResponseData = new SelectSalesReturnDetailsByIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                if (RequestData.ReturnWithOutInvoiceNo)
                {
                    sSql.Append("Select * from SalesReturnDetail with(NoLock) where SalesReturnID=" + RequestData.ID);                    
                }
                else if (RequestData.ReturnMode != null && RequestData.ReturnMode.Trim().ToLower() == "exchangeinvoice")
                {
                    sSql.Append("Select srd.*,0 as SerialNo  from SalesReturnHeader srh with(NoLock) ");
                    sSql.Append("join SalesReturnDetail srd with(NoLock) on srh.ID = srd.SalesReturnID ");
                    sSql.Append("join SalesExchangeHeader seh with(NoLock) on srh.SalesInvoiceNumber = seh.DocumentNo ");
                    sSql.Append("join SalesExchangeDetail sed with(NoLock) on seh.ID = sed.SalesExchangeID and srd.ItemCode = sed.SKUCode where  srd.SalesReturnID=" + RequestData.ID);
                }
                else
                {
                    sSql.Append("Select srd.*,id.SerialNo  from SalesReturnHeader srh with(NoLock) join SalesReturnDetail srd with(NoLock) on srh.ID=srd.SalesReturnID join InvoiceDetail id with(NoLock) on srh.SalesInvoiceNumber=id.InvoiceNo and id.SKUCode=srd.ItemCode and id.ID=srd.InvoiceDetailID ");
                    sSql.Append("where  srd.SalesReturnID=" + RequestData.ID + " ");
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesReturnDetail = new SalesReturnDetail();
                        objSalesReturnDetail.ID = Convert.ToInt32(objReader["ID"]);
                        objSalesReturnDetail.SalesReturnID = Convert.ToInt32(objReader["SalesReturnID"]);
                        objSalesReturnDetail.SKUID = Convert.ToInt32(objReader["SKUID"]);
                        objSalesReturnDetail.InvoiceDetailID = Convert.ToInt32(objReader["InvoiceDetailID"]);
                        objSalesReturnDetail.ItemCode = objReader["ItemCode"].ToString();
                        objSalesReturnDetail.ReturnQty = Convert.ToInt32(objReader["ReturnQty"]);
                        objSalesReturnDetail.ReturnAmount = Convert.ToDecimal(objReader["ReturnAmount"]);
                        //objSalesReturnDetail.ModifiedSalesEmployee = objReader["ModifiedSalesEmployee"].ToString();
                        //objSalesReturnDetail.ModifiedSalesManager = objReader["ModifiedSalesManager"].ToString();
                        //objSalesReturnDetail.FromCountryID = Convert.ToInt32(objReader["FromCountryID"]);
                        //objSalesReturnDetail.FromStoreID = Convert.ToInt32(objReader["FromStoreID"]);
                        objSalesReturnDetail.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
                        objSalesReturnDetail.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
                        objSalesReturnDetail.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
                        objSalesReturnDetail.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;
                        objSalesReturnDetail.SyncFailedReason = objReader["SyncFailedReason"].ToString();

                        objSalesReturnDetail.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objSalesReturnDetail.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objSalesReturnDetail.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objSalesReturnDetail.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objSalesReturnDetail.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;

                        objSalesReturnDetail.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSalesReturnDetail.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objSalesReturnDetail.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) : 0;

                        if (RequestData.ReturnWithOutInvoiceNo == false)
                        {
                            objSalesReturnDetail.SerialNo = objReader["SerialNo"] != DBNull.Value ? Convert.ToInt32(objReader["SerialNo"]) : 0;
                        }  
                        
                        SalesReturnDetailList.Add(objSalesReturnDetail);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesReturnDetailData = SalesReturnDetailList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Return");
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

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSalesReturnRequest)RequestObj;
            var ResponseData = new SaveSalesReturnResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateSalesReturn", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.RequestFrom == Enums.RequestFrom.SyncService)
                {
                    _CommandObj.Parameters.AddWithValue("@ID", 0);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@ID", RequestData.SalesReturnHeaderData.ID);
                }

                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.SalesReturnHeaderData.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@DocumentNo", RequestData.SalesReturnHeaderData.DocumentNo);
                //_CommandObj.Parameters.AddWithValue("@DocumentDate", sqlCommon.GetSQLServerDateString(RequestData.SalesReturnHeaderData.DocumentDate));
                _CommandObj.Parameters.AddWithValue("@DocumentDate", RequestData.SalesReturnHeaderData.DocumentDate);
                if (RequestData.SalesReturnHeaderData.SalesInvoiceNumber != null && RequestData.SalesReturnHeaderData.SalesInvoiceNumber != string.Empty)
                {
                    _CommandObj.Parameters.AddWithValue("@SalesInvoiceNumber", RequestData.SalesReturnHeaderData.SalesInvoiceNumber);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@SalesInvoiceNumber", DBNull.Value);
                }
                if (RequestData.SalesReturnHeaderData.SalesDate != DateTime.MinValue && RequestData.SalesReturnHeaderData.SalesDate != DateTime.MaxValue)
                {
                    _CommandObj.Parameters.AddWithValue("@SalesDate", RequestData.SalesReturnHeaderData.SalesDate);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@SalesDate", DBNull.Value);
                }              
                               
                _CommandObj.Parameters.AddWithValue("@ApplicationDate", RequestData.SalesReturnHeaderData.ApplicationDate);
                _CommandObj.Parameters.AddWithValue("@TotalReturnQty", RequestData.SalesReturnHeaderData.TotalReturnQty);
                _CommandObj.Parameters.AddWithValue("@TotalReturnAmount", RequestData.SalesReturnHeaderData.TotalReturnAmount);
                _CommandObj.Parameters.AddWithValue("@PaymentMode", RequestData.SalesReturnHeaderData.PaymentMode);
                _CommandObj.Parameters.AddWithValue("@ReturnWithOutInvoiceNo", RequestData.SalesReturnHeaderData.ReturnWithOutInvoiceNo);
                _CommandObj.Parameters.AddWithValue("@ReturnMode", RequestData.SalesReturnHeaderData.ReturnMode);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.SalesReturnHeaderData.CountryID);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.SalesReturnHeaderData.StoreID);
                _CommandObj.Parameters.AddWithValue("@PosID", RequestData.SalesReturnHeaderData.PosID);
                _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.SalesReturnHeaderData.CashierID);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.SalesReturnHeaderData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.SalesReturnHeaderData.ShiftID);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.SalesReturnHeaderData.CountryCode);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.SalesReturnHeaderData.StoreCode);
                _CommandObj.Parameters.AddWithValue("@PosCode", RequestData.SalesReturnHeaderData.PosCode);
                _CommandObj.Parameters.AddWithValue("@TaxID", RequestData.SalesReturnHeaderData.TaxID);
                _CommandObj.Parameters.AddWithValue("@TotalTaxAmount", RequestData.SalesReturnHeaderData.TotalTaxAmount);

                _CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);

                var SalesReturnDetail = _CommandObj.Parameters.Add("@SalesReturnDetail", SqlDbType.Xml);
                SalesReturnDetail.Direction = ParameterDirection.Input;
                SalesReturnDetail.Value = SalesReturnDetailXML(RequestData.SalesReturnHeaderData.SalesReturnDetailList,RequestData.RequestFrom);

                var SalesReturnPaymentDetails = _CommandObj.Parameters.Add("@SalesReturnPaymentDetails", SqlDbType.Xml);
                SalesReturnPaymentDetails.Direction = ParameterDirection.Input;
                SalesReturnPaymentDetails.Value = SalesReturnPaymentDetailsXML(RequestData.SalesReturnHeaderData.SalesReturnPaymentdetails);

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

                _CommandObj.Parameters.AddWithValue("@CreditSales", RequestData.SalesReturnHeaderData.CreditSales);

                if (RequestData.SalesReturnHeaderData.CreditSales && RequestData.OnAccountPaymentRecord != null)
                {
                    _CommandObj.Parameters.AddWithValue("@CustomerCode", RequestData.OnAccountPaymentRecord.CustomerCode);
                    _CommandObj.Parameters.AddWithValue("@BillingAmount", RequestData.OnAccountPaymentRecord.BillingAmount);
                    _CommandObj.Parameters.AddWithValue("@OnAccountPaymentDetails", OnAccountPaymentDetailsXml(RequestData.OnAccountPaymentRecord.OnAccountPaymentDetailsList));
                    _CommandObj.Parameters.AddWithValue("@OnAcInvoiceWisePayment", OnAcInvoiceWisePaymentXml(RequestData.OnAccountPaymentRecord.OnAcInvoiceWisePaymentList));
                    _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.OnAccountPaymentRecord.Remarks);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@CustomerCode", DBNull.Value);
                    _CommandObj.Parameters.AddWithValue("@BillingAmount", DBNull.Value);
                    _CommandObj.Parameters.AddWithValue("@OnAccountPaymentDetails", DBNull.Value);
                    _CommandObj.Parameters.AddWithValue("@OnAcInvoiceWisePayment", DBNull.Value);
                    _CommandObj.Parameters.AddWithValue("@Remarks", DBNull.Value);
                }

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;
                
                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.SalesReturnHeaderData.SalesReturnDetailList != null && RequestData.SalesReturnHeaderData.SalesReturnDetailList.Count > 0)
                {
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();
                    string strerrormsg = StatusMsg.Value.ToString();
                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sales Return");
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = Convert.ToString(ID1.Value);
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
                    ResponseData.DisplayMessage = "Detail list not found on " + RequestData.SalesReturnHeaderData.DocumentNo;
                    ResponseData.ExceptionMessage = "Detail list not found on " + RequestData.SalesReturnHeaderData.DocumentNo;
                    ResponseData.StackTrace = "Detail list not found on " + RequestData.SalesReturnHeaderData.DocumentNo;
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

        public string SalesReturnDetailXML(List<SalesReturnDetail> SalesReturnDetail,Enums.RequestFrom RequestFrom)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (SalesReturnDetail objSalesReturnDetail in SalesReturnDetail)
            {
                sSql.Append("<SalesReturnDetail>");                
                sSql.Append("<ID>0</ID>");
                sSql.Append("<SalesReturnID>" + objSalesReturnDetail.SalesReturnID + "</SalesReturnID>");               
                sSql.Append("<SKUID>" + (objSalesReturnDetail.SKUID) + "</SKUID>");
                sSql.Append("<InvoiceDetailID>" + (objSalesReturnDetail.InvoiceDetailID) + "</InvoiceDetailID>");
                sSql.Append("<ItemCode>" + objSalesReturnDetail.ItemCode + "</ItemCode>");
                sSql.Append("<SKUCode>" + objSalesReturnDetail.SKUCode + "</SKUCode>");
                sSql.Append("<ReturnQty>" + objSalesReturnDetail.ReturnQty + "</ReturnQty>");
                sSql.Append("<ReturnAmount>" + objSalesReturnDetail.ReturnAmount + "</ReturnAmount>");
                sSql.Append("<CountryID>" + objSalesReturnDetail.CountryID + "</CountryID>");
                sSql.Append("<StoreID>" + objSalesReturnDetail.StoreID + "</StoreID>");
                sSql.Append("<PosID>" + objSalesReturnDetail.PosID + "</PosID>");
                sSql.Append("<CountryCode>" + objSalesReturnDetail.CountryCode + "</CountryCode>");
                sSql.Append("<StoreCode>" + objSalesReturnDetail.StoreCode + "</StoreCode>");
                sSql.Append("<PosCode>" + objSalesReturnDetail.PosCode + "</PosCode>");
                sSql.Append("<InvoiceSerialNo>" + objSalesReturnDetail.SerialNo + "</InvoiceSerialNo>");

                sSql.Append("<TaxID>" + objSalesReturnDetail.TaxID + "</TaxID>");
                sSql.Append("<TaxAmount>" + objSalesReturnDetail.TaxAmount + "</TaxAmount>");
                sSql.Append("<Tag_Id>" + objSalesReturnDetail.Tag_Id + "</Tag_Id>");
                //sSql.Append("<IsDataSyncToCountryServer>" + objSalesReturnDetail.IsDataSyncToCountryServer + "</IsDataSyncToCountryServer>");
                //sSql.Append("<IsDataSyncToMainServer>" + objSalesReturnDetail.IsDataSyncToMainServer + "</IsDataSyncToMainServer>");
                //sSql.Append("<CountryServerSyncTime>" + objSalesReturnDetail.CountryServerSyncTime + "</CountryServerSyncTime>");
                //sSql.Append("<MainServerSyncTime>" + objSalesReturnDetail.MainServerSyncTime + "</MainServerSyncTime>");
                //sSql.Append("<SyncFailedReason>" + objSalesReturnDetail.SyncFailedReason + "</SyncFailedReason>");
                sSql.Append("</SalesReturnDetail>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
            //return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
        }

        public string SalesReturnPaymentDetailsXML(List<PaymentDetail> SalesReturnPaymentDetails)
        {
            var sqlCommon = new MsSqlCommon();
            StringBuilder sSql = new StringBuilder();
            foreach (PaymentDetail objSalesReturnPaymentDetail in SalesReturnPaymentDetails)
            {
                sSql.Append("<PaymentDetail>");
                sSql.Append("<ID>0</ID>");
                sSql.Append("<SlNo>" + objSalesReturnPaymentDetail.SlNo + "</SlNo>");
                sSql.Append("<InvoiceHeaderID>" + (objSalesReturnPaymentDetail.InvoiceHeaderID) + "</InvoiceHeaderID>");
                sSql.Append("<InvoiceNumber>" + (objSalesReturnPaymentDetail.InvoiceNumber) + "</InvoiceNumber>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objSalesReturnPaymentDetail.BusinessDate) + "</BusinessDate>");
                sSql.Append("<FromCountryID>" + objSalesReturnPaymentDetail.FromCountryID + "</FromCountryID>");
                sSql.Append("<ReturnAmount>" + objSalesReturnPaymentDetail.ReturnAmount + "</ReturnAmount>");
                sSql.Append("<FromStoreID>" + objSalesReturnPaymentDetail.FromStoreID + "</FromStoreID>");
                sSql.Append("<Mode>" + objSalesReturnPaymentDetail.Mode + "</Mode>");
                sSql.Append("<PayCurrencyID>" + objSalesReturnPaymentDetail.PayCurrencyID + "</PayCurrencyID>");
                sSql.Append("<PayCurrency>" + objSalesReturnPaymentDetail.PayCurrency + "</PayCurrency>");
                sSql.Append("<ChangeCurrency>" + objSalesReturnPaymentDetail.ChangeCurrency + "</ChangeCurrency>");
                sSql.Append("<ChangeCurrencyID>" + objSalesReturnPaymentDetail.ChangeCurrencyID + "</ChangeCurrencyID>");
                sSql.Append("<ReceivedAmount>" + objSalesReturnPaymentDetail.Receivedamount + "</ReceivedAmount>");

                sSql.Append("<ReturnAmount>" + objSalesReturnPaymentDetail.ReturnAmount + "</ReturnAmount>");
                sSql.Append("<CardNo>" + objSalesReturnPaymentDetail.CardNo + "</CardNo>");
                sSql.Append("<CardHolder>" + objSalesReturnPaymentDetail.CardHolder + "</CardHolder>");
                sSql.Append("<ApproveNo>" + objSalesReturnPaymentDetail.ApproveNo + "</ApproveNo>");
                sSql.Append("<BalanceAmountToBePay>" + objSalesReturnPaymentDetail.BalanceAmountToBePay + "</BalanceAmountToBePay>");
                sSql.Append("<OnAccountReceiveAmount>" + objSalesReturnPaymentDetail.OnAccountReceiveAmount + "</OnAccountReceiveAmount>");
                sSql.Append("<PendingAmount>" + objSalesReturnPaymentDetail.PendingAmount + "</PendingAmount>");
                sSql.Append("<OnAcPaymentCompleted>" + objSalesReturnPaymentDetail.OnAcPaymentCompleted + "</OnAcPaymentCompleted>");
                sSql.Append("<FromSalesOrder>" + objSalesReturnPaymentDetail.FromSalesOrder + "</FromSalesOrder>");
                sSql.Append("<IsPaymentProcesser>" + objSalesReturnPaymentDetail.IsPaymentProcesser + "</IsPaymentProcesser>");
                sSql.Append("<BaseAmount>" + objSalesReturnPaymentDetail.BaseAmount + "</BaseAmount>");
                sSql.Append("<CardType2>" + objSalesReturnPaymentDetail.CardType2 + "</CardType2>");
                sSql.Append("</PaymentDetail>");
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SalesReturnHeaderList = new List<SalesReturnHeader>();
            var RequestData = (EasyBizRequest.Transactions.POS.Invoice.SelectByIDSalesReturnRequest)RequestObj;
            var ResponseData = new SelectByIDSalesReturnResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                long ID = 0;
                string sSql = "Select * from SalesReturnHeader with(NoLock) ";

                if (RequestData.ID > 0)
                {
                    sSql = sSql + " where ID={0}";
                    ID = RequestData.ID;
                    sSql = string.Format(sSql, ID);
                }
                else if (RequestData.DocumentNos != null && RequestData.DocumentNos != string.Empty)
                {
                    sSql = sSql + " where DocumentNo='{0}'";
                    sSql = string.Format(sSql, RequestData.DocumentNos.Trim());
                }
                else
                {
                    ID = Convert.ToInt64(RequestData.DocumentIDs);
                    sSql = sSql + " where ID={0}";
                    sSql = string.Format(sSql, ID);
                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesReturnHeaderTypes = new SalesReturnHeader();
                        objSalesReturnHeaderTypes.ID = Convert.ToInt32(objReader["ID"]);
                        objSalesReturnHeaderTypes.InvoiceHeaderID = objReader["InvoiceHeaderID"] != DBNull.Value ? Convert.ToInt64(objReader["InvoiceHeaderID"]) : 0;
                        objSalesReturnHeaderTypes.DocumentNo = objReader["DocumentNo"] != DBNull.Value ? Convert.ToString(objReader["DocumentNo"]) : string.Empty;
                        objSalesReturnHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesReturnHeaderTypes.SalesInvoiceNumber = objReader["SalesInvoiceNumber"] != DBNull.Value ? Convert.ToString(objReader["SalesInvoiceNumber"]) : string.Empty;
                        objSalesReturnHeaderTypes.SalesDate = objReader["SalesDate"] != DBNull.Value ? Convert.ToDateTime(objReader["SalesDate"]) : DateTime.Now;
                        objSalesReturnHeaderTypes.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objSalesReturnHeaderTypes.TotalReturnQty = Convert.ToInt32(objReader["TotalReturnQty"]);
                        objSalesReturnHeaderTypes.TotalReturnAmount = Convert.ToDecimal(objReader["TotalReturnAmount"]);
                        objSalesReturnHeaderTypes.PaymentMode = Convert.ToString(objReader["PaymentMode"]);
                        objSalesReturnHeaderTypes.ReturnMode = Convert.ToString(objReader["ReturnMode"]);
                        objSalesReturnHeaderTypes.CashierID = objReader["CashierID"] != DBNull.Value ? Convert.ToInt32(objReader["CashierID"]) : 0;
                        objSalesReturnHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesReturnHeaderTypes.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;
                        objSalesReturnHeaderTypes.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSalesReturnHeaderTypes.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objSalesReturnHeaderTypes.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) : 0;
                        objSalesReturnHeaderTypes.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objSalesReturnHeaderTypes.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objSalesReturnHeaderTypes.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objSalesReturnHeaderTypes.ReturnWithOutInvoiceNo = objReader["ReturnWithOutInvoiceNo"] != DBNull.Value ? Convert.ToBoolean(objReader["ReturnWithOutInvoiceNo"]) : false;
                        objSalesReturnHeaderTypes.CreditSales = objReader["CreditSales"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditSales"]) : false;

                        objSalesReturnHeaderTypes.SalesReturnDetailList = new List<SalesReturnDetail>();
                        var objSelectSalesReturnDetailsByIDRequest = new SelectSalesReturnDetailsByIDRequest();
                        var objSelectSalesReturnDetailsByIDResponse = new SelectSalesReturnDetailsByIDResponse();
                        objSelectSalesReturnDetailsByIDRequest.ID = objSalesReturnHeaderTypes.ID;
                        objSelectSalesReturnDetailsByIDRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectSalesReturnDetailsByIDRequest.ReturnWithOutInvoiceNo = objSalesReturnHeaderTypes.ReturnWithOutInvoiceNo;
                        objSelectSalesReturnDetailsByIDRequest.ReturnMode = objSalesReturnHeaderTypes.ReturnMode;

                        objSelectSalesReturnDetailsByIDResponse = SelectByIDSalesReturnDetails(objSelectSalesReturnDetailsByIDRequest);
                        objSalesReturnHeaderTypes.SalesReturnDetailList = new List<SalesReturnDetail>();
                        if (objSelectSalesReturnDetailsByIDResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesReturnHeaderTypes.SalesReturnDetailList = objSelectSalesReturnDetailsByIDResponse.SalesReturnDetailData;
                        }

                        if (RequestData.RequestFrom == Enums.RequestFrom.SyncService && RequestData.FromOrToStoreID == 0)
                        {
                            objSalesReturnHeaderTypes.TransactionLogList = TransactionLogList(objSalesReturnHeaderTypes.StoreID, objSalesReturnHeaderTypes.ID, RequestData);
                        }
                        else
                        {
                            objSalesReturnHeaderTypes.TransactionLogList = new List<TransactionLog>();
                        }

                        if(objSalesReturnHeaderTypes.CreditSales)
                        {
                            objSalesReturnHeaderTypes.OnAccountPaymentRecord = GetOnAccountPayment(RequestData);
                        }
                        else
                        {
                            objSalesReturnHeaderTypes.OnAccountPaymentRecord = null;
                        }

                        ResponseData.SalesReturnHeaderData = objSalesReturnHeaderTypes;
                        ResponseData.ResponseDynamicData = objSalesReturnHeaderTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Return Header");
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
            var SalesReturnHeaderList = new List<SalesReturnHeader>();
            var RequestData = (SelectAllSalesReturnRequest)RequestObj;
            var ResponseData = new SelectAllSalesReturnResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    string sSql = "Select SRH.*,cm.countryname,sm.storename,pm.posname from SalesReturnHeader SRH with(NoLock) left join countrymaster cm with(NoLock) on SRH.countryid=cm.id left join storemaster sm with(NoLock) on SRH.storeid=sm.id left join posmaster pm with(NoLock) on SRH.posid=pm.id Where SRH.Documentdate='" + RequestData.BusinessDate + "' and SRH.storeid ='" + RequestData.StoreID + "'";
                    sSql = string.Format(sSql);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                }
                else
                {
                    string sSql = "Select * from SalesReturnHeader with(NoLock) ";
                    sSql = string.Format(sSql);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                }               
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesReturnHeaderTypes = new SalesReturnHeader();
                        objSalesReturnHeaderTypes.ID = Convert.ToInt32(objReader["ID"]);
                        objSalesReturnHeaderTypes.InvoiceHeaderID = Convert.ToInt64(objReader["InvoiceHeaderID"]);
                        objSalesReturnHeaderTypes.DocumentNo = objReader["DocumentNo"].ToString();
                        objSalesReturnHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objSalesReturnHeaderTypes.SalesInvoiceNumber = objReader["SalesInvoiceNumber"].ToString();
                        objSalesReturnHeaderTypes.SalesDate = objReader["SalesDate"] != DBNull.Value ? Convert.ToDateTime(objReader["SalesDate"]) : DateTime.Now;
                        objSalesReturnHeaderTypes.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objSalesReturnHeaderTypes.TotalReturnQty = Convert.ToInt32(objReader["TotalReturnQty"]);
                        objSalesReturnHeaderTypes.TotalReturnAmount = Convert.ToDecimal(objReader["TotalReturnAmount"]);
                        objSalesReturnHeaderTypes.PaymentMode = objReader["PaymentMode"].ToString();
                        objSalesReturnHeaderTypes.ReturnMode = Convert.ToString(objReader["ReturnMode"]);                       
                        objSalesReturnHeaderTypes.CashierID = objReader["CashierID"] != DBNull.Value ? Convert.ToInt32(objReader["CashierID"]) : 0;
                        objSalesReturnHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesReturnHeaderTypes.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;
                        objSalesReturnHeaderTypes.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objSalesReturnHeaderTypes.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objSalesReturnHeaderTypes.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
                        objSalesReturnHeaderTypes.CreditSales = objReader["CreditSales"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditSales"]) : false;

                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objSalesReturnHeaderTypes.PosName = objReader["posname"] != DBNull.Value ? Convert.ToString(objReader["posname"]) : string.Empty;
                            objSalesReturnHeaderTypes.CountryName = objReader["countryname"] != DBNull.Value ? Convert.ToString(objReader["countryname"]) : string.Empty;
                            objSalesReturnHeaderTypes.StoreName = objReader["storename"] != DBNull.Value ? Convert.ToString(objReader["storename"]) : string.Empty;
                        }
                        var objSelectSalesReturnDetailsByIDRequest = new SelectSalesReturnDetailsByIDRequest();   
                        var objSelectSalesReturnDetailsByIDResponse = new SelectSalesReturnDetailsByIDResponse();
                         objSelectSalesReturnDetailsByIDRequest.ID = objSalesReturnHeaderTypes.ID;
                         objSelectSalesReturnDetailsByIDResponse = SelectByIDSalesReturnDetails(objSelectSalesReturnDetailsByIDRequest);
                         objSalesReturnHeaderTypes.SalesReturnDetailList = new List<SalesReturnDetail>();
                        if(objSelectSalesReturnDetailsByIDResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesReturnHeaderTypes.SalesReturnDetailList = objSelectSalesReturnDetailsByIDResponse.SalesReturnDetailData;
                        }


                        SalesReturnHeaderList.Add(objSalesReturnHeaderTypes);

                    }
                    ResponseData.SalesReturnHeaderList = SalesReturnHeaderList;
                    ResponseData.ResponseDynamicData = SalesReturnHeaderList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }


        public override EasyBizResponse.Transactions.POS.Invoice.SelectInvoiceReturnReceiptByInvoiceNumResponse GetInvoiceReturnReceipt(EasyBizRequest.Transactions.POS.Invoice.SelectInvoiceReturnReceiptByInvoiceNumRequest RequestObj)
        {
            var InvoiceReturnReceipt = new List<InvoiceReturnReceipt>();
            var RequestData = (SelectInvoiceReturnReceiptByInvoiceNumRequest)RequestObj;
            var ResponseData = new SelectInvoiceReturnReceiptByInvoiceNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetReturnDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNum);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReturnReceipt = new InvoiceReturnReceipt();

                        objInvoiceReturnReceipt.Currency = objReader["Currency"].ToString();
                        objInvoiceReturnReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objInvoiceReturnReceipt.ShopName = objReader["ShopName"].ToString();
                        objInvoiceReturnReceipt.TaxNo = objReader["TaxNo"].ToString();
                        objInvoiceReturnReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                        objInvoiceReturnReceipt.SalesMan = objReader["SalesMan"].ToString();
                        objInvoiceReturnReceipt.CustomerName = objReader["CustomerName"].ToString();
                        objInvoiceReturnReceipt.ItemCode = objReader["ItemCode"].ToString();
                        objInvoiceReturnReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objInvoiceReturnReceipt.SalesInvoice = objReader["SalesInvoice"] != DBNull.Value ? Convert.ToString(objReader["SalesInvoice"]) : string.Empty;
                        objInvoiceReturnReceipt.PosName = objReader["PosName"] != DBNull.Value ? Convert.ToString(objReader["PosName"]) : string.Empty;
                        objInvoiceReturnReceipt.item_tax = objReader["item_tax"] != DBNull.Value ? Convert.ToDecimal(objReader["item_tax"]) : 0;
                        objInvoiceReturnReceipt.POSTitle = objReader["POSTitle"].ToString();
                        objInvoiceReturnReceipt.item_total = objReader["item_total"] != DBNull.Value ? Convert.ToDecimal(objReader["item_total"]) : 0;
                        objInvoiceReturnReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                        objInvoiceReturnReceipt.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                        objInvoiceReturnReceipt.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objInvoiceReturnReceipt.Discount = objReader["Discount"] != DBNull.Value ? Convert.ToDecimal(objReader["Discount"]) : 0;
                        objInvoiceReturnReceipt.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
                        objInvoiceReturnReceipt.Footer = objReader["Footer"].ToString();
                        objInvoiceReturnReceipt.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
                        objInvoiceReturnReceipt.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                        objInvoiceReturnReceipt.ArabicDetails = objReader["ArabicDetails"].ToString();                       
                        objInvoiceReturnReceipt.Date = objReader["Date"] != DBNull.Value ? Convert.ToDateTime(objReader["Date"]) : DateTime.Now;
                        objInvoiceReturnReceipt.Time = objReader["Time"] != DBNull.Value ? Convert.ToDateTime(objReader["Time"]) : DateTime.Now;
                        objInvoiceReturnReceipt.CustomerBalance = objReader["CustomerBalance"] != DBNull.Value ? Convert.ToDecimal(objReader["CustomerBalance"]) : 0;
                        objInvoiceReturnReceipt.CASH = objReader["Cash"] != DBNull.Value ? Convert.ToDecimal(objReader["Cash"]) : 0;
                        objInvoiceReturnReceipt.KNET = objReader["KNet"] != DBNull.Value ? Convert.ToDecimal(objReader["KNet"]) : 0;
                        objInvoiceReturnReceipt.VISA = objReader["Visa"] != DBNull.Value ? Convert.ToDecimal(objReader["Visa"]) : 0;
                        objInvoiceReturnReceipt.GrossAmt = objReader["GrossAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["GrossAmt"]) : 0;
                        objInvoiceReturnReceipt.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;
                        InvoiceReturnReceipt.Add(objInvoiceReturnReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InvoiceReturnList = InvoiceReturnReceipt;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Return Print");
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
        public List<TransactionLog> TransactionLogList(int StoreID, long DocumentID, EasyBizRequest.Transactions.POS.Invoice.SelectByIDSalesReturnRequest RequestData)
        {
            var _TransactionLogList = new List<TransactionLog>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from TransactionLog with(NoLock) where StoreID={0} and DocumentID={1} and TransactionType='SalesReturn'";
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
        private string OnAccountPaymentDetailsXml(List<OnAccountPaymentDetails> OnAccountPaymentDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (OnAccountPaymentDetails objOnAccountPaymentDetails in OnAccountPaymentDetailsList)
            {
                sSql.Append("<OnAccountPaymentDetails>");
                sSql.Append("<ID>" + (objOnAccountPaymentDetails.ID) + "</ID>");
                sSql.Append("<StoreID>" + (objOnAccountPaymentDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objOnAccountPaymentDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<PaymentCurrency>" + objOnAccountPaymentDetails.PaymentCurrency + "</PaymentCurrency>");
                sSql.Append("<ChangeCurrency>" + objOnAccountPaymentDetails.ChangeCurrency + "</ChangeCurrency>");
                sSql.Append("<PaymentType>" + (objOnAccountPaymentDetails.PaymentType) + "</PaymentType>");
                sSql.Append("<CardType>" + objOnAccountPaymentDetails.CardType + "</CardType>");
                sSql.Append("<CardNumber>" + (objOnAccountPaymentDetails.CardNumber) + "</CardNumber>");
                sSql.Append("<CardHolderName>" + objOnAccountPaymentDetails.CardHolderName + "</CardHolderName>");
                sSql.Append("<ApprovalNumber>" + objOnAccountPaymentDetails.ApprovalNumber + "</ApprovalNumber>");
                sSql.Append("<ReceivedAmount>" + (objOnAccountPaymentDetails.ReceivedAmount) + "</ReceivedAmount>");
                sSql.Append("<Remarks>" + (objOnAccountPaymentDetails.Remarks) + "</Remarks>");
                sSql.Append("</OnAccountPaymentDetails>");
            }
            return sSql.ToString();
        }
        private string OnAcInvoiceWisePaymentXml(List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (OnAcInvoiceWisePayment objOnAcInvoiceWisePayment in OnAcInvoiceWisePaymentList)
            {
                sSql.Append("<OnAcInvoiceWisePayment>");
                sSql.Append("<ID>" + (objOnAcInvoiceWisePayment.ID) + "</ID>");
                sSql.Append("<OnAccountPaymentID>" + (objOnAcInvoiceWisePayment.OnAccountPaymentID) + "</OnAccountPaymentID>");
                sSql.Append("<PurchaseStoreCode>" + (objOnAcInvoiceWisePayment.PurchaseStoreCode) + "</PurchaseStoreCode>");
                sSql.Append("<InvoiceNo>" + objOnAcInvoiceWisePayment.InvoiceNo + "</InvoiceNo>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objOnAcInvoiceWisePayment.BusinessDate) + "</BusinessDate>");
                sSql.Append("<CustomerCode>" + objOnAcInvoiceWisePayment.CustomerCode + "</CustomerCode>");
                sSql.Append("<BillAmount>" + objOnAcInvoiceWisePayment.BillAmount + "</BillAmount>");
                sSql.Append("<CashPaid>" + objOnAcInvoiceWisePayment.CashPaid + "</CashPaid>");
                sSql.Append("<CardPaid>" + objOnAcInvoiceWisePayment.CardPaid + "</CardPaid>");
                sSql.Append("<TotalPaid>" + objOnAcInvoiceWisePayment.TotalPaid + "</TotalPaid>");
                sSql.Append("<PendingAmount>" + objOnAcInvoiceWisePayment.PendingAmount + "</PendingAmount>");
                sSql.Append("<DiscountAmount>" + objOnAcInvoiceWisePayment.DiscountAmount + "</DiscountAmount>");
                sSql.Append("<PaidAmount>" + (objOnAcInvoiceWisePayment.PaidAmount) + "</PaidAmount>");
                sSql.Append("<CloseBill>" + (objOnAcInvoiceWisePayment.CloseBill) + "</CloseBill>");
                sSql.Append("<Remarks>" + (objOnAcInvoiceWisePayment.Remarks) + "</Remarks>");
                sSql.Append("</OnAcInvoiceWisePayment>");
            }
            return sSql.ToString();
        }
        private OnAccountPayment GetOnAccountPayment(EasyBizRequest.Transactions.POS.Invoice.SelectByIDSalesReturnRequest RequestData)
        {
            var _OnAccountPayment = new OnAccountPayment();           
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("Select * from OnAccountPayment where SalesReturnDocumentNo='" + RequestData.DocumentNumber + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        _OnAccountPayment.Active= objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        _OnAccountPayment.BillingAmount = objReader["BillingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillingAmount"]) : 0;
                        _OnAccountPayment.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        _OnAccountPayment.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        _OnAccountPayment.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        _OnAccountPayment.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        _OnAccountPayment.PaymentDate = objReader["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["PaymentDate"]) : DateTime.Now;
                        _OnAccountPayment.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        _OnAccountPayment.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : string.Empty;
                        _OnAccountPayment.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        _OnAccountPayment.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        _OnAccountPayment.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        _OnAccountPayment.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt64(objReader["UpdateBy"]) : 0;
                        _OnAccountPayment.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;

                        _OnAccountPayment.OnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
                        _OnAccountPayment.OnAccountPaymentDetailsList = OnAccountPaymentDetailsList(_OnAccountPayment.ID, RequestData);

                        _OnAccountPayment.OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
                        _OnAccountPayment.OnAcInvoiceWisePaymentList = OnAcInvoiceWisePaymentList(_OnAccountPayment.ID, RequestData);
                    }                    
                }
            }
            catch (Exception ex)
            {
                _OnAccountPayment = null;
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }   
            return _OnAccountPayment;
        }
        private List<OnAccountPaymentDetails> OnAccountPaymentDetailsList(int OnAccountPaymentID,EasyBizRequest.Transactions.POS.Invoice.SelectByIDSalesReturnRequest RequestData)
        {
            var _OnAccountPaymentDetailsList = new List<OnAccountPaymentDetails>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("Select * from OnAccountPaymentDetails where OnAccountPaymentID=" + OnAccountPaymentID, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var _OnAccountPaymentDetails = new OnAccountPaymentDetails();
                        _OnAccountPaymentDetails.OnAccountPaymentID = objReader["OnAccountPaymentID"] != DBNull.Value ? Convert.ToInt32(objReader["OnAccountPaymentID"]) : 0;
                        _OnAccountPaymentDetails.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        _OnAccountPaymentDetails.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        _OnAccountPaymentDetails.PaymentType = objReader["PaymentType"] != DBNull.Value ? Convert.ToString(objReader["PaymentType"]) : string.Empty;
                        _OnAccountPaymentDetails.PaymentCurrency = objReader["PaymentCurrency"] != DBNull.Value ? Convert.ToString(objReader["PaymentCurrency"]) : string.Empty;
                        _OnAccountPaymentDetails.ChangeCurrency = objReader["ChangeCurrency"] != DBNull.Value ? Convert.ToString(objReader["ChangeCurrency"]) : string.Empty;
                        _OnAccountPaymentDetails.CardType = objReader["CardType"] != DBNull.Value ? Convert.ToString(objReader["CardType"]) : string.Empty;
                        _OnAccountPaymentDetails.CardNumber = objReader["CardNumber"] != DBNull.Value ? Convert.ToString(objReader["CardNumber"]) : string.Empty;
                        _OnAccountPaymentDetails.CardHolderName = objReader["CardHolderName"] != DBNull.Value ? Convert.ToString(objReader["CardHolderName"]) : string.Empty;
                        _OnAccountPaymentDetails.ApprovalNumber = objReader["ApprovalNumber"] != DBNull.Value ? Convert.ToString(objReader["ApprovalNumber"]) : string.Empty;
                        _OnAccountPaymentDetails.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                       
                        _OnAccountPaymentDetailsList.Add(_OnAccountPaymentDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return _OnAccountPaymentDetailsList;
        }

        private List<OnAcInvoiceWisePayment> OnAcInvoiceWisePaymentList(int OnAccountPaymentID, EasyBizRequest.Transactions.POS.Invoice.SelectByIDSalesReturnRequest RequestData)
        {
            var _OnAcInvoiceWisePaymentList = new List<OnAcInvoiceWisePayment>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("Select * from OnAcInvoiceWisePayment where OnAccountPaymentID=" + OnAccountPaymentID, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var _OnAccountPaymentDetails = new OnAcInvoiceWisePayment();
                        _OnAccountPaymentDetails.OnAccountPaymentID = objReader["OnAccountPaymentID"] != DBNull.Value ? Convert.ToInt32(objReader["OnAccountPaymentID"]) : 0;                        
                        _OnAccountPaymentDetails.PurchaseStoreCode = objReader["PurchaseStoreCode"] != DBNull.Value ? Convert.ToString(objReader["PurchaseStoreCode"]) : string.Empty;
                        _OnAccountPaymentDetails.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
                        _OnAccountPaymentDetails.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        _OnAccountPaymentDetails.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
                        _OnAccountPaymentDetails.BillAmount = objReader["BillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BillAmount"]) : 0;
                        _OnAccountPaymentDetails.CashPaid = objReader["CashPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CashPaid"]) : 0;
                        _OnAccountPaymentDetails.CardPaid = objReader["CardPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["CardPaid"]) : 0;
                        _OnAccountPaymentDetails.TotalPaid = objReader["TotalPaid"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalPaid"]) : 0;
                        _OnAccountPaymentDetails.PendingAmount = objReader["PendingAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PendingAmount"]) : 0;
                        _OnAccountPaymentDetails.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                        _OnAccountPaymentDetails.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;
                        _OnAccountPaymentDetails.CloseBill = objReader["CloseBill"] != DBNull.Value ? Convert.ToBoolean(objReader["CloseBill"]) : false;
                        _OnAccountPaymentDetails.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        _OnAccountPaymentDetails.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;                        
                        _OnAcInvoiceWisePaymentList.Add(_OnAccountPaymentDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return _OnAcInvoiceWisePaymentList;
        }
    }
}
