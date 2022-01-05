using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.SubBrandMasterRequest;
using EasyBizResponse.Masters.SubBrandMasterResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Masters
{
    public class SubBrandMasterDAL : BaseSubBrandMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveSubBrandRequest)RequestObj;
            var ResponseData = new SaveSubBrandResponse();
            var SubBrandlist = RequestData.SubBrandlist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);                
                _CommandObj = new SqlCommand("API_InsertOrUpdateSubBrand", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (SubBrandMaster objSubBrand in SubBrandlist)
                {
                    sSql.Append("<SubBrandMaster>");
                    sSql.Append("<ID>" + (objSubBrand.ID) + "</ID>");
                    sSql.Append("<SubBrandCode>" + (objSubBrand.SubBrandCode) + "</SubBrandCode>");
                    sSql.Append("<SubBrandName>" + objSubBrand.SubBrandName + "</SubBrandName>");
                    sSql.Append("<Active>" + (objSubBrand.Active) + "</Active>");
                    sSql.Append("<IsDeleted>" + (objSubBrand.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<BrandID>" + objSubBrand.BrandID + "</BrandID>");
                    sSql.Append("<BrandName>" + objSubBrand.BrandName + "</BrandName>");
                    sSql.Append("<CreateBy>" + objSubBrand.CreateBy + "</CreateBy>");
                    sSql.Append("</SubBrandMaster>");
                }
                var SubBrandData = _CommandObj.Parameters.Add("@SubBrandData", SqlDbType.Xml);
                SubBrandData.Direction = ParameterDirection.Input;
                SubBrandData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@SubIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sub Brand");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Sub Brand");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sub Brand");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.ItIsInRelationdhip.Replace("{}", "Sub Brand");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sub Brand");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubBrandRecord = new SubBrandMaster();
            var RequestData = (DeleteSubBrandRequest)RequestObj;
            var ResponseData = new DeleteSubBrandResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from SubBrandMaster where BrandID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Sub Brand");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Sub Brand");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubBrandRecord = new SubBrandMaster();
            var RequestData = (SelectBySubBrandIDRequest)RequestObj;
            var ResponseData = new SelectBySubBrandIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from SubBrandMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubBrand = new SubBrandMaster();
                        objSubBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubBrand.SubBrandCode = Convert.ToString(objReader["SubBrandCode"]);
                        objSubBrand.SubBrandName = Convert.ToString(objReader["SubBrandName"]);
                        objSubBrand.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSubBrand.BrandName = Convert.ToString(objReader["BrandName"]);                        
                        objSubBrand.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubBrand.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubBrand.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubBrand.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSubBrand.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.SubBrandRecord = objSubBrand;
                        ResponseData.ResponseDynamicData = objSubBrand;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubBrandList = new List<SubBrandMaster>();
            var RequestData = (SelectAllSubBrandRequest)RequestObj;
            var ResponseData = new SelectAllSubBrandResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select distinct brandID,BrandName from SubBrandMaster with(NoLock)";
              
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubBrand = new SubBrandMaster();
                        //objSubBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSubBrand.SubBrandCode = Convert.ToString(objReader["SubBrandCode"]);
                        //objSubBrand.SubBrandName = Convert.ToString(objReader["SubBrandName"]);
                        objSubBrand.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSubBrand.BrandName = Convert.ToString(objReader["BrandName"]);
                        //objSubBrand.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSubBrand.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSubBrand.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSubBrand.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objSubBrand.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objSubBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        SelectSubBrandLookUpRequest objSelectsubCollectionRequest = new SelectSubBrandLookUpRequest();
                        SelectSubBrandLookUpResponse objSelectsubCollectionResponse = new SelectSubBrandLookUpResponse();
                        objSelectsubCollectionRequest.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSelectsubCollectionRequest.ShowInActiveRecords = true;
                        //objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectsubCollectionResponse = SelectSubBrandLookUp(objSelectsubCollectionRequest);
                        if (objSelectsubCollectionResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSubBrand.SubBrandlist = objSelectsubCollectionResponse.SubBrandList;
                        }

                        SubBrandList.Add(objSubBrand);
                    }
                    ResponseData.SubBrandList = SubBrandList;
                    ResponseData.ResponseDynamicData = SubBrandList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SubBrandList = new List<SubBrandMaster>();
            var RequestData = (SelectBySubBrandIDSRequest)RequestObj;
            var ResponseData = new SelectBySubBrandIDsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from SubBrandMaster with(NoLock) where  ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.IDs);
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubBrand = new SubBrandMaster();
                        objSubBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubBrand.SubBrandCode = Convert.ToString(objReader["SubBrandCode"]);
                        objSubBrand.SubBrandName = Convert.ToString(objReader["SubBrandName"]);
                        objSubBrand.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSubBrand.BrandName = Convert.ToString(objReader["BrandName"]);
                        objSubBrand.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubBrand.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubBrand.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubBrand.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSubBrand.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        SubBrandList.Add(objSubBrand);
                    }
                    ResponseData.SubBrandList = SubBrandList;
                    ResponseData.ResponseDynamicData = SubBrandList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override EasyBizResponse.Masters.SubBrandMasterResponse.SelectSubBrandLookUpResponse SelectSubBrandLookUp(EasyBizRequest.Masters.SubBrandMasterRequest.SelectSubBrandLookUpRequest ObjRequest)
        {
            var SubBrandList = new List<SubBrandMaster>();
            var RequestData = (SelectSubBrandLookUpRequest)ObjRequest;
            var ResponseData = new SelectSubBrandLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[SubBrandName],SubBrandCode from SubBrandMaster with(NoLock) where Active='true'";
                
                if (RequestData.BrandID != 0)
                {
                    sQuery = sQuery + " and BrandID='" + RequestData.BrandID + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubBrand = new SubBrandMaster();
                        objSubBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubBrand.SubBrandName = Convert.ToString(objReader["SubBrandName"]);
                        objSubBrand.SubBrandCode = Convert.ToString(objReader["SubBrandCode"]);
                        SubBrandList.Add(objSubBrand);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SubBrandList = SubBrandList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectSubBrandListForCategoryResponse SelectSubBrandListByBrand(SelectSubBrandListForCategoryRequest RequestObj)
        {           
            var MASSubBrandForCategoryList = new List<SubBrandMaster>();
            var RequestData = (SelectSubBrandListForCategoryRequest)RequestObj;
            var ResponseData = new SelectSubBrandListForCategoryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from SubBrandMaster with(NoLock) where BrandID='" + RequestData.BrandID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubBrand = new SubBrandMaster();
                        objSubBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSubBrand.SubBrandCode = Convert.ToString(objReader["SubBrandCode"]);
                        objSubBrand.SubBrandName = Convert.ToString(objReader["SubBrandName"]);
                        objSubBrand.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSubBrand.BrandName = Convert.ToString(objReader["BrandName"]);                        
                        objSubBrand.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSubBrand.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSubBrand.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSubBrand.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSubBrand.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //SelectCollectionListByBrandsRequest objSelectCollectionListByBrandsRequest = new SelectCollectionListByBrandsRequest();
                        //objSelectCollectionListByBrandsRequest.BrandID =objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) :0;
                        //objSelectCollectionListByBrandsRequest.SubBrandID = objMASSubBrand.ID = Convert.ToInt32(objReader["ID"]);
                        //objSelectCollectionListByBrandsRequest.ShowIsActiveRecords = true;

                        //SelectCollectionListByBrandsResponse objSelectCollectionListByBrandsResponse = new SelectCollectionListByBrandsResponse();
                        //objSelectCollectionListByBrandsResponse = _MASCollectionDAL.SelectCollectionListByBrands(objSelectCollectionListByBrandsRequest);
                        //if (objSelectCollectionListByBrandsResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objMASSubBrand.CollectionList = objSelectCollectionListByBrandsResponse.CollectionList;
                        //}
                        //else
                        //{
                        //    objMASSubBrand.CollectionList = new List<MASCollection>();
                        //}
                        //objSubBrand.CollectionList = new List<MASCollection>();
                        MASSubBrandForCategoryList.Add(objSubBrand);
                    }
                    ResponseData.SubBrandList = MASSubBrandForCategoryList;
                    ResponseData.ResponseDynamicData = MASSubBrandForCategoryList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override SelectAllSubBrandResponse API_SelectALL(SelectAllSubBrandRequest requestData)
        {
            var SubBrandList = new List<SubBrandMaster>();
            var RequestData = (SelectAllSubBrandRequest)requestData;
            var ResponseData = new SelectAllSubBrandResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "Select distinct brandID,BrandName from SubBrandMaster with(NoLock)";

                string sSql = "Select distinct BrandID, BrandName, Active,RC.TOTAL_CNT [RecordCount]  " +
                   "from SubBrandMaster with(NoLock) " +
                    "LEFT JOIN(Select  count(distinct SM.ID) As TOTAL_CNT From SubBrandMaster SM with(NoLock) " +
                    "where SM.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or SM.BrandID like isnull('%" + RequestData.SearchString + "%','') " +
                       "or SM.BrandName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or BrandID like isnull('%" + RequestData.SearchString + "%','') " +
                       "or BrandName like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by BrandID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSubBrand = new SubBrandMaster();
                        //objSubBrand.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSubBrand.SubBrandCode = Convert.ToString(objReader["SubBrandCode"]);
                        //objSubBrand.SubBrandName = Convert.ToString(objReader["SubBrandName"]);
                        objSubBrand.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSubBrand.BrandName = Convert.ToString(objReader["BrandName"]);
                        //objSubBrand.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSubBrand.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSubBrand.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSubBrand.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objSubBrand.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSubBrand.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        SelectSubBrandLookUpRequest objSelectsubCollectionRequest = new SelectSubBrandLookUpRequest();
                        SelectSubBrandLookUpResponse objSelectsubCollectionResponse = new SelectSubBrandLookUpResponse();
                        objSelectsubCollectionRequest.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSelectsubCollectionRequest.ShowInActiveRecords = true;
                        //objSelectAFSegmationDetailsRequest.ShowInActiveRecords = true;
                        objSelectsubCollectionResponse = SelectSubBrandLookUp(objSelectsubCollectionRequest);
                        if (objSelectsubCollectionResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSubBrand.SubBrandlist = objSelectsubCollectionResponse.SubBrandList;
                        }

                        SubBrandList.Add(objSubBrand);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.SubBrandList = SubBrandList;
                    //ResponseData.ResponseDynamicData = SubBrandList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sub Brand");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
    }
}
