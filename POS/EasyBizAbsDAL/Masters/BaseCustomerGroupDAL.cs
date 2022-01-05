using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizResponse.Masters.CustomerGroupMasterResponse;
using EasyBizResponse.Masters.CustomerGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCustomerGroupDAL : BaseDAL
    {
        public abstract SelectCustomerGroupLookUpResponse SelectCustomerGroupLookUp(SelectCustomerGroupLookUpRequest RequestObj);
        public abstract SelectAllCustomerGroupMasterResponse API_SelectALL(SelectAllCustomerGroupMasterRequest requestData);
    }
}
