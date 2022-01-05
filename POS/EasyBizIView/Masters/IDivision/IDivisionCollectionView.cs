using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDivision
{
    public interface IDivisionCollectionView : IBaseView
    {
        List<DivisionMaster> DivisionList { get; set; }
    }
}
