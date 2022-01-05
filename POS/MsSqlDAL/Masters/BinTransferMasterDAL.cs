using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Common;
using MsSqlDAL.Common;
using ResourceStrings;
using System.Data;
using System.Data.SqlClient;
using EasyBizRequest.Masters.BinTransferMasterRequest;
using EasyBizResponse.Masters.BinTransferResponse;

namespace MsSqlDAL.Masters
{
    public class BinTransferMasterDAL : BaseBinTransferMasterDAL
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
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var BinTransferMasterList = new List<BinLogTypes>();
            var RequestData = (SelectBinDetailsBySKUCodeRequest)RequestObj;
            var ResponseData = new SelectBinDetailsBySKUCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "select * from BinLogDetails where BinCode = '" + RequestData.FromBin + "'" + " AND (Barcode = '" + RequestData.SKUCode + "' or SKUCode = '" + RequestData.SKUCode + "' or RFID = '" + RequestData.SKUCode + "')";

                
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBin = new BinLogTypes();
                        objBin.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBin.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objBin.BarCode = Convert.ToString(objReader["BarCode"]);
                        objBin.RFID = Convert.ToString(objReader["RFID"]);
                        objBin.StoreID = Convert.ToInt32(objReader["StoreID"]);
                        objBin.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objBin.Quantity = Convert.ToInt32(objReader["Quantity"]);
                        objBin.BinID = Convert.ToInt32(objReader["BinID"]);
                        objBin.BinCode = Convert.ToString(objReader["BinCode"]);
                        objBin.BinSubLevelCode = Convert.ToString(objReader["BinSubLevelCode"]);
                        objBin.Status = Convert.ToString(objReader["Status"]);
                        objBin.Remarks = Convert.ToString(objReader["Remarks"]);
                        objBin.Active = Convert.ToBoolean(objReader["Active"]);
                        BinTransferMasterList.Add(objBin);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BinDetailsList = BinTransferMasterList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Bin Log");
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

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveBinTransferRequest)RequestObj;
            var ResponseData = new SaveBinTransferResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdateBinLogMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var BinLevelDetails = _CommandObj.Parameters.Add("@BinTransferDetails", SqlDbType.Xml);
                BinLevelDetails.Direction = ParameterDirection.Input;
                BinLevelDetails.Value = BinLogDetailXML(RequestData.BinLogList);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Bin Transfer");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Bin Transfer");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Transfer");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Transfer");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string BinLogDetailXML(List<BinLogTypes> BinLogDetaiList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (BinLogTypes objBinLogDetail in BinLogDetaiList)
            {
                sSql.Append("<BinLogDetailsData>");
                sSql.Append("<ID>" + objBinLogDetail.ID + "</ID>");
                sSql.Append("<SKUCode>" + objBinLogDetail.SKUCode + "</SKUCode>");
                sSql.Append("<BarCode>" + objBinLogDetail.BarCode + "</BarCode>");
                sSql.Append("<RFID>" + objBinLogDetail.RFID + "</RFID>");
                sSql.Append("<Quantity>" + objBinLogDetail.Quantity + "</Quantity>");
                sSql.Append("<Status>" + objBinLogDetail.Status + "</Status>");
                sSql.Append("<Remarks>" + objBinLogDetail.Remarks + "</Remarks>");
                sSql.Append("<Active>" + objBinLogDetail.Active + "</Active>");
                sSql.Append("<StoreID>" + objBinLogDetail.StoreID + "</StoreID>");
                sSql.Append("<StoreCode>" + objBinLogDetail.StoreCode + "</StoreCode>");
                sSql.Append("<CreateBy>" + objBinLogDetail.CreateBy + "</CreateBy>");
                sSql.Append("<UpdateBy>" + objBinLogDetail.UpdateBy + "</UpdateBy>");
                sSql.Append("<BinID>" + objBinLogDetail.BinID + "</BinID>");
                sSql.Append("<BinCode>" + objBinLogDetail.BinCode + "</BinCode>");
                sSql.Append("<BinSubLevelCode>" + objBinLogDetail.BinSubLevelCode + "</BinSubLevelCode>");

                sSql.Append("</BinLogDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }
        public override SaveBinTransferResponse UpdateRecord(SaveBinTransferRequest objRequest)
        {
            var RequestData = (SaveBinTransferRequest)objRequest;
            var ResponseData = new SaveBinTransferResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdateBinLogMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var BinLevelDetails = _CommandObj.Parameters.Add("@BinTransferDetails", SqlDbType.Xml);
                BinLevelDetails.Direction = ParameterDirection.Input;
                BinLevelDetails.Value = BinLogDetailXML(RequestData.BinLogList);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Bin Transfer");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Bin Transfer");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Transfer");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Bin Transfer");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
    }
}
