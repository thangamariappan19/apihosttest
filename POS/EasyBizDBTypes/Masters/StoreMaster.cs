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
    public class StoreMaster : BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string StoreCode { get; set; }
        [DataMember]
        public string IDs { get; set; }

        [DataMember]
        public string StoreName { get; set; }

       
        [DataMember]
        public int CountrySetting { get; set; }
        [DataMember]
        public int StateID { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int StoreGroup { get; set; }

        [DataMember]
        public int StoreCompany { get; set; }

        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public string ShopBrand { get; set; }
        [DataMember]
        public string StoreGroupCode { get; set; }

        [DataMember]
        public string StoreType { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string StoreGroupName { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public int PriceListID { get; set; }

        [DataMember]
        public int RetailID { get; set; }

        [DataMember]
        public int TaxID { get; set; }

        [DataMember]

        public string Address { get; set; }

        [DataMember]

        public string Location { get; set; }

        [DataMember]

        public Decimal StoreSize { get; set; }

        [DataMember]

        public int NoOfOptions { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string Grade { get; set; }
        [DataMember]
        public string StoreHeader { get; set; }
        [DataMember]
        public string StoreFooter { get; set; }
        [DataMember]
        public string PrintCount { get; set; }
        [DataMember]
        public string ReturnPrintCount { get; set; }
        [DataMember]
        public string ExchangePrintCount { get; set; }
        [DataMember]
        public byte[] StoreImage { get; set; }
        [DataMember]
        public byte[] LicenseImage { get; set; }
        [DataMember]
        public string DiskID { get; set; }
        [DataMember]
        public string CPUID { get; set; }
        [DataMember]
        public List<StoreMaster> StoreImageList { get; set; }
        [DataMember]
        public string ToMailID { get; set; }
        [DataMember]
        public string CCMailID { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public int BrandID { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        //[DataMember]
        //public List<StoreBrandMapping> StoreBrandMappingList { get; set; }
        [DataMember]

        public List<StoreBrandMapping> SelectStoreBrandMappingList { get; set; }
        [DataMember]
        public string EmailTemplate { get; set; }
        [DataMember]
        public string SMSTemplate { get; set; }
        [DataMember]
        public string FranchiseCode { get; set; }
        [DataMember]
        public int FranchiseID { get; set; }

        [DataMember]
        public string StateCode { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
       
        [DataMember]
        public string StoreCompanyCode { get; set; }
        [DataMember]
        public string PriceListCode { get; set; }
        //[DataMember]
        //public bool EnableCashDrawer { get; set; }

        [DataMember]
        public bool EnableOnlineStock { get; set; }

        [DataMember]
        public bool EnableOrderFulFillment { get; set; }

        [DataMember]
        public bool EnableFingerPrint { get; set; }
        [DataMember]
        public int CityID { get; set; }
        [DataMember]
        public int CreateBy { get; set; }
        [DataMember]
        public int EnableBin { get; set; }

    }
}
