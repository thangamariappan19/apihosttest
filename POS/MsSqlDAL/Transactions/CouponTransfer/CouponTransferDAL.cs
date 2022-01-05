using EasyBizAbsDAL.Transactions.CouponTransfer;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest;
using EasyBizRequest.Transactions.CouponTransfer;
using EasyBizResponse;
using EasyBizResponse.Transactions.CouponTransfer;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.CouponTransfer
{
    public class CouponTransferDAL : BaseCouponTransferDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.Transactions.CouponTransfer.SelectCouponTransferLookUpResponse SelectCouponMasterLookUp(EasyBizRequest.Transactions.CouponTransfer.SelectCouponTransferLookUpRequest RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCouponTransferRequest)RequestObj;
            var ResponseData = new SaveCouponTransferResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateCouponTransfer", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter AgentID = _CommandObj.Parameters.Add("@CouponTransferID", SqlDbType.Int);
                AgentID.Direction = ParameterDirection.Input;
                AgentID.Value = RequestData.CouponTransferRecord.ID;

                SqlParameter CouponCode = _CommandObj.Parameters.Add("@CouponCode", SqlDbType.NVarChar);
                CouponCode.Direction = ParameterDirection.Input;
                CouponCode.Value = RequestData.CouponTransferRecord.CouponCode;

                SqlParameter CouponID = _CommandObj.Parameters.Add("@CouponID", SqlDbType.Int);
                CouponID.Direction = ParameterDirection.Input;
                CouponID.Value = RequestData.CouponTransferRecord.CouponID;

                SqlParameter FromCountryCode = _CommandObj.Parameters.Add("@FromCountryCode", SqlDbType.NVarChar);
                FromCountryCode.Direction = ParameterDirection.Input;
                FromCountryCode.Value = RequestData.CouponTransferRecord.FromCountryCode;

                SqlParameter FromCountryID = _CommandObj.Parameters.Add("@FromCountryID", SqlDbType.Int);
                FromCountryID.Direction = ParameterDirection.Input;
                FromCountryID.Value = RequestData.CouponTransferRecord.FromCountryID;

                SqlParameter ToStoreCode = _CommandObj.Parameters.Add("@ToStoreCode", SqlDbType.NVarChar);
                ToStoreCode.Direction = ParameterDirection.Input;
                ToStoreCode.Value = RequestData.CouponTransferRecord.ToStoreCode;

                SqlParameter ToStoreID = _CommandObj.Parameters.Add("@ToStoreID", SqlDbType.Int);
                ToStoreID.Direction = ParameterDirection.Input;
                ToStoreID.Value = RequestData.CouponTransferRecord.ToStoreID;

                SqlParameter FromSerialNum = _CommandObj.Parameters.Add("@FromSerialNum", SqlDbType.NVarChar);
                FromSerialNum.Direction = ParameterDirection.Input;
                FromSerialNum.Value = RequestData.CouponTransferRecord.FromSerialNum;

                SqlParameter ToSerialNum = _CommandObj.Parameters.Add("@ToSerialNum", SqlDbType.NVarChar);
                ToSerialNum.Direction = ParameterDirection.Input;
                ToSerialNum.Value = RequestData.CouponTransferRecord.ToSerialNum;

                SqlParameter Fromloaction = _CommandObj.Parameters.Add("@Fromloaction", SqlDbType.NVarChar);
                Fromloaction.Direction = ParameterDirection.Input;
                Fromloaction.Value = RequestData.CouponTransferRecord.Fromloaction;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CouponTransferRecord.Active;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CouponTransferRecord.CreateBy;

                SqlParameter CouponReceiptDetails = _CommandObj.Parameters.Add("@CouponTransferDetails", SqlDbType.Xml);
                CouponReceiptDetails.Direction = ParameterDirection.Input;
                CouponReceiptDetails.Value = CouponTransferDetailMasterXML(RequestData.CouponReceiptDetailsList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon Transfer");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon Transfer");
                }
                else
                {
                    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Transfer");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string CouponTransferDetailMasterXML(List<CouponReceiptDetails> CouponReceiptDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (CouponReceiptDetails objCouponReceiptDetails in CouponReceiptDetailsList)
            {
                //if (objCouponReceiptDetails.PhysicalStore == "MainServer")
                //{
                    sSql.Append("<CouponTransferDetailsData>");
                    sSql.Append("<ID>" + objCouponReceiptDetails.ID + "</ID>");
                    sSql.Append("<CouponReceiptHeaderID>" + objCouponReceiptDetails.CouponReceiptHeaderID + "</CouponReceiptHeaderID>");
                    sSql.Append("<CouponSerialCode>" + objCouponReceiptDetails.CouponSerialCode + "</CouponSerialCode>");
                    sSql.Append("<IssuedStatus>" + objCouponReceiptDetails.IssuedStatus + "</IssuedStatus>");
                    sSql.Append("<RedeemedStatus>" + (objCouponReceiptDetails.RedeemedStatus) + "</RedeemedStatus>");
                    sSql.Append("<PhysicalStore>" + (objCouponReceiptDetails.PhysicalStore) + "</PhysicalStore>");
                    sSql.Append("<ToStore>" + (objCouponReceiptDetails.ToStore) + "</ToStore>");
                    sSql.Append("<SCN>" + (objCouponReceiptDetails.SCN) + "</SCN>");
                    sSql.Append("</CouponTransferDetailsData>");
                //}
            }
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
            var CouponTransferRecord = new CouponTransferMaster();
            var RequestData = (SelectByIDCouponTransferRequest)RequestObj;
            var ResponseData = new SelectByIDCouponTransferResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from CouponTransfer  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponTransfer = new CouponTransferMaster();
                        objCouponTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponTransfer.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        objCouponTransfer.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponTransfer.FromCountryID = objReader["FromCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["FromCountryID"]) : 0;
                        objCouponTransfer.FromCountryCode = Convert.ToString(objReader["FromCountryCode"]);
                        objCouponTransfer.ToStoreID = objReader["ToStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["ToStoreID"]) : 0;
                        objCouponTransfer.ToStoreCode = Convert.ToString(objReader["ToStoreCode"]);
                        objCouponTransfer.FromSerialNum = Convert.ToString(objReader["FromSerialNum"]);
                        objCouponTransfer.ToSerialNum = Convert.ToString(objReader["ToSerialNum"]);
                        objCouponTransfer.Fromloaction = Convert.ToString(objReader["Fromloaction"]);
                        objCouponTransfer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponTransfer.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponTransfer.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;


                        objCouponTransfer.CouponTransferDetailsList = new List<CouponReceiptDetails>();

                        SelectByCouponTransferDetailsRequest objSelectByCouponTransferDetailsRequest = new SelectByCouponTransferDetailsRequest();
                        SelectByCouponTransferDetailsResponse objSelectByCouponTransferDetailsResponse = new SelectByCouponTransferDetailsResponse();
                        objSelectByCouponTransferDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByCouponTransferDetailsResponse = SelectByCouponTransferDetails(objSelectByCouponTransferDetailsRequest);
                        if (objSelectByCouponTransferDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCouponTransfer.CouponTransferDetailsList = objSelectByCouponTransferDetailsResponse.CouponTransferDetailsRecord;
                        }



                        ResponseData.CouponTransferMasterRecord = objCouponTransfer;

                        //ResponseData.ResponseDynamicData = objCouponTransfer;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Transfer Master");
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
        public override SelectByCouponTransferDetailsResponse SelectByCouponTransferDetails(SelectByCouponTransferDetailsRequest ObjRequest)
        {
            var CouponTransferDetailsList = new List<CouponReceiptDetails>();
            var RequestData = (SelectByCouponTransferDetailsRequest)ObjRequest;
            var ResponseData = new SelectByCouponTransferDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from CouponTransferDetails ");
                sSql.Append("where  CouponTransferHeaderID=" + RequestData.ID + " ");
                sSql.Append("order by id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponReceiptDetails = new CouponReceiptDetails();
                        objCouponReceiptDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponReceiptDetails.CouponTransferHeaderID = objReader["CouponTransferHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponTransferHeaderID"]) : 0;

                        objCouponReceiptDetails.CouponSerialCode = Convert.ToString(objReader["CouponSerialCode"]);
                        objCouponReceiptDetails.IssuedStatus = objReader["IssuedStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuedStatus"]) : false;
                        objCouponReceiptDetails.RedeemedStatus = objReader["RedeemedStatus"] != DBNull.Value ? Convert.ToBoolean(objReader["RedeemedStatus"]) : false;
                        objCouponReceiptDetails.PhysicalStore = Convert.ToString(objReader["PhysicalStore"]);
                        objCouponReceiptDetails.IsSaved = objReader["IsSaved"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSaved"]) : true;


                        CouponTransferDetailsList.Add(objCouponReceiptDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponTransferDetailsRecord = CouponTransferDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CouponTransfer");
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
            var CouponTransferList = new List<CouponTransferMaster>();
            var RequestData = (SelectAllCouponTransferRequest)RequestObj;
            var ResponseData = new SelectAllCouponTransferResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);               
                string sSql = "Select * from CouponTransfer  ";



                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponTransfer = new CouponTransferMaster();
                        objCouponTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponTransfer.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        objCouponTransfer.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponTransfer.FromCountryID = objReader["FromCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["FromCountryID"]) : 0;
                        objCouponTransfer.FromCountryCode = Convert.ToString(objReader["FromCountryCode"]);
                        objCouponTransfer.ToStoreID = objReader["ToStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["ToStoreID"]) : 0;
                        objCouponTransfer.ToStoreCode = Convert.ToString(objReader["ToStoreCode"]);
                        objCouponTransfer.FromSerialNum = Convert.ToString(objReader["FromSerialNum"]);
                        objCouponTransfer.ToSerialNum = Convert.ToString(objReader["ToSerialNum"]);
                        objCouponTransfer.Fromloaction = Convert.ToString(objReader["Fromloaction"]);
                        objCouponTransfer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;                       
                        objCouponTransfer.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;                                           
                        objCouponTransfer.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        CouponTransferList.Add(objCouponTransfer);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponTransferMasterList = CouponTransferList;

                    ResponseData.ResponseDynamicData = CouponTransferList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Transfer Master");
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

        public override SaveCouponTransactionResponse SaveCouponTransactionDetails(SaveCouponTransactionRequest RequestObj)
        {
            var RequestData = (SaveCouponTransactionRequest)RequestObj;
            var ResponseData = new SaveCouponTransactionResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertCouponTransactionLog", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                //_CommandObj.CommandTimeout = 300000;

                SqlParameter TransactionLogDetails = _CommandObj.Parameters.Add("@CouponTransactionLog", SqlDbType.Xml);
                TransactionLogDetails.Direction = ParameterDirection.Input;
                TransactionLogDetails.Value = CouponTransactionLogDetailXML(RequestData.CouponTransactionList);

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
        public string CouponTransactionLogDetailXML(List<CouponTransaction> CouponTransactionList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (CouponTransaction objCouponTransactionDetails in CouponTransactionList)
            {
                sSql.Append("<CouponTransactionLogDetailsData>");
                sSql.Append("<ID>" + objCouponTransactionDetails.ID + "</ID>");
                sSql.Append("<CouponID>" + objCouponTransactionDetails.CouponID + "</CouponID>");
                sSql.Append("<CouponCode>" + (objCouponTransactionDetails.CouponCode) + "</CouponCode>");
                sSql.Append("<FromLocation>" + objCouponTransactionDetails.FromLocation + "</FromLocation>");
                sSql.Append("<DocumentID>" + objCouponTransactionDetails.DocumentID + "</DocumentID>");
                sSql.Append("<CouponSerialCode>" + objCouponTransactionDetails.CouponSerialCode + "</CouponSerialCode>");
                sSql.Append("<IssuedStatus>" + objCouponTransactionDetails.IssuedStatus + "</IssuedStatus>");
                sSql.Append("<PhysicalStore>" + objCouponTransactionDetails.PhysicalStore + "</PhysicalStore>");
                sSql.Append("<RedeemedStatus>" + (objCouponTransactionDetails.RedeemedStatus) + "</RedeemedStatus>");
                sSql.Append("<IsSaved>" + (objCouponTransactionDetails.IsSaved) + "</IsSaved>");
                sSql.Append("<ToStore>" + (objCouponTransactionDetails.ToStore) + "</ToStore>");

                sSql.Append("<TransactionDate>" + sqlCommon.GetSQLServerDateString(objCouponTransactionDetails.TransactionDate) + "</TransactionDate>");
                sSql.Append("</CouponTransactionLogDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override SelectAllCouponTransferResponse API_SelectALL(SelectAllCouponTransferRequest RequestObj)
        {
            var CouponTransferList = new List<CouponTransferMaster>();
            var RequestData = (SelectAllCouponTransferRequest)RequestObj;
            var ResponseData = new SelectAllCouponTransferResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            string sSql = string.Empty;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sbSql.Append("Select ID,CouponID,CouponCode,FromCountryID,FromCountryCode,ToStoreID,ToStoreCode,FromSerialNum,ToSerialNum,Active,Fromloaction,CreateBy,SCN, RC.TOTAL_CNT [RecordCount] from CouponTransfer ");
                sbSql.Append("LEFT JOIN(Select  count(CT.ID) As TOTAL_CNT From CouponTransfer CT with(NoLock) ");
                sbSql.Append(" where CT.Active = " + RequestData.IsActive + " ");
                sbSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sbSql.Append("or CT.CouponCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or CT.FromCountryCode like isnull('%" + RequestData.SearchString + "%','')");
                sbSql.Append("or CT.ToStoreCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or CT.FromSerialNum like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or CT.ToSerialNum like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or CT.Fromloaction like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                sbSql.Append(" where Active = " + RequestData.IsActive + " ");
                sbSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sbSql.Append("or CouponCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or FromCountryCode like isnull('%" + RequestData.SearchString + "%','')");
                sbSql.Append("or ToStoreCode like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or FromSerialNum like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or ToSerialNum like isnull('%" + RequestData.SearchString + "%','') ");
                sbSql.Append("or Fromloaction like isnull('%" + RequestData.SearchString + "%','')) ");
                sbSql.Append("order by CouponCode asc ");
                sbSql.Append("offset " + RequestData.Offset + " rows ");
                sbSql.Append("fetch first " + RequestData.Limit + " rows only");

                _CommandObj = new SqlCommand(sbSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponTransfer = new CouponTransferMaster();
                        objCouponTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponTransfer.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        objCouponTransfer.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponTransfer.FromCountryID = objReader["FromCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["FromCountryID"]) : 0;
                        objCouponTransfer.FromCountryCode = Convert.ToString(objReader["FromCountryCode"]);
                        objCouponTransfer.ToStoreID = objReader["ToStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["ToStoreID"]) : 0;
                        objCouponTransfer.ToStoreCode = Convert.ToString(objReader["ToStoreCode"]);
                        objCouponTransfer.FromSerialNum = Convert.ToString(objReader["FromSerialNum"]);
                        objCouponTransfer.ToSerialNum = Convert.ToString(objReader["ToSerialNum"]);
                        objCouponTransfer.Fromloaction = Convert.ToString(objReader["Fromloaction"]);
                        objCouponTransfer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponTransfer.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponTransfer.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        CouponTransferList.Add(objCouponTransfer);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponTransferMasterList = CouponTransferList;

                    ResponseData.ResponseDynamicData = CouponTransferList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Transfer Master");
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
