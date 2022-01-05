using EasyBizAbsDAL.DashBoard;
using EasyBizAbsDAL.Import;
using EasyBizAbsDAL.Masters;
using EasyBizAbsDAL.Reports;
using EasyBizAbsDAL.SyncSettings;
using EasyBizAbsDAL.Transactions;
using EasyBizAbsDAL.Transactions.Cardex.CardexLocation;
using EasyBizAbsDAL.Transactions.DiscountMaster;
using EasyBizAbsDAL.Transactions.PaymentDetails;
using EasyBizAbsDAL.Transactions.POS;
using EasyBizAbsDAL.Transactions.Pricing;
using EasyBizAbsDAL.Transactions.Promotions;
using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizAbsDAL.Transactions.TransactionLogs;
using EasyBizAbsDAL.Transactions.Tailoring;
using EasyBizBLL.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizAbsDAL.Transactions.PriceChange;
using EasyBizAbsDAL.Reports.DayWiseTransaction;
using EasyBizAbsDAL.SalesTarget;
using EasyBizAbsDAL.Transactions.CouponTransfer;
using EasyBizAbsDAL.Transactions.CouponReceipt;
using EasyBizAbsDAL.Transactions.StockReceipt;
using EasyBizAbsDAL.PatchForm;
using EasyBizAbsDAL.Transactions.NonTradingItemStock;
using EasyBizAbsDAL.FCPasses;

namespace EasyBizAbsDAL.Common
{
    public abstract class BaseDALRepository
    {
        public abstract BaseUsersDAL GetUsersDAL();
        public abstract BaseProductLineMasterDAL  GetProductLineMasterDAL();
        public abstract BaseWarehouseMasterDAL GetWarehouseMasterDAL();
        public abstract BaseWarehouseTypeMasterDAL GetWarehouseTypeMasterDAL(); 
        public abstract BaseRoleDAL GetRoleDAL();
        public abstract BaseCurrencyDAL GetCurrencyDAL();
        public abstract BaseCountryDAL GetCountryDAL();        
        public abstract BaseCustomerGroupDAL GetCustomerGroupMaster();
        public abstract BaseCompanySettingDAL GetCompanySetting();
        public abstract BaseStateMasterDAL GetBaseStateMasterDAL();
        public abstract BaseStateMasterDAL GetStateDAL();
        public abstract BaseVendorGroupMasterDAL GetVendorGroupMasterDAL();
        public abstract BaseVendorMasterDAL GetVendorMasterDAL();
        public abstract BasePosMasterDAL GetPosMasterDAL();   
        public abstract BaseProductGroupDAL GetProductGroupDAL();
        public abstract BaseSeasonDAL GetSeasonDAL();   
        public abstract BaseBrandMasterDAL GetBrandMasterDAL();
        public abstract BaseCustomerMasterDAL GetCustomerMaster();
        public abstract BasePaymentModeMasterDAL GetPaymentModeMaster();
        public abstract BasePaymentTypeSettingDAL GetPaymentTypeSettingMaster();
        public abstract BaseLanguageDAL GetBaseLanguageDAL();
        public abstract BaseSubBrandMasterDAL GetSubBrandMasterDAL();
        public abstract BaseDocumentTypeDAL GetDocumentTypeDAL();
        public abstract BaseRetailSettingsDAL GetRetailSettingDAL();
        public abstract BaseEmployeeMasterDAL GetEmployeeMasterDAL();
        public abstract BaseEmployeeFingerPrintMasterDAL GetEmployeeFingerPrintMasterDAL();

        public abstract BaseExpenseMasterDAL GetExpenseMasterDAL();

        public abstract BaseWebSalesOrderDAL GetWebSalesOrderDAL();

        public abstract BaseStoreGroupMasterDAL GetStoreGroupMasterDAL();
        public abstract BaseSKUMasterDAL GetSKUMasterDAL();
        public abstract BaseColorMasterDAL GetColorMasterDAL();

        public abstract BaseItemTypeMasterDAL GetItemTypeMasterDAL();
        
        public abstract BaseItemTypeMasterDAL GetItemGroupMasterDAL();

        public abstract BaseDocumentNumberingMasterDAL GetDocumentNumberingMasterDAL();
        public abstract BaseSubSeasonMasterDAL GetSubSeasonMasterDAL();
        
