﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosAPI.DTOs
{
    public class EnumsDTO
    {
        public enum OpStatusCode
        {
            Success = 1,
            GeneralError = 2,
            InvalidInput = 3,
            RecordNotFound = 4,
            BusinessRuleViolation = 5,
            UpdateRecordFailed = 6,
            DeleteRecordFailed = 7,
            CreateRecordFailed = 8,
            DatabaseConnectionError = 9,
            NoChangeToOriginalData = 10,
            UpdateFailedDueToNewerVersionInDB = 11,
            FileNotFound = 12,
            DuplicateRecordFound = 13,
            PassNoGenerationError = 14,
            RecordMismatch = 15,
            Other = 16,
            Reset = 17,
            QuantityMisMatched = 18,
            VersionNotUpdate = 19,
            LogInExist = 20,
            ServerNotResponding = 21,
            SyncFailed = 22
        }
        public enum ProcessMode
        {
            New = 1,
            Edit = 2,
            Delete = 3,
            BulkNew = 4,
            BulkEdit = 5,
            BulkDelete = 6,
            ViewMode = 7,
            TransactionInprogress = 8,
            TransactionDone = 9
        }
        public enum PaymentProcessMode
        {
            PaymentNew = 1,
            PaymentEdit = 2,
            PaymentCancel = 3,
            PaymentInprogress = 4,
            PaymentDone = 5
        }
        public enum StoreToEnterpriseDocumentTypes
        {
            STOCKREQUEST = 1,
            STOCKRECEIPT = 2,
            STOCKRETURN = 3,
            INVENTORYCOUNTING = 4,
            STOCKADJUSTMENT = 5,
            SALES = 6,
            SALESRETURN = 7,
            SALESEXCHANGE = 8,
            SALESHOLD = 9,
            PAYMENTS = 10,
            DAYCLOSING = 11
        }
        public enum EnterpriseToOtherStoresDocumentType
        {
            CASHINCASHOUT = 1,
            EXPENSEMASTER = 2,
            STOCKREQUEST = 3,
            STOCKRECEIPT = 4,
            STOCKRETURN = 5,
            INVENTORYCOUNTING = 6,
            STOCKADJUSTMENT = 7,
            SALES = 8,
            SALESRETURN = 9,
            SALESEXCHANGE = 10,
            SALESHOLD = 11,
            PAYMENTS = 12,
            DAYCLOSING = 13
        }
        public enum EnterpriseToAllStoreDocumentType
        {
            COUNTRY = 1,
            MANAGEROVERRIDE = 2,
            BRANDDIVISIONMAP = 3,
            STATE = 4,
            LANGUAGESETTINGS = 5,
            COMPANYSETTINGS = 6,
            WAREHOUSETYPES = 7,
            WAREHOUSE = 8,
            DOCUMENTNUMBERING = 9,
            DESIGNATION = 10,
            EMPLOYEES = 11,
            VENDORGROUP = 12,
            VENDOR = 13,
            CUSTOMERGROUP = 14,
            CUSTOMERMASTER = 15,
            RETAILSETTINGS = 16,
            STYLESTATUS = 17,
            SEGAMENTATIONTYPES = 18,
            STYLESEGMENTATION = 19,
            DROPMASTER = 20,
            PRICETYPEMASTER = 21,
            TAXMASTER = 22,
            COLLECTIONMASTER = 23,
            SUBCOLLECTIONMASTER = 24,
            AGENTMASTER = 25,
            ROLE = 26,
            LOGINUSERS = 27,
            PREVILEGE = 28,
            CURRENCY = 29,
            EXCHANGERATE = 30,
            PAYMENTTYPE = 31,
            STOREGROUP = 32,
            STORE = 33,
            POS = 34,
            SHIFT = 35,
            BRAND = 36,
            SUBBRAND = 37,
            SCALEMASTER = 38,
            COLORMASTER = 39,
            PRODUCTLINE = 40,
            PRODUCTGROUP = 41,
            PRODUCTSUBGROUP = 42,
            SEASON = 43,
            DESIGNMASTER = 44,
            STYLEMASTER = 45,
            IMPORTSTYLEPRICING = 46,
            SKUMASTER = 47,
            BARCODESETUP = 48,
            DIVISION = 49,
            YEAR = 50,
            REASON = 51,
            PRICELIST = 52,
            PRICEPOINT = 53,
            CUSTOMERSPECIALPRICE = 54,
            PROMOTIONS = 55,
            PROMOTIONSPRIORITY = 56,
            WNPROMOTION = 57,
            COUPON = 58
        }
        public enum DocumentType
        {
            TRANSACTIONLOG = 0,
            COUNTRY = 1,
            MANAGEROVERRIDE = 2,
            BRANDDIVISIONMAP = 3,
            STATE = 4,
            LANGUAGESETTINGS = 5,
            COMPANYSETTINGS = 6,
            WAREHOUSETYPES = 7,
            WAREHOUSE = 8,
            DOCUMENTNUMBERING = 9,
            DESIGNATION = 10,
            EMPLOYEES = 11,
            VENDORGROUP = 12,
            VENDOR = 13,
            CUSTOMERGROUP = 14,
            CUSTOMERMASTER = 15,
            RETAILSETTINGS = 16,
            STYLESTATUS = 17,
            SEGAMENTATIONTYPES = 18,
            STYLESEGMENTATION = 19,
            DROPMASTER = 20,
            PRICETYPEMASTER = 21,
            TAXMASTER = 22,
            COLLECTIONMASTER = 23,
            SUBCOLLECTIONMASTER = 24,
            AGENTMASTER = 25,
            ROLE = 26,
            LOGINUSERS = 27,
            PREVILEGE = 28,
            CURRENCY = 29,
            EXCHANGERATE = 30,
            PAYMENTTYPE = 31,
            STOREGROUP = 32,
            STORE = 33,
            POS = 34,
            SHIFT = 35,
            BRAND = 36,
            SUBBRAND = 37,
            SCALEMASTER = 38,
            COLORMASTER = 39,
            PRODUCTLINE = 40,
            PRODUCTGROUP = 41,
            PRODUCTSUBGROUP = 42,
            SEASON = 43,
            DESIGNMASTER = 44,
            STYLEMASTER = 45,
            IMPORTSTYLEPRICING = 46,
            SKUMASTER = 47,
            BARCODESETUP = 48,
            DIVISION = 49,
            YEAR = 50,
            REASON = 51,
            PRICELIST = 52,
            PRICEPOINT = 53,
            CUSTOMERSPECIALPRICE = 54,
            PROMOTIONS = 55,
            PROMOTIONSPRIORITY = 56,
            WNPROMOTION = 57,
            COUPON = 58,
            CASHINCASHOUT = 59,
            EXPENSEMASTER = 60,
            STOCKREQUEST = 61,
            STOCKRECEIPT = 62,
            STOCKRETURN = 63,
            INVENTORYCOUNTING = 64,
            STOCKADJUSTMENT = 65,
            SALES = 66,
            SALESRETURN = 67,
            SALESEXCHANGE = 68,
            SALESHOLD = 69,
            PAYMENTS = 70,
            DAYCLOSING = 71,
            TAILORINGMASTER = 72,
            DISCOUNTMASTER = 73,
            ONACCOUNTPAYMENT = 74,
            USERPREVILEGE = 75,
            PROMOTIONMAPPING = 76,
            TAILORINGORDER = 77,
            PRICECHANGE = 78,
            FRANCHISEMASTER = 79,
            OPENINGSTOCK = 80,
            USERREPORTS = 81,
            SALESORDER = 82,
            SALESTARGET = 83,
            BASEDATAMANUALSYNC = 84,
            TRANSACTIONMANUALSYNC = 85,
            COUPONTRANSFER = 86,
            DAYWISETRANSACTIONREPORT = 87,
            LABELPRINTING = 88,
            SALESLABELPRINTING = 89,
            SALESREPORT = 90,
            DAYWISEACTIVITY = 91,
            DETAILEDSALESINVOICE = 92,
            DETAILEDSALESRETURNINVOICE = 93,
            SALESMANWISEREPORT = 94,
            CASHIERWISEREPORT = 95,
            DETAILEDSHOWROOMSALESREPORT = 96,
            DAYWISEACTIVITIESREPORT = 97,
            STYLESUMMARY = 98,
            STYLELEDGER = 99,
            PRICECHANGELOGREPORT = 100,
            REGISTERDASHBOARD = 101,
            CREATEDASHBOARD = 102,
            DASHBOARDVIEWER = 103,
            XREPORT = 104,
            ZREPORT = 105,
            COUPONRECEIPT = 106,
            COUPONTRANSACTION = 107,
            PATCHFORM = 108,
            STOCKFREEZE = 109,
            NONTRADINGITEMDISTRIBUTION = 110,
            CITY = 111
        }
        public enum DocumentType2
        {
            PURCHASEREQUEST = 1,
            PURCHASERETURN = 2,
            PURCHASEINVOICE = 3,
            SALES = 4,
            SALESRETURN = 5,
            STOCKREQUEST = 6,
            STOCKRECEIPT = 7,
            STOCKRETURN = 8,
            STOCKADJUSTMENT = 9,
            // STOCKIN = 10,
            // STOCKOUT = 11,
            //STOCKTRANSFER = 12,
            STYLE = 13,
            INVENTORYCOUNTING = 15,
            COUNTRY = 16,
            STATE = 17,
            LANGUAGESETTINGS = 18,
            COMPANYSETTINGS = 19,
            WAREHOUSETYPES = 20,
            WAREHOUSE = 21,
            DOCUMENTNUMBERING = 22,
            DOCUMENTTYPE = 23,
            DATAIMPORT = 24,
            DESIGNATION = 25,
            EMPLOYEES = 26,
            VENDORGROUP = 27,
            VENDOR = 28,
            CUSTOMERGROUP = 29,
            CUSTOMERMASTER = 30,
            RETAILSETTINGS = 31,
            STYLESTATUS = 32,
            STYLESEGMENTATION = 33,
            DROPMASTER = 34,
            PRICETYPEMASTER = 35,
            TAXMASTER = 36,
            COLLECTIONMASTER = 37,
            SUBCOLLECTIONMASTER = 38,
            AGENTMASTER = 39,
            SEGAMENTATIONTYPES = 40,
            CURRENCY = 41,
            EXCHANGERATE = 42,
            PAYMENTTYPE = 43,
            STOREGROUP = 44,
            STORE = 45,
            STORECAPACITY = 46,
            TILL = 47,
            POS = 48,
            //EXCHANGERATE = 49,
            LOGINUSERS = 50,
            BRAND = 51,
            SUBBRAND = 52,
            CATEGORY = 53,
            SCALEMASTER = 54,
            SIZERUNSCALE = 55,
            COLORMASTER = 56,
            PRODUCTLINE = 57,
            PRODUCTGROUP = 58,
            PRODUCTSUBGROUP = 59,
            SEASON = 60,
            SUBSEASON = 61,
            DESIGNMASTER = 62,
            STYLEMASTER = 63,
            SKUMASTER = 64,
            BARCODE = 65,
            DIVISION = 66,
            YEAR = 67,
            REASON = 68,
            PRICELIST = 69,
            PRICEPOINT = 70,
            //SPECIALPRICES = 71,
            PROMOTIONS = 72,
            PROMOTIONSPRIORITY = 73,
            FREIGHT = 74,
            ORDERTYPE = 75,
            REQUESTTYPE = 76,
            COUPON = 77,
            BARCODESETUP = 78,
            ALLOCATIONTYPE = 79,
            //PURCHASEREQUEST = 80,
            ACTIVITYMASTER = 81,
            REQUESTACTIVITYTRACKING = 82,
            PURCHASEORDERGENERATION = 83,
            COUNTRYWISEALLOCATION = 84,
            DELIVERYORDER = 85,
            QUALITYCONTROL = 86,
            GOODSRECEIPTS = 87,
            PICKANDPACKMANAGERS = 88,
            //SHIPING = 89,
            ALLOCATIONGROUPSETUP = 90,
            REPLANISHMENTPROCESS = 91,
            EXPENSEMASTER = 92,
            SALESEXCHANGE = 93,
            SALESHOLD = 94,
            CARDPAYMENT = 95,
            //CASHPAYMENT =96,
            ROLE = 97,
            MANAGEROVERRIDE = 98,
            AFSEGMENTATION = 99,
            BRANDDIVISIONMAP = 100,
            SHIFT = 101,
            REGISTERDASHBOARD = 102,
            IMPORTSTYLEPRICING = 103,
            CUSTOMERSPECIALPRICE = 104,
            COUPONDETAIL = 105,
            DENOMINATION = 106,
            GIFTVOUCHERDETAIL = 107,
            CASHINCASHOUT = 108,
            WNPROMOTION = 109,
            TRANSACTIONLOG = 110,
            CashIn = 111,
            CashOut = 112,
            PREVILEGE = 114,
            DAILYSALESREPORT = 115,
            DAYWISEACTIVITY = 116,
            ENTERPRISETOSTORE = 117,
            FAILEDDATASYNC = 118,
            DATACOMPARE = 119,
            CREATEDASHBOARD = 120,
            DASHBOARDVIEWER = 121,
            STORETOSERVER = 122,
            PAYMENTS = 123
        }
        public enum RequestFrom
        {
            MainServer = 1,
            CountryServer = 2,
            StoreServer = 3,
            StoreSales = 4,
            Upload = 5,
            Search = 6,
            DefaultLoad = 7,
            SyncService = 8

        }
        public enum SyncTypes
        {
            MainServer = 1,
            CountryServer = 2,
            StoreServer = 3,
            StorePOS = 4
        }
        public enum SyncMode
        {
            EnterpriseToAllStores = 1,
            EnterpriseToSpecificStores = 2,
            EnterpriseToBrandWiseStores = 3,
            EnterpriseToStore = 4,
            StoreToEnterprise = 5
        }
        public enum PromotionRecordType
        {
            Store = 1,
            Customer = 2,
            Category = 3,
            GetItem = 4,
            BuyItem = 5
        }

        public enum SpecialPriceRecordType
        {
            Store = 1,
            Customer = 2,
            Category = 3,
        }
        public enum CategoryTypes
        {
            All = 0,
            StyleSegmentation = 1,
            Year = 2,
            Brand = 3, //Don't change the Value , If change then StoreDetailsTypes Brand Enum value too.It must be same
            SubBrand = 4,
            Seasons = 5,
            ProductGroup = 6,
            ProductSubGroup = 7,
            Style = 8,
            Coupon = 9
        }
        public enum CustomerDetailsTypes
        {
            CustomerGroup = 1,
            Customer = 2
        }
        public enum StoreDetailsTypes
        {
            StoreGroup = 1,
            Store = 2,
            Brand = 3, // CategoryTypes.Brand = StoreDetailsTypes.Brand
            Country = 4
        }
        public enum InvoiceStatus
        {
            New = 0,
            Completed = 1,
            VoidSale = 2,
            ParkSale = 3,
            SalesExchange = 4,
            SalesReturn = 5,
            Resale = 6
        }
        public enum PrintableDocument
        {
            SalesInvoice = 1,
            SalesExchange = 2,
            SalesReturn = 3,
            SalesHold = 4,
            InvoiceSearch = 5,
            ZReport = 6,
            XReport = 7,
            DuplicateReceipt = 8
        }
        public static string GetDocumentName(int enumValue)
        {
            try
            {
                return Enum.GetName(typeof(DocumentType), enumValue);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string GetProcessModeName(int enumValue)
        {
            try
            {
                return Enum.GetName(typeof(ProcessMode), enumValue);
            }
            catch
            {
                return string.Empty;
            }
        }
        public enum Enum_Order_Status
        {
            OPEN = 1,
            INPROGRESS = 2,
            CLOSED = 3,
            CANCELLED = 4,
            READ = 5,
            REJECTED = 6,
            NOSTOCK = 7
        }
    }
}