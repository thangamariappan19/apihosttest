using EasyBizAbsDAL.Masters;
using EasyBizRequest;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ResourceStrings;
using EasyBizDBTypes.Common;
using MsSqlDAL.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.WarehouseMasterRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;

namespace MsSqlDAL.Masters
{
    public class WarehouseMasterDAL : BaseWarehouseMasterDAL
    {

      SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {

            var RequestData = (SaveWarehouseMasterRequest)RequestObj;
            var ResponseData = new SaveWarehouseMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertWarehouseMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter WarehouseID = _CommandObj.Parameters.Add("@WarehouseID", SqlDbType.Int);
                WarehouseID.Direction = ParameterDirection.Input;
                WarehouseID.Value = RequestData.WarehouseMasterData.ID;

                var WarehouseCode = _CommandObj.Parameters.Add("@WarehouseCode", SqlDbType.NVarChar);
                WarehouseCode.Direction = ParameterDirection.Input;
                WarehouseCode.Value = RequestData.WarehouseMasterData.WarehouseCode;

                var WarehouseName = _CommandObj.Parameters.Add("@WarehouseName", SqlDbType.NVarChar);
                WarehouseName.Direction = ParameterDirection.Input;
                WarehouseName.Value = RequestData.WarehouseMasterData.WarehouseName;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.NVarChar);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.WarehouseMasterData.CountryID;

                
                var CompanyID = _CommandObj.Parameters.Add("@CompanyID", SqlDbType.NVarChar);
                CompanyID.Direction = ParameterDirection.Input;
                CompanyID.Value = RequestData.WarehouseMasterData.CompanyID;

                var WarehouseTypeID = _CommandObj.Parameters.Add("@WarehouseTypeID", SqlDbType.NVarChar);
                WarehouseTypeID.Direction = ParameterDirection.Input;
                WarehouseTypeID.Value = RequestData.WarehouseMasterData.WarehouseTypeID;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.WarehouseMasterData.CreateBy;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.WarehouseMasterData.Remarks;

                var CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.WarehouseMasterData.CountryCode;

                var CompanyCode = _CommandObj.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
                CompanyCode.Direction = ParameterDirection.Input;
                CompanyCode.Value = RequestData.WarehouseMasterData.CompanyCode;

