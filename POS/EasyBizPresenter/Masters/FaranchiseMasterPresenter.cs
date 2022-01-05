using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IFranchise;
using EasyBizRequest.Masters.FranchiseRequest;
using EasyBizResponse.Masters.FranchiseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class FaranchiseMasterPresenter
    {
       IFranchiseView _IFranchiseView;
       FranchiseBLL _FranchiseBLL = new FranchiseBLL();
       public FaranchiseMasterPresenter(IFranchiseView ViewObj)
        {
            _IFranchiseView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_IFranchiseView.FranchiseCode.Trim() == string.Empty)
           {
               _IFranchiseView.Message = "Franchise Code is missing Please Enter it.";
           }          
           else if (_IFranchiseView.franchiseName.Trim() == string.Empty)
           {
               _IFranchiseView.Message = "Franchise Name is missing Please Enter it.";
           }
           else
           {
               objBool = true;
           }
           return objBool;
       }
       public void SaveFranchise()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveFranchiseMasterRequest();
                   RequestData.FranchiseTypeData = new FranchiseType();

                   RequestData.FranchiseTypeData.ID = _IFranchiseView.ID;
                   RequestData.FranchiseTypeData.FranchiseCode = _IFranchiseView.FranchiseCode;
                   RequestData.FranchiseTypeData.FranchiseName = _IFranchiseView.franchiseName;
                   RequestData.FranchiseTypeData.Remarks = _IFranchiseView.Remarks;
                   RequestData.FranchiseTypeData.CreateBy = _IFranchiseView.UserID;
                   RequestData.FranchiseTypeData.CreateOn = DateTime.Now;
                   RequestData.FranchiseTypeData.Active = _IFranchiseView.Active;
                   RequestData.FranchiseTypeData.SCN = _IFranchiseView.SCN;
                   var ResponseData = _FranchiseBLL.SaveFranchise(RequestData);
                   _IFranchiseView.Message = ResponseData.DisplayMessage;
                   _IFranchiseView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IFranchiseView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void UpdateFranchise()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new UpdateFranchiseMasterRequest();
                   RequestData.FranchiseTypeRecord = new FranchiseType();
                   RequestData.FranchiseTypeRecord.ID = _IFranchiseView.ID;
                   RequestData.FranchiseTypeRecord.FranchiseCode = _IFranchiseView.FranchiseCode;
                   RequestData.FranchiseTypeRecord.FranchiseName = _IFranchiseView.franchiseName;
                   RequestData.FranchiseTypeRecord.Remarks = _IFranchiseView.Remarks;
                   RequestData.FranchiseTypeRecord.UpdateBy = _IFranchiseView.UserID;
                   RequestData.FranchiseTypeRecord.UpdateOn = DateTime.Now;
                   RequestData.FranchiseTypeRecord.Active = _IFranchiseView.Active;
                   RequestData.FranchiseTypeRecord.SCN = _IFranchiseView.SCN;
                   var ResponseData = _FranchiseBLL.UpdateFranchise(RequestData);
                   _IFranchiseView.Message = ResponseData.DisplayMessage;
                   _IFranchiseView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IFranchiseView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SelectFranchiseRecord()
       {
           try
           {
               var RequestData = new SelectByIDFranchiseRequest();
               RequestData.ID = _IFranchiseView.ID;
               var ResponseData = _FranchiseBLL.SelectFranchiseRecord(RequestData);
               _IFranchiseView.FranchiseCode = ResponseData.FranchiseTypeRecord.FranchiseCode;
               _IFranchiseView.franchiseName = ResponseData.FranchiseTypeRecord.FranchiseName;
               _IFranchiseView.Remarks = ResponseData.FranchiseTypeRecord.Remarks;  
               _IFranchiseView.Active = ResponseData.FranchiseTypeRecord.Active;
               _IFranchiseView.SCN = ResponseData.FranchiseTypeRecord.SCN;

               if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _IFranchiseView.Message = ResponseData.DisplayMessage;
               }
               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _IFranchiseView.Message = ResponseData.DisplayMessage;
               }
               _IFranchiseView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
   public class FranchiseMasterListPresenter
   {

       FranchiseBLL _FranchiseBLL = new FranchiseBLL();
       IFranchiseCollection _IFranchiseCollection;
       public FranchiseMasterListPresenter(IFranchiseCollection ViewObj)
       {
           _IFranchiseCollection = ViewObj;
       }
       public void GetFranchiseList()
       {
           try
           {
               var RequestData = new SelectAllFranchiseMasterRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllfranchiseResponse();
               ResponseData = _FranchiseBLL.SelectAllFranchise(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IFranchiseCollection.FranchiseTypeList = ResponseData.FranchiseTypeList;
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
