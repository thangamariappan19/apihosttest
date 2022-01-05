using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IRetailSettings
{
    public interface IRetailCollectionView : IBaseView
    {
        List<RetailSettingsType> RetailList { get; set; }
    }
}
