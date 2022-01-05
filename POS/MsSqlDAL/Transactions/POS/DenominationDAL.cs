using EasyBizAbsDAL.Transactions;
using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizRequest.Transactions.POS.DenominationRequest;
using EasyBizResponse.Transactions.POS.DenominationResponse;
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
    public class DenominationDAL : BaseDenominationDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {

            var RequestData = (SaveDenominationRequest)RequestObj;
            var ResponseData = new SaveDenominationResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);







                //_CommandObj = new SqlCommand("InsertOrUpdateDenomination", _ConnectionObj);
                //_CommandObj.CommandType = CommandType.StoredProcedure;              
                //_CommandObj.Parameters.AddWithValue("@DenominationType", RequestData.ReceivedDenominationData.DenominationType);
                //_CommandObj.Parameters.AddWithValue("@DenominationValue", RequestData.ReceivedDenominationData.DenominationValue);
                //_CommandObj.Parameters.AddWithValue("@DenominationNumber", RequestData.ReceivedDenominationData.DenominationNumber);
                //_CommandObj.Parameters.AddWithValue("@TotalDenominationValue", RequestData.ReceivedDenominationData.TotalDenominationValue);
                //_CommandObj.Parameters.AddWithValue("@CreatedBy", RequestData.ReceivedDenominationData.CreateBy);

                _CommandObj = new SqlCommand("InsertTill", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@ShiftLogID", RequestData.DenominationForShiftoutTypeHeader.ShifLogId);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.DenominationForShiftoutTypeHeader.StoreCode);
                _CommandObj.Parameters.AddWithValue("@PosCode", RequestData.DenominationForShiftoutTypeHeader.POSCode);
                _CommandObj.Parameters.AddWithValue("@ShiftCode", RequestData.DenominationForShiftoutTypeHeader.ShiftCode);
                _CommandObj.Parameters.AddWithValue("@ShiftInAmount", RequestData.DenominationForShiftoutTypeHeader.ShiftInAmount);
                _CommandObj.Parameters.AddWithValue("@ShiftOutAmount", RequestData.DenominationForShiftoutTypeHeader.ShiftOutAmount);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.DenominationForShiftoutTypeHeader.remarks);
                _CommandObj.Parameters.AddWithValue("@GrandTotalValue", RequestData.DenominationForShiftoutTypeHeader.GrandTotalValue);
                _CommandObj.Parameters.AddWithValue("@TotalValueCount", RequestData.DenominationForShiftoutTypeHeader.TotalValueCount);
                _CommandObj.Parameters.AddWithValue("@TotalCardValue", RequestData.DenominationForShiftoutTypeHeader.TotalCardValue);


                var AFSegamationDetails = _CommandObj.Parameters.Add("@TillDenominationDetails", SqlDbType.Xml);
                AFSegamationDetails.Direction = ParameterDirection.Input;
                AFSegamationDetails.Value = TillDenominationXML(RequestData.DenominationForShiftOutTypeList);

                var PaymentTypeMasterTypeList = _CommandObj.Parameters.Add("@TillCardDetails", SqlDbType.Xml);
                PaymentTypeMasterTypeList.Direction = ParameterDirection.Input;
                PaymentTypeMasterTypeList.Value = TillDenominationXML(RequestData.PaymentTypeMasterTypeList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                SqlParameter ID1 = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID1.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Denomination");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Denomination");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Denomination");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Denomination");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public string TillDenominationXML(List<DenominationForShiftOutType> DenominationForShiftOutTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (DenominationForShiftOutType objDenominationForShiftOutType in DenominationForShiftOutTypeList)
            {
                if (objDenominationForShiftOutType.PaymemtValue != 0)
                {
                    sSql.Append("<TillDenominationDetails>");
                    sSql.Append("<ID>" + (objDenominationForShiftOutType.ID) + "</ID>");
                    sSql.Append("<CurrencyCode>" + objDenominationForShiftOutType.CurrencyCode + "</CurrencyCode>");
                    sSql.Append("<CurrencyValue>" + (objDenominationForShiftOutType.CurrencyValue) + "</CurrencyValue>");
                    sSql.Append("<PaymemtValue>" + objDenominationForShiftOutType.PaymemtValue + "</PaymemtValue>");                               
                    sSql.Append("<TotalValue>" + objDenominationForShiftOutType.TotalValue + "</TotalValue>");
                    sSql.Append("</TillDenominationDetails>");
                }
            }
            return sSql.ToString();
        }
        public string TillDenominationXML(List<PaymentTypeMasterType> PaymentTypeMasterTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (PaymentTypeMasterType objDenominationForShiftOutType in PaymentTypeMasterTypeList)
            {
                if (objDenominationForShiftOutType.PaymemtValue != 0)
                {
                    sSql.Append("<TillCardDetails>");
                    sSql.Append("<ID>" + (objDenominationForShiftOutType.ID) + "</ID>");
                    sSql.Append("<PaymentCode>" + objDenominationForShiftOutType.PaymentCode + "</PaymentCode>");                   
                    sSql.Append("<PaymemtValue>" + objDenominationForShiftOutType.PaymemtValue + "</PaymemtValue>");
                    
                    sSql.Append("</TillCardDetails>");
                }
            }
            return sSql.ToString();
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
    }
}
