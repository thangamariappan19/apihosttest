using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.ManagerOverrideResponse;
using EasyBizTypes.Masters;
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
    public class ManagerOverrideDAL : BaseManagerOverrideDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        SqlTransaction transaction = null;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override SelectManagerOverrideLookUpResponse SelectManagerOverrideLookUp(SelectManagerOverrideLookUpRequest ObjRequest)
        {
            var ManagerOverrideList = new List<ManagerOverride>();
            var RequestData = (SelectManagerOverrideLookUpRequest)ObjRequest;
            var ResponseData = new SelectManagerOverrideLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,[Name],Code from ManagerOverride with(NoLock) where Active='true'";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objRetailSettings = new ManagerOverride();
                        objRetailSettings.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRetailSettings.Name = Convert.ToString(objReader["Name"]);
                        objRetailSettings.Code = Convert.ToString(objReader["Code"]);
                        ManagerOverrideList.Add(objRetailSettings);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ManagerOverrideList = ManagerOverrideList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Manager Override");
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
            var RequestData = (SaveManagerOverrideRequest)RequestObj;
            var ResponseData = new SaveManagerOverrideResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertOrUpdateManagerOverride", _ConnectionObj, transaction);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ManagerOverrideData.ID;

                SqlParameter Code = _CommandObj.Parameters.Add("@Code", SqlDbType.NVarChar);
                Code.Direction = ParameterDirection.Input;
                Code.Value = RequestData.ManagerOverrideData.Code;

                SqlParameter Name = _CommandObj.Parameters.Add("@Name", SqlDbType.NVarChar);
                Name.Direction = ParameterDirection.Input;
                Name.Value = RequestData.ManagerOverrideData.Name;

                SqlParameter CreditLimitOverride = _CommandObj.Parameters.Add("@CreditLimitOverride", SqlDbType.NVarChar);
                CreditLimitOverride.Direction = ParameterDirection.Input;
                CreditLimitOverride.Value = RequestData.ManagerOverrideData.CreditLimitOverride;

                SqlParameter ReprintTransReceipt = _CommandObj.Parameters.Add("@ReprintTransReceipt", SqlDbType.Bit);
                ReprintTransReceipt.Direction = ParameterDirection.Input;
                ReprintTransReceipt.Value = RequestData.ManagerOverrideData.ReprintTransReceipt;

                SqlParameter changeSalesPersoninSOE = _CommandObj.Parameters.Add("@changeSalesPersoninSOE", SqlDbType.Bit);
                changeSalesPersoninSOE.Direction = ParameterDirection.Input;
                changeSalesPersoninSOE.Value = RequestData.ManagerOverrideData.changeSalesPersoninSOE;

                SqlParameter ChangeSalesPersonRefund = _CommandObj.Parameters.Add("@ChangeSalesPersonRefund", SqlDbType.Bit);
                ChangeSalesPersonRefund.Direction = ParameterDirection.Input;
                ChangeSalesPersonRefund.Value = RequestData.ManagerOverrideData.ChangeSalesPersonRefund;

                SqlParameter DelSuspendedTransaction = _CommandObj.Parameters.Add("@DelSuspendedTransaction", SqlDbType.Bit);
                DelSuspendedTransaction.Direction = ParameterDirection.Input;
                DelSuspendedTransaction.Value = RequestData.ManagerOverrideData.DelSuspendedTransaction;

                SqlParameter VoidSale = _CommandObj.Parameters.Add("@VoidSale", SqlDbType.Bit);
                VoidSale.Direction = ParameterDirection.Input;
                VoidSale.Value = RequestData.ManagerOverrideData.VoidSale;

                SqlParameter voidItem = _CommandObj.Parameters.Add("@voidItem", SqlDbType.Bit);
                voidItem.Direction = ParameterDirection.Input;
                voidItem.Value = RequestData.ManagerOverrideData.voidItem;

                SqlParameter TransModeChange = _CommandObj.Parameters.Add("@TransModeChange", SqlDbType.Bit);
                TransModeChange.Direction = ParameterDirection.Input;
                TransModeChange.Value = RequestData.ManagerOverrideData.TransModeChange;

                SqlParameter CustomerSearch = _CommandObj.Parameters.Add("@CustomerSearch", SqlDbType.Bit);
                CustomerSearch.Direction = ParameterDirection.Input;
                CustomerSearch.Value = RequestData.ManagerOverrideData.CustomerSearch;

                SqlParameter ProductSearch = _CommandObj.Parameters.Add("@ProductSearch", SqlDbType.Bit);
                ProductSearch.Direction = ParameterDirection.Input;
                ProductSearch.Value = RequestData.ManagerOverrideData.ProductSearch;

                SqlParameter SaleInfoEdit = _CommandObj.Parameters.Add("@SaleInfoEdit", SqlDbType.Bit);
                SaleInfoEdit.Direction = ParameterDirection.Input;
                SaleInfoEdit.Value = RequestData.ManagerOverrideData.SaleInfoEdit;

                SqlParameter ItemInfoEdit = _CommandObj.Parameters.Add("@ItemInfoEdit", SqlDbType.Bit);
                ItemInfoEdit.Direction = ParameterDirection.Input;
                ItemInfoEdit.Value = RequestData.ManagerOverrideData.ItemInfoEdit;

                SqlParameter TransactionSearch = _CommandObj.Parameters.Add("@TransactionSearch", SqlDbType.Bit);
                TransactionSearch.Direction = ParameterDirection.Input;
                TransactionSearch.Value = RequestData.ManagerOverrideData.TransactionSearch;

                SqlParameter SuspendRecall = _CommandObj.Parameters.Add("@SuspendRecall", SqlDbType.Bit);
                SuspendRecall.Direction = ParameterDirection.Input;
                SuspendRecall.Value = RequestData.ManagerOverrideData.SuspendRecall;

                SqlParameter CashOut = _CommandObj.Parameters.Add("@CashOut", SqlDbType.Bit);
                CashOut.Direction = ParameterDirection.Input;
                CashOut.Value = RequestData.ManagerOverrideData.CashOut;

                SqlParameter CashIn = _CommandObj.Parameters.Add("@CashIn", SqlDbType.Bit);
                CashIn.Direction = ParameterDirection.Input;
                CashIn.Value = RequestData.ManagerOverrideData.CashIn;

                SqlParameter TransactionRefund = _CommandObj.Parameters.Add("@TransactionRefund", SqlDbType.Bit);
                TransactionRefund.Direction = ParameterDirection.Input;
                TransactionRefund.Value = RequestData.ManagerOverrideData.TransactionRefund;

                SqlParameter TotalDiscount = _CommandObj.Parameters.Add("@TotalDiscount", SqlDbType.Bit);
                TotalDiscount.Direction = ParameterDirection.Input;
                TotalDiscount.Value = RequestData.ManagerOverrideData.TotalDiscount;

                SqlParameter DayInDayOut = _CommandObj.Parameters.Add("@DayInDayOut", SqlDbType.Bit);
                DayInDayOut.Direction = ParameterDirection.Input;
                DayInDayOut.Value = RequestData.ManagerOverrideData.DayInDayOut;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ManagerOverrideData.Active;

                SqlParameter AllowEditcustomer = _CommandObj.Parameters.Add("@AllowEditcustomer", SqlDbType.Bit);
                AllowEditcustomer.Direction = ParameterDirection.Input;
                AllowEditcustomer.Value = RequestData.ManagerOverrideData.AllowEditcustomer;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ManagerOverrideData.CreateBy;

                //SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                //UpdateBy.Direction = ParameterDirection.Input;
                //UpdateBy.Value = RequestData.RoleMasterData.UpdateBy;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Manager Override");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID1.Value.ToString();

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Manager Override");

                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Manager Override");

                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Manager Override");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
                transaction.Rollback();
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateManagerOverrideRequest)RequestObj;

            var ResponseData = new UpdateManagerOverrideResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateManagerOverride", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ManagerOverrideData.ID;

                

                SqlParameter Code = _CommandObj.Parameters.Add("@Code", SqlDbType.NVarChar);
                Code.Direction = ParameterDirection.Input;
                Code.Value = RequestData.ManagerOverrideData.Code;

                SqlParameter Name = _CommandObj.Parameters.Add("@Name", SqlDbType.NVarChar);
                Name.Direction = ParameterDirection.Input;
                Name.Value = RequestData.ManagerOverrideData.Name;

                SqlParameter CreditLimitOverride = _CommandObj.Parameters.Add("@CreditLimitOverride", SqlDbType.NVarChar);
                CreditLimitOverride.Direction = ParameterDirection.Input;
                CreditLimitOverride.Value = RequestData.ManagerOverrideData.CreditLimitOverride;

                SqlParameter ReprintTransReceipt = _CommandObj.Parameters.Add("@ReprintTransReceipt", SqlDbType.Bit);
                ReprintTransReceipt.Direction = ParameterDirection.Input;
                ReprintTransReceipt.Value = RequestData.ManagerOverrideData.ReprintTransReceipt;

                SqlParameter changeSalesPersoninSOE = _CommandObj.Parameters.Add("@changeSalesPersoninSOE", SqlDbType.Bit);
                changeSalesPersoninSOE.Direction = ParameterDirection.Input;
                changeSalesPersoninSOE.Value = RequestData.ManagerOverrideData.changeSalesPersoninSOE;

                SqlParameter ChangeSalesPersonRefund = _CommandObj.Parameters.Add("@ChangeSalesPersonRefund", SqlDbType.Bit);
                ChangeSalesPersonRefund.Direction = ParameterDirection.Input;
                ChangeSalesPersonRefund.Value = RequestData.ManagerOverrideData.ChangeSalesPersonRefund;

                SqlParameter DelSuspendedTransaction = _CommandObj.Parameters.Add("@DelSuspendedTransaction", SqlDbType.Bit);
                DelSuspendedTransaction.Direction = ParameterDirection.Input;
                DelSuspendedTransaction.Value = RequestData.ManagerOverrideData.DelSuspendedTransaction;

                SqlParameter VoidSale = _CommandObj.Parameters.Add("@VoidSale", SqlDbType.Bit);
                VoidSale.Direction = ParameterDirection.Input;
                VoidSale.Value = RequestData.ManagerOverrideData.VoidSale;

                SqlParameter voidItem = _CommandObj.Parameters.Add("@voidItem", SqlDbType.Bit);
                voidItem.Direction = ParameterDirection.Input;
                voidItem.Value = RequestData.ManagerOverrideData.voidItem;

                SqlParameter TransModeChange = _CommandObj.Parameters.Add("@TransModeChange", SqlDbType.Bit);
                TransModeChange.Direction = ParameterDirection.Input;
                TransModeChange.Value = RequestData.ManagerOverrideData.TransModeChange;

                SqlParameter CustomerSearch = _CommandObj.Parameters.Add("@CustomerSearch", SqlDbType.Bit);
                CustomerSearch.Direction = ParameterDirection.Input;
                CustomerSearch.Value = RequestData.ManagerOverrideData.CustomerSearch;

                SqlParameter ProductSearch = _CommandObj.Parameters.Add("@ProductSearch", SqlDbType.Bit);
                ProductSearch.Direction = ParameterDirection.Input;
                ProductSearch.Value = RequestData.ManagerOverrideData.ProductSearch;

                SqlParameter SaleInfoEdit = _CommandObj.Parameters.Add("@SaleInfoEdit", SqlDbType.Bit);
                SaleInfoEdit.Direction = ParameterDirection.Input;
                SaleInfoEdit.Value = RequestData.ManagerOverrideData.SaleInfoEdit;

                SqlParameter ItemInfoEdit = _CommandObj.Parameters.Add("@ItemInfoEdit", SqlDbType.Bit);
                ItemInfoEdit.Direction = ParameterDirection.Input;
                ItemInfoEdit.Value = RequestData.ManagerOverrideData.ItemInfoEdit;

                SqlParameter TransactionSearch = _CommandObj.Parameters.Add("@TransactionSearch", SqlDbType.Bit);
                TransactionSearch.Direction = ParameterDirection.Input;
                TransactionSearch.Value = RequestData.ManagerOverrideData.TransactionSearch;

                SqlParameter SuspendRecall = _CommandObj.Parameters.Add("@SuspendRecall", SqlDbType.Bit);
                SuspendRecall.Direction = ParameterDirection.Input;
                SuspendRecall.Value = RequestData.ManagerOverrideData.SuspendRecall;

                SqlParameter CashOut = _CommandObj.Parameters.Add("@CashOut", SqlDbType.Bit);
                CashOut.Direction = ParameterDirection.Input;
                CashOut.Value = RequestData.ManagerOverrideData.CashOut;

                SqlParameter CashIn = _CommandObj.Parameters.Add("@CashIn", SqlDbType.Bit);
                CashIn.Direction = ParameterDirection.Input;
                CashIn.Value = RequestData.ManagerOverrideData.CashIn;

                SqlParameter TransactionRefund = _CommandObj.Parameters.Add("@TransactionRefund", SqlDbType.Bit);
                TransactionRefund.Direction = ParameterDirection.Input;
                TransactionRefund.Value = RequestData.ManagerOverrideData.TransactionRefund;

                SqlParameter TotalDiscount = _CommandObj.Parameters.Add("@TotalDiscount", SqlDbType.Bit);
                TotalDiscount.Direction = ParameterDirection.Input;
                TotalDiscount.Value = RequestData.ManagerOverrideData.TotalDiscount;

                SqlParameter DayInDayOut = _CommandObj.Parameters.Add("@DayInDayOut", SqlDbType.Bit);
                DayInDayOut.Direction = ParameterDirection.Input;
                DayInDayOut.Value = RequestData.ManagerOverrideData.DayInDayOut;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ManagerOverrideData.Active;

                SqlParameter AllowEditcustomer = _CommandObj.Parameters.Add("@AllowEditcustomer", SqlDbType.Bit);
                AllowEditcustomer.Direction = ParameterDirection.Input;
                AllowEditcustomer.Value = RequestData.ManagerOverrideData.AllowEditcustomer;

                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.ManagerOverrideData.UpdateBy;

           

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Manager Override");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = RequestData.ManagerOverrideData.ID.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Manager Override");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Manager Override");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Manager Override");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ManagerOverrideRecord = new ManagerOverride();
            var RequestData = (DeleteManagerOverrideRequest)RequestObj;
            var ResponseData = new DeleteManagerOverrideResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Delete from ManagerOverride where ID='" + RequestData.ID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Manager Override");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Manager Override");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ManagerOverrideRecord = new ManagerOverride();
            var RequestData = (SelectByIDManagerOverrideRequest)RequestObj;
            var ResponseData = new SelectByIDManagerOverrideResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select * from ManagerOverride  with(NoLock) where ID='" + RequestData.ID + "'" );
               
               

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objManagerOverride = new ManagerOverride();

                        objManagerOverride.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objManagerOverride.Code = Convert.ToString(objReader["Code"]);
                        objManagerOverride.Name = Convert.ToString(objReader["Name"]);
                        objManagerOverride.CreditLimitOverride = Convert.ToString(objReader["CreditLimitOverride"]);


                        objManagerOverride.ReprintTransReceipt = objReader["ReprintTransReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["ReprintTransReceipt"]) : true;
                        objManagerOverride.changeSalesPersoninSOE = objReader["changeSalesPersoninSOE"] != DBNull.Value ? Convert.ToBoolean(objReader["changeSalesPersoninSOE"]) : true;
                        objManagerOverride.ChangeSalesPersonRefund = objReader["ChangeSalesPersonRefund"] != DBNull.Value ? Convert.ToBoolean(objReader["ChangeSalesPersonRefund"]) : true;
                        objManagerOverride.DelSuspendedTransaction = objReader["DelSuspendedTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["DelSuspendedTransaction"]) : true;
                        objManagerOverride.VoidSale = objReader["VoidSale"] != DBNull.Value ? Convert.ToBoolean(objReader["VoidSale"]) : true;
                        objManagerOverride.voidItem = objReader["voidItem"] != DBNull.Value ? Convert.ToBoolean(objReader["voidItem"]) : true;
                        objManagerOverride.TransModeChange = objReader["TransModeChange"] != DBNull.Value ? Convert.ToBoolean(objReader["TransModeChange"]) : true;
                        objManagerOverride.CustomerSearch = objReader["CustomerSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["CustomerSearch"]) : true;
                        objManagerOverride.ProductSearch = objReader["ProductSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["ProductSearch"]) : true;
                        objManagerOverride.SaleInfoEdit = objReader["SaleInfoEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["SaleInfoEdit"]) : true;
                        objManagerOverride.ItemInfoEdit = objReader["ItemInfoEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["ItemInfoEdit"]) : true;
                        objManagerOverride.TransactionSearch = objReader["TransactionSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionSearch"]) : true;
                        objManagerOverride.SuspendRecall = objReader["SuspendRecall"] != DBNull.Value ? Convert.ToBoolean(objReader["SuspendRecall"]) : true;
                        objManagerOverride.CashOut = objReader["CashOut"] != DBNull.Value ? Convert.ToBoolean(objReader["CashOut"]) : true;
                        objManagerOverride.CashIn = objReader["CashIn"] != DBNull.Value ? Convert.ToBoolean(objReader["CashIn"]) : true;
                        objManagerOverride.DayInDayOut = objReader["DayInDayOut"] != DBNull.Value ? Convert.ToBoolean(objReader["DayInDayOut"]) : true;
                        objManagerOverride.TransactionRefund = objReader["TransactionRefund"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionRefund"]) : true;
                        objManagerOverride.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToBoolean(objReader["TotalDiscount"]) : false;                        
                        objManagerOverride.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objManagerOverride.AllowEditcustomer = objReader["AllowEditcustomer"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowEditcustomer"]) : true;
                        objManagerOverride.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objManagerOverride.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objManagerOverride.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objManagerOverride.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objManagerOverride.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

                        ResponseData.ManagerOverrideRecord = objManagerOverride;
                        ResponseData.ResponseDynamicData = objManagerOverride;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Manager Override");
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
            var ManagerOverrideList = new List<ManagerOverride>();
            var RequestData = (SelectAllManagerOverrideRequest)RequestObj;
            var ResponseData = new SelectAllManagerOverrideResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select * from ManagerOverride  with(NoLock) ");
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objManagerOverride = new ManagerOverride();


                        objManagerOverride.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objManagerOverride.Code = Convert.ToString(objReader["Code"]);
                        objManagerOverride.Name = Convert.ToString(objReader["Name"]);
                        objManagerOverride.CreditLimitOverride = Convert.ToString(objReader["CreditLimitOverride"]);
                        objManagerOverride.ReprintTransReceipt = objReader["ReprintTransReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["ReprintTransReceipt"]) : true;
                        objManagerOverride.changeSalesPersoninSOE = objReader["changeSalesPersoninSOE"] != DBNull.Value ? Convert.ToBoolean(objReader["changeSalesPersoninSOE"]) : true;
                        objManagerOverride.ChangeSalesPersonRefund = objReader["ChangeSalesPersonRefund"] != DBNull.Value ? Convert.ToBoolean(objReader["ChangeSalesPersonRefund"]) : true;
                        objManagerOverride.DelSuspendedTransaction = objReader["DelSuspendedTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["DelSuspendedTransaction"]) : true;
                        objManagerOverride.VoidSale = objReader["VoidSale"] != DBNull.Value ? Convert.ToBoolean(objReader["VoidSale"]) : true;
                        objManagerOverride.voidItem = objReader["voidItem"] != DBNull.Value ? Convert.ToBoolean(objReader["voidItem"]) : true;
                        objManagerOverride.TransModeChange = objReader["TransModeChange"] != DBNull.Value ? Convert.ToBoolean(objReader["TransModeChange"]) : true;
                        objManagerOverride.CustomerSearch = objReader["CustomerSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["CustomerSearch"]) : true;
                        objManagerOverride.ProductSearch = objReader["ProductSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["ProductSearch"]) : true;
                        objManagerOverride.SaleInfoEdit = objReader["SaleInfoEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["SaleInfoEdit"]) : true;
                        objManagerOverride.ItemInfoEdit = objReader["ItemInfoEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["ItemInfoEdit"]) : true;
                        objManagerOverride.TransactionSearch = objReader["TransactionSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionSearch"]) : true;
                        objManagerOverride.SuspendRecall = objReader["SuspendRecall"] != DBNull.Value ? Convert.ToBoolean(objReader["SuspendRecall"]) : true;
                        objManagerOverride.CashOut = objReader["CashOut"] != DBNull.Value ? Convert.ToBoolean(objReader["CashOut"]) : true;
                        objManagerOverride.CashIn = objReader["CashIn"] != DBNull.Value ? Convert.ToBoolean(objReader["CashIn"]) : true;
                        objManagerOverride.TransactionRefund = objReader["TransactionRefund"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionRefund"]) : true;
                        objManagerOverride.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToBoolean(objReader["TotalDiscount"]) : false;
                        objManagerOverride.DayInDayOut = objReader["DayInDayOut"] != DBNull.Value ? Convert.ToBoolean(objReader["DayInDayOut"]) : true;
                        objManagerOverride.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objManagerOverride.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objManagerOverride.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objManagerOverride.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objManagerOverride.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objManagerOverride.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objManagerOverride.AllowEditcustomer = objReader["AllowEditcustomer"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowEditcustomer"]) : true;

                        ManagerOverrideList.Add(objManagerOverride);                        
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ManagerOverrideList = ManagerOverrideList;
                    ResponseData.ResponseDynamicData = ManagerOverrideList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Manager Overide");
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

        public override SelectAllManagerOverrideResponse API_SelectAllManagerOverride(SelectAllManagerOverrideRequest RequestObj)
        {
            var ManagerOverrideList = new List<ManagerOverride>();
            var RequestData = (SelectAllManagerOverrideRequest)RequestObj;
            var ResponseData = new SelectAllManagerOverrideResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
               // StringBuilder sSql = new StringBuilder();
               // sSql.Append("Select Id,Code,Name,Active from ManagerOverride  with(NoLock) ");

                string sSql = "Select Id,Code,Name,Active, RC.TOTAL_CNT [RecordCount] " +
               "from ManagerOverride with(NoLock) " +
               "LEFT JOIN(Select  count(MO.ID) As TOTAL_CNT From ManagerOverride MO with(NoLock) " +
               " where MO.Active = " + RequestData.IsActive + " " +
                   "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or MO.Code like isnull('%" + RequestData.SearchString + "%','') " +
                       "or MO.Name like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +

               " where Active = " + RequestData.IsActive + " " +
                   "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or Code like isnull('%" + RequestData.SearchString + "%','') " +
                       "or Name like isnull('%" + RequestData.SearchString + "%','')) " +
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
                        var objManagerOverride = new ManagerOverride();


                        objManagerOverride.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objManagerOverride.Code = Convert.ToString(objReader["Code"]);
                        objManagerOverride.Name = Convert.ToString(objReader["Name"]);
                        //objManagerOverride.CreditLimitOverride = Convert.ToString(objReader["CreditLimitOverride"]);
                        //objManagerOverride.ReprintTransReceipt = objReader["ReprintTransReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["ReprintTransReceipt"]) : true;
                        //objManagerOverride.changeSalesPersoninSOE = objReader["changeSalesPersoninSOE"] != DBNull.Value ? Convert.ToBoolean(objReader["changeSalesPersoninSOE"]) : true;
                        //objManagerOverride.ChangeSalesPersonRefund = objReader["ChangeSalesPersonRefund"] != DBNull.Value ? Convert.ToBoolean(objReader["ChangeSalesPersonRefund"]) : true;
                        //objManagerOverride.DelSuspendedTransaction = objReader["DelSuspendedTransaction"] != DBNull.Value ? Convert.ToBoolean(objReader["DelSuspendedTransaction"]) : true;
                        //objManagerOverride.VoidSale = objReader["VoidSale"] != DBNull.Value ? Convert.ToBoolean(objReader["VoidSale"]) : true;
                        //objManagerOverride.voidItem = objReader["voidItem"] != DBNull.Value ? Convert.ToBoolean(objReader["voidItem"]) : true;
                        //objManagerOverride.TransModeChange = objReader["TransModeChange"] != DBNull.Value ? Convert.ToBoolean(objReader["TransModeChange"]) : true;
                        //objManagerOverride.CustomerSearch = objReader["CustomerSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["CustomerSearch"]) : true;
                        //objManagerOverride.ProductSearch = objReader["ProductSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["ProductSearch"]) : true;
                        //objManagerOverride.SaleInfoEdit = objReader["SaleInfoEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["SaleInfoEdit"]) : true;
                        //objManagerOverride.ItemInfoEdit = objReader["ItemInfoEdit"] != DBNull.Value ? Convert.ToBoolean(objReader["ItemInfoEdit"]) : true;
                        //objManagerOverride.TransactionSearch = objReader["TransactionSearch"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionSearch"]) : true;
                        //objManagerOverride.SuspendRecall = objReader["SuspendRecall"] != DBNull.Value ? Convert.ToBoolean(objReader["SuspendRecall"]) : true;
                        //objManagerOverride.CashOut = objReader["CashOut"] != DBNull.Value ? Convert.ToBoolean(objReader["CashOut"]) : true;
                        //objManagerOverride.CashIn = objReader["CashIn"] != DBNull.Value ? Convert.ToBoolean(objReader["CashIn"]) : true;
                        //objManagerOverride.TransactionRefund = objReader["TransactionRefund"] != DBNull.Value ? Convert.ToBoolean(objReader["TransactionRefund"]) : true;
                        //objManagerOverride.TotalDiscount = objReader["TotalDiscount"] != DBNull.Value ? Convert.ToBoolean(objReader["TotalDiscount"]) : false;
                        //objManagerOverride.DayInDayOut = objReader["DayInDayOut"] != DBNull.Value ? Convert.ToBoolean(objReader["DayInDayOut"]) : true;
                        //objManagerOverride.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objManagerOverride.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objManagerOverride.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objManagerOverride.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objManagerOverride.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objManagerOverride.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objManagerOverride.AllowEditcustomer = objReader["AllowEditcustomer"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowEditcustomer"]) : true;

                        ManagerOverrideList.Add(objManagerOverride);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ManagerOverrideList = ManagerOverrideList;
                    ResponseData.ResponseDynamicData = ManagerOverrideList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Manager Overide");
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
