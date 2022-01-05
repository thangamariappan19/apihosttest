using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
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
    public class PosMasterDAL : BasePosMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override SelectPosMasterLookUpResponse SelectPosMasterLookUp(SelectPosMasterLookUpRequest RequestObj)
        {
            var PosMasterList = new List<PosMaster>();
            var RequestData = (SelectPosMasterLookUpRequest)RequestObj;
            var ResponseData = new SelectPosMasterLookUpResponse();
         
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                if(RequestData.StoreID != 0)
                {
                    sQuery = "Select ID,PosName,PosCode from PosMaster with(NoLock) where Active='True' and StoreID='" + RequestData.StoreID + "'  ";
                }
                else
                {
                    sQuery = "Select ID,PosName,PosCode from PosMaster with(NoLock) where Active='True'";
                }
                
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPosMaster = new PosMaster();
                        objPosMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objPosMaster.PosName = Convert.ToString(objReader["PosName"]);
                        objPosMaster.PosCode = Convert.ToString(objReader["PosCode"]);
                        PosMasterList.Add(objPosMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PosMasterList = PosMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pos Master");
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

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SavePosMasterRequest)RequestObj;
            var ResponseData = new SavePosMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertPosMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var POSID = _CommandObj.Parameters.Add("@PosID", SqlDbType.Int);
                POSID.Direction = ParameterDirection.Input;
                POSID.Value = RequestData.PosMasterData.POSID;

                var PosCode = _CommandObj.Parameters.Add("@PosCode", SqlDbType.NVarChar);
                PosCode.Direction = ParameterDirection.Input;
                PosCode.Value = RequestData.PosMasterData.PosCode;

                var PosName = _CommandObj.Parameters.Add("@PosName", SqlDbType.NVarChar);
                PosName.Direction = ParameterDirection.Input;
                PosName.Value = RequestData.PosMasterData.PosName;               

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.PosMasterData.CountryID;

                var StoreGroupID = _CommandObj.Parameters.Add("@StoreGroupID", SqlDbType.Int);
                StoreGroupID.Direction = ParameterDirection.Input;
                StoreGroupID.Value = RequestData.PosMasterData.StoreGroupID;

                var StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.PosMasterData.StoreID;

                var CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.PosMasterData.CountryCode;

                var StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.PosMasterData.StoreCode;

                var StoreGroupCode = _CommandObj.Parameters.Add("@StoreGroupCode", SqlDbType.NVarChar);
                StoreGroupCode.Direction = ParameterDirection.Input;
                StoreGroupCode.Value = RequestData.PosMasterData.StoreGroupCode;

                var DefaultCustomerCode = _CommandObj.Parameters.Add("@DefaultCustomerCode", SqlDbType.NVarChar);
                DefaultCustomerCode.Direction = ParameterDirection.Input;
                DefaultCustomerCode.Value = RequestData.PosMasterData.DefaultCustomerCode;


                var DefaultCustomerID = _CommandObj.Parameters.Add("@DefaultCustomerID", SqlDbType.Int);
                DefaultCustomerID.Direction = ParameterDirection.Input;
                DefaultCustomerID.Value = RequestData.PosMasterData.DefaultCustomer;

                var PoleDisplayPort = _CommandObj.Parameters.Add("@PoleDisplayPort", SqlDbType.NVarChar);
                PoleDisplayPort.Direction = ParameterDirection.Input;
                PoleDisplayPort.Value = RequestData.PosMasterData.PoleDisplayPort;

                var DisplayLineMsgOne = _CommandObj.Parameters.Add("@DisplayLineMsgOne", SqlDbType.NVarChar);
                DisplayLineMsgOne.Direction = ParameterDirection.Input;
                DisplayLineMsgOne.Value = RequestData.PosMasterData.DisplayLineMsgOne;

                var DisplayLineMsgTwo = _CommandObj.Parameters.Add("@DisplayLineMsgTwo", SqlDbType.NVarChar);
                DisplayLineMsgTwo.Direction = ParameterDirection.Input;
                DisplayLineMsgTwo.Value = RequestData.PosMasterData.DisplayLineMsgTwo;

                var DiskID = _CommandObj.Parameters.Add("@DiskID", SqlDbType.VarChar);
                DiskID.Direction = ParameterDirection.Input;
                DiskID.Value = RequestData.PosMasterData.DiskID;

                var CPUID = _CommandObj.Parameters.Add("@CPUID", SqlDbType.VarChar);
                CPUID.Direction = ParameterDirection.Input;
                CPUID.Value = RequestData.PosMasterData.CPUID;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.PosMasterData.Active;
              

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.PosMasterData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                var PrinterDeviceName = _CommandObj.Parameters.Add("@PrinterDeviceName", SqlDbType.VarChar);
                PrinterDeviceName.Direction = ParameterDirection.Input;
                PrinterDeviceName.Value = RequestData.PosMasterData.PrinterDeviceName;  


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Pos Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Pos Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Pos Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Pos Master");
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
            var RequestData = (UpdatePosMasterRequest)RequestObj;
            var ResponseData = new UpdatePosMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdatePosMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                //var PosID = _CommandObj.Parameters.Add("@PosID", SqlDbType.Int);
                //PosID.Direction = ParameterDirection.Input;
                //PosID.Value = RequestData.PosMasterData.ID;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.PosMasterData.ID;

                var PosCode = _CommandObj.Parameters.Add("@PosCode", SqlDbType.NVarChar);
                PosCode.Direction = ParameterDirection.Input;
                PosCode.Value = RequestData.PosMasterData.PosCode;

                var PosName = _CommandObj.Parameters.Add("@PosName", SqlDbType.NVarChar);
                PosName.Direction = ParameterDirection.Input;
                PosName.Value = RequestData.PosMasterData.PosName;               

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.PosMasterData.CountryID;

                var StoreGroupID = _CommandObj.Parameters.Add("@StoreGroupID", SqlDbType.Int);
                StoreGroupID.Direction = ParameterDirection.Input;
                StoreGroupID.Value = RequestData.PosMasterData.StoreGroupID;

                var StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.PosMasterData.StoreID;

                var DefaultCustomerID = _CommandObj.Parameters.Add("@DefaultCustomerID", SqlDbType.Int);
                DefaultCustomerID.Direction = ParameterDirection.Input;
                DefaultCustomerID.Value = RequestData.PosMasterData.DefaultCustomer;

                var CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.PosMasterData.CountryCode;

                var StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.PosMasterData.StoreCode;

                var StoreGroupCode = _CommandObj.Parameters.Add("@StoreGroupCode", SqlDbType.NVarChar);
                StoreGroupCode.Direction = ParameterDirection.Input;
                StoreGroupCode.Value = RequestData.PosMasterData.StoreGroupCode;

                var DefaultCustomerCode = _CommandObj.Parameters.Add("@DefaultCustomerCode", SqlDbType.NVarChar);
                DefaultCustomerCode.Direction = ParameterDirection.Input;
                DefaultCustomerCode.Value = RequestData.PosMasterData.DefaultCustomerCode;

                var PoleDisplayPort = _CommandObj.Parameters.Add("@PoleDisplayPort", SqlDbType.NVarChar);
                PoleDisplayPort.Direction = ParameterDirection.Input;
                PoleDisplayPort.Value = RequestData.PosMasterData.PoleDisplayPort;

                var DisplayLineMsgOne = _CommandObj.Parameters.Add("@DisplayLineMsgOne", SqlDbType.NVarChar);
                DisplayLineMsgOne.Direction = ParameterDirection.Input;
                DisplayLineMsgOne.Value = RequestData.PosMasterData.DisplayLineMsgOne;

                var DisplayLineMsgTwo = _CommandObj.Parameters.Add("@DisplayLineMsgTwo", SqlDbType.NVarChar);
                DisplayLineMsgTwo.Direction = ParameterDirection.Input;
                DisplayLineMsgTwo.Value = RequestData.PosMasterData.DisplayLineMsgTwo;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.PosMasterData.Active;

                var DiskID = _CommandObj.Parameters.Add("@DiskID", SqlDbType.VarChar);
                DiskID.Direction = ParameterDirection.Input;
                DiskID.Value = RequestData.PosMasterData.DiskID;

                var CPUID = _CommandObj.Parameters.Add("@CPUID", SqlDbType.VarChar);
                CPUID.Direction = ParameterDirection.Input;
                CPUID.Value = RequestData.PosMasterData.CPUID;         


                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.PosMasterData.UpdateBy;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.PosMasterData.SCN;

                var PrinterDeviceName = _CommandObj.Parameters.Add("@PrinterDeviceName", SqlDbType.VarChar);
                PrinterDeviceName.Direction = ParameterDirection.Input;
                PrinterDeviceName.Value = RequestData.PosMasterData.PrinterDeviceName;  


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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Pos Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Pos Master");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Pos Master");
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
            var WarehouseMasterRecord = new PosMaster();

            var RequestData = (DeletePosMasterRequest)RequestObj;
            var ResponseData = new DeletePosMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                string sSql = "Delete from PosMaster  where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);   
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Pos Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Pos Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var PosMasterRecord = new PosMaster();
            var RequestData = (SelectByIDPosMasterRequest)RequestObj;
            var ResponseData = new SelectByIDPosMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                string sSql = "Select * from PosMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);   
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPosMaster = new PosMaster();
                        objPosMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPosMaster.PosCode = Convert.ToString(objReader["PosCode"]);
                        objPosMaster.PosName = Convert.ToString(objReader["PosName"]);
                        objPosMaster.StoreID =objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) :0;                        
                        objPosMaster.CountryID =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objPosMaster.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                        objPosMaster.PrinterDeviceName = Convert.ToString(objReader["PrinterDeviceName"]);
                        objPosMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPosMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPosMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPosMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPosMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPosMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPosMaster.DefaultCustomer = objReader["DefaultCustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCustomerID"]) : 0;
                        objPosMaster.PoleDisplayPort = Convert.ToString(objReader["PoleDisplayPort"]);
                        objPosMaster.DisplayLineMsgOne = Convert.ToString(objReader["DisplayLineMsgOne"]);
                        objPosMaster.DisplayLineMsgTwo = Convert.ToString(objReader["DisplayLineMsgTwo"]);
                        objPosMaster.DiskID = Convert.ToString(objReader["DiskID"]);
                        objPosMaster.CPUID = Convert.ToString(objReader["CPUID"]);

                        objPosMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objPosMaster.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objPosMaster.StoreGroupCode = Convert.ToString(objReader["StoreGroupCode"]);
                        objPosMaster.DefaultCustomerCode = Convert.ToString(objReader["DefaultCustomerCode"]);

                        ResponseData.PosMasterRecord = objPosMaster;
                        ResponseData.ResponseDynamicData = objPosMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pos Master");
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
            var PosMasterList = new List<PosMaster>();
            var RequestData = (SelectAllPosMasterRequest)RequestObj;
            var ResponseData = new SelectAllPosMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                sSql.Append("select PM. *,SGM.StoreGroupName,CM.CountryName,SM.StoreName   from PosMaster PM  ");
                sSql.Append("left outer join StoreGroupMaster SGM on SGM.ID=PM.StoreGroupID   ");
                sSql.Append("left outer join CountryMaster CM on CM.ID=PM.CountryID   ");
                sSql.Append("left outer join StoreMaster SM on SM.ID=PM.StoreID   ");

                if(RequestData.StoreID > 0)
                {
                    sSql.Append("where PM.StoreID=" + RequestData.StoreID);
                }
                if(RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql.Append(" and isnull(PM.DiskID,'') ='' and isnull(PM.CPUID,'')=''");
                }

                sSql.Append(" order by PM.id  asc");
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                   sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //_CommandObj = new SqlCommand("Select * from PosMaster with(NoLock) where Active='" + RequestData.ShowInActiveRecords + "'", _ConnectionObj);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPosMaster = new PosMaster();
                        objPosMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPosMaster.PosCode = Convert.ToString(objReader["PosCode"]);
                        objPosMaster.PosName = Convert.ToString(objReader["PosName"]);
                        objPosMaster.StoreID =objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) :0;                       
                        objPosMaster.CountryID =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objPosMaster.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                        objPosMaster.PrinterDeviceName = Convert.ToString(objReader["PrinterDeviceName"]);
                        objPosMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPosMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPosMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPosMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPosMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPosMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPosMaster.DefaultCustomer = objReader["DefaultCustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCustomerID"]) : 0;
                        objPosMaster.PoleDisplayPort = Convert.ToString(objReader["PoleDisplayPort"]);
                        objPosMaster.DisplayLineMsgOne = Convert.ToString(objReader["DisplayLineMsgOne"]);
                        objPosMaster.DisplayLineMsgTwo = Convert.ToString(objReader["DisplayLineMsgTwo"]);
                        objPosMaster.DiskID = Convert.ToString(objReader["DiskID"]);
                        objPosMaster.CPUID = Convert.ToString(objReader["CPUID"]);
                        //   ResponseData.PosMasterRecord = objPosMaster;


                        objPosMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objPosMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objPosMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objPosMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);

                        PosMasterList.Add(objPosMaster);                      
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PosMasterList = PosMasterList;
                    ResponseData.ResponseDynamicData = PosMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pos Master");
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

        public override SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest RequestObj)
        {
            var StoreMasterList = new List<StoreMaster>();


            SelectStoreMasterLookUpRequest RequestData = (SelectStoreMasterLookUpRequest) RequestObj;

            SelectStoreMasterLookUpResponse ResponseData = new SelectStoreMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock) where Active='True' and StoreGroupID='" + RequestData.StoreGroupID + "' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();
                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);      
                        StoreMasterList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreMasterList = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pos Master");
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
        public override SelectStoreGroupLookUpResponse SelectStoreGroupLookUp(SelectStoreGroupLookUpRequest RequestObj)
        {
            var StoreGroupMasterList = new List<StoreGroupMaster>();
            
            var ResponseData = new SelectStoreGroupLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestObj.ConnectionString;
                _RequestFrom = RequestObj.RequestFrom;
                
                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                sQuery = "Select distinct b.ID,b.StoreGroupName,b.StoreGroupCode from StoreMaster  a join StoreGroupMaster b on b.ID=a.StoreGroupID where  a.CountryID='" + RequestObj.CountryID + "'  ";                              


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

        public override SelectStoreMasterLookUpResponse SelectStoreBasedOnStoreGroupandCountryMasterLookUp(SelectStoreMasterLookUpRequest RequestObj)
        {
            var StoreMasterList = new List<StoreMaster>();


            SelectStoreMasterLookUpRequest RequestData = (SelectStoreMasterLookUpRequest)RequestObj;

            SelectStoreMasterLookUpResponse ResponseData = new SelectStoreMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock) where Active='True' and StoreGroupID='" + RequestData.StoreGroupID + "' and CountryID='" + RequestData.CountryID + "'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();
                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        StoreMasterList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreMasterList = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pos Master");
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

        public override SelectAllPosMasterResponse API_SelectALL(SelectAllPosMasterRequest requestData)
        {

            var PosMasterList = new List<PosMaster>();
            var RequestData = (SelectAllPosMasterRequest)requestData;
            var ResponseData = new SelectAllPosMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                //sSql.Append("select PM. *,SGM.StoreGroupName,CM.CountryName,SM.StoreName   from PosMaster PM  ");
                //sSql.Append("left outer join StoreGroupMaster SGM on SGM.ID=PM.StoreGroupID   ");
                //sSql.Append("left outer join CountryMaster CM on CM.ID=PM.CountryID   ");
                //sSql.Append("left outer join StoreMaster SM on SM.ID=PM.StoreID   ");

                //if (RequestData.StoreID > 0)
                //{
                //    sSql.Append("where PM.StoreID=" + RequestData.StoreID);
                //}
                //if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                //{
                //    sSql.Append(" and isnull(PM.DiskID,'') ='' and isnull(PM.CPUID,'')=''");
                //}

                //sSql.Append(" order by PM.id  asc");

                sSql.Append("select PM.ID, PM.PosCode, PM.PosName, CM.CountryName, SGM.StoreGroupName, SM.StoreName, PM.Active, RC.TOTAL_CNT [RecordCount]  from PosMaster PM with(NoLock) ");
                sSql.Append("left outer join StoreGroupMaster SGM with(NoLock) on SGM.ID=PM.StoreGroupID   ");
                sSql.Append("left outer join CountryMaster CM with(NoLock) on CM.ID=PM.CountryID   ");
                sSql.Append("left outer join StoreMaster SM with(NoLock) on SM.ID=PM.StoreID   ");
               
                sSql.Append(" LEFT JOIN (Select count(CM1.ID) As TOTAL_CNT from PosMaster PM1 with(NoLock) ");
                sSql.Append(" left outer join StoreGroupMaster SGM1 with(NoLock) on SGM1.ID=PM1.StoreGroupID ");

                sSql.Append("left outer join CountryMaster CM1 with(NoLock) on CM1.ID=PM1.CountryID ");
                sSql.Append("left outer join StoreMaster SM1 with(NoLock) on SM1.ID=PM1.StoreID  ");

                
                
                sSql.Append("where PM1.Active = " + RequestData.IsActive + " " );
                if (RequestData.StoreID > 0)
                {
                    sSql.Append(" and PM1.StoreID=" + RequestData.StoreID);
                }
                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql.Append(" and isnull(PM1.DiskID,'') ='' and isnull(PM1.CPUID,'')=''");
                }

                sSql.Append(" and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or PM1.PosCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or PM1.PosName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CM1.CountryName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SGM1.StoreGroupName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM1.StoreName like isnull('%" + RequestData.SearchString + "%',''))) AS RC ON 1 = 1 ");
                sSql.Append("where PM.Active = " + RequestData.IsActive + "");

                if (RequestData.StoreID > 0)
                {
                    sSql.Append(" and PM.StoreID=" + RequestData.StoreID);
                }
                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql.Append(" and isnull(PM.DiskID,'') ='' and isnull(PM.CPUID,'')=''");
                }

                

                sSql.Append(" and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or PM.PosCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or PM.PosName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CM.CountryName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SGM.StoreGroupName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM.StoreName like isnull('%" + RequestData.SearchString + "%','')) ");
                sSql.Append("order by PM.ID  asc ");
                sSql.Append("offset " + RequestData.Offset + " rows ");
                sSql.Append("fetch first " + RequestData.Limit + " rows only");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //_CommandObj = new SqlCommand("Select * from PosMaster with(NoLock) where Active='" + RequestData.ShowInActiveRecords + "'", _ConnectionObj);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPosMaster = new PosMaster();
                        objPosMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPosMaster.PosCode = Convert.ToString(objReader["PosCode"]);
                        objPosMaster.PosName = Convert.ToString(objReader["PosName"]);
                        //objPosMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        //objPosMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objPosMaster.StoreGroupID = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                        //objPosMaster.PrinterDeviceName = Convert.ToString(objReader["PrinterDeviceName"]);
                        //objPosMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPosMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPosMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPosMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPosMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPosMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objPosMaster.DefaultCustomer = objReader["DefaultCustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCustomerID"]) : 0;
                        //objPosMaster.PoleDisplayPort = Convert.ToString(objReader["PoleDisplayPort"]);
                        //objPosMaster.DisplayLineMsgOne = Convert.ToString(objReader["DisplayLineMsgOne"]);
                        //objPosMaster.DisplayLineMsgTwo = Convert.ToString(objReader["DisplayLineMsgTwo"]);
                        //objPosMaster.DiskID = Convert.ToString(objReader["DiskID"]);
                        //objPosMaster.CPUID = Convert.ToString(objReader["CPUID"]);
                        //   ResponseData.PosMasterRecord = objPosMaster;


                        objPosMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objPosMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objPosMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        //objPosMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);

                        PosMasterList.Add(objPosMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PosMasterList = PosMasterList;
                    //ResponseData.ResponseDynamicData = PosMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Pos Master");
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
