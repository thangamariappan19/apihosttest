using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ProductGroup
{
    public interface IProductGroupView : IBaseView
    {
        int ID { get; set; }
        string ProductGroupCode { get; set; }
        string ProductGroupName { get; set; }
        string Description { get; set; }
        bool Active { get; set; }
        List<ProductGroupMaster> ProductGroupMasterList { get; set; }
    }
}
