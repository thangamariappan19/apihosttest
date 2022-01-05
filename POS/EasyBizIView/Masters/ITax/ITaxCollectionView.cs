using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.ITax
{
    public interface ITaxCollectionView : IBaseView
    {
        List<TaxMaster> TaxList { get; set; }
    }
}
