using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.FranchiseRequest;
using EasyBizResponse.Masters.FranchiseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseFranchiseMasterDAL : BaseDAL
    {
        public abstract SelectFranchiseLookupResponse SelectFranchiseLookUp(SelectFranchiseLookUpRequest ObjRequest);
        public abstract SelectAllfranchiseResponse API_SelectALL(SelectAllFranchiseMasterRequest requestData);
        public abstract SelectFranchiseLookupResponse API_SelectFranchiseMasterLookUp(SelectFranchiseLookUpRequest objRequest);
    }
}
