using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Stocks.InventoryCounting;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest.Transactions.Stocks.InventoryCounting;
using EasyBizResponse.Transactions.Stocks.InventoryCounting;
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
    public class InventoryCountingDAL : BaseInventoryCountingDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveInventoryCountingRequest)RequestObj;
            var ResponseData = new SaveInventoryCountingResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertInventoryCounting", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.InventoryCountingHeaderRecord.ID;

                SqlParameter DocumentNumber = _CommandObj.Parameters.Add("@DocumentNumber", SqlDbType.NVarChar);
                DocumentNumber.Direction = ParameterDirection.Input;
                DocumentNumber.Value = RequestData.InventoryCountingHeaderRecord.DocumentNumber;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.InventoryCountingHeaderRecord.DocumentDate);

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.InventoryCountingHeaderRecord.StoreID;

                SqlParameter StoreCode = _CommandObj.Parameters.Add("@StoreCode", SqlDbType.NVarChar);
                StoreCode.Direction = ParameterDirection.Input;
                StoreCode.Value = RequestData.InventoryCountingHeaderRecord.StoreCode; 

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.InventoryCountingHeaderRecord.CreateBy;

                SqlParameter InventoryCountingDetails = _CommandObj.Parameters.Add("@InventoryCountingDetails", SqlDbType.Xml);
                InventoryCountingDetails.Direction = ParameterDirection.Input;
                InventoryCountingDetails.Value = InventoryCountingDetailMasterXML(RequestData.InventoryCountingDetailsList);

                SqlParameter ReturnID = _CommandObj.Parameters.Add("@ReturnID", SqlDbType.VarChar, 10);
                ReturnID.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "InventoryCounting");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ReturnID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "InventoryCounting");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "InventoryCounting");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "InventoryCounting");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string InventoryCountingDetailMasterXML(List<InventoryCountingDetails> InventoryCountingDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (InventoryCountingDetails objInventoryCountingDetailMasterDetails in InventoryCountingDetailMasterList)
            {
                sSql.Append("<InventoryCountingDetailsData>");
                sSql.Append("<ID>" + objInventoryCountingDetailMasterDetails.ID + "</ID>");
                sSql.Append("<InventoryCountingID>" + objInventoryCountingDetailMasterDetails.InventoryCountingID + "</InventoryCountingID>");
                sSql.Append("<SKUCode>" + objInventoryCountingDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<BarCode>" + objInventoryCountingDetailMasterDetails.BarCode + "</BarCode>");
                sSql.Append("<StyleCode>" + objInventoryCountingDetailMasterDetails.StyleCode + "</StyleCode>");
                sSql.Append("<BrandCode>" + objInventoryCountingDetailMasterDetails.BrandCode + "</BrandCode>");
                sSql.Append("<ColorCode>" + objInventoryCountingDetailMasterDetails.ColorCode + "</ColorCode>");
                sSql.Append("<SizeCode>" + (objInventoryCountingDetailMasterDetails.SizeCode) + "</SizeCode>");
                sSql.Append("<StoreID>" + objInventoryCountingDetailMasterDetails.StoreID + "</StoreID>");
                sSql.Append("<StoreName>" + objInventoryCountingDetailMasterDetails.StoreName + "</StoreName>");
                sSql.Append("<SystemQuantity>" + objInventoryCountingDetailMasterDetails.SystemQuantity + "</SystemQuantity>");
                sSql.Append("<PhysicalQuantity>" + objInventoryCountingDetailMasterDetails.PhysicalQuantity + "</PhysicalQuantity>");
                sSql.Append("<DifferenceQuantity>" + objInventoryCountingDetailMasterDetails.DifferenceQuantity + "</DifferenceQuantity>");       
                sSql.Append("</InventoryCountingDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); 
            //return sSql.ToString();
        }  
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveInventoryCountingRequest)RequestObj;
            var ResponseData = new SaveInventoryCountingResponse();
            
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Update InventoryCountingHeader Set PostingDone='True',PostingDate=SYSDATETIME() where ID={0}";

                sSql = string.Format(sSql, RequestData.InventoryCountingHeaderRecord.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;  

                _CommandObj.ExecuteNonQuery();

                int strStatusCode = _CommandObj.ExecuteNonQuery();
                if (strStatusCode == 1)
                {
                    ResponseData.DisplayMessage = "Inventory Counting posted sucessfully !.";
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                    
                }                
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "InventoryCounting");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "InventoryCounting");
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
            var InventoryCountingRecord = new InventoryCountingHeader();
            var RequestData = (DeleteInventoryCountingRequest)RequestObj;
            var ResponseData = new DeleteInventoryCountingResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from  InventoryCountingDetails where HeaderID={0} ; Delete from InventoryCountingHeader where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Inventory Counting");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Inventory Counting");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var InventoryCountingRecord = new InventoryCountingHeader();
            var RequestData = (SelectByInventoryCountingIDRequest)RequestObj;
            var ResponseData = new SelectByInventoryCountingIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from InventoryCountingHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryCounting = new InventoryCountingHeader();
                        objInventoryCounting.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;                        
                        objInventoryCounting.DocumentNumber = Convert.ToString(objReader["DocumentNumber"]);
                        objInventoryCounting.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInventoryCounting.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryCounting.StoreCode = Convert.ToString(objReader["StoreCode"]);
                        objInventoryCounting.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInventoryCounting.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInventoryCounting.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInventoryCounting.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInventoryCounting.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInventoryCounting.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objInventoryCounting.PostingDone = objReader["PostingDone"] != DBNull.Value ? Convert.ToBoolean(objReader["PostingDone"]) : false;
                        objInventoryCounting.PostingDate = objReader["PostingDate"] != DBNull.Value ? (Nullable<DateTime>)(objReader["PostingDate"]) : null; 

                        objInventoryCounting.InventoryCountingDetailList = new List<InventoryCountingDetails>();
                        SelectByInventoryCountingDetailsResponse objSelectByInventoryCountingDetailsResponse = new SelectByInventoryCountingDetailsResponse();
                        SelectByInventoryCountingDetailsRequest objSelectByInventoryCountingDetailsRequest = new SelectByInventoryCountingDetailsRequest();

                        objSelectByInventoryCountingDetailsRequest.ID = objInventoryCounting.ID;

                        objSelectByInventoryCountingDetailsResponse = SelectByInventoryCountingDetails(objSelectByInventoryCountingDetailsRequest);
                        
                        if(objSelectByInventoryCountingDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objInventoryCounting.InventoryCountingDetailList = objSelectByInventoryCountingDetailsResponse.InventoryCountingDetailsRecord;
                        }
                        ResponseData.InventoryCountingHeaderRecord = objInventoryCounting;
                        ResponseData.ResponseDynamicData = objInventoryCounting;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting");
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
            var InventoryCountingList = new List<InventoryCountingHeader>();
            var RequestData = (SelectAllInventoryCountingRequest)RequestObj;
            var ResponseData = new SelectAllInventoryCountingResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select * from InventoryCountingHeader ";
               

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryCounting = new InventoryCountingHeader();
                        objInventoryCounting.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryCounting.DocumentNumber = Convert.ToString(objReader["DocumentNumber"]);       
                        objInventoryCounting.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInventoryCounting.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0; 
                        objInventoryCounting.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objInventoryCounting.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objInventoryCounting.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objInventoryCounting.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objInventoryCounting.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objInventoryCounting.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objInventoryCounting.PostingDone = objReader["PostingDone"] != DBNull.Value ? Convert.ToBoolean(objReader["PostingDone"]) : false;
                        objInventoryCounting.PostingDate = objReader["PostingDate"] != DBNull.Value ? (Nullable<DateTime>)(objReader["PostingDate"]) : null; 
                        InventoryCountingList.Add(objInventoryCounting);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventoryCountingHeaderList = InventoryCountingList;
                    ResponseData.ResponseDynamicData = InventoryCountingList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting Master");
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
        public override SelectByInventoryCountingDetailsResponse SelectByInventoryCountingDetails(SelectByInventoryCountingDetailsRequest ObjRequest)
        {
            var InventoryCountingDetailMasterList = new List<InventoryCountingDetails>();
            var RequestData = (SelectByInventoryCountingDetailsRequest)ObjRequest;
            var ResponseData = new SelectByInventoryCountingDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from InventoryCountingDetails ");
                sSql.Append("where  InventoryCountingID=" + RequestData.ID + " ");
                sSql.Append("order by id  asc");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryCountingDetailMaster = new InventoryCountingDetails();
                        objInventoryCountingDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryCountingDetailMaster.InventoryCountingID = objReader["InventoryCountingID"] != DBNull.Value ? Convert.ToInt32(objReader["InventoryCountingID"]) : 0;                        
                                             
                        objInventoryCountingDetailMaster.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objInventoryCountingDetailMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objInventoryCountingDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objInventoryCountingDetailMaster.BrandCode = Convert.ToString(objReader["BrandCode"]);
                        objInventoryCountingDetailMaster.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objInventoryCountingDetailMaster.SizeCode = Convert.ToString(objReader["SizeCode"]);
                        objInventoryCountingDetailMaster.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryCountingDetailMaster.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : string.Empty; 
                        objInventoryCountingDetailMaster.PhysicalQuantity = objReader["PhysicalQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["PhysicalQuantity"]) : 0;                      
                        objInventoryCountingDetailMaster.SystemQuantity = objReader["SystemQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["SystemQuantity"]) : 0;
                        objInventoryCountingDetailMaster.DifferenceQuantity = objReader["DifferenceQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["DifferenceQuantity"]) : 0;
                                       
                        
                        InventoryCountingDetailMasterList.Add(objInventoryCountingDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventoryCountingDetailsRecord = InventoryCountingDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting");
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

        public override GetSystemStockByStoreResponse GetSystemStockByStore(GetSystemStockByStoreRequest ObjRequest)
        {
            var InventorySysCountList = new List<InventorySysCount>();
            var RequestData = (GetSystemStockByStoreRequest)ObjRequest;
            var ResponseData = new GetSystemStockByStoreResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("GetStockByStore", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventorySysCount = new InventorySysCount();                        
                        objInventorySysCount.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objInventorySysCount.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : string.Empty;
                        objInventorySysCount.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objInventorySysCount.ID = 0;
                        objInventorySysCount.InventoryInitID = 0;
                        objInventorySysCount.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : string.Empty;
                        objInventorySysCount.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objInventorySysCount.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                        objInventorySysCount.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objInventorySysCount.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
                        objInventorySysCount.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objInventorySysCount.StyleName = objReader["StyleName"] != DBNull.Value ? Convert.ToString(objReader["StyleName"]) : string.Empty;
                        objInventorySysCount.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objInventorySysCount.SalesPrice = objReader["SalesPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SalesPrice"]) : 0;
                        InventorySysCountList.Add(objInventorySysCount);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventorySysCountList = InventorySysCountList;
                    ResponseData.ResponseDynamicData = InventorySysCountList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "System Stock");
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

        public override SaveSystemStockResponse SaveSystemStock(SaveSystemStockRequest ObjRequest)
        {
            var RequestData = (SaveSystemStockRequest)ObjRequest;
            var ResponseData = new SaveSystemStockResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertInventorySysCount1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;              

                SqlParameter DocumentNumber = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.NVarChar);
                DocumentNumber.Direction = ParameterDirection.Input;
                DocumentNumber.Value = RequestData.InventoryManualCountRecord.DocumentNo;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.InventoryManualCountRecord.StoreID;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.VarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.InventoryManualCountRecord.Remarks;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@CreatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.InventoryManualCountRecord.CreateBy;

                //SqlParameter SysCountList = _CommandObj.Parameters.Add("@SysCountList", SqlDbType.Xml);
                //SysCountList.Direction = ParameterDirection.Input;
                //SysCountList.Value = InventorySysCountXml(RequestData.InventoryManualCountRecord.InventorySysCountList);

                SqlParameter RunningNo = _CommandObj.Parameters.Add("@RunningNo", SqlDbType.Int);
                RunningNo.Direction = ParameterDirection.Input;
                RunningNo.Value = RequestData.RunningNo;

                SqlParameter DocumentNumberingID = _CommandObj.Parameters.Add("@DocumentNumberingID", SqlDbType.Int);
                DocumentNumberingID.Direction = ParameterDirection.Input;
                DocumentNumberingID.Value = RequestData.DocumentNumberingID;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Inventory initialize and freeze");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Inventory initialize and freeze");
                    ResponseData.StatusCode = Enums.OpStatusCode.DuplicateRecordFound;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory initialize and freeze");
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory initialize and freeze");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override GetInventoryCountingInitResponse GetInventoryCountingInitList(GetInventoryCountingInitRequest RequestObj)
        {
            var InventoryInitList = new List<InventoryInit>();            
            var RequestData = (GetInventoryCountingInitRequest)RequestObj;
            var ResponseData = new GetInventoryCountingInitResponse();
            SqlDataReader objReader;
            //StringBuilder sbSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select * from InventoryInit ";
               

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryInit = new InventoryInit();
                        objInventoryInit.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryInit.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryInit.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objInventoryInit.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInventoryInit.Remarks = Convert.ToString(objReader["Remarks"]);
                        objInventoryInit.PostingDone = objReader["PostingDone"] != DBNull.Value ? Convert.ToBoolean(objReader["PostingDone"]) : false;
                        objInventoryInit.Status = Convert.ToString(objReader["Status"]);
                        objInventoryInit.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objInventoryInit.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;

                        if (objReader["PostingDate"] != DBNull.Value)
                        {
                            objInventoryInit.PostingDate = Convert.ToDateTime(objReader["PostingDate"]);
                        }

                        objInventoryInit.ApprovedBy = objReader["ApprovedBy"] != DBNull.Value ? Convert.ToInt32(objReader["ApprovedBy"]) : 0;

                        InventoryInitList.Add(objInventoryInit);
                       
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventoryInitList = InventoryInitList;
                    ResponseData.ResponseDynamicData = InventoryInitList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting Master");
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

        public string InventorySysCountXml(List<InventorySysCount> InventorySysCountList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (InventorySysCount objInventoryCountingDetailMasterDetails in InventorySysCountList)
            {
                sSql.Append("<InventorySysCount>");
                sSql.Append("<SKUCode>" + objInventoryCountingDetailMasterDetails.SKUCode + "</SKUCode>");
                sSql.Append("<SKUName>" + objInventoryCountingDetailMasterDetails.SKUName + "</SKUName>");
                sSql.Append("<BarCode>" + objInventoryCountingDetailMasterDetails.BarCode + "</BarCode>");
                sSql.Append("<SupplierBarCode>" + objInventoryCountingDetailMasterDetails.SupplierBarCode + "</SupplierBarCode>");
                sSql.Append("<StyleCode>" + objInventoryCountingDetailMasterDetails.StyleCode + "</StyleCode>");
                sSql.Append("<StyleName>" + objInventoryCountingDetailMasterDetails.StyleName + "</StyleName>");
                sSql.Append("<BrandCode>" + objInventoryCountingDetailMasterDetails.BrandCode + "</BrandCode>");
                sSql.Append("<ColorCode>" + objInventoryCountingDetailMasterDetails.ColorCode + "</ColorCode>");
                sSql.Append("<SizeCode>" + (objInventoryCountingDetailMasterDetails.SizeCode) + "</SizeCode>");
                sSql.Append("<StockQty>" + objInventoryCountingDetailMasterDetails.StockQty + "</StockQty>");
                sSql.Append("<RRPPrice>" + objInventoryCountingDetailMasterDetails.RRPPrice + "</RRPPrice>");
                sSql.Append("<SalesPrice>" + objInventoryCountingDetailMasterDetails.SalesPrice + "</SalesPrice>");
                sSql.Append("</InventorySysCount>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");           
        }

        public override GetInventoryCountingInitRecordResponse GetInventoryCountingInitRecord(GetInventoryCountingInitRecordRequest ObjRequest)
        {
           
            var RequestData = (GetInventoryCountingInitRecordRequest)ObjRequest;
            var ResponseData = new GetInventoryCountingInitRecordResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select * from InventoryInit ";

                if(RequestData.SelectionMode == "Date")
                {
                    sQuery = sQuery + " where DocumentDate='" + sqlCommon.GetSQLServerDateString(RequestData.DocumentDate) + "'";
                }
                else if (RequestData.SelectionMode == "DocumentNo")
                {
                    sQuery = sQuery + " where DocumentNo='" + RequestData.DocumentNo + "'";
                }
                else
                {
                    sQuery = sQuery + " where DocumentNo='" + RequestData.DocumentNo + "' and [Status]='Stock Uploaded'";
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryInit = new InventoryInit();
                        objInventoryInit.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryInit.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryInit.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objInventoryInit.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInventoryInit.Remarks = Convert.ToString(objReader["Remarks"]);
                        objInventoryInit.PostingDone = objReader["PostingDone"] != DBNull.Value ? Convert.ToBoolean(objReader["PostingDone"]) : false;
                        objInventoryInit.Status = Convert.ToString(objReader["Status"]);
                        objInventoryInit.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objInventoryInit.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;

                        if (objReader["PostingDate"] != DBNull.Value)
                        {
                            objInventoryInit.PostingDate = Convert.ToDateTime(objReader["PostingDate"]);
                        }
                        objInventoryInit.ApprovedBy = objReader["ApprovedBy"] != DBNull.Value ? Convert.ToInt32(objReader["ApprovedBy"]) : 0;

                        objInventoryInit.InventorySysCountList = new List<InventorySysCount>();
                        objInventoryInit.InventorySysCountList = GetInventorySysCountList(objInventoryInit.ID, RequestData);

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.InventoryInitRecord = objInventoryInit;
                        ResponseData.ResponseDynamicData = objInventoryInit;
                    }                    
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting Master");
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
        public List<InventorySysCount> GetInventorySysCountList(int InventoryInitID, GetInventoryCountingInitRecordRequest RequestData)
        {
            
            var InventorySysCountList = new List<InventorySysCount>();
            try
            {
                SqlDataReader objReader;
                var sqlCommon = new MsSqlCommon();
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.GroupByMode != null && RequestData.GroupByMode == "StyleCode")
                {
                    sQuery = "select StyleCode, StyleName, sum(StockQty) as StockQty,RRPPrice,SalesPrice from InventorySysCount ";
                    sQuery = sQuery + "where InventoryInitID = "+ InventoryInitID + " group by StyleCode,StyleName,RRPPrice,SalesPrice";
                }
                else
                {
                    sQuery = "Select * from InventorySysCount where InventoryInitID=" + InventoryInitID;
                }

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventorySysCount = new InventorySysCount();
                        if (RequestData.GroupByMode == null || RequestData.GroupByMode == "SKUCode" || RequestData.GroupByMode == string.Empty)
                        {
                            objInventorySysCount.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objInventorySysCount.InventoryInitID = objReader["InventoryInitID"] != DBNull.Value ? Convert.ToInt32(objReader["InventoryInitID"]) : 0;
                            objInventorySysCount.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                            objInventorySysCount.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                            objInventorySysCount.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                            objInventorySysCount.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;

                            objInventorySysCount.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : string.Empty;
                            objInventorySysCount.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                            objInventorySysCount.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : string.Empty;
                        }
                        objInventorySysCount.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objInventorySysCount.StyleName = objReader["StyleName"] != DBNull.Value ? Convert.ToString(objReader["StyleName"]) : string.Empty;                        
                        objInventorySysCount.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objInventorySysCount.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objInventorySysCount.SalesPrice = objReader["SalesPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SalesPrice"]) : 0;
                        InventorySysCountList.Add(objInventorySysCount);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return InventorySysCountList;
        }

        public override SaveManualStockResponse SaveManualStock(SaveManualStockRequest ObjRequest)
        {
            var RequestData = (SaveManualStockRequest)ObjRequest;
            var ResponseData = new SaveManualStockResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertInventoryManualCount", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.InventoryManualCountRecord.StoreID;

                SqlParameter InventoryInitID = _CommandObj.Parameters.Add("@InventoryInitID", SqlDbType.Int);
                InventoryInitID.Direction = ParameterDirection.Input;
                InventoryInitID.Value = RequestData.InventoryManualCountRecord.InventoryInitID;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.InventoryManualCountRecord.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.DateTime);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.InventoryManualCountRecord.DocumentDate);
               
                SqlParameter CountingType = _CommandObj.Parameters.Add("@CountingType", SqlDbType.NVarChar);
                CountingType.Direction = ParameterDirection.Input;
                CountingType.Value = RequestData.InventoryManualCountRecord.CountingType;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@CreatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.InventoryManualCountRecord.CreateBy;

                SqlParameter ManualCountList = _CommandObj.Parameters.Add("@ManualCountList", SqlDbType.Xml);
                ManualCountList.Direction = ParameterDirection.Input;
                ManualCountList.Value = InventoryManualCountXml(RequestData.InventoryManualCountRecord.InventoryManualCountDetailList);

                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.VarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.Status;

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Inventory Manual Counting");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Manual Counting");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Manual Counting");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string InventoryManualCountXml(List<InventoryManualCountDetail> InventoryManualCountList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (InventoryManualCountDetail objInventoryManualCountDetail in InventoryManualCountList)
            {
                sSql.Append("<InventoryManualCount>");               
                sSql.Append("<StoreID>" + objInventoryManualCountDetail.StoreID + "</StoreID>");              
                sSql.Append("<LocationID>" + objInventoryManualCountDetail.LocationID + "</LocationID>");
                sSql.Append("<SheetName>" + objInventoryManualCountDetail.SheetName + "</SheetName>");
                sSql.Append("<BarCode>" + objInventoryManualCountDetail.BarCode + "</BarCode>");
                sSql.Append("<SKUCode>" + objInventoryManualCountDetail.SKUCode + "</SKUCode>");
                sSql.Append("<StyleCode>" + objInventoryManualCountDetail.StyleCode + "</StyleCode>");
                sSql.Append("<StockQty>" + objInventoryManualCountDetail.StockQty + "</StockQty>");               
                sSql.Append("</InventoryManualCount>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override GetInventoryManualCountRecordResponse GetInventoryManualCountRecord(GetInventoryManualCountRecordRequest ObjRequest)
        {
            var RequestData = (GetInventoryManualCountRecordRequest)ObjRequest;
            var ResponseData = new GetInventoryManualCountRecordResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select * from InventoryManualCount  where DocumentNo='" + RequestData.DocumentNo + "'";

               
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryManualCount = new InventoryManualCount();
                        objInventoryManualCount.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryManualCount.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryManualCount.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objInventoryManualCount.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInventoryManualCount.CountingType = Convert.ToString(objReader["CountingType"]);
                        objInventoryManualCount.InventoryInitID = objReader["InventoryInitID"] != DBNull.Value ? Convert.ToInt32(objReader["InventoryInitID"]) : 0;
                        objInventoryManualCount.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objInventoryManualCount.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;
                        objInventoryManualCount.UpdateOn = objReader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdatedOn"]) : DateTime.Now;
                        objInventoryManualCount.UpdateBy = objReader["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdatedBy"]) : 0;


                        objInventoryManualCount.InventoryManualCountDetailList = new List<InventoryManualCountDetail>();
                        objInventoryManualCount.InventoryManualCountDetailList = GetInventoryManualCountDetailList(objInventoryManualCount.ID, RequestData);

                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.InventoryManualCountRecord = objInventoryManualCount;
                        ResponseData.ResponseDynamicData = objInventoryManualCount;
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting Master");
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
        public List<InventoryManualCountDetail> GetInventoryManualCountDetailList(int InventoryManualCountID, GetInventoryManualCountRecordRequest RequestData)
        {
            var InventoryManualCountDetailList = new List<InventoryManualCountDetail>();
            try
            {
                SqlDataReader objReader;
                var sqlCommon = new MsSqlCommon();
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select * from InventoryManualCountDetail where InventoryManualCountID=" + InventoryManualCountID;

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryManualCountDetail = new InventoryManualCountDetail();
                        objInventoryManualCountDetail.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryManualCountDetail.InventoryManualCountID = objReader["InventoryManualCountID"] != DBNull.Value ? Convert.ToInt32(objReader["InventoryManualCountID"]) : 0;
                        objInventoryManualCountDetail.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objInventoryManualCountDetail.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objInventoryManualCountDetail.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryManualCountDetail.LocationID = objReader["LocationID"] != DBNull.Value ? Convert.ToInt32(objReader["LocationID"]) : 0;
                        objInventoryManualCountDetail.SheetName = objReader["SheetName"] != DBNull.Value ? Convert.ToString(objReader["SheetName"]) : string.Empty;
                        objInventoryManualCountDetail.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objInventoryManualCountDetail.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        InventoryManualCountDetailList.Add(objInventoryManualCountDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return InventoryManualCountDetailList;
        }

        public override InventoryFinalizeResponse InventoryFinalize(InventoryFinalizeRequest ObjRequest)
        {
            var RequestData = (InventoryFinalizeRequest)ObjRequest;
            var ResponseData = new InventoryFinalizeResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InventoryFinalize", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;  

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.DocumentNo;
               
                SqlParameter Status = _CommandObj.Parameters.Add("@Status", SqlDbType.VarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = RequestData.Status;

                SqlParameter RARemarks = _CommandObj.Parameters.Add("@RARemarks", SqlDbType.VarChar);
                RARemarks.Direction = ParameterDirection.Input;
                RARemarks.Value = RequestData.RARemarks;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@CreatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.RequestedByUserID;

                SqlParameter TransactionLogData = _CommandObj.Parameters.Add("@TransactionLogData", SqlDbType.Xml);
                TransactionLogData.Direction = ParameterDirection.Input;
                TransactionLogData.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Inventory Finalize");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;                    
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Finalize");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Finalize");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public string TransactionLogDetailMasterXML(List<TransactionLog> TransactionLogList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (TransactionLog objTransactionLog in TransactionLogList)
            {
                sSql.Append("<TransactionLogData>");
                sSql.Append("<ID>" + objTransactionLog.ID + "</ID>");
                sSql.Append("<TransactionType>" + objTransactionLog.TransactionType + "</TransactionType>");
                sSql.Append("<BusinessDate>" + sqlCommon.GetSQLServerDateString(objTransactionLog.BusinessDate) + "</BusinessDate>");
                sSql.Append("<ActualDateTime>" + sqlCommon.GetSQLServerDateString(objTransactionLog.ActualDateTime) + "</ActualDateTime>");
                sSql.Append("<DocumentID>" + (objTransactionLog.DocumentID) + "</DocumentID>");
                sSql.Append("<StyleCode>" + objTransactionLog.StyleCode + "</StyleCode>");
                sSql.Append("<SKUCode>" + objTransactionLog.SKUCode + "</SKUCode>");
                sSql.Append("<InQty>" + objTransactionLog.InQty + "</InQty>");
                sSql.Append("<OutQty>" + objTransactionLog.OutQty + "</OutQty>");
                sSql.Append("<TransactionPrice>" + objTransactionLog.TransactionPrice + "</TransactionPrice>");
                sSql.Append("<Currency>" + (objTransactionLog.Currency) + "</Currency>");
                sSql.Append("<ExchangeRate>" + (objTransactionLog.ExchangeRate) + "</ExchangeRate>");
                sSql.Append("<DocumentPrice>" + (objTransactionLog.DocumentPrice) + "</DocumentPrice>");
                sSql.Append("<UserID>" + (objTransactionLog.UserID) + "</UserID>");
                sSql.Append("<CountryID>" + (objTransactionLog.CountryID) + "</CountryID>");
                sSql.Append("<CountryCode>" + (objTransactionLog.CountryCode) + "</CountryCode>");
                sSql.Append("<StoreID>" + (objTransactionLog.StoreID) + "</StoreID>");
                sSql.Append("<StoreCode>" + (objTransactionLog.StoreCode) + "</StoreCode>");
                sSql.Append("<DocumentNo>" + (objTransactionLog.DocumentNo) + "</DocumentNo>");
                sSql.Append("</TransactionLogData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override InventorySyncResponse InventorySyncToServer(InventorySyncRequest ObjRequest)
        {
            var RequestData = (InventorySyncRequest)ObjRequest;
            var ResponseData = new InventorySyncResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertInventoryRecords", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter DocumentNo = _CommandObj.Parameters.Add("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Direction = ParameterDirection.Input;
                DocumentNo.Value = RequestData.DocumentNo;

                SqlParameter DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.Date);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.DocumentDate);

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StoreID;

                SqlParameter Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.VarChar);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = RequestData.Remarks;

                SqlParameter RARemarks = _CommandObj.Parameters.Add("@RARemarks", SqlDbType.VarChar);
                RARemarks.Direction = ParameterDirection.Input;
                RARemarks.Value = RequestData.RARemarks;

                SqlParameter CountingType = _CommandObj.Parameters.Add("@CountingType", SqlDbType.VarChar);
                CountingType.Direction = ParameterDirection.Input;
                CountingType.Value = RequestData.CountingType;

                SqlParameter CreatedBy = _CommandObj.Parameters.Add("@CreatedBy", SqlDbType.Int);
                CreatedBy.Direction = ParameterDirection.Input;
                CreatedBy.Value = RequestData.RequestedByUserID;

                SqlParameter SysCountList = _CommandObj.Parameters.Add("@SysCountList", SqlDbType.Xml);
                SysCountList.Direction = ParameterDirection.Input;
                SysCountList.Value = InventorySysCountXml(RequestData.InventorySysCountList);

                SqlParameter ManualCountList = _CommandObj.Parameters.Add("@ManualCountList", SqlDbType.Xml);
                ManualCountList.Direction = ParameterDirection.Input;
                ManualCountList.Value = InventoryManualCountXml(RequestData.ManualCountList);

                SqlParameter TransactionLogData = _CommandObj.Parameters.Add("@TransactionLogData", SqlDbType.Xml);
                TransactionLogData.Direction = ParameterDirection.Input;
                TransactionLogData.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Inventory Finalize");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "3")
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = StatusMsg.Value.ToString();
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.CreateRecordFailed;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Finalize");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Inventory Finalize");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override GetInventoryCountingInitResponse API_GetInventoryCountingInitList(GetInventoryCountingInitRequest ObjRequest)
        {
            var InventoryInitList = new List<InventoryInit>();
            var RequestData = (GetInventoryCountingInitRequest)ObjRequest;
            var ResponseData = new GetInventoryCountingInitResponse();
            SqlDataReader objReader;
            StringBuilder sbSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //sQuery = "Select * from InventoryInit ";
                //if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
                //{
                    sbSql.Append("Select ID,StoreID,DocumentNo,DocumentDate,Remarks,RARemarks,PostingDone,Status,CreatedBy,CreatedOn,ApprovedBy,PostingDate,UpdatedBy,UpdatedOn,RC.TOTAL_CNT [RecordCount]");
                    sbSql.Append(" from InventoryInit with(NoLock) ");
                    sbSql.Append("left JOIN (Select count(NI.ID) As TOTAL_CNT from InventoryInit NI with(NoLock) ");
                    sbSql.Append(" where isnull('" + RequestData.SearchString + "','') = '' or NI.DocumentNo  like isnull('%" + RequestData.SearchString + "%','')");
                    sbSql.Append(" or  NI.DocumentDate like isnull('%" + RequestData.SearchString + "%','')");
                    sbSql.Append(" or  NI.CreatedOn like isnull('%" + RequestData.SearchString + "%','')");
                    sbSql.Append(" or  NI.Status like isnull('%" + RequestData.SearchString + "%','')) As RC ON 1 = 1 ");
                    sbSql.Append(" where isnull('" + RequestData.SearchString + "','') = '' or DocumentNo  like isnull('%" + RequestData.SearchString + "%','')");
                    sbSql.Append(" or  DocumentDate like isnull('%" + RequestData.SearchString + "%','')");
                    sbSql.Append(" or  CreatedOn like isnull('%" + RequestData.SearchString + "%','')");
               
                    sbSql.Append(" or  Status like isnull('%" + RequestData.SearchString + "%','')");
                    sbSql.Append(" order by ID asc ");
                    sbSql.Append("offset " + RequestData.Offset + " rows ");
                    sbSql.Append("fetch first " + RequestData.Limit + " rows only");
                    //sSql = "Select top 100 * from OnAccountPayment";
                //}
                //else if (RequestData.SearchString != null && RequestData.SearchString != string.Empty)
                //{
                //    DateTime temp;
                //    if (DateTime.TryParse(RequestData.SearchString, out temp))
                //    {
                //        sbSql.Append("Select ID,StoreID,DocumentNo,DocumentDate,Remarks,RARemarks,PostingDone,Status,CreatedBy,CreatedOn,ApprovedBy,PostingDate,UpdatedBy,UpdatedOn,RecordCount = COUNT(*) OVER() from InventoryInit");
                //        sbSql.Append("  where DocumentDate  = isnull('" + sqlCommon.GetSQLServerDateString(Convert.ToDateTime(RequestData.SearchString)) + "','')");
                //        sbSql.Append("or  CreatedOn = isnull('" + sqlCommon.GetSQLServerDateString(Convert.ToDateTime(RequestData.SearchString)) + "','')");
                //        sbSql.Append("order by ID  asc ");
                //        sbSql.Append("offset " + RequestData.Offset + " rows ");
                //        sbSql.Append("fetch first " + RequestData.Limit + " rows only");
                //    }
                //    else
                //    {
                //        sbSql.Append("Select ID,StoreID,DocumentNo,DocumentDate,Remarks,RARemarks,PostingDone,Status,CreatedBy,CreatedOn,ApprovedBy,PostingDate,UpdatedBy,UpdatedOn , RecordCount = COUNT(*) OVER() from InventoryInit");
                //        sbSql.Append(" where  DocumentNo  = isnull('" + RequestData.SearchString + "','')");
                //        sbSql.Append("or  Status = isnull('" + RequestData.SearchString + "','')");
                //        sbSql.Append("order by ID  asc ");
                //        sbSql.Append("offset " + RequestData.Offset + " rows ");
                //        sbSql.Append("fetch first " + RequestData.Limit + " rows only");
                //    }

                //}

                _CommandObj = new SqlCommand(sbSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventoryInit = new InventoryInit();
                        objInventoryInit.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objInventoryInit.StoreID = objReader["StoreID"] != DBNull.Value ? Convert.ToInt32(objReader["StoreID"]) : 0;
                        objInventoryInit.DocumentNo = Convert.ToString(objReader["DocumentNo"]);
                        objInventoryInit.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objInventoryInit.Remarks = Convert.ToString(objReader["Remarks"]);
                        objInventoryInit.PostingDone = objReader["PostingDone"] != DBNull.Value ? Convert.ToBoolean(objReader["PostingDone"]) : false;
                        objInventoryInit.Status = Convert.ToString(objReader["Status"]);
                        objInventoryInit.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objInventoryInit.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;

                        if (objReader["PostingDate"] != DBNull.Value)
                        {
                            objInventoryInit.PostingDate = Convert.ToDateTime(objReader["PostingDate"]);
                        }

                        objInventoryInit.ApprovedBy = objReader["ApprovedBy"] != DBNull.Value ? Convert.ToInt32(objReader["ApprovedBy"]) : 0;

                        InventoryInitList.Add(objInventoryInit);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventoryInitList = InventoryInitList;
                    ResponseData.ResponseDynamicData = InventoryInitList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "InventoryCounting Master");
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

        public override GetSystemStockByStoreResponse API_SystemStockByStoreCount(GetSystemStockByStoreRequest ObjRequest)
        {
            var InventorySysCountList = new List<InventorySysCount>();
            var RequestData = (GetSystemStockByStoreRequest)ObjRequest;
            var ResponseData = new GetSystemStockByStoreResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("GetStockByStoreCount", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventorySysCountList = InventorySysCountList;
                    ResponseData.ResponseDynamicData = InventorySysCountList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "System Stock");
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

        public override GetSystemStockByStoreResponse API_SystemStockByStorelimit(GetSystemStockByStoreRequest ObjRequest)
        {
            var InventorySysCountList = new List<InventorySysCount>();
            var RequestData = (GetSystemStockByStoreRequest)ObjRequest;
            var ResponseData = new GetSystemStockByStoreResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("GetStockByStoreLimitRow", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@limit", RequestData.Limit);
                _CommandObj.Parameters.AddWithValue("@offset", RequestData.Offset);
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInventorySysCount = new InventorySysCount();
                        objInventorySysCount.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objInventorySysCount.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : string.Empty;
                        objInventorySysCount.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objInventorySysCount.ID = 0;
                        objInventorySysCount.InventoryInitID = 0;
                        objInventorySysCount.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : string.Empty;
                        objInventorySysCount.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objInventorySysCount.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                        objInventorySysCount.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objInventorySysCount.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
                        objInventorySysCount.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objInventorySysCount.StyleName = objReader["StyleName"] != DBNull.Value ? Convert.ToString(objReader["StyleName"]) : string.Empty;
                        objInventorySysCount.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objInventorySysCount.SalesPrice = objReader["SalesPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["SalesPrice"]) : 0;
                        InventorySysCountList.Add(objInventorySysCount);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.InventorySysCountList = InventorySysCountList;
                    ResponseData.ResponseDynamicData = InventorySysCountList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "System Stock");
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
