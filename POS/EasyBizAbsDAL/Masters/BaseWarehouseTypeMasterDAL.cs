using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.WarehouseTypeMasterRequest;
using EasyBizResponse.Masters.WarehouseMasterResponse;
using EasyBizResponse.Masters.WarehouseTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseWarehouseTypeMasterDAL : BaseDAL
    {
        public abstract SelectWarehouseTypeMasterLookUpResponse SelectWarehouseTypeMasterLookUp(SelectWarehouseTypeMasterLookUpRequest RequestObj);
        public abstract SelectAllWarehouseTypeMasterResponse API_SelectAll(SelectAllWarehouseTypeMasterRequest objRequest);
        public abstract SelectAllWarehouseTypeMasterResponse SelectAllWarehouseTypeMasterLookUp(SelectAllWarehouseTypeMasterRequest requestData);
    }
}
