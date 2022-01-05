using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SearchEngineRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.SearchEngineResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EasyBizAbsDAL.Masters
{

    public abstract class BaseSearchEngineDAL : BaseDAL
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

        public override BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public abstract CustomersSkuResponse GetCustomerSKUSearchPOS(CustomerSkuRequest requestData);
        public abstract SearchCustomerResponse GetCustomerSearchPOS(SearchCustomerRequest requestData);
        public abstract SearchBrandResponse GetBrandSearch(SearchBrandRequest requestData);
        public abstract SearchSalesReturnExchangeResponse GetSalereturnsearch(SearchSalesReturnExchangeRequest requestData);

        public abstract SearchExchangeResponse GetExchangeSearch(SearchExchangeRequest requestData);

    }

}