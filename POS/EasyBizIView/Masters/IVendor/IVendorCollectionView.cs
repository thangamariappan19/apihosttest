using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.Vendor
{
    public interface IVendorCollectionView : IBaseView
    {
        List<VendorMaster> VendorList { get; set; }
    }
}
