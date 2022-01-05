using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizRequest.Masters.PriceListRequest;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Masters.PriceListResponse;
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
    public class PriceListDAL : BasePriceListDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;
        public override SelectPriceListLookUPResponse SelectPriceListLookUPResponse(SelectPriceListLookUPRequest ObjRequest)
        {
            var PriceListTypeList = new List<PriceListType>();
            var RequestData = (SelectPriceListLookUPRequest)ObjRequest;
            var ResponseData = new SelectPriceListLookUPResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.Type.Trim().ToUpper() == "WNPROMOTION")
                {
                    sQuery ="Select plm.ID ,plm.PriceListName,plm.PriceListCode,co.ID as CountryID,co.CountryCode from PriceListMaster plm ";
                    sQuery = sQuery + "join CurrencyMaster cm on plm.PriceListCurrencyType=cm.ID ";
                    sQuery = sQuery + "join CountryMaster co on cm.ID=co.CurrencyID where plm.PriceCategory ='Sales'";
                }
                else if (RequestData.Type == "All")
                {
                    //sQuery = "select distinct sp.PriceListID as ID ,plm.PriceListName,sp.Price from StylePricing SP inner join PriceListMaster PLM on sp.PriceListID=plm.ID where plm.PriceCategory='sales' group by plm.PriceListName,sp.PriceListID,sp.Price,sp.ID";
                    sQuery = "select distinct sp.PriceListID as ID ,plm.PriceListName,plm.PriceListCode from StylePricing SP inner join PriceListMaster PLM on sp.PriceListID=plm.ID where plm.PriceCategory='sales' group by plm.PriceListName,plm.PriceListCode,sp.PriceListID,sp.Price,sp.ID";
                }
                else if (RequestData.Type == "Type")
                {
                    sQuery = "Select ID,PriceListName,PriceListCode from PriceListMaster with(NoLock) where Active='True'";
                }
                else
                {
                    sQuery = "Select ID,PriceListName,PriceListCode from PriceListMaster with(NoLock) where Active='True' and PriceCategory='" + RequestData.Type + "'";                   
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceListType = new PriceListType();
                        objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceListType.PriceListName = Convert.ToString(objReader["PriceListName"]);
                        objPriceListType.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                        
                        if (RequestData.Type.Trim().ToUpper() == "WNPROMOTION")
                        {
                            objPriceListType.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objPriceListType.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        }

                        PriceListTypeList.Add(objPriceListType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceListTypeData = PriceListTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");
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
            var RequestData = (SavePriceListRequest)RequestObj;
            var ResponseData = new SavePriceListResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertPriceListMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@PriceListCode", RequestData.PriceListTypeRecords.PriceListCode);
                _CommandObj.Parameters.AddWithValue("@PriceListName", RequestData.PriceListTypeRecords.PriceListName);
                _CommandObj.Parameters.AddWithValue("@PriceListCurrencyType", RequestData.PriceListTypeRecords.PriceListCurrencyType);
                _CommandObj.Parameters.AddWithValue("@BasePriceListID", RequestData.PriceListTypeRecords.BasePriceListID);
                _CommandObj.Parameters.AddWithValue("@ConversionFactore", RequestData.PriceListTypeRecords.ConversionFactore);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PriceListTypeRecords.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PriceListTypeRecords.Active);
                _CommandObj.Parameters.AddWithValue("@PriceCategory", RequestData.PriceListTypeRecords.PriceCategory);
                _CommandObj.Parameters.AddWithValue("@PriceType", RequestData.PriceListTypeRecords.PriceType);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.PriceListTypeRecords.CreateBy);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;


                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Price List");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price List");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price List");
                }


            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdatePriceListRequest)RequestObj;
            var ResponseData = new UpdatePriceListResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("API_UpdatePriceListMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.PriceListTypeRecords.ID);
                _CommandObj.Parameters.AddWithValue("@PriceListCode", RequestData.PriceListTypeRecords.PriceListCode);
                _CommandObj.Parameters.AddWithValue("@PriceListName", RequestData.PriceListTypeRecords.PriceListName);
                _CommandObj.Parameters.AddWithValue("@PriceListCurrencyType", RequestData.PriceListTypeRecords.PriceListCurrencyType);
                _CommandObj.Parameters.AddWithValue("@BasePriceListID", RequestData.PriceListTypeRecords.BasePriceListID);
                _CommandObj.Parameters.AddWithValue("@ConversionFactore", RequestData.PriceListTypeRecords.ConversionFactore);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.PriceListTypeRecords.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.PriceListTypeRecords.Active);
                _CommandObj.Parameters.AddWithValue("@PriceCategory", RequestData.PriceListTypeRecords.PriceCategory);
                _CommandObj.Parameters.AddWithValue("@PriceType", RequestData.PriceListTypeRecords.PriceType);
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.PriceListTypeRecords.CreateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.PriceListTypeRecords.SCN);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Price List");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Price List");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Price List");
                }


            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var BrandRecord = new BrandMaster();
            var RequestData = (DeletePriceListRequest)RequestObj;
            var ResponseData = new DeletePriceListResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from PriceListMaster  where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Price List Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Price List Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {

            var RequestData = (SelectByIDPriceListRequest)RequestObj;
            var ResponseData = new SelectByIDPriceListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from PriceListMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceListType = new PriceListType();
                        objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceListType.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                        objPriceListType.PriceListName = Convert.ToString(objReader["PriceListName"]);
                        objPriceListType.PriceListCurrencyType = objReader["PriceListCurrencyType"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrencyType"]) : 0;
                        objPriceListType.ConversionFactore = objReader["ConversionFactore"] != DBNull.Value ? Convert.ToDecimal(objReader["ConversionFactore"]) : 0;
                        objPriceListType.BasePriceListID = objReader["BasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["BasePriceListID"]) : 0;
                        objPriceListType.Remarks = Convert.ToString(objReader["Remarks"]);
                        objPriceListType.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceListType.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceListType.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceListType.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceListType.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPriceListType.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceListType.PriceCategory = Convert.ToString(objReader["PriceCategory"]);
                        objPriceListType.PriceType = Convert.ToString(objReader["PriceType"]);
                        ResponseData.PriceListTypeRecord = objPriceListType;
                        ResponseData.ResponseDynamicData = objPriceListType;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");

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
            var PriceListTypeList = new List<PriceListType>();
            var RequestData = (SelectAllPriceListRequest)RequestObj;
            var ResponseData = new SelectAllPriceListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select PM2.*,PM1.PriceListName as basecurrency1,CM.CurrencyName from PriceListMaster PM1 INNER JOIN PriceListMaster PM2  ON PM2.BasePriceListID = PM1.ID INNER join CurrencyMaster CM ON PM2.PriceListCurrencyType= CM.ID";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceListType = new PriceListType();
                        objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceListType.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                        objPriceListType.PriceListName = Convert.ToString(objReader["PriceListName"]);
                        objPriceListType.PriceListCurrencyType = objReader["PriceListCurrencyType"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrencyType"]) : 0;
                        objPriceListType.ConversionFactore = objReader["ConversionFactore"] != DBNull.Value ? Convert.ToDecimal(objReader["ConversionFactore"]) : 0;
                        objPriceListType.BasePriceListID = objReader["BasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["BasePriceListID"]) : 0;
                        objPriceListType.Remarks = Convert.ToString(objReader["Remarks"]);
                        objPriceListType.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceListType.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceListType.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceListType.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceListType.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPriceListType.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceListType.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        objPriceListType.BaseCurrency1 = Convert.ToString(objReader["basecurrency1"]);
                        PriceListTypeList.Add(objPriceListType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceListTypeList = PriceListTypeList;
                    ResponseData.ResponseDynamicData = PriceListTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");
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
            var PriceListTypeList = new List<PriceListType>();
            var RequestData = (SelectByIDsPriceListRequest)RequestObj;
            var ResponseData = new SelectByIDsPriceListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select PM2.*,PM1.PriceListName as basecurrency1,CM.CurrencyName from PriceListMaster PM1 INNER JOIN PriceListMaster PM2  ON PM2.BasePriceListID = PM1.ID INNER join CurrencyMaster CM ON PM2.PriceListCurrencyType= CM.ID";
                sSql = sSql + " where PM1.ID in({0})";
                sSql = string.Format(sSql, RequestData.PriceListIDS);

                if (RequestData.ShowInActiveRecords == false)
                {


                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceListType = new PriceListType();
                        objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceListType.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                        objPriceListType.PriceListName = Convert.ToString(objReader["PriceListName"]);
                        objPriceListType.PriceListCurrencyType = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrencyType"]) : 0;
                        objPriceListType.ConversionFactore = objReader["ConversionFactore"] != DBNull.Value ? Convert.ToDecimal(objReader["ConversionFactore"]) : 0;
                        objPriceListType.BasePriceListID = objReader["BasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["BasePriceListID"]) : 0;
                        objPriceListType.Remarks = Convert.ToString(objReader["Remarks"]);
                        objPriceListType.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceListType.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceListType.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceListType.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceListType.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPriceListType.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceListType.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        objPriceListType.BaseCurrency1 = Convert.ToString(objReader["basecurrency1"]);
                        PriceListTypeList.Add(objPriceListType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceList = PriceListTypeList;
                    ResponseData.ResponseDynamicData = PriceListTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");
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

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override SelectSalePriceListLookupResponse SelectSalePriceListLookUP(EasyBizRequest.Masters.StyleMasterRequest.SelectSalePriceListLookupRequest ObjRequest)
        {
            var PriceListTypeList = new StylePricing();
            var RequestData = (SelectSalePriceListLookupRequest)ObjRequest;
            var ResponseData = new SelectSalePriceListLookupResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                StringBuilder sQuery = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //sQuery = "Select distinct  PriceListID,Price from StylePricing where PriceListID='" + RequestData.SalePriceListID + "'";

                if (RequestData.SalePriceListID > 0 && RequestData.stylecode != string.Empty)
                {
                    //sQuery.Append("select distinct sp.PriceListID, sp.PriceListCurrency,plm.PriceListName,sp.Price,cm.ID as CountryID,plm.PriceCategory,plm.PriceType,sm.StyleCode from StylePricing sp,SKUMaster sm,PriceListMaster plm,countrymaster cm  where plm.ID=sp.PriceListID and sp.SKUCode=sm.SKUCode and cm.CurrencyID=plm.PriceListCurrencyType and PriceListID='" + RequestData.SalePriceListID + "'and sm.StyleCode='" + RequestData.stylecode + "'");
                    sQuery.Append("select distinct sp.ID, sp.PriceListID,sp.skucode, sp.PriceListCurrency,plm.PriceListName,sp.Price,plm.PriceCategory,plm.PriceType,sm.StyleCode,cm.ID as CountryID from StylePricing sp,SKUMaster sm,PriceListMaster plm,countrymaster cm  where plm.ID=sp.PriceListID and sp.SKUCode=sm.SKUCode and PriceListID='" + RequestData.SalePriceListID + "'and sm.StyleCode='" + RequestData.stylecode + "' order by sp.ID");
                }
                else if (RequestData.SalePriceListID == 0 && RequestData.stylecode == string.Empty)
                {
                    //sQuery = "select distinct sp.PriceListID, sp.PriceListCurrency,plm.PriceListName,sp.Price,cm.ID as CountryID,plm.PriceCategory,plm.PriceType from StylePricing sp,SKUMaster sm,PriceListMaster plm,countrymaster cm  where plm.ID=sp.PriceListID and sp.SKUCode=sm.SKUCode and cm.CurrencyID=plm.PriceListCurrencyType";
                    sQuery.Append("select distinct sp.PriceListID,sp.skucode, sp.PriceListCurrency,plm.PriceListName,sp.Price,cm.ID as CountryID,plm.PriceCategory,plm.PriceType,sku.StyleCode ");
                    sQuery.Append("from StylePricing sp join SKUMaster sku on sp.SKUCode=sku.SKUCode join PriceListMaster plm on plm.ID=sp.PriceListID ");
                    sQuery.Append("join countrymaster cm on cm.CurrencyID=plm.PriceListCurrencyType ");

                    if(RequestData.Type.Trim() != string.Empty)
                    {
                        sQuery.Append("and plm.PriceCategory='" + RequestData.Type.Trim() + "'");
                    }
                }
                else
                {
                    sQuery.Append("select * from StylePricing ");
                }
                _CommandObj = new SqlCommand(sQuery.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                var StylePricingList = new List<StylePricing>();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStylePricing = new StylePricing();
                        //objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStylePricing.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objStylePricing.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objStylePricing.PriceListCurrency = objReader["PriceListCurrency"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrency"]) : 0;
                        objStylePricing.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;

                        if (RequestData.SalePriceListID > 0 && RequestData.stylecode != string.Empty)
                        {
                            objStylePricing.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objStylePricing.PriceCategory = objReader["PriceCategory"] != DBNull.Value ? Convert.ToString(objReader["PriceCategory"]) : string.Empty;
                            objStylePricing.PriceType = objReader["PriceType"] != DBNull.Value ? Convert.ToString(objReader["PriceType"]) : string.Empty;
                            objStylePricing.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                            objStylePricing.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        }
                        else if (RequestData.SalePriceListID == 0 && RequestData.stylecode == string.Empty)
                        {
                            objStylePricing.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objStylePricing.PriceCategory = objReader["PriceCategory"] != DBNull.Value ? Convert.ToString(objReader["PriceCategory"]) : string.Empty;
                            objStylePricing.PriceType = objReader["PriceType"] != DBNull.Value ? Convert.ToString(objReader["PriceType"]) : string.Empty;
                            objStylePricing.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : string.Empty;
                            objStylePricing.SKUCode = objReader["SKUCode"] != DBNull.Value ? Convert.ToString(objReader["SKUCode"]) : string.Empty;
                        }
                        StylePricingList.Add(objStylePricing);
                        ResponseData.SalePriceListTypeData = StylePricingList;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");
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

        public override SelectPriceListLookUPResponse API_SelectPriceListMasterLookUp(SelectPriceListLookUPRequest requestData)
        {
            var PriceListTypeList = new List<PriceListType>();
            var RequestData = (SelectPriceListLookUPRequest)requestData;
            var ResponseData = new SelectPriceListLookUPResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.Type.Trim().ToUpper() == "WNPROMOTION")
                {
                    sQuery = "Select plm.ID ,plm.PriceListName,plm.PriceListCode,co.ID as CountryID,co.CountryCode from PriceListMaster plm ";
                    sQuery = sQuery + "join CurrencyMaster cm on plm.PriceListCurrencyType=cm.ID ";
                    sQuery = sQuery + "join CountryMaster co on cm.ID=co.CurrencyID where plm.PriceCategory ='Sales'";
                }
                else if (RequestData.Type == "All")
                {
                    //sQuery = "select distinct sp.PriceListID as ID ,plm.PriceListName,sp.Price from StylePricing SP inner join PriceListMaster PLM on sp.PriceListID=plm.ID where plm.PriceCategory='sales' group by plm.PriceListName,sp.PriceListID,sp.Price,sp.ID";
                    sQuery = "select distinct sp.PriceListID as ID ,plm.PriceListName,plm.PriceListCode from StylePricing SP inner join PriceListMaster PLM on sp.PriceListID=plm.ID where plm.PriceCategory='sales' group by plm.PriceListName,plm.PriceListCode,sp.PriceListID,sp.Price,sp.ID";
                }
                else if (RequestData.Type == "Type")
                {
                    sQuery = "Select ID,PriceListName,PriceListCode from PriceListMaster with(NoLock) where Active='True'";
                }
                else
                {
                    sQuery = "Select ID,PriceListName,PriceListCode from PriceListMaster with(NoLock) where Active='True' and PriceCategory='" + RequestData.Type + "'";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceListType = new PriceListType();
                        objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceListType.PriceListName = Convert.ToString(objReader["PriceListName"]);
                        objPriceListType.PriceListCode = Convert.ToString(objReader["PriceListCode"]);

                        if (RequestData.Type.Trim().ToUpper() == "WNPROMOTION")
                        {
                            objPriceListType.CountryID = objReader["CountryID"] != DBNull.Value ? Convert.ToInt32(objReader["CountryID"]) : 0;
                            objPriceListType.CountryCode = objReader["CountryCode"] != DBNull.Value ? Convert.ToString(objReader["CountryCode"]) : string.Empty;
                        }

                        PriceListTypeList.Add(objPriceListType);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceListTypeData = PriceListTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");
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

    

    public override SelectAllPriceListResponse API_SelectALL(SelectAllPriceListRequest requestData)
        {
            var PriceListTypeList = new List<PriceListType>();
            var RequestData = (SelectAllPriceListRequest)requestData;
            var ResponseData = new SelectAllPriceListResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                var sSql = new StringBuilder();
                decimal myInt;
                bool isNumerical = decimal.TryParse(RequestData.SearchString, out myInt);

                if (isNumerical)
                {
                    sSql.Append("Select PM2.ID,PM2.PriceListCode,PM2.PriceListName,PM2.ConversionFactore,PM2.Remarks,PM2.Active,CM.CurrencyName,  RC.TOTAL_CNT [RecordCount] from PriceListMaster PM1 INNER JOIN PriceListMaster PM2  ON PM2.BasePriceListID = PM1.ID INNER join CurrencyMaster CM ON PM2.PriceListCurrencyType= CM.ID ");
                    sSql.Append("LEFT JOIN(Select  count(PM3.ID) As TOTAL_CNT From PriceListMaster PM3 with(NoLock) ");
                    sSql.Append("INNER JOIN PriceListMaster PM4  ON PM4.BasePriceListID = PM3.ID ");
                    sSql.Append("INNER join CurrencyMaster CM1 ON PM4.PriceListCurrencyType= CM1.ID ");
                    sSql.Append(" where PM4.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or PM4.PriceListCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM4.PriceListName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM4.ConversionFactore like isnull('%" + decimal.Parse(RequestData.SearchString) + "%','') ");
                    sSql.Append("or PM4.Remarks like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM1.CurrencyName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ");

                    sSql.Append(" where PM2.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or PM2.PriceListCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM2.PriceListName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM2.ConversionFactore like isnull('%" + decimal.Parse(RequestData.SearchString) + "%','') ");
                    sSql.Append("or PM2.Remarks like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencyName like isnull('%" + RequestData.SearchString + "%','')) ");
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }
                else
                {
                    sSql.Append("Select PM2.ID,PM2.PriceListCode,PM2.PriceListName,PM2.ConversionFactore,PM2.Remarks,PM2.Active,CM.CurrencyName, RC.TOTAL_CNT [RecordCount] from PriceListMaster PM1 INNER JOIN PriceListMaster PM2  ON PM2.BasePriceListID = PM1.ID INNER join CurrencyMaster CM ON PM2.PriceListCurrencyType= CM.ID ");
                    sSql.Append("LEFT JOIN(Select  count(PM3.ID) As TOTAL_CNT From PriceListMaster PM3 with(NoLock) ");
                    sSql.Append("INNER JOIN PriceListMaster PM4  ON PM4.BasePriceListID = PM3.ID ");
                    sSql.Append("INNER join CurrencyMaster CM1 ON PM4.PriceListCurrencyType= CM1.ID ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or PM4.PriceListCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM4.PriceListName like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM4.ConversionFactore like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM4.Remarks like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM1.CurrencyName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ");


                    sSql.Append(" where PM2.Active = " + RequestData.IsActive + " ");
                    sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                    sSql.Append("or PM2.PriceListCode like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM2.PriceListName like isnull('%" + RequestData.SearchString + "%','') ");
                    //sSql.Append("or PM2.ConversionFactore like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or PM2.Remarks like isnull('%" + RequestData.SearchString + "%','') ");
                    sSql.Append("or CM.CurrencyName like isnull('%" + RequestData.SearchString + "%','')) ");
                    sSql.Append("order by ID asc ");
                    sSql.Append("offset " + RequestData.Offset + " rows ");
                    sSql.Append("fetch first " + RequestData.Limit + " rows only");
                }
                //string sSql = "Select PM2.ID,PM2.PriceListCode,PM2.PriceListName,PM2.ConversionFactore,PM2.Remarks,PM2.Active,CM.CurrencyName,  RecordCount = COUNT(*) OVER() from PriceListMaster PM1 INNER JOIN PriceListMaster PM2  ON PM2.BasePriceListID = PM1.ID INNER join CurrencyMaster CM ON PM2.PriceListCurrencyType= CM.ID" +
                //   " where PM2.Active = " + RequestData.IsActive + " " +
                //       "and (isnull('" + RequestData.SearchString + "','') = '' " +
                //       "or PM2.PriceListCode = isnull('" + RequestData.SearchString + "','') " +
                //       "or PM2.PriceListName = isnull('" + RequestData.SearchString + "','') " +
                //       //"or PM2.ConversionFactore = isnull('" + RequestData.SearchString + "','') " +
                //       "or PM2.Remarks = isnull('" + RequestData.SearchString + "','') " +
                //       "or CM.CurrencyName = isnull('" + RequestData.SearchString + "','')) " +
                //       "order by ID asc " +
                //       "offset " + RequestData.Offset + " rows " +
                //       "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPriceListType = new PriceListType();
                        objPriceListType.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPriceListType.PriceListCode = Convert.ToString(objReader["PriceListCode"]);
                        objPriceListType.PriceListName = Convert.ToString(objReader["PriceListName"]);
                        //objPriceListType.PriceListCurrencyType = objReader["PriceListCurrencyType"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrencyType"]) : 0;
                        objPriceListType.ConversionFactore = objReader["ConversionFactore"] != DBNull.Value ? Convert.ToDecimal(objReader["ConversionFactore"]) : 0;
                        //objPriceListType.BasePriceListID = objReader["BasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["BasePriceListID"]) : 0;
                        objPriceListType.Remarks = Convert.ToString(objReader["Remarks"]);
                        /*objPriceListType.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPriceListType.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPriceListType.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPriceListType.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPriceListType.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;*/
                        objPriceListType.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPriceListType.CurrencyName = Convert.ToString(objReader["CurrencyName"]);
                        //objPriceListType.BaseCurrency1 = Convert.ToString(objReader["basecurrency1"]);
                        PriceListTypeList.Add(objPriceListType);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PriceListTypeList = PriceListTypeList;
                    ResponseData.ResponseDynamicData = PriceListTypeList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Price List Master");
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
