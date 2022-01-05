using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Masters.ITax;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.TaxMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class TaxMasterPresenter
    {
         ITaxView _ITaxView;
       TaxBLL _TaxBLL = new TaxBLL();
      
       public TaxMasterPresenter(ITaxView ViewObj)
        {
            _ITaxView = ViewObj;
        }
       public bool IsValidForm()
       {
           bool objBool = false;
           if (_ITaxView.TaxList.Count == 0)
           {
               _ITaxView.Message = "Please Enter Tax Details";
           }
          
           else
           {
               objBool = true;
           }
           return objBool;
       }
       public void SaveTax()
       {
           try
           {
               if (IsValidForm())
               {
                   var RequestData = new SaveTaxRequest();
                   RequestData.Taxlist = _ITaxView.TaxList;
                   var ResponseData = _TaxBLL.SaveTax(RequestData);
                   _ITaxView.Message = ResponseData.DisplayMessage;
                   _ITaxView.ProcessStatus = ResponseData.StatusCode;
               }
               else
               {
                   _ITaxView.ProcessStatus = Enums.OpStatusCode.GeneralError;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteTax()
       {
           try
           {
               var RequestData = new DeleteTaxRequest();
               RequestData.ID = _ITaxView.ID;
               var ResponseData = _TaxBLL.DeleteTax(RequestData);
               _ITaxView.Message = ResponseData.DisplayMessage;
               _ITaxView.ProcessStatus = ResponseData.StatusCode;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
       public void SelectAllTax()
       {
           try
           {
               var RequestData = new SelectAllTaxRequest();
               RequestData.ShowInActiveRecords = true;
               var ResponseData = _TaxBLL.SelectAllTaxRecords(RequestData);
               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ITaxView.TaxList = ResponseData.TaxList;
               }
               else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
               {
                   _ITaxView.Message = ResponseData.DisplayMessage;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void GetTaxByID()
       {
           try
           {
               var RequestData = new SelectByTaxIDRequest();
               RequestData.ID = _ITaxView.ID;
               RequestData.ShowInActiveRecords = true;
               var ResponseData = _TaxBLL.SelectTaxRecord(RequestData);


               if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
               {
                   _ITaxView.TaxList = ResponseData.TaxList;
               }
               else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
               {
                   _ITaxView.TaxList = ResponseData.TaxList;
                   _ITaxView.Message = ResponseData.DisplayMessage;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
    public class TaxListPresenter
    {
        ITaxCollectionView _ITaxCollectionView;
        TaxBLL _TaxBLL = new TaxBLL();

        public TaxListPresenter(ITaxCollectionView ViewObj)
        {
            _ITaxCollectionView = ViewObj;
        }

        public void GetTaxList()
        {
            try
            {
                var RequestData = new SelectAllTaxRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllTaxResponse();
                ResponseData = _TaxBLL.SelectAllTaxRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ITaxCollectionView.TaxList = ResponseData.TaxList;
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
