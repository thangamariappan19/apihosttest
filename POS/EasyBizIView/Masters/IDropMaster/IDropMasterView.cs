using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDropMaster
{
    public interface IDropMasterView:IBaseView
    {
        int ID { get; set; }

        string DropCode { get; set; }

        string DropName { get; set; }

        string Remarks { get; set; }

        bool Active { get; set; }

    }
}
