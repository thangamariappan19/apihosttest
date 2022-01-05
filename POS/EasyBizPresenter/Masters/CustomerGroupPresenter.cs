using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters;
using EasyBizIView.Masters.ICustomerGroupMaster;
using EasyBizRequest.Masters.CustomerGroupMasterRequest;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizResponse.Masters.CustomerGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class CustomerGroupPresenter
    {
        ICustomerGroupMasterView _ICustomerGroupMasterView;
        CustomerGroupBLL _CustomerGroupMasterBLL = new CustomerGroupBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        public CustomerGroupPresenter(ICustomerGroupMasterView ViewObj)
        {
            _ICustomerGroupMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICustomerGroupMasterView.GroupCode == string.Empty)
            {
                _ICustomerGroupMasterView.Message = "Group Code is missing Please Enter it.";
            }
            else if (_ICustomerGroupMasterView.GroupCode.Length > 8)
            {
                _ICustomerGroupMasterView.Message = "Please Enter Valid Code";
            }

            else if (_ICustomerGroupMasterView.GroupName.Trim() == string.Empty)
            {
                _ICustomerGroupMasterView.Message = "Please Enter Group Name";
            }
            //else if (_ICustomerGroupMasterView.DiscountPercentage == 0.0)
            //{
            //    _ICustomerGroupMasterView.Message = "Please Discount Percentage";
            //}
            //else if (_ICustomerGroupMasterView.PriceListID == 0)
            //{
            //    _ICustomerGroupMasterView.Message = "Please Select Price List";
            //}
            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SaveCustomerGroupMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCustomerGroupRequest();
                    RequestData.CustomerGroupMasterData = new CustomerGroupMaster();

                    RequestData.CustomerGroupMasterData.ID = _ICustomerGroupMasterView.ID;
                    RequestData.CustomerGroupMasterData.GroupCode = _ICustomerGroupMasterView.GroupCode;
                    RequestData.CustomerGroupMasterData.GroupName = _ICustomerGroupMasterView.GroupName;
                    RequestData.CustomerGroupMasterData.DiscountPercentage = _ICustomerGroupMasterView.DiscountPercentage;
                    //RequestData.CustomerGroupMasterData.PriceListID = _ICustomerGroupMasterView.PriceListID;
                    RequestData.CustomerGroupMasterData.PriceListID =0;
                    RequestData.CustomerGroupMasterData.CreateBy = 1;
                    RequestData.CustomerGroupMasterData.Remarks = _ICustomerGroupMasterView.Remarks;
                    RequestData.CustomerGroupMasterData.CreateBy = 1;
                    RequestData.CustomerGroupMasterData.Active = _ICustomerGroupMasterView.Active;

                    var ResponseData = _CustomerGroupMasterBLL.SaveCustomerGroup(RequestData);

                    _ICustomerGroupMasterView.Message = ResponseData.DisplayMessage;
                    _ICustomerGroupMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _ICustomerGroupMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
          
        }

        public void UpdateCustomerGroupMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateCustomerGroupMasterRequest();
                    RequestData.CustomerGroupMasterData = new CustomerGroupMaster();

                    RequestData.CustomerGroupMasterData.ID = Convert.ToInt32(_ICustomerGroupMasterView.ID);
                    RequestData.CustomerGroupMasterData.GroupCode = _ICustomerGroupMasterView.GroupCode;
                    RequestData.CustomerGroupMasterData.GroupName = _ICustomerGroupMasterView.GroupName;
                    RequestData.CustomerGroupMasterData.DiscountPercentage = _ICustomerGroupMasterView.DiscountPercentage;
                    RequestData.CustomerGroupMasterData.PriceListID = _ICustomerGroupMasterView.PriceListID;
                    RequestData.CustomerGroupMasterData.CreateBy = 1;
                    RequestData.CustomerGroupMasterData.SCN = _ICustomerGroupMasterView.SCN;
                    RequestData.CustomerGroupMasterData.Remarks = _ICustomerGroupMasterView.Remarks;
                    RequestData.CustomerGroupMasterData.Active = _ICustomerGroupMasterView.Active;

                    var ResponseData = _CustomerGroupMasterBLL.UpdateCustomerGroup(RequestData);

                    _ICustomerGroupMasterView.Message = ResponseData.DisplayMessage;
                    _ICustomerGroupMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _ICustomerGroupMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


        public void DeleteCustomerGroupMaster()
        {
            try
            {
                var RequestData = new DeleteCustomerGroupMasterRequest();
                RequestData.CustomerGroupMaster = new CustomerGroupMaster();

                RequestData.CustomerGroupMaster.ID = Convert.ToInt32(_ICustomerGroupMasterView.ID);


                var ResponseData = _CustomerGroupMasterBLL.DeleteCustomerGroup(RequestData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SelectCustomerGroupMaster()
        {
            try
            {
                var RequestData = new SelectByIDCustomerGroupRequest();
                RequestData.ID = _ICustomerGroupMasterView.ID;
                var ResponseData = _CustomerGroupMasterBLL.SelectByIDCustomerGroupResponse(RequestData);
                _ICustomerGroupMasterView.GroupCode = ResponseData.CustomerGroupMaster.GroupCode;
                _ICustomerGroupMasterView.GroupName = ResponseData.CustomerGroupMaster.GroupName;
                _ICustomerGroupMasterView.DiscountPercentage = ResponseData.CustomerGroupMaster.DiscountPercentage;
                _ICustomerGroupMasterView.PriceListID = ResponseData.CustomerGroupMaster.PriceListID;
                _ICustomerGroupMasterView.SCN = ResponseData.CustomerGroupMaster.SCN;

                _ICustomerGroupMasterView.Remarks = ResponseData.CustomerGroupMaster.Remarks;
                _ICustomerGroupMasterView.Active = ResponseData.CustomerGroupMaster.Active;
                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICustomerGroupMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ICustomerGroupMasterView.Message = ResponseData.DisplayMessage;
                }

                _ICustomerGroupMasterView.ProcessStatus = ResponseData.StatusCode;
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
                var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICustomerGroupMasterView.PriceListTypeLookUP = ResponseData.PriceListTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



       
    }



    public class CustomerGroupListPresenter
    {

        CustomerGroupBLL _CustomerGroupMasterBLL = new CustomerGroupBLL();
        ICustomerGroupMasterList _ICustomerGroupMasterList;

        public CustomerGroupListPresenter(ICustomerGroupMasterList ViewObj)
        {
            _ICustomerGroupMasterList = ViewObj;

        }

        public void GetCustomerGroup()
        {
            try
            {
                var RequestData = new SelectAllCustomerGroupMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCustomerGroupMasterResponse();
                ResponseData = _CustomerGroupMasterBLL.SelectAllCustomerGroupMasterResponse(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICustomerGroupMasterList.CustomerGroupMasterList = ResponseData.CustomerGroupMasterList;
                }
                else
                {
                    _ICustomerGroupMasterList.CustomerGroupMasterList = ResponseData.CustomerGroupMasterList;
                    _ICustomerGroupMasterList.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}



    


