using EasyBizAbsDAL.Transactions.Cardex.CardexLocation;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.Cardex.CardexLocation;
using EasyBizRequest;
using EasyBizRequest.Transactions.Cardex.CardexLocationRequest;
using EasyBizResponse;
using EasyBizResponse.Transactions.Cardex.CardexLocationResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Transactions.Cardex.CardexLocation
{
    public class CardexLocationDAL : BaseCardexLocationDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var CardexLocationList = new List<CardexLocationDetails>();
            var RequestData = new SelectAllCardexLocationRequest();
            var ResponseData = new SelectAllCardexLocationResponse();

            RequestData = (SelectAllCardexLocationRequest)RequestObj;

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                RequestData.ShowInActiveRecords = true;

                var sSql = string.Empty;
                double tbalance = 0;
                double tInQty = 0;
                double tOutQty = 0; 
                double tIndInQty = 0;
                double tIndOutQty = 0;
                String SKUgroup = "";
                String StyleCode = "";
                String TransactionType = "";
           
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);      
 
                sSql = "GetCardexLocationDetails";
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@SearchString", '%' + RequestData.SearchString + '%');
                _CommandObj.Parameters.AddWithValue("@StoreID", RequestData.StoreID);
                _CommandObj.Parameters.AddWithValue("@DateFrom", RequestData.FromDate);
                _CommandObj.Parameters.AddWithValue("@Dateto", RequestData.ToDate);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                     
                        if (!(TransactionType == objReader["TransactionType"].ToString() && SKUgroup == objReader["SKUCode"].ToString() && objReader["TransactionType"].ToString() == "Opening Stock") )
                        {
                            var objCardexLocation = new CardexLocationDetails();

                            objCardexLocation.TransactionDate = objReader["BusinessDate"] != DBNull.Value ? Convert.ToDateTime(objReader["BusinessDate"]).ToString("dd/MMM/yyyy") : "";
                            objCardexLocation.TransactionType = objReader["TransactionType"].ToString();
                            objCardexLocation.DocumentNumber = objReader["DocNo"] != DBNull.Value ? objReader["DocNo"].ToString() : "";
                            objCardexLocation.QuantityIn = objReader["InQty"].ToString();
                            objCardexLocation.QuantityOut = objReader["OutQty"].ToString();
                            objCardexLocation.LocationCode = objReader["StoreID"] != DBNull.Value ? objReader["StoreID"].ToString() : "";
                            objCardexLocation.SKUCode = objReader["SKUCode"] != DBNull.Value ? objReader["SKUCode"].ToString() : "";
                            objCardexLocation.StyleCode = objReader["StyleCode"] != DBNull.Value ? objReader["StyleCode"].ToString() : "";                       
                            if (SKUgroup != objCardexLocation.SKUCode && SKUgroup != "")
                            {
                                var objSubTotalCardexLocation = new CardexLocationDetails();
                                objSubTotalCardexLocation.SKUCode = SKUgroup;
                                objSubTotalCardexLocation.StyleCode =StyleCode;
                                objSubTotalCardexLocation.QuantityIn = tIndInQty.ToString();
                                objSubTotalCardexLocation.QuantityOut = tIndOutQty.ToString();
                                objSubTotalCardexLocation.TransactionType = "TOTAL";
                                objSubTotalCardexLocation.Balance = tbalance;
                                CardexLocationList.Add(objSubTotalCardexLocation);
                                tbalance = 0;
                                tIndInQty = 0;
                                tIndOutQty = 0;
                            }
                            SKUgroup = objCardexLocation.SKUCode;
                            StyleCode = objCardexLocation.StyleCode;
                            TransactionType = objCardexLocation.TransactionType;
                            tInQty = tInQty + Convert.ToDouble(objCardexLocation.QuantityIn);
                            tOutQty = tOutQty + Convert.ToDouble(objCardexLocation.QuantityOut);
                            tIndInQty = tIndInQty + Convert.ToDouble(objCardexLocation.QuantityIn);
                            tIndOutQty = tIndOutQty + Convert.ToDouble(objCardexLocation.QuantityOut);
                            tbalance = tbalance + Convert.ToDouble(objCardexLocation.QuantityIn) - Convert.ToDouble(objCardexLocation.QuantityOut);
                            objCardexLocation.Balance = tbalance;
                       
                            CardexLocationList.Add(objCardexLocation);
                        }
                    }
                    if (CardexLocationList.Count > 0 )
                    {
                        var objSubTtlCardexLocation = new CardexLocationDetails();
                        objSubTtlCardexLocation.SKUCode = SKUgroup;
                        objSubTtlCardexLocation.StyleCode = StyleCode;
                        objSubTtlCardexLocation.QuantityIn = tIndInQty.ToString();
                        objSubTtlCardexLocation.QuantityOut = tIndOutQty.ToString();
                        objSubTtlCardexLocation.TransactionType = "TOTAL";
                        objSubTtlCardexLocation.Balance = tbalance;
                        CardexLocationList.Add(objSubTtlCardexLocation);
                    }
                    var objCardexLocationTotal =  new CardexLocationTotalDetails();                  
                    objCardexLocationTotal.TotalInQty = Convert.ToString(tInQty);
                    objCardexLocationTotal.TotalOutQty = Convert.ToString(tOutQty);
                    objCardexLocationTotal.TotalBalance = Convert.ToString(tInQty - tOutQty);
                    ResponseData.CardexLocationTotalData = objCardexLocationTotal;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CardexLOcationData = CardexLocationList;
                 
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Ledger");
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
            return null;
        }
        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            return null;
        }
        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            return null;
        }
        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            return null;
        }
        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
