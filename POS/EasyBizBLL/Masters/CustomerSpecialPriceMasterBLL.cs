using EasyBizFactory;
using EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest;
using EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
  public  class CustomerSpecialPriceMasterBLL
    {
      public SaveCustomerSpecialPriceMasterResponse SaveCustomerSpecialPriceMaster(SaveCustomerSpecialPriceMasterRequest objRequest)
      {
          SaveCustomerSpecialPriceMasterResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var objBaseCustomerSpecialPriceMasterDAL = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();

              objResponse = (SaveCustomerSpecialPriceMasterResponse)objBaseCustomerSpecialPriceMasterDAL.InsertRecord(objRequest);
              
          }
          catch (Exception ex)
          {
              objResponse = new SaveCustomerSpecialPriceMasterResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CustomerSpecialPriceMasterDAL");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public UpdateCustomerSpecialPriceMasterResponse UpdateCustomerSpecialPriceMaster(UpdateCustomerSpecialPriceMasterRequest objRequest)
      {
          UpdateCustomerSpecialPriceMasterResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var objBaseCustomerSpecialPriceMasterDAL = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();

              objResponse = (UpdateCustomerSpecialPriceMasterResponse)objBaseCustomerSpecialPriceMasterDAL.UpdateRecord(objRequest);

          }
          catch (Exception ex)
          {
              objResponse = new UpdateCustomerSpecialPriceMasterResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CustomerSpecialPriceMasterDAL");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public SelectAllCustomerSpecialPriceMasterResponse SelectAllCustomerSpecialPriceMaster(SelectAllCustomerSpecialPriceMasterRequest objRequest)
      {
          SelectAllCustomerSpecialPriceMasterResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var BaseCustomerSpecialPriceMaster = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();
              objResponse = (SelectAllCustomerSpecialPriceMasterResponse)BaseCustomerSpecialPriceMaster.SelectAll(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectAllCustomerSpecialPriceMasterResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CustomerSpecialPriceMaster");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public SelectByIDCustomerSpecialPriceMasterResponse SelectByIDCustomerSpecialPriceMaster(SelectByIDCustomerSpecialPriceMasterRequest objRequest)
      {
          SelectByIDCustomerSpecialPriceMasterResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var BaseCustomerSpecialPriceMaster = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();
              objResponse = (SelectByIDCustomerSpecialPriceMasterResponse)BaseCustomerSpecialPriceMaster.SelectRecord(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectByIDCustomerSpecialPriceMasterResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SelectByIDCustomerSpecialPriceMaster");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      public SelectByCustomerSpecialPriceDetailsResponse SelectByCustomerSpecialPriceDetails(SelectByCustomerSpecialPriceDetailsRequest objRequest)
      {
          SelectByCustomerSpecialPriceDetailsResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var CustomerSpecialPriceDetails = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();
              objResponse = (SelectByCustomerSpecialPriceDetailsResponse)CustomerSpecialPriceDetails.SelectByCustomerSpecialPriceDetails(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectByCustomerSpecialPriceDetailsResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CustomerSpecialPriceDetails");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
      //public SelectByIDCustomerSpecialStoreDetailsResponse SelectByIDCustomerSpecialStoreDetails(SelectByIDCustomerSpecialStoreDetailsRequest objRequest)
      //{
      //    SelectByIDCustomerSpecialStoreDetailsResponse objResponse = null;
      //    var objFactory = new DALFactory();
      //    try
      //    {
      //        var CustomerSpecialStoreDetails = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();
      //        objResponse = (SelectByIDCustomerSpecialStoreDetailsResponse)CustomerSpecialStoreDetails.SelectByIDCustomerSpecialStoreDetails(objRequest);
      //    }
      //    catch (Exception ex)
      //    {
      //        objResponse = new SelectByIDCustomerSpecialStoreDetailsResponse();
      //        objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CustomerSpecialPriceDetails");
      //        objResponse.ExceptionMessage = ex.Message;
      //        objResponse.StackTrace = ex.StackTrace;
      //    }
      //    return objResponse;
      //}

      public SelectByIDCustomerSpecialCategoryResponse SelectByIDCustomerSpecialCategoryDetails(SelectByIDCustomerSpecialCategoryRequest objRequest)
      {
          SelectByIDCustomerSpecialCategoryResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var CustomerSpecialCategoryDetails = objFactory.GetDALRepository().GetCustomerSpecialPriceMasterrDAL();
              objResponse = (SelectByIDCustomerSpecialCategoryResponse)CustomerSpecialCategoryDetails.SelectByIDCustomerSpecialCategoryDetails(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new SelectByIDCustomerSpecialCategoryResponse();
              objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "CustomerSpecialPriceDetails");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }
    }
}
