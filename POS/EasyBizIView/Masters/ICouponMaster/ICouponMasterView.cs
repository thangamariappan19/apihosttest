using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ICouponMaster
{
    public interface ICouponMasterView:IBaseView
    {
        int ID { get; set; }

        string CouponCode { get; set; }

        
         string Coupondescription { get; set; }
      
         string BarCode { get; set; }
       
         string Country { get; set; }
     
         string CouponType { get; set; }

      
         DateTime StartDate { get; set; }

       
         DateTime EndDate { get; set; }

      
         string DiscountType { get; set; }

     
         double DiscountValue { get; set; }

      
         bool IssuableAtPOS { get; set; }
       
         bool Serial { get; set; }

         string Remarks { get; set; }

         bool Active { get; set; }



        
          string CouponSerialCode { get; set; }


          string Issuedstatus { get; set; }

 
          string PhysicalStore { get; set; }

    
          double Remainingamount { get; set; }

    
          string Redeemedstatus { get; set; }


         List<CountryMaster> CountryMasterLookUp {  set; }

         List<StoreGroupMaster> StoreGroupMasterLookUp { get; set; }

         List<StoreMaster> StoreMasterList { get;  set; }

         List<CustomerMaster> CustomerMasterList { get; set; }

         List<CustomerGroupMaster> CustomerGroupMasterList { get; set; }

         string CouponStoreType { get; set; }

         string StoreTypeValue { get; set; }

         string CouponCustomerType { get; set; }

         string CouponProductType { get; set; }

         List<AFSegamationMasterTypes> SegamationMasterList { get; set; }
         List<YearMaster> YearList { get; set; }

         List<BrandMaster> BrandList { get; set; }

         List<SubBrandMaster> SubBrandMasterList { get; set; }

         List<SeasonMaster> SeasonList { get; set; }

         List<ProductGroupMaster> ProductGroupList { get; set; }

         List<ProductSubGroupMaster> ProductSubGroupList { get; set; }

         List<CommonUtil> StoreCommonUtil { get; set; }

         List<CommonUtil> CustomerCommonUtil { get; set; }

         List<CommonUtil> TotalMasterCommonUtil { get; set; }


         List<CommonUtil> StoreCommonUtilDetails { get; set; }

         List<CommonUtil> CustomerCommonUtilDetails { get; set; }

         List<CommonUtil> TotalMasterCommonUtilDetails { get; set; }
        
    }
}
