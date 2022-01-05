using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BinRequest;
using EasyBizResponse.Masters.BinMasterRespose;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest;
using EasyBizResponse;

namespace MsSqlDAL.Masters
{
    public class BinLevelMasterDAL : BaseBinLevelMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveBinLevelMasterRequest)RequestObj;
            var ResponseData = new SaveBinLevelResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateBinLevelMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var BinLevelMaster = _CommandObj.Parameters.Add("@BinLevelMaster", SqlDbType.Xml);
                BinLevelMaster.Direction = ParameterDirection.Input;
                BinLevelMaster.Value = BinConfigMasterXML(RequestData.BinLevelMasterRecord.BinLevelMasterList , RequestData.BinLevelMasterRecord.StoreID);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.NVarChar,50);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Bin Config Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Bin Config Master");
                }
                else
                {
                    ResponseData.DisplayMessage = Convert.ToString(StatusMsg.Value);
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Bin Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public string BinConfigMasterXML(List<BinLevelMasterTypes> BinConfigMasterList , int StoreID)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (BinLevelMasterTypes objBinConfigMasterDetails in BinConfigMasterList)
            {
                sSql.Append("<BinLevelMaster>");
                sSql.Append("<ID>" + (objBinConfigMasterDetails.ID) + "</ID>");
                sSql.Append("<StoreID>" + StoreID + "</StoreID>");
                sSql.Append("<StoreCode>" + (objBinConfigMasterDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<LevelNo>" + objBinConfigMasterDetails.LevelNo + "</LevelNo>");
                sSql.Append("<LevelName>" + objBinConfigMasterDetails.LevelName + "</LevelName>");
                sSql.Append("<Active>" + objBinConfigMasterDetails.Active + "</Active>");
                sSql.Append("<CreateBy>" + objBinConfigMasterDetails.CreateBy + "</CreateBy>");
                sSql.Append("<EnableBin>" + objBinConfigMasterDetails.EnableBin + "</EnableBin>");
                sSql.Append("</BinLevelMaster>");
            }
            return sSql.ToString();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var BinMasterList = new List<BinLevelMasterTypes>();
            var RequestData = (SelectByIDBinMasterRequest)RequestObj;
            var ResponseData = new SelectAllBinConfigMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from AgentMaster with(NoLock) where Active='{0}'";
                string sSql = "Select * from StoreBinConfig Where StoreID = "+RequestData.ID;



                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBin = new BinLevelMasterTypes();
                        objBin.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBin.LevelNo = objReader["LevelNo"] != DBNull.Value ? Convert.ToInt32(objReader["LevelNo"]) : 0;
                        objBin.LevelName = Convert.ToString(objReader["LevelName"]);
                        objBin.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objBin.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        //objAgent.Active = Convert.ToString(objReader["Active"]);
                        /*objBin.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objBin.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objBin.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objBin.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;*/
                        //objAgent.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objBin.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objBin.EnableBin = objReader["EnableBin"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableBin"]) : false;
                        BinMasterList.Add(objBin);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BinConfigMasterList = BinMasterList;

                    //ResponseData.ResponseDynamicData = AgentList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Agent Master");
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

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
