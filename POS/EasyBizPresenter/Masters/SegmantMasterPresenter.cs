using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ISegmentMaster;
using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizResponse.Masters.SegmentationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class SegmantMasterPresenter
    {
       ISegmentMasterView _ISegmentMasterView;
       SegmentMasterBLL _SegmentMasterBLL = new SegmentMasterBLL();
       public SegmantMasterPresenter(ISegmentMasterView ViewObj)
        {
            _ISegmentMasterView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_ISegmentMasterView.SegmentName.Trim() == string.Empty)
           {
               _ISegmentMasterView.Message = "SegmentName is missing Please Enter it.";
           }

          
           else
           {
               objBool = true;
           }
           return objBool;
       }
       public void SaveSegmentationMasterView()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveSegmentRequest();
                   RequestData.SegmentationRecord = new SegmentMaster();

                   RequestData.SegmentationRecord.ID = _ISegmentMasterView.ID;
                   RequestData.SegmentationRecord.SegmentName = _ISegmentMasterView.SegmentName;
                   RequestData.SegmentationRecord.MaxLength = _ISegmentMasterView.MaxLength;
                   RequestData.SegmentationRecord.Remarks = _ISegmentMasterView.Remarks;
                   RequestData.SegmentationRecord.Active = _ISegmentMasterView.Active;
                   RequestData.SegmentationRecord.CreateBy = 1;

                   var ResponseData = _SegmentMasterBLL.SaveSegementMaster(RequestData);
                   _ISegmentMasterView.Message = ResponseData.DisplayMessage;
                   _ISegmentMasterView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _ISegmentMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }


       }
       public void UpdateSegmentationMasterView()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new UpdateSegmentRequest();
                   RequestData.SegmentMasterData = new SegmentMaster();
                   RequestData.SegmentMasterData.ID = _ISegmentMasterView.ID;      
                   RequestData.SegmentMasterData.SegmentName = _ISegmentMasterView.SegmentName;                  
                   RequestData.SegmentMasterData.MaxLength = _ISegmentMasterView.MaxLength;
                   RequestData.SegmentMasterData.Remarks = _ISegmentMasterView.Remarks;
                   RequestData.SegmentMasterData.Active = _ISegmentMasterView.Active;
                   RequestData.SegmentMasterData.UpdateBy = 1;
                   RequestData.SegmentMasterData.SCN = _ISegmentMasterView.SCN;

                   var ResponseData = _SegmentMasterBLL.UpdateSegmentMaster(RequestData);
                   _ISegmentMasterView.Message = ResponseData.DisplayMessage;
                   _ISegmentMasterView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _ISegmentMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public void SelectBySegmentID()
       {
           try
           {
               var RequestData = new SelectBySegmentIDRequest();
               RequestData.ID = _ISegmentMasterView.ID;
               var ResponseData = _SegmentMasterBLL.SelectByIDSegmentMaster(RequestData);
               _ISegmentMasterView.SegmentName = ResponseData.SegmentMasterRecord.SegmentName;
               _ISegmentMasterView.MaxLength = ResponseData.SegmentMasterRecord.MaxLength;
               _ISegmentMasterView.Remarks = ResponseData.SegmentMasterRecord.Remarks;
               _ISegmentMasterView.Active = ResponseData.SegmentMasterRecord.Active;
               _ISegmentMasterView.SCN = ResponseData.SegmentMasterRecord.SCN;

               if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _ISegmentMasterView.Message = ResponseData.DisplayMessage;
               }

               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _ISegmentMasterView.Message = ResponseData.DisplayMessage;
               }

               _ISegmentMasterView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }


       public void DeleteSegmentationMasterView()
       {
           try
           {
               var RequestData = new DeleteSegmentRequest();
               RequestData.ID = _ISegmentMasterView.ID;
               var ResponseData = _SegmentMasterBLL.DeleteSegmentMaster(RequestData);
               _ISegmentMasterView.Message = ResponseData.DisplayMessage;
               _ISegmentMasterView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }

   public class SegmentTypeListPresenter
   {
       SegmentMasterBLL _SegmentMasterBLL = new SegmentMasterBLL();
       ISegmentMasterViewList _ISegmentMasterViewList;
       public SegmentTypeListPresenter(ISegmentMasterViewList ViewObj)
       {
           _ISegmentMasterViewList = ViewObj;
       }


       public void GetSegmentationMasterList()
       {
           var RequestData = new SelectAllSegmentRequest();
           RequestData.ShowInActiveRecords = true;
           var ResponseData = new SelectAllSegmentResponse();
           ResponseData = _SegmentMasterBLL.SelectAllSegmentMaster(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _ISegmentMasterViewList.SegmentMasterTypesList = ResponseData.SegmentMasterList;
           }
           else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
           {

           }
       }
      
   }
}
