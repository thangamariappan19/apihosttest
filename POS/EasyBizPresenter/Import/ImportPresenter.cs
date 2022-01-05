using CommonRoutines;
using EasyBizBLL.Import;
using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Import;
using EasyBizIView.Masters.Style;
using EasyBizRequest.Import.StylePricingRequest;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.AFSegamationMasterResponse;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Import.StylePricingResponse;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.ProductGroupResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Import
{
    public class ImportPresenter
    {
        DesignMasterBLL _DesignMasterBLL = new DesignMasterBLL();
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        ImportStylePricingBLL _ImportStylePricingBLL = new ImportStylePricingBLL();
        IImportView _IImportView;       
        String IDs;
        List<SegmentMaster> _SegmentDetailsTypes = new List<SegmentMaster>();
        List<AFSegamationMasterTypes> _AFSegamationMasterTypes = new List<AFSegamationMasterTypes>();

        string SKUBarCode = string.Empty;
        long BarCodeRunningNo = 0;
        int BarCodeID = 0;

        public ImportPresenter(IImportView ViewObj)
        {
            _IImportView = ViewObj;
        }

        public void SaveImportDesignMaster()
        {
            var RequestData = new SaveDesignMasterRequest();
            RequestData.DesignMasterData = new DesignMasterTypes();
            RequestData.ImportExcelList = _IImportView.ImportDesignMasterList;
            RequestData.BaseIntegrateStoreID = 19;
            SaveDesignMasterResponse ResponseData = _DesignMasterBLL.SaveImportExcelDesignMaster(RequestData);
            _IImportView.Message = ResponseData.DisplayMessage;

        }
        //public void SaveImportSkuMaster()
        //{
        //    var RequestData = new SaveSKUMasterRequest();
        //    RequestData.SkuMasterData = new SKUMasterTypes();
        //    RequestData.ImportExcelList = _IImportView.ImportSKUMasterList;

        //    SaveSKUMasterResponse ResponseData = _SKUMasterBLL.SaveImportExcelSkuMaster(RequestData);
        //    _IImportView.Message = ResponseData.DisplayMessage;

        //}

 //***********************************************************/UpdateImportBarCode/**********************************************************************************
        public void UpdateImportBarCode()
        {
            var RequestData = new SaveSKUMasterRequest();
            RequestData.SkuMasterData = new SKUMasterTypes();
            RequestData.ImportExcelList = _IImportView.ImportSKUMasterList;
            RequestData.BaseIntegrateStoreID = 19;
            SaveSKUMasterResponse ResponseData = _SKUMasterBLL.UpdateImportBarCode(RequestData);
           // _IImportView.Message = ResponseData.DisplayMessage;
            if (ResponseData.ReturnIDs != null)
            {
                _IImportView.Message = "SKU Code Not Exists -" + ResponseData.ReturnIDs; 
                _IImportView.Message = ResponseData.DisplayMessage;
            }
            else
            {
                _IImportView.Message = ResponseData.DisplayMessage;
            }
        }
//**************************************************************************Style Master**********************************************************************************
        public void SaveImportStyleMaster()
        {
            var RequestData = new SaveStyleRequest();
            RequestData.StyleMasterData = new StyleMaster();
            RequestData.ImportExcelList = _IImportView.ImportStyleMasterList;
            RequestData.ImportcolorExcelList = _IImportView.ImportStyleWithColorMasterList;
            RequestData.ImportScaleExcelList = _IImportView.ImportStyleWithScaleMasterList;
            RequestData.BaseIntegrateStoreID = 19;
            SaveStyleResponse ResponseData = _StyleMasterBLL.SaveImportExcelStyleMaster(RequestData);
           if( ResponseData.ReturnIDs != "")
           {
               IDs=ResponseData.ReturnIDs;
               SelectStyleRecordIDs(IDs);
           }
            _IImportView.Message = ResponseData.DisplayMessage;
        }
       public void SelectStyleRecordIDs(String  ReturnIDs)
       {
            try
            {
                var RequestData = new SelectByStyleIDsRequest();
                RequestData.IDs = ReturnIDs;
                RequestData.RequestFrom = Enums.RequestFrom.Upload;
                RequestData.BaseIntegrateStoreID = 19;
                SelectByStyleIDsResponse ResponseData = _StyleMasterBLL.SelectStyleRecordByIDs(RequestData);
                if(ResponseData.StyleMasterList != null)
                {
                    SaveSKUList(ResponseData.StyleMasterList);                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       public List<BrandMaster> GetBrandList()
       {
           var _BrandBLL = new BrandBLL();
           var objBrandList = new List<BrandMaster>();
           try
           {
               var RequestData = new SelectAllBrandRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllBrandResponse();
               ResponseData = _BrandBLL.SelectAllBrandRecords(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   objBrandList = ResponseData.BrandList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return objBrandList;
       }
       public List<ProductGroupMaster> GetProductGroupList()
       {
           var _ProductGroupBLL = new ProductGroupBLL();
           var ObjProductGroupList = new List<ProductGroupMaster>();
           try
           {
               var RequestData = new SelectAllProductGroupRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.BaseIntegrateStoreID = 19;
               var ResponseData = new SelectAllProductGroupResponse();
               ResponseData = _ProductGroupBLL.SelectAllProductGroup(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   ObjProductGroupList = ResponseData.ProductGroupList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ObjProductGroupList;
       }

       public List<AFSegamationMasterTypes> GetStyleSegmentationList()
       {
           var _ProductGroupBLL = new AFSegamationMasterBLL();
           var ObjProductGroupList = new List<AFSegamationMasterTypes>();
           try
           {
               var RequestData = new SelectAllAFSegamationMasterRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.BaseIntegrateStoreID = 19;
               var ResponseData = new SelectAllAFSegamationMasterResponse();
               ResponseData = _ProductGroupBLL.SelectAllAFSegamationMaster(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   ObjProductGroupList = ResponseData.AFSegamationMasterTypesList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ObjProductGroupList;
       }

       public List<DesignMasterTypes> GetDesignList()
       {
           var _DesignmasterBLL = new DesignMasterBLL();
           var ObjDesignMasterList = new List<DesignMasterTypes>();
           try
           {
               var RequestData = new SelectAllDesignMasterRequest();
               RequestData.ShowInActiveRecords = true;
               RequestData.BaseIntegrateStoreID = 19;
               var ResponseData = new SelectAllDesignMasterResponse();
               ResponseData = _DesignmasterBLL.SelectAllDesignMaster(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   ObjDesignMasterList = ResponseData.DesignMasterTypesList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ObjDesignMasterList;
       }

       public List<SKUMasterTypes> GenerateSKUList(List<StyleMaster> StyleMasterList)
       {           
           var SKUList = new List<SKUMasterTypes>();
           var ScaleList = new List<ScaleDetailMaster>();
           var ColorList = new List<ColorMaster>();
           var BrandList = new List<BrandMaster>();
           var ProductGroupList = new List<ProductGroupMaster>();
           var StyleSegmentationList = new List<AFSegamationMasterTypes>();
           var DesignList = new List<DesignMasterTypes>();
           BrandList = GetBrandList();
           ProductGroupList = GetProductGroupList();
           StyleSegmentationList = GetStyleSegmentationList();
           DesignList = GetDesignList();
           try
           {
               foreach (StyleMaster objStyleMaster in StyleMasterList)
               {
                   ColorList = objStyleMaster.ColorMasterList;
                   ScaleList = objStyleMaster.ScaleDetailMasterList;
                   

                   foreach (ScaleDetailMaster objScaleDetailMaster in ScaleList)
                   {
                       SKUMasterTypes objSKUMasterTypes = new SKUMasterTypes();
                       if ((objScaleDetailMaster.Active == true) || (objScaleDetailMaster.Active == false && objScaleDetailMaster.ID != 0))
                       {
                           //objSKUMasterTypes.CreateBy = _IImportView.UserID;
                           objSKUMasterTypes.StyleCode = objStyleMaster.StyleCode;
                           objSKUMasterTypes.StyleName = objStyleMaster.StyleName;
                           objSKUMasterTypes.StyleID = objStyleMaster.ID;
                           objSKUMasterTypes.SegamentationID = objStyleMaster.StyleSegmentation;
                           objSKUMasterTypes.ShortDescription = objStyleMaster.ShortDescriptionn;
                           objSKUMasterTypes.DesignID = objStyleMaster.DesignID;
                           objSKUMasterTypes.ArmadaCollectionID = objStyleMaster.ArmadaCollectionID;
                          // objSKUMasterTypes.DesignCode = objStyleMaster.DesignCode;
                           objSKUMasterTypes.BrandID = objStyleMaster.BrandID;
                           objSKUMasterTypes.SubBrandID = objStyleMaster.SubBrandID;
                           objSKUMasterTypes.CollectionID = objStyleMaster.CollectionID;
                           objSKUMasterTypes.DivisionID = objStyleMaster.DivisionID;
                           objSKUMasterTypes.ProductGroupID = objStyleMaster.ProductGroupID;
                           objSKUMasterTypes.ProductSubGroupID = objStyleMaster.ProductSubGroupID;
                           objSKUMasterTypes.SeasonID = objStyleMaster.SeasonID;
                           objSKUMasterTypes.YearID = objStyleMaster.YearCode;
                           objSKUMasterTypes.ProductLineID = objStyleMaster.ProductLineID;
                           objSKUMasterTypes.DesignerID = objStyleMaster.DesignerID;
                           objSKUMasterTypes.PurchasePrice = objStyleMaster.PurchasePrice;
                           objSKUMasterTypes.PurchaseCurrencyID = objStyleMaster.PurchaseCurrencyID;
                           objSKUMasterTypes.RRPPrice = objStyleMaster.RRPPrice;
                           objSKUMasterTypes.RRPCurrencyID = objStyleMaster.RRPCurrencyID;
                           objSKUMasterTypes.ScaleID = objStyleMaster.ScaleID;
                           objSKUMasterTypes.SizeID = objScaleDetailMaster.SizeID;
                           objSKUMasterTypes.SizeCode = objScaleDetailMaster.SizeCode;
                           objSKUMasterTypes.SizeName = objScaleDetailMaster.Description;
                           objSKUMasterTypes.ExchangeRate = objStyleMaster.ExchangeRate;
                           objSKUMasterTypes.PurchasePriceListID = objStyleMaster.PurchasePriceListID;
                          // objSKUMasterTypes.BrandShortCode = objStyleMaster.BrandShortCode;

                           GetSegamationDetailsLookUp(objStyleMaster.StyleSegmentation);

                           foreach (ColorMaster objColorMaster in ColorList)
                           {
                               SKUMasterTypes TempStyleDetails = DeepCopyCreator.StyleDetailsDeepCopy(objSKUMasterTypes);

                               if ((objColorMaster.Active == true) || (objColorMaster.Active == false && objColorMaster.ID != 0))
                               {                                  
                                   TempStyleDetails.ColorName = objColorMaster.Description;
                                   TempStyleDetails.ColorCode = objColorMaster.ColorCode;                                  
                                   TempStyleDetails.ColorID = objColorMaster.ID;

                                   var StyleDefaulDescription = (from c in _SegmentDetailsTypes
                                                                 where c.SegmentName == "Style"
                                                                 select
                                                                 c.DefaultDescription).SingleOrDefault<System.Boolean>();
                                   var ColorDefaulDescription = (from c in _SegmentDetailsTypes
                                                                 where c.SegmentName == "Color"
                                                                 select
                                                                 c.DefaultDescription).SingleOrDefault<System.Boolean>();
                                   var SizeDefaulDescription = (from c in _SegmentDetailsTypes
                                                                where c.SegmentName == "Size"
                                                                select
                                                                c.DefaultDescription).SingleOrDefault<System.Boolean>();

                                   var Seperator = (from c in _AFSegamationMasterTypes
                                                    where c.ID == objStyleMaster.StyleSegmentation
                                                    select
                                                    c.UseSeperator).SingleOrDefault<System.String>();

                                   var BrandShortDescriptionName = objStyleMaster.StyleName;
                                   //if (GetBrandList().Where(x => x.BrandID == objStyleMaster.BrandID) != null)
                                   //{
                                   //    //BrandList = GetBrandList().Where(x => x.ID == objStyleMaster.BrandID).ToList();
                                   //    //string result = (from s in GetBrandList()
                                   //    //                 where s.ID == objStyleMaster.BrandID
                                   //    //                 select s.ShortDescriptionName).ToString();
                                   //    //Brand = result;
                                   //    //foreach (var Brand in BrandList)
                                   //    //{
                                   //    //    if (Brand.ID == objStyleMaster.BrandID)
                                   //    //    {
                                   //    //        BrandShortDescriptionName = Brand.ShortDescriptionName;
                                   //    //    }
                                   //    //}
                                      
                                   //}
                                   var ProductGroupName = "";

                                   if (ProductGroupList.Count==0)
                                   {
                                       GetProductGroupList();
                                   }
                                  else if (ProductGroupList.Where(x => x.ID == objStyleMaster.ProductGroupID) != null)
                                   {
                                       foreach (var ProductGroup in ProductGroupList)
                                       {
                                           if (ProductGroup.ID == objStyleMaster.ProductGroupID)
                                           {
                                               ProductGroupName = ProductGroup.ProductGroupName;
                                           }
                                       }
                                   }
                                    var UseSeperator = "";
                                    if (StyleSegmentationList.Count == 0)
                                    {
                                        GetStyleSegmentationList();
                                    }
                                    if (StyleSegmentationList.Where(x => x.ID == objStyleMaster.StyleSegmentation) != null)
                                   {
                                       foreach (var StyleSegmentation in StyleSegmentationList)
                                       {
                                           if (StyleSegmentation.ID == objStyleMaster.StyleSegmentation)
                                           {
                                               UseSeperator = StyleSegmentation.UseSeperator;
                                           }
                                       }
                                   }

                                   

                                   var DesignCode = "";
                                   if (DesignList.Count == 0)
                                   {
                                       GetDesignList();
                                   }
                                   if (DesignList.Where(x => x.ID == objStyleMaster.DesignID) != null)
                                   {
                                       foreach (var DesignMaster in DesignList)
                                       {
                                           if (DesignMaster.ID == objStyleMaster.DesignID)
                                           {
                                               DesignCode = DesignMaster.DesignCode;
                                           }
                                       }
                                   }

                                   if (StyleDefaulDescription == true && ColorDefaulDescription == true && SizeDefaulDescription == true)
                                   {
                                       //TempStyleDetails.SKUName = BrandShortDescriptionName + SKUNameSeperator + DesignCode + SKUNameSeperator + ProductGroupName + SKUNameSeperator + objSKUMasterTypes.ShortDescription + " " + objColorMaster.Description + " " + objScaleDetailMaster.Description;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName ;
                                   }
                                   else if (StyleDefaulDescription == true && ColorDefaulDescription == false && SizeDefaulDescription == true)
                                   {
                                       //TempStyleDetails.SKUName = BrandShortDescriptionName + SKUNameSeperator + DesignCode + SKUNameSeperator + ProductGroupName + SKUNameSeperator + objSKUMasterTypes.ShortDescription + " " + objScaleDetailMaster.Description;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName;
                                   }
                                   else if (StyleDefaulDescription == true && ColorDefaulDescription == false && SizeDefaulDescription == false)
                                   {
                                       //TempStyleDetails.SKUName = BrandShortDescriptionName + SKUNameSeperator + DesignCode + SKUNameSeperator + ProductGroupName + SKUNameSeperator + objSKUMasterTypes.ShortDescription;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName;
                                   }
                                   else if (StyleDefaulDescription == true && ColorDefaulDescription == true && SizeDefaulDescription == false)
                                   {
                                       //TempStyleDetails.SKUName = BrandShortDescriptionName + SKUNameSeperator + DesignCode + SKUNameSeperator + ProductGroupName + SKUNameSeperator + objSKUMasterTypes.ShortDescription + " " + objColorMaster.Description;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName;
                                   }
                                   else if (StyleDefaulDescription == false && ColorDefaulDescription == true && SizeDefaulDescription == true)
                                   {
                                       //TempStyleDetails.SKUName = objColorMaster.Description + " " + objScaleDetailMaster.Description;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName;
                                   }
                                   else if (StyleDefaulDescription == false && ColorDefaulDescription == false && SizeDefaulDescription == true)
                                   {
                                       //TempStyleDetails.SKUName = objScaleDetailMaster.Description;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName;
                                   }
                                   else if (StyleDefaulDescription == false && ColorDefaulDescription == true && SizeDefaulDescription == false)
                                   {
                                      // TempStyleDetails.SKUName = objColorMaster.Description;
                                       TempStyleDetails.SKUName = BrandShortDescriptionName;
                                   }                                 
                                   else if (StyleDefaulDescription == false && ColorDefaulDescription == false && SizeDefaulDescription == false)
                                   {
                                       TempStyleDetails.SKUName = "";
                                   }

                                   TempStyleDetails.SKUCode = objSKUMasterTypes.StyleCode + UseSeperator + objColorMaster.ColorCode + UseSeperator + objScaleDetailMaster.SizeCode;
                                   TempStyleDetails.ColorID = objColorMaster.ColorID;
                                   TempStyleDetails.SizeCode = objSKUMasterTypes.SizeCode;
                                   TempStyleDetails.StyleCode = objSKUMasterTypes.StyleCode;
                                   TempStyleDetails.StyleID = objSKUMasterTypes.StyleID;
                                   TempStyleDetails.SegamentationID = objSKUMasterTypes.SegamentationID;                                  
                                   TempStyleDetails.DesignID = objSKUMasterTypes.DesignID;                                   
                                   TempStyleDetails.BrandID = objSKUMasterTypes.BrandID;
                                   TempStyleDetails.SubBrandID = objSKUMasterTypes.SubBrandID;
                                   TempStyleDetails.CollectionID = objSKUMasterTypes.CollectionID;
                                   TempStyleDetails.DivisionID = objSKUMasterTypes.DivisionID;
                                   TempStyleDetails.ProductGroupID = objSKUMasterTypes.ProductGroupID;
                                   TempStyleDetails.ProductSubGroupID = objSKUMasterTypes.ProductSubGroupID;
                                   TempStyleDetails.SeasonID = objSKUMasterTypes.SeasonID;
                                   TempStyleDetails.YearID = objSKUMasterTypes.YearID;
                                   TempStyleDetails.ArmadaCollectionID = objSKUMasterTypes.ArmadaCollectionID;
                                   TempStyleDetails.SizeName = objSKUMasterTypes.SizeName;
                                   TempStyleDetails.ProductLineID = objSKUMasterTypes.ProductLineID;
                                   TempStyleDetails.DesignerID = objSKUMasterTypes.DesignerID;
                                   TempStyleDetails.PurchasePrice = objSKUMasterTypes.PurchasePrice;
                                   TempStyleDetails.PurchaseCurrencyID = objSKUMasterTypes.PurchaseCurrencyID;
                                   TempStyleDetails.RRPPrice = objSKUMasterTypes.RRPPrice;
                                   TempStyleDetails.RRPCurrencyID = objSKUMasterTypes.RRPCurrencyID;
                                   TempStyleDetails.ScaleID = objSKUMasterTypes.ScaleID;
                                   TempStyleDetails.SizeID = objSKUMasterTypes.SizeID;                                   
                                   TempStyleDetails.ExchangeRate = objSKUMasterTypes.ExchangeRate;
                                   TempStyleDetails.PurchasePriceListID = objSKUMasterTypes.PurchasePriceListID;                                   
                                   TempStyleDetails.Active = true;
                                   SKUList.Add(TempStyleDetails);
                               }
                           }
                       }
                   }
               }               
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return SKUList;
        }
       public void GetSegamationDetailsLookUp(int SegmentationID)
       {
           try
           {
               var _AFSegamationMasterBLL = new AFSegamationMasterBLL();
               var RequestData = new SelectSegamationDetailsLookUpRequest();
               RequestData.ShowInActiveRecords = false;
               RequestData.ID = SegmentationID;
               RequestData.BaseIntegrateStoreID = 19;
               var ResponseData = _AFSegamationMasterBLL.SegamationDetailsLookUp(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {                   
                   _SegmentDetailsTypes = ResponseData.SegmentDetailList;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void SKUBarCodeGenerate()
       {
           try
           {
               var _BarcodeSettingsBLL = new BarcodeSettingsBLL();
               var RequestData = new SelectBarcodeGenerateBySKURequest();
               RequestData.BaseIntegrateStoreID = 19;
               var ResponseData = _BarcodeSettingsBLL.BarcodeGenerateBySKU(RequestData);

               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   SKUBarCode = (ResponseData.BarcodeGenerateBySKURecord.Prefix + ResponseData.BarcodeGenerateBySKURecord.Suffix);
                   BarCodeRunningNo = ResponseData.BarcodeGenerateBySKURecord.RunningNo;
                   BarCodeID = ResponseData.BarcodeGenerateBySKURecord.ID;
               }
               else
               {
                   BarCodeRunningNo = 0;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void SaveSKUList(List<StyleMaster> StyleMasterList)
       {
           long BarcodeRunningNum = 0;
           List<SKUMasterTypes> SKUList=new List<SKUMasterTypes>();
           SKUList = GenerateSKUList(StyleMasterList);
           SaveSKUMasterRequest RequestData=new SaveSKUMasterRequest();
           SaveSKUMasterResponse ResponseData=new SaveSKUMasterResponse();

           SKUBarCodeGenerate();

           int SkuIndex = 0;
           int length = 0;
           int Longlength = 0;

           string DynamicNo = string.Empty;
           string RunningNolength = string.Empty;
           string SumPrefixSuffix = SKUBarCode;
           string SKUBarcode = string.Empty;
           BarcodeRunningNum = BarCodeRunningNo;

           RunningNolength = Convert.ToString(BarcodeRunningNum);
           length = (13 - (RunningNolength.Length));
           long BarCode = 0;

           foreach (SKUMasterTypes objSKUMasterTypes in SKUList)
           {
               if (objSKUMasterTypes.Active == true && (objSKUMasterTypes.BarCode == null || objSKUMasterTypes.BarCode == string.Empty))
               {
                   BarCode = BarcodeRunningNum + 1;
                   //string BarCode = string.Empty;
                   //FilteredSkuList[SkuIndex].BarCode = BarCode;
                   if (SumPrefixSuffix.Length <= 13)
                   {
                       DynamicNo = SumPrefixSuffix.PadRight(length, '0');
                   }
                   SKUBarcode = DynamicNo + BarCode;
                   if (SKUBarcode.Length > 13)
                   {
                       Longlength = (13 - (SumPrefixSuffix.Length + BarCode.ToString().Length));
                       Longlength = Longlength + (SumPrefixSuffix.Length);
                       DynamicNo = SumPrefixSuffix.PadRight(Longlength, '0');
                       SKUBarcode = DynamicNo + BarCode;
                   }
                   SKUList[SkuIndex].BarCode = SKUBarcode;
                   SkuIndex++;
                   BarcodeRunningNum++;
               }
           }
           RequestData.SKUCode = string.Empty;
           RequestData.SKUMasterTypesRecord = SKUList;
           RequestData.BarCodeRunningNo = BarcodeRunningNum;
           RequestData.BaseIntegrateStoreID = 19;
           ResponseData = _SKUMasterBLL.SaveSKUMaster(RequestData);
           //_IImportView.Message = ResponseData.DisplayMessage;

       }
//*************************************************************************************************************************************************************************
       public void SaveImportStylePricingMaster()
       {
           var RequestData = new SaveStylePricingMasterRequest();
           RequestData.BaseIntegrateStoreID = 19;
           RequestData.ImportStylePricingExcelList = _IImportView.ImportStylePricingList;
           SaveStylePricingMasterResponse ResponseData = _ImportStylePricingBLL.SaveImportExcelStylePricingMaster(RequestData);
           _IImportView.Message = ResponseData.DisplayMessage;

       }

   }
}
