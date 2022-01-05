using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EasyBizAbsDAL.Transactions.POS;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizRequest;
using EasyBizRequest.Transactions.POS.SalesReturnReceipt;
using EasyBizRequest.Transactions.POS.SalesReturnRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.POS.SalesReturnReceipt;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
using MsSqlDAL.Common;
using ResourceStrings;

namespace MsSqlDAL.Transactions.POS
{

    //public class SalesReturnReceiptDAL : BaseSalesReturnReceiptDAL
    //{

    //    SqlConnection _ConnectionObj;
    //    SqlCommand _CommandObj;

    //    string _ConnectionString; Enums.RequestFrom _RequestFrom;

    //    public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //#region SelectBillCompletedSalesInvoice

    //    //public override SalesReturnReceiptResponse SalesReturnReceiptdtls(SalesReturnReceiptRequest objRequest)
    //    //{

    //    //    var SalesReturnReceiptList = new List<SalesReturnReceipt>();
    //    //    var RequestData = (SalesReturnReceiptRequest)objRequest;
    //    //    var ResponseData = new SalesReturnReceiptResponse();
    //    //    SqlDataReader objReader;
    //    //    var sqlCommon = new MsSqlCommon();
    //    //    try
    //    //    {
    //    //        _ConnectionString = RequestData.ConnectionString;
    //    //        _RequestFrom = RequestData.RequestFrom;

    //    //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


    //    //        string sSql = string.Empty;
    //    //        //var InvoiceDetailsList = new List<InvoiceDetails>();

    //    //        //string BusinessDate = sqlCommon.SearchByDate(RequestData.SearchString);
    //    //        _CommandObj = new SqlCommand("SP_DetailedSalesInvoice", _ConnectionObj);
    //    //        _CommandObj.CommandType = CommandType.StoredProcedure;
    //    //        _CommandObj.Parameters.AddWithValue("@InvoiceNo", RequestData.InvoiceNo);

    //    //        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.InvoiceNo);
    //    //        _CommandObj.Parameters.AddWithValue("@MODE", RequestData.InvoiceNo);


    //    //        objReader = _CommandObj.ExecuteReader();
    //    //        if (objReader.HasRows)
    //    //        {
    //    //            while (objReader.Read())
    //    //            {
    //    //                var objInvoiceHeaderTypes = new SalesReturnReceipt();


    //    //            //    objInvoiceHeaderTypes.ID = Convert.ToInt64(objReader["ID"]);
    //    //            //    objInvoiceHeaderTypes.CountryID = Convert.ToInt32(objReader["CountryID"]);
    //    //            //    objInvoiceHeaderTypes.StoreID = Convert.ToInt32(objReader["StoreID"]);
    //    //            //    objInvoiceHeaderTypes.PosID = Convert.ToInt32(objReader["PosID"]);
    //    //            //    objInvoiceHeaderTypes.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
    //    //            //    objInvoiceHeaderTypes.BusinessDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]) : DateTime.Now;
    //    //            //    objInvoiceHeaderTypes.InvoiceNo = objReader["InvoiceNo"] != DBNull.Value ? Convert.ToString(objReader["InvoiceNo"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.CustomerGroupID = objReader["CustomerGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroupID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.CustomerID = objReader["CustomerID"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.TotalQty = objReader["TotalQty"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQty"]) : 0;
    //    //            //    objInvoiceHeaderTypes.SubTotalAmount = objReader["SubTotalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalAmount"]) : 0;
    //    //            //    objInvoiceHeaderTypes.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.TaxAmount = objReader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["TaxAmount"]) : 0;
    //    //            //    objInvoiceHeaderTypes.SubTotalWithTaxAmount = objReader["SubTotalWithTaxAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["SubTotalWithTaxAmount"]) : 0;
    //    //            //    objInvoiceHeaderTypes.NetAmount = objReader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["NetAmount"]) : 0;
    //    //            //    objInvoiceHeaderTypes.ReceivedAmount = objReader["ReceivedAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReceivedAmount"]) : 0;
    //    //            //    objInvoiceHeaderTypes.ReturnAmount = objReader["ReturnAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["ReturnAmount"]) : 0;
    //    //            //    objInvoiceHeaderTypes.SalesStatus = objReader["SalesStatus"] != DBNull.Value ? Convert.ToString(objReader["SalesStatus"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.AppliedPriceListID = objReader["AppliedPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPriceListID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.AppliedCustomerSpecialPriceID = objReader["AppliedCustomerSpecialPricesID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedCustomerSpecialPricesID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.AppliedPromotionID = objReader["AppliedPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["AppliedPromotionID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.IsCreditSale = objReader["IsCreditSale"] != DBNull.Value ? Convert.ToBoolean(objReader["IsCreditSale"]) : true;
    //    //            //    objInvoiceHeaderTypes.SalesEmployeeID = objReader["SalesEmployeeID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesEmployeeID"]) : 0;
    //    //            //    objInvoiceHeaderTypes.SalesManagerID = objReader["SalesManagerID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesManagerID"]) : 0;

