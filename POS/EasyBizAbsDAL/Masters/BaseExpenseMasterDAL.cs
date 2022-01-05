using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.ExpenseMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.ExpenseMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public abstract class BaseExpenseMasterDAL : BaseDAL
    {
        public abstract SelectAllExpenseMasterResponse API_SelectALL(SelectAllExpenseMasterRequest requestData);
    }
}
