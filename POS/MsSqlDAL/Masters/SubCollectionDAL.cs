using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using EasyBizResponse.Masters.SubCollectionResponse;
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
    public class SubCollectionDAL : BaseSubCollectionDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSubCollectionRequest)RequestObj;
            var ResponseData = new SaveSubCollectionResponse();
            var SubSubCollectionMaster = RequestData.SubCollectionMasterlist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateSubCollection", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (SubCollectionMaster objSubCollectionMaster in SubSubCollectionMaster)
                {
                    sSql.Append("<SubCollectionMaster>");
                    sSql.Append("<ID>" + (objSubCollectionMaster.ID) + "</ID>");
                    sSql.Append("<SubCollectionCode>" + (objSubCollectionMaster.SubCollectionCode) + "</SubCollectionCode>");
                    sSql.Append("<SubCollectionName>" + objSubCollectionMaster.SubCollectionName + "</SubCollectionName>");
                    sSql.Append("<Active>" + (objSubCollectionMaster.Active) + "</Active>");
                    sSql.Append("<IsDeleted>" + (objSubCollectionMaster.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<CollectionID>" + objSubCollectionMaster.CollectionID + "</CollectionID>");
                    sSql.Append("<CollectionCode>" + objSubCollectionMaster.CollectionCode + "</CollectionCode>");
                    sSql.Append("<CreateBy>" + objSubCollectionMaster.CreateBy + "</CreateBy>");
                    sSql.Append("<UpdateBy>" + objSubCollectionMaster.UpdateBy + "</UpdateBy>");
                    sSql.Append("<SCN>" + objSubCollectionMaster.SCN + "</SCN>");
                    sSql.Append("</SubCollectionMaster>");
                }
                var SubCollectionMaster = _CommandObj.Parameters.Add("@SubCollectionMaster", SqlDbType.Xml);
                SubCollectionMaster.Direction = ParameterDirection.Input;
                SubCollectionMaster.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@SubCIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sub Collection");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Sub Collection");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage =  "Sub Collection Updated Successfully";
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "5")
                {
                    ResponseData.DisplayMessage = "Updated Successfully, one or more Subcollection used in Design Master, which can't be removed";
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "4")
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.ItIsInRelationdhip.Replace("{}", "Sub Collection");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sub Collection");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            } 
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubCollectionMaster = new SubCollectionMaster();
            var RequestData = (DeleteSubCollectionRequest)RequestObj;
            var ResponseData = new DeleteSubCollectionResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from SubCollectionMaster where CollectionID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Sub Collection");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Sub Collection");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubCollectionMaster = new SubCollectionMaster();
            var RequestData = (SelectByIDSubCollectionRequest)RequestObj;
            var ResponseData = new SelectByIDSubCollectionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from SubCollectionMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubCollectionMaster = new SubCollectionMaster();
                        objSubCollectionMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubCollectionMaster.SubCollectionCode = Convert.ToString(objReader["SubCollectionCode"]);
                        objSubCollectionMaster.SubCollectionName = Convert.ToString(objReader["SubCollectionName"]);
                        objSubCollectionMaster.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;

                        objSubCollectionMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubCollectionMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubCollectionMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubCollectionMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSubCollectionMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubCollectionMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.SubCollectionMasterData = objSubCollectionMaster;
                        ResponseData.ResponseDynamicData = objSubCollectionMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Collection");
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
            var SubCollectionMasterList = new List<SubCollectionMaster>();
            var RequestData = (SelectAllSubCollectionRequest)RequestObj;
            var ResponseData = new SelectAllSubCollectionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select distinct SM.CollectionID,M.CollectionName,M.CollectionCode from SubCollectionMaster as SM with(NoLock) join CollectionMaster as M with(Nolock) on SM.CollectionID = M.ID and M.Active='True' Order by CollectionID";
               
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubCollectionMaster = new SubCollectionMaster();
                        //objSubCollectionMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSubCollectionMaster.SubCollectionCode = Convert.ToString(objReader["SubCollectionCode"]);
                        //objSubCollectionMaster.SubCollectionName = Convert.ToString(objReader["SubCollectionName"]);
                        objSubCollectionMaster.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objSubCollectionMaster.CollectionCode = Convert.ToString(objReader["CollectionCode"]);
                        objSubCollectionMaster.CollectionName = Convert.ToString(objReader["CollectionName"]);
                        //objSubCollectionMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSubCollectionMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSubCollectionMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSubCollectionMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objSubCollectionMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objSubCollectionMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //SelectSubCollectionListForCollectionRequest objSelectsubCollectionRequest = new SelectSubCollectionListForCollectionRequest();
                        //SelectSubCollectionListForCollectionResponse objSelectsubCollectionResponse = new SelectSubCollectionListForCollectionResponse();
                        //objSelectsubCollectionRequest.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        //objSelectsubCollectionRequest.ShowInActiveRecords = true;
                        ////objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        //objSelectsubCollectionResponse = SelectSubCollectionByCollection(objSelectsubCollectionRequest);
                        //if (objSelectsubCollectionResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objSubCollectionMaster.SubCollectionMasterlist = objSelectsubCollectionResponse.ResponseDynamicData;
                        //}

                        SubCollectionMasterList.Add(objSubCollectionMaster);

                    }

                    ResponseData.SubCollectionMasterList = SubCollectionMasterList;
                    //ResponseData.ResponseDynamicData = SubCollectionMasterList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Collection");
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
            var SubCollectionMasterList = new List<SubCollectionMaster>();
            var RequestData = (SelectByIDsSubCollectionRequest)RequestObj;
            var ResponseData = new SelectByIDsSubCollectionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from SubCollectionMaster with(NoLock) where ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.IDs);
               
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubCollectionMaster = new SubCollectionMaster();
                        objSubCollectionMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubCollectionMaster.SubCollectionCode = Convert.ToString(objReader["SubCollectionCode"]);
                        objSubCollectionMaster.SubCollectionName = Convert.ToString(objReader["SubCollectionName"]);
                        objSubCollectionMaster.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        
                        objSubCollectionMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubCollectionMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubCollectionMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubCollectionMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSubCollectionMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubCollectionMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        SubCollectionMasterList.Add(objSubCollectionMaster);

                    }

                    ResponseData.SubCollectionMasterList = SubCollectionMasterList;
                    ResponseData.ResponseDynamicData = SubCollectionMasterList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Collection");
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

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.Masters.SubCollectionResponse.SelectSubCollectionLookUpResponse SelectSubCollectionLookUp(EasyBizRequest.Masters.SubCollectionRequest.SelectSubCollectionLookUpRequest ObjRequest)
        {
            var SubCollectionMasterList = new List<SubCollectionMaster>();
            var RequestData = (SelectSubCollectionLookUpRequest)ObjRequest;
            var ResponseData = new SelectSubCollectionLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sSql = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sSql = "Select * from SubCollectionMaster with(NoLock) where Active='true'";

                if (RequestData.CollectionID != null)
                {
                    sSql = sSql + " and CollectionID='" + RequestData.CollectionID + "'";
                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubCollectionMaster = new SubCollectionMaster();
                        objSubCollectionMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubCollectionMaster.SubCollectionName = Convert.ToString(objReader["SubCollectionName"]);
                        objSubCollectionMaster.SubCollectionCode = Convert.ToString(objReader["SubCollectionCode"]);



                        SubCollectionMasterList.Add(objSubCollectionMaster);

                    }

                    ResponseData.SubCollectionMasterLookUpList = SubCollectionMasterList;

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Collection");
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

        public override EasyBizRequest.Masters.SubCollectionResponse.SelectSubCollectionListForCollectionResponse SelectSubCollectionByCollection(EasyBizRequest.Masters.SubCollectionRequest.SelectSubCollectionListForCollectionRequest RequestObj)
        {
            var SubCollectionMasterList = new List<SubCollectionMaster>();
            var RequestData = (SelectSubCollectionListForCollectionRequest)RequestObj;
            var ResponseData = new SelectSubCollectionListForCollectionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select SCM.*,CM.CollectionName from SubCollectionMaster SCM with(NoLock) left outer Join CollectionMaster CM on  SCM.CollectionID=CM.ID where CollectionID='" + RequestData.CollectionID + "' and CM.Active='" + RequestData.ShowInActiveRecords + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubCollectionMaster = new SubCollectionMaster();
                        objSubCollectionMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubCollectionMaster.SubCollectionCode = Convert.ToString(objReader["SubCollectionCode"]);
                        objSubCollectionMaster.SubCollectionName = Convert.ToString(objReader["SubCollectionName"]);
                        objSubCollectionMaster.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objSubCollectionMaster.CollectionName = Convert.ToString(objReader["CollectionName"]);
                        objSubCollectionMaster.CollectionCode = Convert.ToString(objReader["CollectionCode"]);
                        objSubCollectionMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubCollectionMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubCollectionMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubCollectionMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSubCollectionMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubCollectionMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        SubCollectionMasterList.Add(objSubCollectionMaster);
                    }
                    ResponseData.ResponseDynamicData = SubCollectionMasterList;
                    ResponseData.SubCollectionMasterList = SubCollectionMasterList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Collection");
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

        public override SelectAllSubCollectionResponse SelectAllSubCollectionDetails(SelectAllSubCollectionRequest RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectAllSubCollectionResponse API_SelectALL(SelectAllSubCollectionRequest requestData)
        {
            var SubCollectionMasterList = new List<SubCollectionMaster>();
            var RequestData = (SelectAllSubCollectionRequest)requestData;
            var ResponseData = new SelectAllSubCollectionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //string sSql = "Select distinct SM.CollectionID,M.CollectionName,M.CollectionCode from SubCollectionMaster as SM with(NoLock) join CollectionMaster as M with(Nolock) on SM.CollectionID = M.ID and M.Active='True' Order by CollectionID";

                string sSql = "Select distinct SM.CollectionID,M.CollectionName,M.CollectionCode,M.Active,RC.TOTAL_CNT [RecordCount] " +
                   "from SubCollectionMaster as SM with(NoLock) " +
                   "join CollectionMaster as M with(Nolock) on SM.CollectionID = M.ID and M.Active='True' " +
                   "LEFT JOIN(Select distinct count(SM1.ID) As TOTAL_CNT From SubCollectionMaster SM1 with(NoLock) " +
                        "join CollectionMaster as M1 with(Nolock) on SM1.CollectionID = M1.ID and M1.Active = 'True' " +
                        "where M1.Active = " + RequestData.IsActive + "" +
                        " and(isnull('" + RequestData.SearchString + "','') = '' " +
                        "or M1.CollectionName like isnull('%" + RequestData.SearchString + "%','') " +
                        "or M1.CollectionCode like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                        "where M.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or M.CollectionName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or M.CollectionCode like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by SM.CollectionID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubCollectionMaster = new SubCollectionMaster();
                        //objSubCollectionMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSubCollectionMaster.SubCollectionCode = Convert.ToString(objReader["SubCollectionCode"]);
                        //objSubCollectionMaster.SubCollectionName = Convert.ToString(objReader["SubCollectionName"]);
                        objSubCollectionMaster.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objSubCollectionMaster.CollectionCode = Convert.ToString(objReader["CollectionCode"]);
                        objSubCollectionMaster.CollectionName = Convert.ToString(objReader["CollectionName"]);
                        //objSubCollectionMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSubCollectionMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSubCollectionMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSubCollectionMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objSubCollectionMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubCollectionMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //SelectSubCollectionListForCollectionRequest objSelectsubCollectionRequest = new SelectSubCollectionListForCollectionRequest();
                        //SelectSubCollectionListForCollectionResponse objSelectsubCollectionResponse = new SelectSubCollectionListForCollectionResponse();
                        //objSelectsubCollectionRequest.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        //objSelectsubCollectionRequest.ShowInActiveRecords = true;
                        ////objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        //objSelectsubCollectionResponse = SelectSubCollectionByCollection(objSelectsubCollectionRequest);
                        //if (objSelectsubCollectionResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objSubCollectionMaster.SubCollectionMasterlist = objSelectsubCollectionResponse.ResponseDynamicData;
                        //}

                        SubCollectionMasterList.Add(objSubCollectionMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;

                    }

                    ResponseData.SubCollectionMasterList = SubCollectionMasterList;
                    //ResponseData.ResponseDynamicData = SubCollectionMasterList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Collection");
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
