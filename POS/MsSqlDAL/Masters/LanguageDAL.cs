using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.LanguageResponse;
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
  public  class LanguageDAL:BaseLanguageDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveLanguageRequest)RequestObj;
            var ResponseData = new SaveLanguageResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertLanguageMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var LanguageID = _CommandObj.Parameters.Add("@LanguageID", SqlDbType.Int);
                LanguageID.Direction = ParameterDirection.Input;
                LanguageID.Value = RequestData.LanguageMasterRecord.ID;

                var LanguageCode = _CommandObj.Parameters.Add("@LanguageCode", SqlDbType.NVarChar);
                LanguageCode.Direction = ParameterDirection.Input;
                LanguageCode.Value = RequestData.LanguageMasterRecord.LanguageCode;

                var LanguageName = _CommandObj.Parameters.Add("@LanguageName", SqlDbType.NVarChar);
                LanguageName.Direction = ParameterDirection.Input;
                LanguageName.Value = RequestData.LanguageMasterRecord.LanguageName;                

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.LanguageMasterRecord.CreateBy;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.LanguageMasterRecord.Remarks;

                _CommandObj.Parameters.AddWithValue("@Active", RequestData.LanguageMasterRecord.Active);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Language Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Language Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Language Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Language Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
             var RequestData = (UpdateLanguageRequest)RequestObj;
            var ResponseData = new UpdateLanguageResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateLanguageMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                 var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.LanguageMasterRecord.ID;

                var LanguageCode = _CommandObj.Parameters.Add("@LanguageCode", SqlDbType.NVarChar);
                LanguageCode.Direction = ParameterDirection.Input;
                LanguageCode.Value = RequestData.LanguageMasterRecord.LanguageCode;

                var LanguageName = _CommandObj.Parameters.Add("@LanguageName", SqlDbType.NVarChar);
                LanguageName.Direction = ParameterDirection.Input;
                LanguageName.Value = RequestData.LanguageMasterRecord.LanguageName;               

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.LanguageMasterRecord.UpdateBy;

                //var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                //SCN.Direction = ParameterDirection.Input;
                //SCN.Value = RequestData.LanguageMasterRecord.SCN;


                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.LanguageMasterRecord.Remarks;


                _CommandObj.Parameters.AddWithValue("@Active", RequestData.LanguageMasterRecord.Active);


                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Language Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Language Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Language Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Language Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var LanguageMasterRecord = new LanguageMaster();
            var RequestData = (DeleteLanguageRequest)RequestObj;
            var ResponseData = new DeleteLanguageResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("delete from LanguageMaster where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Language Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Language Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var LanguageMasterRecord = new LanguageMaster();
            var RequestData = (SelectByLanguageIDRequest)RequestObj;
            var ResponseData = new SelectByLanguageIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from LanguageMaster with(NoLock) where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjLanguage = new LanguageMaster();
                        ObjLanguage.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjLanguage.LanguageCode = Convert.ToString(objReader["LanguageCode"]);
                        ObjLanguage.LanguageName = Convert.ToString(objReader["LanguageName"]);                       
                        ObjLanguage.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        ObjLanguage.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        ObjLanguage.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        ObjLanguage.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        ObjLanguage.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        ObjLanguage.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ObjLanguage.Remarks = Convert.ToString(objReader["Remarks"]);  
                        ResponseData.LanguageMasterRecord = ObjLanguage;
                        ResponseData.ResponseDynamicData = ObjLanguage;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Language Master");
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

        public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var LanguageMasterList= new List<LanguageMaster>();
            var RequestData = (SelectAllLanguageRequest)RequestObj;
            var ResponseData = new SelectAllLanguageResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from LanguageMaster with(NoLock)";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjLanguage = new LanguageMaster();
                        ObjLanguage.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjLanguage.LanguageCode = Convert.ToString(objReader["LanguageCode"]);
                        ObjLanguage.LanguageName = Convert.ToString(objReader["LanguageName"]);
                        ObjLanguage.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        ObjLanguage.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        ObjLanguage.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        ObjLanguage.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        ObjLanguage.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        ObjLanguage.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ObjLanguage.Remarks = Convert.ToString(objReader["Remarks"]);  

                        LanguageMasterList.Add(ObjLanguage);                       
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.LanguageMasterList = LanguageMasterList;
                    ResponseData.ResponseDynamicData = LanguageMasterList;   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Language Master");
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


        public override SelectAllLanguageResponse API_SelectLanguageLookUp(SelectAllLanguageRequest objRequest)
        {
            var LanguageMasterList = new List<LanguageMaster>();
            var RequestData = (SelectAllLanguageRequest)objRequest;
            var ResponseData = new SelectAllLanguageResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select  ID, LanguageCode,LanguageName,Active from LanguageMaster with(NoLock) WHERE Active = 1 ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjLanguage = new LanguageMaster();
                        ObjLanguage.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjLanguage.LanguageCode = Convert.ToString(objReader["LanguageCode"]);
                        ObjLanguage.LanguageName = Convert.ToString(objReader["LanguageName"]);
                        ObjLanguage.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                      
                        LanguageMasterList.Add(ObjLanguage);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.LanguageMasterList = LanguageMasterList;
                   // ResponseData.ResponseDynamicData = LanguageMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Language Master");
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

        public override SelectAllLanguageResponse API_SelectAll(SelectAllLanguageRequest objRequest)
        {
            var LanguageMasterList = new List<LanguageMaster>();
            var RequestData = (SelectAllLanguageRequest)objRequest;
            var ResponseData = new SelectAllLanguageResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
               // string sSql = "Select * from LanguageMaster with(NoLock)";

                string sSql = "select ID, LanguageCode,LanguageName,Active,Remarks, RC.TOTAL_CNT [RecordCount] " +
                   "from LanguageMaster " +
                   "LEFT JOIN(Select count(LM.ID) As TOTAL_CNT From LanguageMaster LM " +
                   "where LM.Active = " + RequestData.IsActive + " and (isnull('"+RequestData.SearchString+"', '') = '' " +
                   "or LM.LanguageCode like isnull('%"+RequestData.SearchString+"%', '') " +
                   "or LM.LanguageName like isnull('%"+RequestData.SearchString+"%', '') " +
                   "or LM.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                           "or LanguageCode like isnull('%" + RequestData.SearchString + "%','') " +
                           "or LanguageName like isnull('%" + RequestData.SearchString + "%','') " +
                           "or Remarks like isnull('%" + RequestData.SearchString + "%','')) " +
                   "order by ID asc " +
                   "offset " + RequestData.Offset + " rows " +
                   "fetch first " + RequestData.Limit + " rows only";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjLanguage = new LanguageMaster();
                        ObjLanguage.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjLanguage.LanguageCode = Convert.ToString(objReader["LanguageCode"]);
                        ObjLanguage.LanguageName = Convert.ToString(objReader["LanguageName"]);
                        //ObjLanguage.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //ObjLanguage.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //ObjLanguage.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //ObjLanguage.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //ObjLanguage.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        ObjLanguage.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ObjLanguage.Remarks = Convert.ToString(objReader["Remarks"]);

                        LanguageMasterList.Add(ObjLanguage);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.LanguageMasterList = LanguageMasterList;
                   // ResponseData.ResponseDynamicData = LanguageMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Language Master");
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

        public override SelectLookUpResponse SelectLanguageLookUp(SelectLookUpRequest ObjRequest)
        {
           var LanguageLookUpList= new List<LanguageMaster>();
            var RequestData = (SelectLookUpRequest)ObjRequest;
            var ResponseData = new SelectLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[LanguageName] from LanguageMaster with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjLanguage = new LanguageMaster();
                        ObjLanguage.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjLanguage.LanguageName = Convert.ToString(objReader["LanguageName"]);
                        LanguageLookUpList.Add(ObjLanguage);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.LanguageMasterList = LanguageLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Language Master");
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

        public override BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
