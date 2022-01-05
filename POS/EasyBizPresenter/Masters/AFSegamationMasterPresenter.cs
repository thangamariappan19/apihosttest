using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IAFSegamation;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizResponse.Masters.AFSegamationMasterResponse;
using EasyBizResponse.Masters.SegmentationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class AFSegamationMasterPresenter
    {
        IAFSegamationView _IAFSegamationView;
        AFSegamationMasterBLL _AFSegamationMasterBLL = new AFSegamationMasterBLL();
        SegmentMasterBLL _SegmentMasterBLL = new SegmentMasterBLL();
        public AFSegamationMasterPresenter(IAFSegamationView ViewObj)
        {
            _IAFSegamationView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;

            if (_IAFSegamationView.AFSegamationCode <= 0 || Convert.ToString(_IAFSegamationView.AFSegamationCode) == String.Empty)
            {
                _IAFSegamationView.Message = "SegmationCode is missing Please Enter it.";
            }
           
            else if (_IAFSegamationView.AFSegamationName.Trim() == string.Empty)
            { 
                _IAFSegamationView.Message = "Name is missing Please Enter it.";
               
            }
            else if (_IAFSegamationView.CodeLength > 50)
             {
                 _IAFSegamationView.Message = "Please Select The Code Below 50";
             }
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SaveAFSegamationView()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new SaveAFSegamationMasterRequest();
                    RequestData.AFSegamationMasterTypesRecord = new AFSegamationMasterTypes();
                    RequestData.AFSegmentationDetailMasterList = _IAFSegamationView.SegmentMasterList;
                    RequestData.AFSegamationMasterTypesRecord.ID = _IAFSegamationView.ID;                
                    RequestData.AFSegamationMasterTypesRecord.AFSegamationCode = _IAFSegamationView.AFSegamationCode;
                    RequestData.AFSegamationMasterTypesRecord.AFSegamationName = _IAFSegamationView.AFSegamationName;
                    RequestData.AFSegamationMasterTypesRecord.Remarks = _IAFSegamationView.Remarks;
                    RequestData.AFSegamationMasterTypesRecord.CodeLength = _IAFSegamationView.CodeLength;
                    RequestData.AFSegamationMasterTypesRecord.UseSeperator = _IAFSegamationView.UseSeperator;
                    RequestData.AFSegamationMasterTypesRecord.Active = _IAFSegamationView.Active;
                    RequestData.AFSegamationMasterTypesRecord.CreateBy = 1;                    

                    var ResponseData = _AFSegamationMasterBLL.SaveAFSegamationMaster(RequestData);
                    _IAFSegamationView.Message = ResponseData.DisplayMessage;
                    _IAFSegamationView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IAFSegamationView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void UpdateAFSegamationView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateAFSegamationMasterRequest();
                    RequestData.AFSegamationMasterTypesRecord = new AFSegamationMasterTypes();
                    RequestData.AFSegamationMasterTypesRecord.ID = _IAFSegamationView.ID;
                    RequestData.AFSegamationMasterTypesRecord.AFSegamationCode = _IAFSegamationView.AFSegamationCode;
                    RequestData.AFSegamationMasterTypesRecord.AFSegamationName = _IAFSegamationView.AFSegamationName;
                    RequestData.AFSegamationMasterTypesRecord.Remarks = _IAFSegamationView.Remarks;
                    RequestData.AFSegamationMasterTypesRecord.CodeLength = _IAFSegamationView.CodeLength;
                    RequestData.AFSegamationMasterTypesRecord.UseSeperator = _IAFSegamationView.UseSeperator;
                    RequestData.AFSegamationMasterTypesRecord.SCN = _IAFSegamationView.SCN;
                    RequestData.AFSegamationMasterTypesRecord.UpdateBy = 1;

                    var ResponseData = _AFSegamationMasterBLL.UpdateAFSegamationMaster(RequestData);
                    _IAFSegamationView.Message = ResponseData.DisplayMessage;
                    _IAFSegamationView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IAFSegamationView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void DeleteAFSegamationView()
        {
            try
            {

                var RequestData = new DeleteAFSegamationMasterRequest();

                RequestData.ID = _IAFSegamationView.ID;
                var ResponseData = _AFSegamationMasterBLL.DeleteAFSegamationMaster(RequestData);
                _IAFSegamationView.Message = ResponseData.DisplayMessage;
                _IAFSegamationView.ProcessStatus = ResponseData.StatusCode;


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void SelectByIDAFSegamationMaster()
        {
            try
            {
                var RequestData = new SelectByIDAFSegamationMasterRequest();
                RequestData.ID = _IAFSegamationView.ID;
                var ResponseData = _AFSegamationMasterBLL.SelectByIDAFSegamationMaster(RequestData);
                _IAFSegamationView.AFSegamationCode = ResponseData.AFSegamationMasterTypesData.AFSegamationCode;
                _IAFSegamationView.AFSegamationName = ResponseData.AFSegamationMasterTypesData.AFSegamationName;
                _IAFSegamationView.CodeLength = ResponseData.AFSegamationMasterTypesData.CodeLength;
                _IAFSegamationView.UseSeperator = ResponseData.AFSegamationMasterTypesData.UseSeperator;
                _IAFSegamationView.Remarks = ResponseData.AFSegamationMasterTypesData.Remarks;
                _IAFSegamationView.Active = ResponseData.AFSegamationMasterTypesData.Active;
                _IAFSegamationView.SCN = ResponseData.AFSegamationMasterTypesData.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IAFSegamationView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IAFSegamationView.Message = ResponseData.DisplayMessage;
                }

                _IAFSegamationView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        
         public void SelectByIDAFSegamationDetails()
         {
             try
             {
                 var RequestData = new SelectAFSegmationDetailsRequest();
                 RequestData.ShowInActiveRecords = true;
                 RequestData.ID = _IAFSegamationView.ID;
                 var ResponseData = _AFSegamationMasterBLL.SelectByIDAFSegamationDetils(RequestData);                

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IAFSegamationView.SegmentMasterList = ResponseData.AFSegmentDetailMasterRecord;
                 }

                 else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                 {
                     _IAFSegamationView.Message = ResponseData.DisplayMessage;
                 }

                 _IAFSegamationView.ProcessStatus = ResponseData.StatusCode;
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }
        public void SelectSegmentationDetails()
        {
            SelectAllSegmentRequest RequestData = new SelectAllSegmentRequest();
            RequestData.ShowInActiveRecords = true;          
            SelectAllSegmentResponse ResponseData = _SegmentMasterBLL.SelectAllSegmentMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IAFSegamationView.SegmentMasterList = ResponseData.SegmentMasterList;
            }
            else
            {
                _IAFSegamationView.Message = ResponseData.DisplayMessage;
                _IAFSegamationView.ProcessStatus = ResponseData.StatusCode;
            }
        }
    }


    public class AFSegamationMasterPresenterList
    {
        IAFSegamationViewList _IAFSegamationViewList;
        AFSegamationMasterBLL _AFSegamationMasterBLL = new AFSegamationMasterBLL();
        public AFSegamationMasterPresenterList(IAFSegamationViewList ViewObj)
        {
            _IAFSegamationViewList = ViewObj;

        }

        public void SelectAllAFSegamationMaster()
        {
            try
            {
                var RequestData = new SelectAllAFSegamationMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllAFSegamationMasterResponse();

                ResponseData = _AFSegamationMasterBLL.SelectAllAFSegamationMaster(RequestData);


                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IAFSegamationViewList.AFSegamationMasterList = ResponseData.AFSegamationMasterTypesList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {
                    _IAFSegamationViewList.AFSegamationMasterList = ResponseData.AFSegamationMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
