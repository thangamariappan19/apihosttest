using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDesignMaster
{
    public interface IDesignView:IBaseView
    {
     
         int ID { get; set; }
      
         string DesignCode { get; set; }

         string DesignName { get; set; }

        
         string Description { get; set; }

      
         string ForeignDescription { get; set; }

    
         int SegamentationID { get; set; }

      
         int StyleStatusID { get; set; }

       
         int ProductLineID { get; set; }

      
         int ProductGroupID { get; set; }

       
         int SeasonID { get; set; }
         String Grade { get; set; }

        
         int CollectionID { get; set; }

       
         int SubCollectionID { get; set; }

       
         string Composition { get; set; }

      
         string SimbolGroup { get; set; }

        
         int BrandID { get; set; }

      
        
         int DesignerID { get; set; }

       
         int DivisionID { get; set; }

         int DropID { get; set; }

         string ProductDepartmentCode { get; set; }
         string DevelopmentOffice { get; set; }
         string ShortDescription { get; set; }

         int YearID { get; set; }

         string Remarks { get; set; }

         Boolean Active { get; set; }


         List<AFSegamationMasterTypes> AFSegamationMasterLookUp { set; }

         List<CollectionMasterTypes> CollectionMasterLookUp { set; }

         List<BrandMaster> BrandMasterLookUp { set; }

         List<DivisionMaster> DivisionMasterLookUp { set; }

         List<SubCollectionMaster> SubCollectionMasterLookUp { set; }

         List<StyleStatusMasterType> StyleStatusMasterLookUp { set; }

         List<ProductLineMaster> ProductLineMasterLookUp { set; }

         List<ProductGroupMaster> ProductGroupMasterLookUp { set; }

         List<SeasonMaster> SeasonMasterLookUp { set; }

         List<EmployeeMaster> EmployeeMasterLookUp { set; }

         List<DropMasterTypes> DropMasterLookUp { set; }

         List<YearMaster> YearList { set; }

         List<ItemImageMaster> DesigntemImageMasterList { get; set; }
         List<PricePoint> PricePointList { get; set; }

         List<DesignGradeTypes> DesignGradeLookUp { set; }
         List<DesignDevelopmentOfficeTypes> DesignDevelopmentOfficeLookUp { set; }

         string SegamentationCode { get; }
         string StyleStatusCode { get;  }
         string ProductLineCode { get;  }
         string ProductGroupCode { get;  }
         string SeasonCode { get;  }
         string DropCode { get;  }
         string CollectionCode { get;  }
         string SubCollectionCode { get; }
         string BrandCode { get;  }
         string DesignerCode { get;  }   
         string DivisionCode { get; }
         string YearCode { get; }
    }
}
