using EasyBizDBTypes.Masters;
using EasyBizTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IManagerOverride
{
    public interface IManagerOverrideList : IBaseView
    {
        List<ManagerOverride> ManagerOverrideList { get; set; }
    }
}
