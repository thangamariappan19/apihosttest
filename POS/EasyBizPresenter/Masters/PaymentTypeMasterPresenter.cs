using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPaymentTypeMaster;
using EasyBizIView.Masters.PaymentTypeMaster;
using EasyBizIView.Masters.State;
using EasyBizRequest.Masters.CountryRequest;
using EasyBizRequest.Masters.PaymentTypeSettingRequest;
using EasyBizResponse.Masters.PaymentTypeSettingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{


     
    public class PaymentTypeMasterPresenter
    {


        IPaymentTypeMasterView _IPaymentTypeMasterView;
        CountryBLL _CountryBLL = new CountryBLL();
        PaymentTypeMasterBLL _PaymentTypeMasterBLL = new PaymentTypeMasterBLL();
        

        public PaymentTypeMasterPresenter(IPaymentTypeMasterView ViewObj)
        {
            _IPaymentTypeMasterView=ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IPaymentTypeMasterView.PaymentCode == string.Empty)
            {
                _IPaymentTypeMasterView.Message = "Payment Code is missing Please Enter it.";
            }
            else if (_IPaymentTypeMasterView.PaymentCode.Length > 15)
            {
                _IPaymentTypeMasterView.Message = "Please Enter Valid Code";
            }
            else if (_IPaymentTypeMasterView.PaymentName == string.Empty)
            {
                _IPaymentTypeMasterView.Message = "Payment Name is missing Please Enter it.";
            }
            else if (_IPaymentTypeMasterView.PaymentType == string.Empty)
            {
                _IPaymentTypeMasterView.Message = "PaymentType is missing Please Enter it.";
            }
            else if (_IPaymentTypeMasterView.PaymentImage == null)
            {
                _IPaymentTypeMasterView.Message = "Payment Image Is Missings.";
            }
            else if (_IPaymentTypeMasterView.IsCountryNeed==false)
            {
                if (_IPaymentTypeMasterView.CountryID == 0)
                {
                    _IPaymentTypeMasterView.Message = "Country Is Missing";
                }
                else
                {
                    objBool = true;
                }
            }
            //else if (_IPaymentTypeMasterView.CountType == string.Empty)
            //{
            //    _IPaymentTypeMasterView.Message = "CountType is missing Please Enter it.";
            //}   

            else
            {
                objBool = true;
            }
            return objBool;
        }


        public void SavePaymentTypeMaster()
        {
            try
            {
                if (IsValidForm())
                {

                    var RequestData = new SavePaymentTypeRequest();
                    RequestData.PaymentTypeMasterData = new PaymentTypeMasterType();

                    RequestData.PaymentTypeMasterData.ID = _IPaymentTypeMasterView.ID;
                    RequestData.PaymentTypeMasterData.PaymentCode = _IPaymentTypeMasterView.PaymentCode;
                    RequestData.PaymentTypeMasterData.PaymentName = _IPaymentTypeMasterView.PaymentName;
                    RequestData.PaymentTypeMasterData.PaymentType = _IPaymentTypeMasterView.PaymentType;
                    RequestData.PaymentTypeMasterData.CountRequired = _IPaymentTypeMasterView.CountRequired;
                    RequestData.PaymentTypeMasterData.CountType = _IPaymentTypeMasterView.CountType;
                    RequestData.PaymentTypeMasterData.CountryID = _IPaymentTypeMasterView.CountryID;                   
                    RequestData.PaymentTypeMasterData.CountryCode = _IPaymentTypeMasterView.CountryCode;
                    RequestData.PaymentTypeMasterData.IsCountryNeed = _IPaymentTypeMasterView.IsCountryNeed;
                    RequestData.PaymentTypeMasterData.Refundable = _IPaymentTypeMasterView.Refundable;
                    RequestData.PaymentTypeMasterData.RequiredManageApproval = _IPaymentTypeMasterView.RequiredManageApproval;
                    RequestData.PaymentTypeMasterData.OpenCashDraw = _IPaymentTypeMasterView.OpenCashDraw;
                    RequestData.PaymentTypeMasterData.AllowOverTender = _IPaymentTypeMasterView.AllowOverTender;
                    RequestData.PaymentTypeMasterData.AllowPartialTender = _IPaymentTypeMasterView.AllowPartialTender;
                    RequestData.PaymentTypeMasterData.CreateBy = 1;
                    RequestData.PaymentTypeMasterData.SCN = _IPaymentTypeMasterView.SCN;
                    RequestData.PaymentTypeMasterData.Active = _IPaymentTypeMasterView.Active;
                    RequestData.PaymentTypeMasterData.Remarks = _IPaymentTypeMasterView.Remarks;
                    RequestData.PaymentTypeMasterData.PaymentImage = _IPaymentTypeMasterView.PaymentImage;
                    RequestData.PaymentTypeMasterData.SortOrder = _IPaymentTypeMasterView.SortOrder;
                    RequestData.PaymentTypeMasterData.PaymentReceivedType = _IPaymentTypeMasterView.PaymentReceivedType;

                    var ResponseData = _PaymentTypeMasterBLL.SavePaymentType(RequestData);

                    _IPaymentTypeMasterView.Message = ResponseData.DisplayMessage;
                    _IPaymentTypeMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IPaymentTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }


        public void UpdatePaymentTypeMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdatePaymentTypeRequest();
                    RequestData.PaymentTypeMasterData = new PaymentTypeMasterType();

                    RequestData.PaymentTypeMasterData.ID = _IPaymentTypeMasterView.ID;
                    RequestData.PaymentTypeMasterData.PaymentCode = _IPaymentTypeMasterView.PaymentCode;
                    RequestData.PaymentTypeMasterData.PaymentName = _IPaymentTypeMasterView.PaymentName;
                    RequestData.PaymentTypeMasterData.PaymentType = _IPaymentTypeMasterView.PaymentType;
                    RequestData.PaymentTypeMasterData.CountRequired = _IPaymentTypeMasterView.CountRequired;
                    RequestData.PaymentTypeMasterData.CountType = _IPaymentTypeMasterView.CountType;
                    RequestData.PaymentTypeMasterData.CountryID = _IPaymentTypeMasterView.CountryID;
                    RequestData.PaymentTypeMasterData.CountryCode = _IPaymentTypeMasterView.CountryCode;
                    RequestData.PaymentTypeMasterData.IsCountryNeed = _IPaymentTypeMasterView.IsCountryNeed;
                    RequestData.PaymentTypeMasterData.Refundable = _IPaymentTypeMasterView.Refundable;
                    RequestData.PaymentTypeMasterData.RequiredManageApproval = _IPaymentTypeMasterView.RequiredManageApproval;
                    RequestData.PaymentTypeMasterData.OpenCashDraw = _IPaymentTypeMasterView.OpenCashDraw;
                    RequestData.PaymentTypeMasterData.AllowOverTender = _IPaymentTypeMasterView.AllowOverTender;
                    RequestData.PaymentTypeMasterData.AllowPartialTender = _IPaymentTypeMasterView.AllowPartialTender;
                    RequestData.PaymentTypeMasterData.CreateBy = 1;
                    RequestData.PaymentTypeMasterData.SCN = _IPaymentTypeMasterView.SCN;
                    RequestData.PaymentTypeMasterData.Active = _IPaymentTypeMasterView.Active;
                    RequestData.PaymentTypeMasterData.IsPaymentProcesser = _IPaymentTypeMasterView.PaymentProcesser;
                    RequestData.PaymentTypeMasterData.Remarks = _IPaymentTypeMasterView.Remarks;
                    RequestData.PaymentTypeMasterData.PaymentImage = _IPaymentTypeMasterView.PaymentImage;
                    RequestData.PaymentTypeMasterData.SortOrder = _IPaymentTypeMasterView.SortOrder;
                    RequestData.PaymentTypeMasterData.PaymentReceivedType = _IPaymentTypeMasterView.PaymentReceivedType;

                    var ResponseData = _PaymentTypeMasterBLL.UpdatePaymentType(RequestData);

                    _IPaymentTypeMasterView.Message = ResponseData.DisplayMessage;
                    _IPaymentTypeMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IPaymentTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }


        public void DeletePaymentTypeMaster()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new DeletePaymentTypeRequest();
                    RequestData.PaymentTypeMasterData = new PaymentTypeMasterType();

                    RequestData.PaymentTypeMasterData.ID = _IPaymentTypeMasterView.ID;


                    var ResponseData = _PaymentTypeMasterBLL.DeletePaymentType(RequestData);

                    _IPaymentTypeMasterView.Message = ResponseData.DisplayMessage;
                    _IPaymentTypeMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IPaymentTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public void SelectByIDPaymentTypeMaster()
        {
            try
            {
                var RequestData = new SelectByIDPaymentTypeRequest();
                RequestData.ID = _IPaymentTypeMasterView.ID;
                var ResponseData = _PaymentTypeMasterBLL.SelectByIDPaymentType(RequestData);

                _IPaymentTypeMasterView.ID = ResponseData.PaymentTypeMasterData.ID;
                _IPaymentTypeMasterView.PaymentCode = ResponseData.PaymentTypeMasterData.PaymentCode;
                _IPaymentTypeMasterView.PaymentName = ResponseData.PaymentTypeMasterData.PaymentName;
                _IPaymentTypeMasterView.PaymentType = ResponseData.PaymentTypeMasterData.PaymentType;
                _IPaymentTypeMasterView.CountRequired = ResponseData.PaymentTypeMasterData.CountRequired;
                _IPaymentTypeMasterView.CountType = ResponseData.PaymentTypeMasterData.CountType;
                _IPaymentTypeMasterView.CountryID = ResponseData.PaymentTypeMasterData.CountryID;              
                _IPaymentTypeMasterView.IsCountryNeed = ResponseData.PaymentTypeMasterData.IsCountryNeed;
                _IPaymentTypeMasterView.Refundable = ResponseData.PaymentTypeMasterData.Refundable;
                _IPaymentTypeMasterView.RequiredManageApproval = ResponseData.PaymentTypeMasterData.RequiredManageApproval;
                _IPaymentTypeMasterView.OpenCashDraw = ResponseData.PaymentTypeMasterData.OpenCashDraw;
                _IPaymentTypeMasterView.AllowOverTender = ResponseData.PaymentTypeMasterData.AllowOverTender;
                _IPaymentTypeMasterView.AllowPartialTender = ResponseData.PaymentTypeMasterData.AllowPartialTender;
                _IPaymentTypeMasterView.SCN = ResponseData.PaymentTypeMasterData.SCN;
                _IPaymentTypeMasterView.Active = ResponseData.PaymentTypeMasterData.Active;
                _IPaymentTypeMasterView.PaymentProcesser = ResponseData.PaymentTypeMasterData.IsPaymentProcesser;
                _IPaymentTypeMasterView.SortOrder = ResponseData.PaymentTypeMasterData.SortOrder;
                _IPaymentTypeMasterView.PaymentReceivedType = ResponseData.PaymentTypeMasterData.PaymentReceivedType;
                _IPaymentTypeMasterView.Remarks = ResponseData.PaymentTypeMasterData.Remarks;
                _IPaymentTypeMasterView.PaymentImage = ResponseData.PaymentTypeMasterData.PaymentImage;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IPaymentTypeMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IPaymentTypeMasterView.Message = ResponseData.DisplayMessage;
                }

                _IPaymentTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetCountryLookUp()
        {
            try
            {
                var RequestData = new SelectCountryLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CountryBLL.SelectCountryLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPaymentTypeMasterView.CountryLookUp = ResponseData.CountryMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }


    public class PaymentTypeMasterPresenterList
    {


        IPaymentTypeMasterList _IPaymentTypeMasterList;

        PaymentTypeMasterBLL _PaymentTypeMasterBLL = new PaymentTypeMasterBLL();

        public PaymentTypeMasterPresenterList(IPaymentTypeMasterList ViewObj)
        {
            _IPaymentTypeMasterList = ViewObj;
        }

       


        public void GetPaymentTypeList()
        {
            try
            {
                var RequestData = new SelectAllPaymentTypeRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllPaymentTypeResponse();
                ResponseData = _PaymentTypeMasterBLL.SelectAllPaymentType(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPaymentTypeMasterList._PaymentTypeMasterList = ResponseData.PaymentTypeMasterList;
                }
                else
                {
                    _IPaymentTypeMasterList._PaymentTypeMasterList = ResponseData.PaymentTypeMasterList;
                    _IPaymentTypeMasterList.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           


        }
    }
}
