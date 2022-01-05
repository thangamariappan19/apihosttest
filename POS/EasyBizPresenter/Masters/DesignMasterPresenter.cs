using EasyBizBLL.Masters;
using EasyBizBLL.Transactions.Pricing;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizIView.Masters.IDesignMaster;
using EasyBizRequest.Masters.AFSegamationMasterRequest;
using EasyBizRequest.Masters.BrandMasterRequest;
using EasyBizRequest.Masters.CollectionMasterRequest;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizRequest.Masters.DivisionMasterRequest;
using EasyBizRequest.Masters.DropMasterRequest;
using EasyBizRequest.Masters.EmployeeMasterRequest;
using EasyBizRequest.Masters.ProductGroupRequest;
using EasyBizRequest.Masters.ProductLineMasterRequest;
using EasyBizRequest.Masters.SeasonRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizRequest.Masters.StyleStatusMasterRequest;
using EasyBizRequest.Masters.SubCollectionRequest;
using EasyBizRequest.Masters.YearMasterRequest;
using EasyBizRequest.Transactions.Pricing.PricePointRequest;
using EasyBizResponse.Masters.ProductGroupResponse;
using EasyBizResponse.Masters.SeasonResponse;
using EasyBizResponse.Masters.StyleMasterResponse;
using EasyBizResponse.Masters.YearMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Masters
{
    public class DesignMasterPresenter
    {
        IDesignView _IDesignView;
        DesignMasterBLL _DesignMasterBLL = new DesignMasterBLL();
        AFSegamationMasterBLL _AFSegamationMasterBLL = new AFSegamationMasterBLL();
        StyleStatusBLL _StyleStatusBLL = new StyleStatusBLL();
        CollectionMasterBLL _CollectionMasterBLL = new CollectionMasterBLL();
        BrandBLL _BrandBLL = new BrandBLL();
        DivisionBLL _DivisionBLL = new DivisionBLL();
        SubCollectionBLL _SubCollectionBLL = new SubCollectionBLL();
        ProductLineMasterBLL _ProductLineMasterBLL = new ProductLineMasterBLL();
        ProductGroupBLL _ProductGroupBLL = new ProductGroupBLL();
        SeasonBLL _SeasonBLL = new SeasonBLL();
        EmployeeMasterBLL _EmployeeMasterBLL = new EmployeeMasterBLL();
        DropMasterBLL _DropMasterBLL = new DropMasterBLL();
        YearBLL _YearBLL = new YearBLL();
        StyleMasterBLL _StyleMasterBLL = new StyleMasterBLL();
        PricePointBLL _PricePointBLL = new PricePointBLL();
        public DesignMasterPresenter(IDesignView ViewObj)
        {
            _IDesignView = ViewObj;
        }

        public bool IsValidForm()
        {
            bool objBool = false;
            //if (_IDesignView.DesignCode == string.Empty)
            //{
            //    _IDesignView.Message = "Design Code is missing.";
            //}
            if (_IDesignView.DesignName.Trim() == string.Empty)
            {
                _IDesignView.Message = "Please Enter Design Name";
            }
            else if (_IDesignView.SeasonID == 0)
            {
                _IDesignView.Message = "Please Select Season";
            }
            else if (_IDesignView.DropID == 0)
            {
                _IDesignView.Message = "Please Select Drop";
            }
            else if (_IDesignView.CollectionID == 0)
            {
                _IDesignView.Message = "Please Select Collection";
            }
            else if (_IDesignView.SubCollectionID == 0)
            {
                _IDesignView.Message = "Please Select SubCollection";
            }
            else if (_IDesignView.BrandID == 0)
            {
                _IDesignView.Message = "Please Select Brand";
            }
            else if (_IDesignView.ProductGroupID == 0)
            {
                _IDesignView.Message = "Please Select Product Group";
            }
            else if (_IDesignView.ProductLineID == 0)
            {
                _IDesignView.Message = "Please Select Product Line";
            }
            else if (_IDesignView.StyleStatusID == 0)
            {
                _IDesignView.Message = "Please Select Style Status";
            }
            else if (_IDesignView.YearID == 0)
            {
                _IDesignView.Message = "Please Select Year";
            }
            else if (_IDesignView.Description.Trim() == string.Empty)
            {
                _IDesignView.Message = "Please Enter Description";
            }
            
            //else if (_IDesignView.SegamentationID == 0)
            //{
            //    _IDesignView.Message = "Please Select Segamentation";
            //}
           
           
                
           
            
            else if (_IDesignView.DesignerID == 0)
            {
                _IDesignView.Message = "Please Select Designer";
            }
            else if (_IDesignView.DivisionID == 0)
            {
                _IDesignView.Message = "Please Select Division";
            }
           
            else
            {
                objBool = true;
            }
            return objBool;
        }



        public void SaveDesignMaster()
        {
            if (IsValidForm())
            {
                var RequestData = new SaveDesignMasterRequest();
                RequestData.DesignMasterData = new DesignMasterTypes();
                RequestData.DesignWithItemImageList = _IDesignView.DesigntemImageMasterList;
                RequestData.DesignMasterData.ID = _IDesignView.ID;
                RequestData.DesignMasterData.DesignCode = _IDesignView.DesignCode;
                RequestData.DesignMasterData.DesignName = _IDesignView.DesignName;
                RequestData.DesignMasterData.Description = _IDesignView.Description;
                RequestData.DesignMasterData.ForeignDescription = _IDesignView.ForeignDescription;
                RequestData.DesignMasterData.SegamentationID = 0;
                RequestData.DesignMasterData.StyleStatusID = _IDesignView.StyleStatusID;
                RequestData.DesignMasterData.ProductLineID = _IDesignView.ProductLineID;
                RequestData.DesignMasterData.ProductGroupID = _IDesignView.ProductGroupID;
                RequestData.DesignMasterData.SeasonID = _IDesignView.SeasonID;
                RequestData.DesignMasterData.Grade = _IDesignView.Grade;
                RequestData.DesignMasterData.DropID = _IDesignView.DropID;
                RequestData.DesignMasterData.CollectionID = _IDesignView.CollectionID;
                RequestData.DesignMasterData.SubCollectionID = _IDesignView.SubCollectionID;
                RequestData.DesignMasterData.Composition = _IDesignView.Composition;
                RequestData.DesignMasterData.SimbolGroup = _IDesignView.SimbolGroup;
                RequestData.DesignMasterData.BrandID = _IDesignView.BrandID;
                RequestData.DesignMasterData.DesignerID = _IDesignView.DesignerID;
                RequestData.DesignMasterData.DivisionID = _IDesignView.DivisionID;
                RequestData.DesignMasterData.ProductDepartmentCode = _IDesignView.ProductDepartmentCode;
                RequestData.DesignMasterData.CreateBy = 1;
                RequestData.DesignMasterData.YearID = _IDesignView.YearID;
                RequestData.DesignMasterData.DevelopmentOffice = _IDesignView.DevelopmentOffice;
                RequestData.DesignMasterData.ShortDescription = _IDesignView.ShortDescription;
                RequestData.DesignMasterData.Active = _IDesignView.Active;
                RequestData.DesignMasterData.Remarks = _IDesignView.Remarks;

                RequestData.DesignMasterData.SegamentationCode = _IDesignView.SegamentationCode;
                RequestData.DesignMasterData.StyleStatusCode = _IDesignView.StyleStatusCode;
                RequestData.DesignMasterData.ProductLineCode = _IDesignView.ProductLineCode;
                RequestData.DesignMasterData.ProductGroupCode = _IDesignView.ProductGroupCode;
                RequestData.DesignMasterData.SeasonCode = _IDesignView.SeasonCode;
                RequestData.DesignMasterData.DropCode = _IDesignView.DropCode;
                RequestData.DesignMasterData.CollectionCode = _IDesignView.CollectionCode;
                RequestData.DesignMasterData.SubCollectionCode = _IDesignView.SubCollectionCode;
                RequestData.DesignMasterData.BrandCode = _IDesignView.BrandCode;
                RequestData.DesignMasterData.DesignerCode = _IDesignView.DesignerCode;
                RequestData.DesignMasterData.DivisionCode = _IDesignView.DivisionCode;
                RequestData.DesignMasterData.YearCode = _IDesignView.YearCode;

                SaveDesignMasterResponse ResponseData = _DesignMasterBLL.SaveDesignMaster(RequestData);
                _IDesignView.Message = ResponseData.DisplayMessage;
                _IDesignView.ProcessStatus = ResponseData.StatusCode;
            }
          
        }


        public void UpdateDesignMaster()
        {
            if (IsValidForm())
            {
                var RequestData = new UpdateDesignMasterRequest();
                RequestData.DesignMasterData = new DesignMasterTypes();
                RequestData.DesignWithItemImageList= _IDesignView.DesigntemImageMasterList;
                RequestData.DesignMasterData.ID = _IDesignView.ID;
                RequestData.DesignMasterData.DesignCode = _IDesignView.DesignCode;
                RequestData.DesignMasterData.DesignName = _IDesignView.DesignName;
                RequestData.DesignMasterData.Description = _IDesignView.Description;
                RequestData.DesignMasterData.ForeignDescription = _IDesignView.ForeignDescription;
                RequestData.DesignMasterData.SegamentationID = _IDesignView.SegamentationID;
                RequestData.DesignMasterData.StyleStatusID = _IDesignView.StyleStatusID;
                RequestData.DesignMasterData.ProductLineID = _IDesignView.ProductLineID;
                RequestData.DesignMasterData.ProductGroupID = _IDesignView.ProductGroupID;
                RequestData.DesignMasterData.SeasonID = _IDesignView.SeasonID;
                RequestData.DesignMasterData.DropID = _IDesignView.DropID;
                RequestData.DesignMasterData.CollectionID = _IDesignView.CollectionID;
                RequestData.DesignMasterData.SubCollectionID = _IDesignView.SubCollectionID;
                RequestData.DesignMasterData.Composition = _IDesignView.Composition;
                RequestData.DesignMasterData.SimbolGroup = _IDesignView.SimbolGroup;
                RequestData.DesignMasterData.BrandID = _IDesignView.BrandID;
                RequestData.DesignMasterData.Grade = _IDesignView.Grade;
                RequestData.DesignMasterData.DesignerID = _IDesignView.DesignerID;
                RequestData.DesignMasterData.DivisionID = _IDesignView.DivisionID;
                RequestData.DesignMasterData.ProductDepartmentCode = _IDesignView.ProductDepartmentCode;
                RequestData.DesignMasterData.UpdateBy = 1;
                RequestData.DesignMasterData.YearID = _IDesignView.YearID;
                RequestData.DesignMasterData.DevelopmentOffice = _IDesignView.DevelopmentOffice;
                RequestData.DesignMasterData.ShortDescription = _IDesignView.ShortDescription;
                RequestData.DesignMasterData.SCN = _IDesignView.SCN;
                RequestData.DesignMasterData.Active = _IDesignView.Active;
                RequestData.DesignMasterData.Remarks = _IDesignView.Remarks;

                RequestData.DesignMasterData.SegamentationCode = _IDesignView.SegamentationCode;
                RequestData.DesignMasterData.StyleStatusCode = _IDesignView.StyleStatusCode;
                RequestData.DesignMasterData.ProductLineCode = _IDesignView.ProductLineCode;
                RequestData.DesignMasterData.ProductGroupCode = _IDesignView.ProductGroupCode;
                RequestData.DesignMasterData.SeasonCode = _IDesignView.SeasonCode;
                RequestData.DesignMasterData.DropCode = _IDesignView.DropCode;
                RequestData.DesignMasterData.CollectionCode = _IDesignView.CollectionCode;
                RequestData.DesignMasterData.SubCollectionCode = _IDesignView.SubCollectionCode;
                RequestData.DesignMasterData.BrandCode = _IDesignView.BrandCode;
                RequestData.DesignMasterData.DesignerCode = _IDesignView.DesignerCode;
                RequestData.DesignMasterData.DivisionCode = _IDesignView.DivisionCode;
                RequestData.DesignMasterData.YearCode = _IDesignView.YearCode;

                UpdateDesignMasterResponse ResponseData = _DesignMasterBLL.UpdateDesignMaster(RequestData);
                _IDesignView.Message = ResponseData.DisplayMessage;
                _IDesignView.ProcessStatus = ResponseData.StatusCode;
            }

        }
        public void DeleteDesignMaster()
        {
            try
            {
                var RequestData = new DeleteDesignMasterRequest();

                RequestData.ID = _IDesignView.ID;
                RequestData.BrandID = _IDesignView.BrandID;

                var ResponseData = _DesignMasterBLL.DeleteDesignMaster(RequestData);
                _IDesignView.Message = ResponseData.DisplayMessage;
                _IDesignView.ProcessStatus = ResponseData.StatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void SelectDesignMasterRecord()
        {


            var RequestData = new SelectByIDDesignMasterRequest();
            RequestData.ID = _IDesignView.ID;

            var ResponseData = _DesignMasterBLL.SelectByIDDesignMaster(RequestData);
            _IDesignView.DesignCode = ResponseData.DesignMasterTypesData.DesignCode;
            _IDesignView.DesignName = ResponseData.DesignMasterTypesData.DesignName;
            _IDesignView.Description = ResponseData.DesignMasterTypesData.Description;
            _IDesignView.ForeignDescription = ResponseData.DesignMasterTypesData.ForeignDescription;
           // _IDesignView.SegamentationID = ResponseData.DesignMasterTypesData.SegamentationID;
            _IDesignView.StyleStatusID = ResponseData.DesignMasterTypesData.StyleStatusID;
            _IDesignView.ProductLineID = ResponseData.DesignMasterTypesData.ProductLineID;
            _IDesignView.ProductGroupID = ResponseData.DesignMasterTypesData.ProductGroupID;
            _IDesignView.SeasonID = ResponseData.DesignMasterTypesData.SeasonID;
            _IDesignView.DropID = ResponseData.DesignMasterTypesData.DropID;
            _IDesignView.Grade = ResponseData.DesignMasterTypesData.Grade;
            _IDesignView.CollectionID = ResponseData.DesignMasterTypesData.CollectionID;
            _IDesignView.SubCollectionID = ResponseData.DesignMasterTypesData.SubCollectionID;
            _IDesignView.Composition = ResponseData.DesignMasterTypesData.Composition;
            _IDesignView.SimbolGroup = ResponseData.DesignMasterTypesData.SimbolGroup;
            _IDesignView.BrandID = ResponseData.DesignMasterTypesData.BrandID;
            _IDesignView.DesignerID = ResponseData.DesignMasterTypesData.DesignerID;
            _IDesignView.DivisionID = ResponseData.DesignMasterTypesData.DivisionID;           
            _IDesignView.YearID = ResponseData.DesignMasterTypesData.YearID;
            _IDesignView.DevelopmentOffice = ResponseData.DesignMasterTypesData.DevelopmentOffice;
            _IDesignView.ShortDescription = ResponseData.DesignMasterTypesData.ShortDescription;           
            _IDesignView.SCN = ResponseData.DesignMasterTypesData.SCN;
            _IDesignView.Active = ResponseData.DesignMasterTypesData.Active;
            _IDesignView.Remarks = ResponseData.DesignMasterTypesData.Remarks;

            if (ResponseData.StatusCode == Enums.OpStatusCode.RecordNotFound)
            {
                _IDesignView.Message = ResponseData.DisplayMessage;
            }

            else if (ResponseData.StatusCode == Enums.OpStatusCode.GeneralError)
            {
                _IDesignView.Message = ResponseData.DisplayMessage;
            }

            _IDesignView.ProcessStatus = ResponseData.StatusCode;
        }
        public void SelectPricePointRecord()
        {
            try
            {
                var RequestData = new SelectAllPricePointRequest();
                var ResponseData = _PricePointBLL.GetPricePointList(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.PricePointList = ResponseData.PricePointList;
                }
                else
                {
                    _IDesignView.Message = ResponseData.DisplayMessage;
                    _IDesignView.ProcessStatus = ResponseData.StatusCode;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetAFSegamationMasterLookUp()
        {
            try
            {
                var RequestData = new SelectAllAFSegamationMasterRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _AFSegamationMasterBLL.SelectAllAFSegamationMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.AFSegamationMasterLookUp = ResponseData.AFSegamationMasterTypesList;
                }
            }
            catch
            {


            }

        }

        public void GetDropMasterLookUp()
        {
            try
            {
                var RequestData = new SelectAllDropMasterRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _DropMasterBLL.SelectAllDropMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.DropMasterLookUp = ResponseData.DropMasterTypesList;
                }
            }
            catch
            {


            }

        }

        public void GetStyleStatusMasterLookUp()
        {
            try
            {
                var RequestData = new SelectStyleStatusLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _StyleStatusBLL.SelectStyleStatusLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.StyleStatusMasterLookUp = ResponseData.StyleStatusMasterTypeList;
                }
            }
            catch
            {

            }

        }

        public void GetCollectionMasterLookUp()
        {
            try
            {
                var RequestData = new SelectCollectionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _CollectionMasterBLL.CollectionLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.CollectionMasterLookUp = ResponseData.CollectionMasterTypesList;
                }
            }
            catch
            {
                
            }

        }
        public void GetGrade()
        {
            try
            {
                var RequestData = new SelectDesignGradeLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _DesignMasterBLL.GradeLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.DesignGradeLookUp = ResponseData.DesignGradeList;
                }
            }
            catch
            {

            }

        }
        public void GetDevelopmemtOffice()
        {
            try
            {
                var RequestData = new SelectDesignDevelopmentOfficeLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _DesignMasterBLL.DevelopmentOfficeLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.DesignDevelopmentOfficeLookUp = ResponseData.DesignDevelopmentOfficeList;
                }
            }
            catch
            {

            }

        }

        public void GetBrandMasterLookUp()
        {
            try
            {
                var RequestData = new SelectBrandLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _BrandBLL.BrandLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.BrandMasterLookUp = ResponseData.BrandList;
                }
            }
            catch
            {
                
            }

        }

        public void GetDivisionMasterLookUp()
        {
            try
            {
                var RequestData = new SelectDivisionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _DivisionBLL.DivisionLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.DivisionMasterLookUp = ResponseData.DivisionList;
                }
            }
            catch
            {

            }

        }


        public void GetSubCollectionLookUp()
        {
            try
            {
                var RequestData = new SelectSubCollectionLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                RequestData.CollectionID = _IDesignView.CollectionID;
                var ResponseData = _SubCollectionBLL.SelectAllLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.SubCollectionMasterLookUp = ResponseData.SubCollectionMasterLookUpList;
                }
                else
                {
                    _IDesignView.SubCollectionMasterLookUp = new List<SubCollectionMaster>();
                }
            }
            catch
            {

            }

        }

        public void GetProductLineLookUp()
        {
            try
            {
                var RequestData = new SelectProductLineLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _ProductLineMasterBLL.SelectProductLineLookUP(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.ProductLineMasterLookUp = ResponseData.ProductLineMasterList;
                }
            }
            catch
            {

            }
        }

        public void GetProductGroupMasterLookUp()
        {
            try
            {
                var RequestData = new SelectProductGroupLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _ProductGroupBLL.ProductGroupLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.ProductGroupMasterLookUp = ResponseData.ProductGroupList;
                }
            }
            catch
            {

            }
        }

        public void GetSeasonMasterLookUp()
        {
            try
            {
                var RequestData = new SelectSeasonLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _SeasonBLL.SelectSeasonLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.SeasonMasterLookUp = ResponseData.SeasonList;
                }
            }
            catch
            {

            }
        }


        public void GetEmployeeMasterLookUp()
        {
            try
            {
                var RequestData = new SelectEmployeeLookUpRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _EmployeeMasterBLL.SelectEmployeeLookUp(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignView.EmployeeMasterLookUp = ResponseData.EmployeeList;
                }
            }
            catch
            {

            }
        }


        public void GetYearList()
        {
            var RequestData = new SelectAllYearRequest();
            RequestData.ShowInActiveRecords = true;
            var ResponseData = new SelectAllYearResponse();
            ResponseData = _YearBLL.SelectAllYear(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDesignView.YearList = ResponseData.YearList;
            }
            else
            {

            }
        }
        public void SelectDesignWithItemImageDetails()
        {
            SelectItemImageRequest RequestData = new SelectItemImageRequest();
            RequestData.ShowInActiveRecords = true;
            RequestData.ID = _IDesignView.ID;
            RequestData.FormName = "Design";
            SelectItemImageResponse ResponseData = _StyleMasterBLL.SelectStyleWithItemImage(RequestData);

            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDesignView.DesigntemImageMasterList = ResponseData.ItemImageMaster;

                //_IDesignView.SKUImageList =
            }
            else
            {
                _IDesignView.Message = ResponseData.DisplayMessage;
                _IDesignView.ProcessStatus = ResponseData.StatusCode;
            }
        }
    }




    public class DesignMasterPresenterList
    {

        IDesignMasterViewList _IDesignMasterViewList;
        DesignMasterBLL _DesignMasterBLL = new DesignMasterBLL();
     
        public DesignMasterPresenterList(IDesignMasterViewList ViewObj)
        {
            _IDesignMasterViewList = ViewObj;
        }


        public void GetAllDesignMasterRecords()
        {
            try
            {
                var RequestData = new SelectAllDesignMasterRequest();
                RequestData.ShowInActiveRecords = false;
                var ResponseData = _DesignMasterBLL.SelectAllDesignMaster(RequestData);
                if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                {
                    _IDesignMasterViewList.DesignMasterTypesList = ResponseData.DesignMasterTypesList;
                }
            }
            catch
            {

            }
        }
       
    }


}
