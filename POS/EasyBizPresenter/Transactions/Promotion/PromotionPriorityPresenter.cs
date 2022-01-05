using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizIView.Transactions.IPromotionPriority;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Masters.StoreGroupRequest;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionPriority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.Promotion
{
    public class PromotionPriorityPresenter
    {
        IPromotionPriorityView _IPromotionPriorityView;
        PromotionsMasterBLL _PromotionsMasterBLL = new PromotionsMasterBLL();

        PromotionPriorityBLL _PromotionPriorityBLL = new PromotionPriorityBLL();
        StoreGroupBLL _StoreGroupBLL = new StoreGroupBLL();
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        PriceListBLL _PriceListBLL = new PriceListBLL();
        public PromotionPriorityPresenter(IPromotionPriorityView ViewObj)
        {
            _IPromotionPriorityView = ViewObj;
        }


        public void SelectPromotionsRecord()
        {
            try
            {
                var RequestData = new SelectAllPromotionsRequest();

                var ResponseData = _PromotionsMasterBLL.SelectAllPromotionsRecords(RequestData);

                if(ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionPriorityView.PromotionsMasterList = ResponseData.PromotionsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
       
        public void GetPriceListLookUp()
        {
            try
            {
                var RequestData = new SelectPriceListLookUPRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _PriceListBLL.PriceListLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionPriorityView.PriceListLookUp = ResponseData.PriceListTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsValidForm()
        {
            bool objBool = false;
            if (_IPromotionPriorityView.PromotionPriorityTypeList == null)
            {
                _IPromotionPriorityView.Message = "PromotionPriorityTypeList is missing Please Enter it.";
            }          

            else
            {
                objBool = true;
            }
            return objBool;
        }
        public void SaveExpenseMaster()
        {
            try
            {
                if (IsValidForm())
                {
                var RequestData = new SavePromotionPriorityRequest();
                RequestData.PromotionPriorityTypeData = _IPromotionPriorityView.PromotionPriorityTypeList;
                var ResponseData = _PromotionPriorityBLL.SavePromotionPriority(RequestData);
               _IPromotionPriorityView.Message = ResponseData.DisplayMessage;
                    _IPromotionPriorityView.ProcessStatus = ResponseData.StatusCode;
                   
                }
                else
                {
                    _IPromotionPriorityView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SelectPromotionPriorityDetails()
        {
            try
            {
                var RequestData = new SelectByIDPromotionPriorityRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.ID = _IPromotionPriorityView.PriceListID;
                var ResponseData = _PromotionPriorityBLL.SelectByIDs(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionPriorityView.PromotionPriorityTypeList = ResponseData.PromotionPriorityTypeList;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IPromotionPriorityView.Message = ResponseData.DisplayMessage;
                }

                _IPromotionPriorityView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    
    public class PromotionPriorityPresenterCollection
    {
        IPromotionPriorityCollectionView _IPromotionPriorityView;
        PromotionsMasterBLL _PromotionsMasterBLL = new PromotionsMasterBLL();

        PromotionPriorityBLL _PromotionPriorityBLL = new PromotionPriorityBLL();

        public PromotionPriorityPresenterCollection(IPromotionPriorityCollectionView ViewObj)
        {
            _IPromotionPriorityView = ViewObj;
        }


        public void SelectPromotionsPriorityRecord()
        {
            try
            {
                var RequestData = new SelectAllPromotionPriorityRequest();

                var ResponseData = _PromotionPriorityBLL.SelectAllPromotionPriority(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IPromotionPriorityView.PromotionPriorityTypeList = ResponseData.PromotionPriorityTypeData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveExpenseMaster()
        {
            try
            {
                var RequestData = new SavePromotionPriorityRequest();

                RequestData.PromotionPriorityTypeData = _IPromotionPriorityView.PromotionPriorityTypeList;

                var ResponseData = _PromotionPriorityBLL.SavePromotionPriority(RequestData);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
