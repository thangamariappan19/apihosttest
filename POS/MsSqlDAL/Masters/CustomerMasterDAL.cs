using EasyBizAbsDAL.Masters;
using EasyBizDBTypes;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CustomerMasterResponse;
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
    public class CustomerMasterDAL : BaseCustomerMasterDAL
    {


        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public SelectAllCustomerMasterRequest RequestObj { get; private set; }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveCustomerMasterRequest)RequestObj;
            var ResponseData = new SaveCustomerMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertCustomerMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.CustomerMasterData.StoreID);
                _CommandObj.Parameters.AddWithValue("@CustromeMasterID", RequestData.CustomerMasterData.ID); // enterprise server Id
                _CommandObj.Parameters.AddWithValue("@BaseID", RequestData.BaseID); // enterprise server Id
                _CommandObj.Parameters.AddWithValue("@CustomerCode", RequestData.CustomerMasterData.CustomerCode);
                _CommandObj.Parameters.AddWithValue("@CustomerName", RequestData.CustomerMasterData.CustomerName);
                _CommandObj.Parameters.AddWithValue("@PhoneNumber", RequestData.CustomerMasterData.PhoneNumber);
                _CommandObj.Parameters.AddWithValue("@AlterPhoneNumber", RequestData.CustomerMasterData.AlterPhoneNumber);
                _CommandObj.Parameters.AddWithValue("@CustomerGroupID", RequestData.CustomerMasterData.CustomerGroupID);
                _CommandObj.Parameters.AddWithValue("@BuildingAndBlockNo", RequestData.CustomerMasterData.BuildingAndBlockNo);
                _CommandObj.Parameters.AddWithValue("@StreetName", RequestData.CustomerMasterData.StreetName);
                _CommandObj.Parameters.AddWithValue("@AreaName1", RequestData.CustomerMasterData.AreaName1);
                _CommandObj.Parameters.AddWithValue("@AreaName2", RequestData.CustomerMasterData.AreaName2);
                _CommandObj.Parameters.AddWithValue("@BillingPhoneNumber", RequestData.CustomerMasterData.BillingPhoneNumber);

                _CommandObj.Parameters.AddWithValue("@City", RequestData.CustomerMasterData.City);
                _CommandObj.Parameters.AddWithValue("@Pincode", RequestData.CustomerMasterData.Pincode);
                _CommandObj.Parameters.AddWithValue("@StateID", RequestData.CustomerMasterData.StateID);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.CustomerMasterData.CountryID);
                _CommandObj.Parameters.AddWithValue("@Email", RequestData.CustomerMasterData.Email);
                _CommandObj.Parameters.AddWithValue("@IsoCode", RequestData.CustomerMasterData.IsoCode);


                if (RequestData.CustomerMasterData.DOB == Convert.ToDateTime("01 / 01 / 0001 12:00:00 AM"))
                {
                    _CommandObj.Parameters.AddWithValue("@DOB", DBNull.Value);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@DOB", RequestData.CustomerMasterData.DOB);
                }
                //01 / 01 / 0001 12:00:00 AM
                _CommandObj.Parameters.AddWithValue("@Gender", RequestData.CustomerMasterData.Gender);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CustomerMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CustomerMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@CreditAmount", RequestData.CustomerMasterData.CreditAmount);
                _CommandObj.Parameters.AddWithValue("@OnAccountApplicable", RequestData.CustomerMasterData.OnAccountApplicable);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CustomerMasterData.Active);

                _CommandObj.Parameters.AddWithValue("@CustomerGroupCode", RequestData.CustomerMasterData.CustomerGroupCode);
                _CommandObj.Parameters.AddWithValue("@StateCode", RequestData.CustomerMasterData.StateCode);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.CustomerMasterData.CountryCode);

                /*_CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.RunningNo);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.DocumentNumberingID);*/
                _CommandObj.Parameters.AddWithValue("@RunningNo", RequestData.CustomerMasterData.DocumentTypeID);
                _CommandObj.Parameters.AddWithValue("@DocumentNumberingID", RequestData.CustomerMasterData.DocumentNumberingID);
                _CommandObj.Parameters.AddWithValue("@CustomerImage", RequestData.CustomerMasterData.CustomerImage);

                _CommandObj.Parameters.AddWithValue("@StateName", RequestData.CustomerMasterData.StateName);
                // _CommandObj.Parameters.AddWithValue("@Pincode", RequestData.CustomerMasterData.Pincode);
                _CommandObj.Parameters.AddWithValue("@ShippingAddress1", RequestData.CustomerMasterData.ShippingAddress1);
                _CommandObj.Parameters.AddWithValue("@ShippingAddress2", RequestData.CustomerMasterData.ShippingAddress2);
                _CommandObj.Parameters.AddWithValue("@ShippingPhoneNumber", RequestData.CustomerMasterData.ShippingPhoneNumber);
                _CommandObj.Parameters.AddWithValue("@ShippingStateID", RequestData.CustomerMasterData.ShippingStateID);
                _CommandObj.Parameters.AddWithValue("@ShippingStateCode", RequestData.CustomerMasterData.ShippingStateCode);
                _CommandObj.Parameters.AddWithValue("@ShippingStateName", RequestData.CustomerMasterData.ShippingStateName);
                _CommandObj.Parameters.AddWithValue("@ShippingCity", RequestData.CustomerMasterData.ShippingCity);
                _CommandObj.Parameters.AddWithValue("@ShippingPincode", RequestData.CustomerMasterData.ShippingPincode);
                _CommandObj.Parameters.AddWithValue("@ShippingCountryCode", RequestData.CustomerMasterData.ShippingCountryCode);
                _CommandObj.Parameters.AddWithValue("@ShippingCountryID", RequestData.CustomerMasterData.ShippingCountryID);

                _CommandObj.Parameters.AddWithValue("@LoyalityPoint", RequestData.CustomerMasterData.LoyalityPoint);
                _CommandObj.Parameters.AddWithValue("@LoyalityValue", RequestData.CustomerMasterData.LoyalityValue);
                _CommandObj.Parameters.AddWithValue("@LoyalityGroup", RequestData.CustomerMasterData.LoyalityGroup);
                _CommandObj.Parameters.AddWithValue("@IsFCSync", RequestData.CustomerMasterData.IsFCSync);
                _CommandObj.Parameters.AddWithValue("@AddressIsoCode", RequestData.CustomerMasterData.AddressIsoCode);
                _CommandObj.Parameters.AddWithValue("@ShippingIsoCode", RequestData.CustomerMasterData.ShippingIsoCode);

                #region "New Fields"
                _CommandObj.Parameters.AddWithValue("@LastName", RequestData.CustomerMasterData.LastName);
                _CommandObj.Parameters.AddWithValue("@SubGroupID", RequestData.CustomerMasterData.SubGroupID);
                _CommandObj.Parameters.AddWithValue("@SubGroupCode", RequestData.CustomerMasterData.SubGroupCode);
                _CommandObj.Parameters.AddWithValue("@PaymentTermsDays", RequestData.CustomerMasterData.PaymentTermsDays);
                _CommandObj.Parameters.AddWithValue("@CreditDays", RequestData.CustomerMasterData.CreditDays);
                _CommandObj.Parameters.AddWithValue("@IsLoyalty", RequestData.CustomerMasterData.IsLoyalty);
                _CommandObj.Parameters.AddWithValue("@IsTaxExempt", RequestData.CustomerMasterData.IsTaxExempt);
                _CommandObj.Parameters.AddWithValue("@LoyaltyID", RequestData.CustomerMasterData.LoyaltyID);
                _CommandObj.Parameters.AddWithValue("@LoyaltyPlan", RequestData.CustomerMasterData.LoyaltyPlan);
                #endregion

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Customer Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Phone Number");
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
            var RequestData = (UpdateCustomerMasterRequest)RequestObj;
            var ResponseData = new UpdateCustomerMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdateCustomerMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CustomerMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@BaseID", RequestData.CustomerMasterData.BaseID);

                _CommandObj.Parameters.AddWithValue("@CustomerCode", RequestData.CustomerMasterData.CustomerCode);
                _CommandObj.Parameters.AddWithValue("@CustomerName", RequestData.CustomerMasterData.CustomerName);
                _CommandObj.Parameters.AddWithValue("@PhoneNumber", RequestData.CustomerMasterData.PhoneNumber);
                _CommandObj.Parameters.AddWithValue("@AlterPhoneNumber", RequestData.CustomerMasterData.AlterPhoneNumber);
                _CommandObj.Parameters.AddWithValue("@CustomerGroupID", RequestData.CustomerMasterData.CustomerGroupID);
                _CommandObj.Parameters.AddWithValue("@BuildingAndBlockNo", RequestData.CustomerMasterData.BuildingAndBlockNo);
                _CommandObj.Parameters.AddWithValue("@StreetName", RequestData.CustomerMasterData.StreetName);
                _CommandObj.Parameters.AddWithValue("@AreaName1", RequestData.CustomerMasterData.AreaName1);
                _CommandObj.Parameters.AddWithValue("@AreaName2", RequestData.CustomerMasterData.AreaName2);
                _CommandObj.Parameters.AddWithValue("@BillingPhoneNumber", RequestData.CustomerMasterData.BillingPhoneNumber);
                _CommandObj.Parameters.AddWithValue("@City", RequestData.CustomerMasterData.City);
                _CommandObj.Parameters.AddWithValue("@StateID", RequestData.CustomerMasterData.StateID);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.CustomerMasterData.CountryID);
                _CommandObj.Parameters.AddWithValue("@Email", RequestData.CustomerMasterData.Email);
                if (RequestData.CustomerMasterData.DOB == Convert.ToDateTime("01 / 01 / 0001 12:00:00 AM"))
                {
                    _CommandObj.Parameters.AddWithValue("@DOB", DBNull.Value);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@DOB", RequestData.CustomerMasterData.DOB);
                }
                _CommandObj.Parameters.AddWithValue("@Gender", RequestData.CustomerMasterData.Gender);
                _CommandObj.Parameters.AddWithValue("@RequestFrom", (int)RequestData.RequestFrom);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CustomerMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.CustomerMasterData.SCN);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CustomerMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@CreditAmount", RequestData.CustomerMasterData.CreditAmount);
                _CommandObj.Parameters.AddWithValue("@OnAccountApplicable", RequestData.CustomerMasterData.OnAccountApplicable);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CustomerMasterData.Active);

                _CommandObj.Parameters.AddWithValue("@CustomerGroupCode", RequestData.CustomerMasterData.CustomerGroupCode);
                _CommandObj.Parameters.AddWithValue("@StateCode", RequestData.CustomerMasterData.StateCode);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.CustomerMasterData.CountryCode);
                _CommandObj.Parameters.AddWithValue("@CustomerImage", RequestData.CustomerMasterData.CustomerImage);

                _CommandObj.Parameters.AddWithValue("@StateName", RequestData.CustomerMasterData.StateName);
                _CommandObj.Parameters.AddWithValue("@Pincode", RequestData.CustomerMasterData.Pincode);
                _CommandObj.Parameters.AddWithValue("@ShippingAddress1", RequestData.CustomerMasterData.ShippingAddress1);
                _CommandObj.Parameters.AddWithValue("@ShippingAddress2", RequestData.CustomerMasterData.ShippingAddress2);
                _CommandObj.Parameters.AddWithValue("@ShippingPhoneNumber", RequestData.CustomerMasterData.ShippingPhoneNumber);
                _CommandObj.Parameters.AddWithValue("@ShippingStateID", RequestData.CustomerMasterData.ShippingStateID);
                _CommandObj.Parameters.AddWithValue("@ShippingStateCode", RequestData.CustomerMasterData.ShippingStateCode);
                _CommandObj.Parameters.AddWithValue("@ShippingStateName", RequestData.CustomerMasterData.ShippingStateName);
                _CommandObj.Parameters.AddWithValue("@ShippingCity", RequestData.CustomerMasterData.ShippingCity);
                _CommandObj.Parameters.AddWithValue("@ShippingPincode", RequestData.CustomerMasterData.ShippingPincode);
                _CommandObj.Parameters.AddWithValue("@ShippingCountryCode", RequestData.CustomerMasterData.ShippingCountryCode);
                _CommandObj.Parameters.AddWithValue("@ShippingCountryID", RequestData.CustomerMasterData.ShippingCountryID);
                _CommandObj.Parameters.AddWithValue("@LoyalityPoint", RequestData.CustomerMasterData.LoyalityPoint);
                _CommandObj.Parameters.AddWithValue("@LoyalityValue", RequestData.CustomerMasterData.LoyalityValue);
                _CommandObj.Parameters.AddWithValue("@LoyalityGroup", (object)RequestData.CustomerMasterData.LoyalityGroup ?? DBNull.Value);
                _CommandObj.Parameters.AddWithValue("@IsFCSync", RequestData.CustomerMasterData.IsFCSync);
                _CommandObj.Parameters.AddWithValue("@IsoCode", RequestData.CustomerMasterData.IsoCode);
                _CommandObj.Parameters.AddWithValue("@AddressIsoCode", RequestData.CustomerMasterData.AddressIsoCode);
                _CommandObj.Parameters.AddWithValue("@ShippingIsoCode", RequestData.CustomerMasterData.ShippingIsoCode);

                #region "New Fields"
                _CommandObj.Parameters.AddWithValue("@LastName", RequestData.CustomerMasterData.LastName);
                _CommandObj.Parameters.AddWithValue("@SubGroupID", RequestData.CustomerMasterData.SubGroupID);
                _CommandObj.Parameters.AddWithValue("@SubGroupCode", RequestData.CustomerMasterData.SubGroupCode);
                _CommandObj.Parameters.AddWithValue("@PaymentTermsDays", RequestData.CustomerMasterData.PaymentTermsDays);
                _CommandObj.Parameters.AddWithValue("@CreditDays", RequestData.CustomerMasterData.CreditDays);
                _CommandObj.Parameters.AddWithValue("@IsLoyalty", RequestData.CustomerMasterData.IsLoyalty);
                _CommandObj.Parameters.AddWithValue("@IsTaxExempt", RequestData.CustomerMasterData.IsTaxExempt);
                _CommandObj.Parameters.AddWithValue("@LoyaltyID", RequestData.CustomerMasterData.LoyaltyID);
                _CommandObj.Parameters.AddWithValue("@LoyaltyPlan", RequestData.CustomerMasterData.LoyaltyPlan);
                #endregion

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Customer Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Customer Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Master");
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
            var CustomerMaster = new CustomerMaster();
            var RequestData = (DeleteCustomerMasterRequest)RequestObj;
            var ResponseData = new DeleteCustomerMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Delete from  CustomerMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.CustomerMasterData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Customer Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Customer Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var CustomerMaster = new CustomerMaster();
            var CustomerMasterList = new List<CustomerMaster>();
            var RequestData = (SelectByIDCustomerMasterRequest)RequestObj;
            var ResponseData = new SelectByIDCustomerMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            string sSql = "";
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sSql = "Select top 1 *,b.GroupName from CustomerMaster a with(nolock) inner join CustomerGroupMaster b with(nolock) on b.ID=a.CustomerGroupID ";

                if (RequestData.DocumentNos != null && RequestData.DocumentNos != string.Empty)
                {
                    sSql = sSql + " where a.CustomerCode='{0}'";
                    sSql = string.Format(sSql, RequestData.DocumentNos.Trim());
                }
                else if (RequestData.DocumentIDs != null && RequestData.DocumentIDs != string.Empty)
                {
                    sSql = sSql + " where a.ID={0}";
                    sSql = string.Format(sSql, Convert.ToInt64(RequestData.DocumentIDs));
                }
                else
                {
                    sSql = sSql + " where a.ID={0}";
                    sSql = string.Format(sSql, RequestData.ID);
                }

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    var objCustomerMaster = new CustomerMaster();

                    while (objReader.Read())
                    {
                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                        objCustomerMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objCustomerMaster.BuildingAndBlockNo = objReader["BuildingAndBlockNo"].ToString();
                        objCustomerMaster.StreetName = objReader["StreetName"].ToString();
                        objCustomerMaster.AreaName1 = objReader["AreaName1"].ToString();
                        objCustomerMaster.AreaName2 = objReader["AreaName2"].ToString();
                        objCustomerMaster.City = objReader["City"].ToString();
                        objCustomerMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        objCustomerMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objCustomerMaster.Email = objReader["Email"].ToString();
                        objCustomerMaster.DOB = objReader["DOB"] != DBNull.Value ? Convert.ToDateTime(objReader["DOB"]) : DateTime.Now;
                        objCustomerMaster.Gender = objReader["Gender"].ToString();
                        objCustomerMaster.GroupName = objReader["GroupName"].ToString();
                        //objCustomerMaster.CountryName = objReader["CountryName"].ToString();
                        //objCustomerMaster.StateName = objReader["StateName"].ToString();


                        objCustomerMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objCustomerMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerMaster.OnAccountApplicable = objReader["OnAccountApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAccountApplicable"]) : false;
                        objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerMaster.Remarks = objReader["Remarks"].ToString();
                        objCustomerMaster.CreditAmount = objReader["CreditAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CreditAmount"]) : 0;

                        objCustomerMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        objCustomerMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : string.Empty;
                        objCustomerMaster.CountryCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        objCustomerMaster.LoyalityPoint = objReader["LoyalityPoint"] != DBNull.Value ? Convert.ToDecimal(objReader["LoyalityPoint"]) : 0;
                        objCustomerMaster.LoyalityValue = objReader["LoyalityValue"] != DBNull.Value ? Convert.ToDecimal(objReader["LoyalityValue"]) : 0;
                        objCustomerMaster.LoyalityGroup = objReader["LoyalityGroup"] != DBNull.Value ? Convert.ToString(objReader["LoyalityGroup"]) : string.Empty;
                    }

                    ResponseData.CustomerMaster = objCustomerMaster;
                    ResponseData.ResponseDynamicData = objCustomerMaster;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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
            var CustomerMasterList = new List<CustomerMaster>();

            var RequestData = new SelectAllCustomerMasterRequest();
            var ResponseData = new SelectAllCustomerMasterResponse();

            RequestData = (SelectAllCustomerMasterRequest)RequestObj;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                if (RequestData.ID > 0)
                {
                    sSql = "Select top 1 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID left join CustomerAddress CA on a.ID=CA.CustomerID  where a.ID='{0}'";
                    sSql = string.Format(sSql, RequestData.ID);
                }
                else if (RequestData.Source == "Sales")
                {
                    sbSql.Append("Select top 10 *,b.GroupName,a.isoCode from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode='{0}' Or CustomerName='{1}' Or PhoneNumber='{2}'");
                    sSql = string.Format(sbSql.ToString(), RequestData.CustomerInfo, RequestData.CustomerInfo, RequestData.CustomerInfo);
                }
                else
                {
                    sbSql.Append("Select top 100 CM.*,CGM.GroupName  from CustomerMaster CM ");
                    sbSql.Append("left join CustomerGroupMaster CGM  on CM.CustomerGroupID=CGM.ID   ");
                    sbSql.Append("where CGM.Active='True'");
                    if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
                    {
                        sbSql.Append(" and CM.CustomerCode='" + RequestData.SearchString + "' Or CM.CustomerName='" + RequestData.SearchString + "' Or CM.PhoneNumber='" + RequestData.SearchString + "'");
                    }
                    sbSql.Append(" order by id  desc ");

                    sSql = sbSql.ToString();
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCustomerMaster = new CustomerMaster();

                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                        objCustomerMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objCustomerMaster.BuildingAndBlockNo = objReader["BuildingAndBlockNo"].ToString();
                        objCustomerMaster.StreetName = objReader["StreetName"].ToString();
                        objCustomerMaster.AreaName1 = objReader["AreaName1"].ToString();
                        objCustomerMaster.AreaName2 = objReader["AreaName2"].ToString();
                        objCustomerMaster.City = objReader["City"].ToString();
                        objCustomerMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        objCustomerMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objCustomerMaster.Email = objReader["Email"].ToString();
                        objCustomerMaster.DOB = objReader["DOB"] != DBNull.Value ? Convert.ToDateTime(objReader["DOB"]) : DateTime.MinValue;
                        objCustomerMaster.Gender = objReader["Gender"].ToString();
                        objCustomerMaster.GroupName = objReader["GroupName"].ToString();
                        //objCustomerMaster.StateName = objReader["StateName"].ToString();
                        // objCustomerMaster.CountryName = objReader["CountryName"].ToString();
                        objCustomerMaster.CustomerImage = objReader["CustomerImage"].ToString();
                        objCustomerMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCustomerMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerMaster.OnAccountApplicable = objReader["OnAccountApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAccountApplicable"]) : false;
                        objCustomerMaster.Remarks = objReader["Remarks"].ToString();
                        objCustomerMaster.CreditAmount = objReader["CreditAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CreditAmount"]) : 0;

                        objCustomerMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        objCustomerMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : string.Empty;
                        objCustomerMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;

                        #region "New Fields"
                        objCustomerMaster.LastName = objReader["LastName"] != DBNull.Value ? Convert.ToString(objReader["LastName"]) : string.Empty;
                        objCustomerMaster.SubGroupID = objReader["SubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["SubGroupID"]) : 0;
                        objCustomerMaster.SubGroupCode = objReader["SubGroupCode"] != DBNull.Value ? Convert.ToString(objReader["SubGroupCode"]) : string.Empty;
                        objCustomerMaster.PaymentTermsDays = objReader["PaymentTermsDays"] != DBNull.Value ? Convert.ToString(objReader["PaymentTermsDays"]) : string.Empty;
                        objCustomerMaster.CreditDays = objReader["CreditDays"] != DBNull.Value ? Convert.ToString(objReader["CreditDays"]) : string.Empty;
                        objCustomerMaster.IsLoyalty = objReader["IsLoyalty"] != DBNull.Value ? Convert.ToBoolean(objReader["IsLoyalty"]) : false;
                        objCustomerMaster.IsTaxExempt = objReader["IsTaxExempt"] != DBNull.Value ? Convert.ToBoolean(objReader["IsTaxExempt"]) : false;
                        objCustomerMaster.LoyaltyID = objReader["LoyaltyID"] != DBNull.Value ? Convert.ToString(objReader["LoyaltyID"]) : string.Empty;
                        objCustomerMaster.LoyaltyPlan = objReader["LoyaltyPlan"] != DBNull.Value ? Convert.ToString(objReader["LoyaltyPlan"]) : string.Empty;
                        #endregion



                        if (RequestData.ID > 0)
                        {
                            objCustomerMaster.BillingPhoneNumber = objReader["BillingPhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["BillingPhoneNumber"]) : string.Empty;
                            objCustomerMaster.Pincode = objReader["BillingPincode"] != DBNull.Value ? Convert.ToString(objReader["BillingPincode"]) : string.Empty;
                            objCustomerMaster.StateName = objReader["BillingStateName"] != DBNull.Value ? Convert.ToString(objReader["BillingStateName"]) : string.Empty;
                            objCustomerMaster.ShippingAddress1 = objReader["ShippingAddress1"] != DBNull.Value ? Convert.ToString(objReader["ShippingAddress1"]) : string.Empty;
                            objCustomerMaster.ShippingAddress2 = objReader["ShippingAddress2"] != DBNull.Value ? Convert.ToString(objReader["ShippingAddress2"]) : string.Empty;
                            objCustomerMaster.ShippingPhoneNumber = objReader["ShippingPhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["ShippingPhoneNumber"]) : string.Empty;
                            objCustomerMaster.ShippingStateID = objReader["ShippingStateID"] != DBNull.Value ? Convert.ToInt32(objReader["ShippingStateID"]) : 0;
                            objCustomerMaster.ShippingStateCode = objReader["ShippingStateCode"] != DBNull.Value ? Convert.ToString(objReader["ShippingStateCode"]) : string.Empty;
                            objCustomerMaster.ShippingStateName = objReader["ShippingStateName"] != DBNull.Value ? Convert.ToString(objReader["ShippingStateName"]) : string.Empty;
                            objCustomerMaster.ShippingCity = objReader["ShippingCity"] != DBNull.Value ? Convert.ToString(objReader["ShippingCity"]) : string.Empty;
                            objCustomerMaster.ShippingPincode = objReader["SippingPincode"] != DBNull.Value ? Convert.ToString(objReader["SippingPincode"]) : string.Empty;
                            objCustomerMaster.ShippingCountryID = objReader["ShippingCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["ShippingCountryID"]) : 0;
                            objCustomerMaster.ShippingCountryCode = objReader["ShippingCountryCode"] != DBNull.Value ? Convert.ToString(objReader["ShippingCountryCode"]) : string.Empty;
                            objCustomerMaster.IsoCode = objReader["isoCode"] != DBNull.Value ? Convert.ToString(objReader["isoCode"]) : string.Empty;
                            objCustomerMaster.AddressIsoCode = objReader["AddressIsoCode"] != DBNull.Value ? Convert.ToString(objReader["AddressIsoCode"]) : string.Empty;
                            objCustomerMaster.ShippingIsoCode = objReader["ShippingIsoCode"] != DBNull.Value ? Convert.ToString(objReader["ShippingIsoCode"]) : string.Empty;
                        }
                        objCustomerMaster.LoyalityPoint = objReader["LoyalityPoint"] != DBNull.Value ? Convert.ToDecimal(objReader["LoyalityPoint"]) : 0;
                        objCustomerMaster.LoyalityValue = objReader["LoyalityValue"] != DBNull.Value ? Convert.ToDecimal(objReader["LoyalityValue"]) : 0;
                        objCustomerMaster.LoyalityGroup = objReader["LoyalityGroup"] != DBNull.Value ? Convert.ToString(objReader["LoyalityGroup"]) : string.Empty;

                        CustomerMasterList.Add(objCustomerMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterData = CustomerMasterList;
                    ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectCustomerMasterLookUpResponse SelectCustomerMasterLookUp(SelectCustomerMasterLookUpRequest ObjRequest)
        {
            var CustomerMasterList = new List<CustomerMaster>();
            var RequestData = (SelectCustomerMasterLookUpRequest)ObjRequest;
            var ResponseData = new SelectCustomerMasterLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,CustomerName,CustomerCode,Phonenumber from CustomerMaster with(NoLock) where Active='true'";
                if (RequestData.ID != 0)
                {
                    sQuery = sQuery + " and CustomerGroupID='" + RequestData.ID + "'";
                }


                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerMaster = new CustomerMaster();
                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.CustomerName = Convert.ToString(objReader["CustomerName"]);
                        objCustomerMaster.CustomerCode = Convert.ToString(objReader["CustomerCode"]);
                        objCustomerMaster.PhoneNumber = Convert.ToString(objReader["PhoneNumber"]);
                        CustomerMasterList.Add(objCustomerMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterList = CustomerMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectCustomerByPhoneNoResponse SelectCustomerByPhoneNo(SelectCustomerByPhoneNoRequest RequestObj)
        {
            var CustomerMaster = new CustomerMaster();
            var CustomerMasterList = new List<CustomerMaster>();
            var RequestData = (SelectCustomerByPhoneNoRequest)RequestObj;
            var ResponseData = new SelectCustomerByPhoneNoResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            string sSql = "";
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sSql = "Select top 1 * from CustomerMaster with(nolock) ";


                sSql = sSql + " where PhoneNumber ='{0}'";
                sSql = string.Format(sSql, RequestData.PhoneNumber.Trim());



                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    var objCustomerMaster = new CustomerMaster();

                    while (objReader.Read())
                    {
                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                        objCustomerMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objCustomerMaster.BuildingAndBlockNo = objReader["BuildingAndBlockNo"].ToString();
                        objCustomerMaster.StreetName = objReader["StreetName"].ToString();
                        objCustomerMaster.AreaName1 = objReader["AreaName1"].ToString();
                        objCustomerMaster.AreaName2 = objReader["AreaName2"].ToString();
                        objCustomerMaster.City = objReader["City"].ToString();
                        objCustomerMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        objCustomerMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objCustomerMaster.Email = objReader["Email"].ToString();
                        objCustomerMaster.DOB = objReader["DOB"] != DBNull.Value ? Convert.ToDateTime(objReader["DOB"]) : DateTime.Now;
                        objCustomerMaster.Gender = objReader["Gender"].ToString();
                        //objCustomerMaster.GroupName = objReader["GroupName"].ToString();
                        //objCustomerMaster.CountryName = objReader["CountryName"].ToString();
                        //objCustomerMaster.StateName = objReader["StateName"].ToString();


                        objCustomerMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objCustomerMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerMaster.OnAccountApplicable = objReader["OnAccountApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAccountApplicable"]) : false;
                        objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerMaster.Remarks = objReader["Remarks"].ToString();
                        objCustomerMaster.CreditAmount = objReader["CreditAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CreditAmount"]) : 0;

                        objCustomerMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        objCustomerMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : string.Empty;
                        objCustomerMaster.CountryCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                    }

                    ResponseData.CustomerMaster = objCustomerMaster;
                    ResponseData.ResponseDynamicData = objCustomerMaster;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectAllCustomerMasterResponse GetCommonCustomerData(SelectAllCustomerMasterRequest RequestObj)
        {
            var CustomerMasterList = new List<CustomerMaster>();

            var RequestData = new SelectAllCustomerMasterRequest();
            var ResponseData = new SelectAllCustomerMasterResponse();

            RequestData = (SelectAllCustomerMasterRequest)RequestObj;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                // sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerName LIKE '" + RequestData.SearchString+'%' +"'");


                sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString(), RequestData.CustomerInfo, RequestData.CustomerInfo, RequestData.CustomerInfo);



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCustomerMaster = new CustomerMaster();

                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                        objCustomerMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        objCustomerMaster.BuildingAndBlockNo = objReader["BuildingAndBlockNo"].ToString();
                        objCustomerMaster.StreetName = objReader["StreetName"].ToString();
                        objCustomerMaster.AreaName1 = objReader["AreaName1"].ToString();
                        objCustomerMaster.AreaName2 = objReader["AreaName2"].ToString();
                        objCustomerMaster.City = objReader["City"].ToString();
                        objCustomerMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        objCustomerMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objCustomerMaster.Email = objReader["Email"].ToString();
                        objCustomerMaster.DOB = objReader["DOB"] != DBNull.Value ? Convert.ToDateTime(objReader["DOB"]) : DateTime.Now;
                        objCustomerMaster.Gender = objReader["Gender"].ToString();
                        objCustomerMaster.GroupName = objReader["GroupName"].ToString();
                        //objCustomerMaster.StateName = objReader["StateName"].ToString();
                        // objCustomerMaster.CountryName = objReader["CountryName"].ToString();

                        objCustomerMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCustomerMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerMaster.OnAccountApplicable = objReader["OnAccountApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAccountApplicable"]) : false;
                        objCustomerMaster.Remarks = objReader["Remarks"].ToString();
                        objCustomerMaster.CreditAmount = objReader["CreditAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CreditAmount"]) : 0;

                        objCustomerMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        objCustomerMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : string.Empty;
                        objCustomerMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;

                        CustomerMasterList.Add(objCustomerMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterData = CustomerMasterList;
                    //ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectAllCustomerMasterResponse GetCustomerSearchPOS(SelectAllCustomerMasterRequest requestData)
        {
            var CustomerMasterList = new List<CustomerMaster>();
            var RequestData = new SelectAllCustomerMasterRequest();
            var ResponseData = new SelectAllCustomerMasterResponse();

            RequestData = (SelectAllCustomerMasterRequest)requestData;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;
                sbSql.Append("Select top 1 a.ID,a.CustomerCode,a.CustomerName,a.PhoneNumber,a.CustomerGroupID,a.CustomerGroupCode,a.OnAccountApplicable " +
                    "from CustomerMaster a " +
                    "where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber = '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString(), RequestData.CustomerInfo, RequestData.CustomerInfo, RequestData.CustomerInfo);



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCustomerMaster = new CustomerMaster();

                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objCustomerMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        //objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                        //objCustomerMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        //objCustomerMaster.BuildingAndBlockNo = objReader["BuildingAndBlockNo"].ToString();
                        //objCustomerMaster.StreetName = objReader["StreetName"].ToString();
                        //objCustomerMaster.AreaName1 = objReader["AreaName1"].ToString();
                        //objCustomerMaster.AreaName2 = objReader["AreaName2"].ToString();
                        //objCustomerMaster.City = objReader["City"].ToString();
                        //objCustomerMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        //objCustomerMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        //objCustomerMaster.Email = objReader["Email"].ToString();
                        //objCustomerMaster.DOB = objReader["DOB"] != DBNull.Value ? Convert.ToDateTime(objReader["DOB"]) : DateTime.Now;
                        //objCustomerMaster.Gender = objReader["Gender"].ToString();
                        //objCustomerMaster.GroupName = objReader["GroupName"].ToString();
                        //objCustomerMaster.StateName = objReader["StateName"].ToString();
                        // objCustomerMaster.CountryName = objReader["CountryName"].ToString();

                        //objCustomerMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCustomerMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCustomerMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCustomerMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objCustomerMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCustomerMaster.OnAccountApplicable = objReader["OnAccountApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAccountApplicable"]) : false;
                        //objCustomerMaster.Remarks = objReader["Remarks"].ToString();
                        //objCustomerMaster.CreditAmount = objReader["CreditAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CreditAmount"]) : 0;

                        //objCustomerMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        //objCustomerMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : string.Empty;
                        //objCustomerMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;

                        CustomerMasterList.Add(objCustomerMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterData = CustomerMasterList;
                    //ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectAllCustomerMasterResponse API_SelectAll(SelectAllCustomerMasterRequest objRequest)
        {
            var CustomerMasterList = new List<CustomerMaster>();

            var RequestData = new SelectAllCustomerMasterRequest();
            var ResponseData = new SelectAllCustomerMasterResponse();

            RequestData = (SelectAllCustomerMasterRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                sSql = " Select 																			    \n" +
                        " 	CM.CustomerName,CM.ID,CM.CustomerCode,CM.Email,CM.PhoneNumber,CM.AlterPhoneNumber   \n" +
                        " 	,CM.CountryCode,CM.Active,CM.CustomerImage,CGM.GroupName,RC.TOTAL_CNT [RecordCount] \n" +
                        " from CustomerMaster CM  															    \n" +
                        " left join CustomerGroupMaster CGM on CM.CustomerGroupID=CGM.ID    				    \n" +
                        " LEFT JOIN (Select count(CM1.ID) As TOTAL_CNT 										    \n" +
                        " 			from CustomerMaster CM1  												    \n" +
                        " 			left join CustomerGroupMaster CGM1 on CM1.CustomerGroupID=CGM1.ID 		    \n" +
                        " 			where CM1.Active = " + RequestData.IsActive + "                             \n" +
                        "               and (isnull('" + RequestData.SearchString + "','') = ''                 \n" +
                        " 				or CM1.CustomerCode like isnull('%" + RequestData.SearchString + "%','') \n" +
                        " 				or CM1.CustomerName like isnull('%" + RequestData.SearchString + "%','')  \n" +
                        " 				or CM1.PhoneNumber like isnull('%" + RequestData.SearchString + "%','')) ) AS RC ON 1 = 1 \n" +
                        " where CM.Active = " + RequestData.IsActive + " and (isnull('sent','') = '' 			\n" +
                        " 	or CM.CustomerCode like isnull('%" + RequestData.SearchString + "%','') 			\n" +
                        " 	or CM.CustomerName like isnull('%" + RequestData.SearchString + "%','')				\n" +
                        " 	or CM.PhoneNumber like isnull('%" + RequestData.SearchString + "%','')) 			\n" +
                        " order by ID asc                                                                       \n" +
                        " offset " + RequestData.Offset + " rows fetch first " + RequestData.Limit + " rows only ";

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCustomerMaster = new CustomerMaster();

                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objCustomerMaster.BaseID = objReader["BaseID"] != DBNull.Value ? Convert.ToInt32(objReader["BaseID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        objCustomerMaster.AlterPhoneNumber = objReader["AlterPhoneNumber"].ToString();
                        //objCustomerMaster.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
                        //objCustomerMaster.BuildingAndBlockNo = objReader["BuildingAndBlockNo"].ToString();
                        //objCustomerMaster.StreetName = objReader["StreetName"].ToString();
                        //objCustomerMaster.AreaName1 = objReader["AreaName1"].ToString();
                        //objCustomerMaster.AreaName2 = objReader["AreaName2"].ToString();
                        // objCustomerMaster.City = objReader["City"].ToString();
                        //objCustomerMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        //objCustomerMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objCustomerMaster.Email = objReader["Email"].ToString();
                        //objCustomerMaster.DOB = objReader["DOB"] != DBNull.Value ? Convert.ToDateTime(objReader["DOB"]) : DateTime.Now;
                        //objCustomerMaster.Gender = objReader["Gender"].ToString();
                        objCustomerMaster.GroupName = objReader["GroupName"].ToString();
                        //objCustomerMaster.StateName = objReader["StateName"].ToString();
                        objCustomerMaster.CountryCode = objReader["CountryCode"].ToString();
                        // objCustomerMaster.CountryName = objReader["CountryName"].ToString();
                        objCustomerMaster.CustomerImage = objReader["CustomerImage"].ToString();
                        //objCustomerMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCustomerMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCustomerMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCustomerMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objCustomerMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objCustomerMaster.OnAccountApplicable = objReader["OnAccountApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["OnAccountApplicable"]) : false;
                        //objCustomerMaster.Remarks = objReader["Remarks"].ToString();
                        //objCustomerMaster.CreditAmount = objReader["CreditAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["CreditAmount"]) : 0;

                        // objCustomerMaster.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;
                        //objCustomerMaster.StateCode = objReader["StateCode"] != DBNull.Value ? Convert.ToString(objReader["StateCode"]) : string.Empty;
                        //objCustomerMaster.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        /*if (RequestData.ID > 0)
                        {
                            objCustomerMaster.Pincode = objReader["BillingPincode"] != DBNull.Value ? Convert.ToString(objReader["BillingPincode"]) : string.Empty;
                            objCustomerMaster.StateName = objReader["BillingStateName"] != DBNull.Value ? Convert.ToString(objReader["BillingStateName"]) : string.Empty;
                            objCustomerMaster.ShippingAddress1 = objReader["ShippingAddress1"] != DBNull.Value ? Convert.ToString(objReader["ShippingAddress1"]) : string.Empty;
                            objCustomerMaster.ShippingAddress2 = objReader["ShippingAddress2"] != DBNull.Value ? Convert.ToString(objReader["ShippingAddress2"]) : string.Empty;
                            objCustomerMaster.ShippingPhoneNumber = objReader["ShippingPhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["ShippingPhoneNumber"]) : string.Empty;
                            objCustomerMaster.ShippingStateID = objReader["ShippingStateID"] != DBNull.Value ? Convert.ToInt32(objReader["ShippingStateID"]) : 0;
                            objCustomerMaster.ShippingStateCode = objReader["ShippingStateCode"] != DBNull.Value ? Convert.ToString(objReader["ShippingStateCode"]) : string.Empty;
                            objCustomerMaster.ShippingStateName = objReader["ShippingStateName"] != DBNull.Value ? Convert.ToString(objReader["ShippingStateName"]) : string.Empty;
                            objCustomerMaster.ShippingCity = objReader["ShippingCity"] != DBNull.Value ? Convert.ToString(objReader["ShippingCity"]) : string.Empty;
                            objCustomerMaster.ShippingPincode = objReader["SippingPincode"] != DBNull.Value ? Convert.ToString(objReader["SippingPincode"]) : string.Empty;
                            objCustomerMaster.ShippingCountryID = objReader["ShippingCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["ShippingCountryID"]) : 0;
                            objCustomerMaster.ShippingCountryCode = objReader["ShippingCountryCode"] != DBNull.Value ? Convert.ToString(objReader["ShippingCountryCode"]) : string.Empty;
                        }*/
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                        CustomerMasterList.Add(objCustomerMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterData = CustomerMasterList;
                    //ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectAllCustomerMasterResponse GetCommonCustomerDetailsData(SelectAllCustomerMasterRequest RequestObj)
        {
            var CustomerMasterList = new List<CustomerMaster>();

            var RequestData = new SelectAllCustomerMasterRequest();
            var ResponseData = new SelectAllCustomerMasterResponse();

            RequestData = (SelectAllCustomerMasterRequest)RequestObj;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                sbSql.Append("select Top 1 ID,CustomerCode, CustomerName, PhoneNumber from CustomerMaster where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");

                //sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString(), RequestData.CustomerInfo, RequestData.CustomerInfo, RequestData.CustomerInfo);



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerMaster = new CustomerMaster();
                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        //  objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CustomerMasterList.Add(objCustomerMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterData = CustomerMasterList;
                    ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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

        public override SelectAllCustomerMasterResponse GetCommonCustomerDetailsDataID(SelectAllCustomerMasterRequest RequestObj)
        {
            var CustomerMasterList = new List<CustomerMaster>();

            var RequestData = new SelectAllCustomerMasterRequest();
            var ResponseData = new SelectAllCustomerMasterResponse();

            RequestData = (SelectAllCustomerMasterRequest)RequestObj;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                sbSql.Append("select Top 1 ID,CustomerCode, CustomerName, PhoneNumber from CustomerMaster where CustomerCode= '" + RequestData.SearchString + "'");

                //sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString(), RequestData.CustomerInfo, RequestData.CustomerInfo, RequestData.CustomerInfo);



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerMaster = new CustomerMaster();
                        objCustomerMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerMaster.CustomerCode = objReader["CustomerCode"].ToString();
                        objCustomerMaster.CustomerName = objReader["CustomerName"].ToString();
                        objCustomerMaster.PhoneNumber = objReader["PhoneNumber"].ToString();
                        //  objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CustomerMasterList.Add(objCustomerMaster);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerMasterData = CustomerMasterList;
                    ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Master");
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
            throw new NotImplementedException();
        }


        public override SelectAllCustomerSaleTransactionResponse API_CustomerSalesTransactionAll(SelectAllCustomerSalesTransactionRequest objRequest)
        {
            var CustomerSalesTransactionList = new List<CustomerViewTransactionTypes>();

            var RequestData = new SelectAllCustomerSalesTransactionRequest();
            var ResponseData = new SelectAllCustomerSaleTransactionResponse();

            RequestData = (SelectAllCustomerSalesTransactionRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                sbSql.Append("select businessDate as Date,InvoiceNo,Netamount,Receivedamount,CreateOn,(Netamount-Receivedamount) As Dueamount,CASE WHEN Netamount = Receivedamount THEN 'Completed' ELSE 'Paid' END AS Status from invoiceheader where CustomerID='" + RequestData.CustomerID + "' order By businessDate DESC ");

                //sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString());



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerSalesTransactionList = new CustomerViewTransactionTypes();
                        objCustomerSalesTransactionList.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objCustomerSalesTransactionList.Date = Convert.ToDateTime(objReader["Date"]);
                        objCustomerSalesTransactionList.NetAmount = Convert.ToDecimal(objReader["Netamount"]);
                        objCustomerSalesTransactionList.ReceivedAmount = Convert.ToDecimal(objReader["Receivedamount"]);
                        objCustomerSalesTransactionList.DueAmount = Convert.ToDecimal(objReader["Dueamount"]);
                        objCustomerSalesTransactionList.Status = Convert.ToString(objReader["Status"]);
                        objCustomerSalesTransactionList.CreateOn = Convert.ToDateTime(objReader["CreateOn"]);
                        //  objCustomerMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CustomerSalesTransactionList.Add(objCustomerSalesTransactionList);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerViewTransactionList = CustomerSalesTransactionList;
                    //ResponseData.ResponseDynamicData = CustomerMasterList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Sales Transaction");
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
            throw new NotImplementedException();
        }

        public override SelectAllCustomerSaleTransactionResponse API_CustomerReturnTransactionAll(SelectAllCustomerSalesTransactionRequest objRequest)
        {
            var CustomerReturnTransactionList = new List<CustomerViewTransactionTypes>();

            var RequestData = new SelectAllCustomerSalesTransactionRequest();
            var ResponseData = new SelectAllCustomerSaleTransactionResponse();

            RequestData = (SelectAllCustomerSalesTransactionRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

                sbSql.Append("select r.DocumentDate [Date], r.salesInvoiceNumber [InvoiceNo],TotalReturnAmount [ReturnAmount] ,r.CreateOn from SalesReturnHeader r inner join InvoiceHeader i on r.InvoiceHeaderID=i.ID where i.CustomerID='" + RequestData.CustomerID + "' order By r.DocumentDate DESC");

                //sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString());



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerReturnTransactionList = new CustomerViewTransactionTypes();
                        objCustomerReturnTransactionList.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objCustomerReturnTransactionList.Date = Convert.ToDateTime(objReader["Date"]);
                        objCustomerReturnTransactionList.CreateOn = Convert.ToDateTime(objReader["CreateOn"]);
                        objCustomerReturnTransactionList.ReturnAmount = Convert.ToDecimal(objReader["ReturnAmount"]);
                        CustomerReturnTransactionList.Add(objCustomerReturnTransactionList);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerViewTransactionList = CustomerReturnTransactionList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Return Transaction");
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
            throw new NotImplementedException();
        }

        public override SelectAllCustomerSaleTransactionResponse API_CustomerReturnExchange(SelectAllCustomerSalesTransactionRequest objRequest)
        {
            var CutomerViewReturnTransaction = new List<CutomerViewReturnTransaction>();

            var RequestData = new SelectAllCustomerSalesTransactionRequest();
            var ResponseData = new SelectAllCustomerSaleTransactionResponse();

            RequestData = (SelectAllCustomerSalesTransactionRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;
                var sbSql = new StringBuilder();

                var sSql = string.Empty;

               // sbSql.Append("select r.DocumentDate [Date], r.salesInvoiceNumber [InvoiceNo],TotalReturnAmount [ReturnAmount] from SalesReturnHeader r inner join InvoiceHeader i on r.InvoiceHeaderID=i.ID where i.CustomerID='" + RequestData.CustomerID + "'");

                sbSql.Append("select r.DocumentDate [Date], r.salesInvoiceNumber [InvoiceNo],0 [ReturnAmount],r.CreateOn from SalesExchangeHeader r inner join InvoiceHeader i on r.InvoiceHeaderID=i.ID where i.CustomerID='" + RequestData.CustomerID + "'");
                //sbSql.Append("Select top 10 *,b.GroupName from CustomerMaster a inner join CustomerGroupMaster b on b.ID=a.CustomerGroupID where CustomerCode= '" + RequestData.SearchString + "' Or CustomerName= '" + RequestData.SearchString + "' Or PhoneNumber= '" + RequestData.SearchString + "' ");
                sSql = string.Format(sbSql.ToString());



                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerReturnExchangeList = new CutomerViewReturnTransaction();
                        objCustomerReturnExchangeList.InvoiceNo = Convert.ToString(objReader["InvoiceNo"]);
                        objCustomerReturnExchangeList.Date = Convert.ToDateTime(objReader["Date"]);
                        objCustomerReturnExchangeList.CreateOn = Convert.ToDateTime(objReader["CreateOn"]);
                        objCustomerReturnExchangeList.ReturnAmount = Convert.ToDecimal(objReader["ReturnAmount"]);
                        CutomerViewReturnTransaction.Add(objCustomerReturnExchangeList);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CutomerViewReturnTransaction = CutomerViewReturnTransaction;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Return Exchange");
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
            throw new NotImplementedException();
        }
    }
}



