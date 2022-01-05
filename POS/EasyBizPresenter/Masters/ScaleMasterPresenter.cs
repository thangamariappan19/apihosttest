using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IScale;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizResponse.Masters.ScaleMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class ScaleMasterPresenter
    {
        IScaleView _IScaleView;
        ScaleBLL _ScaleBLL = new ScaleBLL();
        BrandBLL _BrandBLL = new BrandBLL();
         public ScaleMasterPresenter(IScaleView ViewObj)
        {
            _IScaleView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IScaleView.ScaleCode.Trim() == string.Empty)
            {
                _IScaleView.Message = "Scale Code is missing Please Enter it.";
            }
            //else if (_IScaleView.ScaleCode.Length > 3)
            //{
            //    _IScaleView.Message = " Scale Code not allow more than Three Character.";
            //}
            else if (_IScaleView.ScaleName.Trim() == string.Empty)
            {
                _IScaleView.Message = "Scale Name is missing Please Enter it.";
            }
            else if (_IScaleView.SaveBrandMasterList.Count == 0)
            {
                _IScaleView.Message = "Select any one Brand ";
            }
            //else if (_IScaleView.InternalCode.Trim() == string.Empty)
            //{
            //    _IScaleView.Message = "Internal Code is missing Please Enter it.";
            //}
            //else if (_IScaleView.VisualOrder == null)
            //{
            //    _IScaleView.Message = "VisualOrder is missing Please Enter it.";
            //}
          

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveScale()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveScaleRequest();
                    RequestData.ScaleDetailMasterList = _IScaleView.ScaleDetailMasterList;
                    RequestData.BrandMasterList = _IScaleView.SaveBrandMasterList;
                    RequestData.ScaleRecord = new ScaleMaster();
                    RequestData.ScaleRecord.ID = _IScaleView.ID;
                    RequestData.ScaleRecord.ScaleCode = _IScaleView.ScaleCode;
                    RequestData.ScaleRecord.ScaleName = _IScaleView.ScaleName;
                    RequestData.ScaleRecord.InternalCode = _IScaleView.InternalCode;
                    RequestData.ScaleRecord.Active = _IScaleView.Active;
                    RequestData.ScaleRecord.ApplytoAll = _IScaleView.ApplytoAll;
                   // RequestData.ScaleRecord.VisualOrder = _IScaleView.VisualOrder;
                    RequestData.ScaleRecord.CreateBy = _IScaleView.UserID;
                    RequestData.ScaleRecord.CreateOn = DateTime.Now;
                    RequestData.ScaleRecord.Active = true;
                    RequestData.ScaleRecord.SCN = _IScaleView.SCN;
                    var ResponseData = _ScaleBLL.SaveScale(RequestData);
                    _IScaleView.Message = ResponseData.DisplayMessage;
                    _IScaleView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IScaleView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateScale()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateScaleRequest();
                    RequestData.ScaleRecord = new ScaleMaster();
                    RequestData.ScaleRecord.ID = _IScaleView.ID;
                    RequestData.ScaleRecord.ScaleCode = _IScaleView.ScaleCode;
                    RequestData.ScaleRecord.ScaleName = _IScaleView.ScaleName;
                    RequestData.ScaleRecord.UpdateBy = _IScaleView.UserID;
                    RequestData.ScaleRecord.UpdateOn = DateTime.Now;
                    RequestData.ScaleRecord.Active = _IScaleView.Active;
                    RequestData.ScaleRecord.ApplytoAll = _IScaleView.ApplytoAll;
                    RequestData.ScaleRecord.SCN = _IScaleView.SCN;
                    var ResponseData = _ScaleBLL.UpdateScale(RequestData);
                    _IScaleView.Message = ResponseData.DisplayMessage;
                    _IScaleView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IScaleView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectScaleRecord()
        {
            try
            {
                var RequestData = new SelectByScaleIDRequest();
                RequestData.ID = _IScaleView.ID;
                var ResponseData = _ScaleBLL.SelectScaleRecord(RequestData);
                _IScaleView.ScaleCode = ResponseData.ScaleRecord.ScaleCode;
                _IScaleView.ScaleName = ResponseData.ScaleRecord.ScaleName;    
                _IScaleView.VisualOrder = ResponseData.ScaleRecord.VisualOrder;
                _IScaleView.InternalCode = ResponseData.ScaleRecord.InternalCode;
                _IScaleView.Active = ResponseData.ScaleRecord.Active;               
                _IScaleView.ApplytoAll = ResponseData.ScaleRecord.ApplytoAll;     
                _IScaleView.SCN = ResponseData.ScaleRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IScaleView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IScaleView.Message = ResponseData.DisplayMessage;
                }
                _IScaleView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetBrandLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IScaleView.BrandMasterList = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectScaleDetails()
        {
            SelectScaleDetailsRequest RequestData = new SelectScaleDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IScaleView.ID;
            SelectScaleDetailsResponse ResponseData = _ScaleBLL.SelectAllStoreGroupDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IScaleView.ScaleDetailMasterList = ResponseData.ScaleDetailMasterRecord;
            }
            else
            {
                _IScaleView.Message = ResponseData.DisplayMessage;
                _IScaleView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectScaleBrandDetails()
        {
            SelectScaleDetailsRequest RequestData = new SelectScaleDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IScaleView.ID;
            SelectScaleDetailsResponse ResponseData = _ScaleBLL.SelectAllStoreBrandDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IScaleView.ScaleWithBrandMasterList = ResponseData.ScaleBrandDetailMasterRecord;
            }
            else
            {
                _IScaleView.Message = ResponseData.DisplayMessage;
                _IScaleView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void DeleteScale()
        {
            try
            {
                var RequestData = new DeleteScaleRequest();
                RequestData.ID = _IScaleView.ID;
                var ResponseData = _ScaleBLL.DeleteScale(RequestData);
                _IScaleView.Message = ResponseData.DisplayMessage;
                _IScaleView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
   public class ScaleMasterListPresenter
   {

       ScaleBLL _ScaleBLL = new ScaleBLL();
       IScaleCollectionView _IScaleCollectionView;
       public ScaleMasterListPresenter(IScaleCollectionView ViewObj)
       {
           _IScaleCollectionView = ViewObj;
       }
       public void GetScaleList()
       {
           try
           {
               var RequestData = new SelectAllScaleRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllScaleResponse();
               ResponseData = _ScaleBLL.SelectAllScale(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IScaleCollectionView.ScaleList = ResponseData.ScaleList;
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
