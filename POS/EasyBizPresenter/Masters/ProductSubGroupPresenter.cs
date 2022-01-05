using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Masters.IProductSubGroup;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class ProductSubGroupPresenter
    {
        IProductSubGroupView _IProductSubGroupView;
        ProductSubGroupBLL _ProductSubGroupBLL = new ProductSubGroupBLL();
        ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();
        public ProductSubGroupPresenter(IProductSubGroupView ViewObj)
        {
            _IProductSubGroupView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IProductSubGroupView.ProductSubGroupList.Count == 0)
            {
                _IProductSubGroupView.Message = "Please Enter ProductSubGroup Details";
            }
            else if (_IProductSubGroupView.ProductGroupName.Trim() == string.Empty)
            {
                _IProductSubGroupView.Message = "Please Select ProductGroupName";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveProductSubGroup()
        {
            if (IsValidForm())
            {
                var RequestData = new SaveProductSubGroupRequest();
                RequestData.ProductSubGrouplist = _IProductSubGroupView.ProductSubGroupList;                               
                var ResponseData = _ProductSubGroupBLL.SaveProductSubGroup(RequestData);
                _IProductSubGroupView.Message = ResponseData.DisplayMessage;
                _IProductSubGroupView.ProcessStatus = ResponseData.StatusCode;
            }
            else
            {
                _IProductSubGroupView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }
        public void DeleteProductSubGroup()
        {
            var RequestData = new DeleteProductSubGroupRequest();
            RequestData.ID = _IProductSubGroupView.ID;
            var ResponseData = _ProductSubGroupBLL.DeleteProductSubGroup(RequestData);
            _IProductSubGroupView.Message = ResponseData.DisplayMessage;
            _IProductSubGroupView.ProcessStatus = ResponseData.StatusCode;
        }
       
        public void SelectProductSubGroupListForProductGroup()
        {
            var RequestData = new SelectProductGroupListForProductSubGroupRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.ProductGroupID = _IProductSubGroupView.ProductGroupID;
            SelectProductGroupListForProductSubGroupResponse ResponseData = _ProductSubGroupBLL.ProductSubGroupByProductGroup(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IProductSubGroupView.ProductSubGroupList = ResponseData.ProductSubGroupList;
            }
            else
            {
                _IProductSubGroupView.ProductSubGroupList = new List<EasyBizDBTypes.Masters.ProductSubGroupMaster>() ;
                _IProductSubGroupView.Message = ResponseData.DisplayMessage;
                _IProductSubGroupView.ProcessStatus = ResponseData.StatusCode;
            }
        }       
        public void GetProductGroupLookUp()
        {
            var RequestData = new SelectProductGroupLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            var ResponseData = _ProductGroupBLL.ProductGroupLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IProductSubGroupView.ProductGroupLookUp = ResponseData.ProductGroupList;
            }
        }
    }
   public class ProductSubGroupListPresenter
   {
       IProductSubGroupCollectionView _IProductSubGroupCollectionView;
       ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();

       public ProductSubGroupListPresenter(IProductSubGroupCollectionView ViewObj)
       {
           _IProductSubGroupCollectionView = ViewObj;
       }

       public void GetProductList()
       {
           var RequestData = new SelectAllProductGroupRequest();
           RequestData.ShowInActiveRecords = true;
           var ResponseData = new SelectAllProductGroupResponse();
           ResponseData = _ProductGroupBLL.SelectAllProductGroup(RequestData);
           if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
           {
               _IProductSubGroupCollectionView.ProductGroupList = ResponseData.ProductGroupList;
           }
           else
           {

           }
       }
   }
}
