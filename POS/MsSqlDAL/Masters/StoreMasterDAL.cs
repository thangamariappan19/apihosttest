using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizRequest.Masters.StoreMasterResponse;
//using EasyBizRequest.Transactions.Promotions.FamilyDiscount;
using EasyBizResponse.Masters.StoreMasterResponse;
//using EasyBizResponse.Transactions.Promotions.FamilyDiscount;
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
   public class StoreMasterDAL: BaseStoreMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override SelectStoreMasterLookUpResponse SelectStoreMasterLookUp(SelectStoreMasterLookUpRequest RequestObj)
        {
            var StoreMasterList = new List<StoreMaster>();
            var RequestData = (SelectStoreMasterLookUpRequest)RequestObj;
            var ResponseData = new SelectStoreMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer && RequestData.Type == "Reports")
                //{
                //    sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock)  where Active='True' and StoreGroupID='" + RequestData.StoreGroupID + "' ";
                //}
                if (RequestData.Type == "Reports")
                {
                    sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock)  where Active='True' and StoreGroupID='" + RequestData.StoreGroupID + "' ";
                }
                else if (RequestData.Type != "Reports" && RequestData.StateID != 0)
                {
                    sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock)  where Active='True' and stateID='" + RequestData.StateID + "' ";
                }
                else if (RequestData.CountryID != 0 && (RequestData.type == "" || RequestData.type==null))
                    {
                        sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock)  where Active='True' and CountryID='" + RequestData.CountryID + "' ";
                    }                    
                    else if (RequestData.CountryIDs != null && RequestData.CountryIDs != string.Empty)
                    {
                        sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock)  where Active='True' and CountryID in(" + RequestData.CountryIDs + ")";
                    }
                    else if (RequestData.CountryID != 0 && RequestData.type == "SalesTarget")
                    {
                        sQuery = "select distinct sbm.StoreID as ID,sm.StoreName,sm.StoreCode from StoreBrandMapping SBM join StoreMaster SM on sbm.StoreID=sm.ID  where sbm.BrandID=" + RequestData.BrandID + " ";
                    }
                    else
                    {
                        sQuery = "Select ID,StoreName,StoreCode from StoreMaster with(NoLock)  where Active='True'";
                    }
                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
             
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();
                        objStoreMaster.ID = Convert.ToInt32(objReader["ID"]);
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        StoreMasterList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreMasterList = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
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

       
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStoreMasterRequest)RequestObj;
            var ResponseData = new SaveStoreMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("API_InsertStoreMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreMasterRecord.ID);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.StoreMasterRecord.StoreCode);
                _CommandObj.Parameters.AddWithValue("@StoreName", RequestData.StoreMasterRecord.StoreName);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.StoreMasterRecord.CountryID);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.StoreMasterRecord.CountryCode);
                _CommandObj.Parameters.AddWithValue("@StateID", RequestData.StoreMasterRecord.StateID);
                _CommandObj.Parameters.AddWithValue("@StateCode", RequestData.StoreMasterRecord.StateCode);
                _CommandObj.Parameters.AddWithValue("@StoreGroupID", RequestData.StoreMasterRecord.StoreGroup);
                _CommandObj.Parameters.AddWithValue("@StoreGroupCode", RequestData.StoreMasterRecord.StoreGroupCode);
                _CommandObj.Parameters.AddWithValue("@StoreCompanyID", RequestData.StoreMasterRecord.StoreCompany);
                _CommandObj.Parameters.AddWithValue("@StoreCompanyCode", RequestData.StoreMasterRecord.StoreCompanyCode);
                _CommandObj.Parameters.AddWithValue("@Brand_ID", RequestData.StoreMasterRecord.Brand);
                _CommandObj.Parameters.AddWithValue("@ShopBrand_ID", RequestData.StoreMasterRecord.ShopBrand);
                _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.StoreMasterRecord.PriceListID);
                _CommandObj.Parameters.AddWithValue("@PriceListCode", RequestData.StoreMasterRecord.PriceListCode);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.StoreMasterRecord.CreateBy);
                _CommandObj.Parameters.AddWithValue("@StoreType", RequestData.StoreMasterRecord.StoreType);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.StoreMasterRecord.Remarks);
                _CommandObj.Parameters.AddWithValue("@Address", RequestData.StoreMasterRecord.Address);
                _CommandObj.Parameters.AddWithValue("@Location", RequestData.StoreMasterRecord.Location);
                _CommandObj.Parameters.AddWithValue("@StoreSize", RequestData.StoreMasterRecord.StoreSize);
                _CommandObj.Parameters.AddWithValue("@NoOfOptions", RequestData.StoreMasterRecord.NoOfOptions);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.StoreMasterRecord.Active);
                _CommandObj.Parameters.AddWithValue("@RetailID", RequestData.StoreMasterRecord.RetailID);
                _CommandObj.Parameters.AddWithValue("@TaxID", RequestData.StoreMasterRecord.TaxID);
                //sqlCommon.GetSQLServerDateString(RequestData.BusinessDate)
                _CommandObj.Parameters.AddWithValue("@StartDate", sqlCommon.GetSQLServerDateString(RequestData.StoreMasterRecord.StartDate));
                _CommandObj.Parameters.AddWithValue("@EndDate", sqlCommon.GetSQLServerDateString(RequestData.StoreMasterRecord.EndDate));
                _CommandObj.Parameters.AddWithValue("@Grade", RequestData.StoreMasterRecord.Grade);
                //_CommandObj.Parameters.AddWithValue("@StoreHeader", RequestData.StoreMasterRecord.StoreHeader);
                //_CommandObj.Parameters.AddWithValue("@StoreFooter", RequestData.StoreMasterRecord.StoreFooter);
                _CommandObj.Parameters.AddWithValue("@PrintCount", RequestData.StoreMasterRecord.PrintCount);
                _CommandObj.Parameters.AddWithValue("@ReturnPrintCount", RequestData.StoreMasterRecord.ReturnPrintCount);
                _CommandObj.Parameters.AddWithValue("@ExchangePrintCount", RequestData.StoreMasterRecord.ExchangePrintCount);
                _CommandObj.Parameters.AddWithValue("@StoreImage", RequestData.StoreMasterRecord.StoreImage);
                _CommandObj.Parameters.AddWithValue("@LicenseImage", RequestData.StoreMasterRecord.LicenseImage);
                //_CommandObj.Parameters.AddWithValue("@StoreImage", RequestData.StoreImageList[0].StoreImage);
                _CommandObj.Parameters.AddWithValue("@EnableOnlineStock", RequestData.StoreMasterRecord.EnableOnlineStock);
                _CommandObj.Parameters.AddWithValue("@EnableOrderFulFillment", RequestData.StoreMasterRecord.EnableOrderFulFillment);
                _CommandObj.Parameters.AddWithValue("@EnableFingerPrint", RequestData.StoreMasterRecord.EnableFingerPrint);
                _CommandObj.Parameters.AddWithValue("@CityID", RequestData.StoreMasterRecord.CityID);

                _CommandObj.Parameters.AddWithValue("@DiskID", RequestData.StoreMasterRecord.DiskID);
                _CommandObj.Parameters.AddWithValue("@CPUID", RequestData.StoreMasterRecord.CPUID);

                SqlParameter StoreHeader = _CommandObj.Parameters.Add("@StoreHeader", SqlDbType.NVarChar);
                StoreHeader.Direction = ParameterDirection.Input;
                StoreHeader.Value = RequestData.StoreMasterRecord.StoreHeader;

                SqlParameter StoreFooter = _CommandObj.Parameters.Add("@StoreFooter", SqlDbType.NVarChar);
                StoreFooter.Direction = ParameterDirection.Input;
                StoreFooter.Value = RequestData.StoreMasterRecord.StoreFooter;

                SqlParameter EmailTemplate = _CommandObj.Parameters.Add("@EmailTemplate", SqlDbType.NVarChar);
                EmailTemplate.Direction = ParameterDirection.Input;
                EmailTemplate.Value = RequestData.StoreMasterRecord.EmailTemplate;

                SqlParameter SMSTemplate = _CommandObj.Parameters.Add("@SMSTemplate", SqlDbType.NVarChar);
                SMSTemplate.Direction = ParameterDirection.Input;
                SMSTemplate.Value = RequestData.StoreMasterRecord.SMSTemplate;

                var DocumentNumberingDetails = _CommandObj.Parameters.Add("@StoreBrandMappingDetails", SqlDbType.Xml);
                DocumentNumberingDetails.Direction = ParameterDirection.Input;
                DocumentNumberingDetails.Value = StoreBrandMappingXml(RequestData.StoreBrandMappingList);

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;
             

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Store");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Store");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        private string StoreBrandMappingXml(List<StoreBrandMapping> StoreBrandMappingList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StoreBrandMapping objDocumentNumberingDetails in StoreBrandMappingList)
            {
                sSql.Append("<StoreBrandMappingDetails>");
                sSql.Append("<ID>" + (objDocumentNumberingDetails.ID) + "</ID>");
                sSql.Append("<CountryID>" + (objDocumentNumberingDetails.CountryID) + "</CountryID>");
                sSql.Append("<StoreID>" + (objDocumentNumberingDetails.StoreID) + "</StoreID>");
                sSql.Append("<BrandID>" + (objDocumentNumberingDetails.BrandID) + "</BrandID>");
                sSql.Append("<CountryCode>" + objDocumentNumberingDetails.CountryCode + "</CountryCode>");
                sSql.Append("<StoreCode>" + (objDocumentNumberingDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<BrandCode>" + objDocumentNumberingDetails.BrandCode + "</BrandCode>");
                sSql.Append("<FranchiseCode>" + (objDocumentNumberingDetails.FranchiseCode) + "</FranchiseCode>");
                sSql.Append("<FranchiseID>" + objDocumentNumberingDetails.FranchiseID + "</FranchiseID>");
                sSql.Append("</StoreBrandMappingDetails>");
            }
            return sSql.ToString();
        }
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateStoreMasterRequest)RequestObj;
            var ResponseData = new UpdateStoreMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateStoreMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;


                _CommandObj.Parameters.AddWithValue("@ID", RequestData.StoreMasterRecord.ID);
                _CommandObj.Parameters.AddWithValue("@StoreCode", RequestData.StoreMasterRecord.StoreCode);
                _CommandObj.Parameters.AddWithValue("@StoreName", RequestData.StoreMasterRecord.StoreName);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.StoreMasterRecord.CountrySetting);
                _CommandObj.Parameters.AddWithValue("@CountryCode", RequestData.StoreMasterRecord.CountryCode);
                _CommandObj.Parameters.AddWithValue("@StateID", RequestData.StoreMasterRecord.StateID);
                _CommandObj.Parameters.AddWithValue("@StateCode", RequestData.StoreMasterRecord.StateCode);
                _CommandObj.Parameters.AddWithValue("@StoreGroupID", RequestData.StoreMasterRecord.StoreGroup);
                _CommandObj.Parameters.AddWithValue("@StoreGroupCode", RequestData.StoreMasterRecord.StoreGroupCode);
                _CommandObj.Parameters.AddWithValue("@StoreCompanyID", RequestData.StoreMasterRecord.StoreCompany);
                _CommandObj.Parameters.AddWithValue("@StoreCompanyCode", RequestData.StoreMasterRecord.StoreCompanyCode);
                _CommandObj.Parameters.AddWithValue("@Brand_ID", RequestData.StoreMasterRecord.Brand);
                _CommandObj.Parameters.AddWithValue("@ShopBrand_ID", RequestData.StoreMasterRecord.ShopBrand);
                _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.StoreMasterRecord.PriceListID);
                _CommandObj.Parameters.AddWithValue("@PriceListCode", RequestData.StoreMasterRecord.PriceListCode);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.StoreMasterRecord.CreateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.StoreMasterRecord.SCN);              
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.StoreMasterRecord.Remarks);
                _CommandObj.Parameters.AddWithValue("@Address", RequestData.StoreMasterRecord.Address);
                _CommandObj.Parameters.AddWithValue("@Location", RequestData.StoreMasterRecord.Location);
                _CommandObj.Parameters.AddWithValue("@StoreSize", RequestData.StoreMasterRecord.StoreSize);
                _CommandObj.Parameters.AddWithValue("@NoOfOptions", RequestData.StoreMasterRecord.NoOfOptions);
                _CommandObj.Parameters.AddWithValue("@StoreType", RequestData.StoreMasterRecord.StoreType);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.StoreMasterRecord.Active);
                _CommandObj.Parameters.AddWithValue("@RetailID", RequestData.StoreMasterRecord.RetailID);
                _CommandObj.Parameters.AddWithValue("@TaxID", RequestData.StoreMasterRecord.TaxID);
                _CommandObj.Parameters.AddWithValue("@StartDate", RequestData.StoreMasterRecord.StartDate);
                _CommandObj.Parameters.AddWithValue("@EndDate", RequestData.StoreMasterRecord.EndDate);
                _CommandObj.Parameters.AddWithValue("@Grade", RequestData.StoreMasterRecord.Grade);
                //_CommandObj.Parameters.AddWithValue("@StoreHeader", RequestData.StoreMasterRecord.StoreHeader);
                //_CommandObj.Parameters.AddWithValue("@StoreFooter", Footer);
                _CommandObj.Parameters.AddWithValue("@EnableOnlineStock", RequestData.StoreMasterRecord.EnableOnlineStock);
                _CommandObj.Parameters.AddWithValue("@EnableOrderFulFillment", RequestData.StoreMasterRecord.EnableOrderFulFillment);
                _CommandObj.Parameters.AddWithValue("@EnableFingerPrint", RequestData.StoreMasterRecord.EnableFingerPrint);
                _CommandObj.Parameters.AddWithValue("@CityID", RequestData.StoreMasterRecord.CityID);

                SqlParameter StoreHeader = _CommandObj.Parameters.Add("@StoreHeader", SqlDbType.NVarChar);
                StoreHeader.Direction = ParameterDirection.Input;
                StoreHeader.Value = RequestData.StoreMasterRecord.StoreHeader;

                SqlParameter StoreFooter = _CommandObj.Parameters.Add("@StoreFooter", SqlDbType.NVarChar);
                StoreFooter.Direction = ParameterDirection.Input;
                StoreFooter.Value = RequestData.StoreMasterRecord.StoreFooter;

                SqlParameter EmailTemplate = _CommandObj.Parameters.Add("@EmailTemplate", SqlDbType.NVarChar);
                EmailTemplate.Direction = ParameterDirection.Input;
                EmailTemplate.Value = RequestData.StoreMasterRecord.EmailTemplate;

                SqlParameter SMSTemplate = _CommandObj.Parameters.Add("@SMSTemplate", SqlDbType.NVarChar);
                SMSTemplate.Direction = ParameterDirection.Input;
                SMSTemplate.Value = RequestData.StoreMasterRecord.SMSTemplate;

                var DocumentNumberingDetails = _CommandObj.Parameters.Add("@StoreBrandMappingDetails", SqlDbType.Xml);
                DocumentNumberingDetails.Direction = ParameterDirection.Input;
                DocumentNumberingDetails.Value = StoreBrandMappingXml(RequestData.StoreBrandMappingList);


                _CommandObj.Parameters.AddWithValue("@PrintCount", RequestData.StoreMasterRecord.PrintCount);
                _CommandObj.Parameters.AddWithValue("@ReturnPrintCount", RequestData.StoreMasterRecord.ReturnPrintCount);
                _CommandObj.Parameters.AddWithValue("@ExchangePrintCount", RequestData.StoreMasterRecord.ExchangePrintCount);
                _CommandObj.Parameters.AddWithValue("@StoreImage", RequestData.StoreMasterRecord.StoreImage);
                _CommandObj.Parameters.AddWithValue("@LicenseImage", RequestData.StoreMasterRecord.LicenseImage);   
                _CommandObj.Parameters.AddWithValue("@DiskID", RequestData.StoreMasterRecord.DiskID);
                _CommandObj.Parameters.AddWithValue("@CPUID", RequestData.StoreMasterRecord.CPUID);                

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Store");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Store");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Store Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StateMasterRecord = new StateMaster();
            var RequestData = (DeleteStoreMasterRequest)RequestObj;
            var ResponseData = new DeleteStoreMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Delete from StoreMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Store Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Store Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StoreMasterList = new StoreMaster();
            var RequestData = (SelectByIDStoreMasterRequest)RequestObj;
            var ResponseData = new SelectByIDStoreMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                //string sSql = "Select *,0 as BrandID from StoreMaster with(NoLock) where ID='{0}'";
                string sSql = "Select TOP 1 *,0 as BrandID from StoreMaster SM with(NoLock) left JOIN StoreBinConfig SBG ON SM.ID=SBG.StoreID where SM.ID='{0}'";
                //string sSql = "Select sbm.FranchiseID,sbm.FranchiseCode,* from StoreMaster SM left join StoreBrandMapping SBM on sm.id=sbm.storeid where sm.ID='{0}'";
                //string sSql = "Select SM.*,BM.ID as 'BrandID' from StoreMaster SM with(nolock) JOIN BrandMaster BM with(Nolock) on SM.Brand_ID = BM.BrandName where sm.ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();
                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStoreMaster.CountrySetting =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objStoreMaster.StateID = objReader["StateID"] != DBNull.Value ? Convert.ToInt32(objReader["StateID"]) : 0;
                        objStoreMaster.Brand = Convert.ToString(objReader["Brand_ID"]);
                        objStoreMaster.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objStoreMaster.ShopBrand = Convert.ToString(objReader["ShopBrand_ID"]);
                        objStoreMaster.StoreGroup = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                        objStoreMaster.StoreCompany =objReader["StoreCompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreCompanyID"]) :0;
                        objStoreMaster.PriceListID =objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) :0;
                        objStoreMaster.RetailID =objReader["RetailID"] != DBNull.Value ? Convert.ToInt32(objReader["RetailID"]) :0;
                        objStoreMaster.TaxID = objReader["TaxID"] != DBNull.Value ? Convert.ToInt32(objReader["TaxID"]) : 0;
                        objStoreMaster.StoreType = Convert.ToString(objReader["StoreType"]);
                        objStoreMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStoreMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStoreMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStoreMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStoreMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objStoreMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objStoreMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStoreMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStoreMaster.Grade = objReader["Grade"].ToString();
                        objStoreMaster.StoreHeader = Convert.ToString(objReader["StoreHeader"]);
                        objStoreMaster.StoreFooter = Convert.ToString(objReader["StoreFooter"]);
                        objStoreMaster.PrintCount = Convert.ToString(objReader["PrintCount"]);
                        objStoreMaster.ReturnPrintCount = Convert.ToString(objReader["ReturnPrintCount"]);
                        objStoreMaster.ExchangePrintCount = Convert.ToString(objReader["ExchangePrintCount"]); 
                        objStoreMaster.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objStoreMaster.LicenseImage = objReader["LicenseImage"] != DBNull.Value ? (byte[])objReader["LicenseImage"] : null;
                        objStoreMaster.Address = Convert.ToString(objReader["Address"]);
                        objStoreMaster.Location = Convert.ToString(objReader["Location"]);
                        objStoreMaster.NoOfOptions = objReader["NoOfOptions"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfOptions"]) : 0;
                        objStoreMaster.StoreSize = objReader["StoreSize"] != DBNull.Value ? Convert.ToDecimal(objReader["StoreSize"]) : 0;
                        objStoreMaster.DiskID = Convert.ToString(objReader["DiskID"]);
                        objStoreMaster.CPUID = Convert.ToString(objReader["CPUID"]);
                        objStoreMaster.ToMailID = Convert.ToString(objReader["ToMailID"]);
                        objStoreMaster.CCMailID = Convert.ToString(objReader["CCMailID"]);
                        objStoreMaster.EmailTemplate = Convert.ToString(objReader["EmailTemplate"]);
                        objStoreMaster.SMSTemplate = Convert.ToString(objReader["SMSTemplate"]);
                        objStoreMaster.EnableOnlineStock = objReader["EnableOnlineStock"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableOnlineStock"]) : true;
                        objStoreMaster.EnableOrderFulFillment = objReader["EnableOrderFulFillment"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableOrderFulFillment"]) : true;
                        objStoreMaster.EnableFingerPrint = objReader["EnableFingerPrint"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableFingerPrint"]) : true;
                        objStoreMaster.CityID = objReader["CityID"] != DBNull.Value ? Convert.ToInt32(objReader["CityID"]) : 0;
                        //objStoreMaster.FranchiseID = objReader["FranchiseID"] != DBNull.Value ? Convert.ToInt32(objReader["FranchiseID"]) : 0;
                        //objStoreMaster.FranchiseCode = Convert.ToString(objReader["FranchiseCode"]);
                        objStoreMaster.EnableBin = objReader["EnableBin"]!= DBNull.Value ? Convert.ToInt32(objReader["EnableBin"]):0;
                        objStoreMaster.SelectStoreBrandMappingList = new List<StoreBrandMapping>();

                        SelectByIDStorebrandMappingRequest objSelectByIDStorebrandMappingRequest = new SelectByIDStorebrandMappingRequest();
                        SelectByIDStorebrandmappingrespons objSelectByIDStorebrandmappingrespons = new SelectByIDStorebrandmappingrespons();
                        objSelectByIDStorebrandMappingRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;                      

                        objSelectByIDStorebrandmappingrespons = SelectByIDStoreBrandMappingDetails(objSelectByIDStorebrandMappingRequest);
                        if (objSelectByIDStorebrandmappingrespons.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStoreMaster.SelectStoreBrandMappingList = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList;
                            objStoreMaster.FranchiseID = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList.FirstOrDefault().FranchiseID;
                            objStoreMaster.FranchiseCode = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList.FirstOrDefault().FranchiseCode;
                        }



                        ResponseData.StoreMasterData = objStoreMaster;
                        //ResponseData.ResponseDynamicData = objStoreMaster;
                    }
                 
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
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
        public override EasyBizResponse.Masters.StoreMasterResponse.SelectByIDStorebrandmappingrespons SelectByIDStoreBrandMappingDetails(EasyBizRequest.Masters.StoreMasterRequest.SelectByIDStorebrandMappingRequest ObjRequest)
        {
            var StoreBrandMappingList = new List<StoreBrandMapping>();
            var RequestData = (SelectByIDStorebrandMappingRequest)ObjRequest;
            var ResponseData = new SelectByIDStorebrandmappingrespons();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //var sSql = new StringBuilder();

                string sSql = "select sbm.ID,sbm.CountryID,sbm.StoreID,sbm.BrandID,sbm.StoreCode,sbm.CountryCode,sbm.BrandCode, sbm.FranchiseID, FranchiseCode from StoreBrandMapping SBM left join storemaster SM on sm.ID=sbm.StoreID where storeid=" + RequestData.ID;              
               
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreBrandMapping = new StoreBrandMapping();
                        objStoreBrandMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreBrandMapping.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objStoreBrandMapping.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objStoreBrandMapping.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objStoreBrandMapping.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objStoreBrandMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStoreBrandMapping.BrandCode = Convert.ToString(objReader["BrandCode"]);

                        objStoreBrandMapping.FranchiseID = objReader["FranchiseID"] != DBNull.Value ? Convert.ToInt32(objReader["FranchiseID"]) : 0;
                        objStoreBrandMapping.FranchiseCode = objReader["FranchiseCode"] != DBNull.Value ? Convert.ToString(objReader["FranchiseCode"]) : "";
                        StoreBrandMappingList.Add(objStoreBrandMapping);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreBrandMappingList = StoreBrandMappingList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreBrandMapping");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
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
            var StoreMasterList = new List<StoreMaster>();
            var RequestData = (SelectAllStoreMasterRequest)RequestObj;
            var ResponseData = new SelectAllStoreMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                sSql.Append("Select SM.*,SM.StoreCode,SM.StoreName,CUNM.CountryName,SGM.StoreGroupName,CS.CompanyName  from StoreMaster SM  ");
                sSql.Append("left join CountryMaster CUNM  on SM.CountryID=CUNM.ID   ");
                sSql.Append("left join StoreGroupMaster SGM  on SM.StoreGroupID=SGM.ID   ");
                sSql.Append("left join CompanySettings CS  on SM.StoreCompanyID=CS.ID   ");
                
                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql.Append("where isnull(SM.CPUID,'') <> '' and isnull(SM.CPUID,'') <> '' and SM.Active='True' ");
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                {
                    sSql.Append("where isnull(SM.CPUID,'') = '' and isnull(SM.CPUID,'') = '' and SM.Active='True' ");
                }

                sSql.Append("and CUNM.Active='" + RequestData.ShowInActiveRecords + "' ");
                sSql.Append("and  SGM.Active='" + RequestData.ShowInActiveRecords + "'  ");
                sSql.Append("and  CS.Active='" + RequestData.ShowInActiveRecords + "'  ");
                sSql.Append("order by id  desc");

               // sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();

                        objStoreMaster.ID =objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) :0;
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        objStoreMaster.CountrySetting =objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) :0;
                        objStoreMaster.Brand = Convert.ToString(objReader["Brand_ID"]);
                        objStoreMaster.StoreGroup = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;                         
                        objStoreMaster.StoreCompany = objReader["StoreCompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreCompanyID"]) : 0;
                        objStoreMaster.StoreType = Convert.ToString(objReader["StoreType"]);
                        objStoreMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStoreMaster.Address = Convert.ToString(objReader["Address"]);
                        objStoreMaster.Location = Convert.ToString(objReader["Location"]);
                        objStoreMaster.Grade = objReader["Grade"].ToString();
                        objStoreMaster.StoreHeader = Convert.ToString(objReader["StoreHeader"]);
                        objStoreMaster.StoreFooter = Convert.ToString(objReader["StoreFooter"]);
                        objStoreMaster.PrintCount = Convert.ToString(objReader["PrintCount"]);
                        objStoreMaster.ReturnPrintCount = Convert.ToString(objReader["ReturnPrintCount"]);
                        objStoreMaster.ExchangePrintCount = Convert.ToString(objReader["ExchangePrintCount"]); 
                        objStoreMaster.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        objStoreMaster.LicenseImage = objReader["LicenseImage"] != DBNull.Value ? (byte[])objReader["LicenseImage"] : null;
                        objStoreMaster.NoOfOptions = objReader["NoOfOptions"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfOptions"]) : 0;
                        objStoreMaster.StoreSize = objReader["StoreSize"] != DBNull.Value ? Convert.ToDecimal(objReader["StoreSize"]) : 0;
                        objStoreMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStoreMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStoreMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStoreMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStoreMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]):DateTime.Now;
                        objStoreMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objStoreMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStoreMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objStoreMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreMaster.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        objStoreMaster.DiskID = Convert.ToString(objReader["DiskID"]);
                        objStoreMaster.CPUID = Convert.ToString(objReader["CPUID"]);
                        objStoreMaster.ToMailID = Convert.ToString(objReader["ToMailID"]);
                        objStoreMaster.CCMailID = Convert.ToString(objReader["CCMailID"]);
                        objStoreMaster.EmailTemplate = Convert.ToString(objReader["EmailTemplate"]);
                        objStoreMaster.SMSTemplate = Convert.ToString(objReader["SMSTemplate"]);
                        objStoreMaster.EnableOnlineStock = objReader["EnableOnlineStock"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableOnlineStock"]) : true;
                        objStoreMaster.EnableOrderFulFillment = objReader["EnableOrderFulFillment"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableOrderFulFillment"]) : true;
                        objStoreMaster.EnableFingerPrint = objReader["EnableFingerPrint"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableFingerPrint"]) : true;


                        objStoreMaster.SelectStoreBrandMappingList = new List<StoreBrandMapping>();

                        SelectByIDStorebrandMappingRequest objSelectByIDStorebrandMappingRequest = new SelectByIDStorebrandMappingRequest();
                        SelectByIDStorebrandmappingrespons objSelectByIDStorebrandmappingrespons = new SelectByIDStorebrandmappingrespons();
                        objSelectByIDStorebrandMappingRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objSelectByIDStorebrandmappingrespons = SelectByIDStoreBrandMappingDetails(objSelectByIDStorebrandMappingRequest);
                        if (objSelectByIDStorebrandmappingrespons.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStoreMaster.SelectStoreBrandMappingList = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList;
                            objStoreMaster.FranchiseID = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList.FirstOrDefault().FranchiseID;
                            objStoreMaster.FranchiseCode = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList.FirstOrDefault().FranchiseCode;
                        }

                        StoreMasterList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreMasterList = StoreMasterList;
                    ResponseData.ResponseDynamicData = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public override EasyBizResponse.Masters.StoreMasterResponse.SelectAllStoreBrandMappingResponse SelectStoreBrandMappingDetails(EasyBizRequest.Masters.StoreMasterRequest.SelectAllStoreBrandMappingRequest ObjRequest)
        {
            var StoreBrandMappingList = new List<StoreBrandMapping>();
            var RequestData = (SelectAllStoreBrandMappingRequest)ObjRequest;
            var ResponseData = new SelectAllStoreBrandMappingResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                sSql.Append("select sbm.ID,sbm.CountryID,sbm.StoreID,sbm.BrandID,sbm.StoreCode,sbm.CountryCode,sbm.BrandCode from StoreBrandMapping SBM ");
                sSql.Append("left join storemaster SM on sm.ID=sbm.StoreID");
                                
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreBrandMapping = new StoreBrandMapping();
                        objStoreBrandMapping.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreBrandMapping.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objStoreBrandMapping.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objStoreBrandMapping.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objStoreBrandMapping.CountryCode = Convert.ToString(objReader["CountryCode"]);
                        objStoreBrandMapping.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStoreBrandMapping.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        StoreBrandMappingList.Add(objStoreBrandMapping);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreBrandMappingList = StoreBrandMappingList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StoreBrandMapping");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
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

       public override SelectStoreGradeLookUpResponse SelectStoreGradeLookUp(SelectStoreGradeLookUpRequest ObjRequest)
        {
            var StoreMasterList = new List<StoreGradeTypes>();
            var RequestData = (SelectStoreGradeLookUpRequest)ObjRequest;
            var ResponseData = new SelectStoreGradeLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,Grade from StoreGrade with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreGradeTypes();
                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.Grade = Convert.ToString(objReader["Grade"]);
                        StoreMasterList.Add(objStoreMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreGradeList = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
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

       public override UpdateUniqueIDResponse UpdateUniqueID(UpdateUniqueIDRequest ObjRequest)
       {
           var RequestData = (UpdateUniqueIDRequest)ObjRequest;
           var ResponseData = new UpdateUniqueIDResponse();
          
           var sqlCommon = new MsSqlCommon();

           _ConnectionString = RequestData.ConnectionString;
           _RequestFrom = RequestData.RequestFrom;

           sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
           try
           {
               if(ObjRequest.Type=="Store")
               {
                   string sSql = "update StoreMaster set CPUID='" + ObjRequest.CPUID + "',DiskID='" + ObjRequest.DiskID + "' where ID=" + ObjRequest.ID;
                   _CommandObj = new SqlCommand(sSql, _ConnectionObj);                   
               }
               else if (ObjRequest.Type == "POS")
               {
                   string sSql = "update PosMaster set CPUID='" + ObjRequest.CPUID + "',DiskID='" + ObjRequest.DiskID + "' where ID=" + ObjRequest.ID;
                   _CommandObj = new SqlCommand(sSql, _ConnectionObj);                   
               }
               _CommandObj.CommandType = CommandType.Text;
               int Update = _CommandObj.ExecuteNonQuery();

               if(Update == 1)
               {
                   ResponseData.StatusCode = Enums.OpStatusCode.Success;
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
       public override SelectStoreMasterLookUpResponse SelectStoreNameRecord(SelectStoreMasterLookUpRequest RequestObj)
       {
           var StoreMasterList = new List<StoreMaster>();
           var RequestData = (SelectStoreMasterLookUpRequest)RequestObj;
           var ResponseData = new SelectStoreMasterLookUpResponse();
           SqlDataReader objReader;
           var sqlCommon = new MsSqlCommon();
           try
           {
               _ConnectionString = RequestData.ConnectionString;
               _RequestFrom = RequestData.RequestFrom;

               sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
               _CommandObj = new SqlCommand("Select StoreName from storemaster with(NoLock) where ID='" + RequestData.StoreID + "'", _ConnectionObj);             
               _CommandObj.CommandType = CommandType.Text;
               objReader = _CommandObj.ExecuteReader();
               if (objReader.HasRows)
               {
                   while (objReader.Read())
                   {
                       var objStoreMasterTypes = new StoreMaster();
                       objStoreMasterTypes.StoreName = Convert.ToString(objReader["StoreName"]);


                       ResponseData.StoreMasterData = objStoreMasterTypes;

                       ResponseData.ResponseDynamicData = objStoreMasterTypes;
                   }

                   ResponseData.StatusCode = Enums.OpStatusCode.Success;
               }
               else
               {
                   ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                   ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Shift Master");
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
        public override SelectByIDStoreMasterResponse SelectedStoreId(SelectByIDStoreMasterRequest ObjRequest)
        {
            var StoreMasterList = new List<StoreMaster>();
            var RequestData = (SelectByIDStoreMasterRequest)ObjRequest;
            var ResponseData = new SelectByIDStoreMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    string sSql = "Select * from StoreMaster with(NoLock)";
                    sSql = string.Format(sSql, RequestData.ID);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                {
                    string sSql = "Select * from StoreMaster with(NoLock) where ID='{0}'";
                    sSql = string.Format(sSql, RequestData.FromOrToStoreID);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    string sSql = "Select * from StoreMaster with(NoLock) where ID='{0}'";
                    sSql = string.Format(sSql, RequestData.FromOrToStoreID);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.SyncService)
                {
                    string sSql = "SELECT * FROM STOREMASTER  SM with(NoLock) JOIN DBConnections DB with(NoLock)  ON SM.ID=DB.StoreID";
                    sSql = string.Format(sSql, RequestData.ID);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                else
                {
                    string sSql = "Select * from StoreMaster with(NoLock) where ID='{0}'";
                    sSql = string.Format(sSql, RequestData.ID);
                    _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                }
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();
                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        //objStoreMaster.EnableCashDrawer = objReader["EnableCashDrawer"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableCashDrawer"]) : false;

                        ResponseData.StoreMasterData = objStoreMaster;

                        StoreMasterList.Add(objStoreMaster);
                    }

                    ResponseData.StoreList = StoreMasterList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
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

        public override StoreBrandMapResponse GetStoreBrandMapping(StoreBrandMapRequest objRequest)
       {
           var StoreBrandMapLists = new List<StoreBrandMapping>();
           var RequestData = (StoreBrandMapRequest)objRequest;
           var ResponseData = new StoreBrandMapResponse();
           SqlDataReader objReader;
           var sqlCommon = new MsSqlCommon();
           try
           {
               _ConnectionString = RequestData.ConnectionString;
               _RequestFrom = RequestData.RequestFrom;

               sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
               string sSql = "Select Distinct sbm.BrandID,bm.BrandCode,bm.BrandName from StoreBrandmapping sbm join BrandMaster bm on sbm.BrandID=bm.ID where sbm.StoreID in("+ RequestData.StoreIDs +")";
               sSql = string.Format(sSql, RequestData.StoreIDs);

               _CommandObj = new SqlCommand(sSql, _ConnectionObj);
               _CommandObj.CommandType = CommandType.Text;
               objReader = _CommandObj.ExecuteReader();
               if (objReader.HasRows)
               {
                   while (objReader.Read())
                   {
                       var objStoreMaster = new StoreBrandMapping();                       
                       //objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                       objStoreMaster.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;                    
                       objStoreMaster.BrandCode = Convert.ToString(objReader["BrandCode"]);
                       objStoreMaster.BrandName = Convert.ToString(objReader["BrandName"]);
                       objStoreMaster.DiscountValue =  0;         

                       //if (objStoreMaster.BrandID > 0)
                       //{
                       //    objStoreMaster.DiscountPercentage = 0;

                       //}


                       StoreBrandMapLists.Add(objStoreMaster);                       
                       
                   }

                   ResponseData.StoreBrandMapList = StoreBrandMapLists;

                   ResponseData.StatusCode = Enums.OpStatusCode.Success;
               }
               else
               {
                   ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                   ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Family Discount");
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

        public override SelectAllStoreMasterResponse API_SelectALL(SelectAllStoreMasterRequest requestData)
        {
            var StoreMasterList = new List<StoreMaster>();
            var RequestData = (SelectAllStoreMasterRequest)requestData;
            var ResponseData = new SelectAllStoreMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                //sSql.Append("Select SM.*,SM.StoreCode,SM.StoreName,CUNM.CountryName,SGM.StoreGroupName,CS.CompanyName  from StoreMaster SM  ");
                //sSql.Append("left join CountryMaster CUNM  on SM.CountryID=CUNM.ID   ");
                //sSql.Append("left join StoreGroupMaster SGM  on SM.StoreGroupID=SGM.ID   ");
                //sSql.Append("left join CompanySettings CS  on SM.StoreCompanyID=CS.ID   ");   

                //if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                //{
                //    sSql.Append("where isnull(SM.CPUID,'') <> '' and isnull(SM.CPUID,'') <> '' and SM.Active='True' ");
                //}
                //else if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                //{
                //    sSql.Append("where isnull(SM.CPUID,'') = '' and isnull(SM.CPUID,'') = '' and SM.Active='True' ");
                //}

                //sSql.Append("and CUNM.Active='" + RequestData.ShowInActiveRecords + "' ");
                //sSql.Append("and  SGM.Active='" + RequestData.ShowInActiveRecords + "'  ");
                //sSql.Append("and  CS.Active='" + RequestData.ShowInActiveRecords + "'  ");


                sSql.Append("Select SM.ID, SM.StoreCode, SM.StoreName, CUNM.CountryName, SGM.StoreGroupName, CS.CompanyName, SM.Brand_ID, SM.StoreType, SM.Remarks, SM.Active,RC.TOTAL_CNT [RecordCount] from StoreMaster SM  ");
                sSql.Append("left join CountryMaster CUNM  on SM.CountryID=CUNM.ID   ");
                sSql.Append("left join StoreGroupMaster SGM  on SM.StoreGroupID=SGM.ID   ");
                sSql.Append("left join CompanySettings CS  on SM.StoreCompanyID=CS.ID   ");

                sSql.Append("LEFT JOIN(Select  count(SM1.ID) As TOTAL_CNT From StoreMaster SM1 with(NoLock) ");
                sSql.Append("left join CountryMaster CUNM1  on SM1.CountryID=CUNM1.ID   ");
                sSql.Append("left join StoreGroupMaster SGM1  on SM1.StoreGroupID=SGM1.ID   ");
                sSql.Append("left join CompanySettings CS1  on SM1.StoreCompanyID=CS1.ID   ");
                sSql.Append("where SM1.Active = " + RequestData.IsActive + " ");

                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql.Append("and isnull(SM1.CPUID,'') <> '' and isnull(SM1.CPUID,'') <> '' ");
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                {
                    sSql.Append("and isnull(SM1.CPUID,'') = '' and isnull(SM1.CPUID,'') = '' ");
                }

                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or SM1.StoreCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM1.StoreName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CUNM1.CountryName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SGM1.StoreGroupName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CS1.CompanyName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM1.Brand_ID like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM1.StoreType like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM1.Remarks like  isnull('%" + RequestData.SearchString + "%','')))  As RC ON 1 = 1  ");


                sSql.Append("where SM.Active = " + RequestData.IsActive + " ");

                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sSql.Append("and isnull(SM.CPUID,'') <> '' and isnull(SM.CPUID,'') <> '' ");
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.StoreServer)
                {
                   sSql.Append("and isnull(SM.CPUID,'') = '' and isnull(SM.CPUID,'') = '' ");
                }

                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or SM.StoreCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM.StoreName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CUNM.CountryName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SGM.StoreGroupName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CS.CompanyName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM.Brand_ID like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM.StoreType like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM.Remarks like isnull('%" + RequestData.SearchString + "%','')) ");
                sSql.Append("order by SM.ID  asc ");
                sSql.Append("offset " + RequestData.Offset + " rows ");
                sSql.Append("fetch first " + RequestData.Limit + " rows only");

                // sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStoreMaster = new StoreMaster();

                        objStoreMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStoreMaster.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStoreMaster.StoreName = Convert.ToString(objReader["StoreName"]);
                        //objStoreMaster.CountrySetting = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objStoreMaster.Brand = Convert.ToString(objReader["Brand_ID"]);
                        //objStoreMaster.StoreGroup = objReader["StoreGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreGroupID"]) : 0;
                        //objStoreMaster.StoreCompany = objReader["StoreCompanyID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreCompanyID"]) : 0;
                        objStoreMaster.StoreType = Convert.ToString(objReader["StoreType"]);
                        objStoreMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objStoreMaster.Address = Convert.ToString(objReader["Address"]);
                        //objStoreMaster.Location = Convert.ToString(objReader["Location"]);
                        //objStoreMaster.Grade = objReader["Grade"].ToString();
                        //objStoreMaster.StoreHeader = Convert.ToString(objReader["StoreHeader"]);
                        //objStoreMaster.StoreFooter = Convert.ToString(objReader["StoreFooter"]);
                        //objStoreMaster.PrintCount = Convert.ToString(objReader["PrintCount"]);
                        //objStoreMaster.ReturnPrintCount = Convert.ToString(objReader["ReturnPrintCount"]);
                        //objStoreMaster.ExchangePrintCount = Convert.ToString(objReader["ExchangePrintCount"]);
                        //objStoreMaster.StoreImage = objReader["StoreImage"] != DBNull.Value ? (byte[])objReader["StoreImage"] : null;
                        //objStoreMaster.LicenseImage = objReader["LicenseImage"] != DBNull.Value ? (byte[])objReader["LicenseImage"] : null;
                        //objStoreMaster.NoOfOptions = objReader["NoOfOptions"] != DBNull.Value ? Convert.ToInt32(objReader["NoOfOptions"]) : 0;
                        //objStoreMaster.StoreSize = objReader["StoreSize"] != DBNull.Value ? Convert.ToDecimal(objReader["StoreSize"]) : 0;
                        //objStoreMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStoreMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStoreMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStoreMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStoreMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        //objStoreMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        //objStoreMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStoreMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStoreMaster.CountryName = Convert.ToString(objReader["CountryName"]);
                        objStoreMaster.StoreGroupName = Convert.ToString(objReader["StoreGroupName"]);
                        objStoreMaster.CompanyName = Convert.ToString(objReader["CompanyName"]);
                        //objStoreMaster.DiskID = Convert.ToString(objReader["DiskID"]);
                        //objStoreMaster.CPUID = Convert.ToString(objReader["CPUID"]);
                        //objStoreMaster.ToMailID = Convert.ToString(objReader["ToMailID"]);
                        //objStoreMaster.CCMailID = Convert.ToString(objReader["CCMailID"]);
                        //objStoreMaster.EmailTemplate = Convert.ToString(objReader["EmailTemplate"]);
                        //objStoreMaster.SMSTemplate = Convert.ToString(objReader["SMSTemplate"]);
                        //objStoreMaster.EnableOnlineStock = objReader["EnableOnlineStock"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableOnlineStock"]) : true;
                        //objStoreMaster.EnableOrderFulFillment = objReader["EnableOrderFulFillment"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableOrderFulFillment"]) : true;
                        //objStoreMaster.EnableFingerPrint = objReader["EnableFingerPrint"] != DBNull.Value ? Convert.ToBoolean(objReader["EnableFingerPrint"]) : true;


                        objStoreMaster.SelectStoreBrandMappingList = new List<StoreBrandMapping>();

                        SelectByIDStorebrandMappingRequest objSelectByIDStorebrandMappingRequest = new SelectByIDStorebrandMappingRequest();
                        SelectByIDStorebrandmappingrespons objSelectByIDStorebrandmappingrespons = new SelectByIDStorebrandmappingrespons();
                        objSelectByIDStorebrandMappingRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objSelectByIDStorebrandmappingrespons = SelectByIDStoreBrandMappingDetails(objSelectByIDStorebrandMappingRequest);
                        if (objSelectByIDStorebrandmappingrespons.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStoreMaster.SelectStoreBrandMappingList = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList;
                            objStoreMaster.FranchiseID = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList.FirstOrDefault().FranchiseID;
                            objStoreMaster.FranchiseCode = objSelectByIDStorebrandmappingrespons.StoreBrandMappingList.FirstOrDefault().FranchiseCode;
                        }

                        StoreMasterList.Add(objStoreMaster);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StoreMasterList = StoreMasterList;
                    //ResponseData.ResponseDynamicData = StoreMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
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
