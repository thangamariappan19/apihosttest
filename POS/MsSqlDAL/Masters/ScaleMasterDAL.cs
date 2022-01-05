using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizResponse.Masters.ScaleMasterResponse;
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

    public class ScaleMasterDAL : BaseScaleMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveScaleRequest)RequestObj;
            var ResponseData = new SaveScaleResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateScaleMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ScaleRecord.ID;

                SqlParameter ScaleCode = _CommandObj.Parameters.Add("@ScaleCode", SqlDbType.NVarChar);
                ScaleCode.Direction = ParameterDirection.Input;
                ScaleCode.Value = RequestData.ScaleRecord.ScaleCode;

                SqlParameter ScaleName = _CommandObj.Parameters.Add("@ScaleName", SqlDbType.NVarChar);
                ScaleName.Direction = ParameterDirection.Input;
                ScaleName.Value = RequestData.ScaleRecord.ScaleName;

                SqlParameter InternalCode = _CommandObj.Parameters.Add("@InternalCode", SqlDbType.NVarChar);
                InternalCode.Direction = ParameterDirection.Input;
                InternalCode.Value = RequestData.ScaleRecord.InternalCode;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ScaleRecord.Active;

                SqlParameter ApplytoAll = _CommandObj.Parameters.Add("@ApplytoAll", SqlDbType.Bit);
                ApplytoAll.Direction = ParameterDirection.Input;
                ApplytoAll.Value = RequestData.ScaleRecord.ApplytoAll;

                //SqlParameter VisualOrder = _CommandObj.Parameters.Add("@VisualOrder", SqlDbType.Int);
                //VisualOrder.Direction = ParameterDirection.Input;
                //VisualOrder.Value = RequestData.ScaleRecord.VisualOrder;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ScaleRecord.CreateBy;


                SqlParameter ScaleDetails = _CommandObj.Parameters.Add("@ScaleDetails", SqlDbType.Xml);
                ScaleDetails.Direction = ParameterDirection.Input;                
                ScaleDetails.Value = ScaleDetailMasterXML(RequestData.ScaleDetailMasterList);
               // ScaleDetails.Value = ScaleDetails.ToString().Replace("&", "&#38;");

                var ScaleWithBrandDetails = _CommandObj.Parameters.Add("@ScaleWithBrandDetails", SqlDbType.Xml);
                ScaleWithBrandDetails.Direction = ParameterDirection.Input;
                ScaleWithBrandDetails.Value = ScaleWithBrandDetailMasterXML(RequestData.BrandMasterList);
                //ScaleWithBrandDetails.Value = ScaleWithBrandDetails.ToString().Replace("&", "&#38;");

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Scale Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Scale Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Scale Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Scale Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string ScaleDetailMasterXML(List<ScaleDetailMaster> ScaleDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (ScaleDetailMaster objScaleDetailMasterDetails in ScaleDetailMasterList)
            {
                sSql.Append("<ScaleMasterData>");
                sSql.Append("<ID>" + (objScaleDetailMasterDetails.ID) + "</ID>");
                sSql.Append("<ScaleHeaderID>" + (objScaleDetailMasterDetails.ScaleHeaderID) + "</ScaleHeaderID>");
                sSql.Append("<SizeCode>" + objScaleDetailMasterDetails.SizeCode + "</SizeCode>");
                sSql.Append("<Description>" + (objScaleDetailMasterDetails.Description) + "</Description>");
                sSql.Append("<VisualOrder>" + objScaleDetailMasterDetails.VisualOrder + "</VisualOrder>");
                sSql.Append("<SCN>" + objScaleDetailMasterDetails.SCN + "</SCN>");
                sSql.Append("<Active>" + objScaleDetailMasterDetails.Active + "</Active>");
                sSql.Append("<CreateBy>" + objScaleDetailMasterDetails.CreateBy + "</CreateBy>");
                sSql.Append("</ScaleMasterData>");            
            }
            return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
        }
        public string ScaleWithBrandDetailMasterXML(List<BrandMaster> BrandMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (BrandMaster objBrandMasterDetails in BrandMasterList)
            {
                sSql.Append("<ScaleWithBrandMasterData>");
                sSql.Append("<ID>" + (objBrandMasterDetails.ID) + "</ID>");
                sSql.Append("<BrandID>" + (objBrandMasterDetails.BrandID) + "</BrandID>");
                sSql.Append("<BrandCode>" + objBrandMasterDetails.BrandCode + "</BrandCode>");
                sSql.Append("<BrandName>" + (objBrandMasterDetails.BrandName) + "</BrandName>");               
                sSql.Append("<Active>" + objBrandMasterDetails.Active + "</Active>");
                sSql.Append("<SCN>" + objBrandMasterDetails.SCN + "</SCN>");
                sSql.Append("<CreateBy>" + objBrandMasterDetails.CreateBy + "</CreateBy>");
                sSql.Append("</ScaleWithBrandMasterData>");
            }
            return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
        }
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateScaleRequest)RequestObj;
            var ResponseData = new UpdateScaleResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateScaleMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ScaleRecord.ID;

                SqlParameter ScaleCode = _CommandObj.Parameters.Add("@ScaleCode", SqlDbType.Int);
                ScaleCode.Direction = ParameterDirection.Input;
                ScaleCode.Value = RequestData.ScaleRecord.ScaleCode;

                SqlParameter ScaleName = _CommandObj.Parameters.Add("@ScaleName", SqlDbType.NVarChar);
                ScaleName.Direction = ParameterDirection.Input;
                ScaleName.Value = RequestData.ScaleRecord.ScaleName;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.ScaleRecord.UpdateBy;

                SqlParameter SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.ScaleRecord.SCN;

                //SqlParameter ScaleDetails = _CommandObj.Parameters.Add("@ScaleDetails", SqlDbType.Xml);
                //ScaleDetails.Direction = ParameterDirection.Input;
                //ScaleDetails.Value = ScaleDetailMasterXML(RequestData.ScaleDetailMaster);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Scale Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Scale Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Scale Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Scale Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ScaleRecord = new ScaleMaster();
            var RequestData = (DeleteScaleRequest)RequestObj;
            var ResponseData = new DeleteScaleResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from ScaleMasterDetails where ScaleHeaderID={0};Delete from ScaleMasterBrandDetails where ScaleHeaderID={0};Delete from  ScaleMaster  where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Scale Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Scale Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ScaleRecord = new ScaleMaster();
            var RequestData = (SelectByScaleIDRequest)RequestObj;
            var ResponseData = new SelectByScaleIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ScaleMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScale = new ScaleMaster();
                        objScale.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScale.ScaleCode = Convert.ToString(objReader["ScaleCode"]);
                        objScale.ScaleName = Convert.ToString(objReader["ScaleName"]);
                        objScale.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        objScale.VisualOrder = Convert.ToString(objReader["VisualOrder"]);
                        objScale.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objScale.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objScale.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objScale.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objScale.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objScale.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objScale.ApplytoAll = objReader["ApplytoAll"] != DBNull.Value ? Convert.ToBoolean(objReader["ApplytoAll"]) : true;

                        // Changed by Senthamil @ 11.09.2018

                        objScale.ScaleDetailMasterList = new List<ScaleDetailMaster>();
                        SelectScaleDetailsRequest objSelectAFSegmationDetailsRequest = new SelectScaleDetailsRequest();
                        SelectScaleDetailsResponse objSelectAFSegamationDetailsResponse = new SelectScaleDetailsResponse();
                        objSelectAFSegmationDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectAFSegamationDetailsResponse = SelectScaleDetails(objSelectAFSegmationDetailsRequest);
                        if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objScale.ScaleDetailMasterList = objSelectAFSegamationDetailsResponse.ScaleDetailMasterRecord;
                        }

                        objScale.BrandMasterList = new List<BrandMaster>();
                        objSelectAFSegmationDetailsRequest = new SelectScaleDetailsRequest();
                        objSelectAFSegamationDetailsResponse = new SelectScaleDetailsResponse();
                        objSelectAFSegmationDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectAFSegamationDetailsResponse = SelectScaleBrandDetails(objSelectAFSegmationDetailsRequest);
                        if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objScale.BrandMasterList = objSelectAFSegamationDetailsResponse.ScaleBrandDetailMasterRecord;
                        }

                        //objScale.ScaleDetailMasterList = new List<ScaleDetailMaster>();                         

                        //SelectScaleDetailsRequest objSelectAFSegmationDetailsRequest = new SelectScaleDetailsRequest();
                        //SelectScaleDetailsResponse objSelectAFSegamationDetailsResponse = new SelectScaleDetailsResponse();
                        //objSelectAFSegmationDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSelectAFSegamationDetailsResponse = SelectScaleDetails(objSelectAFSegmationDetailsRequest);
                        //objSelectAFSegamationDetailsResponse = SelectScaleBrandDetails(objSelectAFSegmationDetailsRequest);
                        //if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objScale.ScaleDetailMasterList = objSelectAFSegamationDetailsResponse.ScaleDetailMasterRecord;
                        //}


                        ResponseData.ScaleRecord = objScale;
                        ResponseData.ResponseDynamicData = objScale;
                        
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Scale Master");
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
            var ScaleList = new List<ScaleMaster>();
            var RequestData = (SelectAllScaleRequest)RequestObj;
            var ResponseData = new SelectAllScaleResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                sQuery = "Select * from ScaleMaster with(NoLock)";
                if (RequestData.ShowInActiveRecords == false)
                {
                    sQuery = sQuery + " where Active='True'";
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScale = new ScaleMaster();
                        objScale.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScale.ScaleCode = Convert.ToString(objReader["ScaleCode"]);
                        objScale.ScaleName = Convert.ToString(objReader["ScaleName"]);
                        objScale.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        objScale.VisualOrder = Convert.ToString(objReader["VisualOrder"]);
                        objScale.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objScale.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objScale.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objScale.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objScale.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objScale.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ScaleList.Add(objScale);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleList = ScaleList;
                    ResponseData.ResponseDynamicData = ScaleList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Scale Master");
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
            var ScaleMasterList = new List<ScaleMaster>();
            var RequestData = (SelectByIDsScaleMasterRequest)RequestObj;
            var ResponseData = new SelectByIDsScaleResponse();
            var ScaleMasterdata = new ScaleMaster();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from ScaleMaster with(NoLock)  where ID in '{0}'";
                sSql = string.Format(sSql, RequestData.IDs);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {                       

                        var objScale = new ScaleMaster();
                        objScale.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScale.ScaleCode = Convert.ToString(objReader["ScaleCode"]);
                        objScale.ScaleName = Convert.ToString(objReader["ScaleName"]);
                        objScale.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        objScale.VisualOrder = Convert.ToString(objReader["VisualOrder"]);
                        objScale.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objScale.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objScale.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objScale.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objScale.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objScale.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ScaleMasterList.Add(objScale);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleMasterList = ScaleMasterList;
                    ResponseData.ResponseDynamicData = ScaleMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Scale Master");
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
        public override EasyBizResponse.Masters.ScaleMasterResponse.SelectScaleLookUpResponse SelectScaleLookUp(EasyBizRequest.Masters.ScaleRequest.SelectScaleLookUpRequest ObjRequest)
        {
            var ScaleList = new List<ScaleMaster>();
            var RequestData = (SelectScaleLookUpRequest)ObjRequest;
            var ResponseData = new SelectScaleLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                sQuery = "select s.ID,s.ScaleCode, s.ScaleName from  ScaleMaster s  where s.ApplytoAll = 'true' UNION select  a.ID,a.ScaleCode,a.Scalename from ScaleMaster a inner join ScaleMasterBrandDetails b on a.id=b.ScaleHeaderID where b.BrandID='" + RequestData.BrandID + "' ";
                //sQuery = "select  a.ID,a.ScaleCode,a.Scalename from ScaleMaster a inner join ScaleMasterBrandDetails b on a.id=b.ScaleHeaderID where b.BrandID='" + RequestData.BrandID + "'";
                //sQuery = "Select ID,ScaleCode,Scalename from ScaleMaster with(NoLock)";
                if (RequestData.ShowInActiveRecords == false)
                {
                    sQuery = sQuery + " and  a.Active='True'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScale = new ScaleMaster();
                        objScale.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objScale.SizeID = objReader["SizeID"] != DBNull.Value ?  Convert.ToInt32(objReader["SizeID"]) :0;
                        objScale.ScaleCode = Convert.ToString(objReader["ScaleCode"]);
                        objScale.ScaleName = Convert.ToString(objReader["ScaleName"]);
                        ScaleList.Add(objScale);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleList = ScaleList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "VendorGroup Master");
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

      

        public override SelectAllScaleDetailsResponse SelectAllScaleDetails(SelectAllScaleDetailsRequest ObjRequest)
        {
            throw new NotImplementedException();
        }

        public override SelectScaleDetailsResponse SelectScaleDetails(SelectScaleDetailsRequest ObjRequest)
        {
            var ScaleDetailMasterList = new List<ScaleDetailMaster>();
            var RequestData = (SelectScaleDetailsRequest)ObjRequest;
            var ResponseData = new SelectScaleDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select ACM.ID,ACM.ScaleHeaderID, ACM.Active,ACM.SizeCode,ACM.Description,ACM.VisualOrder from ScaleMaster SM join ScaleMasterDetails ACM on sm.ID=ACM.ScaleHeaderID ");
                sSql.Append("where  SM.ID=" + RequestData.ID + " ");
                sSql.Append("order by ACM.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScaleDetailMaster = new ScaleDetailMaster();
                        objScaleDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.ScaleHeaderID = objReader["ScaleHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleHeaderID"]) : 0;
                        objScaleDetailMaster.SizeCode = Convert.ToString(objReader["SizeCode"]);
                        objScaleDetailMaster.Description = Convert.ToString(objReader["Description"]);
                        //objStoreGroupMaster.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objScaleDetailMaster.VisualOrder = objReader["VisualOrder"] != DBNull.Value ? Convert.ToInt32(objReader["VisualOrder"]) : 0;
                        objScaleDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ScaleDetailMasterList.Add(objScaleDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleDetailMasterRecord = ScaleDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Scale Master");
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
        public override SelectScaleDetailsResponse SelectScaleBrandDetails(SelectScaleDetailsRequest ObjRequest)
        {
            var ScaleDetailBrandMasterList = new List<BrandMaster>();
            var RequestData = (SelectScaleDetailsRequest)ObjRequest;
            var ResponseData = new SelectScaleDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select ACM.ID,ACM.ScaleHeaderID, ACM.Active,ACM.brandcode,acm.BrandID,ACM.brandname from ScaleMaster SM join ScaleMasterBrandDetails ACM on sm.ID=ACM.ScaleHeaderID ");
                sSql.Append("where  SM.ID=" + RequestData.ID + " ");
                sSql.Append("order by ACM.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScaleBrandDetailMaster = new BrandMaster();
                        objScaleBrandDetailMaster.ScaleWithBrandID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleBrandDetailMaster.ScaleHeaderID = objReader["ScaleHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleHeaderID"]) : 0;
                        objScaleBrandDetailMaster.BrandID = Convert.ToInt32(objReader["BrandID"]);
                        objScaleBrandDetailMaster.BrandCode = Convert.ToString(objReader["brandcode"]);
                        objScaleBrandDetailMaster.BrandName = Convert.ToString(objReader["brandname"]);
                        objScaleBrandDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ScaleDetailBrandMasterList.Add(objScaleBrandDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleBrandDetailMasterRecord = ScaleDetailBrandMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Scale Master");
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

        public override SelectScaleDetailsLookUpResponse SelectScaleDetailsLookUp(SelectScaleDetailsLookUpRequest ObjRequest)
        {
            var ScaleDetailMasterList = new List<ScaleDetailMaster>();
            var RequestData = (SelectScaleDetailsLookUpRequest)ObjRequest;
            var ResponseData = new SelectScaleDetailsLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ssd.ID SSID,smd.ID,smd.SizeCode,smd.Description,smd.VisualOrder from ScaleMasterDetails smd ";
                sQuery = sQuery + " left join [StyleWithScaleDetails] ssd on ssd.SizeID=smd.ID and smd.Active='true' and ssd.StyleID={0} where smd.ScaleHeaderID={1} order by smd.VisualOrder asc ";

                sQuery = string.Format(sQuery, RequestData.StyleID, RequestData.ID);

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScaleDetailMaster = new ScaleDetailMaster();
                        objScaleDetailMaster.SSID = objReader["SSID"] != DBNull.Value ? Convert.ToInt32(objReader["SSID"]) : 0;
                        objScaleDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SizeID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SizeCode = Convert.ToString(objReader["SizeCode"]);
                        objScaleDetailMaster.Description = Convert.ToString(objReader["Description"]);
                        objScaleDetailMaster.VisualOrder = objReader["VisualOrder"] != DBNull.Value ? Convert.ToInt32(objReader["VisualOrder"]) : 0;
                        //objScaleDetailMaster.Active =objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ScaleDetailMasterList.Add(objScaleDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleDetailMasterList = ScaleDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ScaleDetails Master");
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

        public override SelectAllScaleResponse API_SelectALL(SelectAllScaleRequest requestData)
        {
            var ScaleList = new List<ScaleMaster>();
            var RequestData = (SelectAllScaleRequest)requestData;
            var ResponseData = new SelectAllScaleResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //sQuery = "Select * from ScaleMaster with(NoLock)";

                sQuery = "Select ID, ScaleCode, ScaleName, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from ScaleMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(SM.ID) As TOTAL_CNT From ScaleMaster SM with(NoLock)" +
                   "where SM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or SM.ScaleCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or SM.ScaleName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ScaleCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ScaleName like isnull('%" + RequestData.SearchString + "%','')) " +                       
                       "order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                //if (RequestData.ShowInActiveRecords == false)
                //{
                //    sQuery = sQuery + " where Active='True'";
                //}

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScale = new ScaleMaster();
                        objScale.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScale.ScaleCode = Convert.ToString(objReader["ScaleCode"]);
                        objScale.ScaleName = Convert.ToString(objReader["ScaleName"]);
                        //objScale.InternalCode = Convert.ToString(objReader["InternalCode"]);
                        //objScale.VisualOrder = Convert.ToString(objReader["VisualOrder"]);
                        //objScale.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objScale.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objScale.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objScale.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objScale.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objScale.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ScaleList.Add(objScale);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.ScaleList = ScaleList;
                    ResponseData.ResponseDynamicData = ScaleList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Scale Master");
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
