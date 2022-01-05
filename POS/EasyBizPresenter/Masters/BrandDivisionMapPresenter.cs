using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Masters.IBrandDivisionMap;
using EasyBizRequest.Masters.BrandDivisionMapRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizResponse.Masters.BrandDivisionMapResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class BrandDivisionMapPresenter
    {
        public BrandDivisionMapPresenter(IBrandDivisionMapView ViewObj)
        {
            _IBrandDivisionMapView = ViewObj;
        }

        IBrandDivisionMapView _IBrandDivisionMapView;
        BrandDivisionMapBLL _BrandDivisionMapBLL = new BrandDivisionMapBLL();
        DivisionBLL _DvisionBLL = new DivisionBLL();
        BrandBLL _BrandBLL = new BrandBLL();

        public void GetAllMappingRecord()
        {
            try
            {
                var RequestData = new SelectAllBrandDivisionRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.BrandID = _IBrandDivisionMapView.BrandID;
                var ResponseData = new SelectAllBrandDivisionMapResponse();
                ResponseData = _BrandDivisionMapBLL.SelectAllBrandDivisionRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IBrandDivisionMapView.BrandDivisionList = ResponseData.BrandDivisionList;
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetBrandDivisionMapLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IBrandDivisionMapView.BrandLookUp = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveBrandDivision()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveBrandDivisionMapRequest();
                    RequestData.BrandDivisionList = _IBrandDivisionMapView.MappingList;
                    var ResponseData = _BrandDivisionMapBLL.SaveBrandDivision(RequestData);
                    _IBrandDivisionMapView.Message = ResponseData.DisplayMessage;
                    _IBrandDivisionMapView.ProcessStatus = ResponseData.StatusCode;

                    if (_IBrandDivisionMapView.ProcessStatus == Enums.OpStatusCode.Success)
                    {
                        GetAllMappingRecord();
                    }
                }
                else
                {
                    _IBrandDivisionMapView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidForm()
        {

            bool ObjBool = false;
            if (_IBrandDivisionMapView.BrandID == 0)
            {
                _IBrandDivisionMapView.Message = "Please Select Atleast One Brand!";
            }
            else if(_IBrandDivisionMapView.MappingList.Count == 0)
            {
                _IBrandDivisionMapView.Message = "Please Select atleast one Division !";
            }
            else
            {
                ObjBool = true;
            }
            return ObjBool;
        }
    }
}



