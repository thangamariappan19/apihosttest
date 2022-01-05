using EasyBizAbsDAL.Transactions.TransactionLogs;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.TransactionLogs;
using EasyBizRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizRequest.Transactions.POS.NonTradingStockRequest;
using EasyBizRequest.Transactions.TransactionLogs;
using EasyBizResponse;
using EasyBizResponse.Masters.SKUMasterResponse;
using EasyBizResponse.Transactions.POS.NonTradingStockResponse;
using EasyBizResponse.Transactions.TransactionLogs;
using MsSqlDAL.Common;
using MsSqlDAL.Masters;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.TransactionLogs
{
    public class TransactionLogsDAL : BaseTransactionLogsDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;
        Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveTransactionLogRequest)RequestObj;
            var ResponseData = new SaveTransactionLogResponse();
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertTransactionLog", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.CommandTimeout = 300000;

                SqlParameter TransactionLogDetails = _CommandObj.Parameters.Add("@TransactionLog", SqlDbType.Xml);
                TransactionLogDetails.Direction = ParameterDirection.Input;
                TransactionLogDetails.Value = TransactionLogDetailMasterXML(RequestData.TransactionLogList);

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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "TransactionLog");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = TLIDs.Value.ToString(); 
                }              
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "TransactionLog");
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
                sSql.Append("<BusinessDate>" +sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.BusinessDate) + "</BusinessDate>");
                sSql.Append("<ActualDateTime>" + sqlCommon.GetSQLServerDateString(objTransactionLogDetailMasterDetails.ActualDateTime) + "</ActualDateTime>");
                //sSql.Append("<ReasonID>" + objTransactionLogDetailMasterDetails.ReasonID + "</ReasonID>");
                sSql.Append("<DocumentID>" + (objTransactionLogDetailMasterDetails.DocumentID) + "</DocumentID>");
                //sSql.Append("<DocumentLineID>" + (objTransactionLogDetailMasterDetails.DocumentLineID) + "</DocumentLineID>");
                //sSql.Append("<SKUCode>" + objTransactionLogDetailMasterDetails.SKUCode + "</SKUCode>");                
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
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override GetStockByStyleCodeResponse GetStockByStyleCode(GetStockByStyleCodeRequest RequestObj)
        {
            var StockList = new List<TransactionLog>();
            var ColorWiseStockList = new List<TransactionLog>();
            var ScaleWiseStockList = new List<TransactionLog>();
            var RequestData = (GetStockByStyleCodeRequest)RequestObj;
            var ResponseData = new GetStockByStyleCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.StockWiseName == "All")
                {
                    sSql.Append("SELECT SM.SKUCode,SM.SKUName,SM.BarCode,SM.ColorCode,CM.Description AS 'ColorName',SM.StyleCode,SMD.SizeCode,SMD.Description AS 'SizeName',SMD.VisualOrder, ");
                    sSql.Append("ISNULL((SUM(TL.InQty) - SUM(TL.OutQty)), 0) AS StockQty FROM SKUMaster SM WITH (NOLOCK) INNER JOIN ColorMaster CM WITH (NOLOCK) ON SM.ColorID = CM.ID ");
                    sSql.Append("INNER JOIN ScaleMasterDetails SMD ON SM.SizeID = SMD.ID ");
                    sSql.Append("LEFT OUTER JOIN TransactionLog TL WITH (NOLOCK) ON SM.SKUCode = TL.SKUCode where SM.StyleCode='" + RequestObj.StyleCode + "' ");
                    sSql.Append("GROUP BY SM.SKUCode,SM.SKUName,SM.ColorCode,CM.Description,SMD.SizeCode,SMD.Description,SMD.VisualOrder,SM.StyleCode,SM.BarCode ");
                    sSql.Append("ORDER BY SMD.VisualOrder,CM.Description ");

                    //sQuery = "Select TL.SKUCode,SM.StyleCode,sm.SKUName,(SUM(InQty) - SUM(OutQty)) as StockQty from TransactionLog TL with(NoLock) inner join SKUMaster SM on sm.SKUCode=TL.SKUCode where TL.StyleCode='{0}' Group by TL.SKUCode,SM.StyleCode,sm.SKUName";
                    //sQuery = "Select TL.SKUCode,SM.SKUName,(SUM(InQty) - SUM(OutQty)) as StockQty from TransactionLog TL with(NoLock) inner join SKUMaster SM on sm.StyleCode=TL.StyleCode where TL.StyleCode='{0}' Group by TL.SKUCode,SM.SKUName";
                    //sQuery = "SELECT distinct T1.SKUCode,T1.SKUName,T1.StyleCode, ISNULL(( SUM(T0.InQty) - SUM(T0.OutQty)),0) AS StockQty FROM	dbo.SKUMaster T1 cross JOIN storemaster SM left join TransactionLog T0 WITH ( NOLOCK ) on T0.skuCode=T1.SkuCode and T0.StoreID=SM.ID WHERE T1.StyleCode= '{0}' GROUP BY T1.SKUCode, T1.StyleCode,T1.SKUName";
                   // sQuery = string.Format(sSql, RequestData.StyleCode);

                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objTransactionLog = new TransactionLog();
                            objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                            objTransactionLog.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                            objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                            objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                            objTransactionLog.ColorName = objReader["ColorName"] != DBNull.Value ? Convert.ToString(objReader["ColorName"]) : string.Empty;
                            objTransactionLog.SizeName = objReader["SizeName"] != DBNull.Value ? Convert.ToString(objReader["SizeName"]) : string.Empty;
                            objTransactionLog.VisualOrder = objReader["VisualOrder"] != DBNull.Value ? Convert.ToString(objReader["VisualOrder"]) : string.Empty;
                            objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                            objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                            objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : String.Empty;
                            
                            StockList.Add(objTransactionLog);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.StockList = StockList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Item Stock List");
                    }
                }
                else if (RequestData.StockWiseName == "Color")
                {
                    sQuery = "SELECT distinct T1.StyleCode, T1.ColorCode,CM.Description, ISNULL(( SUM(T0.InQty) - SUM(T0.OutQty)),0) AS StockQty FROM	dbo.SKUMaster T1 cross JOIN storemaster SM left join TransactionLog T0 WITH ( NOLOCK ) on T0.skuCode=T1.SkuCode and T0.StoreID=SM.ID INNER JOIN ColorMaster CM ON T1.ColorCode=CM.ColorCode WHERE T1.StyleCode= '{0}' GROUP BY T1.StyleCode, T1.ColorCode ,CM.Description, T1.SKUName";

                    sQuery = string.Format(sQuery, RequestData.StyleCode);

                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objTransactionLog = new TransactionLog();
                            //objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                            //objTransactionLog.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                            objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                            objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                            objTransactionLog.ColorName = objReader["Description"] != DBNull.Value ? Convert.ToString(objReader["Description"]) : string.Empty;

                            ColorWiseStockList.Add(objTransactionLog);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ColorWiseStockList = ColorWiseStockList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Item Stock List");
                    }
                }
                else if (RequestData.StockWiseName == "Scale")
                {
                    sQuery = "SELECT distinct T1.SKUCode,T1.SKUName,t1.BarCode,T1.StyleCode, T1.SizeCode,CM.Description, ISNULL(( SUM(T0.InQty) - SUM(T0.OutQty)),0) AS StockQty FROM	dbo.SKUMaster T1 cross JOIN storemaster SM left join TransactionLog T0 WITH ( NOLOCK ) on T0.skuCode=T1.SkuCode and T0.StoreID=SM.ID INNER JOIN ColorMaster CM ON T1.ColorCode=CM.ColorCode WHERE T1.StyleCode= '{0}' GROUP BY T1.SKUCode, T1.StyleCode, T1.SizeCode ,CM.Description, T1.SKUName,t1.BarCode";

                    sQuery = string.Format(sQuery, RequestData.StyleCode);

                    _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objTransactionLog = new TransactionLog();
                            objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                            objTransactionLog.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                            objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                            objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : string.Empty;
                            objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                            ScaleWiseStockList.Add(objTransactionLog);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ScaleWiseStockList = ScaleWiseStockList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Item Stock List");
                    }
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

        public override GetStockByStyleCodeResponse GetStockByStyleOverView(GetStockByStyleCodeRequest RequestObj)
        {
            var StockList = new List<TransactionLog>();
            var ColorWiseStockList = new List<TransactionLog>();
            var ScaleWiseStockList = new List<TransactionLog>();
            var RequestData = (GetStockByStyleCodeRequest)RequestObj;
            var ResponseData = new GetStockByStyleCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //sSql.Append("SELECT SM.SKUCode,SM.SKUName,SM.BarCode,SM.ColorCode,CM.Description AS 'ColorName',SM.StyleCode,SMD.SizeCode,SMD.Description AS 'SizeName',SMD.VisualOrder, ");
                //sSql.Append("ISNULL((SUM(TL.InQty) - SUM(TL.OutQty)), 0) AS StockQty FROM SKUMaster SM WITH (NOLOCK) INNER JOIN ColorMaster CM WITH (NOLOCK) ON SM.ColorID = CM.ID ");
                //sSql.Append("INNER JOIN ScaleMasterDetails SMD ON SM.SizeID = SMD.ID ");
                //sSql.Append("LEFT OUTER JOIN TransactionLog TL WITH (NOLOCK) ON SM.SKUCode = TL.SKUCode ");
                //sSql.Append("where SM.SKUCode='" + RequestData.SearchValue + "' or SM.StyleCode='" + RequestData.SearchValue + "'");               
                //sSql.Append("GROUP BY SM.SKUCode,SM.SKUName,SM.ColorCode,CM.Description,SMD.SizeCode,SMD.Description,SMD.VisualOrder,SM.StyleCode,SM.BarCode ");
                //sSql.Append("ORDER BY SMD.VisualOrder,CM.Description ");

                _CommandObj = new SqlCommand("API_GetStock", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                if (RequestData.SearchValue != null && RequestData.SearchValue != string.Empty)
                {
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchValue);
                }
                else
                {
                    _CommandObj.Parameters.AddWithValue("@SearchString", string.Empty);
                }
                _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.ColorName = objReader["ColorName"] != DBNull.Value ? Convert.ToString(objReader["ColorName"]) : string.Empty;
                        objTransactionLog.SizeName = objReader["SizeName"] != DBNull.Value ? Convert.ToString(objReader["SizeName"]) : string.Empty;
                        objTransactionLog.VisualOrder = objReader["VisualOrder"] != DBNull.Value ? Convert.ToString(objReader["VisualOrder"]) : string.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : String.Empty;
                        objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : String.Empty;

                        if (RequestData.RequestFrom != Enums.RequestFrom.DefaultLoad && (RequestData.SearchValue != null && RequestData.SearchValue != string.Empty))
                        {
                            long StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt64(objReader["StyleID"]) : 0;

                            SKUMasterDAL _SKUMasterDAL = new SKUMasterDAL();

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = 0;
                            objSelectByALLSKUImagesRequest.StyleID = StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = _SKUMasterDAL.SelectAllSKUImages(objSelectByALLSKUImagesRequest);

                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                var SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                var _ImageProcess = new DataBaseImageProcess();

                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(SKUImage);
                                objTransactionLog.SKUImageSource = SKUImage;
                                //objTransactionLog.SKUImageSource = _ImageProcess.GetImageStream(image);
                            }
                        }
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockList = StockList;

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
        public override GetStockBySkuResponse GetStockBySku(GetStockBySkuRequest RequestObj)
        {
            var StockList = new List<TransactionLog>();            
            var RequestData = (GetStockBySkuRequest)RequestObj;
            var ResponseData = new GetStockBySkuResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_GetStockBySKU", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SKUCode", RequestData.SKUCode);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        /*objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
                        objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;                        
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;*/
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockData = StockList.FirstOrDefault();
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
        public override GetStockBySkuResponse GetStockBySku1(GetStockBySkuRequest RequestObj)
        {
            var StockList = new List<TransactionLog>();
            var ScaleWiseStockList = new List<TransactionLog>();
            var RequestData = (GetStockBySkuRequest)RequestObj;
            var ResponseData = new GetStockBySkuResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetStockBySKU1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SKUCode", RequestData.SKUCode);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objTransactionLog.BrandName = objReader["brandname"] != DBNull.Value ? Convert.ToString(objReader["brandname"]) : string.Empty;
                        objTransactionLog.ColorName = objReader["colorname"] != DBNull.Value ? Convert.ToString(objReader["colorname"]) : string.Empty;
                        objTransactionLog.SizeName = objReader["sizename"] != DBNull.Value ? Convert.ToString(objReader["sizename"]) : string.Empty;
                        ScaleWiseStockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleWiseStockList = ScaleWiseStockList;
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

        public override FindStockResponse GetStoreStockByCountry(FindStockRequest RequestObj)
        {
            var StockList = new List<TransactionLog>();
            var RequestData = (FindStockRequest)RequestObj;
            var ResponseData = new FindStockResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetStoreStockByCountry", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.CountryID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        //objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : String.Empty;
                        objTransactionLog.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockList = StockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Item Stock");
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

        public override FindStockResponse GetStyleSummary(FindStockRequest RequestObj)
        {
            DataSet StyleSummaryDataSet = new DataSet();
            SqlDataAdapter DataAdapter = new SqlDataAdapter();

            var StockList = new List<TransactionLog>();
            var RequestData = (FindStockRequest)RequestObj;
            var ResponseData = new FindStockResponse();
           
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("GetStyleSummary", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.CountryID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                DataAdapter = new SqlDataAdapter(_CommandObj);
                DataAdapter.Fill(StyleSummaryDataSet);

                ResponseData.StyleSummaryDataSet = StyleSummaryDataSet;
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
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

        public override GetQuantityBySKUResponse GetQuantityBySku(GetQuantityBySKURequest RequestObj)
        {

            var QuantityBySKUList = new List<TransactionLog>();
            var RequestData = (GetQuantityBySKURequest)RequestObj;
            var ResponseData = new GetQuantityBySKUResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sSql.Append("select sm.stylecode,sm.ColorCode,sm.SizeCode, ISNULL((SUM(TL.InQty) - SUM(TL.OutQty)), 0) AS StockQty from SKUMaster SM ");
                sSql.Append("left outer join TransactionLog TL on SM.SKUCode = TL.SKUCode  where  SM.SKUCode like  '" + RequestData.Department + "-" + RequestData.Productcode + "-" + RequestData.ColorCode + "-" + "%" + "' ");
                sSql.Append("  group by sm.SKUCode ,sm.stylecode,sm.ColorCode,sm.SizeCode ");

                //sSql.Append("SELECT ISNULL((SUM(TL.InQty) - SUM(TL.OutQty)), 0) AS StockQty FROM SKUMaster SM WITH (NOLOCK)");
                //sSql.Append("LEFT OUTER JOIN TransactionLog TL WITH (NOLOCK) ON SM.SKUCode = TL.SKUCode ");
                //sSql.Append("and   SM.SKUCode='" + RequestData.Department + "-" + RequestData.Productcode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "'");
                

                                 
                //sSql.Append("SELECT ISNULL((SUM(TL.InQty) - SUM(TL.OutQty)), 0) AS StockQty FROM SKUMaster SM WITH (NOLOCK)");
                //sSql.Append("LEFT OUTER JOIN TransactionLog TL WITH (NOLOCK) ON SM.SKUCode = TL.SKUCode and (SM.SKUCode='" + RequestData.Department + "-" + RequestData.Productcode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "')RIGHT OUTER JOIN StoreMaster Store WITH (NOLOCK) ");
                //sSql.Append("ON Store.ID=TL.StoreID JOIN StoreBrandMapping SBM WITH (NOLOCK) ON SBM.StoreID = Store.ID where Store.ID='" + RequestData.StoreID +"'");            
               //sSql.Append("where SKUCode='" + RequestData.Department + "-" + RequestData.Productcode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "'");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;            
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : String.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : String.Empty;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;

                        QuantityBySKUList.Add(objTransactionLog);
                        //ResponseData.ResponseDynamicData = objTransactionLog;
                    }
                    ResponseData.QuantityBySKUList = QuantityBySKUList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Transaction Log");
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

        public override GetStockByStyleCodeResponse GetStockPivotByStyle(GetStockByStyleCodeRequest RequestObj)
        {
            var QuantityBySKUList = new List<TransactionLog>();
            var RequestData = (GetStockByStyleCodeRequest)RequestObj;
            var ResponseData = new GetStockByStyleCodeResponse();           
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;               
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                var _DataSet = new DataSet();
                _CommandObj = new SqlCommand("GetStockPivotByStyle", _ConnectionObj);
                _CommandObj.Parameters.AddWithValue("@StyleCode", RequestData.StyleCode);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.FromOrToStoreID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var _SqlAdapter = new SqlDataAdapter(_CommandObj);
                _SqlAdapter.Fill(_DataSet);
                ResponseData.StockDataSet = _DataSet;

                ResponseData.StatusCode = Enums.OpStatusCode.Success;
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

        public override GetNonTradingStockBySKUResponse GetNonTradingStockBySku(GetNonTradingStockBySKURequest objRequest)
        {
            var NonTradingStockList = new List<TransactionLog>();
            var RequestData = (GetNonTradingStockBySKURequest)objRequest;
            var ResponseData = new GetNonTradingStockBySKUResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_GetNonTradingStockBySKU", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SKUCode", RequestData.SKUCode);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
                        objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        NonTradingStockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.NonTradingStockList = NonTradingStockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.InvalidInput;
                    ResponseData.DisplayMessage = "Item not found or Item is not a Non-TradingItem";
                    //ResponseData.NonTradingStockData = null;
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

        public override FindStockByCountryResponse GetFindStockByCountry(FindStockRequest objRequest)
        {
            var StockList = new List<FindStockByCountry>();
            var RequestData = (FindStockRequest)objRequest;
            var ResponseData = new FindStockByCountryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_FindStock", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.CountryID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new FindStockByCountry();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;                      
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.StoreCode = objReader["StoreCode"] != DBNull.Value ? Convert.ToString(objReader["StoreCode"]) : String.Empty;
                        objTransactionLog.StoreName = objReader["StoreName"] != DBNull.Value ? Convert.ToString(objReader["StoreName"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objTransactionLog.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;                      
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockByCountryList = StockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Item Stock");
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

        public override GetProductDescSearchResponse GetProductDescSearch(GetProductDescSearchRequest objRequest)
        {
            var StockList = new List<ProductDescSearch>();
            var RequestData = (GetProductDescSearchRequest)objRequest;
            var ResponseData = new GetProductDescSearchResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_ProductDescriptionSearch", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.Storeid);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new ProductDescSearch();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
                        objTransactionLog.BrandName = objReader["BrandName"] != DBNull.Value ? Convert.ToString(objReader["BrandName"]) : String.Empty;
                        objTransactionLog.SubBrandCode = objReader["SubBrandCode"] != DBNull.Value ? Convert.ToString(objReader["SubBrandCode"]) : String.Empty;
                        objTransactionLog.SubBrandName = objReader["SubBrandName"] != DBNull.Value ? Convert.ToString(objReader["SubBrandName"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objTransactionLog.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductDescList = StockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Description");
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

        public override GetProductDescSearchResponse GetPOSProductDescSearch(GetProductDescSearchRequest objRequest)
        {
            var StockList = new List<ProductDescSearch>();
            var RequestData = (GetProductDescSearchRequest)objRequest;
            var ResponseData = new GetProductDescSearchResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_POSProductDescriptionSearch", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.Storeid);
                _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.PriceListID);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new ProductDescSearch();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
                        objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
                        //objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        //objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        //objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
                        objTransactionLog.BrandName = objReader["BrandName"] != DBNull.Value ? Convert.ToString(objReader["BrandName"]) : String.Empty;
                        //objTransactionLog.SubBrandCode = objReader["SubBrandCode"] != DBNull.Value ? Convert.ToString(objReader["SubBrandCode"]) : String.Empty;
                        objTransactionLog.SubBrandName = objReader["SubBrandName"] != DBNull.Value ? Convert.ToString(objReader["SubBrandName"]) : String.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objTransactionLog.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductDescList = StockList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Product Description");
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

        public override GetStockByStyleCodeResponse GetStoreStockByStyleOverView(GetStockByStyleCodeRequest objRequest)
        {
            var StockList = new List<TransactionLog>();
            var ColorWiseStockList = new List<TransactionLog>();
            var ScaleWiseStockList = new List<TransactionLog>();
            var RequestData = (GetStockByStyleCodeRequest)objRequest;
            var ResponseData = new GetStockByStyleCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_GetStoreStockStyleWise", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;             
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchValue);   
                _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                _CommandObj.Parameters.AddWithValue("@FromName",RequestData.StockWiseName);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objTransactionLog = new TransactionLog();
                        objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objTransactionLog.SKUName = objReader["SKUName"] != DBNull.Value ? Convert.ToString(objReader["SKUName"]) : string.Empty;
                        objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
                        objTransactionLog.ColorName = objReader["ColorName"] != DBNull.Value ? Convert.ToString(objReader["ColorName"]) : string.Empty;
                        //objTransactionLog.SizeName = objReader["SizeName"] != DBNull.Value ? Convert.ToString(objReader["SizeName"]) : string.Empty;
                        //objTransactionLog.VisualOrder = objReader["VisualOrder"] != DBNull.Value ? Convert.ToString(objReader["VisualOrder"]) : string.Empty;
                        objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
                        objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
                        objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : String.Empty;
                        objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : String.Empty;
                        long StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt64(objReader["StyleID"]) : 0;
                        objTransactionLog.BinCode = objReader["BinCode"] != DBNull.Value ? Convert.ToString(objReader["BinCode"]) : string.Empty;
                        objTransactionLog.BinID = objReader["BinID"]!= DBNull.Value ? Convert.ToInt32(objReader["BinID"]) : 0;
                        objTransactionLog.BinSubLevelCode = objReader["BinSubLevelCode"] != DBNull.Value ? Convert.ToString(objReader["BinSubLevelCode"]) : string.Empty;
                        objTransactionLog.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        SKUMasterDAL _SKUMasterDAL = new SKUMasterDAL();

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = 0;
                            objSelectByALLSKUImagesRequest.StyleID = StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = _SKUMasterDAL.SelectAllSKUImages(objSelectByALLSKUImagesRequest);

                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                var SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                var _ImageProcess = new DataBaseImageProcess();

                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(SKUImage);
                                objTransactionLog.SKUImageSource = SKUImage;
                                //objTransactionLog.SKUImageSource = _ImageProcess.GetImageStream(image);
                            }
                        
                        StockList.Add(objTransactionLog);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StockList = StockList;

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


        public override GetProductCommonSearchResponse GetProductCommonSearch(GetProductCommonSearchRequest objRequest)
        {
            var SearchEngineList = new List<SearchEngine>();
            var RequestData = new GetProductCommonSearchRequest();
            var ResponseData = new GetProductCommonSearchResponse();

            RequestData = (GetProductCommonSearchRequest)objRequest;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                if (RequestData.SearchString != null)
                {
                    //sSql.Append("SELECT Top 10 SKU.SKUCode [Code],SKU.SKUName [Name], SKU.BarCode [Number] ,SKU.SupplierBarcode [Type] FROM   SKUMaster SKU WITH(NOLOCK) ");
                    //sSql.Append("Left JOIN TransactionLog TL WITH(NOLOCK) ON TL.SKUCode = SKU.SKUCode ");
                    //sSql.Append(" WHERE TL.StoreID = " + RequestData.Storeid + " and SKU.SKUCode LIKE '%" + RequestData.SearchString + "%' Or SKU.SKUName LIKE '%" + RequestData.SearchString + "%' Or SKU.Barcode LIKE '%" + RequestData.SearchString + "%'");

                    string qry = " SELECT Top 10 SKU.SKUCode [Code], SKU.SKUName [Name], SKU.BarCode [Number] \n " +
                                " 	, SKU.SupplierBarcode [Type] 											 \n " +
                                " FROM   SKUMaster SKU WITH(NOLOCK) 										 \n " +
                                " Left JOIN TransactionLog TL WITH(NOLOCK) ON TL.SKUCode = SKU.SKUCode  	 \n " +
                                " WHERE TL.StoreID = " + RequestData.Storeid + " " +
                                "   and SKU.SKUCode LIKE '%" + RequestData.SearchString + "%' 				 \n " +
                                " 	Or SKU.SKUName LIKE '%" + RequestData.SearchString + "%' 				 \n " +
                                " 	Or SKU.Barcode LIKE '%" + RequestData.SearchString + "%'				 \n " +
                                " group by SKU.SKUCode,SKU.SKUName, SKU.BarCode,SKU.SupplierBarcode			 \n " +
                                " order by SKU.SKUCode,SKU.SKUName, SKU.BarCode,SKU.SupplierBarcode			  ";

                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(qry, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objSearchEngine = new SearchEngine();

                            objSearchEngine.Code = objReader["Code"] != DBNull.Value ? Convert.ToString(objReader["Code"]) : string.Empty;
                            objSearchEngine.Name = objReader["Name"] != DBNull.Value ? Convert.ToString(objReader["Name"]) : string.Empty;
                            objSearchEngine.Type = objReader["Type"] != DBNull.Value ? Convert.ToString(objReader["Type"]) : string.Empty;
                            objSearchEngine.Number = objReader["Number"] != DBNull.Value ? Convert.ToString(objReader["Number"]) : string.Empty;



                            SearchEngineList.Add(objSearchEngine);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.SearchEngineDataList = SearchEngineList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Search Engine");
                    }
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.InvalidInput;
                    ResponseData.SearchEngineDataList = null;
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


        //public override GetNonTradingStockBySKUResponse GetNonTradingStockBySku(GetNonTradingStockBySKURequest objRequest)
        //{
        //    var NonTradingStockList = new List<TransactionLog>();
        //    var RequestData = (GetNonTradingStockBySKURequest)objRequest;
        //    var ResponseData = new GetNonTradingStockBySKUResponse();
        //    SqlDataReader objReader;
        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        _ConnectionString = RequestData.ConnectionString;
        //        _RequestFrom = RequestData.RequestFrom;

        //        var sSql = new StringBuilder();

        //        string sQuery = string.Empty;
        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

        //        _CommandObj = new SqlCommand("GetNonTradingStockBySKU", _ConnectionObj);
        //        _CommandObj.CommandType = CommandType.StoredProcedure;
        //        _CommandObj.Parameters.AddWithValue("@SKUCode", RequestData.SKUCode);
        //        _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
        //        _CommandObj.CommandType = CommandType.StoredProcedure;

        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                var objTransactionLog = new TransactionLog();
        //                objTransactionLog.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
        //                objTransactionLog.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
        //                objTransactionLog.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
        //                objTransactionLog.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"]) : string.Empty;
        //                objTransactionLog.SupplierBarCode = objReader["SupplierBarCode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarCode"]) : string.Empty;
        //                objTransactionLog.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : String.Empty;
        //                objTransactionLog.ColorCode = objReader["ColorCode"] != DBNull.Value ? Convert.ToString(objReader["ColorCode"]) : string.Empty;
        //                objTransactionLog.SizeCode = objReader["SizeCode"] != DBNull.Value ? Convert.ToString(objReader["SizeCode"]) : String.Empty;
        //                objTransactionLog.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;
        //                NonTradingStockList.Add(objTransactionLog);
        //            }
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //            ResponseData.NonTradingStockData = NonTradingStockList.FirstOrDefault();
        //        }
        //        else
        //        {
        //            ResponseData.StatusCode = Enums.OpStatusCode.InvalidInput;
        //            ResponseData.DisplayMessage = "Item not found or Item is not a Non-TradingItem";
        //            //ResponseData.NonTradingStockData = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlCommon.CloseConnection(_ConnectionObj);
        //    }
        //    return ResponseData;
        //}
    }
}