        public abstract BaseStyleStatusMasterDAL GetStyleStatusMasterDAL();
        public abstract BaseDivisionMasterDAL GetDivisionMasterDAL();
        public abstract BaseScaleMasterDAL GetScaleMasterDAL();

        public abstract BasePriceListDAL GetPriceListDAL();

        public abstract BaseAFSegamationMasterDAL GetAFSegamationMasterDAL();
        public abstract BaseDropMasterDAL GetDropMasterDAL();

        public abstract BaseCouponMasterDAL GetCouponMasterDAL();
        public abstract BaseCouponTransferDAL GetCouponTransferDAL();



        public abstract BaseCouponReceiptDAL GetCouponReceiptDAL();





        public abstract BaseFreightMasterDAL GetFreightMasterDAL();

        public abstract BaseOrderTypeMasterDAL GetOrderTypeMasterDAL();

        public abstract BaseRequestTypeMasterDAL GetRequestTypeMasterDAL();

        public abstract BaseYearMasterDAL GetYearMasterDAL();
        public abstract BasePriceTypeDAL GetPriceTypeDAL();

        public abstract BaseAllocationTypeMasterDAL GetAllocationTypeMasterDAL();

        public abstract BaseBarcodeSettingsDAL GetBarcodeSettingsDAL();
        public abstract BaseProductSubGroupMasterDAL GetProductSubGroupMasterDAL();

        public abstract BaseCollectionMasterDAL GetCollectionMasterDAL();
        public abstract BaseReasonMasterDAL GetReasonMasterDAL();

        public abstract BaseDesignationMasterDAL GetDesignationMasterDAL();

        public abstract BaseTaxMasterDAL GetTaxMasterDAL();

        public abstract BaseSubCollectionDAL GetSubCollectionDAL();
        public abstract BaseAgentMasterDAL GetAgentMasterDAL();
        //Bin
        public abstract BaseBinLevelMasterDAL GetBinLevelMasterDAL();
        public abstract BaseBinLevelDetailsDAL GetBinLevelDetailsDAL();

        public abstract BaseBinTransferMasterDAL GetBinTransferMasterDAL();
        public abstract BaseFranchiseMasterDAL GetFranchiseMasterDAL();
        public abstract BaseTailoringMasterDAL GetTailoringMasterDAL();   
		public abstract BaseTailoringOrderDAL GetTailoringOrderDAL();
		public abstract BasePriceChangeDAL GetPriceChangeDAL();   

        public abstract BaseShiftMasterDAL GetShiftMasterDAL();

        public abstract BaseTillSettingsDAL GetTillSettingsDAL();

        public abstract BaseStoreMasterDAL GetStoreMasterDAL();
        public abstract BaseCountTypeMasterDAL GetCountTypeMasterDAL();
        public abstract BaseStyleMasterDAL GetBaseStyleMasterDAL();

        public abstract BaseDesignMasterDAL GetDesignMasterDAL();

        public abstract BaseSegmentationMasterDAL GetBaseSegmentationMasterDAL();
        public abstract BaseArmadaCollectionMasterDAL GetBaseArmadaCollectionMasterDAL();

        public abstract BaseCustomerSpecialPriceMasterDAL GetCustomerSpecialPriceMasterrDAL();
        public abstract BasePromotionsMasterDAL GetBasePromotionsMasterDAL();

        public abstract BasePricePointDAL GetPricePointDAL();

        public abstract BaseInvoiceDAL GetInvoiceDAL();    
        public abstract BaseSalesReturnHeaderDAL GetSalesReturnDAL();

