using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.Vendor;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.VendorGroupMasterRequest;
using EasyBizRequest.Masters.VendorMasterRequest;
using EasyBizResponse.Masters.CompanySettingResponse;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.VendorGroupMasterResponse;
using EasyBizResponse.Masters.VendorMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class VendorPresenter
    {
        IVendorView _IVendorView;
        VendorMasterBLL _VendorMasterBLL = new VendorMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        CompanySettingBLL _CompanySettingBLL = new CompanySettingBLL();
        VendorGroupMasterBLL _VendorGroupMasterBLL = new VendorGroupMasterBLL();
     
         public VendorPresenter(IVendorView ViewObj)
        {
            _IVendorView = ViewObj;
        }
         public bool IsValidForm()
         {
             bool objBool = false;
             if (_IVendorView.VendorCode.Trim() == string.Empty)
             {
                 _IVendorView.Message = "Vendor Code is missing Please Enter it.";
             }
             else if (_IVendorView.VendorCode.Length > 8)
             {
                 _IVendorView.Message = " Vendor Code not allow more than eight Character.";
             }
             else if (_IVendorView.VendorName.Trim() == string.Empty)
             {
                 _IVendorView.Message = "Vendor Name is missing Please Enter it.";
             }
             else if (_IVendorView.VendorGroupID == 0)
             {
                 _IVendorView.Message = "Vendor GroupName is missing Please Select it.";
             }
             else if (_IVendorView.CountryID == 0)
             {
                 _IVendorView.Message = "Country Name is missing Please Select it.";
             }
             else if (_IVendorView.CompanyID == 0)
             {
                 _IVendorView.Message = "Company Name is missing Please Select it.";
             }
            
            
             else
             {
                 objBool = true;
             }
             return objBool;
         }
         public void SaveVendor()
         {
             try
             {
                 if (IsValidForm())
                 {
                     var RequestData = new SaveVendorRequest();
                     RequestData.VendorRecord = new VendorMaster();

                     RequestData.VendorRecord.ID = _IVendorView.ID;
                     RequestData.VendorRecord.VendorCode = _IVendorView.VendorCode;
                     RequestData.VendorRecord.VendorName = _IVendorView.VendorName;
                     RequestData.VendorRecord.ShortName = _IVendorView.ShortName;
                     RequestData.VendorRecord.PhoneNumber = _IVendorView.PhoneNumber;
                     RequestData.VendorRecord.CountryID = _IVendorView.CountryID;
                     RequestData.VendorRecord.CompanyID = _IVendorView.CompanyID;
                     RequestData.VendorRecord.VendorGroupID = _IVendorView.VendorGroupID;
                     RequestData.VendorRecord.Address = _IVendorView.Address;
                     RequestData.VendorRecord.EmailID = _IVendorView.EmailID;
                     RequestData.VendorRecord.CreateBy = _IVendorView.UserID;
                     RequestData.VendorRecord.CreateOn = DateTime.Now;
                     RequestData.VendorRecord.Active = _IVendorView.Active;
                     RequestData.VendorRecord.SCN = _IVendorView.SCN;
                     RequestData.VendorRecord.Remarks = _IVendorView.Remarks;
                     var ResponseData = _VendorMasterBLL.SaveVendor(RequestData);
                     _IVendorView.Message = ResponseData.DisplayMessage;
                     _IVendorView.ProcessStatus = ResponseData.StatusCode;
                 }
                 else
                 {
                     _IVendorView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void UpdateVendor()
         {
             try
             {
                 if (IsValidForm())
                 {
                     var RequestData = new UpdateVendorRequest();
                     RequestData.VendorRecord = new VendorMaster();
                     RequestData.VendorRecord.ID = _IVendorView.ID;
                     RequestData.VendorRecord.VendorCode = _IVendorView.VendorCode;
                     RequestData.VendorRecord.VendorName = _IVendorView.VendorName;
                     RequestData.VendorRecord.ShortName = _IVendorView.ShortName;
                     RequestData.VendorRecord.PhoneNumber = _IVendorView.PhoneNumber;
                     RequestData.VendorRecord.CountryID = _IVendorView.CountryID;
                     RequestData.VendorRecord.CompanyID = _IVendorView.CompanyID;
                     RequestData.VendorRecord.VendorGroupID = _IVendorView.VendorGroupID;
                     RequestData.VendorRecord.Address = _IVendorView.Address;
                     RequestData.VendorRecord.EmailID = _IVendorView.EmailID;
                     RequestData.VendorRecord.UpdateBy = _IVendorView.UserID;
                     RequestData.VendorRecord.UpdateOn = DateTime.Now;
                     RequestData.VendorRecord.Active = true;
                     RequestData.VendorRecord.SCN = _IVendorView.SCN;
                     RequestData.VendorRecord.Remarks = _IVendorView.Remarks;
                     RequestData.VendorRecord.Active = _IVendorView.Active;
                     var ResponseData = _VendorMasterBLL.UpdateVendor(RequestData);
                     _IVendorView.Message = ResponseData.DisplayMessage;
                     _IVendorView.ProcessStatus = ResponseData.StatusCode;
                 }
                 else
                 {
                     _IVendorView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SelectVendorRecord()
         {
             try
             {
                 var RequestData = new SelectByVendorIDRequest();
                 RequestData.ID = _IVendorView.ID;
                 var ResponseData = _VendorMasterBLL.SelectVendorRecord(RequestData);
                 _IVendorView.VendorCode = ResponseData.VendorRecord.VendorCode;
                 _IVendorView.VendorName = ResponseData.VendorRecord.VendorName;
                 _IVendorView.Address = ResponseData.VendorRecord.Address;
                 _IVendorView.ShortName = ResponseData.VendorRecord.ShortName;
                 _IVendorView.VendorGroupID = ResponseData.VendorRecord.VendorGroupID;
                 _IVendorView.CountryID = ResponseData.VendorRecord.CountryID;
                 _IVendorView.CompanyID = ResponseData.VendorRecord.CompanyID;
                 _IVendorView.EmailID = ResponseData.VendorRecord.EmailID;
                 _IVendorView.PhoneNumber = ResponseData.VendorRecord.PhoneNumber;
                 _IVendorView.SCN = ResponseData.VendorRecord.SCN;
                 _IVendorView.Remarks = ResponseData.VendorRecord.Remarks;
                 _IVendorView.Active = ResponseData.VendorRecord.Active;

                 if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                 {
                     _IVendorView.Message = ResponseData.DisplayMessage;
                 }
                 else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                 {
                     _IVendorView.Message = ResponseData.DisplayMessage;
                 }
                 _IVendorView.ProcessStatus = ResponseData.StatusCode;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void DeleteVendor()
         {
             try
             {
                 var RequestData = new DeleteVendorRequest();
                 RequestData.ID = _IVendorView.ID;
                 var ResponseData = _VendorMasterBLL.DeleteVendor(RequestData);
                 _IVendorView.Message = ResponseData.DisplayMessage;
                 _IVendorView.ProcessStatus = ResponseData.StatusCode;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetVendorGroupLookUp()
         {
             try
             {
                 var RequestData = new SelectVendorGroupLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _VendorGroupMasterBLL.VendorGroupLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IVendorView.VendorGroupLookUp = ResponseData.VendorGroupList;
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
                     _IVendorView.CountryLookUp = ResponseData.CountryMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetCompanyLookUp()
         {
             try
             {
                 var RequestData = new SelectCompanySettingsLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.CountryID = _IVendorView.CountryID;
                 var ResponseData = _CompanySettingBLL.SelectCompanySettingsLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IVendorView.CompanyLookUp = ResponseData.CompanySettingsList;
                 }
                  else
                 {
                     _IVendorView.CompanyLookUp = new List<CompanySettings>();
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
     
    }
   public class VendorListPresenter
   {
       VendorMasterBLL _VendorMasterBLL = new VendorMasterBLL();
       IVendorCollectionView _IVendorCollectionView;
       public VendorListPresenter(IVendorCollectionView ViewObj)
       {
           _IVendorCollectionView = ViewObj;
       }
       public void GetVendorList()
       {
           try
           {
               var RequestData = new SelectAllVendorRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllVendorResponse();
               ResponseData = _VendorMasterBLL.SelectAllVendorRecord(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IVendorCollectionView.VendorList = ResponseData.VendorList;
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
