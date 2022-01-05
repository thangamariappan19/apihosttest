using EasyBizAbsDAL.Reports;
using EasyBizAbsDAL.SalesTarget;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.POS;
using EasyBizDBTypes.Transactions.POSOperations;
using EasyBizRequest.SalesTargetRequest;
using EasyBizResponse.SalesTargetResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.SalesTarget
{
    public class SalesTargetDAL : BaseSalesTargetDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.SalesTargetResponse.SalestargetHistoryResponse HistorySalesTarget(EasyBizRequest.SalesTargetRequest.SalestargetHistoryRequest ObjRequest)
        {
            var SalestargetDetailsList = new List<SalestargetDetails>();
            var RequestData = (SalestargetHistoryRequest)ObjRequest;
            var ResponseData = new SalestargetHistoryResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                //string sSql = "select month(BusinessDate)'Month',sum(a.x) as SalesQty,sum(a.y) as salesamount from (select sum(TotalQty) x,sum(NetAmount) y, BusinessDate from InvoiceHeader where StoreID in(" + RequestData.StoreIDs + ") and month(BusinessDate)=coalesce(NULLIF('',''),month(BusinessDate)) and year(BusinessDate)=coalesce(nullif('" + RequestData.Year + "',''),year(BusinessDate))  group by BusinessDate) a group by month(BusinessDate)";             
                string sSql = "select month(BusinessDate)'Month',sum(a.x) as SalesQty,sum(a.y) as salesamount from (select sum(qty) x,sum(price) y, ih.BusinessDate from InvoiceDetail join InvoiceHeader ih on ih.id=InvoiceDetail.InvoiceHeaderID where ih.StoreID in(" + RequestData.StoreIDs + ") and IsReturned IS NULL and month(BusinessDate)=coalesce(NULLIF('',''),month(BusinessDate)) and year(BusinessDate)=coalesce(nullif('" + RequestData.Year + "',''),year(BusinessDate))  group by BusinessDate) a group by month(BusinessDate)";             
                sSql = string.Format(sSql);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objInvoiceHeaderTypes = new SalestargetDetails();

                        objInvoiceHeaderTypes.Qty = objReader["SalesQty"] != DBNull.Value ? Convert.ToInt32(objReader["SalesQty"]) : 0;
                        objInvoiceHeaderTypes.Amount = objReader["salesamount"] != DBNull.Value ? Convert.ToDecimal(objReader["salesamount"]) : 0;
                        objInvoiceHeaderTypes.Month = objReader["Month"] != DBNull.Value ? Convert.ToString(objReader["Month"]) : string.Empty;
                        SalestargetDetailsList.Add(objInvoiceHeaderTypes);
                    }
                    ResponseData.SalestargetDetailsList = SalestargetDetailsList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = SalestargetDetailsList;
                }
                else
                {
                    while (objReader.Read())
                    {
                    var objInvoiceHeaderTypes = new SalestargetDetails();

                        objInvoiceHeaderTypes.Qty = 0;
                        objInvoiceHeaderTypes.Amount = 0;
                        objInvoiceHeaderTypes.Month = "";
                        SalestargetDetailsList.Add(objInvoiceHeaderTypes);
                    }
                    ResponseData.SalestargetDetailsList = SalestargetDetailsList;
                    //ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ResponseDynamicData = SalestargetDetailsList;

                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    //ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Salestarget");
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
            var RequestData = (SaveSalesTargetRequest)RequestObj;
            var ResponseData = new SaveSalesTargetResponse();

            var sqlCommon = new MsSqlCommon();

            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertSalesTarget", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var HeaderID = _CommandObj.Parameters.Add("@HeaderID", SqlDbType.Int);
                HeaderID.Direction = ParameterDirection.Input;
                HeaderID.Value = RequestData.SalesTargetHeaderRecord.ID;

                var CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.SalesTargetHeaderRecord.CountryID;

                var BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                BrandID.Direction = ParameterDirection.Input;
                BrandID.Value = RequestData.SalesTargetHeaderRecord.BrandID;

                var StateID = _CommandObj.Parameters.Add("@Brand", SqlDbType.VarChar);
                StateID.Direction = ParameterDirection.Input;
                StateID.Value = RequestData.SalesTargetHeaderRecord.Brand;


                var StoreID = _CommandObj.Parameters.Add("@StoreIDs", SqlDbType.VarChar);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.SalesTargetHeaderRecord.StoreIDs;

                var PosID = _CommandObj.Parameters.Add("@Year", SqlDbType.VarChar);
                PosID.Direction = ParameterDirection.Input;
                PosID.Value = RequestData.SalesTargetHeaderRecord.Year;               

                var DocumentTypeID = _CommandObj.Parameters.Add("@DocumentTypeID", SqlDbType.Int);
                DocumentTypeID.Direction = ParameterDirection.Input;
                DocumentTypeID.Value = RequestData.SalesTargetHeaderRecord.DocumentTypeID;

                //var Remarks = _CommandObj.Parameters.Add("@Remarks", SqlDbType.NVarChar);
                //Remarks.Direction = ParameterDirection.Input;
                //Remarks.Value = RequestData.DocumentNumberingMasterRecord.Remarks;

                var DocumentNumberingDetails = _CommandObj.Parameters.Add("@SalesTargetDetails", SqlDbType.Xml);
                DocumentNumberingDetails.Direction = ParameterDirection.Input;
                DocumentNumberingDetails.Value = SalesTargetDetailsXml(RequestData.SalestargetDetailsList);

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.SalesTargetHeaderRecord.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Sales Target");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Target");
                }
                else
                {


                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Sales Target");

                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Sales Target");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        private string SalesTargetDetailsXml(List<SalestargetDetails> SalestargetDetailsList)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (SalestargetDetails objSalestargetDetails in SalestargetDetailsList)
            {
                sSql.Append("<SalestargetDetails>");
                sSql.Append("<ID>" + (objSalestargetDetails.ID) + "</ID>");
                sSql.Append("<StoreCode>" + (objSalestargetDetails.StoreCode) + "</StoreCode>");
                sSql.Append("<Qty>" + (objSalestargetDetails.Qty) + "</Qty>");
                sSql.Append("<Amount>" + objSalestargetDetails.Amount + "</Amount>");
                sSql.Append("<Month>" + (objSalestargetDetails.Month) + "</Month>");
                //sSql.Append("<CreateBy>" + objDocumentNumberingDetails.CreateBy + "</CreateBy>");
                sSql.Append("</SalestargetDetails>");
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
            var SalesTargetHeaderRecord = new SalesTargetHeader();
            var RequestData = (SelectByIDSalesTargetRequest)RequestObj;
            var ResponseData = new SelectByIDSalesTargetResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                string sSql = "select * from salestargetheader where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalesTargetHeaderTypes = new SalesTargetHeader();

                        objSalesTargetHeaderTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalesTargetHeaderTypes.Year = objReader["Year"].ToString();
                        objSalesTargetHeaderTypes.Brand = objReader["Brand"].ToString();
                        objSalesTargetHeaderTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSalesTargetHeaderTypes.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        objSalesTargetHeaderTypes.StoreIDs = objReader["StoreIDs"].ToString();
                        objSalesTargetHeaderTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSalesTargetHeaderTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSalesTargetHeaderTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSalesTargetHeaderTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSalesTargetHeaderTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                      

                        objSalesTargetHeaderTypes.SalestargetDetails = new List<SalestargetDetails>();

                        SelectSalesTargetDetailsRequest objSelectSalesTargetDetailsRequest = new SelectSalesTargetDetailsRequest();
                        SelectSalesTargetDetailsResponse objSelectSalesTargetDetailsResponse = new SelectSalesTargetDetailsResponse();
                        objSelectSalesTargetDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectSalesTargetDetailsResponse = SelectSalesTargetDetails(objSelectSalesTargetDetailsRequest);
                        if (objSelectSalesTargetDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSalesTargetHeaderTypes.SalestargetDetails = objSelectSalesTargetDetailsResponse.SalestargetDetailsRecord;
                        }

                        ResponseData.SalesTargetHeaderData = objSalesTargetHeaderTypes;
                        ResponseData.ResponseDynamicData = objSalesTargetHeaderTypes;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Target");
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
            var SalesTargetHeaderList = new List<SalesTargetHeader>();
            var RequestData = (SelectAllSalesTargetRequest)RequestObj;
            var ResponseData = new SelectAllSalesTargetResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = ("select * from salestargetheader ");
              


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();


                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var ObjDocumentNumberingMaster = new SalesTargetHeader();

                        ObjDocumentNumberingMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        ObjDocumentNumberingMaster.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                        ObjDocumentNumberingMaster.Brand = Convert.ToString(objReader["Brand"]) ;
                        ObjDocumentNumberingMaster.StoreIDs = Convert.ToString(objReader["StoreIDs"]);
                        ObjDocumentNumberingMaster.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        ObjDocumentNumberingMaster.Year = Convert.ToString(objReader["Year"]);
                        ObjDocumentNumberingMaster.DocumentTypeID = objReader["DocumentTypeID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentTypeID"]) : 0;                                        
                        ObjDocumentNumberingMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        ObjDocumentNumberingMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        ObjDocumentNumberingMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        ObjDocumentNumberingMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;                      
                        SalesTargetHeaderList.Add(ObjDocumentNumberingMaster);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalesTargetHeaderList = SalesTargetHeaderList;
                    ResponseData.ResponseDynamicData = SalesTargetHeaderList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Target");
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

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectSalesTargetDetailsResponse SelectSalesTargetDetails(SelectSalesTargetDetailsRequest ObjRequest)
        {
            var SalestargetDetailsList = new List<SalestargetDetails>();
            var RequestData = (SelectSalesTargetDetailsRequest)ObjRequest;
            var ResponseData = new SelectSalesTargetDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select sd.* from salestargetheader SH join salestargetDetails SD on sh.id=sd.headerID where sd.headerID=" + RequestData.ID + " ");              
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSalestargetDetails = new SalestargetDetails();
                        objSalestargetDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSalestargetDetails.Qty = objReader["Qty"] != DBNull.Value ? Convert.ToInt32(objReader["Qty"]) : 0;
                        objSalestargetDetails.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToDecimal(objReader["Amount"]) : 0;
                        objSalestargetDetails.Month = Convert.ToString(objReader["Month"]);
                        SalestargetDetailsList.Add(objSalestargetDetails);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SalestargetDetailsRecord = SalestargetDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Sales Target");
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