    //    //            //    objInvoiceHeaderTypes.IsDataSyncToCountryServer = objReader["IsDataSyncToCountryServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToCountryServer"]) : true;
    //    //            //    objInvoiceHeaderTypes.IsDataSyncToMainServer = objReader["IsDataSyncToMainServer"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDataSyncToMainServer"]) : true;
    //    //            //    objInvoiceHeaderTypes.CountryServerSyncTime = objReader["CountryServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["CountryServerSyncTime"]) : DateTime.Now;
    //    //            //    objInvoiceHeaderTypes.MainServerSyncTime = objReader["MainServerSyncTime"] != DBNull.Value ? Convert.ToDateTime(objReader["MainServerSyncTime"]) : DateTime.Now;

    //    //            //    objInvoiceHeaderTypes.SyncFailedReason = objReader["SyncFailedReason"] != DBNull.Value ? Convert.ToString(objReader["SyncFailedReason"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
    //    //            //    objInvoiceHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
    //    //            //    objInvoiceHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
    //    //            //    objInvoiceHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
    //    //            //    objInvoiceHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
    //    //            //    objInvoiceHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;

    //    //            //    //objInvoiceHeaderTypes.FromCountryID = objReader["FromCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["FromCountryID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnRefID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;


    //    //            //    //objInvoiceHeaderTypes.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeRefID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.FromPosID = objReader["FromPosID"] != DBNull.Value ? Convert.ToInt32(objReader["FromPosID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ShiftID = objReader["ShiftID"] != DBNull.Value ? Convert.ToInt32(objReader["ShiftID"]) : 0;

    //    //            //    //objInvoiceHeaderTypes.ExchangeRefID = objReader["ExchangeRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeRefID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ExchangeQty = objReader["ExchangeQty"] != DBNull.Value ? Convert.ToInt32(objReader["ExchangeQty"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ReturnRefID = objReader["ReturnRefID"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnRefID"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.ReturnQty = objReader["ReturnQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReturnQty"]) : 0;

    //    //            //    objInvoiceHeaderTypes.TotalDiscountPercentage = objReader["TotalDiscountPercentage"] != DBNull.Value ? Convert.ToInt32(objReader["TotalDiscountPercentage"]) : 0;
    //    //            //    objInvoiceHeaderTypes.CashierID = objReader["CashierID"] != DBNull.Value ? Convert.ToInt32(objReader["CashierID"]) : 0;

    //    //            //    //objInvoiceHeaderTypes.OrjwanEntry = objReader["OrjwanEntry"] != DBNull.Value ? Convert.ToInt32(objReader["OrjwanEntry"]) : 0;
    //    //            //    //objInvoiceHeaderTypes.IsDataSyncToOrjwan = objReader["IsDataSyncToOrjwan"] != DBNull.Value ? Convert.ToInt32(objReader["IsDataSyncToOrjwan"]) : 0;

    //    //            //    //  objInvoiceHeaderTypes.OrjwanServerSyncTime = objReader["OrjwanServerSyncTime"] != DBNull.Value ? Convert.ToInt32(objReader["OrjwanServerSyncTime"]) : 0;



    //    //            //    objInvoiceHeaderTypes.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.PosCode = objReader["PosCode"] != DBNull.Value ? Convert.ToString(objReader["PosCode"]) : string.Empty;
    //    //            //    // objInvoiceHeaderTypes.CustomerGroupCode = objReader["CustomerGroupCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerGroupCode"]) : string.Empty;

