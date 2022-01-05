using EasyBizDBTypes.Common;
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
    public class UsersSettings : BaseType
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
        public int StateID { get; set; }

        [DataMember]
        public int PriceListID { get; set; }

        [DataMember]
        public int CountryID { get; set; }

      
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public int POSID { get; set; }
        [DataMember]
        public int DefaultCustomerID { get; set; }

         [DataMember]
        public int CurrencyID { get; set; }

        
        [DataMember]
         public string IsLoggedStoreCode { get; set; }
        [DataMember]
        public int IsLoggedPosID { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]       
        public string CompanyName { get; set; }
        //  public string CountryName { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        public string StoreGroupName { get; set; }
        public string BrandID { get; set; }
        public int StoreGroupID { get; set; }
        public bool AllowNewRow { get; set; }
        public string EmployeeImage { get; set; }
        public string PrinterDeviceName { get; set; }
        public string PosName { get; set; }
        public string CurrencySymbol { get; set; }
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
        //[DataMember]
        //public string NewPassword { get; set; }
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

        //public string ConfirmPassword { get; set; }
       

       // public string ConfirmPassword { get; set; }  

        public int PrintCount { get; set; }
        public int ExchangePrintCount { get; set; }
        public int SaleReturnPrintCount { get; set; }
        [DataMember]
        public string CountryName { get; set; }

        public string CountryCode { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string ToMailID {get;set;}
        [DataMember]
        public string CCMailID {get;set;}

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
        //[DataMember]
        //public bool EnableCashDrawer { get; set; }

    }
}
