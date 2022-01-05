using EasyBizRequest;
using EasyBizResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Common
{
    public abstract class BaseDAL
    {
        public abstract BaseResponseType InsertRecord(BaseRequestType RequestObj);

        //public abstract BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj);

        public abstract BaseResponseType UpdateRecord(BaseRequestType RequestObj);

        public abstract BaseResponseType DeleteRecord(BaseRequestType RequestObj);

        public abstract BaseResponseType SelectRecord(BaseRequestType RequestObj);

        public abstract BaseResponseType SelectAll(BaseRequestType RequestObj);
        
        public abstract BaseResponseType SelectByIDs(BaseRequestType RequestObj);

        public abstract BaseResponseType DeleteByIDs(BaseRequestType RequestObj);

      



    }
}
