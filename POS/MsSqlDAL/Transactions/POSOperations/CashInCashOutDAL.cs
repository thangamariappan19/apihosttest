using EasyBizAbsDAL.Transactions.PaymentDetails;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.PaymentDetails;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizRequest.Transactions.PaymentDetails;
using EasyBizRequest.Transactions.POSOperations;
using EasyBizResponse.Transactions.PaymentDetails.CashInCashOut;
using EasyBizResponse.Transactions.POSOperations.CashInCashOut;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.PaymentDetails
{
    public class CashInCashOutDAL : BaseCashInCashOutDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCashInCashOutRequest)RequestObj;
            var ResponseData = new SaveCashInCashOutResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateCashInCashOutMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.CashInCashOutMasterRecord.ID;

                SqlParameter Total = _CommandObj.Parameters.Add("@Total", SqlDbType.Money);
                Total.Direction = ParameterDirection.Input;
                Total.Value = RequestData.CashInCashOutMasterRecord.Total;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.CashInCashOutMasterRecord.DocumentDate);

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.CashInCashOutMasterRecord.StoreID;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.CashInCashOutMasterRecord.CountryID;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CashInCashOutMasterRecord.CreateBy;

                //SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.VarChar);
                //StoreCode.Direction = ParameterDirection.Input;
                //StoreCode.Value = RequestData.CashInCashOutMasterRecord.StoreCode;

                //SqlParameter POSCode = _CommandObj.Parameters.Add("@POSCode", SqlDbType.VarChar);
                //POSCode.Direction = ParameterDirection.Input;
                //POSCode.Value = RequestData.CashInCashOutMasterRecord.POSCode;

                //SqlParameter ShiftCode = _CommandObj.Parameters.Add("@ShiftCode", SqlDbType.VarChar);
                //ShiftCode.Direction = ParameterDirection.Input;
                //ShiftCode.Value = RequestData.CashInCashOutMasterRecord.ShiftCode;

                //SqlParameter POSID = _CommandObj.Parameters.Add("@POSID", SqlDbType.Int);
                //POSID.Direction = ParameterDirection.Input;
                //POSID.Value = RequestData.CashInCashOutMasterRecord.POSID;

                

                //SqlParameter ShiftID = _CommandObj.Parameters.Add("@ShiftID", SqlDbType.Int);
                //ShiftID.Direction = ParameterDirection.Input;
                //ShiftID.Value = RequestData.CashInCashOutMasterRecord.ShiftID;

                SqlParameter CashInCashOutDetails = _CommandObj.Parameters.Add("@CashInCashOutDetails", SqlDbType.Xml);
                CashInCashOutDetails.Direction = ParameterDirection.Input;
                CashInCashOutDetails.Value = CashInCashOutDetailMasterXML(RequestData.CashInCashOutDetailsList);
                //CashInCashOutDetails.Value = CashInCashOutDetails.ToString().Replace("&", "&#38;");

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "CashInCashOut");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "CashInCashOut");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CashInCashOut");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CashInCashOut");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string CashInCashOutDetailMasterXML(List<CashInCashOutDetails> CashInCashOutDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (CashInCashOutDetails objCashInCashOutDetailMasterDetails in CashInCashOutDetailMasterList)
            {
                if (objCashInCashOutDetailMasterDetails.ReasonID != 0)
                {
                    sSql.Append("<CashInCashOutDetailsData>");
                    sSql.Append("<ID>" + objCashInCashOutDetailMasterDetails.ID + "</ID>");
                    sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString(objCashInCashOutDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                    sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objCashInCashOutDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                    //sSql.Append("<ReasonID>" + objCashInCashOutDetailMasterDetails.ReasonID + "</ReasonID>");
                    sSql.Append("<Reason>" + (objCashInCashOutDetailMasterDetails.ReasonID) + "</Reason>");
                    sSql.Append("<IsDeleted>" + (objCashInCashOutDetailMasterDetails.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<ReceivedAmount>" + objCashInCashOutDetailMasterDetails.ReceivedAmount + "</ReceivedAmount>");
                    sSql.Append("<PaidAmount>" + (objCashInCashOutDetailMasterDetails.PaidAmount) + "</PaidAmount>");
                    sSql.Append("<Remarks>" + (objCashInCashOutDetailMasterDetails.Remarks) + "</Remarks>");
                    sSql.Append("<CategoryType>" + (objCashInCashOutDetailMasterDetails.Type) + "</CategoryType>");
                    sSql.Append("<POSID>" + objCashInCashOutDetailMasterDetails.POSID + "</POSID>");
                    sSql.Append("<ShiftID>" + objCashInCashOutDetailMasterDetails.ShiftID + "</ShiftID>");
                    sSql.Append("<StoreID>" + objCashInCashOutDetailMasterDetails.StoreID + "</StoreID>");
                    sSql.Append("<StoreCode>" + objCashInCashOutDetailMasterDetails.StoreCode + "</StoreCode>");
                    sSql.Append("<POSCode>" + objCashInCashOutDetailMasterDetails.POSCode + "</POSCode>");
                    sSql.Append("<ShiftCode>" + objCashInCashOutDetailMasterDetails.ShiftCode + "</ShiftCode>");
                    sSql.Append("</CashInCashOutDetailsData>");
                }
            }
            return sSql.ToString();
        }        
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var CashInCashOutRecord = new CashInCashOutMaster();
            var RequestData = (DeleteCashInCashOutRequest)RequestObj;
            var ResponseData = new DeleteCashInCashOutResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from  CashInCashOutDetails where HeaderID={0} ; Delete from CashInCashOutHeader where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "CashInCashOut");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "CashInCashOut");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var CashInCashOutRecord = new CashInCashOutMaster();
            var RequestData = (SelectByCashInCashOutIDRequest)RequestObj;
            var ResponseData = new SelectByCashInCashOutIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from CashInCashOutHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCashInCashOut = new CashInCashOutMaster();
                        objCashInCashOut.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCashInCashOut.Total = objReader["Total"] != DBNull.Value ? Convert.ToDecimal(objReader["Total"]) : 0;
                        objCashInCashOut.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;  
                        objCashInCashOut.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCashInCashOut.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCashInCashOut.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCashInCashOut.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCashInCashOut.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCashInCashOut.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //objCashInCashOut.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        //objCashInCashOut.POSCode = Convert.ToString(objReader["POSCode"]);
                        //objCashInCashOut.ShiftCode =  Convert.ToString(objReader["ShiftCode"]);
                        //objCashInCashOut.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //objCashInCashOut.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0;
                        //objCashInCashOut.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;

                        ResponseData.CashInCashOutMasterRecord = objCashInCashOut;
                        ResponseData.ResponseDynamicData = objCashInCashOut;
                    }
                       
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CashInCashOut");
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
            var CashInCashOutList = new List<CashInCashOutMaster>();
            var RequestData = (SelectAllCashInCashOutRequest)RequestObj;
            var ResponseData = new SelectAllCashInCashOutResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if(RequestData.Type=="CashIn")
                {
                    sQuery = "Select * from CashInCashOutHeader CH inner join CashInCashOutDetails CD on cd.headerid=ch.id   where cd.categorytype='" + RequestData.Type + "'";
                }
                else if (RequestData.Type == "CashOut")
                {
                    sQuery = "Select * from CashInCashOutHeader CH inner join CashInCashOutDetails CD on cd.headerid=ch.id   where cd.categorytype='" + RequestData.Type + "'";
                }
               else
                {
                    sQuery = "Select * from CashInCashOutHeader";
                }
                

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCashInCashOut = new CashInCashOutMaster();
                        objCashInCashOut.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCashInCashOut.Total = objReader["Total"] != DBNull.Value ? Convert.ToDecimal(objReader["Total"]) : 0;
                        objCashInCashOut.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now; 
                        objCashInCashOut.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCashInCashOut.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCashInCashOut.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCashInCashOut.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCashInCashOut.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCashInCashOut.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //objCashInCashOut.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        //objCashInCashOut.POSCode = Convert.ToString(objReader["POSCode"]);
                        //objCashInCashOut.ShiftCode = Convert.ToString(objReader["ShiftCode"]);

                        CashInCashOutList.Add(objCashInCashOut);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CashInCashOutMasterList = CashInCashOutList;
                    ResponseData.ResponseDynamicData = CashInCashOutList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CashInCashOut Master");
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
        public override EasyBizResponse.Transactions.PaymentDetails.CashInCashOut.SelectCashInCashOutDetailsResponse SelectCashInCashOutDetails(EasyBizRequest.Transactions.PaymentDetails.SelectCashInCashOutDetailsRequest ObjRequest)
        {
            var CashInCashOutDetailMasterList = new List<CashInCashOutDetails>();
            var RequestData = (SelectCashInCashOutDetailsRequest)ObjRequest;
            var ResponseData = new SelectCashInCashOutDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from CashInCashOutDetails ");
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
                        var objCashInCashOutDetailMaster = new CashInCashOutDetails();
                        objCashInCashOutDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCashInCashOutDetailMaster.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objCashInCashOutDetailMaster.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;                            
                        objCashInCashOutDetailMaster.Reason = objReader["Reason"] != DBNull.Value ? Convert.ToInt32(objReader["Reason"]) : 0;
                        objCashInCashOutDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objCashInCashOutDetailMaster.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objCashInCashOutDetailMaster.ApplicationDate = objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objCashInCashOutDetailMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                      //  objCashInCashOutDetailMaster.Type = Convert.ToString(objReader["CategoryType"]);
                        CashInCashOutDetailMasterList.Add(objCashInCashOutDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CashInCashOutDetailsRecord = CashInCashOutDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CashInCashOut");
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

        public override SelectAllCashInCashoutReportResponse SelectCashInCashOutReportDetails(SelectAllCashInCashoutReportRequest ObjRequest)
        {
            var CashInCashOutList = new List<CashInCashOutReportDetails>();
            var RequestData = (SelectAllCashInCashoutReportRequest)ObjRequest;
            var ResponseData = new SelectAllCashInCashoutReportResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //if (RequestData.CategoryType == "CashIn")
                //{
                    sQuery = "Select ch.ID,ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SerialNo,ch.DocumentDate,ch.Total,cd.ReceivedAmount,cd.PaidAmount,cd.Remarks,cd.CategoryType,rm.ReasonName,cd.StoreID,cd.StoreCode from CashInCashOutHeader CH inner join CashInCashOutDetails CD on cd.headerid = ch.id inner join reasonmaster rm on cd.reason = rm.id  " +
                             " where cd.StoreID='"+ RequestData.StoreID +"' and cd.categorytype='" + RequestData.CategoryType + "' and ch.DocumentDate between '" + sqlCommon.GetSQLServerDateString(RequestData.FromDate) + "' and '" + sqlCommon.GetSQLServerDateString(RequestData.ToDate) + "' ";
                //}
                //else if (RequestData.CategoryType == "CashOut")
                //{
                //    sQuery = "Select * from CashInCashOutHeader CH inner join CashInCashOutDetails CD on cd.headerid=ch.id   where cd.categorytype='" + RequestData.Type + "'";
                //}
              
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCashInCashOut = new CashInCashOutReportDetails();
                        objCashInCashOut.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCashInCashOut.SerialNo = objReader["SerialNo"] != DBNull.Value ? Convert.ToInt32(objReader["SerialNo"]) : 0;
                        objCashInCashOut.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objCashInCashOut.Total = objReader["Total"] != DBNull.Value ? Convert.ToDecimal(objReader["Total"]) : 0;
                        objCashInCashOut.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objCashInCashOut.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;
                        objCashInCashOut.Remarks = Convert.ToString(objReader["Remarks"]);
                        objCashInCashOut.ReasonName = Convert.ToString(objReader["ReasonName"]);
                        objCashInCashOut.CategoryType = Convert.ToString(objReader["CategoryType"]);
                        //objCashInCashOut.CategoryType = Convert.ToString(objReader["CategoryType"]);
                        objCashInCashOut.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objCashInCashOut.StoreCode = Convert.ToString(objReader["StoreCode"]);
                       

                        CashInCashOutList.Add(objCashInCashOut);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CashInCashOutReportList = CashInCashOutList;
                    ResponseData.ResponseDynamicData = CashInCashOutList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CashInCashOut Master");
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

        public override SelectAllCashInCashOutDateWiseReponse SelectCashInCashOutRecord(SelectAllCashInCashOutDateWiseRequest ObjRequest)
        {

            var CashInCashOutList = new List<CashInCashOutReportDetails>();
            var RequestData = (SelectAllCashInCashOutDateWiseRequest)ObjRequest;
            var ResponseData = new SelectAllCashInCashOutDateWiseReponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ch.ID,ch.DocumentDate,ch.Total,cd.ReceivedAmount, "+
                          "cd.PaidAmount,cd.Remarks,cd.CategoryType,rm.ReasonName,cd.StoreID,cd.StoreCode,rm.id [ReasonID],cd.ApplicationDate,cd.POSID,cd.POSCode,cd.ShiftCode,cd.ShiftID from CashInCashOutHeader CH " +
                          "inner join CashInCashOutDetails CD on cd.headerid = ch.id inner join reasonmaster rm on cd.reason = rm.id  " +
                          " where cd.StoreID='" + RequestData.StoreID + "'"+
                          "and cd.categorytype='" + RequestData.CategoryType + "' "+
                          "and ch.DocumentDate ='" + sqlCommon.GetSQLServerDateString(RequestData.Date) + "'";
                

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCashInCashOut = new CashInCashOutReportDetails();
                        objCashInCashOut.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objCashInCashOut.SerialNo = objReader["SerialNo"] != DBNull.Value ? Convert.ToInt32(objReader["SerialNo"]) : 0;
                        objCashInCashOut.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objCashInCashOut.Total = objReader["Total"] != DBNull.Value ? Convert.ToDecimal(objReader["Total"]) : 0;
                        objCashInCashOut.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
                        objCashInCashOut.PaidAmount = objReader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["PaidAmount"]) : 0;
                        objCashInCashOut.Remarks = Convert.ToString(objReader["Remarks"]);
                        objCashInCashOut.Reason = Convert.ToString(objReader["ReasonName"]);
                        objCashInCashOut.ReasonID= objReader["ReasonID"] != DBNull.Value ? Convert.ToInt32(objReader["ReasonID"]) : 0;
                        objCashInCashOut.CategoryType = Convert.ToString(objReader["CategoryType"]);
                        //objCashInCashOut.CategoryType = Convert.ToString(objReader["CategoryType"]);
                        //objCashInCashOut.CategoryType = Convert.ToString(objReader["CategoryType"]);
                        objCashInCashOut.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objCashInCashOut.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objCashInCashOut.ApplicationDate= objReader["ApplicationDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ApplicationDate"]) : DateTime.Now;
                        objCashInCashOut.POSCode= Convert.ToString(objReader["POSCode"]);
                        objCashInCashOut.POSID= objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0;
                        objCashInCashOut.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;
                        objCashInCashOut.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        objCashInCashOut.Type= Convert.ToString(objReader["CategoryType"]);




                        CashInCashOutList.Add(objCashInCashOut);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CashInCashOutReportList = CashInCashOutList;
                    ResponseData.ResponseDynamicData = CashInCashOutList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CashInCashOut Master");
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
