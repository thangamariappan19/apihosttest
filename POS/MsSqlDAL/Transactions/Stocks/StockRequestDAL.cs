using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockRequest;
using EasyBizDBTypes.Transactions.Stocks.StockReceipt;
using EasyBizDBTypes.Transactions.Stocks.StockRequest;
using EasyBizRequest.Transactions.Stocks.StockRequest;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Stocks
{
    public class StockRequestDAL : BaseStockRequestDAL
    {
        SqlConnection _ConnectionObj;
        SqlConnection cnn;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        string _CentralUnitConnectionString; Enums.RequestFrom _RequestFrom1;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStockRequestRequest)RequestObj;
            var ResponseData = new SaveStockRequestResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateStockRequest", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StockRequestHeaderRecord.ID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StockRequestHeaderRecord.FromStore;

                SqlParameter WareHouseID = _CommandObj.Parameters.Add("@WareHouseID", SqlDbType.Int);
                WareHouseID.Direction = ParameterDirection.Input;
                WareHouseID.Value = RequestData.StockRequestHeaderRecord.WareHouseID;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.StockRequestHeaderRecord.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.StockRequestHeaderRecord.DocumentDate);

                SqlParameter TotalQuantity = _CommandObj.Parameters.Add("@TotalQuantity", SqlDbType.Int);
                TotalQuantity.Direction = ParameterDirection.Input;
                TotalQuantity.Value = RequestData.StockRequestHeaderRecord.TotalQuantity;

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.NVarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.StockRequestHeaderRecord.Status;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.StockRequestHeaderRecord.Remarks;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.StockRequestHeaderRecord.StoreCode;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StockRequestHeaderRecord.CreateBy;


                SqlParameter StockRequestDetails = _CommandObj.Parameters.Add("@StockRequestDetails", SqlDbType.Xml);
                StockRequestDetails.Direction = ParameterDirection.Input;
                StockRequestDetails.Value = StockRequestDetailMasterXML(RequestData.StockRequestDetailsList);
                //StockRequestDetails.Value = StockRequestDetails.ToString().Replace("&", "&#38;");

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "StockRequest");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString(); 
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "StockRequest");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockRequest");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockRequest");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string StockRequestDetailMasterXML(List<StockRequestDetails> StockRequestDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (StockRequestDetails objStockRequestDetailMasterDetails in StockRequestDetailMasterList)
            {
                sSql.Append("<StockRequestDetailsData>");
                sSql.Append("<ID>" + objStockRequestDetailMasterDetails.StockRequestDetailID + "</ID>");
                sSql.Append("<HeaderID>" + objStockRequestDetailMasterDetails.HeaderID + "</HeaderID>");
                sSql.Append("<ApplicationDate>" + sqlCommon.GetSQLServerDateString( objStockRequestDetailMasterDetails.ApplicationDate) + "</ApplicationDate>");
                sSql.Append("<DocumentDate>" + sqlCommon.GetSQLServerDateString(objStockRequestDetailMasterDetails.DocumentDate) + "</DocumentDate>");
                sSql.Append("<StyleCode>" + objStockRequestDetailMasterDetails.StyleCode + "</StyleCode>");
                sSql.Append("<SKUID>" + (objStockRequestDetailMasterDetails.SKUID) + "</SKUID>");

                sSql.Append("<SKUName>" + objStockRequestDetailMasterDetails.SKUName + "</SKUName>");
                sSql.Append("<Brand>" + objStockRequestDetailMasterDetails.Brand + "</Brand>");
                sSql.Append("<Color>" + objStockRequestDetailMasterDetails.Color + "</Color>");
                sSql.Append("<Size>" + (objStockRequestDetailMasterDetails.Size) + "</Size>");


                sSql.Append("<SKUCode>" + objStockRequestDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<FromStoreID>" + objStockRequestDetailMasterDetails.FromStoreID + "</FromStoreID>");
                sSql.Append("<Quantity>" + objStockRequestDetailMasterDetails.Quantity + "</Quantity>");
                sSql.Append("<BarCode>" + objStockRequestDetailMasterDetails.BarCode + "</BarCode>");
                sSql.Append("<FromStoreID>" + (objStockRequestDetailMasterDetails.FromStoreID) + "</FromStoreID>");
                sSql.Append("<Remarks>" + (objStockRequestDetailMasterDetails.Remarks) + "</Remarks>");
                sSql.Append("</StockRequestDetailsData>");

            }
            //return sSql.ToString().Replace("&", "&#38;");
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
            //return sSql.ToString();
        }   

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockRequestRecord = new StockRequestHeader();
            var RequestData = (DeleteStockRequestRequest)RequestObj;
            var ResponseData = new DeleteStockRequestResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from StockRequestDetails where HeaderID={0} ; Delete from StockRequestHeader where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Stock Request");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Stock Request");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StockRequestRecord = new StockRequestHeader();
            var RequestData = (SelectByStockRequestIDRequest)RequestObj;
            var ResponseData = new SelectByStockRequestIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from StockRequestHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockRequest = new StockRequestHeader();
                        objStockRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockRequest.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;                            
                        objStockRequest.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockRequest.FromStore = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        objStockRequest.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStockRequest.WareHouseID = objReader["WareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["WareHouseID"]) : 0;
                        objStockRequest.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockRequest.Status = Convert.ToString(objReader["Status"]);
                        objStockRequest.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockRequest.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockRequest.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockRequest.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockRequest.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockRequest.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockRequest.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        objStockRequest.StockRequestDetailsList = new List<StockRequestDetails>();

                        SelectByStockRequestDetailsRequest objSelectByStockRequestDetailsRequest = new SelectByStockRequestDetailsRequest();
                        SelectByStockRequestDetailsResponse objSelectByStockRequestDetailsResponse = new SelectByStockRequestDetailsResponse();
                        objSelectByStockRequestDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectByStockRequestDetailsResponse = SelectByStockRequestDetails(objSelectByStockRequestDetailsRequest);
                        if (objSelectByStockRequestDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStockRequest.StockRequestDetailsList = objSelectByStockRequestDetailsResponse.StockRequestDetailsRecord;
                        }



                        ResponseData.StockRequestHeaderRecord = objStockRequest;
                        ResponseData.ResponseDynamicData = objStockRequest;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest");
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
            var StockRequestList = new List<StockRequestHeader>();
            var RequestData = (SelectAllStockRequestRequest)RequestObj;
            var ResponseData = new SelectAllStockRequestResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.Mode == "Edit")
                {
                    sQuery = "Select * from StockRequestHeader ";
                }
                else
                {
                    sQuery = "Select * from StockRequestHeader with(NoLock) where status = 'Open' and StoreID="+RequestData.StoreID;
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockRequest = new StockRequestHeader();
                        objStockRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockRequest.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockRequest.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockRequest.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockRequest.Status = Convert.ToString(objReader["Status"]);
                        objStockRequest.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockRequest.FromStore = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;                       
                        objStockRequest.WareHouseID = objReader["WareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["WareHouseID"]) : 0;
                        objStockRequest.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockRequest.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockRequest.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockRequest.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockRequest.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockRequest.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StockRequestList.Add(objStockRequest);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockRequestHeaderList = StockRequestList;
                    ResponseData.ResponseDynamicData = StockRequestList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest Master");
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
        public override SelectByStockRequestDetailsResponse SelectByStockRequestDetails(SelectByStockRequestDetailsRequest ObjRequest)
        {
            var StockRequestDetailMasterList = new List<StockRequestDetails>();
            var RequestData = (SelectByStockRequestDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockRequestDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select REQUEST.*, ReceivedQuantity = SUM(ISNULL(RECEIPT.ReceivedQuantity,0)),DifferenceQuantity = REQUEST.Quantity - SUM(ISNULL(RECEIPT.ReceivedQuantity,0))  ");
                sSql.Append("from StockRequestHeader SRH LEFT JOIN  StockRequestDetails REQUEST ON SRH.ID=REQUEST.HeaderID  ");
                sSql.Append("LEFT JOIN StockReceiptHeader SRR ON SRH.DocumentNo=SRR.StockRequestDocumentNo LEFT JOIN StockReceiptDetails RECEIPT ON SRR.ID=RECEIPT.HeaderID AND RECEIPT.SKUCode = REQUEST.SKUCode ");
                sSql.Append("where SRH.ID =" + RequestData.ID);

                sSql.Append(" GROUP BY REQUEST.ID, REQUEST.HeaderID, REQUEST.DocumentDate, REQUEST.SKUID, REQUEST.SKUCode, REQUEST.StyleCode, REQUEST.SKUName, REQUEST.Brand, REQUEST.Color, REQUEST.Size, REQUEST.FromStoreID, REQUEST.Quantity, REQUEST.Remarks  ");
                sSql.Append(", REQUEST.ApplicationDate, REQUEST.CreateBy, REQUEST.CreateOn, REQUEST.UpdateBy, REQUEST.UpdateOn, REQUEST.SCN, REQUEST.Active, REQUEST.BarCode, REQUEST.UnitPrice, REQUEST.LineTotal, REQUEST.CurrencyCode, REQUEST.CurrencyID  ");
                sSql.Append(", REQUEST.SellingPrice, REQUEST.FromStoreCode, REQUEST.ReceivedQty");
             
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockRequestDetailMaster = new StockRequestDetails();
                        objStockRequestDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockRequestDetailMaster.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;                            
                        objStockRequestDetailMaster.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;                           
                        objStockRequestDetailMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objStockRequestDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStockRequestDetailMaster.SKUName = Convert.ToString(objReader["SKUName"]);
                        objStockRequestDetailMaster.Brand = Convert.ToString(objReader["Brand"]);
                        objStockRequestDetailMaster.Color = Convert.ToString(objReader["Color"]);
                        objStockRequestDetailMaster.Size = Convert.ToString(objReader["Size"]);
                        objStockRequestDetailMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objStockRequestDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockRequestDetailMaster.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;                      
                        objStockRequestDetailMaster.ApplicationDate = Convert.ToDateTime(objReader["ApplicationDate"]);
                        objStockRequestDetailMaster.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockRequestDetailMaster.FromStoreID = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;                           
                        objStockRequestDetailMaster.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockRequestDetailMaster.OldReceivedQuantity = objReader["ReceivedQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["ReceivedQuantity"]) : 0;
                        objStockRequestDetailMaster.DifferenceQuantity = objReader["DifferenceQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DifferenceQuantity"]) : 0; 
                        StockRequestDetailMasterList.Add(objStockRequestDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockRequestDetailsRecord = StockRequestDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest");
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


        public override SelectByStockRequestDetailsResponse SelectByStockRequestHeaderID(SelectByStockRequestDetailsRequest ObjRequest)
        {
            var StockRequestRecord = new StockRequestHeader();
            var RequestData = (SelectByStockRequestDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockRequestDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from StockRequestHeader with(NoLock) where documentno='{0}' ";
                sSql = string.Format(sSql, RequestData.StockRequestDocumentNo);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockRequest = new StockRequestHeader();
                        objStockRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStockRequest.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockRequest.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockRequest.FromStore = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        objStockRequest.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objStockRequest.WareHouseID = objReader["WareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["WareHouseID"]) : 0;
                        objStockRequest.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockRequest.Status = Convert.ToString(objReader["Status"]);
                        objStockRequest.Remarks = Convert.ToString(objReader["Remarks"]);
                        objStockRequest.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStockRequest.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStockRequest.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStockRequest.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStockRequest.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockRequest.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        
                        ResponseData.StockRequestHeaderRecord = objStockRequest;
                        ResponseData.ResponseDynamicData = objStockRequest;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest");
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

        public override SaveStockRequestResponse Saveint_stock(SaveStockRequestRequest ObjRequest)
        {
            var RequestData = (SaveStockRequestRequest)ObjRequest;
            var ResponseData = new SaveStockRequestResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon1 = new MsSqlCommon();
            var sqlCommon = new MsSqlCommon();
            string connetionString;
           
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _RequestFrom = RequestData.RequestFrom;

                    _CommandObj = new SqlCommand("Insertint_stock", cnn);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.CommandTimeout = 300000;

                    SqlParameter int_stock = _CommandObj.Parameters.Add("@int_stock", SqlDbType.Xml);
                    int_stock.Direction = ParameterDirection.Input;
                    int_stock.Value = int_stockXML(RequestData.int_stockrequestTypesList);

                    SqlParameter TLIDs = _CommandObj.Parameters.Add("@TLIDs", SqlDbType.VarChar, 10);
                    TLIDs.Direction = ParameterDirection.Output;

                    SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                    StatusCode.Direction = ParameterDirection.Output;

                    SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                    StatusMsg.Direction = ParameterDirection.Output;

                    _CommandObj.CommandType = CommandType.StoredProcedure;
                    _CommandObj.ExecuteNonQuery();

                    string strStatusCode = StatusCode.Value.ToString();
                    if (strStatusCode == "1")
                    {
                        ResponseData.DisplayMessage = "Records saved into CentralUnit database !.";
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.IDs = TLIDs.Value.ToString();
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                        ResponseData.DisplayMessage = "Records are not saved into CentralUnit.Please save the records manually !.";
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit databse not connected .Please save the records manually !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }     
            return ResponseData;
        }
        public string int_stockXML(List<int_stockrequestTypes> int_stockrequestTypesList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (int_stockrequestTypes objint_stockrequestTypes in int_stockrequestTypesList)
            {
                sSql.Append("<int_stockrequestData>");
                sSql.Append("<ID>" + objint_stockrequestTypes.ID + "</ID>");
                sSql.Append("<DocNum>" + objint_stockrequestTypes.DocNum + "</DocNum>");
                sSql.Append("<DocDate>" + sqlCommon.GetSQLServerDateString(objint_stockrequestTypes.DocDate) + "</DocDate>");
                sSql.Append("<DelDate>" + sqlCommon.GetSQLServerDateString(objint_stockrequestTypes.DelDate) + "</DelDate>");                
                sSql.Append("<LineId>" + objint_stockrequestTypes.LineId + "</LineId>");
                sSql.Append("<FromLocation>" + objint_stockrequestTypes.FromLocation + "</FromLocation>");
                sSql.Append("<ToLocation>" + objint_stockrequestTypes.ToLocation + "</ToLocation>");
                sSql.Append("<Priority>" + objint_stockrequestTypes.Priority + "</Priority>");
                sSql.Append("<ItemCode>" + (objint_stockrequestTypes.ItemCode) + "</ItemCode>");
                sSql.Append("<ItemName>" + (objint_stockrequestTypes.ItemName) + "</ItemName>");
                sSql.Append("<BarCode>" + (objint_stockrequestTypes.BarCode) + "</BarCode>");
                sSql.Append("<Quantity>" + (objint_stockrequestTypes.Quantity) + "</Quantity>");
                sSql.Append("<Remarks>" + (objint_stockrequestTypes.Remarks) + "</Remarks>");
                sSql.Append("<Flag>" + (objint_stockrequestTypes.Flag) + "</Flag>");

                sSql.Append("</int_stockrequestData>");

            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override SelectAllStockRequestResponse SelectAllInt_ConfirmTransfer(SelectAllStockRequestRequest ObjRequest)
        {
            var StockRequestList = new List<StockRequestHeader>();
            var RequestData = (SelectAllStockRequestRequest)ObjRequest;
            var ResponseData = new SelectAllStockRequestResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();


                    _RequestFrom = RequestData.RequestFrom;

                    string sSql = "Select distinct docnum from Int_ConfirmTransfer with(NoLock) where ToLocation='{0}' and isnull(flag,0)=0 ";
                    sSql = string.Format(sSql, RequestData.StoreCode);

                    _CommandObj = new SqlCommand(sSql, cnn);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objStockRequest = new StockRequestHeader();
                            objStockRequest.ID = 0;

                            objStockRequest.DocumentNo = Convert.ToString(objReader["docnum"]);

                            StockRequestList.Add(objStockRequest);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.StockRequestHeaderList = StockRequestList;
                        ResponseData.ResponseDynamicData = StockRequestList;                        
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest Master");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit databse not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }            
            return ResponseData;
        }

        public override SelectByStockRequestDetailsResponse Selectint_stockreceiptDetails(SelectByStockRequestDetailsRequest ObjRequest)
        {
            var int_stockreceiptList = new List<int_stockreceipt>();
            var RequestData = (SelectByStockRequestDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockRequestDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _RequestFrom = RequestData.RequestFrom;

                    string sSql = "Select * from int_stockreceipt where BasDocNum='{0}' and ToLocation='{1}' and isnull(flag,0)=0 ";
                    sSql = string.Format(sSql, RequestData.StockRequestDocumentNo, RequestData.StoreCode);

                    _CommandObj = new SqlCommand(sSql, cnn);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objInt_ConfirmTransfer = new int_stockreceipt();
                            objInt_ConfirmTransfer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objInt_ConfirmTransfer.DocNum = Convert.ToString(objReader["DocNum"]);
                            objInt_ConfirmTransfer.DocDate = objReader["DocDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocDate"]) : DateTime.Now;
                            objInt_ConfirmTransfer.DelDate = objReader["DelDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DelDate"]) : DateTime.Now;
                            objInt_ConfirmTransfer.LineId = objReader["LineId"] != DBNull.Value ? Convert.ToInt32(objReader["LineId"]) : 0;
                            objInt_ConfirmTransfer.FromLocation = Convert.ToString(objReader["FromLocation"]);
                            objInt_ConfirmTransfer.ToLocation = Convert.ToString(objReader["ToLocation"]);
                            objInt_ConfirmTransfer.StoreCode = Convert.ToString(objReader["ToLocation"]);
                            objInt_ConfirmTransfer.SKUCode = Convert.ToString(objReader["ItemCode"]);
                            objInt_ConfirmTransfer.SKUName = Convert.ToString(objReader["ItemName"]);
                            objInt_ConfirmTransfer.BarCode = Convert.ToString(objReader["BarCode"]);
                            objInt_ConfirmTransfer.RequestQuantity = objReader["ReqQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReqQty"]) : 0;
                            objInt_ConfirmTransfer.TransferQuantity = objReader["TfrdQty"] != DBNull.Value ? Convert.ToInt32(objReader["TfrdQty"]) : 0;
                            objInt_ConfirmTransfer.Remarks = Convert.ToString(objReader["Rermarks"]);
                            objInt_ConfirmTransfer.Basedocument = Convert.ToString(objReader["Basedocument"]);
                            objInt_ConfirmTransfer.BaseDocKey = Convert.ToString(objReader["BaseDocKey"]);
                            objInt_ConfirmTransfer.BasDocNum = Convert.ToString(objReader["BasDocNum"]);
                            objInt_ConfirmTransfer.WMSReqKey = objReader["WMSReqKey"] != DBNull.Value ? Convert.ToInt32(objReader["WMSReqKey"]) : 0;
                            objInt_ConfirmTransfer.Flag = objReader["Flag"] != DBNull.Value ? Convert.ToBoolean(objReader["Flag"]) : false;
                            objInt_ConfirmTransfer.DocType = Convert.ToString(objReader["DocType"]);

                            int_stockreceiptList.Add(objInt_ConfirmTransfer);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.int_stockreceiptRecord = int_stockreceiptList;
                        ResponseData.ResponseDynamicData = int_stockreceiptList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "stockreceipt");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit databse not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }           
            return ResponseData;
        }

        public override SelectByStockRequestDetailsResponse SelectWithOutint_stockreceipt(SelectByStockRequestDetailsRequest ObjRequest)
        {
            var int_stockreceiptList = new List<int_stockreceipt>();
            var RequestData = (SelectByStockRequestDetailsRequest)ObjRequest;
            var ResponseData = new SelectByStockRequestDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sqlCommon1 = new MsSqlCommon();
            string connetionString;
            try
            {
                connetionString = ConfigurationManager.AppSettings["CentralUnitConnection"];
                if (IsPinging(connetionString))
                {
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();


                    _RequestFrom = RequestData.RequestFrom;

                    string sSql = "Select * from int_stockreceipt with(NoLock) where ToLocation='{0}' and Basedocument='N' and isnull(flag,0)=0 ";
                    sSql = string.Format(sSql, RequestData.StoreCode);

                    _CommandObj = new SqlCommand(sSql, cnn);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objStockRequest = new int_stockreceipt();
                            objStockRequest.ID = 0;

                            objStockRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objStockRequest.DocNum = Convert.ToString(objReader["DocNum"]);
                            objStockRequest.DocDate = objReader["DocDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocDate"]) : DateTime.Now;
                            objStockRequest.DelDate = objReader["DelDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DelDate"]) : DateTime.Now;
                            objStockRequest.LineId = objReader["LineId"] != DBNull.Value ? Convert.ToInt32(objReader["LineId"]) : 0;
                            objStockRequest.FromLocation = Convert.ToString(objReader["ToLocation"]);
                            objStockRequest.ToLocation = Convert.ToString(objReader["FromLocation"]);
                            objStockRequest.SKUCode = Convert.ToString(objReader["ItemCode"]);
                            objStockRequest.SKUName = Convert.ToString(objReader["ItemName"]);
                            objStockRequest.BarCode = Convert.ToString(objReader["BarCode"]);
                            objStockRequest.RequestQuantity = objReader["ReqQty"] != DBNull.Value ? Convert.ToInt32(objReader["ReqQty"]) : 0;
                            objStockRequest.TransferQuantity = objReader["TfrdQty"] != DBNull.Value ? Convert.ToInt32(objReader["TfrdQty"]) : 0;
                            objStockRequest.Remarks = Convert.ToString(objReader["Rermarks"]);
                            objStockRequest.Basedocument = Convert.ToString(objReader["Basedocument"]);
                            objStockRequest.BaseDocKey = Convert.ToString(objReader["BaseDocKey"]);
                            objStockRequest.BasDocNum = Convert.ToString(objReader["BasDocNum"]);
                            objStockRequest.WMSReqKey = objReader["WMSReqKey"] != DBNull.Value ? Convert.ToInt32(objReader["WMSReqKey"]) : 0;
                            objStockRequest.Flag = objReader["Flag"] != DBNull.Value ? Convert.ToBoolean(objReader["Flag"]) : false;
                            objStockRequest.DocType = Convert.ToString(objReader["DocType"]);

                            int_stockreceiptList.Add(objStockRequest);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.int_stockreceiptRecord = int_stockreceiptList;
                        ResponseData.ResponseDynamicData = int_stockreceiptList;                        
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest Master");
                    }
                    cnn.Close();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.ServerNotResponding;
                    ResponseData.DisplayMessage = "CentralUnit databse not connected !.";
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }            
            return ResponseData;
        }
        public bool IsPinging(string SqlConString)
        {
            return true;
            //bool objBool = false;
            //try
            //{
            //    SqlConnectionStringBuilder SQLConBuilder = new SqlConnectionStringBuilder(SqlConString);
            //    var DataSource = SQLConBuilder.DataSource.Split('\\');
            //    string StoreIP = string.Empty;
            //    if (DataSource.Length > 0)
            //    {
            //        StoreIP = Convert.ToString(DataSource[0]);
            //    }
            //    if (StoreIP != null && StoreIP != string.Empty)
            //    {
            //        Ping myPing = new Ping();
            //        PingReply reply = myPing.Send(StoreIP, 60);
            //        if (reply != null)
            //        {
            //            if (reply.Status == IPStatus.Success)
            //            {
            //                objBool = true;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    objBool = false;
            //}
            //return objBool;
        }

        public override SelectAllStockRequestResponse API_SelectALL(SelectAllStockRequestRequest objRequest)
        {
            var StockRequestList = new List<StockRequestHeader>();
            var RequestData = (SelectAllStockRequestRequest)objRequest;
            var ResponseData = new SelectAllStockRequestResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //if (RequestData.Mode == "Edit")
                //{
                //    sQuery = "Select * from StockRequestHeader ";
                //}
                //else
                //{
                //    sQuery = "Select * from StockRequestHeader with(NoLock) where status = 'Open' and StoreID=" + RequestData.StoreID;
                //}

                if (RequestData.Mode == "Edit")
                {
                    sQuery = "Select ID, DocumentNo, DocumentDate, Status, Active, RecordCount = COUNT(*) OVER() " +
                    "from StockRequestHeader " +
                    "where Active = " + RequestData.IsActive + " " +
                    "and (isnull('" + RequestData.SearchString + "','') = '' " +
                            "or DocumentNo = isnull('" + RequestData.SearchString + "','') " +
                            "or DocumentDate = isnull('" + RequestData.SearchString + "','') " +
                            "or Status = isnull('" + RequestData.SearchString + "','')) " +                    
                    "order by ID asc " +
                    "offset " + RequestData.Offset + " rows " +
                    "fetch first " + RequestData.Limit + " rows only";
                }
                else
                {
                    sQuery = "Select ID, DocumentNo, DocumentDate, Status, Active, RecordCount = COUNT(*) OVER() " +
                    "from StockRequestHeader " +
                    "where Active = " + RequestData.IsActive + " " +
                    "and StoreID = " + RequestData.StoreID + " " +                    
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                        "or DocumentNo = isnull('" + RequestData.SearchString + "','') " +
                        "or DocumentDate = isnull('" + RequestData.SearchString + "','') " +
                        "or Status = isnull('" + RequestData.SearchString + "','')) " +
                    "and Status = 'Open' " +
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
                        var objStockRequest = new StockRequestHeader();
                        objStockRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objStockRequest.TotalQuantity = objReader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["TotalQuantity"]) : 0;
                        objStockRequest.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objStockRequest.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objStockRequest.Status = Convert.ToString(objReader["Status"]);
                        //objStockRequest.Remarks = Convert.ToString(objReader["Remarks"]);
                        //objStockRequest.FromStore = objReader["FromStoreID"] != DBNull.Value ? Convert.ToInt32(objReader["FromStoreID"]) : 0;
                        //objStockRequest.WareHouseID = objReader["WareHouseID"] != DBNull.Value ? Convert.ToInt32(objReader["WareHouseID"]) : 0;
                        //objStockRequest.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockRequest.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockRequest.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockRequest.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objStockRequest.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStockRequest.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        StockRequestList.Add(objStockRequest);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.StockRequestHeaderList = StockRequestList;
                    ResponseData.ResponseDynamicData = StockRequestList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StockRequest Master");
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
