using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView
{
    public interface IBaseView
    {
        string Message { set; }

        Nullable<int> SCN { get; set; }        

        long UserID { get; }

        Enums.OpStatusCode ProcessStatus { get; set; }

        Enums.RequestFrom RequestFrom { get;}
    }
}
