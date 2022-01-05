using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPosMaster;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.PosMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.CountryResponse;
using EasyBizResponse.Masters.CustomerMasterResponse;
using EasyBizResponse.Masters.PosMasterResponse;
using EasyBizResponse.Masters.StoreGroupResponse;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class PosMasterPresenter
    {
        IPosMasterView _IPosMasterView;
        PosMasterBLL _PosMasterBLL = new PosMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();
        public PosMasterPresenter(IPosMasterView ViewObj)
        {
            _IPosMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IPosMasterView.PosCode.Trim() == string.Empty)
            {
                _IPosMasterView.Message = "PosCode is missing Please Enter it.";
            }
            else if (_IPosMasterView.PosName.Trim() == string.Empty)
            {
                _IPosMasterView.Message = "Please Enter PosName";
            }
            else if (_IPosMasterView.CountryID == 0)
            {
                _IPosMasterView.Message = "Country Is mising";
            }
            else if (_IPosMasterView.StoreGroupID == 0)
            {
                _IPosMasterView.Message = "Store Group Is mising";
            }
            else if (_IPosMasterView.StoreID == 0)
            {
                _IPosMasterView.Message = "Store Is mising";
            }
            else if (_IPosMasterView.CustomerID == 0)
            {
                _IPosMasterView.Message = "Default Customer is Missing";
            }

            //else if (_IPosMasterView.Description.Trim() == string.Empty)
            //{
            //    _IPosMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SavePosMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SavePosMasterRequest();
                    RequestData.PosMasterData = new PosMaster();
                    RequestData.PosMasterData = new PosMaster();
                    RequestData.PosMasterData.ID = _IPosMasterView.ID;
                    RequestData.PosMasterData.PosCode = _IPosMasterView.PosCode;
                    RequestData.PosMasterData.PosName = _IPosMasterView.PosName;
                    RequestData.PosMasterData.CountryID = _IPosMasterView.CountryID;
                    RequestData.PosMasterData.StoreID = _IPosMasterView.StoreID;
                    RequestData.PosMasterData.StoreGroupID = _IPosMasterView.StoreGroupID;
                    RequestData.PosMasterData.DefaultCustomer = _IPosMasterView.CustomerID;
                    RequestData.PosMasterData.PoleDisplayPort = _IPosMasterView.PoleDisplayPort;
                    RequestData.PosMasterData.DisplayLineMsgOne = _IPosMasterView.DisplayLineMsgOne;
                    RequestData.PosMasterData.DisplayLineMsgTwo = _IPosMasterView.DisplayLineMsgTwo;
                    RequestData.PosMasterData.UpdateBy = _IPosMasterView.UserID;
                    RequestData.PosMasterData.Active = _IPosMasterView.Active;
                    RequestData.PosMasterData.CountryCode = _IPosMasterView.CountryCode;
                    RequestData.PosMasterData.StoreCode = _IPosMasterView.StoreCode;
                    RequestData.PosMasterData.StoreGroupCode = _IPosMasterView.StoreGroupCode;
                    RequestData.PosMasterData.DefaultCustomerCode = _IPosMasterView.CustmerCode;
                    RequestData.PosMasterData.SCN = _IPosMasterView.SCN;
                    RequestData.PosMasterData.CreateBy = _IPosMasterView.UserID;
                    RequestData.PosMasterData.PrinterDeviceName = _IPosMasterView.PrinterDeviceName;
                    SavePosMasterResponse ResponseData = _PosMasterBLL.SavePosMaster(RequestData);
                    _IPosMasterView.Message = ResponseData.DisplayMessage;
                    _IPosMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPosMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAllCustomerMaster()
        {
            try
            {
                var RequestData = new SelectAllCustomerMasterRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.ID = _IPosMasterView.CustomerID;
                RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                var ResponseData = new SelectAllCustomerMasterResponse();
                ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPosMasterView.CustomerMasterList = ResponseData.CustomerMasterData;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdatePosMaster()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new UpdatePosMasterRequest();
                    RequestData.PosMasterData = new PosMaster();
                    RequestData.PosMasterData.ID = _IPosMasterView.ID;
                    RequestData.PosMasterData.PosCode = _IPosMasterView.PosCode;
                    RequestData.PosMasterData.PosName = _IPosMasterView.PosName;
                    RequestData.PosMasterData.CountryID = _IPosMasterView.CountryID;
                    RequestData.PosMasterData.StoreID = _IPosMasterView.StoreID;
                    RequestData.PosMasterData.StoreGroupID = _IPosMasterView.StoreGroupID;
                    RequestData.PosMasterData.DefaultCustomer = _IPosMasterView.CustomerID;
                    RequestData.PosMasterData.PoleDisplayPort = _IPosMasterView.PoleDisplayPort;
                    RequestData.PosMasterData.DisplayLineMsgOne = _IPosMasterView.DisplayLineMsgOne;
                    RequestData.PosMasterData.DisplayLineMsgTwo = _IPosMasterView.DisplayLineMsgTwo;
                    RequestData.PosMasterData.Active = _IPosMasterView.Active;
                    RequestData.PosMasterData.DiskID = _IPosMasterView.DiskID;
                    RequestData.PosMasterData.CPUID = _IPosMasterView.CPUID;
                    RequestData.PosMasterData.UpdateBy = _IPosMasterView.UserID;
                    RequestData.PosMasterData.CountryCode = _IPosMasterView.CountryCode;
                    RequestData.PosMasterData.StoreCode = _IPosMasterView.StoreCode;
                    RequestData.PosMasterData.StoreGroupCode = _IPosMasterView.StoreGroupCode;
                    RequestData.PosMasterData.DefaultCustomerCode = _IPosMasterView.CustmerCode;
                    RequestData.PosMasterData.SCN = _IPosMasterView.SCN;
                    RequestData.PosMasterData.PrinterDeviceName = _IPosMasterView.PrinterDeviceName;
                    var ResponseData = _PosMasterBLL.UpdatePosMaster(RequestData);
                    _IPosMasterView.Message = ResponseData.DisplayMessage;
                    _IPosMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPosMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePosMaster()
        {
            try
            {

                var RequestData = new DeletePosMasterRequest();
                RequestData.ID = _IPosMasterView.ID;
                RequestData.StoreID = _IPosMasterView.StoreID;
                var ResponseData = _PosMasterBLL.DeletePosMaster(RequestData);
                _IPosMasterView.Message = ResponseData.DisplayMessage;
                _IPosMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectPosMasterRecord()
        {


            var RequestData = new SelectByIDPosMasterRequest();
            RequestData.ID = _IPosMasterView.ID;

            var ResponseData = _PosMasterBLL.SelectPosMasterRecord(RequestData);
            _IPosMasterView.PosCode = ResponseData.PosMasterRecord.PosCode;
            _IPosMasterView.PosName = ResponseData.PosMasterRecord.PosName;
            _IPosMasterView.CountryID = ResponseData.PosMasterRecord.CountryID;
            _IPosMasterView.StoreID = ResponseData.PosMasterRecord.StoreID;
            _IPosMasterView.CustomerID = ResponseData.PosMasterRecord.DefaultCustomer;
            _IPosMasterView.StoreGroupID = ResponseData.PosMasterRecord.StoreGroupID;
            _IPosMasterView.Active = ResponseData.PosMasterRecord.Active;
            _IPosMasterView.DiskID = ResponseData.PosMasterRecord.DiskID;
            _IPosMasterView.CPUID = ResponseData.PosMasterRecord.CPUID;
            _IPosMasterView.SCN = ResponseData.PosMasterRecord.SCN;
            _IPosMasterView.PrinterDeviceName = ResponseData.PosMasterRecord.PrinterDeviceName;
            _IPosMasterView.PoleDisplayPort = ResponseData.PosMasterRecord.PoleDisplayPort;
            _IPosMasterView.DisplayLineMsgOne = ResponseData.PosMasterRecord.DisplayLineMsgOne;
            _IPosMasterView.DisplayLineMsgTwo = ResponseData.PosMasterRecord.DisplayLineMsgTwo;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IPosMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IPosMasterView.Message = ResponseData.DisplayMessage;
            }

            _IPosMasterView.ProcessStatus = ResponseData.StatusCode;
        }
        public void GetStoreLookUp()
        {
            try
            {
                var RequestData = new SelectStoreMasterLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPosMasterView.StoreMasterLookUp = ResponseData.StoreMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCountryLookUP()
        {
            SelectCountryLookUpRequest RequestData = new SelectCountryLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectCountryLookUpResponse ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPosMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
            }
        }
        public void GetStoreGroupMasterLookUP()
        {
            SelectStoreGroupLookUpRequest RequestData = new SelectStoreGroupLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.CountryID = _IPosMasterView.CountryID;
            SelectStoreGroupLookUpResponse ResponseData = _PosMasterBLL.SelectStoreGroupMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPosMasterView.StoreGroupMasterLookUp = ResponseData.StoreGroupMasterList;
            }
        }


        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            RequestData.StoreGroupID = _IPosMasterView.StoreGroupID;
            SelectStoreMasterLookUpResponse ResponseData = _PosMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPosMasterView.StoreRecord = ResponseData.StoreMasterList;
            }
        }

    }
    public class PosMasterListPresenter
    {

        PosMasterBLL _PosMasterBLL = new PosMasterBLL();

        IPosMasterList _IPosMasterList;

        public PosMasterListPresenter(IPosMasterList ViewObj)
        {
            _IPosMasterList = ViewObj;
        }

        public void GetPosMasterList()
        {

            var RequestData = new SelectAllPosMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllPosMasterResponse();
            ResponseData = _PosMasterBLL.SelectAllPosMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IPosMasterList.PosMasterList = ResponseData.PosMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }
        }
    }


}
