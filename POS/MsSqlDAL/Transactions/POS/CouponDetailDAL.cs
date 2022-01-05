using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Transactions.POS.CouponDetailRequest;
using EasyBizResponse.Transactions.POS.CouponDetailResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.POS
{
    public class CouponDetailDAL : BaseCouponDetailDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveCouponDetailRequest)RequestObj;
            var ResponseData = new SaveCouponDetailResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateCouponDetailsforPayment", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CouponDetailDataforPayment.ID);
                _CommandObj.Parameters.AddWithValue("@ApplicationDate", RequestData.CouponDetailDataforPayment.ApplicationDate);
                _CommandObj.Parameters.AddWithValue("@InvoiceHeaderID", RequestData.CouponDetailDataforPayment.InvoiceHeaderID);
                _CommandObj.Parameters.AddWithValue("@InvoiceNumber", RequestData.CouponDetailDataforPayment.InvoiceNumber);
                _CommandObj.Parameters.AddWithValue("@PayMentMode", RequestData.CouponDetailDataforPayment.PayMentMode);
                _CommandObj.Parameters.AddWithValue("@CouponCode", RequestData.CouponDetailDataforPayment.CouponCode);
                _CommandObj.Parameters.AddWithValue("@StoreGroupCode", RequestData.CouponDetailDataforPayment.StoreGroupCode);
                _CommandObj.Parameters.AddWithValue("@Customer", RequestData.CouponDetailDataforPayment.Customer);
                _CommandObj.Parameters.AddWithValue("@ValidityFromDate", RequestData.CouponDetailDataforPayment.ValidityStartDate);
                _CommandObj.Parameters.AddWithValue("@ValidityToDate", RequestData.CouponDetailDataforPayment.ValidityEndDate);
                _CommandObj.Parameters.AddWithValue("@DiscountType", RequestData.CouponDetailDataforPayment.DiscountType);
                _CommandObj.Parameters.AddWithValue("@Discountvalue", RequestData.CouponDetailDataforPayment.Discountvalue);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.CouponDetailDataforPayment.CreateBy);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Value = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Invoice Coupon Details");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = Convert.ToInt32(ID1.Value).ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Invoice");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Invoice");
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectCouponDetailByInvoiceNoResponse SelectCouponDetailByInvoiceNo(SelectCouponDetailByInvoiceNoRequest ReqObj)
        {
            var InvoiceNoCouponDatas = new CouponDetail();
            var RequestData = (SelectCouponDetailByInvoiceNoRequest)ReqObj;
            var ResponseData = new SelectCouponDetailByInvoiceNoResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from CouponDetails where InvoiceNumber = '" + RequestData.InvoiceNumber + "'");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponDetail = new CouponDetail();
                        objCouponDetail.ID = Convert.ToInt32(objReader["ID"]);
                        objCouponDetail.InvoiceHeaderID = Convert.ToInt32(objReader["InvoiceHeaderID"]);
                        objCouponDetail.InvoiceNumber = objReader["InvoiceNumber"].ToString();
                        objCouponDetail.ApplicationDate = Convert.ToDateTime(objReader["ApplicationDate"]);
                        objCouponDetail.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponDetail.ValidityStartDate = objReader["ValidityFromDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidityFromDate"]) : DateTime.Now;
                        objCouponDetail.ValidityEndDate = objReader["ValidityToDate"] != DBNull.Value ? Convert.ToDateTime(objReader["ValidityToDate"]) : DateTime.Now;
                        objCouponDetail.DiscountType = objReader["DiscountType"] != DBNull.Value ? Convert.ToString(objReader["DiscountType"]) : string.Empty;
                        objCouponDetail.Discountvalue = objReader["Discountvalue"] != DBNull.Value ? Convert.ToInt32(objReader["Discountvalue"]) : 0;

                        objCouponDetail.Customer = objReader["Customer"] != DBNull.Value ? Convert.ToString(objReader["Customer"]) : string.Empty;
                        objCouponDetail.StoreGroupCode = objReader["StoreGroupCode"] != DBNull.Value ? Convert.ToString(objReader["StoreGroupCode"]) : string.Empty;

                        ResponseData.CouponDetailData = objCouponDetail;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice");
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

        public override GetCouponMasterByCustomerListResponse GetCouponMasterByCustomerList(GetCouponMasterByCustomerListRequest ReqObj)
        {
            var InvoiceNoCouponDatas = new CouponDetail();
            var RequestData = (GetCouponMasterByCustomerListRequest)ReqObj;
            var ResponseData = new GetCouponMasterByCustomerListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("GetCouponMasterByCustomerList");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter CouponCode = _CommandObj.Parameters.Add("@CouponCode", SqlDbType.VarChar);
                CouponCode.Direction = ParameterDirection.Input;
                CouponCode.Value = RequestData.CouponCode;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponDetail = new CouponDetail();
                        objCouponDetail.ID = Convert.ToInt32(objReader["ID"]);
                        objCouponDetail.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponDetail.ValidityStartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponDetail.ValidityEndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objCouponDetail.DiscountType = objReader["DiscountType"] != DBNull.Value ? Convert.ToString(objReader["DiscountType"]) : string.Empty;
                        objCouponDetail.Discountvalue = objReader["Discountvalue"] != DBNull.Value ? Convert.ToInt32(objReader["Discountvalue"]) : 0;

                        objCouponDetail.Customer = objReader["Customer"] != DBNull.Value ? Convert.ToString(objReader["Customer"]) : string.Empty;
                        objCouponDetail.StoreGroupCode = objReader["StoreGroupCode"] != DBNull.Value ? Convert.ToString(objReader["StoreGroupCode"]) : string.Empty;
                        ResponseData.CouponMasterByCustomerData = objCouponDetail;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice");
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
