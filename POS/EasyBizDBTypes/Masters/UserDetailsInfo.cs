using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{
    [DataContract]
    [Serializable]
    public class UserDetailsInfo : BaseType
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string UserCode { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string POSCode { get; set; }
        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }

        [DataMember]
        public int RoleID { get; set; }

        [DataMember]
        public int PriceListID { get; set; }
        [DataMember]
        public string PriceListCode { get; set; }
        [DataMember]
        public string PriceListName { get; set; }
        [DataMember]
        public int CountryID { get; set; }


        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int POSID { get; set; }


        [DataMember]
        public int CurrencyID { get; set; }


        [DataMember]
        public string StoreName { get; set; }

        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string StoreGroupName { get; set; }
        [DataMember]
        public string BrandID { get; set; }
        [DataMember]
        public int StoreGroupID { get; set; }
        [DataMember]
        public bool AllowNewRow { get; set; }
        [DataMember]
        public string EmployeeImage { get; set; }
        [DataMember]
        public string PrinterDeviceName { get; set; }
        [DataMember]
        public string PosName { get; set; }
        [DataMember]
        public string CurrencySymbol { get; set; }
        [DataMember]
        public string BaseCurrency { get; set; }

        [DataMember]
        public int ManagerOverrideID { get; set; }

        [DataMember]
        public int RetailID { get; set; }
        [DataMember]
        public string ManagerOverrideName { get; set; }
        [DataMember]
        public string RetailName { get; set; }
        [DataMember]
        public int DecimalPlaces { get; set; }
        [DataMember]
        public Decimal NearByRoundOff { get; set; }
        [DataMember]
        public bool PasswordReset { get; set; }
        [DataMember]
        public string CurrentPassword { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
        [DataMember]
        public bool IsLoggedIn { get; set; }
        [DataMember]
        public string PoleDisplayPort { get; set; }
        [DataMember]
        public string DisplayLineMsgOne { get; set; }
        [DataMember]
        public string DisplayLineMsgTwo { get; set; }

        [DataMember]
        public int PrintCount { get; set; }
        [DataMember]
        public int ExchangePrintCount { get; set; }
        [DataMember]
        public int SaleReturnPrintCount { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string ToMailID { get; set; }
        [DataMember]
        public string CCMailID { get; set; }

        [DataMember]
        public string ManagerOverrideCode { get; set; }
        [DataMember]
        public string RetailSettingCode { get; set; }
        [DataMember]
        public string RoleCode { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string POSTitle { get; set; }
        [DataMember]
        public bool AllowStockEdit { get; set; }
        [DataMember]
        public bool MobileUser { get; set; }
        [DataMember]
        public Decimal PromotionRoundOff { get; set; }
        [DataMember]
        public int TaxID { get; set; }
        [DataMember]
        public string TaxCode { get; set; }
        [DataMember]
        public string TaxPercentage { get; set; }
        [DataMember]
        public string StoreFooter { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        //[DataMember]
        //public List<LoginPromoDetails> PromoDetails { get; set; }


    }
}
