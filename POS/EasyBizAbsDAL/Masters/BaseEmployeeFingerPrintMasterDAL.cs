using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.EmployeeFingerPrintRequest;
using EasyBizResponse.Masters.EmployeeFingerPrintResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseEmployeeFingerPrintMasterDAL : BaseDAL
    {
        public abstract SelectEmployeeFingerPrintByIDResponse SelectEmployeeFingerPrintByID(SelectEmployeeFingerPrintByIDRequest objRequest);
        
    }
}
