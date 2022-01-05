using EasyBizAbsDAL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest;
using EasyBizRequest.Transactions.Promotions.PromotionMappingRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.Promotions.PromotionMappingResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Promotions
{
   public class PromotionMappingDAL : BasePromotionMappingDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;
        public override SelectPromotionMappingLookUpResponse SelectPromotionMappingLookUp(SelectPromotionMappingLookUpRequest ObjRequest)
        {
            var PromotionMappingList = new List<PromotionMappingTypes>();
            var RequestData = (SelectPromotionMappingLookUpRequest)ObjRequest;
            var ResponseData = new SelectPromotionMappingLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,StoreName,StoreCode from PromotionMapping with(NoLock) where Active='true'";

                if (RequestData.WNPromotionID != 0)
                {
                    sQuery = sQuery + " and WNPromotionID='" + RequestData.WNPromotionID + "' or BrandCode='" + RequestData.WNPromotionCode + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotionMapping = new PromotionMappingTypes();
                        objPromotionMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionMapping.StoreName = Convert.ToString(objReader["StoreName"]);
                        objPromotionMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        PromotionMappingList.Add(objPromotionMapping);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionMappingList = PromotionMappingList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotion Mapping");
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

        public override SelectPromotionMapListForCategoryResponse SelectPromotionMappingListByPromotion(SelectPromotionMapListForCategoryRequest RequestObj)
        {
            var PromotionMappingorCategoryList = new List<PromotionMappingTypes>();
            var RequestData = (SelectPromotionMapListForCategoryRequest)RequestObj;
            var ResponseData = new SelectPromotionMapListForCategoryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("Select * from PromotionMapping with(NoLock) where WNPromotionID='" + RequestData.WNPromotionID + "'", _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotionMapping = new PromotionMappingTypes();
                        objPromotionMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objPromotionMapping.StoreName = Convert.ToString(objReader["StoreName"]);
                        objPromotionMapping.WNPromotionID = objReader["WNPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["WNPromotionID"]) : 0;
                        objPromotionMapping.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotionMapping.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotionMapping.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotionMapping.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objPromotionMapping.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotionMapping.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        PromotionMappingorCategoryList.Add(objPromotionMapping);
                    }
                    ResponseData.PromotionMappingList = PromotionMappingorCategoryList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotion Mapping");
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

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SavePromotionMappingRequest)RequestObj;
            var ResponseData = new SavePromotionMappingResponse();
            var StoreMasterList = RequestData.PromotionMappingList;
            var sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdatePromotionMapping", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                //SqlParameter BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                //BrandID.Direction = ParameterDirection.Input;
                //BrandID.Value = RequestData.BrandDivisionRecord.BrandID;

                foreach (PromotionMappingTypes objBrandDivision in StoreMasterList)
                {
                    sSql.Append("<PromotionMapping>");
                    sSql.Append("<ID>" + objBrandDivision.ID + "</ID>");
                    sSql.Append("<StoreID>" + (objBrandDivision.StoreID) + "</StoreID>");
                    sSql.Append("<StoreCode>" + (objBrandDivision.StoreCode) + "</StoreCode>");
                    sSql.Append("<StoreName>" + objBrandDivision.StoreName + "</StoreName>");
                    sSql.Append("<Active>" + (objBrandDivision.Active) + "</Active>");
                    sSql.Append("<CreateBy>" + (objBrandDivision.CreateBy) + "</CreateBy>");
                    sSql.Append("<IsDeleted>" + (objBrandDivision.IsDeleted) + "</IsDeleted>");
                    sSql.Append("<WNPromotionID>" + objBrandDivision.WNPromotionID + "</WNPromotionID>");
                    sSql.Append("<WNPromotionCode>" + (objBrandDivision.WNPromotionCode) + "</WNPromotionCode>");
                    sSql.Append("</PromotionMapping>");
                }

                var PromotionMapData = _CommandObj.Parameters.Add("@PromotionMapData", SqlDbType.Xml);
                PromotionMapData.Direction = ParameterDirection.Input;
                PromotionMapData.Value = sSql.ToString();

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@PromotionMapIDs", SqlDbType.VarChar, 500);
                ID2.Direction = ParameterDirection.Output;

                // Changed by Senthamil @ 07.09.2018
                SqlParameter PromotionID = _CommandObj.Parameters.Add("@PromotionID", SqlDbType.Int);
                PromotionID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Promotion Mapping");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.IDs = ID2.Value.ToString();
                    ResponseData.IDs = PromotionID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Promotion Mapping");
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotion Mapping");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.ItIsInRelationdhip.Replace("{}", "BrandDivision Map");
                }
            }


            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotion Mapping");
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
            var PromotionMappingRecord = new PromotionMappingTypes();
            var RequestData = (DeletePromotionMappingRequest)RequestObj;
            var ResponseData = new DeletePromotionMappingResponse();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from PromotionMapping where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Promotion Mapping");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Promotion Mapping");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var PromotionMappingRecord = new PromotionMappingTypes();
            var RequestData = (SelectByPromotionMappingIDRequest)RequestObj;
            var ResponseData = new SelectByPromotionMappingIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from PromotionMapping with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotionMapping = new PromotionMappingTypes();
                        objPromotionMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objPromotionMapping.StoreName = Convert.ToString(objReader["StoreName"]);
                        objPromotionMapping.WNPromotionID = objReader["WNPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["WNPromotionID"]) : 0;
                        objPromotionMapping.WNPromotionCode = Convert.ToString(objReader["WNPromotionCode"]);
                        objPromotionMapping.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotionMapping.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotionMapping.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotionMapping.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotionMapping.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotionMapping.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.PromotionMappingRecord = objPromotionMapping;
                        ResponseData.ResponseDynamicData = objPromotionMapping;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotion Mapping");
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
            var PromotionMappingList = new List<PromotionMappingTypes>();
            var RequestData = (SelectAllPromotionMappingRequest)RequestObj;
            var ResponseData = new SelectAllPromotionMappingResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = " Select SM.ID as StoreID,SM.StoreCode,SM.StoreName,PM.ID,PM.WNPromotionID,PM.WNPromotionCode from PromotionMapping PM join StoreMaster SM with(nolock) ON SM.StoreCode=PM.StoreCode  and PM.Active='True'";
                string sSql = " Select SM.ID as StoreID,SM.StoreCode,SM.StoreName,PM.ID,PM.WNPromotionID,PM.WNPromotionCode from StoreMaster SM with(nolock) left join PromotionMapping PM with(nolock) ON SM.StoreCode=PM.StoreCode  and PM.Active='True'";

                //sSql = sSql + " and WNPromotionID='" + RequestData.WNPromotionID + "' where SM.CountryID in ('" + RequestData.Countries + "')";
                sSql = sSql + " and WNPromotionID='" + RequestData.WNPromotionID + "'";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotionMapping = new PromotionMappingTypes();
                        objPromotionMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionMapping.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objPromotionMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objPromotionMapping.StoreName = Convert.ToString(objReader["StoreName"]);
                        objPromotionMapping.WNPromotionID = objReader["WNPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["WNPromotionID"]) : 0;
                        objPromotionMapping.WNPromotionCode = Convert.ToString(objReader["WNPromotionCode"]);

                        //objBrandDivision.BrandCode = Convert.ToString(objReader["BrandCode"]);

                        if (objPromotionMapping.WNPromotionID > 0)
                        {
                            objPromotionMapping.Active = true;
                        }

                        PromotionMappingList.Add(objPromotionMapping);
                    }
                    ResponseData.PromotionMappingList = PromotionMappingList;
                    ResponseData.ResponseDynamicData = PromotionMappingList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    string sSql1 = " Select * from  StoreMaster where Active='True'";
                    _CommandObj = new SqlCommand(sSql1, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    while (objReader.Read())
                    {
                        var objPromotionMapping = new PromotionMappingTypes();
                        objPromotionMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionMapping.StoreID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotionMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objPromotionMapping.StoreName = Convert.ToString(objReader["StoreName"]);
                        objPromotionMapping.WNPromotionID =  0;
                        objPromotionMapping.WNPromotionCode = "";

                        //objBrandDivision.BrandCode = Convert.ToString(objReader["BrandCode"]);

                        if (objPromotionMapping.WNPromotionID > 0)
                        {
                            objPromotionMapping.Active = true;
                        }

                        PromotionMappingList.Add(objPromotionMapping);
                    }
                    ResponseData.PromotionMappingList = PromotionMappingList;
                    ResponseData.ResponseDynamicData = PromotionMappingList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    //ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    //ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotion Mapping");
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

        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
