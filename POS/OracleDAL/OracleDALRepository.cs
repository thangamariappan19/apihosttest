using EasyBizAbsDAL;
using EasyBizAbsDAL.Common;
using EasyBizAbsDAL.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleDAL
{
    public class OracleDALRepository : BaseDALRepository
    {
        public override BaseUsersDAL GetUsersDAL()
        {
            throw new NotImplementedException();
        }
<<<<<<< .mine
       
    
        public override BaseProductLineMasterDAL GetProductLineMasterDAL()
        {
 	        throw new NotImplementedException();
        }

        public override BaseWarehouseMasterDAL GetWarehouseMasterDAL()
        {
            throw new NotImplementedException();
        }

        public override BaseWarehouseTypeMasterDAL GetWarehouseTypeMasterDAL()
        {
            throw new NotImplementedException();
        }
||||||| .r3
=======
<<<<<<< .mine
        public override BaseRoleDAL GetRoleDAL()
        {
            throw new NotImplementedException();
        }

        public override BaseCurrencyDAL GetCurrencyDAL()
        {
            throw new NotImplementedException();
        }

        public override BaseCountryDAL GetCountryDAL()
        {
            throw new NotImplementedException();
        }
||||||| .r3
=======

        public override BaseCustomerGroupMasterDAL GetCustomerGroupMaster()
        {
            throw new NotImplementedException();
        }

        public override BaseCompanySettingDAL GetCompanySetting()
        {
            throw new NotImplementedException();
        }
>>>>>>> .r9
>>>>>>> .r16
    }
}
