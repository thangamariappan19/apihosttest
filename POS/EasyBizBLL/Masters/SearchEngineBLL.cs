using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizFactory;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.SearchEngineRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Masters.SearchEngineResponse;
using ResourceStrings;

namespace EasyBizBLL.Masters
{
    public class SearchEngineBLL
    {

        public CustomersSkuResponse GetCustomerSKUSearchPOS(CustomerSkuRequest requestData)
        {
            CustomersSkuResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerSKUMaster = objFactory.GetDALRepository().GetCustomerSkuMasterDAL();
                objResponse = (CustomersSkuResponse)objBaseCustomerSKUMaster.GetCustomerSKUSearchPOS(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new CustomersSkuResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Search Engine");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SearchCustomerResponse GetCustomerSearchPOS(SearchCustomerRequest requestData)
        {
            SearchCustomerResponse  objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerSKUMaster = objFactory.GetDALRepository().GetCustomerSkuMasterDAL();
                objResponse = (SearchCustomerResponse)objBaseCustomerSKUMaster.GetCustomerSearchPOS(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SearchCustomerResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Search Engine");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        public SearchBrandResponse GetBrandSearch(SearchBrandRequest requestData)
        {
            SearchBrandResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerSKUMaster = objFactory.GetDALRepository().GetCustomerSkuMasterDAL();
                objResponse = (SearchBrandResponse)objBaseCustomerSKUMaster.GetBrandSearch(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SearchBrandResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Search Engine");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }
        //public SearchCustomerResponse GetSaleReturnSearch(SearchCustomerRequest requestData)
        //{
        //    SearchCustomerResponse objResponse = null;
        //    var objFactory = new DALFactory();
        //    try
        //    {

        //        var objBaseCustomerSKUMaster = objFactory.GetDALRepository().GetCustomerSkuMasterDAL();
        //        objResponse = (SearchCustomerResponse)objBaseCustomerSKUMaster.GetSalereturnsearch(requestData);

        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse = new SearchCustomerResponse();
        //        objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Search Engine");
        //        objResponse.ExceptionMessage = ex.Message;
        //        objResponse.StackTrace = ex.StackTrace;
        //    }

        //    return objResponse;
        //}

        public SearchSalesReturnExchangeResponse GetSaleReturnSearch(SearchSalesReturnExchangeRequest requestData)
        {
            SearchSalesReturnExchangeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerSKUMaster = objFactory.GetDALRepository().GetCustomerSkuMasterDAL();
                objResponse = (SearchSalesReturnExchangeResponse)objBaseCustomerSKUMaster.GetSalereturnsearch(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SearchSalesReturnExchangeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Search Engine");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }


        public SearchExchangeResponse GetExchangeSearch(SearchExchangeRequest requestData)
        {
            SearchExchangeResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {

                var objBaseCustomerSKUMaster = objFactory.GetDALRepository().GetCustomerSkuMasterDAL();
                objResponse = (SearchExchangeResponse)objBaseCustomerSKUMaster.GetExchangeSearch(requestData);

            }
            catch (Exception ex)
            {
                objResponse = new SearchExchangeResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Search Engine");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }

            return objResponse;
        }

    }
}
