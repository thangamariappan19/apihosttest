using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IStyleStatusMaster;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class StyleStatusMasterPresenter
    {
        IStyleStatusView _IStyleStatusView;
        StyleStatusBLL _StyleStatusBLL=new StyleStatusBLL();

        public StyleStatusMasterPresenter(IStyleStatusView ViewObj)
        {
            _IStyleStatusView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IStyleStatusView.StyleStatusCode.Trim() == string.Empty)
            {
                _IStyleStatusView.Message = "Status Code is missing Please Enter it.";
            }
            else if (_IStyleStatusView.StyleStatusCode.Length > 8)
            {
                _IStyleStatusView.Message = " Please Enter Vail Code.";
            }
            else if (_IStyleStatusView.StatusName.Trim() == string.Empty)
            {
                _IStyleStatusView.Message = "Status Name is missing Please Enter it. ";
            }
         
            else
            {
                objBool = true;
            }
            return objBool;
        }



        public void SaveStyleStatusMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveStyleStatusMasterRequest();
                    RequestData.StyleStatusMasterTypeRecord = new StyleStatusMasterType();

                    RequestData.StyleStatusMasterTypeRecord.ID = _IStyleStatusView.ID;
                    RequestData.StyleStatusMasterTypeRecord.StyleStatusCode = _IStyleStatusView.StyleStatusCode;
                    RequestData.StyleStatusMasterTypeRecord.StatusName = _IStyleStatusView.StatusName;
                    RequestData.StyleStatusMasterTypeRecord.Remarks = _IStyleStatusView.Remarks;
                    RequestData.StyleStatusMasterTypeRecord.Active = _IStyleStatusView.Active;
                    RequestData.StyleStatusMasterTypeRecord.CreateBy = 1;

                    var ResponseData = _StyleStatusBLL.SaveStyleStatus(RequestData);

                    _IStyleStatusView.Message = ResponseData.DisplayMessage;
                    _IStyleStatusView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IStyleStatusView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
        

        public void UpdateStyleStatusMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateStyleStatusMasterRequest();
                    RequestData.StyleStatusMasterTypeRecord = new StyleStatusMasterType();
                    RequestData.StyleStatusMasterTypeRecord.ID = _IStyleStatusView.ID;
                    RequestData.StyleStatusMasterTypeRecord.StyleStatusCode = _IStyleStatusView.StyleStatusCode;
                    RequestData.StyleStatusMasterTypeRecord.StatusName = _IStyleStatusView.StatusName;
                    RequestData.StyleStatusMasterTypeRecord.UpdateBy = 1;
                    RequestData.StyleStatusMasterTypeRecord.SCN = _IStyleStatusView.SCN;
                    RequestData.StyleStatusMasterTypeRecord.Remarks = _IStyleStatusView.Remarks;
                    RequestData.StyleStatusMasterTypeRecord.Active = _IStyleStatusView.Active;
                    var ResponseData = _StyleStatusBLL.UpdateStyleStatus(RequestData);

                    _IStyleStatusView.Message = ResponseData.DisplayMessage;
                    _IStyleStatusView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IStyleStatusView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteStyleStatusMaster()
        {
            try
            {
                var RequestData = new DeleteStyleStatusMasterRequest();
                RequestData.ID = _IStyleStatusView.ID;
                var ResponseData = _StyleStatusBLL.DeleteStyleStatus(RequestData);

                _IStyleStatusView.Message = ResponseData.DisplayMessage;
                _IStyleStatusView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
              
           

        }

        public void SelectByIDStyleStatusMaster()
        {
            try
            {
                var RequestData = new SelectByIDStyleStatusMasterRequest();
                RequestData.ID = _IStyleStatusView.ID;
                var ResponseData = _StyleStatusBLL.SelectByIDStyleStatus(RequestData);
                _IStyleStatusView.StyleStatusCode = ResponseData.StyleStatusMasterTypeData.StyleStatusCode;
                _IStyleStatusView.StatusName = ResponseData.StyleStatusMasterTypeData.StatusName;
                _IStyleStatusView.SCN = ResponseData.StyleStatusMasterTypeData.SCN;
                _IStyleStatusView.Active = ResponseData.StyleStatusMasterTypeData.Active;
                _IStyleStatusView.Remarks = ResponseData.StyleStatusMasterTypeData.Remarks;


                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStyleStatusView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IStyleStatusView.Message = ResponseData.DisplayMessage;
                }

                _IStyleStatusView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


    }


    public class StyleStatusMasterViewListPresenter
    {
        IStyleStatusViewList _IStyleStatusViewList;
        StyleStatusBLL _StyleStatusBLL=new StyleStatusBLL();


        public StyleStatusMasterViewListPresenter(IStyleStatusViewList ViewObj)
        {
            _IStyleStatusViewList = ViewObj;
        }


        public void GetStyleStatusList()
        {
            try
            {
                var RequestData = new SelectAllStyleStatusMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStyleStatusMasterResponse();
                ResponseData = _StyleStatusBLL.SelectAllStyleStatus(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStyleStatusViewList.StyleStatusMasterTypeList = ResponseData.StyleStatusMasterTypeList;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IStyleStatusViewList.StyleStatusMasterTypeList = ResponseData.StyleStatusMasterTypeList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

    }
}
