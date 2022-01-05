using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IAgent
{
    public interface IAgentView : IBaseView
    {
        int ID { get; set; }

        string AgentCode { get; set; }

        string AgentName { get; set; }
        string Remarks { get; set; }
        Boolean Active { get; set; }
        List<AgentMaster> AgentList { get; set; }
    }
}
