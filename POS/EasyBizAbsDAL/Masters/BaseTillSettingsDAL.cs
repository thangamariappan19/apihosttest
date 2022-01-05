using EasyBizAbsDAL.Common;
using EasyBizRequest;
using EasyBizRequest.Masters.TillSettingRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.TillSettingsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseTillSettingsDAL : BaseDAL
    {
        public abstract SelectTillSettingsLookUpResponse SelectTillSettingsLookUp(SelectTillSettingsLookUpRequest RequestObj);



        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
