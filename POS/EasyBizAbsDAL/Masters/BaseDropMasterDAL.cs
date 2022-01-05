using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DropMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseDropMasterDAL : BaseDAL
    {
        public abstract SelectAllDropMasterResponse API_SelectALL(object objRequest);
    }
}
