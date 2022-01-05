using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizResponse.Masters.ProductLineMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class ProductLineMasterPresenter
    {
        IProductLineMasterView _IProductLineMasterView;
        ProductLineMasterBLL _ProductLineMasterBLL = new ProductLineMasterBLL();

        public ProductLineMasterPresenter(IProductLineMasterView ViewObj)
        {
            _IProductLineMasterView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IProductLineMasterView.ProductLineCode.Trim() == string.Empty)
            {
                _IProductLineMasterView.Message = " Code is missing Please Enter it.";
            }           
            else if (_IProductLineMasterView.ProductLineName.Trim() == string.Empty)
            {
                _IProductLineMasterView.Message = "Please Enter ProducLine Name";
            }
            else if (_IProductLineMasterView.Description.Trim() == string.Empty)
            {
                _IProductLineMasterView.Message = "Please Give Description";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveProductLineMaster()
        {
            if (IsValidForm())
            {
                var RequestData = new SaveProductLineMasterRequest();
                RequestData.ProductLineMasterData = new ProductLineMaster();

                RequestData.ProductLineMasterData.ID = _IProductLineMasterView.ID;
                RequestData.ProductLineMasterData.ProductLineCode = _IProductLineMasterView.ProductLineCode;
                RequestData.ProductLineMasterData.ProductLineName = _IProductLineMasterView.ProductLineName;
                RequestData.ProductLineMasterData.Description = _IProductLineMasterView.Description;
                RequestData.ProductLineMasterData.CreateBy = _IProductLineMasterView.UserID;
                RequestData.ProductLineMasterData.Active = _IProductLineMasterView.Active;      
               // RequestData.ProductLineMasterData.CreateOn = DateTime.Now;
               
                RequestData.ProductLineMasterData.SCN = _IProductLineMasterView.SCN;
                SaveProductLineMasterResponse ResponseData = _ProductLineMasterBLL.SaveProductLineMaster(RequestData);
                _IProductLineMasterView.Message = ResponseData.DisplayMessage;
                _IProductLineMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IProductLineMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void UpdateProductLineMaster()
        {
            if (IsValidForm())
            {

                var RequestData = new UpdateProductLineMasterRequest();
                RequestData.ProductLineMasterData = new ProductLineMaster();
                RequestData.ProductLineMasterData.ID = _IProductLineMasterView.ID;
                RequestData.ProductLineMasterData.ProductLineCode = _IProductLineMasterView.ProductLineCode;
                RequestData.ProductLineMasterData.ProductLineName = _IProductLineMasterView.ProductLineName;
                RequestData.ProductLineMasterData.Description = _IProductLineMasterView.Description;
                RequestData.ProductLineMasterData.UpdateBy = _IProductLineMasterView.UserID;
                RequestData.ProductLineMasterData.UpdateOn = DateTime.Now;
                RequestData.ProductLineMasterData.Active = _IProductLineMasterView.Active;      
                RequestData.ProductLineMasterData.SCN = _IProductLineMasterView.SCN;
                var ResponseData = _ProductLineMasterBLL.UpdateProductLineMaster(RequestData);
                _IProductLineMasterView.Message = ResponseData.DisplayMessage;
                _IProductLineMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IProductLineMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteProductLineMaster()
        {

            var RequestData = new DeleteProductLineMasterRequest();
            RequestData.ID = _IProductLineMasterView.ID;
            var ResponseData = _ProductLineMasterBLL.DeleteProductLineMaster(RequestData);
            _IProductLineMasterView.Message = ResponseData.DisplayMessage;
            _IProductLineMasterView.ProcessStatus = ResponseData.StatusCode;
        }

        public void SelectProductLineMasterRecord()
        {


            var RequestData = new SelectByIDProductLineMasterRequest();
            RequestData.ID = _IProductLineMasterView.ID;
            var ResponseData = _ProductLineMasterBLL.SelectProductLineMasterRecord(RequestData);
            _IProductLineMasterView.ProductLineCode = ResponseData.ProductLineMasterRecord.ProductLineCode;
            _IProductLineMasterView.ProductLineName = ResponseData.ProductLineMasterRecord.ProductLineName;
            _IProductLineMasterView.Description = ResponseData.ProductLineMasterRecord.Description;
            _IProductLineMasterView.Active = ResponseData.ProductLineMasterRecord.Active;
            _IProductLineMasterView.SCN = ResponseData.ProductLineMasterRecord.SCN;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IProductLineMasterView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IProductLineMasterView.Message = ResponseData.DisplayMessage;
            }

            _IProductLineMasterView.ProcessStatus = ResponseData.StatusCode;
        }
    }

    public class ProductLineMasterListPresenter
    {
        ProductLineMasterBLL _ProductLineMasterBLL = new ProductLineMasterBLL();
        IProductLineMasterList _IProductLineMasterList;

        public ProductLineMasterListPresenter(IProductLineMasterList ViewObj)
        {
            _IProductLineMasterList = ViewObj;
        }

        public void GetProductLineMasterList()
        {

            var RequestData = new SelectAllProductLineMasterRequest();
            RequestData.ShowInActiveRecords = true;

            var ResponseData = new SelectAllProductLineMasterResponse();
            ResponseData = _ProductLineMasterBLL.SelectAllProductLineMaster(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IProductLineMasterList.ProductLineMasterList = ResponseData.ProductLineMasterList;
            }
            else
            {
                //_IMASCompanyList.Message = ResponseData.DisplayMessage;
            }

        }
    }
}
