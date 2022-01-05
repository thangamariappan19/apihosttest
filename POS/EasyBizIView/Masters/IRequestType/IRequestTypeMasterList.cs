using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IRequestType
{
    public interface IRequestTypeMasterList:IBaseView
    {
        List<RequestTypeMaster> RequestTypeMasterList { get; set; }
    }
}
