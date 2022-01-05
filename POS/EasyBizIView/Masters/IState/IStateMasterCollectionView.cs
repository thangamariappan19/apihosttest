using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.State
{
   public interface IStateMasterCollectionView : IBaseView
    {
       List<StateMaster> StateList { get; set; }
    }
}
