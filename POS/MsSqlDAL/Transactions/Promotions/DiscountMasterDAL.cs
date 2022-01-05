using EasyBizAbsDAL.Transactions.DiscountMaster;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest;
using EasyBizRequest.Transactions.DiscountMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.Promotions.DiscountMasterResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Promotions
{
   public class DiscountMasterDAL : BaseDiscountMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override SelectDiscountMasterDetailsResponse SelecDiscountMasterDetails(SelectDiscountMasterDetailsRequest ObjRequest)
        {
            throw new NotImplementedException();
        }

        public override SelectByIDDiscountMasterResponse SelectHeaderID(SelectByIDDiscountMasterRequest ObjRequest)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveDiscountMasterRequest)RequestObj;
            var ResponseData = new SaveDiscountMasterResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateDiscountMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var HeaderID = _CommandObj.Parameters.Add("@HeaderID", SqlDbType.Int);
                HeaderID.Direction = ParameterDirection.Input;
                HeaderID.Value = RequestData.DiscountMasterRecord.ID;

                var CustomerGroupID = _CommandObj.Parameters.Add("@CustomerGroupID", SqlDbType.Int);
                CustomerGroupID.Direction = ParameterDirection.Input;
                CustomerGroupID.Value = RequestData.DiscountMasterRecord.CustomerGroupID;

                var CustomerGroupCode = _CommandObj.Parameters.Add("@CustomerGroupCode", SqlDbType.VarChar);
                CustomerGroupCode.Direction = ParameterDirection.Input;
                CustomerGroupCode.Value = RequestData.DiscountMasterRecord.CustomerGroupCode;

                //var CountryIDs = _CommandObj.Parameters.Add("@CountryIDs", SqlDbType.VarChar);
                //CountryIDs.Direction = ParameterDirection.Input;
                //CountryIDs.Value = RequestData.DiscountMasterRecord.CountryIDs;

                //var CountryCodes = _CommandObj.Parameters.Add("@CountryCodes", SqlDbType.VarChar);
                //CountryCodes.Direction = ParameterDirection.Input;
                //CountryCodes.Value = RequestData.DiscountMasterRecord.CountryCodes;

                //var StoreIDs = _CommandObj.Parameters.Add("@StoreIDs", SqlDbType.VarChar);
                //StoreIDs.Direction = ParameterDirection.Input;
                //StoreIDs.Value = RequestData.DiscountMasterRecord.StoreIDs;

                //var StoreCodes = _CommandObj.Parameters.Add("@StoreCodes", SqlDbType.VarChar);
                //StoreCodes.Direction = ParameterDirection.Input;
                //StoreCodes.Value = RequestData.DiscountMasterRecord.StoreCodes;

                var DiscountType = _CommandObj.Parameters.Add("@DiscountType", SqlDbType.VarChar);
                DiscountType.Direction = ParameterDirection.Input;
                DiscountType.Value = RequestData.DiscountMasterRecord.DiscountType;

                var EmployeeDiscountDetails = _CommandObj.Parameters.Add("@EmployeeDiscountDetails", SqlDbType.Xml);
                EmployeeDiscountDetails.Direction = ParameterDirection.Input;
                EmployeeDiscountDetails.Value = EmployeeDiscountDetailsXml(RequestData.EmployeeDiscountDetailList);

                var FamilyDiscountDetails = _CommandObj.Parameters.Add("@FamilyDiscountDetails", SqlDbType.Xml);
                FamilyDiscountDetails.Direction = ParameterDirection.Input;
                FamilyDiscountDetails.Value = FamilyDiscountDetailsXml(RequestData.FamilyDiscountDetailList);

                //var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                //CreateBy.Direction = ParameterDirection.Input;
                //CreateBy.Value = RequestData.DiscountMasterRecord.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Discount Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Discount Master");
                }
                else
                {


                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Discount Master");

                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Discount Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        private string EmployeeDiscountDetailsXml(List<EmployeDiscountDetailTypes> EmployeeDiscountDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (EmployeDiscountDetailTypes objEmployeeDiscountDetails in EmployeeDiscountDetailList)
            {
                sSql.Append("<EmployeDiscountDetailTypes>");
                sSql.Append("<ID>" + (objEmployeeDiscountDetails.ID) + "</ID>");
                sSql.Append("<DiscountHeaderID>" + (objEmployeeDiscountDetails.DiscountHeaderID) + "</DiscountHeaderID>");
                sSql.Append("<FromDate>" + sqlCommon.GetSQLServerDateString(objEmployeeDiscountDetails.FromDate) + "</FromDate>");
                sSql.Append("<ToDate>" + sqlCommon.GetSQLServerDateString(objEmployeeDiscountDetails.ToDate) + "</ToDate>");
                sSql.Append("<DiscountValue>" + (objEmployeeDiscountDetails.DiscountValue) + "</DiscountValue>");             
                sSql.Append("</EmployeDiscountDetailTypes>");
            }
            return sSql.ToString();
        }

        private string FamilyDiscountDetailsXml(List<FamilyDiscountDetailTypes> FamilyDiscountDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (FamilyDiscountDetailTypes objFamilyDiscountDetails in FamilyDiscountDetailList)
            {
                sSql.Append("<FamilyDiscountDetailTypes>");
                sSql.Append("<ID>" + (objFamilyDiscountDetails.ID) + "</ID>");
                sSql.Append("<DiscountHeaderID>" + (objFamilyDiscountDetails.DiscountHeaderID) + "</DiscountHeaderID>");
                sSql.Append("<BrandID>" + (objFamilyDiscountDetails.BrandID) + "</BrandID>");
                sSql.Append("<BrandCode>" + (objFamilyDiscountDetails.BrandCode) + "</BrandCode>");
                sSql.Append("<DiscountValue>" + (objFamilyDiscountDetails.DiscountValue) + "</DiscountValue>");
                sSql.Append("<StoreCode>" + (objFamilyDiscountDetails.StoreCode) + "</StoreCode>");
                sSql.Append("</FamilyDiscountDetailTypes>");
            }
            return sSql.ToString();
        }

      
        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var EmployeeDiscountRecord = new EmployeDiscountDetailTypes();
            var FamilyDiscountrRecords = new FamilyDiscountDetailTypes();
            var RequestData = (UpdateDiscountMasterRequest)RequestObj;
            var ResponseData = new UpdateDiscountMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "delete from employeediscountdetails where  DiscountHeaderID='{0}';delete from familydiscountdetails where  DiscountHeaderID='{0}';delete from discountmaster where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Discount Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Discount Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {            
            var DiscountMasterRecord = new DiscountMasterTypes();
            var objEmployeeDiscountList = new List<EmployeDiscountDetailTypes>();
            var objFamilyDiscountList = new List<FamilyDiscountDetailTypes>();

            var RequestData = (SelectAllDiscountMasterRequest)RequestObj;
            var ResponseData = new SelectAllDiscountMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "SELECT * FROM [dbo].[DiscountMaster] WHERE CUSTOMERGROUPID=" + RequestData.CustomerGroupID + " AND CUSTOMERGROUPCODE='" + RequestData.CustomerGroupCode + "'";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDiscountMaster = new DiscountMasterTypes();
                        objDiscountMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDiscountMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objDiscountMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        //objDiscountMaster.CountryIDs = Convert.ToString(objReader["CountryIDs"]);
                        //objDiscountMaster.StoreIDs = Convert.ToString(objReader["StoreIDs"]);
                        objDiscountMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);

                        DiscountMasterRecord = objDiscountMaster;
                    }
                    objEmployeeDiscountList = EmployeeDiscountList(DiscountMasterRecord.ID, RequestData);
                    objFamilyDiscountList = FamilyDiscountList(DiscountMasterRecord.ID, RequestData);
                    
                    ResponseData.DiscountMasterRecord = DiscountMasterRecord;
                    ResponseData.DiscountMasterRecord.EmployeeDiscountDetails = new List<EmployeDiscountDetailTypes>();
                    ResponseData.DiscountMasterRecord.EmployeeDiscountDetails = objEmployeeDiscountList;

                    ResponseData.DiscountMasterRecord.FamilyDiscountDetails = new List<FamilyDiscountDetailTypes>();
                    ResponseData.DiscountMasterRecord.FamilyDiscountDetails = objFamilyDiscountList;

                    ResponseData.EmployeeDiscountDetailList = objEmployeeDiscountList;
                    ResponseData.FamilyDiscountDetailList = objFamilyDiscountList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "BrandDivision Map");
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
        public List<EmployeDiscountDetailTypes> EmployeeDiscountList(int DiscountID, BaseRequestType RequestObj)
        {
            var objEmployeeDiscountList = new List<EmployeDiscountDetailTypes>();
            var RequestData = (SelectAllDiscountMasterRequest)RequestObj;
            var ResponseData = new SelectAllDiscountMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "SELECT * FROM [dbo].[EmployeeDiscountDetails] WHERE DISCOUNTHEADERID=" + DiscountID;

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objEmployeDiscountDetailTypes = new EmployeDiscountDetailTypes();
                        objEmployeDiscountDetailTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;                     
                        objEmployeDiscountDetailTypes.FromDate = objReader["FromDate"] != DBNull.Value ? Convert.ToDateTime(objReader["FromDate"]) : DateTime.Now;
                        objEmployeDiscountDetailTypes.ToDate = objReader["ToDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ToDate"]) : DateTime.Now;
                        objEmployeDiscountDetailTypes.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;   
                      
                        
                        objEmployeeDiscountList.Add(objEmployeDiscountDetailTypes);
                    }
                    ResponseData.EmployeeDiscountDetailList = objEmployeeDiscountList;                   
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                                  

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Employee Discount");
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
            return objEmployeeDiscountList;
        }

        public List<FamilyDiscountDetailTypes> FamilyDiscountList(int DiscountID, BaseRequestType RequestObj)
        {
            var objFamilyDiscountList = new List<FamilyDiscountDetailTypes>();
            var RequestData = (SelectAllDiscountMasterRequest)RequestObj;
            var ResponseData = new SelectAllDiscountMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "SELECT * FROM [dbo].[FamilyDiscountDetails] WHERE DISCOUNTHEADERID=" + DiscountID;                
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objFamilyDiscountDetailTypes = new FamilyDiscountDetailTypes();
                        objFamilyDiscountDetailTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objFamilyDiscountDetailTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objFamilyDiscountDetailTypes.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : string.Empty;
                        objFamilyDiscountDetailTypes.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
                        objFamilyDiscountDetailTypes.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objFamilyDiscountList.Add(objFamilyDiscountDetailTypes);
                    }
                    ResponseData.FamilyDiscountDetailList = objFamilyDiscountList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Family Discount");
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
            return objFamilyDiscountList;
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
