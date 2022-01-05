using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizFactory;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Masters.DesignMasterResponse;
using EasyBizResponse.Masters.ScaleMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EasyBizBLL.Masters
{
   public class StyleMasterBLL
    {
       public SaveStyleResponse SaveStyle(SaveStyleRequest objRequest)
       {
           SaveStyleResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;

               // Changed by Senthamil @ 10.09.2018
               //objRequest.BaseIntegrateStoreID = objRequest.StyleRecord.BrandID;

               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var objStyle = new StyleMaster();
                   objStyle = (StyleMaster)objRequest.RequestDynamicData;                   
                   objRequest.StyleWithScaleDetailsList = objStyle.ScaleDetailMasterList;
                   objRequest.StyleWithColorDetailsList = objStyle.ColorMasterList;
                   objRequest.ItemImageMasterDetailsList = objStyle.ItemImageMasterList;
                   objRequest.StyleMasterData = objStyle;
                   objRequest.ImportExcelList = objStyle.ImportExcelList;
                   objRequest.ImportcolorExcelList = objStyle.ImportcolorExcelList;
                   objRequest.ImportScaleExcelList = objStyle.ImportScaleExcelList;
                   objRequest.StyleRecord = objStyle;

                   if (objRequest.RequestDynamicData.BrandID != null)
                   {
                       objRequest.BaseIntegrateStoreID = objRequest.RequestDynamicData.BrandID;
                   }
               }
               else
               {                   
                   objRequest.BaseIntegrateStoreID = objRequest.StyleRecord.BrandID;
               }
               objResponse = (SaveStyleResponse)objBaseStyleMasterDAL.InsertRecord(objRequest);
              //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
              //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;
               //    objRequest.StyleRecord.ID = Convert.ToInt32(objResponse.IDs);
               //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
               //    objRequest.DocumentType = Enums.DocumentType.STYLEMASTER;
               //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;                   

               //     //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleMasterBLL", "SaveStyle");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new SaveStyleResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public InserSKUImageResponse UpdateSKUImages(InserSKUImageRequest objRequest)
       {
           InserSKUImageResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               objRequest.BaseIntegrateStoreID = objRequest.BrandID;

               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ImageList = new List<ItemImageMaster>();
                   ImageList = (List<ItemImageMaster>)objRequest.RequestDynamicData;
                   objRequest.ImageList = ImageList;
               }
               objResponse = (InserSKUImageResponse)objBaseStyleMasterDAL.InsertSKUImages(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;
               //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
               //    objRequest.DocumentType = Enums.DocumentType.STYLEMASTER;
               //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
               //  //  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleMasterBLL", "InsertSKUImages");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new InserSKUImageResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Images");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public UpdateStyleResponse UpdateStyle(UpdateStyleRequest objRequest)
       {
           UpdateStyleResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;

               //// Changed by Senthamil @ 07.09.2018
               objRequest.BaseIntegrateStoreID = objRequest.StyleRecord.BrandID;
               //if (objRequest.RequestDynamicData != null)
               //{
               //    var objStyle = new StyleMaster();
               //    objStyle = (StyleMaster)objRequest.RequestDynamicData;
               //    objRequest.StyleWithScaleDetailsList = objStyle.ScaleDetailMasterList;
               //    objRequest.StyleWithColorDetailsList = objStyle.ColorMasterList;
               //    objRequest.ItemImageMasterDetailsList = objStyle.ItemImageMasterList;
               //    objRequest.StyleMasterData = objStyle;
               //    objRequest.ImportExcelList = objStyle.ImportExcelList;
               //    objRequest.ImportcolorExcelList = objStyle.ImportcolorExcelList;
               //    objRequest.ImportScaleExcelList = objStyle.ImportScaleExcelList;

               //    if (objRequest.RequestDynamicData.BrandID != null)
               //    {
               //        objRequest.BaseIntegrateStoreID = objRequest.RequestDynamicData.BrandID;
               //    }
               //}
               //else
               //{
               //    objRequest.BaseIntegrateStoreID = objRequest.StyleRecord.BrandID;
               //}

               objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (UpdateStyleResponse)objBaseStyleMasterDAL.UpdateRecord(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;
               //    objRequest.DocumentIDs = Convert.ToString(objRequest.StyleRecord.ID);
               //    objRequest.DocumentType = Enums.DocumentType.STYLEMASTER;
               //    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;
               //  //  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleMasterBLL", "UpdateStyle");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new UpdateStyleResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public DeleteStyleResponse DeleteStyle(DeleteStyleRequest objRequest)
       {
           DeleteStyleResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               objRequest.BaseIntegrateStoreID = objRequest.BrandID;

               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (DeleteStyleResponse)objBaseStyleMasterDAL.DeleteRecord(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;
               //   // objRequest.DocumentIDs = Convert.ToString(objRequest.StyleRecord.ID);
               //    objRequest.DocumentType = Enums.DocumentType.STYLEMASTER;
               //    objRequest.ProcessMode = Enums.ProcessMode.Delete;                  

               //   // BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleMasterBLL", "DeleteStyle");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new DeleteStyleResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectAllStyleResponse SelectAllStyleRecord(SelectAllStyleRequest objRequest)
       {
           SelectAllStyleResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectAllStyleResponse)objBaseStyleMasterDAL.SelectAll(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectAllStyleResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

        public SelectAllStyleResponse API_SelectAllStyleRecord(SelectAllStyleRequest objRequest)
        {
            SelectAllStyleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
                objResponse = (SelectAllStyleResponse)objBaseStyleMasterDAL.API_SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStyleResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectByStyleIDResponse SelectStyleRecord(SelectByStyleIDRequest objRequest)
       {
           SelectByStyleIDResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               if (objRequest.ID == 0)
               {
                   objRequest.ID = Convert.ToInt32(objRequest.DocumentIDs);
               } 
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectByStyleIDResponse)objBaseStyleMasterDAL.SelectRecord(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByStyleIDResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectScaleDetailsResponse SelectStyleWithScaleRecord(SelectScaleDetailsRequest objRequest)
       {
           SelectScaleDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectScaleDetailsResponse)objBaseStyleMasterDAL.SelectStyleWithScaleDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectScaleDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectStyleWithScaleforStockResponse SelectStyleWithScaleRecordForStock(SelectStyleWithScaleforStockRequest objRequest)
       {
           SelectStyleWithScaleforStockResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectStyleWithScaleforStockResponse)objBaseStyleMasterDAL.SelectStyleWithScaleForStock(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectStyleWithScaleforStockResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       
       public SelectColorDetailsResponse SelectStyleWithColorDetailsRecord(SelectColorDetailsRequest objRequest)
       {
           SelectColorDetailsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectColorDetailsResponse)objBaseStyleMasterDAL.SelectStyleWithColorDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectColorDetailsResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectStyleLookUpResponse SelectStyleLookUp(SelectStyleLookUpRequest objRequest)
       {
           SelectStyleLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectStyleLookUpResponse)objBaseStyleMasterDAL.SelectStyleLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectStyleLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public SelectItemImageResponse SelectStyleWithItemImage(SelectItemImageRequest objRequest)
       {
           SelectItemImageResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectItemImageResponse)objBaseStyleMasterDAL.SelectStyleWithItemImageDetails(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectItemImageResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public StyleCodeGeneratingResponse SelectStyleCodeRunningNum(StyleCodeGeneratingRequest objRequest)
       {
           StyleCodeGeneratingResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (StyleCodeGeneratingResponse)objBaseStyleMasterDAL.SelectStyleCode(objRequest);
               if(objResponse.Autonumbering == 0)
               {
                   objResponse.Autonumbering = 0;
               }
           }
           catch (Exception ex)
           {
               objResponse = new StyleCodeGeneratingResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SaveStyleResponse SaveImportExcelStyleMaster(SaveStyleRequest objRequest)
       {
           SaveStyleResponse objResponse = null;
           var ObjFactory = new DALFactory();
           try
           {               
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;

               if (objRequest.ImportExcelList != null && objRequest.ImportExcelList.Count > 0)
               {
                   objRequest.BaseIntegrateStoreID = objRequest.ImportExcelList.FirstOrDefault().BrandID;
               }

               if(objRequest.RequestDynamicData != null)
               {
                   List<StyleMaster> ImportExcelList = new List<StyleMaster>();     
                   List<StyleMaster> ImportcolorExcelList = new List<StyleMaster>();
                   List<StyleMaster> ImportScaleExcelList = new List<StyleMaster>();
               }
               var objBaseStyleMasterDAL = ObjFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SaveStyleResponse)objBaseStyleMasterDAL.ImportExcelStyleInsert(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success )
               ////if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.ItemImageMasterDetailsList.Capacity!=0)
               //{
               //    //InserSKUImageResponse InsertSKUImages(InserSKUImageRequest objRequest)
               //    InserSKUImageRequest objInserSKUImageRequest = new InserSKUImageRequest();
               //    objInserSKUImageRequest.BaseIntegrateStoreID = 9;
               //    objInserSKUImageRequest.ConnectionString = objRequest.ConnectionString;
               //    objInserSKUImageRequest.ImageList = objRequest.ItemImageMasterDetailsList;
               //    InserSKUImageResponse objInserSKUImageResponse = new InserSKUImageResponse();
               //    objInserSKUImageResponse = InsertSKUImages(objInserSKUImageRequest);
               //}

               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;                   
               //    objRequest.DocumentIDs = Convert.ToString(objResponse.ReturnIDs);
               //    objRequest.DocumentType = Enums.DocumentType.STYLEMASTER;
               //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
               //  //  BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleMasterBLL", "SaveImportExcelStyleMaster");

               //}
           }
           catch (Exception ex)
           {
               objResponse = new SaveStyleResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectByStyleIDsResponse SelectStyleRecordByIDs(SelectByStyleIDsRequest objRequest)
       {
           SelectByStyleIDsResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (SelectByStyleIDsResponse)objBaseStyleMasterDAL.SelectByIDs(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectByStyleIDsResponse();
               objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }


       public SelectDesignGradeLookUpResponse GradeLookUp(SelectDesignGradeLookUpRequest objRequest)
       {
           SelectDesignGradeLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseCollection = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectDesignGradeLookUpResponse)BaseCollection.SelectDesignGradeLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectDesignGradeLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public SelectDesignDevelopmentOfficeLookUpResponse DevelopmentOfficeLookUp(SelectDesignDevelopmentOfficeLookUpRequest objRequest)
       {
           SelectDesignDevelopmentOfficeLookUpResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               var BaseCollection = objFactory.GetDALRepository().GetDesignMasterDAL();
               objResponse = (SelectDesignDevelopmentOfficeLookUpResponse)BaseCollection.SelectDesignDevelopmentOfficeLookUp(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new SelectDesignDevelopmentOfficeLookUpResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Collection Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }
       public InserSKUImageResponse InsertSKUImages(InserSKUImageRequest objRequest)
       {
           InserSKUImageResponse objResponse = null;
           var objFactory = new DALFactory();
           try
           {
               objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
               objRequest.BaseIntegrateStoreID = objRequest.BrandID;

               var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
               if (objRequest.RequestDynamicData != null)
               {
                   var ImageList = new List<ItemImageMaster>();
                   ImageList = (List<ItemImageMaster>)objRequest.RequestDynamicData;
                   objRequest.ImageList = ImageList;                   
               }
               objResponse = (InserSKUImageResponse)objBaseStyleMasterDAL.InsertSKUImages(objRequest);
               //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
               //{
               //    objRequest.RequestFrom = objRequest.RequestFrom;                 
               //    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
               //    objRequest.DocumentType = Enums.DocumentType.STYLEMASTER;
               //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
               //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.StyleMasterBLL", "InsertSKUImages");
               //}
           }
           catch (Exception ex)
           {
               objResponse = new InserSKUImageResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Images");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

       public GetStyleNameResponse GetStyleName(GetStyleNameRequest objRequest)
       {
           GetStyleNameResponse objResponse = null;
           var ObjFactory = new DALFactory();

           try
           {
               BaseStyleMasterDAL objStyleMasterDAL = ObjFactory.GetDALRepository().GetBaseStyleMasterDAL();
               objResponse = (GetStyleNameResponse)objStyleMasterDAL.GetStyleName(objRequest);
           }
           catch (Exception ex)
           {
               objResponse = new GetStyleNameResponse();
               objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
               objResponse.ExceptionMessage = ex.Message;
               objResponse.StackTrace = ex.StackTrace;
           }
           return objResponse;
       }

        public SelectAllStyleResponse SelectstyleColorSizeTypesListRecord(SelectAllStyleRequest objRequest)
        {
            SelectAllStyleResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStyleMasterDAL = objFactory.GetDALRepository().GetBaseStyleMasterDAL();
                objResponse = (SelectAllStyleResponse)objBaseStyleMasterDAL.API_GetStyleColorScale(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStyleResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
