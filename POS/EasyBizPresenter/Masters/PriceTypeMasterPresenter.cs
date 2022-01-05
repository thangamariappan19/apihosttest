using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IPriceTypeMaster;
using EasyBizRequest.Masters.PriceTypeMasterResponse;
using EasyBizRequest.Masters.PriceTypeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class PriceTypeMasterPresenter
    {
        IPriceTypeView _IPriceTypeView;
        PriceTypeBLL _PriceTypeBLL = new PriceTypeBLL();
        public PriceTypeMasterPresenter(IPriceTypeView ViewObj)
        {
            _IPriceTypeView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IPriceTypeView.PriceCode.Trim() == string.Empty)
            {
                _IPriceTypeView.Message = "Price Code is missing Please Enter it.";
            }
            else if (_IPriceTypeView.PriceCode.Length > 8)
            {
                _IPriceTypeView.Message = "Please Enter Valid Code";
            }
            else if (_IPriceTypeView.PriceName.Trim() == string.Empty)
            {
                _IPriceTypeView.Message = "Name is missing Please Enter it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SavePriceTypeMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SavePriceTypeRequest();
                    RequestData.PriceTypesRecord = new PriceTypeMasterTypes();

                    RequestData.PriceTypesRecord.ID = _IPriceTypeView.ID;
                    RequestData.PriceTypesRecord.PriceCode = _IPriceTypeView.PriceCode;
                    RequestData.PriceTypesRecord.PriceName = _IPriceTypeView.PriceName;
                    RequestData.PriceTypesRecord.CreateBy = 1;
                    RequestData.PriceTypesRecord.Remarks = _IPriceTypeView.Remarks;
                   

                    var ResponseData = _PriceTypeBLL.SavePriceType(RequestData);
                    _IPriceTypeView.Message = ResponseData.DisplayMessage;
                    _IPriceTypeView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPriceTypeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public void UpdatePriceTypeMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdatePriceTypeRequest();
                    RequestData.PriceTypeData = new PriceTypeMasterTypes();
                    RequestData.PriceTypeData.ID = _IPriceTypeView.ID;
                    RequestData.PriceTypeData.PriceCode = _IPriceTypeView.PriceCode;
                    RequestData.PriceTypeData.PriceName = _IPriceTypeView.PriceName;
                    RequestData.PriceTypeData.UpdateBy = 1;
                    RequestData.PriceTypeData.SCN = _IPriceTypeView.SCN;
                    RequestData.PriceTypeData.Remarks = _IPriceTypeView.Remarks;
                    RequestData.PriceTypeData.Active = _IPriceTypeView.Active;


                    var ResponseData = _PriceTypeBLL.UpdatePriceType(RequestData);
                    _IPriceTypeView.Message = ResponseData.DisplayMessage;
                    _IPriceTypeView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPriceTypeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeletePriceTypeMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new DeletePriceTypeRequest();

                    RequestData.ID = _IPriceTypeView.ID;
                    var ResponseData = _PriceTypeBLL.DeletePriceType(RequestData);
                    _IPriceTypeView.Message = ResponseData.DisplayMessage;
                    _IPriceTypeView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _IPriceTypeView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }


        public void SelectByIDPriceTypeMaster()
        {
            try
            {
                var RequestData = new SelectByIDPriceTypeRequest();
                RequestData.ID = _IPriceTypeView.ID;
                var ResponseData = _PriceTypeBLL.SelectByIDPriceType(RequestData);
                _IPriceTypeView.PriceCode = ResponseData.PriceTypeMasterData.PriceCode;
                _IPriceTypeView.PriceName = ResponseData.PriceTypeMasterData.PriceName;
                _IPriceTypeView.SCN = ResponseData.PriceTypeMasterData.SCN;
                _IPriceTypeView.Remarks = ResponseData.PriceTypeMasterData.Remarks;
                _IPriceTypeView.Active = ResponseData.PriceTypeMasterData.Active;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IPriceTypeView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IPriceTypeView.Message = ResponseData.DisplayMessage;
                }

                _IPriceTypeView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }


    public class PriceTypeViewPresenterList
    {
        IPriceTypeViewList _IPriceTypeViewList;
        PriceTypeBLL _PriceTypeBLL = new PriceTypeBLL();
        public PriceTypeViewPresenterList(IPriceTypeViewList ViewObj)
        {
            _IPriceTypeViewList = ViewObj;
        }

        public void SelectAllPriceTypeMaster()
        {
            try
            {
                var RequestData = new SelectAllPriceTypeRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllPriceTypeResponse();

                ResponseData = _PriceTypeBLL.SelectAllPriceType(RequestData);


                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPriceTypeViewList.PriceTypeMasterTypesList = ResponseData.PriceTypeMasterList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {
                    _IPriceTypeViewList.PriceTypeMasterTypesList = ResponseData.PriceTypeMasterList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
