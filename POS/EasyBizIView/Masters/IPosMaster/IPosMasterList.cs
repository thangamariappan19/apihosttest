using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IPosMaster
{
    public interface IPosMasterList : IBaseView
    {
        List<PosMaster> PosMasterList { get; set; }
    }
}
