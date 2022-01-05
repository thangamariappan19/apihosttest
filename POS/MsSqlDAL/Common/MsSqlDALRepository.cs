using EasyBizAbsDAL.Common;
using EasyBizAbsDAL.DashBoard;
using EasyBizAbsDAL.FCPasses;
using EasyBizAbsDAL.Masters;
using EasyBizAbsDAL.PatchForm;
using EasyBizAbsDAL.Reports;
using EasyBizAbsDAL.Reports.DayWiseTransaction;
using EasyBizAbsDAL.SalesTarget;
using EasyBizAbsDAL.SyncSettings;
using EasyBizAbsDAL.Transactions;
using EasyBizAbsDAL.Transactions.Cardex.CardexLocation;
using EasyBizAbsDAL.Transactions.DiscountMaster;
using EasyBizAbsDAL.Transactions.NonTradingItemStock;
using EasyBizAbsDAL.Transactions.PaymentDetails;
using EasyBizAbsDAL.Transactions.POS;
using EasyBizAbsDAL.Transactions.Pricing;
using EasyBizAbsDAL.Transactions.Promotions;
using EasyBizAbsDAL.Transactions.StockReceipt;
using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizAbsDAL.Transactions.TransactionLogs;
using MsSqlDAL.DashBoard;
using MsSqlDAL.FCPasses;
using MsSqlDAL.Import;
using MsSqlDAL.Masters;
using MsSqlDAL.PatchForm;
using MsSqlDAL.Reports;
using MsSqlDAL.Reports.DayWiseTransaction;
using MsSqlDAL.SalesTarget;
using MsSqlDAL.SyncSettings;
using MsSqlDAL.Transactions.Cardex.CardexLocation;
using MsSqlDAL.Transactions.CouponReceipt;
using MsSqlDAL.Transactions.CouponTransfer;
using MsSqlDAL.Transactions.PaymentDetails;
using MsSqlDAL.Transactions.POS;
using MsSqlDAL.Transactions.PriceChange;
using MsSqlDAL.Transactions.Pricing;
using MsSqlDAL.Transactions.Promotions;
using MsSqlDAL.Transactions.Stocks;
//using MsSqlDAL.Transactions.Stocks;
using MsSqlDAL.Transactions.Tailoring;
using MsSqlDAL.Transactions.TransactionLogs;
using System;

namespace MsSqlDAL.Common
{
    public class MsSqlDALRepository : BaseDALRepository
    {
        public override BaseUsersDAL GetUsersDAL()
        {
            var retObj = new UsersDAL();
            return retObj;
        }
        public override BaseRoleDAL GetRoleDAL()
        {
            var retObj = new RoleDAL();
            return retObj;
        }
        public override BaseCurrencyDAL GetCurrencyDAL()
        {
            var retObj = new CurrencyDAL();
            return retObj;
        }
        public override BaseCountryDAL GetCountryDAL()
        {
            var retObj = new CountryDAL();
            return retObj;
        }
        public override BaseCustomerGroupDAL GetCustomerGroupMaster()
        {
            var retObj = new CustomerGroupDAL();
            return retObj;
        }
        public override BaseCompanySettingDAL GetCompanySetting()
        {
            var retObj = new CompanySettingDAL();
            return retObj;
        }
        public override BaseStateMasterDAL GetBaseStateMasterDAL()
        {
            var retObj = new StateMasterDAL();
            return retObj;
        }
        public override BaseStateMasterDAL GetStateDAL()
        {
            var retObj = new StateMasterDAL();
            return retObj;
        }
        public override BaseVendorGroupMasterDAL GetVendorGroupMasterDAL()
        {
            var retObj = new VendorGroupMasterDAL();
            return retObj;
        }
        public override BaseVendorMasterDAL GetVendorMasterDAL()
        {
            var retObj = new VendorMasterDAL();
            return retObj;
        }
        public override BaseProductLineMasterDAL GetProductLineMasterDAL()
        {
            var retObj = new ProductLineMasterDAL();
            return retObj;
        }
        public override BaseWarehouseMasterDAL GetWarehouseMasterDAL()
        {
            var retObj = new WarehouseMasterDAL();
            return retObj;
        }
        public override BaseWarehouseTypeMasterDAL GetWarehouseTypeMasterDAL()
        {
            var retObj = new WarehouseTypeMasterDAL();
            return retObj;
        }
        public override BasePosMasterDAL GetPosMasterDAL()
        {
            var retObj = new PosMasterDAL();
            return retObj;
        }
        public override BaseBrandMasterDAL GetBrandMasterDAL()
        {
            var retObj = new BrandMasterDAL();
            return retObj;
        }
        public override BaseProductGroupDAL GetProductGroupDAL()
        {
            var retObj = new ProductGroupDAL();
            return retObj;
        }
        public override BaseSeasonDAL GetSeasonDAL()
        {
            var retObj = new SeasonDAL();
            return retObj;
        }

