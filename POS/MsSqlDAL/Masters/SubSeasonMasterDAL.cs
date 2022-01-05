using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SubSeasonMasterRequest;
using EasyBizResponse.Masters.SubSeasonMasterResponse;
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
    public class SubSeasonMasterDAL : BaseSubSeasonMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSubSeasonRequest)RequestObj;
            var ResponseData = new SaveSubSeasonResponse();
            var SubSeasonlist = RequestData.SubSeasonlist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateSubSeason", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (SubSeasonMaster objSubSeason in SubSeasonlist)
                {
                    sSql.Append("<SubSeasonMaster>");
                    sSql.Append("<ID>" + (objSubSeason.ID) + "</ID>");
                    sSql.Append("<SubSeasonCode>" + (objSubSeason.SubSeasonCode) + "</SubSeasonCode>");
                    sSql.Append("<SubSeasonName>" + objSubSeason.SubSeasonName + "</SubSeasonName>");
                    sSql.Append("<Active>" + (objSubSeason.Active) + "</Active>");
                    sSql.Append("<SeasonID>" + objSubSeason.SeasonID + "</SeasonID>");                    
                    sSql.Append("</SubSeasonMaster>");
                }
                var SubBrandData = _CommandObj.Parameters.Add("@SubSeasonData", SqlDbType.Xml);
                SubBrandData.Direction = ParameterDirection.Input;
                SubBrandData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@SubSIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sub Season");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Sub Season");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sub Season");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sub Season");
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
            var SubSeasonRecord = new SubSeasonMaster();
            var RequestData = (DeleteSubSeasonRequest)RequestObj;
            var ResponseData = new DeleteSubSeasonResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from SubSeasonMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "SubSeason Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "SubSeason Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubSeasonRecord = new SubSeasonMaster();
            var RequestData = (SelectBySubSeasonIDRequest)RequestObj;
            var ResponseData = new SelectBySubSeasonIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from SubSeasonMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubSeason = new SubSeasonMaster();
                        objSubSeason.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubSeason.SubSeasonCode = Convert.ToString(objReader["SubSeasonCode"]);
                        objSubSeason.SubSeasonName = Convert.ToString(objReader["SubSeasonName"]);
                        objSubSeason.SeasonID =objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) :0;                      
                        objSubSeason.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubSeason.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubSeason.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubSeason.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSubSeason.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubSeason.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.SubSeasonRecord = objSubSeason;
                        ResponseData.ResponseDynamicData = objSubSeason;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SubSeason Master");
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
            var SubSeasonList = new List<SubSeasonMaster>();
            var RequestData = (SelectAllSubSeasonRequest)RequestObj;
            var ResponseData = new SelectAllSubSeasonResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from SubSeasonMaster with(NoLock)";
               
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubSeason = new SubSeasonMaster();
                        objSubSeason.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubSeason.SubSeasonCode = Convert.ToString(objReader["SubSeasonCode"]);
                        objSubSeason.SubSeasonName = Convert.ToString(objReader["SubSeasonName"]);
                        objSubSeason.SeasonID =objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) :0;                       
                        objSubSeason.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubSeason.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubSeason.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubSeason.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSubSeason.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubSeason.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        SubSeasonList.Add(objSubSeason);
                    }
                    ResponseData.SubSeasonList = SubSeasonList;
                    ResponseData.ResponseDynamicData = SubSeasonList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SubSeason Master");
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
            var SubSeasonList = new List<SubSeasonMaster>();
            var RequestData = (SelectBySubSeasonIDsRequest)RequestObj;
            var ResponseData = new SelectBySubSeasonIDsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from SubSeasonMaster with(NoLock) where ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.IDs);
             
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubSeason = new SubSeasonMaster();
                        objSubSeason.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubSeason.SubSeasonCode = Convert.ToString(objReader["SubSeasonCode"]);
                        objSubSeason.SubSeasonName = Convert.ToString(objReader["SubSeasonName"]);
                        objSubSeason.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) : 0;
                        objSubSeason.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubSeason.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubSeason.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubSeason.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSubSeason.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubSeason.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        SubSeasonList.Add(objSubSeason);
                    }
                    ResponseData.SubSeasonList = SubSeasonList;
                    ResponseData.ResponseDynamicData = SubSeasonList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SubSeason Master");
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
        public override EasyBizResponse.Masters.SubSeasonMasterResponse.SelectSubSeasonLookUpResponse SelectSubSeasonLookUp(EasyBizRequest.Masters.SubSeasonMasterRequest.SelectSubSeasonLookUpRequest ObjRequest)
        {
            var SubSeasonList = new List<SubSeasonMaster>();
            var RequestData = (SelectSubSeasonLookUpRequest)ObjRequest;
            var ResponseData = new SelectSubSeasonLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[SubSeasonName] from SubSeasonMaster with(NoLock) where Active='true'";
               
                if (RequestData.SeasonID != null)
                {
                    sQuery = sQuery + " and SeasonID='" + RequestData.SeasonID + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubSeason = new SubSeasonMaster();
                        objSubSeason.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubSeason.SubSeasonName = Convert.ToString(objReader["SubSeasonName"]);
                        SubSeasonList.Add(objSubSeason);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SubSeasonList = SubSeasonList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SubSeason Master");
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

        public override EasyBizResponse.Masters.SubSeasonMasterResponse.SelectSeasonListForSubSeasonResponse SelectSubSeasonListBySeason(EasyBizRequest.Masters.SubSeasonMasterRequest.SelectSeasonListForSubSeasonRequest RequestObj)
        {
            var SeasonForSubSeasonList = new List<SubSeasonMaster>();
            var RequestData = (SelectSeasonListForSubSeasonRequest)RequestObj;
            var ResponseData = new SelectSeasonListForSubSeasonResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from SubSeasonMaster with(NoLock) where SeasonID='" + RequestData.SeasonID + "' ", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubSeason = new SubSeasonMaster();
                        objSubSeason.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubSeason.SubSeasonCode = Convert.ToString(objReader["SubSeasonCode"]);
                        objSubSeason.SubSeasonName = Convert.ToString(objReader["SubSeasonName"]);
                        objSubSeason.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) : 0;                      
                        objSubSeason.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubSeason.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubSeason.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubSeason.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSubSeason.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubSeason.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //SelectCollectionListByBrandsRequest objSelectCollectionListByBrandsRequest = new SelectCollectionListByBrandsRequest();
                        //objSelectCollectionListByBrandsRequest.BrandID = Convert.ToInt32(objReader["BrandID"]);
                        //objSelectCollectionListByBrandsRequest.SubBrandID = objMASSubBrand.ID = Convert.ToInt32(objReader["ID"]);
                        //objSelectCollectionListByBrandsRequest.ShowIsActiveRecords = true;

                        //SelectCollectionListByBrandsResponse objSelectCollectionListByBrandsResponse = new SelectCollectionListByBrandsResponse();
                        //objSelectCollectionListByBrandsResponse = _MASCollectionDAL.SelectCollectionListByBrands(objSelectCollectionListByBrandsRequest);
                        //if (objSelectCollectionListByBrandsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objMASSubBrand.CollectionList = objSelectCollectionListByBrandsResponse.CollectionList;
                        //}
                        //else
                        //{
                        //    objMASSubBrand.CollectionList = new List<MASCollection>();
                        //}
                        //objSubBrand.CollectionList = new List<MASCollection>();
                        SeasonForSubSeasonList.Add(objSubSeason);
                    }
                    ResponseData.SubSeasonList = SeasonForSubSeasonList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SubSeason Master");
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
