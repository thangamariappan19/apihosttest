using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IShift;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.ShiftMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class ShiftMasterPresenter
    {
        IShiftMasterView _IShiftMasterView;
        ShiftBLL _ShiftBLL = new ShiftBLL();
        CountryBLL _CountryBLL = new CountryBLL();

        public ShiftMasterPresenter(IShiftMasterView ViewObj)
        {
            _IShiftMasterView = ViewObj;
        }

        public void SaveShift()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveShiftRequest();
                    RequestData.Shiftlist = _IShiftMasterView.ShiftList ;                   
                    var ResponseData = _ShiftBLL.SaveShift(RequestData);
                    _IShiftMasterView.Message = ResponseData.DisplayMessage;
                    _IShiftMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IShiftMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidForm()
        {
            bool objBool = false;
             if(_IShiftMasterView.CountryName.Trim() == String.Empty)
            {
                _IShiftMasterView.Message = " Please Select Country";
            }
            else if (_IShiftMasterView.ShiftList.Count == 0)
            {
                _IShiftMasterView.Message = "Please Enter Shift Details";
            }
                       
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void DeleteShift()
        {
            try
            {
                var RequestData = new DeleteShiftRequest();
                RequestData.ID = _IShiftMasterView.ID;
                var ResponseData = _ShiftBLL.DeleteShift(RequestData);
                _IShiftMasterView.Message = ResponseData.DisplayMessage;
                _IShiftMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectShiftListForCategory()
        {
            try
            {
                var RequestData = new SelectShiftListForCategoryRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CountryID = _IShiftMasterView.CountryID;
                SelectShiftListForCategoryResponse ResponseData = _ShiftBLL.ShiftByCountry(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftMasterView.ShiftList = ResponseData.ShiftList;
                }
                else
                {
                    _IShiftMasterView.Message = ResponseData.DisplayMessage;
                    _IShiftMasterView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCountryLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftMasterView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class ShiftListPresenter
    {
        IShiftMasterCollectionView _IShiftMasterCollectionView;
        CountryBLL _CountryBLL = new CountryBLL();

        public ShiftListPresenter(IShiftMasterCollectionView ViewObj)
        {
            _IShiftMasterCollectionView = ViewObj;
        }

        public void GetCountryList()
        {
            try
            {
                var RequestData = new SelectAllCountryRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCountryResponse();
                ResponseData = _CountryBLL.SelectAllCountryMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IShiftMasterCollectionView.CountryList = ResponseData.CountryMasterList;
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
