using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ICustomerMaster;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.CustomerGroupRequest;
using EasyBizRequest.Masters.CustomerMasterRequest;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.StateMasterRequest;
using EasyBizResponse.Masters.CustomerGroupResponse;
using EasyBizResponse.Masters.CustomerMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizBLL.Common;
using EasyBizRequest.Masters.ManagerOverrideRequest;

namespace EasyBizPresenter.Masters
{
    public class CustomerMasterPresenter
    {

        ICustomerMasterView _ICustomerMasterView;

        CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();
        CustomerGroupBLL _CustomerGroupMasterBLL = new CustomerGroupBLL();
        StateMasterBLL _StateMasterBLL = new StateMasterBLL();
        CountryBLL _CountryBLL = new CountryBLL();
        DocumentNumberingBLL _DocumentNumberingBLL = new DocumentNumberingBLL();
        string _BillNo = string.Empty;
        int _RunningNo = 0;
        int _DetailID = 0;
        public CustomerMasterPresenter(ICustomerMasterView ViewObj)
        {

            _ICustomerMasterView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICustomerMasterView.CustomerCode == string.Empty)
            {
                _ICustomerMasterView.Message = "Customer code cannot be empty!";
            }
            //else if (_ICustomerMasterView.CustomerCode.Length > 11)
            //{
            //    _ICustomerMasterView.Message = "Customer code allowed maximum length is 11!";
            //}
            else if (_ICustomerMasterView.CustomerName == string.Empty)
            {
                _ICustomerMasterView.Message = "Customer Name cannot be empty!";
            }
            else if (_ICustomerMasterView.PhoneNumber == string.Empty)
            {
                _ICustomerMasterView.Message = "Phone number cannot be empty!";
            }
            //else if (_ICustomerMasterView.CustomerGroupID == 0)
            //{
            //    _ICustomerMasterView.Message = "Customer group cannot be empty!";
            //}
            //else  if (_ICustomerMasterView.AreaName1 == string.Empty)
            //{
            //    _ICustomerMasterView.Message = "AreaName1 is missing Please Enter it.";
            //}
            //else if (_ICustomerMasterView.CountryID == 0)
            //{
            //    _ICustomerMasterView.Message = "Country cannot be empty!";
            //}
            //else if (_ICustomerMasterView.StateID == 0)
            //{
            //    _ICustomerMasterView.Message = "State cannot be empty!";
            //}

            //else if (_ICustomerMasterView.Email == string.Empty)
            //{
            //    _ICustomerMasterView.Message = "Email is missing Please Enter it.";
            //}
            //else if (_ICustomerMasterView.DOB == null)
            //{
            //    _ICustomerMasterView.Message = "DOB cannot be empty";
            //}
            //else if (_ICustomerMasterView.Gender == string.Empty)
            //{
            //    _ICustomerMasterView.Message = "Gender is mandatory";
            //}
            else
            {
                objBool = true;
            }
            return objBool;
        }      
      
        public void SaveCustomerMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    if(_ICustomerMasterView.ID ==0)
                        SelectDocumentNumberingRecord();

                    var RequestData = new SaveCustomerMasterRequest();
                    RequestData.CustomerMasterData = new CustomerMaster();

                    RequestData.CustomerMasterData.ID = _ICustomerMasterView.ID;
                    RequestData.CustomerMasterData.CustomerCode = _ICustomerMasterView.CustomerCode;
                    RequestData.CustomerMasterData.CustomerName = _ICustomerMasterView.CustomerName;
                    RequestData.CustomerMasterData.PhoneNumber = _ICustomerMasterView.PhoneNumber;
                    RequestData.CustomerMasterData.AlterPhoneNumber = _ICustomerMasterView.AlterPhoneNumber;
                    RequestData.CustomerMasterData.CustomerGroupID = _ICustomerMasterView.CustomerGroupID;
                    RequestData.CustomerMasterData.BuildingAndBlockNo = _ICustomerMasterView.BuildingAndBlockNo;
                    RequestData.CustomerMasterData.StreetName = _ICustomerMasterView.StreetName;
                    RequestData.CustomerMasterData.AreaName1 = _ICustomerMasterView.AreaName1;
                    RequestData.CustomerMasterData.AreaName2 = _ICustomerMasterView.AreaName2;
                    RequestData.CustomerMasterData.City = _ICustomerMasterView.City;
                    RequestData.CustomerMasterData.StateID = _ICustomerMasterView.StateID;
                    RequestData.CustomerMasterData.CountryID = _ICustomerMasterView.CountryID;
                    RequestData.CustomerMasterData.Email = _ICustomerMasterView.Email;
                    RequestData.CustomerMasterData.DOB = _ICustomerMasterView.DOB;
                    RequestData.CustomerMasterData.Gender = _ICustomerMasterView.Gender;
                    RequestData.CustomerMasterData.CreateBy = 1;
                    RequestData.CustomerMasterData.SCN = _ICustomerMasterView.SCN;
                    RequestData.CustomerMasterData.CreditAmount = _ICustomerMasterView.CreditAmount;
                    RequestData.CustomerMasterData.Remarks = _ICustomerMasterView.Remarks;
                    RequestData.CustomerMasterData.Active = _ICustomerMasterView.Active;
                    RequestData.CustomerMasterData.OnAccountApplicable = _ICustomerMasterView.OnAccountApplicable;
                    RequestData.RequestFrom = _ICustomerMasterView.RequestFrom;

