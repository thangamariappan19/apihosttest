using EasyBizBLL.Masters;
using EasyBizBLL.SalesTargetBLL;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizIView.Transactions.IPOSOperations.ISalesTarget;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizRequest.SalesTargetRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.SalesTargetResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POSOperations
{
   public class SalesTargetPresenter
    {
       IsalesTargetView _IsalesTargetView;
       CountryBLL _CountryBLL = new CountryBLL();
       StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
       YearBLL _YearBLL = new YearBLL();
       BrandBLL _BrandBLL = new BrandBLL();
       SalesTargetBLL _SalesTargetBLL =new SalesTargetBLL();

       public SalesTargetPresenter(IsalesTargetView ViewObj)
        {
            _IsalesTargetView = ViewObj;
        }
       public void GetCountryLookUP()
       {
           SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
           RequestData.ShowInActiveRecords = false;
           SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IsalesTargetView.CountryMasterLookUp = ResponseData.CountryMasterList;
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
                   _IsalesTargetView.BrandMasterLookUp = ResponseData.BrandList;                  
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public void GetYearLookUp()
       {
           try
           {
               var RequestData = new SelectYearLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _YearBLL.YearLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IsalesTargetView.YearLookUp = ResponseData.YearList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetStoreMasterLookUp()
       {
           try
           {
               var RequestData = new SelectStoreMasterLookUpRequest();
               RequestData.CountryID = _IsalesTargetView.CountryID;
               RequestData.BrandID = _IsalesTargetView.BrandID;
               RequestData.type = "SalesTarget";
                   RequestData.ShowInActiveRecords = false;
               var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IsalesTargetView.StoreMasterList = ResponseData.StoreMasterList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetSalesHistory()
       {
           try
           {
               var RequestData = new SalestargetHistoryRequest();
               RequestData.Year = _IsalesTargetView.Year;
               RequestData.StoreIDs = _IsalesTargetView.StoreIDs;              
               RequestData.ShowInActiveRecords = false;
               var ResponseData = _SalesTargetBLL.SelectSalesHistory(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IsalesTargetView.SalestargetDetailsList = ResponseData.SalestargetDetailsList;
               }
               else
               {
                   _IsalesTargetView.SalestargetDetailsList = ResponseData.SalestargetDetailsList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SelectByIDSalesTargetHeader()
       {
           try
           {
               var RequestData = new SelectByIDSalesTargetRequest();
               RequestData.ID = _IsalesTargetView.ID;
               var ResponseData = _SalesTargetBLL.SelectByIDSalesTargetHeader(RequestData);
               _IsalesTargetView.Year = ResponseData.SalesTargetHeaderData.Year;
               _IsalesTargetView.Brand = ResponseData.SalesTargetHeaderData.Brand;
               _IsalesTargetView.BrandID = ResponseData.SalesTargetHeaderData.BrandID;
               _IsalesTargetView.StoreIDs = ResponseData.SalesTargetHeaderData.StoreIDs;
               _IsalesTargetView.CountryID = ResponseData.SalesTargetHeaderData.CountryID;

               _IsalesTargetView.SCN = ResponseData.SalesTargetHeaderData.SCN;

               if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
               {
                   _IsalesTargetView.Message = ResponseData.DisplayMessage;
               }

               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _IsalesTargetView.Message = ResponseData.DisplayMessage;
               }

               _IsalesTargetView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public void SelectByIDSalesTargetDetails()
       {
           try
           {
               var RequestData = new SelectSalesTargetDetailsRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.ID = _IsalesTargetView.ID;
               var ResponseData = _SalesTargetBLL.SelectByIDAFSegamationDetils(RequestData);

               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IsalesTargetView.SalestargetDetailsList = ResponseData.SalestargetDetailsRecord;
               }

               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _IsalesTargetView.Message = ResponseData.DisplayMessage;
               }

               _IsalesTargetView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       public void SaveDocumentNumberingMaster()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveSalesTargetRequest();
                   RequestData.SalestargetDetailsList = _IsalesTargetView.SalestargetDetailsList;
                   RequestData.SalesTargetHeaderRecord = new SalesTargetHeader();
                   RequestData.SalesTargetHeaderRecord.ID = _IsalesTargetView.ID;
                   RequestData.SalesTargetHeaderRecord.CountryID = _IsalesTargetView.CountryID;
                   RequestData.SalesTargetHeaderRecord.BrandID = _IsalesTargetView.BrandID;
                   RequestData.SalesTargetHeaderRecord.Brand = _IsalesTargetView.Brand;
                   RequestData.SalesTargetHeaderRecord.StoreIDs = _IsalesTargetView.StoreIDs;
                   RequestData.SalesTargetHeaderRecord.Year = _IsalesTargetView.Year;                 
                   RequestData.SalesTargetHeaderRecord.CreateBy = _IsalesTargetView.UserID;
                   RequestData.SalesTargetHeaderRecord.CreateOn = DateTime.Now;
                   RequestData.SalesTargetHeaderRecord.Active = true;
                   RequestData.SalesTargetHeaderRecord.SCN = _IsalesTargetView.SCN;

                   var ResponseData = _SalesTargetBLL.SaveDocumentNumberingMaster(RequestData);

                   _IsalesTargetView.Message = ResponseData.DisplayMessage;
                   _IsalesTargetView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _IsalesTargetView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public bool IsValidForm()
       {
           bool objBool = false;         
               if (_IsalesTargetView.CountryID == 0 || _IsalesTargetView.CountryID.ToString() == string.Empty)
               {
                   _IsalesTargetView.Message = " Country Name is missing Please Select it.";
               }

               else if (_IsalesTargetView.BrandID == 0 || _IsalesTargetView.BrandID.ToString() == string.Empty)
               {
                   _IsalesTargetView.Message = "Brand is missing Please Select it. ";
               }
               else if (_IsalesTargetView.Year == "" || _IsalesTargetView.Year.ToString() == string.Empty)
               {
                   _IsalesTargetView.Message = "Year is missing Please Select it. ";
               }
               else if (_IsalesTargetView.StoreIDs == "" || _IsalesTargetView.StoreIDs.ToString() == string.Empty)
               {
                   _IsalesTargetView.Message = "Store is missing Please Select it. ";
               }
               else
               {
                   objBool = true;
               }          
           return objBool;
       }
    }
   public class SalesTargetCollectionPresenter
   {
       SalesTargetBLL _SalesTargetBLL = new SalesTargetBLL();
       ItargetCollection _ItargetCollection;
       public SalesTargetCollectionPresenter(ItargetCollection ViewObj)
       {
           _ItargetCollection = ViewObj;
       }
       public void SelectAllSalesTargetList()
       {
           var RequestData = new SelectAllSalesTargetRequest();
           RequestData.ShowInActiveRecords = true;
           var ResponseData = new SelectAllSalesTargetResponse();
           ResponseData = _SalesTargetBLL.SelectAllDocumentNumberingMaster(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _ItargetCollection.SalesTargetHeaderList = ResponseData.SalesTargetHeaderList;
           }
           else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
           {

           }
       }


   }
}
