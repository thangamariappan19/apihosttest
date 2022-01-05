using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.SubBrand;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.Brand_Response;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
   public class SubBrandMasterPresenter
    {
       ISubBrandMasterView _ISubBrandMasterView;
        SubBrandBLL _SubBrandBLL = new SubBrandBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        public SubBrandMasterPresenter(ISubBrandMasterView ViewObj)
        {
            _ISubBrandMasterView = ViewObj;
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ISubBrandMasterView.SubBrandList.Count == 0)
            {
                _ISubBrandMasterView.Message = "Please Enter SubBrand Details";
            }
            else if (_ISubBrandMasterView.BrandName.Trim() == string.Empty)
            {
                _ISubBrandMasterView.Message = "Please Select BrandName";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveSubBrand()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveSubBrandRequest();
                    RequestData.SubBrandlist = _ISubBrandMasterView.SubBrandList;
                    var ResponseData = _SubBrandBLL.SaveSubBrand(RequestData);
                    _ISubBrandMasterView.Message = ResponseData.DisplayMessage;
                    _ISubBrandMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ISubBrandMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteSubBrand()
        {
            try
            {
                var RequestData = new DeleteSubBrandRequest();
                RequestData.ID = _ISubBrandMasterView.ID;
                var ResponseData = _SubBrandBLL.DeleteSubBrand(RequestData);
                _ISubBrandMasterView.Message = ResponseData.DisplayMessage;
                _ISubBrandMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public void SelectSubBrandListForCategory()
        {
            try
            {
                var RequestData = new SelectSubBrandListForCategoryRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.BrandID = _ISubBrandMasterView.BrandID;
                SelectSubBrandListForCategoryResponse ResponseData = _SubBrandBLL.SubBrandByBrand(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISubBrandMasterView.SubBrandList = ResponseData.SubBrandList;
                }
                else
                {
                    _ISubBrandMasterView.Message = ResponseData.DisplayMessage;
                    _ISubBrandMasterView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        public void GetBrandLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISubBrandMasterView.BrandLookUp = ResponseData.BrandList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
   public class SubBrandListPresenter
   {
       ISubBrandMasterCollectionView _ISubBrandMasterCollectionView;
       BrandBLL _BrandBLL = new BrandBLL();  

       public SubBrandListPresenter(ISubBrandMasterCollectionView ViewObj)
       {
           _ISubBrandMasterCollectionView = ViewObj;
       }

       public void GetBrandList()
       {
           try
           {
               var RequestData = new SelectAllBrandRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = new SelectAllBrandResponse();
               ResponseData = _BrandBLL.SelectAllBrandRecords(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ISubBrandMasterCollectionView.BrandList = ResponseData.BrandList;
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
}
