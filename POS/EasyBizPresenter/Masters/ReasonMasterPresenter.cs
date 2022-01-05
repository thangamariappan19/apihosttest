using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IReason;
using EasyBizRequest.Masters.ReasonMasterRequest;
using EasyBizResponse.Masters.ReasonMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class ReasonMasterPresenter
    {     
        IReasonMasterView _IReasonMasterView;
         ReasonMasterBLL _ReasonMasterBLL = new ReasonMasterBLL();

        public ReasonMasterPresenter(IReasonMasterView ViewObj)
        {
            _IReasonMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IReasonMasterView.ReasonCode.Trim() == string.Empty)
            {
                _IReasonMasterView.Message = "ReasonCode is missing Please Enter it.";
            }
            else if (_IReasonMasterView.ReasonName.Trim() == string.Empty)
            {
                _IReasonMasterView.Message = "Please Enter ReasonName";
            }
            //else if (_IReasonMasterView.Description.Trim() == string.Empty)
            //{
            //    _IReasonMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveReasonMaster()
        {
            try
        {
            if (IsValidForm())
            {
                var RequestData = new SaveReasonMasterRequest();
                RequestData.ReasonMasterData = new ReasonMaster();

                RequestData.ReasonMasterData.ReasonID = _IReasonMasterView.ID;
                RequestData.ReasonMasterData.ReasonCode = _IReasonMasterView.ReasonCode;
                RequestData.ReasonMasterData.ReasonName = _IReasonMasterView.ReasonName;                
                RequestData.ReasonMasterData.Description = _IReasonMasterView.Description;
                RequestData.ReasonMasterData.CreateBy = _IReasonMasterView.UserID;
                RequestData.ReasonMasterData.Active = _IReasonMasterView.Active;
                //RequestData.ReasonMasterData.CreateBy = _IReasonMasterView.CreateBy;                               
               // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
              
                RequestData.ReasonMasterData.SCN = _IReasonMasterView.SCN;
                SaveReasonMasterResponse ResponseData = _ReasonMasterBLL.SaveReasonMaster(RequestData);
                _IReasonMasterView.Message = ResponseData.DisplayMessage;
                _IReasonMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IReasonMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateReasonMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateReasonMasterRequest();
                RequestData.ReasonMasterData = new ReasonMaster();
                RequestData.ReasonMasterData.ReasonID = _IReasonMasterView.ID;
                RequestData.ReasonMasterData.ReasonCode = _IReasonMasterView.ReasonCode;
                RequestData.ReasonMasterData.ReasonName = _IReasonMasterView.ReasonName;                
                RequestData.ReasonMasterData.Description = _IReasonMasterView.Description;
                RequestData.ReasonMasterData.UpdateBy = _IReasonMasterView.UserID;
                RequestData.ReasonMasterData.Active = _IReasonMasterView.Active;
                //RequestData.ReasonMasterData.UpdateOn = DateTime.Now;
                //RequestData.ReasonMasterData.Active = true;
                RequestData.ReasonMasterData.SCN = _IReasonMasterView.SCN;
                var ResponseData = _ReasonMasterBLL.UpdateReasonMaster(RequestData);
                _IReasonMasterView.Message = ResponseData.DisplayMessage;
                _IReasonMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IReasonMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteReasonMaster()
        {
            try
        {

            var RequestData = new DeleteReasonMasterRequest ();
            RequestData.ID = _IReasonMasterView.ID;
            var ResponseData = _ReasonMasterBLL.DeleteReasonMaster(RequestData);
            _IReasonMasterView.Message = ResponseData.DisplayMessage;
            _IReasonMasterView.ProcessStatus = ResponseData.StatusCode;
         
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectReasonMasterRecord()
        {


            var RequestData = new SelectByIDReasonMasterRequest();
            RequestData.ID = _IReasonMasterView.ID;

            var ResponseData = _ReasonMasterBLL.SelectReasonMasterRecord(RequestData);
            _IReasonMasterView.ReasonCode = ResponseData.ReasonMasterRecord.ReasonCode;
            _IReasonMasterView.ReasonName = ResponseData.ReasonMasterRecord.ReasonName;
            _IReasonMasterView.Description = ResponseData.ReasonMasterRecord.Description;
            _IReasonMasterView.Active = ResponseData.ReasonMasterRecord.Active;
            _IReasonMasterView.SCN = ResponseData.ReasonMasterRecord.SCN;
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IReasonMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IReasonMasterView.Message = ResponseData.DisplayMessage;
            }

            _IReasonMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class ReasonMasterListPresenter
    {

        ReasonMasterBLL _ReasonMasterBLL = new ReasonMasterBLL();

        IReasonMasterList _IReasonMasterList;

        public ReasonMasterListPresenter(IReasonMasterList ViewObj)
        {
            _IReasonMasterList = ViewObj;
        }

        public void GetReasonMasterList()
        {

            var RequestData = new SelectAllReasonMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllReasonMasterResponse();
            ResponseData = _ReasonMasterBLL.SelectAllReasonMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IReasonMasterList.ReasonMasterList = ResponseData.ReasonMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }
 
    }

