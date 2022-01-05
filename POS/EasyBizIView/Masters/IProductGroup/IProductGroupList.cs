using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ProductGroup
{
   public interface IProductGroupList:IBaseView
    {
       List<ProductGroupMaster> ProductGroupMasterList { get; set; }
    }
}
