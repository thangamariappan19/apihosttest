using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.Brand;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class BrandMasterPresenter
    {
        IBrandMasterView _IBrandMasterView;
        BrandBLL _BrandBLL = new BrandBLL();

        public BrandMasterPresenter(IBrandMasterView ViewObj)
        {
            _IBrandMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IBrandMasterView.BrandCode.Trim() == string.Empty)
            {
                _IBrandMasterView.Message = "Code is missing Please Enter it.";
            }
            else if (_IBrandMasterView.BrandCode.Length > 8)
            {
                _IBrandMasterView.Message = "Please Enter Valid Code";
            }
             else if (_IBrandMasterView.BrandCode.Length < 2)
            {
                _IBrandMasterView.Message=("Brand Code must have 2 characters");
            }

               
            else if (_IBrandMasterView.BrandName.Trim() == string.Empty)
            {
                _IBrandMasterView.Message = "Name is missing Please Enter it.";
            }

            else if (_IBrandMasterView.BrandType.Trim() == string.Empty)
            {
                _IBrandMasterView.Message = "BrandType is missing Please Choose it.";
            }
          
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveBrand()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveBrandRequest();
                    RequestData.BrandRecord = new BrandMaster();
                    RequestData.BrandRecord.ID = _IBrandMasterView.ID;
                    RequestData.BrandRecord.BrandCode = _IBrandMasterView.BrandCode;
                    RequestData.BrandRecord.BrandName = _IBrandMasterView.BrandName;
                    RequestData.BrandRecord.BrandLogo = _IBrandMasterView.BrandLogo;
                    RequestData.BrandRecord.ARBName = _IBrandMasterView.ARBname;
                    RequestData.BrandRecord.ShortDescriptionName = _IBrandMasterView.ShortDescriptionName;
                    RequestData.BrandRecord.BrandType = _IBrandMasterView.BrandType;
                    RequestData.BrandRecord.Remarks = _IBrandMasterView.Remarks;
                    RequestData.BrandRecord.Active = _IBrandMasterView.Active;
                    RequestData.BrandRecord.CreateBy = _IBrandMasterView.UserID;
                    RequestData.BrandRecord.CreateOn = DateTime.Now;                   
                    RequestData.BrandRecord.SCN = _IBrandMasterView.SCN;
                    var ResponseData = _BrandBLL.SaveBrand(RequestData);
                    _IBrandMasterView.Message = ResponseData.DisplayMessage;
                    _IBrandMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IBrandMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteBrand()
        {
            try
            {
                var RequestData = new DeleteBrandRequest();
                RequestData.ID = _IBrandMasterView.ID;
                var ResponseData = _BrandBLL.DeleteBrand(RequestData);
                _IBrandMasterView.Message = ResponseData.DisplayMessage;
                _IBrandMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectBrandRecord()
        {
            try
            {
                var RequestData = new SelectByBrandIDRequest();
                RequestData.ID = _IBrandMasterView.ID;
                var ResponseData = _BrandBLL.SelectBrandRecord(RequestData);
                _IBrandMasterView.BrandCode = ResponseData.BrandRecord.BrandCode;
                _IBrandMasterView.BrandName = ResponseData.BrandRecord.BrandName;
                _IBrandMasterView.BrandLogo = ResponseData.BrandRecord.BrandLogo;
                _IBrandMasterView.ARBname = ResponseData.BrandRecord.ARBName;
                _IBrandMasterView.ShortDescriptionName = ResponseData.BrandRecord.ShortDescriptionName;
                _IBrandMasterView.BrandType = ResponseData.BrandRecord.BrandType;
                _IBrandMasterView.Remarks = ResponseData.BrandRecord.Remarks;
                _IBrandMasterView.Active = ResponseData.BrandRecord.Active;
                _IBrandMasterView.SCN = ResponseData.BrandRecord.SCN;
                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IBrandMasterView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IBrandMasterView.Message = ResponseData.DisplayMessage;
                }

                _IBrandMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateBrand()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateBrandRequest();
                    RequestData.BrandRecord = new BrandMaster();
                    RequestData.BrandRecord.ID = _IBrandMasterView.ID;
                    RequestData.BrandRecord.BrandCode = _IBrandMasterView.BrandCode;
                    RequestData.BrandRecord.BrandName = _IBrandMasterView.BrandName;
                    RequestData.BrandRecord.BrandLogo = _IBrandMasterView.BrandLogo;
                    RequestData.BrandRecord.ARBName = _IBrandMasterView.ARBname;
                    RequestData.BrandRecord.ShortDescriptionName = _IBrandMasterView.ShortDescriptionName;
                    RequestData.BrandRecord.BrandType = _IBrandMasterView.BrandType;
                    RequestData.BrandRecord.Remarks = _IBrandMasterView.Remarks;
                    RequestData.BrandRecord.Active = _IBrandMasterView.Active;
                    RequestData.BrandRecord.UpdateBy = _IBrandMasterView.UserID;
                    RequestData.BrandRecord.UpdateOn = DateTime.Now;
                    //RequestData.BrandRecord.Active = true;
                    RequestData.BrandRecord.SCN = _IBrandMasterView.SCN;
                    var ResponseData = _BrandBLL.UpdateBrand(RequestData);
                    _IBrandMasterView.Message = ResponseData.DisplayMessage;
                    _IBrandMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IBrandMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectAllBrand()
        {
            try
            {
                var RequestData = new SelectAllBrandRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.SelectAllBrandRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IBrandMasterView.BrandList = ResponseData.BrandList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {
                    _IBrandMasterView.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class BrandListPresenter
    {
        BrandBLL _BrandBLL = new BrandBLL();
        IBrandMasterListView _IBrandMasterListView;
        public BrandListPresenter(IBrandMasterListView ViewObj)
        {
            _IBrandMasterListView = ViewObj;
        }
        public void GetBrandList()
        {
            try
            {
                var RequestData = new SelectAllBrandRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllBrandResponse();
                ResponseData = _BrandBLL.SelectAllBrandRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IBrandMasterListView.BrandList = ResponseData.BrandList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
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
