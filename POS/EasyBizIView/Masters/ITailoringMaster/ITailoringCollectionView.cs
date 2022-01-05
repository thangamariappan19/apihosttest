using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ITailoringMaster
{
    public interface ITailoringCollectionView : IBaseView
    {
        List<TailoringMasterTypes> TailoringMasterList { get; set; }
      
    }
}
