using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse;
using EasyBizResponse.Transactions.Stocks.StockAdjustment;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Stocks
{
    public class StockAdjustmentDAL : BaseStockAdjustmentDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; 
        Enums.RequestFrom _RequestFrom;  

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveStockAdjustmentRequest)RequestObj;
            var ResponseData = new SaveStockAdjustmentResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertStockAdjustment", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DocumentNumber = _CommandObj.Parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
                DocumentNumber.Direction = ParameterDirection.Input;
                DocumentNumber.Value = RequestData.StockAdjustmentRecord.DocumentNumber;
              
                //SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.Date);
                //DocumentDate.Direction = ParameterDirection.Input;
                //DocumentDate.Value = sqlCommon.GetSQLServerDateTimeString(RequestData.StockAdjustmentRecord.DocumentDate);

                SqlParameter StyleID = _CommandObj.Parameters.Add("@StyleID", SqlDbType.Int);
                StyleID.Direction = ParameterDirection.Input;
                StyleID.Value = RequestData.StockAdjustmentRecord.StyleID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StockAdjustmentRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StockAdjustmentRecord.StoreCode;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.StockAdjustmentRecord.CountryID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.StockAdjustmentRecord.CountryCode;

                SqlParameter StyleCode = _CommandObj.Parameters.Add("@StyleCode", SqlDbType.VarChar,100);
                StyleCode.Direction = ParameterDirection.Input;
                StyleCode.Value = RequestData.StockAdjustmentRecord.StyleCode;             

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StockAdjustmentRecord.CreateBy;

                SqlParameter StockAdjustmentDetails = _CommandObj.Parameters.Add("@StockAdjustmentDetails", SqlDbType.Xml);
                StockAdjustmentDetails.Direction = ParameterDirection.Input;
                StockAdjustmentDetails.Value = StockAdjustmentDetailXML(RequestData.StockAdjustmentRecord.StockAdjustmentDetailList);

                SqlParameter TransactionLogDetails = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLogDetails.Direction = ParameterDirection.Input;
                TransactionLogDetails.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "StockAdjustment");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "StockAdjustment");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockAdjustment");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockAdjustment");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;           
        }
        //public string StockAdjustmentDetailMasterXML(List<StockAdjustmentDetails> StockAdjustmentDetailMasterList)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    var sqlCommon = new MsSqlCommon();
        //    foreach (StockAdjustmentDetails objStockAdjustmentDetailMasterDetails in StockAdjustmentDetailMasterList)
        //    {
        //        sSql.Append("<StockAdjustmentDetailsData>");
        //        sSql.Append("<ID>" + objStockAdjustmentDetailMasterDetails.ID + "</ID>");
        //        sSql.Append("<SAHID>" + objStockAdjustmentDetailMasterDetails.SAHID + "</SAHID>");              
        //        sSql.Append("<SKUCode>" + (objStockAdjustmentDetailMasterDetails.SKUCode) + "</SKUCode>");
        //        sSql.Append("<SystemQuantity>" + (objStockAdjustmentDetailMasterDetails.SystemQuantity) + "</SystemQuantity>");
        //        sSql.Append("<PhysicalQuantity>" + objStockAdjustmentDetailMasterDetails.PhysicalQuantity + "</PhysicalQuantity>");              
        //        sSql.Append("</StockAdjustmentDetailsData>");

        //    }
        //    return sSql.ToString();
        //}   


        public string TransactionLogDetailMasterXML(List<TransactionLog> TransactionLogDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (TransactionLog objTransactionLogDetailMasterDetails in TransactionLogDetailMasterList)
            {
                sSql.Append("<TransactionLogDetailsData>");
                sSql.Append("<ID>" + objTransactionLogDetailMasterDetails.ID + "</ID>");
                sSql.Append("<TransactionType>" + objTransactionLogDetailMasterDetails.TransactionType + "</TransactionType>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.BusinessDate) + "</BusinessDate>");
                sSql.Append("<ActualDateTime>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ActualDateTime) + "</ActualDateTime>");
                sSql.Append("<DocumentID>" + (objTransactionLogDetailMasterDetails.DocumentID) + "</DocumentID>");
                sSql.Append("<StyleCode>" + objTransactionLogDetailMasterDetails.StyleCode + "</StyleCode>");
                sSql.Append("<SKUCode>" + objTransactionLogDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<InQty>" + objTransactionLogDetailMasterDetails.InQty + "</InQty>");
                sSql.Append("<OutQty>" + objTransactionLogDetailMasterDetails.OutQty + "</OutQty>");
                sSql.Append("<TransactionPrice>" + objTransactionLogDetailMasterDetails.TransactionPrice + "</TransactionPrice>");
                sSql.Append("<Currency>" + (objTransactionLogDetailMasterDetails.Currency) + "</Currency>");
                sSql.Append("<ExchangeRate>" + (objTransactionLogDetailMasterDetails.ExchangeRate) + "</ExchangeRate>");
                sSql.Append("<DocumentPrice>" + (objTransactionLogDetailMasterDetails.DocumentPrice) + "</DocumentPrice>");
                sSql.Append("<UserID>" + (objTransactionLogDetailMasterDetails.UserID) + "</UserID>");
                sSql.Append("<CountryID>" + (objTransactionLogDetailMasterDetails.CountryID) + "</CountryID>");
                sSql.Append("<CountryCode>" + (objTransactionLogDetailMasterDetails.CountryCode) + "</CountryCode>");
                sSql.Append("<StoreID>" + (objTransactionLogDetailMasterDetails.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objTransactionLogDetailMasterDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<DocumentNo>" + (objTransactionLogDetailMasterDetails.DocumentNo) + "</DocumentNo>");
                sSql.Append("</TransactionLogDetailsData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
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
            var StockAdjustmentRecord = new StockAdjustmentHeader();
            var RequestData = (SelectRecordStockAdjustmentRequest)RequestObj;
            var ResponseData = new SelectRecordStockAdjustmentResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from StockAdjustmentHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockAdjustmentHeader = new StockAdjustmentHeader();
                        objStockAdjustmentHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockAdjustmentHeader.DocumentNumber = Convert.ToString(objReader["DocumentNumber"]);
                        //objStockAdjustmentHeader.CreatedByUserName = Convert.ToString(objReader["UserName"]);
                        objStockAdjustmentHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockAdjustmentHeader.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objStockAdjustmentHeader.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;

                        objStockAdjustmentHeader.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objStockAdjustmentHeader.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : string.Empty;

                        objStockAdjustmentHeader.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockAdjustmentHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockAdjustmentHeader.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockAdjustmentHeader.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockAdjustmentHeader.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockAdjustmentHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        SelectStockAdjustmentDetailsRequest objRequest = new SelectStockAdjustmentDetailsRequest();
                        SelectStockAdjustmentDetailsResponse objResponse = new SelectStockAdjustmentDetailsResponse();
                        objRequest.SAHID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objResponse = SelectByStockAdjustmentDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockAdjustmentHeader.StockAdjustmentDetailList = objResponse.StockAdjustmentDetailsList;
                        }

                        ResponseData.StockAdjustmentHeaderRecord = objStockAdjustmentHeader;
                        ResponseData.ResponseDynamicData = objStockAdjustmentHeader;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockReturn");
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
            var StockAdjustmentList = new List<StockAdjustmentHeader>();
            var RequestData = (GetAllStockAdjustmentRecordRequest)RequestObj;
            var ResponseData = new GetAllStockAdjustmentRecordResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select SAH.*,UM.UserName from StockAdjustmentHeader SAH join UserMaster UM on SAH.CreateBy=um.ID AND SAH.StoreID="+RequestData.StoreID +" Order by ID desc";

               

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockAdjustmentHeader = new StockAdjustmentHeader();
                        objStockAdjustmentHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;                            
                        objStockAdjustmentHeader.DocumentNumber = Convert.ToString(objReader["DocumentNumber"]);
                        objStockAdjustmentHeader.CreatedByUserName = Convert.ToString(objReader["UserName"]);
                        objStockAdjustmentHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockAdjustmentHeader.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objStockAdjustmentHeader.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;                        
                        objStockAdjustmentHeader.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockAdjustmentHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockAdjustmentHeader.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockAdjustmentHeader.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockAdjustmentHeader.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockAdjustmentHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        SelectStockAdjustmentDetailsRequest objRequest = new SelectStockAdjustmentDetailsRequest();
                        SelectStockAdjustmentDetailsResponse objResponse = new SelectStockAdjustmentDetailsResponse();
                        objRequest.SAHID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objResponse = SelectByStockAdjustmentDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockAdjustmentHeader.StockAdjustmentDetailList = objResponse.StockAdjustmentDetailsList;
                        }

                    StockAdjustmentList.Add(objStockAdjustmentHeader);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    ResponseData.StockAdjustmentList = StockAdjustmentList;
                    ResponseData.ResponseDynamicData = StockAdjustmentList;
                    
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "");
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
        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        private string StockAdjustmentDetailXML(List<StockAdjustmentDetails> StockAdjustmentDetailList)
        {
            StringBuilder sXml = new StringBuilder();
            foreach (StockAdjustmentDetails objStockAdjustmentDetails in StockAdjustmentDetailList)
            {

                int DifferenceQty = objStockAdjustmentDetails.PhysicalQuantity - objStockAdjustmentDetails.SystemQuantity;

                sXml.Append("<StockAdjustmentDetailsData>");
                sXml.Append("<SKUCode>" + objStockAdjustmentDetails.SKUCode + "</SKUCode>");
                sXml.Append("<SystemQuantity>" + (objStockAdjustmentDetails.SystemQuantity) + "</SystemQuantity>");
                sXml.Append("<PhysicalQuantity>" + objStockAdjustmentDetails.PhysicalQuantity + "</PhysicalQuantity>");
                sXml.Append("<DifferenceQty>" + DifferenceQty + "</DifferenceQty>");
                sXml.Append("<BinID>" + objStockAdjustmentDetails.BinID + "</BinID>");
                sXml.Append("<BinCode>" + objStockAdjustmentDetails.BinCode + "</BinCode>");
                sXml.Append("<BinSubLevelCode>" + objStockAdjustmentDetails.BinSubLevelCode + "</BinSubLevelCode>");
                sXml.Append("<BarCode>" + objStockAdjustmentDetails.BarCode + "</BarCode>");
                sXml.Append("</StockAdjustmentDetailsData>");
            }
            //return sXml.ToString();
            return sXml.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
        }

        public override SelectStockAdjustmentDetailsResponse SelectByStockAdjustmentDetails(SelectStockAdjustmentDetailsRequest ObjRequest)
        {
            var StockAdjustmentDetailsList = new List<StockAdjustmentDetails>();
            var RequestData = (SelectStockAdjustmentDetailsRequest)ObjRequest;
            var ResponseData = new SelectStockAdjustmentDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select *,(PhysicalQuantity-SystemQuantity) As AdjustableQuantity from StockAdjustmentDetails ";

                if (!RequestData.ShowInActiveRecords)
                {
                    sSql = sSql + "where SAHID= " + RequestData.SAHID + "";
                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockAdjustmentDetails = new StockAdjustmentDetails();
                        objStockAdjustmentDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockAdjustmentDetails.SAHID = objReader["SAHID"] != DBNull.Value ? Convert.ToInt32(objReader["SAHID"]) : 0;                          
                        objStockAdjustmentDetails.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objStockAdjustmentDetails.SystemQuantity = objReader["SystemQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["SystemQuantity"]) : 0;                           
                        objStockAdjustmentDetails.PhysicalQuantity = objReader["PhysicalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["PhysicalQuantity"]) : 0;                          
                        objStockAdjustmentDetails.AdjustableQuantity = objReader["AdjustableQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["AdjustableQuantity"]) : 0;                           
                        StockAdjustmentDetailsList.Add(objStockAdjustmentDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockAdjustmentDetailsList = StockAdjustmentDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "");
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

        public override GetAllStockAdjustmentRecordResponse API_SelectALL(GetAllStockAdjustmentRecordRequest RequestObj)
        {
            var StockAdjustmentList = new List<StockAdjustmentHeader>();
            var RequestData = (GetAllStockAdjustmentRecordRequest)RequestObj;
            var ResponseData = new GetAllStockAdjustmentRecordResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //string sSql = "Select SAH.*,UM.UserName from StockAdjustmentHeader SAH join UserMaster UM on SAH.CreateBy=um.ID AND SAH.StoreID=" + RequestData.StoreID + " Order by ID desc";

                string sSql = "Select SAH.ID, SAH.DocumentNumber, SAH.DocumentDate , UM.UserName, SAH.Active,  RC.TOTAL_CNT [RecordCount] " +
                  "from StockAdjustmentHeader SAH with(NoLock) join UserMaster UM with(NoLock) on SAH.CreateBy=um.ID " +
                  "LEFT JOIN(Select  count(SAH1.ID) As TOTAL_CNT From StockAdjustmentHeader SAH1 with(NoLock) " +
                  "join UserMaster UM1 with(NoLock) on SAH1.CreateBy=UM1.ID " +
                  "where SAH1.Active = " + RequestData.IsActive + " " +
                  "and SAH1.StoreID = " + RequestData.StoreID + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or SAH1.DocumentNumber like isnull('%" + RequestData.SearchString + "%','') " +
                          "or CONVERT(varchar, SAH1.DocumentDate, 23) like isnull('%" + RequestData.SearchString + "%','') " +
                          "or UM1.UserName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 " +
                  "where SAH.Active = " + RequestData.IsActive + " " +
                  "and SAH.StoreID = " + RequestData.StoreID + " " +
                      "and (isnull('" + RequestData.SearchString + "','') = '' " +
                          "or SAH.DocumentNumber like isnull('%" + RequestData.SearchString + "%','') " +
                          "or CONVERT(varchar, SAH.DocumentDate, 23) like isnull('%" + RequestData.SearchString + "%','') " +
                          "or UM.UserName like isnull('%" + RequestData.SearchString + "%','')) " +
                  "order by SAH.ID asc " +
                  "offset " + RequestData.Offset + " rows " +
                  "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockAdjustmentHeader = new StockAdjustmentHeader();
                        objStockAdjustmentHeader.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockAdjustmentHeader.DocumentNumber = Convert.ToString(objReader["DocumentNumber"]);
                        objStockAdjustmentHeader.CreatedByUserName = Convert.ToString(objReader["UserName"]);
                        objStockAdjustmentHeader.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        //objStockAdjustmentHeader.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        //objStockAdjustmentHeader.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        //objStockAdjustmentHeader.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockAdjustmentHeader.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockAdjustmentHeader.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockAdjustmentHeader.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStockAdjustmentHeader.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockAdjustmentHeader.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        SelectStockAdjustmentDetailsRequest objRequest = new SelectStockAdjustmentDetailsRequest();
                        SelectStockAdjustmentDetailsResponse objResponse = new SelectStockAdjustmentDetailsResponse();
                        objRequest.SAHID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;

                        objResponse = SelectByStockAdjustmentDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockAdjustmentHeader.StockAdjustmentDetailList = objResponse.StockAdjustmentDetailsList;
                        }

                        StockAdjustmentList.Add(objStockAdjustmentHeader);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    ResponseData.StockAdjustmentList = StockAdjustmentList;
                    //ResponseData.ResponseDynamicData = StockAdjustmentList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "");
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
