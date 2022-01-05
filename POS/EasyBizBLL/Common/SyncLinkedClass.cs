using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.PaymentDetails;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POS.SalesExchange;
using EasyBizDBTypes.Transactions.POS.SalesReturn;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizDBTypes.Transactions.StockReceipt;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using EasyBizRequest.Masters.AgentMasterRequest;
using EasyBizRequest.Masters.AllocationTypeMasterRequest;
using EasyBizRequest.Masters.ArmadaCollectionsMasterRequest;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CountTypeMasterRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest;
using EasyBizRequest.Masters.DesignationMasterRequest;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizRequest.Masters.DropMasterRequest;
using EasyBizRequest.Masters.DropMasterResponse;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.ExchangeRatesRequest;
using EasyBizRequest.Masters.ExpenseMasterRequest;
using EasyBizRequest.Masters.FreightMasterRequest;
using EasyBizRequest.Masters.ItemGroupMasterRequest;
using EasyBizRequest.Masters.ItemTypeMasterRequest;
using EasyBizRequest.Masters.LanguageRequest;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizRequest.Masters.OrderTypeMasterRequest;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.PrevilegesRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.PriceTypeMasterResponse;
using EasyBizRequest.Masters.PriceTypeRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizRequest.Masters.RequestTypeMasterRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizRequest.Masters.RoleRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterResponse;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using EasyBizRequest.Masters.SubSeasonMasterRequest;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizRequest.Masters.TillSettingRequest;
using EasyBizRequest.Masters.UsersRequest;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizRequest.Masters.VendorMasterRequest;
using EasyBizRequest.Masters.WarehouseMasterRequest;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizRequest.Transactions.PaymentDetails;
using EasyBizRequest.Transactions.POS.CardDetails;
using EasyBizRequest.Transactions.POS.CouponDetailRequest;
using EasyBizRequest.Transactions.POS.DenominationRequest;
using EasyBizRequest.Transactions.POS.GiftvoucherDetailsRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizRequest.Transactions.POS.InVoiceCashDetailsRequest;
using EasyBizRequest.Transactions.POS.OnAccountPaymentRequest;
using EasyBizRequest.Transactions.POS.Sales_Return;
using EasyBizRequest.Transactions.POS.SalesExchangeRequest;
using EasyBizRequest.Transactions.POS.TransactionStatusRequest;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizRequest.Transactions.Promotions.PromotionMappingRequest;
using EasyBizRequest.Transactions.Promotions.PromotionPriority;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse;
using EasyBizResponse.Common;
using EasyBizResponse.Masters.AgentMasterResponse;
using EasyBizResponse.Masters.AllocationTypeResponse;
using EasyBizResponse.Masters.ArmadaCollectionsMasterResponse;
using EasyBizResponse.Masters.BarcodeSettingsResponse;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
using EasyBizResponse.Masters.ColorMasterResponse;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.CountTypeMasterResponse;
using EasyBizResponse.Masters.CouponMasterResponse;
using EasyBizResponse.Masters.CurrencyResponse;
using EasyBizResponse.Masters.CustomerGroupMasterResponse;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse;
using EasyBizResponse.Masters.DesignationMasterResponse;
using EasyBizResponse.Masters.DivisionMasterResponse;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.DocumentTypeResponse;
using EasyBizResponse.Masters.EmployeeMasterResponse;
using EasyBizResponse.Masters.ExchangeRatesResponse;
using EasyBizResponse.Masters.ExpenseMasterResponse;
using EasyBizResponse.Masters.FreightMasterResponse;
using EasyBizResponse.Masters.ItemGroupMasterResponse;
using EasyBizResponse.Masters.ItemTypeMasterResponse;
using EasyBizResponse.Masters.LanguageResponse;
using EasyBizResponse.Masters.ManagerOverrideResponse;
using EasyBizResponse.Masters.OrderTypeMasterResponse;
using EasyBizResponse.Masters.PaymentTypeSettingResponse;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.PrevilegesResponse;
using EasyBizResponse.Masters.PriceListResponse;
using EasyBizResponse.Masters.ProductGroupResponse;
using EasyBizResponse.Masters.ProductLineMasterResponse;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Masters.ReasonMasterResponse;
using EasyBizResponse.Masters.RequestTypeMasterResponse;
using EasyBizResponse.Masters.RetailSettingsResponse;
using EasyBizResponse.Masters.RoleResponse;
using EasyBizResponse.Masters.ScaleMasterResponse;
using EasyBizResponse.Masters.SeasonResponse;
using EasyBizResponse.Masters.SegmentationMasterResponse;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using EasyBizResponse.Masters.SubSeasonMasterResponse;
using EasyBizResponse.Masters.TailoringMasterResponse;
using EasyBizResponse.Masters.TaxMasterResponse;
using EasyBizResponse.Masters.TillSettingsResponse;
using EasyBizResponse.Masters.UsersResponse;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using EasyBizResponse.Masters.VendorMasterResponse;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using EasyBizResponse.Masters.YearMasterResponse;
using EasyBizResponse.Transactions.PaymentDetails.CashInCashOut;
using EasyBizResponse.Transactions.POS.CardDetails;
using EasyBizResponse.Transactions.POS.CouponDetailResponse;
using EasyBizResponse.Transactions.POS.DenominationResponse;
using EasyBizResponse.Transactions.POS.GiftvoucherDetailsResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using EasyBizResponse.Transactions.POS.InvoiceCashDetailsResponse;
using EasyBizResponse.Transactions.POS.OnAccountPaymentResponse;
using EasyBizResponse.Transactions.POS.SalesExchangeResponse;
using EasyBizResponse.Transactions.POS.SalesReturnResponse;
using EasyBizResponse.Transactions.POS.TransactionStatusResponse;
using EasyBizResponse.Transactions.PriceChange;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using EasyBizResponse.Transactions.Promotions.PromotionMappingResponse;
using EasyBizResponse.Transactions.Promotions.PromotionPriority;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Transactions.Stocks.StockReceipt;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using EasyBizResponse.Transactions.TransactionLogs;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Common
{
    public class BllAndFunction
    {
        public string ClassName { get; set; }

        public string SelectFunctionName { get; set; }

        public string InsertFunctionName { get; set; }

        public string UpdateFunctionName { get; set; }

        public string DeleteFunctionName { get; set; }

        public BaseRequestType SelectRequestType { get; set; }

        public BaseResponseType SelectBaseResponse { get; set; }

        public BaseRequestType InsertRequestData { get; set; }

        public BaseResponseType InsertResponseData { get; set; }

        public BaseRequestType UpdateRequestData { get; set; }

        public BaseResponseType UpdateResponseData { get; set; }

        public dynamic DynamicData { get; set; }

        public Enums.SyncMode SyncMode { get; set; }
    }
    public static class SyncLinkedClass
    {
        public static BllAndFunction ClassAndFunction(string ClassName, string FunctionName)
        {
            var objBllAndFunction = new BllAndFunction();
            if ((ClassName == "EasyBizBLL.Masters.AFSegamationMasterBLL") && (FunctionName == "SaveAFSegamationMaster" || FunctionName == "UpdateAFSegamationMaster" || FunctionName == "DeleteAFSegamationMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.AFSegamationMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDAFSegamationMaster";
                objBllAndFunction.InsertFunctionName = "SaveAFSegamationMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateAFSegamationMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteAFSegamationMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDAFSegamationMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDAFSegamationMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveAFSegamationMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveAFSegamationMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateAFSegamationMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateAFSegamationMasterResponse();

                objBllAndFunction.DynamicData = new AFSegamationMasterTypes();

                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.AgentBLL") && (FunctionName == "SaveAgent" || FunctionName == "UpdateAgent" || FunctionName == "DeleteAgent"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.AgentBLL";
                objBllAndFunction.SelectFunctionName = "SelectAgentRecord";
                objBllAndFunction.InsertFunctionName = "SaveAgent";
                objBllAndFunction.UpdateFunctionName = "UpdateAgent";
                objBllAndFunction.DeleteFunctionName = "DeleteAgent";
                objBllAndFunction.SelectRequestType = new SelectByAgentIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByAgentIDResponse();                
                objBllAndFunction.InsertRequestData = new SaveAgentRequest();
                objBllAndFunction.InsertResponseData = new SaveAgentResponse();
                objBllAndFunction.UpdateRequestData = new UpdateAgentRequest();
                objBllAndFunction.UpdateResponseData = new UpdateAgentResponse();

                objBllAndFunction.DynamicData = new AgentMaster();

                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.AllocationTypeMasterBLL") && (FunctionName == "SaveAllocationTypeMaster" || FunctionName == "UpdateAllocationTypeMaster" || FunctionName == "DeleteAllocationTypeMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.AllocationTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectAllocationTypeMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveAllocationTypeMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateAllocationTypeMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteAllocationTypeMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDAllocationTypeMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveAllocationTypeMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateAllocationTypeMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDAllocationTypeMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveAllocationTypeMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateAllocationTypeMasterResponse();

                objBllAndFunction.DynamicData = new AllocationTypeMaster();

                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ArmadaCollectionBLL") && (FunctionName == "ArmadaCollectionLookUp" ))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ArmadaCollectionBLL";
                objBllAndFunction.SelectFunctionName = "ArmadaCollectionLookUp";
                objBllAndFunction.SelectRequestType = new SelectArmadaCollectionLookUpRequest();               
                objBllAndFunction.SelectBaseResponse = new SelectArmadaCollectionLookUpResponse();
                objBllAndFunction.DynamicData = new ArmadaCollectionsMaster();
            }
            else if ((ClassName == "EasyBizBLL.Masters.BarcodeSettingsBLL") && (FunctionName == "SaveBarcodeSettings" || FunctionName == "UpdateBarcodeSettings" || FunctionName == "DeleteBarcodeSettings"))
            {
                // Changed by Senthamil @ 06.09.2018
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.BarcodeSettingsBLL";
                //objBllAndFunction.SelectFunctionName = "SelectBarcodeSettingsRecord";
                objBllAndFunction.SelectFunctionName = "SelectAllBarcodeSettings";                
                objBllAndFunction.InsertFunctionName = "SaveBarcodeSettings";
                objBllAndFunction.UpdateFunctionName = "UpdateBarcodeSettings";
                objBllAndFunction.DeleteFunctionName = "DeleteBarcodeSettings";
                //objBllAndFunction.SelectRequestType = new SelectByIDBarcodeSettingsRequest();
                objBllAndFunction.SelectRequestType = new SelectAllBarcodeSettingsRequest();
                objBllAndFunction.InsertRequestData = new SaveBarcodeSettingsRequest();
                objBllAndFunction.UpdateRequestData = new UpdateBarcodeSettingsRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectByIDBarcodeSettingsResponse();
                objBllAndFunction.SelectBaseResponse = new SelectAllBarcodeSettingsResponse();
                objBllAndFunction.InsertResponseData = new SaveBarcodeSettingsResponse();
                objBllAndFunction.UpdateResponseData = new UpdateBarcodeSettingsResponse();

                objBllAndFunction.DynamicData = new BarcodeSettings();

                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.BrandBLL") && (FunctionName == "SaveBrand" || FunctionName == "UpdateBrand" || FunctionName == "DeleteBrand"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.BrandBLL";
                objBllAndFunction.SelectFunctionName = "SelectBrandRecord";
                objBllAndFunction.InsertFunctionName = "SaveBrand";
                objBllAndFunction.UpdateFunctionName = "UpdateBrand";
                objBllAndFunction.DeleteFunctionName = "DeleteBrand";
                objBllAndFunction.SelectRequestType = new SelectByBrandIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByBrandIDResponse();
                objBllAndFunction.InsertRequestData = new SaveBrandRequest();
                objBllAndFunction.InsertResponseData = new SaveBrandResponse();
                objBllAndFunction.UpdateRequestData = new UpdateBrandRequest();
                objBllAndFunction.UpdateResponseData = new UpdateBrandResponse();

                objBllAndFunction.DynamicData = new BrandMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.BrandDivisionMapBLL") && (FunctionName == "SaveBrandDivision" || FunctionName == "DeleteBrandDivision" || FunctionName == "UpdateBrandDivision" || FunctionName == "SelectBrandDivisionRecord"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.BrandDivisionMapBLL";
                //objBllAndFunction.SelectFunctionName = "SelectBrandDivisionRecord";
                objBllAndFunction.SelectFunctionName = "SelectAllBrandDivisionRecords";
                objBllAndFunction.InsertFunctionName = "SaveBrandDivision";
                objBllAndFunction.UpdateFunctionName = "UpdateBrandDivision";
                objBllAndFunction.DeleteFunctionName = "DeleteBrandDivision";
                //objBllAndFunction.SelectRequestType = new SelectByBrandDivisionIDRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectByBrandDivisionIDResponse();
                objBllAndFunction.SelectRequestType = new SelectAllBrandDivisionRequest();
                objBllAndFunction.SelectBaseResponse = new SelectAllBrandDivisionMapResponse();
                objBllAndFunction.InsertRequestData = new SaveBrandDivisionMapRequest();
                objBllAndFunction.InsertResponseData = new SaveBrandDivisionMapResponse();
                objBllAndFunction.UpdateRequestData = new UpdateBrandDivisionMapRequest();
                objBllAndFunction.UpdateResponseData = new UpdateBrandDivisionMapResponse();

                objBllAndFunction.DynamicData = new BrandDivisionTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.CollectionMasterBLL") && (FunctionName == "SaveCollectionMaster" || FunctionName == "UpdateCollectionMaster" || FunctionName == "DeleteCollectionMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CollectionMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDCollectionMaster";
                objBllAndFunction.InsertFunctionName = "SaveCollectionMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateCollectionMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteCollectionMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDCollectionMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCollectionMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveCollectionMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveCollectionMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCollectionMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCollectionMasterResponse();

                objBllAndFunction.DynamicData = new CollectionMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ColorBLL") && (FunctionName == "SaveColor" || FunctionName == "UpdateColor" || FunctionName == "DeleteColor"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ColorBLL";
                objBllAndFunction.SelectFunctionName = "SelectColorRecord";
                objBllAndFunction.InsertFunctionName = "SaveColor";
                objBllAndFunction.UpdateFunctionName = "UpdateColor";
                objBllAndFunction.DeleteFunctionName = "DeleteColor";
                objBllAndFunction.SelectRequestType = new SelectByColorIDRequest();
                objBllAndFunction.InsertRequestData = new SaveColorRequest();
                objBllAndFunction.UpdateRequestData = new UpdateColorRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByColorIDResponse();
                objBllAndFunction.InsertResponseData = new SaveColorResponse();
                objBllAndFunction.UpdateResponseData = new UpdateColorResponse();

                objBllAndFunction.DynamicData = new ColorMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
           
            else if ((ClassName == "EasyBizBLL.Masters.CompanySettingBLL") && (FunctionName == "SaveCompanySetting" || FunctionName == "UpdateCompanySetting" || FunctionName == "DeleteCompanySetting"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CompanySettingBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDCompanySetting";
                objBllAndFunction.InsertFunctionName = "SaveCompanySetting";
                objBllAndFunction.UpdateFunctionName = "UpdateCompanySetting";
                objBllAndFunction.DeleteFunctionName = "DeleteCompanySetting";
                objBllAndFunction.SelectRequestType = new SelectByIDCompanySettingRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCompanySettingResponse();
                objBllAndFunction.InsertRequestData = new SaveCompanySettingRequest();
                objBllAndFunction.InsertResponseData = new SaveCompanySettingResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCompanySettingRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCompanySettingResponse();

                objBllAndFunction.DynamicData = new CompanySettings();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.CountryBLL") && (FunctionName == "SaveCountryMaster" || FunctionName == "UpdateCountryMaster" || FunctionName == "DeleteCountryMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CountryBLL";
                objBllAndFunction.SelectFunctionName = "SelectCountryMaster";
                objBllAndFunction.InsertFunctionName = "SaveCountryMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateCountryMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteCountryMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDCountryRequest();
                objBllAndFunction.InsertRequestData = new SaveCountryRequest();
                objBllAndFunction.UpdateRequestData = new UpdateCountryRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCountryResponse();
                objBllAndFunction.InsertResponseData = new SaveCountryResponse();
                objBllAndFunction.UpdateResponseData = new UpdateCountryResponse();

                objBllAndFunction.DynamicData = new CountryMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.CountTypeMasterBLL") && ( FunctionName == "SelectCountTypeMasterLookUp"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CountTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectCountTypeMasterLookUp";

                objBllAndFunction.SelectRequestType = new SelectCountTypeMasterLookUpRequest();               
                objBllAndFunction.SelectBaseResponse = new SelectCountTypeMasterLookUpResponse();


                objBllAndFunction.DynamicData = new CountTypeMaster();
            }
         
            else if ((ClassName == "EasyBizBLL.Masters.CurrencyBLL") && (FunctionName == "SaveCurrencyMaster" || FunctionName == "UpdateCurrencyMaster" || FunctionName == "DeleteCurrencyMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CurrencyBLL";
                objBllAndFunction.SelectFunctionName = "SelectCurrencyMaster";
                objBllAndFunction.InsertFunctionName = "SaveCurrencyMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateCurrencyMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteCurrencyMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDCurrencyRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCurrencyResponse();
                objBllAndFunction.InsertRequestData = new SaveCurrencyRequest();
                objBllAndFunction.InsertResponseData = new SaveCurrencyResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCurrencyRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCurrencyResponse();

                objBllAndFunction.DynamicData = new CurrencyMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
         
            else if ((ClassName == "EasyBizBLL.Masters.CustomerGroupBLL") && (FunctionName == "SaveCustomerGroup" || FunctionName == "UpdateCustomerGroup" || FunctionName == "DeleteCustomerGroup"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CustomerGroupBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDCustomerGroupResponse";
                objBllAndFunction.InsertFunctionName = "SaveCustomerGroup";
                objBllAndFunction.UpdateFunctionName = "UpdateCustomerGroup";
                objBllAndFunction.DeleteFunctionName = "DeleteCompanySetting";
                objBllAndFunction.SelectRequestType = new SelectByIDCustomerGroupRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCustomerGroupResponse();
                objBllAndFunction.InsertRequestData = new SaveCustomerGroupRequest();
                objBllAndFunction.InsertResponseData = new SaveCustomerGroupResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCustomerGroupMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCustomerGroupMasterResponse();

                objBllAndFunction.DynamicData = new CustomerGroupMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
           
            else if ((ClassName == "EasyBizBLL.Masters.CustomerMasterBLL") && (FunctionName == "SaveCustomerMaster" || FunctionName == "UpdateCustomerMaster" || FunctionName == "DeleteCustomerMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CustomerMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDCustomerMaster";
                objBllAndFunction.InsertFunctionName = "SaveCustomerMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateCustomerMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteCustomerMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDCustomerMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCustomerMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveCustomerMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveCustomerMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCustomerMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCustomerMasterResponse();
                objBllAndFunction.DynamicData = new CustomerMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.DesignationMasterBLL") && (FunctionName == "SaveDesignationMaster" || FunctionName == "UpdateDesignationMaster" || FunctionName == "DeleteDesignationMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.DesignationMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectDesignationMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveDesignationMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateDesignationMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteDesignationMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDDesignationMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveDesignationMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateDesignationMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDDesignationMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveDesignationMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateDesignationMasterResponse();

                objBllAndFunction.DynamicData = new DesignationMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.DesignMasterBLL") && (FunctionName == "SaveDesignMaster" || FunctionName == "UpdateDesignMaster" || FunctionName == "DeleteDesignMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.DesignMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDDesignMaster";
                objBllAndFunction.InsertFunctionName = "SaveDesignMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateDesignMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteDesignMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDDesignMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDDesignMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveDesignMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveDesignMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateDesignMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateDesignMasterResponse();
                objBllAndFunction.DynamicData = new DesignMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                //objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.DivisionBLL") && (FunctionName == "SaveDivision" || FunctionName == "UpdateDivision" || FunctionName == "DeleteDivision"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.DivisionBLL";
                objBllAndFunction.SelectFunctionName = "SelectDivisionRecord";
                objBllAndFunction.InsertFunctionName = "SaveDivision";
                objBllAndFunction.UpdateFunctionName = "UpdateDivision";
                objBllAndFunction.DeleteFunctionName = "DeleteDivision";
                objBllAndFunction.SelectRequestType = new SelectByDivisionIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByDivisionIDResponse();
                objBllAndFunction.InsertRequestData = new SaveDivisionRequest();
                objBllAndFunction.InsertResponseData = new SaveDivisionResponse();
                objBllAndFunction.UpdateRequestData = new UpdateDivisionRequest();
                objBllAndFunction.UpdateResponseData = new UpdateDivisionResponse();
                objBllAndFunction.DynamicData = new DivisionMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.DocumentNumberingBLL") && (FunctionName == "SaveDocumentNumberingMaster" || FunctionName == "UpdateDocumentNumberingMaster" || FunctionName == "DeleteDocumentNumberingMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.DocumentNumberingBLL";
                objBllAndFunction.SelectFunctionName = "SelectDocumentNumberingMaster";
                objBllAndFunction.InsertFunctionName = "SaveDocumentNumberingMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateDocumentNumberingMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteDocumentNumberingMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDDocumentNumberingMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDDocumentNumberingMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveDocumentNumberingMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveDocumentNumberingMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateDocumentNumberingMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateDocumentNumberingMasterResponse();

                objBllAndFunction.DynamicData = new DocumentNumberingMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToStore;
            }
            else if ((ClassName == "EasyBizBLL.Masters.DocumentTypeBLL") && (FunctionName == "SaveDocumentType" || FunctionName == "UpdateDocumentType" || FunctionName == "DeleteDocumentType"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.DocumentTypeBLL";
                objBllAndFunction.SelectFunctionName = "SelectDocumentType";
                objBllAndFunction.InsertFunctionName = "SaveDocumentType";
                objBllAndFunction.UpdateFunctionName = "UpdateDocumentType";
                objBllAndFunction.DeleteFunctionName = "DeleteDocumentType";
                objBllAndFunction.SelectRequestType = new SelectByIDDocumentTypeRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDDocumentTypeResponse();
                objBllAndFunction.InsertRequestData = new SaveDocumentTypeRequest();
                objBllAndFunction.InsertResponseData = new SaveDocumentTypeResponse();
                objBllAndFunction.UpdateRequestData = new UpdateDocumentTypeRequest();
                objBllAndFunction.UpdateResponseData = new UpdateDocumentTypeResponse();

                objBllAndFunction.DynamicData = new DocumentTypes();
            }
            else if ((ClassName == "EasyBizBLL.Masters.DropMasterBLL") && (FunctionName == "SaveDropMaster" || FunctionName == "UpdateDropMaster" || FunctionName == "DeleteDropMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.DropMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDDropMaster";
                objBllAndFunction.InsertFunctionName = "SaveDropMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateDropMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteDropMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDDropMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveDropMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateDropMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDDropMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveDropMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateDropMasterResponse();

                objBllAndFunction.DynamicData = new DropMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.EmployeeMasterBLL") && (FunctionName == "SaveEmployeeMaster" || FunctionName == "UpdateEmployeeMaster" || FunctionName == "DeleteEmployeeMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.EmployeeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectEmployeeMaster";
                objBllAndFunction.InsertFunctionName = "SaveEmployeeMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateEmployeeMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteEmployeeMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDEmployeeMasterRequest();
                objBllAndFunction.SelectBaseResponse = new GetEmployeeByStoreResponse();
                objBllAndFunction.InsertRequestData = new SaveEmployeeMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveEmployeeMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateEmployeeMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateEmployeeMasterResponse();

                objBllAndFunction.DynamicData = new EmployeeMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ExchangeRatesBLL") && (FunctionName == "SaveExchangeRates" || FunctionName == "SelectExchangeRatesRecord"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ExchangeRatesBLL";
                objBllAndFunction.SelectFunctionName = "SelectExchangeRatesRecord";
                objBllAndFunction.InsertFunctionName = "SaveExchangeRates";               
                objBllAndFunction.SelectRequestType = new SelectByIDExchangeRatesRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDExchangeRatesResponse();
                objBllAndFunction.InsertRequestData = new SaveExchangeRatesRequest();
                objBllAndFunction.InsertResponseData = new SaveExchangeRatesResponse();          
                objBllAndFunction.DynamicData = new ExchangeRates();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ExpenseMasterBLL") && (FunctionName == "SaveExpenseMaster" || FunctionName == "UpdateExpenseMaster" || FunctionName == "DeleteExpenseMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ExpenseMasterBLL"; 
                //objBllAndFunction.SelectFunctionName = "SelectByIDExpenseMasterResponse";
                objBllAndFunction.SelectFunctionName = "SelectIDAllExpenseMaster";
                objBllAndFunction.InsertFunctionName = "SaveExpenseMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateExpenseMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteExpenseMaster"; 
                //objBllAndFunction.SelectRequestType = new SelectByIDExpenseMasterRequest();
                objBllAndFunction.SelectRequestType = new SelectIDExpenseMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveExpenseMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateExpenseMasterRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectByIDExpenseMasterResponse();
                objBllAndFunction.SelectBaseResponse = new SelectIDExpenseMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveExpenseMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateExpenseMasterResponse();

                objBllAndFunction.DynamicData = new ExpenseMasterTypes();
                //objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.FreightMasterBLL") && (FunctionName == "SaveFreightMaster" || FunctionName == "UpdateFreightMaster" || FunctionName == "DeleteFreightMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.FreightMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectFreightMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveFreightMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateFreightMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteFreightMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDFreightMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveFreightMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateFreightMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDFreightMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveFreightMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateFreightMasterResponse();

                objBllAndFunction.DynamicData = new FreightMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ItemGroupMasterBLL") && (FunctionName == "SelectAllGroupTypeMaster" ))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ItemGroupMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectAllGroupTypeMaster";

                objBllAndFunction.SelectRequestType = new SelectAllGroupTypeMasterRequest();                
                objBllAndFunction.SelectBaseResponse = new SelectAllItemGroupMasterResponse();
                objBllAndFunction.DynamicData = new ItemGroupMasterTypes();
            }
            else if ((ClassName == "EasyBizBLL.Masters.ItemTypeMasterBLL") && (FunctionName == "SelectAllItemTypeMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ItemTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectAllItemTypeMaster";

                objBllAndFunction.SelectRequestType = new SelectAllItemTypeMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectAllItemTypeMasterResponse();
                objBllAndFunction.DynamicData = new ItemTypeMasterTypes();
            }
            else if ((ClassName == "EasyBizBLL.Masters.LanguageBLL") && (FunctionName == "SaveLanguage" || FunctionName == "UpdateLanguage" || FunctionName == "DeleteLanguage"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.LanguageBLL";
                objBllAndFunction.SelectFunctionName = "SelectLanguage";
                objBllAndFunction.InsertFunctionName = "SaveLanguage";
                objBllAndFunction.UpdateFunctionName = "UpdateLanguage";
                objBllAndFunction.DeleteFunctionName = "DeleteLanguage";
                objBllAndFunction.SelectRequestType = new SelectByLanguageIDRequest();
                objBllAndFunction.InsertRequestData = new SaveLanguageRequest();
                objBllAndFunction.UpdateRequestData = new UpdateLanguageRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByLanguageIDResponse();
                objBllAndFunction.InsertResponseData = new SaveLanguageResponse();
                objBllAndFunction.UpdateResponseData = new UpdateLanguageResponse();

                objBllAndFunction.DynamicData = new LanguageMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ManagerOverrideBLL") && (FunctionName == "SaveManagerOverride" || FunctionName == "UpdateManagerOverride" || FunctionName == "DeleteManagerOverride" || FunctionName == "SelectManagerOverride"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ManagerOverrideBLL";
                objBllAndFunction.SelectFunctionName = "SelectManagerOverride";
                objBllAndFunction.InsertFunctionName = "SaveManagerOverride";
                objBllAndFunction.UpdateFunctionName = "UpdateManagerOverride";
                objBllAndFunction.DeleteFunctionName = "DeleteManagerOverride";
                objBllAndFunction.SelectRequestType = new SelectByIDManagerOverrideRequest();
                objBllAndFunction.InsertRequestData = new SaveManagerOverrideRequest();
                objBllAndFunction.UpdateRequestData = new UpdateManagerOverrideRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDManagerOverrideResponse();
                objBllAndFunction.InsertResponseData = new SaveManagerOverrideResponse();
                objBllAndFunction.UpdateResponseData = new UpdateManagerOverrideResponse();

                objBllAndFunction.DynamicData = new ManagerOverride();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.OrderTypeMasterBLL") && (FunctionName == "SaveOrderTypeMaster" || FunctionName == "UpdateOrderTypeMaster" || FunctionName == "DeleteOrderTypeMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.OrderTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectOrderTypeMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveOrderTypeMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateOrderTypeMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteOrderTypeMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDOrderTypeMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveOrderTypeMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateOrderTypeMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDOrderTypeMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveOrderTypeMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateOrderTypeMasterResponse();

                objBllAndFunction.DynamicData = new OrderTypeMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.PaymentTypeMasterBLL") && (FunctionName == "SavePaymentType" || FunctionName == "UpdatePaymentType" || FunctionName == "DeletePaymentType"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.PaymentTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDPaymentType";
                objBllAndFunction.InsertFunctionName = "SavePaymentType";
                objBllAndFunction.UpdateFunctionName = "UpdatePaymentType";
                objBllAndFunction.DeleteFunctionName = "DeletePaymentType";
                objBllAndFunction.SelectRequestType = new SelectByIDPaymentTypeRequest();
                objBllAndFunction.InsertRequestData = new SavePaymentTypeRequest();
                objBllAndFunction.UpdateRequestData = new UpdatePaymentTypeRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDPaymentTypeResponse();
                objBllAndFunction.InsertResponseData = new SavePaymentTypeResponse();
                objBllAndFunction.UpdateResponseData = new UpdatePaymentTypeResponse();

                objBllAndFunction.DynamicData = new PaymentTypeMasterType();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.PosMasterBLL") && (FunctionName == "SavePosMaster" || FunctionName == "UpdatePosMaster" || FunctionName == "DeletePosMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.PosMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectPosMasterRecord";
                objBllAndFunction.InsertFunctionName = "SavePosMaster";
                objBllAndFunction.UpdateFunctionName = "UpdatePosMaster";
                objBllAndFunction.DeleteFunctionName = "DeletePosMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDPosMasterRequest();
                objBllAndFunction.InsertRequestData = new SavePosMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdatePosMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDPosMasterResponse();
                objBllAndFunction.InsertResponseData = new SavePosMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdatePosMasterResponse();

                objBllAndFunction.DynamicData = new PosMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToStore;
            }
            // Changed by Senthamil @ 06.09.2018
            else if ((ClassName == "EasyBizBLL.Masters.PrevilegesBLL") && (FunctionName == "SaveMASUserprivilagesResponse"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.PrevilegesBLL";
                objBllAndFunction.SelectFunctionName = "SelectUserIDPrivilagesResponse";
                objBllAndFunction.InsertFunctionName = "SaveMASUserprivilagesResponse";
                objBllAndFunction.SelectRequestType = new SelectByUserIDPrivilagesRequest();
                objBllAndFunction.InsertRequestData = new SavePrevilegesRequestt();
                //objBllAndFunction.UpdateRequestData = new UpdateYearRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByUserIDPrivilagesResponse();
                objBllAndFunction.InsertResponseData = new SavePrevilegesResponse();
                //objBllAndFunction.UpdateResponseData = new UpdateYearResponse();

                objBllAndFunction.DynamicData = new UserPrivilagesTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            // Changed by Senthamil @ 07.09.2018
            else if ((ClassName == "EasyBizBLL.Transactions.Promotions.PromotionMappingBLL") && (FunctionName == "SavePromotionMapping"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Promotions.PromotionMappingBLL";
                objBllAndFunction.SelectFunctionName = "SelectAllPromotionMapping";
                objBllAndFunction.InsertFunctionName = "SavePromotionMapping";
                objBllAndFunction.SelectRequestType = new SelectAllPromotionMappingRequest();
                objBllAndFunction.InsertRequestData = new SavePromotionMappingRequest();
                //objBllAndFunction.UpdateRequestData = new UpdateYearRequest();
                objBllAndFunction.SelectBaseResponse = new SelectAllPromotionMappingResponse();
                objBllAndFunction.InsertResponseData = new SavePromotionMappingResponse();
                //objBllAndFunction.UpdateResponseData = new UpdateYearResponse();

                objBllAndFunction.DynamicData = new PromotionMappingTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.PriceListBLL") && (FunctionName == "SavePriceList" || FunctionName == "UpdatePriceList" || FunctionName == "DeletePriceList"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.PriceListBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDPriceList";
                objBllAndFunction.InsertFunctionName = "SavePriceList";
                objBllAndFunction.UpdateFunctionName = "UpdatePriceList";
                objBllAndFunction.DeleteFunctionName = "DeletePriceList";
                objBllAndFunction.SelectRequestType = new SelectByIDPriceListRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDPriceListResponse();
                objBllAndFunction.InsertRequestData = new SavePriceListRequest();
                objBllAndFunction.InsertResponseData = new SavePriceListResponse();
                objBllAndFunction.UpdateRequestData = new UpdatePriceListRequest();
                objBllAndFunction.UpdateResponseData = new UpdatePriceListResponse();

                objBllAndFunction.DynamicData = new PriceListType();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.PriceTypeBLL") && (FunctionName == "SavePriceType" || FunctionName == "UpdatePriceType" || FunctionName == "DeletePriceType"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.PriceTypeBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDPriceType";
                objBllAndFunction.InsertFunctionName = "SavePriceType";
                objBllAndFunction.UpdateFunctionName = "UpdatePriceType";
                objBllAndFunction.DeleteFunctionName = "DeletePriceType";
                objBllAndFunction.SelectRequestType = new SelectByIDPriceTypeRequest();
                objBllAndFunction.InsertRequestData = new SavePriceTypeRequest();
                objBllAndFunction.UpdateRequestData = new UpdatePriceTypeRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDPriceTypeResponse();
                objBllAndFunction.InsertResponseData = new SavePriceTypeResponse();
                objBllAndFunction.UpdateResponseData = new UpdatePriceTypeResponse();

                objBllAndFunction.DynamicData = new PriceTypeMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ProductGroupBLL") && (FunctionName == "SaveProductGroup" || FunctionName == "UpdateProductGroup" || FunctionName == "DeleteProductGroup"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ProductGroupBLL";
                objBllAndFunction.SelectFunctionName = "SelectProductGroup";
                objBllAndFunction.InsertFunctionName = "SaveProductGroup";
                objBllAndFunction.UpdateFunctionName = "UpdateProductGroup";
                objBllAndFunction.DeleteFunctionName = "DeleteProductGroup";
                objBllAndFunction.SelectRequestType = new SelectByIDProductGroupRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDProductGroupResponse();
                objBllAndFunction.InsertRequestData = new SaveProductGroupRequest();
                objBllAndFunction.InsertResponseData = new SaveProductGroupResponse();
                objBllAndFunction.UpdateRequestData = new UpdateProductGroupRequest();
                objBllAndFunction.UpdateResponseData = new UpdateProductGroupResponse();

                objBllAndFunction.DynamicData = new ProductGroupMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.ProductSubGroupBLL") && (FunctionName == "SaveProductSubGroup" || FunctionName == "UpdateProductSubGroup" || FunctionName == "DeleteProductSubGroup"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ProductSubGroupBLL";
                // Changed by Senthamil @ 06-09-2018
                //objBllAndFunction.SelectFunctionName = "";
                objBllAndFunction.SelectFunctionName = "ProductSubGroupByProductGroup";                
                objBllAndFunction.InsertFunctionName = "SaveProductSubGroup";
                objBllAndFunction.UpdateFunctionName = "UpdateProductSubGroup";
                objBllAndFunction.DeleteFunctionName = "DeleteProductSubGroup";
                // Changed by Senthamil @ 06-09-2018
                //objBllAndFunction.SelectRequestType = new SelectByProductSubGroupIDRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectByProductSubGroupIDResponse();
                objBllAndFunction.SelectRequestType = new SelectProductGroupListForProductSubGroupRequest();
                objBllAndFunction.SelectBaseResponse = new SelectProductGroupListForProductSubGroupResponse();
                objBllAndFunction.InsertRequestData = new SaveProductSubGroupRequest();
                objBllAndFunction.InsertResponseData = new SaveProductSubGroupResponse();
                objBllAndFunction.UpdateRequestData = new UpdateProductSubGroupRequest();
                objBllAndFunction.UpdateResponseData = new UpdateProductSubGroupResponse();

                objBllAndFunction.DynamicData = new ProductSubGroupMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ProductLineMasterBLL") && (FunctionName == "SaveProductLineMaster" || FunctionName == "UpdateProductLineMaster" || FunctionName == "DeleteProductLineMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ProductLineMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectProductLineMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveProductLineMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateProductLineMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteProductLineMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDProductLineMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveProductLineMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateProductLineMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDProductLineMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveProductLineMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateProductLineMasterResponse();

                objBllAndFunction.DynamicData = new ProductLineMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.PromotionsMasterBLL") && (FunctionName == "SavePromotions" || FunctionName == "UpdatePromotions" || FunctionName == "DeletePromotions"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.PromotionsMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectPromotionsRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SavePromotions";
                objBllAndFunction.UpdateFunctionName = "UpdatePromotions";
                objBllAndFunction.DeleteFunctionName = "DeletePromotions";
                objBllAndFunction.SelectRequestType = new SelectByPromotionsIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByPromotionsIDResponse();
                objBllAndFunction.InsertRequestData = new SavePromotionsRequest();
                objBllAndFunction.InsertResponseData = new SavePromotionsResponse();
                objBllAndFunction.UpdateRequestData = new UpdatePromotionsRequest();
                objBllAndFunction.UpdateResponseData = new UpdatePromotionsResponse();

                objBllAndFunction.DynamicData = new PromotionsMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ReasonMasterBLL") && (FunctionName == "SaveReasonMaster" || FunctionName == "UpdateReasonMaster" || FunctionName == "DeleteReasonMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ReasonMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectReasonMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveReasonMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateReasonMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteReasonMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDReasonMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveReasonMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateReasonMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDReasonMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveReasonMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateReasonMasterResponse();

                objBllAndFunction.DynamicData = new ReasonMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.RequestTypeMasterBLL") && (FunctionName == "SaveRequestTypeMaster" || FunctionName == "UpdateRequestTypeMaster" || FunctionName == "DeleteRequestTypeMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.RequestTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectRequestTypeMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveRequestTypeMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateRequestTypeMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteRequestTypeMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDRequestTypeMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveRequestTypeMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateRequestTypeMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDRequestTypeMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveRequestTypeMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateRequestTypeMasterResponse();

                objBllAndFunction.DynamicData = new RequestTypeMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.RetailSettingsBLL") && (FunctionName == "SaveRetail" || FunctionName == "UpdateRetail" || FunctionName == "DeleteRetail" || FunctionName == "SelectRetailRecord"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.RetailSettingsBLL";
                objBllAndFunction.SelectFunctionName = "SelectRetailRecord";
                objBllAndFunction.InsertFunctionName = "SaveRetail";
                objBllAndFunction.UpdateFunctionName = "UpdateRetail";
                objBllAndFunction.DeleteFunctionName = "DeleteRetail";
                objBllAndFunction.SelectRequestType = new SelectByRetailIDRequest();
                objBllAndFunction.InsertRequestData = new SaveRetailRequest();
                objBllAndFunction.UpdateRequestData = new UpdateRetailRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByRetailIDResponse();
                objBllAndFunction.InsertResponseData = new SaveRetailResponse();
                objBllAndFunction.UpdateResponseData = new UpdateRetailReponse();


                objBllAndFunction.DynamicData = new RetailSettingsType();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.RoleBLL") && (FunctionName == "SaveRoleMaster" || FunctionName == "UpdateRoleMaster" || FunctionName == "DeleteRoleMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.RoleBLL";
                objBllAndFunction.SelectFunctionName = "SelectRoleMaster";
                objBllAndFunction.InsertFunctionName = "SaveRoleMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateRoleMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteRoleMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDRoleRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDRoleResponse();
                objBllAndFunction.InsertRequestData = new SaveRoleRequest();
                objBllAndFunction.InsertResponseData = new SaveRoleResponse();
                objBllAndFunction.UpdateRequestData = new UpdateRoleRequest();
                objBllAndFunction.UpdateResponseData = new UpdateRoleResponse();

                objBllAndFunction.DynamicData = new RoleMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ScaleBLL") && (FunctionName == "SaveScale" || FunctionName == "UpdateScale" || FunctionName == "DeleteScale"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ScaleBLL";
                objBllAndFunction.SelectFunctionName = "SelectScaleRecord";
                objBllAndFunction.InsertFunctionName = "SaveScale";
                objBllAndFunction.UpdateFunctionName = "UpdateScale";
                objBllAndFunction.DeleteFunctionName = "DeleteScale";
                objBllAndFunction.SelectRequestType = new SelectByScaleIDRequest();
                objBllAndFunction.InsertRequestData = new SaveScaleRequest();
                objBllAndFunction.UpdateRequestData = new UpdateScaleRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByScaleIDResponse();
                objBllAndFunction.InsertResponseData = new SaveScaleResponse();
                objBllAndFunction.UpdateResponseData = new UpdateScaleResponse();

                objBllAndFunction.DynamicData = new ScaleMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.SeasonBLL") && (FunctionName == "SaveSeasonMaster" || FunctionName == "UpdateSeasonMaster" || FunctionName == "DeleteSeasonMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.SeasonBLL";
                objBllAndFunction.SelectFunctionName = "SelectSeasonMaster"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveSeasonMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateSeasonMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteSeasonMaster";
                objBllAndFunction.SelectRequestType = new SelectBySeasonIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectBySeasonIDResponse();
                objBllAndFunction.InsertRequestData = new SaveSeasonRequest();
                objBllAndFunction.InsertResponseData = new SaveSeasonResponse();
                objBllAndFunction.UpdateRequestData = new UpdateSeasonRequest();
                objBllAndFunction.UpdateResponseData = new UpdateSeasonResponse();

                objBllAndFunction.DynamicData = new SeasonMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.SegmentMasterBLL") && (FunctionName == "SaveSegementMaster" || FunctionName == "UpdateSegmentMaster" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.SegmentMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDSegmentMaster"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveSegementMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateSegmentMaster";
                objBllAndFunction.DeleteFunctionName = "";
                objBllAndFunction.SelectRequestType = new SelectBySegmentIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectBySegmentIDResponse();
                objBllAndFunction.InsertRequestData = new SaveSegmentRequest();
                objBllAndFunction.InsertResponseData = new SaveSegmentResponse();
                objBllAndFunction.UpdateRequestData = new UpdateSegmentRequest();
                objBllAndFunction.UpdateResponseData = new UpdateSegmentResponse();

                objBllAndFunction.DynamicData = new SegmentMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.ShiftBLL") && (FunctionName == "SaveShift" || FunctionName == "UpdateShift" || FunctionName == "DeleteShift"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.ShiftBLL";
                objBllAndFunction.SelectFunctionName = "ShiftByCountry"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveShift";
                objBllAndFunction.UpdateFunctionName = "UpdateShift";
                objBllAndFunction.DeleteFunctionName = "DeleteShift";              
                objBllAndFunction.InsertRequestData = new SaveShiftRequest();
                objBllAndFunction.InsertResponseData = new SaveShiftResponse();
                objBllAndFunction.UpdateRequestData = new UpdateShiftRequest();
                objBllAndFunction.UpdateResponseData = new UpdateShiftResponse();
                objBllAndFunction.SelectRequestType = new SelectShiftListForCategoryRequest();
                objBllAndFunction.SelectBaseResponse = new SelectShiftListForCategoryResponse();

                objBllAndFunction.DynamicData = new SegmentMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.SKUMasterBLL") && (FunctionName == "SaveSKUMaster" || FunctionName == "UpdateSKUMaster" || FunctionName == "DeleteSKUMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.SKUMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIdSKUMaster";
                objBllAndFunction.InsertFunctionName = "SaveSKUMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateSKUMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteSKUMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDSKUMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveSKUMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateSKUMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDSKUMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveSKUMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateSKUMasterResponse();

                objBllAndFunction.DynamicData = new SKUMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                //objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.StateMasterBLL") && (FunctionName == "SaveStateMaster" || FunctionName == "UpdateState" || FunctionName == "DeleteStateMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.StateMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectStateRecord";
                objBllAndFunction.InsertFunctionName = "SaveStateMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateState";
                objBllAndFunction.DeleteFunctionName = "DeleteStateMaster";
                objBllAndFunction.SelectRequestType = new SelectByStateIDRequest();
                objBllAndFunction.InsertRequestData = new SaveStateRequest();
                objBllAndFunction.UpdateRequestData = new UpdateStateRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByStateIDResponse();
                objBllAndFunction.InsertResponseData = new SaveStateResponse();
                objBllAndFunction.UpdateResponseData = new UpdateStateResponse();

                objBllAndFunction.DynamicData = new StateMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.StoreGroupBLL") && (FunctionName == "SaveStoreGroupMaster" || FunctionName == "UpdateStoreGroupMaster" || FunctionName == "DeleteStoreGroupMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.StoreGroupBLL";
                objBllAndFunction.SelectFunctionName = "SelectStoreGroupMasterRecord";
                objBllAndFunction.InsertFunctionName = "SaveStoreGroupMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateStoreGroupMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteStoreGroupMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDStoreGroupRequest();
                objBllAndFunction.InsertRequestData = new SaveStoreGroupRequest();
                objBllAndFunction.UpdateRequestData = new UpdateStoreGroupRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDStoreGroupResponse();
                objBllAndFunction.InsertResponseData = new SaveStoreGroupResponse();
                objBllAndFunction.UpdateResponseData = new UpdateStoreGroupResponse();

                objBllAndFunction.DynamicData = new StoreGroupMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.StoreMasterBLL") && (FunctionName == "SaveStoreMaster" || FunctionName == "UpdateStoreMaster" || FunctionName == "DeleteStoreMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.StoreMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDStoreMaster";
                objBllAndFunction.InsertFunctionName = "SaveStoreMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateStoreMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteStoreMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDStoreMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveStoreMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateStoreMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDStoreMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveStoreMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateStoreMasterResponse();

                objBllAndFunction.DynamicData = new StoreMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }


            else if ((ClassName == "EasyBizBLL.Masters.StyleMasterBLL") && (FunctionName == "SaveStyle" || FunctionName == "UpdateStyle" || FunctionName == "DeleteStyle"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.StyleMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectStyleRecord";
                objBllAndFunction.InsertFunctionName = "SaveStyle";
                objBllAndFunction.UpdateFunctionName = "UpdateStyle";
                objBllAndFunction.DeleteFunctionName = "DeleteStyle";
                objBllAndFunction.SelectRequestType = new SelectByStyleIDRequest();
                objBllAndFunction.InsertRequestData = new SaveStyleRequest();
                objBllAndFunction.UpdateRequestData = new UpdateStyleRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectByStyleIDResponse();
                objBllAndFunction.InsertResponseData = new SaveStyleResponse();
                objBllAndFunction.UpdateResponseData = new UpdateStyleResponse();

                objBllAndFunction.DynamicData = new StyleMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                //objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            // Changed by Senthamil @ 05.09.2018
            else if ((ClassName == "EasyBizBLL.Masters.StyleStatusBLL") && (FunctionName == "SaveStyleStatus" || FunctionName == "UpdateStyleStatus" || FunctionName == "DeleteStyleStatus" || FunctionName == "SelectByIDStyleStatus"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.StyleStatusBLL";
                //objBllAndFunction.SelectFunctionName = "SelectStyleRecord";
                objBllAndFunction.SelectFunctionName = "SelectByIDStyleStatus";                
                objBllAndFunction.InsertFunctionName = "SaveStyleStatus";
                objBllAndFunction.UpdateFunctionName = "UpdateStyleStatus";
                objBllAndFunction.DeleteFunctionName = "DeleteStyleStatus";
                objBllAndFunction.SelectRequestType = new SelectByIDStyleStatusMasterRequest();
                objBllAndFunction.InsertRequestData = new SaveStyleStatusMasterRequest();
                objBllAndFunction.UpdateRequestData = new UpdateStyleStatusMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDStyleStatusMasterResponse();
                objBllAndFunction.InsertResponseData = new SaveStyleStatusMasterResponse();
                objBllAndFunction.UpdateResponseData = new UpdateStyleStatusMasterResponse();

                objBllAndFunction.DynamicData = new StyleStatusMasterType();
                //objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.TailoringMasterBLL") && (FunctionName == "SaveTailoringmaster" || FunctionName == "UpdateTailoringmaster" || FunctionName == "DeleteTailoringmaster" || FunctionName == "SelectTailoringUnitRecord"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.TailoringMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectTailoringUnitRecord";
                objBllAndFunction.InsertFunctionName = "SaveTailoringmaster";
                //objBllAndFunction.UpdateFunctionName = "";
                //objBllAndFunction.DeleteFunctionName = "";
                objBllAndFunction.SelectRequestType = new SelectByTailoringIDRequest();
                objBllAndFunction.InsertRequestData = new SaveTailoringRequest();
                //objBllAndFunction.UpdateRequestData = new UpdateStyleStatusMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByTailoringIDResponse();
                objBllAndFunction.InsertResponseData = new SaveTailoringResponse();
                //objBllAndFunction.UpdateResponseData = new UpdateStyleStatusMasterResponse();

                objBllAndFunction.DynamicData = new TailoringMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
			else if ((ClassName == "EasyBizBLL.Transactions.PriceChange.PriceChangeBLL") && (FunctionName == "SavePriceChange" || FunctionName == "SelectPriceChangeRecord"))
			{
				objBllAndFunction.ClassName = "EasyBizBLL.Transactions.PriceChange.PriceChangeBLL";
				objBllAndFunction.SelectFunctionName = "SelectPriceChangeRecord";
				objBllAndFunction.InsertFunctionName = "SavePriceChange";
				//objBllAndFunction.UpdateFunctionName = "";
				//objBllAndFunction.DeleteFunctionName = "";
				objBllAndFunction.SelectRequestType = new SelectPriceChangeRecordRequest();
				objBllAndFunction.InsertRequestData = new SavePriceChangeRequest();
				//objBllAndFunction.UpdateRequestData = new UpdateStyleStatusMasterRequest();
				objBllAndFunction.SelectBaseResponse = new SelectPriceChangeRecordResponse();
				objBllAndFunction.InsertResponseData = new SavePriceChangeResponse();
				//objBllAndFunction.UpdateResponseData = new UpdateStyleStatusMasterResponse();

				objBllAndFunction.DynamicData = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
				objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
			}
            else if ((ClassName == "EasyBizBLL.Masters.SubBrandBLL") && (FunctionName == "SaveSubBrand" || FunctionName == "UpdateSubBrand" || FunctionName == "DeleteSubBrand"))
            {
                // Changed by Senthamil @ 11.09.2018
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.SubBrandBLL";
                //objBllAndFunction.SelectFunctionName = "SelectSubBrandRecord";
                objBllAndFunction.SelectFunctionName = "SubBrandByBrand";
                objBllAndFunction.InsertFunctionName = "SaveSubBrand";
                objBllAndFunction.UpdateFunctionName = "UpdateSubBrand";
                objBllAndFunction.DeleteFunctionName = "DeleteSubBrand";
                //objBllAndFunction.SelectRequestType = new SelectBySubBrandIDRequest();
                objBllAndFunction.SelectRequestType = new SelectSubBrandListForCategoryRequest();
                objBllAndFunction.InsertRequestData = new SaveSubBrandRequest();
                objBllAndFunction.UpdateRequestData = new UpdateSubBrandRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectBySubBrandIDResponse();
                objBllAndFunction.SelectBaseResponse = new SelectSubBrandListForCategoryResponse();
                objBllAndFunction.InsertResponseData = new SaveSubBrandResponse();
                objBllAndFunction.UpdateResponseData = new UpdateSubBrandResponse();

                objBllAndFunction.DynamicData = new SubBrandMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.SubCollectionBLL") && (FunctionName == "SaveSubCollection" || FunctionName == "" || FunctionName == ""))
            {
                // Changed by Senthamil @ 11.09.2018
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.SubCollectionBLL";
                //objBllAndFunction.SelectFunctionName = "SelectAllSubBrandRecords"; // Need to update DAL
                objBllAndFunction.SelectFunctionName = "SelectSubCollectionByCollection"; // Need to update DAL                
                objBllAndFunction.InsertFunctionName = "SaveSubCollection";
                objBllAndFunction.UpdateFunctionName = "";
                objBllAndFunction.DeleteFunctionName = "";
                //objBllAndFunction.SelectRequestType = new SelectByIDSubCollectionRequest();
                //objBllAndFunction.SelectBaseResponse = new SelectByIDSubCollectionResponse();
                objBllAndFunction.SelectRequestType = new SelectSubCollectionListForCollectionRequest();
                objBllAndFunction.SelectBaseResponse = new SelectSubCollectionListForCollectionResponse();
                objBllAndFunction.InsertRequestData = new SaveSubCollectionRequest();
                objBllAndFunction.InsertResponseData = new SaveSubCollectionResponse();
                objBllAndFunction.UpdateRequestData = new UpdateSubCollectionRequest();
                objBllAndFunction.UpdateResponseData = new UpdateSubCollectionResponse();

                objBllAndFunction.DynamicData = new SubCollectionMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.SubSeasonBLL") && (FunctionName == "SaveSubSeason" || FunctionName == "UpdateSubSeason" || FunctionName == "DeleteSubSeason"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.SubSeasonBLL";
                objBllAndFunction.SelectFunctionName = "SelectSubSeasonRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveSubSeason";
                objBllAndFunction.UpdateFunctionName = "UpdateSubSeason";
                objBllAndFunction.DeleteFunctionName = "DeleteSubSeason";
                objBllAndFunction.SelectRequestType = new SelectBySubSeasonIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectBySubSeasonIDResponse();
                objBllAndFunction.InsertRequestData = new SaveSubSeasonRequest();
                objBllAndFunction.InsertResponseData = new SaveSubSeasonResponse();
                objBllAndFunction.UpdateRequestData = new UpdateSubSeasonRequest();
                objBllAndFunction.UpdateResponseData = new UpdateSubSeasonResponse();

                objBllAndFunction.DynamicData = new SubSeasonMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.TaxBLL") && (FunctionName == "SaveTax" || FunctionName == "UpdateTax" || FunctionName == "DeleteTax"))
            {
                // Changed by Senthamil @ 10.09.2018
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.TaxBLL";
                objBllAndFunction.SelectFunctionName = "SelectTaxRecord";
                //objBllAndFunction.SelectFunctionName = "SelectAllTaxRecords";
                objBllAndFunction.InsertFunctionName = "SaveTax";
                objBllAndFunction.UpdateFunctionName = "UpdateTax";
                objBllAndFunction.DeleteFunctionName = "DeleteTax";
                objBllAndFunction.SelectRequestType = new SelectByTaxIDRequest();
                //objBllAndFunction.SelectRequestType = new SelectAllTaxRequest();
                objBllAndFunction.InsertRequestData = new SaveTaxRequest();
                objBllAndFunction.UpdateRequestData = new UpdateTaxRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByTaxIDResponse();
                //objBllAndFunction.SelectBaseResponse = new SelectAllTaxResponse();
                objBllAndFunction.InsertResponseData = new SaveTaxResponse();                
                objBllAndFunction.UpdateResponseData = new UpdateTaxResponse();

                objBllAndFunction.DynamicData = new TaxMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.TillSettingsBLL") && (FunctionName == "SaveTillSettings" || FunctionName == "UpdateTillSettings" || FunctionName == "DeleteTillSettings"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.TillSettingsBLL";
                objBllAndFunction.SelectFunctionName = "SelectTillSettingsRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveTillSettings";
                objBllAndFunction.UpdateFunctionName = "UpdateTillSettings";
                objBllAndFunction.DeleteFunctionName = "DeleteTillSettings";
                objBllAndFunction.SelectRequestType = new SelectByIDTillSettingsRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDTillSettingsResponse();
                objBllAndFunction.InsertRequestData = new SaveTillSettingsRequest();
                objBllAndFunction.InsertResponseData = new SaveTillSettingsResponse();
                objBllAndFunction.UpdateRequestData = new UpdateTillSettingsRequest();
                objBllAndFunction.UpdateResponseData = new UpdateTillSettingsResponse();

                objBllAndFunction.DynamicData = new TillSettings();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.UsersBLL") && (FunctionName == "SaveUsers" || FunctionName == "UpdateUsers" || FunctionName == "DeleteUsers"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.UsersBLL";
                objBllAndFunction.SelectFunctionName = "SelectUserMaster";
                objBllAndFunction.InsertFunctionName = "SaveUsers";
                objBllAndFunction.UpdateFunctionName = "UpdateUsers";
                objBllAndFunction.DeleteFunctionName = "DeleteUsers";
                objBllAndFunction.SelectRequestType = new SelectByUsersIDRequest();
                objBllAndFunction.InsertRequestData = new SaveUsersRequest();
                objBllAndFunction.UpdateRequestData = new UpdateUsersRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByUsersIDResponse();
                objBllAndFunction.InsertResponseData = new SaveUsersResponse();
                objBllAndFunction.UpdateResponseData = new UpdateUsersResponse();

                objBllAndFunction.DynamicData = new UsersSettings();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.VendorGroupMasterBLL") && (FunctionName == "SaveVendorGroup" || FunctionName == "UpdateVendorGroup" || FunctionName == "DeleteVendorGroup"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.VendorGroupMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectVendorGroupRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveVendorGroup";
                objBllAndFunction.UpdateFunctionName = "UpdateVendorGroup";
                objBllAndFunction.DeleteFunctionName = "DeleteVendorGroup";
                objBllAndFunction.SelectRequestType = new SelectByVendorGroupIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByVendorGroupIDResponse();
                objBllAndFunction.InsertRequestData = new SaveVendorGroupRequest();
                objBllAndFunction.InsertResponseData = new SaveVendorGroupResponse();
                objBllAndFunction.UpdateRequestData = new UpdateVendorGroupRequest();
                objBllAndFunction.UpdateResponseData = new UpdateVendorGroupResponse();

                objBllAndFunction.DynamicData = new VendorGroupMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.VendorMasterBLL") && (FunctionName == "SaveVendor" || FunctionName == "UpdateVendor" || FunctionName == "DeleteVendor"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.VendorMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectVendorRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveVendor";
                objBllAndFunction.UpdateFunctionName = "UpdateVendor";
                objBllAndFunction.DeleteFunctionName = "DeleteVendor";
                objBllAndFunction.SelectRequestType = new SelectByVendorIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByVendorIDResponse();
                objBllAndFunction.InsertRequestData = new SaveVendorRequest();
                objBllAndFunction.InsertResponseData = new SaveVendorResponse();
                objBllAndFunction.UpdateRequestData = new UpdateVendorRequest();
                objBllAndFunction.UpdateResponseData = new UpdateVendorResponse();

                objBllAndFunction.DynamicData = new VendorMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.WarehouseMasterBLL") && (FunctionName == "SaveWarehouseMaster" || FunctionName == "UpdateWarehouseMaster" || FunctionName == "DeleteWarehouseMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.WarehouseMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectWarehouseMasterRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveWarehouseMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateWarehouseMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteWarehouseMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDWarehouseMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDWarehouseMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveWarehouseMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveWarehouseMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateWarehouseMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateWarehouseMasterResponse();

                objBllAndFunction.DynamicData = new WarehouseMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.WarehouseTypeMasterBLL") && (FunctionName == "SaveWarehouseTypeMaster" || FunctionName == "UpdateWarehouseTypeMaster" || FunctionName == "DeleteWarehouseTypeMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.WarehouseTypeMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectWarehouseTypeMasterRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveWarehouseTypeMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateWarehouseTypeMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteWarehouseTypeMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDWarehouseTypeMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDWarehouseTypeMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveWarehouseTypeMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveWarehouseTypeMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateWarehouseTypeMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateWarehouseTypeMasterResponse();

                objBllAndFunction.DynamicData = new WarehouseTypeMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Masters.YearBLL") && (FunctionName == "SaveYear" || FunctionName == "UpdateYear" || FunctionName == "DeleteYear"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.YearBLL";
                objBllAndFunction.SelectFunctionName = "SelectYearRecord";
                objBllAndFunction.InsertFunctionName = "SaveYear";
                objBllAndFunction.UpdateFunctionName = "UpdateYear";
                objBllAndFunction.DeleteFunctionName = "DeleteYear";
                objBllAndFunction.SelectRequestType = new SelectByYearIDRequest();
                objBllAndFunction.InsertRequestData = new SaveYearRequest();
                objBllAndFunction.UpdateRequestData = new UpdateYearRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByYearIDResponse();
                objBllAndFunction.InsertResponseData = new SaveYearResponse();
                objBllAndFunction.UpdateResponseData = new UpdateYearResponse();

                objBllAndFunction.DynamicData = new YearMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            //--------------------------------Transactions-------------------------------------------------------------------------------------// EasyBizBLL.Masters.CouponMasterBLL

            else if ((ClassName == "EasyBizBLL.Transactions.Coupens.CouponMasterBLL") && (FunctionName == "SaveCouponMaster" || FunctionName == "UpdateCouponMaster" || FunctionName == "DeleteCouponMaster"))
            //else if ((ClassName == "EasyBizBLL.Transactions.Coupens.CouponMasterBLL") && (FunctionName == "SaveCouponMaster" || FunctionName == "UpdateCouponMaster" || FunctionName == "DeleteCouponMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Coupens.CouponMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectCouponMasterRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveCouponMaster";
                objBllAndFunction.UpdateFunctionName = "UpdateCouponMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteCouponMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDCouponMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCouponMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveCouponMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveCouponMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCouponMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCouponMasterResponse();

                objBllAndFunction.DynamicData = new CouponMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Masters.CustomerSpecialPrice.CustomerSpecialPriceMasterBLL") && (FunctionName == "SaveCustomerSpecialPriceMaster" || FunctionName == "UpdateCustomerSpecialPriceMaster" || FunctionName == "DeleteCustomerSpecialPriceMaster"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Masters.CustomerSpecialPrice.CustomerSpecialPriceMasterBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDCustomerSpecialPriceMaster";
                objBllAndFunction.InsertFunctionName = "SaveCustomerSpecialPriceMaster";
                objBllAndFunction.UpdateFunctionName = "SaveCustomerSpecialPriceMaster";
                objBllAndFunction.DeleteFunctionName = "DeleteCustomerSpecialPriceMaster";
                objBllAndFunction.SelectRequestType = new SelectByIDCustomerSpecialPriceMasterRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCustomerSpecialPriceMasterResponse();
                objBllAndFunction.InsertRequestData = new SaveCustomerSpecialPriceMasterRequest();
                objBllAndFunction.InsertResponseData = new SaveCustomerSpecialPriceMasterResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCustomerSpecialPriceMasterRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCustomerSpecialPriceMasterResponse();

                objBllAndFunction.DynamicData = new CustomerSpecialPriceMasterTypes();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Transactions.POS.CouponDetailBLL") && (FunctionName == "SavePaymentCouponDetail" || FunctionName == "SelectCouponDetailByInvoiceNo" ))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.CouponDetailBLL";
                objBllAndFunction.SelectFunctionName = "SelectCouponDetailByInvoiceNo";
                objBllAndFunction.InsertFunctionName = "SavePaymentCouponDetail";              
                objBllAndFunction.SelectRequestType = new SelectCouponDetailByInvoiceNoRequest();
                objBllAndFunction.SelectBaseResponse = new SelectCouponDetailByInvoiceNoResponse();
                objBllAndFunction.InsertRequestData = new SaveCouponDetailRequest();
                objBllAndFunction.InsertResponseData = new SaveCouponDetailResponse();

                objBllAndFunction.DynamicData = new CouponDetail();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.DenominationBLL") && (FunctionName == "SaveDenomination" || FunctionName == "" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.DenominationBLL";
                objBllAndFunction.SelectFunctionName = "SelectCouponMasterRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveDenomination";
                objBllAndFunction.UpdateFunctionName = "";
                objBllAndFunction.DeleteFunctionName = "";
                objBllAndFunction.SelectRequestType = new SelectByIDDenominationRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDDenominationResponse();
                objBllAndFunction.InsertRequestData = new SaveDenominationRequest();
                objBllAndFunction.InsertResponseData = new SaveDenominationResponse();
                objBllAndFunction.UpdateRequestData = new UpdateDenominationRequest();
                objBllAndFunction.UpdateResponseData = new UpdateDenominationResponse();

                objBllAndFunction.DynamicData = new ReceivedDenomination();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.GiftvoucherDetailBLL") && (FunctionName == "SaveGiftvoucherDetails" || FunctionName == "" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.GiftvoucherDetailBLL";
                objBllAndFunction.SelectFunctionName = "SelectCouponMasterRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveGiftvoucherDetails";
                objBllAndFunction.UpdateFunctionName = "";
                objBllAndFunction.DeleteFunctionName = "";
                objBllAndFunction.InsertRequestData = new SaveGiftvoucherDetailsRequest();
                objBllAndFunction.InsertResponseData = new SaveGiftvoucherDetailsResponse();

                objBllAndFunction.DynamicData = new GiftvoucherDetail();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }          
   
            else if ((ClassName == "EasyBizBLL.Transactions.POS.InvoiceBLL") && (FunctionName == "SaveInvoice" || FunctionName == "UpdateInvoice" || FunctionName == "DeleteInvoice"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.InvoiceBLL";
                objBllAndFunction.SelectFunctionName = "SelectRecord";
                objBllAndFunction.InsertFunctionName = "SaveInvoice";
                objBllAndFunction.UpdateFunctionName = "UpdateInvoice";
                objBllAndFunction.DeleteFunctionName = "DeleteInvoice";
                objBllAndFunction.SelectRequestType = new SelectByIDInvoiceRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDInvoiceResponse(); 
                objBllAndFunction.InsertRequestData = new SaveInvoiceRequest();
                objBllAndFunction.InsertResponseData = new SaveInvoiceResponse();
                objBllAndFunction.UpdateRequestData = new UpdateInvoiceRequest();
                objBllAndFunction.UpdateResponseData = new UpdateInvoiceResponse();

                objBllAndFunction.DynamicData = new InvoiceHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
            }          
           
            else if ((ClassName == "EasyBizBLL.Transactions.POS.InvoiceCardDetailsBLL") && (FunctionName == "SaveInvoiceCardDetails" || FunctionName == "" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.InvoiceCardDetailsBLL";
                objBllAndFunction.SelectFunctionName = "";
                objBllAndFunction.InsertFunctionName = "SaveInvoiceCardDetails";
                //objBllAndFunction.UpdateFunctionName = "UpdateInvoice";
                //objBllAndFunction.DeleteFunctionName = "DeleteInvoice";
                objBllAndFunction.SelectRequestType = new SelectByIDCardDetailsRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDCardDetailsResponse();
                objBllAndFunction.InsertRequestData = new SaveCardDetailsRequest();
                objBllAndFunction.InsertResponseData = new SaveCardDetailsResponse();
                //objBllAndFunction.UpdateRequestData = new UpdateCardDetailsRequest();
                //objBllAndFunction.UpdateResponseData = new UpdateInvoiceResponse();

                objBllAndFunction.DynamicData = new InvoiceHeader();
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.InvoiceCashDetailsBLL") && (FunctionName == "SaveInvoiceCardDetails" || FunctionName == "" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.InvoiceCashDetailsBLL";
                objBllAndFunction.SelectFunctionName = "SelectCouponMasterRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveInvoiceCardDetails";
                objBllAndFunction.UpdateFunctionName = "";
                objBllAndFunction.DeleteFunctionName = "";
                objBllAndFunction.InsertRequestData = new SaveInVoiceCashDetailsRequest();
                objBllAndFunction.InsertResponseData = new SaveInvoiceCashDetailsResponse();

                objBllAndFunction.DynamicData = new InVoiceCashDetails();
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.PaymentTypeBLL") && (FunctionName == "SelectAllPaymentType"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.PaymentTypeBLL";
                objBllAndFunction.SelectFunctionName = "SelectAllPaymentType";
                objBllAndFunction.InsertRequestData = new SelectAllPaymentTypeRequest();
                objBllAndFunction.InsertResponseData = new SelectAllPaymentTypeResponse();

                objBllAndFunction.DynamicData = new PaymentType();
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.SalesExchangeBLL") && (FunctionName == "SaveSalesExchange"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.SalesExchangeBLL";
                objBllAndFunction.SelectFunctionName = "SelectSalesExchangeRecord";
                objBllAndFunction.InsertFunctionName = "SaveSalesExchange";
                objBllAndFunction.SelectRequestType = new SelectSalesExchangeRecordRequest();
                objBllAndFunction.SelectBaseResponse = new SelectSalesExchangeRecordResponse();
                objBllAndFunction.InsertRequestData = new SaveSalesExchangeRequest();
                objBllAndFunction.InsertResponseData = new SaveSalesExchangeResponse();
                objBllAndFunction.DynamicData = new SalesExchangeHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.SalesReturnBLL") && (FunctionName == "SaveSalesReturn"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.SalesReturnBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDInvoiceHeader"; 
                objBllAndFunction.InsertFunctionName = "SaveSalesReturn";
                objBllAndFunction.SelectRequestType = new SelectByIDSalesReturnRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByIDSalesReturnResponse();
                objBllAndFunction.InsertRequestData = new SaveSalesReturnRequest();
                objBllAndFunction.InsertResponseData = new SaveSalesReturnResponse();
                objBllAndFunction.DynamicData = new SalesReturnHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.TransactionStatusBLL") && (FunctionName == "SelectAllTransactionStatus"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.TransactionStatusBLL";
                objBllAndFunction.SelectFunctionName = "SelectByIDInvoiceHeader";
                objBllAndFunction.InsertFunctionName = "SelectAllTransactionStatus";
                objBllAndFunction.SelectRequestType = new SelectAllTransactionStatusRequest();
                objBllAndFunction.SelectBaseResponse = new SelectAllTransactionStatusResponse();

                objBllAndFunction.DynamicData = new TransactionStatusTypes();
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POSOperations.CashInCashOutBLL") && (FunctionName == "SaveCashInCashOut" || FunctionName == "UpdateCashInCashOut" || FunctionName == "DeleteCashInCashOut"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POSOperations.CashInCashOutBLL";
                objBllAndFunction.SelectFunctionName = "SelectCashInCashOutRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveCashInCashOut";
                objBllAndFunction.UpdateFunctionName = "UpdateCashInCashOut";
                objBllAndFunction.DeleteFunctionName = "DeleteCashInCashOut";
                objBllAndFunction.SelectRequestType = new SelectByCashInCashOutIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByCashInCashOutIDResponse();
                objBllAndFunction.InsertRequestData = new SaveCashInCashOutRequest();
                objBllAndFunction.InsertResponseData = new SaveCashInCashOutResponse();
                objBllAndFunction.UpdateRequestData = new UpdateCashInCashOutRequest();
                objBllAndFunction.UpdateResponseData = new UpdateCashInCashOutResponse();

                objBllAndFunction.DynamicData = new CashInCashOutMaster();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;



            }
            else if ((ClassName == "EasyBizBLL.Transactions.Pricing.PricePointBLL") && (FunctionName == "SavePricePointList" || FunctionName == "" || FunctionName == ""))
            {
				// Changed by Senthamil @ 11.09.2018
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Pricing.PricePointBLL";
				//objBllAndFunction.SelectFunctionName = "GetPricePointRecord"; // Need to update DAL
				objBllAndFunction.SelectFunctionName = "GetPricePointRecord"; 
                objBllAndFunction.InsertFunctionName = "SavePricePointList";
               // objBllAndFunction.UpdateFunctionName = "UpdateCashInCashOut";
                objBllAndFunction.DeleteFunctionName = "DeleteCashInCashOut";
				objBllAndFunction.SelectRequestType = new SelectPricePointByIDRequest();
				objBllAndFunction.SelectBaseResponse = new SelectPricePointByIDResponse();
                objBllAndFunction.InsertRequestData = new SavePricePointRequest();
                objBllAndFunction.InsertResponseData = new SavePricePointResponse();

                objBllAndFunction.DynamicData = new PricePoint();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.Promotions.PromotionPriorityBLL") && (FunctionName == "SavePromotionPriority"))
            {
				objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Promotions.PromotionPriorityBLL";
				//objBllAndFunction.ClassName = "EasyBizBLL.Masters.PromotionsMasterBLL";
				//objBllAndFunction.SelectFunctionName = "SelectByIDs"; // Need to Rename the classname
				objBllAndFunction.SelectFunctionName = "SelectAllPromotionsRecords"; // Need to Rename the classname
                objBllAndFunction.InsertFunctionName = "SavePromotionPriority";               
				//objBllAndFunction.SelectRequestType = new SelectByIDPromotionPriorityRequest();
				//objBllAndFunction.SelectBaseResponse = new SelectByIDPromotionPriorityResponse();
                objBllAndFunction.SelectRequestType = new SelectAllPromotionPriorityRequest();
                objBllAndFunction.SelectBaseResponse = new SelectAllPromotionPriorityResponse();
                objBllAndFunction.InsertRequestData = new SavePromotionPriorityRequest();
                objBllAndFunction.InsertResponseData = new SavePromotionPriorityResponse();               

                objBllAndFunction.DynamicData = new PromotionPriorityType();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }

            else if ((ClassName == "EasyBizBLL.Transactions.Promotions.WNPromotionBLL") && (FunctionName == "SaveWNPromotion" || FunctionName == "SelectWNPromotionRecord"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Promotions.WNPromotionBLL";
                objBllAndFunction.SelectFunctionName = "SelectWNPromotionRecord";
                objBllAndFunction.InsertFunctionName = "SaveWNPromotion";
                objBllAndFunction.SelectRequestType = new SelectWNPromotionByIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectWNPromotionByIDResponse();
                objBllAndFunction.InsertRequestData = new SaveWNPromotionRequest();
                objBllAndFunction.InsertResponseData = new SaveWNPromotionResponse();

                objBllAndFunction.DynamicData = new WNPromotion();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToAllStores;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.Stocks.InventoryCountingBLL") && (FunctionName == "SaveInventoryCounting" || FunctionName == "UpdateInventoryCounting" || FunctionName == "DeleteInventoryCounting"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Stocks.InventoryCountingBLL";
                objBllAndFunction.SelectFunctionName = "SelectInventoryCountingRecord";
                objBllAndFunction.InsertFunctionName = "SaveInventoryCounting";
                objBllAndFunction.UpdateFunctionName = "UpdateInventoryCounting";
                objBllAndFunction.DeleteFunctionName = "DeleteInventoryCounting";
                objBllAndFunction.SelectRequestType = new SelectByInventoryCountingIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByInventoryCountingIDResponse();
                objBllAndFunction.InsertRequestData = new SaveInventoryCountingRequest();
                objBllAndFunction.InsertResponseData = new SaveInventoryCountingResponse();
                objBllAndFunction.UpdateRequestData = new UpdateInventoryCountingRequest();
                objBllAndFunction.UpdateResponseData = new UpdateInventoryCountingResponse();

                objBllAndFunction.DynamicData = new InventoryCountingHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }                
          
            else if ((ClassName == "EasyBizBLL.Transactions.Stocks.StockAdjustmentBLL") && (FunctionName == "SaveStockAdjustment" || FunctionName == "" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Stocks.StockAdjustmentBLL";
                objBllAndFunction.SelectFunctionName = "SelectStockAdjustmentRecord"; // Need to update DAL SelectAll
                //objBllAndFunction.SelectFunctionName = "SelectAll"; // Need to update DAL 
                objBllAndFunction.InsertFunctionName = "SaveStockAdjustment";
                //objBllAndFunction.UpdateFunctionName = "UpdateInventoryCounting";
                objBllAndFunction.DeleteFunctionName = "DeleteInventoryCounting";
                objBllAndFunction.SelectRequestType = new SelectRecordStockAdjustmentRequest();
                objBllAndFunction.SelectBaseResponse = new SelectRecordStockAdjustmentResponse();
                objBllAndFunction.InsertRequestData = new SaveStockAdjustmentRequest();
                objBllAndFunction.InsertResponseData = new SaveStockAdjustmentResponse();             

                objBllAndFunction.DynamicData = new StockAdjustmentHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.Stocks.StockReceiptBLL") && (FunctionName == "SaveStockReceipt" || FunctionName == "UpdateStockReceipt" || FunctionName == "DeleteStockReceipt"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Stocks.StockReceiptBLL";
                objBllAndFunction.SelectFunctionName = "SelectStockReceiptRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveStockReceipt";
                objBllAndFunction.UpdateFunctionName = "UpdateStockReceipt";
                objBllAndFunction.DeleteFunctionName = "DeleteStockReceipt";
                objBllAndFunction.SelectRequestType = new SelectByStockReceiptIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByStockReceiptIDResponse();
                objBllAndFunction.InsertRequestData = new SaveStockReceiptRequest();
                objBllAndFunction.InsertResponseData = new SaveStockReceiptResponse();
                objBllAndFunction.UpdateRequestData = new UpdateStockReceiptRequest();
                objBllAndFunction.UpdateResponseData = new UpdateStockReceiptResponse();
               
                objBllAndFunction.DynamicData = new StockReceiptHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.Stocks.StockRequestBLL") && (FunctionName == "SaveStockRequest" || FunctionName == "UpdateStockRequest" || FunctionName == "DeleteStockRequest"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Stocks.StockRequestBLL";
                objBllAndFunction.SelectFunctionName = "SelectStockRequestRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveStockRequest";
                objBllAndFunction.UpdateFunctionName = "UpdateStockRequest";
                objBllAndFunction.DeleteFunctionName = "DeleteStockRequest";
                objBllAndFunction.SelectRequestType = new SelectByStockRequestIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByStockRequestIDResponse();
                objBllAndFunction.InsertRequestData = new SaveStockRequestRequest();
                objBllAndFunction.InsertResponseData = new SaveStockRequestResponse();
                objBllAndFunction.UpdateRequestData = new UpdateStockRequestRequest();
                objBllAndFunction.UpdateResponseData = new UpdateStockRequestResponse();
               
                objBllAndFunction.DynamicData = new StockRequestHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.Stocks.StockReturnBLL") && (FunctionName == "SaveStockReturn" || FunctionName == "UpdateStockReturn" || FunctionName == "DeleteStockReturn"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.Stocks.StockReturnBLL";
                objBllAndFunction.SelectFunctionName = "SelectStockReturnRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveStockReturn";
                objBllAndFunction.UpdateFunctionName = "UpdateStockReturn";
                objBllAndFunction.DeleteFunctionName = "DeleteStockReturn";
                objBllAndFunction.SelectRequestType = new SelectByStockReturnIDRequest();
                objBllAndFunction.SelectBaseResponse = new SelectByStockReturnIDResponse();
                objBllAndFunction.InsertRequestData = new SaveStockReturnRequest();
                objBllAndFunction.InsertResponseData = new SaveStockReturnResponse();
                objBllAndFunction.UpdateRequestData = new UpdateStockReturnRequest();
                objBllAndFunction.UpdateResponseData = new UpdateStockReturnResponse();

                objBllAndFunction.DynamicData = new StockReturnHeader();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }
            else if ((ClassName == "EasyBizBLL.Common.DayClosingBLL") && (FunctionName == "SaveDayClosing" || FunctionName == "UpdateDayClosing"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Common.DayClosingBLL";
                objBllAndFunction.SelectFunctionName = "SelectStockReturnRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveDayClosing";
                objBllAndFunction.UpdateFunctionName = "UpdateDayClosing";                       
                objBllAndFunction.InsertRequestData = new SaveDayClosingRequest();
                objBllAndFunction.InsertResponseData = new SaveDayClosingResponse();
                objBllAndFunction.UpdateRequestData = new SaveDayClosingRequest();
                objBllAndFunction.UpdateResponseData = new SaveDayClosingResponse();

                objBllAndFunction.DynamicData = new DayClosing();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }
            else if ((ClassName == "EasyBizBLL.Common.DayShiftLOGBLL") && (FunctionName == "SaveDayClosing" || FunctionName == "UpdateDayClosing"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Common.DayShiftLOGBLL";
                objBllAndFunction.SelectFunctionName = "SelectStockReturnRecord"; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveDayClosing";
                objBllAndFunction.UpdateFunctionName = "UpdateDayClosing";
                objBllAndFunction.InsertRequestData = new SaveShiftLOGRequest();
                objBllAndFunction.InsertResponseData = new SaveShiftLOGResponse();
                objBllAndFunction.UpdateRequestData = new SaveShiftLOGRequest();
                objBllAndFunction.UpdateResponseData = new SaveShiftLOGResponse();

                objBllAndFunction.DynamicData = new DayClosing();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
                
            }
            else if ((ClassName == "EasyBizBLL.Transactions.TransactionLogs.TransactionLogBLL") && (FunctionName == "SaveTransactionLog" || FunctionName == "" || FunctionName == ""))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.TransactionLogs.TransactionLogBLL";
                objBllAndFunction.SelectFunctionName = ""; // Need to update DAL
                objBllAndFunction.InsertFunctionName = "SaveTransactionLog";            
                objBllAndFunction.InsertRequestData = new SaveTransactionLogRequest();
                objBllAndFunction.InsertResponseData = new SaveTransactionLogResponse();
                objBllAndFunction.DynamicData = new TransactionLog();
                objBllAndFunction.SyncMode = Enums.SyncMode.StoreToEnterprise;
            }
            else if ((ClassName == "EasyBizBLL.Transactions.POS.OnAccountPaymentBLL") && (FunctionName == "SaveOnAccountPayment"))
            {
                objBllAndFunction.ClassName = "EasyBizBLL.Transactions.POS.OnAccountPaymentBLL";
                objBllAndFunction.SelectFunctionName = "GetOnAccountPaymentRecord";
                objBllAndFunction.InsertFunctionName = "SaveOnAccountPayment";
                objBllAndFunction.SelectRequestType = new SelectOnAccountPaymentRequest();
                objBllAndFunction.SelectBaseResponse = new SelectOnAccountPaymentResponse();
                objBllAndFunction.InsertRequestData = new SaveOnAccountPaymentRequest();
                objBllAndFunction.InsertResponseData = new SaveOnAccountPaymentResponse();
                objBllAndFunction.DynamicData = new OnAccountPayment();
                objBllAndFunction.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
            }

            return objBllAndFunction;
        }
    }
}
