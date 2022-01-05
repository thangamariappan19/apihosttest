using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.CustomerSpecialPriceMasterRequest;
using EasyBizResponse.Masters.CustomerSpecialPriceMasterResponse;
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
    public class CustomerSpecialPriceMasterDAL : BaseCustomerSpecialPriceMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveCustomerSpecialPriceMasterRequest)RequestObj;
            var ResponseData = new SaveCustomerSpecialPriceMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdateCustomerSpecialPriceMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.CustomerSpecialPriceMasterRecord.ID;

                SqlParameter ApplicablePriceList = _CommandObj.Parameters.Add("@ApplicablePriceList", SqlDbType.Int);
                ApplicablePriceList.Direction = ParameterDirection.Input;
                ApplicablePriceList.Value = RequestData.CustomerSpecialPriceMasterRecord.ApplicablePriceList;

                SqlParameter DateFrom = _CommandObj.Parameters.Add("@DateFrom", SqlDbType.Date);
                DateFrom.Direction = ParameterDirection.Input;
                DateFrom.Value = RequestData.CustomerSpecialPriceMasterRecord.DateFrom;

                SqlParameter DateTo = _CommandObj.Parameters.Add("@DateTo", SqlDbType.Date);
                DateTo.Direction = ParameterDirection.Input;
                DateTo.Value = RequestData.CustomerSpecialPriceMasterRecord.DateTo;

                SqlParameter CustomerGroup = _CommandObj.Parameters.Add("@CustomerGroup", SqlDbType.NVarChar);
                CustomerGroup.Direction = ParameterDirection.Input;
                CustomerGroup.Value = RequestData.CustomerSpecialPriceMasterRecord.CustomerGroup;

                SqlParameter DiscountType = _CommandObj.Parameters.Add("@DiscountType", SqlDbType.NVarChar);
                DiscountType.Direction = ParameterDirection.Input;
                DiscountType.Value = RequestData.CustomerSpecialPriceMasterRecord.DiscountType;

                SqlParameter DiscountValue = _CommandObj.Parameters.Add("@DiscountValue", SqlDbType.Int);
                DiscountValue.Direction = ParameterDirection.Input;
                DiscountValue.Value = RequestData.CustomerSpecialPriceMasterRecord.DiscountValue;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.CustomerSpecialPriceMasterRecord.Active;

                SqlParameter CustomerGroupUsed = _CommandObj.Parameters.Add("@CustomerGroupUsed", SqlDbType.Bit);
                CustomerGroupUsed.Direction = ParameterDirection.Input;
                CustomerGroupUsed.Value = RequestData.CustomerSpecialPriceMasterRecord.CustomerGroupUsed;

                SqlParameter CustomerMasterUsed = _CommandObj.Parameters.Add("@CustomerMasterUsed", SqlDbType.Bit);
                CustomerMasterUsed.Direction = ParameterDirection.Input;
                CustomerMasterUsed.Value = RequestData.CustomerSpecialPriceMasterRecord.CustomerMasterUsed;

                //SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                //CreateBy.Direction = ParameterDirection.Input;
                //CreateBy.Value = RequestData.CustomerSpecialPriceMasterRecord.CreateBy;

                var CustomerStoreSpecialPriceDetails = _CommandObj.Parameters.Add("@CustomerSpecialPriceStoreDetails", SqlDbType.Xml);
                CustomerStoreSpecialPriceDetails.Direction = ParameterDirection.Input;
                CustomerStoreSpecialPriceDetails.Value = CustomerStoreSpecialPriceDetailsXML(RequestData.StoreCommonUtil);

                var CustomerMasterSpecialPriceDetails = _CommandObj.Parameters.Add("@CustomerSpecialPriceCustomerDetails", SqlDbType.Xml);
                CustomerMasterSpecialPriceDetails.Direction = ParameterDirection.Input;
                CustomerMasterSpecialPriceDetails.Value = CustomerMasterSpecialPriceDetailsXML(RequestData.CustomerMasterSpecialPriceMasterList);

                var CustomerMasterSpecialCategory = _CommandObj.Parameters.Add("@CustomerSpecialPriceCategoryDetails", SqlDbType.Xml);
                CustomerMasterSpecialCategory.Direction = ParameterDirection.Input;
                CustomerMasterSpecialCategory.Value = CustomerSpecialPriceCategoryDetailsXML(RequestData.CategoryCommonUtil);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Customer Special Price");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Customer Special Price");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Special Price");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Customer Special Price");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string CustomerStoreSpecialPriceDetailsXML(List<CommonUtil> CustomerSpecialPriceMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objCustomerSpecialPriceDetailsDetails in CustomerSpecialPriceMasterList)
            {
                sSql.Append("<CustomerSpecialPriceStoreDetails>");
                sSql.Append("<ID>" + (objCustomerSpecialPriceDetailsDetails.ID) + "</ID>");
                sSql.Append("<Type>" + objCustomerSpecialPriceDetailsDetails.TypeName + "</Type>");
                sSql.Append("<Code>" + objCustomerSpecialPriceDetailsDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objCustomerSpecialPriceDetailsDetails.DocumentName) + "</Name>");
                sSql.Append("<Active>" + (objCustomerSpecialPriceDetailsDetails.Active) + "</Active>");
               
                sSql.Append("<DocumentID>" + (objCustomerSpecialPriceDetailsDetails.DocumentID) + "</DocumentID>");
                sSql.Append("</CustomerSpecialPriceStoreDetails>");
            }
            return sSql.ToString();
        }
        public string CustomerMasterSpecialPriceDetailsXML(List<CustomerMaster> CustomerSpecialPriceMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CustomerMaster objcustoCustomerSpecialPriceDetailsDetails in CustomerSpecialPriceMasterList)
            {
                sSql.Append("<CustomerSpecialPriceCustomerDetails>");
                sSql.Append("<ID>" + (objcustoCustomerSpecialPriceDetailsDetails.ID) + "</ID>");
                sSql.Append("<CustomerCode>" + objcustoCustomerSpecialPriceDetailsDetails.CustomerCode + "</CustomerCode>");
                sSql.Append("<CustomerName>" + (objcustoCustomerSpecialPriceDetailsDetails.CustomerName) + "</CustomerName>");
                sSql.Append("<Active>" + (objcustoCustomerSpecialPriceDetailsDetails.Active) + "</Active>");
                sSql.Append("<CreateBy>" + objcustoCustomerSpecialPriceDetailsDetails.CreateBy + "</CreateBy>");
                sSql.Append("<SCN>" + objcustoCustomerSpecialPriceDetailsDetails.SCN + "</SCN>");
                sSql.Append("</CustomerSpecialPriceCustomerDetails>");
            }
            return sSql.ToString();
        }
        public string CustomerSpecialPriceCategoryDetailsXML(List<CommonUtil> CustomerSpecialPriceMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objCustomerSpecialPriceCategoryDetails in CustomerSpecialPriceMasterList)
            {
                sSql.Append("<CustomerSpecialPriceCategoryDetails>");
                sSql.Append("<ID>" + (objCustomerSpecialPriceCategoryDetails.ID) + "</ID>");
                sSql.Append("<Type>" + objCustomerSpecialPriceCategoryDetails.TypeName + "</Type>");
                sSql.Append("<Code>" + objCustomerSpecialPriceCategoryDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objCustomerSpecialPriceCategoryDetails.DocumentName) + "</Name>");
                sSql.Append("<Active>" + (objCustomerSpecialPriceCategoryDetails.Active) + "</Active>");
                
                sSql.Append("<DocumentID>" + (objCustomerSpecialPriceCategoryDetails.DocumentID) + "</DocumentID>");
               // sSql.Append("<UpdateFlag>" + (objCustomerSpecialPriceCategoryDetails.Active) + "</Active>");
                //sSql.Append("<CreateBy>" + objCustomerSpecialPriceCategoryDetails.CreateBy + "</CreateBy>");
                //sSql.Append("<SCN>" + objCustomerSpecialPriceCategoryDetails.SCN + "</SCN>");
                sSql.Append("</CustomerSpecialPriceCategoryDetails>");
            }
            return sSql.ToString();
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SelectByIDCustomerSpecialPriceMasterRequest)RequestObj;
            var ResponseData = new SelectByIDCustomerSpecialPriceMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);


                string sSql = "Select * from CustomerSpecialPriceMaster  with(NoLock)  where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerSpecialPriceMasterTypes = new CustomerSpecialPriceMasterTypes();
                        objCustomerSpecialPriceMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerSpecialPriceMasterTypes.ApplicablePriceList = objReader["ApplicablePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["ApplicablePriceListID"]) : 0;
                        objCustomerSpecialPriceMasterTypes.DateFrom = objReader["DateFrom"] != DBNull.Value ? Convert.ToDateTime(objReader["DateFrom"]) : DateTime.Now;
                        objCustomerSpecialPriceMasterTypes.DateTo = objReader["DateTo"] != DBNull.Value ? Convert.ToDateTime(objReader["DateTo"]) : DateTime.Now;
                        objCustomerSpecialPriceMasterTypes.DiscountType = objReader["DiscountType"].ToString();
                        objCustomerSpecialPriceMasterTypes.CustomerGroup =objReader["CustomerGroup"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroup"]) :0;
                        objCustomerSpecialPriceMasterTypes.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        objCustomerSpecialPriceMasterTypes.CustomerGroupUsed = objReader["CustomerGroupUsed"] != DBNull.Value ? Convert.ToBoolean(objReader["CustomerGroupUsed"]) : true;
                        objCustomerSpecialPriceMasterTypes.CustomerMasterUsed = objReader["CustomerMasterUsed"] != DBNull.Value ? Convert.ToBoolean(objReader["CustomerMasterUsed"]) : true;

                        objCustomerSpecialPriceMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCustomerSpecialPriceMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCustomerSpecialPriceMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCustomerSpecialPriceMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCustomerSpecialPriceMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCustomerSpecialPriceMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        ResponseData.CustomerSpecialPriceMasterRecord = objCustomerSpecialPriceMasterTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CustomerSpecialPriceMasterTypes");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var CustomerSpecialPriceMaster = new List<CustomerSpecialPriceMasterTypes>();
            var RequestData = (SelectAllCustomerSpecialPriceMasterRequest)RequestObj;
            var ResponseData = new SelectAllCustomerSpecialPriceMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "select  a.*,b.PriceListName from CustomerSpecialPriceMaster a inner join PriceListMaster b on a.ApplicablePriceListID=b.ID ";
  
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerSpecialPriceMasterTypes = new CustomerSpecialPriceMasterTypes();
                        objCustomerSpecialPriceMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCustomerSpecialPriceMasterTypes.ApplicablePriceList =objReader["ApplicablePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["ApplicablePriceListID"]) :0;
                        objCustomerSpecialPriceMasterTypes.ApplicablePriceListName = Convert.ToString(objReader["PriceListName"]);
                        objCustomerSpecialPriceMasterTypes.CustomerGroup =objReader["CustomerGroup"] != DBNull.Value ? Convert.ToInt32(objReader["CustomerGroup"]) :0;
                        objCustomerSpecialPriceMasterTypes.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCustomerSpecialPriceMasterTypes.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        
                        SelectByIDCustomerSpecialCategoryRequest objRequest = new SelectByIDCustomerSpecialCategoryRequest();
                        SelectByIDCustomerSpecialCategoryResponse objResponse = new SelectByIDCustomerSpecialCategoryResponse();
                        objRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRequest.DetailsType = Enums.SpecialPriceRecordType.Category;
                        objRequest.Type = "";
                        objResponse = SelectByIDCustomerSpecialCategoryDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCustomerSpecialPriceMasterTypes.GetItemTypeList = objResponse.CustomerSpecialPriceCategoryRecord;
                        }

                        objRequest.DetailsType = Enums.SpecialPriceRecordType.Store;
                        objRequest.Type = "";
                        objResponse = SelectByIDCustomerSpecialCategoryDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objCustomerSpecialPriceMasterTypes.StoreList = objResponse.CustomerSpecialPriceCategoryRecord;
                        }

                        //objRequest.DetailsType = "";
                        //objRequest.Type = "";
                        //objResponse = SelectByIDCustomerSpecialCategoryDetails(objRequest);
                        //if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        //{
                        //    objCustomerSpecialPriceMasterTypes.GetCustomerList = objResponse.CustomerSpecialPriceCategoryRecord;
                        //}
                        
                        CustomerSpecialPriceMaster.Add(objCustomerSpecialPriceMasterTypes);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerSpecialPriceMasterTypesList = CustomerSpecialPriceMaster;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CustomerSpecialPriceMaster");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
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

        public override SelectByCustomerSpecialPriceDetailsResponse SelectByCustomerSpecialPriceDetails(SelectByCustomerSpecialPriceDetailsRequest ObjRequest)
        {
            var CustomerSpecialPriceDetailsList = new List<CustomerMaster>();
            var CustomerSpecialStoreDetailsList = new List<StoreGroupMaster>();
            var RequestData = (SelectByCustomerSpecialPriceDetailsRequest)ObjRequest;
            var ResponseData = new SelectByCustomerSpecialPriceDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                sQuery = ("select  ID,Code,Name,Active from CustomerSpecialPriceCustomerDetails  where  CustomerSpecialMasterHeaderID=" + RequestData.ID + " ");
             
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCustomerSpecialPriceDetails = new CustomerMaster();
                        objCustomerSpecialPriceDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        // objDocumentNumberingDetails.DocNumID =objReader["DocNumID"] != DBNull.Value ? Convert.ToInt32(objReader["DocNumID"]) :0;
                        objCustomerSpecialPriceDetails.CustomerCode = Convert.ToString(objReader["Code"]);
                        objCustomerSpecialPriceDetails.CustomerName = Convert.ToString(objReader["Name"]);
                        objCustomerSpecialPriceDetails.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CustomerSpecialPriceDetailsList.Add(objCustomerSpecialPriceDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerSpecialCustomerRecord = CustomerSpecialPriceDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "CustomerSpecialPriceDetails");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        //public override SelectByIDCustomerSpecialStoreDetailsResponse SelectByIDCustomerSpecialStoreDetails(SelectByIDCustomerSpecialStoreDetailsRequest ObjRequest)
        //{

        //    var CustomerSpecialStoreDetailsList = new List<CommonUtil>();
        //    var RequestData = (SelectByIDCustomerSpecialStoreDetailsRequest)ObjRequest;
        //    var ResponseData = new SelectByIDCustomerSpecialStoreDetailsResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

        //        sQuery = "Select CustomerSpecialMasterHeaderID,ID,Active,Code,Name from CustomerSpecialPriceStoreDetails with(NoLock)";
        //        if (RequestData.ShowInActiveRecords == false)
        //        {
        //            sQuery = sQuery + " where  CustomerSpecialMasterHeaderID=" + RequestData.ID + " ";
        //        }
        //        _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        //var sSql = new StringBuilder();
        //        //sSql.Append("select  CustomerSpecialMasterHeaderID,StoreGroupCode,StoreGroupName,Active from CustomerSpecialPriceStoreDetails  ");
        //        //sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
        //        //_CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
        //        //_CommandObj.CommandType = CommandType.Text;
        //        //objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                var objStoreMaster = new CommonUtil();
        //                objStoreMaster.ID =objReader["ID"] != DBNull.Value ? Convert.ToInt16(objReader["ID"]) :0;
        //                objStoreMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
        //                objStoreMaster.Code = Convert.ToString(objReader["Code"]);
        //                objStoreMaster.Name = Convert.ToString(objReader["Name"]);
        //                objStoreMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
        //                CustomerSpecialStoreDetailsList.Add(objStoreMaster);
        //            }
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //            ResponseData.CustomerSpecialStoreRecord = CustomerSpecialStoreDetailsList;
        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //            ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "AfSegementation Master");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    return ResponseData;
        //}

        public override SelectByIDCustomerSpecialCategoryResponse SelectByIDCustomerSpecialCategoryDetails(SelectByIDCustomerSpecialCategoryRequest ObjRequest)
        {
            var CustomerSpecialCategoryDetailsList = new List<CommonUtil>();
            var RequestData = (SelectByIDCustomerSpecialCategoryRequest)ObjRequest;
            var ResponseData = new SelectByIDCustomerSpecialCategoryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                if (RequestData.DetailsType == Enums.SpecialPriceRecordType.Category)
                {
                    sQuery = "Select CustomerSpecialMasterHeaderID,ID,Active,Code,Name,UpdateFlag,CategoryType,DocumentID from CustomerSpecialPriceCategoryDetails with(NoLock) where  CustomerSpecialMasterHeaderID=" + RequestData.ID + "";
                    
                }
                else if (RequestData.DetailsType == Enums.SpecialPriceRecordType.Store)
                {
                    sQuery = "Select CustomerSpecialMasterHeaderID,ID,Active,Code,Name,UpdateFlag,CategoryType,DocumentID from CustomerSpecialPriceStoreDetails with(NoLock) where  CustomerSpecialMasterHeaderID=" + RequestData.ID + "";
                   
                }
                else if (RequestData.DetailsType == Enums.SpecialPriceRecordType.Customer)
                {
                    sQuery = "select  ID,Code,Name,Active,CategoryType,DocumentID from CustomerSpecialPriceCustomerDetails where  CustomerSpecialMasterHeaderID=" + RequestData.ID + " ";
                   
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {

                    while (objReader.Read())
                    {
                        var objCategoryMaster = new CommonUtil();
                        objCategoryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt16(objReader["ID"]) : 0;
                        objCategoryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCategoryMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objCategoryMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objCategoryMaster.TypeName = Convert.ToString(objReader["CategoryType"]);
                        objCategoryMaster.DocumentID = objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) : 0;
                        //objCategoryMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                        CustomerSpecialCategoryDetailsList.Add(objCategoryMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerSpecialPriceCategoryRecord = CustomerSpecialCategoryDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Customer Special Price Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

      
    }
}