    //    //            //    objInvoiceHeaderTypes.CustomerCode = objReader["CustomerCode"] != DBNull.Value ? Convert.ToString(objReader["CustomerCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.TaxCode = objReader["TaxCode"] != DBNull.Value ? Convert.ToString(objReader["TaxCode"]) : string.Empty;
    //    //            //    // objInvoiceHeaderTypes.AppliedPriceListCode = objReader["AppliedPriceListCode"] != DBNull.Value ? Convert.ToString(objReader["AppliedPriceListCode"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.SalesEmployeeCode = objReader["SalesEmployeeCode"] != DBNull.Value ? Convert.ToString(objReader["SalesEmployeeCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.SalesManagerCode = objReader["SalesManagerCode"] != DBNull.Value ? Convert.ToString(objReader["SalesManagerCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.ShiftCode = objReader["ShiftCode"] != DBNull.Value ? Convert.ToString(objReader["ShiftCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.FromCountryCode = objReader["FromCountryCode"] != DBNull.Value ? Convert.ToString(objReader["FromCountryCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.FromStoreCode = objReader["FromStoreCode"] != DBNull.Value ? Convert.ToString(objReader["FromStoreCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.FromPosCode = objReader["FromPosCode"] != DBNull.Value ? Convert.ToString(objReader["FromPosCode"]) : string.Empty;


    //    //            //    //objInvoiceHeaderTypes.ShiftCode = objReader["ShiftCode"] != DBNull.Value ? Convert.ToString(objReader["ShiftCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.ExchangeRefCode = objReader["ExchangeRefCode"] != DBNull.Value ? Convert.ToString(objReader["ExchangeRefCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.ReturnRefCode = objReader["ReturnRefCode"] != DBNull.Value ? Convert.ToString(objReader["ReturnRefCode"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.CashierCode = objReader["CashierCode"] != DBNull.Value ? Convert.ToString(objReader["CashierCode"]) : string.Empty;

    //    //            //    //objInvoiceHeaderTypes.IsDataSyncToOtherStores = objReader["IsDataSyncToOtherStores"] != DBNull.Value ? Convert.ToString(objReader["IsDataSyncToOtherStores"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.DataSyncToOtherStoresTime = objReader["DataSyncToOtherStoresTime"] != DBNull.Value ? Convert.ToString(objReader["DataSyncToOtherStoresTime"]) : string.Empty;

    //    //            //    objInvoiceHeaderTypes.DiscountRemarks = objReader["DiscountRemarks"] != DBNull.Value ? Convert.ToString(objReader["DiscountRemarks"]) : string.Empty;
    //    //            //    // objInvoiceHeaderTypes.EmailSend = objReader["EmailSend"] != DBNull.Value ? Convert.ToString(objReader["EmailSend"]) : string.Empty;
    //    //            //    // objInvoiceHeaderTypes.SMSSend = objReader["SMSSend"] != DBNull.Value ? Convert.ToString(objReader["SMSSend"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.BeforeRoundOffAmount = objReader["BeforeRoundOffAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BeforeRoundOffAmount"]) : 0;

    //    //            //    objInvoiceHeaderTypes.RoundOffAmount = objReader["RoundOffAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["RoundOffAmount"]) : 0;
    //    //            //    // objInvoiceHeaderTypes.DocDetailID = objReader["DocDetailID"] != DBNull.Value ? Convert.ToString(objReader["DocDetailID"]) : string.Empty;

    //    //            //    // objInvoiceHeaderTypes.DocRunningNo = objReader["DocRunningNo"] != DBNull.Value ? Convert.ToString(objReader["DocRunningNo"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.CustomerName = objReader["CustomerName"] != DBNull.Value ? Convert.ToString(objReader["CustomerName"]) : string.Empty;
    //    //            //    //objInvoiceHeaderTypes.PhoneNumber = objReader["PhoneNumber"] != DBNull.Value ? Convert.ToString(objReader["PhoneNumber"]) : string.Empty;
    //    //            //    objInvoiceHeaderTypes.InvoiceDetailList = InvoiceDetailsList;
    //    //            //    //objInvoiceHeaderTypes.Add(InvoiceDetailsList);

    //    //            //    InvoiceHeaderList.Add(objInvoiceHeaderTypes);

    //    //            }

    //    //            //ResponseData.InvoiceHeaderList = InvoiceHeaderList;
    //    //            //ResponseData.ResponseDynamicData = InvoiceHeaderList;
    //    //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
    //    //        }
    //    //        else
    //    //        {
    //    //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
    //    //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Invoice Header");
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
    //    //        ResponseData.DisplayMessage = ex.Message;
    //    //    }
    //    //    finally
    //    //    {
    //    //        sqlCommon.CloseConnection(_ConnectionObj);
    //    //    }
    //    //    return ResponseData;
    //    //    throw new NotImplementedException();
    //    //}

    //    //public override BaseResponseType SelectAll(BaseRequestType RequestObj)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //#endregion

    //}

}