using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MsSqlDAL.Masters
{
    public class DocumentNumberingMasterDAL : BaseDocumentNumberingMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveDocumentNumberingMasterRequest)RequestObj;
            var ResponseData = new SaveDocumentNumberingMasterResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateDocumentNumbering", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var HeaderID = _CommandObj.Parameters.Add("@HeaderID", SqlDbType.Int);
                HeaderID.Direction = ParameterDirection.Input;
                HeaderID.Value = RequestData.DocumentNumberingMasterRecord.ID;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.DocumentNumberingMasterRecord.CountryID;


                var StateID = _CommandObj.Parameters.Add("@StateID", SqlDbType.Int);
                StateID.Direction = ParameterDirection.Input;
                StateID.Value = RequestData.DocumentNumberingMasterRecord.StateID;


                var StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.DocumentNumberingMasterRecord.StoreID;

                var PosID = _CommandObj.Parameters.Add("@PosID", SqlDbType.Int);
                PosID.Direction = ParameterDirection.Input;
                PosID.Value = DBNull.Value; //RequestData.DocumentNumberingMasterRecord.PosID;

                var CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.DocumentNumberingMasterRecord.CountryCode;

                var StateCode = _CommandObj.Parameters.Add("@StateCode", SqlDbType.NVarChar);
                StateCode.Direction = ParameterDirection.Input;
                StateCode.Value = RequestData.DocumentNumberingMasterRecord.StateCode;

                var StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.DocumentNumberingMasterRecord.StoreCode;

                var PosCode = _CommandObj.Parameters.Add("@PosCode", SqlDbType.NVarChar);
                PosCode.Direction = ParameterDirection.Input;
                PosCode.Value = DBNull.Value; // RequestData.DocumentNumberingMasterRecord.PosCode;

                var DocumentTypeID = _CommandObj.Parameters.Add("@DocumentTypeID", SqlDbType.Int);
                DocumentTypeID.Direction = ParameterDirection.Input;
                DocumentTypeID.Value = RequestData.DocumentNumberingMasterRecord.DocumentTypeID;

                //var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                //Remarks.Direction = ParameterDirection.Input;
                //Remarks.Value = RequestData.DocumentNumberingMasterRecord.Remarks;

                var DocumentNumberingDetails = _CommandObj.Parameters.Add("@DocumentNumberingDetails", SqlDbType.Xml);
                DocumentNumberingDetails.Direction = ParameterDirection.Input;
                DocumentNumberingDetails.Value = DocumentNumberingDetailsXml(RequestData.DocumentNumberingDetailsList);

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.DocumentNumberingMasterRecord.CreateBy;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.DocumentNumberingMasterRecord.Active;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Document Numbering");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                    RequestData.DocumentNumberingMasterRecord.DocumentNumberingDetails = new List<DocumentNumberingDetails>();
                    RequestData.DocumentNumberingMasterRecord.DocumentNumberingDetails = RequestData.DocumentNumberingDetailsList;
                    RequestData.RequestDynamicData = RequestData.DocumentNumberingMasterRecord;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {


                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Document Numbering");

                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Document Numbering");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        private string DocumentNumberingDetailsXml(List<DocumentNumberingDetails> DocumentNumberingDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (DocumentNumberingDetails objDocumentNumberingDetails in DocumentNumberingDetailsList)
            {
                sSql.Append("<DocumentNumberingDetails>");
                sSql.Append("<ID>" + (objDocumentNumberingDetails.ID) + "</ID>");
                sSql.Append("<DocNumID>" + (objDocumentNumberingDetails.DocNumID) + "</DocNumID>");
                sSql.Append("<Prefix>" + (objDocumentNumberingDetails.Prefix) + "</Prefix>");
                sSql.Append("<Suffix>" + objDocumentNumberingDetails.Suffix + "</Suffix>");
                sSql.Append("<StartNumber>" + (objDocumentNumberingDetails.StartNumber) + "</StartNumber>");
                sSql.Append("<EndNumber>" + objDocumentNumberingDetails.EndNumber + "</EndNumber>");
                sSql.Append("<NumberOfDigit>" + objDocumentNumberingDetails.NumberOfCharacter + "</NumberOfDigit>");
                sSql.Append("<StartDate>" + sqlCommon.GetSQLServerDateString(objDocumentNumberingDetails.StartDate) + "</StartDate>");
                sSql.Append("<EndDate>" + sqlCommon.GetSQLServerDateString(objDocumentNumberingDetails.EndDate) + "</EndDate>");
                sSql.Append("<RunningNo>" + objDocumentNumberingDetails.RunningNo + "</RunningNo>");
                sSql.Append("<Active>" + objDocumentNumberingDetails.Active + "</Active>");
                //sSql.Append("<CreateBy>" + objDocumentNumberingDetails.CreateBy + "</CreateBy>");
                sSql.Append("</DocumentNumberingDetails>");
            }
            return sSql.ToString();
        }
        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var DocumentNumberingMasterRecord = new DocumentNumberingDetails();
            var RequestData = (UpdateDocumentNumberingMasterRequest)RequestObj;
            var ResponseData = new UpdateDocumentNumberingMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "delete from DocumentNumberingDetails where  DocNumID='{0}';delete from DocumentNumberingMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "DocumentNumbering Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "DocumentNumbering Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var DocumentNumberingMasterRecord = new DocumentNumberingMaster();
            var RequestData = (DeleteDocumentNumberingMasterRequest)RequestObj;
            var ResponseData = new DeleteDocumentNumberingMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Update DocumentNumberingMaster set Active='false' where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "DocumentNumbering Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "DocumentNumbering Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }


        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var DocumentNumberingMasterRecord = new DocumentNumberingMaster();
            var RequestData = (SelectByIDDocumentNumberingMasterRequest)RequestObj;
            var ResponseData = new SelectByIDDocumentNumberingMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from DocumentNumberingMaster with(NoLock)  where  ID='{0}'";



                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new DocumentNumberingMaster();
                        ObjDocumentNumberingMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        ObjDocumentNumberingMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        ObjDocumentNumberingMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //ObjDocumentNumberingMaster.PosID =objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) :0;
                        ObjDocumentNumberingMaster.DocumentTypeID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
                        //ObjDocumentNumberingMaster.Remarks = Convert.ToString(objReader["Remarks"]);     
                        ObjDocumentNumberingMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        ObjDocumentNumberingMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        ObjDocumentNumberingMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        ObjDocumentNumberingMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //ObjDocumentNumberingMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        // 03.10.2018 Changed by Senthamil
                        ObjDocumentNumberingMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : "";
                        ObjDocumentNumberingMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : "";
                        ObjDocumentNumberingMaster.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : "";
                        //ObjDocumentNumberingMaster.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : "";

                        ObjDocumentNumberingMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;



                        ObjDocumentNumberingMaster.DocumentNumberingDetails = new List<DocumentNumberingDetails>();

                        SelectDocumentNumberingDetailsRequest objSelectAFSegmationDetailsRequest = new SelectDocumentNumberingDetailsRequest();
                        SelectDocumentNumberingDetailsResponse objSelectAFSegamationDetailsResponse = new SelectDocumentNumberingDetailsResponse();
                        objSelectAFSegmationDetailsRequest.ID = Convert.ToInt32(objReader["ID"]);
                        objSelectAFSegamationDetailsResponse = SelecDocumentNumberingDetails(objSelectAFSegmationDetailsRequest);
                        if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            ObjDocumentNumberingMaster.DocumentNumberingDetails = objSelectAFSegamationDetailsResponse.DocumentNumberingDetailsList;
                        }


                        ResponseData.DocumentNumberingMasterRecord = ObjDocumentNumberingMaster;
                        ResponseData.ResponseDynamicData = ObjDocumentNumberingMaster;

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumbering Master");
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
            var DocumentNumberingMasterList = new List<DocumentNumberingMaster>();
            var RequestData = (SelectAllDocumentNumberingMasterRequest)RequestObj;
            var ResponseData = new SelectAllDocumentNumberingMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = ("select DNM.ID, cm.ID'CountryID',stm.ID'StateID',sm.ID'StoreID',dt.ID'DocumentTypeID',cm.CreateOn,cm.CreateBy,cm.UpdateOn,cm.UpdateBy,cm.Active,Sm.StoreName,STM.StateName,DT.DocumentName,CM.CountryName from DocumentNumberingMaster DNM ");
                sSql = sSql + ("left outer  join StoreMaster SM on SM.ID=DNM.StoreID  ");
                sSql = sSql + ("left outer join StateMaster STM on STM.ID=DNM.StateID  left outer join DocumentType DT on DT.ID=DNM.DocumentTypeID  ");
                sSql = sSql + ("left outer  join CountryMaster CM on CM.ID=DNM.CountryID ORDER BY DNM.ID DESC ");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();


                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new DocumentNumberingMaster();

                        ObjDocumentNumberingMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjDocumentNumberingMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        ObjDocumentNumberingMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        ObjDocumentNumberingMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //ObjDocumentNumberingMaster.PosID = objReader["PosID"] != DBNull.Value ?Convert.ToInt32(objReader["PosID"]) :0;
                        ObjDocumentNumberingMaster.DocumentTypeID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
                        //ObjDocumentNumberingMaster.Remarks = Convert.ToString(objReader["Remarks"]);                        
                        ObjDocumentNumberingMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        ObjDocumentNumberingMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        ObjDocumentNumberingMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        ObjDocumentNumberingMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //ObjDocumentNumberingMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        ObjDocumentNumberingMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;


                        ObjDocumentNumberingMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        ObjDocumentNumberingMaster.StateName = Convert.ToString(objReader["StateName"]);
                        ObjDocumentNumberingMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        //ObjDocumentNumberingMaster.PosName = Convert.ToString(objReader["PosName"]);
                        ObjDocumentNumberingMaster.DocumentName = Convert.ToString(objReader["DocumentName"]);

                        DocumentNumberingMasterList.Add(ObjDocumentNumberingMaster);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DocumentNumberingMasterList = DocumentNumberingMasterList;
                    ResponseData.ResponseDynamicData = DocumentNumberingMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumbering Master");
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
            var DocumentNumberingMasterList = new List<DocumentNumberingMaster>();

            var RequestData = (SelectByIDsDocumentNumberingRequest)RequestObj;
            var ResponseData = new SelectByIDsDocumentNumberingResponse();

            var DocumentNumberingMasterdata = new DocumentNumberingMaster();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from DocumentNumberingMaster with(NoLock)  where ID in '{0}'";
                sSql = string.Format(sSql, RequestData.IDs);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objDocumentNumberingMaster = new DocumentNumberingMaster();

                        objDocumentNumberingMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objDocumentNumberingMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        objDocumentNumberingMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //objDocumentNumberingMaster.PosID = objReader["PosID"] != DBNull.Value ? Convert.ToInt32(objReader["PosID"]) :0;
                        objDocumentNumberingMaster.DocumentTypeID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
                        objDocumentNumberingMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDocumentNumberingMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDocumentNumberingMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDocumentNumberingMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //ObjDocumentNumberingMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDocumentNumberingMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;

                        DocumentNumberingMasterList.Add(objDocumentNumberingMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DocumentNumberingMasterList = DocumentNumberingMasterList;
                    ResponseData.ResponseDynamicData = DocumentNumberingMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design Master");
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

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }


        public override SelectDocumentNumberingMasterLookUpResponse SelecDocumentNumberingMasterLookUp(SelectDocumentNumberingMasterLookUpRequest ObjRequest)
        {
            var DocumentNumberingMasterList = new List<DocumentNumberingMaster>();
            var RequestData = (SelectDocumentNumberingMasterLookUpRequest)ObjRequest;
            var ResponseData = new SelectDocumentNumberingMasterLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,[ScreenID] from DocumentNumberingMaster with(NoLock)  where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new DocumentNumberingMaster();
                        ObjDocumentNumberingMaster.ID = Convert.ToInt32(objReader["ID"]);
                        DocumentNumberingMasterList.Add(ObjDocumentNumberingMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DocumentNumberingMasterList = DocumentNumberingMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumberingMaster ");
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

        public override SelectDocumentNumberingDetailsResponse SelecDocumentNumberingDetails(SelectDocumentNumberingDetailsRequest ObjRequest)
        {
            var DocumentNumberingDetailsList = new List<DocumentNumberingDetails>();
            var RequestData = (SelectDocumentNumberingDetailsRequest)ObjRequest;
            var ResponseData = new SelectDocumentNumberingDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select DND.ID  as ID,DND.Prefix,DND.Suffix,DND.DocNumID,DND.StartDate,DND.StartNumber,DND.EndNumber,DND.NumberOfDigit,DND.StartDate,DND.EndDate,DND.RunningNo,DND.Active  ");
                sSql.Append("from DocumentNumberingMaster DNM left outer join DocumentNumberingDetails DND on DNM.ID=DND.DocNumID  ");

                sSql.Append("where DND.DocNumID=" + RequestData.ID + " ");
                sSql.Append("order by DND.id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDocumentNumberingDetails = new DocumentNumberingDetails();
                        objDocumentNumberingDetails.ID = Convert.ToInt32(objReader["ID"]);
                        objDocumentNumberingDetails.DocNumID = Convert.ToInt32(objReader["DocNumID"]);
                        //objDocumentNumberingDetails.DocumentName = Convert.ToString(objReader["DocumentName"]);
                        objDocumentNumberingDetails.Prefix = Convert.ToString(objReader["Prefix"]);
                        objDocumentNumberingDetails.Suffix = Convert.ToString(objReader["Suffix"]);
                        objDocumentNumberingDetails.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt32(objReader["EndNumber"]) : 0;
                        objDocumentNumberingDetails.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt32(objReader["StartNumber"]) : 0;
                        objDocumentNumberingDetails.NumberOfCharacter = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
                        objDocumentNumberingDetails.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objDocumentNumberingDetails.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objDocumentNumberingDetails.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        objDocumentNumberingDetails.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        DocumentNumberingDetailsList.Add(objDocumentNumberingDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DocumentNumberingDetailsList = DocumentNumberingDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumberingMaster");
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
        public override SelectDocumentNumberingDetailsResponse SelectAutoIncrementID(SelectDocumentNumberingDetailsRequest ObjRequest)
        {
            var DocumentNumberingDetailsRecord = new DocumentNumberingDetails();
            var RequestData = (SelectDocumentNumberingDetailsRequest)ObjRequest;
            var ResponseData = new SelectDocumentNumberingDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select StartNumber from DocumentNumberingDetails where DocNumID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        DocumentNumberingDetails objDocumentNumberingMaster = new DocumentNumberingDetails();
                        //objDocumentNumberingMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objDocumentNumberingMaster.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt32(objReader["StartNumber"]) : 0;

                        ResponseData.DocumentNumberingDetailsRecord = objDocumentNumberingMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {

                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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
        public override SelectDocumentNumberingBillNoDetailsResponse SelectDocumentNumberingBillNoDetails(SelectDocumentNumberingBillNoDetailsRequest ObjRequest)
        {
            var DocumentNumberingBillNoRecord = new DocumentNumberingDetails();
            var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)ObjRequest;
            var ResponseData = new SelectDocumentNumberingBillNoDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select dt.ID as DocumentTypeID,dnd.ID as DetailID,dnd.Prefix,dnd.Suffix,dnd.StartNumber,dnd.EndNumber,dnd.NumberOfDigit,dnd.RunningNo from DocumentType dt with(NoLock) join DocumentNumberingMaster dnm with(NoLock) on dt.ID=dnm.DocumentTypeID join DocumentNumberingDetails dnd with(NoLock) on dnm.ID=dnd.DocNumID where dt.ID={0} and dnm.StoreCode='{1}' ";
                sSql = sSql + "and dnm.Active='True' and dnd.Active='True' and isnull(dnm.PosID,0)=0 and dnd.StartDate < SYSDATETIME() and dnd.EndDate > SYSDATETIME()";

                sSql = string.Format(sSql, RequestData.DocumentTypeID, RequestData.StoreCode);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    int DetailID = 0;
                    int RunningNo = 0;
                    while (objReader.Read())
                    {
                        DocumentNumberingDetails objDocumentNumberingMaster = new DocumentNumberingDetails();
                        objDocumentNumberingMaster.ID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
                        objDocumentNumberingMaster.Prefix = Convert.ToString(objReader["Prefix"]);
                        objDocumentNumberingMaster.Suffix = Convert.ToString(objReader["Suffix"]);
                        objDocumentNumberingMaster.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt32(objReader["StartNumber"]) : 0;
                        objDocumentNumberingMaster.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt32(objReader["EndNumber"]) : 0;
                        objDocumentNumberingMaster.NumberOfCharacter = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
                        objDocumentNumberingMaster.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        objDocumentNumberingMaster.DetailID = objReader["DetailID"] != DBNull.Value ? Convert.ToInt32(objReader["DetailID"]) : 0;
                        objDocumentNumberingMaster.DetailID = objReader["DetailID"] != DBNull.Value ? Convert.ToInt32(objReader["DetailID"]) : 0;
                        DetailID = objReader["DetailID"] != DBNull.Value ? Convert.ToInt32(objReader["DetailID"]) : 0;

                        SqlConnection con = new SqlConnection();
                        //con = RequestData.ConnectionString;
                        sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                        //con.Open();
                        var sql = "Select top 1 DocRunningNo from Invoiceheader where StoreID = " + RequestData.StoreID + " order by ID desc";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        string InvRunningNo = Convert.ToString(cmd.ExecuteScalar());
                        if (InvRunningNo == null)
                        {
                            //RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                            objDocumentNumberingMaster.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        }
                        else
                        {
                            //RunningNo = objReader["DocRunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["DocRunningNo"]) : 0;
                            objDocumentNumberingMaster.RunningNo = Convert.ToInt32(InvRunningNo);//objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        }
                        con.Close();


                        ResponseData.DocumentNumberingBillNoDetailsRecord = objDocumentNumberingMaster;
                    }
                    objReader.Close();
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Document Numbering");
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

        public bool UpdateRunningNo(int DetailID, int RunningNo)
        {

            bool IsSuccess = false;
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                int RunningNumber = RunningNo + 1;
                string sSql = "update DocumentNumberingDetails set RunningNo=RunningNo+1 where ID=" + DetailID + " ";
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return IsSuccess;

        }

        public override SelectByIDDocumentNumberingMasterResponse SelectHeaderID(SelectByIDDocumentNumberingMasterRequest ObjRequest)
        {
            var DocumentNumberingMasterRecord = new DocumentNumberingMaster();
            var RequestData = (SelectByIDDocumentNumberingMasterRequest)ObjRequest;
            var ResponseData = new SelectByIDDocumentNumberingMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from DocumentNumberingMaster with(NoLock)  where  DocumentTypeID={0} and CountryID={1} and StateID={2} and StoreID={3} and Isnull(PosID,0)={4}";
                sSql = string.Format(sSql, RequestData.DocumentTypeID, RequestData.CountryID, RequestData.StateID, RequestData.StoreID, RequestData.PosID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new DocumentNumberingMaster();
                        ObjDocumentNumberingMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        ResponseData.DocumentNumberingMasterRecord = ObjDocumentNumberingMaster;


                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumbering Master");
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

        //public override SelectDocumentNumberingBillNoDetailsResponse SelectDocumentNumberingCustomerDetails(SelectDocumentNumberingBillNoDetailsRequest ObjRequest)
        //{
        //    var DocumentNumberingBillNoRecord = new DocumentNumberingDetails();
        //    var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)ObjRequest;
        //    var ResponseData = new SelectDocumentNumberingBillNoDetailsResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        _ConnectionString = RequestData.ConnectionString;
        //        _RequestFrom = RequestData.RequestFrom;

        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
        //        string sSql = "Select dt.ID as DocumentTypeID,dnd.ID as DetailID,dnd.Prefix,dnd.Suffix,dnd.StartNumber,dnd.EndNumber,dnd.NumberOfDigit,dnd.RunningNo from DocumentType dt join DocumentNumberingMaster dnm on dt.ID=dnm.DocumentTypeID join DocumentNumberingDetails dnd on dnm.ID=dnd.DocNumID where dt.ID={0} and dnm.CountryID={1} and dnm.StoreID={2} and dnm.Active='True' and dnd.Active='True' and dnd.StartDate < SYSDATETIME() and dnd.EndDate > SYSDATETIME()";
        //        sSql = string.Format(sSql, RequestData.DocumentTypeID, RequestData.CountryID, RequestData.StoreID);
        //        _CommandObj = new SqlCommand(sSql, _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            int DetailID = 0;
        //            int RunningNo = 0;
        //            while (objReader.Read())
        //            {
        //                DocumentNumberingDetails objDocumentNumberingMaster = new DocumentNumberingDetails();
        //                objDocumentNumberingMaster.ID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
        //                objDocumentNumberingMaster.Prefix = Convert.ToString(objReader["Prefix"]);
        //                objDocumentNumberingMaster.Suffix = Convert.ToString(objReader["Suffix"]);
        //                objDocumentNumberingMaster.StartNumber = objReader["StartNumber"] != DBNull.Value ? Convert.ToInt32(objReader["StartNumber"]) : 0;
        //                objDocumentNumberingMaster.EndNumber = objReader["EndNumber"] != DBNull.Value ? Convert.ToInt32(objReader["EndNumber"]) : 0;
        //                objDocumentNumberingMaster.NumberOfCharacter = objReader["NumberOfDigit"] != DBNull.Value ? Convert.ToInt32(objReader["NumberOfDigit"]) : 0;
        //                objDocumentNumberingMaster.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
        //                objDocumentNumberingMaster.DetailID = objReader["DetailID"] != DBNull.Value ? Convert.ToInt32(objReader["DetailID"]) : 0;                       

        //                DetailID = objReader["DetailID"] != DBNull.Value ? Convert.ToInt32(objReader["DetailID"]) : 0;
        //                RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
        //                ResponseData.DocumentNumberingBillNoDetailsRecord = objDocumentNumberingMaster;
        //            }
        //            objReader.Close();
        //            ResponseData.DocumentNumberingBillNoDetailsRecord = DocumentNumberingBillNoRecord;
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //            //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //            //{

        //            //    UpdateRunningNo(DetailID, RunningNo);
        //            //}
        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Document Numbering");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlCommon.CloseConnection(_ConnectionObj);
        //    }
        //    return ResponseData;
        //}



        public override SelectByIDDocumentNumberingMasterResponse DateValidation(SaveDocumentNumberingMasterRequest RequestObj)
        {
            var RequestData = (SaveDocumentNumberingMasterRequest)RequestObj;
            // var ResponseData = new SaveDocumentNumberingMasterResponse();
            var ResponseData = new SelectByIDDocumentNumberingMasterResponse();
            var DocumentNumberingMasterRecord = new DocumentNumberingMaster();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "select Convert(Varchar(10),max(B.EndDate),126) [MaxDate] from DocumentNumberingMaster[A] Left Join DocumentNumberingDetails[B] On A.ID=B.DocNumID where A.CountryID='" + RequestData.CountryID + "' and A.StateID='" + RequestData.StateID + "' and A.StoreID='" + RequestData.StoreID + "' and A.DocumentTypeID='" + RequestData.DocumentTypeID + "' group by A.CountryID,A.StateID,A.StoreID,A.DocumentTypeID";

                //sSql = string.Format(sSql);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new DocumentNumberingMaster();
                        ObjDocumentNumberingMaster.MaxDate = Convert.ToString(objReader["MaxDate"]);

                        //ObjDocumentNumberingMaster.DocumentNumberingDetails = new List<DocumentNumberingDetails>();

                        //SelectDocumentNumberingDetailsRequest objSelectAFSegmationDetailsRequest = new SelectDocumentNumberingDetailsRequest();
                        //SelectDocumentNumberingDetailsResponse objSelectAFSegamationDetailsResponse = new SelectDocumentNumberingDetailsResponse();
                        //objSelectAFSegmationDetailsRequest.ID = Convert.ToInt32(objReader["ID"]);
                        //objSelectAFSegamationDetailsResponse = SelecDocumentNumberingDetails(objSelectAFSegmationDetailsRequest);
                        //if (objSelectAFSegamationDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    ObjDocumentNumberingMaster.DocumentNumberingDetails = objSelectAFSegamationDetailsResponse.DocumentNumberingDetailsList;
                        //}


                        ResponseData.DocumentNumberingMasterRecord = ObjDocumentNumberingMaster;


                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumbering Master");
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

        public override UpdateRunningNumResponse UpdateRunningNum(UpdateRunningNumRequest ObjRequest)
        {
            var DocumentNumberingMasterRecord = new DocumentNumberingDetails();
            var RequestData = (UpdateRunningNumRequest)ObjRequest;
            var ResponseData = new UpdateRunningNumResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                int RunningNo = RequestData.RunningNo + 1;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                // string sSql = "update DocumentNumberingDetails set RunningNo=" + RunningNo + " where ID='" + RequestData.DetailID + "'";              
                // sSql = string.Format(sSql, RequestData.DetailID, RequestData.RunningNo);
                string sSql = "update DocumentNumberingDetails set RunningNo={0} where ID={1}";
                sSql = string.Format(sSql, RunningNo, RequestData.DetailID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
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


        public override SelectDocumentNumberingBillNoDetailsResponse SelectDocumentNumberingDetailsAPI(SelectDocumentNumberingBillNoDetailsRequest ObjRequest)
        {
            var DocumentNumberingBillNoRecord = new DocumentNumberingDetails();
            var RequestData = (SelectDocumentNumberingBillNoDetailsRequest)ObjRequest;
            var ResponseData = new SelectDocumentNumberingBillNoDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "select DocumentNo, DocDetailsID, RunningNo " +
                    "from dbo.API_Get_Document_Numbering(" + RequestData.StoreID.ToString() + "," +
                    "" + RequestData.DocumentTypeID.ToString() + "," +
                    "'" + RequestData.DocumentDate.ToString("yyyy-MM-dd") + "')";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    int DetailID = 0;
                    int RunningNo = 0;
                    while (objReader.Read())
                    {
                        DocumentNumberingDetails objDocumentNumberingMaster = new DocumentNumberingDetails();

                        objDocumentNumberingMaster.Prefix = Convert.ToString(objReader["DocumentNo"]);

                        objDocumentNumberingMaster.RunningNo = objReader["RunningNo"] != DBNull.Value ? Convert.ToInt32(objReader["RunningNo"]) : 0;
                        objDocumentNumberingMaster.DetailID = objReader["DocDetailsID"] != DBNull.Value ? Convert.ToInt32(objReader["DocDetailsID"]) : 0;


                        ResponseData.DocumentNumberingBillNoDetailsRecord = objDocumentNumberingMaster;
                    }
                    objReader.Close();

                    if (ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID == 0 || string.IsNullOrWhiteSpace(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix))
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Document Numbering");
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Document Numbering");
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

        public override SelectAllDocumentNumberingMasterResponse API_SelectALL(SelectAllDocumentNumberingMasterRequest requestData)
        {
            var DocumentNumberingMasterList = new List<DocumentNumberingMaster>();
            var RequestData = (SelectAllDocumentNumberingMasterRequest)requestData;
            var ResponseData = new SelectAllDocumentNumberingMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = ("select DNM.ID, cm.ID'CountryID',stm.ID'StateID',sm.ID'StoreID',dt.ID'DocumentTypeID',cm.CreateOn,cm.CreateBy,cm.UpdateOn,cm.UpdateBy,cm.Active,Sm.StoreName,STM.StateName,DT.DocumentName,CM.CountryName,RC.TOTAL_CNT [RecordCount] from DocumentNumberingMaster DNM ");
                //sSql = sSql + ("left outer  join StoreMaster SM on SM.ID=DNM.StoreID  ");
                //sSql = sSql + ("left outer join StateMaster STM on STM.ID=DNM.StateID  left outer join DocumentType DT on DT.ID=DNM.DocumentTypeID  ");
                //sSql = sSql + ("left outer  join CountryMaster CM on CM.ID=DNM.CountryID ORDER BY DNM.ID DESC ");

                string sSql = "Select DNM.ID, CM.CountryName, STM.StateName, Sm.StoreName, DT.DocumentName, cm.Active,RC.TOTAL_CNT [RecordCount] " +
                   "from DocumentNumberingMaster DNM " +
                   "left outer  join StoreMaster SM on SM.ID=DNM.StoreID " +
                   "left outer join StateMaster STM on STM.ID=DNM.StateID  left outer join DocumentType DT on DT.ID=DNM.DocumentTypeID " +
                   "left outer  join CountryMaster CM on CM.ID=DNM.CountryID " +
                       "LEFT JOIN(Select count(DNM1.ID) As TOTAL_CNT From DocumentNumberingMaster DNM1 " +
                       "left outer  join StoreMaster SM1 on SM1.ID = DNM1.StoreID "+

                       "left outer join StateMaster STM1 on STM1.ID = DNM1.StateID "+

                       "left outer join DocumentType DT1 on DT1.ID = DNM1.DocumentTypeID "+

                        "left outer  join CountryMaster CM1 on CM1.ID = DNM1.CountryID "+


                       " where DNM1.Active = " + RequestData.IsActive + " "+

                       "and (isnull('" + RequestData.SearchString + "','') = '' " +

                       "or CM1.CountryName like isnull('%" + RequestData.SearchString + "%', '') " +

                       "or STM1.StateName like isnull('%" + RequestData.SearchString + "%', '') " +

                       "or Sm1.StoreName like isnull('%" + RequestData.SearchString + "%', '') " +

                       "or DT1.DocumentName like isnull('%" + RequestData.SearchString + "%', ''))) As RC ON 1 = 1 " +
                      
                       "where DNM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or CM.CountryName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or STM.StateName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Sm.StoreName  like isnull('%" + RequestData.SearchString + "%','') " +
                       "or DT.DocumentName like isnull('%" + RequestData.SearchString + "%','')) " +                       
                       "order by  DNM.ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();


                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new DocumentNumberingMaster();

                        ObjDocumentNumberingMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //ObjDocumentNumberingMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //ObjDocumentNumberingMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        //ObjDocumentNumberingMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        ////ObjDocumentNumberingMaster.PosID = objReader["PosID"] != DBNull.Value ?Convert.ToInt32(objReader["PosID"]) :0;
                        //ObjDocumentNumberingMaster.DocumentTypeID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;
                        ////ObjDocumentNumberingMaster.Remarks = Convert.ToString(objReader["Remarks"]);                        
                        //ObjDocumentNumberingMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //ObjDocumentNumberingMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //ObjDocumentNumberingMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //ObjDocumentNumberingMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        ////ObjDocumentNumberingMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        ObjDocumentNumberingMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;


                        ObjDocumentNumberingMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        ObjDocumentNumberingMaster.StateName = Convert.ToString(objReader["StateName"]);
                        ObjDocumentNumberingMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        //ObjDocumentNumberingMaster.PosName = Convert.ToString(objReader["PosName"]);
                        ObjDocumentNumberingMaster.DocumentName = Convert.ToString(objReader["DocumentName"]);

                        DocumentNumberingMasterList.Add(ObjDocumentNumberingMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DocumentNumberingMasterList = DocumentNumberingMasterList;
                    //ResponseData.ResponseDynamicData = DocumentNumberingMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "DocumentNumbering Master");
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



