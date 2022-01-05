using CommonRoutines;
using EasyBizAbsDAL.Masters;
using EasyBizBLL.Common;
using EasyBizBLL.Transactions.Pricing;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizFactory;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse.Masters.PriceListResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.Pricing.PricePointResponse;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
    public class SKUMasterBLL
    {        
        public SaveSKUMasterResponse SaveSKUMaster(SaveSKUMasterRequest objRequest)
        {
            SaveSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
                //objRequest.SyncMode = Enums.SyncMode.EnterpriseToAllStores;

                if (objRequest.RequestDynamicData != null)
                {
                    var objSKUMasterTypes = new List<SKUMasterTypes>();
                    objSKUMasterTypes = (List<SKUMasterTypes>)objRequest.RequestDynamicData;

                    objRequest.ID = objSKUMasterTypes.FirstOrDefault().ID;
                    objRequest.SKUMasterTypesRecord = objSKUMasterTypes.FirstOrDefault().SKUMasterTypesRecord;
                    objRequest.SKUCode = objSKUMasterTypes.FirstOrDefault().SKUCode;
                    objRequest.PriceListID = objSKUMasterTypes.FirstOrDefault().PriceListID;
                    objRequest.SalePriceListID = objSKUMasterTypes.FirstOrDefault().SalePriceListID;
                    objRequest.BaseEntry = objSKUMasterTypes.FirstOrDefault().BaseEntry;
                    objRequest.StylePricingList = objSKUMasterTypes.FirstOrDefault().StylePricingList;
                    objRequest.ItemImageMasterList = objSKUMasterTypes.FirstOrDefault().ItemImageMasterList;
                    objRequest.Active = objSKUMasterTypes.FirstOrDefault().Active;
                    objRequest.BarCodeRunningNo = objSKUMasterTypes.FirstOrDefault().BarCodeRunningNo;
                    objRequest.BarCodeID = objSKUMasterTypes.FirstOrDefault().BarCodeID;
                    //objRequest.SkuMasterData = objSKUMasterTypes;
                    objRequest.ImportExcelList = objSKUMasterTypes.FirstOrDefault().ImportExcelList;
                    
                    objRequest.SKUMasterTypesRecord = objSKUMasterTypes;
                }

                if (objRequest.SKUMasterTypesRecord != null && objRequest.SKUMasterTypesRecord.Count > 0)
                {
                    objRequest.BaseIntegrateStoreID = objRequest.SKUMasterTypesRecord.FirstOrDefault().BrandID;
                }
                else if (objRequest.SkuMasterData != null)
                {
                    objRequest.BaseIntegrateStoreID = objRequest.SkuMasterData.BrandID;
                }

                objRequest.StylePricingList = new List<StylePricing>();
                //if (objRequest.BaseEntry == "StyleMaster")
                //{                    
                objRequest.StylePricingList = GetSKUStylePricing(objRequest.SKUMasterTypesRecord, objRequest.StylePricingList);
                //}
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                
                objResponse = (SaveSKUMasterResponse)objSKUMasterDAL.InsertRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    ////objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objResponse.ReturnIDs);
                //    objRequest.DocumentType = Enums.DocumentType.SKUMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                //   // BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SKUMasterBLL", "SaveSKUMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new SaveSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectAllSKUMasterResponse GetSKUSearchForSales(SelectAllSKUMasterRequest objRequest)
        {
            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.GetSKUSearchForSales(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSKUMasterResponse GetSKUDetails(SelectAllSKUMasterRequest objRequest)
        {
            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.GetSKUDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSKUMasterResponse GetSKUDetailsByBin(SelectAllSKUMasterRequest objRequest)
        {
            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.GetSKUDetailsByBin(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectSKUOnCountryResponse SelectSKUOnCountry(SelectSKUOnCountryRequest request)
        {
            SelectSKUOnCountryResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectSKUOnCountryResponse)objSKUMasterDAL.SelectSKUOnCountry(request);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSKUOnCountryResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllSKUMasterResponse GetSKUWithoutStoreID(SelectAllSKUMasterRequest objRequest)
        {
            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.GetSKUWithoutStoreID(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public UpdateSKUMasterResponse UpdateSKUMaster(UpdateSKUMasterRequest objRequest)
        {

            UpdateSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (UpdateSKUMasterResponse)objSKUMasterDAL.UpdateRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //    objRequest.DocumentIDs = Convert.ToString(objRequest.SKUMasterTypesRecord.ID);
                //    objRequest.DocumentType = Enums.DocumentType.SKUMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;


             //   BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SKUMasterBLL", "UpdateSKUMaster");
               
            }
            catch (Exception ex)
            {
                objResponse = new UpdateSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
        public DeleteSKUMasterResponse DeleteSKUMaster(DeleteSKUMasterRequest objRequest)
        {

            DeleteSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;
                objRequest.BrandID = objRequest.BrandID;

                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (DeleteSKUMasterResponse)objSKUMasterDAL.DeleteRecord(objRequest);
                //if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                //{
                //    objRequest.RequestFrom = objRequest.RequestFrom;
                //  //  objRequest.DocumentIDs = Convert.ToString(objRequest.SKUMasterTypesRecord.ID);
                //    objRequest.DocumentType = Enums.DocumentType.SKUMASTER;
                //    objRequest.ProcessMode = Enums.ProcessMode.Delete;
                   
                //    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SKUMasterBLL", "DeleteSKUMaster");
                //}
            }
            catch (Exception ex)
            {
                objResponse = new DeleteSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }


        public SelectAllSKUMasterResponse SelectAllSKUMaster(SelectAllSKUMasterRequest objRequest)
        {

            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectAllSKUMasterResponse API_SelectAllSKUMaster(SelectAllSKUMasterRequest objRequest)
        {

            SelectAllSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUMasterResponse)objSKUMasterDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectColorCodeResponse SelectColorCodeSKUMaster(SelectColorCodeRequest objRequest)
        {

            SelectColorCodeResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectColorCodeResponse)objSKUMasterDAL.SelectColorCodeSKUCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectColorCodeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectSizeCodeResponse SelectSizeCodeSKUMaster(SelectSizeCodeRequest objRequest)
        {

            SelectSizeCodeResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectSizeCodeResponse)objSKUMasterDAL.SelectZizeCodeSKUCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSizeCodeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
        public SelectAllSKUImagesResponse SelectAllSKUImages(SelectByALLSKUImagesRequest objRequest)
        {

            SelectAllSKUImagesResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectAllSKUImagesResponse)objSKUMasterDAL.SelectAllSKUImages(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllSKUImagesResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }

        public SelectByIDSKUMasterResponse SelectByIdSKUMaster(SelectByIDSKUMasterRequest objRequest)
        {

            SelectByIDSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();
            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                if(objRequest.ID == null || objRequest.ID == 0)
                {
                    
                    if(!string.IsNullOrEmpty(objRequest.DocumentIDs))
                    {
                        int Doc_Id;
                        int.TryParse(objRequest.DocumentIDs, out Doc_Id);
                        objRequest.ID = Doc_Id;
                    }                    
                }
                objResponse = (SelectByIDSKUMasterResponse)objSKUMasterDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByIDSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
        public List<StylePricing> GetSKUStylePricing(List<SKUMasterTypes> SKUMasterList,List<StylePricing> StylePriceList)
        {
            var StylePricingList = new List<StylePricing>();
            var _PricePointBLL = new PricePointBLL();
            var _PriceListBLL = new PriceListBLL();
            var PricePointList = new List<PricePoint>();
            var PriceTypeList = new List<PriceListType>();
            var objSelectAllPricePointRequest = new SelectAllPricePointRequest();
            var objSelectAllPricePointResponse = new SelectAllPricePointResponse();
            var objSelectAllPriceListRequest = new SelectAllPriceListRequest();
            var objSelectAllPriceListResponse = new SelectAllPriceListResponse();
           objSelectAllPricePointResponse = _PricePointBLL.GetPricePointList(objSelectAllPricePointRequest);
            if (objSelectAllPricePointResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
            {
                PricePointList = objSelectAllPricePointResponse.PricePointList;
            }
            objSelectAllPriceListResponse = _PriceListBLL.SelectAllPriceList(objSelectAllPriceListRequest);

            if (objSelectAllPriceListResponse.StatusCode == EasyBizDBTypes.Common.Enums.OpStatusCode.Success)
            {
                PriceTypeList = objSelectAllPriceListResponse.PriceListTypeList;
            }
            foreach (SKUMasterTypes objSKUMasterTypes in SKUMasterList)
            {
                var objStylePricing = new StylePricing();
                objStylePricing.ID = 0;
                objStylePricing.SKUID = objSKUMasterTypes.ID;
                objStylePricing.SKUCode = objSKUMasterTypes.SKUCode;

                PricePoint PricePointData = new PricePoint();
                PricePointData = PricePointList.Where(x => x.BrandID == objSKUMasterTypes.BrandID && x.Active == true).FirstOrDefault();

                foreach (PriceListType objPriceListType in PriceTypeList)
                {
                    objStylePricing.PriceListID = objPriceListType.ID;
                    objStylePricing.PriceListCurrency = objPriceListType.PriceListCurrencyType;
                    Decimal Cfactor = objPriceListType.ConversionFactore;

                    PriceListType BasePrice = new PriceListType();
                    BasePrice = PriceTypeList.Where(x => x.ID == objPriceListType.BasePriceListID).FirstOrDefault();
                    if (PricePointData != null && PricePointData.PricePointRangeList != null)
                    {
                        //Decimal SKUPrice = (objSKUMasterTypes.RRPPrice * objSKUMasterTypes.ExchangeRate);
                        Decimal SKUPrice = (objSKUMasterTypes.RRPPrice * 1);
                        PricePointRange DefaultRangeList = new PricePointRange();

                        //DefaultRangeList = PricePointData.PricePointRangeList.Where(x => x.RangeFrom <= SKUPrice && x.RangeTo >= SKUPrice && x.CurrencyID == objPriceListType.PriceListCurrencyType).ToList();
                        DefaultRangeList = PricePointData.PricePointRangeList.Where(x => x.RangeFrom <= SKUPrice && x.RangeTo >= SKUPrice && x.CurrencyID == BasePrice.PriceListCurrencyType).FirstOrDefault(); // By default get the base price list for calc
                        if (objSKUMasterTypes.SalesPrice == Convert.ToDecimal(0))
                        {
                            if (DefaultRangeList != null)
                            {
                                objStylePricing.Price = Math.Round((DefaultRangeList.Price * Cfactor), MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                objStylePricing.Price = (Decimal)SKUPrice;
                            }
                        }
                        else
                        {
                            if (objSKUMasterTypes.SalesPriceListID == objPriceListType.ID)
                            {
                                objStylePricing.Price = Convert.ToDecimal(objSKUMasterTypes.SalesPrice);
                            }
                            else
                            {
                                if (DefaultRangeList != null)
                                {
                                    objStylePricing.Price = Math.Round((DefaultRangeList.Price * Cfactor), MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    objStylePricing.Price = Convert.ToDecimal(SKUPrice);
                                }
                            }
                        }
                    }
                    objStylePricing.IsManualEntry = false;
                    objStylePricing.Active = true;
                    objStylePricing.AppVersion = string.Empty;
                    objStylePricing.CreateBy = objSKUMasterTypes.CreateBy;
                    objStylePricing.UpdateBy = objSKUMasterTypes.CreateBy;

                    StylePricingList.Add(DeepCopyCreator.StylePricingDeepCopy(objStylePricing));
                }
            }
            return StylePricingList;
        }


        public GetStylePricingBySKUCodeResponse SelectGetStylePricingBySKUCode(GetStylePricingBySKUCodeRequest objRequest)
        {

            GetStylePricingBySKUCodeResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (GetStylePricingBySKUCodeResponse)objSKUMasterDAL.SelectGetStylePricingBySKUCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStylePricingBySKUCodeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
        public GetStylePricingBySKUCodeResponse SelectCurrencyStylePricingBySKUCode(GetStylePricingBySKUCodeRequest objRequest)
        {

            GetStylePricingBySKUCodeResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (GetStylePricingBySKUCodeResponse)objSKUMasterDAL.SelectCurrencyStylePricingBySKUCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStylePricingBySKUCodeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }
        public SelectSKUByStyleIDResponse SelectByStyleID(SelectSKUByStyleIDRequest objRequest)
        {

            SelectSKUByStyleIDResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectSKUByStyleIDResponse)objSKUMasterDAL.SelectByStyleID (objRequest);


            }
            catch (Exception ex)
            {
                objResponse = new SelectSKUByStyleIDResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;

        }       

        public SaveSKUMasterResponse UpdateImportBarCode(SaveSKUMasterRequest objRequest)
        {

            SaveSKUMasterResponse objResponse = null;
            var ObjFactory = new DALFactory();
            try
            {
               // objRequest.SyncMode = Enums.SyncMode.EnterpriseToSpecificStores;
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToBrandWiseStores;

                if (objRequest.ImportExcelList != null && objRequest.ImportExcelList.Count > 0)
                {
                    objRequest.BaseIntegrateStoreID = objRequest.ImportExcelList.FirstOrDefault().BrandID;
                }
               
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SaveSKUMasterResponse)objSKUMasterDAL.UpdateImportBarCode(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.DocumentIDs = Convert.ToString(objResponse.ReturnIDs);
                    objRequest.DocumentType = Enums.DocumentType.SKUMASTER;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;
                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Masters.SKUMasterBLL", "UpdateImportBarCode");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveSKUMasterResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }


        public GetStylePricingBySKUCodeResponse GetPriceBySKUCode(GetStylePricingBySKUCodeRequest objRequest)
        {
            GetStylePricingBySKUCodeResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (GetStylePricingBySKUCodeResponse)objSKUMasterDAL.GetPriceBySKUCode(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetStylePricingBySKUCodeResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public GetBarCodeBySKUResponse GetBarCodeBySKU(GetBarCodeBySKURequest objRequest)
        {
            GetBarCodeBySKUResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (GetBarCodeBySKUResponse)objSKUMasterDAL.GetBarCodeBySKU(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new GetBarCodeBySKUResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectSKUDetailsResponse SelectSKUDetails(SelectAllSKUMasterRequest objRequest)
        {
            SelectSKUDetailsResponse objResponse = null;
            var ObjFactory = new DALFactory();

            try
            {
                BaseSKUMasterDAL objSKUMasterDAL = ObjFactory.GetDALRepository().GetSKUMasterDAL();
                objResponse = (SelectSKUDetailsResponse)objSKUMasterDAL.SelectSKUDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectSKUDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
