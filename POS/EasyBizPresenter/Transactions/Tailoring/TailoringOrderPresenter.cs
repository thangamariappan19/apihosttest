using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Tailoring;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Tailoring;
using EasyBizIView.Transactions.ITailoring;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.Tailoring;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using EasyBizResponse.Transactions.Tailoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ManagerOverrideRequest;
using EasyBizResponse.Masters.TailoringMasterResponse;
using EasyBizRequest.Masters.TailoringMasterRequest;
using EasyBizRequest.Masters.RetailSettingsRequest;

namespace EasyBizPresenter.Transactions.Tailoring
{
	public class TailoringOrderPresenter
	{
		ITailoringOrderView _ITailoringOrderView;
        TailoringOrderBLL _TailoringOrderBLL = new TailoringOrderBLL();
        SKUMasterBLL _SKUMasterBLL = new SKUMasterBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
		//WarehouseMasterBLL _WarehouseMasterBLL = new WarehouseMasterBLL();
		CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();
        UsersBLL _UsersBLL = new UsersBLL();

        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        int _RunningNo;
        int _DetailID;
		public TailoringOrderPresenter(ITailoringOrderView ViewObj)
        {
            _ITailoringOrderView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ITailoringOrderView.DocumentNo == "")
            {
                _ITailoringOrderView.Message = "Document No is missing.Please Make a entry in Document Numbering Screen.";
            }
            else if (_ITailoringOrderView.DocumentDate == null)
            {
                _ITailoringOrderView.Message = "Document Date is missing Please Enter it.";
            }           
            else if (_ITailoringOrderView.StoreCode == "")
            {
                _ITailoringOrderView.Message = "Store Code is missing.";
            }
			else if (_ITailoringOrderView.CustomerCode == "")
			{
				_ITailoringOrderView.Message = "Customer Code is missing Please Select it.";
			}
			else if (_ITailoringOrderView.ExpectedDeliveryDate == null)
			{
				_ITailoringOrderView.Message = "Expected Delivery Date is missing Please Enter it.";
			} 
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SelectRetailSettings()
        {
            try
            {
                var _RetailSettingsBLL = new RetailSettingsBLL();
                var RequestData = new SelectByRetailIDRequest();
                RequestData.ID = _ITailoringOrderView.RetailID;
                var ResponseData = _RetailSettingsBLL.SelectRetailRecord(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ITailoringOrderView.RetailSetting = ResponseData.RetailRecord;
                }
                else
                {
                    _ITailoringOrderView.RetailSetting = new RetailSettingsType();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveTailoringOrder() 
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveTailoringOrderRequest();
                    RequestData.TailoringOrderHeaderRecord = new TailoringOrder();

					var NewTailoringOrderDetailsList = new List<TailoringOrderDetails>();

					var TailoringOrderDetailsList = _ITailoringOrderView.TailoringOrderDetailsList;
					List<List<TailoringOrderDetails>> GroupByList = TailoringOrderDetailsList.GroupBy(d => d.SKUCode).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    //List<List<StyleMaster>> BrandWiseStyleList = BrandStyleList.GroupBy(d => d.BrandID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

					foreach (List<TailoringOrderDetails> objTailoringOrderDetails in GroupByList)
                    {
						var _TailoringOrderDetails = objTailoringOrderDetails.FirstOrDefault();
                        int Qty = objTailoringOrderDetails.Sum(x => x.OrderQuantity);
						_TailoringOrderDetails.OrderQuantity = Qty;
						NewTailoringOrderDetailsList.Add(_TailoringOrderDetails);
                    }

					RequestData.TailoringOrderDetailsList = NewTailoringOrderDetailsList; // _IStockRequestView.StockRequestDetailsList;                   
					RequestData.TailoringOrderHeaderRecord.ID = _ITailoringOrderView.ID;
					RequestData.TailoringOrderHeaderRecord.DocumentNo = _ITailoringOrderView.DocumentNo;
					RequestData.TailoringOrderHeaderRecord.DocumentDate = _ITailoringOrderView.DocumentDate;
					RequestData.TailoringOrderHeaderRecord.StoreCode = _ITailoringOrderView.StoreCode;
					RequestData.TailoringOrderHeaderRecord.CustomerCode = _ITailoringOrderView.CustomerCode;
					RequestData.TailoringOrderHeaderRecord.ExpectedDeliveryDate = _ITailoringOrderView.ExpectedDeliveryDate;
					RequestData.TailoringOrderHeaderRecord.CreateBy = _ITailoringOrderView.UserID;
					RequestData.TailoringOrderHeaderRecord.CreateOn = DateTime.Now;
					//RequestData.StockRequestHeaderRecord.Active = true;
					//RequestData.StockRequestHeaderRecord.SCN = _ITailoringOrderView.SCN;
                    var ResponseData = _TailoringOrderBLL.SaveTailoringOrder(RequestData);
                    _ITailoringOrderView.Message = ResponseData.DisplayMessage;
                    _ITailoringOrderView.ProcessStatus = ResponseData.StatusCode;

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        UpdateRunningNo();
                    }
                }
                else
                {
                    _ITailoringOrderView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRunningNo()
        {
            try
            {
                UpdateRunningNumRequest objUpdateRunningNumRequest = new UpdateRunningNumRequest();
                UpdateRunningNumResponse objUpdateRunningNumResponse = new UpdateRunningNumResponse();

                objUpdateRunningNumRequest.RunningNo = _RunningNo;
                objUpdateRunningNumRequest.DetailID = _DetailID;
				objUpdateRunningNumRequest.StoreID = _ITailoringOrderView.StoreID;
                objUpdateRunningNumResponse = _DocumentNumberingBLL.UpdateDocumentRunningNumber(objUpdateRunningNumRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectTailoringOrderRecord()
        {
            try
            {
                var RequestData = new SelectByIDTailoringOrderRequest();
                RequestData.ID = _ITailoringOrderView.ID;
                var ResponseData = _TailoringOrderBLL.SelectTailoringOrder(RequestData);
                _ITailoringOrderView.ID = ResponseData.TailoringOrderRecord.ID;
				_ITailoringOrderView.DocumentDate = ResponseData.TailoringOrderRecord.DocumentDate;
				_ITailoringOrderView.DocumentNo = ResponseData.TailoringOrderRecord.DocumentNo;
				_ITailoringOrderView.StoreCode = ResponseData.TailoringOrderRecord.StoreCode;
				_ITailoringOrderView.CustomerCode = ResponseData.TailoringOrderRecord.CustomerCode;
				_ITailoringOrderView.ExpectedDeliveryDate = ResponseData.TailoringOrderRecord.ExpectedDeliveryDate;
				                          
				//_ITailoringOrderView.SCN = ResponseData.StockRequestHeaderRecord.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ITailoringOrderView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ITailoringOrderView.Message = ResponseData.DisplayMessage;
                }
                _ITailoringOrderView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectTailoringOrderDetails()
        {
			SelectTailoringOrderDetailsRequest RequestData = new SelectTailoringOrderDetailsRequest();
            //RequestData.ShowInActiveRecords = true;
            RequestData.ID = _ITailoringOrderView.ID;
            var ResponseData = _TailoringOrderBLL.SelectTailoringOrderDetails(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITailoringOrderView.TailoringOrderDetailsList = ResponseData.TailoringOrderDetailsList;
            }
            else
            {
                _ITailoringOrderView.Message = ResponseData.DisplayMessage;
                _ITailoringOrderView.ProcessStatus = ResponseData.StatusCode;
            }
        }
        public void DeleteTailoringOrder()
        {
            try
            {
                var RequestData = new DeleteTailoringOrderRequest();
                RequestData.ID = _ITailoringOrderView.ID;
                var ResponseData = _TailoringOrderBLL.DeleteTailoringOrder(RequestData);
                _ITailoringOrderView.Message = ResponseData.DisplayMessage;
                _ITailoringOrderView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		public void GetCustomerLookUP()
		{
			SelectCustomerMasterLookUpRequest RequestData = new SelectCustomerMasterLookUpRequest();
			RequestData.ShowInActiveRecords = false;
			//RequestData.CountryID = _ITailoringOrderView.CountryID;
			SelectCustomerMasterLookUpResponse ResponseData = _CustomerMasterBLL.SelectCustomerMasterLookUp(RequestData);
			if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
			{
				_ITailoringOrderView.CustomerMasterLookUp = ResponseData.CustomerMasterList;
			}
		}

		public void GetCustomer()
		{
			try
			{
				//SelectAllCustomerMasterResponse SelectAllCustomerMaster(SelectAllCustomerMasterRequest objRequest)

				var RequestData = new SelectAllCustomerMasterRequest();
				RequestData.ShowInActiveRecords = false;
				RequestData.Source = "Sales";
				RequestData.ID = _ITailoringOrderView.CustomerID;
				RequestData.CustomerInfo = _ITailoringOrderView.CustomerSearchString;
				RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
				var ResponseData = new SelectAllCustomerMasterResponse();

				ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
				if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
				{
					_ITailoringOrderView.CustomerMasterList = ResponseData.CustomerMasterData;
				}
				else
				{
					var CustomerList = new List<CustomerMaster>();
					_ITailoringOrderView.CustomerMasterList = CustomerList;
				}
			}
			catch (Exception ex)
			{
				var CustomerList = new List<CustomerMaster>();
				_ITailoringOrderView.CustomerMasterList = CustomerList;
				throw ex;
			}
		}

		public void SelectManagerOverride(string Source)
		{
			try
			{
				var _ManagerOverrideBLL = new ManagerOverrideBLL();
				var RequestData = new SelectByIDManagerOverrideRequest();
				RequestData.ID = _ITailoringOrderView.ManagerOverrideID;
				var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
				if (Source == "PAGELOAD")
				{
					_ITailoringOrderView.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
				}
				else
				{
					_ITailoringOrderView.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
			RequestData.StoreID = _ITailoringOrderView.StoreID;
            RequestData.StoreCode = _ITailoringOrderView.StoreCode;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStorename(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _ITailoringOrderView.StoreCode = ResponseData.StoreMasterData.StoreCode;
            }
        }
        //public void GetToStoreMasterLookUP()
        //{
        //    SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
        //    RequestData.ShowInActiveRecords = false;
        //    RequestData.StateID = _IStockRequestView.StateID;
        //    SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
        //    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        //    {
        //        _IStockRequestView.ToStoreMasterLookUp = ResponseData.StoreMasterList;
        //    }
        //}
        public void GetSKU()
        {
            try
            {
                var RequestData = new SelectAllSKUMasterRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.Count = 1;
                RequestData.SearchString = _ITailoringOrderView.SKUSearchString;
                RequestData.Mode = "SALES";
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ITailoringOrderView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
                else
                {
                    _ITailoringOrderView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectDocumentNumberingRecord()
        {
            try
            {
                var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
                RequestData.RequestFrom = Enums.RequestFrom.StoreServer;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.TAILORINGORDER;
				RequestData.StoreID = _ITailoringOrderView.StoreID;
                RequestData.StoreCode = _ITailoringOrderView.StoreCode;
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string DocumentNo = string.Empty;
                    DocumentNo = DocumentNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);
                   
                    _ITailoringOrderView.DocumentNo = DocumentNo;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectSKULookup()
        {
            var RequestData = new SelectAllSKUMasterRequest();

            var ResponseData = _SKUMasterBLL.SelectAllSKUMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
				_ITailoringOrderView.SKUMasterTypesList = ResponseData.SKUMasterTypesList;
            }
            else
            {
                _ITailoringOrderView.Message = ResponseData.DisplayMessage;
                _ITailoringOrderView.ProcessStatus = ResponseData.StatusCode;
            }
        }
	}
	public class TailoringOrderListPresenter
	{
		ITailoringOrderCollectionView _ITailoringOrderCollectionView;
		TailoringOrderBLL _TailoringOrderBLL = new TailoringOrderBLL();

		public TailoringOrderListPresenter(ITailoringOrderCollectionView ViewObj)
		{
			_ITailoringOrderCollectionView = ViewObj;
		}

		public void GetTailoringOrderlist()
		{
			try
			{
				var RequestData = new SelectAllTailoringOrderRequest();
				RequestData.ShowInActiveRecords = true;
				RequestData.StoreCode = _ITailoringOrderCollectionView.StoreCode;
				var ResponseData = new SelectAllTailoringOrderResponse();
				ResponseData = _TailoringOrderBLL.SelectAllTailoringOrder(RequestData);
				if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
				{
					_ITailoringOrderCollectionView.TailoringOrderList = ResponseData.TailoringOrderList;
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
	public class DespatchToTailoringUnitPresenter
	{
		IDespatchToTailoringUnit _IDespatchToTailoringUnit;
		TailoringOrderBLL _TailoringOrderBLL = new TailoringOrderBLL();
		TailoringMasterBLL _TailoringMasterBLL = new TailoringMasterBLL();

		public DespatchToTailoringUnitPresenter(IDespatchToTailoringUnit ViewObj)
		{
			_IDespatchToTailoringUnit = ViewObj;
		}

		public void GetAllOpenTailoringOrderList()
		{
			try
			{
				var RequestData = new SelectAllOPENTailoringOrderRequest();
				RequestData.StoreCode = _IDespatchToTailoringUnit.StoreCode;
				var ResponseData = new SelectAllOPENTailoringOrderResponse();
				ResponseData = _TailoringOrderBLL.SelectAllOPENTailoringOrder(RequestData);
				if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
				{
					_IDespatchToTailoringUnit.TailoringOrderList = ResponseData.TailoringOrderList;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		public void GetTailoringMasterLookUp()
		{
			SelectAllTailoringMasterByStoreRequest RequestData = new SelectAllTailoringMasterByStoreRequest();
			RequestData.ShowInActiveRecords = false;
			RequestData.StoreID = _IDespatchToTailoringUnit.StoreID;
			//RequestData.CountryID = _ITailoringOrderView.CountryID;
			SelectAllTailoringMasterByStoreResponse ResponseData = _TailoringMasterBLL.SelectTailoringMasterLookup(RequestData);
			if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
			{
				_IDespatchToTailoringUnit.TailoringMasterTypesLookUp = ResponseData.TailoringMasterList;
			}
		}

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IDespatchToTailoringUnit.TailoringUnitCode == "")
            {
                _IDespatchToTailoringUnit.Message = "Tailoring Unit Code is missing.Please Select it.";
            }
            else if (_IDespatchToTailoringUnit.TailorDeliveryDate == DateTime.MinValue)
            {
                _IDespatchToTailoringUnit.Message = "Tailor Delivery Date is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveDespatchToTailoringUnit()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveDespatchToTailorRequest();
                    RequestData.TailoringOrderList = new List<TailoringOrder>();

                    var NewTailoringOrderList = new List<TailoringOrder>();

                    var TailoringOrderList = _IDespatchToTailoringUnit.TailoringOrderList;
                    List<List<TailoringOrder>> GroupByList = TailoringOrderList.GroupBy(d => d.ID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    //List<List<StyleMaster>> BrandWiseStyleList = BrandStyleList.GroupBy(d => d.BrandID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    foreach (List<TailoringOrder> objTailoringOrder in GroupByList)
                    {
                        var _TailoringOrder = objTailoringOrder.FirstOrDefault();
                        if(_TailoringOrder.DeliveredToTailor == true)
                        {
                            NewTailoringOrderList.Add(_TailoringOrder);
                        }
                        
                    }

                    RequestData.TailoringOrderList = NewTailoringOrderList; // _IStockRequestView.StockRequestDetailsList;                   
                    RequestData.TailoringUnitCode = _IDespatchToTailoringUnit.TailoringUnitCode;
                    RequestData.TailorDeliveryDate = _IDespatchToTailoringUnit.TailorDeliveryDate;
                    var ResponseData = _TailoringOrderBLL.SaveDespatchToTailor(RequestData);
                    _IDespatchToTailoringUnit.Message = ResponseData.DisplayMessage;
                    _IDespatchToTailoringUnit.ProcessStatus = ResponseData.StatusCode;

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {                      
                    }
                }
                else
                {
                    _IDespatchToTailoringUnit.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}

    public class ReceiveFromTailorPresenter
    {
        IReceiveFromTailorView _IReceiveFromTailorView;
        TailoringOrderBLL _TailoringOrderBLL = new TailoringOrderBLL();
        TailoringMasterBLL _TailoringMasterBLL = new TailoringMasterBLL();

        public ReceiveFromTailorPresenter(IReceiveFromTailorView ViewObj)
        {
            _IReceiveFromTailorView = ViewObj;
        }

        public void SelectTailoringOrderForReceiveFromTailor()
        {
            try
            {
                var RequestData = new SelectTailoringOrderForReceiveFromTailorRequest();
                RequestData.StoreCode = _IReceiveFromTailorView.StoreCode;
                RequestData.TailoringUnitCode = _IReceiveFromTailorView.TailoringUnitCode;
                var ResponseData = new SelectTailoringOrderForReceiveFromTailorResponse();
                ResponseData = _TailoringOrderBLL.SelectTailoringOrderForReceiveFromTailor(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IReceiveFromTailorView.TailoringOrderList = ResponseData.TailoringOrderList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GetTailoringMasterLookUp()
        {
            SelectAllTailoringMasterByStoreRequest RequestData = new SelectAllTailoringMasterByStoreRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IReceiveFromTailorView.StoreID;
            //RequestData.CountryID = _ITailoringOrderView.CountryID;
            SelectAllTailoringMasterByStoreResponse ResponseData = _TailoringMasterBLL.SelectTailoringMasterLookup(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IReceiveFromTailorView.TailoringMasterTypesLookUp = ResponseData.TailoringMasterList;
            }
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            foreach(var TL in _IReceiveFromTailorView.TailoringOrderList)
            {
                _IReceiveFromTailorView.TailoringOrderDetailsList.AddRange(TL.TailoringOrderDetailsList);
            }
            if (_IReceiveFromTailorView.TailoringOrderDetailsList.Where(x => x.OrderQuantity < (x.ReceivedFromTailor + x.ReceivedTailor)).Count() > 0)
            {
                _IReceiveFromTailorView.Message = "Quantity is exceed from the oreder Quantity";
            }
            else if (_IReceiveFromTailorView.ReceivedDate == DateTime.MinValue)
            {
                _IReceiveFromTailorView.Message = "Received Date is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveReceiveFromTailoring()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveReceiveFromTailoringOrderRequest();
                    RequestData.TailoringOrderList = new List<TailoringOrder>();

                    var NewTailoringOrderList = new List<TailoringOrder>();

                    var TailoringOrderList = _IReceiveFromTailorView.TailoringOrderList;
                    List<List<TailoringOrder>> GroupByList = TailoringOrderList.GroupBy(d => d.ID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    //List<List<StyleMaster>> BrandWiseStyleList = BrandStyleList.GroupBy(d => d.BrandID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();

                    foreach (List<TailoringOrder> objTailoringOrder in GroupByList)
                    {
                        var _TailoringOrder = objTailoringOrder.FirstOrDefault();
                        NewTailoringOrderList.Add(_TailoringOrder);
                    }

                    RequestData.TailoringOrderList = NewTailoringOrderList; // _IStockRequestView.StockRequestDetailsList;                   
                    RequestData.ReceivedDate = _IReceiveFromTailorView.ReceivedDate;                    
                    var ResponseData = _TailoringOrderBLL.SaveReceiveFromTailoring(RequestData);
                    _IReceiveFromTailorView.Message = ResponseData.DisplayMessage;
                    _IReceiveFromTailorView.ProcessStatus = ResponseData.StatusCode;

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                    }
                }
                else
                {
                    _IReceiveFromTailorView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class DeliverToCustomerPresenter
    {
        IDeliverToCustomer _IDeliverToCustomer;
        TailoringOrderBLL _TailoringOrderBLL = new TailoringOrderBLL();
        TailoringMasterBLL _TailoringMasterBLL = new TailoringMasterBLL();

        public DeliverToCustomerPresenter(IDeliverToCustomer ViewObj)
        {
            _IDeliverToCustomer = ViewObj;
        }
        public void GetTailoringMasterLookUp()
        {
            SelectAllTailoringMasterByStoreRequest RequestData = new SelectAllTailoringMasterByStoreRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreID = _IDeliverToCustomer.StoreID;
            //RequestData.CountryID = _ITailoringOrderView.CountryID;
            SelectAllTailoringMasterByStoreResponse ResponseData = _TailoringMasterBLL.SelectTailoringMasterLookup(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDeliverToCustomer.TailoringMasterTypesLookUp = ResponseData.TailoringMasterList;
            }
        }
        public void SelectTailoringOrderForReceiveFromTailor()
        {
            try
            {
                var RequestData = new SelectTailoringOrderForDeliverToCustomerRequest();
                RequestData.StoreCode = _IDeliverToCustomer.StoreCode;
                RequestData.TailoringUnitCode = _IDeliverToCustomer.TailoringUnitCode;
                RequestData.FromDeliverCode = "DeliverToCustomer";
                var ResponseData = new SelectTailoringOrderDeliverToCustomerResponse();
                ResponseData = _TailoringOrderBLL.SelectTailoringOrderForReceiveFromTailor(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDeliverToCustomer.TailoringOrderList = ResponseData.TailoringOrderList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveDeliverToCustomer()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveDeliverToCustomerRequest();
                    RequestData.TailoringOrderList = new List<TailoringOrder>();

                    var NewTailoringOrderList = new List<TailoringOrder>();

                    var TailoringOrderList = _IDeliverToCustomer.TailoringOrderList;
                    List<List<TailoringOrder>> GroupByList = TailoringOrderList.GroupBy(d => d.ID).OrderBy(g => g.Key).Select(g => g.ToList()).ToList();                 

                    foreach (List<TailoringOrder> objTailoringOrder in GroupByList)
                    {
                        var _TailoringOrder = objTailoringOrder.FirstOrDefault();                       
                        NewTailoringOrderList.Add(_TailoringOrder);
                    }

                    RequestData.TailoringOrderList = NewTailoringOrderList; // _IStockRequestView.StockRequestDetailsList;                   
                    RequestData.ReceivedDate = _IDeliverToCustomer.ReceivedDate;                    
                    var ResponseData = _TailoringOrderBLL.SaveDeliverToCustomer(RequestData);
                    _IDeliverToCustomer.Message = ResponseData.DisplayMessage;
                    _IDeliverToCustomer.ProcessStatus = ResponseData.StatusCode;

                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                    }
                }
                else
                {
                    _IDeliverToCustomer.ProcessStatus = Enums.OpStatusCode.GeneralError;
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
            foreach (var TL in _IDeliverToCustomer.TailoringOrderList)
            {
                _IDeliverToCustomer.TailoringOrderDetailsList.AddRange(TL.TailoringOrderDetailsList);
            }
            if (_IDeliverToCustomer.TailoringOrderDetailsList.Where(x => x.OrderQuantity < (x.AlreadyDeliveredQuantity + x.DeliveredQuantity)).Count() > 0)
            {
                _IDeliverToCustomer.Message = "Quantity is exceed from the oreder Quantity";
            }
           else if (_IDeliverToCustomer.ReceivedDate == DateTime.MinValue)
            {
                _IDeliverToCustomer.Message = "Received Date is missing Please Enter it.";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
    }
}
