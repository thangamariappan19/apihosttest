using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.SubBrand
{
  public  interface ISubBrandMasterCollectionView : IBaseView
    {
        List<SubBrandMaster> SubBrandList { get; set; }

        List<BrandMaster> BrandList { get; set; }
    }
}
