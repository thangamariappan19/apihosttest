using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DesignationMasterRequest;
using EasyBizResponse.Masters.DesignationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
  
    public abstract class BaseDesignationMasterDAL : BaseDAL
    {
        public abstract SelectDesignationMasterLookUpResponse SelectDesignationMasterLookUp(SelectDesignationMasterLookUpRequest RequestObj);
        public abstract SelectAllDesignationMasterResponse API_SelectAll(SelectAllDesignationMasterRequest requestData);
    }
}
