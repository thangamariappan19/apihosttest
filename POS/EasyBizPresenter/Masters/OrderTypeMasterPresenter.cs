using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IOrderTypeMaster;
using EasyBizRequest.Masters.OrderTypeMasterRequest;
using EasyBizResponse.Masters.OrderTypeMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class OrderTypeMasterPresenter
    {
          IOrderTypeMasterView _IOrderTypeMasterView;
         OrderTypeMasterBLL _OrderTypeMasterBLL = new OrderTypeMasterBLL();

        public OrderTypeMasterPresenter(IOrderTypeMasterView ViewObj)
        {
            _IOrderTypeMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IOrderTypeMasterView.OrderTypeCode.Trim() == string.Empty)
            {
                _IOrderTypeMasterView.Message = "OrderTypeCode is missing Please Enter it.";
            }
            else if (_IOrderTypeMasterView.OrderTypeName.Trim() == string.Empty)
            {
                _IOrderTypeMasterView.Message = "Please Enter OrderTypeName";
            }
            //else if (_IOrderTypeMasterView.Description.Trim() == string.Empty)
            //{
            //    _IOrderTypeMasterView.Message = "Please Give Description";
            //}

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveOrderTypeMaster()
        {
            try

        {
            if (IsValidForm())
            {
                var RequestData = new SaveOrderTypeMasterRequest();
                RequestData.OrderTypeMasterData = new OrderTypeMaster();

                RequestData.OrderTypeMasterData.ID = _IOrderTypeMasterView.ID;
                RequestData.OrderTypeMasterData.OrderTypeCode = _IOrderTypeMasterView.OrderTypeCode;
                RequestData.OrderTypeMasterData.OrderTypeName = _IOrderTypeMasterView.OrderTypeName;                
                RequestData.OrderTypeMasterData.Description = _IOrderTypeMasterView.Description;
                RequestData.OrderTypeMasterData.CreateBy = _IOrderTypeMasterView.UserID;     
                //RequestData.OrderTypeMasterData.CreateBy = _IOrderTypeMasterView.CreateBy;                               
               // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
                RequestData.OrderTypeMasterData.Active = _IOrderTypeMasterView.Active;  
                RequestData.OrderTypeMasterData.SCN = _IOrderTypeMasterView.SCN;
                SaveOrderTypeMasterResponse ResponseData = _OrderTypeMasterBLL.SaveOrderTypeMaster(RequestData);
                _IOrderTypeMasterView.Message = ResponseData.DisplayMessage;
                _IOrderTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IOrderTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOrderTypeMaster()
        {
            try
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateOrderTypeMasterRequest();
                RequestData.OrderTypeMasterData = new OrderTypeMaster();
                RequestData.OrderTypeMasterData.ID = _IOrderTypeMasterView.ID;
                RequestData.OrderTypeMasterData.OrderTypeCode = _IOrderTypeMasterView.OrderTypeCode;
                RequestData.OrderTypeMasterData.OrderTypeName = _IOrderTypeMasterView.OrderTypeName;                
                RequestData.OrderTypeMasterData.Description = _IOrderTypeMasterView.Description;
                RequestData.OrderTypeMasterData.UpdateBy = _IOrderTypeMasterView.UserID;
                //RequestData.OrderTypeMasterData.UpdateOn = DateTime.Now;
                RequestData.OrderTypeMasterData.Active = _IOrderTypeMasterView.Active;  
                RequestData.OrderTypeMasterData.SCN = _IOrderTypeMasterView.SCN;
                var ResponseData = _OrderTypeMasterBLL.UpdateOrderTypeMaster(RequestData);
                _IOrderTypeMasterView.Message = ResponseData.DisplayMessage;
                _IOrderTypeMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IOrderTypeMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOrderTypeMaster()
        {
            try
        {

            var RequestData = new DeleteOrderTypeMasterRequest ();
            RequestData.ID = _IOrderTypeMasterView.ID;
            var ResponseData = _OrderTypeMasterBLL.DeleteOrderTypeMaster(RequestData);
            _IOrderTypeMasterView.Message = ResponseData.DisplayMessage;
            _IOrderTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SelectOrderTypeMasterRecord()
        {


            var RequestData = new SelectByIDOrderTypeMasterRequest();
            RequestData.ID = _IOrderTypeMasterView.ID;

            var ResponseData = _OrderTypeMasterBLL.SelectOrderTypeMasterRecord(RequestData);
            _IOrderTypeMasterView.OrderTypeCode = ResponseData.OrderTypeMasterRecord.OrderTypeCode;
            _IOrderTypeMasterView.OrderTypeName = ResponseData.OrderTypeMasterRecord.OrderTypeName;
            _IOrderTypeMasterView.Description = ResponseData.OrderTypeMasterRecord.Description;
            _IOrderTypeMasterView.Active = ResponseData.OrderTypeMasterRecord.Active;
            _IOrderTypeMasterView.SCN = ResponseData.OrderTypeMasterRecord.SCN;
            
            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IOrderTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IOrderTypeMasterView.Message = ResponseData.DisplayMessage;
            }

            _IOrderTypeMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }
    public class OrderTypeMasterListPresenter
    {

        OrderTypeMasterBLL _OrderTypeMasterBLL = new OrderTypeMasterBLL();
        
        IOrderTypeMasterList _IOrderTypeMasterList;

        public OrderTypeMasterListPresenter(IOrderTypeMasterList ViewObj)
        {
            _IOrderTypeMasterList = ViewObj;
        }

        public void GetOrderTypeMasterList()
        {

            var RequestData = new SelectAllOrderTypeMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllOrderTypeMasterResponse();
            ResponseData = _OrderTypeMasterBLL.SelectAllOrderTypeMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IOrderTypeMasterList.OrderTypeMasterList = ResponseData.OrderTypeMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }
 
}
