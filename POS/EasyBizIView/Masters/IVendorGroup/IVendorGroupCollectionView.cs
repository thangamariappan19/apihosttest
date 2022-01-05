using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.VendorGroup
{
   public interface IVendorGroupCollectionView : IBaseView
    {
        List<VendorGroupMaster> VendorGroupList { get; set; }
    }
}