        public override BaseCustomerMasterDAL GetCustomerMaster()
        {
            var retObj = new CustomerMasterDAL();
            return retObj;
        }
        public override BasePaymentTypeSettingDAL GetPaymentTypeSettingMaster()
        {
            var retObj = new PaymentTypeSettingDAL();
            return retObj;
        }
        public override BaseLanguageDAL GetBaseLanguageDAL()
        {
            var retObj = new LanguageDAL();
            return retObj;
        }
        public override BaseSubBrandMasterDAL GetSubBrandMasterDAL()
        {
            var retObj = new SubBrandMasterDAL();
            return retObj;
        }
        public override BaseDocumentTypeDAL GetDocumentTypeDAL()
        {
            var retObj = new DocumentTypeDAL();
            return retObj;
        }
        public override BaseRetailSettingsDAL GetRetailSettingDAL()
        {
            var retObj = new RetailSettingsDAL();
            return retObj;
        }

        public override BaseEmployeeMasterDAL GetEmployeeMasterDAL()
        {
            var retObj = new EmployeeMasterDAL();
            return retObj;
        }

        public override EasyBizBLL.Masters.BaseExpenseMasterDAL GetExpenseMasterDAL()
        {
            var retObj = new ExpenseMasterDAL();
            return retObj;
        }

        public override BaseStoreGroupMasterDAL GetStoreGroupMasterDAL()
        {
            var retObj = new StoreGroupMasterDAL();
            return retObj;
        }

        public override BaseSKUMasterDAL GetSKUMasterDAL()
        {
            var retObj = new SKUMasterDAL();
            return retObj;
        }

        public override BaseColorMasterDAL GetColorMasterDAL()
        {
            var retObj = new ColorMasterDAL();
            return retObj;
        }

        public override BaseItemTypeMasterDAL GetItemTypeMasterDAL()
        {
            var retObj = new ItemTypeMasterDAL();
            return retObj;
        }

        public override BaseItemTypeMasterDAL GetItemGroupMasterDAL()
        {
            var retObj = new ItemGroupMasterDAL();
            return retObj;
        }

        public override BaseDocumentNumberingMasterDAL GetDocumentNumberingMasterDAL()
        {
            var retObj = new DocumentNumberingMasterDAL();
            return retObj;
        }

        public override BaseSubSeasonMasterDAL GetSubSeasonMasterDAL()
        {
            var retObj = new SubSeasonMasterDAL();
            return retObj;
        }

        public override BaseStyleStatusMasterDAL GetStyleStatusMasterDAL()
        {
            var retObj = new StyleStatusMasterDAL();
            return retObj;
        }

        public override BaseDivisionMasterDAL GetDivisionMasterDAL()
        {
            var retObj = new DivisionMasterDAL();
            return retObj;
        }

        public override BaseScaleMasterDAL GetScaleMasterDAL()
        {
            var retObj = new ScaleMasterDAL();
            return retObj;
        }

        public override BasePriceListDAL GetPriceListDAL()
        {
            var retObj = new PriceListDAL();
            return retObj;
        }

        public override BaseAFSegamationMasterDAL GetAFSegamationMasterDAL()
        {
            var retObj = new AFSegamationMasterDAL();
            return retObj;
        }

        public override BaseDropMasterDAL GetDropMasterDAL()
        {

            var retObj = new DropMasterDAL();
            return retObj;

        }

