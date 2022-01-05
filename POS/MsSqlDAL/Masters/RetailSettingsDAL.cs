using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizResponse.Masters.RetailSettingsResponse;
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
    public class RetailSettingsDAL : BaseRetailSettingsDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override SelectRetailSettingsLookUpResponse SelectRetailSettingsLookUp(SelectRetailSettingsLookUpRequest RequestObj)
        {
            var RetailSettingsList = new List<RetailSettingsType>();
            var RequestData = (SelectRetailSettingsLookUpRequest)RequestObj;
            var ResponseData = new SelectRetailSettingsLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[RetailName],RetailCode from RetailSettings with(NoLock)";
               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objRetailSettings = new RetailSettingsType();
                        objRetailSettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        objRetailSettings.RetailName = Convert.ToString(objReader["RetailName"]);
                        objRetailSettings.RetailCode = Convert.ToString(objReader["RetailCode"]);
                        RetailSettingsList.Add(objRetailSettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.RetailSettingsList = RetailSettingsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Retail Settings");
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

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveRetailRequest)RequestObj;
            var ResponseData = new SaveRetailResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateRetailSettings", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                 SqlParameter ID = _CommandObj.Parameters.Add("ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.RetailRecord.ID;

                SqlParameter RetailCode = _CommandObj.Parameters.Add("@RetailCode", SqlDbType.NVarChar);
                RetailCode.Direction = ParameterDirection.Input;
                RetailCode.Value = RequestData.RetailRecord.RetailCode;

                SqlParameter RetailName = _CommandObj.Parameters.Add("@RetailName", SqlDbType.NVarChar);
                RetailName.Direction = ParameterDirection.Input;
                RetailName.Value = RequestData.RetailRecord.RetailName;

                SqlParameter DefaultTransMode = _CommandObj.Parameters.Add("@DefaultTransMode", SqlDbType.NVarChar);
                DefaultTransMode.Direction = ParameterDirection.Input;
                DefaultTransMode.Value = RequestData.RetailRecord.DefaultTransMode;

                SqlParameter PriceLowerLimit = _CommandObj.Parameters.Add("@PriceLowerLimit", SqlDbType.Decimal);
                PriceLowerLimit.Direction = ParameterDirection.Input;
                PriceLowerLimit.Value = RequestData.RetailRecord.PriceLowerLimit;

                SqlParameter PriceUpperLimit = _CommandObj.Parameters.Add("@PriceUpperLimit", SqlDbType.Decimal);
                PriceUpperLimit.Direction = ParameterDirection.Input;
                PriceUpperLimit.Value = RequestData.RetailRecord.PriceUpperLimit;

                SqlParameter RowforScan = _CommandObj.Parameters.Add("@RowforScan", SqlDbType.Bit);
                RowforScan.Direction = ParameterDirection.Input;
                RowforScan.Value = RequestData.RetailRecord.RowforScan;

                SqlParameter ChangeAmountCurrency = _CommandObj.Parameters.Add("@ChangeAmountCurrency", SqlDbType.Int);
                ChangeAmountCurrency.Direction = ParameterDirection.Input;
                ChangeAmountCurrency.Value = RequestData.RetailRecord.ChangeAmountCurrency;

                SqlParameter RefundPromotinal = _CommandObj.Parameters.Add("@RefundPromotinal", SqlDbType.Bit);
                RefundPromotinal.Direction = ParameterDirection.Input;
                RefundPromotinal.Value = RequestData.RetailRecord.RefundPromotinal;

                SqlParameter PrintParked = _CommandObj.Parameters.Add("@PrintParked", SqlDbType.Bit);
                PrintParked.Direction = ParameterDirection.Input;
                PrintParked.Value = RequestData.RetailRecord.PrintParked;

                SqlParameter DeleteParkedDayEnd = _CommandObj.Parameters.Add("@DeleteParkedDayEnd", SqlDbType.Bit);
                DeleteParkedDayEnd.Direction = ParameterDirection.Input;
                DeleteParkedDayEnd.Value = RequestData.RetailRecord.DeleteParkedDayEnd;

                SqlParameter ChangeSaleEmployee = _CommandObj.Parameters.Add("@ChangeSaleEmployee", SqlDbType.Bit);
                ChangeSaleEmployee.Direction = ParameterDirection.Input;
                ChangeSaleEmployee.Value = RequestData.RetailRecord.ChangeSaleEmployee;

                SqlParameter QuickComplete = _CommandObj.Parameters.Add("@QuickComplete", SqlDbType.Bit);
                QuickComplete.Direction = ParameterDirection.Input;
                QuickComplete.Value = RequestData.RetailRecord.QuickComplete;

                SqlParameter LoginDiffDate = _CommandObj.Parameters.Add("@LoginDiffDate", SqlDbType.Bit);
                LoginDiffDate.Direction = ParameterDirection.Input;
                LoginDiffDate.Value = RequestData.RetailRecord.LoginDiffDate;

                SqlParameter LogVoidedTransaction = _CommandObj.Parameters.Add("@LogVoidedTransaction", SqlDbType.Bit);
                LogVoidedTransaction.Direction = ParameterDirection.Input;
                LogVoidedTransaction.Value = RequestData.RetailRecord.LogVoidedTransaction;

                SqlParameter MaxLinesPerTransaction = _CommandObj.Parameters.Add("@MaxLinesPerTransaction", SqlDbType.Decimal);
                MaxLinesPerTransaction.Direction = ParameterDirection.Input;
                MaxLinesPerTransaction.Value = RequestData.RetailRecord.MaxLinesPerTransaction;

                SqlParameter MaxDiscountPercentage = _CommandObj.Parameters.Add("@MaxDiscountPercentage", SqlDbType.Decimal);
                MaxDiscountPercentage.Direction = ParameterDirection.Input;
                MaxDiscountPercentage.Value = RequestData.RetailRecord.MaxDiscountPercentage;

                SqlParameter MaxLineDiscountPercentage = _CommandObj.Parameters.Add("@MaxLineDiscountPercentage", SqlDbType.Decimal);
                MaxLineDiscountPercentage.Direction = ParameterDirection.Input;
                MaxLineDiscountPercentage.Value = RequestData.RetailRecord.MaxLineDiscountPercentage;

                SqlParameter MaxDiscountAmt = _CommandObj.Parameters.Add("@MaxDiscountAmt", SqlDbType.Decimal);
                MaxDiscountAmt.Direction = ParameterDirection.Input;
                MaxDiscountAmt.Value = RequestData.RetailRecord.MaxDiscountAmt;

                SqlParameter MaxLieDiscountAmt = _CommandObj.Parameters.Add("@MaxLieDiscountAmt", SqlDbType.Decimal);
                MaxLieDiscountAmt.Direction = ParameterDirection.Input;
                MaxLieDiscountAmt.Value = RequestData.RetailRecord.MaxLieDiscountAmt;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.RetailRecord.Active;

                SqlParameter AllowRefundToExchangedItems = _CommandObj.Parameters.Add("@AllowRefundToExchangedItems", SqlDbType.Bit);
                AllowRefundToExchangedItems.Direction = ParameterDirection.Input;
                AllowRefundToExchangedItems.Value = RequestData.RetailRecord.AllowRefundToExchangedItems;

                SqlParameter AllowSalesForNegativeStock = _CommandObj.Parameters.Add("@AllowSalesForNegativeStock", SqlDbType.Bit);
                AllowSalesForNegativeStock.Direction = ParameterDirection.Input;
                AllowSalesForNegativeStock.Value = RequestData.RetailRecord.AllowSalesForNegativeStock;

                SqlParameter AllowSalesForZeroPrice = _CommandObj.Parameters.Add("@AllowSalesForZeroPrice", SqlDbType.Bit);
                AllowSalesForZeroPrice.Direction = ParameterDirection.Input;
                AllowSalesForZeroPrice.Value = RequestData.RetailRecord.AllowSalesForZeroPrice;

                SqlParameter IsCreditCardDetailsMandatory = _CommandObj.Parameters.Add("@IsCreditCardDetailsMandatory", SqlDbType.Bit);
                IsCreditCardDetailsMandatory.Direction = ParameterDirection.Input;
                IsCreditCardDetailsMandatory.Value = RequestData.RetailRecord.IsCreditCardDetailsMandatory;

                SqlParameter AllowMultiplePromotions = _CommandObj.Parameters.Add("@AllowMultiplePromotions", SqlDbType.Bit);
                AllowMultiplePromotions.Direction = ParameterDirection.Input;
                AllowMultiplePromotions.Value = RequestData.RetailRecord.AllowMultiplePromotions;

                SqlParameter AllowWNPromotions = _CommandObj.Parameters.Add("@AllowWNPromotions", SqlDbType.Bit);
                AllowWNPromotions.Direction = ParameterDirection.Input;
                AllowWNPromotions.Value = RequestData.RetailRecord.AllowWNPromotions;
               
                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.RetailRecord.CreateBy;                 
                       
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
                    if(RequestData.RetailRecord.ID == 0)
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Retail Settings");
                    else
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Retail Settings");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();                    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Retail Settings");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Retail Settings");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Retail Settings");
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

            var RetailRecord = new RetailSettingsType();
            var RequestData = (DeleteRetailRequest)RequestObj;
            var ResponseData = new DeleteRetailResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from  RetailSettings where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Retail Settings");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Retail Settings");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RetailRecord = new RetailSettingsType();
            var RequestData = (SelectByRetailIDRequest)RequestObj;
            var ResponseData = new SelectByRetailIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from RetailSettings  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objRetail = new RetailSettingsType();
                        objRetail.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRetail.RetailCode = Convert.ToString(objReader["RetailCode"]);
                        objRetail.RetailName = Convert.ToString(objReader["RetailName"]);
                        objRetail.DefaultTransMode = Convert.ToString(objReader["DefaultTransMode"]);
                        objRetail.PriceLowerLimit = objReader["PriceLowerLimit"] != DBNull.Value ? Convert.ToDecimal(objReader["PriceLowerLimit"]) : 0;
                        objRetail.PriceUpperLimit = objReader["PriceUpperLimit"] != DBNull.Value ? Convert.ToDecimal(objReader["PriceUpperLimit"]) : 0;
                        objRetail.RowforScan = objReader["RowforScan"] != DBNull.Value ? Convert.ToBoolean(objReader["RowforScan"]) : false;
                        objRetail.ChangeAmountCurrency = objReader["ChangeAmountCurrency"] != DBNull.Value ? Convert.ToInt32(objReader["ChangeAmountCurrency"]) : 0;
                        objRetail.RefundPromotinal = objReader["RefundPromotinal"] != DBNull.Value ? Convert.ToBoolean(objReader["RefundPromotinal"]) : false;
                        objRetail.PrintParked = objReader["PrintParked"] != DBNull.Value ? Convert.ToBoolean(objReader["PrintParked"]) : false;
                        objRetail.DeleteParkedDayEnd = objReader["DeleteParkedDayEnd"] != DBNull.Value ? Convert.ToBoolean(objReader["DeleteParkedDayEnd"]) : false;
                        objRetail.ChangeSaleEmployee = objReader["ChangeSaleEmployee"] != DBNull.Value ? Convert.ToBoolean(objReader["ChangeSaleEmployee"]) : false;
                        objRetail.QuickComplete = objReader["QuickComplete"] != DBNull.Value ? Convert.ToBoolean(objReader["QuickComplete"]) : false;
                        objRetail.LoginDiffDate = objReader["LoginDiffDate"] != DBNull.Value ? Convert.ToBoolean(objReader["LoginDiffDate"]) : false;
                        objRetail.LogVoidedTransaction = objReader["LogVoidedTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["LogVoidedTransaction"]) : false;
                        objRetail.MaxLinesPerTransaction = objReader["MaxLinesPerTransaction"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLinesPerTransaction"]) : 0;
                        objRetail.MaxDiscountPercentage = objReader["MaxDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxDiscountPercentage"]) : 0;
                        objRetail.MaxLineDiscountPercentage = objReader["MaxLineDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLineDiscountPercentage"]) : 0;
                        objRetail.MaxDiscountAmt = objReader["MaxDiscountAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxDiscountAmt"]) : 0;
                        objRetail.MaxLieDiscountAmt = objReader["MaxLieDiscountAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLieDiscountAmt"]) : 0;
                        objRetail.AllowRefundToExchangedItems = objReader["AllowRefundToExchangedItems"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowRefundToExchangedItems"]) : false;
                        objRetail.AllowSalesForNegativeStock = objReader["AllowSalesForNegativeStock"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSalesForNegativeStock"]) : false;
                        objRetail.AllowSalesForZeroPrice = objReader["AllowSalesForZeroPrice"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSalesForZeroPrice"]) : false;
                        objRetail.IsCreditCardDetailsMandatory = objReader["IsCreditCardDetailsMandatory"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditCardDetailsMandatory"]) : false;
                        objRetail.AllowMultiplePromotions = objReader["AllowMultiplePromotions"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiplePromotions"]) : false;
                        objRetail.AllowWNPromotions = objReader["AllowWNPromotions"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowWNPromotions"]) : false;
                        objRetail.EnableFingerPrint = objReader["EnableFingerPrint"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableFingerPrint"]) : false;



                        // objRetail.RefundWOReceipt = objReader["RefundWOReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["RefundWOReceipt"]) : true;
                        // objRetail.ExchangeWOReceipt = objReader["ExchangeWOReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["ExchangeWOReceipt"]) : true;
                        // objRetail.AutoUpdateAmt = objReader["AutoUpdateAmt"] != DBNull.Value ? Convert.ToBoolean(objReader["AutoUpdateAmt"]) : true;                    
                        //objRetail.MandatorySalePerson = objReader["MandatorySalePerson"] != DBNull.Value ? Convert.ToBoolean(objReader["MandatorySalePerson"]) : true;

                        // objRetail.RestrictOtherPos = objReader["RestrictOtherPos"] != DBNull.Value ? Convert.ToBoolean(objReader["RestrictOtherPos"]) : true;
                        // objRetail.MultipleTransRefundInSaingle = objReader["MultipleTransRefundInSaingle"] != DBNull.Value ? Convert.ToBoolean(objReader["MultipleTransRefundInSaingle"]) : true;

                        // objRetail.ProductSearch = objReader["ProductSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["ProductSearch"]) : true;
                        // objRetail.StyleSearch = objReader["StyleSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["StyleSearch"]) : true;
                        // objRetail.CustomerSearch = objReader["CustomerSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["CustomerSearch"]) : true;
                        // objRetail.TransactionSearch = objReader["TransactionSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionSearch"]) : true;
                        // objRetail.CreditLimit = objReader["CreditLimit"] != DBNull.Value ? Convert.ToBoolean(objReader["CreditLimit"]) : true;
                        // objRetail.ReceiptReprint = objReader["ReceiptReprint"] != DBNull.Value ? Convert.ToBoolean(objReader["ReceiptReprint"]) : true;
                        // objRetail.DeletingSuspended = objReader["DeletingSuspended"] != DBNull.Value ? Convert.ToBoolean(objReader["DeletingSuspended"]) : true;
                        // objRetail.VoidSale = objReader["VoidSale"] != DBNull.Value ? Convert.ToBoolean(objReader["VoidSale"]) : true;
                        // objRetail.VoidItem = objReader["VoidItem"] != DBNull.Value ? Convert.ToBoolean(objReader["VoidItem"]) : true;
                        // objRetail.Suspend = objReader["Suspend"] != DBNull.Value ? Convert.ToBoolean(objReader["Suspend"]) : true;
                        // objRetail.PaymentCancel = objReader["PaymentCancel"] != DBNull.Value ? Convert.ToBoolean(objReader["PaymentCancel"]) : true;
                        // objRetail.RefundTransaction = objReader["RefundTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["RefundTransaction"]) : true;
                        objRetail.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;                      
                     
                        objRetail.Remarks = Convert.ToString(objReader["Remarks"]);                                      
                        objRetail.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objRetail.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objRetail.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objRetail.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objRetail.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        
                        ResponseData.RetailRecord = objRetail;

                        ResponseData.ResponseDynamicData = objRetail;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Retail Settings");
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
            var RetailList = new List<RetailSettingsType>();
            var RequestData = (SelectAllRetailRequest)RequestObj;
            var ResponseData = new SelectAllRetailResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from AgentMaster with(NoLock) where Active='{0}'";
                string sSql = "Select * from RetailSettings  ";



                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objRetail = new RetailSettingsType();                       
                        objRetail.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRetail.RetailCode = Convert.ToString(objReader["RetailCode"]);
                        objRetail.RetailName = Convert.ToString(objReader["RetailName"]);
                        objRetail.DefaultTransMode = Convert.ToString(objReader["DefaultTransMode"]);
                        objRetail.PriceLowerLimit = objReader["PriceLowerLimit"] != DBNull.Value ? Convert.ToDecimal(objReader["PriceLowerLimit"]) : 0;
                        objRetail.PriceUpperLimit = objReader["PriceUpperLimit"] != DBNull.Value ? Convert.ToDecimal(objReader["PriceUpperLimit"]) : 0;
                        objRetail.RowforScan = objReader["RowforScan"] != DBNull.Value ? Convert.ToBoolean(objReader["RowforScan"]) : false;
                        objRetail.ChangeAmountCurrency = objReader["ChangeAmountCurrency"] != DBNull.Value ? Convert.ToInt32(objReader["ChangeAmountCurrency"]) : 0;
                        objRetail.RefundPromotinal = objReader["RefundPromotinal"] != DBNull.Value ? Convert.ToBoolean(objReader["RefundPromotinal"]) : false;
                        objRetail.PrintParked = objReader["PrintParked"] != DBNull.Value ? Convert.ToBoolean(objReader["PrintParked"]) : false;
                        objRetail.DeleteParkedDayEnd = objReader["DeleteParkedDayEnd"] != DBNull.Value ? Convert.ToBoolean(objReader["DeleteParkedDayEnd"]) : false;
                        objRetail.ChangeSaleEmployee = objReader["ChangeSaleEmployee"] != DBNull.Value ? Convert.ToBoolean(objReader["ChangeSaleEmployee"]) : false;
                        objRetail.QuickComplete = objReader["QuickComplete"] != DBNull.Value ? Convert.ToBoolean(objReader["QuickComplete"]) : false;
                        objRetail.LoginDiffDate = objReader["LoginDiffDate"] != DBNull.Value ? Convert.ToBoolean(objReader["LoginDiffDate"]) : false;
                        objRetail.LogVoidedTransaction = objReader["LogVoidedTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["LogVoidedTransaction"]) : false;
                        objRetail.MaxLinesPerTransaction = objReader["MaxLinesPerTransaction"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLinesPerTransaction"]) : 0;
                        objRetail.MaxDiscountPercentage = objReader["MaxDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxDiscountPercentage"]) : 0;
                        objRetail.MaxLineDiscountPercentage = objReader["MaxLineDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLineDiscountPercentage"]) : 0;
                        objRetail.MaxDiscountAmt = objReader["MaxDiscountAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxDiscountAmt"]) : 0;
                        objRetail.MaxLieDiscountAmt = objReader["MaxLieDiscountAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLieDiscountAmt"]) : 0;
                        objRetail.AllowSalesForNegativeStock = objReader["AllowSalesForNegativeStock"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSalesForNegativeStock"]) : false;
                        objRetail.AllowSalesForZeroPrice = objReader["AllowSalesForZeroPrice"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSalesForZeroPrice"]) : false;
                        objRetail.IsCreditCardDetailsMandatory = objReader["IsCreditCardDetailsMandatory"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditCardDetailsMandatory"]) : false;
                        objRetail.AllowMultiplePromotions = objReader["AllowMultiplePromotions"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiplePromotions"]) : false;
                        objRetail.AllowWNPromotions = objReader["AllowWNPromotions"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowWNPromotions"]) : false;
                        objRetail.EnableFingerPrint = objReader["EnableFingerPrint"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableFingerPrint"]) : false;

                        objRetail.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objRetail.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objRetail.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objRetail.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objRetail.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objRetail.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objRetail.AllowRefundToExchangedItems = objReader["AllowRefundToExchangedItems"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowRefundToExchangedItems"]) : true;
                        RetailList.Add(objRetail);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.RetailList = RetailList;

                    ResponseData.ResponseDynamicData = RetailList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Retail Settings");
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectAllRetailResponse API_SelectALL(SelectAllRetailRequest requestData)
        {
            var RetailList = new List<RetailSettingsType>();
            var RequestData = (SelectAllRetailRequest)requestData;
            var ResponseData = new SelectAllRetailResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select * from AgentMaster with(NoLock) where Active='{0}'";
                //string sSql = "Select * from RetailSettings  ";

                string sSql = "Select ID, RetailCode, RetailName, ChangeAmountCurrency, Active, RC.TOTAL_CNT [RecordCount]  " +
                   "from RetailSettings " +
                   "LEFT JOIN(Select  count(RS.ID) As TOTAL_CNT From RetailSettings RS with(NoLock) " +
                   "where RS.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or RS.RetailCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or RS.ChangeAmountCurrency like isnull('%" + RequestData.SearchString + "%','') " +
                       "or RS.RetailName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +

                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or RetailCode like isnull('%" + RequestData.SearchString + "%','') " +
                       "or ChangeAmountCurrency like isnull('%" + RequestData.SearchString + "%','') " +
                       "or RetailName like isnull('" + RequestData.SearchString + "','')) " +
                       "order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";



                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objRetail = new RetailSettingsType();
                        objRetail.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRetail.RetailCode = Convert.ToString(objReader["RetailCode"]);
                        objRetail.RetailName = Convert.ToString(objReader["RetailName"]);
                        //objRetail.DefaultTransMode = Convert.ToString(objReader["DefaultTransMode"]);
                        //objRetail.PriceLowerLimit = objReader["PriceLowerLimit"] != DBNull.Value ? Convert.ToDecimal(objReader["PriceLowerLimit"]) : 0;
                        //objRetail.PriceUpperLimit = objReader["PriceUpperLimit"] != DBNull.Value ? Convert.ToDecimal(objReader["PriceUpperLimit"]) : 0;
                        //objRetail.RowforScan = objReader["RowforScan"] != DBNull.Value ? Convert.ToBoolean(objReader["RowforScan"]) : false;
                        objRetail.ChangeAmountCurrency = objReader["ChangeAmountCurrency"] != DBNull.Value ? Convert.ToInt32(objReader["ChangeAmountCurrency"]) : 0;
                        //objRetail.RefundPromotinal = objReader["RefundPromotinal"] != DBNull.Value ? Convert.ToBoolean(objReader["RefundPromotinal"]) : false;
                        //objRetail.PrintParked = objReader["PrintParked"] != DBNull.Value ? Convert.ToBoolean(objReader["PrintParked"]) : false;
                        //objRetail.DeleteParkedDayEnd = objReader["DeleteParkedDayEnd"] != DBNull.Value ? Convert.ToBoolean(objReader["DeleteParkedDayEnd"]) : false;
                        //objRetail.ChangeSaleEmployee = objReader["ChangeSaleEmployee"] != DBNull.Value ? Convert.ToBoolean(objReader["ChangeSaleEmployee"]) : false;
                        //objRetail.QuickComplete = objReader["QuickComplete"] != DBNull.Value ? Convert.ToBoolean(objReader["QuickComplete"]) : false;
                        //objRetail.LoginDiffDate = objReader["LoginDiffDate"] != DBNull.Value ? Convert.ToBoolean(objReader["LoginDiffDate"]) : false;
                        //objRetail.LogVoidedTransaction = objReader["LogVoidedTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["LogVoidedTransaction"]) : false;
                        //objRetail.MaxLinesPerTransaction = objReader["MaxLinesPerTransaction"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLinesPerTransaction"]) : 0;
                        //objRetail.MaxDiscountPercentage = objReader["MaxDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxDiscountPercentage"]) : 0;
                        //objRetail.MaxLineDiscountPercentage = objReader["MaxLineDiscountPercentage"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLineDiscountPercentage"]) : 0;
                        //objRetail.MaxDiscountAmt = objReader["MaxDiscountAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxDiscountAmt"]) : 0;
                        //objRetail.MaxLieDiscountAmt = objReader["MaxLieDiscountAmt"] != DBNull.Value ? Convert.ToDecimal(objReader["MaxLieDiscountAmt"]) : 0;
                        //objRetail.AllowSalesForNegativeStock = objReader["AllowSalesForNegativeStock"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSalesForNegativeStock"]) : false;
                        //objRetail.AllowSalesForZeroPrice = objReader["AllowSalesForZeroPrice"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowSalesForZeroPrice"]) : false;
                        //objRetail.IsCreditCardDetailsMandatory = objReader["IsCreditCardDetailsMandatory"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditCardDetailsMandatory"]) : false;
                        //objRetail.AllowMultiplePromotions = objReader["AllowMultiplePromotions"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiplePromotions"]) : false;
                        //objRetail.AllowWNPromotions = objReader["AllowWNPromotions"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowWNPromotions"]) : false;
                        //objRetail.EnableFingerPrint = objReader["EnableFingerPrint"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableFingerPrint"]) : false;

                        //objRetail.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objRetail.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objRetail.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objRetail.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objRetail.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objRetail.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objRetail.AllowRefundToExchangedItems = objReader["AllowRefundToExchangedItems"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowRefundToExchangedItems"]) : true;
                        RetailList.Add(objRetail);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.RetailList = RetailList;

                    ResponseData.ResponseDynamicData = RetailList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Retail Settings");
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
