using EasyBizDBTypes.Transactions.POS.SalesExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{
    public static partial class DeepCopyCreator
    {
        public static List<SalesExchangeDetail> SalesExchangeDetailDeepCopyCreator(List<SalesExchangeDetail> ExchangeList)
        {
            var tempExchangeList = new List<SalesExchangeDetail>();
            foreach (SalesExchangeDetail objSalesExchangeDetail in ExchangeList)
            {
                var TempSalesExchangeDetail = new SalesExchangeDetail();
                TempSalesExchangeDetail.ID = objSalesExchangeDetail.ID;
                TempSalesExchangeDetail.SalesExchangeID = objSalesExchangeDetail.SalesExchangeID;
                TempSalesExchangeDetail.SKUID = objSalesExchangeDetail.SKUID;
                TempSalesExchangeDetail.InvoiceHeaderID = objSalesExchangeDetail.InvoiceHeaderID;
                TempSalesExchangeDetail.SKUCode = objSalesExchangeDetail.SKUCode;
                TempSalesExchangeDetail.ModifiedSalesEmployee = objSalesExchangeDetail.ModifiedSalesEmployee;
                TempSalesExchangeDetail.ModifiedSalesManager = objSalesExchangeDetail.ModifiedSalesManager;
                TempSalesExchangeDetail.IsDataSyncToCountryServer = objSalesExchangeDetail.IsDataSyncToCountryServer;
                TempSalesExchangeDetail.IsDataSyncToMainServer = objSalesExchangeDetail.IsDataSyncToMainServer;
                TempSalesExchangeDetail.CountryServerSyncTime = objSalesExchangeDetail.CountryServerSyncTime;
                TempSalesExchangeDetail.MainServerSyncTime = objSalesExchangeDetail.MainServerSyncTime;
                TempSalesExchangeDetail.SyncFailedReason = objSalesExchangeDetail.SyncFailedReason;
                TempSalesExchangeDetail.CountryID = objSalesExchangeDetail.CountryID;
                TempSalesExchangeDetail.StoreID = objSalesExchangeDetail.StoreID;
                TempSalesExchangeDetail.PosID = objSalesExchangeDetail.PosID;
                TempSalesExchangeDetail.ExchangeQty = objSalesExchangeDetail.ExchangeQty;
                TempSalesExchangeDetail.ExchangedQty = objSalesExchangeDetail.ExchangedQty; // Exisiting Exchange Qty
                TempSalesExchangeDetail.InvoiceDetailID = objSalesExchangeDetail.InvoiceDetailID;
                TempSalesExchangeDetail.StyleCode = objSalesExchangeDetail.StyleCode;
                TempSalesExchangeDetail.IsExchange = objSalesExchangeDetail.IsExchange;
                TempSalesExchangeDetail.ExchangeRemarks = objSalesExchangeDetail.ExchangeRemarks;
                TempSalesExchangeDetail.ExchangedSKU = objSalesExchangeDetail.ExchangedSKU;
                TempSalesExchangeDetail.IsExchanged = objSalesExchangeDetail.IsExchanged;
                TempSalesExchangeDetail.EnableCell = objSalesExchangeDetail.EnableCell;
                TempSalesExchangeDetail.Qty = objSalesExchangeDetail.Qty;
                TempSalesExchangeDetail.SellingPricePerQty = objSalesExchangeDetail.SellingPricePerQty;
                TempSalesExchangeDetail.ExchangeQty = objSalesExchangeDetail.ExchangeQty;
                TempSalesExchangeDetail.IsExchanged = objSalesExchangeDetail.IsExchanged;
                TempSalesExchangeDetail.ReturnQty = objSalesExchangeDetail.ReturnQty;
                TempSalesExchangeDetail.IsReturned = objSalesExchangeDetail.IsReturned;
                TempSalesExchangeDetail.CountryCode = objSalesExchangeDetail.CountryCode;
                TempSalesExchangeDetail.StoreCode = objSalesExchangeDetail.StoreCode;
                TempSalesExchangeDetail.POSCode = objSalesExchangeDetail.POSCode;
                TempSalesExchangeDetail.InvoiceSerialNo = objSalesExchangeDetail.InvoiceSerialNo;
                TempSalesExchangeDetail.InvoiceType = objSalesExchangeDetail.InvoiceType;
                TempSalesExchangeDetail.TaxID = objSalesExchangeDetail.TaxID;
                TempSalesExchangeDetail.TaxAmount = objSalesExchangeDetail.TaxAmount;
                TempSalesExchangeDetail.ExchangeSKU = objSalesExchangeDetail.ExchangeSKU;
                TempSalesExchangeDetail.Tag_Id = objSalesExchangeDetail.Tag_Id;
                TempSalesExchangeDetail.CreditSales = objSalesExchangeDetail.CreditSales;
                TempSalesExchangeDetail.SalesInvoiceNumber = objSalesExchangeDetail.SalesInvoiceNumber;
                tempExchangeList.Add(TempSalesExchangeDetail);
            }
            return tempExchangeList;
        }
        public static SalesExchangeDetail SalesExchangeRecordDeepCopyCreator(SalesExchangeDetail objSalesExchangeDetail)
        {
            var TempSalesExchangeDetail = new SalesExchangeDetail();
            TempSalesExchangeDetail.ID = objSalesExchangeDetail.ID;
            TempSalesExchangeDetail.SalesExchangeID = objSalesExchangeDetail.SalesExchangeID;
            TempSalesExchangeDetail.SKUID = objSalesExchangeDetail.SKUID;
            TempSalesExchangeDetail.InvoiceHeaderID = objSalesExchangeDetail.InvoiceHeaderID;
            TempSalesExchangeDetail.SKUCode = objSalesExchangeDetail.SKUCode;
            TempSalesExchangeDetail.ModifiedSalesEmployee = objSalesExchangeDetail.ModifiedSalesEmployee;
            TempSalesExchangeDetail.ModifiedSalesManager = objSalesExchangeDetail.ModifiedSalesManager;
            TempSalesExchangeDetail.IsDataSyncToCountryServer = objSalesExchangeDetail.IsDataSyncToCountryServer;
            TempSalesExchangeDetail.IsDataSyncToMainServer = objSalesExchangeDetail.IsDataSyncToMainServer;
            TempSalesExchangeDetail.CountryServerSyncTime = objSalesExchangeDetail.CountryServerSyncTime;
            TempSalesExchangeDetail.MainServerSyncTime = objSalesExchangeDetail.MainServerSyncTime;
            TempSalesExchangeDetail.SyncFailedReason = objSalesExchangeDetail.SyncFailedReason;
            TempSalesExchangeDetail.CountryID = objSalesExchangeDetail.CountryID;
            TempSalesExchangeDetail.StoreID = objSalesExchangeDetail.StoreID;
            TempSalesExchangeDetail.PosID = objSalesExchangeDetail.PosID;
            TempSalesExchangeDetail.ExchangeQty = objSalesExchangeDetail.ExchangeQty;
            TempSalesExchangeDetail.ExchangedQty = objSalesExchangeDetail.ExchangedQty; // Exisiting Exchange Qty
            TempSalesExchangeDetail.InvoiceDetailID = objSalesExchangeDetail.InvoiceDetailID;
            TempSalesExchangeDetail.StyleCode = objSalesExchangeDetail.StyleCode;
            TempSalesExchangeDetail.IsExchange = objSalesExchangeDetail.IsExchange;
            TempSalesExchangeDetail.ExchangeRemarks = objSalesExchangeDetail.ExchangeRemarks;
            TempSalesExchangeDetail.ExchangedSKU = objSalesExchangeDetail.ExchangedSKU;
            TempSalesExchangeDetail.IsExchanged = objSalesExchangeDetail.IsExchanged;
            TempSalesExchangeDetail.EnableCell = objSalesExchangeDetail.EnableCell;
            TempSalesExchangeDetail.Qty = objSalesExchangeDetail.Qty;
            TempSalesExchangeDetail.SellingPricePerQty = objSalesExchangeDetail.SellingPricePerQty;
            TempSalesExchangeDetail.CountryCode = objSalesExchangeDetail.CountryCode;
            TempSalesExchangeDetail.StoreCode = objSalesExchangeDetail.StoreCode;
            TempSalesExchangeDetail.POSCode = objSalesExchangeDetail.POSCode;
            
            TempSalesExchangeDetail.ExchangeQty = objSalesExchangeDetail.ExchangeQty;
            TempSalesExchangeDetail.IsExchanged = objSalesExchangeDetail.IsExchanged;
            TempSalesExchangeDetail.ReturnQty = objSalesExchangeDetail.ReturnQty;
            TempSalesExchangeDetail.IsReturned = objSalesExchangeDetail.IsReturned;

            TempSalesExchangeDetail.InvoiceSerialNo = objSalesExchangeDetail.InvoiceSerialNo;
            TempSalesExchangeDetail.InvoiceType = objSalesExchangeDetail.InvoiceType;

            TempSalesExchangeDetail.TaxID = objSalesExchangeDetail.TaxID;
            TempSalesExchangeDetail.TaxAmount = objSalesExchangeDetail.TaxAmount;

            TempSalesExchangeDetail.ExchangeSKU = objSalesExchangeDetail.ExchangeSKU;
            TempSalesExchangeDetail.Tag_Id = objSalesExchangeDetail.Tag_Id;
            TempSalesExchangeDetail.CreditSales = objSalesExchangeDetail.CreditSales;
            TempSalesExchangeDetail.SalesInvoiceNumber = objSalesExchangeDetail.SalesInvoiceNumber;

            return TempSalesExchangeDetail;
        }
    }
}
