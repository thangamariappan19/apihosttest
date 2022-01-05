using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.Brand_Response;
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
    public class BrandMasterDAL : BaseBrandMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlTransaction transaction = null;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;       

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveBrandRequest)RequestObj;
            var ResponseData = new SaveBrandResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertBrandMaster", _ConnectionObj, transaction);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                BrandID.Direction = ParameterDirection.Input;
                BrandID.Value = RequestData.BrandRecord.ID;

                var BrandCode = _CommandObj.Parameters.Add("@BrandCode", SqlDbType.NVarChar);
                BrandCode.Direction = ParameterDirection.Input;
                BrandCode.Value = RequestData.BrandRecord.BrandCode;

                var BrandName = _CommandObj.Parameters.Add("@BrandName", SqlDbType.NVarChar);
                BrandName.Direction = ParameterDirection.Input;
                BrandName.Value = RequestData.BrandRecord.BrandName;

                var BrandLogo = _CommandObj.Parameters.Add("@BrandLogo", SqlDbType.NVarChar);
                BrandLogo.Direction = ParameterDirection.Input;
                BrandLogo.Value = RequestData.BrandRecord.BrandLogo;

                var ARBName = _CommandObj.Parameters.Add("@ARBName", SqlDbType.NVarChar);
                ARBName.Direction = ParameterDirection.Input;
                ARBName.Value = RequestData.BrandRecord.ARBName;

                var ShortDescriptionName = _CommandObj.Parameters.Add("@ShortDescriptionName", SqlDbType.NVarChar);
                ShortDescriptionName.Direction = ParameterDirection.Input;
                ShortDescriptionName.Value = RequestData.BrandRecord.ShortDescriptionName;

                var BrandType = _CommandObj.Parameters.Add("@BrandType", SqlDbType.NVarChar);
                BrandType.Direction = ParameterDirection.Input;
                BrandType.Value = RequestData.BrandRecord.BrandType;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.BrandRecord.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.BrandRecord.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.BrandRecord.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Brand");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Brand");
                  
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Brand");
                 
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Brand");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
                transaction.Rollback();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdateBrandRequest)RequestObj;
            var ResponseData = new UpdateBrandResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateBrandMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.BrandRecord.ID;

                var BrandCode = _CommandObj.Parameters.Add("@BrandCode", SqlDbType.VarChar);
                BrandCode.Direction = ParameterDirection.Input;
                BrandCode.Value = RequestData.BrandRecord.BrandCode;

                var BrandName = _CommandObj.Parameters.Add("@BrandName", SqlDbType.VarChar);
                BrandName.Direction = ParameterDirection.Input;
                BrandName.Value = RequestData.BrandRecord.BrandName;

                var BrandLogo = _CommandObj.Parameters.Add("@BrandLogo", SqlDbType.NVarChar);
                BrandLogo.Direction = ParameterDirection.Input;
                BrandLogo.Value = RequestData.BrandRecord.BrandLogo;

                var ARBName = _CommandObj.Parameters.Add("@ARBName", SqlDbType.NVarChar);
                ARBName.Direction = ParameterDirection.Input;
                ARBName.Value = RequestData.BrandRecord.ARBName;

                var ShortDescriptionName = _CommandObj.Parameters.Add("@ShortDescriptionName", SqlDbType.NVarChar);
                ShortDescriptionName.Direction = ParameterDirection.Input;
                ShortDescriptionName.Value = RequestData.BrandRecord.ShortDescriptionName;

                var BrandType = _CommandObj.Parameters.Add("@BrandType", SqlDbType.NVarChar);
                BrandType.Direction = ParameterDirection.Input;
                BrandType.Value = RequestData.BrandRecord.BrandType;

                var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.BrandRecord.Remarks;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.BrandRecord.Active;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.BrandRecord.UpdateBy;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.BrandRecord.SCN;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Brand");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Brand");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Brand");
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
            var BrandRecord = new BrandMaster();
            var RequestData = (DeleteBrandRequest)RequestObj;
            var ResponseData = new DeleteBrandResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "delete from BrandMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Brand");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Brand");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var BrandRecord = new BrandMaster();
            var RequestData = (SelectByBrandIDRequest)RequestObj;
            var ResponseData = new SelectByBrandIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from BrandMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandMaster = new BrandMaster();
                        objBrandMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandMaster.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrandMaster.BrandName = Convert.ToString(objReader["BrandName"]);
                        objBrandMaster.BrandLogo = Convert.ToString(objReader["BrandLogo"]);
                        objBrandMaster.ARBName = Convert.ToString(objReader["ARBName"]);
                        objBrandMaster.ShortDescriptionName = Convert.ToString(objReader["ShortDescriptionName"]);
                        objBrandMaster.BrandType = Convert.ToString(objReader["BrandType"]);
                        objBrandMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objBrandMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objBrandMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objBrandMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objBrandMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objBrandMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objBrandMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.BrandRecord = objBrandMaster;
                        ResponseData.IDs = objBrandMaster.ID.ToString();
                        ResponseData.ResponseDynamicData = objBrandMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand");

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
           var BrandList = new List<BrandMaster>();
           var RequestData = (SelectAllBrandRequest)RequestObj;
           var ResponseData = new SelectAllBrandResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //string sSql = "Select * from BrandMaster with(NoLock) where Active='{0}'";
                string sSql = "Select ID,BrandCode,BrandName,Active from BrandMaster order by ID desc ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrandMaster = new BrandMaster();
                        objBrandMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandMaster.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrandMaster.BrandName = Convert.ToString(objReader["BrandName"]);
                        //objBrandMaster.BrandLogo = Convert.ToString(objReader["BrandLogo"]);
                        //objBrandMaster.ARBName = Convert.ToString(objReader["ARBName"]);
                        //objBrandMaster.ShortDescriptionName = Convert.ToString(objReader["ShortDescriptionName"]);
                        //objBrandMaster.BrandType = Convert.ToString(objReader["BrandType"]);
                        //objBrandMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objBrandMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objBrandMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objBrandMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objBrandMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objBrandMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objBrandMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BrandList.Add(objBrandMaster);

                        
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BrandList = BrandList;
                    ResponseData.ResponseDynamicData = BrandList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Settings");
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
        public override SelectBrandLookUpResponse SelectBrandLookUp(SelectBrandLookUpRequest ObjRequest)
        {
            var BrandList = new List<BrandMaster>();
            var VendorGroupList = new List<VendorGroupMaster>();
            var RequestData = (SelectBrandLookUpRequest)ObjRequest;
            var ResponseData = new SelectBrandLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[BrandName],BrandCode,ShortDescriptionName,Active from BrandMaster with(NoLock)  where Active='true'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrand = new BrandMaster();
                        objBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrand.BrandID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrand.BrandName = Convert.ToString(objReader["BrandName"]);
                        objBrand.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrand.ShortDescriptionName = Convert.ToString(objReader["ShortDescriptionName"]);
                        objBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BrandList.Add(objBrand);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BrandList = BrandList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Master");
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


        public override SelectAllBrandResponse API_SelectALL(SelectAllBrandRequest requestData)
        {
            var BrandList = new List<BrandMaster>();
            var RequestData = (SelectAllBrandRequest)requestData;
            var ResponseData = new SelectAllBrandResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from BrandMaster with(NoLock) where Active='{0}'";

                //string sSql = "Select ID,BrandCode,BrandName,Active from BrandMaster order by ID desc ";

                string sSql = "Select ID, BrandCode, BrandName, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from BrandMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(BM.ID) As TOTAL_CNT From BrandMaster BM with(NoLock)" +
                    "where BM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or BM.BrandCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or BM.BrandName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or BrandCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or BrandName like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objBrandMaster = new BrandMaster();
                        objBrandMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrandMaster.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrandMaster.BrandName = Convert.ToString(objReader["BrandName"]);
                        //objBrandMaster.BrandLogo = Convert.ToString(objReader["BrandLogo"]);
                        //objBrandMaster.ARBName = Convert.ToString(objReader["ARBName"]);
                        //objBrandMaster.ShortDescriptionName = Convert.ToString(objReader["ShortDescriptionName"]);
                        //objBrandMaster.BrandType = Convert.ToString(objReader["BrandType"]);
                        //objBrandMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objBrandMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objBrandMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objBrandMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objBrandMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objBrandMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objBrandMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BrandList.Add(objBrandMaster);

                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;


                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BrandList = BrandList;
                    ResponseData.ResponseDynamicData = BrandList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Settings");
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

        public override SelectBrandLookUpResponse API_SelectBrandMasterLookUp(SelectBrandLookUpRequest requestData)
        {
            var BrandList = new List<BrandMaster>();
            var VendorGroupList = new List<VendorGroupMaster>();
            var RequestData = (SelectBrandLookUpRequest)requestData;
            var ResponseData = new SelectBrandLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID, [BrandName], BrandCode, ShortDescriptionName, Active from BrandMaster with(NoLock)  where Active='true'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBrand = new BrandMaster();
                        objBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrand.BrandID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objBrand.BrandName = Convert.ToString(objReader["BrandName"]);
                        objBrand.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objBrand.ShortDescriptionName = Convert.ToString(objReader["ShortDescriptionName"]);
                        objBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        BrandList.Add(objBrand);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.BrandList = BrandList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Brand Master");
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
