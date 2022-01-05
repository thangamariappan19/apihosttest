using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IScale
{
    public interface IScaleCollectionView : IBaseView
    {
        List<ScaleMaster> ScaleList { get; set; }
    }
}
