using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ICompanySettingMaster;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizResponse.Masters.CountryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public  class CompanySettingPresenter
    {
        ICompanySettingMasterView _ICompanySettingMasterView;
        CompanySettingBLL _CompanySettingBLL = new CompanySettingBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        RetailSettingsBLL _RetailSettingsBLL = new RetailSettingsBLL();


        public CompanySettingPresenter(ICompanySettingMasterView ViewObj)
        {
            _ICompanySettingMasterView = ViewObj;
        }



        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICompanySettingMasterView.CompanyCode == string.Empty)
            {
                _ICompanySettingMasterView.Message = "Company Code is missing Please Enter it.";
            }
            else if (_ICompanySettingMasterView.CompanyCode.Length > 8)
            {
                _ICompanySettingMasterView.Message = "Please Enter Valid Code";
            }
            else if (_ICompanySettingMasterView.CompanyName == string.Empty)
            {
                _ICompanySettingMasterView.Message = "Please Enter Company Name";
            }
            //else if (_ICompanySettingMasterView.RetailSettingID==0)
            //{
            //    _ICompanySettingMasterView.Message = "Please Select Retail Settings";
            //}
            else if (_ICompanySettingMasterView.CountrySettingID == 0)
            {
                _ICompanySettingMasterView.Message = "Please Select Country Settings";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }





        public void SaveCompanySetting()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCompanySettingRequest();
                    RequestData.CompanySettingData = new CompanySettings();

                    RequestData.CompanySettingData.ID = _ICompanySettingMasterView.ID;
                    RequestData.CompanySettingData.CompanyCode = _ICompanySettingMasterView.CompanyCode;
                    RequestData.CompanySettingData.CompanyName = _ICompanySettingMasterView.CompanyName;
                    RequestData.CompanySettingData.Address = _ICompanySettingMasterView.Address;
                    RequestData.CompanySettingData.CountrySettingID = _ICompanySettingMasterView.CountrySettingID;
                    RequestData.CompanySettingData.CountrySettingCode = _ICompanySettingMasterView.CountrySettingCode;                    
                    //RequestData.CompanySettingData.RetailSettingID = _ICompanySettingMasterView.RetailSettingID;
                    RequestData.CompanySettingData.Remarks = _ICompanySettingMasterView.Remarks;
                    RequestData.CompanySettingData.Active = _ICompanySettingMasterView.Active;
                    RequestData.CompanySettingData.CreateBy = 1;

                    SaveCompanySettingResponse ResponseData = _CompanySettingBLL.SaveCompanySetting(RequestData);

                    _ICompanySettingMasterView.Message = ResponseData.DisplayMessage;
                    _ICompanySettingMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _ICompanySettingMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public void UpdateCompanySetting()
         {
             try
             {
                 if (IsValidForm())
                 {
                     var RequestData = new UpdateCompanySettingRequest();
                     RequestData.CompanySettingData = new CompanySettings();

                     RequestData.CompanySettingData.ID = _ICompanySettingMasterView.ID;
                     RequestData.CompanySettingData.CompanyCode = _ICompanySettingMasterView.CompanyCode;
                     RequestData.CompanySettingData.CompanyName = _ICompanySettingMasterView.CompanyName;
                     RequestData.CompanySettingData.Address = _ICompanySettingMasterView.Address;
                     RequestData.CompanySettingData.CountrySettingID = _ICompanySettingMasterView.CountrySettingID;
                     RequestData.CompanySettingData.CountrySettingCode = _ICompanySettingMasterView.CountrySettingCode;  
                     //RequestData.CompanySettingData.RetailSettingID = _ICompanySettingMasterView.RetailSettingID;
                     RequestData.CompanySettingData.Remarks = _ICompanySettingMasterView.Remarks;
                     RequestData.CompanySettingData.Active = _ICompanySettingMasterView.Active;
                     RequestData.CompanySettingData.CreateBy = 1;
                     RequestData.CompanySettingData.SCN = _ICompanySettingMasterView.SCN;

                     UpdateCompanySettingResponse ResponseData = _CompanySettingBLL.UpdateCompanySetting(RequestData);

                     _ICompanySettingMasterView.Message = ResponseData.DisplayMessage;
                     _ICompanySettingMasterView.ProcessStatus = ResponseData.StatusCode;

                 }
                 else
                 {
                     _ICompanySettingMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
            
         }


        public void DeleteCompanySetting()
        {
            try
            {
                var RequestData = new DeleteCompanySettingRequest();
                RequestData.CompanySettingData = new CompanySettings();

                RequestData.CompanySettingData.ID = _ICompanySettingMasterView.ID;
                RequestData.CompanySettingData.CompanyCode = _ICompanySettingMasterView.CompanyCode;
                RequestData.CompanySettingData.CompanyName = _ICompanySettingMasterView.CompanyName;
                RequestData.CompanySettingData.Address = _ICompanySettingMasterView.Address;
                RequestData.CompanySettingData.CountrySettingID = _ICompanySettingMasterView.CountrySettingID;
                RequestData.CompanySettingData.CountrySettingCode = _ICompanySettingMasterView.CountrySettingCode;  
                //RequestData.CompanySettingData.RetailSettingID = _ICompanySettingMasterView.RetailSettingID;
                RequestData.CompanySettingData.Remarks = _ICompanySettingMasterView.Remarks;
                RequestData.CompanySettingData.Active = _ICompanySettingMasterView.Active;
                RequestData.CompanySettingData.CreateBy = 1;

                var ResponseData = _CompanySettingBLL.DeleteCompanySetting(RequestData);

                _ICompanySettingMasterView.Message = ResponseData.DisplayMessage;
                _ICompanySettingMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }                
           
         }



         public void SelectCompanySettings()
         {
             try
             {
                 var RequestData = new SelectByIDCompanySettingRequest();
                 RequestData.ID = _ICompanySettingMasterView.ID;
                 var ResponseData = _CompanySettingBLL.SelectByIDCompanySetting(RequestData);
                 _ICompanySettingMasterView.CompanyCode = ResponseData.CompanySettings.CompanyCode;
                 _ICompanySettingMasterView.CompanyName = ResponseData.CompanySettings.CompanyName;
                 _ICompanySettingMasterView.Address = ResponseData.CompanySettings.Address;
                 _ICompanySettingMasterView.CountrySettingID = ResponseData.CompanySettings.CountrySettingID;                               
                 //_ICompanySettingMasterView.RetailSettingID = ResponseData.CompanySettings.RetailSettingID;
                 _ICompanySettingMasterView.Remarks = ResponseData.CompanySettings.Remarks;
                 _ICompanySettingMasterView.Active = ResponseData.CompanySettings.Active;
                 _ICompanySettingMasterView.SCN = ResponseData.CompanySettings.SCN;
                 if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                 {
                     _ICompanySettingMasterView.Message = ResponseData.DisplayMessage;
                 }
                 else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                 {
                     _ICompanySettingMasterView.Message = ResponseData.DisplayMessage;
                 }

                 _ICompanySettingMasterView.ProcessStatus = ResponseData.StatusCode;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void GetCountryMasterLookUp()
         {
             try
             {
                 var RequestData = new SelectCountryLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _ICompanySettingMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
           
         }


         public void GetRetailSettingsLookUp()
         {
             try
             {
                 var RequestData = new SelectRetailSettingsLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _RetailSettingsBLL.SelectRetailSettingsLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     //_ICompanySettingMasterView.RetailSettingsLookUp = ResponseData.RetailSettingsList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
            
         }

    }



    public class CompanySettingMasterListPresenter
    {
        ICompanySettingCollectionList _ICompanySettingCollectionList;
        CompanySettingBLL _CompanySettingBLL = new CompanySettingBLL();


        public CompanySettingMasterListPresenter(ICompanySettingCollectionList ViewObj)
        {
            _ICompanySettingCollectionList = ViewObj;
        }




        public void GetCompanySetting()
        {

            try
            {
                var RequestData = new SelectAllCompanySettingRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCompanySettingResponse();
                ResponseData = _CompanySettingBLL.SelectAllCompanySettingResponse(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICompanySettingCollectionList.CompanySettingsList = ResponseData.CompanySettingList;
                }
                else
                {
                    _ICompanySettingCollectionList.CompanySettingsList = ResponseData.CompanySettingList;
                    _ICompanySettingCollectionList.Message = ResponseData.DisplayMessage;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

    }
}