                var WarehouseTypeCode = _CommandObj.Parameters.Add("@WarehouseTypeCode", SqlDbType.NVarChar);
                WarehouseTypeCode.Direction = ParameterDirection.Input;
                WarehouseTypeCode.Value = RequestData.WarehouseMasterData.WarehouseTypeCode;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.WarehouseMasterData.Active;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Warehouse");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Warehouse");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Warehouse");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Warehouse");
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

            var RequestData = (UpdateWarehouseMasterRequest)RequestObj;
            var ResponseData = new UpdateWarehouseMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdateWarehouseMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.WarehouseMasterData.ID;

                var WarehouseCode = _CommandObj.Parameters.Add("@WarehouseCode", SqlDbType.NVarChar);
                WarehouseCode.Direction = ParameterDirection.Input;
                WarehouseCode.Value = RequestData.WarehouseMasterData.WarehouseCode;

                var WarehouseName = _CommandObj.Parameters.Add("@WarehouseName", SqlDbType.NVarChar);
                WarehouseName.Direction = ParameterDirection.Input;
                WarehouseName.Value = RequestData.WarehouseMasterData.WarehouseName;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.NVarChar);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.WarehouseMasterData.CountryID;


                var CompanyID = _CommandObj.Parameters.Add("@CompanyID", SqlDbType.NVarChar);
                CompanyID.Direction = ParameterDirection.Input;
                CompanyID.Value = RequestData.WarehouseMasterData.CompanyID;

                var WarehouseTypeID = _CommandObj.Parameters.Add("@WarehouseTypeID", SqlDbType.NVarChar);
                WarehouseTypeID.Direction = ParameterDirection.Input;
                WarehouseTypeID.Value = RequestData.WarehouseMasterData.WarehouseTypeID;

                var CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.WarehouseMasterData.CountryCode;

                var CompanyCode = _CommandObj.Parameters.Add("@CompanyCode", SqlDbType.NVarChar);
                CompanyCode.Direction = ParameterDirection.Input;
                CompanyCode.Value = RequestData.WarehouseMasterData.CompanyCode;

                var WarehouseTypeCode = _CommandObj.Parameters.Add("@WarehouseTypeCode", SqlDbType.NVarChar);
                WarehouseTypeCode.Direction = ParameterDirection.Input;
                WarehouseTypeCode.Value = RequestData.WarehouseMasterData.WarehouseTypeCode;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.WarehouseMasterData.UpdateBy;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.WarehouseMasterData.SCN;


                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.WarehouseMasterData.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.WarehouseMasterData.Active;


                //var UpdateOn = _CommandObj.Parameters.Add("@UpdateOn", SqlDbType.DateTime);
                //UpdateOn.Direction = ParameterDirection.Input;
                //UpdateOn.Value = RequestData.WarehouseMasterData.UpdateOn;
                
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Warehouse");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Warehouse");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Warehouse");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Warehouse");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var WarehouseMasterRecord = new WarehouseMaster();

            var RequestData = (DeleteWarehouseMasterRequest)RequestObj;
            var ResponseData = new DeleteWarehouseMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);              
                string sSql = "Delete from WarehouseMaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);       
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Warehouse");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Warehouse");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {

            var WarehouseMasterRecord = new WarehouseMaster();
            var RequestData = (SelectByIDWarehouseMasterRequest)RequestObj;
            var ResponseData = new SelectByIDWarehouseMasterResponse();
            SqlDataReader objReader;            
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                string sSql = "Select * from WarehouseMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);       
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseMaster = new WarehouseMaster();
                        objWarehouseMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseMaster.WarehouseCode = Convert.ToString(objReader["WarehouseCode"]);
                        objWarehouseMaster.WarehouseName = Convert.ToString(objReader["WarehouseName"]);
                        objWarehouseMaster.CountryID =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objWarehouseMaster.CompanyID =objReader["CompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["CompanyID"]) :0;
                        objWarehouseMaster.WarehouseTypeID = objReader["WarehouseTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["WarehouseTypeID"]) : 0;
                        objWarehouseMaster.WarehouseTypeCode = Convert.ToString(objReader["WarehouseTypeCode"]);
                        objWarehouseMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objWarehouseMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objWarehouseMaster.CompanyCode= Convert.ToString(objReader["CompanyCode"]);
                        objWarehouseMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objWarehouseMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objWarehouseMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; 
                        objWarehouseMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objWarehouseMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        ResponseData.WarehouseMasterRecord = objWarehouseMaster;
                        ResponseData.ResponseDynamicData = objWarehouseMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Warehouse Master");
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
            var WarehouseMasterList = new List<WarehouseMaster>();
            var RequestData = (SelectAllWarehouseMasterRequest)RequestObj;
            var ResponseData = new SelectAllWarehouseMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY WarehouseTypeName asc) AS RowNumber, ");
                sSql.Append("WM.*,CM.CountryName,CS.CompanyName,WT.WarehouseTypeName  from WarehouseMaster WM  ");
                sSql.Append("left join CompanySettings CS  on WM.CompanyID=CS.ID   ");                
                sSql.Append("left join CountryMaster CM  on WM.CountryID=CM.ID   ");
                sSql.Append("left join WarehouseTypeMaster WT  on WM.WarehouseTypeID=WT.ID  ");
                if (!RequestData.ShowInActiveRecords)
                {
                    
                    sSql.Append("and CM.Active='True'");
                    sSql.Append("and CS.Active='True'");
                }
                sSql.Append("order by id  asc");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //_CommandObj = new SqlCommand("Select * from WarehouseMaster with(NoLock) where Active='" + RequestData.ShowInActiveRecords + "'", _ConnectionObj);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseMaster = new WarehouseMaster();
                        objWarehouseMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseMaster.WarehouseCode = Convert.ToString(objReader["WarehouseCode"]);
                        objWarehouseMaster.WarehouseName = Convert.ToString(objReader["WarehouseName"]);
                        objWarehouseMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;                

                        objWarehouseMaster.CompanyID =objReader["CompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["CompanyID"]) :0;
                        objWarehouseMaster.WarehouseTypeID = objReader["WarehouseTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["WarehouseTypeID"]) : 0;
                        objWarehouseMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objWarehouseMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objWarehouseMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objWarehouseMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objWarehouseMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objWarehouseMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objWarehouseMaster.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        objWarehouseMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        objWarehouseMaster.RowNumber = Convert.ToString(objReader["RowNumber"]);
                        objWarehouseMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        
                        WarehouseMasterList.Add(objWarehouseMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseMasterList = WarehouseMasterList;
                    ResponseData.ResponseDynamicData = WarehouseMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Warehouse Master");
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
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.Transactions.Stocks.StockRequest.SelectWhareouseLookUpResponse SelectWhareHouseLookUp(EasyBizRequest.Transactions.Stocks.StockRequest.SelectWhareHouseLookUpRequest RequestObj)
        {
            var WarehouseList = new List<WarehouseMaster>();
            var RequestData = (EasyBizRequest.Transactions.Stocks.StockRequest.SelectWhareHouseLookUpRequest)RequestObj;
            var ResponseData = new SelectWhareouseLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);              
                sQuery = "Select * from WarehouseMaster with(NoLock) ";
                if(RequestData.CountryID != 0)
                {
                    sQuery = sQuery + "where countryid = '" + RequestData.CountryID +"'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new WarehouseMaster();
                        objStoreMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objStoreMaster.WarehouseCode = Convert.ToString(objReader["WarehouseCode"]);
                        objStoreMaster.WarehouseName = Convert.ToString(objReader["WarehouseName"]);
                        WarehouseList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseMasterList = WarehouseList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
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

        public override SelectAllWarehouseMasterResponse API_SelectAll(SelectAllWarehouseMasterRequest objRequest)
        {
            var WarehouseMasterList = new List<WarehouseMaster>();
            var RequestData = (SelectAllWarehouseMasterRequest)objRequest;
            var ResponseData = new SelectAllWarehouseMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY WarehouseTypeName asc) AS RowNumber, ");
                //sSql.Append("WM.*,CM.CountryName,CS.CompanyName,WT.WarehouseTypeName  from WarehouseMaster WM  ");
                //sSql.Append("left join CompanySettings CS  on WM.CompanyID=CS.ID   ");
                //sSql.Append("left join CountryMaster CM  on WM.CountryID=CM.ID   ");
                //sSql.Append("left join WarehouseTypeMaster WT  on WM.WarehouseTypeID=WT.ID  ");
                //if (!RequestData.ShowInActiveRecords)
                //{

                //    sSql.Append("and CM.Active='True'");
                //    sSql.Append("and CS.Active='True'");
                //}
                //sSql.Append("order by id  asc");

                string sSql = "Select WM.ID,WM.WarehouseCode,WM.WarehouseName,CM.CountryName,CS.CompanyName,WT.WarehouseTypeName,WM.Active ,WM.Remarks , RC.TOTAL_CNT [RecordCount] " +
                  "from WarehouseMaster WM  left join CompanySettings CS  on WM.CompanyID=CS.ID left join CountryMaster CM  on WM.CountryID=CM.ID " +
                  "left join WarehouseTypeMaster WT  on WM.WarehouseTypeID=WT.ID   " +
                  "LEFT JOIN(Select count(WM1.ID) As TOTAL_CNT From WarehouseMaster WM1 " +

                  "left join CompanySettings CS1  on WM1.CompanyID = CS1.ID " +

                  "left join CountryMaster CM1  on WM1.CountryID = CM1.ID " +

                  "left join WarehouseTypeMaster WT1  on WM1.WarehouseTypeID = WT1.ID " +

                  "where WM1.Active = " + RequestData.IsActive + "" +

                  "and (isnull('" + RequestData.SearchString + "','') = '' " +

                  "or WM1.WarehouseCode like isnull('%" + RequestData.SearchString + "%', '') " +

                  "or WM1.WarehouseName like isnull('%" + RequestData.SearchString + "%', '') " +


                  "or CM1.CountryName  like isnull('%" + RequestData.SearchString + "%', '') " +

                  "or WT1.WarehouseTypeName like isnull('%" + RequestData.SearchString + "%', '') " +


                  "or WM1.Remarks  like isnull('%" + RequestData.SearchString + "%','') " +

                  "or CS1.CompanyName  like isnull('%" + RequestData.SearchString + "%','') " +

                  "and CM1.Active = 'True' and CS1.Active = 'True')) As RC ON 1 = 1 " +
                  "where WM.Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or WM.WarehouseCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or WM.WarehouseName like isnull('%" + RequestData.SearchString + "%','') " +
                           "or CM.CountryName like isnull('%" + RequestData.SearchString + "%','') " +
                            "or WT.WarehouseTypeName like isnull('%" + RequestData.SearchString + "%','') " +
                             "or WM.Remarks like isnull('%" + RequestData.SearchString + "%','') " +
                            "or CS.CompanyName like isnull('%" + RequestData.SearchString + "%','')) " +
                  " and CM.Active='True' and CS.Active='True' order by id  asc " +
                  "offset " + RequestData.Offset + " rows " +
                  "fetch first " + RequestData.Limit + " rows only";

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //_CommandObj = new SqlCommand("Select * from WarehouseMaster with(NoLock) where Active='" + RequestData.ShowInActiveRecords + "'", _ConnectionObj);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWarehouseMaster = new WarehouseMaster();
                        objWarehouseMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWarehouseMaster.WarehouseCode = Convert.ToString(objReader["WarehouseCode"]);
                        objWarehouseMaster.WarehouseName = Convert.ToString(objReader["WarehouseName"]);
                       // objWarehouseMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;

                       // objWarehouseMaster.CompanyID = objReader["CompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["CompanyID"]) : 0;
                        //objWarehouseMaster.WarehouseTypeID = objReader["WarehouseTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["WarehouseTypeID"]) : 0;
                        //objWarehouseMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objWarehouseMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objWarehouseMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objWarehouseMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objWarehouseMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWarehouseMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objWarehouseMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objWarehouseMaster.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        objWarehouseMaster.WarehouseTypeName = Convert.ToString(objReader["WarehouseTypeName"]);
                        //objWarehouseMaster.RowNumber = Convert.ToString(objReader["RowNumber"]);
                        objWarehouseMaster.Remarks = Convert.ToString(objReader["Remarks"]);

                        WarehouseMasterList.Add(objWarehouseMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WarehouseMasterList = WarehouseMasterList;
                    //ResponseData.ResponseDynamicData = WarehouseMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Warehouse Master");
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
