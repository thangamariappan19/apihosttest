using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IFreight
{
    public interface IFreightMasterView: IBaseView
    {
        int ID { get; set; }
        string FreightCode { get; set; }
        string FreightName { get; set; }
        string Description { get; set; }
        string Remarks { get; set; }
        bool Active { get; set; }
    }
}
