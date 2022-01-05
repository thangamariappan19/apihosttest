using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPriceTypeMaster
{
    public interface IPriceTypeViewList:IBaseView
    {
        List<PriceTypeMasterTypes> PriceTypeMasterTypesList { get; set; }
    }
}
