using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.ExpenseMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.ExpenseMasterResponse;
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
    public class ExpenseMasterDAL : BaseExpenseMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {

            SaveExpenseMasterRequest RequestData = (SaveExpenseMasterRequest)RequestObj;
            SaveExpenseMasterResponse ResponseData = new SaveExpenseMasterResponse();
            List<ExpenseMasterTypes> ExpenseMasterTypesList = RequestData.ExpenseMasterTypesData;
            StringBuilder sSql = new StringBuilder();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //transaction = _ConnectionObj.BeginTransaction();

                _CommandObj = new SqlCommand("API_InsertOrUpdateExpenseMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (ExpenseMasterTypes objExpenseMasterTypes in ExpenseMasterTypesList)
                {
                    sSql.Append("<ExpenseMasterTypes>");
                    sSql.Append("<ID>" + (objExpenseMasterTypes.ID) + "</ID>");
                    sSql.Append("<ExpenseCode>" + (objExpenseMasterTypes.ExpenseCode) + "</ExpenseCode>");
                    sSql.Append("<ExpenseName>" + (objExpenseMasterTypes.ExpenseName) + "</ExpenseName>");
                    sSql.Append("<Remarks>" + (objExpenseMasterTypes.Remarks) + "</Remarks>");
                    sSql.Append("<CreateBy>" + (objExpenseMasterTypes.CreateBy) + "</CreateBy>");
                    sSql.Append("<UpdateBy>" + (objExpenseMasterTypes.UpdateBy) + "</UpdateBy>");
                    sSql.Append("<Active>" + (objExpenseMasterTypes.Active) + "</Active>");
                    sSql.Append("<SCN>" + (objExpenseMasterTypes.SCN) + "</SCN>");
                    sSql.Append("</ExpenseMasterTypes>");
                }

                SqlParameter CollectionData = _CommandObj.Parameters.Add("@CollectionData", SqlDbType.Xml);
                CollectionData.Direction = ParameterDirection.Input;
                CollectionData.Value = sSql.ToString();

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                SqlParameter ID = _CommandObj.Parameters.Add("@ExIDs", SqlDbType.VarChar,500);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    if(ExpenseMasterTypesList[0].ID==0)
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Expense Master");
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Expense Master");
                    }
                    
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Expense Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Expense Master");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "MASCollection");
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
            var CustomerGroupMaster = new CustomerGroupMaster();
            var RequestData = (DeleteExpenseMasterRequest)RequestObj;
            var ResponseData = new DeleteExpenseMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "delete from ExpenseMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ExpenseMasterTypesData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Expense Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Expense Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var ExpenseMasterTypesdata = new ExpenseMasterTypes();
            var RequestData = (SelectByIDExpenseMasterRequest)RequestObj;
            var ResponseData = new SelectByIDExpenseMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "Select * from ExpenseMaster with(NoLock) where ID='{0}'";

                if(RequestData.ID == 0)
                {
                    if(! string.IsNullOrEmpty( RequestData.DocumentIDs))
                    {
                        sSql = "Select * from ExpenseMaster with(NoLock) where ID in ({0})";
                        sSql = string.Format(sSql, RequestData.DocumentIDs);
                    }
                }
                else
                {
                    sSql = string.Format(sSql, RequestData.ID);
                }

                
                

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objExpenseMasterTypes = new ExpenseMasterTypes();
                        objExpenseMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objExpenseMasterTypes.ExpenseCode = objReader["ExpenseCode"].ToString();
                        objExpenseMasterTypes.ExpenseName = objReader["ExpenseName"].ToString();
                        objExpenseMasterTypes.Remarks = objReader["Remarks"].ToString();
                        objExpenseMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objExpenseMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objExpenseMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objExpenseMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objExpenseMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExpenseMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        ResponseData.ExpenseMasterTypesData = objExpenseMasterTypes;
                        ResponseData.ResponseDynamicData = objExpenseMasterTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Expense Master");
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
            var ExpenseMasterTypesList = new List<ExpenseMasterTypes>();

            var RequestData = new SelectAllExpenseMasterRequest();
            var ResponseData = new SelectAllExpenseMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ExpenseMaster with(NoLock)";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objExpenseMasterTypes = new ExpenseMasterTypes();

                        objExpenseMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objExpenseMasterTypes.ExpenseCode = objReader["ExpenseCode"].ToString();
                        objExpenseMasterTypes.ExpenseName = objReader["ExpenseName"].ToString();
                        objExpenseMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objExpenseMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objExpenseMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objExpenseMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objExpenseMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExpenseMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objExpenseMasterTypes.Remarks = objReader["Remarks"].ToString();

                        ExpenseMasterTypesList.Add(objExpenseMasterTypes);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ExpenseMasterTypesList = ExpenseMasterTypesList;
                    ResponseData.ResponseDynamicData = ExpenseMasterTypesList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Expense Master");
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
            var ExpenseMasterTypesList = new List<ExpenseMasterTypes>();

            var RequestData = (SelectIDExpenseMasterRequest)RequestObj;
            var ResponseData = new SelectIDExpenseMasterResponse();

            var ExpenseMasterTypesdata = new ExpenseMasterTypes();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "Select * from ExpenseMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                if (RequestData.ID == 0)
                {
                    if (!string.IsNullOrEmpty(RequestData.DocumentIDs))
                    {
                        sSql = "Select * from ExpenseMaster with(NoLock) where ID in ({0})";
                        sSql = string.Format(sSql, RequestData.DocumentIDs);
                    }
                }
                else
                {
                    sSql = string.Format(sSql, RequestData.ID);
                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objExpenseMasterTypes = new ExpenseMasterTypes();

                        objExpenseMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objExpenseMasterTypes.ExpenseCode = objReader["ExpenseCode"].ToString();
                        objExpenseMasterTypes.ExpenseName = objReader["ExpenseName"].ToString();
                        objExpenseMasterTypes.Remarks = objReader["Remarks"].ToString();

                        objExpenseMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objExpenseMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objExpenseMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objExpenseMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objExpenseMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExpenseMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ExpenseMasterTypesList.Add(objExpenseMasterTypes);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ExpenseMasterTypesList = ExpenseMasterTypesList;
                    ResponseData.ResponseDynamicData = ExpenseMasterTypesList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Expense Master");
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

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectAllExpenseMasterResponse API_SelectALL(SelectAllExpenseMasterRequest requestData)
        {
            var ExpenseMasterTypesList = new List<ExpenseMasterTypes>();

            var RequestData = (SelectAllExpenseMasterRequest)requestData;
            var ResponseData = new SelectAllExpenseMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from ExpenseMaster with(NoLock)";
                string sSql = "Select ID, ExpenseCode, ExpenseName, Remarks, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from ExpenseMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(EM.ID) As TOTAL_CNT From ExpenseMaster EM with(NoLock) " +
                   "where EM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or EM.ExpenseCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM.ExpenseName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or EM.Remarks like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +

                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or ExpenseCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ExpenseName like isnull('%" + RequestData.SearchString + "%','') " +                       
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

                        var objExpenseMasterTypes = new ExpenseMasterTypes();

                        objExpenseMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objExpenseMasterTypes.ExpenseCode = objReader["ExpenseCode"].ToString();
                        objExpenseMasterTypes.ExpenseName = objReader["ExpenseName"].ToString();
                        //objExpenseMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objExpenseMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objExpenseMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objExpenseMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objExpenseMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objExpenseMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objExpenseMasterTypes.Remarks = objReader["Remarks"].ToString();

                        ExpenseMasterTypesList.Add(objExpenseMasterTypes);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ExpenseMasterTypesList = ExpenseMasterTypesList;
                    //ResponseData.ResponseDynamicData = ExpenseMasterTypesList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Expense Master");
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
