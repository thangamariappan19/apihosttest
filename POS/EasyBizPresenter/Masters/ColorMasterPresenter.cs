using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IColor;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizResponse.Masters.ColorMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class ColorMasterPresenter
    {
        IColorMasterView _IColorMasterView;
        ColorBLL _ColorBLL = new ColorBLL();

        public ColorMasterPresenter(IColorMasterView ViewObj)
        {
            _IColorMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            //if (_IColorMasterView.InternalCode.Trim() == string.Empty)
            //{
            //    _IColorMasterView.Message = "Internal Code is missing Please Enter it.";
            //}
            if (_IColorMasterView.ColorCode.Trim() == string.Empty)
            {
                _IColorMasterView.Message = "Color Code is missing Please Enter it.";
            }
            else if (_IColorMasterView.Description.Trim() == string.Empty)
            {
                _IColorMasterView.Message = "Description is missing Please Enter it.";
            }
            //else if (_IColorMasterView.Colors == 0)
            //{
            //    _IColorMasterView.Message = "Please Select any one Color";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveColorMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveColorRequest();
                    RequestData.ColorRecord = new ColorMaster();

                    RequestData.ColorRecord.ID = _IColorMasterView.ID;
                    RequestData.ColorRecord.InternalCode = _IColorMasterView.InternalCode;
                    RequestData.ColorRecord.ColorCode = _IColorMasterView.ColorCode;
                    RequestData.ColorRecord.Description = _IColorMasterView.Description;
                    RequestData.ColorRecord.Shade = _IColorMasterView.Shade;
                    RequestData.ColorRecord.NRFCode = _IColorMasterView.NRFCode;
                    RequestData.ColorRecord.Colors = _IColorMasterView.Colors;
                    RequestData.ColorRecord.Attribute1 = _IColorMasterView.Attribute1;
                    RequestData.ColorRecord.Attribute2 = _IColorMasterView.Attribute2;
                    RequestData.ColorRecord.MultiColorImage = _IColorMasterView.MulticolorImage;
                    RequestData.ColorRecord.Remarks = _IColorMasterView.Remarks;
                    RequestData.ColorRecord.CreateBy = _IColorMasterView.UserID;
                    RequestData.ColorRecord.CreateOn = DateTime.Now;
                    RequestData.ColorRecord.Active = _IColorMasterView.Active;
                    RequestData.ColorRecord.SCN = _IColorMasterView.SCN;

                    var ResponseData = _ColorBLL.SaveColor(RequestData);

                    _IColorMasterView.Message = ResponseData.DisplayMessage;
                    _IColorMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IColorMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateColorMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateColorRequest();
                    RequestData.ColorRecord = new ColorMaster();
                    RequestData.ColorRecord.ID = _IColorMasterView.ID;
                    RequestData.ColorRecord.InternalCode = _IColorMasterView.InternalCode;
                    RequestData.ColorRecord.ColorCode = _IColorMasterView.ColorCode;
                    RequestData.ColorRecord.Description = _IColorMasterView.Description;
                    RequestData.ColorRecord.Shade = _IColorMasterView.Shade;
                    RequestData.ColorRecord.NRFCode = _IColorMasterView.NRFCode;
                    RequestData.ColorRecord.Colors = _IColorMasterView.Colors;
                    RequestData.ColorRecord.Attribute1 = _IColorMasterView.Attribute1;
                    RequestData.ColorRecord.Attribute2 = _IColorMasterView.Attribute2;
                    RequestData.ColorRecord.MultiColorImage = _IColorMasterView.MulticolorImage;
                   
                    RequestData.ColorRecord.UpdateBy = _IColorMasterView.UserID;
                    RequestData.ColorRecord.UpdateOn = DateTime.Now;
                    RequestData.ColorRecord.Active = _IColorMasterView.Active;
                    RequestData.ColorRecord.SCN = _IColorMasterView.SCN;
                    var ResponseData = _ColorBLL.UpdateColor(RequestData);
                    _IColorMasterView.Message = ResponseData.DisplayMessage;
                    _IColorMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IColorMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectColorMasterRecord()
        {
            try
            {
                var RequestData = new SelectByColorIDRequest();
                _IColorMasterView.Colors = 0;
                RequestData.ID = _IColorMasterView.ID;
                var ResponseData = _ColorBLL.SelectColorRecord(RequestData);
                _IColorMasterView.InternalCode = ResponseData.ColorRecord.InternalCode;
                _IColorMasterView.ColorCode = ResponseData.ColorRecord.ColorCode;
                _IColorMasterView.Description = ResponseData.ColorRecord.Description;
                _IColorMasterView.Shade = ResponseData.ColorRecord.Shade;
                _IColorMasterView.NRFCode = ResponseData.ColorRecord.NRFCode;
                _IColorMasterView.Colors = ResponseData.ColorRecord.Colors;
                _IColorMasterView.Attribute1 = ResponseData.ColorRecord.Attribute1;
                _IColorMasterView.Attribute2 = ResponseData.ColorRecord.Attribute2;
                _IColorMasterView.MulticolorImage = ResponseData.ColorRecord.MultiColorImage;
                _IColorMasterView.Remarks = ResponseData.ColorRecord.Remarks;
                _IColorMasterView.Active = ResponseData.ColorRecord.Active;
                _IColorMasterView.SCN = ResponseData.ColorRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IColorMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IColorMasterView.Message = ResponseData.DisplayMessage;
                }

                _IColorMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteStateMaster()
        {
            try
            {
                var RequestData = new DeleteColorRequest();
                RequestData.ID = _IColorMasterView.ID;
                var ResponseData = _ColorBLL.DeleteColor(RequestData);
                _IColorMasterView.Message = ResponseData.DisplayMessage;
                _IColorMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class ColorMasterListPresenter
    {
        ColorBLL _ColorBLL = new ColorBLL();
        IColorMasterCollectionView _IColorMasterCollectionView;
        public ColorMasterListPresenter(IColorMasterCollectionView ViewObj)
        {
            _IColorMasterCollectionView = ViewObj;
        }


        public void GetColorList()
        {
            try
            {
                var RequestData = new SelectAllColorRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllColorResponse();
                ResponseData = _ColorBLL.SelectAllColorRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IColorMasterCollectionView.ColorList = ResponseData.ColorList;
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
