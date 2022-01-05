using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
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
    public class StoreGroupMasterDAL : BaseStoreGroupMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public int StoreGroupID;
     
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveStoreGroupRequest)RequestObj;
            var ResponseData = new SaveStoreGroupResponse();

            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            SqlDataReader objReader;

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertStoreOrUpdateGroupMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StoreGroupMasterData.ID;

                var StoreGroupCode = _CommandObj.Parameters.Add("@StoreGroupCode", SqlDbType.NVarChar);
                StoreGroupCode.Direction = ParameterDirection.Input;
                StoreGroupCode.Value = RequestData.StoreGroupMasterData.StoreGroupCode;

                var StoreGroupName = _CommandObj.Parameters.Add("@StoreGroupName", SqlDbType.NVarChar);
                StoreGroupName.Direction = ParameterDirection.Input;
                StoreGroupName.Value = RequestData.StoreGroupMasterData.StoreGroupName;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.StoreGroupMasterData.Description;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.StoreGroupMasterData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StoreGroupMasterData.CreateBy;
               
                var StoreGroupDetails = _CommandObj.Parameters.Add("@StoreGroupDetails", SqlDbType.Xml);
                StoreGroupDetails.Direction = ParameterDirection.Input;
                StoreGroupDetails.Value = StoreGroupDetailsXML(RequestData.StoreGroupDetailsList);
               
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();
                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Store Group");                   
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    ResponseData.IDs = ID2.Value.ToString(); 

                }
                else if (strStatusCode == "2" )
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Store Group Code");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Group");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StoreGroup Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            //var RequestData = (UpdateStoreGroupRequest)RequestObj;
            //var ResponseData = new UpdateStoreGroupResponse();

            //var sqlCommon = new MsSqlCommon();
            //try
            //{

            //    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

            //    _CommandObj = new SqlCommand("UpdateStoreGroupMaster", _ConnectionObj);
            //    _CommandObj.CommandType = CommandType.StoredProcedure;

            //    var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
            //    ID.Direction = ParameterDirection.Input;
            //    ID.Value = RequestData.StoreGroupMasterData.ID;

            //    var StoreGroupCode = _CommandObj.Parameters.Add("@StoreGroupCode", SqlDbType.NVarChar);
            //    StoreGroupCode.Direction = ParameterDirection.Input;
            //    StoreGroupCode.Value = RequestData.StoreGroupMasterData.StoreGroupCode;

            //    var StoreGroupName = _CommandObj.Parameters.Add("@StoreGroupName", SqlDbType.NVarChar);
            //    StoreGroupName.Direction = ParameterDirection.Input;
            //    StoreGroupName.Value = RequestData.StoreGroupMasterData.StoreGroupName;

            //    var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
            //    Description.Direction = ParameterDirection.Input;
            //    Description.Value = RequestData.StoreGroupMasterData.Description;

            //    var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
            //    SCN.Direction = ParameterDirection.Input;
            //    SCN.Value = RequestData.StoreGroupMasterData.SCN;

            //    var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
            //    UpdateBy.Direction = ParameterDirection.Input;
            //    UpdateBy.Value = RequestData.StoreGroupMasterData.UpdateBy;

            //    var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
            //    StatusCode.Direction = ParameterDirection.Output;

            //    var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
            //    StatusMsg.Direction = ParameterDirection.Output;

            //    _CommandObj.CommandType = CommandType.StoredProcedure;
            //    _CommandObj.ExecuteNonQuery();

            //    var strStatusCode = StatusCode.Value.ToString();
            //    if (strStatusCode == "1")
            //    {
            //        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "StoreGroup Master");
            //        ResponseData.StatusCode = Enums.OpStatusCode.Success;
            //    }
            //    else if (strStatusCode == "2")
            //    {
            //        ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "StoreGroup Master");
            //    }
            //    else if (strStatusCode == "3")
            //    {
            //        ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
            //    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StoreGroup Master");
            //    ResponseData.ExceptionMessage = ex.Message;
            //    ResponseData.StackTrace = ex.StackTrace;
            //}
            //finally
            //{
            //    sqlCommon.CloseConnection(_ConnectionObj);
            //}

            //return ResponseData;
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var StoreGroupMasterRecord = new StoreGroupMaster();

            var RequestData = (DeleteStoreGroupRequest)RequestObj;
            var ResponseData = new DeleteStoreGroupResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from StoreGroupDetails where StoreGroupID='{0}';Delete from StoreGroupMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);   
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "StoreGroup Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "StoreGroup Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var StoreGroupMasterRecord = new StoreGroupMaster();       
            
            var RequestData = (SelectByIDStoreGroupRequest)RequestObj;
            var ResponseData = new SelectByIDStoreGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                string sSql = "Select * from StoreGroupMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);   
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreGroupMaster = new StoreGroupMaster();
                        objStoreGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreGroupMaster.StoreGroupCode = Convert.ToString(objReader["StoreGroupCode"]);
                        objStoreGroupMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objStoreGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStoreGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStoreGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStoreGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStoreGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        objStoreGroupMaster.StoreGroupDetailsList = new List<StoreGroupDetails>();

                        SelectStoreGroupDetailsRequest objSelectStoreGroupDetailsRequest = new SelectStoreGroupDetailsRequest();
                        SelectStoreGroupDetailsResponse objSelectStoreGroupDetailsResponse = new SelectStoreGroupDetailsResponse();
                        objSelectStoreGroupDetailsRequest.ShowInActiveRecords = true;
                        objSelectStoreGroupDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectStoreGroupDetailsResponse = SelectStoreGroupDetails(objSelectStoreGroupDetailsRequest);
                        if (objSelectStoreGroupDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStoreGroupMaster.StoreGroupDetailsList = objSelectStoreGroupDetailsResponse.StoreGroupDetailsList;
                        }

                        
                        
                        ResponseData.StoreGroupMasterRecord = objStoreGroupMaster;
                        ResponseData.ResponseDynamicData = objStoreGroupMaster;
                        ResponseData.ResponseDynamicData.StoreGroupDetailsList = objSelectStoreGroupDetailsResponse.StoreGroupDetailsList;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreGroup Master");
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
            var StoreGroupMasterList = new List<StoreGroupMaster>();
            var RequestData = (SelectAllStoreGroupRequest)RequestObj;
            var ResponseData = new SelectAllStoreGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select * from StoreGroupMaster with(NoLock)";
              
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreGroupMaster = new StoreGroupMaster();
                        objStoreGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreGroupMaster.StoreGroupCode = Convert.ToString(objReader["StoreGroupCode"]);
                        objStoreGroupMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objStoreGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStoreGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStoreGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStoreGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStoreGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StoreGroupMasterList.Add(objStoreGroupMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGroupMasterList = StoreGroupMasterList;
                    ResponseData.ResponseDynamicData = StoreGroupMasterList;
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            var StoreGroupList = new List <StoreGroupMaster>();

            var RequestData = (SelectByIDsStoreGroupRequest)RequestObj;
            var ResponseData = new SelectByIDsStoreGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from StoreGroupMaster with(NoLock) where  ID in '{0}'";
                sSql = string.Format(sSql, RequestData.IDs);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreGroupMaster = new StoreGroupMaster();
                        objStoreGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreGroupMaster.StoreGroupCode = Convert.ToString(objReader["StoreGroupCode"]);
                        objStoreGroupMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objStoreGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStoreGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStoreGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStoreGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStoreGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StoreGroupList.Add(objStoreGroupMaster);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGroupMasterList = StoreGroupList;
                    ResponseData.ResponseDynamicData = StoreGroupList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreGroup Master");
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

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectStoreGroupLookUpResponse SelectStoreGroupLookUp(SelectStoreGroupLookUpRequest RequestObj)
        {
            var StoreGroupMasterList = new List<StoreGroupMaster>();
            var RequestData =  (SelectStoreGroupLookUpRequest)RequestObj;
            var ResponseData = new SelectStoreGroupLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                if (RequestData.FormType == "Reports")
                {
                    sQuery = "Select distinct sgm.ID,sgm.StoreGroupName,sgm.StoreGroupCode from StoreGroupMaster SGM inner join StoreMaster SM on sgm.ID=sm.StoreGroupID  where sm.CountryID='" + RequestData.CountryID + "' and  sgm.Active='True' ";
                }
                else
                {
                    sQuery = "Select ID,StoreGroupName,StoreGroupCode from StoreGroupMaster with(NoLock) where Active='True' ";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreGroupMaster = new StoreGroupMaster();
                        objStoreGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreGroupMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreGroupMaster.StoreGroupCode = Convert.ToString(objReader["StoreGroupCode"]);
                        StoreGroupMasterList.Add(objStoreGroupMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGroupMasterList = StoreGroupMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreGroup Master");
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


        public override SelectAllStoreGroupDetailsResponse SelectAllStoreGroupDetails(SelectAllStoreGroupDetailsRequest ObjRequest)
        {
            var StoreGroupDetailsList = new List<StoreGroupDetails>();
            var RequestData = (SelectAllStoreGroupDetailsRequest)ObjRequest;
            var ResponseData = new SelectAllStoreGroupDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select ID as ProductGroupID, ProductGroupCode,ProductGroupName , 0 As MIN,0 As MAX,0 as AVG from productGroupMaster   ");                
                sSql.Append("where Active='" + RequestData.ShowInActiveRecords + "' ");
                sSql.Append("order by id  asc");                
                //sSql.Append("select PGM.ID as ProductGroupID,PGM.ProductGroupCode,PGM.ProductGroupName,ISNULL(SGD.Min,0) As MIN,ISNULL(SGD.Max,0) AS MAX,ISNULL(SGD.Avg,0) AS AVG,PGM.Active  ");
                //sSql.Append("from StoreGroupDetails  SGD right outer join  ProductGroupMaster PGM  on SGD.ProductGroupID=PGM.ID  ");
                //sSql.Append("where PGM.Active='" + RequestData.ShowInActiveRecords + "' ");
                //sSql.Append("order by PGM.id  asc");                
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreGroupDetails = new StoreGroupDetails();
                        objStoreGroupDetails.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objStoreGroupDetails.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        objStoreGroupDetails.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);                       
                        objStoreGroupDetails.Min = objReader["Min"] != DBNull.Value ? Convert.ToInt32(objReader["Min"]) :0;
                        objStoreGroupDetails.Max = objReader["Max"] != DBNull.Value ? Convert.ToInt32(objReader["Max"]) :0;
                        objStoreGroupDetails.Avg = objReader["Avg"] != DBNull.Value ? Convert.ToInt32(objReader["Avg"]) : 0;
                        objStoreGroupDetails.Active = true;
                        StoreGroupDetailsList.Add(objStoreGroupDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGroupDetailsList = StoreGroupDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreGroup Master");
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

        public override SelectStoreGroupDetailsResponse SelectStoreGroupDetails(SelectStoreGroupDetailsRequest ObjRequest)
        {
            var StoreGroupDetailsList = new List<StoreGroupDetails>();
            var RequestData = (SelectStoreGroupDetailsRequest)ObjRequest;
            var ResponseData = new SelectStoreGroupDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select SGD.ID as ID,PGM.ID as ProductGroupID,PGM.ProductGroupCode,PGM.ProductGroupName,ISNULL(SGD.Min,0) As MIN,ISNULL(SGD.Max,0) AS MAX,ISNULL(SGD.Avg,0) AS AVG,PGM.Active,  ");
                sSql.Append("SGD.StoreGroupID  as StoreGroupID from StoreGroupDetails  SGD right outer join  ProductGroupMaster PGM  on SGD.ProductGroupID=PGM.ID  ");
                sSql.Append("where PGM.Active='" + RequestData.ShowInActiveRecords + "' and SGD.StoreGroupID="+ RequestData.ID+" ");
                sSql.Append("order by PGM.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreGroupDetails = new StoreGroupDetails();
                        objStoreGroupDetails.ID =objReader["Min"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        objStoreGroupDetails.ProductGroupID =objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) :0;
                        objStoreGroupDetails.StoreGroupID =objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) :0;
                        objStoreGroupDetails.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objStoreGroupDetails.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        objStoreGroupDetails.Min =objReader["Min"] != DBNull.Value ? Convert.ToInt32(objReader["Min"]) :0;
                        objStoreGroupDetails.Max =objReader["Max"] != DBNull.Value ? Convert.ToInt32(objReader["Max"]) :0;
                        objStoreGroupDetails.Avg = objReader["Avg"] != DBNull.Value ? Convert.ToInt32(objReader["Avg"]) : 0;
                        objStoreGroupDetails.Active = true;
                        StoreGroupDetailsList.Add(objStoreGroupDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGroupDetailsList = StoreGroupDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreGroup Master");
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
        private string StoreGroupDetailsXML(List<StoreGroupDetails> StoreGroupDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (StoreGroupDetails objStoreGroupDetails in StoreGroupDetailList)
            {
                sSql.Append("<ProductGroupData>");
                sSql.Append("<ID>" + (objStoreGroupDetails.ID) + "</ID>");
                sSql.Append("<ProductGroupID>" + (objStoreGroupDetails.ProductGroupID) + "</ProductGroupID>");
                sSql.Append("<ProductGroupCode>" + (objStoreGroupDetails.ProductGroupCode) + "</ProductGroupCode>");
                sSql.Append("<Min>" + objStoreGroupDetails.Min + "</Min>");
                sSql.Append("<Max>" + (objStoreGroupDetails.Max) + "</Max>");
                sSql.Append("<Avg>" + objStoreGroupDetails.Avg + "</Avg>");
                sSql.Append("<SCN>" + objStoreGroupDetails.SCN + "</SCN>");
                sSql.Append("<Active>" + objStoreGroupDetails.Active + "</Active>");
                sSql.Append("<CreateBy>" + objStoreGroupDetails.CreateBy + "</CreateBy>");
                sSql.Append("</ProductGroupData>");
            }
            return sSql.ToString();
        }

        public override SelectAllStoreGroupResponse API_SelectALL(SelectAllStoreGroupRequest requestData)
        {
            var StoreGroupMasterList = new List<StoreGroupMaster>();
            var RequestData = (SelectAllStoreGroupRequest)requestData;
            var ResponseData = new SelectAllStoreGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //sQuery = "Select * from StoreGroupMaster with(NoLock)";

                sQuery = "Select ID, StoreGroupCode, StoreGroupName, Description, Active, RC.TOTAL_CNT [RecordCount]  " +
                   "from StoreGroupMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(SG.ID) As TOTAL_CNT From StoreGroupMaster SG with(NoLock)" +
                   "where SG.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or SG.StoreGroupCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or SG.StoreGroupName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or SG.Description like isnull('%" + RequestData.SearchString + "%','')) )  As RC ON 1 = 1 " +

                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or StoreGroupCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or StoreGroupName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Description like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objStoreGroupMaster = new StoreGroupMaster();
                        objStoreGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreGroupMaster.StoreGroupCode = Convert.ToString(objReader["StoreGroupCode"]);
                        objStoreGroupMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        //objStoreGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStoreGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStoreGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStoreGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStoreGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StoreGroupMasterList.Add(objStoreGroupMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGroupMasterList = StoreGroupMasterList;
                    //ResponseData.ResponseDynamicData = StoreGroupMasterList;
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
