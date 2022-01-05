using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPriceListMaster;
using EasyBizRequest.Masters.CurrencyRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizResponse.Masters.PriceListResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class PriceListMasterPresenter
    {

        IPriceListView _IPriceListView;
        PriceListBLL _PriceListBLL = new PriceListBLL();
        CurrencyBLL _CurrencyBLL = new CurrencyBLL();

        public PriceListMasterPresenter(IPriceListView ViewObj)
        {
            _IPriceListView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IPriceListView.PriceListCode.Trim() == string.Empty)
            {
                _IPriceListView.Message = "Price List Code is missing Please Enter it.";
            }
            else if (_IPriceListView.PriceListName.Trim() == string.Empty)
            {
                _IPriceListView.Message = "Please Enter Price List Name";
            }
            else if (_IPriceListView.PriceListCurrencyType==0)
            {
                _IPriceListView.Message = "Please Select Currency";
            }
            else if (_IPriceListView.PriceListType == string.Empty)
            {
                _IPriceListView.Message = "Please Select Price List Type";
            }   

            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SavePriceListMaster()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new SavePriceListRequest();
                    RequestData.PriceListTypeRecords = new PriceListType();

                    RequestData.PriceListTypeRecords.PriceListCode = _IPriceListView.PriceListCode;
                    RequestData.PriceListTypeRecords.PriceListName = _IPriceListView.PriceListName;
                    RequestData.PriceListTypeRecords.PriceListCurrencyType = _IPriceListView.PriceListCurrencyType;
                    RequestData.PriceListTypeRecords.BasePriceListID = _IPriceListView.BasePriceListID;
                    RequestData.PriceListTypeRecords.ConversionFactore = _IPriceListView.ConversionFactore;
                    RequestData.PriceListTypeRecords.CreateBy = 1;
                    RequestData.PriceListTypeRecords.Remarks = _IPriceListView.Remarks;
                    RequestData.PriceListTypeRecords.Active = _IPriceListView.Active;
                    RequestData.PriceListTypeRecords.PriceCategory = _IPriceListView.PriceListType;
                    RequestData.PriceListTypeRecords.PriceType = _IPriceListView.PriceCategory;
                    var ResponseData = _PriceListBLL.SavePriceList(RequestData);

                    _IPriceListView.Message = ResponseData.DisplayMessage;
                    _IPriceListView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPriceListView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePriceListMaster()
        {
            try
            {
                  if (IsValidForm())
                {

                var RequestData = new UpdatePriceListRequest();
                RequestData.PriceListTypeRecords = new PriceListType();

                RequestData.PriceListTypeRecords.ID = _IPriceListView.ID;
                RequestData.PriceListTypeRecords.PriceListCode = _IPriceListView.PriceListCode;
                RequestData.PriceListTypeRecords.PriceListName = _IPriceListView.PriceListName;
                RequestData.PriceListTypeRecords.PriceListCurrencyType = _IPriceListView.PriceListCurrencyType;
                RequestData.PriceListTypeRecords.BasePriceListID = _IPriceListView.BasePriceListID;
                RequestData.PriceListTypeRecords.ConversionFactore = _IPriceListView.ConversionFactore;
                RequestData.PriceListTypeRecords.CreateBy = 1;
                RequestData.PriceListTypeRecords.Remarks = _IPriceListView.Remarks;
                RequestData.PriceListTypeRecords.Active = _IPriceListView.Active;
                RequestData.PriceListTypeRecords.PriceCategory = _IPriceListView.PriceListType;
                RequestData.PriceListTypeRecords.PriceType = _IPriceListView.PriceCategory;
                RequestData.PriceListTypeRecords.SCN = _IPriceListView.SCN;

                var ResponseData = _PriceListBLL.UpdatePriceList(RequestData);

                _IPriceListView.Message = ResponseData.DisplayMessage;
                _IPriceListView.ProcessStatus = ResponseData.StatusCode;
                }
                  else
                  {
                      _IPriceListView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                  }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePriceListMaster()
        {
            try
            {

                var RequestData = new DeletePriceListRequest();   
                RequestData.ID = _IPriceListView.ID;
                var ResponseData = _PriceListBLL.DeletePriceList(RequestData);

                _IPriceListView.Message = ResponseData.DisplayMessage;
                _IPriceListView.ProcessStatus = ResponseData.StatusCode;


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
                    _IPriceListView.SalesCurrencyLookUp = ResponseData.CurrencyMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetPriceListLookUp()
        {
            try
            {
                var RequestData = new SelectPriceListLookUPRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.Type = "Type";
                var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPriceListView.PriceListTypeLookUp = ResponseData.PriceListTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectPriceListRecord()
        {


            var RequestData = new SelectByIDPriceListRequest();
            RequestData.ID = _IPriceListView.ID;

           
            var ResponseData = _PriceListBLL.SelectByIDPriceList(RequestData);
            _IPriceListView.PriceListCode = ResponseData.PriceListTypeRecord.PriceListCode;
            _IPriceListView.PriceListName = ResponseData.PriceListTypeRecord.PriceListName;           
           _IPriceListView.PriceListCurrencyType= ResponseData.PriceListTypeRecord.PriceListCurrencyType;
           _IPriceListView.BasePriceListID = ResponseData.PriceListTypeRecord.BasePriceListID;
            _IPriceListView.ConversionFactore = ResponseData.PriceListTypeRecord.ConversionFactore;
            _IPriceListView.Remarks = ResponseData.PriceListTypeRecord.Remarks;
            _IPriceListView.Active = ResponseData.PriceListTypeRecord.Active;
            _IPriceListView.PriceListType = ResponseData.PriceListTypeRecord.PriceCategory;
            _IPriceListView.PriceCategory = ResponseData.PriceListTypeRecord.PriceType;     
            _IPriceListView.SCN = ResponseData.PriceListTypeRecord.SCN;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IPriceListView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IPriceListView.Message = ResponseData.DisplayMessage;
            }

            _IPriceListView.ProcessStatus = ResponseData.StatusCode;
        }
    }

    public class PriceListMasterPresenterView
    {

        PriceListBLL _PriceListBLL = new PriceListBLL();

        IPriceListCollectionView _IPriceListCollectionView;

        public PriceListMasterPresenterView(IPriceListCollectionView ViewObj)
        {
            _IPriceListCollectionView = ViewObj;
        }

        public void GetAllPriceList()
        {

            var RequestData = new SelectAllPriceListRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllPriceListResponse();
            ResponseData = _PriceListBLL.SelectAllPriceList(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPriceListCollectionView.PriceListTypeList = ResponseData.PriceListTypeList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }
}