        public abstract BaseTransactionStatusDAL GetTransactionStatusDAL();
        public abstract BasePaymentTypeDAL GetPaymentTypeDAL();
        public abstract BaseDenominationDAL GetDenominationDAL();
        public abstract BasePromotionPriorityDAL GetBasePromotionPriorityDAL();
        public abstract BaseCardDetailsDAL GetCardDetailsDAL();
        //public abstract BaseCardDetailsDAL GetPaymetProcessor();
        public abstract BaseSyncSettingsDAL GetSyncSettingsDAL();
        public abstract MasterDataSyncAbsDAL GetSyncStorePrice();
        public abstract BasePrevilegesDAL GetPrevilegesDAL();
        public abstract BaseCashInCashOutDAL GetCashInCashOutDAL();
        public abstract BaseStockRequestDAL GetStockRequestDAL();
        public abstract BaseStockReceiptDAL GetStockReceiptDAL();
        public abstract BaseOpeningStockDAL GetOpeningStockDAL();
        public abstract BaseStockReturnDAL GetStockReturnDAL();
        public abstract BaseStockStagingDAL GetStockStagingDAL();
        public abstract BaseTransactionLogsDAL GetTransactionLogsDAL();
        public abstract BaseStockAdjustmentDAL GetStockAdjustmentDAL();
        public abstract BaseInventoryCountingDAL GetInventoryCountingDAL();
        public abstract BaseCouponDetailDAL GetBaseCouponDetailDAL();
        public abstract BaseCouponMasterDAL GetBaseCouponMasterDAL();
        public abstract BaseGiftvoucherDetailsDAL GetBaseGiftvoucherDetailsDAL();
        public abstract BaseInvoiceCashDetailsDAL GetBaseInvoiceCashDetailsDAL();
        public abstract BaseDayClosingDAL GetBaseDayClosingDAL();
        public abstract BaseExchangeRatesDAL GetBaseExchangeRatesDAL();
        public abstract BaseSalesExchangeDAL GetBaseSalesExchangeDAL();
        public abstract BaseDailySalesReportDAL GetBaseDailySalesReportDAL();
        public abstract BaseSalesTargetDAL GetBaseSalesTargetDAL();
        public abstract BasePatchFormDAL GetBasePatchFormDAL();
        public abstract BaseCurrentStockReportDAL GetBaseCurrentStockReportDAL();
        public abstract BaseStockMovementReportDAL GetStockMovementReportDAL();
        public abstract BaseBrandDivisionMapDAL GetBrandDivisionMapDAL();
        public abstract BaseShiftLOGDAL GetBaseShiftLOGDAL();
        public abstract BaseWNPromotionDAL GetWNPromotionDAL();
        public abstract BaseImportStylePricingDAL GetBaseImportStylePricingDAL();

        public abstract BaseManagerOverrideDAL GetBaseManagerOverrideDAL();
        public abstract BaseRegisterDashBoardDAL GetRegisterDashBoardDAL();
        public abstract BaseSchemaInfoDAL GetSchemaInfoDAL();
        public abstract BaseCommonReportDAL GetCommonReportDAL();
        public abstract BasePromotionMappingDAL GetPromotionMappingDAL();
        public abstract BaseDiscountMasterDAL GetDiscountMasterDAL();
        public abstract BaseLabelPrintingDAL GetLabelPrintingReportDAL();
        public abstract BaseOnAccountPaymentDAL GetOnAccountPaymentDAL();
        public abstract BaseCardexLocationDAL GetCardexLocation();
        public abstract BaseInvoiceTransactionDAL GetInvoiceTransactionDAL();
        public abstract BaseSalesReturnTransactionDAL GetSalesReturnTransactionDAL();
        public abstract BaseSalesExchangeTransactionDAL GetSalesExchangeTransactionDAL();
        public abstract BaseStockReceiptTransactionDAL GetStockReceiptTransactionDAL();
        public abstract BaseStockReturnTransactionDAL GetStockReturnTransactionDAL();
        public abstract BaseStockAdjustmentTransactionDAL GetStockAdjustmentTransactionDAL();
        public abstract BaseUserReportDAL GetUserReportDAL();
        public abstract BaseSalesOrderDAL GetSalesOrderDAL();
        public abstract BaseAPI_SalesOrderDAL GetAPISalesOrderDAL();
        public abstract ManualMasterSyncAbsDAL GetSyncAbsDAL();
        public abstract BaseNonTradingItemStockDAL GetNonTradingItemStockDAL();
        public abstract BaseCityMasterDAL GetCityDAL();
        public abstract BassPassMasterDAL GetPassDAL();
        public abstract BasePassesTransactionDAL GetPassesTransactionDAL();
        public abstract BaseRegisterDashBoardDAL GetDashBoardDAL();
        public abstract BaseComboOfferMasterDAL GetComboOfferMasterDAL();
        public abstract BaseSearchEngineDAL GetCustomerSkuMasterDAL();


    }
}
