using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.TaxMasterResponse;
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
    public class TaxMasterDAL : BaseTaxMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveTaxRequest)RequestObj;
            var ResponseData = new SaveTaxResponse();
            var Taxlist = RequestData.Taxlist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateTaxMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (TaxMaster objTax in Taxlist)
                    if (objTax.TaxCode != "" || objTax.TaxCode != null)
                    {
                        {
                            sSql.Append("<TaxMaster>");
                            sSql.Append("<ID>" + (objTax.ID) + "</ID>");
                            sSql.Append("<TaxCode>" + (objTax.TaxCode) + "</TaxCode>");
                            sSql.Append("<TaxPercentage>" + objTax.TaxPercentage + "</TaxPercentage>");
                            sSql.Append("<Sales>" + (objTax.Sales) + "</Sales>");
                            sSql.Append("<Purchase>" + (objTax.Purchase) + "</Purchase>");
                            sSql.Append("<CreateBy>" + (objTax.CreateBy) + "</CreateBy>");
                            sSql.Append("<Active>" + (objTax.Active) + "</Active>");
                            sSql.Append("<inclusivetax>" + (objTax.inclusivetax) + "</inclusivetax>");
                            sSql.Append("</TaxMaster>");
                        }
                    }
                var SubBrandData = _CommandObj.Parameters.Add("@TaxData", SqlDbType.Xml);
                SubBrandData.Direction = ParameterDirection.Input;
                SubBrandData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@TaxIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Tax");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Tax");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tax");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Tax");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var TaxRecord = new TaxMaster();
            var RequestData = (DeleteTaxRequest)RequestObj;
            var ResponseData = new DeleteTaxResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from TaxMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Tax Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Tax Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var TaxList = new List<TaxMaster>();
            var TaxRecord = new TaxMaster();
            var RequestData = (SelectByTaxIDRequest)RequestObj;
            var ResponseData = new SelectByTaxIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from TaxMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objTax.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);     
                        objTax.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToBoolean(objReader["Sales"]) : true;
                         objTax.Purchase = objReader["Purchase"] != DBNull.Value ? Convert.ToBoolean(objReader["Purchase"]): true;
                        objTax.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTax.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTax.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objTax.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objTax.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTax.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objTax.inclusivetax = objReader["inclusivetax"] != DBNull.Value ? Convert.ToBoolean(objReader["inclusivetax"]) : true;
                        TaxList.Add(objTax);                       
                      
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TaxList = TaxList;
                    ResponseData.ResponseDynamicData = TaxList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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
            var TaxList = new List<TaxMaster>();
            var RequestData = (SelectAllTaxRequest)RequestObj;
            var ResponseData = new SelectAllTaxResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from TaxMaster with(NoLock)";
                
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objTax.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);     
                        objTax.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToBoolean(objReader["Sales"]) : true;
                         objTax.Purchase = objReader["Purchase"] != DBNull.Value ? Convert.ToBoolean(objReader["Purchase"]): true;
                        objTax.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTax.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTax.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objTax.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objTax.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTax.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objTax.inclusivetax = objReader["inclusivetax"] != DBNull.Value ? Convert.ToBoolean(objReader["inclusivetax"]) : true;

                        TaxList.Add(objTax);
                    }
                    ResponseData.TaxList = TaxList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = TaxList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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
            var TaxList = new List<TaxMaster>();
            var RequestData = (SelectByTaxIDsRequest)RequestObj;
            var ResponseData = new SelectByTaxIDsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from TaxMaster with(NoLock) where ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.IDs);

               
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objTax.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);
                        objTax.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToBoolean(objReader["Sales"]) : true;
                        objTax.Purchase = objReader["Purchase"] != DBNull.Value ? Convert.ToBoolean(objReader["Purchase"]) : true;
                        objTax.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTax.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTax.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objTax.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objTax.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTax.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objTax.inclusivetax = objReader["inclusivetax"] != DBNull.Value ? Convert.ToBoolean(objReader["inclusivetax"]) : true;

                        TaxList.Add(objTax);
                    }
                    ResponseData.TaxMasterList = TaxList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = TaxList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override EasyBizResponse.Masters.TaxMasterResponse.SelectTaxLookUpResponse SelectTaxLookUp(EasyBizRequest.Masters.TaxMasterRequest.SelectTaxLookUpRequest ObjRequest)
        {
            var TaxList = new List<TaxMaster>();
            var RequestData = (SelectTaxLookUpRequest)ObjRequest;
            var ResponseData = new SelectTaxLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[TaxCode] from TaxMaster with(NoLock) where Active='true'";
                
              
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        TaxList.Add(objTax);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.TaxList = TaxList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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

        public override SelectTaxDetailsByCountryIDResponse SelectTaxDetailsByCountryID(SelectTaxDetailsByCountryIDRequest ObjRequest)
        {
            var TaxList = new List<TaxMaster>();
            var RequestData = (SelectTaxDetailsByCountryIDRequest)ObjRequest;
            var ResponseData = new SelectTaxDetailsByCountryIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "Select TM.* from CountryMaster CM join taxmaster TM on CM.TaxID=TM.id where CM.ID='" + RequestData.CountryID + "' and TM.Sales='" + RequestData.Type + "'";           
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objTax.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);
                        objTax.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToBoolean(objReader["Sales"]) : true;
                        objTax.Purchase = objReader["Purchase"] != DBNull.Value ? Convert.ToBoolean(objReader["Purchase"]) : true;
                        objTax.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objTax.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objTax.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objTax.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objTax.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTax.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objTax.inclusivetax = objReader["inclusivetax"] != DBNull.Value ? Convert.ToBoolean(objReader["inclusivetax"]) : true;

                        TaxList.Add(objTax);
                    }
                    ResponseData.TaxDetailList = TaxList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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

        public override SelectAllTaxResponse API_SelectALL(SelectAllTaxRequest requestData)
        {
            var TaxList = new List<TaxMaster>();
            var RequestData = (SelectAllTaxRequest)requestData;
            var ResponseData = new SelectAllTaxResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //string sSql = "Select * from TaxMaster with(NoLock)";

                var sSql = new StringBuilder();
                decimal myInt;
                bool isNumerical = decimal.TryParse(RequestData.SearchString, out myInt);

                if (isNumerical)
                {
                    sSql.Append("Select ID, TaxCode, TaxPercentage, Sales, Purchase, Active, RC.TOTAL_CNT [RecordCount]  from TaxMaster ");
                    sSql.Append("LEFT JOIN(Select  count(TM.ID) As TOTAL_CNT From TaxMaster TM with(NoLock) ");

                    sSql.Append("where TM.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or TM.TaxCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or TM.TaxPercentage like isnull('%" + decimal.Parse(RequestData.SearchString) + "%','')) As RC ON 1 = 1 ");
                    sSql.Append("where Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or TaxCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or TaxPercentage = isnull('%" + decimal.Parse(RequestData.SearchString) + "%','')) ");
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }
                else
                {
                    sSql.Append("Select ID, TaxCode, TaxPercentage, Sales, Purchase, Active, RC.TOTAL_CNT [RecordCount]  from TaxMaster ");
                    sSql.Append("LEFT JOIN(Select  count(TM.ID) As TOTAL_CNT From TaxMaster TM with(NoLock) ");

                    sSql.Append("where TM.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or TM.TaxCode like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ");
                    
                    sSql.Append("where Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or TaxCode like isnull('%" + RequestData.SearchString + "%','')) ");
                    //sSql.Append("or TaxPercentage = isnull('" + RequestData.SearchString + "','')) ");                    
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }

                //string sSql = "Select ID, TaxCode, TaxPercentage, Sales, Purchase, Active, RecordCount = COUNT(*) OVER() " +
                //    "from TaxMaster " +
                //    "where Active = " + RequestData.IsActive + " " +
                //        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //            "or TaxCode = isnull('" + RequestData.SearchString + "','') " +
                //            "or TaxPercentage = isnull('" + RequestData.SearchString + "','')) " +
                //    //"or Sales = isnull('" + RequestData.SearchString + "','') " +
                //    //"or Purchase = isnull('" + RequestData.SearchString + "','')) " +
                //    "order by ID asc " +
                //    "offset " + RequestData.Offset + " rows " +
                //    "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objTax.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);

                        objTax.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToBoolean(objReader["Sales"]) : true;
                        objTax.Purchase = objReader["Purchase"] != DBNull.Value ? Convert.ToBoolean(objReader["Purchase"]) : true;
                        //objTax.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objTax.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objTax.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objTax.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objTax.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objTax.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objTax.inclusivetax = objReader["inclusivetax"] != DBNull.Value ? Convert.ToBoolean(objReader["inclusivetax"]) : true;

                        TaxList.Add(objTax);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    //ResponseData.TaxList = TaxList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = TaxList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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

        public override SelectAllTaxResponse API_SelectTaxLookUp(SelectAllTaxRequest objRequest)
        {
            var TaxList = new List<TaxMaster>();
            var RequestData = (SelectAllTaxRequest)objRequest;
            var ResponseData = new SelectAllTaxResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select ID,TaxCode,TaxPercentage,Active from TaxMaster with(NoLock) WHERE Active = 1";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTax = new TaxMaster();
                        objTax.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTax.TaxCode = Convert.ToString(objReader["TaxCode"]);
                        objTax.TaxPercentage = Convert.ToString(objReader["TaxPercentage"]);
                        //objTax.Sales = objReader["Sales"] != DBNull.Value ? Convert.ToBoolean(objReader["Sales"]) : true;
                        //objTax.Purchase = objReader["Purchase"] != DBNull.Value ? Convert.ToBoolean(objReader["Purchase"]) : true;
                        objTax.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        // objTax.inclusivetax = objReader["inclusivetax"] != DBNull.Value ? Convert.ToBoolean(objReader["inclusivetax"]) : true;

                        TaxList.Add(objTax);
                    }
                    ResponseData.TaxList = TaxList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = TaxList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Tax Master");
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

