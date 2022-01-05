using EasyBizAbsDAL.FCPasses;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.FCPasses;
using EasyBizRequest;
using EasyBizRequest.FCPasses;
using EasyBizResponse;
using EasyBizResponse.FCPasses;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.FCPasses
{
    public class PassesTransactionDAL : BasePassesTransactionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var PassesTransactionList = new List<PassesTransaction>();
            var RequestData = (PassesTransactionRequest)RequestObj;
            var ResponseData = new PassesTransactionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select PTH.ID, PTH.PassCode,PM.ValidFrom,PM.ValidTo,PTh.SendEmail,PTH.SendWhatsapp" +
                    " , PTH.SendText,PM.Active,RecordCount = COUNT(*) OVER() from PassesTransactionheader PTH" +
                    " JOIN PassMaster PM ON PTH.PassCode = PM.PassCode   ";





                //sQuery = "Select ID, PassCode, PassName, Points, CardType, ValidFrom, ValidTo" +
                //    ", ScanMethod, Notes, IsOneTimePass, Active " +
                //    "from PassMaster with(NoLock) " +
                //    "where 1=1 " +
                //        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //        "or PassCode = isnull('" + RequestData.SearchString + "','') " +
                //        "or PassName = isnull('" + RequestData.SearchString + "','') " +
                //        "or convert(varchar(50),Points) = isnull('" + RequestData.SearchString + "','')) " +
                //        "or CardType = isnull('" + RequestData.SearchString + "','') " +
                //        "or ScanMethod = isnull('" + RequestData.SearchString + "','') " +
                //        "or Notes = isnull('" + RequestData.SearchString + "','') ";


                //if (!string.IsNullOrWhiteSpace(RequestData.IsActive))
                //    sQuery = sQuery + " and Active = " + RequestData.IsActive;

                sQuery = sQuery + " order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPassesTransaction = new PassesTransaction();
                        objPassesTransaction.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPassesTransaction.PassCode = objReader["PassCode"] != DBNull.Value ? Convert.ToString(objReader["PassCode"]) : "";

                        objPassesTransaction.ValidFrom = objReader["ValidFrom"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidFrom"]) : DateTime.Now;
                        objPassesTransaction.ValidTo = objReader["ValidTo"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidTo"]) : DateTime.Now;
                        objPassesTransaction.EmailSend = objReader["SendEmail"] != DBNull.Value ? Convert.ToBoolean(objReader["SendEmail"]) : true;
                        objPassesTransaction.WhatsappSend = objReader["SendWhatsapp"] != DBNull.Value ? Convert.ToBoolean(objReader["SendWhatsapp"]) : true;
                        objPassesTransaction.TextSend = objReader["SendText"] != DBNull.Value ? Convert.ToBoolean(objReader["SendText"]) : true;
                        objPassesTransaction.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        PassesTransactionList.Add(objPassesTransaction);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PassesTransactionResponseList = PassesTransactionList;
                    //ResponseData.ResponseDynamicData = CityList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Passes Transaction");
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


        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (PassesTransactionRequest)RequestObj;
            var ResponseData = new PassesTransactionResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertPassesTransaction", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var PassID = _CommandObj.Parameters.Add("@PassID", SqlDbType.BigInt);
                PassID.Direction = ParameterDirection.Input;
                PassID.Value = RequestData.PassesTransactionHeaderData.PassID;

                var PassCode = _CommandObj.Parameters.Add("@PassCode", SqlDbType.NVarChar);
                PassCode.Direction = ParameterDirection.Input;
                PassCode.Value = RequestData.PassesTransactionHeaderData.PassCode;

                //var PassName = _CommandObj.Parameters.Add("@PassName", SqlDbType.NVarChar);
                //PassName.Direction = ParameterDirection.Input;
                //PassName.Value = RequestData.PassesTransactionHeaderData.PassName;

                _CommandObj.Parameters.AddWithValue("@SendEmail", RequestData.PassesTransactionHeaderData.EmailSend);
                _CommandObj.Parameters.AddWithValue("@SendWhatsapp", RequestData.PassesTransactionHeaderData.WhatsappSend);
                _CommandObj.Parameters.AddWithValue("@SendText", RequestData.PassesTransactionHeaderData.TextSend);

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.PassesTransactionHeaderData.DocumentDate);

                SqlParameter PassesTransaction = _CommandObj.Parameters.Add("@PassesTransactionDetails", SqlDbType.Xml);
                PassesTransaction.Direction = ParameterDirection.Input;
                PassesTransaction.Value = PassesTransactionDetailsXML(RequestData.PassesTransactionDetailsList);

                //var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                //CreateBy.Direction = ParameterDirection.Input;
                //CreateBy.Value = RequestData.PassMasterRequestData.CreateBy;


                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Passes Transaction");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = "Passes Transacion Already Exists.";
                }
                else
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Passes Transacion");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        private string PassesTransactionDetailsXML(List<PassesTransactionDetails> passesTransactionDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (PassesTransactionDetails objPassesTransactionDetails in passesTransactionDetailsList)
            {
                sSql.Append("<PassesTransactionDetailsData>");
                sSql.Append("<ID>" + objPassesTransactionDetails.ID + "</ID>");
                sSql.Append("<HeaderID>" + objPassesTransactionDetails.HeaderID + "</HeaderID>");
                sSql.Append("<CustomerID>" + objPassesTransactionDetails.CustomerID + "</CustomerID>");
                sSql.Append("<CustomerCode>" + (objPassesTransactionDetails.CustomerCode) + "</CustomerCode>");
                sSql.Append("<PassRefNo>" + objPassesTransactionDetails.PassRefNo + "</PassRefNo>");
                sSql.Append("<IsSent>" + (objPassesTransactionDetails.IsSent) + "</IsSent>");
                sSql.Append("<IsUsed>" + (objPassesTransactionDetails.IsUsed) + "</IsUsed>");
                sSql.Append("<IsFCSync>" + (objPassesTransactionDetails.IsFCSync) + "</IsFCSync>");
                sSql.Append("</PassesTransactionDetailsData>");

            }
            //return sSql.ToString().Replace("&", "&#38;");
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString();
        }
        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var PassesTransactionList = new List<PassesTransaction>();
            var RequestData = (SelectPassesTransactionRequest)RequestObj;
            var ResponseData = new SelectPassesTransactionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from PassesTransactionHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPassesTransaction = new PassesTransaction();
                        objPassesTransaction.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPassesTransaction.PassID = objReader["PassID"] != DBNull.Value ? Convert.ToInt32(objReader["PassID"]) : 0;
                        objPassesTransaction.PassCode = objReader["PassCode"] != DBNull.Value ? Convert.ToString(objReader["PassCode"]) : "";
                        //objPassesTransaction.ValidFrom = objReader["ValidFrom"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidFrom"]) : DateTime.Now;
                        //objPassesTransaction.ValidTo = objReader["ValidTo"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidTo"]) : DateTime.Now;
                        objPassesTransaction.EmailSend = objReader["SendEmail"] != DBNull.Value ? Convert.ToBoolean(objReader["SendEmail"]) : true;
                        objPassesTransaction.WhatsappSend = objReader["SendWhatsapp"] != DBNull.Value ? Convert.ToBoolean(objReader["SendWhatsapp"]) : true;
                        objPassesTransaction.TextSend = objReader["SendText"] != DBNull.Value ? Convert.ToBoolean(objReader["SendText"]) : true;
                        //objPassesTransaction.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPassesTransaction.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;

                        PassesTransactionList.Add(objPassesTransaction);

                        objPassesTransaction.PassesTransactionDetailsList = new List<PassesTransactionDetails>();

                        SelectPassesTransactionRequest objSelectByPassesTransactionDetailsRequest = new SelectPassesTransactionRequest();
                        SelectPassesTransactionResponse objSelectByPassesTransactionDetailsResponse = new SelectPassesTransactionResponse();
                        objSelectByPassesTransactionDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByPassesTransactionDetailsResponse = SelectByPassesTransactionDetails(objSelectByPassesTransactionDetailsRequest);
                        if (objSelectByPassesTransactionDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPassesTransaction.PassesTransactionDetailsList = objSelectByPassesTransactionDetailsResponse.PassesTransactionDetailsList;
                        }
                                               
                        ResponseData.PassesTransactionHeaderData = objPassesTransaction;
                        ResponseData.ResponseDynamicData = objPassesTransaction;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest");
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

        private SelectPassesTransactionResponse SelectByPassesTransactionDetails(SelectPassesTransactionRequest ObjRequest)
        {
            var PassesTransactionList = new List<PassesTransactionDetails>();
            var RequestData = (SelectPassesTransactionRequest)ObjRequest;
            var ResponseData = new SelectPassesTransactionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("Select * from PassesTransactionDetails PTD with(NoLock)");
                sSql.Append("where PTD.HeaderID =" + RequestData.ID);
                
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPassesTransactionDetails = new PassesTransactionDetails();
                        objPassesTransactionDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPassesTransactionDetails.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objPassesTransactionDetails.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
                        objPassesTransactionDetails.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                        objPassesTransactionDetails.PassRefNo = Convert.ToString(objReader["PassRefNo"]);
                        objPassesTransactionDetails.IsSent = objReader["IsSent"] != DBNull.Value ? Convert.ToBoolean(objReader["IsSent"]) : true;
                        objPassesTransactionDetails.IsUsed = objReader["IsUsed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsUsed"]) : true;
                        objPassesTransactionDetails.IsFCSync = objReader["IsFCSync"] != DBNull.Value ? Convert.ToBoolean(objReader["IsFCSync"]) : true;

                        PassesTransactionList.Add(objPassesTransactionDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PassesTransactionDetailsList = PassesTransactionList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest");
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
