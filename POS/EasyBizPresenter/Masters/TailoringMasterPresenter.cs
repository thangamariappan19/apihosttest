using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ITailoringMaster;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Masters.TailoringMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class TailoringMasterPresenter
    {
        ITailoringMasterView _ITailoringMasterView;
        CountryBLL _CountryBLL = new CountryBLL();
        TailoringMasterBLL _TailoringMasterBLL = new TailoringMasterBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public TailoringMasterPresenter(ITailoringMasterView ViewObj)
        {
            _ITailoringMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ITailoringMasterView.tailoringunitcode.Trim() == string.Empty)
            {
                _ITailoringMasterView.Message = "Tailoring Code is missing Please Enter it.";
            }           
            else if (_ITailoringMasterView.tailoringunitName.Trim() == string.Empty)
            {
                _ITailoringMasterView.Message = "Tailoring Name is missing Please Enter it.";
            }
            else if (_ITailoringMasterView.CountryID == 0 || _ITailoringMasterView.CountryID.ToString() == string.Empty)
            {
                _ITailoringMasterView.Message = " Country Name is missing Please Select it.";
            }
            else if (_ITailoringMasterView.StoreID == 0 || _ITailoringMasterView.StoreID.ToString() == string.Empty)
            {
                _ITailoringMasterView.Message = " Store Name is missing Please Select it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveTailoringUnit()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveTailoringRequest();
                    RequestData.TailoringMasterRecord = new TailoringMasterTypes();

                    RequestData.TailoringMasterRecord.ID = _ITailoringMasterView.ID;
                    RequestData.TailoringMasterRecord.tailoringunitcode = _ITailoringMasterView.tailoringunitcode;
                    RequestData.TailoringMasterRecord.tailoringunitName = _ITailoringMasterView.tailoringunitName;
                    RequestData.TailoringMasterRecord.CountryID = _ITailoringMasterView.CountryID;
                    RequestData.TailoringMasterRecord.CountryCode = _ITailoringMasterView.CountryCode;                    
                    RequestData.TailoringMasterRecord.StoreID = _ITailoringMasterView.StoreID;
                    RequestData.TailoringMasterRecord.StoreCode = _ITailoringMasterView.StoreCode;                    
                    RequestData.TailoringMasterRecord.CreateBy = _ITailoringMasterView.UserID;
                    RequestData.TailoringMasterRecord.CreateOn = DateTime.Now;
                    RequestData.TailoringMasterRecord.Active = _ITailoringMasterView.Active;
                    RequestData.TailoringMasterRecord.SCN = _ITailoringMasterView.SCN;
                    var ResponseData = _TailoringMasterBLL.SaveTailoringmaster(RequestData);
                    _ITailoringMasterView.Message = ResponseData.DisplayMessage;
                    _ITailoringMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ITailoringMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCountryLookUP()
        {
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITailoringMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
            }
        }


        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _ITailoringMasterView.CountryID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITailoringMasterView.StoreMasterLookUp = ResponseData.StoreMasterList;
            }
        }

        public void SelectTailoringUnitRecord()
        {
            try
            {
                var RequestData = new SelectByTailoringIDRequest();
                RequestData.ID = _ITailoringMasterView.ID;
                var ResponseData = _TailoringMasterBLL.SelectTailoringUnitRecord(RequestData);
                _ITailoringMasterView.tailoringunitcode = ResponseData.TailoringMasterRecord.tailoringunitcode;
                _ITailoringMasterView.tailoringunitName = ResponseData.TailoringMasterRecord.tailoringunitName;
                _ITailoringMasterView.CountryID = ResponseData.TailoringMasterRecord.CountryID;
                _ITailoringMasterView.StoreID = ResponseData.TailoringMasterRecord.StoreID;               
                _ITailoringMasterView.Active = ResponseData.TailoringMasterRecord.Active;
                _ITailoringMasterView.SCN = ResponseData.TailoringMasterRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ITailoringMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ITailoringMasterView.Message = ResponseData.DisplayMessage;
                }
                _ITailoringMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
     public class TailoringCollectionPresenter
   {

         TailoringMasterBLL _TailoringMasterBLL = new TailoringMasterBLL();
       ITailoringCollectionView _ITailoringCollectionView;
       public TailoringCollectionPresenter(ITailoringCollectionView ViewObj)
       {
           _ITailoringCollectionView = ViewObj;
       }
       public void GetTailoringUnitList()
       {
           try
           {
               var RequestData = new SelectAllTailoringRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllTailoringResponse();
               ResponseData = _TailoringMasterBLL.SelectAllTailoringUnit(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ITailoringCollectionView.TailoringMasterList = ResponseData.TailoringMasterList;
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