        public override BaseCouponMasterDAL GetCouponMasterDAL()
        {
            var retObj = new CouponMasterDAL();
            return retObj;

        }

        public override BaseFreightMasterDAL GetFreightMasterDAL()
        {
            var retObj = new FreightMasterDAL();
            return retObj;

        }

        public override BaseOrderTypeMasterDAL GetOrderTypeMasterDAL()
        {
            var retObj = new OrderTypeMasterDAL();
            return retObj;

        }

        public override BaseRequestTypeMasterDAL GetRequestTypeMasterDAL()
        {
            var retObj = new RequestTypeMasterDAL();
            return retObj;

        }

        public override BaseYearMasterDAL GetYearMasterDAL()
        {
            var retObj = new YearMasterDAL();
            return retObj;
        }

        public override BasePriceTypeDAL GetPriceTypeDAL()
        {
            var retObj = new PriceTypeDAL();
            return retObj;
        }

        public override BaseAllocationTypeMasterDAL GetAllocationTypeMasterDAL()
        {
            var retObj = new AllocationTypeMasterDAL();
            return retObj;
        }

        public override BaseBarcodeSettingsDAL GetBarcodeSettingsDAL()
        {
            var retObj = new BarcodeSettingsDAL();
            return retObj;
        }



        public override BaseProductSubGroupMasterDAL GetProductSubGroupMasterDAL()
        {
            var retObj = new ProductSubGroupMasterDAL();
            return retObj;
        }

        public override BaseCollectionMasterDAL GetCollectionMasterDAL()
        {
            var retObj = new CollectionMasterDAL();
            return retObj;
        }

        public override BaseReasonMasterDAL GetReasonMasterDAL()
        {
            var retObj = new ReasonMasterDAL();
            return retObj;
        }

        public override BaseDesignationMasterDAL GetDesignationMasterDAL()
        {
            var retObj = new DesignationMasterDAL();
            return retObj;
        }

        public override BaseTaxMasterDAL GetTaxMasterDAL()
        {
            var retObj = new TaxMasterDAL();
            return retObj;
        }

        public override BaseSubCollectionDAL GetSubCollectionDAL()
        {
            var retObj = new SubCollectionDAL();
            return retObj;
        }

        public override BaseAgentMasterDAL GetAgentMasterDAL()
        {
            var retObj = new AgentMasterDAL();
            return retObj;
        }
        public override BaseFranchiseMasterDAL GetFranchiseMasterDAL()
        {
            var retObj = new FranchiseMasterDAL();
            return retObj;
        }

        public override BaseTillSettingsDAL GetTillSettingsDAL()
        {
            var retObj = new TillSettingsDAL();
            return retObj;
        }

        public override BaseStoreMasterDAL GetStoreMasterDAL()
        {
            var retObj = new StoreMasterDAL();
            return retObj;
        }

        public override BaseCountTypeMasterDAL GetCountTypeMasterDAL()
        {
            var retObj = new CountTypeMasterDAL();
            return retObj;
        }

        public override BaseStyleMasterDAL GetBaseStyleMasterDAL()
        {
            var retObj = new StyleMasterDAL();
            return retObj;
        }

        public override BaseDesignMasterDAL GetDesignMasterDAL()
        {
            var retObj = new DesignMasterDAL();
            return retObj;
        }

        public override BaseSegmentationMasterDAL GetBaseSegmentationMasterDAL()
        {
            var retObj = new SegmentationDAL();
            return retObj;
        }

        public override BaseArmadaCollectionMasterDAL GetBaseArmadaCollectionMasterDAL()
        {
            var retObj = new ArmadaCollectionsMasterDAL();
            return retObj;
        }

        public override BaseCustomerSpecialPriceMasterDAL GetCustomerSpecialPriceMasterrDAL()
        {
            var retObj = new CustomerSpecialPriceMasterDAL();
            return retObj;
        }

        public override BaseCouponMasterDAL GetBaseCouponMasterDAL()
        {
            var retObj = new CouponMasterDAL();
            return retObj;
        }