                    RequestData.CustomerMasterData.CustomerGroupCode = _ICustomerMasterView.CustomerGroupCode;
                    RequestData.CustomerMasterData.StateCode = _ICustomerMasterView.StateCode;
                    RequestData.CustomerMasterData.CountryCode = _ICustomerMasterView.CountryCode;

                    RequestData.RunningNo = _RunningNo;
                    RequestData.DocumentNumberingID = _DetailID;

                    var ResponseData = _CustomerMasterBLL.SaveCustomerMaster(RequestData);
                    if (_ICustomerMasterView.ID == 0)
                    {
                        _ICustomerMasterView.Message = ResponseData.DisplayMessage;
                        _ICustomerMasterView.ProcessStatus = ResponseData.StatusCode;
                    }
                    else if (ResponseData.StatusCode == Enums.OpStatusCode.Success && _ICustomerMasterView.ID != 0 )
                    {
                        _ICustomerMasterView.Message = "Given Phone Number is already used by Other Customer So please give another number";
                    }
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _ICustomerMasterView.ID = Convert.ToInt32(ResponseData.IDs);
                    }
                } 
                else
                {
                    _ICustomerMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateCustomerMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateCustomerMasterRequest();
                    RequestData.CustomerMasterData = new CustomerMaster();

                    RequestData.CustomerMasterData.ID = _ICustomerMasterView.ID;
                    RequestData.CustomerMasterData.BaseID = _ICustomerMasterView.BaseID;
                    RequestData.CustomerMasterData.CustomerCode = _ICustomerMasterView.CustomerCode;
                    RequestData.CustomerMasterData.CustomerName = _ICustomerMasterView.CustomerName;
                    RequestData.CustomerMasterData.PhoneNumber = _ICustomerMasterView.PhoneNumber;
                    RequestData.CustomerMasterData.AlterPhoneNumber = _ICustomerMasterView.AlterPhoneNumber;
                    RequestData.CustomerMasterData.CustomerGroupID = _ICustomerMasterView.CustomerGroupID;
                    RequestData.CustomerMasterData.BuildingAndBlockNo = _ICustomerMasterView.BuildingAndBlockNo;
                    RequestData.CustomerMasterData.StreetName = _ICustomerMasterView.StreetName;
                    RequestData.CustomerMasterData.AreaName1 = _ICustomerMasterView.AreaName1;
                    RequestData.CustomerMasterData.AreaName2 = _ICustomerMasterView.AreaName2;
                    RequestData.CustomerMasterData.City = _ICustomerMasterView.City;
                    RequestData.CustomerMasterData.StateID = _ICustomerMasterView.StateID;
                    RequestData.CustomerMasterData.CountryID = _ICustomerMasterView.CountryID;
                    RequestData.CustomerMasterData.Email = _ICustomerMasterView.Email;
                    RequestData.CustomerMasterData.DOB = _ICustomerMasterView.DOB;
                    RequestData.CustomerMasterData.Gender = _ICustomerMasterView.Gender;
                    RequestData.CustomerMasterData.CreateBy = 1;
                    RequestData.CustomerMasterData.SCN = _ICustomerMasterView.SCN;
                    RequestData.CustomerMasterData.Remarks = _ICustomerMasterView.Remarks;
                    RequestData.CustomerMasterData.CreditAmount = _ICustomerMasterView.CreditAmount;
                    RequestData.CustomerMasterData.Active = _ICustomerMasterView.Active;
                    RequestData.CustomerMasterData.OnAccountApplicable = _ICustomerMasterView.OnAccountApplicable;
                    RequestData.RequestFrom = _ICustomerMasterView.RequestFrom;
                    
                    RequestData.CustomerMasterData.CustomerGroupCode = _ICustomerMasterView.CustomerGroupCode;
                    RequestData.CustomerMasterData.StateCode = _ICustomerMasterView.StateCode;
                    RequestData.CustomerMasterData.CountryCode = _ICustomerMasterView.CountryCode;

                    var ResponseData = _CustomerMasterBLL.UpdateCustomerMaster(RequestData);

                    _ICustomerMasterView.Message = ResponseData.DisplayMessage;
                    _ICustomerMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _ICustomerMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void DeleteCustomerMaster()
        {
            try
            {
                var RequestData = new DeleteCustomerMasterRequest();
                RequestData.CustomerMasterData = new CustomerMaster();

                RequestData.CustomerMasterData.ID = _ICustomerMasterView.ID;

                var ResponseData = _CustomerMasterBLL.DeleteCustomerMaster(RequestData);

                _ICustomerMasterView.Message = ResponseData.DisplayMessage;
                _ICustomerMasterView.ProcessStatus = ResponseData.StatusCode;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void SelectCustomerMaster()
        {
            try
            {
                var RequestData = new SelectByIDCustomerMasterRequest();
                RequestData.ID = _ICustomerMasterView.ID;
                var ResponseData = _CustomerMasterBLL.SelectByIDCustomerMaster(RequestData);
                _ICustomerMasterView.CustomerCode = ResponseData.CustomerMaster.CustomerCode;
                _ICustomerMasterView.CustomerName = ResponseData.CustomerMaster.CustomerName;
                _ICustomerMasterView.PhoneNumber = ResponseData.CustomerMaster.PhoneNumber;
                _ICustomerMasterView.AlterPhoneNumber = ResponseData.CustomerMaster.AlterPhoneNumber;
                _ICustomerMasterView.CustomerGroupID = ResponseData.CustomerMaster.CustomerGroupID;


                _ICustomerMasterView.BuildingAndBlockNo = ResponseData.CustomerMaster.BuildingAndBlockNo;
                _ICustomerMasterView.StreetName = ResponseData.CustomerMaster.StreetName;
                _ICustomerMasterView.AreaName1 = ResponseData.CustomerMaster.AreaName1;
                _ICustomerMasterView.AreaName2 = ResponseData.CustomerMaster.AreaName2;
                _ICustomerMasterView.City = ResponseData.CustomerMaster.City;
                _ICustomerMasterView.CountryID = ResponseData.CustomerMaster.CountryID;
                _ICustomerMasterView.StateID = ResponseData.CustomerMaster.StateID;
                _ICustomerMasterView.Email = ResponseData.CustomerMaster.Email;
                _ICustomerMasterView.DOB = ResponseData.CustomerMaster.DOB;
                _ICustomerMasterView.BaseID = ResponseData.CustomerMaster.BaseID;
                _ICustomerMasterView.Gender = ResponseData.CustomerMaster.Gender;
                _ICustomerMasterView.SCN = ResponseData.CustomerMaster.SCN;
                _ICustomerMasterView.Remarks = ResponseData.CustomerMaster.Remarks;
                _ICustomerMasterView.CreditAmount = ResponseData.CustomerMaster.CreditAmount;
                _ICustomerMasterView.Active = ResponseData.CustomerMaster.Active;
                _ICustomerMasterView.OnAccountApplicable = ResponseData.CustomerMaster.OnAccountApplicable;


                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICustomerMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ICustomerMasterView.Message = ResponseData.DisplayMessage;
                }

                _ICustomerMasterView.ProcessStatus = ResponseData.StatusCode;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void GetCustomerGroupLookUp()
        {
            try
            {
                var RequestData = new SelectCustomerGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CustomerGroupMasterBLL.SelectCustomerGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICustomerMasterView.CustomerGroupNameLookUp = ResponseData.CustomerGroupMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void UpdateRunningNum()
        {
            try
            {
                var RequestData = new UpdateRunningNumRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.RunningNo = _ICustomerMasterView.RunningNum;
                RequestData.DetailID = _ICustomerMasterView.DetailID;
                var ResponseData = _DocumentNumberingBLL.UpdateDocumentRunningNumber(RequestData);
                //if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                //{
                //    //_ICustomerMasterView.CustomerGroupNameLookUp = ResponseData.CustomerGroupMasterList;
                //}
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
                RequestData.RequestFrom = _ICustomerMasterView.RequestFrom;
                RequestData.DocumentTypeID = (int)Enums.DocumentType.CUSTOMERMASTER;
                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    //RequestData.CountryID = _ICustomerMasterView.UserInformation.CountryID;
                    //RequestData.StateID = _ICustomerMasterView.UserInformation.StateID;
                    //RequestData.StoreID = _ICustomerMasterView.UserInformation.StoreID;
                    //RequestData.POSID = _ICustomerMasterView.UserInformation.POSID;

                    RequestData.CountryID = 0;
                    RequestData.StateID = 0;
                    RequestData.StoreID = 0;
                    RequestData.POSID = 0;
                   
                }
                if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    RequestData.CountryID = _ICustomerMasterView.UserInformation.CountryID;
                    RequestData.StateID = _ICustomerMasterView.UserInformation.StateID;
                    RequestData.StoreID = _ICustomerMasterView.UserInformation.StoreID;
                    RequestData.POSID = _ICustomerMasterView.UserInformation.POSID;

                    RequestData.StoreCode = _ICustomerMasterView.UserInformation.StoreCode;
                    RequestData.POSCode = _ICustomerMasterView.UserInformation.POSCode;
                }                
                
                var ResponseData = _DocumentNumberingBLL.GetDocumentNoDetail(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    string BillNo = string.Empty;
                    BillNo = BillNo.ToDocumentNo(ResponseData.DocumentNumberingBillNoDetailsRecord.Prefix, ResponseData.DocumentNumberingBillNoDetailsRecord.Suffix, ResponseData.DocumentNumberingBillNoDetailsRecord.NumberOfCharacter, ResponseData.DocumentNumberingBillNoDetailsRecord.StartNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.EndNumber, ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo);

                    _ICustomerMasterView.DocumentNo = BillNo;
                    _BillNo = BillNo;
                    _ICustomerMasterView.RunningNum = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _ICustomerMasterView.DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;

                    _RunningNo = ResponseData.DocumentNumberingBillNoDetailsRecord.RunningNo;
                    _DetailID = ResponseData.DocumentNumberingBillNoDetailsRecord.DetailID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       






        public void GetStateMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStateLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CountryID = _ICustomerMasterView.CountryID;
                var ResponseData = _StateMasterBLL.SelectStateLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICustomerMasterView.StateMasterLookUp = ResponseData.StateMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCuntryMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICustomerMasterView.CountryMasterLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public void SelectDocumentNumberingRecord()
        //{
        //    try
        //    {
        //        var RequestData = new SelectDocumentNumberingBillNoDetailsRequest();
        //        RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
        //        RequestData.DocumentTypeID = (int)Enums.DocumentType.CUSTOMERMASTER;
        //        RequestData.CountryID = _ICustomerMasterView.UserCountryID;
        //        RequestData.StateID = _ICustomerMasterView.UserStateID;
        //        RequestData.StoreID = _ICustomerMasterView.UserStoreID;
        //        var ResponseData = _DocumentNumberingBLL.DocumentNumberingCustomerCodeGenerate(RequestData);
        //        _ICustomerMasterView.DocumentNo = ResponseData;



        //        //if (ResponseData. == Enums.OpStatusCode.RecordNotFound)
        //        //{
        //        //    _IDivisionView.Message = ResponseData.DisplayMessage;
        //        //}
        //        //else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
        //        //{
        //        //    _IDivisionView.Message = ResponseData.DisplayMessage;
        //        //}
        //        //_IDivisionView.ProcessStatus = ResponseData.StatusCode;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public class CustomerMasterPresenterList
        {

            ICustomerMasterCollectionViewList _ICustomerMasterCollectionViewList;

            CustomerMasterBLL _CustomerMasterBLL = new CustomerMasterBLL();


            public CustomerMasterPresenterList(ICustomerMasterCollectionViewList ViewObj)
            {

                _ICustomerMasterCollectionViewList = ViewObj;
            }


            public void GetCustomerMaster()
            {
                try
                {
                    var RequestData = new SelectAllCustomerMasterRequest();
                    RequestData.ShowInActiveRecords = true;
                    RequestData.RequestFrom = _ICustomerMasterCollectionViewList.RequestFrom;
                    RequestData.SearchString = _ICustomerMasterCollectionViewList.SearchString;
                    var ResponseData = new SelectAllCustomerMasterResponse();
                    ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _ICustomerMasterCollectionViewList._CustomerMasterList = ResponseData.CustomerMasterData;
                    }
                    else
                    {
                        _ICustomerMasterCollectionViewList._CustomerMasterList = ResponseData.CustomerMasterData;
                        _ICustomerMasterCollectionViewList.Message = ResponseData.DisplayMessage;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
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
                    RequestData.ID = _ICustomerMasterCollectionViewList.CustomerID;
                    RequestData.CustomerInfo = _ICustomerMasterCollectionViewList.CustomerSearchString;
                    RequestData.RequestFrom = Enums.RequestFrom.StoreSales;
                    var ResponseData = new SelectAllCustomerMasterResponse();

                    ResponseData = _CustomerMasterBLL.SelectAllCustomerMaster(RequestData);
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        _ICustomerMasterCollectionViewList.CustomerMasterList = ResponseData.CustomerMasterData;
                    }
                    else
                    {
                        var CustomerList = new List<CustomerMaster>();
                        _ICustomerMasterCollectionViewList.CustomerMasterList = CustomerList;
                    }
                }
                catch (Exception ex)
                {
                    var CustomerList = new List<CustomerMaster>();
                    _ICustomerMasterCollectionViewList.CustomerMasterList = CustomerList;
                    throw ex;
                }
            }

            public void SelectManagerOverride(string Source)
            {
                try
                {
                    var _ManagerOverrideBLL = new ManagerOverrideBLL();
                    var RequestData = new SelectByIDManagerOverrideRequest();
                    RequestData.ID = _ICustomerMasterCollectionViewList.ManagerOverrideID;
                    var ResponseData = _ManagerOverrideBLL.SelectManagerOverride(RequestData);
                    if (Source == "PAGELOAD")
                    {
                        _ICustomerMasterCollectionViewList.DefaultManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                    }
                    else
                    {
                        _ICustomerMasterCollectionViewList.ManagerOverrideSetting = ResponseData.ManagerOverrideRecord;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SelectCustomerRecordByPhoneNo()
        {
            try
            {
                var RequestData = new SelectCustomerByPhoneNoRequest();
                RequestData.PhoneNumber = _ICustomerMasterView.PhoneNumber;
                var ResponseData = _CustomerMasterBLL.SelectCustomerByPhoneNo(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICustomerMasterView.CustomerCode =null;
                    _ICustomerMasterView.CustomerName = null;                


                }
                else
                {
                    _ICustomerMasterView.CustomerCode = ResponseData.CustomerMaster.CustomerCode;
                    _ICustomerMasterView.CustomerName = ResponseData.CustomerMaster.CustomerName;
                    _ICustomerMasterView.PhoneNumber = ResponseData.CustomerMaster.PhoneNumber;
                    _ICustomerMasterView.AlterPhoneNumber = ResponseData.CustomerMaster.AlterPhoneNumber;
                    _ICustomerMasterView.CustomerGroupID = ResponseData.CustomerMaster.CustomerGroupID;


                    _ICustomerMasterView.BuildingAndBlockNo = ResponseData.CustomerMaster.BuildingAndBlockNo;
                    _ICustomerMasterView.StreetName = ResponseData.CustomerMaster.StreetName;
                    _ICustomerMasterView.AreaName1 = ResponseData.CustomerMaster.AreaName1;
                    _ICustomerMasterView.AreaName2 = ResponseData.CustomerMaster.AreaName2;
                    _ICustomerMasterView.City = ResponseData.CustomerMaster.City;
                    _ICustomerMasterView.CountryID = ResponseData.CustomerMaster.CountryID;
                    _ICustomerMasterView.StateID = ResponseData.CustomerMaster.StateID;
                    _ICustomerMasterView.Email = ResponseData.CustomerMaster.Email;
                    _ICustomerMasterView.DOB = ResponseData.CustomerMaster.DOB;
                    _ICustomerMasterView.BaseID = ResponseData.CustomerMaster.BaseID;
                    _ICustomerMasterView.Gender = ResponseData.CustomerMaster.Gender;
                    _ICustomerMasterView.SCN = ResponseData.CustomerMaster.SCN;
                    _ICustomerMasterView.Remarks = ResponseData.CustomerMaster.Remarks;
                    _ICustomerMasterView.CreditAmount = ResponseData.CustomerMaster.CreditAmount;
                    _ICustomerMasterView.Active = ResponseData.CustomerMaster.Active;
                    _ICustomerMasterView.OnAccountApplicable = ResponseData.CustomerMaster.OnAccountApplicable;

                    _ICustomerMasterView.Message = "Given Phone Number is already used by Customer " + ResponseData.CustomerMaster.CustomerName + "So please give another number";
                }
                              
                //else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                //{
                //    _ICustomerMasterView.Message = ResponseData.DisplayMessage;
                //}

                _ICustomerMasterView.ProcessStatus = ResponseData.StatusCode;
                //_ICustomerMasterView.Message = "Given Phone Number is already used by Customer " + ResponseData.CustomerMaster.CustomerName + "So please give another number";

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

