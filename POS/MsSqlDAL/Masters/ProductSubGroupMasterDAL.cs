using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.ProductSubGroupMasterRequest;
using EasyBizResponse.Masters.ProductSubGroupMasterResponse;
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
   public class ProductSubGroupMasterDAL : BaseProductSubGroupMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveProductSubGroupRequest)RequestObj;
            var ResponseData = new SaveProductSubGroupResponse();
            var ProductSubGrouplist = RequestData.ProductSubGrouplist;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateProductSubGroup", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                foreach (ProductSubGroupMaster objProductSubGroup in ProductSubGrouplist)
                {
                    sSql.Append("<ProductSubGroupMaster>");
                    sSql.Append("<ID>" + (objProductSubGroup.ID) + "</ID>");
                    sSql.Append("<ProductSubGroupCode>" + (objProductSubGroup.ProductSubGroupCode) + "</ProductSubGroupCode>");
                    sSql.Append("<ProductSubGroupName>" + objProductSubGroup.ProductSubGroupName + "</ProductSubGroupName>");
                    sSql.Append("<Active>" + (objProductSubGroup.Active) + "</Active>");
                    sSql.Append("<UserID>" + (objProductSubGroup.CreateBy) + "</UserID>");
                    sSql.Append("<CreateBy>" + (objProductSubGroup.CreateBy) + "</CreateBy>");
                    sSql.Append("<IsDeleted>" + (objProductSubGroup.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<ProductGroupID>" + objProductSubGroup.ProductGroupID + "</ProductGroupID>");
                    sSql.Append("</ProductSubGroupMaster>");
                }
                var ProductSubGroupData = _CommandObj.Parameters.Add("@ProductSubGroupData", SqlDbType.Xml);
                ProductSubGroupData.Direction = ParameterDirection.Input;
                ProductSubGroupData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                SqlParameter ID = _CommandObj.Parameters.Add("@PRIDs", SqlDbType.VarChar, 500);
                ID.Direction = ParameterDirection.Output;

                SqlParameter ProductGroupId = _CommandObj.Parameters.Add("@ProductGroupId", SqlDbType.VarChar, 500);
                ProductGroupId.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Product SubGroup");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ProductGroupId.Value.ToString();    
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Product SubGroup");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Product SubGroup");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.ItIsInRelationdhip.Replace("{}", "Product SubGroup");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Product SubGroup");
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
            var ProductSubGroupRecord = new ProductSubGroupMaster();
            var RequestData = (DeleteProductSubGroupRequest)RequestObj;
            var ResponseData = new DeleteProductSubGroupResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from  ProductSubGroup where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "ProductSubGroup Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "ProductSubGroup Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var ProductSubGroupRecord = new ProductSubGroupMaster();
            var RequestData = (SelectByProductSubGroupIDRequest)RequestObj;
            var ResponseData = new SelectByProductSubGroupIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from ProductSubGroupMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductSubGroup = new ProductSubGroupMaster();
                        objProductSubGroup.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductSubGroup.ProductSubGroupCode = Convert.ToString(objReader["ProductSubGroupCode"]);
                        objProductSubGroup.ProductSubGroupName = Convert.ToString(objReader["ProductSubGroupName"]);
                        objProductSubGroup.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objProductSubGroup.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductSubGroup.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductSubGroup.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductSubGroup.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objProductSubGroup.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objProductSubGroup.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.ProductSubGroupRecord = objProductSubGroup;
                        ResponseData.ResponseDynamicData = objProductSubGroup;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductSubGroup Master");
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
            var ProductSubGroupList = new List<ProductSubGroupMaster>();
            var RequestData = (SelectAllProductSubGroupRequest)RequestObj;
            var ResponseData = new SelectAllProductSubGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sQuery = "Select * from ProductSubGroupMaster with(NoLock)";          
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductSubGroup = new ProductSubGroupMaster();
                        objProductSubGroup.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductSubGroup.ProductSubGroupCode = Convert.ToString(objReader["ProductSubGroupCode"]);
                        objProductSubGroup.ProductSubGroupName = Convert.ToString(objReader["ProductSubGroupName"]);
                        objProductSubGroup.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objProductSubGroup.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductSubGroup.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductSubGroup.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductSubGroup.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objProductSubGroup.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objProductSubGroup.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ProductSubGroupList.Add(objProductSubGroup);
                    }
                    ResponseData.ProductSubGroupList = ProductSubGroupList;
                    ResponseData.ResponseDynamicData = ProductSubGroupList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductSubGroup Master");
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override EasyBizResponse.Masters.ProductSubGroupMasterResponse.SelectProductSubGroupLookUpResponse SelectProductSubGroupLookUp(EasyBizRequest.Masters.ProductSubGroupMasterRequest.SelectProductSubGroupLookUpRequest ObjRequest)
        {
            var ProductSubGroupList = new List<ProductSubGroupMaster>();
            var RequestData = (SelectProductSubGroupLookUpRequest)ObjRequest;
            var ResponseData = new SelectProductSubGroupLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,[ProductSubGroupName],ProductSubGroupCode from ProductSubGroupMaster with(NoLock)  where Active='true'";
                if (RequestData.ProductGroupID != 0)
                {
                    sQuery = sQuery + " and ProductGroupID='" + RequestData.ProductGroupID + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductSubGroup = new ProductSubGroupMaster();
                        objProductSubGroup.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductSubGroup.ProductSubGroupName = Convert.ToString(objReader["ProductSubGroupName"]);
                        objProductSubGroup.ProductSubGroupCode = Convert.ToString(objReader["ProductSubGroupCode"]);
                        ProductSubGroupList.Add(objProductSubGroup);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductSubGroupList = ProductSubGroupList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductSubGroup Master");
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

        public override EasyBizResponse.Masters.ProductSubGroupMasterResponse.SelectProductGroupListForProductSubGroupResponse SelectProductSubGroupListByProductGroup(EasyBizRequest.Masters.ProductSubGroupMasterRequest.SelectProductGroupListForProductSubGroupRequest RequestObj)
        {
            var SeasonForProductSubGroupList = new List<ProductSubGroupMaster>();
            var RequestData = (SelectProductGroupListForProductSubGroupRequest)RequestObj;
            var ResponseData = new SelectProductGroupListForProductSubGroupResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from ProductSubGroupMaster with(NoLock) where ProductGroupID='" + RequestData.ProductGroupID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objProductSubGroup = new ProductSubGroupMaster();
                        objProductSubGroup.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objProductSubGroup.ProductSubGroupCode = Convert.ToString(objReader["ProductSubGroupCode"]);
                        objProductSubGroup.ProductSubGroupName = Convert.ToString(objReader["ProductSubGroupName"]);
                        objProductSubGroup.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objProductSubGroup.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objProductSubGroup.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objProductSubGroup.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objProductSubGroup.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objProductSubGroup.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objProductSubGroup.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //SelectCollectionListByBrandsRequest objSelectCollectionListByBrandsRequest = new SelectCollectionListByBrandsRequest();
                        //objSelectCollectionListByBrandsRequest.BrandID = Convert.ToInt32(objReader["BrandID"]);
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
                        SeasonForProductSubGroupList.Add(objProductSubGroup);
                    }
                    ResponseData.ProductSubGroupList = SeasonForProductSubGroupList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = SeasonForProductSubGroupList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "ProductSubGroup Master");
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