        public override BasePromotionsMasterDAL GetBasePromotionsMasterDAL()
        {
            var retObj = new PromotionsMasterDAL();
            return retObj;
        }

        public override BasePricePointDAL GetPricePointDAL()
        {
            var retObj = new PricePointDAL();
            return retObj;
        }

        public override BaseInvoiceDAL GetInvoiceDAL()
        {
            var retObj = new InvoiceDAL();
            return retObj;
        }

        public override BaseTransactionStatusDAL GetTransactionStatusDAL()
        {
            var retObj = new TransactionStatusDAL();
            return retObj;
        }

        public override BasePaymentTypeDAL GetPaymentTypeDAL()
        {
            var retObj = new PaymentTypeDAL();
            return retObj;
        }

        public override BaseDenominationDAL GetDenominationDAL()
        {
            var retObj = new DenominationDAL();
            return retObj;
        }

        public override BasePromotionPriorityDAL GetBasePromotionPriorityDAL()
        {
            var retObj = new PromotionPriorityDAL();
            return retObj;
        }

        public override BaseCardDetailsDAL GetCardDetailsDAL()
        {
            var retObj = new InvoiceCardDetailsDAL();
            return retObj;
        }
        public override BaseSyncSettingsDAL GetSyncSettingsDAL()
        {
            var retObj = new SyncSettingsDAL();
            return retObj;
        }

        public override BasePrevilegesDAL GetPrevilegesDAL()
        {
            var retObj = new PrevilegesDAL();
            return retObj;
        }
        public override BaseCashInCashOutDAL GetCashInCashOutDAL()
        {
            var retObj = new CashInCashOutDAL();
            return retObj;
        }
        public override BaseStockRequestDAL GetStockRequestDAL()
        {
            var retObj = new StockRequestDAL();
            return retObj;
        }

        public override BaseStockReceiptDAL GetStockReceiptDAL()
        {
            var retObj = new StockReceiptDAL();
            return retObj;
        }
        public override BaseStockReturnDAL GetStockReturnDAL()
        {
            var retObj = new StockReturnDAL();
            return retObj;
        }
        public override BaseOpeningStockDAL GetOpeningStockDAL()
        {
            var retObj = new OpeningStockDAL();
            return retObj;
        }
        public override BaseStockStagingDAL GetStockStagingDAL()
        {
            var retObj = new StockStagingDAL();
            return retObj;
        }
        public override BaseTransactionLogsDAL GetTransactionLogsDAL()
        {
            var retObj = new TransactionLogsDAL();
            return retObj;
        }
        public override BaseStockAdjustmentDAL GetStockAdjustmentDAL()
        {
            var retObj = new StockAdjustmentDAL();
            return retObj;
        }

        public override BaseInventoryCountingDAL GetInventoryCountingDAL()
        {
            var retObj = new InventoryCountingDAL();
            return retObj;
        }
        public override BaseCouponDetailDAL GetBaseCouponDetailDAL()
        {
            var retObj = new CouponDetailDAL();
            return retObj;
        }
        public override BaseGiftvoucherDetailsDAL GetBaseGiftvoucherDetailsDAL()
        {
            var retObj = new GiftvoucherDetailsDAL();
            return retObj;
        }
        public override BaseInvoiceCashDetailsDAL GetBaseInvoiceCashDetailsDAL()
        {
            var retObj = new InvoiceCashDetailsDAL();
            return retObj;
        }

        public override BaseDayClosingDAL GetBaseDayClosingDAL()
        {
            var retObj = new DayClosingDAL();
            return retObj;
        }
        public override BaseExchangeRatesDAL GetBaseExchangeRatesDAL()
        {
            var retObj = new ExchangeRatesDAL();
            return retObj;
        }
        public override BaseSalesExchangeDAL GetBaseSalesExchangeDAL()
        {
            var retObj = new SalesExchangeDAL();
            return retObj;
        }

        public override EasyBizAbsDAL.Reports.BaseDailySalesReportDAL GetBaseDailySalesReportDAL()
        {
            var retObj = new DailySalesReportDAL();
            return retObj;
        }

