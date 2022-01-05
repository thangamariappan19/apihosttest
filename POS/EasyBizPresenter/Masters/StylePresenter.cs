using CommonRoutines;
using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Pricing;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizIView.Masters.IPriceTypeMaster;
using EasyBizIView.Masters.IStyle;
using EasyBizIView.Masters.Style;
using EasyBizIView.Transactions.IPricePoint;
using EasyBizPresenter.Common;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.ArmadaCollectionsMasterRequest;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.DropMasterRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.PriceTypeRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.ScaleMasterResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class StylePresenter
    {
        IStyleView _IStyleView;        
        PricePointBLL _PricePointBLL = new PricePointBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        YearBLL _YearBLL = new YearBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        ColorBLL _ColorBLL = new ColorBLL();
        ScaleBLL _ScaleBLL = new ScaleBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        DesignMasterBLL _DesignBLL = new DesignMasterBLL();
        AFSegamationMasterBLL _AFSegamationMasterBLL = new AFSegamationMasterBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        AFSegamationMasterBLL _SegamationBLL = new AFSegamationMasterBLL();
        CollectionMasterBLL _CollectionMasterBLL = new CollectionMasterBLL();
        DivisionBLL _DvisionBLL = new DivisionBLL();
        ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();
        SubBrandBLL _SubBrandBLL = new SubBrandBLL();
        ProductSubGroupBLL _ProductSubGroupBLL = new ProductSubGroupBLL();
        SubCollectionBLL _SubCollectionBLL = new SubCollectionBLL();
        EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();
        SeasonBLL _SeasonBLL = new SeasonBLL();
        ArmadaCollectionBLL _ArmadaCollectionBLL = new ArmadaCollectionBLL();
        StyleStatusBLL _StyleStatusBLL = new StyleStatusBLL();
       ProductLineMasterBLL _ProductLineBLL = new ProductLineMasterBLL();
       SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
       DropMasterBLL _DropMasterBLL = new DropMasterBLL();
       List<SegmentMaster> _SegmentDetailsTypes = new List<SegmentMaster>();
       List<AFSegamationMasterTypes> _AFSegamationMasterTypes = new List<AFSegamationMasterTypes>();
       PriceListBLL _PriceListBLL = new PriceListBLL();
       TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
       BarcodeSettingsBLL _BarcodeSettingsBLL = new BarcodeSettingsBLL();
       long BarcodeRunningNum = 0;
        public StylePresenter(IStyleView ViewObj)
        {
            _IStyleView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if(_IStyleView.Franchise==false)
            {
                if (_IStyleView.NewDesignID == 0)
                {
                    _IStyleView.Message = "Design Code  is missing Please Select it.";
                }
                else if (_IStyleView.StyleCode.Trim() == string.Empty)
                {
                    _IStyleView.Message = "StyleCode Code is missing Please Enter it.";
                }
                else if (_IStyleView.ItemImage.Trim() == string.Empty)
                {
                    _IStyleView.Message = "Image is missing.Please Select Image";
                }
                //else if (_IStyleView.StyleCode.Length > 8)
                //{
                //    _IStyleView.Message = " StyleCode not allow more than eight Character.";
                //}
                else if (_IStyleView.StyleName.Trim() == string.Empty)
                {
                    _IStyleView.Message = "StyleName is missing Please Enter it.";
                }
                else if (_IStyleView.SegmentationID == 0)
                {
                    _IStyleView.Message = "Segmentation  is missing Please Select it.";
                }
                else if (_IStyleView.BrandID == 0)
                {
                    _IStyleView.Message = "Brand is missing Please Select it.";
                }
                else if (_IStyleView.DropID == 0)
                {
                    _IStyleView.Message = "Drop is missing Please Select it.";
                }
                else if (_IStyleView.SubBrandID == 0)
                {
                    _IStyleView.Message = "SubBrand is missing Please Select it.";
                }
                else if (_IStyleView.CollectionID == 0)
                {
                    _IStyleView.Message = "Collection is missing Please Select it.";
                }
                else if (_IStyleView.StyleStatusID == 0)
                {
                    _IStyleView.Message = "Status  is missing Please Select it.";
                }
                else if (_IStyleView.ProductGroupID == 0)
                {
                    _IStyleView.Message = "Product Group  is missing Please Select it.";
                }
                else if (_IStyleView.ProductSubGroupID == 0)
                {
                    _IStyleView.Message = "Product SubGroup  is missing Please Select it.";
                }
                else if (_IStyleView.SeasonID == 0)
                {
                    _IStyleView.Message = "Season is missing Please Select it.";
                }
                else if (_IStyleView.YearCode == 0)
                {
                    _IStyleView.Message = "Year is missing Please Select it.";
                }
                else if (_IStyleView.DivisionID == 0)
                {
                    _IStyleView.Message = "Division is missing Please Select it.";
                }
                else if (_IStyleView.ProductLineID == 0)
                {
                    _IStyleView.Message = "ProductLine is missing Please Select it.";
                }
                else if (_IStyleView.DesignerID == 0)
                {
                    _IStyleView.Message = "Designer is missing Please Select it.";
                }
                else if (_IStyleView.PurchasePriceListID == 0)
                {
                    _IStyleView.Message = "PurchasePriceList  is missing Please Select it.";
                }
                else if (_IStyleView.PurchasePrice == Convert.ToDecimal(0))
                {
                    _IStyleView.Message = "PurchasePrice is missing Please Enter it.";
                }
                else if (_IStyleView.PurchaseCurrencyID == 0)
                {
                    _IStyleView.Message = "PurchaseCurrency is missing Please Select it.";
                }
                else if (_IStyleView.ExchangeRate == Convert.ToDecimal(0))
                {
                    _IStyleView.Message = "Exchange Rate is missing Please Enter it.";
                }
               
                    
                else if (_IStyleView.ScaleDetailList == null)
                {
                    _IStyleView.Message = "Select Any One Scale.";
                }
                else if (_IStyleView.ColorList == null)
                {
                    _IStyleView.Message = "Select Any One Color.";
                }
                else if (_IStyleView.ScaleID == 0)
                {
                    _IStyleView.Message = " Please Select One Scale.";
                }
                else if (_IStyleView.SKUItemImageMasterList == null)
                {
                    _IStyleView.Message = " Please Add One Image.";
                }

                else
                {
                    objBool = true;
                }
                return objBool;
            }
           
            else if (_IStyleView.StyleCode.Trim() == string.Empty)
            {
                _IStyleView.Message = "StyleCode Code is missing Please Enter it.";
            }
            else if (_IStyleView.ItemImage.Trim() == string.Empty)
            {
                _IStyleView.Message = "Image is missing Please Enter it.";
            }
            //else if (_IStyleView.StyleCode.Length > 8)
            //{
            //    _IStyleView.Message = " StyleCode not allow more than eight Character.";
            //}
            else if (_IStyleView.StyleName.Trim() == string.Empty)
            {
                _IStyleView.Message = "StyleName is missing Please Enter it.";
            }
            else if (_IStyleView.SegmentationID == 0)
            {
                _IStyleView.Message = "Segmentation  is missing Please Select it.";
            }
            else if (_IStyleView.BrandID == 0)
            {
                _IStyleView.Message = "Brand is missing Please Select it.";
            }
            else if (_IStyleView.SubBrandID == 0)
            {
                _IStyleView.Message = "SubBrand is missing Please Select it.";
            }
            else if (_IStyleView.CollectionID == 0)
            {
                _IStyleView.Message = "Collection is missing Please Select it.";
            }
            else if (_IStyleView.StyleStatusID == 0)
            {
                _IStyleView.Message = "Status  is missing Please Select it.";
            }
            else if (_IStyleView.ProductGroupID == 0)
            {
                _IStyleView.Message = "Product Group  is missing Please Select it.";
            }
            else if (_IStyleView.ProductSubGroupID == 0)
            {
                _IStyleView.Message = "Product SubGroup  is missing Please Select it.";
            }
            else if (_IStyleView.SeasonID == 0)
            {
                _IStyleView.Message = "Season is missing Please Select it.";
            }
            else if (_IStyleView.YearCode == 0)
            {
                _IStyleView.Message = "Year is missing Please Select it.";
            }
            else if (_IStyleView.DivisionID == 0)
            {
                _IStyleView.Message = "Division is missing Please Select it.";
            }
            else if (_IStyleView.ProductLineID == 0)
            {
                _IStyleView.Message = "ProductLine is missing Please Select it.";
            }
            else if (_IStyleView.DesignerID == 0)
            {
                _IStyleView.Message = "Designer is missing Please Select it.";
            }
            else if (_IStyleView.PurchasePriceListID == 0)
            {
                _IStyleView.Message = "PurchasePriceList  is missing Please Select it.";
            }
            else if (_IStyleView.PurchasePrice == Convert.ToDecimal(0))
            {
                _IStyleView.Message = "PurchasePrice is missing Please Enter it.";
            }
            else if (_IStyleView.PurchaseCurrencyID == 0)
            {
                _IStyleView.Message = "PurchaseCurrency is missing Please Select it.";
            }
            else if (_IStyleView.ExchangeRate == Convert.ToDecimal(0))
            {
                _IStyleView.Message = "Exchange Rate is missing Please Enter it.";
            }
            //else if (_IStyleView.SalesCurrencyID == 0)
            //{
            //    _IStyleView.Message = "RRPCurrency  is missing Please Select it.";
            //}
            //else if (_IStyleView.RRPPrice == 0.0)
            //{
            //    _IStyleView.Message = "RRPPrice is missing Please Enter it.";
            //}
            //else if (_IStyleView.ScaleDetailList == null)
            //{
            //    _IStyleView.Message = "Select Any One Scale.";
            //}
            //else if (_IStyleView.ColorList == null)
            //{
            //    _IStyleView.Message = "Select Any One Color.";
            //}


            else if (_IStyleView.ScaleDetailList == null)
            {
                _IStyleView.Message = "Select Any One Scale.";
            }
            else if (_IStyleView.ColorList == null)
            {
                _IStyleView.Message = "Select Any One Color.";
            }
            else if (_IStyleView.ScaleID == 0)
            {
                _IStyleView.Message = " Please Select One Scale.";
            }
            else if (_IStyleView.SKUItemImageMasterList == null)
            {
                _IStyleView.Message = " Please Add One Image.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        
         public void SaveStyle()
         {
             try
             {
                 if (IsValidForm())
                 {
                     var RequestData = new SaveStyleRequest();
                     RequestData.StyleRecord = new StyleMaster();
                     RequestData.StyleWithScaleDetailsList = _IStyleView.ScaleDetailList;
                     RequestData.ItemImageMasterDetailsList = _IStyleView.SKUItemImageMasterList;
                     RequestData.StyleWithColorDetailsList = _IStyleView.ColorList;
                     RequestData.StyleRecord.ID = _IStyleView.ID;
                     RequestData.StyleRecord.ScaleID = _IStyleView.ScaleID;
                     RequestData.StyleRecord.StyleCode = _IStyleView.StyleCode;
                     RequestData.StyleRecord.StyleName = _IStyleView.StyleName;
                     RequestData.StyleRecord.SalesType = _IStyleView.SalesType;
                     //RequestData.StyleRecord.ItemImage = _IStyleView.ItemImage;
                     RequestData.StyleRecord.DocumentID = _IStyleView.DocumentID;
                     RequestData.StyleRecord.ShortDesignName = _IStyleView.ShortDesignName;
                     RequestData.StyleRecord.DesignID = _IStyleView.NewDesignID;
                     RequestData.StyleRecord.ProductDepartmentCode = _IStyleView.ProductDepartmentCode;
                     RequestData.StyleRecord.Composition = _IStyleView.Composition;
                     RequestData.StyleRecord.ArabicStyle = _IStyleView.ArabicStyle;
                     RequestData.StyleRecord.SymbolGroup = _IStyleView.SymbolGroup;
                     RequestData.StyleRecord.Owner = _IStyleView.Owner;
                     RequestData.StyleRecord.CountryOfOrigin = _IStyleView.CountryOfOrigin;
                     RequestData.StyleRecord.ShortDescriptionn = _IStyleView.ShortDescriptionn;
                     RequestData.StyleRecord.DesignName = _IStyleView.DesignName;
                     RequestData.StyleRecord.BrandID = _IStyleView.BrandID;
                     RequestData.StyleRecord.DropID = _IStyleView.DropID;
                     RequestData.StyleRecord.Grade = _IStyleView.Grade;
                     RequestData.StyleRecord.DevelopmentOffice = _IStyleView.DevelopmentOffice;
                     RequestData.StyleRecord.SubBrandID = _IStyleView.SubBrandID;
                     RequestData.StyleRecord.CollectionID = _IStyleView.CollectionID;
                     RequestData.StyleRecord.DivisionID = _IStyleView.DivisionID;
                     RequestData.StyleRecord.ProductGroupID = _IStyleView.ProductGroupID;
                     RequestData.StyleRecord.ProductSubGroupID = _IStyleView.ProductSubGroupID;
                     RequestData.StyleRecord.PurchasePriceListID = _IStyleView.PurchasePriceListID;
                     RequestData.StyleRecord.PurchasePriceListID = _IStyleView.PurchasePriceListID;
                     RequestData.StyleRecord.SeasonID = _IStyleView.SeasonID;
                     RequestData.StyleRecord.ProductLineID = _IStyleView.ProductLineID;
                     RequestData.StyleRecord.DesignerID = _IStyleView.DesignerID;
                     RequestData.StyleRecord.RRPCurrencyID = _IStyleView.SalesCurrencyID;
                     RequestData.StyleRecord.YearCode = _IStyleView.YearCode;                     
                     RequestData.StyleRecord.PurchaseCurrencyID = _IStyleView.PurchaseCurrencyID;
                     RequestData.StyleRecord.PurchasePrice = _IStyleView.PurchasePrice;
                     RequestData.StyleRecord.SalesPriceListID = _IStyleView.SalesPriceListID;
                     RequestData.StyleRecord.SalesPrice = _IStyleView.SalesPrice;                     
                     RequestData.StyleRecord.RRPPrice = _IStyleView.RRPPrice;
                     RequestData.StyleRecord.PurchaseCurrencyID = _IStyleView.PurchaseCurrencyID;
                     RequestData.StyleRecord.StyleSegmentation = _IStyleView.SegmentationID;
                     RequestData.StyleRecord.StyleStatusID = _IStyleView.StyleStatusID;
                     RequestData.StyleRecord.ArmadaCollectionID = _IStyleView.ArmadaCollectionID;
                     RequestData.StyleRecord.CreateBy = _IStyleView.UserID;
                     RequestData.StyleRecord.CreateOn = DateTime.Now;
                     RequestData.StyleRecord.Active = _IStyleView.Active;
                     RequestData.StyleRecord.Franchise = _IStyleView.Franchise;
                     RequestData.StyleRecord.SCN = _IStyleView.SCN;
                     RequestData.StyleRecord.ExchangeRate = _IStyleView.ExchangeRate;
                     RequestData.StyleRecord.CountryID = _IStyleView.CountryID;
                     RequestData.StyleRecord.StoreID = _IStyleView.StoreID;
                     RequestData.StyleRecord.StateID = _IStyleView.StateID;

                     RequestData.BaseIntegrateStoreID = _IStyleView.BrandID; // If want to syncronize all stores remove this line

                     var ResponseData = _StyleMasterBLL.SaveStyle(RequestData);
                     _IStyleView.Message = ResponseData.DisplayMessage;
                     _IStyleView.ProcessStatus = ResponseData.StatusCode;
                     _IStyleView.ID =Convert.ToInt32(ResponseData.IDs);
                 }
                 else
                 {
                     _IStyleView.ProcessStatus = Enums.OpStatusCode.GeneralError;
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
                 var RequestData = new SelectBarcodeGenerateBySKURequest();

                 var ResponseData = _BarcodeSettingsBLL.BarcodeGenerateBySKU(RequestData);

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SKUBarCode = (ResponseData.BarcodeGenerateBySKURecord.Prefix + ResponseData.BarcodeGenerateBySKURecord.Suffix);
                     _IStyleView.BarCodeRunningNo = ResponseData.BarcodeGenerateBySKURecord.RunningNo;
                     _IStyleView.BarCodeID = ResponseData.BarcodeGenerateBySKURecord.ID;
                 }
                 else
                 {
                     _IStyleView.BarCodeRunningNo = 0;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SaveItemMaster()
         {
             try
             {
                 if (IsValidForm())
                 {
                     var RequestData = new SaveSKUMasterRequest();
                     RequestData.BarCodeID = _IStyleView.BarCodeID;                   
                     
                     var FilteredSkuList = new List<SKUMasterTypes>();

                     var OldSkuList = new List<SKUMasterTypes>();
                     OldSkuList = GetSKUList();

                     var NewSkuList = new List<SKUMasterTypes>();
                     NewSkuList = _IStyleView.StyleDetailsList;

                     List<StylePricing> StylePriceList = new List<StylePricing>();
                     StylePriceList = _IStyleView.StylepricingList;

                     FilteredSkuList = NewSkuList.Where(x => x.Active == true).ToList();
                     //***********************Style Pricing***********************
                     //int index = -1;
                     //if (OldSkuList.Count == 0)
                     //{
                     //    FilteredSkuList = NewSkuList.Where(x => x.Active == true).ToList();
                     //}
                     //else if (OldSkuList.Count > NewSkuList.Count)
                     //{
                     //    foreach (SKUMasterTypes objSKUMasterTypes in NewSkuList)
                     //    {
                     //        index = OldSkuList.IndexOf(OldSkuList.Single(i => i.SKUCode.Trim().ToUpper() == objSKUMasterTypes.SKUCode.Trim().ToUpper()));
                     //        if (index > -1)
                     //        {
                     //            OldSkuList[index].Active = false;
                     //        }
                     //    }
                     //    FilteredSkuList = OldSkuList;
                     //}
                     //else
                     //{
                     //    foreach (SKUMasterTypes objSKUMasterTypes in OldSkuList)
                     //    {
                     //        index = NewSkuList.IndexOf(NewSkuList.Single(i => i.SKUCode.Trim().ToUpper() == objSKUMasterTypes.SKUCode.Trim().ToUpper()));
                     //        if (index > -1)
                     //        {
                     //            if (NewSkuList[index].Active == true && objSKUMasterTypes.Active == true)
                     //            {
                     //                NewSkuList.RemoveAt(index);
                     //            }
                     //            else if (NewSkuList[index].Active == true && objSKUMasterTypes.Active == false)
                     //            {
                     //                NewSkuList[index].ID = objSKUMasterTypes.ID;
                     //                NewSkuList[index].Active = true;
                     //            }
                     //            else
                     //            {
                     //                NewSkuList[index].ID = objSKUMasterTypes.ID;
                     //                NewSkuList[index].Active = false;
                     //            }
                     //        }
                     //    }
                    // FilteredSkuList = NewSkuList;
                     //}
                     //****************************************************************************************
                     //BarCode Generate
                     int SkuIndex = 0;
                     int length = 0;
                     int Longlength = 0;
                     
                     string DynamicNo = string.Empty;
                     string RunningNolength = string.Empty;
                     string SumPrefixSuffix = _IStyleView.SKUBarCode;
                     string SKUBarcode =string.Empty;
                     string SKUCode = string.Empty;
                     int PriceListID = 0;
                     BarcodeRunningNum = _IStyleView.BarCodeRunningNo;
                     RunningNolength = Convert.ToString(_IStyleView.BarCodeRunningNo);
                     length = (13 - (RunningNolength.Length));
                     long BarCode = 0;
                     
                     foreach(SKUMasterTypes objSKUMasterTypes in FilteredSkuList)
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

                             FilteredSkuList[SkuIndex].BarCode = SKUBarcode;
                             //FilteredSkuList[SkuIndex].SKUCode = SKUCode;
                             SKUCode = objSKUMasterTypes.SKUCode;
                             PriceListID = objSKUMasterTypes.SalesPriceListID;
                             SkuIndex++;
                             BarcodeRunningNum++;
                             
                         }
                     }
                     //SKUCode = FilteredSkuList[SkuIndex].SKUCode;
                     RequestData.SKUCode = SKUCode;
                     RequestData.SalePriceListID = PriceListID;
                     RequestData.SKUMasterTypesRecord = FilteredSkuList; //FilteredSkuList;//_IStyleView.StyleDetailsList;

                     //RequestData.ItemImageMasterList = null;
                     RequestData.ItemImageMasterList = _IStyleView.SKUItemImageMasterList;
                     RequestData.BaseEntry = "StyleMaster";
                     RequestData.BarCodeRunningNo = BarCode;
                     RequestData.StylePricingList = StylePriceList;
                     
                     var ResponseData = _SKUMasterBLL.SaveSKUMaster(RequestData);
                     _IStyleView.Message = ResponseData.DisplayMessage;
                     _IStyleView.ProcessStatus = ResponseData.StatusCode;
                 }
                     if (_IStyleView.ProcessStatus == Enums.OpStatusCode.Success)
                     {

                     }
                 else
                 {
                     _IStyleView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SelectStyleRecord()
         {
             try
             {
                 var RequestData = new SelectByStyleIDRequest();
                 RequestData.ID = _IStyleView.ID;
                 var ResponseData = _StyleMasterBLL.SelectStyleRecord(RequestData);
                 _IStyleView.StyleCode = ResponseData.StyleRecord.StyleCode;
                 _IStyleView.StyleName = ResponseData.StyleRecord.StyleName;
                 _IStyleView.SalesType = ResponseData.StyleRecord.SalesType;
                 //_IStyleView.ItemImage = ResponseData.StyleRecord.ItemImage;
                 _IStyleView.ShortDesignName = ResponseData.StyleRecord.ShortDesignName;
                 _IStyleView.NewDesignID = ResponseData.StyleRecord.DesignID;
                 _IStyleView.DesignName = ResponseData.StyleRecord.DesignName;
                 _IStyleView.BrandID = ResponseData.StyleRecord.BrandID;
                 _IStyleView.DropID = ResponseData.StyleRecord.DropID;
                 _IStyleView.Grade = ResponseData.StyleRecord.Grade;
                 _IStyleView.DevelopmentOffice = ResponseData.StyleRecord.DevelopmentOffice;
                 _IStyleView.SubBrandID = ResponseData.StyleRecord.SubBrandID;
                 _IStyleView.CollectionID = ResponseData.StyleRecord.CollectionID;
                 _IStyleView.DivisionID = ResponseData.StyleRecord.DivisionID;
                 _IStyleView.ProductGroupID = ResponseData.StyleRecord.ProductGroupID;
                 _IStyleView.ProductDepartmentCode = ResponseData.StyleRecord.ProductDepartmentCode;
                 _IStyleView.Composition = ResponseData.StyleRecord.Composition;
                 _IStyleView.ArabicStyle = ResponseData.StyleRecord.ArabicStyle;
                 _IStyleView.SymbolGroup = ResponseData.StyleRecord.SymbolGroup;
                 _IStyleView.Owner = ResponseData.StyleRecord.Owner;
                 _IStyleView.CountryOfOrigin = ResponseData.StyleRecord.CountryOfOrigin;
                 _IStyleView.ShortDescriptionn = ResponseData.StyleRecord.ShortDescriptionn;
                 _IStyleView.ProductSubGroupID = ResponseData.StyleRecord.ProductSubGroupID;
                 _IStyleView.PurchasePrice = ResponseData.StyleRecord.PurchasePrice;
                 _IStyleView.RRPPrice = ResponseData.StyleRecord.RRPPrice;
                 _IStyleView.SeasonID = ResponseData.StyleRecord.SeasonID;
                 _IStyleView.ProductLineID = ResponseData.StyleRecord.ProductLineID;
                 _IStyleView.DesignerID = ResponseData.StyleRecord.DesignerID;
                 _IStyleView.SalesCurrencyID = ResponseData.StyleRecord.RRPCurrencyID;
                 _IStyleView.PurchaseCurrencyID = ResponseData.StyleRecord.PurchaseCurrencyID;
                 _IStyleView.PurchasePriceListID = ResponseData.StyleRecord.PurchasePriceListID;
                 _IStyleView.SalesPriceListID = ResponseData.StyleRecord.SalesPriceListID;
                 _IStyleView.SalesPrice = ResponseData.StyleRecord.SalesPrice;              
                 _IStyleView.YearCode = ResponseData.StyleRecord.YearCode;
                 _IStyleView.ScaleID = ResponseData.StyleRecord.ScaleID;
                 _IStyleView.SegmentationID = ResponseData.StyleRecord.StyleSegmentation;
                 _IStyleView.StyleStatusID = ResponseData.StyleRecord.StyleStatusID;
                 _IStyleView.ArmadaCollectionID = ResponseData.StyleRecord.ArmadaCollectionID;
                
                 _IStyleView.SCN = ResponseData.StyleRecord.SCN;
                 _IStyleView.ExchangeRate = ResponseData.StyleRecord.ExchangeRate;
                 _IStyleView.Active = ResponseData.StyleRecord.Active;
                 _IStyleView.Franchise = ResponseData.StyleRecord.Franchise;

                _IStyleView.ScaleDetailList = ResponseData.StyleRecord.ScaleDetailMasterList;
                _IStyleView.ColorList = ResponseData.StyleRecord.ColorMasterList;

                 if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                 {
                     _IStyleView.Message = ResponseData.DisplayMessage;
                 }
                 else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                 {
                     _IStyleView.Message = ResponseData.DisplayMessage;
                 }
                 _IStyleView.ProcessStatus = ResponseData.StatusCode;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public List<SKUMasterTypes>  GetSKUList()
         {
             var SKUList = new List<SKUMasterTypes>();
             try
             {
                 var RequestData = new SelectSKUByStyleIDRequest();                
                 RequestData.StyleID = _IStyleView.ID;
                 var ResponseData = _SKUMasterBLL.SelectByStyleID(RequestData);
                 //var ResponseData = _SKUMasterBLL.SelectByStyleID(RequestData);                

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     SKUList =  ResponseData.SKUMasterTypesList;
                 }                 
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return SKUList;
         }
         public void GetStockList()
         {
             try
             {
                 var RequestData = new GetStockByStyleCodeRequest();
                 var ResponseData = new GetStockByStyleCodeResponse();

                 RequestData.StyleCode = _IStyleView.StyleCode;
                 RequestData.StockWiseName = "All";
                 ResponseData = _TransactionLogBLL.GetStockByStyleCode(RequestData);

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.StockList = ResponseData.StockList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }
         public void GetStyleListOverView()
         {
             try
             {
                 var RequestData = new GetStockByStyleCodeRequest();
                 var ResponseData = new GetStockByStyleCodeResponse();

                 RequestData.StyleCode = _IStyleView.StyleCode;
                 RequestData.StockWiseName = "All";
                 RequestData.RequestFrom = Enums.RequestFrom.DefaultLoad;
                 ResponseData = _TransactionLogBLL.StyleStockOverView(RequestData);

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.StockList = ResponseData.StockList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }
         public void GetColorWiseStockList()
         {
             try
             {
                 var RequestData = new GetStockByStyleCodeRequest();
                 var ResponseData = new GetStockByStyleCodeResponse();

                 RequestData.StyleCode = _IStyleView.StyleCode;
                 RequestData.StockWiseName = "Color";
                 ResponseData = _TransactionLogBLL.GetStockByStyleCode(RequestData);

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ColorWiseStockList = ResponseData.ColorWiseStockList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }
         public void GetScaleWiseStockList()
         {
             try
             {
                 var RequestData = new GetStockByStyleCodeRequest();
                 var ResponseData = new GetStockByStyleCodeResponse();

                 RequestData.StyleCode = _IStyleView.StyleCode;
                // RequestData.StockWiseName = "Scale";  TaskID=POS-43
                 RequestData.StockWiseName = "All";
                 ResponseData = _TransactionLogBLL.GetStockByStyleCode(RequestData);

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ScaleWiseStockList = ResponseData.ScaleWiseStockList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }
         public void SelectPricePointRecord()
         {
             try
             {
                 var RequestData = new SelectAllPricePointRequest();             
                 var ResponseData = _PricePointBLL.GetPricePointList(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.PricePointList = ResponseData.PricePointList;
                 }
                 else
                 {
                     _IStyleView.Message = ResponseData.DisplayMessage;
                     _IStyleView.ProcessStatus = ResponseData.StatusCode;
                 }

             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SelectStyleWithScaleDetails()
         {
             SelectScaleDetailsRequest RequestData = new SelectScaleDetailsRequest();
             RequestData.ShowInActiveRecords = true;
             RequestData.ID = _IStyleView.ID;
             RequestData.ScaleID = _IStyleView.ScaleID;
             SelectScaleDetailsResponse ResponseData = _StyleMasterBLL.SelectStyleWithScaleRecord(RequestData);
             
             if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
             {
                 _IStyleView.ScaleDetailList = ResponseData.ScaleDetailMasterRecord;

             
             }
             else
             {
                 _IStyleView.Message = ResponseData.DisplayMessage;
                 _IStyleView.ProcessStatus = ResponseData.StatusCode;
             }
         }
         public void SelectStyleWithColorDetails()
         {
             SelectColorDetailsRequest RequestData = new SelectColorDetailsRequest();
             RequestData.ShowInActiveRecords = true;
             RequestData.ID = _IStyleView.ID;
             SelectColorDetailsResponse ResponseData = _StyleMasterBLL.SelectStyleWithColorDetailsRecord(RequestData);

             if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
             {
                 _IStyleView.ColorList = ResponseData.StyleWithColorDetailsRecord;
             }
             else
             {
                 _IStyleView.Message = ResponseData.DisplayMessage;
                 _IStyleView.ProcessStatus = ResponseData.StatusCode;
             }
         }
         public void SelectStyleWithItemImageDetails()
         {
             SelectItemImageRequest RequestData = new SelectItemImageRequest();
             RequestData.ShowInActiveRecords = true;
             RequestData.ID = _IStyleView.ID;
             RequestData.FormName = "Style";
             SelectItemImageResponse ResponseData = _StyleMasterBLL.SelectStyleWithItemImage(RequestData);

             if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
             {
                 _IStyleView.SKUItemImageMasterList = ResponseData.ItemImageMaster;


             }
             else
             {
                 _IStyleView.Message = ResponseData.DisplayMessage;
                 _IStyleView.ProcessStatus = ResponseData.StatusCode;
             }
         }
         public void SelectDesignWithItemImageDetails()
         {
             try
             {
                 SelectItemImageRequest RequestData = new SelectItemImageRequest();
                 RequestData.ShowInActiveRecords = true;
                 RequestData.ID = _IStyleView.NewDesignID;
                 RequestData.FormName = "Design";
                 SelectItemImageResponse ResponseData = _StyleMasterBLL.SelectStyleWithItemImage(RequestData);

                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SKUItemImageMasterList = ResponseData.ItemImageMaster;


                 }
                 else
                 {
                     //_IStyleView.Message = ResponseData.DisplayMessage;
                     _IStyleView.ProcessStatus = ResponseData.StatusCode;
                 }
             }
             catch
             {

             }
         }
         public void DeleteStyle()
         {
             try
             {
                 var RequestData = new DeleteStyleRequest();
                 RequestData.ID = _IStyleView.ID;
                 RequestData.BrandID = _IStyleView.BrandID;
                 var ResponseData = _StyleMasterBLL.DeleteStyle(RequestData);
                 _IStyleView.Message = ResponseData.DisplayMessage;
                 _IStyleView.ProcessStatus = ResponseData.StatusCode;
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
                     _IStyleView.YearLookUp = ResponseData.YearList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetColorLookUp()
         {
             try
             {
                 var RequestData = new SelectColorLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _ColorBLL.SelectColorLookup(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ColorList = ResponseData.ColorList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetStyleStatusLookUp()
         {
             try
             {
                 var RequestData = new SelectStyleStatusLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _StyleStatusBLL.SelectStyleStatusLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.StyleStatusMasterTypeLookUp = ResponseData.StyleStatusMasterTypeList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetScaleLookUp()
         {
             try
             {
                 var RequestData = new SelectScaleLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.BrandID = _IStyleView.BrandID;
                 var ResponseData = _ScaleBLL.ScaleLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ScaleMasterLookUp = ResponseData.ScaleList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetSalePriceListLookup()
         {
             try
             {
                 var RequestData = new SelectSalePriceListLookupRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.SalePriceListID = _IStyleView.SalesPriceListID;
                 RequestData.stylecode = _IStyleView.StyleCode;                
                 var ResponseData = _PriceListBLL.SalePriceListLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SalePrice = ResponseData.SalePriceListTypeData[0].Price;
                     _IStyleView.StylepricingList = ResponseData.SalePriceListTypeData;
                 }
                 else
                 {
                     _IStyleView.SalePrice = 0;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }            
         }

         public void GetSalePriceList()
         {
             try
             {
                 var RequestData = new SelectSalePriceListLookupRequest();
                 RequestData.ShowInActiveRecords = false;                
                 RequestData.stylecode = _IStyleView.StyleCode;
                 var ResponseData = _PriceListBLL.SalePriceListLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SalePrice = ResponseData.SalePriceListTypeData[0].Price;
                     _IStyleView.StylepricingList = ResponseData.SalePriceListTypeData;
                 }
                 else
                 {
                     _IStyleView.SalePrice = 0;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetScaleDeatilsLookUp()
         {
             try
             {
                 var RequestData = new SelectScaleDetailsLookUpRequest();

                 RequestData.ID = _IStyleView.ScaleID;
                 RequestData.StyleID = _IStyleView.ID;

                 var ResponseData = _ScaleBLL.ScaleDetailsLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ScaleDetailList = ResponseData.ScaleDetailMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetSegamationDetailsLookUp()
         {
             try
             {
                 var RequestData = new SelectSegamationDetailsLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.ID = _IStyleView.SegmentationID;
                 var ResponseData = _AFSegamationMasterBLL.SegamationDetailsLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SegmentDetailList = ResponseData.SegmentDetailList;
                     _SegmentDetailsTypes = ResponseData.SegmentDetailList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetDesignCodeLookUp()
         {
             try
             {
                 var RequestData = new SelectDesignMasterLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _DesignBLL.SelectDesignLookUP(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.DesignLookUp = ResponseData.DesignMasterTypesList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetStyleCode()
         {
             try
             {
                 var RequestData = new StyleCodeGeneratingRequest();
                 RequestData.StyleCode = _IStyleView.StyleCode;
                 var ResponseData = _StyleMasterBLL.SelectStyleCodeRunningNum(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.RunningNum = ResponseData.Autonumbering;
                     _IStyleView.StyleRunningNum = ResponseData.Autonumbering;
                 }
                 else
                 {
                     _IStyleView.RunningNum = ResponseData.Autonumbering;
                     _IStyleView.StyleRunningNum = ResponseData.Autonumbering;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetBrandCodeLookUp()
         {
             try
             {
                 var RequestData = new SelectBrandLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.BrandLookUp = ResponseData.BrandList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetSegamationLookUp()
         {
             try
             {
                 var RequestData = new SelectAFsegmentationLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _SegamationBLL.SelectSegmentationLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SegamationMasterLookUp = ResponseData.AFSegmentationMaster;
                     _AFSegamationMasterTypes = ResponseData.AFSegmentationMaster;
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
                 RequestData.CurrencyType = _IStyleView.SalesCurrencyType;
                 var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SalesCurrencyLookUp = ResponseData.CurrencyMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetPurchaseCurrencyLookUp()
         {
             try
             {
                 var RequestData = new SelectCurrencyLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.CurrencyType = _IStyleView.PurchaseCurrencyType;
                 var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.PurchaseCurrencyLookUp = ResponseData.CurrencyMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetCollectionCodeLookUp()
         {
             try
             {
                 var RequestData = new SelectCollectionLookUpRequest();
                 RequestData.ShowInActiveRecords = true;
                 var ResponseData = _CollectionMasterBLL.CollectionLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.CollectionLookUp = ResponseData.CollectionMasterTypesList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetProductGroupLookUp()
         {
             try
             {
                 var RequestData = new SelectProductGroupLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _ProductGroupBLL.ProductGroupLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ProductGroupLookUp = ResponseData.ProductGroupList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetDivisionLookUp()
         {
             try
             {
                 var RequestData = new SelectDivisionLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _DvisionBLL.DivisionLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.DivisionLookUp = ResponseData.DivisionList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetSeasonLookUp()
         {
             try
             {
                 var RequestData = new SelectSeasonLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _SeasonBLL.SelectSeasonLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SeasonLookUp = ResponseData.SeasonList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetDesignerLookUp()
         {
             try
             {
                 var RequestData = new SelectEmployeeLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _EmployeeMasterBLL.SelectEmployeeLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.DesignerLookUp = ResponseData.EmployeeList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetSubBrandLookUp()
         {
             try
             {
                 var RequestData = new SelectSubBrandListForCategoryRequest();
                 RequestData.ShowInActiveRecords = true;
                 RequestData.BrandID = _IStyleView.BrandID;
                 var ResponseData = _SubBrandBLL.SubBrandByBrand(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SubBrandMasterLookUp = ResponseData.SubBrandList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }

         }

         public void PurchasePriceListMasterLookUp()
         {
             try
             {
                 var RequestData = new SelectPriceListLookUPRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.Type = "Purchase";
                 var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.PriceListTypeLookUp = ResponseData.PriceListTypeData;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SalesPriceListMasterLookUp()
         {
             try
             {
                 var RequestData = new SelectPriceListLookUPRequest();
                 RequestData.ShowInActiveRecords = false;
                 RequestData.Type = "All";
                 var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.SalesPriceListTypeLookUp = ResponseData.PriceListTypeData;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetProductSubGroupLookUp()
         {
             try
             {
                 var RequestData = new SelectProductGroupListForProductSubGroupRequest();
                 RequestData.ShowInActiveRecords = true;
                 RequestData.ProductGroupID = _IStyleView.ProductGroupID;
                 var ResponseData = _ProductSubGroupBLL.ProductSubGroupByProductGroup(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ProductSubGroupLookUp = ResponseData.ProductSubGroupList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetProductLineLookUp()
         {
             try
             {
                 var RequestData = new SelectProductLineLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                
                 var ResponseData = _ProductLineBLL.SelectProductLineLookUP(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ProductLineMasterLookUp = ResponseData.ProductLineMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GetArmadaCollectionLookUp()
         {
             try
             {
                 var RequestData = new SelectArmadaCollectionLookUpRequest();
                 RequestData.ShowInActiveRecords = false;

                 var ResponseData = _ArmadaCollectionBLL.ArmadaCollectionLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.ArmadaCollectionsLookUp = ResponseData.ArmadaCollectionsMasterList;
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SelectProductDepartmentCode()
         {
             try
             {
                 var RequestData = new SelectByIDDesignMasterRequest();
                 RequestData.ID = _IStyleView.NewDesignID;
                 var ResponseData = _DesignBLL.SelectByIDDesignMaster(RequestData);
                 //_IStyleView.ProductDepartmentCode = ResponseData.DesignMasterTypesData.ProductDepartmentCode;
                 //_IStyleView.ShortBrandCode = ResponseData.DesignMasterTypesData.ProductDepartmentCode;
                 _IStyleView.StyleName = ResponseData.DesignMasterTypesData.Description;   
                 _IStyleView.DesignName = ResponseData.DesignMasterTypesData.DesignName;            
                 _IStyleView.BrandID = ResponseData.DesignMasterTypesData.BrandID;
                 _IStyleView.DivisionID = ResponseData.DesignMasterTypesData.DivisionID;
                 _IStyleView.ProductGroupID = ResponseData.DesignMasterTypesData.ProductGroupID;
                 _IStyleView.CollectionID = ResponseData.DesignMasterTypesData.CollectionID;
                 _IStyleView.DesignerID = ResponseData.DesignMasterTypesData.DesignerID;
                 _IStyleView.SeasonID = ResponseData.DesignMasterTypesData.SeasonID;
                 _IStyleView.YearCode = ResponseData.DesignMasterTypesData.YearID;
                 _IStyleView.ProductLineID = ResponseData.DesignMasterTypesData.ProductLineID;
                 _IStyleView.ShortDesignName = ResponseData.DesignMasterTypesData.ForeignDescription;
                 _IStyleView.ShortDescription = ResponseData.DesignMasterTypesData.ShortDescription;
                 if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                 {
                     _IStyleView.Message = ResponseData.DisplayMessage;
                 }

                 else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                 {
                     _IStyleView.Message = ResponseData.DisplayMessage;
                 }

                 _IStyleView.ProcessStatus = ResponseData.StatusCode;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void SelectAutoIncrementID()
         {
             try
             {
                 var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                 //RequestData.ID = _IStyleView.DocumentID;
                // RequestData.
                 RequestData.DocumentTypeID = (int)Enums.DocumentType.STYLEMASTER;
                 RequestData.CountryID = _IStyleView.CountryID;
                 RequestData.StateID = _IStyleView.StateID;
                 RequestData.StoreID = _IStyleView.StoreID;
                 var ResponseData = _DocumentNumberingBLL.DocumentNumberingBillNoGenerate(RequestData);
                 _IStyleView.DocumentNo = ResponseData;               
               
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         public void GenerateStyle()
         {
             string SKUNameSeperator = ":";
             try
             {
                 if (IsValidForm())
                 {
                     var StyleDetailsList = new List<SKUMasterTypes>();
                     var ScaleList = new List<ScaleDetailMaster>();
                     var ColorList = new List<ColorMaster>();
                     var ProductGroupList=new List<ProductGroupMaster>();
                     var StyleHeaderList = new List<StyleMaster>();
                     ProductGroupList=_IStyleView. ProductGroupMasterList;
                     ScaleList = _IStyleView.ScaleDetailList;
                     ColorList = _IStyleView.ColorList;
                     foreach (ScaleDetailMaster objScaleDetailMaster in ScaleList)
                     {                         
                         SKUMasterTypes objStyleDetails = new SKUMasterTypes();
                                if ((objScaleDetailMaster.Active == true) || (objScaleDetailMaster.Active == false && objScaleDetailMaster.ID != 0))
                         {

                                     var ProductGroupName = (from c in ProductGroupList
                                                                   where c.ID == _IStyleView.ProductGroupID
                                                                   select
                                                                   c.ProductGroupName).SingleOrDefault<System.String>();
                             //objStyleDetails.Active = true;
                             objStyleDetails.CreateBy = _IStyleView.UserID;
                             objStyleDetails.StyleCode = _IStyleView.StyleCode;
                             objStyleDetails.StyleName = _IStyleView.StyleName;
                             objStyleDetails.StyleID = _IStyleView.ID;
                             objStyleDetails.SegamentationID = _IStyleView.SegmentationID;
                             objStyleDetails.ShortDescription = _IStyleView.ShortDescription;
                             objStyleDetails.DesignID = _IStyleView.NewDesignID;
                             objStyleDetails.ArmadaCollectionID = _IStyleView.ArmadaCollectionID;
                             objStyleDetails.DesignCode = _IStyleView.DesignCode;
                             objStyleDetails.BrandID = _IStyleView.BrandID;
                             objStyleDetails.SubBrandID = _IStyleView.SubBrandID;
                             objStyleDetails.CollectionID = _IStyleView.CollectionID;
                             objStyleDetails.DivisionID = _IStyleView.DivisionID;
                             objStyleDetails.ProductGroupID = _IStyleView.ProductGroupID;
                             objStyleDetails.ProductSubGroupID = _IStyleView.ProductSubGroupID;
                             objStyleDetails.SeasonID = _IStyleView.SeasonID;
                             objStyleDetails.YearID = _IStyleView.YearCode;
                             objStyleDetails.ProductLineID = _IStyleView.ProductLineID;
                             objStyleDetails.DesignerID = _IStyleView.DesignerID;
                             objStyleDetails.PurchasePrice = _IStyleView.PurchasePrice;
                             objStyleDetails.PurchaseCurrencyID = _IStyleView.PurchaseCurrencyID;
                             objStyleDetails.RRPPrice = _IStyleView.RRPPrice;
                             objStyleDetails.RRPCurrencyID = _IStyleView.SalesCurrencyID;
                             objStyleDetails.ScaleID = _IStyleView.ScaleID;
                             objStyleDetails.SizeID = objScaleDetailMaster.ID;
                             objStyleDetails.SizeCode = objScaleDetailMaster.SizeCode;
                             objStyleDetails.SizeName = objScaleDetailMaster.Description;
                             objStyleDetails.ExchangeRate = _IStyleView.ExchangeRate;
                             objStyleDetails.PurchasePriceListID = _IStyleView.PurchasePriceListID;
                             objStyleDetails.ProductGroupName = ProductGroupName;
                             objStyleDetails.BrandShortCode = _IStyleView.ShortBrandCode;
                             objStyleDetails.SalesPriceListID = _IStyleView.SalesPriceListID;
                             objStyleDetails.SalesPrice = _IStyleView.SalesPrice;
                             //objStyleDetails.ColorCode = objScaleDetailMaster.ColorCode;
                             //objStyleDetails.SizeDescription = objScaleDetailMaster.Description;

                             foreach (ColorMaster objColorMaster in ColorList)
                             {
                                 SKUMasterTypes TempStyleDetails = DeepCopyCreator.StyleDetailsDeepCopy(objStyleDetails);

                                 if ((objColorMaster.Active == true) || (objColorMaster.Active == false && objColorMaster.ID != 0))
                                 {
                                     //TempStyleDetails.SizeID = objColorMaster.SizeID;
                                     TempStyleDetails.ColorName = objColorMaster.Description;
                                     TempStyleDetails.ColorCode = objColorMaster.ColorCode;
                                     //TempStyleDetails.SizeCode = objScaleDetailMaster.;
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
                                                      where c.ID == _IStyleView.SegmentationID
                                                      select
                                                      c.UseSeperator).SingleOrDefault<System.String>();
                                     //var ItemCodeMaxLength = (from c in _AFSegamationMasterTypes
                                     //                 where c.ID == _IStyleView.SegmentationID
                                     //                 select
                                     //                 c.MaxLength).SingleOrDefault<System.String>();

                                     if (StyleDefaulDescription == true && ColorDefaulDescription == true && SizeDefaulDescription == true)
                                     {
                                         TempStyleDetails.SKUName = objStyleDetails.BrandShortCode + SKUNameSeperator + objStyleDetails.DesignCode + SKUNameSeperator + objStyleDetails.ProductGroupName + SKUNameSeperator + objStyleDetails.ShortDescription + " " + objColorMaster.Description + " " + objScaleDetailMaster.Description;
                                     }
                                     else if (StyleDefaulDescription == true && ColorDefaulDescription == false && SizeDefaulDescription == true)
                                     {
                                         TempStyleDetails.SKUName = objStyleDetails.BrandShortCode + SKUNameSeperator + objStyleDetails.DesignCode + SKUNameSeperator + objStyleDetails.ProductGroupName + SKUNameSeperator + objStyleDetails.ShortDescription + " " + objScaleDetailMaster.Description;
                                     }
                                     else if (StyleDefaulDescription == true && ColorDefaulDescription == false && SizeDefaulDescription == false)
                                     {
                                         TempStyleDetails.SKUName = objStyleDetails.BrandShortCode + SKUNameSeperator + objStyleDetails.DesignCode + SKUNameSeperator + objStyleDetails.ProductGroupName + SKUNameSeperator + objStyleDetails.ShortDescription;
                                     }
                                     else if (StyleDefaulDescription == true && ColorDefaulDescription == true && SizeDefaulDescription == false)
                                     {
                                         TempStyleDetails.SKUName = objStyleDetails.BrandShortCode + SKUNameSeperator + objStyleDetails.DesignCode + SKUNameSeperator + objStyleDetails.ProductGroupName + SKUNameSeperator + objStyleDetails.ShortDescription + " " + objColorMaster.Description;
                                     }
                                     else if (StyleDefaulDescription == false && ColorDefaulDescription == true && SizeDefaulDescription == true)
                                     {
                                         TempStyleDetails.SKUName =  objColorMaster.Description + " " + objScaleDetailMaster.Description;
                                     }
                                     else if (StyleDefaulDescription == false && ColorDefaulDescription == false && SizeDefaulDescription == true)
                                     {
                                         TempStyleDetails.SKUName =  objScaleDetailMaster.Description;
                                     }
                                     else if (StyleDefaulDescription == false && ColorDefaulDescription == true && SizeDefaulDescription == false)
                                     {
                                         TempStyleDetails.SKUName = objColorMaster.Description ;
                                     }
                                     //else if (StyleDefaulDescription == true && ColorDefaulDescription == true && SizeDefaulDescription == false)
                                     //{
                                     //    TempStyleDetails.SKUName = objStyleDetails.StyleName + Seperator + objColorMaster.ColorCode;
                                     //}
                                     else if (StyleDefaulDescription == false && ColorDefaulDescription == false && SizeDefaulDescription == false)
                                     {
                                         TempStyleDetails.SKUName = "";
                                     }

                                     TempStyleDetails.SKUCode = objStyleDetails.StyleCode + Seperator + objColorMaster.ColorCode + Seperator + objScaleDetailMaster.SizeCode;
                                     TempStyleDetails.ColorID = objColorMaster.ColorID;
                                     TempStyleDetails.SizeCode = objStyleDetails.SizeCode;
                                     TempStyleDetails.StyleCode = objStyleDetails.StyleCode;
                                     TempStyleDetails.StyleID = objStyleDetails.StyleID;
                                     TempStyleDetails.SegamentationID = objStyleDetails.SegamentationID;
                                     //TempStyleDetails.ShortDesignName = objStyleDetails.ShortDesignName;
                                     TempStyleDetails.DesignID = objStyleDetails.DesignID;
                                     //TempStyleDetails.ProductDepartmentCode = objStyleDetails.ProductDepartmentCode;
                                     TempStyleDetails.BrandID = objStyleDetails.BrandID;
                                     TempStyleDetails.SubBrandID = objStyleDetails.SubBrandID;
                                     TempStyleDetails.CollectionID = objStyleDetails.CollectionID;
                                     TempStyleDetails.DivisionID = objStyleDetails.DivisionID;
                                     TempStyleDetails.ProductGroupID = objStyleDetails.ProductGroupID;
                                     TempStyleDetails.ProductSubGroupID = objStyleDetails.ProductSubGroupID;
                                     TempStyleDetails.SeasonID = objStyleDetails.SeasonID;
                                     TempStyleDetails.YearID = objStyleDetails.YearID;
                                     TempStyleDetails.ArmadaCollectionID = objStyleDetails.ArmadaCollectionID;
                                     TempStyleDetails.SizeName = objStyleDetails.SizeName;
                                     TempStyleDetails.ProductLineID = objStyleDetails.ProductLineID;
                                     TempStyleDetails.DesignerID = objStyleDetails.DesignerID;
                                     TempStyleDetails.PurchasePrice = objStyleDetails.PurchasePrice;
                                     TempStyleDetails.PurchaseCurrencyID = objStyleDetails.PurchaseCurrencyID;
                                     TempStyleDetails.RRPPrice = objStyleDetails.RRPPrice;
                                     TempStyleDetails.RRPCurrencyID = objStyleDetails.RRPCurrencyID;
                                     TempStyleDetails.SalesPriceListID = objStyleDetails.SalesPriceListID;
                                     TempStyleDetails.SalesPrice = objStyleDetails.SalesPrice;
                                     TempStyleDetails.ScaleID = objStyleDetails.ScaleID;
                                     TempStyleDetails.SizeID = objStyleDetails.SizeID;
                                     //TempStyleDetails.ItemImage = objStyleDetails.ItemImage;
                                     TempStyleDetails.ExchangeRate = objStyleDetails.ExchangeRate;
                                     TempStyleDetails.PurchasePriceListID = objStyleDetails.PurchasePriceListID;

                                     if (objScaleDetailMaster.Active == false || objColorMaster.Active == false)
                                     {
                                         TempStyleDetails.Active = false;
                                     }
                                     else
                                     {
                                         TempStyleDetails.Active = true;
                                     }                                     
                                     StyleDetailsList.Add(TempStyleDetails);
                                 }
                             }
                         }
                     }
                     _IStyleView.StyleDetailsList = StyleDetailsList;

                 }
                 else
                 {
                     _IStyleView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                 }
             }

             catch (Exception ex)
             {
                 throw ex;
             }
         }

         public void GetDropMasterLookUp()
         {
             try
             {
                 var RequestData = new SelectAllDropMasterRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _DropMasterBLL.SelectAllDropMaster(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.DropMasterLookUp = ResponseData.DropMasterTypesList;
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
                 var RequestData = new SelectDesignGradeLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _StyleMasterBLL.GradeLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.DesignGradeLookUp = ResponseData.DesignGradeList;
                 }
             }
             catch
             {

             }
         }

         public void GetDevelopmemtOffice()
         {
             try
             {
                 var RequestData = new SelectDesignDevelopmentOfficeLookUpRequest();
                 RequestData.ShowInActiveRecords = false;
                 var ResponseData = _StyleMasterBLL.DevelopmentOfficeLookUp(RequestData);
                 if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                 {
                     _IStyleView.DesignDevelopmentOfficeLookUp = ResponseData.DesignDevelopmentOfficeList;
                 }
             }
             catch
             {

             }
         }
    }

    public class StyleListPresenter
    {
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        IStyleCollectionView _IStyleCollectionView;
        public StyleListPresenter(IStyleCollectionView ViewObj)
        {
            _IStyleCollectionView = ViewObj;
        }
        public void GetStyleList()
        {
            try
            {
                var RequestData = new SelectAllStyleRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStyleResponse();
                ResponseData = _StyleMasterBLL.SelectAllStyleRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IStyleCollectionView.StyleList = ResponseData.StyleList;
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
