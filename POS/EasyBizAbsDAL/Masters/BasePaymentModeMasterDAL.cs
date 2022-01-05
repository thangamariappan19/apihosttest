using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.PaymentModeMaterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.PaymentModeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyBizAbsDAL.Masters
{
    public abstract class BasePaymentModeMasterDAL:BaseDAL
    {
        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public abstract SelectAllPaymentModeMasterResponse SelectAll(SelectAllPaymentModeMasterRequest objRequest);
        
        public abstract SelectPaymentModeLooKUpResponse SelectPaymentModeRecord(SelectPaymentModeLooKUpRequest RequestObj);
       


    }
}
