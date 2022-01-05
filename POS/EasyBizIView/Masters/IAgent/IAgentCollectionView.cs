using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IAgent
{
    public interface IAgentCollectionView : IBaseView
    {
        List<AgentMaster> AgentList { get; set; }
    }
}
