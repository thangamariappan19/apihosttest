using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizRequest.SyncSettings;
using EasyBizResponse.SyncSettings;
using EasyBizIView.SyncSettings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizBLL.SyncSettings;

namespace EasyBizPresenter.SyncSettings
{
   public class MasterDataSyncPresenter
    {
       IMasterDataSyncView _IMDSyncView;
       MasterDataSyncBLL MDSBLL = new MasterDataSyncBLL();
       public MasterDataSyncPresenter(IMasterDataSyncView ViewObj)
       {
           _IMDSyncView = ViewObj;
       }
       public void GetBrandList()
          {
            try
            {
                EasyBizBLL.Masters.BrandBLL _BrandBLL = new EasyBizBLL.Masters.BrandBLL();
                var RequestData = new SelectAllBrandRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllBrandResponse();
                ResponseData = _BrandBLL.SelectAllBrandRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IMDSyncView.BrandList = ResponseData.BrandList;
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
       public void PriceStoreSync()
       {

           try
           {
               var doclist = new List<MasterDataSyncDBType>();
               var objmds = new MasterDataSyncDBType();
               objmds.Mode = "PRICE"; 
               objmds.BrandID = 0;
               objmds.SkuCode = _IMDSyncView.Skucode;
               objmds.StoreID = _IMDSyncView.StoreID;
               objmds.INVOICE = string.Empty;
               objmds.UserName = string.Empty;
               var RequestData = new MasterDataSyncRequest();
               var ResponseData = new MasterDataSyncResponse();
               RequestData.ShowInActiveRecords = true;
               RequestData.PriceUP = objmds;
               ResponseData = MDSBLL.SyncStorePriceBLL(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IMDSyncView.ProcessStatus = ResponseData.StatusCode;

                   _IMDSyncView.Message = ResponseData.DisplayMessage;

               }
               
           }
           catch (Exception ex)
           {
               throw ex;
           }

          

       }
       public void BarcodeStoreSync()
       {

           try
           {
               var doclist = new List<MasterDataSyncDBType>();
               var objmds = new MasterDataSyncDBType();
               objmds.Mode = "BARCODE";
               objmds.BrandID = 0;
               objmds.SkuCode = _IMDSyncView.Barcode;
               objmds.StoreID = _IMDSyncView.StoreID;
               objmds.INVOICE = string.Empty;
               objmds.UserName = string.Empty;
               var RequestData = new MasterDataSyncRequest();
               var ResponseData = new MasterDataSyncResponse();
               RequestData.ShowInActiveRecords = true;
               RequestData.PriceUP = objmds;
               ResponseData = MDSBLL.SyncStorePriceBLL(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IMDSyncView.ProcessStatus = ResponseData.StatusCode;

                   _IMDSyncView.Message = ResponseData.DisplayMessage;

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }



       }
       public void ImageStoreSync()
       {

           try
           {
               var doclist = new List<MasterDataSyncDBType>();
               var objmds = new MasterDataSyncDBType();
               objmds.Mode = "IMAGE";
               objmds.BrandID = _IMDSyncView.BrandID;
               objmds.SkuCode = string.Empty;
               objmds.StoreID = _IMDSyncView.StoreID;
               objmds.INVOICE = string.Empty;
               objmds.UserName = string.Empty;
               var RequestData = new MasterDataSyncRequest();
               var ResponseData = new MasterDataSyncResponse();
               RequestData.ShowInActiveRecords = true;
               RequestData.PriceUP = objmds;
               ResponseData = MDSBLL.SyncStorePriceBLL(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IMDSyncView.ProcessStatus = ResponseData.StatusCode;

                   _IMDSyncView.Message = ResponseData.DisplayMessage;

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }



       }
       public void InvoiceStoreSync()
       {

           try
           {
               var doclist = new List<MasterDataSyncDBType>();
               var objmds = new MasterDataSyncDBType();
               objmds.Mode = "INVOICE";
               objmds.BrandID = 0;
               objmds.SkuCode = string.Empty;
               objmds.StoreID = _IMDSyncView.StoreID;
               objmds.INVOICE = _IMDSyncView.INVOICE;
               objmds.UserName = string.Empty;
               var RequestData = new MasterDataSyncRequest();
               var ResponseData = new MasterDataSyncResponse();
               RequestData.ShowInActiveRecords = true;
               RequestData.PriceUP = objmds;
               ResponseData = MDSBLL.SyncStorePriceBLL(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IMDSyncView.ProcessStatus = ResponseData.StatusCode;

                   _IMDSyncView.Message = ResponseData.DisplayMessage;
              
               }
           
           }
           catch (Exception ex)
           {
               throw ex;
           }



       }
       public void USerStoreSync()
       {

           try
           {
               var doclist = new List<MasterDataSyncDBType>();
               var objmds = new MasterDataSyncDBType();
               objmds.Mode = "USER";
               objmds.BrandID = 0;
               objmds.SkuCode = string.Empty;
               objmds.StoreID = _IMDSyncView.StoreID;
               objmds.INVOICE = string.Empty;
               objmds.UserName = _IMDSyncView.UserName;
               var RequestData = new MasterDataSyncRequest();
               var ResponseData = new MasterDataSyncResponse();
               RequestData.ShowInActiveRecords = true;
               RequestData.PriceUP = objmds;
               ResponseData = MDSBLL.SyncStorePriceBLL(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _IMDSyncView.ProcessStatus = ResponseData.StatusCode;

                   _IMDSyncView.Message = ResponseData.DisplayMessage;

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }



       }
    }
}
