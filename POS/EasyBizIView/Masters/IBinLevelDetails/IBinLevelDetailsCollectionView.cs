using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IBinLevelDetails
{
    public interface IBinLevelDetailsCollectionView : IBaseView
    {
        List<BinLevelMasterTypes> BinLevelMasterList { get; set; }
    }
}
