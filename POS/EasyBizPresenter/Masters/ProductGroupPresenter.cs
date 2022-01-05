using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ProductGroup;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class ProductGroupPresenter
    {
       IProductGroupView _IProductGroupView;
       ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();

       public ProductGroupPresenter(IProductGroupView ViewObj)
        {
            _IProductGroupView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_IProductGroupView.ProductGroupCode.Trim() == string.Empty)
           {
               _IProductGroupView.Message = "ProductGroup Code is missing Please Enter it.";
           }
           else if (_IProductGroupView.ProductGroupCode.Length > 8)
           {
               _IProductGroupView.Message = " Please Enter Vaild Code.";
           }
           else if (_IProductGroupView.ProductGroupCode.Length < 2)
           {
               _IProductGroupView.Message = " Product Group Code must have 2 characters";
           }
           else if (_IProductGroupView.ProductGroupName.Trim() == string.Empty)
           {
               _IProductGroupView.Message = "ProductGroup Name is missing Please Enter it. ";
           }
           else if (_IProductGroupView.Description == null)
           {
               _IProductGroupView.Message = "Description is missing Please Enter it.";
           }          
           else
           {
               objBool = true;
           }
           return objBool;
       }
       public void SaveProductGroup()
       {
           try
       {
           if (IsValidForm())
           {
               var RequestData = new SaveProductGroupRequest();
               RequestData.ProductGroupRecord = new ProductGroupMaster();

               RequestData.ProductGroupRecord.ID = _IProductGroupView.ID;
               RequestData.ProductGroupRecord.ProductGroupCode = _IProductGroupView.ProductGroupCode;
               RequestData.ProductGroupRecord.ProductGroupName = _IProductGroupView.ProductGroupName;
               RequestData.ProductGroupRecord.Description = _IProductGroupView.Description;               
               RequestData.ProductGroupRecord.CreateBy = _IProductGroupView.UserID;
               RequestData.ProductGroupRecord.CreateOn = DateTime.Now;
               RequestData.ProductGroupRecord.Active = _IProductGroupView.Active;
               RequestData.ProductGroupRecord.SCN = _IProductGroupView.SCN;

               var ResponseData = _ProductGroupBLL.SaveProductGroup(RequestData);

               _IProductGroupView.Message = ResponseData.DisplayMessage;
               _IProductGroupView.ProcessStatus = ResponseData.StatusCode;
           }
           else
           {
               _IProductGroupView.ProcessStatus = Enums.OpStatusCode.GeneralError;
           }
       }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void UpdateProductGroup()
       {
           try
       {
           if (IsValidForm())
           {
               var RequestData = new UpdateProductGroupRequest();
               RequestData.ProductGroupRecord = new ProductGroupMaster();
               RequestData.ProductGroupRecord.ID = _IProductGroupView.ID;
               RequestData.ProductGroupRecord.ProductGroupCode = _IProductGroupView.ProductGroupCode;
               RequestData.ProductGroupRecord.ProductGroupName = _IProductGroupView.ProductGroupName;
               RequestData.ProductGroupRecord.Description = _IProductGroupView.Description;
               RequestData.ProductGroupRecord.UpdateBy = _IProductGroupView.UserID;
               RequestData.ProductGroupRecord.UpdateOn = DateTime.Now;
               RequestData.ProductGroupRecord.Active = _IProductGroupView.Active;
               RequestData.ProductGroupRecord.SCN = _IProductGroupView.SCN;

               var ResponseData = _ProductGroupBLL.UpdateProductGroup(RequestData);

               _IProductGroupView.Message = ResponseData.DisplayMessage;
               _IProductGroupView.ProcessStatus = ResponseData.StatusCode;
           }
           else
           {
               _IProductGroupView.ProcessStatus = Enums.OpStatusCode.GeneralError;
           }
       }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteProductGroup()
       {
           try
       {
           var RequestData = new DeleteProductGroupRequest();
           RequestData.ID = _IProductGroupView.ID;
           var ResponseData = _ProductGroupBLL.DeleteProductGroup(RequestData);
           _IProductGroupView.Message = ResponseData.DisplayMessage;
           _IProductGroupView.ProcessStatus = ResponseData.StatusCode;
       }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    
    public void SelectProductGroup()
    {
        try
    {
            var RequestData = new SelectByIDProductGroupRequest();
            RequestData.ID = _IProductGroupView.ID;
            var ResponseData = _ProductGroupBLL.SelectProductGroup(RequestData);
            _IProductGroupView.ProductGroupCode = ResponseData.ProductGroupData.ProductGroupCode;
            _IProductGroupView.ProductGroupName = ResponseData.ProductGroupData.ProductGroupName;
            _IProductGroupView.Description = ResponseData.ProductGroupData.Description;
            _IProductGroupView.Active = ResponseData.ProductGroupData.Active;      

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IProductGroupView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IProductGroupView.Message = ResponseData.DisplayMessage;
            }

            _IProductGroupView.ProcessStatus = ResponseData.StatusCode;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
        public void SelectAllProductGroup()
        {
            var RequestData = new SelectAllProductGroupRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = _ProductGroupBLL.SelectAllProductGroup(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IProductGroupView.ProductGroupMasterList = ResponseData.ProductGroupList;
            }
            else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
            {
                _IProductGroupView.Message = ResponseData.DisplayMessage;
            }
        }
    }
}

public class ProductGroupListPresenter
{
    IProductGroupList _IProductGroupList;
    ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();

    public ProductGroupListPresenter(IProductGroupList ViewObj)
    {
        _IProductGroupList = ViewObj;
    }
    public void GetProductGroupList()
    {
        var RequestData = new SelectAllProductGroupRequest();
        RequestData.ShowInActiveRecords = true;
        var ResponseData = new SelectAllProductGroupResponse();
        ResponseData = _ProductGroupBLL.SelectAllProductGroup(RequestData);
        if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
        {
            _IProductGroupList.ProductGroupMasterList = ResponseData.ProductGroupList;
        }
        else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
        {

        }
    }
}

