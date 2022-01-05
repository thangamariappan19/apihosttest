using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.PriceTypeMasterResponse;
using EasyBizRequest.Masters.PriceTypeRequest;
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
    public class PriceTypeDAL : BasePriceTypeDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SavePriceTypeRequest)RequestObj;
            var ResponseData = new SavePriceTypeResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertPriceTypeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@PriceTypeID", RequestData.PriceTypesRecord.ID);
                _CommandObj.Parameters.AddWithValue("@PriceTypeCode", RequestData.PriceTypesRecord.PriceCode);
                _CommandObj.Parameters.AddWithValue("@PriceTypeName", RequestData.PriceTypesRecord.PriceName);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PriceTypesRecord.Remarks);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.PriceTypesRecord.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PriceTypesRecord.Active);
                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Price Type");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price Type ");
                    ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                    //ResponseData.IDs = ID2.Value.ToString();
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
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
            var RequestData = (UpdatePriceTypeRequest)RequestObj;
            var ResponseData = new UpdatePriceTypeResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdatePriceTypeMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.PriceTypeData.ID);
                _CommandObj.Parameters.AddWithValue("@PriceTypeCode", RequestData.PriceTypeData.PriceCode);
                _CommandObj.Parameters.AddWithValue("@PriceTypeName", RequestData.PriceTypeData.PriceName);
                _CommandObj.Parameters.AddWithValue("@UpdateBy", RequestData.PriceTypeData.UpdateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.PriceTypeData.SCN);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PriceTypeData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PriceTypeData.Active);


                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Price Type");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price Type");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price Type");
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
            var RequestData = (DeletePriceTypeRequest)RequestObj;
            var ResponseData = new DeletePriceTypeResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Delete from PriceTypeMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Price Type");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Price Type");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SelectByIDPriceTypeRequest)RequestObj;
            var ResponseData = new SelectByIDPriceTypeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from PriceTypeMaster with(NoLock)  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceTypeMaster = new PriceTypeMasterTypes();
                        objPriceTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceTypeMaster.PriceCode = objReader["PriceTypeCode"].ToString();
                        objPriceTypeMaster.PriceName = objReader["PriceTypeName"].ToString();


                        objPriceTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPriceTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceTypeMaster.Remarks = objReader["Remarks"].ToString();

                        ResponseData.PriceTypeMasterData = objPriceTypeMaster;
                        ResponseData.ResponseDynamicData = objPriceTypeMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Type");
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
            var PriceTypeMaster = new List<PriceTypeMasterTypes>();
            var RequestData = (SelectAllPriceTypeRequest)RequestObj;
            var ResponseData = new SelectAllPriceTypeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from PriceTypeMaster";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceTypeMaster = new PriceTypeMasterTypes();
                        objPriceTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceTypeMaster.PriceCode = objReader["PriceTypeCode"].ToString();
                        objPriceTypeMaster.PriceName = objReader["PriceTypeName"].ToString();


                        objPriceTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPriceTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceTypeMaster.Remarks = objReader["Remarks"].ToString();


                        PriceTypeMaster.Add(objPriceTypeMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceTypeMasterList = PriceTypeMaster;
                    ResponseData.ResponseDynamicData = PriceTypeMaster;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Type Master");
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectAllPriceTypeResponse API_SelectALL(SelectAllPriceTypeRequest requestData)
        {
            var PriceTypeMaster = new List<PriceTypeMasterTypes>();
            var RequestData = (SelectAllPriceTypeRequest)requestData;
            var ResponseData = new SelectAllPriceTypeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //string sSql = "Select * from PriceTypeMaster";
                string sSql = "Select ID, PriceTypeCode, PriceTypeName, Remarks, Active, RC.TOTAL_CNT [RecordCount] " +
                    "from PriceTypeMaster with(NoLock) " +
                    "LEFT JOIN(Select  count(PTM.ID) As TOTAL_CNT From PriceTypeMaster PTM with(NoLock) " +
                     "where PTM.Active = " + RequestData.IsActive + " " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or PTM.PriceTypeCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "or PTM.PriceTypeName like isnull('%" + RequestData.SearchString + "%','') " +
                        "or PTM.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                    "where Active = " + RequestData.IsActive + " " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or PriceTypeCode like isnull('%" + RequestData.SearchString + "%','') " +
                        "or PriceTypeName like isnull('%" + RequestData.SearchString + "%','') " +
                        "or Remarks like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objPriceTypeMaster = new PriceTypeMasterTypes();
                        objPriceTypeMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceTypeMaster.PriceCode = objReader["PriceTypeCode"].ToString();
                        objPriceTypeMaster.PriceName = objReader["PriceTypeName"].ToString();
                        //objPriceTypeMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPriceTypeMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPriceTypeMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPriceTypeMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPriceTypeMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPriceTypeMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceTypeMaster.Remarks = objReader["Remarks"].ToString();


                        PriceTypeMaster.Add(objPriceTypeMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.PriceTypeMasterList = PriceTypeMaster;
                    ResponseData.ResponseDynamicData = PriceTypeMaster;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price Type Master");
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
