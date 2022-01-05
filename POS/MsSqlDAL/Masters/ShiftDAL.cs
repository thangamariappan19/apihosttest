using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.ShiftResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizResponse.Common;

namespace MsSqlDAL.Masters
{
    public class ShiftDAL : BaseShiftMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.Masters.ShiftResponse.SelectByCountryIDResponse SelectCountryByID(EasyBizRequest.Masters.ShiftRequest.SelectByCountryIDRequest RequestObj)
        {
            var ShiftMaster = new List<ShiftMaster>();
            var RequestData = (SelectByCountryIDRequest)RequestObj;
            var ResponseData = new SelectByCountryIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from ShiftMaster  where CountryID='{0}'";
                sSql = string.Format(sSql, RequestData.CountryID);


                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        objShift.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ShiftMaster.Add(objShift);

                      
                    }
                   
                    ResponseData.ShiftMasterList = ShiftMaster;

                    ResponseData.ResponseDynamicData = ShiftMaster;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Master");
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

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveShiftRequest)RequestObj;
            var ResponseData = new SaveShiftResponse();
            var Shiftlist = RequestData.Shiftlist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateShift", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (ShiftMaster objShift in Shiftlist)
                {
                    sSql.Append("<ShiftMaster>");
                    sSql.Append("<ID>" + (objShift.ID) + "</ID>");
                    sSql.Append("<ShiftCode>" + (objShift.ShiftCode) + "</ShiftCode>");
                    sSql.Append("<ShiftName>" + objShift.ShiftName + "</ShiftName>");
                    sSql.Append("<SortOrder>" + objShift.SortOrder + "</SortOrder>");
                    sSql.Append("<Active>" + (objShift.Active) + "</Active>");
                    sSql.Append("<CreateBy>" + (objShift.CreateBy) + "</CreateBy>");
                    sSql.Append("<IsDeleted>" + (objShift.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<CountryID>" + objShift.CountryID + "</CountryID>");
                    //sSql.Append("<CountryName>" + objShift.CountryName + "</CountryName>");
                    sSql.Append("</ShiftMaster>");
                }
                var ShiftData = _CommandObj.Parameters.Add("@ShiftData", SqlDbType.Xml);
                ShiftData.Direction = ParameterDirection.Input;
                ShiftData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@SubIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                //@CountryId

                SqlParameter CountryId = _CommandObj.Parameters.Add("@CountryId", SqlDbType.Int);
                CountryId.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "ShiftMaster");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.IDs = ID2.Value.ToString();
                    ResponseData.IDs = CountryId.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", " ShiftMaster");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ShiftMaster");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.ItIsInRelationdhip.Replace("{}", "ShiftMaster");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ShiftMaster");
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
            var ShiftRecord = new ShiftMaster();
            var RequestData = (DeleteShiftRequest)RequestObj;
            var ResponseData = new DeleteShiftResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from ShiftMaster where CountryID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "ShiftMaster");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ShiftMaster");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ShiftRecord = new ShiftMaster();
            var RequestData = (SelectByShiftIDRequest)RequestObj;
            var ResponseData = new SelectByShiftIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from ShiftMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objShift.CountryName = Convert.ToString(objReader["CountryName"]);        
                        objShift.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        objShift.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objShift.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objShift.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objShift.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objShift.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objShift.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                       // ResponseData.ShiftRecord = objShift;
                        ResponseData.ResponseDynamicData = objShift;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ShiftMaster");
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
            var ShiftList = new List<ShiftMaster>();
            var RequestData = (SelectAllShiftRequest)RequestObj;
            var ResponseData = new SelectAllShiftResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select distinct SM.CountryID,CM.countrycode,CM.countryname from ShiftMaster SM with(NoLock) join countrymaster CM with(nolock) on CM.id=sm.countryid";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        //objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        //objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objShift.CountryName = Convert.ToString(objReader["CountryName"]);  
                        //objShift.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        //objShift.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objShift.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objShift.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objShift.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objShift.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objShift.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objShift.CountryCode= Convert.ToString(objReader["countrycode"]);
                        objShift.CountryName = Convert.ToString(objReader["countryname"]);

                        SelectShiftListForCategoryRequest objSelectshiftrequest = new SelectShiftListForCategoryRequest();
                        SelectShiftListForCategoryResponse objSelectshiftResponse = new SelectShiftListForCategoryResponse();
                        objSelectshiftrequest.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSelectshiftrequest.ShowInActiveRecords = true;
                        //objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectshiftResponse = SelectShiftListByCountry(objSelectshiftrequest);
                        if (objSelectshiftResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objShift.Shiftlist = objSelectshiftResponse.ShiftList;
                        }

                        ShiftList.Add(objShift);
                    }
                    ResponseData.ShiftList = ShiftList;
                    ResponseData.ResponseDynamicData = ShiftList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ShiftMaster");
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
            var ShiftList = new List<ShiftMaster>();
            var RequestData = (SelectByShiftIDRequest)RequestObj;
            var ResponseData = new SelectByShiftIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from ShiftMaster with(NoLock) where  ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objShift.CountryName = Convert.ToString(objReader["CountryName"]);  
                        objShift.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        objShift.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objShift.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objShift.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objShift.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objShift.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objShift.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ShiftList.Add(objShift);
                    }
                   // ResponseData.ShiftList = ShiftList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
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

        public override EasyBizResponse.Masters.ShiftMasterResponse.SelectShiftLookUpResponse SelectShiftLookUp(SelectShiftLookUpRequest ObjRequest)
        {
            var ShiftList = new List<ShiftMaster>();
            var RequestData = (SelectShiftLookUpRequest)ObjRequest;
            var ResponseData = new SelectShiftLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.type == "XREPORT")
                {
                    sQuery = "select sm.ID,sm.ShiftCode,sm.ShiftName from ShiftMaster sm join shiftlog sl on sl.shiftid=sm.id where sl.StoreID = '" + RequestData.StoreID + "' and  sl.BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "'and  sl.POSID = '" + RequestData.POSID + "'";
                }
                else
                {
                    sQuery = "Select ID,[ShiftName],ShiftCode from ShiftMaster with(NoLock) where Active='true'";
                }
                if (RequestData.CountryID != 0)
                {
                    sQuery = sQuery + " and CountryID='" + RequestData.CountryID + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        ShiftList.Add(objShift);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ShiftList = ShiftList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ShiftMaster");
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

        public override EasyBizResponse.Masters.ShiftMasterResponse.SelectShiftListForCategoryResponse SelectShiftListByCountry(SelectShiftListForCategoryRequest RequestObj)
        {
            var MAShiftForCategoryList = new List<ShiftMaster>();
            var RequestData = (SelectShiftListForCategoryRequest)RequestObj;
            var ResponseData = new SelectShiftListForCategoryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from ShiftMaster with(NoLock) where CountryID='" + RequestData.CountryID + "' order by SortOrder asc", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;                     
                        objShift.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        objShift.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objShift.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objShift.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objShift.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objShift.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objShift.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        MAShiftForCategoryList.Add(objShift);
                    }
                    ResponseData.ShiftList = MAShiftForCategoryList;                    
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                    
                    ResponseData.ResponseDynamicData = MAShiftForCategoryList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ShiftMaster");
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

        public override EasyBizRequest.Common.SelectShiftLogResponse SelectShiftLogRecordbyID(EasyBizRequest.Common.SelectShiftLogRequest RequestObj)
        {
            var MAShiftForCategoryList = new List<ShiftMaster>();
            var RequestData = (SelectShiftLogRequest)RequestObj;
            var ResponseData = new SelectShiftLogResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.Type == "DayClosing")
                {
                    //_CommandObj = new SqlCommand("Select Max(BusinessDate)'BusinessDate' from ShiftLOG with(NoLock) where CountryID='" + RequestData.CountryID + "' and POSID= '" + RequestData.POSID + "'and StoreID = '" + RequestData.StoreID + "'", _ConnectionObj);
                    _CommandObj = new SqlCommand("Select Max(BusinessDate)'BusinessDate' from ShiftLOG with(NoLock) where CountryID='" + RequestData.CountryID + "' and  StoreID = '" + RequestData.StoreID + "'", _ConnectionObj);
                }
                else if (RequestData.Type == "POSLog")
                {
                    //_CommandObj = new SqlCommand("Select Max(BusinessDate)'BusinessDate' from ShiftLOG with(NoLock) where CountryID='" + RequestData.CountryID + "' and POSID= '" + RequestData.POSID + "'and StoreID = '" + RequestData.StoreID + "'", _ConnectionObj);
                    _CommandObj = new SqlCommand("Select POSID from ShiftLOG with(NoLock) where CountryID='" + RequestData.CountryID + "' and  StoreID = '" + RequestData.StoreID + "' and  BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and ShiftStatus='Open' and  SHIFTINUSERID = '" + RequestData.ID + "'", _ConnectionObj);
                }
                else if (RequestData.Type == "BusinessDateStatus")
                {
                    _CommandObj = new SqlCommand("select * from ShiftLOG where BusinessDate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and Status='Close'", _ConnectionObj);
                }
                else 
                {
                    _CommandObj = new SqlCommand("Select * from ShiftLOG with(NoLock) where CountryID='" + RequestData.CountryID + "' and POSID= '" + RequestData.POSID + "'and StoreID = '" + RequestData.StoreID + "'", _ConnectionObj);
                }
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShiftLOGTypes = new ShiftLOGTypes();
                        //objShiftLOGTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objShiftLOGTypes.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objShiftLOGTypes.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //objShiftLOGTypes.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0;
                        //objShiftLOGTypes.ShiftInUserID = objReader["ShiftInUserID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftInUserID"]) : 0;
                        //objShiftLOGTypes.ShiftOutUserID = objReader["ShiftOutUserID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftOutUserID"]) : 0;
                        //objShiftLOGTypes.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;
                        if (RequestData.Type != "POSLog")
                        {
                            objShiftLOGTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.MinValue;
                        }
                        if (RequestData.Type == "POSLog")
                        {
                            objShiftLOGTypes.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0;
                        }
                        if (RequestData.Type == "BusinessDateStatus")
                        {
                            objShiftLOGTypes.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["POSID"]) : string.Empty;
                        }
                        //objShiftLOGTypes.ShiftInDateTime = objReader["ShiftInDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ShiftInDateTime"]) : DateTime.Now;
                        //objShiftLOGTypes.ShiftOutDateTime = objReader["ShiftOutDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ShiftOutDateTime"]) : DateTime.Now;
                        //objShiftLOGTypes.Status = Convert.ToString(objReader["Status"]);


                        ResponseData.ShiftTypesData = objShiftLOGTypes;

                        ResponseData.ResponseDynamicData = objShiftLOGTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Master");
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

        public override SelectShiftLogResponse SelectAllShiftLog(SelectShiftLogRequest RequestObj)
        {
            var ShiftLOGList = new List<ShiftLOGTypes>();
            var RequestData = (SelectShiftLogRequest)RequestObj;
            var ResponseData = new SelectShiftLogResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);                
                string sSql = "Select * from ShiftLOG  ";
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShiftLOGTypes = new ShiftLOGTypes();
                        objShiftLOGTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShiftLOGTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
                        objShiftLOGTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objShiftLOGTypes.POSID = Convert.ToInt32(objReader["POSID"]);                       
                        objShiftLOGTypes.ShiftID = Convert.ToInt32(objReader["ShiftID"]);
                        objShiftLOGTypes.ShiftInUserID = Convert.ToInt32(objReader["ShiftInUserID"]);
                        objShiftLOGTypes.ShiftInDateTime = objReader["ShiftInDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ShiftInDateTime"]) : DateTime.Now;
                        objShiftLOGTypes.Status = Convert.ToString(objReader["Status"]);
                       
                        ShiftLOGList.Add(objShiftLOGTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.AllShiftLOGTypesList = ShiftLOGList;

                    ResponseData.ResponseDynamicData = ShiftLOGList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Log Master");
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

        public override SelectShiftLogResponse SelectJoinShiftMasterandLog(SelectShiftLogRequest RequestObj)
        {
            var ShiftLOGList = new List<ShiftMaster>();
            var RequestData = (SelectShiftLogRequest)RequestObj;
            var ResponseData = new SelectShiftLogResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            string sSql = string.Empty;
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                

                if (RequestData.Type == "Shift")
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    // sSql = "Select top 1 sm.*,sl.ID as shiftLogID,sl.StoreID,sl.ShiftInUserID,sl.ShiftOutUserID,sl.ShiftInDateTime, ";
                    //sSql = sSql + "sl.ShiftOutDateTime,sl.POSID,sl.ShiftID, SL.CountryID,sl.BusinessDate,sl.Status,sl.ShiftStatus,sl.ShiftInAmount, sl.Status AS OriginalDayInStatus,sl.ShiftStatus  AS OriginalShiftInStatus";
                    //sSql = sSql + " from  ShiftMaster SM  left join ShiftLOG SL on SL.ShiftID = SM.ID AND  sl.Status = 'Open' and SL.CountryID='" + RequestData.CountryID + "' and SL.POSID = '" + RequestData.POSID + "' and SL.BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SL.StoreID = '" + RequestData.StoreID + "'  where sm.CountryID='" + RequestData.CountryID + "' and SM.ID not in (select ShiftID from ShiftLog where BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "')  ORDER BY SM.SortOrder";

                    sSql = "Select top 1 sm.ID, sm.ShiftCode, sm.ShiftName, sm.SortOrder " +
	                            ", sl.ID as shiftLogID,sl.StoreID,sl.ShiftInUserID,sl.ShiftOutUserID " +
	                            ", sl.ShiftInDateTime, sl.ShiftOutDateTime,sl.POSID,sl.ShiftID, SL.CountryID " +
	                            ", sl.BusinessDate,sl.[Status],sl.ShiftStatus,sl.ShiftInAmount " +
	                            ", sl.[Status] OriginalDayInStatus,sl.ShiftStatus[OriginalShiftInStatus] " +
                            "from ShiftMaster SM " +
                            " left join ShiftLOG SL on SL.ShiftID = SM.ID AND sl.[Status] = 'Open' " +
                                "and SL.CountryID = " + RequestData.CountryID + " " +
                                "and SL.POSID = " + RequestData.POSID + " " +
                                "and SL.BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' " +
                                "and SL.StoreID = " + RequestData.StoreID + " " +
                            "where sm.CountryID = " + RequestData.CountryID + " " +
                                "and SM.ID not in (select ShiftID from ShiftLog " +
                                    "where BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "') " +
                            "ORDER BY SM.SortOrder";

                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }               
                else
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    // sSql = "Select sm.*,sl.ID as shiftLogID,sl.StoreID,sl.ShiftInUserID,sl.ShiftOutUserID,sl.ShiftInDateTime, ";
                    //sSql = sSql + "sl.ShiftOutDateTime,sl.POSID,sl.ShiftID, SL.CountryID,sl.BusinessDate,sl.Status,sl.ShiftStatus,sl.ShiftInAmount, sl.Status AS OriginalDayInStatus,sl.ShiftStatus  AS OriginalShiftInStatus";
                    //sSql = sSql + " from  ShiftMaster SM  left join ShiftLOG SL on SL.ShiftID = SM.ID AND  sl.Status = 'Open' and SL.CountryID='" + RequestData.CountryID + "'  and SL.StoreID = '" + RequestData.StoreID + "' where sm.CountryID='" + RequestData.CountryID + "' ORDER BY SM.SortOrder";

                    sSql = "Select sm.ID, sm.ShiftCode, sm.ShiftName, sm.SortOrder " +
	                            ", sl.ID [shiftLogID], sl.StoreID, sl.ShiftInUserID, sl.ShiftOutUserID " +
	                            ", sl.ShiftInDateTime, sl.ShiftOutDateTime, sl.POSID, sl.ShiftID, SL.CountryID " +
	                            ", sl.BusinessDate, sl.[Status], sl.ShiftStatus, sl.ShiftInAmount " +
	                            ", sl.Status [OriginalDayInStatus], sl.ShiftStatus [OriginalShiftInStatus] " +
                            "from ShiftMaster SM with(nolock) " +
                            "left join ShiftLOG SL with(nolock) on SL.ShiftID = SM.ID AND  sl.[Status] = 'Open' " +
	                            "and SL.CountryID = " + RequestData.CountryID + " and SL.StoreID = " + RequestData.StoreID + " " +
                            "where sm.CountryID = " + RequestData.CountryID + " " +
                            "ORDER BY SM.SortOrder";


                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                //string sSql = "Select sm.*,sl.ID as shiftLogID,sl.StoreID,sl.ShiftInUserID,sl.ShiftOutUserID,sl.ShiftInDateTime,sl.ShiftOutDateTime,sl.POSID,sl.ShiftID,SL.CountryID,sl.BusinessDate,";
                //sSql = sSql + "ISNULL(sl.Status,'Open') AS [Status],ISNULL(sl.ShiftStatus,'Open') AS ShiftStatus,sl.Status AS OriginalDayInStatus,sl.ShiftStatus  AS OriginalShiftInStatus from ShiftLOG SL right join ShiftMaster SM on SM.ID=SL.ShiftID AND sl.Status = 'Open' or sl.ShiftStatus = 'Open' ORDER BY SM.SortOrder";

                
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShiftLOGTypes = new ShiftMaster();
                        objShiftLOGTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShiftLOGTypes.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objShiftLOGTypes.ShiftCode = objReader["ShiftCode"] != DBNull.Value ? Convert.ToString(objReader["ShiftCode"]) : string.Empty;
                        objShiftLOGTypes.ShiftName = objReader["ShiftName"] != DBNull.Value ? Convert.ToString(objReader["ShiftName"]) : string.Empty;
                        objShiftLOGTypes.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        objShiftLOGTypes.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objShiftLOGTypes.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0;
                        objShiftLOGTypes.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;
                        objShiftLOGTypes.ShiftInUserID = objReader["ShiftInUserID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftInUserID"]) : 0;
                        objShiftLOGTypes.ShiftOutUserID = objReader["ShiftOutUserID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftOutUserID"]) : 0;
                        objShiftLOGTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objShiftLOGTypes.ShiftInDateTime = objReader["ShiftInDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ShiftInDateTime"]) : DateTime.Now;
                        objShiftLOGTypes.ShiftOutDateTime = objReader["ShiftOutDateTime"] != DBNull.Value ? Convert.ToDateTime(objReader["ShiftOutDateTime"]) : DateTime.Now;
                        objShiftLOGTypes.ShiftLogID = objReader["ShiftLogID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftLogID"]) : 0;
                        objShiftLOGTypes.ShiftInAmount = objReader["ShiftInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ShiftInAmount"]) : 0;
                        objShiftLOGTypes.Status = objReader["Status"] != DBNull.Value ? Convert.ToString(objReader["Status"]) : string.Empty;
                        objShiftLOGTypes.ShiftStatus = objReader["ShiftStatus"] != DBNull.Value ? Convert.ToString(objReader["ShiftStatus"]) : string.Empty;

                        objShiftLOGTypes.OriginalDayInStatus = objReader["OriginalDayInStatus"] != DBNull.Value ? Convert.ToString(objReader["OriginalDayInStatus"]) : string.Empty;
                        objShiftLOGTypes.OriginalShiftInStatus = objReader["OriginalShiftInStatus"] != DBNull.Value ? Convert.ToString(objReader["OriginalShiftInStatus"]) : string.Empty;

                        ShiftLOGList.Add(objShiftLOGTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.AllShiftLOGandTypesList = ShiftLOGList;
                    ResponseData.ResponseDynamicData = ShiftLOGList;
                   
                }
                else
                {
                    ResponseData.AllShiftLOGandTypesList = new List<ShiftMaster>();
                    ResponseData.ResponseDynamicData = new List<ShiftMaster>();

                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Log Master");
                   
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

        public override SelectShiftLogResponse SelectJoinShiftAmount(SelectShiftLogRequest RequestObj)
        {
            var ShiftAmountRecord = new ShiftLOGTypes();
            var RequestData = (SelectShiftLogRequest)RequestObj;
            var ResponseData = new SelectShiftLogResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //string sSql = "select sum(ics.receivedamount)'TotalAmount', sum(icd.receivedcardamount)'TotalCardAmount',sl.ShiftInAmount,sum(icd.ReturnAmount)'ReturnAmount' from InvoiceHeader IH left join InVoiceCashDetails ICS on IH.id=ICS.invoiceheaderid left join InvoiceCardDetails ICD on IH.id=ICD.invoiceheaderid  left join ShiftLog SL on IH.ShiftID=SL.ShiftID and ih.businessdate=sl.Businessdate where sl.businessdate='" + RequestData.BusinessDate + "' and ih.shiftid= '" + RequestData.ShiftID + "' group by sl.ShiftInAmount ";
               // string sSql = "select sum(ics.receivedamount)'TotalAmount', sum(icd.receivedcardamount)'TotalCardAmount',sl.ShiftInAmount,sum(SRH.Totalreturnamount)'SalesRetunAmount' ,  sum(icd.ReturnAmount)'ReturnAmount',(select sum(receivedamount)'cashin' from [CashInCashOutDetails] where shiftid=ih.shiftid and applicationdate='" + RequestData.BusinessDate + "' and POSID='" + RequestData.POSID + "')'cashin',(select sum(paidamount)'paid' from [CashInCashOutDetails] where shiftid=ih.shiftid and applicationdate='" + RequestData.BusinessDate + "' and POSID='" + RequestData.POSID + "')'cashout' from InvoiceHeader IH left join InVoiceCashDetails ICS on IH.id=ICS.invoiceheaderid LEFT JOIN [SalesReturnHeader] SRH on IH.invoiceNo=SRH.salesinvoicenumber left join InvoiceCardDetails ICD on IH.id=ICD.invoiceheaderid left join ShiftLog SL on IH.ShiftID=SL.ShiftID and ih.businessdate=sl.Businessdate  where sl.businessdate='" + RequestData.BusinessDate + "' and ih.shiftid= '" + RequestData.ShiftID + "' group by sl.ShiftInAmount,ih.ShiftID ";
                string sSql = "select sum(ics.receivedamount)'TotalAmount', sum(icd.receivedcardamount)'TotalCardAmount',sl.ShiftInAmount,(select sum(Totalreturnamount)'SalesRetunAmount' from [SalesReturnHeader] where documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and shiftid='" + RequestData.ShiftID + "' and POSID='" + RequestData.POSID + "')'SalesRetunAmount'   ,  sum(icd.ReturnAmount)'ReturnAmount',(select sum(receivedamount)'cashin' from [CashInCashOutDetails] where shiftid=ih.shiftid and applicationdate='" + RequestData.BusinessDate + "' and POSID='" + RequestData.POSID + "')'cashin',(select sum(paidamount)'paid' from [CashInCashOutDetails] where shiftid=ih.shiftid and applicationdate='" + RequestData.BusinessDate + "' and POSID='" + RequestData.POSID + "')'cashout' from InvoiceHeader IH left join InVoiceCashDetails ICS on IH.id=ICS.invoiceheaderid left join InvoiceCardDetails ICD on IH.id=ICD.invoiceheaderid left join ShiftLog SL on IH.ShiftID=SL.ShiftID and ih.businessdate=sl.Businessdate  where sl.businessdate='" + RequestData.BusinessDate + "' and ih.shiftid= '" + RequestData.ShiftID + "' group by sl.ShiftInAmount,ih.ShiftID ";
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShiftLOGTypes = new ShiftLOGTypes();
                        objShiftLOGTypes.Amount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objShiftLOGTypes.CardAmount = objReader["TotalCardAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCardAmount"]) : 0;
                        objShiftLOGTypes.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objShiftLOGTypes.SalesRetunAmount = objReader["SalesRetunAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SalesRetunAmount"]) : 0;
                        objShiftLOGTypes.ShiftAmount = objReader["ShiftInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ShiftInAmount"]) : 0;
                        objShiftLOGTypes.CashInAmount = objReader["cashin"] != DBNull.Value ? Convert.ToDecimal(objReader["cashin"]) : 0;
                        objShiftLOGTypes.CashOutAmount = objReader["cashout"] != DBNull.Value ? Convert.ToDecimal(objReader["cashout"]) : 0;
                        ResponseData.ShiftAmount = objShiftLOGTypes;
                        ResponseData.ResponseDynamicData = objShiftLOGTypes;
                    }

                }
                else
                {
                    _ConnectionString = RequestData.ConnectionString;
                    _RequestFrom = RequestData.RequestFrom;
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    //string sSql1 = "select ShiftInAmount from ShiftLog  where businessdate='" + RequestData.BusinessDate + "' and shiftid= '" + RequestData.ShiftID + "'";
                    string sSql1 = "select ShiftInAmount'TotalShiftAmount',(select sum(receivedamount)'cashin' from [CashInCashOutDetails] where shiftid=ShiftLog.shiftid and applicationdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and POSID='" + RequestData.POSID + "')'cashin',(select sum(paidamount)'paid' from [CashInCashOutDetails] where shiftid=ShiftLog.shiftid and applicationdate='" + RequestData.BusinessDate + "' and POSID='" + RequestData.POSID + "')'cashout' from ShiftLog  where businessdate='" + RequestData.BusinessDate + "' and shiftid= '" + RequestData.ShiftID + "' ";
                    _CommandObj = new SqlCommand(sSql1, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objShiftLOGTypes = new ShiftLOGTypes();
                            objShiftLOGTypes.Amount = objReader["TotalShiftAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalShiftAmount"]) : 0;
                            objShiftLOGTypes.CardAmount =  0;
                            objShiftLOGTypes.ShiftAmount =  0;
                            objShiftLOGTypes.CashInAmount = objReader["cashin"] != DBNull.Value ? Convert.ToDecimal(objReader["cashin"]) : 0;
                            objShiftLOGTypes.CashOutAmount = objReader["cashout"] != DBNull.Value ? Convert.ToDecimal(objReader["cashout"]) : 0;
                            ResponseData.ShiftAmount = objShiftLOGTypes;
                            ResponseData.ResponseDynamicData = objShiftLOGTypes;
                        }

                    }
                    else
                    {
                        var objShiftLOGTypes = new ShiftLOGTypes();
                        objShiftLOGTypes.Amount =  0;
                        objShiftLOGTypes.CardAmount = 0;
                        objShiftLOGTypes.ShiftAmount = 0;
                        objShiftLOGTypes.CashInAmount =  0;
                        objShiftLOGTypes.CashOutAmount = 0;

                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Log Master");
                    }
                   
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

        public override EasyBizResponse.Transactions.POS.Invoice.SelectXReportByDetailsResponse GetXReceipt(EasyBizRequest.Transactions.POS.Invoice.SelectXReportByDetailsRequest RequestObj)
        {
            var InvoiceReceiptList = new List<XreportTypes>();
            var RequestData = (SelectXReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectXReportByDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SP_ShiftClose", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
               

                if (RequestData.CashierID != null && RequestData.ShiftID != null && RequestData.BusinessDate != null && RequestData.StoreID != null && RequestData.POSID != null)
                {
                    _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                    _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                    _CommandObj.Parameters.AddWithValue("@BusinessDate",sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                    _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                    _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@CashierID", "");
                    _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                    _CommandObj.Parameters.AddWithValue("@StoreID", "");
                    _CommandObj.Parameters.AddWithValue("@POSID", "");
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new XreportTypes();

                        objInvoiceReceipt.SubTotal = objReader["SubTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotal"]) : 0;
                        objInvoiceReceipt.DiscountTotal = objReader["DiscountTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountTotal"]) : 0;
                        objInvoiceReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                        objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objInvoiceReceipt.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                        objInvoiceReceipt.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objInvoiceReceipt.CreditSale = objReader["CreditSale"] != DBNull.Value ? Convert.ToInt32(objReader["CreditSale"]) : 0;
                        objInvoiceReceipt.IsReturn = objReader["IsReturn"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturn"]) : true;
                        objInvoiceReceipt.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                        objInvoiceReceipt.ShopName = objReader["ShopName"].ToString();
                        objInvoiceReceipt.PosName = objReader["POSName"] != DBNull.Value ? Convert.ToString(objReader["POSName"]) : string.Empty;
                        objInvoiceReceipt.ShiftName = objReader["ShiftName"].ToString();
                        objInvoiceReceipt.NetSales = objReader["NetSales"] != DBNull.Value ? Convert.ToDecimal(objReader["NetSales"]) : 0;
                        objInvoiceReceipt.FloatAmount = objReader["FloatAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FloatAmount"]) : 0;
                        objInvoiceReceipt.TotalCashInBox = objReader["TotalCashInBox"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCashInBox"]) : 0;
                        objInvoiceReceipt.CashInAmount = objReader["CashInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashInAmount"]) : 0;
                        objInvoiceReceipt.CashOutAmount = objReader["CashOutAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashOutAmount"]) : 0;
                       // objInvoiceReceipt.IsReturn1 = objReader["IsReturn1"] != DBNull.Value ? Convert.ToDecimal(objReader["IsReturn1"]) : 0;
                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        InvoiceReceiptList.Add(objInvoiceReceipt);                      
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.XReportList = InvoiceReceiptList;
                }
                else
                {
                   
                        var objInvoiceReceipt = new XreportTypes();

                        objInvoiceReceipt.SubTotal =  0;
                        objInvoiceReceipt.DiscountTotal =  0;
                        objInvoiceReceipt.InvoiceNo = "";
                        objInvoiceReceipt.StoreImage =  null;
                        objInvoiceReceipt.TotalDiscount =  0;
                        objInvoiceReceipt.TotalAmount =  0;
                        objInvoiceReceipt.CreditSale =  0;
                        objInvoiceReceipt.IsReturn =  true;
                        objInvoiceReceipt.BusinessDate =  DateTime.MinValue;
                        objInvoiceReceipt.Cashier = "";
                        objInvoiceReceipt.ShopName ="";
                        objInvoiceReceipt.PosName = "";
                        objInvoiceReceipt.ShiftName = "";
                        objInvoiceReceipt.NetSales = 0;
                        objInvoiceReceipt.FloatAmount =  0;
                        objInvoiceReceipt.TotalCashInBox =  0;
                        objInvoiceReceipt.CashInAmount = 0;
                        objInvoiceReceipt.CashOutAmount =  0;
                        // objInvoiceReceipt.IsReturn1 = objReader["IsReturn1"] != DBNull.Value ? Convert.ToDecimal(objReader["IsReturn1"]) : 0;
                        objInvoiceReceipt.DecimalPlaces =  0;
                        InvoiceReceiptList.Add(objInvoiceReceipt);
                   
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.XReportList = InvoiceReceiptList;
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

        public override SelectZReportByDetailsResponse GetZReceipt(SelectZReportByDetailsRequest RequestObj)
        {
            var ZReceiptList = new List<ZReport>();
            var RequestData = (SelectZReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectZReportByDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SP_DayClose1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                if (RequestData.BusinessDate != null && RequestData.StoreID != null )
                {                   
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                    _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);                
                }
                else
                {                 
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                    _CommandObj.Parameters.AddWithValue("@StoreID", "");                   
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new ZReport();
                        objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objInvoiceReceipt.ShopCode = objReader["ShopCode"].ToString();
                        objInvoiceReceipt.Netamount = objReader["Netamount"] != DBNull.Value ? Convert.ToDecimal(objReader["Netamount"]) : 0;
                        objInvoiceReceipt.Country = objReader["Country"].ToString();                                                
                        objInvoiceReceipt.CreditSale = objReader["CreditSale"] != DBNull.Value ? Convert.ToInt32(objReader["CreditSale"]) : 0;
                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;             
                        objInvoiceReceipt.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                        objInvoiceReceipt.ShopName = objReader["ShopName"].ToString();
                        objInvoiceReceipt.ShiftName = objReader["ShiftName"].ToString();
                        objInvoiceReceipt.FloatAmount = objReader["FloatAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FloatAmount"]):0 ;                                        
                        objInvoiceReceipt.TotalCashInBox = objReader["TotalCashInBox"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCashInBox"]) : 0;
                        objInvoiceReceipt.CashInAmount = objReader["CashInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashInAmount"]) : 0;
                        objInvoiceReceipt.CashOutAmount = objReader["CashOutAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashOutAmount"]) : 0;
                       
                        ZReceiptList.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ZReportList = ZReceiptList;
                }
                else
                {
                    var objInvoiceReceipt = new ZReport();
                    objInvoiceReceipt.StoreImage = null;
                    objInvoiceReceipt.ShopCode ="";
                    objInvoiceReceipt.Netamount =  0;
                    objInvoiceReceipt.Country = "";
                    objInvoiceReceipt.CreditSale = 0;
                    objInvoiceReceipt.DecimalPlaces = 0;
                    //objInvoiceReceipt.CASH =  0;
                    //objInvoiceReceipt.Debit = 0;
                    //objInvoiceReceipt.Credit =  0;
                    objInvoiceReceipt.BusinessDate = DateTime.Now;
                    objInvoiceReceipt.Cashier = "";
                    objInvoiceReceipt.ShopName = "";
                    objInvoiceReceipt.ShiftName = "";
                    objInvoiceReceipt.FloatAmount =  0;

                    
                    objInvoiceReceipt.TotalCashInBox =  0;
                    objInvoiceReceipt.CashInAmount =  0;
                    objInvoiceReceipt.CashOutAmount = 0;
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                    ZReceiptList.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ZReportList = ZReceiptList;
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

        public override SelectZReportByDetailsResponse GetZReceipt1(SelectZReportByDetailsRequest RequestObj)
        {
            var ZReceiptList1 = new List<ZSubReport>();
            var RequestData = (SelectZReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectZReportByDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SP_DayClosing1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.BusinessDate != null && RequestData.StoreID != null )
                {                   
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                    _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);                
                }
                else
                {                 
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                    _CommandObj.Parameters.AddWithValue("@StoreID", "");                   
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new ZSubReport();                       
                        
                        objInvoiceReceipt.ShiftID = objReader["ShiftID"].ToString();
                        objInvoiceReceipt.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                        objInvoiceReceipt.KNET = objReader["KNET"] != DBNull.Value ? Convert.ToDecimal(objReader["KNET"]) : 0;
                        objInvoiceReceipt.VISA = objReader["VISA"] != DBNull.Value ? Convert.ToDecimal(objReader["VISA"]) : 0;
                        objInvoiceReceipt.CREDITCARD = objReader["CREDITCARD"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDITCARD"]) : 0;
                        objInvoiceReceipt.PaymentCash = objReader["PaymentCash"] != DBNull.Value ? Convert.ToDecimal(objReader["PaymentCash"]) : 0;
                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objInvoiceReceipt.TotalCash = objReader["TotalCash"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCash"]) : 0;
                        objInvoiceReceipt.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objInvoiceReceipt.CurrencyName = objReader["CurrencyName"].ToString();
                        objInvoiceReceipt.ForeignTotal = objReader["ForeignTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["ForeignTotal"]) : 0;
                        objInvoiceReceipt.POSID = objReader["POSID"].ToString();
                        ZReceiptList1.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ZReportList1 = ZReceiptList1;
                    ResponseData.ResponseDynamicData = ZReceiptList1;
                }
                else                                
                {
                    var objInvoiceReceipt = new ZSubReport();
                    objInvoiceReceipt.ShiftID = "";
                    objInvoiceReceipt.PaymentCurrency = "";
                    objInvoiceReceipt.KNET =  0;
                    objInvoiceReceipt.VISA = 0;
                    objInvoiceReceipt.CREDITCARD = 0;
                    objInvoiceReceipt.PaymentCash = 0;
                    objInvoiceReceipt.DecimalPlaces = 0;
                    objInvoiceReceipt.TotalCash = 0;
                    objInvoiceReceipt.ReturnAmount = 0;
                    objInvoiceReceipt.CurrencyName = "";
                    objInvoiceReceipt.ForeignTotal = 0;
                    objInvoiceReceipt.POSID ="";          
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                    ZReceiptList1.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ZReportList1 = ZReceiptList1;
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
        public override SelectZReportByDetailsResponse GetZReceipt2(SelectZReportByDetailsRequest RequestObj)
        {
            var Zreport2 = new List<Zreport2>();
            var RequestData = (SelectZReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectZReportByDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("SP_Z_InvoiceDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                if (RequestData.BusinessDate != null && RequestData.StoreID != null)
                {
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                    _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                    _CommandObj.Parameters.AddWithValue("@StoreID", "");
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new Zreport2();

                        objInvoiceReceipt.SubTotal = objReader["SubTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotal"]) : 0;
                        objInvoiceReceipt.DiscountTotal = objReader["DiscountTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountTotal"]) : 0;
                        objInvoiceReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                        objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objInvoiceReceipt.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                        objInvoiceReceipt.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                        objInvoiceReceipt.CreditSale = objReader["CreditSale"] != DBNull.Value ? Convert.ToInt32(objReader["CreditSale"]) : 0;
                        objInvoiceReceipt.IsReturn = objReader["IsReturn"] != DBNull.Value ? Convert.ToBoolean(Convert.ToInt32(objReader["IsReturn"])) : true;
                        //objInvoiceReceipt.CASH = objReader["CASH"] != DBNull.Value ? Convert.ToDecimal(objReader["CASH"]) : 0; ;
                        //objInvoiceReceipt.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                        //objInvoiceReceipt.Debit = objReader["Debit"] != DBNull.Value ? Convert.ToDecimal(objReader["Debit"]) : 0; ;
                        //objInvoiceReceipt.Credit = objReader["Credit"] != DBNull.Value ? Convert.ToDecimal(objReader["Credit"]) : 0; ;
                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;


                        objInvoiceReceipt.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                        objInvoiceReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                        objInvoiceReceipt.ShopName = objReader["ShopName"].ToString();
                        objInvoiceReceipt.ShiftName = objReader["ShiftName"].ToString();
                        objInvoiceReceipt.PosName = objReader["POSName"] != DBNull.Value ? Convert.ToString(objReader["POSName"]) : string.Empty;
                        objInvoiceReceipt.NetSales = objReader["NetSales"] != DBNull.Value ? Convert.ToDecimal(objReader["NetSales"]) : 0;
                        objInvoiceReceipt.FloatAmount = objReader["FloatAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FloatAmount"]) : 0;
                        objInvoiceReceipt.TotalCashInBox = objReader["TotalCashInBox"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCashInBox"]) : 0;
                        objInvoiceReceipt.CashInAmount = objReader["CashInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashInAmount"]) : 0;
                        objInvoiceReceipt.CashOutAmount = objReader["CashOutAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashOutAmount"]) : 0;
                        //objInvoiceReceipt.IsReturn1 = objReader["IsReturn1"] != DBNull.Value ? Convert.ToInt32(objReader["IsReturn1"]) : 0;
                        
                        Zreport2.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.Zreport2 = Zreport2;
                    ResponseData.ResponseDynamicData = Zreport2;
                }
                else
                {
                    var objInvoiceReceipt = new Zreport2();

                    objInvoiceReceipt.SubTotal = 0;
                    objInvoiceReceipt.DiscountTotal = 0;
                    objInvoiceReceipt.InvoiceNo = "";
                    objInvoiceReceipt.StoreImage = null;
                    objInvoiceReceipt.TotalDiscount = 0;
                    objInvoiceReceipt.TotalAmount = 0;
                    objInvoiceReceipt.CreditSale = 0;                   
                    objInvoiceReceipt.IsReturn = true;
                    //objInvoiceReceipt.CASH = 0;
                    //objInvoiceReceipt.PaymentCurrency = "";
                    //objInvoiceReceipt.Debit = 0;
                    //objInvoiceReceipt.Credit = 0;

                    objInvoiceReceipt.DecimalPlaces = 0;     

                    objInvoiceReceipt.BusinessDate = DateTime.Now;
                    objInvoiceReceipt.Cashier = "";
                    objInvoiceReceipt.ShopName = "";
                    objInvoiceReceipt.ShiftName = "";
                    objInvoiceReceipt.PosName = "";
                    objInvoiceReceipt.NetSales = 0;
                    objInvoiceReceipt.FloatAmount = 0;
                    objInvoiceReceipt.TotalCashInBox = 0;
                    objInvoiceReceipt.CashInAmount = 0;
                    objInvoiceReceipt.CashOutAmount = 0;
                     
                    //objInvoiceReceipt.DiscountTotal =  0;                  
                    //objInvoiceReceipt.IsReturn1 = 0;                                
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                    Zreport2.Add(objInvoiceReceipt);
                }
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.Zreport2 = Zreport2;
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

        public override SelectShiftLogResponse SelectShiftInEnabled(SelectShiftLogRequest RequestObj)
        {
            var ShiftLOGList = new List<ShiftMaster>();
            var RequestData = (SelectShiftLogRequest)RequestObj;
            var ResponseData = new SelectShiftLogResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                if (RequestData.Type == "Shift")
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                    string sSql = "select max(shiftid)'ShiftID' from shiftlog Sl inner join  shiftmaster SM on sl.shiftid=sm.id  ";
                    sSql = sSql + " where sl.businessdate='" + RequestData.BusinessDate + "' ";
                    sSql = sSql + " and SL.CountryID='" + RequestData.CountryID + "' and SL.POSID = '" + RequestData.POSID + "' and SL.StoreID = '" + RequestData.StoreID + "' and sl.shiftstatus='Close'";
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }              

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShiftLOGTypes = new ShiftMaster();
                        objShiftLOGTypes.ID = objReader["shiftid"] != DBNull.Value ? Convert.ToInt32(objReader["shiftid"]) : 0;                 
                       
                        ResponseData.MaxShiftTypesData1 = objShiftLOGTypes;
                        ResponseData.ResponseDynamicData = objShiftLOGTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Log Master");
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

        public override SelectShiftLogResponse SelectMaxShiftInEnabled(SelectShiftLogRequest RequestObj)
        {
            var ShiftLOGList = new List<ShiftMaster>();
            var RequestData = (SelectShiftLogRequest)RequestObj;
            var ResponseData = new SelectShiftLogResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;                
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    string sSql = "select max(ID)'shiftid' from shiftmaster   ";                   
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);                
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShiftLOGTypes = new ShiftMaster();
                        objShiftLOGTypes.ID = objReader["shiftid"] != DBNull.Value ? Convert.ToInt32(objReader["shiftid"]) : 0;
                        ResponseData.MaxShiftTypesData = objShiftLOGTypes;
                        ResponseData.ResponseDynamicData = objShiftLOGTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Log Master");
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

        public override SelectXReportByDetailsResponse GetXReceipt1(SelectXReportByDetailsRequest RequestObj)
        {
            var InvoiceReceiptList = new List<XSubreportTypes>();
            var RequestData = (SelectXReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectXReportByDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetXDetails1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                if (RequestData.CashierID != null && RequestData.ShiftID != null && RequestData.BusinessDate != null && RequestData.StoreID != null && RequestData.POSID != null)
                {
                    _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                    _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                    _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                    _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@CashierID", "");
                    _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                    _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                    _CommandObj.Parameters.AddWithValue("@StoreID", "");
                    _CommandObj.Parameters.AddWithValue("@POSID", "");
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new XSubreportTypes();
                        objInvoiceReceipt.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                        objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                        objInvoiceReceipt.CASH = objReader["CASH"] != DBNull.Value ? Convert.ToDecimal(objReader["CASH"]) : 0;
                        objInvoiceReceipt.KNET = objReader["KNET"] != DBNull.Value ? Convert.ToDecimal(objReader["KNET"]) : 0;
                        objInvoiceReceipt.VISA = objReader["VISA"] != DBNull.Value ? Convert.ToDecimal(objReader["VISA"]) : 0;
                        objInvoiceReceipt.CREDITCARD = objReader["CREDITCARD"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDITCARD"]) : 0;
                        objInvoiceReceipt.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                        objInvoiceReceipt.PaymentCash = objReader["PaymentCash"] != DBNull.Value ? Convert.ToDecimal(objReader["PaymentCash"]) : 0;
                        objInvoiceReceipt.ExchangeAmount = objReader["ExchangeAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeAmount"]) : 0;
                        objInvoiceReceipt.ShiftID = Convert.ToInt32(objReader["ShiftID"]);
                        objInvoiceReceipt.CurrencyName = objReader["CurrencyName"].ToString();
                        objInvoiceReceipt.KDAmount = objReader["KDAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["KDAmount"]) : 0;
                        InvoiceReceiptList.Add(objInvoiceReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.XSubReportList = InvoiceReceiptList;
                }
                else
                {
                    while (objReader.Read())
                    {
                        var objInvoiceReceipt = new XSubreportTypes();
                        objInvoiceReceipt.ReturnAmount =  0;
                        objInvoiceReceipt.DecimalPlaces =  0;
                        objInvoiceReceipt.CASH =  0;
                        objInvoiceReceipt.KNET =  0;
                        objInvoiceReceipt.VISA =0;
                        objInvoiceReceipt.CREDITCARD =  0;
                        objInvoiceReceipt.PaymentCurrency = "";
                        objInvoiceReceipt.PaymentCash = 0;
                        objInvoiceReceipt.ExchangeAmount =  0;
                        objInvoiceReceipt.ShiftID = 0 ;
                        InvoiceReceiptList.Add(objInvoiceReceipt);
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XSubReportList = InvoiceReceiptList;
                    }
                   
                  
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

        //public override SelectDayInResponse GetDayIn(SelectDayInRequest RequestObj)
        //{
        //    var ShiftList = new List<ShiftMaster>();
        //    var RequestData = (SelectDayInRequest)RequestObj;
        //    var ResponseData = new SelectDayInResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        _ConnectionString = RequestData.ConnectionString;
        //        _RequestFrom = RequestData.RequestFrom;
        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
        //        string sSql = null;
        //        if (RequestData.UserName == "")
        //        {
        //            sSql = "Select top 1 * from ShiftLog with(nolock) where StoreID=" + RequestData.StoreID + " AND Status='Open' Order by ID DESC";
        //        }
        //        else if((RequestData.Mode=="Shift")||(RequestData.Mode == "Day"))
        //        {
        //            sSql = "Select * from ShiftLog with(NoLock) where BusinessDate = '" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and StoreID=" + RequestData.StoreID + " and (Status='Open' or ShiftStatus='Open') and POSID="+RequestData.POSID+" and ShiftInUserID = " + RequestData.UserID + ") ";
        //        }

        //        _CommandObj = new SqlCommand(sSql, _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                var objShift = new ShiftMaster();
        //                objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
        //                objShift.BusinessDate = Convert.ToDateTime(objReader["BusinessDate"]);
        //                objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
        //                objShift.Status = Convert.ToString(objReader["Status"]);
        //                objShift.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0; ;
        //                objShift.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0; ;
        //                objShift.POSCode = Convert.ToString(objReader["POSCode"]);
        //                ShiftList.Add(objShift);
        //            }
        //            ResponseData.ShiftList = ShiftList;
        //            ResponseData.POSList = null;
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
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

        public override SelectDayInResponse GetDayIn(SelectDayInRequest RequestObj)
        {
            var ShiftList = new List<ShiftMaster>();
            var POSavailableList = new List<PosMaster>();
            var ShiftMasterList = new List<ShiftMaster>();
            var RequestData = (SelectDayInRequest)RequestObj;
            var ResponseData = new SelectDayInResponse();
            SqlDataReader objReader;
            SqlDataReader objPosReader;
            DateTime businessdate = new DateTime();
            SqlDataReader objShiftListReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = null;                
                //sSql = "Select * from ShiftLog with(NoLock) where StoreID=" + RequestData.StoreID + " and (Status='Open' or ShiftStatus='Open') and ShiftInUserID = " + RequestData.UserID ;
                _CommandObj = new SqlCommand("API_GetDayIN", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreId", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@UserId", RequestData.UserID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                var objShift = new ShiftMaster();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        
                        objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objShift.BusinessDate = Convert.ToDateTime(objReader["BusinessDate"]);
                        objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        objShift.Status = Convert.ToString(objReader["Status"]);
                        objShift.ShiftStatus = Convert.ToString(objReader["ShiftStatus"]);
                        objShift.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0; 
                        objShift.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0; 
                        objShift.POSCode = Convert.ToString(objReader["POSCode"]);
                        businessdate = Convert.ToDateTime(objReader["BusinessDate"]);
                        objShift.POSName = Convert.ToString(objReader["POSName"]);
                        objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.Dayin = objReader["Dayin"] != DBNull.Value ? Convert.ToBoolean(objReader["Dayin"]) : false; 
                        objShift.Shiftin = objReader["ShiftIn"] != DBNull.Value ? Convert.ToBoolean(objReader["ShiftIn"]) : false;

                        objShift.DefaultCustomerID = objReader["DefaultCustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCustomerID"]) : 0;
                        objShift.DefaultCustomerGroupID = objReader["DefaultCustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCustomerGroupID"]) : 0;
                        objShift.DefaultCustomerCode = Convert.ToString(objReader["DefaultCustomerCode"]);
                        objShift.DefaultCustomerName = Convert.ToString(objReader["DefaultCustomerName"]);
                        objShift.DefaultPhoneNumber = Convert.ToString(objReader["DefaultPhoneNumber"]);
                        objShift.DefaultCustomerGroupCode = Convert.ToString(objReader["DefaultCustomerGroupCode"]);
                        objShift.PrinterDeviceName = Convert.ToString(objReader["PrinterDeviceName"]);

                        ShiftList.Add(objShift);
                    }
                    ResponseData.LogShiftList = objShift;
                    /*  _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                  _CommandObj.CommandType = CommandType.Text;
                  objReader = _CommandObj.ExecuteReader();
                  if (objReader.HasRows)
                  {
                      while (objReader.Read())
                      {
                          var objShift = new ShiftMaster();
                          objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                          objShift.BusinessDate = Convert.ToDateTime(objReader["BusinessDate"]);
                          objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                          objShift.Status = Convert.ToString(objReader["Status"]);
                          objShift.ShiftStatus = Convert.ToString(objReader["ShiftStatus"]);
                          objShift.POSID = objReader["POSID"] != DBNull.Value ? Convert.ToInt32(objReader["POSID"]) : 0; ;
                          objShift.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0; ;
                          objShift.POSCode = Convert.ToString(objReader["POSCode"]);
                          businessdate = Convert.ToDateTime(objReader["BusinessDate"]);
                          ShiftList.Add(objShift);
                      }*/
                    SqlConnection con = new SqlConnection();
                    //con = RequestData.ConnectionString;
                    sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    //con.Open();
                    //var sql = "select * from posmaster where id not in (select posid from shiftlog where shiftstatus = 'Open') and storeid = " + RequestData.StoreID + " order by ID";
                    var sql = ";with Shift_log " +
                                "as ( " +
                                    "select posid from shiftlog " +
                                    "where storeid = " + RequestData.StoreID + " and shiftstatus = 'Open' " +
                                ") " +
                                "select p.ID, p.PosCode, p.PosName " +
                                "from PosMaster p " +
                                "left outer join Shift_log s on p.ID = s.POSID " +
                                "where p.StoreID = " + RequestData.StoreID + " " +
                                "and s.POSID is null " +
                                "order by p.ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    objPosReader = cmd.ExecuteReader();
                    if (objPosReader.HasRows)
                    {
                        while (objPosReader.Read())
                        {
                            var objpos = new PosMaster();
                            objpos.ID = objPosReader["ID"] != DBNull.Value ? Convert.ToInt32(objPosReader["ID"]) : 0;
                            objpos.POSID = objPosReader["ID"] != DBNull.Value ? Convert.ToInt32(objPosReader["ID"]) : 0;
                            objpos.PosCode = Convert.ToString(objPosReader["PosCode"]); 
                            objpos.PosName = Convert.ToString(objPosReader["PosName"]);
                            POSavailableList.Add(objpos);
                        }
                    }

                    SqlConnection con1 = new SqlConnection();
                    sqlCommon.InitializeDataComponents(ref con1, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    //var Ssql = "Select sm.*,sl.ID as shiftLogID,sl.StoreID,sl.ShiftInUserID,sl.ShiftOutUserID,sl.ShiftInDateTime,";
                    //Ssql = Ssql + "sl.ShiftOutDateTime,sl.POSID,sl.ShiftID, SL.CountryID,sl.BusinessDate,sl.Status,sl.ShiftStatus,sl.ShiftInAmount, sl.Status AS OriginalDayInStatus,sl.ShiftStatus  AS OriginalShiftInStatus";
                    //Ssql = Ssql + " from  ShiftMaster SM  left join ShiftLOG SL on SL.ShiftID = SM.ID AND  sl.Status = 'Open' and SL.CountryID='" + RequestData.CountryID + "' and SL.BusinessDate = '" + sqlCommon.GetSQLServerDateString(businessdate) + "' and SL.StoreID = '" + RequestData.StoreID + "'  where sm.CountryID='" + RequestData.CountryID + "' ORDER BY SM.SortOrder";

                    var Ssql = " Select sm.ID, sm.SortOrder, sm.ShiftCode, sl.Status, sl.ShiftStatus " +
                                "from  ShiftMaster SM " +
                                "left join ShiftLOG SL on SL.ShiftID = SM.ID AND sl.Status = 'Open' " +
                                    "and SL.CountryID = " + RequestData.CountryID + " " +
                                    "and SL.BusinessDate = '" + sqlCommon.GetSQLServerDateString(businessdate) + "' " +
                                    "and SL.StoreID = " + RequestData.StoreID + " " +
                                "where sm.CountryID = " + RequestData.CountryID + " " +
                                "ORDER BY SM.SortOrder";



                    SqlCommand cmd1 = new SqlCommand(Ssql, con1);
                    cmd1.CommandType = CommandType.Text;
                    objShiftListReader = cmd1.ExecuteReader();
                    if (objShiftListReader.HasRows)
                    {
                        while (objShiftListReader.Read())
                        {
                            var objshiftmasterlist = new ShiftMaster();
                            objshiftmasterlist.ID = objShiftListReader["ID"] != DBNull.Value ? Convert.ToInt32(objShiftListReader["ID"]) : 0;
                            objshiftmasterlist.SortOrder = objShiftListReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objShiftListReader["SortOrder"]) : 0;
                            objshiftmasterlist.ShiftCode = Convert.ToString(objShiftListReader["ShiftCode"]);
                            objshiftmasterlist.Status = Convert.ToString(objShiftListReader["Status"]);
                            objshiftmasterlist.ShiftStatus = Convert.ToString(objShiftListReader["ShiftStatus"]);
                            ShiftMasterList.Add(objshiftmasterlist);
                        }
                    }
                    //ResponseData.ShiftList = ShiftList;
                    ResponseData.POSList = POSavailableList;
                    ResponseData.ShiftMasterList = ShiftMasterList;
                    if (ResponseData.ShiftMasterList.Count > 0)
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Master");
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Day In");
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

        public override SelectDayInResponse UpdateShift(SelectDayInRequest RequestObj)
        {
            var ShiftList = new List<ShiftMaster>();
            var POSavailableList = new List<PosMaster>();
            var RequestData = (SelectDayInRequest)RequestObj;
            var ResponseData = new SelectDayInResponse();
            SqlDataReader objReader;
            SqlDataReader objPosReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = null;
                sSql = "Update ShiftLog set ShiftStatus='Close',ShiftOutUserID="+RequestData.UserID+ ",ShiftOutDateTime=SYSDATETIME(),ShiftOutUserCode = ShiftInUserCode, ShiftOutAmount='"+RequestData.Amount+"' where StoreID=" + RequestData.StoreID + " and ShiftInUserID = " + RequestData.UserID + " and BusinessDate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and POSID="+RequestData.POSID;


                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                //objReader = _CommandObj.ExecuteReader();
                _CommandObj.ExecuteNonQuery();

                SqlConnection con = new SqlConnection();
                //con = RequestData.ConnectionString;
                sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //con.Open();
                var sql = "update UserMaster set IsLoggedIn=0, IsLoggedPosID='0' where ID="+RequestData.UserID;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "ShiftMaster");
            }
            catch(Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ShiftMaster");
            }
        
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectAllShiftResponse API_SelectALL(SelectAllShiftRequest requestData)
        {
            var ShiftList = new List<ShiftMaster>();
            var RequestData = (SelectAllShiftRequest)requestData;
            var ResponseData = new SelectAllShiftResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select distinct SM.CountryID,CM.countrycode,CM.countryname from ShiftMaster SM with(NoLock) join countrymaster CM with(nolock) on CM.id=sm.countryid";

                string sSql = "Select distinct SM.CountryID, CM.countrycode, CM.countryname, SM.Active, RC.TOTAL_CNT [RecordCount] " +
                  "from ShiftMaster SM with(NoLock) " +
                  "join countrymaster CM with(nolock) on CM.id=sm.countryid " +
                  "LEFT JOIN(Select  count(distinct SM1.CountryID) As TOTAL_CNT From ShiftMaster SM1 with(NoLock) " +
                  "join countrymaster CM1 with(nolock) on CM1.id=SM1.countryid " +
                  "where SM1.Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                      "or CM1.countrycode like isnull('%" + RequestData.SearchString + "%','') " +
                      "or CM1.countryname like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +

                  "where SM.Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                      "or CM.countrycode like isnull('%" + RequestData.SearchString + "%','') " +
                      "or CM.countryname like isnull('%" + RequestData.SearchString + "%','')) " +
                      "order by SM.CountryID asc " +
                      "offset " + RequestData.Offset + " rows " +
                      "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objShift = new ShiftMaster();
                        //objShift.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objShift.ShiftCode = Convert.ToString(objReader["ShiftCode"]);
                        //objShift.ShiftName = Convert.ToString(objReader["ShiftName"]);
                        objShift.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objShift.CountryName = Convert.ToString(objReader["CountryName"]);  
                        //objShift.SortOrder = objReader["SortOrder"] != DBNull.Value ? Convert.ToInt32(objReader["SortOrder"]) : 0;
                        //objShift.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objShift.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objShift.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objShift.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objShift.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objShift.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objShift.CountryCode = Convert.ToString(objReader["countrycode"]);
                        objShift.CountryName = Convert.ToString(objReader["countryname"]);

                        SelectShiftListForCategoryRequest objSelectshiftrequest = new SelectShiftListForCategoryRequest();
                        SelectShiftListForCategoryResponse objSelectshiftResponse = new SelectShiftListForCategoryResponse();
                        objSelectshiftrequest.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSelectshiftrequest.ShowInActiveRecords = true;
                        //objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectshiftResponse = SelectShiftListByCountry(objSelectshiftrequest);
                        if (objSelectshiftResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objShift.Shiftlist = objSelectshiftResponse.ShiftList;
                        }

                        ShiftList.Add(objShift);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.ShiftList = ShiftList;
                    //ResponseData.ResponseDynamicData = ShiftList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ShiftMaster");
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

       

        public override SelectNewZReportByDetailsResponse GetZReceiptdetails(SelectNewZReportByDetailsRequest RequestObj)
        {
            var RequestData = (SelectNewZReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectNewZReportByDetailsResponse();
            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                using (_CommandObj = new SqlCommand("SP_NewDayClose1", _ConnectionObj))
                {
                    SqlDataReader objReader;
                    var ZReceiptList1 = new List<NewZReportDetails1>();

                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    if (RequestData.BusinessDate != null)
                    {
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                    }
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new NewZReportDetails1();
                            objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                            objInvoiceReceipt.ShopCode = objReader["ShopCode"] != DBNull.Value ? Convert.ToString(objReader["ShopCode"]) : string.Empty;
                            objInvoiceReceipt.Country = objReader["Country"] != DBNull.Value ? Convert.ToString(objReader["Country"]) : string.Empty;
                            objInvoiceReceipt.Date = objReader["Date"] != DBNull.Value ? Convert.ToDateTime(objReader["Date"]) : DateTime.Now;
                            objInvoiceReceipt.Time = objReader["Time"] != DBNull.Value ? Convert.ToString(objReader["Time"]) : string.Empty;
                            objInvoiceReceipt.ShopName = objReader["ShopName"] != DBNull.Value ? Convert.ToString(objReader["ShopName"]) : string.Empty;
                            ZReceiptList1.Add(objInvoiceReceipt);


                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ZReportList1 = ZReceiptList1;

                    }
                    objReader.Close();
                }
                using (_CommandObj = new SqlCommand("SP_Z_NewInvoiceDetails", _ConnectionObj))
                {
                    SqlDataReader objReader;
                    var ZReceiptList1 = new List<NewZReportDetails2>();
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    if (RequestData.BusinessDate != null && RequestData.StoreID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                    }
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt1 = new NewZReportDetails2();
                            objInvoiceReceipt1.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objInvoiceReceipt1.InvoiceNo = objReader["InvoiceNo"].ToString();
                            objInvoiceReceipt1.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToDecimal(objReader["Amount"]) : 0;
                            objInvoiceReceipt1.DiscountAmount = objReader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountAmount"]) : 0;
                            objInvoiceReceipt1.Category = objReader["Category"] != DBNull.Value ? Convert.ToString(objReader["Category"]) : string.Empty;
                            ZReceiptList1.Add(objInvoiceReceipt1);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ZReportList2 = ZReceiptList1;
                    }
                    objReader.Close();
                }
                using (_CommandObj = new SqlCommand("SP_NewDayClosing1", _ConnectionObj))
                {
                    SqlDataReader objReader;
                    var Zreport2 = new List<NewZReportDetails3>();
                    _CommandObj.CommandType = CommandType.StoredProcedure;

                    if (RequestData.BusinessDate != null && RequestData.StoreID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                    }
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt1 = new NewZReportDetails3();
                            objInvoiceReceipt1.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToString(objReader["ShiftID"]) : "";
                            objInvoiceReceipt1.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                            objInvoiceReceipt1.KNET = objReader["KNET"] != DBNull.Value ? Convert.ToDecimal(objReader["KNET"]) : 0;
                            objInvoiceReceipt1.VISA = objReader["VISA"] != DBNull.Value ? Convert.ToDecimal(objReader["VISA"]) : 0;
                            objInvoiceReceipt1.CREDITCARD = objReader["CREDITCARD"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDITCARD"]) : 0;
                            objInvoiceReceipt1.PaymentCash = objReader["PaymentCash"] != DBNull.Value ? Convert.ToDecimal(objReader["PaymentCash"]) : 0;
                            objInvoiceReceipt1.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            objInvoiceReceipt1.TotalCash = objReader["TotalCash"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCash"]) : 0;
                            objInvoiceReceipt1.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                            objInvoiceReceipt1.CurrencyName = objReader["CurrencyName"].ToString();
                            objInvoiceReceipt1.ForeignTotal = objReader["ForeignTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["ForeignTotal"]) : 0;
                            objInvoiceReceipt1.Credit = objReader["CREDIT"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDIT"]) : 0;
                            objInvoiceReceipt1.POSID = objReader["POSID"].ToString(); //nvert.ToInt32(objReader["IsReturn1"]) : 0;

                            Zreport2.Add(objInvoiceReceipt1);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ZReportList3 = Zreport2;
                    }
                   // else
                    //{
                    //    //var objInvoiceReceipt = new Zreport2();

                    //    //objInvoiceReceipt.SubTotal = 0;
                    //    //objInvoiceReceipt.DiscountTotal = 0;
                    //    //objInvoiceReceipt.InvoiceNo = "";
                    //    //objInvoiceReceipt.StoreImage = null;
                    //    //objInvoiceReceipt.TotalDiscount = 0;
                    //    //objInvoiceReceipt.TotalAmount = 0;
                    //    objInvoiceReceipt.CreditSale = 0;
                    //    objInvoiceReceipt.IsReturn = true;
                    //    objInvoiceReceipt.DecimalPlaces = 0;
                    //    objInvoiceReceipt.BusinessDate = DateTime.Now;
                    //    objInvoiceReceipt.Cashier = "";
                    //    objInvoiceReceipt.ShopName = "";
                    //    objInvoiceReceipt.ShiftName = "";
                    //    objInvoiceReceipt.PosName = "";
                    //    objInvoiceReceipt.NetSales = 0;
                    //    objInvoiceReceipt.FloatAmount = 0;
                    //    objInvoiceReceipt.TotalCashInBox = 0;
                    //    objInvoiceReceipt.CashInAmount = 0;
                    //    objInvoiceReceipt.CashOutAmount = 0;
                    //    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    //    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Print");
                    //    Zreport2.Add(objInvoiceReceipt);
                    //}
                   // ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.Zreport2 = Zreport2;
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

        public override SelectXReportByDetailsResponse GetXReceiptDetails(SelectXReportByDetailsRequest RequestObj)
        {
            var RequestData = (SelectXReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectXReportByDetailsResponse();
           
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
            
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                using (_CommandObj = new SqlCommand("SP_ShiftClose", _ConnectionObj))
                {
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    var InvoiceReceiptList = new List<XreportTypes>();
                    SqlDataReader objReader;
                    if (RequestData.CashierID != 0 && RequestData.ShiftID != 0 && RequestData.BusinessDate != null && RequestData.StoreID != 0 && RequestData.POSID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                        _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                        _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", "");
                        _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                        _CommandObj.Parameters.AddWithValue("@StoreID", "");
                        _CommandObj.Parameters.AddWithValue("@POSID", "");
                    }

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XreportTypes();

                            objInvoiceReceipt.SubTotal = objReader["SubTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotal"]) : 0;
                            objInvoiceReceipt.DiscountTotal = objReader["DiscountTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountTotal"]) : 0;
                            objInvoiceReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                            objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                            objInvoiceReceipt.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                            objInvoiceReceipt.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                            objInvoiceReceipt.CreditSale = objReader["CreditSale"] != DBNull.Value ? Convert.ToInt32(objReader["CreditSale"]) : 0;
                            objInvoiceReceipt.IsReturn = objReader["IsReturn"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturn"]) : true;
                            objInvoiceReceipt.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                            objInvoiceReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                            objInvoiceReceipt.ShopName = objReader["ShopName"].ToString();
                            objInvoiceReceipt.PosName = objReader["POSName"] != DBNull.Value ? Convert.ToString(objReader["POSName"]) : string.Empty;
                            objInvoiceReceipt.ShiftName = objReader["ShiftName"].ToString();
                            objInvoiceReceipt.NetSales = objReader["NetSales"] != DBNull.Value ? Convert.ToDecimal(objReader["NetSales"]) : 0;
                            objInvoiceReceipt.FloatAmount = objReader["FloatAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FloatAmount"]) : 0;
                            objInvoiceReceipt.TotalCashInBox = objReader["TotalCashInBox"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCashInBox"]) : 0;
                            objInvoiceReceipt.CashInAmount = objReader["CashInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashInAmount"]) : 0;
                            objInvoiceReceipt.CashOutAmount = objReader["CashOutAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashOutAmount"]) : 0;
                            // objInvoiceReceipt.IsReturn1 = objReader["IsReturn1"] != DBNull.Value ? Convert.ToDecimal(objReader["IsReturn1"]) : 0;
                            objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList = InvoiceReceiptList;
                    }
                    else
                    {

                        var objInvoiceReceipt = new XreportTypes();

                        objInvoiceReceipt.SubTotal = 0;
                        objInvoiceReceipt.DiscountTotal = 0;
                        objInvoiceReceipt.InvoiceNo = "";
                        objInvoiceReceipt.StoreImage = null;
                        objInvoiceReceipt.TotalDiscount = 0;
                        objInvoiceReceipt.TotalAmount = 0;
                        objInvoiceReceipt.CreditSale = 0;
                        objInvoiceReceipt.IsReturn = true;
                        objInvoiceReceipt.BusinessDate = DateTime.MinValue;
                        objInvoiceReceipt.Cashier = "";
                        objInvoiceReceipt.ShopName = "";
                        objInvoiceReceipt.PosName = "";
                        objInvoiceReceipt.ShiftName = "";
                        objInvoiceReceipt.NetSales = 0;
                        objInvoiceReceipt.FloatAmount = 0;
                        objInvoiceReceipt.TotalCashInBox = 0;
                        objInvoiceReceipt.CashInAmount = 0;
                        objInvoiceReceipt.CashOutAmount = 0;
                        // objInvoiceReceipt.IsReturn1 = objReader["IsReturn1"] != DBNull.Value ? Convert.ToDecimal(objReader["IsReturn1"]) : 0;
                        objInvoiceReceipt.DecimalPlaces = 0;
                        InvoiceReceiptList.Add(objInvoiceReceipt);

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList = InvoiceReceiptList;
                    }
                    objReader.Close();
                }
                using (_CommandObj = new SqlCommand("GetXDetails1", _ConnectionObj))
                {
                    var InvoiceReceiptList = new List<XSubreportTypes>();
                    SqlDataReader objReader;
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    if (RequestData.CashierID != 0 && RequestData.ShiftID != 0 && RequestData.BusinessDate != null && RequestData.StoreID != 0 && RequestData.POSID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                        _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                        _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", "");
                        _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                        _CommandObj.Parameters.AddWithValue("@StoreID", "");
                        _CommandObj.Parameters.AddWithValue("@POSID", "");
                    }

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XSubreportTypes();
                            objInvoiceReceipt.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                            objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            objInvoiceReceipt.CASH = objReader["CASH"] != DBNull.Value ? Convert.ToDecimal(objReader["CASH"]) : 0;
                            objInvoiceReceipt.KNET = objReader["KNET"] != DBNull.Value ? Convert.ToDecimal(objReader["KNET"]) : 0;
                            objInvoiceReceipt.VISA = objReader["VISA"] != DBNull.Value ? Convert.ToDecimal(objReader["VISA"]) : 0;
                            objInvoiceReceipt.CREDITCARD = objReader["CREDITCARD"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDITCARD"]) : 0;
                            objInvoiceReceipt.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                            objInvoiceReceipt.PaymentCash = objReader["PaymentCash"] != DBNull.Value ? Convert.ToDecimal(objReader["PaymentCash"]) : 0;
                            objInvoiceReceipt.ExchangeAmount = objReader["ExchangeAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeAmount"]) : 0;
                            objInvoiceReceipt.ShiftID = Convert.ToInt32(objReader["ShiftID"]);
                            objInvoiceReceipt.CurrencyName = objReader["CurrencyName"].ToString();
                            objInvoiceReceipt.KDAmount = objReader["KDAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["KDAmount"]) : 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XSubReportList = InvoiceReceiptList;
                    }
                    else
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XSubreportTypes();
                            objInvoiceReceipt.ReturnAmount = 0;
                            objInvoiceReceipt.DecimalPlaces = 0;
                            objInvoiceReceipt.CASH = 0;
                            objInvoiceReceipt.KNET = 0;
                            objInvoiceReceipt.VISA = 0;
                            objInvoiceReceipt.CREDITCARD = 0;
                            objInvoiceReceipt.PaymentCurrency = "";
                            objInvoiceReceipt.PaymentCash = 0;
                            objInvoiceReceipt.ExchangeAmount = 0;
                            objInvoiceReceipt.ShiftID = 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                            ResponseData.XSubReportList = InvoiceReceiptList;
                        }


                    }
                    objReader.Close();
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

        public override SelectNewXReportByDetailsReponse GetNewXReceiptDetails(SelectNewXReportByDetailsRequest RequestObj)
        {
            var RequestData = (SelectNewXReportByDetailsRequest)RequestObj;
            var ResponseData = new SelectNewXReportByDetailsReponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                using (_CommandObj = new SqlCommand("API_XReportHeader", _ConnectionObj))
                {
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    var InvoiceReceiptList = new List<XReportHeaderTypes>();
                    SqlDataReader objReader;
                    if (RequestData.CashierID != 0 && RequestData.ShiftID != 0 && RequestData.BusinessDate != null && RequestData.StoreID != 0 && RequestData.POSID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                        _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                        _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", "");
                        _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                        _CommandObj.Parameters.AddWithValue("@StoreID", "");
                        _CommandObj.Parameters.AddWithValue("@POSID", "");
                    }

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XReportHeaderTypes();

                            //objInvoiceReceipt.SubTotal = objReader["SubTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotal"]) : 0;
                            //objInvoiceReceipt.DiscountTotal = objReader["DiscountTotal"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountTotal"]) : 0;
                            //objInvoiceReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                            objInvoiceReceipt.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                            //objInvoiceReceipt.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                            //objInvoiceReceipt.TotalAmount = objReader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalAmount"]) : 0;
                            //objInvoiceReceipt.CreditSale = objReader["CreditSale"] != DBNull.Value ? Convert.ToInt32(objReader["CreditSale"]) : 0;
                            //objInvoiceReceipt.IsReturn = objReader["IsReturn"] != DBNull.Value ? Convert.ToBoolean(objReader["IsReturn"]) : true;
                            objInvoiceReceipt.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
                            objInvoiceReceipt.Cashier = objReader["Cashier"] != DBNull.Value ? Convert.ToString(objReader["Cashier"]) : string.Empty;
                            objInvoiceReceipt.ShopName = objReader["ShopName"].ToString();
                            objInvoiceReceipt.PosName = objReader["PosName"] != DBNull.Value ? Convert.ToString(objReader["POSName"]) : string.Empty;
                            objInvoiceReceipt.Time = objReader["time"] != DBNull.Value ? Convert.ToString(objReader["time"]) : string.Empty;
                            objInvoiceReceipt.ShiftName = objReader["Shiftname"].ToString();
                            //objInvoiceReceipt.NetSales = objReader["NetSales"] != DBNull.Value ? Convert.ToDecimal(objReader["NetSales"]) : 0;
                           
                            //// objInvoiceReceipt.IsReturn1 = objReader["IsReturn1"] != DBNull.Value ? Convert.ToDecimal(objReader["IsReturn1"]) : 0;
                            //objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList = InvoiceReceiptList;
                    }
                    else
                    {

                        var objInvoiceReceipt = new XReportHeaderTypes();

                      
                        objInvoiceReceipt.BusinessDate = DateTime.MinValue;
                        objInvoiceReceipt.Cashier = "";
                        objInvoiceReceipt.ShopName = "";
                        objInvoiceReceipt.PosName = "";
                        objInvoiceReceipt.ShiftName = "";
          
                        InvoiceReceiptList.Add(objInvoiceReceipt);

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList = InvoiceReceiptList;
                    }
                    objReader.Close();
                }
                using (_CommandObj = new SqlCommand("API_XReportInvoiceDetails", _ConnectionObj))
                {
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    var InvoiceReceiptList = new List<XReportDetailsTypes>();
                    SqlDataReader objReader;
                    if (RequestData.CashierID != 0 && RequestData.ShiftID != 0 && RequestData.BusinessDate != null && RequestData.StoreID != 0 && RequestData.POSID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                        _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                        _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", "");
                        _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                        _CommandObj.Parameters.AddWithValue("@StoreID", "");
                        _CommandObj.Parameters.AddWithValue("@POSID", "");
                    }

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XReportDetailsTypes();

                       
                            objInvoiceReceipt.DiscountTotal = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalDiscount"]) : 0;
                            objInvoiceReceipt.InvoiceNo = objReader["InvoiceNo"].ToString();
                            objInvoiceReceipt.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToDecimal(objReader["Amount"]) : 0;
                            objInvoiceReceipt.Category = objReader["Category"] != DBNull.Value ? Convert.ToString(objReader["Category"]) : string.Empty;
                            objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList1 = InvoiceReceiptList;
                    }
                    else
                    {

                        var objInvoiceReceipt = new XReportDetailsTypes();
                        objInvoiceReceipt.InvoiceNo = null;
                        objInvoiceReceipt.Amount = 0;
                        objInvoiceReceipt.Category = "";
                        objInvoiceReceipt.DiscountTotal = 0;
                        objInvoiceReceipt.DecimalPlaces = 0;
                        InvoiceReceiptList.Add(objInvoiceReceipt);
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList1 = InvoiceReceiptList;
                    }
                    objReader.Close();
                }
                using (_CommandObj = new SqlCommand("API_GetXDetailsSummary", _ConnectionObj))
                {
                    var InvoiceReceiptList = new List<XReportSummaryTypes>();
                    SqlDataReader objReader;
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    if (RequestData.CashierID != 0 && RequestData.ShiftID != 0 && RequestData.BusinessDate != null && RequestData.StoreID != 0 && RequestData.POSID != 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", RequestData.CashierID);
                        _CommandObj.Parameters.AddWithValue("@ShiftID", RequestData.ShiftID);
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", sqlCommon.GetSQLServerDateString(RequestData.BusinessDate));
                        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                        _CommandObj.Parameters.AddWithValue("@POSID", RequestData.POSID);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@CashierID", "");
                        _CommandObj.Parameters.AddWithValue("@ShiftID", "");
                        _CommandObj.Parameters.AddWithValue("@BusinessDate", "");
                        _CommandObj.Parameters.AddWithValue("@StoreID", "");
                        _CommandObj.Parameters.AddWithValue("@POSID", "");
                    }

                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XReportSummaryTypes();
                            objInvoiceReceipt.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
                            objInvoiceReceipt.DecimalPlaces = objReader["DecimalPlaces"] != DBNull.Value ? Convert.ToInt32(objReader["DecimalPlaces"]) : 0;
                            objInvoiceReceipt.CASH = objReader["CASH"] != DBNull.Value ? Convert.ToDecimal(objReader["CASH"]) : 0;
                            objInvoiceReceipt.KNET = objReader["KNET"] != DBNull.Value ? Convert.ToDecimal(objReader["KNET"]) : 0;
                            objInvoiceReceipt.VISA = objReader["VISA"] != DBNull.Value ? Convert.ToDecimal(objReader["VISA"]) : 0;
                            objInvoiceReceipt.CREDITCARD = objReader["CREDITCARD"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDITCARD"]) : 0;
                            objInvoiceReceipt.PaymentCurrency = objReader["PaymentCurrency"].ToString();
                            objInvoiceReceipt.PaymentCash = objReader["PaymentCash"] != DBNull.Value ? Convert.ToDecimal(objReader["PaymentCash"]) : 0;
                            objInvoiceReceipt.ExchangeAmount = objReader["ExchangeAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeAmount"]) : 0;
                            objInvoiceReceipt.ShiftID = Convert.ToInt32(objReader["ShiftID"]);
                            objInvoiceReceipt.CurrencyName = objReader["CurrencyName"].ToString();
                            objInvoiceReceipt.KDAmount = objReader["KDAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["KDAmount"]) : 0;
                            objInvoiceReceipt.FloatAmount = objReader["FloatAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["FloatAmount"]) : 0;
                            objInvoiceReceipt.TotalCashInBox = objReader["TotalCashInBox"] != DBNull.Value ? Convert.ToDecimal(objReader["TotalCashInBox"]) : 0;
                            objInvoiceReceipt.CashInAmount = objReader["CashInAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashInAmount"]) : 0;
                            objInvoiceReceipt.CashOutAmount = objReader["CashOutAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CashOutAmount"]) : 0;
                            objInvoiceReceipt.CREDIT = objReader["CREDIT"] != DBNull.Value ? Convert.ToDecimal(objReader["CREDIT"]) : 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.XReportList2 = InvoiceReceiptList;
                    }
                    else
                    {
                        while (objReader.Read())
                        {
                            var objInvoiceReceipt = new XReportSummaryTypes();
                            objInvoiceReceipt.ReturnAmount = 0;
                            objInvoiceReceipt.DecimalPlaces = 0;
                            objInvoiceReceipt.CASH = 0;
                            objInvoiceReceipt.KNET = 0;
                            objInvoiceReceipt.VISA = 0;
                            objInvoiceReceipt.CREDITCARD = 0;
                            objInvoiceReceipt.PaymentCurrency = "";
                            objInvoiceReceipt.PaymentCash = 0;
                            objInvoiceReceipt.ExchangeAmount = 0;
                            objInvoiceReceipt.ShiftID = 0;
                            InvoiceReceiptList.Add(objInvoiceReceipt);
                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                            ResponseData.XReportList2 = InvoiceReceiptList;
                        }


                    }
                    objReader.Close();
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
