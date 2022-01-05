using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IYear;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizResponse.Masters.YearMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class YearMasterPresenter
    {
          IYearView _IYearView;
        YearBLL _YearBLL = new YearBLL();
        public YearMasterPresenter(IYearView ViewObj)
        {
            _IYearView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IYearView.YearCode.Trim() == string.Empty)
            {
                _IYearView.Message = "Year Code is missing Please Enter it.";
            }           
            //else if (_IYearView.YearCode.Length != 4)
            //{
            //    _IYearView.Message = " Year Code must have 4 characters";
            //}
            else if (_IYearView.Year.Trim() == string.Empty)
            {
                _IYearView.Message = " Year Name is Missing";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveYear()
        {
            if (IsValidForm())
            {
                var RequestData = new SaveYearRequest();
                RequestData.YearRecord = new YearMaster();

                RequestData.YearRecord.ID = _IYearView.ID;
                RequestData.YearRecord.YearCode = _IYearView.YearCode;
                RequestData.YearRecord.Year = _IYearView.Year;
                RequestData.YearRecord.CreateBy = _IYearView.UserID;
                RequestData.YearRecord.CreateOn = DateTime.Now;
                RequestData.YearRecord.Remarks = _IYearView.Remarks;
                RequestData.YearRecord.Active = _IYearView.Active;
                RequestData.YearRecord.SCN = _IYearView.SCN;
                var ResponseData = _YearBLL.SaveYear(RequestData);
                _IYearView.Message = ResponseData.DisplayMessage;
                _IYearView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IYearView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }

        }
        public void UpdateYear()
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateYearRequest();
                RequestData.YearRecord = new YearMaster();
                RequestData.YearRecord.ID = _IYearView.ID;
                RequestData.YearRecord.YearCode = _IYearView.YearCode;
                RequestData.YearRecord.Year = _IYearView.Year;
                RequestData.YearRecord.UpdateBy = _IYearView.UserID;
                RequestData.YearRecord.Remarks = _IYearView.Remarks;
                RequestData.YearRecord.Active = _IYearView.Active;
                RequestData.YearRecord.UpdateOn = DateTime.Now;
                //RequestData.YearRecord.Active = true;
                RequestData.YearRecord.SCN = _IYearView.SCN;
                var ResponseData = _YearBLL.UpdateYear(RequestData);
                _IYearView.Message = ResponseData.DisplayMessage;
                _IYearView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IYearView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void SelectYearRecord()
        {
            var RequestData = new SelectByYearIDRequest();
            RequestData.ID = _IYearView.ID;
            var ResponseData = _YearBLL.SelectYearRecord(RequestData);
            _IYearView.YearCode = ResponseData.YearRecord.YearCode;
            _IYearView.Year = ResponseData.YearRecord.Year;
            _IYearView.SCN = ResponseData.YearRecord.SCN;
            _IYearView.Remarks = ResponseData.YearRecord.Remarks;
            _IYearView.Active = ResponseData.YearRecord.Active;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IYearView.Message = ResponseData.DisplayMessage;
            }
            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IYearView.Message = ResponseData.DisplayMessage;
            }
            _IYearView.ProcessStatus = ResponseData.StatusCode;
        }
        public void DeleteYear()
        {
            var RequestData = new DeleteYearRequest();
            RequestData.ID = _IYearView.ID;
            var ResponseData = _YearBLL.DeleteYear(RequestData);
            _IYearView.Message = ResponseData.DisplayMessage;
            _IYearView.ProcessStatus = ResponseData.StatusCode;
        }       
    }
   public class YearMasterListPresenter
   {

      YearBLL _YearBLL = new YearBLL();
      IYearCollcetionView _IYearCollectionView;
      public YearMasterListPresenter(IYearCollcetionView ViewObj)
       {
           _IYearCollectionView = ViewObj;
       }
       public void GetYearList()
       {
           var RequestData = new SelectAllYearRequest();
           RequestData.ShowInActiveRecords = true;
           var ResponseData = new SelectAllYearResponse();
           ResponseData = _YearBLL.SelectAllYear(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IYearCollectionView.YearList = ResponseData.YearList;
           }
           else
           {

           }
       }
    }
}
