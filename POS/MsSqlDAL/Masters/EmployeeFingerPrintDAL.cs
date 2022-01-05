using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.EmployeeFingerPrintRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.EmployeeFingerPrintResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
    public class EmployeeFingerPrintDAL : BaseEmployeeFingerPrintMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;


        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var EmployeeFPRecord = new EmployeeFingerPrintMaster();
            var RequestData = (DeleteEmployeeFingerPrintRequest)RequestObj;
            var ResponseData = new DeleteEmployeeFingerPrintResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from EmployeefingerPrint where EmployeeID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "EmployeeFingerPrint");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "EmployeeFingerPrint");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveEmployeeFingerPrintRequest)RequestObj;
            var ResponseData = new SaveEmployeeFingerPrintMasterResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            string strStatusCode = "";
            string StatusMsg = "";
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //_CommandObj = new SqlCommand("InsertEmployeeFingerPrint", _ConnectionObj);
                //_CommandObj.CommandType = CommandType.StoredProcedure;

                //SqlParameter NonTradingStockList = _CommandObj.Parameters.Add("@EmpFingerPrintDetails", SqlDbType.Xml);
                //NonTradingStockList.Direction = ParameterDirection.Input;
                //NonTradingStockList.Value = EmpFingerPrintXML(RequestData.EmployeeFingerPrintList);

                try
                {
                    DataTable dt = ToDataTable(RequestData.EmployeeFingerPrintList);


                    SqlConnection con = new SqlConnection(_ConnectionObj.ConnectionString);
                    SqlBulkCopy objbulk = new SqlBulkCopy(con);
                    objbulk.DestinationTableName = "EmployeeFingerPrint";
                    objbulk.ColumnMappings.Add("EmployeeID", "EmployeeID");
                    objbulk.ColumnMappings.Add("StoreID", "StoreID");
                    objbulk.ColumnMappings.Add("FingerPrint", "FingerPrint");
                    objbulk.ColumnMappings.Add("CreateBy", "CreateBy");
                    objbulk.ColumnMappings.Add("CreateOn", "CreateOn");
                    objbulk.ColumnMappings.Add("UpdateBy", "UpdateBy");
                    objbulk.ColumnMappings.Add("UpdateOn", "UpdateOn");
                    objbulk.ColumnMappings.Add("SCN", "SCN");
                    con.Open();
                    //insert bulk Records into DataBase.  
                    objbulk.WriteToServer(dt);
                    con.Close();
                    strStatusCode = "1";
                }
                catch (Exception ex)
                {
                    strStatusCode = "3";
                    StatusMsg = ex.Message;
                }
                //SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                //StatusCode.Direction = ParameterDirection.Output;

                //SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                //StatusMsg.Direction = ParameterDirection.Output;

                //_CommandObj.CommandType = CommandType.StoredProcedure;
                //_CommandObj.ExecuteNonQuery();

                
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "EmployeeFingerPrint");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "EmployeeFingerPrint");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "EmployeeFingerPrint");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "EmployeeFingerPrint");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        private object EmpFingerPrintXML(List<EmployeeFingerPrintMaster> employeeFingerPrintList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlcommon = new MsSqlCommon();
            foreach (EmployeeFingerPrintMaster objEmployeefingerPrint in employeeFingerPrintList)
            {
                sSql.Append("<EmpFingerPrintDetails>");
                sSql.Append("<EmployeeID>" + objEmployeefingerPrint.EmployeeID + "</EmployeeID>");
                //sSql.Append("<EmployeeName>" + objEmployeefingerPrint.EmployeeName + "</EmployeeName>");
                sSql.Append("<FingerPrint>" + objEmployeefingerPrint.FingerPrint +  "</FingerPrint>");
                //sSql.Append("<StoreCode>" + objEmployeefingerPrint.StoreCode + "</StoreCode>");
                sSql.Append("<CreatedBy>" + objEmployeefingerPrint.CreateBy + "</CreatedBy>");
                //sSql.Append("<StoreID>" + objEmployeefingerPrint.StoreID + "</StoreID>");
                sSql.Append("</EmpFingerPrintDetails>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectEmployeeFingerPrintByIDResponse SelectEmployeeFingerPrintByID(SelectEmployeeFingerPrintByIDRequest objRequest)
        {
            var EmployeeFingerPrintList = new List<EmployeeFingerPrintMaster>();
            var EmployeeFingerPrintRecord = new EmployeeFingerPrintMaster();
            var RequestData = (SelectEmployeeFingerPrintByIDRequest)objRequest;
            var ResponseData = new SelectEmployeeFingerPrintByIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select EM.ID,EM.EmployeeCode,Em.EmployeeName,EFP.FingerPrint,EM.StoreID,EM.StoreCode from EmployeeMaster EM left JOIN EmployeeFingerPrint EFP ON EM.ID = EFP.EmployeeID where EM.ID = '" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {


                    while (objReader.Read())
                    {
                        var objEmployeeFingerPrintMaster = new EmployeeFingerPrintMaster();

                        
                            ResponseData.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                            ResponseData.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                            ResponseData.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;

                        if (objReader["FingerPrint"] != System.DBNull.Value) {
                            objEmployeeFingerPrintMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objEmployeeFingerPrintMaster.EmployeeID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                            //objEmployeeFingerPrintMaster.EmployeeCode = Convert.ToString(objReader["EmployeeCode"]);
                            //objEmployeeFingerPrintMaster.EmployeeName = Convert.ToString(objReader["EmployeeName"]);
                            /*objEmployeeFingerPrintMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                            objEmployeeFingerPrintMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;*/
                            //objEmployeeMaster.FingerPrint = Convert.ToString(objReader["FingerPrint"]);                     
                            //objEmployeeFingerPrintMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                            //objEmployeeFingerPrintMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                            //objEmployeeFingerPrintMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                            //objEmployeeFingerPrintMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                            //objEmployeeFingerPrintMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                            objEmployeeFingerPrintMaster.FingerPrint = objReader["FingerPrint"] != DBNull.Value ? Convert.ToString(objReader["FingerPrint"]) : string.Empty;
                            EmployeeFingerPrintList.Add(objEmployeeFingerPrintMaster);
                            ////ResponseData.EmployeeFingerPrintRecord = objEmployeeFingerPrintMaster;
                            //ResponseData.ResponseDynamicData = objEmployeeFingerPrintMaster;
                        }
                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                            ResponseData.EmployeeFingerPrintList = EmployeeFingerPrintList;
                            ResponseData.ResponseDynamicData = EmployeeFingerPrintList;
                        
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee Finger Print Master");
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
