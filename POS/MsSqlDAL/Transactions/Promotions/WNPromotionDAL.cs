using EasyBizAbsDAL.Transactions.Promotions;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest;
using EasyBizRequest.Transactions.Promotions.WNPromotionRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.Promotions.WNPromotionResponse;
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
    public class WNPromotionDAL : BaseWNPromotionDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveWNPromotionRequest)RequestObj;
            var ResponseData = new SaveWNPromotionResponse();
            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateWNPromotion", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter Mode = _CommandObj.Parameters.Add("@Mode", SqlDbType.Int);
                Mode.Direction = ParameterDirection.Input;
                Mode.Value = RequestData.Mode;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.WNPromotionData.ID;

                SqlParameter PromotionCode = _CommandObj.Parameters.Add("@PromotionCode", SqlDbType.VarChar);
                PromotionCode.Direction = ParameterDirection.Input;
                PromotionCode.Value = RequestData.WNPromotionData.PromotionCode;

                SqlParameter PromotionName = _CommandObj.Parameters.Add("@PromotionName", SqlDbType.VarChar);
                PromotionName.Direction = ParameterDirection.Input;
                PromotionName.Value = RequestData.WNPromotionData.PromotionName;

                SqlParameter StartDate = _CommandObj.Parameters.Add("@StartDate", SqlDbType.DateTime);
                StartDate.Direction = ParameterDirection.Input;
                StartDate.Value = RequestData.WNPromotionData.StartDate;

                SqlParameter EndDate = _CommandObj.Parameters.Add("@EndDate", SqlDbType.DateTime);
                EndDate.Direction = ParameterDirection.Input;
                EndDate.Value = RequestData.WNPromotionData.EndDate;

                SqlParameter PriceListID = _CommandObj.Parameters.Add("@PriceListID", SqlDbType.Int);
                PriceListID.Direction = ParameterDirection.Input;
                PriceListID.Value = RequestData.WNPromotionData.PriceListID;

                SqlParameter PriceListCode = _CommandObj.Parameters.Add("@PriceListCode", SqlDbType.VarChar);
                PriceListCode.Direction = ParameterDirection.Input;
                PriceListCode.Value = RequestData.WNPromotionData.PriceListCode;

                SqlParameter DefaultCountryID = _CommandObj.Parameters.Add("@DefaultCountryID", SqlDbType.Int);
                DefaultCountryID.Direction = ParameterDirection.Input;
                DefaultCountryID.Value = RequestData.WNPromotionData.DefaultCountryID;

                SqlParameter Countries = _CommandObj.Parameters.Add("@Countries", SqlDbType.VarChar);
                Countries.Direction = ParameterDirection.Input;
                Countries.Value = RequestData.WNPromotionData.Countries;

                SqlParameter UploadType = _CommandObj.Parameters.Add("@UploadType", SqlDbType.NVarChar);
                UploadType.Direction = ParameterDirection.Input;
                UploadType.Value = RequestData.WNPromotionData.UploadType;

                SqlParameter PricePointApplicable = _CommandObj.Parameters.Add("@PricePointApplicable", SqlDbType.Bit);
                PricePointApplicable.Direction = ParameterDirection.Input;
                PricePointApplicable.Value = RequestData.WNPromotionData.PricePointApplicable;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateOrUpdateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.WNPromotionData.CreateBy;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.WNPromotionData.Active;

                SqlParameter WNPromotionDetails = _CommandObj.Parameters.Add("@WNPromotionDetails", SqlDbType.Xml);
                WNPromotionDetails.Direction = ParameterDirection.Input;
                WNPromotionDetails.Value = WNPromotionDetailXML(RequestData.WNPromotionData.WNPromotionDetailsList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter WNPromotionID = _CommandObj.Parameters.Add("@WNPromotionID", SqlDbType.Int);
                WNPromotionID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();

                if (strStatusCode == "1")
                {
                    if(RequestData.WNPromotionData.ID==0)
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "WNPromotions");
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "WNPromotions");
                    }
                  
                    
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = WNPromotionID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "WNPromotions Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WNPromotions Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "WNPromotions Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var WNPromotionRecord = new WNPromotion();
            var RequestData = (SelectWNPromotionByIDRequest)RequestObj;
            var ResponseData = new SelectWNPromotionByIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery = "Select * from WNPromotion with(NoLock) Where ID=" + RequestData.ID; ;
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        WNPromotionRecord.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        WNPromotionRecord.PromotionCode = objReader["PromotionCode"] != DBNull.Value ? Convert.ToString(objReader["PromotionCode"]) : string.Empty;
                        WNPromotionRecord.PromotionName = objReader["PromotionName"] != DBNull.Value ? Convert.ToString(objReader["PromotionName"]) : string.Empty;
                        WNPromotionRecord.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        WNPromotionRecord.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        WNPromotionRecord.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        WNPromotionRecord.PriceListCode = objReader["PriceListCode"] != DBNull.Value ? Convert.ToString(objReader["PriceListCode"]) : string.Empty;
                        WNPromotionRecord.DefaultCountryID = objReader["DefaultCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCountryID"]) : 0;
                        WNPromotionRecord.UploadType = objReader["UploadType"] != DBNull.Value ? Convert.ToString(objReader["UploadType"]) : string.Empty;
                        WNPromotionRecord.Countries = objReader["Countries"] != DBNull.Value ? Convert.ToString(objReader["Countries"]) : string.Empty;
                        WNPromotionRecord.PricePointApplicable = objReader["PricePointApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["PricePointApplicable"]) : false;
                        WNPromotionRecord.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        WNPromotionRecord.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        WNPromotionRecord.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        WNPromotionRecord.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        WNPromotionRecord.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        WNPromotionRecord.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        WNPromotionRecord.WNPromotionDetailsList = new List<WNPromotionDetails>();

                        var objSelectWNPromotionDetailsRequest = new SelectWNPromotionDetailsRequest();
                        var objSelectWNPromotionDetailsResponse = new SelectWNPromotionDetailsResponse();

                        objSelectWNPromotionDetailsRequest.WNPromotionID = WNPromotionRecord.ID;
                        objSelectWNPromotionDetailsResponse = SelectWNPromotionDetailsList(objSelectWNPromotionDetailsRequest);

                        if (objSelectWNPromotionDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            WNPromotionRecord.WNPromotionDetailsList = objSelectWNPromotionDetailsResponse.WNPromotionDetailsList;
                        }
                    }
                    ResponseData.WNPromotionRecord = new WNPromotion();
                    ResponseData.WNPromotionRecord = WNPromotionRecord;
                    ResponseData.ResponseDynamicData = WNPromotionRecord;
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

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var WNPromotionList = new List<WNPromotion>();
            var RequestData = (SelectAllWNPromotionRequest)RequestObj;
            var ResponseData = new SelectAllWNPromotionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery = "Select * from WNPromotion with(NoLock)";

                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sQuery = sQuery + " WHERE (StartDate <= SYSDATETIME() OR EndDate >= SYSDATETIME()) and PriceListID=" + RequestData.PriceListID + " and Countries like '%" + RequestData.CountryID + "%'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWNPromotion = new WNPromotion();
                        objWNPromotion.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWNPromotion.PromotionCode = objReader["PromotionCode"] != DBNull.Value ? Convert.ToString(objReader["PromotionCode"]) : string.Empty;
                        objWNPromotion.PromotionName = objReader["PromotionName"] != DBNull.Value ? Convert.ToString(objReader["PromotionName"]) : string.Empty;
                        objWNPromotion.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objWNPromotion.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objWNPromotion.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objWNPromotion.DefaultCountryID = objReader["DefaultCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCountryID"]) : 0;
                        objWNPromotion.UploadType = objReader["UploadType"] != DBNull.Value ? Convert.ToString(objReader["UploadType"]) : string.Empty;
                        objWNPromotion.Countries = objReader["Countries"] != DBNull.Value ? Convert.ToString(objReader["Countries"]) : string.Empty;
                        objWNPromotion.PricePointApplicable = objReader["PricePointApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["PricePointApplicable"]) : false;
                        objWNPromotion.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objWNPromotion.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objWNPromotion.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objWNPromotion.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objWNPromotion.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWNPromotion.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objWNPromotion.WNPromotionDetailsList = new List<WNPromotionDetails>();

                        var objSelectWNPromotionDetailsRequest = new SelectWNPromotionDetailsRequest();
                        var objSelectWNPromotionDetailsResponse = new SelectWNPromotionDetailsResponse();

                        objSelectWNPromotionDetailsRequest.WNPromotionID = objWNPromotion.ID;
                        objSelectWNPromotionDetailsResponse = SelectWNPromotionDetailsList(objSelectWNPromotionDetailsRequest);

                        if (objSelectWNPromotionDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objWNPromotion.WNPromotionDetailsList = objSelectWNPromotionDetailsResponse.WNPromotionDetailsList;
                        }


                        WNPromotionList.Add(objWNPromotion);
                    }
                    ResponseData.WNPromotionList = new List<WNPromotion>();
                    ResponseData.WNPromotionList = WNPromotionList;
                    ResponseData.ResponseDynamicData = WNPromotionList;
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

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        private string WNPromotionDetailXML(List<WNPromotionDetails> WNPromotionDetailsList)
        {
            StringBuilder sbXML = new StringBuilder();
            foreach (WNPromotionDetails objWNPromotionDetails in WNPromotionDetailsList)
            {
                sbXML.Append("<WNPromotionDetails>");
                sbXML.Append("<ID>" + (objWNPromotionDetails.ID) + "</ID>");
                sbXML.Append("<CountryID>" + (objWNPromotionDetails.CountryID) + "</CountryID>");
                sbXML.Append("<Country>" + objWNPromotionDetails.Country + "</Country>");
                sbXML.Append("<StyleID>" + (objWNPromotionDetails.StyleID) + "</StyleID>");
                sbXML.Append("<StyleCode>" + (objWNPromotionDetails.StyleCode) + "</StyleCode>");
                sbXML.Append("<BrandID>" + objWNPromotionDetails.BrandID + "</BrandID>");
                sbXML.Append("<Brand>" + objWNPromotionDetails.Brand + "</Brand>");
                sbXML.Append("<WasPrice>" + objWNPromotionDetails.WasPrice + "</WasPrice>");
                sbXML.Append("<Discount>" + objWNPromotionDetails.Discount + "</Discount>");
                sbXML.Append("<NowPrice>" + objWNPromotionDetails.NowPrice + "</NowPrice>");
                sbXML.Append("<CreateOrUpdateBy>" + objWNPromotionDetails.CreateBy + "</CreateOrUpdateBy>");
                sbXML.Append("<SCN>" + objWNPromotionDetails.SCN + "</SCN>");
                sbXML.Append("</WNPromotionDetails>");
            }
            return sbXML.ToString();
        }
        public override SelectWNPromotionDetailsResponse SelectWNPromotionDetailsList(SelectWNPromotionDetailsRequest RequestObj)
        {
            var WNPromotionDetailsList = new List<WNPromotionDetails>();
            var RequestData = (SelectWNPromotionDetailsRequest)RequestObj;
            var ResponseData = new SelectWNPromotionDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery = "Select * from WNPromotionDetails with(NoLock)";

                if (RequestData.ID > 0 && RequestData.WNPromotionID > 0)
                {
                    sQuery = sQuery + " where ID=" + RequestData.ID + " and WNPromotionID=" + RequestData.WNPromotionID;
                }
                else if (RequestData.WNPromotionID > 0)
                {
                    sQuery = sQuery + " where WNPromotionID=" + RequestData.WNPromotionID;
                }
                else if (RequestData.ID > 0)
                {
                    sQuery = sQuery + " where ID=" + RequestData.ID;
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWNPromotionDetails = new WNPromotionDetails();
                        objWNPromotionDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWNPromotionDetails.WNPromotionID = objReader["WNPromotionID"] != DBNull.Value ? Convert.ToInt32(objReader["WNPromotionID"]) : 0;
                        objWNPromotionDetails.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objWNPromotionDetails.Country = objReader["Country"] != DBNull.Value ? Convert.ToString(objReader["Country"]) : string.Empty;
                        objWNPromotionDetails.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objWNPromotionDetails.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objWNPromotionDetails.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objWNPromotionDetails.Brand = objReader["Brand"] != DBNull.Value ? Convert.ToString(objReader["Brand"]) : string.Empty;
                        objWNPromotionDetails.WasPrice = objReader["WasPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["WasPrice"]) : 0;
                        objWNPromotionDetails.Discount = objReader["Discount"] != DBNull.Value ? Convert.ToDecimal(objReader["Discount"]) : 0;
                        objWNPromotionDetails.NowPrice = objReader["NowPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["NowPrice"]) : 0;
                        objWNPromotionDetails.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objWNPromotionDetails.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objWNPromotionDetails.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objWNPromotionDetails.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objWNPromotionDetails.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objWNPromotionDetails.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        WNPromotionDetailsList.Add(objWNPromotionDetails);
                    }
                    ResponseData.WNPromotionDetailsList = new List<WNPromotionDetails>();
                    ResponseData.WNPromotionDetailsList = WNPromotionDetailsList;
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

        public override SelectWNPromotionLookUpResponse SelectWNPromotionLookUp(SelectWNPromotionLookUpRequest objRequest)
        {
            var WNPromotionList = new List<WNPromotion>();
            //  var VendorGroupList = new List<VendorGroupMaster>();
            var RequestData = (SelectWNPromotionLookUpRequest)objRequest;
            var ResponseData = new SelectWNPromotionLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = " Select ID,PromotionName,PromotionCode,countries from WNPromotion with(NoLock)  where Active='true'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWNPromotion = new WNPromotion();
                        objWNPromotion.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objBrand. = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWNPromotion.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objWNPromotion.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        objWNPromotion.Countries = Convert.ToString(objReader["Countries"]);
                        WNPromotionList.Add(objWNPromotion);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WNPromotionList = WNPromotionList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WNPromotion");
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
        public override SelectWNPromotionDetailsResponse GetWNPrice(SelectWNPromotionDetailsRequest RequestObj)
        {
            var WNPriceData = new WNPromotionDetails();
            var RequestData = (SelectWNPromotionDetailsRequest)RequestObj;
            var ResponseData = new SelectWNPromotionDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sSql.Append("Select WasPrice,NowPrice from WNPromotionDetails  where StyleCode='" + RequestData.Department + "-" + RequestData.ProductCode + "'");
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWNPromotionDetails = new WNPromotionDetails();
                        objWNPromotionDetails.WasPrice = objReader["WasPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["WasPrice"]) : 0;
                        objWNPromotionDetails.NowPrice = objReader["NowPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["NowPrice"]) : 0;
                        ResponseData.WNPriceData = objWNPromotionDetails;
                        ResponseData.ResponseDynamicData = objWNPromotionDetails;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WNPromotion");
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

        public override SelectWNPromotionLookUpResponse API_SelectALL(SelectWNPromotionLookUpRequest requestData)
        {
            var WNPromotionList = new List<WNPromotion>();
            //  var VendorGroupList = new List<VendorGroupMaster>();
            var RequestData = (SelectWNPromotionLookUpRequest)requestData;
            var ResponseData = new SelectWNPromotionLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //sQuery = "Select ID, PromotionName, PromotionCode, Countries from WNPromotion with(NoLock)  where Active='true'";

                sQuery = "Select ID, PromotionName, PromotionCode, Active, RC.TOTAL_CNT [RecordCount] " +
                   "from WNPromotion with(NoLock) " +
                   "LEFT JOIN(Select  count(PM1.ID) As TOTAL_CNT From WNPromotion PM1 with(NoLock) " +
                   "where PM1.Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or PM1.PromotionName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or PM1.PromotionCode like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                   "where Active = " + RequestData.IsActive + " " +
                       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                       "or PromotionName like isnull('%" + RequestData.SearchString + "%','') " +
                       "or PromotionCode like isnull('%" + RequestData.SearchString + "%','')) " +
                       "order by ID asc " +
                       "offset " + RequestData.Offset + " rows " +
                       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWNPromotion = new WNPromotion();
                        objWNPromotion.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objBrand. = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWNPromotion.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objWNPromotion.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        //objWNPromotion.Countries = Convert.ToString(objReader["Countries"]);
                        objWNPromotion.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        WNPromotionList.Add(objWNPromotion);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.WNPromotionList = WNPromotionList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "WNPromotion");
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

        public override SelectAllWNPromotionResponse API_SelectALLWN(SelectAllWNPromotionRequest requestData)
        {
            var WNPromotionList = new List<WNPromotion>();
            var RequestData = (SelectAllWNPromotionRequest)requestData;
            var ResponseData = new SelectAllWNPromotionResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sQuery;
                //string sQuery = "Select * from WNPromotion with(NoLock)";

                //if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                //{
                //    sQuery = sQuery + " WHERE (StartDate <= SYSDATETIME() OR EndDate >= SYSDATETIME()) and PriceListID=" + RequestData.PriceListID + " and Countries like '%" + RequestData.CountryID + "%'";
                //}

                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    sQuery = "SELECT ID, PromotionCode, PromotionName, Active, RC.TOTAL_CNT [RecordCount] " +
                       "FROM WNPromotion with(NoLock) " +
                       "LEFT JOIN(Select  count(WN.ID) As TOTAL_CNT From WNPromotion WN with(NoLock) " +
                       "where WN.Active = " + RequestData.IsActive + " " +
                       "and (WN.StartDate <= SYSDATETIME() OR EndDate >= SYSDATETIME()) and WN.PriceListID=" + RequestData.PriceListID + " and WN.Countries like '%" + RequestData.CountryID + "%'" +
                           "and (isnull('" + RequestData.SearchString + "','') = '' " +
                           "or PromotionCode like isnull('%" + RequestData.SearchString + "%','') " +
                           "or PromotionName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  " +
                       "where Active = " + RequestData.IsActive + " " +
                       "and (StartDate <= SYSDATETIME() OR EndDate >= SYSDATETIME()) and PriceListID=" + RequestData.PriceListID + " and Countries like '%" + RequestData.CountryID + "%'" +
                           "and (isnull('" + RequestData.SearchString + "','') = '' " +
                           "or PromotionCode like isnull('%" + RequestData.SearchString + "%','') " +
                           "or PromotionName like isnull('%" + RequestData.SearchString + "%','')) " +
                           "order by ID asc " +
                           "offset " + RequestData.Offset + " rows " +
                           "fetch first " + RequestData.Limit + " rows only";                    
                }
                else
                {
                    sQuery = "SELECT ID, PromotionCode, PromotionName, Active, RC.TOTAL_CNT [RecordCount] " +
                       "FROM WNPromotion with(NoLock) " +
                       "LEFT JOIN(Select  count(WN.ID) As TOTAL_CNT From WNPromotion WN with(NoLock) " +
                       "where WN.Active = " + RequestData.IsActive + " " +
                           "and (isnull('" + RequestData.SearchString + "','') = '' " +
                           "or WN.PromotionCode like isnull('%" + RequestData.SearchString + "%','') " +
                           "or WN.PromotionName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1   " +
                       "where Active = " + RequestData.IsActive + " " +
                           "and (isnull('" + RequestData.SearchString + "','') = '' " +
                           "or PromotionCode like isnull('%" + RequestData.SearchString + "%','') " +
                           "or PromotionName like isnull('%" + RequestData.SearchString + "%','')) " +
                           "order by ID asc " +
                           "offset " + RequestData.Offset + " rows " +
                           "fetch first " + RequestData.Limit + " rows only";
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objWNPromotion = new WNPromotion();
                        objWNPromotion.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objWNPromotion.PromotionCode = objReader["PromotionCode"] != DBNull.Value ? Convert.ToString(objReader["PromotionCode"]) : string.Empty;
                        objWNPromotion.PromotionName = objReader["PromotionName"] != DBNull.Value ? Convert.ToString(objReader["PromotionName"]) : string.Empty;
                        //objWNPromotion.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        //objWNPromotion.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        //objWNPromotion.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        //objWNPromotion.DefaultCountryID = objReader["DefaultCountryID"] != DBNull.Value ? Convert.ToInt32(objReader["DefaultCountryID"]) : 0;
                        //objWNPromotion.UploadType = objReader["UploadType"] != DBNull.Value ? Convert.ToString(objReader["UploadType"]) : string.Empty;
                        //objWNPromotion.Countries = objReader["Countries"] != DBNull.Value ? Convert.ToString(objReader["Countries"]) : string.Empty;
                        //objWNPromotion.PricePointApplicable = objReader["PricePointApplicable"] != DBNull.Value ? Convert.ToBoolean(objReader["PricePointApplicable"]) : false;
                        //objWNPromotion.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objWNPromotion.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objWNPromotion.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objWNPromotion.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objWNPromotion.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objWNPromotion.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objWNPromotion.WNPromotionDetailsList = new List<WNPromotionDetails>();

                        var objSelectWNPromotionDetailsRequest = new SelectWNPromotionDetailsRequest();
                        var objSelectWNPromotionDetailsResponse = new SelectWNPromotionDetailsResponse();

                        objSelectWNPromotionDetailsRequest.WNPromotionID = objWNPromotion.ID;
                        objSelectWNPromotionDetailsResponse = SelectWNPromotionDetailsList(objSelectWNPromotionDetailsRequest);

                        if (objSelectWNPromotionDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objWNPromotion.WNPromotionDetailsList = objSelectWNPromotionDetailsResponse.WNPromotionDetailsList;
                        }


                        WNPromotionList.Add(objWNPromotion);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.WNPromotionList = new List<WNPromotion>();
                    ResponseData.WNPromotionList = WNPromotionList;
                    //ResponseData.ResponseDynamicData = WNPromotionList;
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
    }
}
    

