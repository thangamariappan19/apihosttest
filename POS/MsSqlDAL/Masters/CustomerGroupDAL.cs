using EasyBizAbsDAL.Masters;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizResponse.Masters.CustomerGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MsSqlDAL.Common;
using System.Data;
using ResourceStrings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizResponse;
using EasyBizRequest;
using EasyBizResponse.Masters.CustomerGroupResponse;
using EasyBizRequest.Masters.CustomerGroupRequest;

namespace MsSqlDAL.Masters
{
    public class CustomerGroupDAL : BaseCustomerGroupDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveCustomerGroupRequest)RequestObj;
            var ResponseData = new SaveCustomerGroupResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertCustomerGroupMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@CustomerGroupID", RequestData.CustomerGroupMasterData.ID); // enterprise server Id
                _CommandObj.Parameters.AddWithValue("@GroupCode", RequestData.CustomerGroupMasterData.GroupCode);
                _CommandObj.Parameters.AddWithValue("@GroupName", RequestData.CustomerGroupMasterData.GroupName);
                _CommandObj.Parameters.AddWithValue("@DiscountPercentage", RequestData.CustomerGroupMasterData.DiscountPercentage);
               _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.CustomerGroupMasterData.PriceListID);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CustomerGroupMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CustomerGroupMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CustomerGroupMasterData.Active);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Customer Group Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Customer Group Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Group Master");
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

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {

            var RequestData = (UpdateCustomerGroupMasterRequest)RequestObj;
            var ResponseData = new UpdateCustomerGroupMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateCustomerGroupMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CustomerGroupMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@GroupCode", RequestData.CustomerGroupMasterData.GroupCode);
                _CommandObj.Parameters.AddWithValue("@GroupName", RequestData.CustomerGroupMasterData.GroupName);
                _CommandObj.Parameters.AddWithValue("@DiscountPercentage", RequestData.CustomerGroupMasterData.DiscountPercentage);
                _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.CustomerGroupMasterData.PriceListID);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CustomerGroupMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.CustomerGroupMasterData.SCN);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CustomerGroupMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CustomerGroupMasterData.Active);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Customer Group Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Customer Group Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Group Master");
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



        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var CustomerGroupMaster = new CustomerGroupMaster();
            var RequestData = (DeleteCustomerGroupMasterRequest)RequestObj;
            var ResponseData = new DeleteCustomerGroupMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Delete from CustomerGroupMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.CustomerGroupMaster.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Customer Group Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Customer Group Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var CustomerGroupMaster = new CustomerGroupMaster();
            var RequestData = (SelectByIDCustomerGroupRequest)RequestObj;
            var ResponseData = new SelectByIDCustomerGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from CustomerGroupMaster with(NoLock)  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerGroupMaster = new CustomerGroupMaster();
                        objCustomerGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerGroupMaster.GroupCode = objReader["GroupCode"].ToString();
                        objCustomerGroupMaster.GroupName = objReader["GroupName"].ToString();
                        objCustomerGroupMaster.DiscountPercentage = objReader["DiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountPercentage"]) : 0;
                        objCustomerGroupMaster.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;

                        objCustomerGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCustomerGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerGroupMaster.Remarks = objReader["Remarks"].ToString();

                        ResponseData.CustomerGroupMaster = objCustomerGroupMaster;
                        ResponseData.ResponseDynamicData = objCustomerGroupMaster;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Group Master");
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
            var CustomerGroupMasterList = new List<CustomerGroupMaster>();

            var RequestData = new SelectAllCustomerGroupMasterRequest();
            var ResponseData = new SelectAllCustomerGroupMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
               // string sSql = "Select CG.*,PriceListName from CustomerGroupMaster CG inner join PriceListMaster PL ON CG.PriceListID=PL.ID";
                string sSql = "Select * from CustomerGroupMaster";               
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCustomerGroupMaster = new CustomerGroupMaster();

                        objCustomerGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerGroupMaster.GroupCode = objReader["GroupCode"].ToString();
                        objCustomerGroupMaster.GroupName = objReader["GroupName"].ToString();

                        objCustomerGroupMaster.DiscountPercentage = objReader["DiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountPercentage"]) : 0;
                        objCustomerGroupMaster.PriceListID = 0;
                        //objCustomerGroupMaster.PriceListName = Convert.ToString(objReader["PriceListName"]);   
                        objCustomerGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCustomerGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerGroupMaster.Remarks = objReader["Remarks"].ToString();

                        CustomerGroupMasterList.Add(objCustomerGroupMaster);                       

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerGroupMasterList = CustomerGroupMasterList;
                    ResponseData.ResponseDynamicData = CustomerGroupMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Group Master");
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectCustomerGroupLookUpResponse SelectCustomerGroupLookUp(SelectCustomerGroupLookUpRequest RequestObj)
        {
            var CustomerGroupMasterList = new List<CustomerGroupMaster>();
            var RequestData = (SelectCustomerGroupLookUpRequest)RequestObj;
            var ResponseData = new SelectCustomerGroupLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[GroupName],GroupCode from CustomerGroupMaster with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerGroupMaster = new CustomerGroupMaster();
                        objCustomerGroupMaster.ID = objReader["Id"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        (objCustomerGroupMaster.GroupName)  = Convert.ToString(objReader["GroupName"]);
                        objCustomerGroupMaster.GroupCode = Convert.ToString(objReader["GroupCode"]);
                        CustomerGroupMasterList.Add(objCustomerGroupMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerGroupMasterList = CustomerGroupMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Group Master");
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

        public override SelectAllCustomerGroupMasterResponse API_SelectALL(SelectAllCustomerGroupMasterRequest requestData)
        {
            var CustomerGroupMasterList = new List<CustomerGroupMaster>();

            var RequestData = (SelectAllCustomerGroupMasterRequest)requestData;
            var ResponseData = new SelectAllCustomerGroupMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                // string sSql = "Select CG.*,PriceListName from CustomerGroupMaster CG inner join PriceListMaster PL ON CG.PriceListID=PL.ID";
                //string sSql = "Select * from CustomerGroupMaster";

                string sSql = "Select ID, GroupCode, GroupName, DiscountPercentage, Remarks, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from CustomerGroupMaster " +
                   "LEFT JOIN(Select  count(CGM.ID) As TOTAL_CNT From CustomerGroupMaster CGM with(NoLock) " +
                   "where CGM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or CGM.GroupCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CGM.GroupName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CGM.DiscountPercentage like isnull('%" + RequestData.SearchString + "%','') " +
                       "or CGM.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or GroupCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or GroupName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or DiscountPercentage like isnull('%" + RequestData.SearchString + "%','') " +
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

                        var objCustomerGroupMaster = new CustomerGroupMaster();

                        objCustomerGroupMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerGroupMaster.GroupCode = objReader["GroupCode"].ToString();
                        objCustomerGroupMaster.GroupName = objReader["GroupName"].ToString();

                        objCustomerGroupMaster.DiscountPercentage = objReader["DiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountPercentage"]) : 0;
                        //objCustomerGroupMaster.PriceListID = 0;
                        ////objCustomerGroupMaster.PriceListName = Convert.ToString(objReader["PriceListName"]);   
                        //objCustomerGroupMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCustomerGroupMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCustomerGroupMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCustomerGroupMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objCustomerGroupMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerGroupMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerGroupMaster.Remarks = objReader["Remarks"].ToString();

                        CustomerGroupMasterList.Add(objCustomerGroupMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerGroupMasterList = CustomerGroupMasterList;
                    //ResponseData.ResponseDynamicData = CustomerGroupMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Group Master");
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
