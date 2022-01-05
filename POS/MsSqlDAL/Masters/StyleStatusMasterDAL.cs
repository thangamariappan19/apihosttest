using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterResponse;
using EasyBizResponse;
using EasyBizResponse.Masters.StyleStatusMasterResponse;
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
    public class StyleStatusMasterDAL : BaseStyleStatusMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStyleStatusMasterRequest)RequestObj;
            var ResponseData = new SaveStyleStatusMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertStyleStatusMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StyleStatusID", RequestData.StyleStatusMasterTypeRecord.ID);
                _CommandObj.Parameters.AddWithValue("@StyleStatusCode", RequestData.StyleStatusMasterTypeRecord.StyleStatusCode);
                _CommandObj.Parameters.AddWithValue("@StatusName", RequestData.StyleStatusMasterTypeRecord.StatusName);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.StyleStatusMasterTypeRecord.Remarks);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.StyleStatusMasterTypeRecord.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.StyleStatusMasterTypeRecord.Active);

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Status Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Status Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
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

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateStyleStatusMasterRequest)RequestObj;
            var ResponseData = new UpdateStyleStatusMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateStyleStatusMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.StyleStatusMasterTypeRecord.ID);
                _CommandObj.Parameters.AddWithValue("@StyleStatusCode", RequestData.StyleStatusMasterTypeRecord.StyleStatusCode);
                _CommandObj.Parameters.AddWithValue("@StatusName", RequestData.StyleStatusMasterTypeRecord.StatusName);
                _CommandObj.Parameters.AddWithValue("@UpdateBy", RequestData.StyleStatusMasterTypeRecord.UpdateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.StyleStatusMasterTypeRecord.SCN);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.StyleStatusMasterTypeRecord.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.StyleStatusMasterTypeRecord.Active);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Style Status Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Status Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Status Master");
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

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {

            var RequestData = (DeleteStyleStatusMasterRequest)RequestObj;
            var ResponseData = new DeleteStyleStatusMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Delete from StyleStatusMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Style Status Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Style Status Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var StyleStatusMasterType = new StyleStatusMasterType();
            var RequestData = (SelectByIDStyleStatusMasterRequest)RequestObj;
            var ResponseData = new SelectByIDStyleStatusMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from StyleStatusMaster with(NoLock)  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleStatusMasterTypes = new StyleStatusMasterType();
                        objStyleStatusMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleStatusMasterTypes.StyleStatusCode = objReader["StyleStatusCode"].ToString();
                        objStyleStatusMasterTypes.StatusName = objReader["StatusName"].ToString();

                        objStyleStatusMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStyleStatusMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStyleStatusMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStyleStatusMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStyleStatusMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStyleStatusMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleStatusMasterTypes.Remarks = objReader["Remarks"].ToString();

                        ResponseData.StyleStatusMasterTypeData = objStyleStatusMasterTypes;
                        ResponseData.ResponseDynamicData = objStyleStatusMasterTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Status Master");
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
            var StyleStatusMasterTypeList = new List<StyleStatusMasterType>();

            var RequestData = new SelectAllStyleStatusMasterRequest();
            var ResponseData = new SelectAllStyleStatusMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select * from StyleStatusMaster with(NoLock)";

               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
             
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objStyleStatusMasterTypes = new StyleStatusMasterType();
                        objStyleStatusMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleStatusMasterTypes.StyleStatusCode = objReader["StyleStatusCode"].ToString();
                        objStyleStatusMasterTypes.StatusName = objReader["StatusName"].ToString();

                        objStyleStatusMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStyleStatusMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStyleStatusMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStyleStatusMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStyleStatusMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStyleStatusMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleStatusMasterTypes.Remarks = objReader["Remarks"].ToString();

                        StyleStatusMasterTypeList.Add(objStyleStatusMasterTypes);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleStatusMasterTypeList = StyleStatusMasterTypeList;
                    ResponseData.ResponseDynamicData = StyleStatusMasterTypeList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Status Master");
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

        public override EasyBizResponse.Masters.StyleStatusMasterResponse.SelectStyleStatusLookUpResponse SelectStyleStatusLookUp(SelectStyleStatusLookUpRequest ObjRequest)
        {
            var StyleStatusMasterList = new List<StyleStatusMasterType>();
            var RequestData = (SelectStyleStatusLookUpRequest)ObjRequest;
            var ResponseData = new SelectStyleStatusLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[StatusName] from StyleStatusMaster with(NoLock) where Active='True'";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleStatusMasterType = new StyleStatusMasterType();
                        objStyleStatusMasterType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleStatusMasterType.StatusName = Convert.ToString(objReader["StatusName"]);
                        StyleStatusMasterList.Add(objStyleStatusMasterType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleStatusMasterTypeList = StyleStatusMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StyleStatus Master");
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

        public override SelectAllStyleStatusMasterResponse API_SelectALL(SelectAllStyleStatusMasterRequest requestData)
        {
            var StyleStatusMasterTypeList = new List<StyleStatusMasterType>();

            //var RequestData = new SelectAllStyleStatusMasterRequest();
            //var ResponseData = new SelectAllStyleStatusMasterResponse();

            var RequestData = (SelectAllStyleStatusMasterRequest)requestData;
            var ResponseData = new SelectAllStyleStatusMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //sQuery = "Select * from StyleStatusMaster with(NoLock)";

                 sQuery = "Select ID, StyleStatusCode,StatusName, Remarks, Active, RC.TOTAL_CNT [RecordCount] " +
                    "from StyleStatusMaster with(NoLock) " +
                    "LEFT JOIN(Select  count(SSM.ID) As TOTAL_CNT From StyleStatusMaster SSM with(NoLock) " +
                    "where SSM.Active = " + RequestData.IsActive + " " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or SSM.StyleStatusCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "or SSM.StatusName like isnull('%" + RequestData.SearchString + "%','') " +
                        "or SSM>Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                    "where Active = " + RequestData.IsActive + " " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or StyleStatusCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "or StatusName like isnull('%" + RequestData.SearchString + "%','') " +
                        "or Remarks like isnull('%" + RequestData.SearchString + "%','')) " +
                        "order by ID asc " +
                        "offset " + RequestData.Offset + " rows " +
                        "fetch first " + RequestData.Limit + " rows only";



                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objStyleStatusMasterTypes = new StyleStatusMasterType();
                        objStyleStatusMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleStatusMasterTypes.StyleStatusCode = objReader["StyleStatusCode"].ToString();
                        objStyleStatusMasterTypes.StatusName = objReader["StatusName"].ToString();

                        //objStyleStatusMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStyleStatusMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStyleStatusMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStyleStatusMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStyleStatusMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStyleStatusMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleStatusMasterTypes.Remarks = objReader["Remarks"].ToString();

                        StyleStatusMasterTypeList.Add(objStyleStatusMasterTypes);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleStatusMasterTypeList = StyleStatusMasterTypeList;
                    //ResponseData.ResponseDynamicData = StyleStatusMasterTypeList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Status Master");
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
    }
}
