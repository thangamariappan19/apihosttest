using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.SubBrand
{
   public interface ISubBrandMasterView : IBaseView
    {
        int ID { get; set; }
        long BrandID { get; set; }
        string BrandName { get; set; }
      
        List<SubBrandMaster> SubBrandList { get; set; }
        List<BrandMaster> BrandLookUp { set; }
    }
}
