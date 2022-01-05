using CommonRoutines;
using EasyBizAbsDAL.SyncSettings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizRequest;
using EasyBizRequest.Common;
using EasyBizRequest.SyncSettings;
using EasyBizResponse;
using EasyBizResponse.Common;
using EasyBizResponse.SyncSettings;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Configuration;

namespace MsSqlDAL.SyncSettings
{
    public class ManualMasterSyncDAL : ManualMasterSyncAbsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;


        private string GetMainconnString()
        {
            //To get the mainserver connection string
            string ConString = string.Empty;
            try
            {
                string AppType = FSRetailResource.ResourceManager.GetString("ApplicationType");
                string FilePath = System.Windows.Forms.Application.StartupPath + "\\" + AppType + "SetUp.xml";

                if (File.Exists(FilePath))
                {
                    DataSet objDataTable = new DataSet();
                    objDataTable.ReadXml(FilePath);
                    if (objDataTable.Tables.Count > 0 && objDataTable.Tables[0].Rows.Count > 0)
                    {
                        ConString = Convert.ToString(objDataTable.Tables[0].Rows[0]["MainServerConnection"]);

                    }
                }
                else
                {
                    ConString = ConfigurationManager.AppSettings["ConString"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ConString;
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }


        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {

            throw new NotImplementedException();
        }
        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            var RequestData = (ManualMasterSyncRequest)RequestObj;
            var ResponseData = new ManualMasterSyncResponse();
            SqlDataReader objReader;
            string StoreDBConnection = null;
            string EncyptConnection = null;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = GetMainconnString();
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select * from DBConnections where StoreID = " + RequestData.SyncSKU.StoreID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StoreDBConnection = objReader["ConnString_Integration"] != DBNull.Value ? Convert.ToString(objReader["ConnString_Integration"]) : string.Empty;
                        EncyptConnection = objReader["ConnectionString"] != DBNull.Value ? Convert.ToString(objReader["ConnectionString"]) : string.Empty;
                    }
                }
                sqlCommon.CloseConnection(_ConnectionObj);
                _ConnectionString = EncyptConnection;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if(RequestData.SyncSKU.Module == "Generate SKU")
                {
                    _CommandObj = new SqlCommand("SP_SKUInsert", _ConnectionObj);
                }
                else if(RequestData.SyncSKU.Module== "Generate Price")
                {
                    _CommandObj = new SqlCommand("SP_InsertStylePricing", _ConnectionObj);
                }
                
                _CommandObj.CommandTimeout = 30000000;
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.VarChar);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.SyncSKU.StoreID;

                /*SqlParameter errorMsg = _CommandObj.Parameters.Add("@errorMsg", SqlDbType.VarChar, 500);
                errorMsg.Direction = ParameterDirection.Output;*/

                _CommandObj.ExecuteNonQuery();

                //ResponseData.DisplayMessage = errorMsg.Value.ToString();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;


            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Master Data Sync");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
