using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{

    [Serializable]
    [DataContract]
    public class CustomerMaster:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public long BaseID { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string AlterPhoneNumber { get; set; }

        [DataMember]
        public int CustomerGroupID { get; set; }

        [DataMember]
        public string BuildingAndBlockNo { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string AreaName1 { get; set; }

        [DataMember]
        public string AreaName2 { get; set; }

        [DataMember]
        public string BillingPhoneNumber { get; set; }


        [DataMember]
        public string City { get; set; }

        [DataMember]
        public int StateID { get; set; }

        [DataMember]
        public int CountryID { get; set; }

        [DataMember]
        public string Email { get; set; }
      

        [DataMember]
        public DateTime DOB { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string GroupName { get; set; }


        [DataMember]
        public string StateName { get; set; }


        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public Decimal CreditAmount { get; set; }

        [DataMember]
        public bool IsDefaultCustomer { get; set; }
        [DataMember]
        public string CustomerGroupCode { get; set; }
        [DataMember]
        public string StateCode { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public bool OnAccountApplicable { get; set; }
        [DataMember]
        public long DocumentNumberingID { get; set; }
        [DataMember]
        public int DocumentTypeID { get; set; }
        /*[DataMember]
        public int RunningNo { get; set; }*/
        [DataMember]
        public int StoreID { get; set; }
        [DataMember]
        public string CustomerImage { get; set; }
        [DataMember]
        public string ShippingAddress1 { get; set; }
        [DataMember]
        public string ShippingAddress2 { get; set; }
        [DataMember]
        public string ShippingPhoneNumber { get; set; }
        [DataMember]
        public int ShippingStateID { get; set; }
         [DataMember]
        public string ShippingStateCode { get; set; }
        [DataMember]
        public string ShippingStateName { get; set; }
        [DataMember]
        public string ShippingCity { get; set; }
        [DataMember]
        public string ShippingPincode { get; set; }
        [DataMember]
        public string Pincode { get; set; }
        [DataMember]
        public int ShippingCountryID { get; set; }
        [DataMember]
        public string ShippingCountryCode { get; set; }
        [DataMember]
        public Decimal LoyalityPoint { get; set; }
        [DataMember]
        public Decimal LoyalityValue { get; set; }
        [DataMember]
        public string LoyalityGroup { get; set; }
        [DataMember]
        public bool IsFCSync { get; set; }
        [DataMember]
        public string IsoCode { get; set; }
        [DataMember]
        public string AddressIsoCode { get; set; }
        [DataMember]
        public string ShippingIsoCode { get; set; }

        #region "new Fields"

        [DataMember]
        public string LastName { get; set; }
        [DataMember]      
        public int SubGroupID { get; set; }
        [DataMember]      
        public string SubGroupCode { get; set; }
        [DataMember]      
        public string PaymentTermsDays { get; set; }
        [DataMember]      
        public string CreditDays { get; set; }
        [DataMember]      
        public bool IsLoyalty { get; set; }
        [DataMember]      
        public bool IsTaxExempt { get; set; }
        [DataMember]      
        public string LoyaltyID { get; set; }
        [DataMember]      
        public string LoyaltyPlan { get; set; }

        #endregion
    }
}
