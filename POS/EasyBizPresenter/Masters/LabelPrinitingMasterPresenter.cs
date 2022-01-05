using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizIView.ILabelPrintingMaster.Masters;
using EasyBizIView.Masters;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.LabelPrintingRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.LabelPrintingResponse;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class LabelPrinitingMasterPresenter
    {
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        TransactionLogBLL _TransactionLogBLL = new TransactionLogBLL();
        IlabelprinitngView _IlabelprinitngView;
        public LabelPrinitingMasterPresenter(IlabelprinitngView ViewObj)
        {
            _IlabelprinitngView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IlabelprinitngView.Department.Trim() == string.Empty)
            {
                _IlabelprinitngView.Message = "Department is missing.Please Enter it.";
            }
            else if (_IlabelprinitngView.ProductCode.Trim() == string.Empty)
            {
                _IlabelprinitngView.Message = "Product is missing.Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void getColorCode()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SelectColorCodeRequest();
                    RequestData.ShowInActiveRecords = true;
                    var ResponseData = new SelectColorCodeResponse();
                    RequestData.Productcode = _IlabelprinitngView.ProductCode;
                    RequestData.Department = _IlabelprinitngView.Department;
                    ResponseData = _SKUMasterBLL.SelectColorCodeSKUMaster(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _IlabelprinitngView.SKUMasterColorList = ResponseData.SKUMasterTypesList;
                    }
                    else
                    {
                        _IlabelprinitngView.Message = ResponseData.DisplayMessage;
                    }
                }
                else
                {
                    _IlabelprinitngView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void getSizeCode()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SelectSizeCodeRequest();
                    RequestData.ShowInActiveRecords = true;
                    var ResponseData = new SelectSizeCodeResponse();
                    RequestData.Productcode = _IlabelprinitngView.ProductCode;
                    RequestData.Department = _IlabelprinitngView.Department;
                    //RequestData.ColorCode = _IlabelprinitngView.ColorCode;
                    ResponseData = _SKUMasterBLL.SelectSizeCodeSKUMaster(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _IlabelprinitngView.SKUMasterSizeList = ResponseData.SKUMasterTypesList;
                    }
                    else
                    {
                        _IlabelprinitngView.Message = ResponseData.DisplayMessage;
                    }
                }
                else
                {
                    _IlabelprinitngView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStoreListByID()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectByIDStoreMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectByIDStoreMasterResponse();
                RequestData.ID = _IlabelprinitngView.SelectedStoreId;
                ResponseData = _StoreMasterBLL.SelectedStoreId(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.StoreMasterRecord = ResponseData.StoreMasterData;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetQuantityBySKU()
        {
            try
            {
                //if (IsValidForm())
                //{

                    var RequestData = new GetQuantityBySKURequest();
                    var ResponseData = new GetQuantityBySKUResponse();

                    RequestData.Department = _IlabelprinitngView.Department;
                    RequestData.Productcode = _IlabelprinitngView.ProductCode;
                    
                    RequestData.ColorCode = _IlabelprinitngView.ColorCode;
                    //RequestData.SizeCode = _IlabelprinitngView.SizeCode;
                    RequestData.StoreID = _IlabelprinitngView.SelectedStoreId;
                    //RequestData.StoreCode = _IlabelprinitngView.SelectedStoreId;
                    ResponseData = _TransactionLogBLL.GetQuantityBySKU(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _IlabelprinitngView.QuantityBySKUList = ResponseData.QuantityBySKUList;
                    }
                    else
                    {
                        _IlabelprinitngView.QuantityBySKUList = null;
                    }
                //}
                //else
                //{
                //    _IlabelprinitngView.ProcessStatus = Enums.OpStatusCode.GeneralError;

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GetLabelPrintingReportData()
        //{
        //    try
        //    {
        //        var _LabelPrintingBLL = new LabelPrintingBLL();
        //        var RequestData = new CommonLabelReportRequest();              
        //        var ResponseData = new CommonLabelPrintingReportResponse();
        //        RequestData.StoreID = _IlabelprinitngView.SelectedStoreId;
        //        RequestData.Department = _IlabelprinitngView.Department;
        //        RequestData.ProductCode = _IlabelprinitngView.ProductCode;
        //        RequestData.ColorCode = _IlabelprinitngView.ColorCode;
        //        RequestData.SizeCode = _IlabelprinitngView.SizeCode;
        //        RequestData.NoOfLabel = _IlabelprinitngView.NoOfLabel;
        //        RequestData.PrintPrice = _IlabelprinitngView.PrintPrice;

        //        ResponseData = _LabelPrintingBLL.GetLabelPrintingReportData(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IlabelprinitngView.LabelPrintingReportTable = ResponseData.LabelPrintingDataTable;
        //        }
        //        else
        //        {
        //            _IlabelprinitngView.Message = ResponseData.DisplayMessage;
        //            _IlabelprinitngView.LabelPrintingReportTable = new System.Data.DataTable();
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void GetStylePricingBySKUCode()
        {
            try
            {
                var RequestData = new GetStylePricingBySKUCodeRequest();
                // RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.Department = _IlabelprinitngView.Department;
                RequestData.ProductCode = _IlabelprinitngView.ProductCode;
                RequestData.ColorCode = _IlabelprinitngView.ColorCode;
                RequestData.SizeCode = _IlabelprinitngView.SizeCode;
                RequestData.PriceListID = _IlabelprinitngView.PriceListID;                

                var ResponseData = new GetStylePricingBySKUCodeResponse();
                ResponseData = _SKUMasterBLL.GetPriceBySKUCode(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.PriceRecord = ResponseData.PriceRecord;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GetPriceListByPriceListIds()
        //{
        //    try
        //    {
        //        var RequestData = new SelectByIDsPriceListRequest();
        //        RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

        //        RequestData.PriceListIDS = _IlabelprinitngView.PriceListIDs;
        //        var ResponseData = _PriceListBLL.SelectByIDsPriceList(RequestData);
        //        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //        {
        //            _IlabelprinitngView.PriceListType = ResponseData.PriceList;
        //        }
        //        else
        //        {
        //            _IlabelprinitngView.PriceListType = new List<PriceListType>();
        //            _IlabelprinitngView.Message = ResponseData.DisplayMessage;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void GetCurrencyByStore()
        {
            try
            {
                var _CountryBLL = new CountryBLL();
                var RequestData = new GetCurrencyByStoreRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new GetCurrencyByStoreResponse();
                RequestData.ID = _IlabelprinitngView.SelectedStoreId;
                ResponseData = _CountryBLL.GetCurencyByStore(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.CurrencyRecord = ResponseData.CurrencyByStore;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetSKUBarCode()
        {
            try
            {
                var RequestData = new GetBarCodeBySKURequest();
                // RequestData.RequestFrom = Enums.RequestFrom.StoreSales;

                RequestData.Department = _IlabelprinitngView.Department;
                RequestData.ProductCode = _IlabelprinitngView.ProductCode;
                RequestData.ColorCode = _IlabelprinitngView.ColorCode;
                RequestData.SizeCode = _IlabelprinitngView.SizeCode;
                RequestData.ChkSupplierBarcode = _IlabelprinitngView.ChkSupplierBarcode;
                

                var ResponseData = new GetBarCodeBySKUResponse();
                ResponseData = _SKUMasterBLL.GetBarCodeBySKU(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.BarCodeRecord = ResponseData.BarCodeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStyleName()
        {
            try
            {
                var RequestData = new GetStyleNameRequest();
                RequestData.Department = _IlabelprinitngView.Department;
                RequestData.ProductCode = _IlabelprinitngView.ProductCode;

                var ResponseData = new GetStyleNameResponse();
                ResponseData = _StyleMasterBLL.GetStyleName(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.StyleRecord = ResponseData.StyleData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetStoreList()
        {
            try
            {
                var _StoreMasterBLL = new StoreMasterBLL();
                var RequestData = new SelectAllStoreMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllStoreMasterResponse();
                ResponseData = _StoreMasterBLL.SelectAllStoreMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.StoreList = ResponseData.StoreMasterList;
                }
                else
                {
                    _IlabelprinitngView.StoreList = new List<StoreMaster>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectPriceListLookUP()
        {
            try
            {
                var RequestData = new SelectPriceListLookUPRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Type = "Type";
                var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IlabelprinitngView.PriceListTypeLookUP = ResponseData.PriceListTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
