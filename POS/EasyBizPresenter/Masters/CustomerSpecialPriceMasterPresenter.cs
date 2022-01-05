using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ICustomerSpecialPriceMaster;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
  public  class CustomerSpecialPriceMasterPresenter
    {
    
      ICustomerSpecialPriceMasterView _ICustomerSpecialPriceMasterView;
      CustomerSpecialPriceMasterBLL _CustomerSpecialPriceMasterBLL = new CustomerSpecialPriceMasterBLL();
      PriceListBLL _PriceListBLL = new PriceListBLL();
      StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
      YearBLL _YearBLL = new YearBLL();
      CustomerGroupBLL _CustomerGroupMasterBLL = new CustomerGroupBLL();
      CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();
      AFSegamationMasterBLL _AFSegamationMasterBLL = new AFSegamationMasterBLL();
      BrandBLL _BrandBLL = new BrandBLL();
      SeasonBLL _SeasonBLL = new SeasonBLL();
      ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();
      SubBrandBLL _SubBrandBLL = new SubBrandBLL();
      ProductSubGroupBLL _ProductSubGroupBLL = new ProductSubGroupBLL();
      StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
      List<CommonUtil> _CommonUtil = new List<CommonUtil>();
     
      //ICustomerSpecialPriceMasterView _ICustomerSpecialPriceMaster;
      public CustomerSpecialPriceMasterPresenter(ICustomerSpecialPriceMasterView ViewObj)
        {
            _ICustomerSpecialPriceMasterView = ViewObj;
        }
      public void GetCustomerMasterLookUp()
      {
          var RequestData = new EasyBizRequest.Masters.CustomerMasterRequest.SelectCustomerMasterLookUpRequest();
          RequestData.ShowInActiveRecords = false;
          var ResponseData = _CustomerMasterBLL.SelectCustomerMasterLookUp(RequestData);
          if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
          {
              _ICustomerSpecialPriceMasterView.CustomerMasterLookUp = ResponseData.CustomerMasterList;
          }
      }
      public void GetPriceLookUp()
      {
          var RequestData = new SelectPriceListLookUPRequest();
          RequestData.ShowInActiveRecords = false;
          var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
          if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
          {
              _ICustomerSpecialPriceMasterView.PriceListLookUp = ResponseData.PriceListTypeData;
          }
      }
      public void GetCustomerGroupLookUp()
      {
          try
          { 
              var RequestData = new EasyBizRequest.Masters.CustomerGroupRequest.SelectCustomerGroupLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _CustomerGroupMasterBLL.SelectCustomerGroupLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.CustomerGroupLookUp = ResponseData.CustomerGroupMasterList;
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }

         }

      public void GetCustomerList()
      {
          try
          {
              var RequestData = new EasyBizRequest.Masters.CustomerGroupRequest.SelectCustomerGroupLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _CustomerGroupMasterBLL.SelectCustomerGroupLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.CustomerGroupLookUp = ResponseData.CustomerGroupMasterList;
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      public void GetGroupBasedCustomerList()
      {
          try
          {
              var RequestData = new EasyBizRequest.Masters.CustomerMasterRequest.SelectCustomerMasterLookUpRequest();
              RequestData.ID = _ICustomerSpecialPriceMasterView.CustomerGroupID;
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _CustomerMasterBLL.SelectCustomerMasterLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.CustomerMasterList = ResponseData.CustomerMasterList;
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }
      public void GetSegamationLookUp()
      {
          try
          {
              var RequestData = new SelectAFsegmentationLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _AFSegamationMasterBLL.SelectSegmentationLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.SegamationMasterList = ResponseData.AFSegmentationMaster;

              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }
      }
      public void GetYearLookUp()
      {
          try
          {
              var RequestData = new SelectYearLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _YearBLL.YearLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.YearList = ResponseData.YearList;
              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }
      }
      public void GetBrandCodeLookUp()
      {
          try
          {
              var RequestData = new SelectBrandLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _BrandBLL.BrandLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.BrandList = ResponseData.BrandList;
              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }
      }
      public void GetSeasonLookUp()
      {
          try
          {
              var RequestData = new SelectSeasonLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _SeasonBLL.SelectSeasonLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.SeasonList = ResponseData.SeasonList;
              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }
      }
      public void GetStoreList()
      {
          var RequestData = new SelectStoreGroupLookUpRequest();
          RequestData.ShowInActiveRecords = true;

          var ResponseData = _StoreGroupBLL.SelectStoreGroupMasterLookUp(RequestData);
          if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
          {
              _ICustomerSpecialPriceMasterView.StoreGroupMasterList = ResponseData.StoreGroupMasterList;
          }
      }
      public void GetProductGroupLookUp()
      {
          try
          {
              var RequestData = new SelectProductGroupLookUpRequest();
              RequestData.ShowInActiveRecords = false;
              var ResponseData = _ProductGroupBLL.ProductGroupLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.ProductGroupList = ResponseData.ProductGroupList;
              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }
      }
      public void GetSubBrandLookUp()
      {
          try
          {
              var RequestData = new SelectSubBrandLookUpRequest();
              RequestData.ShowInActiveRecords = true;
              //RequestData.BrandID = _IPromotionsView.BrandID;
              var ResponseData = _SubBrandBLL.SubBrandLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.SubBrandMasterList = ResponseData.SubBrandList;
              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }

      }
      public void GetProductSubGroupLookUp()
      {
          try
          {
              var RequestData = new SelectProductSubGroupLookUpRequest();
              RequestData.ShowInActiveRecords = true;
              //RequestData.ProductGroupID = _IPromotionsView.ProductGroupID;
              var ResponseData = _ProductSubGroupBLL.ProductSubGroupLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.ProductSubGroupList = ResponseData.ProductSubGroupList;
              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }
      }
      public void GetStoreMasterList()
      {
          try
          {
              var RequestData = new SelectStoreMasterLookUpRequest();
              RequestData.ShowInActiveRecords = true;
              //var ResponseData = new SelectAllStoreMasterResponse();
              var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.StoreMasterList = ResponseData.StoreMasterList;
              }
              else
              {

              }
          }
          catch (Exception ex)
          {
              //throw ex;
          }

      }
      public void SaveAgent()
      {
          try
          {
              if (IsValidForm())
              {


                  var RequestData = new SaveCustomerSpecialPriceMasterRequest();
                  RequestData.CustomerSpecialPriceMasterRecord = new CustomerSpecialPriceMasterTypes();
                  //RequestData.CustomerSpecialPriceMasterList = _ICustomerSpecialPriceMasterView.StoreGroupMasterList;

                

                  RequestData.CustomerMasterSpecialPriceMasterList = _ICustomerSpecialPriceMasterView.CustomerMasterList;
                  RequestData.CustomerSpecialPriceMasterRecord.ID = _ICustomerSpecialPriceMasterView.ID;
                  RequestData.CustomerSpecialPriceMasterRecord.ApplicablePriceList = _ICustomerSpecialPriceMasterView.PriceListID;
                  RequestData.CustomerSpecialPriceMasterRecord.DateFrom = _ICustomerSpecialPriceMasterView.DateFrom;
                  RequestData.CustomerSpecialPriceMasterRecord.DateTo = _ICustomerSpecialPriceMasterView.DateTo;
                  RequestData.CustomerSpecialPriceMasterRecord.CustomerGroup = _ICustomerSpecialPriceMasterView.CustomerGroupID;
                  RequestData.CustomerSpecialPriceMasterRecord.DiscountType = _ICustomerSpecialPriceMasterView.DiscountType;
                  RequestData.CustomerSpecialPriceMasterRecord.DiscountValue = _ICustomerSpecialPriceMasterView.DiscountValue;
                  // RequestData.CustomerSpecialPriceMasterRecord.CreateBy = _ICustomerSpecialPriceMasterView.CreateBy;
                  RequestData.CustomerSpecialPriceMasterRecord.CustomerGroupUsed = _ICustomerSpecialPriceMasterView.CustomerGroupUsed;
                  RequestData.CustomerSpecialPriceMasterRecord.CustomerMasterUsed = _ICustomerSpecialPriceMasterView.CustomerMasterUsed;
                  RequestData.CustomerSpecialPriceMasterRecord.CreateOn = DateTime.Now;
                  RequestData.CustomerSpecialPriceMasterRecord.Active = _ICustomerSpecialPriceMasterView.Active;
                  //RequestData.CustomerSpecialPriceMasterRecord.SCN = _ICustomerSpecialPriceMasterView.SCN;

                  RequestData.CategoryCommonUtil = _ICustomerSpecialPriceMasterView.CategoryCommonUtil;
                  RequestData.StoreCommonUtil = _ICustomerSpecialPriceMasterView.StoreCommonUtil;
                  var ResponseData = _CustomerSpecialPriceMasterBLL.SaveCustomerSpecialPriceMaster(RequestData);
                  _ICustomerSpecialPriceMasterView.Message = ResponseData.DisplayMessage;
                  _ICustomerSpecialPriceMasterView.ProcessStatus = ResponseData.StatusCode;
              }
              else
              {
                  _ICustomerSpecialPriceMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
              }
          }


          catch (Exception ex)
          {
              throw ex;
          }
      }


               public void SelectByIDCustomerSpecialPriceMaster()
      {
          try
          {
              var RequestData = new SelectByIDCustomerSpecialPriceMasterRequest();
              RequestData.ID = _ICustomerSpecialPriceMasterView.ID;
              var ResponseData = _CustomerSpecialPriceMasterBLL.SelectByIDCustomerSpecialPriceMaster(RequestData);
              _ICustomerSpecialPriceMasterView.PriceListID = ResponseData.CustomerSpecialPriceMasterRecord.ApplicablePriceList;
              _ICustomerSpecialPriceMasterView.DateFrom = ResponseData.CustomerSpecialPriceMasterRecord.DateFrom;
              _ICustomerSpecialPriceMasterView.DateTo = ResponseData.CustomerSpecialPriceMasterRecord.DateTo;
              _ICustomerSpecialPriceMasterView.CustomerGroupID = ResponseData.CustomerSpecialPriceMasterRecord.CustomerGroup;
              _ICustomerSpecialPriceMasterView.DiscountType = ResponseData.CustomerSpecialPriceMasterRecord.DiscountType;
              _ICustomerSpecialPriceMasterView.DiscountValue = ResponseData.CustomerSpecialPriceMasterRecord.DiscountValue;
              _ICustomerSpecialPriceMasterView.Active = ResponseData.CustomerSpecialPriceMasterRecord.Active;
              _ICustomerSpecialPriceMasterView.CustomerGroupUsed = ResponseData.CustomerSpecialPriceMasterRecord.CustomerGroupUsed;
              _ICustomerSpecialPriceMasterView.CustomerMasterUsed = ResponseData.CustomerSpecialPriceMasterRecord.CustomerMasterUsed;
              _ICustomerSpecialPriceMasterView.SCN = ResponseData.CustomerSpecialPriceMasterRecord.SCN;

              if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
              {
                  _ICustomerSpecialPriceMasterView.Message = ResponseData.DisplayMessage;
              }

              else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
              {
                  _ICustomerSpecialPriceMasterView.Message = ResponseData.DisplayMessage;
              }
              else if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterView.ProcessStatus = ResponseData.StatusCode;
              }
             
          }
          catch (Exception ex)
          {
              throw ex;
          }

      }

               public void SelectDetails()
               {
                   try
                   {
                       var RequestData = new SelectByIDCustomerSpecialCategoryRequest();
                       int Count = Enum.GetNames(typeof(Enums.SpecialPriceRecordType)).Length;
                       for (int i = 1; i <= Count; i++)
                       {
                           RequestData.ShowInActiveRecords = false;
                           RequestData.ID = _ICustomerSpecialPriceMasterView.ID;
                           RequestData.DetailsType = (Enums.SpecialPriceRecordType)Enum.ToObject(typeof(Enums.SpecialPriceRecordType), i);
                       
                           var ResponseData = _CustomerSpecialPriceMasterBLL.SelectByIDCustomerSpecialCategoryDetails(RequestData);

                           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                           {
                               if ((int)Enums.SpecialPriceRecordType.Store == i)
                               {
                                   _ICustomerSpecialPriceMasterView.StoreCommonUtil = ResponseData.CustomerSpecialPriceCategoryRecord;
                               }

                               else if ((int)Enums.SpecialPriceRecordType.Category == i)
                               {
                                   _ICustomerSpecialPriceMasterView.CategoryCommonUtil = ResponseData.CustomerSpecialPriceCategoryRecord;
                               }
                              
                           }
                           else
                           {
                               _ICustomerSpecialPriceMasterView.ProcessStatus = ResponseData.StatusCode;
                           }


                       }
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }

               }
           public void SelectByIDCustomerSpecialPriceDetails()
           {
               try
               {
                   var RequestData = new SelectByCustomerSpecialPriceDetailsRequest();
                   RequestData.ShowInActiveRecords = false;
                   RequestData.ID = _ICustomerSpecialPriceMasterView.ID;
                   var ResponseData = _CustomerSpecialPriceMasterBLL.SelectByCustomerSpecialPriceDetails(RequestData);

                   if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                   {
                       _ICustomerSpecialPriceMasterView.CustomerMasterList = ResponseData.CustomerSpecialCustomerRecord;
                   }

                   else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                   {
                       _ICustomerSpecialPriceMasterView.Message = ResponseData.DisplayMessage;
                   }

                   _ICustomerSpecialPriceMasterView.ProcessStatus = ResponseData.StatusCode;
               }
               catch (Exception ex)
               {
                   throw ex;
               }

           }
           //public void SelectByIDCustomerSpecialStoreDetails()
           //{
           //    try
           //    {
           //        var RequestData = new SelectByIDCustomerSpecialCategoryRequest();
           //        RequestData.ShowInActiveRecords = false;
           //        RequestData.ID = _ICustomerSpecialPriceMasterView.ID;
           //        RequestData.DetailsType = _ICustomerSpecialPriceMasterView.DetailsType;
           //        RequestData.Type = _ICustomerSpecialPriceMasterView.CategoryType;
           //        var ResponseData = _CustomerSpecialPriceMasterBLL.SelectByIDCustomerSpecialCategoryDetails(RequestData);

           //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           //        {
           //            _ICustomerSpecialPriceMasterView.StoreCommonUtil = ResponseData.CustomerSpecialPriceCategoryRecord;
           //        }

           //        else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
           //        {
           //            _ICustomerSpecialPriceMasterView.Message = ResponseData.DisplayMessage;
           //        }

           //        _ICustomerSpecialPriceMasterView.ProcessStatus = ResponseData.StatusCode;
           //    }
           //    catch (Exception ex)
           //    {
           //        throw ex;
           //    }

           //}
           //public void SelectByIDCustomerSpecialCategoryDetails()
           //{
           //    try
           //    {
           //        var RequestData = new SelectByIDCustomerSpecialCategoryRequest();
           //        RequestData.ShowInActiveRecords = false;
           //        RequestData.ID = _ICustomerSpecialPriceMasterView.ID;
           //        RequestData.DetailsType = _ICustomerSpecialPriceMasterView.DetailsType;
           //        RequestData.Type = _ICustomerSpecialPriceMasterView.CategoryType;
           //        var ResponseData = _CustomerSpecialPriceMasterBLL.SelectByIDCustomerSpecialCategoryDetails(RequestData);
                  
           //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           //        {
           //             _ICustomerSpecialPriceMasterView.CategoryCommonUtil = ResponseData.CustomerSpecialPriceCategoryRecord;
           //        }

           //        else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
           //        {
           //            _ICustomerSpecialPriceMasterView.Message = ResponseData.DisplayMessage;
           //        }

           //        _ICustomerSpecialPriceMasterView.ProcessStatus = ResponseData.StatusCode;
           //    }
           //    catch (Exception ex)
           //    {
           //        throw ex;
           //    }

           //}
          
           public bool IsValidForm()
           {
               bool objBool = false;
              
               if (_ICustomerSpecialPriceMasterView.DiscountValue >= 100 )
               {
                   _ICustomerSpecialPriceMasterView.Message = "Please set Percentage is 0 to 100.";
               }

               else if (_ICustomerSpecialPriceMasterView.PriceListID <= 0 || Convert.ToString(_ICustomerSpecialPriceMasterView.PriceListID) == String.Empty)
               {
                   _ICustomerSpecialPriceMasterView.Message = "PriceList is missing Please Enter it.";
               }
               //else if (_ICustomerSpecialPriceMasterView.CustomerGroupID <= 0 || Convert.ToString(_ICustomerSpecialPriceMasterView.CustomerGroupID) == String.Empty)
               //{
               //    _ICustomerSpecialPriceMasterView.Message = "CustomerGroup is missing Please Enter it.";
               //}

               else
               {
                   objBool = true;
               }
               return objBool;
           }
      }
    }
  public class CustomerSpecialPriceMasterPresenterList
  {
      ICustomerSpecialPriceMasterList _ICustomerSpecialPriceMasterViewList;
      CustomerSpecialPriceMasterBLL _CustomerSpecialPriceMasterBLL = new CustomerSpecialPriceMasterBLL();
      public CustomerSpecialPriceMasterPresenterList(ICustomerSpecialPriceMasterList ViewObj)
      {
          _ICustomerSpecialPriceMasterViewList = ViewObj;

      }

      public void SelectAllCustomerSpecialPriceMaster()
      {
          try
          {
              var RequestData = new SelectAllCustomerSpecialPriceMasterRequest();
              RequestData.ShowInActiveRecords = true;
              var ResponseData = new SelectAllCustomerSpecialPriceMasterResponse();

              ResponseData = _CustomerSpecialPriceMasterBLL.SelectAllCustomerSpecialPriceMaster(RequestData);

             
              if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
              {
                  _ICustomerSpecialPriceMasterViewList.CustomerSpecialPriceMasterList = ResponseData.CustomerSpecialPriceMasterTypesList;
              }
              else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
              {
                  _ICustomerSpecialPriceMasterViewList.CustomerSpecialPriceMasterList = ResponseData.CustomerSpecialPriceMasterTypesList;
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      

    
      
  }
      

