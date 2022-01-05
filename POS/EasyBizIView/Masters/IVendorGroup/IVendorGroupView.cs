using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.VendorGroup
{
  public interface IVendorGroupView : IBaseView
    {
        int ID { get; set; }

        string VendorGroupCode { get; set; }

        string VendorGroupName { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }

        List<VendorGroupMaster> VendorGroupList { get; set; }
    }
}
