using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters
{
     public interface IProductLineMasterList:IBaseView
    {
         List<ProductLineMaster> ProductLineMasterList { get; set; }
    }
}
