using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IStoreMaster
{
    public interface IStoreMasterView:IBaseView
    {

         int ID { get; set; }
         string StoreCode { get; set; }
         string StoreName { get; set; }       
         string Brand { get; set; }
         string ShopBrand { get; set; }  
         int CountrySetting { get; set; }     
         int StoreGroup { get; set; }        
         int StoreCompany { get; set; } 
         string StoreType { get; set; }
         string Remarks { get; set; }
         bool Active { get; set; }
         int PriceListID { get; set; }
         int RetailID { get; set; }
         int TaxID { get; set; }
         int StateID { get; set; }

         string Address { get; set; }

         string Location { get; set; }

         Decimal StoreSize { get; set; }

         int NoOfOptions { get; set; }

         DateTime StartDate { get; set; }

         DateTime EndDate { get; set; }

         String Grade { get; set; }
         string StoreHeader { get; set; }
         string StoreFooter { get; set; }
         string PrintCount { get; set; }
         string ReturnPrintCount { get; set; }
         string ExchangePrintCount { get; set; }
         byte[] StoreImage { get; set; }
         byte[] LicenseImage { get; set; }
         string DiskID { get; set; }
         string CPUID { get; set; }
                
         List<StoreMaster> StoreImageList { get; set; }
         List<StoreMaster> LicenceImageList { get; set; }

         List<StoreGradeTypes> StoreGradeLookUp { set; }
         List<CountryMaster> CountryMasterLookUp { get; set; }

         List<StoreGroupMaster> StoreGroupMasterLookUp { get; set; }

         List<CompanySettings> CompanySettingsLookUp { get; set; }

         List<BrandMaster> BrandMasterLookUp { get; set; }
         List<BrandMaster> ShopBrandMasterLookUp { get; set; }

         List<CurrencyMaster> CurrencyMasterLookUp { get; set; }

         List<PriceListType> PriceListLookUp { get; set; }

         List<RetailSettingsType> RetailSettingsListLookUp { get; set; }

         List<TaxMaster> TaxMasterLookUp { get; set; }
         List<FranchiseType> FranchiseTypeLookUp { get; set; }

         List<StateMaster> StateMasterLookUp { set; }
         string CountryCode { get;  }        
         List<StoreBrandMapping> StoreBrandMappingList { get; set; }
         List<StoreBrandMapping> SelectByIdStoreBrandMappingList { get; set; }

         string EmailTemplate { get; set; }
         string SMSTemplate { get; set; }

         int FranchiseID { get; set; }
         string FranchiseCode { get; set; }

         string StoreGroupCode { get; }
         string StoreCompanyCode { get;}
         string PriceListCode { get; }
         string RetailCode { get; }
         string TaxCode { get; }
         string StateCode { get; }

        bool EnableOnlineStock { get; set; }
        bool EnableOrderFulFillment { get; set; }
        bool EnableFingerPrint { get; set; }

        List<CityMaster> CityMasterLookUp {  set; }
        int CityID { get; set; }
       
    }
}
