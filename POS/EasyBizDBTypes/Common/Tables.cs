using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Common
{
    [DataContract]
    [Serializable]
    public class Tables
    {
        public List<string> GetTables(Enums.DocumentType DocumentType)
        {
            var TableList = new List<string>();
            if (DocumentType == Enums.DocumentType.COUNTRY)
            {
                TableList.Add("CountryMaster");   
            }
            else if (DocumentType == Enums.DocumentType.MANAGEROVERRIDE)
            {
                TableList.Add("ManagerOverride");
            }
            else if (DocumentType == Enums.DocumentType.BRANDDIVISIONMAP)
            {
                TableList.Add("BrandandDivisionMapping");
            }
            else if (DocumentType == Enums.DocumentType.STATE)
            {
                TableList.Add("StateMaster");
            }
            else if (DocumentType == Enums.DocumentType.LANGUAGESETTINGS)
            {
                TableList.Add("LanguageMaster");
            }
            else if (DocumentType == Enums.DocumentType.COMPANYSETTINGS)
            {
                TableList.Add("CompanySettings");
            }
            if (DocumentType == Enums.DocumentType.WAREHOUSETYPES)
            {
                TableList.Add("WarehouseTypeMaster");
            }
            if (DocumentType == Enums.DocumentType.WAREHOUSE)
            {
                TableList.Add("WarehouseMaster");
            }
            else if (DocumentType == Enums.DocumentType.DOCUMENTNUMBERING)
            {
                TableList.Add("DocumentNumberingMaster");
                TableList.Add("DocumentNumberingDetails");
            }
            //else if (DocumentType == Enums.DocumentType.DOCUMENTTYPE)
            //{
            //    TableList.Add("DocumentType");
            //}
           
            else if (DocumentType == Enums.DocumentType.DESIGNATION)
            {
                TableList.Add("DesignationMaster");
            }
            else if (DocumentType == Enums.DocumentType.EMPLOYEES)
            {
                TableList.Add("EmployeeMaster");
            }
            else if (DocumentType == Enums.DocumentType.VENDORGROUP)
            {
                TableList.Add("VendorGroupMaster");
            }
            else if (DocumentType == Enums.DocumentType.VENDOR)
            {
                TableList.Add("VendorMaster");
            }
            else if (DocumentType == Enums.DocumentType.CUSTOMERGROUP)
            {
                TableList.Add("CustomerGroupMaster");
            }
            else if (DocumentType == Enums.DocumentType.CUSTOMERMASTER)
            {
                TableList.Add("CustomerMaster");
            }
            else if (DocumentType == Enums.DocumentType.RETAILSETTINGS)
            {
                TableList.Add("RetailSettings");
            }
            else if (DocumentType == Enums.DocumentType.STYLESTATUS)
            {
                TableList.Add("StyleStatusMaster");
            }
            else if (DocumentType == Enums.DocumentType.SEGAMENTATIONTYPES)
            {
                TableList.Add("SegmentationMaster");
            }
            else if (DocumentType == Enums.DocumentType.STYLESEGMENTATION)
            {
                TableList.Add("AFSegamationMaster");
                TableList.Add("AFSegamationDetails");
            }
            else if (DocumentType == Enums.DocumentType.DROPMASTER)
            {
                TableList.Add("DropMaster");
            }
            else if (DocumentType == Enums.DocumentType.PRICETYPEMASTER)
            {
                TableList.Add("PriceTypeMaster");
            }
            else if (DocumentType == Enums.DocumentType.TAXMASTER)
            {
                TableList.Add("TaxMaster");
            }
            else if (DocumentType == Enums.DocumentType.COLLECTIONMASTER)
            {
                TableList.Add("CollectionMaster");
            }
            else if (DocumentType == Enums.DocumentType.SUBCOLLECTIONMASTER)
            {
                TableList.Add("SubCollectionMaster");
            }
            else if (DocumentType == Enums.DocumentType.AGENTMASTER)
            {
                TableList.Add("AgentMaster");
            }
            else if (DocumentType == Enums.DocumentType.ROLE)
            {
                TableList.Add("RoleMaster");
            }
            else if (DocumentType == Enums.DocumentType.LOGINUSERS)
            {
                TableList.Add("UserMaster");
            }
            else if (DocumentType == Enums.DocumentType.PREVILEGE)
            {
                TableList.Add("UserPrivilages");
            }
            else if (DocumentType == Enums.DocumentType.CURRENCY)
            {
                TableList.Add("CurrencyMaster");
                TableList.Add("CurrencyMasterDetails");
            }
            else if (DocumentType == Enums.DocumentType.EXCHANGERATE)
            {
                TableList.Add("ExchangeRates");
            }
            else if (DocumentType == Enums.DocumentType.PAYMENTTYPE)
            {
                TableList.Add("PaymentTypeMaster");
            }
            else if (DocumentType == Enums.DocumentType.CASHINCASHOUT)
            {
                TableList.Add("CashInCashOutHeader");
                TableList.Add("CashInCashOutDetails");
            }
            else if (DocumentType == Enums.DocumentType.STOREGROUP)
            {
                TableList.Add("StoreGroupMaster");
                TableList.Add("StoreGroupDetails");
            }
            else if (DocumentType == Enums.DocumentType.STORE)
            {
                TableList.Add("StoreMaster");
            }
            else if (DocumentType == Enums.DocumentType.BRAND)
            {
                TableList.Add("BrandMaster");
            }
            else if (DocumentType == Enums.DocumentType.SUBBRAND)
            {
                TableList.Add("SubBrandMaster");
            }            
            else if (DocumentType == Enums.DocumentType.SCALEMASTER)
            {
                TableList.Add("ScaleMaster");
                TableList.Add("ScaleMasterDetails");
                TableList.Add("ScaleMasterBrandDetails");
            }            
            else if (DocumentType == Enums.DocumentType.COLORMASTER)
            {
                TableList.Add("ColorMaster");
            }
            else if (DocumentType == Enums.DocumentType.PRODUCTLINE)
            {
                TableList.Add("ProductLineMaster");
            }
            else if (DocumentType == Enums.DocumentType.PRODUCTGROUP)
            {
                TableList.Add("ProductGroupMaster");
            }
            else if (DocumentType == Enums.DocumentType.PRODUCTSUBGROUP)
            {
                TableList.Add("ProductSubGroupMaster");
            }
            else if (DocumentType == Enums.DocumentType.SEASON)
            {
                TableList.Add("SeasonMaster");
            }
            //else if (DocumentType == Enums.DocumentType.SUBSEASON)
            //{
            //    TableList.Add("SubSeasonMaster");
            //}
            else if (DocumentType == Enums.DocumentType.DESIGNMASTER)
            {
                TableList.Add("DesignMaster");
                TableList.Add("DesignWithItemImage");
            }
            else if (DocumentType == Enums.DocumentType.STYLEMASTER)
            {
                TableList.Add("StyleMaster");
                TableList.Add("StylePricing");
                TableList.Add("StyleWithColorDetails");
                TableList.Add("StyleWithItemDetailsMaster");
                TableList.Add("StyleWithItemImage");
                TableList.Add("StyleWithScaleDetails");
            }
            else if (DocumentType == Enums.DocumentType.SKUMASTER)
            {
                TableList.Add("SKUMaster");
                TableList.Add("SKUImages");
            }
            else if (DocumentType == Enums.DocumentType.BARCODESETUP)
            {
                TableList.Add("BarcodeSettings");
            }
            else if (DocumentType == Enums.DocumentType.DIVISION)
            {
                TableList.Add("DivisionMaster");
            }
            else if (DocumentType == Enums.DocumentType.YEAR)
            {
                TableList.Add("YearMaster");
            }
            else if (DocumentType == Enums.DocumentType.REASON)
            {
                TableList.Add("ReasonMaster");
            }
            else if (DocumentType == Enums.DocumentType.PRICELIST)
            {
                TableList.Add("PriceListMaster");
            }
            else if (DocumentType == Enums.DocumentType.PRICEPOINT)
            {
                TableList.Add("PricePoint");
            }
            else if (DocumentType == Enums.DocumentType.CUSTOMERSPECIALPRICE)
            {
                TableList.Add("CustomerSpecialPriceMaster");
                TableList.Add("CustomerSpecialPriceStoreDetails");
                TableList.Add("CustomerSpecialPriceCustomerDetails");
                TableList.Add("CustomerSpecialPriceCategoryDetails");
            }
            else if (DocumentType == Enums.DocumentType.PROMOTIONS)
            {
                TableList.Add("PromotionsMaster");
                TableList.Add("PromotionsWithCustomerDetails");
                TableList.Add("PromotionsWithProducts");
                TableList.Add("PromotionsWithStoreDetails");
                TableList.Add("PromotionWithBuyItem");
                TableList.Add("PromotionWithGetItemDetails");
            }
            else if (DocumentType == Enums.DocumentType.PROMOTIONSPRIORITY)
            {
                TableList.Add("PromotionPriorityHeader");
                TableList.Add("PromotionPriority");
            }
            else if (DocumentType == Enums.DocumentType.WNPROMOTION)
            {
                TableList.Add("WNPromotion");
                TableList.Add("WNPromotionDetails");
            }
            //else if (DocumentType == Enums.DocumentType.FREIGHT)
            //{
            //    TableList.Add("FreightMaster");
            //}
            //else if (DocumentType == Enums.DocumentType.ORDERTYPE)
            //{
            //    TableList.Add("OrderTypeMaster");
            //}
            //else if (DocumentType == Enums.DocumentType.REQUESTTYPE)
            //{
            //    TableList.Add("RequestTypeMaster");
            //}
            else if (DocumentType == Enums.DocumentType.COUPON)
            {
                TableList.Add("CouponMaster");
                TableList.Add("CouponDetails");
                TableList.Add("CouponListDetails");
                TableList.Add("CouponCustomerDetails");
                TableList.Add("CouponStoreMasterDetails");
            }
            //else if (DocumentType == Enums.DocumentType.ALLOCATIONTYPE)
            //{
            //    TableList.Add("AllocationTypeMaster");
            //}            
            //else if (DocumentType == Enums.DocumentType.REGISTERDASHBOARD)
            //{
            //    TableList.Add("DashboardReport");
            //}            
            else if (DocumentType == Enums.DocumentType.SHIFT)
            {
                TableList.Add("ShiftMaster");
            }
            else if (DocumentType == Enums.DocumentType.EXPENSEMASTER)
            {
                TableList.Add("ExpenseMaster");
            }
            //else if (DocumentType == Enums.DocumentType.TILL)
            //{
            //    TableList.Add("TillSettings");
            //}
            else if (DocumentType == Enums.DocumentType.POS)
            {
                TableList.Add("PosMaster");
            }            
            else if (DocumentType == Enums.DocumentType.STOCKREQUEST)
            {
                TableList.Add("StockRequestHeader");
                TableList.Add("StockRequestDetails");
            }
            else if (DocumentType == Enums.DocumentType.STOCKRECEIPT)
            {
                TableList.Add("StockReceiptHeader");
                TableList.Add("StockReceiptDetails");
            }
            else if (DocumentType == Enums.DocumentType.STOCKRETURN)
            {
                TableList.Add("StockReturnHeader");
                TableList.Add("StockReturnDetails");
            }
            else if (DocumentType == Enums.DocumentType.INVENTORYCOUNTING)
            {
                TableList.Add("InventoryCountingHeader");
                TableList.Add("InventoryCountingDetails");
            }
            else if (DocumentType == Enums.DocumentType.STOCKADJUSTMENT)
            {
                TableList.Add("StockAdjustmentHeader");
                TableList.Add("StockAdjustmentDetails");
            }            
            else if (DocumentType == Enums.DocumentType.SALES)
            {
                TableList.Add("InvoiceHeader");
                TableList.Add("InvoiceDetail");
                TableList.Add("InVoiceCashDetails");
                TableList.Add("InvoiceCardDetails");
            }
            else if (DocumentType == Enums.DocumentType.SALESRETURN)
            {
                TableList.Add("SalesReturnHeader");
                TableList.Add("SalesReturnDetail");
            }
            else if (DocumentType == Enums.DocumentType.SALESEXCHANGE)
            {
                TableList.Add("SalesExchangeHeader");
                TableList.Add("SalesExchangeDetail");
            }
            else if (DocumentType == Enums.DocumentType.SALESHOLD)
            {
                TableList.Add("InvoiceHeader");
                TableList.Add("InvoiceDetail");
            }
            else if (DocumentType == Enums.DocumentType.PAYMENTS)
            {
                TableList.Add("InvoiceCardDetails");
                TableList.Add("InVoiceCashDetails");
            }
            //else if (DocumentType == Enums.DocumentType.DENOMINATION)
            //{
            //    TableList.Add("ReceivedDenominations");
            //}
            //else if (DocumentType == Enums.DocumentType.GIFTVOUCHERDETAIL)
            //{
            //    TableList.Add("GifvoucherPayment");
            //}
            else if (DocumentType == Enums.DocumentType.IMPORTSTYLEPRICING)
            {
                TableList.Add("StylePricing");
            }
            else if (DocumentType == Enums.DocumentType.DAYCLOSING)
            {
                TableList.Add("ShiftLOG");
            }
            //else if (DocumentType == Enums.DocumentType.TRANSACTIONLOG)
            //{
            //    TableList.Add("TransactionLog");
            //}
            //if (DocumentType == Enums.DocumentType.ACTIVITYMASTER)
            //{
                
            //}
            //else if (DocumentType == Enums.DocumentType.AFSEGMENTATION)
            //{
            //    TableList.Add("AFSegamationMaster");
            //    TableList.Add("AFSegamationDetails");
            //}
            
            //else if (DocumentType == Enums.DocumentType.ALLOCATIONGROUPSETUP)
            //{

            //}
            
            //else if (DocumentType == Enums.DocumentType.BARCODE)
            //{
                        
            //}                            
            //else if (DocumentType == Enums.DocumentType.COUNTRYWISEALLOCATION)
            //{
              
            //}           
            //else if (DocumentType == Enums.DocumentType.DELIVERYORDER)
            //{

            //}
           
            //else if (DocumentType == Enums.DocumentType.GOODSRECEIPTS)
            //{
                
            //}
            
                                             
            //else if (DocumentType == Enums.DocumentType.PICKANDPACKMANAGERS)
            //{

            //}
                    
            //else if (DocumentType == Enums.DocumentType.PURCHASEINVOICE)
            //{

            //}
            //else if (DocumentType == Enums.DocumentType.PURCHASEORDERGENERATION)
            //{

            //}
            //else if (DocumentType == Enums.DocumentType.PURCHASEREQUEST)
            //{

            //}
            //else if (DocumentType == Enums.DocumentType.PURCHASERETURN)
            //{

            //}
            //else if (DocumentType == Enums.DocumentType.QUALITYCONTROL)
            //{

            //}                    
            //else if (DocumentType == Enums.DocumentType.REPLANISHMENTPROCESS)
            //{

            //}
            //else if (DocumentType == Enums.DocumentType.REQUESTACTIVITYTRACKING)
            //{

            //}     
            return TableList;
        }
    }  
    
}
