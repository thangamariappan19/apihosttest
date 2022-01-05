using EasyBizAbsDAL.Transactions.Stocks;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockStaging;
using EasyBizRequest;
using EasyBizRequest.Transactions.Stocks.StockStaging;
using EasyBizResponse;
using EasyBizResponse.Transactions.Stocks.StockStaging;
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
    public class StockStagingDAL : BaseStockStagingDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; 
        Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
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
        public override GetStockStagingRecordsByStyleCodeResponse GetStockByStyleCode(GetStockStagingRecordsByStyleCodeRequest RequestObj)
        {
            var ItemStockStagingList = new List<ItemStockStaging>();
            var RequestData = (GetStockStagingRecordsByStyleCodeRequest)RequestObj;
            var ResponseData = new GetStockStagingRecordsByStyleCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sQuery = "Select SKUCode,(SUM(InQty) + SUM(OutQty)) as StockQty from ItemStockStaging with(NoLock) where StyleCode='{0}' group by SKUCode";

                sQuery = string.Format(sQuery, RequestData.StyleCode);
                

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStockReceipt = new ItemStockStaging();
                        //objStockReceipt.ID = Convert.ToInt32(objReader["ID"]);                        
                        //objStockReceipt.DocumentDate = Convert.ToDateTime(objReader["DocumentDate"]);
                        //objStockReceipt.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                        objStockReceipt.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        objStockReceipt.StockQty = objReader["StockQty"] != DBNull.Value ? Convert.ToInt32(objReader["StockQty"]) : 0;


                        //objStockReceipt.InQty = objReader["InQty"] != DBNull.Value ? Convert.ToInt32(objReader["InQty"]) : 0;
                        //objStockReceipt.OutQty = objReader["OutQty"] != DBNull.Value ? Convert.ToInt32(objReader["OutQty"]) : 0;
                        //objStockReceipt.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objStockReceipt.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objStockReceipt.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objStockReceipt.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;                       
                        ItemStockStagingList.Add(objStockReceipt);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ItemStockStagingList = ItemStockStagingList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Item Stock Staging List");
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
