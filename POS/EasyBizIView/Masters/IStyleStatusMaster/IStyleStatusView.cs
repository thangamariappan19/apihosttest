using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IStyleStatusMaster
{
   public interface IStyleStatusView:IBaseView
    {

        int ID { get; set; }
        string StyleStatusCode { get; set; }

        string StatusName { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }


    }
}
