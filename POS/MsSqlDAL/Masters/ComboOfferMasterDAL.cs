using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.ComboOfferRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.ComboOfferResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;



namespace MsSqlDAL.Masters
{
    public class ComboOfferMasterDAL : BaseComboOfferMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;



        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveComboOfferRequest)RequestObj;
            var ResponseData = new SaveComboOfferResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("InsertOrUpdateComboOffer", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.ComboOfferRecord.ID;

                var DocumentDate = _CommandObj.Parameters.Add("@DocumentDate", SqlDbType.NVarChar);
                DocumentDate.Direction = ParameterDirection.Input;
                DocumentDate.Value = sqlCommon.GetSQLServerDateString(RequestData.ComboOfferRecord.DocumentDate);

                var ProductBarcode = _CommandObj.Parameters.Add("@ProductBarcode", SqlDbType.NVarChar);
                ProductBarcode.Direction = ParameterDirection.Input;
                ProductBarcode.Value = RequestData.ComboOfferRecord.ProductBarcode;

                var ProductSKUCode = _CommandObj.Parameters.Add("@ProductSKUCode", SqlDbType.NVarChar);
                ProductSKUCode.Direction = ParameterDirection.Input;
                ProductSKUCode.Value = RequestData.ComboOfferRecord.ProductSKUCode;

                var ProductStylecode = _CommandObj.Parameters.Add("@ProductStylecode", SqlDbType.NVarChar);
                ProductStylecode.Direction = ParameterDirection.Input;
                ProductStylecode.Value = RequestData.ComboOfferRecord.ProductStylecode;

                SqlParameter StockReturnDetails = _CommandObj.Parameters.Add("@StockReturnDetails", SqlDbType.Xml);
                StockReturnDetails.Direction = ParameterDirection.Input;
                StockReturnDetails.Value = ComboOfferDetailXML(RequestData.ComboOfferRecord.ComboOfferDetailsList);

                SqlParameter StylePricing = _CommandObj.Parameters.Add("@StylePricing", SqlDbType.Xml);
                StylePricing.Direction = ParameterDirection.Input;
                StylePricing.Value = StylePricingDetailXML(RequestData.PriceListTypes);

                SqlParameter ColorCode = _CommandObj.Parameters.Add("@ColorCode", SqlDbType.NVarChar);
                ColorCode.Direction = ParameterDirection.Input;
                ColorCode.Value = RequestData.CPOStyleDetailsRecords.ColorCode;

                SqlParameter ColorID = _CommandObj.Parameters.Add("@ColorID", SqlDbType.Int);
                ColorID.Direction = ParameterDirection.Input;
                ColorID.Value = RequestData.CPOStyleDetailsRecords.ColorID;

                SqlParameter StyleID = _CommandObj.Parameters.Add("@StyleID", SqlDbType.Int);
                StyleID.Direction = ParameterDirection.Input;
                StyleID.Value = RequestData.CPOStyleDetailsRecords.ID;

                SqlParameter SizeCode = _CommandObj.Parameters.Add("@SizeCode", SqlDbType.NVarChar);
                SizeCode.Direction = ParameterDirection.Input;
                SizeCode.Value = RequestData.CPOStyleDetailsRecords.SizeCode;

                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.ComboOfferRecord.CreateBy;

                var Active = _CommandObj.Parameters.Add("@Active", SqlDbType.NVarChar);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.ComboOfferRecord.Active;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Combo Offer");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Combo Offer");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Combo Offer");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Combo Offer");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;

        }

        public string ComboOfferDetailXML(List<ComboOfferDetails> ComboOfferDetails)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (ComboOfferDetails objComboOfferDetails in ComboOfferDetails)
            {
                sSql.Append("<ComboOfferDetailsData>");
                sSql.Append("<ID>" + objComboOfferDetails.ID + "</ID>");
                sSql.Append("<HeaderID>" + objComboOfferDetails.HeaderID + "</HeaderID>");
                sSql.Append("<Barcode>" + (objComboOfferDetails.Barcode) + "</Barcode>");
                sSql.Append("<SKUCode>" + (objComboOfferDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<SKUName>" + objComboOfferDetails.SKUName + "</SKUName>");
                sSql.Append("<Stylecode>" + objComboOfferDetails.Stylecode + "</Stylecode>");
                sSql.Append("</ComboOfferDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public string StylePricingDetailXML(List<PriceListType> priceListTypes)
        {
            StringBuilder sSql = new StringBuilder();
            var sqlCommon = new MsSqlCommon();
            foreach (PriceListType objComboOfferDetails in priceListTypes)
            {
                sSql.Append("<StylePricingDetailsData>");
                sSql.Append("<PriceListID>" + objComboOfferDetails.ID + "</PriceListID>");
                sSql.Append("<PriceListCurrency>" + objComboOfferDetails.PriceListCurrencyType + "</PriceListCurrency>");
                sSql.Append("<Price>" + (objComboOfferDetails.Price) + "</Price>");
                /*sSql.Append("<SKUCode>" + (objComboOfferDetails.SKUCode) + "</SKUCode>");
                sSql.Append("<SKUName>" + objComboOfferDetails.SKUName + "</SKUName>");
                sSql.Append("<Stylecode>" + objComboOfferDetails.Stylecode + "</Stylecode>");*/
                sSql.Append("</StylePricingDetailsData>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override SelectComboOfferLookUpResponse SelectComboOfferLookUp(SelectComboOfferLookUpRequest ObjRequest)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            var RequestData = (SelectByComboOfferIDRequest)RequestObj;
            var ResponseData = new SelectByComboOfferIDResponse();
            List<PriceListType> _objPriceType = new List<PriceListType>();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from ProductComboHeader with(NoLock) where ID='{0}' ";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objComboOffer = new ComboOfferMaster();
                        objComboOffer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objComboOffer.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        objComboOffer.ProductStylecode = Convert.ToString(objReader["ProductStylecode"]);

                        objComboOffer.ProductBarcode = Convert.ToString(objReader["ProductBarcode"]);
                        objComboOffer.ProductSKUCode = Convert.ToString(objReader["ProductSKUCode"]);

                        objComboOffer.CreateOn = objReader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreatedOn"]) : DateTime.Now;
                        objComboOffer.CreateBy = objReader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreatedBy"]) : 0;
                        objComboOffer.UpdateOn = objReader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdatedOn"]) : DateTime.Now;
                        objComboOffer.UpdateBy = objReader["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdatedBy"]) : 0; ;
                        objComboOffer.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objComboOffer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        objComboOffer.ComboOfferDetailsList = new List<ComboOfferDetails>();

                        SelectByComboOfferIDRequest objSelectByComboOfferDetailsRequest = new SelectByComboOfferIDRequest();
                        SelectByComboOfferIDResponse objSelectByComboOfferDetailsResponse = new SelectByComboOfferIDResponse();
                        objSelectByComboOfferDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;


                        objSelectByComboOfferDetailsResponse = SelectComboOfferDetailsList(objSelectByComboOfferDetailsRequest);
                        if (objSelectByComboOfferDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objComboOffer.ComboOfferDetailsList = objSelectByComboOfferDetailsResponse.ComboOfferRecord.ComboOfferDetailsList;

                        }

                        SelectByComboOfferIDRequest objSelectByStylePricingDetailsRequest = new SelectByComboOfferIDRequest();
                        SelectByComboOfferIDResponse objSelectByStylePricingDetailsResponse = new SelectByComboOfferIDResponse();
                        objSelectByStylePricingDetailsRequest.SKUcode = Convert.ToString(objReader["ProductSKUCode"]);


                        objSelectByStylePricingDetailsResponse = SelectStylePricingList(objSelectByStylePricingDetailsRequest);
                        if (objSelectByStylePricingDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            _objPriceType = objSelectByStylePricingDetailsResponse.PriceTypeList;// ComboOfferRecord.ComboOfferDetailsList;

                        }

                        ResponseData.ComboOfferRecord = objComboOffer;
                        ResponseData.PriceTypeList = _objPriceType;
                        //ResponseData.ResponseDynamicData = objComboOffer;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Combo Offer");
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
            var ComboOfferList = new List<ComboOfferMaster>();
            var RequestData = (SelectAllComboOfferRequest)RequestObj;
            var ResponseData = new SelectAllComboOfferResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select ID, DocumentDate, ProductBarcode, ProductSKUCode, Active, RecordCount = COUNT(*) OVER() from ProductComboHeader " +
                    "where Active = " + RequestData.IsActive + " " +
                        "and (isnull('" + RequestData.SearchString + "','') = '' " +
                            "or ProductBarcode like isnull('%" + RequestData.SearchString + "%','') " +
                            "or ProductSKUCode like isnull('%" + RequestData.SearchString + "%','')) " +
                    "order by ID asc " +
                    "offset " + RequestData.Offset + " rows " +
                    "fetch first " + RequestData.Limit + " rows only";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objComboOffer = new ComboOfferMaster();
                        objComboOffer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objComboOffer.DocumentDate = objReader["DocumentDate"] != DBNull.Value ? Convert.ToDateTime(objReader["DocumentDate"]) : DateTime.Now;
                        //objComboOffer.ProductStylecode = Convert.ToString(objReader["ProductStylecode"]);
                        objComboOffer.ProductBarcode = Convert.ToString(objReader["ProductBarcode"]);
                        objComboOffer.ProductSKUCode = Convert.ToString(objReader["ProductSKUCode"]);

                        objComboOffer.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ComboOfferList.Add(objComboOffer);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ComboOfferList = ComboOfferList;
                    //ResponseData.ResponseDynamicData = ComboOfferList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Combo Offer");
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

        public override SelectCPOStyleDetailsResponse SelectCPOStyleDetailsRecord(SelectAllComboOfferRequest ObjRequest)
        {
            var RequestData = (SelectAllComboOfferRequest)ObjRequest;
            var ResponseData = new SelectCPOStyleDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "select count(SM.ID) as 'RowCount',SM.ID,SM.StyleCode,SCD.ColorID,SCD.ColorCode,SSD.ScaleID "
                               + ", SSD.SizeID,SSD.SizeCode from stylemaster SM with(nolock) "
                               + "Left Join StyleWithColorDetails as SCD with(nolock) on SM.ID = SCD.StlyeID "
                               + " Left Join StyleWithScaleDetails as SSD with(nolock) on SM.ID = SSD.StyleID where Stylename like 'Product combo offer' group by SM.ID, "
                               + "SM.StyleCode,SCD.ColorID,SCD.ColorCode,SSD.ScaleID,SSD.SizeID,SSD.SizeCode";

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                int tempRowCount = 1;
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objComboOffer = new CPOStyleDetails();
                        tempRowCount++;
                        objComboOffer.RowCount = tempRowCount;//objReader["RowCount"] != DBNull.Value ? Convert.ToInt32(objReader["RowCount"]) : 0;
                        objComboOffer.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objComboOffer.ProductstyleCode = Convert.ToString(objReader["StyleCode"]);
                        objComboOffer.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;

                        objComboOffer.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objComboOffer.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;

                        objComboOffer.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"]) : 0;
                        objComboOffer.SizeCode = Convert.ToString(objReader["SizeCode"]);

                        ResponseData.CPOStyleDetailsRecord = objComboOffer;
                        ResponseData.ResponseDynamicData = objComboOffer;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Combo Offer");
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

        public override SelectByComboOfferIDResponse SelectStylePricingList(SelectByComboOfferIDRequest ObjRequest)
        {
            var objComboOfferDetailsList = new List<PriceListType>();
            var RequestData = (SelectByComboOfferIDRequest)ObjRequest;
            var ResponseData = new SelectByComboOfferIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from StylePricing with(nolock) where SKUcode='" + RequestData.SKUcode + "' and Active='True' order By ID ");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objComboOfferDetails = new PriceListType();
                        objComboOfferDetails.ID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objComboOfferDetails.PriceListCurrencyType = objReader["PriceListCurrency"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrency"]) : 0;
                        objComboOfferDetails.Price = Convert.ToDecimal(objReader["Price"]);
                        //objComboOfferDetails.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        //objComboOfferDetails.SKUName = Convert.ToString(objReader["SKUName"]);
                        ////objComboOfferDetails.Stylecode = Convert.ToString(objReader["StyleCode"]);*/

                        objComboOfferDetailsList.Add(objComboOfferDetails);


                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ComboOfferRecord = new ComboOfferMaster();
                    ResponseData.PriceTypeList = objComboOfferDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Combo Offer");
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

        public override SelectByComboOfferIDResponse SelectComboOfferDetailsList(SelectByComboOfferIDRequest ObjRequest)
        {
            var objComboOfferDetailsList = new List<ComboOfferDetails>();
            var RequestData = (SelectByComboOfferIDRequest)ObjRequest;
            var ResponseData = new SelectByComboOfferIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from ProductComboDetails with(nolock) where HeaderID=" + RequestData.ID + " order By ID ");
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objComboOfferDetails = new ComboOfferDetails();
                        objComboOfferDetails.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objComboOfferDetails.HeaderID = objReader["HeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["HeaderID"]) : 0;
                        objComboOfferDetails.Barcode = Convert.ToString(objReader["Barcode"]);
                        objComboOfferDetails.SKUCode = Convert.ToString(objReader["SKUCode"]);
                        objComboOfferDetails.SKUName = Convert.ToString(objReader["SKUName"]);
                        objComboOfferDetails.Stylecode = Convert.ToString(objReader["StyleCode"]);

                        objComboOfferDetailsList.Add(objComboOfferDetails);


                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ComboOfferRecord = new ComboOfferMaster();
                    ResponseData.ComboOfferRecord.ComboOfferDetailsList = objComboOfferDetailsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Combo Offer");
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




