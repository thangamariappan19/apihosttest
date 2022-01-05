using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IRequestType
{
    public interface  IRequestTypeMasterView: IBaseView
    {
        int ID { get; set; }
        string RequestTypeCode { get; set; }
        string RequestTypeName { get; set; }
        string Description { get; set; }
        bool Active { get; set; }
    }
}
