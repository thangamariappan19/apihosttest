using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.ProductGroupResponse;
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
   public class ProductGroupDAL:BaseProductGroupDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlTransaction transaction = null;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;


        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveProductGroupRequest)RequestObj;
            var ResponseData = new SaveProductGroupResponse();
            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
              

                _CommandObj = new SqlCommand("InsertProductGroup", _ConnectionObj, transaction);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ProductGroupID = _CommandObj.Parameters.Add("@ProductGroupID", SqlDbType.Int);
                ProductGroupID.Direction = ParameterDirection.Input;
                ProductGroupID.Value = RequestData.ProductGroupRecord.ID;

                SqlParameter ProductGroupCode = _CommandObj.Parameters.Add("@ProductGroupCode", SqlDbType.NVarChar);
                ProductGroupCode.Direction = ParameterDirection.Input;
                ProductGroupCode.Value = RequestData.ProductGroupRecord.ProductGroupCode;

                SqlParameter ProductGroupName = _CommandObj.Parameters.Add("@ProductGroupName", SqlDbType.NVarChar);
                ProductGroupName.Direction = ParameterDirection.Input;
                ProductGroupName.Value = RequestData.ProductGroupRecord.ProductGroupName;

                SqlParameter Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ProductGroupRecord.Description;               

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ProductGroupRecord.CreateBy;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ProductGroupRecord.Active;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "ProductGroup");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "ProductGroup");
                   
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ProductGroup");
                  
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "ProductGroup Master");
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

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateProductGroupRequest)RequestObj;
            var ResponseData = new UpdateProductGroupResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateProductGroup", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ProductGroupRecord.ID;

                SqlParameter ProductGroupCode = _CommandObj.Parameters.Add("@ProductGroupCode", SqlDbType.NVarChar);
                ProductGroupCode.Direction = ParameterDirection.Input;
                ProductGroupCode.Value = RequestData.ProductGroupRecord.ProductGroupCode;

                SqlParameter ProductGroupName = _CommandObj.Parameters.Add("@ProductGroupName", SqlDbType.NVarChar);
                ProductGroupName.Direction = ParameterDirection.Input;
                ProductGroupName.Value = RequestData.ProductGroupRecord.ProductGroupName;

                SqlParameter Description = _CommandObj.Parameters.Add("@Description", SqlDbType.NVarChar);
                Description.Direction = ParameterDirection.Input;
                Description.Value = RequestData.ProductGroupRecord.Description;
               

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.ProductGroupRecord.UpdateBy;

                SqlParameter UpdateOn = _CommandObj.Parameters.Add("@UpdateOn", SqlDbType.DateTime);
                UpdateOn.Direction = ParameterDirection.Input;
                UpdateOn.Value = RequestData.ProductGroupRecord.UpdateOn;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ProductGroupRecord.Active;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "ProductGroup");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "ProductGroup");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductGroup");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "ProductGroup Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ProductGroupRecord = new ProductGroupMaster();
            var RequestData = (DeleteProductGroupRequest)RequestObj;
            var ResponseData = new DeleteProductGroupResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("Delete from ProductGroupMaster where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "ProductGroup Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ProductGroup Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ProductGroupRecord = new ProductGroupMaster();
            var RequestData = (SelectByIDProductGroupRequest)RequestObj;
            var ResponseData = new SelectByIDProductGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from ProductGroupMaster with(NoLock) where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductGroupMaster = new ProductGroupMaster();

                        objProductGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductGroupMaster.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        objProductGroupMaster.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objProductGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true; 
                        objProductGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objProductGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.ProductGroupData = objProductGroupMaster;
                        ResponseData.ResponseDynamicData = objProductGroupMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductGroup Master");
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

        public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ProductGroupMasterList = new List<ProductGroupMaster>();
            var RequestData = (SelectAllProductGroupRequest)RequestObj;
            var ResponseData = new SelectAllProductGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ProductGroupMaster with(NoLock)";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductGroupMaster = new ProductGroupMaster();

                        objProductGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductGroupMaster.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        objProductGroupMaster.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objProductGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objProductGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objProductGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ProductGroupMasterList.Add(objProductGroupMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductGroupList = ProductGroupMasterList;
                    ResponseData.ResponseDynamicData = ProductGroupMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductGroup  Master");
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

        public override BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ProductGroupMasterList = new List<ProductGroupMaster>();
            var RequestData = (SelectByIDsProductGroupRequest)RequestObj;
            var ResponseData = new SelectByIDsProductGroupResponse();
            var ProductGroupMasterdata = new ProductGroupMaster();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from ProductGroupMaster with(NoLock)  where ID in '{0}'";
                sSql = string.Format(sSql, RequestData.IDs);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objProductGroupMaster = new ProductGroupMaster();

                        objProductGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductGroupMaster.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        objProductGroupMaster.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objProductGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objProductGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objProductGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        ProductGroupMasterList.Add(objProductGroupMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductGroupMasterList = ProductGroupMasterList;
                    ResponseData.ResponseDynamicData = ProductGroupMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Group Master");
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

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectProductGroupLookUpResponse SelectProductGroupLookUp(SelectProductGroupLookUpRequest ObjRequest)
        {
            var ProductGroupList = new List<ProductGroupMaster>();
            var RequestData = (SelectProductGroupLookUpRequest)ObjRequest;
            var ResponseData = new SelectProductGroupLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[ProductGroupName],ProductGroupCode from ProductGroupMaster with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        ProductGroupMaster objProductGroup = new ProductGroupMaster();
                        objProductGroup.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductGroup.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objProductGroup.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        ProductGroupList.Add(objProductGroup);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductGroupList = ProductGroupList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductGroup Master");
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

        public override SelectAllProductGroupResponse API_SelectALL(SelectAllProductGroupRequest requestData)
        {
            var ProductGroupMasterList = new List<ProductGroupMaster>();
            var RequestData = (SelectAllProductGroupRequest)requestData;
            var ResponseData = new SelectAllProductGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from ProductGroupMaster with(NoLock)";

                string sSql = "Select ID, ProductGroupCode, ProductGroupName, Description, Active, RC.TOTAL_CNT [RecordCount]  " +
                   "from ProductGroupMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(PG.ID) As TOTAL_CNT From ProductGroupMaster PG with(NoLock) " +
                   "where PG.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or PG.ProductGroupCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or PG.ProductGroupName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or PG.Description like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ProductGroupCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ProductGroupName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Description like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objProductGroupMaster = new ProductGroupMaster();

                        objProductGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductGroupMaster.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        objProductGroupMaster.ProductGroupName = Convert.ToString(objReader["ProductGroupName"]);
                        objProductGroupMaster.Description = Convert.ToString(objReader["Description"]);
                        objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objProductGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objProductGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objProductGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objProductGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objProductGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objProductGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ProductGroupMasterList.Add(objProductGroupMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductGroupList = ProductGroupMasterList;
                    //ResponseData.ResponseDynamicData = ProductGroupMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductGroup  Master");
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
