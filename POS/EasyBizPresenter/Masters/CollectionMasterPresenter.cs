using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ICollectionMaster;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class CollectionMasterPresenter
    {
        ICollectionMasterView _ICollectionMasterView;
        CollectionMasterBLL _CollectionMasterBLL = new CollectionMasterBLL();
        public CollectionMasterPresenter(ICollectionMasterView ViewObj)
        {
            _ICollectionMasterView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ICollectionMasterView.CollectionCode.Trim() == string.Empty)
            {
                _ICollectionMasterView.Message = "Collection Code is missing Please Enter it.";
            }
           
            else if (_ICollectionMasterView.CollectionName.Trim() == string.Empty)
            {
                _ICollectionMasterView.Message = "Name is missing Please Enter it.";
            }

            else
            {
                objBool = true;
            }
            return objBool;
        }

        public void SaveCollectionMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveCollectionMasterRequest();
                    RequestData.CollectionMasterTypesRecord = new CollectionMasterTypes();
                    RequestData.CollectionMasterTypesRecord.ID = _ICollectionMasterView.ID;
                    RequestData.CollectionMasterTypesRecord.CollectionCode = _ICollectionMasterView.CollectionCode;
                    RequestData.CollectionMasterTypesRecord.CollectionName = _ICollectionMasterView.CollectionName;
                    RequestData.CollectionMasterTypesRecord.Remarks = _ICollectionMasterView.Remarks;
                    RequestData.CollectionMasterTypesRecord.Active = _ICollectionMasterView.Active;
                    RequestData.CollectionMasterTypesRecord.CreateBy = 1;

                    var ResponseData = _CollectionMasterBLL.SaveCollectionMaster(RequestData);
                    _ICollectionMasterView.Message = ResponseData.DisplayMessage;
                    _ICollectionMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ICollectionMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void UpdateCollectionMasterView()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new UpdateCollectionMasterRequest();
                    RequestData.CollectionMasterTypesData = new CollectionMasterTypes();
                    RequestData.CollectionMasterTypesData.ID = _ICollectionMasterView.ID;
                    RequestData.CollectionMasterTypesData.CollectionCode = _ICollectionMasterView.CollectionCode;
                    RequestData.CollectionMasterTypesData.CollectionName = _ICollectionMasterView.CollectionName;
                    RequestData.CollectionMasterTypesData.Remarks = _ICollectionMasterView.Remarks;
                    RequestData.CollectionMasterTypesData.Active = _ICollectionMasterView.Active;
                    RequestData.CollectionMasterTypesData.UpdateBy = 1;
                    RequestData.CollectionMasterTypesData.SCN = _ICollectionMasterView.SCN;

                    var ResponseData = _CollectionMasterBLL.UpdateCollectionMaster(RequestData);
                    _ICollectionMasterView.Message = ResponseData.DisplayMessage;
                    _ICollectionMasterView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ICollectionMasterView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void DeleteCollectionMasterView()
        {
            try
            {
                var RequestData = new DeleteCollectionMasterRequest();

                RequestData.ID = _ICollectionMasterView.ID;
                var ResponseData = _CollectionMasterBLL.DeleteCollectionMaster(RequestData);
                _ICollectionMasterView.Message = ResponseData.DisplayMessage;
                _ICollectionMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void SelectByIDCollectionMaster()
        {
            try
            {
                var RequestData = new SelectByIDCollectionMasterRequest();
                RequestData.ID = _ICollectionMasterView.ID;
                var ResponseData = _CollectionMasterBLL.SelectByIDCollectionMaster(RequestData);
                _ICollectionMasterView.CollectionCode = ResponseData.CollectionMasterTypesData.CollectionCode;
                _ICollectionMasterView.CollectionName = ResponseData.CollectionMasterTypesData.CollectionName;
                _ICollectionMasterView.Remarks = ResponseData.CollectionMasterTypesData.Remarks;
                _ICollectionMasterView.Active = ResponseData.CollectionMasterTypesData.Active;
                _ICollectionMasterView.SCN = ResponseData.CollectionMasterTypesData.SCN;

                if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
                {
                    _ICollectionMasterView.Message = ResponseData.DisplayMessage;
                }

                else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
                {
                    _ICollectionMasterView.Message = ResponseData.DisplayMessage;
                }

                _ICollectionMasterView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }


    public class CollectionMasterViewListPresenter
    {
        ICollectionMasterViewList _ICollectionMasterViewList;
        CollectionMasterBLL _CollectionMasterBLL = new CollectionMasterBLL();
        public CollectionMasterViewListPresenter(ICollectionMasterViewList ViewObj)
        {
            _ICollectionMasterViewList = ViewObj;
        }

        public void SelectAllCollectionMaster()
        {
            try
            {
                var RequestData = new SelectAllCollectionMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCollectionMasterResponse();

                ResponseData = _CollectionMasterBLL.SelectAllCollectionMaster(RequestData);

                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ICollectionMasterViewList.CollectionMasterTypesList = ResponseData.CollectionMasterTypesList;
                }
                else if (ResponseData.StatusCode != Enums.OpStatusCode.RecordNotFound)
                {
                    _ICollectionMasterViewList.CollectionMasterTypesList = ResponseData.CollectionMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
