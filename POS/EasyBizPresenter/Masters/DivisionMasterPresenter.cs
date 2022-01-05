using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IDivision;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizResponse.Masters.DivisionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
     
   public class DivisionMasterPresenter
    {
        IDivisionView _IDivisionView;
        DivisionBLL _DivisionBLL = new DivisionBLL();
        public DivisionMasterPresenter(IDivisionView ViewObj)
        {
            _IDivisionView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDivisionView.DivisionCode.Trim() == string.Empty)
            {
                _IDivisionView.Message = "Division Code is missing Please Enter it.";
            }
            //else if (_IDivisionView.DivisionCode.Length > 8)
            //{
            //    _IDivisionView.Message = " Division Code not allow more than eight Character.";
            //}
            else if (_IDivisionView.DivisionCode.Length < 1)
            {
                _IDivisionView.Message = " Division Code must have only one Character";
            }
            else if (_IDivisionView.DivisionName.Trim() == string.Empty)
            {
                _IDivisionView.Message = "Division Name is missing Please Enter it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveDivision()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveDivisionRequest();
                    RequestData.DivisionRecord = new DivisionMaster();

                    RequestData.DivisionRecord.ID = _IDivisionView.ID;
                    RequestData.DivisionRecord.DivisionCode = _IDivisionView.DivisionCode;
                    RequestData.DivisionRecord.DivisionName = _IDivisionView.DivisionName;
                    RequestData.DivisionRecord.CreateBy = _IDivisionView.UserID;
                    RequestData.DivisionRecord.CreateOn = DateTime.Now;
                    RequestData.DivisionRecord.Active = _IDivisionView.Active;
                    RequestData.DivisionRecord.SCN = _IDivisionView.SCN;
                    RequestData.DivisionRecord.Remarks = _IDivisionView.Remarks;
                    var ResponseData = _DivisionBLL.SaveDivision(RequestData);
                    _IDivisionView.Message = ResponseData.DisplayMessage;
                    _IDivisionView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IDivisionView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateDivision()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateDivisionRequest();
                    RequestData.DivisionRecord = new DivisionMaster();
                    RequestData.DivisionRecord.ID = _IDivisionView.ID;
                    RequestData.DivisionRecord.DivisionCode = _IDivisionView.DivisionCode;
                    RequestData.DivisionRecord.DivisionName = _IDivisionView.DivisionName;
                    RequestData.DivisionRecord.UpdateBy = _IDivisionView.UserID;
                    RequestData.DivisionRecord.UpdateOn = DateTime.Now;
                    RequestData.DivisionRecord.Active = _IDivisionView.Active;
                    RequestData.DivisionRecord.Remarks = _IDivisionView.Remarks;
                    RequestData.DivisionRecord.SCN = _IDivisionView.SCN;
                    var ResponseData = _DivisionBLL.UpdateDivision(RequestData);
                    _IDivisionView.Message = ResponseData.DisplayMessage;
                    _IDivisionView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IDivisionView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectDivisionRecord()
        {
            try
            {
                var RequestData = new SelectByDivisionIDRequest();
                RequestData.ID = _IDivisionView.ID;
                var ResponseData = _DivisionBLL.SelectDivisionRecord(RequestData);
                _IDivisionView.DivisionCode = ResponseData.DivisionRecord.DivisionCode;
                _IDivisionView.DivisionName = ResponseData.DivisionRecord.DivisionName;
                _IDivisionView.SCN = ResponseData.DivisionRecord.SCN;
                _IDivisionView.Remarks = ResponseData.DivisionRecord.Remarks;
                _IDivisionView.Active = ResponseData.DivisionRecord.Active;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IDivisionView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IDivisionView.Message = ResponseData.DisplayMessage;
                }
                _IDivisionView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteDivision()
        {
            try
            {
                var RequestData = new DeleteDivisionRequest();
                RequestData.ID = _IDivisionView.ID;
                var ResponseData = _DivisionBLL.DeleteDivision(RequestData);
                _IDivisionView.Message = ResponseData.DisplayMessage;
                _IDivisionView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
   public class DivisionMasterListPresenter
   {

      DivisionBLL _DivisionBLL = new DivisionBLL();
       IDivisionCollectionView _IDivisionCollectionView;
       public DivisionMasterListPresenter(IDivisionCollectionView ViewObj)
       {
           _IDivisionCollectionView = ViewObj;
       }
       public void GetDivisionList()
       {
           try
           {
               var RequestData = new SelectAllDivisionRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllDivisionResponse();
               ResponseData = _DivisionBLL.SelectAllDivision(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IDivisionCollectionView.DivisionList = ResponseData.DivisionList;
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
