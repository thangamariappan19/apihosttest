using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IDocumentNumberingMaster;
using EasyBizIView.Masters.IDocumentType;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.DocumentTypeRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.DocumentTypeResponse;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class DocumentNumberingPresenter
    {
        IDocumentNumberingView _IDocumentNumberingMasterView;
        DocumentNumberingBLL _DocumentNumberingMasterBLL = new DocumentNumberingBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        PosMasterBLL _PosBLL = new PosMasterBLL();
        DocumentTypeBLL _DocumentTypeBLL = new DocumentTypeBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        StateMasterBLL _StateBLL = new StateMasterBLL();
        public DocumentNumberingPresenter(IDocumentNumberingView ViewObj)
        {
            _IDocumentNumberingMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
			if(_IDocumentNumberingMasterView.DocumentTypeID == 78)
			{
				objBool = true;								
			}
			else
			{
				if (_IDocumentNumberingMasterView.CountryID == 0)
				{
					_IDocumentNumberingMasterView.Message = " Country Name is missing Please Select it.";
				}
				else if (_IDocumentNumberingMasterView.StateID == 0)
				{
					_IDocumentNumberingMasterView.Message = " State Name is missing Please Select it.";
				}
				else if (_IDocumentNumberingMasterView.StoreID == 0)
				{
					_IDocumentNumberingMasterView.Message = "Store is missing Please Select it. ";
				}				
				else if (_IDocumentNumberingMasterView.DocumentTypeID == 0)
				{
					_IDocumentNumberingMasterView.Message = "DocumentType is missing Please Select it. ";
				}
				else
				{
					objBool = true;
				}
			}            
            return objBool;
        }
        public void SaveDocumentNumberingMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveDocumentNumberingMasterRequest();
                    RequestData.DocumentNumberingDetailsList = _IDocumentNumberingMasterView.DocumentNumberingDetailsList;
                    RequestData.DocumentNumberingMasterRecord = new DocumentNumberingMaster();

                    RequestData.DocumentNumberingMasterRecord.ID = _IDocumentNumberingMasterView.ID;
                    RequestData.DocumentNumberingMasterRecord.CountryID = _IDocumentNumberingMasterView.CountryID;
                    RequestData.DocumentNumberingMasterRecord.StateID = _IDocumentNumberingMasterView.StateID;
                    RequestData.DocumentNumberingMasterRecord.StoreID = _IDocumentNumberingMasterView.StoreID;
                    RequestData.DocumentNumberingMasterRecord.CountryCode = _IDocumentNumberingMasterView.CountryCode;
                    RequestData.DocumentNumberingMasterRecord.StateCode = _IDocumentNumberingMasterView.StateCode;
                    RequestData.DocumentNumberingMasterRecord.StoreCode = _IDocumentNumberingMasterView.StoreCode;
                    RequestData.DocumentNumberingMasterRecord.DocumentTypeID = _IDocumentNumberingMasterView.DocumentTypeID;
                    RequestData.DocumentNumberingMasterRecord.Active = _IDocumentNumberingMasterView.IsActive;

                    if (_IDocumentNumberingMasterView.DocumentTypeID == 78)
                    {
                        RequestData.DocumentNumberingMasterRecord.CountryCode = "";
                        RequestData.DocumentNumberingMasterRecord.StateCode = "";
                        RequestData.DocumentNumberingMasterRecord.StoreCode = "";                       
                    }
                    else
                    {
                        RequestData.DocumentNumberingMasterRecord.CountryCode = _IDocumentNumberingMasterView.CountryCode;
                        RequestData.DocumentNumberingMasterRecord.StateCode = _IDocumentNumberingMasterView.StateCode;
                        RequestData.DocumentNumberingMasterRecord.StoreCode = _IDocumentNumberingMasterView.StoreCode;
                    }

                    RequestData.DocumentNumberingMasterRecord.CreateBy = _IDocumentNumberingMasterView.UserID;
                    RequestData.DocumentNumberingMasterRecord.CreateOn = DateTime.Now;
                    //RequestData.DocumentNumberingMasterRecord.Active = true;
                    RequestData.DocumentNumberingMasterRecord.SCN = _IDocumentNumberingMasterView.SCN;

                    var ResponseData = _DocumentNumberingMasterBLL.SaveDocumentNumberingMaster(RequestData);

                    _IDocumentNumberingMasterView.Message = ResponseData.DisplayMessage;
                    _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IDocumentNumberingMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateDocumentNumberingMaster()
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateDocumentNumberingMasterRequest();
                RequestData.DocumentNumberingMasterRecord = new DocumentNumberingMaster();               
                RequestData.DocumentNumberingMasterRecord.ID = _IDocumentNumberingMasterView.ID;
                RequestData.DocumentNumberingMasterRecord.CountryID = _IDocumentNumberingMasterView.CountryID;
                RequestData.DocumentNumberingMasterRecord.StateID = _IDocumentNumberingMasterView.StateID;
                RequestData.DocumentNumberingMasterRecord.StoreID = _IDocumentNumberingMasterView.StoreID; 
                RequestData.DocumentNumberingMasterRecord.DocumentTypeID = _IDocumentNumberingMasterView.DocumentTypeID;
                RequestData.DocumentNumberingMasterRecord.UpdateBy = _IDocumentNumberingMasterView.UserID;
                RequestData.DocumentNumberingMasterRecord.UpdateOn = DateTime.Now;
                RequestData.DocumentNumberingMasterRecord.Active = true;
                RequestData.DocumentNumberingMasterRecord.SCN = _IDocumentNumberingMasterView.SCN;
                RequestData.DocumentNumberingMasterRecord.Active = _IDocumentNumberingMasterView.IsActive;

                var ResponseData = _DocumentNumberingMasterBLL.UpdateDocumentNumberingMaster(RequestData);

                _IDocumentNumberingMasterView.Message = ResponseData.DisplayMessage;
                _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IDocumentNumberingMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteDocumentNumberingMaster()
        {
            try
        {
            var RequestData = new DeleteDocumentNumberingMasterRequest();
            RequestData.ID = _IDocumentNumberingMasterView.ID;
            var ResponseData = _DocumentNumberingMasterBLL.DeleteDocumentNumberingMaster(RequestData);
            _IDocumentNumberingMasterView.Message = ResponseData.DisplayMessage;
            _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectDocumentNumberingMaster()
        {
            var RequestData = new SelectByIDDocumentNumberingMasterRequest();
            RequestData.ID = _IDocumentNumberingMasterView.ID;
            var ResponseData = _DocumentNumberingMasterBLL.SelectDocumentNumberingMaster(RequestData);
            if (ResponseData.DocumentNumberingMasterRecord != null)
            {
                _IDocumentNumberingMasterView.ID = ResponseData.DocumentNumberingMasterRecord.ID;
                _IDocumentNumberingMasterView.CountryID = ResponseData.DocumentNumberingMasterRecord.CountryID;
                _IDocumentNumberingMasterView.StateID = ResponseData.DocumentNumberingMasterRecord.StateID;
                _IDocumentNumberingMasterView.StoreID = ResponseData.DocumentNumberingMasterRecord.StoreID; 
                _IDocumentNumberingMasterView.DocumentTypeID = ResponseData.DocumentNumberingMasterRecord.DocumentTypeID;
                _IDocumentNumberingMasterView.IsActive = ResponseData.DocumentNumberingMasterRecord.Active;              
            }   
            _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectHeaderID()
        {
            var RequestData = new SelectByIDDocumentNumberingMasterRequest();
            RequestData.CountryID = _IDocumentNumberingMasterView.CountryID;
            RequestData.StateID = _IDocumentNumberingMasterView.StateID;
            RequestData.StoreID = _IDocumentNumberingMasterView.StoreID;            
            RequestData.DocumentTypeID = _IDocumentNumberingMasterView.DocumentTypeID;
            
            var ResponseData = _DocumentNumberingMasterBLL.SelectHeaderID(RequestData);
            if (ResponseData.DocumentNumberingMasterRecord == null)
            {
                _IDocumentNumberingMasterView.ID = 0;
            }
            else
            {
                _IDocumentNumberingMasterView.ID = ResponseData.DocumentNumberingMasterRecord.ID;
            }            

            if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IDocumentNumberingMasterView.Message = ResponseData.DisplayMessage;
            }
            _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
        }

        public void SelectDocumentNumberingDetails()
        {
            SelectDocumentNumberingDetailsRequest RequestData = new SelectDocumentNumberingDetailsRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IDocumentNumberingMasterView.ID;
            SelectDocumentNumberingDetailsResponse ResponseData = _DocumentNumberingMasterBLL.SelectDocumentNumberingDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingMasterView.DocumentNumberingDetailsList = ResponseData.DocumentNumberingDetailsList;
            }
            else
            {
                _IDocumentNumberingMasterView.DocumentNumberingDetailsList = new List<DocumentNumberingDetails>();                
                _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void GetCountryLookUP()
        {
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingMasterView. CountryMasterLookUp = ResponseData.CountryMasterList;
            }
        }

        public void GetStateLookUP()
        {
            SelectStateLookUpRequest RequestData = new SelectStateLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IDocumentNumberingMasterView.CountryID;
            SelectStateLookUpResponse ResponseData = _StateBLL.SelectStateLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingMasterView.StateMasterLookUp = ResponseData.StateMasterList;
            }
        }
        public void GetPosLookUP()
        {
            SelectPosMasterLookUpRequest RequestData = new SelectPosMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IDocumentNumberingMasterView.StoreID;
            SelectPosMasterLookUpResponse ResponseData = _PosBLL.SelectPosMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingMasterView.PosMasterLookUp = ResponseData.PosMasterList;
            }
        }


        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StateID = _IDocumentNumberingMasterView.StateID;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingMasterView.StoreMasterLookUp = ResponseData.StoreMasterList;
            }
        }

        public void GetDocumentTypeMasterLookUP()
        {
            SelectDocumentLookUpRequest RequestData = new SelectDocumentLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectDocumentLookUpResponse ResponseData = _DocumentTypeBLL.SelectDocumentLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingMasterView.DocumentTypeMasterLookUp = ResponseData.DocumentTypeList;
            }
        }

        public string DateValidation()
        {
            var RequestData = new SaveDocumentNumberingMasterRequest();
            RequestData.CountryID = _IDocumentNumberingMasterView.CountryID;
            RequestData.StateID = _IDocumentNumberingMasterView.StateID;
            RequestData.StoreID = _IDocumentNumberingMasterView.StoreID;
           
            RequestData.DocumentTypeID = _IDocumentNumberingMasterView.DocumentTypeID;
            var ResponseData = _DocumentNumberingMasterBLL.DateValidation(RequestData);
            if (ResponseData.DocumentNumberingMasterRecord != null)
            {
                _IDocumentNumberingMasterView.MaxDate = ResponseData.DocumentNumberingMasterRecord.MaxDate;
            }
            else if (ResponseData.DocumentNumberingMasterRecord == null)
            {
                _IDocumentNumberingMasterView.MaxDate = "";
            }
            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IDocumentNumberingMasterView.Message = ResponseData.DisplayMessage;
            }

            _IDocumentNumberingMasterView.ProcessStatus = ResponseData.StatusCode;
           return _IDocumentNumberingMasterView.MaxDate;
        }
        
    }

    public class DocumentNumberingMasterListPresenter
    {
        DocumentNumberingBLL _DocumentNumberingMasterBLL = new DocumentNumberingBLL();
        IDocumentNumberingList _IDocumentNumberingList;
        public DocumentNumberingMasterListPresenter(IDocumentNumberingList ViewObj)
        {
            _IDocumentNumberingList = ViewObj;
        }
        public void GetDocumentNumberingMasterList()
        {
            var RequestData = new SelectAllDocumentNumberingMasterRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllDocumentNumberingMasterResponse();
            ResponseData = _DocumentNumberingMasterBLL.SelectAllDocumentNumberingMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDocumentNumberingList.DocumentNumberingMasterList = ResponseData.DocumentNumberingMasterList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {

            }
        }      
    }
}
