using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Coupons;
using EasyBizRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Transactions.Coupons;
using EasyBizResponse;
using EasyBizResponse.Masters.CouponMasterResponse;
using EasyBizResponse.Transactions.Coupons;
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
    public class CouponMasterDAL : BaseCouponMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveCouponMasterRequest)RequestObj;
            var ResponseData = new SaveCouponMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("[APIInsertCouponMaster]", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CouponMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@CouponCode", RequestData.CouponMasterData.CouponCode);
                _CommandObj.Parameters.AddWithValue("@Coupondescription", RequestData.CouponMasterData.Coupondescription);
                _CommandObj.Parameters.AddWithValue("@BarCode", RequestData.CouponMasterData.BarCode);
                _CommandObj.Parameters.AddWithValue("@CountryName", RequestData.CouponMasterData.Country);
                _CommandObj.Parameters.AddWithValue("@CouponType", RequestData.CouponMasterData.CouponType);
                _CommandObj.Parameters.AddWithValue("@StartDate", RequestData.CouponMasterData.StartDate);
                _CommandObj.Parameters.AddWithValue("@EndDate", RequestData.CouponMasterData.EndDate);
                _CommandObj.Parameters.AddWithValue("@DiscountType", RequestData.CouponMasterData.DiscountType);
                _CommandObj.Parameters.AddWithValue("@DiscountValue", RequestData.CouponMasterData.DiscountValue);
                _CommandObj.Parameters.AddWithValue("@IssuableAtPOS", RequestData.CouponMasterData.IssuableAtPOS);
                _CommandObj.Parameters.AddWithValue("@Serial", RequestData.CouponMasterData.Serial);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CouponMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CouponMasterData.Active);
                _CommandObj.Parameters.AddWithValue("@CouponSerialCode", RequestData.CouponMasterData.CouponSerialCode);
                //_CommandObj.Parameters.AddWithValue("@Issuedstatus", RequestData.CouponMasterData.Issuedstatus);
                _CommandObj.Parameters.AddWithValue("@PhysicalStore", RequestData.CouponMasterData.PhysicalStore);
                _CommandObj.Parameters.AddWithValue("@Remainingamount", RequestData.CouponMasterData.Remainingamount);
                _CommandObj.Parameters.AddWithValue("@Redeemedstatus", RequestData.CouponMasterData.Redeemedstatus);
                _CommandObj.Parameters.AddWithValue("@Issuedstatus", RequestData.CouponMasterData.Issuedstatus);

                _CommandObj.Parameters.AddWithValue("@IsExpirable", RequestData.CouponMasterData.IsCouponExpirable);
                _CommandObj.Parameters.AddWithValue("@MinBillAmount", RequestData.CouponMasterData.MinAmount);
                _CommandObj.Parameters.AddWithValue("@MaxNoIssue", RequestData.CouponMasterData.MaxCouponIssuePerDay);
                _CommandObj.Parameters.AddWithValue("@MaxLimit", RequestData.CouponMasterData.MaxLimitOfCoupon);
                _CommandObj.Parameters.AddWithValue("@RedeemType", RequestData.CouponMasterData.RedeemType);
                _CommandObj.Parameters.AddWithValue("@ExpiryDays", RequestData.CouponMasterData.CouponExpiresInNoOfDays);

                var StoreDataDetails = _CommandObj.Parameters.Add("@StoreDataDetails", SqlDbType.Xml);
                StoreDataDetails.Direction = ParameterDirection.Input;
                StoreDataDetails.Value = StoreMasterMasterXML(RequestData.StoreCommonUtilData);


                var CustomerDataDetails = _CommandObj.Parameters.Add("@CustomerUtilDataDetails", SqlDbType.Xml);
                CustomerDataDetails.Direction = ParameterDirection.Input;
                CustomerDataDetails.Value = CustomerMasterMasterXML(RequestData.CustomerCommonUtilData);

                var TotalDataDetails = _CommandObj.Parameters.Add("@TotalUtilDataDetails", SqlDbType.Xml);
                TotalDataDetails.Direction = ParameterDirection.Input;
                TotalDataDetails.Value = TotalMasterMasterXML(RequestData.TotalMasterCommonUtilData);

                var CouponDetailsList = _CommandObj.Parameters.Add("@CouponListDetails", SqlDbType.Xml);
                CouponDetailsList.Direction = ParameterDirection.Input;
                CouponDetailsList.Value = CouponListDetailsXML(RequestData.CouponDetailsList);


                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CouponMasterData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                //var CountryName = _CommandObj.Parameters.Add("@CountryName", SqlDbType.NVarChar);
                //CountryName.Direction = ParameterDirection.Input;
                //CountryName.Value = RequestData.CouponMasterData.CountryName;
                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;



                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
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
            var RequestData = (UpdateCouponMasterRequest)RequestObj;
            var ResponseData = new UpdateCouponMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("[UpdateCouponMaster1]", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CouponMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@CouponCode", RequestData.CouponMasterData.CouponCode);
                _CommandObj.Parameters.AddWithValue("@Coupondescription", RequestData.CouponMasterData.Coupondescription);
                _CommandObj.Parameters.AddWithValue("@BarCode", RequestData.CouponMasterData.BarCode);
                _CommandObj.Parameters.AddWithValue("@Country", RequestData.CouponMasterData.Country);
                _CommandObj.Parameters.AddWithValue("@CouponType", RequestData.CouponMasterData.CouponType);
                _CommandObj.Parameters.AddWithValue("@StartDate", RequestData.CouponMasterData.StartDate);
                _CommandObj.Parameters.AddWithValue("@EndDate", RequestData.CouponMasterData.EndDate);
                _CommandObj.Parameters.AddWithValue("@DiscountType", RequestData.CouponMasterData.DiscountType);
                _CommandObj.Parameters.AddWithValue("@DiscountValue", RequestData.CouponMasterData.DiscountValue);
                _CommandObj.Parameters.AddWithValue("@IssuableAtPOS", RequestData.CouponMasterData.IssuableAtPOS);
                _CommandObj.Parameters.AddWithValue("@Serial", RequestData.CouponMasterData.Serial);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CouponMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.CouponMasterData.Active);
                _CommandObj.Parameters.AddWithValue("@CouponSerialCode", RequestData.CouponMasterData.CouponSerialCode);
                _CommandObj.Parameters.AddWithValue("@Issuedstatus", RequestData.CouponMasterData.Issuedstatus);
                _CommandObj.Parameters.AddWithValue("@PhysicalStore", RequestData.CouponMasterData.PhysicalStore);
                _CommandObj.Parameters.AddWithValue("@Remainingamount", RequestData.CouponMasterData.Remainingamount);
                _CommandObj.Parameters.AddWithValue("@Redeemedstatus", RequestData.CouponMasterData.Redeemedstatus);

                //_CommandObj.Parameters.AddWithValue("@IsExpirable", RequestData.CouponMasterData.IsExpirable);
                //_CommandObj.Parameters.AddWithValue("@MinBillAmount", RequestData.CouponMasterData.MinBillAmount);
                //_CommandObj.Parameters.AddWithValue("@MaxNoIssue", RequestData.CouponMasterData.MaxNoIssue);
                //_CommandObj.Parameters.AddWithValue("@MaxLimit", RequestData.CouponMasterData.MaxLimit);
                _CommandObj.Parameters.AddWithValue("@RedeemType", RequestData.CouponMasterData.RedeemType);
                //_CommandObj.Parameters.AddWithValue("@ExpiryDays", RequestData.CouponMasterData.ExpiryDays);

                var StoreDataDetails = _CommandObj.Parameters.Add("@StoreDataDetails", SqlDbType.Xml);
                StoreDataDetails.Direction = ParameterDirection.Input;
                StoreDataDetails.Value = StoreMasterMasterXML(RequestData.StoreCommonUtilData);


                var CustomerDataDetails = _CommandObj.Parameters.Add("@CustomerUtilDataDetails", SqlDbType.Xml);
                CustomerDataDetails.Direction = ParameterDirection.Input;
                CustomerDataDetails.Value = CustomerMasterMasterXML(RequestData.CustomerCommonUtilData);

                var TotalDataDetails = _CommandObj.Parameters.Add("@TotalUtilDataDetails", SqlDbType.Xml);
                TotalDataDetails.Direction = ParameterDirection.Input;
                TotalDataDetails.Value = TotalMasterMasterXML(RequestData.TotalMasterCommonUtilData);

                var CouponDetailsList = _CommandObj.Parameters.Add("@CouponListDetails", SqlDbType.Xml);
                CouponDetailsList.Direction = ParameterDirection.Input;
                CouponDetailsList.Value = CouponListDetailsXML(RequestData.CouponDetailsList);

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CouponMasterData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public string StoreMasterMasterXML(List<CommonUtil> StoreMasterMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objScaleDetailMasterDetails in StoreMasterMasterList)
            {
                sSql.Append("<StoreDetailsMasterData>");
                sSql.Append("<ID>" + (objScaleDetailMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objScaleDetailMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objScaleDetailMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objScaleDetailMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + (objScaleDetailMasterDetails.TypeName) + "</Type>");
                sSql.Append("<Active>" + objScaleDetailMasterDetails.Active + "</Active>");
                sSql.Append("</StoreDetailsMasterData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public string CouponDetailsMasterXML(List<CommonUtil> CouponDetailsMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objScaleDetailMasterDetails in CouponDetailsMasterList)
            {
                sSql.Append("<CouponDetailsMasterXML>");
                sSql.Append("<ID>" + (objScaleDetailMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objScaleDetailMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objScaleDetailMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objScaleDetailMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + (objScaleDetailMasterDetails.TypeName) + "</Type>");
                sSql.Append("<Active>" + objScaleDetailMasterDetails.Active + "</Active>");
                sSql.Append("</CouponDetailsMasterXML>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public string CouponListDetailsXML(List<CouponListDetails> CouponListDetails)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (var objScaleListDetail in CouponListDetails)
            {
                sSql.Append("<CouponListDetailsXML>");
                sSql.Append("<CouponSerialCode>" + (objScaleListDetail.CouponSerialCode) + "</CouponSerialCode>");
                sSql.Append("<Issuedstatus>" + (objScaleListDetail.IssuedStatus) + "</Issuedstatus>");
                sSql.Append("<PhysicalStore>" + objScaleListDetail.PhysicalStore + "</PhysicalStore>");
                sSql.Append("<Remainingamount>" + (objScaleListDetail.RemainingAmount) + "</Remainingamount>");
                sSql.Append("<Redeemedstatus>" + (objScaleListDetail.RedeemedStatus) + "</Redeemedstatus>");
                sSql.Append("<LineNo>" + objScaleListDetail.LineNo + "</LineNo>");
                sSql.Append("<RedeemCount>" + objScaleListDetail.RedeemCount + "</RedeemCount>");
                sSql.Append("</CouponListDetailsXML>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }
        public string CustomerMasterMasterXML(List<CommonUtil> CommonUtilList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objCustomerMasterDetails in CommonUtilList)
            {
                sSql.Append("<CustomerMasterData>");
                sSql.Append("<ID>" + (objCustomerMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objCustomerMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objCustomerMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objCustomerMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + objCustomerMasterDetails.TypeName + "</Type>");
                sSql.Append("<Active>" + objCustomerMasterDetails.Active + "</Active>");
              
                sSql.Append("</CustomerMasterData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }


        public string TotalMasterMasterXML(List<CommonUtil> CommonUtilList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objTotalMasterMasterDetails in CommonUtilList)
            {
                sSql.Append("<TotalMasterMasterXML>");
                sSql.Append("<ID>" + (objTotalMasterMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objTotalMasterMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objTotalMasterMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objTotalMasterMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + objTotalMasterMasterDetails.TypeName + "</Type>");
                sSql.Append("<Active>" + objTotalMasterMasterDetails.Active + "</Active>");
               
                sSql.Append("</TotalMasterMasterXML>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var CouponMasterRecord = new CouponMaster();

            var RequestData = (DeleteCouponMasterRequest)RequestObj;
            var ResponseData = new DeleteCouponMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "delete from CouponApplicablelist where  CouponID='{0}';delete from CouponCustomerDetails where  CouponID='{0}';delete from CouponStoreMasterDetails where  CouponID='{0}';delete from CouponListDetails where  CouponID='{0}';delete from CouponMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Coupon Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Coupon Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var CouponMasterRecord = new CouponMaster();
            var RequestData = (SelectByIDCouponMasterRequest)RequestObj;
            var ResponseData = new SelectByIDCouponMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from CouponMaster CM with(NoLock) Inner Join CouponListDetails CD on CM.ID = CD.CouponID   where  CM.ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                         objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;                           
                        objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;        
                           
                        objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponMaster.CouponSerialCode = Convert.ToString(objReader["CouponSerialCode"]);
                        objCouponMaster.PhysicalStore = Convert.ToString(objReader["PhysicalStore"]);
                        objCouponMaster.Remainingamount = Convert.ToInt64(objReader["Remainingamount"]);
                        objCouponMaster.Redeemedstatus = Convert.ToString(objReader["Redeemedstatus"]);

                        //objCouponMaster.IsExpirable = Convert.ToInt32(objReader["IsExpirable"])==1?true:false;
                        //objCouponMaster.MaxLimit = Convert.ToDecimal(objReader["MaxLimit"]);
                        //objCouponMaster.MaxNoIssue = Convert.ToInt32(objReader["MaxNoIssue"]);
                        //objCouponMaster.MinBillAmount = Convert.ToDecimal   (objReader["MinBillAmount"]);
                        objCouponMaster.RedeemType = Convert.ToString(objReader["RedeemType"]);
                        //objCouponMaster.ExpiryDays = Convert.ToInt32(objReader["ExpiryDays"]);

                        // Changed by Senthamil @ 03.10.2018

                        var objCouponListDetails = new CouponMaster();
                        var objSelectCouponCouponListDetailsRequest = new SelectCouponCouponListDetailsRequest();
                        objSelectCouponCouponListDetailsRequest.CouponID = RequestData.ID;
                        var objSelectCouponCouponListDetailsResponse = new SelectCouponCouponListDetailsResponse();
                        objSelectCouponCouponListDetailsResponse = SelectCouponMasterList(objSelectCouponCouponListDetailsRequest);
                        if(objSelectCouponCouponListDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCouponListDetails = objSelectCouponCouponListDetailsResponse.CouponMasterListDetails;
                            objCouponMaster.Redeemedstatus = objCouponListDetails.Redeemedstatus;
                            objCouponMaster.Remainingamount = objCouponListDetails.Remainingamount;
                            objCouponMaster.PhysicalStore = objCouponListDetails.PhysicalStore;
                            objCouponMaster.Issuedstatus = objCouponListDetails.Issuedstatus;
                            objCouponMaster.CouponSerialCode = objCouponListDetails.CouponSerialCode;
                            //objCouponMaster.Remarks = objCouponListDetails.Remarks;
                        }

                        //objCouponMaster.Redeemedstatus = objReader["Redeemedstatus"] != DBNull.Value ? Convert.ToString(objReader["Redeemedstatus"]) : "";
                        //objCouponMaster.Remainingamount = objReader["Remainingamount"] != DBNull.Value ? Convert.ToDecimal(objReader["Remainingamount"]) : 0;
                        //objCouponMaster.PhysicalStore = objReader["PhysicalStore"] != DBNull.Value ? Convert.ToString(objReader["PhysicalStore"]) : "";
                        //objCouponMaster.Issuedstatus = objReader["Issuedstatus"] != DBNull.Value ? Convert.ToString(objReader["Issuedstatus"]) : "";
                        //objCouponMaster.CouponSerialCode = objReader["CouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["CouponSerialCode"]) : "";
                        objCouponMaster.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";


                        List<CommonUtil> StoreCommonUtilData = new List<CommonUtil>();
                        List<CommonUtil> CustomerCommonUtilData = new List<CommonUtil>();
                        List<CommonUtil> TotalMasterCommonUtilData = new List<CommonUtil>();

                        var objSelectCouponStoreDetailsRequest = new SelectCouponStoreDetailsRequest();
                        var objSelectCouponStoreDetailsResponse = new SelectCouponStoreDetailsResponse();
                        objSelectCouponStoreDetailsRequest.DetailsType = Enums.SpecialPriceRecordType.Store;
                        objSelectCouponStoreDetailsRequest.CouponID = RequestData.ID;
                        objSelectCouponStoreDetailsResponse = SelectCouponMasterStoreType(objSelectCouponStoreDetailsRequest);
                        if (objSelectCouponStoreDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            StoreCommonUtilData = objSelectCouponStoreDetailsResponse.StoreCommonUtil;
                        }

                        objSelectCouponStoreDetailsRequest = new SelectCouponStoreDetailsRequest();
                        objSelectCouponStoreDetailsResponse = new SelectCouponStoreDetailsResponse();
                        objSelectCouponStoreDetailsRequest.DetailsType = Enums.SpecialPriceRecordType.Customer;
                        objSelectCouponStoreDetailsRequest.CouponID = RequestData.ID;
                        objSelectCouponStoreDetailsResponse = SelectCouponMasterStoreType(objSelectCouponStoreDetailsRequest);
                        if (objSelectCouponStoreDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            CustomerCommonUtilData = objSelectCouponStoreDetailsResponse.StoreCommonUtil;
                        }

                        objSelectCouponStoreDetailsRequest = new SelectCouponStoreDetailsRequest();
                        objSelectCouponStoreDetailsResponse = new SelectCouponStoreDetailsResponse();
                        objSelectCouponStoreDetailsRequest.DetailsType = Enums.SpecialPriceRecordType.Category;
                        objSelectCouponStoreDetailsRequest.CouponID = RequestData.ID;
                        objSelectCouponStoreDetailsResponse = SelectCouponMasterStoreType(objSelectCouponStoreDetailsRequest);
                        if (objSelectCouponStoreDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            TotalMasterCommonUtilData = objSelectCouponStoreDetailsResponse.StoreCommonUtil;
                        }

                        ResponseData.CouponMasterRecord = objCouponMaster;                        
                        ResponseData.ResponseDynamicData = objCouponMaster;

                        ResponseData.CouponMasterRecord.StoreCommonUtilData = StoreCommonUtilData;
                        ResponseData.CouponMasterRecord.CustomerCommonUtilData = CustomerCommonUtilData;
                        ResponseData.CouponMasterRecord.TotalMasterCommonUtilData = TotalMasterCommonUtilData;

                        ResponseData.ResponseDynamicData.StoreCommonUtilData = StoreCommonUtilData;
                        ResponseData.ResponseDynamicData.CustomerCommonUtilData = CustomerCommonUtilData;
                        ResponseData.ResponseDynamicData.TotalMasterCommonUtilData = TotalMasterCommonUtilData;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
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
            var CouponMasterList = new List<CouponMaster>();
            var RequestData = (SelectAllCouponMasterRequest)RequestObj;
            var ResponseData = new SelectAllCouponMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from CouponMaster with(NoLock)";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.CouponName = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;
                        objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CouponMasterList.Add(objCouponMaster);
                        
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponMasterList = CouponMasterList;
                    ResponseData.ResponseDynamicData = CouponMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
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
            var CouponMasterList = new List<CouponMaster>();
            var RequestData = (SelectByIDsCouponMasterRequest)RequestObj;
            var ResponseData = new SelectByIDsCouponMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from CouponMaster with(NoLock) where  ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.IDs);

               
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;
                        objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCouponMaster.CouponSerialCode = Convert.ToString(objReader["CouponSerialCode"]);
                        objCouponMaster.PhysicalStore = Convert.ToString(objReader["PhysicalStore"]);
                        objCouponMaster.Remainingamount = Convert.ToInt64(objReader["Remainingamount"]);
                        objCouponMaster.Redeemedstatus = Convert.ToString(objReader["Redeemedstatus"]);
                        CouponMasterList.Add(objCouponMaster);
                      
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponMasterList = CouponMasterList;
                    //ResponseData.ResponseDynamicData = CouponMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
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

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectCouponMasterLookUpResponse SelectCouponMasterLookUp(SelectCouponMasterLookUpRequest RequestObj)
        {
            var CouponMasterList = new List<CouponMaster>();


            SelectCouponMasterLookUpRequest RequestData = new SelectCouponMasterLookUpRequest();
            SelectCouponMasterLookUpResponse ResponseData = new SelectCouponMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,CouponCode,Coupondescription CouponName from CouponMaster with(NoLock) where Active='True'";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.CouponName = Convert.ToString(objReader["CouponName"]);
                        CouponMasterList.Add(objCouponMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponMasterList = CouponMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
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

        public override SelectCouponStoreDetailsResponse SelectCouponMasterStoreType(SelectCouponStoreDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new List<CommonUtil>();
          
            var RequestData = (SelectCouponStoreDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponStoreDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                if (RequestData.DetailsType == Enums.SpecialPriceRecordType.Store)
                {
                    sSql.Append("select * from  CouponStoreMasterDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " ");
                }
                else if (RequestData.DetailsType == Enums.SpecialPriceRecordType.Customer)
                {
                    sSql.Append("select * from  CouponCustomerDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " ");
                }
                else if (RequestData.DetailsType == Enums.SpecialPriceRecordType.Category)
                {
                    sSql.Append("select * from  CouponApplicablelist ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " ");
                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

               
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objStyleWithColorDetailMaster = new CommonUtil();
                            objStyleWithColorDetailMaster.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                            objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objStyleWithColorDetailMaster.DocumentID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) : 0;
                            objStyleWithColorDetailMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                            objStyleWithColorDetailMaster.DocumentName = Convert.ToString(objReader["Name"]);
                            objStyleWithColorDetailMaster.TypeName = Convert.ToString(objReader["Type"]);
                            objStyleWithColorDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                            objStyleWithColorDetailMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                            StoreMasterDetailsMasterList.Add(objStyleWithColorDetailMaster);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.StoreCommonUtil = StoreMasterDetailsMasterList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
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

        public override SelectCouponCustomerDetailsResponse SelectCouponMasterCustomerType(SelectCouponCustomerDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new List<CommonUtil>();           
            var RequestData = (SelectCouponCustomerDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponCustomerDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                if (RequestData.CouponCustomerType != "")
                {
                    sSql.Append("select * from  CouponCustomerDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " and  Type='" + RequestData.CouponCustomerType + "'");
                }
                else
                {
                    sSql.Append("select * from  CouponCustomerDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " ");
                }
              
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new CommonUtil();
                        objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleWithColorDetailMaster.DocumentID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) : 0;
                        objStyleWithColorDetailMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objStyleWithColorDetailMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objStyleWithColorDetailMaster.TypeName = Convert.ToString(objReader["Type"]);
                        objStyleWithColorDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleWithColorDetailMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                        StoreMasterDetailsMasterList.Add(objStyleWithColorDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerCommonUtil = StoreMasterDetailsMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
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

        public override SelectCouponProductCategoryDetailsResponse SelectCouponMasterProductType(SelectCouponProductCategoryDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new List<CommonUtil>();        
            var RequestData = (SelectCouponProductCategoryDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponProductCategoryDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                if (RequestData.CouponProductCategoryType!="")
                {
                    sSql.Append("select * from  CouponApplicablelist ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " and  Type='" + RequestData.CouponProductCategoryType + "'");
                }
                else
                {
                    sSql.Append("select * from  CouponApplicablelist ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + "");
                }
              

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new CommonUtil();
                        objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleWithColorDetailMaster.DocumentID = objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) : 0;
                        objStyleWithColorDetailMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objStyleWithColorDetailMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objStyleWithColorDetailMaster.TypeName = Convert.ToString(objReader["Type"]);
                        objStyleWithColorDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleWithColorDetailMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                        StoreMasterDetailsMasterList.Add(objStyleWithColorDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductCategoryCommonUtil = StoreMasterDetailsMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
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

        public override SelectCouponCouponListDetailsResponse SelectCouponMasterList(SelectCouponCouponListDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new CouponMaster();
            var RequestData = (SelectCouponCouponListDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponCouponListDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from  CouponListDetails ");
                sSql.Append("where  CouponID=" + RequestData.CouponID + "");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new CouponMaster();
                        objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleWithColorDetailMaster.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        //objScaleDetailMaster.ScaleHeaderID = Convert.ToInt32(objReader["ScaleHeaderID"]);
                        objStyleWithColorDetailMaster.CouponSerialCode = objReader["CouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["CouponSerialCode"]) : "";
                        objStyleWithColorDetailMaster.Issuedstatus = objReader["Issuedstatus"] != DBNull.Value ? Convert.ToString(objReader["Issuedstatus"]) : "";
                        objStyleWithColorDetailMaster.PhysicalStore = objReader["PhysicalStore"] != DBNull.Value ? Convert.ToString(objReader["PhysicalStore"]) : "";
                        objStyleWithColorDetailMaster.Remainingamount = objReader["Remainingamount"] != DBNull.Value ? Convert.ToDecimal(objReader["Remainingamount"]) : 0;
                        objStyleWithColorDetailMaster.Redeemedstatus = objReader["Redeemedstatus"] != DBNull.Value ? Convert.ToString(objReader["Redeemedstatus"]) : "";
                        //objStyleWithColorDetailMaster.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";
                        ResponseData.CouponMasterListDetails = objStyleWithColorDetailMaster;
                       
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    
                   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
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

        
        public override SelectAllCouponMasterResponse API_SelectALL(SelectAllCouponMasterRequest RequestObj)
        {
            var CouponMasterList = new List<CouponMaster>();
            var RequestData = (SelectAllCouponMasterRequest)RequestObj;
            var ResponseData = new SelectAllCouponMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from CouponMaster with(NoLock)";
                string sSql = "select ID, CouponCode,Coupondescription, Active, RC.TOTAL_CNT [RecordCount] " +
                  "from CouponMaster with(NoLock) " +
                   "LEFT JOIN(Select  count(CM.ID) As TOTAL_CNT From CouponMaster CM with(NoLock) " +
                   "where CM.Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or CM.CouponCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or CM.Coupondescription like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                  "where Active = " + RequestData.IsActive + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or CouponCode like isnull('%" + RequestData.SearchString + "%','') " +
                          "or Coupondescription like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.CouponName = Convert.ToString(objReader["Coupondescription"]);
                        //objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        //objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        //objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        //objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        //objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        //objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        //objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        //objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        //objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;
                        //objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        //objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        //objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        // objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CouponMasterList.Add(objCouponMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponMasterList = CouponMasterList;
                    ResponseData.ResponseDynamicData = CouponMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
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

        #region "Redeem_Coupon"

        public override SelectCouponDataOnCouponCodeResponse SelectCouponDataOnCouponCode(SelectCouponDataOnCouponCodeRequest objRequest)
        {
            var CouponMasterRecord = new CouponMaster();
            var RequestData = (SelectCouponDataOnCouponCodeRequest)objRequest;
            var ResponseData = new SelectCouponDataOnCouponCodeResponse();
            List<CouponListDetails> tempCouponList = new List<CouponListDetails>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //ResponseData.CouponMasterRecord = new CouponMaster();

                string sSql = "select * from CouponListDetails where CouponSerialCode ='{0}' ";
                sSql = string.Format(sSql, RequestData.CouponCode);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();



                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var Temp_CouponListDetails = new List<CouponListDetails>();
                        //ResponseData.CouponMasterRecord.ObjCouponListDetails = new List<CouponListDetails>();
                        var objCouponListMaster = new CouponListDetails();
                        objCouponListMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponListMaster.CouponListHeaderID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        //objScaleDetailMaster.ScaleHeaderID = Convert.ToInt32(objReader["ScaleHeaderID"]);
                        objCouponListMaster.CouponSerialCode = objReader["CouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["CouponSerialCode"]) : "";
                        objCouponListMaster.IssuedStatus = objReader["IssuedStatus"] != DBNull.Value ? Convert.ToString(objReader["IssuedStatus"]) : "";
                        objCouponListMaster.PhysicalStore = objReader["PhysicalStore"] != DBNull.Value ? Convert.ToString(objReader["PhysicalStore"]) : "";
                        objCouponListMaster.RemainingAmount = objReader["Remainingamount"] != DBNull.Value ? Convert.ToString(objReader["Remainingamount"]) : "0";
                        objCouponListMaster.RedeemedStatus = objReader["Redeemedstatus"] != DBNull.Value ? Convert.ToString(objReader["Redeemedstatus"]) : "";
                        //objStyleWithColorDetailMaster.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";
                        objCouponListMaster.LineNo = objReader["LineNo"] != DBNull.Value ? Convert.ToInt32(objReader["LineNo"]) : 0;
                        objCouponListMaster.RedeemCount = objReader["RedeemCount"] != DBNull.Value ? Convert.ToInt32(objReader["RedeemCount"]) : 0;
                        objCouponListMaster.ExpiredDate = objReader["ExpiredDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ExpiredDate"]) : Convert.ToDateTime("01-01-1800");
                        objCouponListMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        //objReader["DateofJoining"] != DBNull.Value ? Convert.ToDateTime(objReader["DateofJoining"]) : DateTime.Now;
                        //ResponseData.CouponMasterRecord.ObjCouponListDetails.Add(objCouponListMaster);
                        Temp_CouponListDetails.Add(objCouponListMaster);


                        //var CouponMasterRecord = new CouponMaster();
                        /*var RequestData = (SelectByIDCouponMasterRequest)RequestObj;
                        var ResponseData = new SelectByIDCouponMasterResponse();
                        List<CouponListDetails> tempCouponList = new List<CouponListDetails>();*/

                        var objCouponListDetails = new CouponMaster();
                        //ResponseData.CouponMasterRecord.ObjCouponListDetails = new List<CouponListDetails>();
                        var objSelectCouponCouponListDetailsRequest = new SelectByIDCouponMasterRequest();
                        objSelectCouponCouponListDetailsRequest.ID = objCouponListMaster.CouponListHeaderID;
                        objSelectCouponCouponListDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        var objSelectCouponCouponListDetailsResponse = new SelectByIDCouponMasterResponse();
                        objSelectCouponCouponListDetailsResponse = SelectCouponDataBasedOnCouponID(objSelectCouponCouponListDetailsRequest);
                        if (objSelectCouponCouponListDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSelectCouponCouponListDetailsResponse.CouponMasterRecord.ObjCouponListDetails = new List<CouponListDetails>();
                            objSelectCouponCouponListDetailsResponse.CouponMasterRecord.ObjCouponListDetails.Add(objCouponListMaster);
                            objCouponListDetails = objSelectCouponCouponListDetailsResponse.CouponMasterRecord;
                            ResponseData.CouponMasterRecord = new CouponMaster();
                            ResponseData.CouponMasterRecord = objCouponListDetails;
                            //ResponseData.CouponMasterRecord.ObjCouponListDetails.Add(objCouponListDetails);
                            /*tempCouponList = objCouponListDetails.ObjCouponListDetails;//objSelectCouponCouponListDetailsResponse.CouponMasterListDetails.ObjCouponListDetails;
                            objCouponMaster.Redeemedstatus = objCouponListDetails.Redeemedstatus;
                            objCouponMaster.Remainingamount = objCouponListDetails.Remainingamount;
                            objCouponMaster.PhysicalStore = objCouponListDetails.PhysicalStore;
                            objCouponMaster.Issuedstatus = objCouponListDetails.Issuedstatus;
                            objCouponMaster.CouponSerialCode = objCouponListDetails.CouponSerialCode;
                            objCouponMaster.Remarks = objCouponListDetails.Remarks;*/
                        }

                        //var objCouponMaster = new CouponMaster();
                        //objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        //objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        //objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        //objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        //objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        //objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        //objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        //objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        //objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        //objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        //objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;

                        //objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        //objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        //objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //objCouponMaster.MaxNumberOfUsagePerCoupon = objReader["MaxNumberOfUsagePerCoupon"] != DBNull.Value ? Convert.ToInt32(objReader["MaxNumberOfUsagePerCoupon"]) : 0;
                        //objCouponMaster.ISCouponMulitpleApply = objReader["ISCouponMulitpleApply"] != DBNull.Value ? Convert.ToBoolean(objReader["ISCouponMulitpleApply"]) : true;
                        //objCouponMaster.IsPartialRedeemAllowed = objReader["IsPartialRedeemAllowed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsPartialRedeemAllowed"]) : true;
                        //objCouponMaster.IsCouponManual = objReader["IsCouponManual"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCouponManual"]) : true;
                        //objCouponMaster.IsLimitedPeriodOffer = objReader["IsLimitedPeriodOffer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsLimitedPeriodOffer"]) : true;
                        //objCouponMaster.IsCouponExpirable = objReader["IsCouponExpirable"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCouponExpirable"]) : true;

                        //objCouponMaster.MinAmount = objReader["MinAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MinAmount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }


                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master Data");
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

        public override SelectByIDCouponMasterResponse SelectCouponDataBasedOnCouponID(SelectByIDCouponMasterRequest RequestObj)
        {
            var CouponMasterRecord = new CouponMaster();
            var RequestData = (SelectByIDCouponMasterRequest)RequestObj;
            var ResponseData = new SelectByIDCouponMasterResponse();
            List<CouponListDetails> tempCouponList = new List<CouponListDetails>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from CouponMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;

                        objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objCouponMaster.MaxNumberOfUsagePerCoupon = objReader["MaxNumberOfUsagePerCoupon"] != DBNull.Value ? Convert.ToInt32(objReader["MaxNumberOfUsagePerCoupon"]) : 0;
                        objCouponMaster.ISCouponMulitpleApply = objReader["ISCouponMulitpleApply"] != DBNull.Value ? Convert.ToBoolean(objReader["ISCouponMulitpleApply"]) : true;
                        objCouponMaster.IsPartialRedeemAllowed = objReader["IsPartialRedeemAllowed"] != DBNull.Value ? Convert.ToBoolean(objReader["IsPartialRedeemAllowed"]) : true;
                        objCouponMaster.IsCouponManual = objReader["IsCouponManual"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCouponManual"]) : true;
                        objCouponMaster.IsLimitedPeriodOffer = objReader["IsLimitedPeriodOffer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsLimitedPeriodOffer"]) : true;
                        objCouponMaster.IsCouponExpirable = objReader["IsCouponExpirable"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCouponExpirable"]) : true;

                        objCouponMaster.MaxCouponIssuePerDay = objReader["MaxCouponToBeIssuedPerDay"] != DBNull.Value ? Convert.ToInt32(objReader["MaxCouponToBeIssuedPerDay"]) : 0;
                        objCouponMaster.MaxLimitOfCoupon = objReader["MaxLimitOfCoupon"] != DBNull.Value ? Convert.ToInt32(objReader["MaxLimitOfCoupon"]) : 0;
                        objCouponMaster.RedeemType = Convert.ToString(objReader["RedeemType"]);
                        objCouponMaster.CouponExpiresInNoOfDays = objReader["CouponExpiresInNoOfDays"] != DBNull.Value ? Convert.ToInt32(objReader["CouponExpiresInNoOfDays"]) : 0;

                        objCouponMaster.MinAmount = objReader["MinAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MinAmount"]) : 0;

                        // Changed by Senthamil @ 03.10.2018

                        //var objCouponListDetails = new CouponMaster();
                        ////ResponseData.CouponMasterRecord.ObjCouponListDetails = new List<CouponListDetails>();
                        //var objSelectCouponCouponListDetailsRequest = new SelectCouponCouponListDetailsRequest();
                        //objSelectCouponCouponListDetailsRequest.CouponID = RequestData.ID;
                        //var objSelectCouponCouponListDetailsResponse = new SelectCouponCouponListDetailsResponse();
                        //objSelectCouponCouponListDetailsResponse = SelectCouponMasterList(objSelectCouponCouponListDetailsRequest);
                        //if (objSelectCouponCouponListDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objCouponListDetails = objSelectCouponCouponListDetailsResponse.CouponMasterListDetails;
                        //    //ResponseData.CouponMasterRecord.ObjCouponListDetails.Add(objCouponListDetails);
                        //    tempCouponList = objCouponListDetails.ObjCouponListDetails;//objSelectCouponCouponListDetailsResponse.CouponMasterListDetails.ObjCouponListDetails;
                        //    objCouponMaster.Redeemedstatus = objCouponListDetails.Redeemedstatus;
                        //    objCouponMaster.Remainingamount = objCouponListDetails.Remainingamount;
                        //    objCouponMaster.PhysicalStore = objCouponListDetails.PhysicalStore;
                        //    objCouponMaster.Issuedstatus = objCouponListDetails.Issuedstatus;
                        //    objCouponMaster.CouponSerialCode = objCouponListDetails.CouponSerialCode;
                        //    objCouponMaster.Remarks = objCouponListDetails.Remarks;
                        //}

                        //objCouponMaster.Redeemedstatus = objReader["Redeemedstatus"] != DBNull.Value ? Convert.ToString(objReader["Redeemedstatus"]) : "";
                        //objCouponMaster.Remainingamount = objReader["Remainingamount"] != DBNull.Value ? Convert.ToDecimal(objReader["Remainingamount"]) : 0;
                        //objCouponMaster.PhysicalStore = objReader["PhysicalStore"] != DBNull.Value ? Convert.ToString(objReader["PhysicalStore"]) : "";
                        //objCouponMaster.Issuedstatus = objReader["Issuedstatus"] != DBNull.Value ? Convert.ToString(objReader["Issuedstatus"]) : "";
                        //objCouponMaster.CouponSerialCode = objReader["CouponSerialCode"] != DBNull.Value ? Convert.ToString(objReader["CouponSerialCode"]) : "";
                        objCouponMaster.Remarks = objReader["Remarks"] != DBNull.Value ? Convert.ToString(objReader["Remarks"]) : "";


                        List<CommonUtil> StoreCommonUtilData = new List<CommonUtil>();
                        List<CommonUtil> CustomerCommonUtilData = new List<CommonUtil>();
                        List<CommonUtil> TotalMasterCommonUtilData = new List<CommonUtil>();

                        var objSelectCouponStoreDetailsRequest = new SelectCouponStoreDetailsRequest();
                        var objSelectCouponStoreDetailsResponse = new SelectCouponStoreDetailsResponse();
                        objSelectCouponStoreDetailsRequest.DetailsType = Enums.SpecialPriceRecordType.Store;
                        objSelectCouponStoreDetailsRequest.CouponID = RequestData.ID;
                        objSelectCouponStoreDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectCouponStoreDetailsResponse = SelectCouponMasterStoreType(objSelectCouponStoreDetailsRequest);
                        if (objSelectCouponStoreDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            StoreCommonUtilData = objSelectCouponStoreDetailsResponse.StoreCommonUtil;
                        }

                        objSelectCouponStoreDetailsRequest = new SelectCouponStoreDetailsRequest();
                        objSelectCouponStoreDetailsResponse = new SelectCouponStoreDetailsResponse();
                        objSelectCouponStoreDetailsRequest.DetailsType = Enums.SpecialPriceRecordType.Customer;
                        objSelectCouponStoreDetailsRequest.CouponID = RequestData.ID;
                        objSelectCouponStoreDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectCouponStoreDetailsResponse = SelectCouponMasterStoreType(objSelectCouponStoreDetailsRequest);
                        if (objSelectCouponStoreDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            CustomerCommonUtilData = objSelectCouponStoreDetailsResponse.StoreCommonUtil;
                        }

                        objSelectCouponStoreDetailsRequest = new SelectCouponStoreDetailsRequest();
                        objSelectCouponStoreDetailsResponse = new SelectCouponStoreDetailsResponse();
                        objSelectCouponStoreDetailsRequest.DetailsType = Enums.SpecialPriceRecordType.Category;
                        objSelectCouponStoreDetailsRequest.CouponID = RequestData.ID;
                        objSelectCouponStoreDetailsRequest.ConnectionString = RequestData.ConnectionString;
                        objSelectCouponStoreDetailsResponse = SelectCouponMasterStoreType(objSelectCouponStoreDetailsRequest);
                        if (objSelectCouponStoreDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            TotalMasterCommonUtilData = objSelectCouponStoreDetailsResponse.StoreCommonUtil;
                        }

                        ResponseData.CouponMasterRecord = objCouponMaster;
                        ResponseData.ResponseDynamicData = objCouponMaster;
                        ResponseData.CouponMasterRecord.ObjCouponListDetails = tempCouponList;
                        ResponseData.CouponMasterRecord.StoreCommonUtilData = StoreCommonUtilData;
                        ResponseData.CouponMasterRecord.CustomerCommonUtilData = CustomerCommonUtilData;
                        ResponseData.CouponMasterRecord.TotalMasterCommonUtilData = TotalMasterCommonUtilData;
                        //ResponseData.CouponMasterRecord.ObjCouponListDetails = CouponListDetails;

                        ResponseData.ResponseDynamicData.ObjCouponListDetails = tempCouponList;
                        ResponseData.ResponseDynamicData.StoreCommonUtilData = StoreCommonUtilData;
                        ResponseData.ResponseDynamicData.CustomerCommonUtilData = CustomerCommonUtilData;
                        ResponseData.ResponseDynamicData.TotalMasterCommonUtilData = TotalMasterCommonUtilData;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
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

        public override UpdateCouponDetailsListResponse InsertCouponListDetails(UpdateCouponDetailsListRequest objRequest)
        {
            var RequestData = (UpdateCouponDetailsListRequest)objRequest;

            var ResponseData = new UpdateCouponDetailsListResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertCouponListDetailsToEnt", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter CouponListHeaderID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                CouponListHeaderID.Direction = ParameterDirection.Input;
                CouponListHeaderID.Value = RequestData.CouponListDetailsReq.CouponListHeaderID;

                SqlParameter CouponSerialCode = _CommandObj.Parameters.Add("@CouponSerialCode", SqlDbType.VarChar);
                CouponSerialCode.Direction = ParameterDirection.Input;
                CouponSerialCode.Value = RequestData.CouponListDetailsReq.CouponSerialCode;

                SqlParameter IssuedStatus = _CommandObj.Parameters.Add("@IssuedStatus", SqlDbType.VarChar);
                IssuedStatus.Direction = ParameterDirection.Input;
                IssuedStatus.Value = RequestData.CouponListDetailsReq.IssuedStatus;

                SqlParameter PhysicalStore = _CommandObj.Parameters.Add("@PhysicalStore", SqlDbType.VarChar);
                PhysicalStore.Direction = ParameterDirection.Input;
                PhysicalStore.Value = RequestData.CouponListDetailsReq.PhysicalStore;

                SqlParameter RemainingAmount = _CommandObj.Parameters.Add("@RemainingAmount", SqlDbType.VarChar);
                RemainingAmount.Direction = ParameterDirection.Input;
                RemainingAmount.Value = RequestData.CouponListDetailsReq.RemainingAmount;

                SqlParameter RedeemedStatus = _CommandObj.Parameters.Add("@RedeemedStatus", SqlDbType.VarChar);
                RedeemedStatus.Direction = ParameterDirection.Input;
                RedeemedStatus.Value = RequestData.CouponListDetailsReq.RedeemedStatus;

                SqlParameter CouponHeaderCode = _CommandObj.Parameters.Add("@CouponHeaderCode", SqlDbType.VarChar);
                CouponHeaderCode.Direction = ParameterDirection.Input;
                CouponHeaderCode.Value = RequestData.CouponListDetailsReq.CouponHeaderCode;

                SqlParameter LineNo = _CommandObj.Parameters.Add("@LineNo", SqlDbType.Int);
                LineNo.Direction = ParameterDirection.Input;
                LineNo.Value = RequestData.CouponListDetailsReq.LineNo;

                SqlParameter RedeemCount = _CommandObj.Parameters.Add("@RedeemCount", SqlDbType.Int);
                RedeemCount.Direction = ParameterDirection.Input;
                RedeemCount.Value = RequestData.CouponListDetailsReq.RedeemCount;

                SqlParameter ExpiredDate = _CommandObj.Parameters.Add("@ExpiredDate", SqlDbType.DateTime);
                ExpiredDate.Direction = ParameterDirection.Input;
                ExpiredDate.Value = RequestData.CouponListDetailsReq.ExpiredDate;


                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon List");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon List");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override UpdateCouponDetailsListResponse GetDeActiveCouponOnReturn(SelectCouponDataOnCouponCodeRequest objRequest)
        {
            var RequestData = (SelectCouponDataOnCouponCodeRequest)objRequest;
            var ResponseData = new UpdateCouponDetailsListResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateDeActiveCouponOnReturn", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;



                _CommandObj.Parameters.AddWithValue("@CouponSerialCode", RequestData.CouponCode);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.ReturnUpdate);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Coupon List");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon List");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override UpdateCouponDetailsListResponse UpdateCouponListDetails(UpdateCouponDetailsListRequest objRequest)
        {
            var RequestData = (UpdateCouponDetailsListRequest)objRequest;

            var ResponseData = new UpdateCouponDetailsListResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateCouponListDetails", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter CouponListHeaderID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                CouponListHeaderID.Direction = ParameterDirection.Input;
                CouponListHeaderID.Value = RequestData.CouponListDetailsReq.CouponListHeaderID;

                //SqlParameter CouponListHeaderID = _CommandObj.Parameters.Add("@CouponID", SqlDbType.Int);
                //CouponListHeaderID.Direction = ParameterDirection.Input;
                //CouponListHeaderID.Value = RequestData.CouponListDetailsReq.CouponListHeaderID;

                SqlParameter CouponSerialCode = _CommandObj.Parameters.Add("@CouponSerialCode", SqlDbType.VarChar);
                CouponSerialCode.Direction = ParameterDirection.Input;
                CouponSerialCode.Value = RequestData.CouponListDetailsReq.CouponSerialCode;

                SqlParameter IssuedStatus = _CommandObj.Parameters.Add("@IssuedStatus", SqlDbType.VarChar);
                IssuedStatus.Direction = ParameterDirection.Input;
                IssuedStatus.Value = RequestData.CouponListDetailsReq.IssuedStatus;

                SqlParameter PhysicalStore = _CommandObj.Parameters.Add("@PhysicalStore", SqlDbType.VarChar);
                PhysicalStore.Direction = ParameterDirection.Input;
                PhysicalStore.Value = RequestData.CouponListDetailsReq.PhysicalStore;

                SqlParameter RemainingAmount = _CommandObj.Parameters.Add("@RemainingAmount", SqlDbType.Decimal);
                RemainingAmount.Direction = ParameterDirection.Input;
                RemainingAmount.Value = RequestData.CouponListDetailsReq.RemainingAmount;

                SqlParameter RedeemedStatus = _CommandObj.Parameters.Add("@RedeemedStatus", SqlDbType.VarChar);
                RedeemedStatus.Direction = ParameterDirection.Input;
                RedeemedStatus.Value = RequestData.CouponListDetailsReq.RedeemedStatus;

                SqlParameter CouponHeaderCode = _CommandObj.Parameters.Add("@CouponHeaderCode", SqlDbType.VarChar);
                CouponHeaderCode.Direction = ParameterDirection.Input;
                CouponHeaderCode.Value = RequestData.CouponListDetailsReq.CouponHeaderCode;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Coupon List");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon List");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Coupon List");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        #endregion
    }
}
