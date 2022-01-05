using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IReason
{
    
    public interface IReasonMasterView : IBaseView
    {

        int ID { get; set; }

        string ReasonCode { get; set; }

        string ReasonName { get; set; }        

        string Description { get; set; }
        bool Active { get; set; }

    }
}
