using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IBinMaster
{
    public interface IBinLevelMasterCollectionView : IBaseView
    {
        List<BinLevelMasterTypes> BinLevelMasterList { get; set; }
    }
}