        public override BaseCurrentStockReportDAL GetBaseCurrentStockReportDAL()
        {
            var retObj = new CurrentStockReportDAL();
            return retObj;
        }

        public override BaseStockMovementReportDAL GetStockMovementReportDAL()
        {
            var retObj = new StockMovementReportDAL();
            return retObj;
        }

        public override BaseBrandDivisionMapDAL GetBrandDivisionMapDAL()
        {
            var retObj = new BrandDivisionMapDAL();
            return retObj;
        }

        public override BaseSalesReturnHeaderDAL GetSalesReturnDAL()
        {
            var retObj = new SalesReturnDAL();
            return retObj;
        }

        public override BaseShiftMasterDAL GetShiftMasterDAL()
        {
            var retObj = new ShiftDAL();
            return retObj;
        }

        public override BaseShiftLOGDAL GetBaseShiftLOGDAL()
        {
            var retObj = new ShiftLOGDAL();
            return retObj;
        }
        public override BaseWNPromotionDAL GetWNPromotionDAL()
        {
            var retObj = new WNPromotionDAL();
            return retObj;
        }

        public override EasyBizAbsDAL.Import.BaseImportStylePricingDAL GetBaseImportStylePricingDAL()
        {
            var retObj = new ImportStylePricingDAL();
            return retObj;
        }


        public override BaseManagerOverrideDAL GetBaseManagerOverrideDAL()
        {
            var retObj = new ManagerOverrideDAL();
            return retObj;
        }
        public override EasyBizAbsDAL.DashBoard.BaseRegisterDashBoardDAL GetRegisterDashBoardDAL()
        {
            var retObj = new RegisterDashboardDAL();
            return retObj;
        }
        public override BaseSchemaInfoDAL GetSchemaInfoDAL()
        {
            var retObj = new SchemaInfoDAL();
            return retObj;
        }
        public override BaseCommonReportDAL GetCommonReportDAL()
        {
            var retObj = new CommonReportDAL();
            return retObj;
        }
        public override BasePromotionMappingDAL GetPromotionMappingDAL()
        {
            var retObj = new PromotionMappingDAL();
            return retObj;
        }

        public override BaseTailoringMasterDAL GetTailoringMasterDAL()
        {
            var retObj = new TailoringMasterDAL();
            return retObj;
        }

        public override BaseDiscountMasterDAL GetDiscountMasterDAL()
        {
            var retObj = new DiscountMasterDAL();
            return retObj;
        }

        public override BaseLabelPrintingDAL GetLabelPrintingReportDAL()
        {
            var retObj = new LabelPrintingDAL();
            return retObj;
        }

        public override BaseOnAccountPaymentDAL GetOnAccountPaymentDAL()
        {
            var retObj = new OnAccountPaymentDAL();
            return retObj;
        }
        public override BaseCardexLocationDAL GetCardexLocation()
        {
            var retObj = new CardexLocationDAL();
            return retObj;
        }

        public override EasyBizAbsDAL.Transactions.Tailoring.BaseTailoringOrderDAL GetTailoringOrderDAL()
        {
            var retObj = new TailoringOrderDAL();
            return retObj;
        }

        public override EasyBizAbsDAL.Transactions.PriceChange.BasePriceChangeDAL GetPriceChangeDAL()
        {
            var retObj = new PriceChangeDAL();
            return retObj;
        }
        public override BaseSalesReturnTransactionDAL GetSalesReturnTransactionDAL()
        {
            var retObj = new SalesReturnTransactionDAL();
            return retObj;

        }
        public override BaseSalesExchangeTransactionDAL GetSalesExchangeTransactionDAL()
        {
            var retObj = new SalesExchangeTransactionDAL();
            return retObj;

        }
        public override BaseStockReceiptTransactionDAL GetStockReceiptTransactionDAL()
        {
            var retObj = new StockReceiptTransactionDAL();
            return retObj;
        }
        public override BaseStockReturnTransactionDAL GetStockReturnTransactionDAL()
        {
            var retObj = new StockReturnTransactionDAL();
            return retObj;
        }
        public override BaseStockAdjustmentTransactionDAL GetStockAdjustmentTransactionDAL()
        {
            var retObj = new StockAdjustmentTransactionDAL();
            return retObj;
        }
        public override BaseInvoiceTransactionDAL GetInvoiceTransactionDAL()
        {
            var retObj = new InvoiceHeaderTransactionDAL();
            return retObj;
        }
        public override BaseUserReportDAL GetUserReportDAL()
        {
            var retObj = new UserReportDAL();
            return retObj;
        }

