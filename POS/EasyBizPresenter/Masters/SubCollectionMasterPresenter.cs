using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.ISubCollectionMaster;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.CollectionMasterResponse;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.SubCollectionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class SubCollectionMasterPresenter
    {
        ISubCollectionView _ISubCollectionView;
        SubCollectionBLL _SubCollectionBLL=new SubCollectionBLL();
        CollectionMasterBLL _CollectionMasterBLL=new CollectionMasterBLL();


        public SubCollectionMasterPresenter(ISubCollectionView ViewObj)
        {
            _ISubCollectionView = ViewObj;
        }


        public bool IsValidForm()
        {
            bool objBool = false;
            if (_ISubCollectionView.SubCollectionMasterList.Count==0)
            {
                _ISubCollectionView.Message = "SubCollection Details is invalid or Empty";
            }
            else if (_ISubCollectionView.CollectionName.Trim() == string.Empty)
            {
                _ISubCollectionView.Message = "Please Select Collection";
            }
            else
            {
                objBool = true;
            }
            return objBool;
        }


        public void SaveSubCollection()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new SaveSubCollectionRequest();
                    RequestData.SubCollectionMasterlist = _ISubCollectionView.SubCollectionMasterList;
                    var ResponseData = _SubCollectionBLL.SaveSubCollection(RequestData);
                    _ISubCollectionView.Message = ResponseData.DisplayMessage;
                    _ISubCollectionView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ISubCollectionView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public void DeleteSubCollection()
        {
            try
            {
                if (IsValidForm())
                {
                    var RequestData = new DeleteSubCollectionRequest();
                    RequestData.ID = _ISubCollectionView.ID;
                    var ResponseData = _SubCollectionBLL.DeleteSubCollection(RequestData);
                    _ISubCollectionView.Message = ResponseData.DisplayMessage;
                    _ISubCollectionView.ProcessStatus = ResponseData.StatusCode;
                }
                else
                {
                    _ISubCollectionView.ProcessStatus = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }


        public void SelectSubCollectionListForCategory()
        {
            try
            {
                var RequestData = new SelectSubCollectionListForCollectionRequest();
                RequestData.ShowInActiveRecords = true;
                RequestData.CollectionID = _ISubCollectionView.CollectionID;
                SelectSubCollectionListForCollectionResponse ResponseData = _SubCollectionBLL.SelectSubCollectionByCollection(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISubCollectionView.SubCollectionMasterList = ResponseData.SubCollectionMasterList;
                }
                else
                {
                    _ISubCollectionView.SubCollectionMasterList = new List<SubCollectionMaster>();
                    _ISubCollectionView.Message = ResponseData.DisplayMessage;
                    _ISubCollectionView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public void SelectAllSubCollectionDetails()
        {
            try
            {
                var RequestData = new SelectAllSubCollectionRequest();
                SelectAllSubCollectionResponse ResponseData = _SubCollectionBLL.SelectAllSubCollectionRecords(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISubCollectionView.AllSubCollectionDetailsList = ResponseData.SubCollectionMasterList;
                }
                else
                {
                    //_ISubCollectionView.AllSubCollectionDetailsList = new List<SubCollectionMaster>();
                    _ISubCollectionView.Message = ResponseData.DisplayMessage;
                    _ISubCollectionView.ProcessStatus = ResponseData.StatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void GetCollectionLookUp()
        {
            try
            {
                var RequestData = new SelectCollectionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CollectionMasterBLL.CollectionLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISubCollectionView.CollectionMasterTypesLookUp = ResponseData.CollectionMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }



        
      

}
    public class SubCollectionListPresenter
    {
        ISubCollectionViewList _ISubCollectionViewList;

        CollectionMasterBLL _CollectionMasterBLL=new CollectionMasterBLL();

        public SubCollectionListPresenter(ISubCollectionViewList ViewObj)
        {
            _ISubCollectionViewList = ViewObj;
        }

        public void GetCollectionList()
        {
            try
            {
                var RequestData = new SelectAllCollectionMasterRequest();
                RequestData.ShowInActiveRecords = true;
                var ResponseData = new SelectAllCollectionMasterResponse();
                ResponseData = _CollectionMasterBLL.SelectAllCollectionMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _ISubCollectionViewList.CollectionMasterTypesList = ResponseData.CollectionMasterTypesList;
                }
                else
                {
                    _ISubCollectionViewList.CollectionMasterTypesList = ResponseData.CollectionMasterTypesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    } 
}
