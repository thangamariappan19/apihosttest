using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CustomerMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseCustomerMasterDAL:BaseDAL
    {
        public abstract SelectCustomerMasterLookUpResponse SelectCustomerMasterLookUp(SelectCustomerMasterLookUpRequest ObjRequest);
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

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public abstract SelectCustomerByPhoneNoResponse SelectCustomerByPhoneNo(SelectCustomerByPhoneNoRequest objRequest);

        public abstract SelectAllCustomerMasterResponse GetCommonCustomerData(SelectAllCustomerMasterRequest requestData);
        public abstract SelectAllCustomerMasterResponse GetCommonCustomerDetailsData(SelectAllCustomerMasterRequest requestData);
        public abstract SelectAllCustomerMasterResponse GetCommonCustomerDetailsDataID(SelectAllCustomerMasterRequest requestData);
        public abstract SelectAllCustomerMasterResponse GetCustomerSearchPOS(SelectAllCustomerMasterRequest requestData);

        public abstract SelectAllCustomerMasterResponse API_SelectAll(SelectAllCustomerMasterRequest objRequest);
        public abstract SelectAllCustomerSaleTransactionResponse API_CustomerSalesTransactionAll(SelectAllCustomerSalesTransactionRequest objRequest);

        public abstract SelectAllCustomerSaleTransactionResponse API_CustomerReturnTransactionAll(SelectAllCustomerSalesTransactionRequest objRequest);
        public abstract SelectAllCustomerSaleTransactionResponse API_CustomerReturnExchange(SelectAllCustomerSalesTransactionRequest objRequest);
    }
}
