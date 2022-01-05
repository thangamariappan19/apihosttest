using EasyBizAbsDAL.Common;
using EasyBizRequest.FCPasses;
using EasyBizResponse.FCPasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.FCPasses
{
    public abstract class BassPassMasterDAL : BaseDAL
    {
        public abstract SelectPassMasterLookUpResponse API_SelectPassMasterLookUp(SelectPassMasterLookUpRequest requestData);
    }
}
