using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IAFSegamation
{
    public interface IAFSegamationViewList:IBaseView
    {
        List<AFSegamationMasterTypes> AFSegamationMasterList { get; set; }
    }
}
