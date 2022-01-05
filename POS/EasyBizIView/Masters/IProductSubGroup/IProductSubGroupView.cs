using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IProductSubGroup
{
    public interface IProductSubGroupView : IBaseView
    {
        int ID { get; set; }
        long ProductGroupID { get; set; }
        string ProductGroupName { get; set; }
        List<ProductSubGroupMaster> ProductSubGroupList { get; set; }
        List<ProductGroupMaster> ProductGroupLookUp { set; }
    }
}