        public override BaseSalesTargetDAL GetBaseSalesTargetDAL()
        {
            var retObj = new SalesTargetDAL();
            return retObj;
        }
        public override BasePatchFormDAL GetBasePatchFormDAL()
        {
            var retObj = new PatchFormDAL();
            return retObj;
        }

        public override BaseSalesOrderDAL GetSalesOrderDAL()
        {
            var retObj = new SalesOrderDAL();
            return retObj;
        }

        public override EasyBizAbsDAL.Transactions.CouponTransfer.BaseCouponTransferDAL GetCouponTransferDAL()
        {
            var retObj = new CouponTransferDAL();
            return retObj;
        }

        public override EasyBizAbsDAL.Transactions.CouponReceipt.BaseCouponReceiptDAL GetCouponReceiptDAL()
        {
            var retObj = new CouponReceiptDAL();
            return retObj;
        }

        public override MasterDataSyncAbsDAL GetSyncStorePrice()
        {
            var retObj = new MasterDataSyncDAL();
            return retObj;
        }

        public override ManualMasterSyncAbsDAL GetSyncAbsDAL()
        {
            var retObj = new ManualMasterSyncDAL();
            return retObj;
        }

        public override BaseEmployeeFingerPrintMasterDAL GetEmployeeFingerPrintMasterDAL()
        {
            var retObj = new EmployeeFingerPrintDAL();
            return retObj;
        }

        public override BaseNonTradingItemStockDAL GetNonTradingItemStockDAL()
        {
            var retObj = new NonTradingItemStockDAL();
            return retObj;
        }

        public override BaseCityMasterDAL GetCityDAL()
        {
            var retObj = new CityMasterDAL();
            return retObj;
        }

        //public override BaseWebOrderDAL GetWebOrderDAL()
        //{
        //    var retObj = new WebSalesOrderDAL();
        //    return retObj;
        //}

        public override BaseWebSalesOrderDAL GetWebSalesOrderDAL()
        {
            var retObj = new WebSalesOrderNew();
            return retObj;
        }

        public override BaseAPI_SalesOrderDAL GetAPISalesOrderDAL()
        {
            var retObj = new API_SalesOrderDAL();
            return retObj;
        }

        public override BaseBinLevelMasterDAL GetBinLevelMasterDAL()
        {
            var retObj = new BinLevelMasterDAL();
            return retObj;
        }

        public override BaseBinLevelDetailsDAL GetBinLevelDetailsDAL()
        {
            var retObj = new BinLevelDetailsDAL();
            return retObj;
        }

        public override BaseBinTransferMasterDAL GetBinTransferMasterDAL()
        {
            var retObj = new BinTransferMasterDAL();
            return retObj;
        }
        /*public override BaseCardDetailsDAL GetPaymetProcessor()
{
throw new NotImplementedException();
}*/

        public override BassPassMasterDAL GetPassDAL()
        {
            var retObj = new PassMasterDAL();
            return retObj;
        }

        public override BasePassesTransactionDAL GetPassesTransactionDAL()
        {
            var retObj = new PassesTransactionDAL();
            return retObj;
        }

        public override BaseRegisterDashBoardDAL GetDashBoardDAL()
        {
            var retObj = new DashboardDAL();
            return retObj;
        }

        public override BaseComboOfferMasterDAL GetComboOfferMasterDAL()
        {
            var retObj = new ComboOfferMasterDAL();
            return retObj;
        }
        public override BaseSearchEngineDAL GetCustomerSkuMasterDAL()
        {
            var retObj = new SearchEngineDAL();
            return retObj;
        }

        public override BasePaymentModeMasterDAL GetPaymentModeMaster()
        {
            var retObj = new PaymentModeMasterDAL();
            return retObj;
        }
    }
}
