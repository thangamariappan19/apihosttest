using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizIView.Transactions.IPromotion;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Transactions.Promotions.PromotionMappingRequest;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Transactions.Promotions.PromotionMappingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Promotion
{
   public class PromotionMappingPresenter
    {
       IPromotionMappingView _IPromotionMappingView;
       CountryBLL _CountryBLL = new CountryBLL();
       PromotionMappingBLL _PromotionMappingBLL = new PromotionMappingBLL();
       StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
       WNPromotionBLL _WNPromotionBLL = new WNPromotionBLL();

       public PromotionMappingPresenter(IPromotionMappingView ViewObj)
        {
            _IPromotionMappingView = ViewObj;
        }
       public void GetAllMappingRecord()
       {
           try
           {
               var RequestData = new SelectAllPromotionMappingRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.WNPromotionID = _IPromotionMappingView.WNPromotionID;
               RequestData.Countries = _IPromotionMappingView.Countries;
               var ResponseData = new SelectAllPromotionMappingResponse();
               ResponseData = _PromotionMappingBLL.SelectAllPromotionMapping(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IPromotionMappingView.PromotionMappingList = ResponseData.PromotionMappingList;
               }
               else
               {

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetCountryLookUP()
       {
           SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
           RequestData.ShowInActiveRecords = false;
           SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IPromotionMappingView.CountryMasterLookUp = ResponseData.CountryMasterList;
           }
       }
       public void GetPromotionMappingLookUp()
       {
           try
           {
               var RequestData = new SelectWNPromotionLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _WNPromotionBLL.WNPromotionLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IPromotionMappingView.WNPromotionLookUp = ResponseData.WNPromotionList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void SavePromotionMapping()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SavePromotionMappingRequest();
                   RequestData.PromotionMappingList = _IPromotionMappingView.MappingList;
                   var ResponseData = _PromotionMappingBLL.SavePromotionMapping(RequestData);
                   _IPromotionMappingView.Message = ResponseData.DisplayMessage;
                   _IPromotionMappingView.ProcessStatus = ResponseData.StatusCode;

                   if (_IPromotionMappingView.ProcessStatus == Enums.OpStatusCode.Success)
                   {
                       GetAllMappingRecord();
                   }
               }
               else
               {
                   _IPromotionMappingView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       private bool IsValidForm()
       {
           bool ObjBool = false;
           if (_IPromotionMappingView.WNPromotionID == 0)
           {
               _IPromotionMappingView.Message = "Please Select Atleast One WNPromotion!";
           }
           else if (_IPromotionMappingView.MappingList.Count == 0)
           {
               _IPromotionMappingView.Message = "Please Select atleast one Store !";
           }
           else
           {
               ObjBool = true;
           }
           return ObjBool;
       }
    }
}
