using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EasyBizAbsDAL.Masters;
using EasyBizResponse;
using EasyBizRequest;
using MsSqlDAL.Common;
using ResourceStrings;
using EasyBizDBTypes.Common;
using EasyBizResponse.Masters.ProductLineMasterResponse;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizDBTypes.Masters;
namespace MsSqlDAL.Masters
{
    public class ProductLineMasterDAL : BaseProductLineMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {

            var RequestData = (SaveProductLineMasterRequest)RequestObj;
            var ResponseData = new SaveProductLineMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertProductLineMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ProductLine = _CommandObj.Parameters.Add("@ProductLineID", SqlDbType.Int);
                ProductLine.Direction = ParameterDirection.Input;
                ProductLine.Value = RequestData.ProductLineMasterData.ID;

                var Code = _CommandObj.Parameters.Add("@ProductLineCode", SqlDbType.NVarChar);
                Code.Direction = ParameterDirection.Input;
                Code.Value = RequestData.ProductLineMasterData.ProductLineCode;

                var Name = _CommandObj.Parameters.Add("@ProductLineName", SqlDbType.NVarChar);
                Name.Direction = ParameterDirection.Input;
                Name.Value = RequestData.ProductLineMasterData.ProductLineName;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ProductLineMasterData.Active;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ProductLineMasterData.CreateBy;

                var Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ProductLineMasterData.Description;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "ProductLine");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "ProductLine");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ProductLine");

                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ProductLine Master");
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

            var RequestData = (UpdateProductLineMasterRequest)RequestObj;
            var ResponseData = new UpdateProductLineMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateProductLineMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ProductLineMasterData.ID;

                var Code = _CommandObj.Parameters.Add("@ProductLineCode", SqlDbType.VarChar);
                Code.Direction = ParameterDirection.Input;
                Code.Value = RequestData.ProductLineMasterData.ProductLineCode;

                var Name = _CommandObj.Parameters.Add("@ProductLineName", SqlDbType.NVarChar);
                Name.Direction = ParameterDirection.Input;
                Name.Value = RequestData.ProductLineMasterData.ProductLineName;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ProductLineMasterData.Active;

                var UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.ProductLineMasterData.UpdateBy;

                var UpdateOn = _CommandObj.Parameters.Add("@UpdateOn", SqlDbType.DateTime);
                UpdateOn.Direction = ParameterDirection.Input;
                UpdateOn.Value = RequestData.ProductLineMasterData.UpdateOn;

                var ReMarks = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                ReMarks.Direction = ParameterDirection.Input;
                ReMarks.Value = RequestData.ProductLineMasterData.Description;

                var SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.ProductLineMasterData.SCN;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.VarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "ProductLine");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "ProductLine");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductLine");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductLine Master");
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
            var ProductLineMasterRecord = new ProductLineMaster();

            var RequestData = (DeleteProductLineMasterRequest)RequestObj;
            var ResponseData = new DeleteProductLineMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                string sSql = "Delete from ProductLineMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);                
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "ProductLine Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ProductLine Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {

            var ProductLineMasterRecord = new ProductLineMaster();
            var RequestData = (SelectByIDProductLineMasterRequest)RequestObj;
            var ResponseData = new SelectByIDProductLineMasterResponse();
            SqlDataReader objReader;            
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
           

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ProductLineMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);              
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductLineMaster = new ProductLineMaster();
                        objProductLineMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductLineMaster.ProductLineCode = Convert.ToString(objReader["ProductLineCode"]);
                        objProductLineMaster.ProductLineName = Convert.ToString(objReader["ProductLineName"]);
                        objProductLineMaster.Description = Convert.ToString(objReader["Description"]);
                        objProductLineMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductLineMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductLineMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductLineMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objProductLineMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objProductLineMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.ProductLineMasterRecord = objProductLineMaster;
                        ResponseData.ResponseDynamicData = objProductLineMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductLine Master");
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
            var ProductLineMasterList = new List<ProductLineMaster>();
            var RequestData = (SelectAllProductLineMasterRequest)RequestObj;
            var ResponseData = new SelectAllProductLineMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sQuery = "Select * from ProductLineMaster with(NoLock)";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductLineMaster = new ProductLineMaster();
                        objProductLineMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;


                        objProductLineMaster.ProductLineCode = Convert.ToString(objReader["ProductLineCode"]);
                        objProductLineMaster.ProductLineName = Convert.ToString(objReader["ProductLineName"]);
                        objProductLineMaster.Description = Convert.ToString(objReader["Description"]);

                        objProductLineMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductLineMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductLineMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductLineMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objProductLineMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objProductLineMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ProductLineMasterList.Add(objProductLineMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductLineMasterList = ProductLineMasterList;
                    ResponseData.ResponseDynamicData = ProductLineMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductLine Master");
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

        public override SelectProductLineLookUpResponse SelectProductLineLookUp(SelectProductLineLookUpRequest ObjRequest)
        {
            var ProductLineLookUpList = new List<ProductLineMaster>();
            var RequestData = (SelectProductLineLookUpRequest)ObjRequest;
            var ResponseData = new SelectProductLineLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                sQuery = "Select ID,ProductLineName from ProductLineMaster with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductLineMaster = new ProductLineMaster();
                        objProductLineMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductLineMaster.ProductLineName = Convert.ToString(objReader["ProductLineName"]);
                        ProductLineLookUpList.Add(objProductLineMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductLineMasterList = ProductLineLookUpList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductLine Master");
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

        public override SelectAllProductLineMasterResponse API_SelectALL(SelectAllProductLineMasterRequest requestData)
        {
            var ProductLineMasterList = new List<ProductLineMaster>();
            var RequestData = (SelectAllProductLineMasterRequest)requestData;
            var ResponseData = new SelectAllProductLineMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //string sQuery = "Select * from ProductLineMaster with(NoLock)";

                string sQuery = "Select ID, ProductLineCode, ProductLineName, Description, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from ProductLineMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(PM.ID) As TOTAL_CNT From ProductLineMaster PM with(NoLock) " +
                   "where PM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or PM.ProductLineCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or PM.ProductLineName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or PM.Description like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ProductLineCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ProductLineName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Description like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductLineMaster = new ProductLineMaster();
                        objProductLineMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;


                        objProductLineMaster.ProductLineCode = Convert.ToString(objReader["ProductLineCode"]);
                        objProductLineMaster.ProductLineName = Convert.ToString(objReader["ProductLineName"]);
                        objProductLineMaster.Description = Convert.ToString(objReader["Description"]);

                        //objProductLineMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objProductLineMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objProductLineMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objProductLineMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objProductLineMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objProductLineMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ProductLineMasterList.Add(objProductLineMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductLineMasterList = ProductLineMasterList;
                    //ResponseData.ResponseDynamicData = ProductLineMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductLine Master");
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
