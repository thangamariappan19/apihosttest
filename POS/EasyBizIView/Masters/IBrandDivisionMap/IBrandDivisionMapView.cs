using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IBrandDivisionMap
{
    public interface IBrandDivisionMapView : IBaseView
    {
        int ID { get; set; }
        string BrandCode { get; }
        long BrandID { get; set; }      
        List<BrandMaster> BrandLookUp { set; }        
        List<BrandDivisionTypes> BrandDivisionList { set; }
        List<BrandDivisionTypes> MappingList { get; set; }
       
    }
}
