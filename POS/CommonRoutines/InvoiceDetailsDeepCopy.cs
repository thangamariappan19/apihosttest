using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonRoutines
{

    public static partial class DeepCopyCreator
    {
        public static InvoiceDetails InvoiceDetailsDeepCopy(InvoiceDetails ObjInvoiceDetails)
        {
            var tempInvoiceDetails = new InvoiceDetails();

            tempInvoiceDetails.SKUID = ObjInvoiceDetails.SKUID;
            tempInvoiceDetails.InvoiceHeaderID = ObjInvoiceDetails.InvoiceHeaderID;
            tempInvoiceDetails.SKUCode = ObjInvoiceDetails.SKUCode;
            tempInvoiceDetails.BrandID = ObjInvoiceDetails.BrandID;
            tempInvoiceDetails.SubBrandID = ObjInvoiceDetails.SubBrandID;
            tempInvoiceDetails.Category = ObjInvoiceDetails.Category;
            tempInvoiceDetails.Qty = ObjInvoiceDetails.Qty;
            tempInvoiceDetails.DummyQty = ObjInvoiceDetails.DummyQty;
            tempInvoiceDetails.Price = ObjInvoiceDetails.Price;
            tempInvoiceDetails.DummyPrice = ObjInvoiceDetails.DummyPrice;
            tempInvoiceDetails.DiscountType = ObjInvoiceDetails.DiscountType;
            tempInvoiceDetails.DiscountAmount = ObjInvoiceDetails.DiscountAmount;
            tempInvoiceDetails.AppliedPriceListID = ObjInvoiceDetails.AppliedPriceListID;
            tempInvoiceDetails.AppliedCustomerSpecialPricesID = ObjInvoiceDetails.AppliedCustomerSpecialPricesID;
            tempInvoiceDetails.AppliedPromotionID = ObjInvoiceDetails.AppliedPromotionID;
            tempInvoiceDetails.SalesStatus = ObjInvoiceDetails.SalesStatus;
            tempInvoiceDetails.ModifiedSalesEmployee = ObjInvoiceDetails.ModifiedSalesEmployee;
            tempInvoiceDetails.ModifiedSalesManager = ObjInvoiceDetails.ModifiedSalesManager;
            tempInvoiceDetails.IsDataSyncToCountryServer = ObjInvoiceDetails.IsDataSyncToCountryServer;
            tempInvoiceDetails.IsDataSyncToMainServer = ObjInvoiceDetails.IsDataSyncToMainServer;
            tempInvoiceDetails.CountryServerSyncTime = ObjInvoiceDetails.CountryServerSyncTime;
            tempInvoiceDetails.MainServerSyncTime = ObjInvoiceDetails.MainServerSyncTime;
            tempInvoiceDetails.SyncFailedReason = ObjInvoiceDetails.SyncFailedReason;
            tempInvoiceDetails.LineTotal = ObjInvoiceDetails.LineTotal;
            tempInvoiceDetails.PromotionAmount = ObjInvoiceDetails.PromotionAmount;
            tempInvoiceDetails.SerialNo = ObjInvoiceDetails.SerialNo;
            tempInvoiceDetails.PromotionName = ObjInvoiceDetails.PromotionName;
            tempInvoiceDetails.PromtionApplied = ObjInvoiceDetails.PromtionApplied;
            tempInvoiceDetails.InvoiceType = ObjInvoiceDetails.InvoiceType;
            tempInvoiceDetails.LinkedSrlNo = ObjInvoiceDetails.LinkedSrlNo;
            tempInvoiceDetails.IsRecordVisible = ObjInvoiceDetails.IsRecordVisible;
            tempInvoiceDetails.SingleDiscountAmount = ObjInvoiceDetails.SingleDiscountAmount;
            tempInvoiceDetails.CountryID = ObjInvoiceDetails.CountryID;
            tempInvoiceDetails.StoreID = ObjInvoiceDetails.StoreID;
            tempInvoiceDetails.TaxAmount = ObjInvoiceDetails.TaxAmount;
            tempInvoiceDetails.TaxID = ObjInvoiceDetails.TaxID;
            tempInvoiceDetails.AppliedPromotionID = ObjInvoiceDetails.AppliedPromotionID;
            tempInvoiceDetails.SKUImage = ObjInvoiceDetails.SKUImage;
            tempInvoiceDetails.SubBrandName = ObjInvoiceDetails.SubBrandName;
            tempInvoiceDetails.CountryCode = ObjInvoiceDetails.CountryCode;
            tempInvoiceDetails.StoreCode = ObjInvoiceDetails.StoreCode;
            tempInvoiceDetails.PosCode = ObjInvoiceDetails.PosCode;
            tempInvoiceDetails.InvoiceNo = ObjInvoiceDetails.InvoiceNo;
            tempInvoiceDetails.SellingPrice = ObjInvoiceDetails.SellingPrice;
            tempInvoiceDetails.SellingLineTotal = ObjInvoiceDetails.SellingLineTotal;
            tempInvoiceDetails.NetAmount = ObjInvoiceDetails.NetAmount;
            tempInvoiceDetails.DiscountRemarks = ObjInvoiceDetails.DiscountRemarks;
            tempInvoiceDetails.FamilyDiscountAmount = ObjInvoiceDetails.FamilyDiscountAmount;
            tempInvoiceDetails.EmployeeDiscountAmount = ObjInvoiceDetails.EmployeeDiscountAmount;
            tempInvoiceDetails.EmployeeDiscountID = ObjInvoiceDetails.EmployeeDiscountID;
            tempInvoiceDetails.SpecialDiscountType = ObjInvoiceDetails.SpecialDiscountType;
            tempInvoiceDetails.IsPromoExcludeItem = ObjInvoiceDetails.IsPromoExcludeItem;
            tempInvoiceDetails.SpecialPromoDiscountType = ObjInvoiceDetails.SpecialPromoDiscountType;
            tempInvoiceDetails.SpecialPromoDiscountPercentage = ObjInvoiceDetails.SpecialPromoDiscountPercentage;
            tempInvoiceDetails.SpecialPromoDiscount = ObjInvoiceDetails.SpecialPromoDiscount;
            tempInvoiceDetails.IsFreeItem = ObjInvoiceDetails.IsFreeItem;
            tempInvoiceDetails.Tag_Id = ObjInvoiceDetails.Tag_Id;
            tempInvoiceDetails.PromoGroupID = ObjInvoiceDetails.PromoGroupID;
            tempInvoiceDetails.CustomerCode = ObjInvoiceDetails.CustomerCode;
            tempInvoiceDetails.ExchangedSKU = ObjInvoiceDetails.ExchangedSKU;
            return tempInvoiceDetails;
        }

        public static StockRequestDetails StockDeepCopy(StockRequestDetails ObjInvoiceDetails)
        {
            var tempInvoiceDetails = new StockRequestDetails();
            tempInvoiceDetails.SerialNo = ObjInvoiceDetails.SerialNo;
            tempInvoiceDetails.SKUCode = ObjInvoiceDetails.SKUCode;
            tempInvoiceDetails.StyleCode = ObjInvoiceDetails.StyleCode;
            tempInvoiceDetails.SKUName = ObjInvoiceDetails.SKUName;                           
            tempInvoiceDetails.Brand = ObjInvoiceDetails.Brand;
            tempInvoiceDetails.Size = ObjInvoiceDetails.Size;
            tempInvoiceDetails.Color = ObjInvoiceDetails.Color;
            tempInvoiceDetails.Quantity = ObjInvoiceDetails.Quantity;
            tempInvoiceDetails.BarCode = ObjInvoiceDetails.BarCode;
            tempInvoiceDetails.SKUID = ObjInvoiceDetails.SKUID;
            tempInvoiceDetails.ID = ObjInvoiceDetails.ID;    
            return tempInvoiceDetails;
        }
        public static OpeningStockDetails OpeningStockDeepCopy(OpeningStockDetails ObjInvoiceDetails)
        {
            var tempInvoiceDetails = new OpeningStockDetails();
            tempInvoiceDetails.SerialNo = ObjInvoiceDetails.SerialNo;
            tempInvoiceDetails.SKUCode = ObjInvoiceDetails.SKUCode;
            tempInvoiceDetails.StyleCode = ObjInvoiceDetails.StyleCode;
            tempInvoiceDetails.SKUName = ObjInvoiceDetails.SKUName;
            tempInvoiceDetails.Brand = ObjInvoiceDetails.Brand;
            tempInvoiceDetails.Size = ObjInvoiceDetails.Size;
            tempInvoiceDetails.Color = ObjInvoiceDetails.Color;
            tempInvoiceDetails.Quantity = ObjInvoiceDetails.Quantity;
            tempInvoiceDetails.BarCode = ObjInvoiceDetails.BarCode;
            tempInvoiceDetails.SKUID = ObjInvoiceDetails.SKUID;
            tempInvoiceDetails.FromStoreID = ObjInvoiceDetails.FromStoreID;
            tempInvoiceDetails.FromStoreCode = ObjInvoiceDetails.FromStoreCode;
            tempInvoiceDetails.SKUID = ObjInvoiceDetails.SKUID;
            tempInvoiceDetails.ID = ObjInvoiceDetails.ID;
            return tempInvoiceDetails;
        }
        public static DenominationForShiftOutType DenominationDeepCopy(DenominationForShiftOutType ObjDenominationDetails)
        {
            var tempDenominationDetails = new DenominationForShiftOutType();
            tempDenominationDetails.CurrencyCode = ObjDenominationDetails.CurrencyCode;
            tempDenominationDetails.CurrencyValue = ObjDenominationDetails.CurrencyValue;
            tempDenominationDetails.PaymemtValue = ObjDenominationDetails.PaymemtValue;          
            tempDenominationDetails.TotalValue = ObjDenominationDetails.TotalValue;
            return tempDenominationDetails;
        }
        public static PaymentTypeMasterType CardDeepCopy(PaymentTypeMasterType ObjDenominationDetails)
        {
            var tempCardDetails = new PaymentTypeMasterType();
            tempCardDetails.PaymentCode = ObjDenominationDetails.PaymentCode;
            tempCardDetails.PaymentName = ObjDenominationDetails.PaymentName;
            tempCardDetails.PaymemtValue = ObjDenominationDetails.PaymemtValue;
        
            return tempCardDetails;
        }
        public static StockReturnDetails StockReturnDeepCopy(StockReturnDetails ObjInvoiceDetails)
        {
            var tempInvoiceDetails = new StockReturnDetails();
            tempInvoiceDetails.SerialNo = ObjInvoiceDetails.SerialNo;
            tempInvoiceDetails.SKUCode = ObjInvoiceDetails.SKUCode;
            tempInvoiceDetails.StyleCode = ObjInvoiceDetails.StyleCode;
            tempInvoiceDetails.SKUName = ObjInvoiceDetails.SKUName;
            tempInvoiceDetails.Brand = ObjInvoiceDetails.Brand;
            tempInvoiceDetails.Size = ObjInvoiceDetails.Size;
            tempInvoiceDetails.Color = ObjInvoiceDetails.Color;
            tempInvoiceDetails.Quantity = ObjInvoiceDetails.Quantity;
            tempInvoiceDetails.StockQty = ObjInvoiceDetails.StockQty;
            tempInvoiceDetails.BarCode = ObjInvoiceDetails.BarCode;
            tempInvoiceDetails.SKUID = ObjInvoiceDetails.SKUID;
            tempInvoiceDetails.Remarks = ObjInvoiceDetails.Remarks;
            tempInvoiceDetails.ID = ObjInvoiceDetails.ID;
            tempInvoiceDetails.ExistingQty = ObjInvoiceDetails.ExistingQty;
            tempInvoiceDetails.Tag_Id = ObjInvoiceDetails.Tag_Id;
            return tempInvoiceDetails;
        }
        public static StockReceiptDetails StockReceiptDeepCopy(StockReceiptDetails ObjInvoiceDetails)
        {
            var tempInvoiceDetails = new StockReceiptDetails();
            tempInvoiceDetails.SerialNo = ObjInvoiceDetails.SerialNo;
            tempInvoiceDetails.SKUCode = ObjInvoiceDetails.SKUCode;
            tempInvoiceDetails.StyleCode = ObjInvoiceDetails.StyleCode;
            tempInvoiceDetails.SKUName = ObjInvoiceDetails.SKUName;
            tempInvoiceDetails.Brand = ObjInvoiceDetails.Brand;
            tempInvoiceDetails.Size = ObjInvoiceDetails.Size;
            tempInvoiceDetails.Color = ObjInvoiceDetails.Color;
            tempInvoiceDetails.Quantity = ObjInvoiceDetails.Quantity;
            tempInvoiceDetails.SKUID = ObjInvoiceDetails.SKUID;
            tempInvoiceDetails.Tag_Id = ObjInvoiceDetails.Tag_Id;
            return tempInvoiceDetails;
        }
        public static List<StockReceiptDetails> StockReceiptDeepCopy(List<StockReceiptDetails> TempStockReceiptDetailsList)
        {
            var StockReceiptDetailsList = new List<StockReceiptDetails>();
            foreach (StockReceiptDetails ObjStockReceiptDetails in TempStockReceiptDetailsList)
            {
                var TempStockReceiptDetails = new StockReceiptDetails();

                TempStockReceiptDetails.Active = ObjStockReceiptDetails.Active;
                TempStockReceiptDetails.ApplicationDate = ObjStockReceiptDetails.ApplicationDate;
                TempStockReceiptDetails.BarCode = ObjStockReceiptDetails.BarCode;
                TempStockReceiptDetails.Brand = ObjStockReceiptDetails.Brand;
                TempStockReceiptDetails.Color = ObjStockReceiptDetails.Color;
                TempStockReceiptDetails.CreateBy = ObjStockReceiptDetails.CreateBy;
                TempStockReceiptDetails.DifferenceQuantity = ObjStockReceiptDetails.DifferenceQuantity;
                TempStockReceiptDetails.DocumentDate = ObjStockReceiptDetails.DocumentDate;
                TempStockReceiptDetails.DocumentNo = ObjStockReceiptDetails.DocumentNo;
                TempStockReceiptDetails.fromApplication = ObjStockReceiptDetails.fromApplication;
                TempStockReceiptDetails.FromStoreID = ObjStockReceiptDetails.FromStoreID;
                TempStockReceiptDetails.ID = ObjStockReceiptDetails.ID;
                TempStockReceiptDetails.Remarks = ObjStockReceiptDetails.Remarks;
                TempStockReceiptDetails.SerialNo = ObjStockReceiptDetails.SerialNo;
                TempStockReceiptDetails.Size = ObjStockReceiptDetails.Size;
                TempStockReceiptDetails.SKUCode = ObjStockReceiptDetails.SKUCode;
                TempStockReceiptDetails.SKUID = ObjStockReceiptDetails.SKUID;
                TempStockReceiptDetails.SKUName = ObjStockReceiptDetails.SKUName;
                TempStockReceiptDetails.StyleCode = ObjStockReceiptDetails.StyleCode;
                TempStockReceiptDetails.UpdateBy = ObjStockReceiptDetails.UpdateBy;
                TempStockReceiptDetails.RequestQuantity = ObjStockReceiptDetails.RequestQuantity;
                TempStockReceiptDetails.OldReceivedQuantity = ObjStockReceiptDetails.OldReceivedQuantity;
                TempStockReceiptDetails.Quantity = ObjStockReceiptDetails.Quantity;
                TempStockReceiptDetails.TransferQuantity = ObjStockReceiptDetails.TransferQuantity;
                TempStockReceiptDetails.ReceivedQuantity = ObjStockReceiptDetails.ReceivedQuantity;
                TempStockReceiptDetails.Tag_Id = ObjStockReceiptDetails.Tag_Id;

                StockReceiptDetailsList.Add(TempStockReceiptDetails);
            }
            return StockReceiptDetailsList;
        }

        public static List<InvoiceDetails> InvoiceDetailsListDeepCopy(List<InvoiceDetails> ObjInvoiceDetailsList)
        {
            var tempInvoiceDetailsList = new List<InvoiceDetails>();


            foreach (InvoiceDetails objInvoiceDetails in ObjInvoiceDetailsList)
            {
                var TempInvoiceDetailsList = new InvoiceDetails();

                TempInvoiceDetailsList.SKUID = objInvoiceDetails.SKUID;
                TempInvoiceDetailsList.SellingPrice = objInvoiceDetails.SellingPrice;
               
                TempInvoiceDetailsList.InvoiceHeaderID = objInvoiceDetails.InvoiceHeaderID;
                TempInvoiceDetailsList.InvoiceDetailID = objInvoiceDetails.InvoiceDetailID;
                TempInvoiceDetailsList.SKUCode = objInvoiceDetails.SKUCode;
                TempInvoiceDetailsList.BrandID = objInvoiceDetails.BrandID;
                TempInvoiceDetailsList.SubBrandID = objInvoiceDetails.SubBrandID;
                TempInvoiceDetailsList.Category = objInvoiceDetails.Category;
                TempInvoiceDetailsList.Qty = objInvoiceDetails.Qty;
                TempInvoiceDetailsList.ReturnQty = objInvoiceDetails.ReturnQty;
                TempInvoiceDetailsList.DummyQty = objInvoiceDetails.DummyQty;
                TempInvoiceDetailsList.Price = objInvoiceDetails.Price;
                TempInvoiceDetailsList.DummyPrice = objInvoiceDetails.DummyPrice;
                TempInvoiceDetailsList.DiscountType = objInvoiceDetails.DiscountType;
                TempInvoiceDetailsList.DiscountAmount = objInvoiceDetails.DiscountAmount;
                TempInvoiceDetailsList.AppliedPriceListID = objInvoiceDetails.AppliedPriceListID;
                TempInvoiceDetailsList.AppliedCustomerSpecialPricesID = objInvoiceDetails.AppliedCustomerSpecialPricesID;
                TempInvoiceDetailsList.AppliedPromotionID = objInvoiceDetails.AppliedPromotionID;
                TempInvoiceDetailsList.SalesStatus = objInvoiceDetails.SalesStatus;
                TempInvoiceDetailsList.ModifiedSalesEmployee = objInvoiceDetails.ModifiedSalesEmployee;
                TempInvoiceDetailsList.ModifiedSalesManager = objInvoiceDetails.ModifiedSalesManager;
                TempInvoiceDetailsList.IsDataSyncToCountryServer = objInvoiceDetails.IsDataSyncToCountryServer;
                TempInvoiceDetailsList.IsDataSyncToMainServer = objInvoiceDetails.IsDataSyncToMainServer;
                TempInvoiceDetailsList.CountryServerSyncTime = objInvoiceDetails.CountryServerSyncTime;
                TempInvoiceDetailsList.MainServerSyncTime = objInvoiceDetails.MainServerSyncTime;
                TempInvoiceDetailsList.SyncFailedReason = objInvoiceDetails.SyncFailedReason;
                TempInvoiceDetailsList.LineTotal = objInvoiceDetails.LineTotal;
                TempInvoiceDetailsList.PromotionAmount = objInvoiceDetails.PromotionAmount;
                TempInvoiceDetailsList.SerialNo = objInvoiceDetails.SerialNo;
                TempInvoiceDetailsList.PromotionName = objInvoiceDetails.PromotionName;
                TempInvoiceDetailsList.PromtionApplied = objInvoiceDetails.PromtionApplied;
                TempInvoiceDetailsList.InvoiceType = objInvoiceDetails.InvoiceType;
                TempInvoiceDetailsList.LinkedSrlNo = objInvoiceDetails.LinkedSrlNo;
                TempInvoiceDetailsList.IsRecordVisible = objInvoiceDetails.IsRecordVisible;
                TempInvoiceDetailsList.SingleDiscountAmount = objInvoiceDetails.SingleDiscountAmount;
                TempInvoiceDetailsList.CountryID = objInvoiceDetails.CountryID;
                TempInvoiceDetailsList.StoreID = objInvoiceDetails.StoreID;
                TempInvoiceDetailsList.TaxAmount = objInvoiceDetails.TaxAmount;
                TempInvoiceDetailsList.ReturnAmount = objInvoiceDetails.ReturnAmount;
                TempInvoiceDetailsList.TaxID = objInvoiceDetails.TaxID;
                TempInvoiceDetailsList.AppliedPromotionID = objInvoiceDetails.AppliedPromotionID;
                TempInvoiceDetailsList.SKUImage = objInvoiceDetails.SKUImage;
                TempInvoiceDetailsList.SubBrandName = objInvoiceDetails.SubBrandName;
                TempInvoiceDetailsList.CountryCode = objInvoiceDetails.CountryCode;
                TempInvoiceDetailsList.StoreCode = objInvoiceDetails.StoreCode;
                TempInvoiceDetailsList.PosCode = objInvoiceDetails.PosCode;
                TempInvoiceDetailsList.InvoiceNo = objInvoiceDetails.InvoiceNo;
                TempInvoiceDetailsList.SellingPrice = objInvoiceDetails.SellingPrice;
                TempInvoiceDetailsList.SellingLineTotal = objInvoiceDetails.SellingLineTotal;

                TempInvoiceDetailsList.IsReturned = objInvoiceDetails.IsReturned;
                TempInvoiceDetailsList.OldReturnQty = objInvoiceDetails.OldReturnQty;
                TempInvoiceDetailsList.IsExchanged = objInvoiceDetails.IsExchanged;
                TempInvoiceDetailsList.OldExchangeQty = objInvoiceDetails.OldExchangeQty;
                TempInvoiceDetailsList.ReturnRemarks = objInvoiceDetails.ReturnRemarks;
                TempInvoiceDetailsList.ExchangeRemarks = objInvoiceDetails.ExchangeRemarks;
                TempInvoiceDetailsList.SerialNo = objInvoiceDetails.SerialNo;
                TempInvoiceDetailsList.NetAmount = objInvoiceDetails.NetAmount;
                TempInvoiceDetailsList.DiscountRemarks = objInvoiceDetails.DiscountRemarks;
                TempInvoiceDetailsList.FamilyDiscountAmount = objInvoiceDetails.FamilyDiscountAmount;
                TempInvoiceDetailsList.EmployeeDiscountAmount = objInvoiceDetails.EmployeeDiscountAmount;
                TempInvoiceDetailsList.EmployeeDiscountID = objInvoiceDetails.EmployeeDiscountID;
                TempInvoiceDetailsList.SpecialDiscountType = objInvoiceDetails.SpecialDiscountType;
                TempInvoiceDetailsList.IsPromoExcludeItem = objInvoiceDetails.IsPromoExcludeItem;
                TempInvoiceDetailsList.SpecialPromoDiscountType = objInvoiceDetails.SpecialPromoDiscountType;
                TempInvoiceDetailsList.SpecialPromoDiscountPercentage = objInvoiceDetails.SpecialPromoDiscountPercentage;
                TempInvoiceDetailsList.SpecialPromoDiscount = objInvoiceDetails.SpecialPromoDiscount;
                TempInvoiceDetailsList.IsFreeItem = objInvoiceDetails.IsFreeItem;
                TempInvoiceDetailsList.Tag_Id = objInvoiceDetails.Tag_Id;
                TempInvoiceDetailsList.PromoGroupID = objInvoiceDetails.PromoGroupID;
                TempInvoiceDetailsList.CustomerCode = objInvoiceDetails.CustomerCode;
                TempInvoiceDetailsList.ExchangedSKU = objInvoiceDetails.ExchangedSKU;
                tempInvoiceDetailsList.Add(TempInvoiceDetailsList);

            }

            return tempInvoiceDetailsList;
        }

    }
  
}
