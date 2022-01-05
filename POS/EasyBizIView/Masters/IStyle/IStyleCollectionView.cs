using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IStyle
{
    public interface IStyleCollectionView:IBaseView
    {
        List<StyleMaster> StyleList { get; set; }
    }
}
