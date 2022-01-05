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
using System.Collections.Generic;
using System.Configuration;

namespace MsSqlDAL.SyncSettings
{
   public class MasterDataSyncDAL : MasterDataSyncAbsDAL
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
        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var RequestData = (MasterDataSyncRequest)RequestObj;
            var ResponseData = new MasterDataSyncResponse();
            var MDList = new List<MasterDataSyncDBType>();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = GetMainconnString();
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Sp_MasterDataSync", _ConnectionObj);
                //_CommandObj.CommandTimeout = 300000;
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter Mode = _CommandObj.Parameters.Add("@Mode", SqlDbType.VarChar);
                Mode.Direction = ParameterDirection.Input;
                Mode.Value = RequestData.PriceUP.Mode;

                SqlParameter BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                BrandID.Direction = ParameterDirection.Input;
                BrandID.Value = RequestData.PriceUP.BrandID;

                SqlParameter Skucode = _CommandObj.Parameters.Add("@SKUCode", SqlDbType.VarChar);
                Skucode.Direction = ParameterDirection.Input;
                Skucode.Value = RequestData.PriceUP.SkuCode;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.PriceUP.StoreID;

                SqlParameter INVOICE = _CommandObj.Parameters.Add("@INVOICE", SqlDbType.VarChar);
                INVOICE.Direction = ParameterDirection.Input;
                INVOICE.Value = RequestData.PriceUP.INVOICE;

                SqlParameter UserName = _CommandObj.Parameters.Add("@UserName", SqlDbType.VarChar);
                UserName.Direction = ParameterDirection.Input;
                UserName.Value = RequestData.PriceUP.UserName;

                SqlParameter errorMsg = _CommandObj.Parameters.Add("@errorMsg", SqlDbType.VarChar, 500);
                errorMsg.Direction = ParameterDirection.Output;

               _CommandObj.ExecuteNonQuery();

                ResponseData.DisplayMessage = errorMsg.Value.ToString();
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
       public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
       {

           throw new NotImplementedException();
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

      
    }
}
