using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IStoreMaster;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CityMasterRequest;
using EasyBizRequest.Masters.CompanySettingRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.FranchiseRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StoreMasterResponse;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.CityMasterResponse;
using EasyBizResponse.Masters.StateMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class StoreMasterPresenter
    {

        IStoreMasterView _IStoreMasterView;

        CountryBLL _CountryBLL = new CountryBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        CompanySettingBLL _CompanySettingBLL = new CompanySettingBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        TaxBLL _TaxBLL = new TaxBLL();
        FranchiseBLL _FranchiseBLL = new FranchiseBLL();
        RetailSettingsBLL _RetailSettingsBLL = new RetailSettingsBLL();
        StateMasterBLL _StateBLL = new StateMasterBLL();
        CityMasterBLL _CityBLL = new CityMasterBLL();

        public StoreMasterPresenter(IStoreMasterView ViewObj)
        {
            _IStoreMasterView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            _IStoreMasterView.ProcessStatus = Enums.OpStatusCode.InvalidInput;

            if (_IStoreMasterView.StoreCode.Trim() == string.Empty)
            {
                _IStoreMasterView.Message = "Store Code is missing Please Enter it.";
            }
            else if (_IStoreMasterView.StoreName.Trim() == string.Empty)
            {
                _IStoreMasterView.Message = "Name is missing Please Enter it.";
            }
            else if (_IStoreMasterView.CountrySetting == 0)
            {
                _IStoreMasterView.Message = "Select Country";
            }
            else if (_IStoreMasterView.StoreGroup == 0)
            {
                _IStoreMasterView.Message = "Select Group";
            }
            else if (_IStoreMasterView.StoreCompany == 0)
            {
                _IStoreMasterView.Message = "Select Company";
            }
            else if (_IStoreMasterView.PriceListID == 0)
            {
                _IStoreMasterView.Message = "Select PriceList";
            }
            else if (_IStoreMasterView.Brand == "")
            {
                _IStoreMasterView.Message = "Select Brand";
            }
            else if (_IStoreMasterView.StoreType == "")
            {
                _IStoreMasterView.Message = "Select Store Type";
            }
            else if(_IStoreMasterView.StoreImage == null)
            {
                _IStoreMasterView.Message = "Store Image is Missing";
            }
            else if(_IStoreMasterView.FranchiseCode == "")
            {
                _IStoreMasterView.Message = "Select Franchise";
            }
            else if (_IStoreMasterView.FranchiseID == 0)
            {
                _IStoreMasterView.Message = "Select Franchise";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void GetCuntryMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SaveStoreMaster()
        {
            try
            {

                if (IsValidForm())
                {

                    var RequestData = new SaveStoreMasterRequest();
                    RequestData.StoreMasterRecord = new StoreMaster();
                    RequestData.StoreMasterRecord.SelectStoreBrandMappingList = _IStoreMasterView.StoreBrandMappingList;
                    RequestData.StoreMasterRecord.ID = _IStoreMasterView.ID;
                    RequestData.StoreMasterRecord.StoreCode = _IStoreMasterView.StoreCode;
                    RequestData.StoreMasterRecord.StoreName = _IStoreMasterView.StoreName;
                    RequestData.StoreMasterRecord.CountrySetting = _IStoreMasterView.CountrySetting;
                    RequestData.StoreMasterRecord.StateID = _IStoreMasterView.StateID;
                    RequestData.StoreMasterRecord.StateCode = _IStoreMasterView.StateCode;
                    RequestData.StoreMasterRecord.CountryCode = _IStoreMasterView.CountryCode;
                    RequestData.StoreMasterRecord.StoreGroup = _IStoreMasterView.StoreGroup;
                    RequestData.StoreMasterRecord.StoreGroupCode = _IStoreMasterView.StoreGroupCode;
                    RequestData.StoreMasterRecord.StoreCompany = _IStoreMasterView.StoreCompany;
                    RequestData.StoreMasterRecord.StoreCompanyCode = _IStoreMasterView.StoreCompanyCode;
                    RequestData.StoreMasterRecord.PriceListID = _IStoreMasterView.PriceListID;
                    RequestData.StoreMasterRecord.PriceListCode = _IStoreMasterView.PriceListCode;
                    RequestData.StoreMasterRecord.RetailID = _IStoreMasterView.RetailID;
                    RequestData.StoreMasterRecord.TaxID = _IStoreMasterView.TaxID;
                    RequestData.StoreMasterRecord.Brand = _IStoreMasterView.Brand;               
                    RequestData.StoreMasterRecord.ShopBrand = _IStoreMasterView.ShopBrand;
                    RequestData.StoreMasterRecord.StoreType = _IStoreMasterView.StoreType;
                    RequestData.StoreMasterRecord.Remarks = _IStoreMasterView.Remarks;
                    RequestData.StoreMasterRecord.Address = _IStoreMasterView.Address;
                    RequestData.StoreMasterRecord.Location = _IStoreMasterView.Location;
                    RequestData.StoreMasterRecord.NoOfOptions = _IStoreMasterView.NoOfOptions;
                    RequestData.StoreMasterRecord.StoreSize = _IStoreMasterView.StoreSize;
                    RequestData.StoreMasterRecord.StartDate = _IStoreMasterView.StartDate;
                    RequestData.StoreMasterRecord.EndDate = _IStoreMasterView.EndDate;
                    RequestData.StoreMasterRecord.Grade = _IStoreMasterView.Grade;
                    RequestData.StoreMasterRecord.StoreHeader = _IStoreMasterView.StoreHeader;
                    RequestData.StoreMasterRecord.StoreFooter = _IStoreMasterView.StoreFooter;
                    RequestData.StoreMasterRecord.PrintCount = _IStoreMasterView.PrintCount;
                    RequestData.StoreMasterRecord.ReturnPrintCount = _IStoreMasterView.ReturnPrintCount;
                    RequestData.StoreMasterRecord.ExchangePrintCount = _IStoreMasterView.ExchangePrintCount;
                    RequestData.StoreMasterRecord.StoreImage = _IStoreMasterView.StoreImage;
                    RequestData.StoreMasterRecord.LicenseImage = _IStoreMasterView.LicenseImage;
                    //RequestData.StoreImageList = _IStoreMasterView.StoreImageList;
                    RequestData.StoreBrandMappingList = _IStoreMasterView.StoreBrandMappingList;
                    RequestData.StoreMasterRecord.EmailTemplate = _IStoreMasterView.EmailTemplate;
                    RequestData.StoreMasterRecord.SMSTemplate = _IStoreMasterView.SMSTemplate;
                    RequestData.StoreMasterRecord.EnableOnlineStock = _IStoreMasterView.EnableOnlineStock;
                    RequestData.StoreMasterRecord.EnableOrderFulFillment = _IStoreMasterView.EnableOrderFulFillment;
                    RequestData.StoreMasterRecord.EnableFingerPrint = _IStoreMasterView.EnableFingerPrint;
                    RequestData.StoreMasterRecord.CityID = _IStoreMasterView.CityID;


                    RequestData.StoreMasterRecord.CreateBy = 1;
                     RequestData.StoreMasterRecord.CreateOn = DateTime.Now;
                    RequestData.StoreMasterRecord.Active = _IStoreMasterView.Active;
                    RequestData.StoreMasterRecord.SCN = _IStoreMasterView.SCN;
                    RequestData.StoreMasterRecord.StoreType = _IStoreMasterView.StoreType;

                    var ResponseData = _StoreMasterBLL.SaveStoreMaster(RequestData);

                    _IStoreMasterView.Message = ResponseData.DisplayMessage;
                    _IStoreMasterView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

           
        }



        public void UpdateStoreMaster()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new UpdateStoreMasterRequest();
                    RequestData.StoreMasterRecord = new StoreMaster();
                    RequestData.StoreMasterRecord.SelectStoreBrandMappingList = _IStoreMasterView.StoreBrandMappingList;
                    RequestData.StoreMasterRecord.ID = _IStoreMasterView.ID;
                    RequestData.StoreMasterRecord.StoreCode = _IStoreMasterView.StoreCode;
                    RequestData.StoreMasterRecord.StoreName = _IStoreMasterView.StoreName;
                    RequestData.StoreMasterRecord.CountrySetting = _IStoreMasterView.CountrySetting;
                    RequestData.StoreMasterRecord.StateID = _IStoreMasterView.StateID;
                    RequestData.StoreMasterRecord.StateCode = _IStoreMasterView.StateCode;
                    RequestData.StoreMasterRecord.CountryCode = _IStoreMasterView.CountryCode;
                    RequestData.StoreMasterRecord.StoreGroup = _IStoreMasterView.StoreGroup;
                    RequestData.StoreMasterRecord.StoreGroupCode = _IStoreMasterView.StoreGroupCode;
                    RequestData.StoreMasterRecord.StoreCompany = _IStoreMasterView.StoreCompany;
                    RequestData.StoreMasterRecord.StoreCompanyCode = _IStoreMasterView.StoreCompanyCode;
                    RequestData.StoreMasterRecord.PriceListID = _IStoreMasterView.PriceListID;
                    RequestData.StoreMasterRecord.PriceListCode = _IStoreMasterView.PriceListCode;
                    RequestData.StoreMasterRecord.RetailID = _IStoreMasterView.RetailID;
                    RequestData.StoreMasterRecord.TaxID = _IStoreMasterView.TaxID;
                    RequestData.StoreMasterRecord.Brand = _IStoreMasterView.Brand;
                    RequestData.StoreMasterRecord.ShopBrand = _IStoreMasterView.ShopBrand;
                    RequestData.StoreMasterRecord.StoreType = _IStoreMasterView.StoreType;
                    RequestData.StoreMasterRecord.CreateBy = 1;
                    RequestData.StoreMasterRecord.CreateOn = DateTime.Now;                  
                    RequestData.StoreMasterRecord.SCN = _IStoreMasterView.SCN;
                    RequestData.StoreMasterRecord.Remarks = _IStoreMasterView.Remarks;
                    RequestData.StoreMasterRecord.Address = _IStoreMasterView.Address;
                    RequestData.StoreMasterRecord.Location = _IStoreMasterView.Location;
                    RequestData.StoreMasterRecord.NoOfOptions = _IStoreMasterView.NoOfOptions;
                    RequestData.StoreMasterRecord.StoreSize = _IStoreMasterView.StoreSize;
                    RequestData.StoreMasterRecord.Active = _IStoreMasterView.Active;
                    RequestData.StoreMasterRecord.StoreType = _IStoreMasterView.StoreType;
                    RequestData.StoreMasterRecord.StartDate = _IStoreMasterView.StartDate;
                    RequestData.StoreMasterRecord.EndDate = _IStoreMasterView.EndDate;
                    RequestData.StoreMasterRecord.Grade = _IStoreMasterView.Grade;
                    RequestData.StoreMasterRecord.StoreHeader = _IStoreMasterView.StoreHeader;
                    RequestData.StoreMasterRecord.StoreFooter = _IStoreMasterView.StoreFooter;
                    RequestData.StoreMasterRecord.PrintCount = _IStoreMasterView.PrintCount;
                    RequestData.StoreMasterRecord.ReturnPrintCount = _IStoreMasterView.ReturnPrintCount;
                    RequestData.StoreMasterRecord.ExchangePrintCount = _IStoreMasterView.ExchangePrintCount;
                    RequestData.StoreMasterRecord.StoreImage = _IStoreMasterView.StoreImage;
                    RequestData.StoreMasterRecord.LicenseImage = _IStoreMasterView.LicenseImage;
                    RequestData.StoreMasterRecord.DiskID = _IStoreMasterView.DiskID;
                    RequestData.StoreMasterRecord.CPUID = _IStoreMasterView.CPUID;
                    //RequestData.StoreImageList = _IStoreMasterView.StoreImageList;
                    RequestData.StoreBrandMappingList = _IStoreMasterView.StoreBrandMappingList;
                    RequestData.StoreMasterRecord.EmailTemplate = _IStoreMasterView.EmailTemplate;
                    RequestData.StoreMasterRecord.SMSTemplate = _IStoreMasterView.SMSTemplate;
                    RequestData.StoreMasterRecord.EnableOnlineStock = _IStoreMasterView.EnableOnlineStock;
                    RequestData.StoreMasterRecord.EnableOrderFulFillment = _IStoreMasterView.EnableOrderFulFillment;
                    RequestData.StoreMasterRecord.EnableFingerPrint = _IStoreMasterView.EnableFingerPrint;
                    RequestData.StoreMasterRecord.CityID = _IStoreMasterView.CityID;

                    var ResponseData = _StoreMasterBLL.UpdateStoreMaster(RequestData);

                    _IStoreMasterView.Message = ResponseData.DisplayMessage;
                    _IStoreMasterView.ProcessStatus = ResponseData.StatusCode;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            


        }

        public void DeleteStoreMaster()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new DeleteStoreMasterRequest();
                    RequestData.ID = _IStoreMasterView.ID;
                    var ResponseData = _StoreMasterBLL.DeleteStoreMaster(RequestData);
                    _IStoreMasterView.Message = ResponseData.DisplayMessage;
                    _IStoreMasterView.ProcessStatus = ResponseData.StatusCode;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }         

        }
        public void UpdateUniqueID()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new UpdateUniqueIDRequest();                   
                    var ResponseData = _StoreMasterBLL.UpdateUniqueID(RequestData);
                    _IStoreMasterView.Message = ResponseData.DisplayMessage;
                    _IStoreMasterView.ProcessStatus = ResponseData.StatusCode;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectByIDStoreMaster()
        {
            try
            {

                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ID = _IStoreMasterView.ID;
                var ResponseData = _StoreMasterBLL.SelectByIDStoreMaster(RequestData);
                _IStoreMasterView.StoreCode = ResponseData.StoreMasterData.StoreCode;
                _IStoreMasterView.StoreName = ResponseData.StoreMasterData.StoreName;
                _IStoreMasterView.CountrySetting = ResponseData.StoreMasterData.CountrySetting;
                _IStoreMasterView.StateID = ResponseData.StoreMasterData.StateID;
                _IStoreMasterView.StoreCompany = ResponseData.StoreMasterData.StoreCompany;
                _IStoreMasterView.PriceListID = ResponseData.StoreMasterData.PriceListID;
                _IStoreMasterView.RetailID = ResponseData.StoreMasterData.RetailID;
                _IStoreMasterView.TaxID = ResponseData.StoreMasterData.TaxID;
                _IStoreMasterView.StoreGroup = ResponseData.StoreMasterData.StoreGroup;
                _IStoreMasterView.Brand = ResponseData.StoreMasterData.Brand;
                _IStoreMasterView.ShopBrand = ResponseData.StoreMasterData.ShopBrand;
                _IStoreMasterView.StoreType = ResponseData.StoreMasterData.StoreType;
                _IStoreMasterView.SCN = ResponseData.StoreMasterData.SCN;
                _IStoreMasterView.Remarks = ResponseData.StoreMasterData.Remarks;
                _IStoreMasterView.Address = ResponseData.StoreMasterData.Address;
                _IStoreMasterView.Location = ResponseData.StoreMasterData.Location;
                _IStoreMasterView.NoOfOptions = ResponseData.StoreMasterData.NoOfOptions;
                _IStoreMasterView.StoreSize = ResponseData.StoreMasterData.StoreSize;
                _IStoreMasterView.Active = ResponseData.StoreMasterData.Active;
                _IStoreMasterView.StartDate = ResponseData.StoreMasterData.StartDate;
                _IStoreMasterView.EndDate = ResponseData.StoreMasterData.EndDate;
                _IStoreMasterView.Grade = ResponseData.StoreMasterData.Grade;
                _IStoreMasterView.StoreHeader = ResponseData.StoreMasterData.StoreHeader;
                _IStoreMasterView.StoreFooter = ResponseData.StoreMasterData.StoreFooter;
                _IStoreMasterView.PrintCount = ResponseData.StoreMasterData.PrintCount;
                _IStoreMasterView.ReturnPrintCount = ResponseData.StoreMasterData.ReturnPrintCount;
                _IStoreMasterView.ExchangePrintCount = ResponseData.StoreMasterData.ExchangePrintCount;
                _IStoreMasterView.StoreImage = ResponseData.StoreMasterData.StoreImage;
                _IStoreMasterView.LicenseImage = ResponseData.StoreMasterData.LicenseImage;
                _IStoreMasterView.DiskID = ResponseData.StoreMasterData.DiskID;
                _IStoreMasterView.CPUID = ResponseData.StoreMasterData.CPUID;
                _IStoreMasterView.EmailTemplate = ResponseData.StoreMasterData.EmailTemplate;
                _IStoreMasterView.SMSTemplate = ResponseData.StoreMasterData.SMSTemplate;
                _IStoreMasterView.FranchiseID = ResponseData.StoreMasterData.FranchiseID;
                //_IStoreMasterView.FranchiseCode = ResponseData.StoreMasterData.FranchiseCode;
                _IStoreMasterView.EnableOnlineStock = ResponseData.StoreMasterData.EnableOnlineStock;
                _IStoreMasterView.EnableOrderFulFillment = ResponseData.StoreMasterData.EnableOrderFulFillment;
                _IStoreMasterView.EnableFingerPrint = ResponseData.StoreMasterData.EnableFingerPrint;
                _IStoreMasterView.CityID = ResponseData.StoreMasterData.CityID;

                _IStoreMasterView.SelectByIdStoreBrandMappingList = ResponseData.StoreMasterData.SelectStoreBrandMappingList;

                //var list = new List<StoreMaster>();
                //list.Add(ResponseData.StoreMasterData);
                //_IStoreMasterView = list;
                
                _IStoreMasterView.ProcessStatus = ResponseData.StatusCode;

                if (ResponseData.StatusCode != Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.Message = ResponseData.DisplayMessage;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetStoreGroupMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStoreGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StoreGroupBLL.SelectStoreGroupMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.StoreGroupMasterLookUp = ResponseData.StoreGroupMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCompanySettingLookUp()
        {
            try
            {
                var RequestData = new SelectCompanySettingsLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = _IStoreMasterView.CountrySetting;
                //RequestData.CountrySettingCode = _IStoreMasterView.CountryCode;
                var ResponseData = _CompanySettingBLL.SelectCompanySettingsLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.CompanySettingsLookUp = ResponseData.CompanySettingsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStateLookUP()
        {
            SelectStateLookUpRequest RequestData = new SelectStateLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IStoreMasterView.CountrySetting;
            SelectStateLookUpResponse ResponseData = _StateBLL.SelectStateLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreMasterView.StateMasterLookUp = ResponseData.StateMasterList;
            }
        }

        public void GetBrandMasterLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.BrandMasterLookUp = ResponseData.BrandList;
                    _IStoreMasterView.ShopBrandMasterLookUp = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetSalesCurrencyLookUp()
        {
            try
            {
                var RequestData = new SelectCurrencyLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CurrencyType = "Sales";
                var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.CurrencyMasterLookUp = ResponseData.CurrencyMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetPriceLookUp()
        {
            var RequestData = new SelectPriceListLookUPRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.Type = "Sales";
            var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreMasterView.PriceListLookUp = ResponseData.PriceListTypeData;
            }
        }

        public void GetRetailLookUp()
        {
            var RequestData = new SelectRetailSettingsLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _RetailSettingsBLL.SelectRetailSettingsLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreMasterView.RetailSettingsListLookUp = ResponseData.RetailSettingsList;
            }
        }

        public void GetTaxLookUp()
        {
            var RequestData = new SelectTaxLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _TaxBLL.SelectTaxLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreMasterView.TaxMasterLookUp = ResponseData.TaxList;
            }
        }
        public void GetFranchise()
        {
            try
            {
                var RequestData = new SelectFranchiseLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _FranchiseBLL.FranchiseLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.FranchiseTypeLookUp = ResponseData.FranchiseList;
                }
            }
            catch
            {

            }

        }

        public void GetGrade()
        {
            try
            {
                var RequestData = new SelectStoreGradeLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StoreMasterBLL.GradeLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterView.StoreGradeLookUp = ResponseData.StoreGradeList;
                }
            }
            catch
            {

            }

        }

        public void GetCityLookUP()
        {
            SelectCityLookUPRequest RequestData = new SelectCityLookUPRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StateID = _IStoreMasterView.StateID;
            SelectCityLookUPResponse ResponseData = _CityBLL.SelectCityLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IStoreMasterView.CityMasterLookUp = ResponseData.CityMasterList;
            }
        }
    }


    public class StoreMasterList
    {
        IStoreMasterList _IStoreMasterList;
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public StoreMasterList(IStoreMasterList ViewObj)        
        {

            _IStoreMasterList = ViewObj;
        }


        public void GetStoreMasterList()
        {
            try
            {
                var RequestData = new SelectAllStoreMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStoreMasterResponse();
                ResponseData = _StoreMasterBLL.SelectAllStoreMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStoreMasterList.StoreMasterList = ResponseData.StoreMasterList;                    
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

