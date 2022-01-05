using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using MsSqlDAL.Common;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizRequest.Transactions.Stocks.OpeningStock;
using EasyBizResponse.Transactions.Stocks.OpeningStock;
using ResourceStrings;
using EasyBizDBTypes.Transactions.Stocks.OpeningStock;
using EasyBizDBTypes.Transactions.TransactionLogs;

namespace MsSqlDAL.Transactions.Stocks
{
    public class OpeningStockDAL : BaseOpeningStockDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        String STKIDS;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveOpeningStockRequest)RequestObj;
            var ResponseData = new SaveOpeningStockResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                if (RequestData.OpeningStockHeaderRecord == null)
                {
                    RequestData.OpeningStockHeaderRecord = RequestData.RequestDynamicData;
                }

                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateOpeningStock", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.OpeningStockHeaderRecord.ID;            

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.OpeningStockHeaderRecord.DocumentNo;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.OpeningStockHeaderRecord.Remarks;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.Date);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateTimeString(RequestData.OpeningStockHeaderRecord.DocumentDate);

                SqlParameter TotalQuantity = _CommandObj.Parameters.Add("@TotalQuantity", SqlDbType.Int);
                TotalQuantity.Direction = ParameterDirection.Input;
                TotalQuantity.Value = RequestData.OpeningStockHeaderRecord.TotalQuantity;             

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.OpeningStockHeaderRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.OpeningStockHeaderRecord.StoreCode;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.OpeningStockHeaderRecord.CountryID;

                SqlParameter CountryCode = _CommandObj.Parameters.Add("@CountryCode", SqlDbType.NVarChar);
                CountryCode.Direction = ParameterDirection.Input;
                CountryCode.Value = RequestData.OpeningStockHeaderRecord.CountryCode;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.OpeningStockHeaderRecord.CreateBy;

                SqlParameter OpeningStockDetails = _CommandObj.Parameters.Add("@OpeningStockDetails", SqlDbType.Xml);
                OpeningStockDetails.Direction = ParameterDirection.Input;
                OpeningStockDetails.Value = OpeningStockDetailMasterXML(RequestData.OpeningStockDetailsList);

                SqlParameter TransactionLogDetails = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLogDetails.Direction = ParameterDirection.Input;
                TransactionLogDetails.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.VarChar, 10);
                ID2.Direction = ParameterDirection.Output;


                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "OpeningStock");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                    STKIDS = ResponseData.IDs;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "OpeningStock");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "OpeningStock");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "OpeningStock");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
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
        public string OpeningStockDetailMasterXML(List<OpeningStockDetails> OpeningStockDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (OpeningStockDetails objOpeningStockDetails in OpeningStockDetailsList)
            {
                sSql.Append("<OpeningStockDetailsData>");
                sSql.Append("<ID>" + objOpeningStockDetails.ID + "</ID>");
                sSql.Append("<HeaderID>" + objOpeningStockDetails.HeaderID + "</HeaderID>");                
              
                sSql.Append("<SKUID>" + (objOpeningStockDetails.SKUID) + "</SKUID>");
                sSql.Append("<SKUName>" + (objOpeningStockDetails.SKUName) + "</SKUName>");
                sSql.Append("<SKUCode>" + objOpeningStockDetails.SKUCode + "</SKUCode>");
                sSql.Append("<Brand>" + (objOpeningStockDetails.Brand) + "</Brand>");
                sSql.Append("<Color>" + (objOpeningStockDetails.Color) + "</Color>");
                sSql.Append("<Size>" + objOpeningStockDetails.Size + "</Size>");
                sSql.Append("<BarCode>" + objOpeningStockDetails.BarCode + "</BarCode>");
                sSql.Append("<Quantity>" + objOpeningStockDetails.Quantity + "</Quantity>");
                sSql.Append("<FromStoreID>" + objOpeningStockDetails.FromStoreID + "</FromStoreID>");
                sSql.Append("<FromStoreCode>" + objOpeningStockDetails.FromStoreCode + "</FromStoreCode>");  
                sSql.Append("<Remarks>" + (objOpeningStockDetails.Remarks) + "</Remarks>");
                sSql.Append("<DocumentDate>" + (objOpeningStockDetails.DocumentDate) + "</DocumentDate>");
                sSql.Append("<StyleCode>" + (objOpeningStockDetails.StyleCode) + "</StyleCode>");
                sSql.Append("<CreateBy>" + (objOpeningStockDetails.CreateBy) + "</CreateBy>");
                sSql.Append("</OpeningStockDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
            //return sSql.ToString().Replace("&", "&#38;");
            //return sSql.ToString();
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
            var OpeningStockHeaderRecord = new OpeningStockHeader();
            var RequestData = (SelectByIDOpeningStockHeaderRequest)RequestObj;
            var ResponseData = new SelectByIDOpeningStockHeaderResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from OpeningStockHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOpeningStock = new OpeningStockHeader();
                        objOpeningStock.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOpeningStock.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objOpeningStock.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objOpeningStock.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOpeningStock.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objOpeningStock.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objOpeningStock.Remarks = Convert.ToString(objReader["Remarks"]);
                        objOpeningStock.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objOpeningStock.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objOpeningStock.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objOpeningStock.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objOpeningStock.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                      


                        objOpeningStock.OpeningStockDetailsList = new List<OpeningStockDetails>();

                        SelectByOpeningStockDetailsRequest objSelectByOpeningStockDetailsRequest = new SelectByOpeningStockDetailsRequest();
                        SelectByOpeningStockDetailsResponse objSelectByOpeningStockDetailsResponse = new SelectByOpeningStockDetailsResponse();
                        objSelectByOpeningStockDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByOpeningStockDetailsResponse = SelectByStockRequestDetails(objSelectByOpeningStockDetailsRequest);
                        if (objSelectByOpeningStockDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objOpeningStock.OpeningStockDetailsList = objSelectByOpeningStockDetailsResponse.OpeningStockDetailsRecord;
                        }



                        ResponseData.OpeningStockHeaderRecord = objOpeningStock;
                        ResponseData.ResponseDynamicData = objOpeningStock;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Opening Stock");
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
            var OpeningStockList = new List<OpeningStockHeader>();
            var RequestData = (SelectAllOpeningStockRequest)RequestObj;
            var ResponseData = new SelectAllOpeningStockResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    sQuery = "Select SRH.*,cm.countryname,sm.storename from StockReceiptHeader SRH left join countrymaster cm on SRH.FromCountryId=cm.id left join storemaster sm on SRH.storeid=sm.id Where SRH.Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SRH.storeid ='" + RequestData.StoreID + "' and SRH.status='Open'";
                //    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                //    _CommandObj.CommandType = CommandType.Text;
                //}
                //else
                //{
                sQuery = "Select * from OpeningStockHeader Where StoreID="+RequestData.StoreID+"Order by ID desc";
                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                //}
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOpeningStock = new OpeningStockHeader();
                        objOpeningStock.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOpeningStock.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objOpeningStock.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objOpeningStock.Remarks = Convert.ToString(objReader["Remarks"]);
                        objOpeningStock.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objOpeningStock.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objOpeningStock.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objOpeningStock.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objOpeningStock.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objOpeningStock.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;                       
                        if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        {
                            objOpeningStock.StoreName = Convert.ToString(objReader["StoreName"]);
                        }
                        OpeningStockList.Add(objOpeningStock);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.OpeningStockHeaderList = OpeningStockList;
                    ResponseData.ResponseDynamicData = OpeningStockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Opening Stock Master");
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
     
        public override SelectByOpeningStockDetailsResponse SelectByStockRequestDetails(SelectByOpeningStockDetailsRequest ObjRequest)
        {
            var OpeningStockDetailsList = new List<OpeningStockDetails>();
            var RequestData = (SelectByOpeningStockDetailsRequest)ObjRequest;
            var ResponseData = new SelectByOpeningStockDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from OpeningStockDetails ");
                sSql.Append("where  HeaderID=" + RequestData.ID + " ");
                sSql.Append("order by id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOpeningStockDetails = new OpeningStockDetails();
                        objOpeningStockDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOpeningStockDetails.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objOpeningStockDetails.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        objOpeningStockDetails.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objOpeningStockDetails.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objOpeningStockDetails.SKUName = Convert.ToString(objReader["SKUName"]);
                        objOpeningStockDetails.Brand = Convert.ToString(objReader["Brand"]);
                        objOpeningStockDetails.Color = Convert.ToString(objReader["Color"]);
                        objOpeningStockDetails.Size = Convert.ToString(objReader["Size"]);
                        objOpeningStockDetails.BarCode = Convert.ToString(objReader["BarCode"]);
                        objOpeningStockDetails.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objOpeningStockDetails.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                        
                        objOpeningStockDetails.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objOpeningStockDetails.FromStoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objOpeningStockDetails.FromStoreCode = Convert.ToString(objReader["StoreCode"]);
                        objOpeningStockDetails.Remarks = Convert.ToString(objReader["Remarks"]);
                        OpeningStockDetailsList.Add(objOpeningStockDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.OpeningStockDetailsRecord = OpeningStockDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Opening Stock");
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

        public override SelectAllOpeningStockResponse API_SelectALL(SelectAllOpeningStockRequest objRequest)
        {
            var OpeningStockList = new List<OpeningStockHeader>();
            var RequestData = (SelectAllOpeningStockRequest)objRequest;
            var ResponseData = new SelectAllOpeningStockResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //string sQuery = string.Empty;
                var sQuery = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                //{
                //    sQuery = "Select SRH.*,cm.countryname,sm.storename from StockReceiptHeader SRH left join countrymaster cm on SRH.FromCountryId=cm.id left join storemaster sm on SRH.storeid=sm.id Where SRH.Documentdate='" + sqlCommon.GetSQLServerDateString(RequestData.BusinessDate) + "' and SRH.storeid ='" + RequestData.StoreID + "' and SRH.status='Open'";
                //    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                //    _CommandObj.CommandType = CommandType.Text;
                //}
                //else
                //{

                //sQuery = "Select * from OpeningStockHeader Where StoreID=" + RequestData.StoreID + "Order by ID desc";
                sQuery.Append("Select ID, DocumentNo, DocumentDate, TotalQuantity, RC.TOTAL_CNT [RecordCount] ");
                sQuery.Append("from OpeningStockHeader ");
                sQuery.Append(" LEFT JOIN(Select  count(OS.ID) As TOTAL_CNT From OpeningStockHeader OS with(NoLock) ");
                sQuery.Append("where OS.StoreID = " + RequestData.StoreID + " ");
                sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                if (RequestData.SearchString.Contains("-"))
                {
                    sQuery.Append("or OS.DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                }
                else
                {
                    sQuery.Append("or OS.DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");
                }
                sQuery.Append("or OS.TotalQuantity like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");
                sQuery.Append("where StoreID = " + RequestData.StoreID + " ");
                sQuery.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                if(RequestData.SearchString.Contains("-"))
                {
                    sQuery.Append("or DocumentDate like isnull('%" + RequestData.SearchString + "%','') ");
                }
                else
                {
                    sQuery.Append("or DocumentNo like isnull('%" + RequestData.SearchString + "%','') ");                    
                }
                
                sQuery.Append("or TotalQuantity like isnull('%" + RequestData.SearchString + "%','')) ");
                sQuery.Append("order by ID asc ");
                sQuery.Append("offset " + RequestData.Offset + " rows " );
                sQuery.Append("fetch first " + RequestData.Limit + " rows only");

                //sQuery = "Select ID, DocumentNo, DocumentDate, TotalQuantity, RecordCount = COUNT(*) OVER() " +
                //    "from OpeningStockHeader " +
                //    "where StoreID = " + RequestData.StoreID + " " +
                //        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //            "or DocumentNo = isnull('" + RequestData.SearchString + "','') " +
                //            "or DocumentDate = isnull('" + RequestData.SearchString + "','') " +
                //            "or TotalQuantity = isnull('" + RequestData.SearchString + "','')) " +
                //    "order by ID asc " +
                //    "offset " + RequestData.Offset + " rows " +
                //    "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                //}
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objOpeningStock = new OpeningStockHeader();
                        objOpeningStock.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objOpeningStock.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objOpeningStock.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        //objOpeningStock.Remarks = Convert.ToString(objReader["Remarks"]);
                        objOpeningStock.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        //objOpeningStock.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objOpeningStock.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objOpeningStock.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objOpeningStock.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objOpeningStock.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                        //{
                        //    objOpeningStock.StoreName = Convert.ToString(objReader["StoreName"]);
                        //}
                        OpeningStockList.Add(objOpeningStock);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.OpeningStockHeaderList = OpeningStockList;
                    ResponseData.ResponseDynamicData = OpeningStockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Opening Stock Master");
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
