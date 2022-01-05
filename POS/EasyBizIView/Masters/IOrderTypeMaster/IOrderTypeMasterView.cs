using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IOrderTypeMaster
{
  public   interface IOrderTypeMasterView:IBaseView
    {
        int ID { get; set; }
        string OrderTypeCode { get; set; }
        string OrderTypeName { get; set; }
        string Description { get; set; }
        bool Active { get; set; }
    }
}
