using EasyBizAbsDAL.Common;
using EasyBizRequest.Transactions.DiscountMasterRequest;
using EasyBizResponse.Transactions.Promotions.DiscountMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Transactions.DiscountMaster
{
    public abstract class BaseDiscountMasterDAL : BaseDAL
    {
        public abstract SelectDiscountMasterDetailsResponse SelecDiscountMasterDetails(SelectDiscountMasterDetailsRequest ObjRequest);
        public abstract SelectByIDDiscountMasterResponse SelectHeaderID(SelectByIDDiscountMasterRequest ObjRequest);      
    }
}
