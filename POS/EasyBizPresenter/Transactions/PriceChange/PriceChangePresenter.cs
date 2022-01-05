using EasyBizIView.Transactions.IPriceChange;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizBLL.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizDBTypes.Common;
using EasyBizRequest.Transactions.PriceChange;
using EasyBizResponse.Transactions.PriceChange;
using EasyBizBLL.Transactions.PriceChange;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizBLL.Common;
using EasyBizDBTypes.Transactions.PriceChange;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using System.Data;

namespace EasyBizPresenter.Transactions.PriceChange
{
	public class PriceChangePresenter
	{
		IPriceChangeView _IPriceChangeView;
		PriceChangeBLL _PriceChangeBLL = new PriceChangeBLL();

		CountryBLL _CountryBLL = new CountryBLL();
		DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();

		int _RunningNo;
		int _DetailID;

		public PriceChangePresenter(IPriceChangeView ViewObj)
		{
			_IPriceChangeView = ViewObj;
		}
		public void GetCountryMasterLookUP()
		{
			SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
			RequestData.ShowInActiveRecords = false;
			//RequestData.CountryID = _ITailoringOrderView.CountryID;
			SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
			if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
			{
				_IPriceChangeView.CountryMasterLookup = ResponseData.CountryMasterList;
			}
		}
		public void ValidatePriceChangeDetails()
		{
			ValidatePriceChangeRequest RequestData = new ValidatePriceChangeRequest();
			RequestData.ValidatingPriceChangeDetailsList = _IPriceChangeView.ValidatingPriceChangeDetailsList;
			RequestData.SourceCountryID = _IPriceChangeView.SourceCountryID;
			RequestData.PriceChangeType = _IPriceChangeView.PriceChangeType;
			ValidatePriceChangeResponse ResponseData = _PriceChangeBLL.ValidatePriceChangeDetails(RequestData);
			if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
			{
				_IPriceChangeView.ValidatingPriceChangeDetailsList = ResponseData.ValidatingPriceChangeDetailsList;
			}
            else
            {
                _IPriceChangeView.Message = ResponseData.DisplayMessage;
            }
		}
		public void SelectDocumentNumberingRecord()
		{
			try
			{
				var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
				RequestData.RequestFrom = Enums.RequestFrom.MainServer;
				RequestData.DocumentTypeID = (int)Enums.DocumentType.PRICECHANGE;
				//RequestData.StoreID = _ITailoringOrderView.StoreID;
				//RequestData.StoreCode = _ITailoringOrderView.StoreCode;
				var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

				if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
				{
					string DocumentNo = string.Empty;
					DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

					_IPriceChangeView.DocumentNo = DocumentNo;

					_RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
					_DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void SavePriceChange()
		{
			try
			{
				if (IsValidForm())
				{
					var RequestData = new SavePriceChangeRequest();
					RequestData.PriceChangeRecord = new EasyBizDBTypes.Transactions.PriceChange.PriceChange();
					RequestData.PriceChangeRecord = new EasyBizDBTypes.Transactions.PriceChange.PriceChange(); // _IStockRequestView.StockRequestDetailsList;                   
					RequestData.PriceChangeRecord.ID = _IPriceChangeView.ID;
					RequestData.PriceChangeRecord.DocumentNo = _IPriceChangeView.DocumentNo;
					RequestData.PriceChangeRecord.DocumentDate = _IPriceChangeView.DocumentDate;
					RequestData.PriceChangeRecord.PriceChangeDate = _IPriceChangeView.PriceChangeDate;
					RequestData.PriceChangeRecord.PriceChangeType = _IPriceChangeView.PriceChangeType;
					RequestData.PriceChangeRecord.MultipleCountry = _IPriceChangeView.MultipleCountry;

					RequestData.PriceChangeRecord.SourceCountryID = _IPriceChangeView.SourceCountryID;
					RequestData.PriceChangeRecord.SourceCountryCode = _IPriceChangeView.SourceCountryCode;
					RequestData.PriceChangeRecord.Status = _IPriceChangeView.Status;
					RequestData.PriceChangeRecord.Remarks = _IPriceChangeView.Remarks;

					RequestData.PriceChangeRecord.CreateBy = _IPriceChangeView.UserID;
					RequestData.PriceChangeRecord.CreateOn = DateTime.Now;
					RequestData.PriceChangeRecord.Active = true;
					RequestData.PriceChangeRecord.SCN = _IPriceChangeView.SCN;

					RequestData.PriceChangeDetailsList = _IPriceChangeView.ValidatingPriceChangeDetailsList;
					RequestData.PriceChangeCountriesList = _IPriceChangeView.SelectedCountries;

					var ResponseData = _PriceChangeBLL.SavePriceChange(RequestData);
					_IPriceChangeView.Message = ResponseData.DisplayMessage;
					_IPriceChangeView.ProcessStatus = ResponseData.StatusCode;

					if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
					{
						UpdateRunningNo();
					}
				}
				else
				{
					_IPriceChangeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
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
			if (_IPriceChangeView.DocumentNo == "")
			{
				_IPriceChangeView.Message = "Document No is missing.Please Make a entry in Document Numbering Screen.";
			}
			else if (_IPriceChangeView.DocumentDate == null)
			{
				_IPriceChangeView.Message = "Document Date is missing Please Enter it.";
			}
			else if (_IPriceChangeView.PriceChangeDate == null)
			{
				_IPriceChangeView.Message = "Price Change Date is missing Please Enter it.";
			}
			else if (_IPriceChangeView.PriceChangeType == "")
			{
				_IPriceChangeView.Message = "Price Change Type is missing Please Select it.";
			}
			else if (_IPriceChangeView.SourceCountryID == 0)
			{
				_IPriceChangeView.Message = "Source Country is missing Please Select it.";
			}
			else
			{
				objBool = true;
			}
			return objBool;
		}
		public void UpdateRunningNo()
		{
			try
			{
				UpdateRunningNumRequest objUpdateRunningNumRequest = new UpdateRunningNumRequest();
				UpdateRunningNumResponse objUpdateRunningNumResponse = new UpdateRunningNumResponse();

				objUpdateRunningNumRequest.RunningNo = _RunningNo;
				objUpdateRunningNumRequest.DetailID = _DetailID;
				//objUpdateRunningNumRequest.StoreID = _IPriceChangeView.StoreID;
				objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void GetPriceChangeRecord()
		{
			try
			{
				var RequestData = new SelectByIDPriceChangeRequest();
				RequestData.ID = _IPriceChangeView.ID;
				var ResponseData = _PriceChangeBLL.GetPriceChangeRecord(RequestData);

				_IPriceChangeView.ID = ResponseData.PriceChangeRecord.ID;
				_IPriceChangeView.DocumentDate = ResponseData.PriceChangeRecord.DocumentDate;
				_IPriceChangeView.DocumentNo = ResponseData.PriceChangeRecord.DocumentNo;
				_IPriceChangeView.PriceChangeDate = ResponseData.PriceChangeRecord.PriceChangeDate;
				_IPriceChangeView.PriceChangeType = ResponseData.PriceChangeRecord.PriceChangeType;
				_IPriceChangeView.MultipleCountry = ResponseData.PriceChangeRecord.MultipleCountry;

				_IPriceChangeView.SourceCountryID = ResponseData.PriceChangeRecord.SourceCountryID;
				_IPriceChangeView.SourceCountryCode = ResponseData.PriceChangeRecord.SourceCountryCode;
				_IPriceChangeView.Status = ResponseData.PriceChangeRecord.Status;
				_IPriceChangeView.Remarks = ResponseData.PriceChangeRecord.Remarks;
				_IPriceChangeView.SCN = ResponseData.PriceChangeRecord.SCN;

				if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
				{
					_IPriceChangeView.Message = ResponseData.DisplayMessage;
				}
				else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
				{
					_IPriceChangeView.Message = ResponseData.DisplayMessage;
				}
				_IPriceChangeView.ProcessStatus = ResponseData.StatusCode;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void GetPriceChangeDetails()
		{
			SelectPriceChangeDetailsRequest RequestData = new SelectPriceChangeDetailsRequest();
			RequestData.ID = _IPriceChangeView.ID;
			SelectPriceChangeDetailsResponse ResponseData = _PriceChangeBLL.GetPriceChangeDetails(RequestData);
			if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
			{
				_IPriceChangeView.ValidatingPriceChangeDetailsList = ResponseData.PriceChangeDetailsList;
			}
		}

		public void GetPriceChangeCountries()
		{
			SelectPriceChangeCountriesRequest RequestData = new SelectPriceChangeCountriesRequest();
			RequestData.ID = _IPriceChangeView.ID;
			SelectPriceChangeCountriesResponse ResponseData = _PriceChangeBLL.GetPriceChangeCountries(RequestData);
			if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
			{
				_IPriceChangeView.SelectedCountries = ResponseData.PriceChangeCountriesList;
			}
		}
	}

	public class PriceChangeListPresenter
	{
		IPriceChangeCollectionView _IPriceChangeCollectionView;
		PriceChangeBLL _PriceChangeBLL = new PriceChangeBLL();

		public PriceChangeListPresenter(IPriceChangeCollectionView ViewObj)
		{
			_IPriceChangeCollectionView = ViewObj;
		}

		public void GetAllPriceChange()
		{
			try
			{
				var RequestData = new SelectAllPriceChangeRequest();
				RequestData.ShowInActiveRecords = true;
				var ResponseData = new SelectAllPriceChangeResponse();
				ResponseData = _PriceChangeBLL.GetAllPriceChange(RequestData);
				if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
				{
					_IPriceChangeCollectionView.PriceChangeList = ResponseData.PriceChangeList;
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
    public class PriceChangeLogPresenter
    {
        IPriceLogView _IPriceLogView;
		PriceChangeBLL _PriceChangeBLL = new PriceChangeBLL();

        public PriceChangeLogPresenter(IPriceLogView ViewObj)
		{
            _IPriceLogView = ViewObj;
		}
        public void SelectPriceChangeLog()
        {
            try
            {
                var RequestData = new SelectPriceChangeLogRequest();
                RequestData.FromStyleCode = _IPriceLogView.FromStyleCode;
                RequestData.ToStyleCode = _IPriceLogView.ToStyleCode;
                if(!string.IsNullOrWhiteSpace(RequestData.FromStyleCode) && !string.IsNullOrWhiteSpace(RequestData.ToStyleCode))
                {
                    var ResponseData = new SelectPriceChangeLogResponse();
                    ResponseData = _PriceChangeBLL.SelectPriceChangeLog(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _IPriceLogView.DT_PriceChange = new DataTable();
                        if(ResponseData.DT_PriceChange != null)
                        {
                            _IPriceLogView.DT_PriceChange = ResponseData.DT_PriceChange.Select("", "").CopyToDataTable();
                        }                        
                    }
                    else
                    {
                        _IPriceLogView.Message = "No Data Found.";
                    }
                }
                else
                {
                    _IPriceLogView.Message = "From / To StyleCode is empty";
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
