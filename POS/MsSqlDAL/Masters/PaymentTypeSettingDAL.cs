using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.PaymentTypeSettingResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
    public class PaymentTypeSettingDAL : BasePaymentTypeSettingDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;


        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SavePaymentTypeRequest)RequestObj;
            var ResponseData = new SavePaymentTypeResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertPaymentTypeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@PaymentTypeID", RequestData.PaymentTypeMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@PaymentCode", RequestData.PaymentTypeMasterData.PaymentCode);
                _CommandObj.Parameters.AddWithValue("@PaymentName", RequestData.PaymentTypeMasterData.PaymentName);
                _CommandObj.Parameters.AddWithValue("@PaymentType", RequestData.PaymentTypeMasterData.PaymentType);
                _CommandObj.Parameters.AddWithValue("@CountRequired", RequestData.PaymentTypeMasterData.CountRequired);
                _CommandObj.Parameters.AddWithValue("@CountType", RequestData.PaymentTypeMasterData.CountType);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.PaymentTypeMasterData.CountryCode);
                _CommandObj.Parameters.AddWithValue("@Refundable", RequestData.PaymentTypeMasterData.Refundable);
              

                _CommandObj.Parameters.AddWithValue("@RequiredManageApproval", RequestData.PaymentTypeMasterData.RequiredManageApproval);
                _CommandObj.Parameters.AddWithValue("@OpenCashDraw", RequestData.PaymentTypeMasterData.OpenCashDraw);
                _CommandObj.Parameters.AddWithValue("@AllowOverTender", RequestData.PaymentTypeMasterData.AllowOverTender);
                _CommandObj.Parameters.AddWithValue("@AllowPartialTender", RequestData.PaymentTypeMasterData.AllowPartialTender);
                _CommandObj.Parameters.AddWithValue("@IsCountryNeed", RequestData.PaymentTypeMasterData.IsCountryNeed);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.PaymentTypeMasterData.CountryID);
                

                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.PaymentTypeMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PaymentTypeMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@PaymentProcesser", RequestData.PaymentTypeMasterData.IsPaymentProcesser);
                //ChkPaymentProcesser
                _CommandObj.Parameters.AddWithValue("@SortOrder", RequestData.PaymentTypeMasterData.SortOrder);
                _CommandObj.Parameters.AddWithValue("@PaymentReceivedType",RequestData.PaymentTypeMasterData.PaymentReceivedType);
                _CommandObj.Parameters.AddWithValue("@PaymentImage", RequestData.PaymentTypeMasterData.PaymentImage);

                _CommandObj.Parameters.AddWithValue("@PaymentModeID", RequestData.PaymentTypeMasterData.PaymentModeID);
                _CommandObj.Parameters.AddWithValue("@PaymentModeCode", RequestData.PaymentTypeMasterData.PaymentModeCode);
                _CommandObj.Parameters.AddWithValue("@TransactionType", RequestData.PaymentTypeMasterData.TransactionType);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Payment Type");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Type");
                    ResponseData.ExceptionMessage = StatusMsg.Value.ToString();
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdatePaymentTypeRequest)RequestObj;
            var ResponseData = new UpdatePaymentTypeResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdatePaymentTypeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.PaymentTypeMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@PaymentCode", RequestData.PaymentTypeMasterData.PaymentCode);
                _CommandObj.Parameters.AddWithValue("@PaymentName", RequestData.PaymentTypeMasterData.PaymentName);
                _CommandObj.Parameters.AddWithValue("@PaymentType", RequestData.PaymentTypeMasterData.PaymentType);
                _CommandObj.Parameters.AddWithValue("@CountRequired", RequestData.PaymentTypeMasterData.CountRequired);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.PaymentTypeMasterData.CountryID);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.PaymentTypeMasterData.CountryCode);
                _CommandObj.Parameters.AddWithValue("@IsCountryNeed", RequestData.PaymentTypeMasterData.IsCountryNeed);
                _CommandObj.Parameters.AddWithValue("@CountType", RequestData.PaymentTypeMasterData.CountType);
                _CommandObj.Parameters.AddWithValue("@Refundable", RequestData.PaymentTypeMasterData.Refundable);
                _CommandObj.Parameters.AddWithValue("@RequiredManageApproval", RequestData.PaymentTypeMasterData.RequiredManageApproval);
                _CommandObj.Parameters.AddWithValue("@OpenCashDraw", RequestData.PaymentTypeMasterData.OpenCashDraw);
                _CommandObj.Parameters.AddWithValue("@AllowOverTender", RequestData.PaymentTypeMasterData.AllowOverTender);
                _CommandObj.Parameters.AddWithValue("@AllowPartialTender", RequestData.PaymentTypeMasterData.AllowPartialTender);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.PaymentTypeMasterData.SCN);
                _CommandObj.Parameters.AddWithValue("@UpdateBy", RequestData.PaymentTypeMasterData.@UpdateBy);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PaymentTypeMasterData.Active);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PaymentTypeMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@PaymentImage", RequestData.PaymentTypeMasterData.PaymentImage);
                _CommandObj.Parameters.AddWithValue("@PaymentProcesser", RequestData.PaymentTypeMasterData.IsPaymentProcesser);
                _CommandObj.Parameters.AddWithValue("@SortOrder", RequestData.PaymentTypeMasterData.SortOrder);
                _CommandObj.Parameters.AddWithValue("@PaymentReceivedType", RequestData.PaymentTypeMasterData.PaymentReceivedType);

                _CommandObj.Parameters.AddWithValue("@PaymentModeID", RequestData.PaymentTypeMasterData.PaymentModeID);
                _CommandObj.Parameters.AddWithValue("@PaymentModeCode", RequestData.PaymentTypeMasterData.PaymentModeCode);
                _CommandObj.Parameters.AddWithValue("@TransactionType", RequestData.PaymentTypeMasterData.TransactionType);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Payment Type");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Payment Type");
                    ResponseData.ExceptionMessage = StatusMsg.Value.ToString();

                }


            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var PaymentTypeMaster = new PaymentTypeMasterType();
            var RequestData = (DeletePaymentTypeRequest)RequestObj;
            var ResponseData = new DeletePaymentTypeResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "delete from PaymentTypeMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.PaymentTypeMasterData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
            
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Payment Type Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Payment Type Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var CompanySettings = new CompanySettings();
            var RequestData = (SelectByIDPaymentTypeRequest)RequestObj;
            var ResponseData = new SelectByIDPaymentTypeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from PaymentTypeMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

               
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPaymentTypeMaster = new PaymentTypeMasterType();




                        objPaymentTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentTypeMaster.PaymentCode = objReader["PaymentCode"].ToString();
                        objPaymentTypeMaster.PaymentName = objReader["PaymentName"].ToString();
                        objPaymentTypeMaster.PaymentType = objReader["PaymentType"].ToString();
                        objPaymentTypeMaster.CountRequired = objReader["CountRequired"] != DBNull.Value ? Convert.ToBoolean(objReader["CountRequired"]) : true;
                        objPaymentTypeMaster.CountType =objReader["CountType"].ToString();
                        objPaymentTypeMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objPaymentTypeMaster.CountryCode = objReader["CountryCode"].ToString();
                        objPaymentTypeMaster.IsCountryNeed = objReader["IsCountryNeed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCountryNeed"]) : false;                                               
                        objPaymentTypeMaster.Refundable = objReader["Refundable"] != DBNull.Value ? Convert.ToBoolean(objReader["Refundable"]) : true;
                        objPaymentTypeMaster.RequiredManageApproval = objReader["RequiredManageApproval"] != DBNull.Value ? Convert.ToBoolean(objReader["RequiredManageApproval"]) : true;
                        objPaymentTypeMaster.OpenCashDraw = objReader["OpenCashDraw"] != DBNull.Value ? Convert.ToBoolean(objReader["OpenCashDraw"]) : true;
                        objPaymentTypeMaster.AllowOverTender = objReader["AllowOverTender"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowOverTender"]) : true;
                        objPaymentTypeMaster.AllowPartialTender = objReader["AllowPartialTender"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowPartialTender"]) : true;


                        objPaymentTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPaymentTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPaymentTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPaymentTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPaymentTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPaymentTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPaymentTypeMaster.IsPaymentProcesser = objReader["PaymentProcesser"] != DBNull.Value ? Convert.ToBoolean(objReader["PaymentProcesser"]) : false;
                        objPaymentTypeMaster.SortOrder = objReader["SortOrder"].ToString();

                        objPaymentTypeMaster.PaymentReceivedType = objReader["PaymentReceivedType"].ToString();
                        objPaymentTypeMaster.Remarks = objReader["Remarks"].ToString();
                        objPaymentTypeMaster.PaymentImage = objReader["PaymentImage"] != DBNull.Value ? (byte[])objReader["PaymentImage"] : null;

                        objPaymentTypeMaster.PaymentModeID = objReader["PaymentModeID"] != DBNull.Value ? Convert.ToInt32(objReader["PaymentModeID"]) : 0;
                        objPaymentTypeMaster.PaymentModeCode = objReader["PaymentModeCode"].ToString();
                        objPaymentTypeMaster.TransactionType = objReader["TransactionType"].ToString();

                        ResponseData.PaymentTypeMasterData = objPaymentTypeMaster;
                        ResponseData.ResponseDynamicData = objPaymentTypeMaster;

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Type Master");
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
        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var PaymentTypeMasterList = new List<PaymentTypeMasterType>();

            var RequestData = new SelectAllPaymentTypeRequest();
            var ResponseData = new SelectAllPaymentTypeResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select ID,PaymentCode,PaymentName,PaymentType,CountryCode,Remarks,Active,PaymentImage from PaymentTypeMaster with(NoLock)";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objPaymentTypeMaster = new PaymentTypeMasterType();

                        objPaymentTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentTypeMaster.PaymentCode = objReader["PaymentCode"].ToString();
                        objPaymentTypeMaster.PaymentName = objReader["PaymentName"].ToString();
                        objPaymentTypeMaster.PaymentType = objReader["PaymentType"].ToString();
                        //objPaymentTypeMaster.CountRequired = objReader["CountRequired"] != DBNull.Value ? Convert.ToBoolean(objReader["CountRequired"]) : true;
                        //objPaymentTypeMaster.CountType = objReader["CountType"].ToString();
                        //objPaymentTypeMaster.Refundable = objReader["Refundable"] != DBNull.Value ? Convert.ToBoolean(objReader["Refundable"]) : true;
                        //objPaymentTypeMaster.RequiredManageApproval = objReader["RequiredManageApproval"] != DBNull.Value ? Convert.ToBoolean(objReader["RequiredManageApproval"]) : true;
                        //objPaymentTypeMaster.OpenCashDraw = objReader["OpenCashDraw"] != DBNull.Value ? Convert.ToBoolean(objReader["OpenCashDraw"]) : true;
                        //objPaymentTypeMaster.AllowOverTender = objReader["AllowOverTender"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowOverTender"]) : true;
                        //objPaymentTypeMaster.AllowPartialTender = objReader["AllowPartialTender"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowPartialTender"]) : true;
                        //objPaymentTypeMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objPaymentTypeMaster.CountryCode = objReader["CountryCode"].ToString();
                        //objPaymentTypeMaster.IsCountryNeed = objReader["IsCountryNeed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCountryNeed"]) : false; 

                        //objPaymentTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPaymentTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPaymentTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPaymentTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPaymentTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPaymentTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objPaymentTypeMaster.IsPaymentProcesser = objReader["PaymentProcesser"] != DBNull.Value ? Convert.ToBoolean(objReader["PaymentProcesser"]) : false;
                        //objPaymentTypeMaster.SortOrder = objReader["SortOrder"].ToString();
                        //objPaymentTypeMaster.PaymentReceivedType = objReader["PaymentReceivedType"].ToString();
                        objPaymentTypeMaster.Remarks = objReader["Remarks"].ToString();
                        objPaymentTypeMaster.PaymentImage = objReader["PaymentImage"] != DBNull.Value ? (byte[])objReader["PaymentImage"] : null;

                        PaymentTypeMasterList.Add(objPaymentTypeMaster);                       

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PaymentTypeMasterList = PaymentTypeMasterList;
                    //ResponseData.ResponseDynamicData = PaymentTypeMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Type Master");
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


      
          


        public override SelectPaymentTypeByCountryResponse SelectPaymentTypeByCountry(SelectPaymentTypeByCountryRequest ObjRequest)
        {
            var PaymentList = new List<PaymentTypeMasterType>();
            var RequestData = (SelectPaymentTypeByCountryRequest)ObjRequest;
            var ResponseData = new SelectPaymentTypeByCountryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = "Select ID,PaymentCode,PaymentName,PaymentType,PaymentImage,PaymentProcesser,PaymentReceivedType from PaymentTypemaster where (CountryID='0' or IsCountryNeed='True') and Active='True' Order by SortOrder ";
                sSql = string.Format(sSql, RequestData.CountryID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPaymentTypeMaster = new PaymentTypeMasterType();

                        objPaymentTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentTypeMaster.PaymentCode = objReader["PaymentCode"].ToString();
                        objPaymentTypeMaster.PaymentName = objReader["PaymentName"].ToString();
                        objPaymentTypeMaster.PaymentType = objReader["PaymentType"].ToString();                   
                        objPaymentTypeMaster.PaymentImage = objReader["PaymentImage"] != DBNull.Value ? (byte[])objReader["PaymentImage"] : null;
                        objPaymentTypeMaster.IsPaymentProcesser = objReader["PaymentProcesser"]!=DBNull.Value? Convert.ToBoolean(objReader["PaymentProcesser"]) : false;
                        objPaymentTypeMaster.PaymentReceivedType = objReader["PaymentReceivedType"].ToString();
                        PaymentList.Add(objPaymentTypeMaster);
                        ResponseData.PaymentDetailsList = PaymentList;
                        //ResponseData.ResponseDynamicData = PaymentList;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Type Master");
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

        public override SelectPaymentTypeLookUpResponse SelectPaymentTypeLookUp(SelectPaymentLookUpRequest ObjRequest)
        {
            var PaymentTypeList = new List<PaymentTypeMasterType>();
            var RequestData = (SelectPaymentLookUpRequest)ObjRequest;
            var ResponseData = new SelectPaymentTypeLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,CountryID,PaymentCode,PaymentName,CountryCode from PaymentTypeMaster";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPaymentType = new PaymentTypeMasterType();
                        objPaymentType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentType.PaymentCode = Convert.ToString(objReader["PaymentCode"]);
                        objPaymentType.PaymentName = Convert.ToString(objReader["PaymentName"]);
                        objPaymentType.CountryCode = Convert.ToString(objReader["CountryCode"]);                      
                        PaymentTypeList.Add(objPaymentType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PaymentTypeList = PaymentTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Type Master");
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

        public override SelectAllPaymentTypeResponse API_SelectALL(SelectAllPaymentTypeRequest requestData)
        {
            var PaymentTypeMasterList = new List<PaymentTypeMasterType>();

            var RequestData = (SelectAllPaymentTypeRequest)requestData;
            var ResponseData = new SelectAllPaymentTypeResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select ID,PaymentCode,PaymentName,PaymentType,CountryCode,Remarks,Active from PaymentTypeMaster with(NoLock)";

                string sSql = " Select pt.ID, pt.PaymentCode, pt.PaymentName, pt.PaymentType, pt.CountryCode " +
                        " 	, pt.Remarks, pt.Active, rc.total_count [RecordCount]					   " +
                        " from PaymentTypeMaster pt with(NoLock) 									   " +
                        " left join(Select count(ID) [total_count]									   " +
                        " 			from PaymentTypeMaster with(NoLock) 							   " +
                        " 			where Active = " + RequestData.IsActive + " " +
                        "               and (isnull('" + RequestData.SearchString + "','') = '' 	    " +
                        " 				or PaymentCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "               or PaymentName like isnull('%" + RequestData.SearchString + "%','')  " +
                        " 				or PaymentType like isnull('%" + RequestData.SearchString + "%','') " +
                        "               or CountryCode like isnull('%" + RequestData.SearchString + "%','')  " +
                        " 				or Remarks like isnull('%" + RequestData.SearchString + "%','')) ) rc on 1 = 1 " +
                        " where Active = " + RequestData.IsActive + " " +
                        "   and (isnull('" + RequestData.SearchString + "','') = ''          		   " +
                        " 	or PaymentCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "   or PaymentName like isnull('%" + RequestData.SearchString + "%','') 			   " +
                        " 	or PaymentType like isnull('%" + RequestData.SearchString + "%','') " +
                        "   or CountryCode like isnull('%" + RequestData.SearchString + "%','') 			   " +
                        " 	or Remarks like isnull('%" + RequestData.SearchString + "%','')) 											   " +
                        " order by ID asc 															   " +
                        " offset " + RequestData.Offset + " rows fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objPaymentTypeMaster = new PaymentTypeMasterType();

                        objPaymentTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPaymentTypeMaster.PaymentCode = objReader["PaymentCode"].ToString();
                        objPaymentTypeMaster.PaymentName = objReader["PaymentName"].ToString();
                        objPaymentTypeMaster.PaymentType = objReader["PaymentType"].ToString();
                        //objPaymentTypeMaster.CountRequired = objReader["CountRequired"] != DBNull.Value ? Convert.ToBoolean(objReader["CountRequired"]) : true;
                        //objPaymentTypeMaster.CountType = objReader["CountType"].ToString();
                        //objPaymentTypeMaster.Refundable = objReader["Refundable"] != DBNull.Value ? Convert.ToBoolean(objReader["Refundable"]) : true;
                        //objPaymentTypeMaster.RequiredManageApproval = objReader["RequiredManageApproval"] != DBNull.Value ? Convert.ToBoolean(objReader["RequiredManageApproval"]) : true;
                        //objPaymentTypeMaster.OpenCashDraw = objReader["OpenCashDraw"] != DBNull.Value ? Convert.ToBoolean(objReader["OpenCashDraw"]) : true;
                        //objPaymentTypeMaster.AllowOverTender = objReader["AllowOverTender"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowOverTender"]) : true;
                        //objPaymentTypeMaster.AllowPartialTender = objReader["AllowPartialTender"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowPartialTender"]) : true;
                        //objPaymentTypeMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objPaymentTypeMaster.CountryCode = objReader["CountryCode"].ToString();
                        //objPaymentTypeMaster.IsCountryNeed = objReader["IsCountryNeed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCountryNeed"]) : false; 

                        //objPaymentTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPaymentTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPaymentTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPaymentTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPaymentTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPaymentTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objPaymentTypeMaster.IsPaymentProcesser = objReader["PaymentProcesser"] != DBNull.Value ? Convert.ToBoolean(objReader["PaymentProcesser"]) : false;
                        //objPaymentTypeMaster.SortOrder = objReader["SortOrder"].ToString();
                        //objPaymentTypeMaster.PaymentReceivedType = objReader["PaymentReceivedType"].ToString();
                        objPaymentTypeMaster.Remarks = objReader["Remarks"].ToString();
                        //objPaymentTypeMaster.PaymentImage = objReader["PaymentImage"] != DBNull.Value ? (byte[])objReader["PaymentImage"] : null;

                        PaymentTypeMasterList.Add(objPaymentTypeMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PaymentTypeMasterList = PaymentTypeMasterList;
                    //ResponseData.ResponseDynamicData = PaymentTypeMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Payment Type Master");
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

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

