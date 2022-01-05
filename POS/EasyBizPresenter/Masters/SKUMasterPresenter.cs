using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ISKUMaster;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.ArmadaCollectionsMasterRequest;
using EasyBizRequest.Masters.BarcodeSettingsRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.ColorMasterRequest;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.ItemGroupMasterRequest;
using EasyBizRequest.Masters.ItemTypeMasterRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizRequest.Transactions.TransactionLogs;
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
    public class SKUMasterPresenter
    {
        ISKUMasterView _ISKUMasterView;
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        YearBLL _YearBLL = new YearBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        ColorBLL _ColorBLL = new ColorBLL();
        ScaleBLL _ScaleBLL = new ScaleBLL();
        DesignMasterBLL _DesignBLL = new DesignMasterBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        CollectionMasterBLL _CollectionMasterBLL = new CollectionMasterBLL();
        DivisionBLL _DvisionBLL = new DivisionBLL();
        ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();
        SubBrandBLL _SubBrandBLL = new SubBrandBLL();
        ProductSubGroupBLL _ProductSubGroupBLL = new ProductSubGroupBLL();
        SubCollectionBLL _SubCollectionBLL = new SubCollectionBLL();
        EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();
        SeasonBLL _SeasonBLL = new SeasonBLL();
        ProductLineMasterBLL _ProductLineBLL = new ProductLineMasterBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        StyleStatusBLL _StyleStatusBLL = new StyleStatusBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();
        ArmadaCollectionBLL _ArmadaCollectionBLL = new ArmadaCollectionBLL();
        AFSegamationMasterBLL _SegamationBLL = new AFSegamationMasterBLL();
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        BarcodeSettingsBLL _BarcodeSettingsBLL = new BarcodeSettingsBLL();




        public SKUMasterPresenter(ISKUMasterView ViewObj)
        {

            _ISKUMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ISKUMasterView.SKUMasterList.Count > 0)
            {
                _ISKUMasterView.Message = "Please Add SKU";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }




        public void SelectByStyleIDRecord()
        {
            try
            {
                var RequestData = new SelectByStyleIDRequest();
                RequestData.ID = _ISKUMasterView.StyleID;
                var ResponseData = _StyleMasterBLL.SelectStyleRecord(RequestData);
                _ISKUMasterView.DesignID = ResponseData.StyleRecord.DesignID;
                _ISKUMasterView.BrandID = ResponseData.StyleRecord.BrandID;                
                _ISKUMasterView.SubBrandID = ResponseData.StyleRecord.SubBrandID;
                _ISKUMasterView.StyleCode = ResponseData.StyleRecord.StyleCode;
                _ISKUMasterView.CollectionID = ResponseData.StyleRecord.CollectionID;
                _ISKUMasterView.ArmadaCollectionID = ResponseData.StyleRecord.ArmadaCollectionID;
                _ISKUMasterView.DivisionID = ResponseData.StyleRecord.DivisionID;
                _ISKUMasterView.ProductGroupID = ResponseData.StyleRecord.ProductGroupID;
                _ISKUMasterView.ProductSubGroupID = ResponseData.StyleRecord.ProductSubGroupID;
                _ISKUMasterView.SeasonID = ResponseData.StyleRecord.SeasonID;
                _ISKUMasterView.YearID = ResponseData.StyleRecord.YearCode;
                _ISKUMasterView.ProductLineID = ResponseData.StyleRecord.ProductLineID;
                _ISKUMasterView.StyleStatusID = ResponseData.StyleRecord.StyleStatusID;
                _ISKUMasterView.DesignerID = ResponseData.StyleRecord.DesignerID;
                _ISKUMasterView.PurchasePriceListID = ResponseData.StyleRecord.PurchasePriceListID;
                _ISKUMasterView.PurchasePrice = ResponseData.StyleRecord.PurchasePrice;
                _ISKUMasterView.PurchaseCurrencyID = ResponseData.StyleRecord.PurchaseCurrencyID;
                _ISKUMasterView.RRPPrice = ResponseData.StyleRecord.RRPPrice;
                _ISKUMasterView.RRPCurrencyID = ResponseData.StyleRecord.RRPCurrencyID;
                _ISKUMasterView.ScaleID = ResponseData.StyleRecord.ScaleID;
                _ISKUMasterView.SupplierBarcode = ResponseData.StyleRecord.SupplierBarcode;
                _ISKUMasterView.ArabicSKU = ResponseData.StyleRecord.ArabicSKU;   
                _ISKUMasterView.ExchangeRate = ResponseData.StyleRecord.ExchangeRate;
                _ISKUMasterView.SegamentationID = ResponseData.StyleRecord.StyleSegmentation;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ISKUMasterView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ISKUMasterView.Message = ResponseData.DisplayMessage;
                }

                _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;
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
                    _ISKUMasterView.ArmadaCollectionsLookUp = ResponseData.ArmadaCollectionsMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStockList()
        {
            try
            {
                var RequestData = new GetStockByStyleCodeRequest();
                var ResponseData = new GetStockByStyleCodeResponse();

                RequestData.StyleCode = _ISKUMasterView.StyleCode;
                ResponseData = _TransactionLogBLL.GetStockByStyleCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.StockList = ResponseData.StockList;
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
                    _ISKUMasterView.SegamationMasterLookUp = ResponseData.AFSegmentationMaster;
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
                    _ISKUMasterView.DesignLookUp = ResponseData.DesignMasterTypesList;
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
                    _ISKUMasterView.BrandMasterLookUp = ResponseData.BrandList;
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
                var RequestData = new SelectStyleLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StyleMasterBLL.SelectStyleLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.StyleMasterTypesLookUp = ResponseData.StyleMasterList;
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
                    _ISKUMasterView.ColorList = ResponseData.ColorList;
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
                RequestData.BrandID = _ISKUMasterView.BrandID;
                var ResponseData = _ScaleBLL.ScaleLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.ScaleMasterLookUp = ResponseData.ScaleList;
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
                RequestData.ShowInActiveRecords = false;
                RequestData.ID = _ISKUMasterView.ScaleID;
                var ResponseData = _ScaleBLL.ScaleDetailsLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.ScaleDetailList = ResponseData.ScaleDetailMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetStyleStatusMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStyleStatusLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StyleStatusBLL.SelectStyleStatusLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.StyleStatusMasterLookUp = ResponseData.StyleStatusMasterTypeList;
                }
            }
            catch
            {

            }

        }

        public void GetSubBrandLookUp()
        {
            try
            {
                var RequestData = new SelectSubBrandListForCategoryRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.BrandID = _ISKUMasterView.BrandID;
                var ResponseData = _SubBrandBLL.SubBrandByBrand(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.SubBrandMasterLookUp = ResponseData.SubBrandList;
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
                    _ISKUMasterView.CollectionLookUp = ResponseData.CollectionMasterTypesList;
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
                    _ISKUMasterView.DivisionLookUp = ResponseData.DivisionList;
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
                RequestData.ShowInActiveRecords = true;
                var ResponseData = _ProductGroupBLL.ProductGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.ProductGroupLookUp = ResponseData.ProductGroupList;
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
                RequestData.ProductGroupID = _ISKUMasterView.ProductGroupID;
                var ResponseData = _ProductSubGroupBLL.ProductSubGroupByProductGroup(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.ProductSubGroupLookUp = ResponseData.ProductSubGroupList;
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
                    _ISKUMasterView.YearLookUp = ResponseData.YearList;
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
                    _ISKUMasterView.ProductLineMasterLookUp = ResponseData.ProductLineMasterList;
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
                    _ISKUMasterView.SeasonLookUp = ResponseData.SeasonList;
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
                    _ISKUMasterView.DesignerLookUp = ResponseData.EmployeeList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PriceListMasterLookUp()
        {
            try
            {
                var RequestData = new SelectPriceListLookUPRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Type = "Purchase";
                var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.PriceListTypeLookUp = ResponseData.PriceListTypeData;
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
                    _ISKUMasterView.SalesCurrencyLookUp = ResponseData.CurrencyMasterList;
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
                RequestData.CurrencyType = "Purchase";
                var ResponseData = _CurrencyBLL.SelectCurrencyLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.PurchaseCurrencyLookUp = ResponseData.CurrencyMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveSKUMaster()
        {
            try
            {

                var RequestData = new SaveSKUMasterRequest();
                RequestData.ID = _ISKUMasterView.ID;
                RequestData.SKUMasterTypesRecord = _ISKUMasterView.SKUMasterList;                
                RequestData.ItemImageMasterList = _ISKUMasterView.ItemImageMasterList;               
                RequestData.BarCodeRunningNo = _ISKUMasterView.BarCodeRunningNo;
                RequestData.BarCodeID = _ISKUMasterView.BarCodeID;
                RequestData.SKUCode = _ISKUMasterView.SKUCode;
                RequestData.Active = _ISKUMasterView.Active;
                RequestData.BaseEntry = "SKUMaster";
                var ResponseData = _SKUMasterBLL.SaveSKUMaster(RequestData);
                _ISKUMasterView.Message = ResponseData.DisplayMessage;
                _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;

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
                _ISKUMasterView.SumOfPrefixSuffix = (ResponseData.BarcodeGenerateBySKURecord.Prefix + ResponseData.BarcodeGenerateBySKURecord.Suffix);
                _ISKUMasterView.BarCodeRunningNo = ResponseData.BarcodeGenerateBySKURecord.RunningNo;
                _ISKUMasterView.BarCodeID = ResponseData.BarcodeGenerateBySKURecord.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectSKUWithItemImageDetails()
        {
            SelectItemImageRequest RequestData = new SelectItemImageRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.ID = _ISKUMasterView.ID;
            RequestData.FormName = "SKU";
            SelectItemImageResponse ResponseData = _StyleMasterBLL.SelectStyleWithItemImage(RequestData);

            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ISKUMasterView.ItemImageMasterList = ResponseData.ItemImageMaster;


            }
            else
            {
                _ISKUMasterView.Message = ResponseData.DisplayMessage;
                _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void SelectStyleWithItemImageDetails()
        {
            try
            {
                SelectItemImageRequest RequestData = new SelectItemImageRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.ID = _ISKUMasterView.StyleID;
                RequestData.FormName = "Style";
                SelectItemImageResponse ResponseData = _StyleMasterBLL.SelectStyleWithItemImage(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterView.ItemImageMasterList = ResponseData.ItemImageMaster;


                }
                else
                {
                    _ISKUMasterView.Message = ResponseData.DisplayMessage;
                    _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch
            {

            }
        }
        public void UpdateSKUMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    //var RequestData = new UpdateSKUMasterRequest();
                    //RequestData.SKUMasterTypesRecord = new SKUMasterTypes();
                    //RequestData.SKUMasterTypesRecord.ID = _ISKUMasterView.ID;
                    //RequestData.SKUMasterTypesRecord.ItemNo = _ISKUMasterView.ItemNo;
                    //RequestData.SKUMasterTypesRecord.ItemDescription = _ISKUMasterView.ItemDescription;
                    //RequestData.SKUMasterTypesRecord.ForeignName = _ISKUMasterView.ForeignName;
                    //RequestData.SKUMasterTypesRecord.ItemTypeID = _ISKUMasterView.ItemTypeID;
                    //RequestData.SKUMasterTypesRecord.ItemGroupID = _ISKUMasterView.ItemGroupID;
                    //RequestData.SKUMasterTypesRecord.ModelNo = _ISKUMasterView.ModelNo;
                    //RequestData.SKUMasterTypesRecord.Barcode = _ISKUMasterView.Barcode;
                    //RequestData.SKUMasterTypesRecord.InventoryItem = _ISKUMasterView.InventoryItem;
                    //RequestData.SKUMasterTypesRecord.SalesItem = _ISKUMasterView.SalesItem;
                    //RequestData.SKUMasterTypesRecord.PurchaseItem = _ISKUMasterView.PurchaseItem;
                    //RequestData.SKUMasterTypesRecord.SCN = _ISKUMasterView.SCN;

                    //var ResponseData = _SKUMasterBLL.UpdateSKUMaster(RequestData);
                    //_ISKUMasterView.Message = ResponseData.DisplayMessage;
                    //_ISKUMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _ISKUMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteSKUMaster()
        {
            try
            {
                var RequestData = new DeleteSKUMasterRequest();

                RequestData.ID = _ISKUMasterView.ID;
                RequestData.BrandID = _ISKUMasterView.BrandID;

                var ResponseData = _SKUMasterBLL.DeleteSKUMaster(RequestData);
                _ISKUMasterView.Message = ResponseData.DisplayMessage;
                _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SelectByIDSKUMaster()
        {

            try
            {
                var RequestData = new SelectByIDSKUMasterRequest();
                RequestData.ID = _ISKUMasterView.ID;
                var ResponseData = _SKUMasterBLL.SelectByIdSKUMaster(RequestData);
                _ISKUMasterView.SKUCode = ResponseData.SKUMasterTypesData.SKUCode;
                _ISKUMasterView.SKUName = ResponseData.SKUMasterTypesData.SKUName;
                _ISKUMasterView.barcode = ResponseData.SKUMasterTypesData.BarCode;
                _ISKUMasterView.StyleID = ResponseData.SKUMasterTypesData.StyleID;
                _ISKUMasterView.StyleCode = ResponseData.SKUMasterTypesData.StyleCode;
                _ISKUMasterView.DesignID = ResponseData.SKUMasterTypesData.DesignID;
                _ISKUMasterView.BrandID = ResponseData.SKUMasterTypesData.BrandID;
                _ISKUMasterView.SubBrandID = ResponseData.SKUMasterTypesData.SubBrandID;
                _ISKUMasterView.CollectionID = ResponseData.SKUMasterTypesData.CollectionID;
                _ISKUMasterView.SegamentationID = ResponseData.SKUMasterTypesData.SegamentationID;
                _ISKUMasterView.DivisionID = ResponseData.SKUMasterTypesData.DivisionID;
                _ISKUMasterView.ProductGroupID = ResponseData.SKUMasterTypesData.ProductGroupID;

                _ISKUMasterView.ProductSubGroupID = ResponseData.SKUMasterTypesData.ProductSubGroupID;
                _ISKUMasterView.SeasonID = ResponseData.SKUMasterTypesData.SeasonID;
                _ISKUMasterView.YearID = ResponseData.SKUMasterTypesData.YearID;
                _ISKUMasterView.ProductLineID = ResponseData.SKUMasterTypesData.ProductLineID;

                _ISKUMasterView.DesignerID = ResponseData.SKUMasterTypesData.DesignerID;
                _ISKUMasterView.PurchasePriceListID = ResponseData.SKUMasterTypesData.PurchasePriceListID;
                _ISKUMasterView.PurchasePrice = ResponseData.SKUMasterTypesData.PurchasePrice;
                _ISKUMasterView.PurchaseCurrencyID = ResponseData.SKUMasterTypesData.PurchaseCurrencyID;
                _ISKUMasterView.RRPPrice = ResponseData.SKUMasterTypesData.RRPPrice;

                _ISKUMasterView.RRPCurrencyID = ResponseData.SKUMasterTypesData.RRPCurrencyID;
                _ISKUMasterView.ScaleID = ResponseData.SKUMasterTypesData.ScaleID;
                _ISKUMasterView.ColorID = ResponseData.SKUMasterTypesData.ColorID;
                _ISKUMasterView.RRPCurrencyID = ResponseData.SKUMasterTypesData.RRPCurrencyID;
                _ISKUMasterView.SizeID = ResponseData.SKUMasterTypesData.SizeID;
                _ISKUMasterView.Remarks = ResponseData.SKUMasterTypesData.Remarks;
                //_ISKUMasterView.ItemImage = ResponseData.SKUMasterTypesData.ItemImage;
                _ISKUMasterView.ExchangeRate = ResponseData.SKUMasterTypesData.ExchangeRate;
                _ISKUMasterView.Active = ResponseData.SKUMasterTypesData.Active;
                _ISKUMasterView.SupplierBarcode = ResponseData.SKUMasterTypesData.SupplierBarcode;
                _ISKUMasterView.ArabicSKU = ResponseData.SKUMasterTypesData.ArabicSKU;
                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ISKUMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ISKUMasterView.Message = ResponseData.DisplayMessage;
                }

                _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        //public void SelectSKUWithItemImageDetails()
        //{
        //    SelectItemImageRequest RequestData = new SelectItemImageRequest();
        //    RequestData.ShowInActiveRecords = true;
        //    RequestData.ID = _ISKUMasterView.ID;
        //    RequestData.FormName = "SKU";
        //    SelectItemImageResponse ResponseData = _StyleMasterBLL.SelectStyleWithItemImage(RequestData);

        //    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //    {
        //        _ISKUMasterView.ItemImageMasterList = ResponseData.ItemImageMaster;


        //    }
        //    else
        //    {
        //        _ISKUMasterView.Message = ResponseData.DisplayMessage;
        //        _ISKUMasterView.ProcessStatus = ResponseData.StatusCode;
        //    }
        //}
    }

    public class SKUMasterPresenterList
    {
        ISKUMasterViewList _ISKUMasterViewList;
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();


        public SKUMasterPresenterList(ISKUMasterViewList ViewObj)
        {

            _ISKUMasterViewList = ViewObj;
        }


        public void SelectAllSKUMaster()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();

                RequestData.ShowInActiveRecords = true;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISKUMasterViewList.SKUMasterList = ResponseData.SKUMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }        

        }
    }
}
