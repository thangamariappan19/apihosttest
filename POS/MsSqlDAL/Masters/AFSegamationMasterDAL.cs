using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using EasyBizResponse.Masters.AFSegamationMasterResponse;
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
    public class AFSegamationMasterDAL : BaseAFSegamationMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;  

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveAFSegamationMasterRequest)RequestObj;
            var ResponseData = new SaveAFSegamationMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertOrUpdateAFSegamationMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter AFSegmentID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                AFSegmentID.Direction = ParameterDirection.Input;
                AFSegmentID.Value = RequestData.AFSegamationMasterTypesRecord.ID;

                SqlParameter AFSegamationCode = _CommandObj.Parameters.Add("@AFSegamationCode", SqlDbType.NVarChar);
                AFSegamationCode.Direction = ParameterDirection.Input;
                AFSegamationCode.Value = RequestData.AFSegamationMasterTypesRecord.AFSegamationCode;

                SqlParameter AFSegamationName = _CommandObj.Parameters.Add("@AFSegamationName", SqlDbType.NVarChar);
                AFSegamationName.Direction = ParameterDirection.Input;
                AFSegamationName.Value = RequestData.AFSegamationMasterTypesRecord.AFSegamationName;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.AFSegamationMasterTypesRecord.Remarks;

                SqlParameter CodeLength = _CommandObj.Parameters.Add("@CodeLength", SqlDbType.Int);
                CodeLength.Direction = ParameterDirection.Input;
                CodeLength.Value = RequestData.AFSegamationMasterTypesRecord.CodeLength;

                SqlParameter UseSeperator = _CommandObj.Parameters.Add("@UseSeperator", SqlDbType.NVarChar);
                UseSeperator.Direction = ParameterDirection.Input;
                UseSeperator.Value = RequestData.AFSegamationMasterTypesRecord.UseSeperator;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.AFSegamationMasterTypesRecord.Active;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.AFSegamationMasterTypesRecord.CreateBy;

                var AFSegamationDetails = _CommandObj.Parameters.Add("@AFSegamationDetails", SqlDbType.Xml);
                AFSegamationDetails.Direction = ParameterDirection.Input;
                AFSegamationDetails.Value = SegmentDetailMasterXML(RequestData.AFSegmentationDetailMasterList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID1", SqlDbType.VarChar, 10);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {

                    if (RequestData.AFSegamationMasterTypesRecord.ID == 0)
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Segmentation");
                    else
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Style Segmentation");
                   // ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Segmentation Master");

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Segmentation");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Segmentation");
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

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateAFSegamationMasterRequest)RequestObj;
            var ResponseData = new UpdateAFSegamationMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateAFSegamationMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.AFSegamationMasterTypesRecord.ID;

                SqlParameter AFSegamationCode = _CommandObj.Parameters.Add("@AFSegamationCode", SqlDbType.NVarChar);
                AFSegamationCode.Direction = ParameterDirection.Input;
                AFSegamationCode.Value = RequestData.AFSegamationMasterTypesRecord.AFSegamationCode;

                SqlParameter AFSegamationName = _CommandObj.Parameters.Add("@AFSegamationName", SqlDbType.NVarChar);
                AFSegamationName.Direction = ParameterDirection.Input;
                AFSegamationName.Value = RequestData.AFSegamationMasterTypesRecord.AFSegamationName;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.AFSegamationMasterTypesRecord.Remarks;

                SqlParameter UseSeperator = _CommandObj.Parameters.Add("@UseSeperator", SqlDbType.NVarChar);
                UseSeperator.Direction = ParameterDirection.Input;
                UseSeperator.Value = RequestData.AFSegamationMasterTypesRecord.UseSeperator;

                SqlParameter CodeLength = _CommandObj.Parameters.Add("@CodeLength", SqlDbType.Int);
                CodeLength.Direction = ParameterDirection.Input;
                CodeLength.Value = RequestData.AFSegamationMasterTypesRecord.CodeLength;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.AFSegamationMasterTypesRecord.UpdateBy;

                SqlParameter SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.AFSegamationMasterTypesRecord.SCN;
                            
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    if (RequestData.AFSegamationMasterTypesRecord.ID == 0)
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Segmentation");
                    else
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Style Segmentation");

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();         
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Segmentation");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Segmentation");
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

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {           
            var RequestData = (DeleteAFSegamationMasterRequest)RequestObj;
            var ResponseData = new DeleteAFSegamationMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Delete from AFSegamationMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Style Segmentation");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Style Segmentation");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var AFSegamationMasterTypesMaster = new AFSegamationMasterTypes();
            var RequestData = (SelectByIDAFSegamationMasterRequest)RequestObj;
            var ResponseData = new SelectByIDAFSegamationMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from AFSegamationMaster with(NoLock)  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objAFSegamationMasterTypes = new AFSegamationMasterTypes();

                        objAFSegamationMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objAFSegamationMasterTypes.AFSegamationCode = objReader["AFSegamationCode"] != DBNull.Value ? Convert.ToInt32(objReader["AFSegamationCode"]) : 0;
                        objAFSegamationMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objAFSegamationMasterTypes.Remarks = objReader["Remarks"].ToString();
                        objAFSegamationMasterTypes.CodeLength = objReader["CodeLength"] != DBNull.Value ? Convert.ToInt32(objReader["CodeLength"]) : 0;
                        objAFSegamationMasterTypes.UseSeperator = Convert.ToString(objReader["UseSeperator"]);


                        objAFSegamationMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objAFSegamationMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objAFSegamationMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objAFSegamationMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objAFSegamationMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objAFSegamationMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objAFSegamationMasterTypes.SegmentList = new List<SegmentMaster>();

                        SelectAFSegmationDetailsRequest objSelectAFSegmationDetailsRequest = new SelectAFSegmationDetailsRequest();
                        SelectAFSegamationDetailsResponse objSelectAFSegamationDetailsResponse = new SelectAFSegamationDetailsResponse();
                        objSelectAFSegmationDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectAFSegamationDetailsResponse = SelectAFSegmentationDetails(objSelectAFSegmationDetailsRequest);
                        if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objAFSegamationMasterTypes.SegmentList = objSelectAFSegamationDetailsResponse.AFSegmentDetailMasterRecord;
                        }

                        ResponseData.AFSegamationMasterTypesData = objAFSegamationMasterTypes;
                        ResponseData.ResponseDynamicData = objAFSegamationMasterTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Segmentation Master");
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
         
            var AFSegamationMasterTypesMaster = new List<AFSegamationMasterTypes>();
            var RequestData = (SelectAllAFSegamationMasterRequest)RequestObj;
            var ResponseData = new SelectAllAFSegamationMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from AFSegamationMaster";             

                 _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                 _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objAFSegamationMasterTypes = new AFSegamationMasterTypes();

                        objAFSegamationMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objAFSegamationMasterTypes.AFSegamationCode = objReader["AFSegamationCode"] != DBNull.Value ? Convert.ToInt32(objReader["AFSegamationCode"]) : 0;
                        objAFSegamationMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objAFSegamationMasterTypes.UseSeperator = objReader["UseSeperator"]!= DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : "";
                        objAFSegamationMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objAFSegamationMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objAFSegamationMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objAFSegamationMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objAFSegamationMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objAFSegamationMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objAFSegamationMasterTypes.SegmentList = new List<SegmentMaster>();

                        SelectAFSegmationDetailsRequest objSelectAFSegmationDetailsRequest = new SelectAFSegmationDetailsRequest();
                        SelectAFSegamationDetailsResponse objSelectAFSegamationDetailsResponse = new SelectAFSegamationDetailsResponse();
                        objSelectAFSegmationDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectAFSegamationDetailsResponse = SelectAFSegmentationDetails(objSelectAFSegmationDetailsRequest);
                        if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objAFSegamationMasterTypes.SegmentList = objSelectAFSegamationDetailsResponse.AFSegmentDetailMasterRecord;
                        }

                        AFSegamationMasterTypesMaster.Add(objAFSegamationMasterTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.AFSegamationMasterTypesList = AFSegamationMasterTypesMaster;
                    ResponseData.ResponseDynamicData = AFSegamationMasterTypesMaster;                    
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Segmentation Master");
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
            //var SegmentMasterList = new List<SegmentMaster>();

            //var RequestData = (SelectByIDsAFsegmentationRequest)RequestObj;
            //var ResponseData = new SelectByIDsAFsegmentationResponse();

            //var SegmentMasterdata = new SegmentMaster();

            //SqlDataReader objReader;

            //var sqlCommon = new MsSqlCommon();
            //try
            //{
            //    _ConnectionString = RequestData.ConnectionString;
            //    _RequestFrom = RequestData.RequestFrom;

            //    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

            //    string sSql = "Select * from DesignMaster with(NoLock) where ID='{0}'";
            //    sSql = string.Format(sSql, RequestData.ID);

            //    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

            //    _CommandObj.CommandType = CommandType.Text;
            //    objReader = _CommandObj.ExecuteReader();
            //    if (objReader.HasRows)
            //    {
            //        while (objReader.Read())
            //        {

            //            var objSegmentMaster = new SegmentMaster();

            //            objSegmentMaster.ID = objReader["ID"] != DBNull.Value ?  Convert.ToInt32(objReader["ID"]) :0;
            //            objSegmentMaster. = objReader["DesignCode"].ToString();
            //            objSegmentMaster.DesignName = objReader["DesignName"].ToString();
            //            objSegmentMaster.Remarks = objReader["Remarks"].ToString();

            //            objSegmentMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
            //            objSegmentMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
            //            objSegmentMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
            //            objSegmentMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
            //            objSegmentMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
            //            objSegmentMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

            //            DesignMasterTypesList.Add(objDesignMasterTypes);

            //        }

            //        ResponseData.StatusCode = Enums.OpStatusCode.Success;
            //        ResponseData.DesignMasterTypesList = DesignMasterTypesList;


            //    }
            //    else
            //    {
            //        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
            //        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design Master");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
            //    ResponseData.DisplayMessage = ex.Message;
            //}
            //return ResponseData;
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        //public override SelectSegmentationDetailsResponse SelectSAFSegamationDetails(SelectSegmentationDetailsRequest ObjRequest)
        //{
        //    var SegmentationList = new List<AFSegamationMasterTypes>();
        //    var RequestData = (SelectSegmentationDetailsRequest)ObjRequest;
        //    var ResponseData = new SelectSegmentationDetailsResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {

        //        var sSql = new StringBuilder();
        //        sSql.Append("select SegmentName,MaxLength from SegmentationMaster  ");
        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
        //        _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                var objSegmentationDetails = new AFSegamationMasterTypes();
        //                objSegmentationDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]):0;
        //                // objDocumentNumberingDetails.DocNumID = objReader["DocNumID"] != DBNull.Value ? Convert.ToInt32(objReader["DocNumID"]) :0;
        //                objSegmentationDetails.SegmentName = Convert.ToString(objReader["SegmentName"]);
        //                objSegmentationDetails.MaxLength = Convert.ToString(objReader["MaxLength"]);
        //                SegmentationList.Add(objSegmentationDetails);
        //            }
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //            ResponseData.AFSegamationMasterTypesList = SegmentationList;
        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SegmentationList");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    return ResponseData;
        //}

        public override SelectSegmentationDetailsResponse SelectSegmentationDetails(SelectSegmentationDetailsRequest ObjRequest)
        {
            var SegmentationList = new List<AFSegamationMasterTypes>();
            var RequestData = (SelectSegmentationDetailsRequest)ObjRequest;
            var ResponseData = new SelectSegmentationDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select SegmentName,MaxLength,UseSeperator from SegmentationMaster  ");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSegmentationDetails = new AFSegamationMasterTypes();
                        objSegmentationDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        // objDocumentNumberingDetails.DocNumID =objReader["DocNumID"] != DBNull.Value ? Convert.ToInt32(objReader["DocNumID"]):0;
                        objSegmentationDetails.SegmentName = Convert.ToString(objReader["SegmentName"]);
                        objSegmentationDetails.MaxLength = Convert.ToString(objReader["MaxLength"]);
                        objSegmentationDetails.UseSeperator = Convert.ToString(objReader["UseSeperator"]);
                        SegmentationList.Add(objSegmentationDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.AFSegamationMasterTypesList = SegmentationList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SegmentationList");
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
        public string SegmentDetailMasterXML(List<SegmentMaster> AFSegmentationDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (SegmentMaster objAFSegmentationDetailMasterDetails in AFSegmentationDetailMasterList)
            {
                sSql.Append("<AFSegmentMasterData>");
                sSql.Append("<ID>" + (objAFSegmentationDetailMasterDetails.ID) + "</ID>");
                sSql.Append("<IsUsed>" + objAFSegmentationDetailMasterDetails.IsUsed + "</IsUsed>");
                sSql.Append("<SegmentName>" + (objAFSegmentationDetailMasterDetails.SegmentName) + "</SegmentName>");
                sSql.Append("<MaxLength>" + objAFSegmentationDetailMasterDetails.MaxLength + "</MaxLength>");
                sSql.Append("<DefaultDescription>" + objAFSegmentationDetailMasterDetails.DefaultDescription + "</DefaultDescription>");
                sSql.Append("<CreateBy>" + objAFSegmentationDetailMasterDetails.CreateBy + "</CreateBy>");
                sSql.Append("<SCN>" + objAFSegmentationDetailMasterDetails.SCN + "</SCN>");
                
                sSql.Append("</AFSegmentMasterData>");
            }
            return sSql.ToString();
        }

        public override SelectAFSegamationDetailsResponse SelectAFSegmentationDetails(SelectAFSegmationDetailsRequest ObjRequest)
        {
            var AFSegmentDetailMasterList = new List<SegmentMaster>();
            var RequestData = (SelectAFSegmationDetailsRequest)ObjRequest;
            var ResponseData = new SelectAFSegamationDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select ACM.ID,ACM.SegmentHeaderID, ACM.IsUsed,ACM.SegmentName,ACM.MaxLength,DefaultDescription,acm.active from AFSegamationMaster SM join AFSegamationDetails ACM on sm.ID=ACM.SegmentHeaderID ");
                sSql.Append("where ACM.Active='" + RequestData.ShowInActiveRecords + "' and SM.ID=" + RequestData.ID + " ");
                sSql.Append("order by ACM.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objAFSegmentDetailMaster = new SegmentMaster();
                        objAFSegmentDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objAFSegmentDetailMaster.SegmentHeaderID = objReader["SegmentHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["SegmentHeaderID"]) : 0;
                        objAFSegmentDetailMaster.IsUsed = objReader["IsUsed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsUsed"]) : true;
                        objAFSegmentDetailMaster.SegmentName = Convert.ToString(objReader["SegmentName"]);
                        objAFSegmentDetailMaster.DefaultDescription = objReader["DefaultDescription"] != DBNull.Value ? Convert.ToBoolean(objReader["DefaultDescription"]) : true;
                        objAFSegmentDetailMaster.MaxLength = objReader["MaxLength"] != DBNull.Value ? Convert.ToInt32(objReader["MaxLength"]) : 0;
                        objAFSegmentDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        AFSegmentDetailMasterList.Add(objAFSegmentDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.AFSegmentDetailMasterRecord = AFSegmentDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Segmentation Master");
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

        public override SelectAfSegmentationLookUpResponse SelectAfSegmentationLookUp(SelectAFsegmentationLookUpRequest ObjRequest)
        {
            var AfSegementationList = new List<AFSegamationMasterTypes>();
            var RequestData = (SelectAFsegmentationLookUpRequest)ObjRequest;
            var ResponseData = new SelectAfSegmentationLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                sQuery = "select Id,AFSegamationCode,AFSegamationName,UseSeperator from AFSegamationMaster with(NoLock) where Active='true'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objAfSegmentation = new AFSegamationMasterTypes();

                        objAfSegmentation.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objAfSegmentation.AFSegamationCode = objReader["AFSegamationCode"] != DBNull.Value ? Convert.ToInt32(objReader["AFSegamationCode"]) : 0;
                        objAfSegmentation.AFSegamationName = Convert.ToString(objReader["AFSegamationName"]);
                        objAfSegmentation.UseSeperator = Convert.ToString(objReader["UseSeperator"]);
                        AfSegementationList.Add(objAfSegmentation);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.AFSegmentationMaster = AfSegementationList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", " Style Segementation Master");
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

        public override SelectSegamationDetailsLookUpResponse SelectSegamationDetailsLookUp(SelectSegamationDetailsLookUpRequest ObjRequest)
        {
            var SegmentDetailsList = new List<SegmentMaster>();
            var RequestData = (SelectSegamationDetailsLookUpRequest)ObjRequest;
            var ResponseData = new SelectSegamationDetailsLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,IsUsed,SegmentName,MaxLength,DefaultDescription from AFSegamationDetails with(NoLock) where Active='True' and SegmentHeaderID=" + RequestData.ID + "";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSegmentDeatils = new SegmentMaster();
                        objSegmentDeatils.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSegmentDeatils.IsUsed = objReader["IsUsed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsUsed"]) : true;
                        objSegmentDeatils.SegmentName = Convert.ToString(objReader["SegmentName"]);
                        objSegmentDeatils.MaxLength = objReader["MaxLength"] != DBNull.Value ? Convert.ToInt32(objReader["MaxLength"]) : 0;
                        objSegmentDeatils.DefaultDescription = objReader["DefaultDescription"] != DBNull.Value ? Convert.ToBoolean(objReader["DefaultDescription"]) : true;
                        SegmentDetailsList.Add(objSegmentDeatils);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SegmentDetailList = SegmentDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", " Style Segementation Master");
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

        public override SelectAllAFSegamationMasterResponse API_SelectALL(SelectAllAFSegamationMasterRequest requestData)
        {
            var AFSegamationMasterTypesMaster = new List<AFSegamationMasterTypes>();
            var RequestData = (SelectAllAFSegamationMasterRequest)requestData;
            var ResponseData = new SelectAllAFSegamationMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from AFSegamationMaster";

                string sSql = "Select ID, AFSegamationCode, AFSegamationName, Active, RC.TOTAL_CNT [RecordCount]  " +
                   "from AFSegamationMaster " +
                   "LEFT JOIN(Select count(SM.ID) As TOTAL_CNT From AFSegamationMaster SM "+
                          "where SM.Active =" + RequestData.IsActive + " " + 
                          "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or SM.AFSegamationCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or SM.AFSegamationName like isnull('%" + RequestData.SearchString + "%', ''))) As RC ON 1 = 1 " +
                      "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or AFSegamationCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or AFSegamationName like isnull('%" + RequestData.SearchString + "%','')) " +                      
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
                        var objAFSegamationMasterTypes = new AFSegamationMasterTypes();

                        objAFSegamationMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objAFSegamationMasterTypes.AFSegamationCode = objReader["AFSegamationCode"] != DBNull.Value ? Convert.ToInt32(objReader["AFSegamationCode"]) : 0;
                        objAFSegamationMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        //objAFSegamationMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : "";
                        //objAFSegamationMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objAFSegamationMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objAFSegamationMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objAFSegamationMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objAFSegamationMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objAFSegamationMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objAFSegamationMasterTypes.SegmentList = new List<SegmentMaster>();

                        SelectAFSegmationDetailsRequest objSelectAFSegmationDetailsRequest = new SelectAFSegmationDetailsRequest();
                        SelectAFSegamationDetailsResponse objSelectAFSegamationDetailsResponse = new SelectAFSegamationDetailsResponse();
                        objSelectAFSegmationDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectAFSegamationDetailsResponse = SelectAFSegmentationDetails(objSelectAFSegmationDetailsRequest);
                        if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objAFSegamationMasterTypes.SegmentList = objSelectAFSegamationDetailsResponse.AFSegmentDetailMasterRecord;
                        }

                        AFSegamationMasterTypesMaster.Add(objAFSegamationMasterTypes);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.AFSegamationMasterTypesList = AFSegamationMasterTypesMaster;
                    ResponseData.ResponseDynamicData = AFSegamationMasterTypesMaster;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Segmentation Master");
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
