using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IOrderTypeMaster
{
    public interface IOrderTypeMasterList:IBaseView
    {
        List<OrderTypeMaster> OrderTypeMasterList { get; set; }
    }
}
