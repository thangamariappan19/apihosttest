using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Masters.ISubSeason;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.SubSeasonMasterRequest;
using EasyBizResponse.Masters.SeasonResponse;
using EasyBizResponse.Masters.SubSeasonMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class SubSeasonMasterPresenter
    {
       ISubSeasonMasterView _ISubSeasonMasterView;
       SubSeasonBLL _SubSeasonBLL = new SubSeasonBLL();
       SeasonBLL _SeasonBLL = new SeasonBLL();
       public SubSeasonMasterPresenter(ISubSeasonMasterView ViewObj)
        {
            _ISubSeasonMasterView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_ISubSeasonMasterView.SubSeasonList.Count == 0)
           {
               _ISubSeasonMasterView.Message = "Please Enter SubSeason Details";
           }
           else if (_ISubSeasonMasterView.SeasonName.Trim() == string.Empty)
           {
               _ISubSeasonMasterView.Message = "Please Select SeasonName";
           }
           else
           {
               objBool = true;
           }
           return objBool;
       }
       public void SaveSubSeason()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveSubSeasonRequest();
                   RequestData.SubSeasonlist = _ISubSeasonMasterView.SubSeasonList;
                   var ResponseData = _SubSeasonBLL.SaveSubSeason(RequestData);
                   _ISubSeasonMasterView.Message = ResponseData.DisplayMessage;
                   _ISubSeasonMasterView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _ISubSeasonMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteSubSeason()
       {
           try
           {
               var RequestData = new DeleteSubSeasonRequest();
               RequestData.ID = _ISubSeasonMasterView.ID;
               var ResponseData = _SubSeasonBLL.DeleteSubSeason(RequestData);
               _ISubSeasonMasterView.Message = ResponseData.DisplayMessage;
               _ISubSeasonMasterView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void SelectSeasonListForSubSeason()
       {
           try
           {
               var RequestData = new SelectSeasonListForSubSeasonRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.SeasonID = _ISubSeasonMasterView.SeasonID;
               SelectSeasonListForSubSeasonResponse ResponseData = _SubSeasonBLL.SubSeasonBySeason(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ISubSeasonMasterView.SubSeasonList = ResponseData.SubSeasonList;
               }
               else
               {
                   _ISubSeasonMasterView.SubSeasonList =new List<EasyBizDBTypes.Masters.SubSeasonMaster>();
                   _ISubSeasonMasterView.Message = ResponseData.DisplayMessage;
                   _ISubSeasonMasterView.ProcessStatus = ResponseData.StatusCode;
               }
           }
           catch (Exception ex)
           {
               throw ex;
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
                   _ISubSeasonMasterView.SeasonLookUp = ResponseData.SeasonList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
   public class SubSeasonListPresenter
   {
       ISubSeasonMasterCollectionView _ISubSeasonMasterCollectionView;
       SeasonBLL _SeasonBLL = new SeasonBLL();

       public SubSeasonListPresenter(ISubSeasonMasterCollectionView ViewObj)
       {
           _ISubSeasonMasterCollectionView = ViewObj;
       }

       public void GetSeasonList()
       {
           try
           {
               var RequestData = new SelectAllSeasonRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = new SelectAllSeasonResponse();
               ResponseData = _SeasonBLL.SelectAllSeasonMaster(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ISubSeasonMasterCollectionView.SeasonList = ResponseData.SeasonMasterList;
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
   } 
}
