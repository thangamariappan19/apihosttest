using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IFreight;
using EasyBizRequest.Masters.FreightMasterRequest;
using EasyBizResponse.Masters.FreightMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class FreightMasterPresenter
    {
         IFreightMasterView _IFreightMasterView;
         FreightMasterBLL _FreightMasterBLL = new FreightMasterBLL();

        public FreightMasterPresenter(IFreightMasterView ViewObj)
        {
            _IFreightMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IFreightMasterView.FreightCode.Trim() == string.Empty)
            {
                _IFreightMasterView.Message = "FreightCode is missing Please Enter it.";
            }
            else if (_IFreightMasterView.FreightName.Trim() == string.Empty)
            {
                _IFreightMasterView.Message = "Please Enter FreightName";
            }
            //else if (_IFreightMasterView.Description.Trim() == string.Empty)
            //{
            //    _IFreightMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveFreightMaster()
        {
            try

        {
            if (IsValidForm())
            {
                var RequestData = new SaveFreightMasterRequest();
                RequestData.FreightMasterData = new FreightMaster();
                RequestData.FreightMasterData.ID = _IFreightMasterView.ID;
                RequestData.FreightMasterData.FreightCode = _IFreightMasterView.FreightCode;
                RequestData.FreightMasterData.FreightName = _IFreightMasterView.FreightName;                
                RequestData.FreightMasterData.Description = _IFreightMasterView.Description;
                RequestData.FreightMasterData.CreateBy = _IFreightMasterView.UserID;     
                //RequestData.FreightMasterData.CreateBy = _IFreightMasterView.CreateBy;                               
               // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
              //  RequestData.ProductLineMasterData.Active = true;
                RequestData.FreightMasterData.SCN = _IFreightMasterView.SCN;
                RequestData.FreightMasterData.Remarks = _IFreightMasterView.Remarks;
                RequestData.FreightMasterData.Active = _IFreightMasterView.Active;    
                SaveFreightMasterResponse ResponseData = _FreightMasterBLL.SaveFreightMaster(RequestData);
                _IFreightMasterView.Message = ResponseData.DisplayMessage;
                _IFreightMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IFreightMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFreightMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateFreightMasterRequest();
                RequestData.FreightMasterData = new FreightMaster();
                RequestData.FreightMasterData.ID = _IFreightMasterView.ID;
                RequestData.FreightMasterData.FreightCode = _IFreightMasterView.FreightCode;
                RequestData.FreightMasterData.FreightName = _IFreightMasterView.FreightName;                
                RequestData.FreightMasterData.Description = _IFreightMasterView.Description;
                RequestData.FreightMasterData.UpdateBy = _IFreightMasterView.UserID;
                RequestData.FreightMasterData.Remarks = _IFreightMasterView.Remarks;
                RequestData.FreightMasterData.Active = _IFreightMasterView.Active;     
                //RequestData.FreightMasterData.UpdateOn = DateTime.Now;
                //RequestData.FreightMasterData.Active = true;
                RequestData.FreightMasterData.SCN = _IFreightMasterView.SCN;
                var ResponseData = _FreightMasterBLL.UpdateFreightMaster(RequestData);
                _IFreightMasterView.Message = ResponseData.DisplayMessage;
                _IFreightMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IFreightMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFreightMaster()
        {
            try
        {

            var RequestData = new DeleteFreightMasterRequest ();
            RequestData.ID = _IFreightMasterView.ID;
            var ResponseData = _FreightMasterBLL.DeleteFreightMaster(RequestData);
            _IFreightMasterView.Message = ResponseData.DisplayMessage;
            _IFreightMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectFreightMasterRecord()
        {


            var RequestData = new SelectByIDFreightMasterRequest();
            RequestData.ID = _IFreightMasterView.ID;

            var ResponseData = _FreightMasterBLL.SelectFreightMasterRecord(RequestData);
            _IFreightMasterView.FreightCode = ResponseData.FreightMasterRecord.FreightCode;
            _IFreightMasterView.FreightName = ResponseData.FreightMasterRecord.FreightName;
            _IFreightMasterView.Description = ResponseData.FreightMasterRecord.Description;
            _IFreightMasterView.SCN = ResponseData.FreightMasterRecord.SCN;
            _IFreightMasterView.Remarks = ResponseData.FreightMasterRecord.Remarks;
            _IFreightMasterView.Active = ResponseData.FreightMasterRecord.Active;
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IFreightMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IFreightMasterView.Message = ResponseData.DisplayMessage;
            }

            _IFreightMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class FreightMasterListPresenter
    {

        FreightMasterBLL _FreightMasterBLL = new FreightMasterBLL();
        
        IFreightMasterList _IFreightMasterList;

        public FreightMasterListPresenter(IFreightMasterList ViewObj)
        {
            _IFreightMasterList = ViewObj;
        }

        public void GetFreightMasterList()
        {

            var RequestData = new SelectAllFreightMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllFreightMasterResponse();
            ResponseData = _FreightMasterBLL.SelectAllFreightMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IFreightMasterList.FreightMasterList = ResponseData.FreightMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }    
    }
}
