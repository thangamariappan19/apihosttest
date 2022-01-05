using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IExpenseMaster;
using EasyBizRequest.Masters.ExpenseMasterRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class ExpenseMasterPresenter
    {

        IExpenseMasterView _IExpenseMasterView;
        ExpenseMasterBLL _ExpenseMasterBLL = new ExpenseMasterBLL();


        public ExpenseMasterPresenter(IExpenseMasterView ViewObj)
        {
            _IExpenseMasterView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = true;
            if (_IExpenseMasterView.ExpenseMasterList.Count==0)
            {
                _IExpenseMasterView.Message = "Expense Master is missing";
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
                    var RequestData = new SaveExpenseMasterRequest();

                    RequestData.ExpenseMasterTypesData = _IExpenseMasterView.ExpenseMasterList;

                    var ResponseData = _ExpenseMasterBLL.SaveExpenseMaster(RequestData);

                    _IExpenseMasterView.Message = ResponseData.DisplayMessage;
                    _IExpenseMasterView.ProcessStatus = ResponseData.StatusCode;

                }
                else
                {
                    _IExpenseMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

        public void UpdateExpenseMaster()
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateExpenseMasterRequest();
                RequestData.ExpenseMasterTypesData = new ExpenseMasterTypes();

                RequestData.ExpenseMasterTypesData.ExpenseCode = _IExpenseMasterView.ExpenseCode;
                RequestData.ExpenseMasterTypesData.ExpenseName = _IExpenseMasterView.ExpenseName;
                RequestData.ExpenseMasterTypesData.CreateBy = 1;
                var ResponseData = _ExpenseMasterBLL.UpdateExpenseMaster(RequestData);

                _IExpenseMasterView.Message = ResponseData.DisplayMessage;
                _IExpenseMasterView.ProcessStatus = ResponseData.StatusCode;

            }
            else
            {
                _IExpenseMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
            }
        }


        public void DeleteExpenseMaster()
        {
            try
            {
                var RequestData = new DeleteExpenseMasterRequest();
                RequestData.ExpenseMasterTypesData = new ExpenseMasterTypes();
                RequestData.ExpenseMasterTypesData.ID = _IExpenseMasterView.ID;
                var ResponseData = _ExpenseMasterBLL.DeleteExpenseMaster(RequestData);
                _IExpenseMasterView.Message = ResponseData.DisplayMessage;
                _IExpenseMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
              
          
        }


        public void SelectExpenseMaster()
        {
            try
            {
                var RequestData = new SelectByIDExpenseMasterRequest();
                RequestData.ID = _IExpenseMasterView.ID;
                var ResponseData = _ExpenseMasterBLL.SelectByIDExpenseMasterResponse(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _IExpenseMasterView.Message = ResponseData.DisplayMessage;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IExpenseMasterView.Message = ResponseData.DisplayMessage;
                }

                _IExpenseMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

         
        }


        public void GetExpenseMasterByID()
        {

            try
            {
                var RequestData = new SelectIDExpenseMasterRequest();
                RequestData.ID = _IExpenseMasterView.ID;
                RequestData.ShowInActiveRecords = true;
                var ResponseData = _ExpenseMasterBLL.SelectIDAllExpenseMaster(RequestData);


                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IExpenseMasterView.ExpenseMasterList = ResponseData.ExpenseMasterTypesList;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IExpenseMasterView.ExpenseMasterList = ResponseData.ExpenseMasterTypesList;
                    _IExpenseMasterView.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


       




    }


    public class ExpenseMasterPresenterList
    {

        IExpenseMasterViewList _IExpenseMasterViewList;
        ExpenseMasterBLL _ExpenseMasterBLL = new ExpenseMasterBLL();


        public ExpenseMasterPresenterList(IExpenseMasterViewList ViewObj)
        {
            _IExpenseMasterViewList = ViewObj;
        }


        public void GetExpenseMaster()
        {

            try
            {
                var RequestData = new SelectAllExpenseMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = _ExpenseMasterBLL.SelectAllExpenseMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IExpenseMasterViewList.ExpenseMasterList = ResponseData.ExpenseMasterTypesList;
                }
                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _IExpenseMasterViewList.ExpenseMasterList = ResponseData.ExpenseMasterTypesList;
                    _IExpenseMasterViewList.Message = ResponseData.DisplayMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }


       

    }
}
