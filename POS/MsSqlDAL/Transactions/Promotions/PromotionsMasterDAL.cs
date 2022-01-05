using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionCriteria;
using EasyBizRequest.Transactions.Promotions.PromotionsMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Transactions.Promotions.PromotionCriteriaResponse;
using EasyBizResponse.Transactions.Promotions.PromotionsMasterRespopnse;
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
    public class PromotionsMasterDAL : BasePromotionsMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SavePromotionsRequest)RequestObj;
            var ResponseData = new SavePromotionsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdatePromotionsMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.PromotionsRecord.ID;

                SqlParameter PromotionCode = _CommandObj.Parameters.Add("@PromotionCode", SqlDbType.NVarChar);
                PromotionCode.Direction = ParameterDirection.Input;
                PromotionCode.Value = RequestData.PromotionsRecord.PromotionCode;

                SqlParameter PromotionName = _CommandObj.Parameters.Add("@PromotionName", SqlDbType.NVarChar);
                PromotionName.Direction = ParameterDirection.Input;
                PromotionName.Value = RequestData.PromotionsRecord.PromotionName;

                SqlParameter PromotionType = _CommandObj.Parameters.Add("@PromotionType", SqlDbType.VarChar);
                PromotionType.Direction = ParameterDirection.Input;
                PromotionType.Value = RequestData.PromotionsRecord.PromotionType;

                SqlParameter Type = _CommandObj.Parameters.Add("@Type", SqlDbType.NVarChar);
                Type.Direction = ParameterDirection.Input;
                Type.Value = RequestData.PromotionsRecord.Type;

                SqlParameter StartDate = _CommandObj.Parameters.Add("@StartDate", SqlDbType.DateTime);
                StartDate.Direction = ParameterDirection.Input;
                StartDate.Value = RequestData.PromotionsRecord.StartDate;

                SqlParameter EndDate = _CommandObj.Parameters.Add("@EndDate", SqlDbType.DateTime);
                EndDate.Direction = ParameterDirection.Input;
                EndDate.Value = RequestData.PromotionsRecord.EndDate;

                SqlParameter MinBillAmount = _CommandObj.Parameters.Add("@MinBillAmount", SqlDbType.Decimal);
                MinBillAmount.Direction = ParameterDirection.Input;
                MinBillAmount.Value = RequestData.PromotionsRecord.MinBillAmount;

                SqlParameter MinQuantity = _CommandObj.Parameters.Add("@MinQuantity", SqlDbType.Int);
                MinQuantity.Direction = ParameterDirection.Input;
                MinQuantity.Value = RequestData.PromotionsRecord.MinQuantity;

                SqlParameter PriceListID = _CommandObj.Parameters.Add("@PriceListID", SqlDbType.Int);
                PriceListID.Direction = ParameterDirection.Input;
                PriceListID.Value = RequestData.PromotionsRecord.PriceListID;

                SqlParameter Discount = _CommandObj.Parameters.Add("@Discount", SqlDbType.NVarChar);
                Discount.Direction = ParameterDirection.Input;
                Discount.Value = RequestData.PromotionsRecord.Discount;

                SqlParameter DiscountValue = _CommandObj.Parameters.Add("@DiscountValue", SqlDbType.Decimal);
                DiscountValue.Direction = ParameterDirection.Input;
                DiscountValue.Value = RequestData.PromotionsRecord.DiscountValue;

                SqlParameter AllowMultiPromotion = _CommandObj.Parameters.Add("@AllowMultiPromotion", SqlDbType.Bit);
                AllowMultiPromotion.Direction = ParameterDirection.Input;
                AllowMultiPromotion.Value = RequestData.PromotionsRecord.AllowMultiPromotion;

                SqlParameter LowestValue = _CommandObj.Parameters.Add("@LowestValue", SqlDbType.Bit);
                LowestValue.Direction = ParameterDirection.Input;
                LowestValue.Value = RequestData.PromotionsRecord.LowestValue;

                SqlParameter MinPromotionQty = _CommandObj.Parameters.Add("@MinPromotionQty", SqlDbType.Int);
                MinPromotionQty.Direction = ParameterDirection.Input;
                MinPromotionQty.Value = RequestData.PromotionsRecord.MinPromotionQty;

                SqlParameter MaxGiftPerInvoice = _CommandObj.Parameters.Add("@MaxGiftPerInvoice", SqlDbType.Int);
                MaxGiftPerInvoice.Direction = ParameterDirection.Input;
                MaxGiftPerInvoice.Value = RequestData.PromotionsRecord.MaxGiftPerInvoice;

                SqlParameter GiftQuantity = _CommandObj.Parameters.Add("@GiftQuantity", SqlDbType.Int);
                GiftQuantity.Direction = ParameterDirection.Input;
                GiftQuantity.Value = RequestData.PromotionsRecord.GiftQuantity;

                SqlParameter GiftBillAmount = _CommandObj.Parameters.Add("@GiftBillAmount", SqlDbType.Decimal);
                GiftBillAmount.Direction = ParameterDirection.Input;
                GiftBillAmount.Value = RequestData.PromotionsRecord.GiftBillAmount;

                SqlParameter MultiApplyForReceipt = _CommandObj.Parameters.Add("@MultiApplyForReceipt", SqlDbType.Bit);
                MultiApplyForReceipt.Direction = ParameterDirection.Input;
                MultiApplyForReceipt.Value = RequestData.PromotionsRecord.MultiApplyForReceipt;

                SqlParameter LowestValueWithGroup = _CommandObj.Parameters.Add("@LowestValueWithGroup", SqlDbType.Bit);
                LowestValueWithGroup.Direction = ParameterDirection.Input;
                LowestValueWithGroup.Value = RequestData.PromotionsRecord.LowestValueWithGroup;                

                SqlParameter ExculdeDiscountItems = _CommandObj.Parameters.Add("@ExculdeDiscountItems", SqlDbType.Bit);
                ExculdeDiscountItems.Direction = ParameterDirection.Input;
                ExculdeDiscountItems.Value = RequestData.PromotionsRecord.ExculdeDiscountItems;

                SqlParameter Prompt = _CommandObj.Parameters.Add("@Prompt", SqlDbType.Bit);
                Prompt.Direction = ParameterDirection.Input;
                Prompt.Value = RequestData.PromotionsRecord.Prompt;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.PromotionsRecord.Active;

                SqlParameter AppliedType = _CommandObj.Parameters.Add("@AppliedType", SqlDbType.VarChar);
                AppliedType.Direction = ParameterDirection.Input;
                AppliedType.Value = RequestData.PromotionsRecord.AppliedType;

                SqlParameter Colors = _CommandObj.Parameters.Add("@Colors", SqlDbType.NVarChar);
                Colors.Direction = ParameterDirection.Input;
                Colors.Value = RequestData.PromotionsRecord.Color;

                SqlParameter BuyOptionalCount = _CommandObj.Parameters.Add("@BuyOptionalCount", SqlDbType.Decimal);
                BuyOptionalCount.Direction = ParameterDirection.Input;
                BuyOptionalCount.Value = RequestData.PromotionsRecord.BuyOptionalCount;

                SqlParameter GetOptionalCount = _CommandObj.Parameters.Add("@GetOptionalCount", SqlDbType.Decimal);
                GetOptionalCount.Direction = ParameterDirection.Input;
                GetOptionalCount.Value = RequestData.PromotionsRecord.GetOptionalCount;

                SqlParameter GetItematFixedPrice = _CommandObj.Parameters.Add("@GetItematFixedPrice", SqlDbType.Decimal);
                GetItematFixedPrice.Direction = ParameterDirection.Input;
                GetItematFixedPrice.Value = RequestData.PromotionsRecord.GetItematFixedPrice;

                SqlParameter BuyItemOptionalAmount = _CommandObj.Parameters.Add("@BuyItemOptionalAmount", SqlDbType.Decimal);
                BuyItemOptionalAmount.Direction = ParameterDirection.Input;
                BuyItemOptionalAmount.Value = RequestData.PromotionsRecord.BuyItemOptionalAmount;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.PromotionsRecord.CreateBy;

                var StoreTypeList = _CommandObj.Parameters.Add("@StoreTypeList", SqlDbType.Xml);
                StoreTypeList.Direction = ParameterDirection.Input;
                StoreTypeList.Value = StoreTypeListXML(RequestData.StoreTypeList);

                var CustomerDataDetails = _CommandObj.Parameters.Add("@CustomerTypeList", SqlDbType.Xml);
                CustomerDataDetails.Direction = ParameterDirection.Input;
                CustomerDataDetails.Value = CustomerTypeListXML(RequestData.CustomerTypeList);

                var TotalDataDetails = _CommandObj.Parameters.Add("@ProductTypeList", SqlDbType.Xml);
                TotalDataDetails.Direction = ParameterDirection.Input;
                TotalDataDetails.Value = ProductTypeListXML(RequestData.ProductTypeList);

                var BuyItemTypeDetails = _CommandObj.Parameters.Add("@BuyItemTypeList", SqlDbType.Xml);
                BuyItemTypeDetails.Direction = ParameterDirection.Input;
                BuyItemTypeDetails.Value = BuyItemTypeXML(RequestData.BuyItemTypeList);

                var GetItemTypeDetails = _CommandObj.Parameters.Add("@GetItemTypeList", SqlDbType.Xml);
                GetItemTypeDetails.Direction = ParameterDirection.Input;
                GetItemTypeDetails.Value = GetItemTypeXML(RequestData.GetItemTypeList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    if (RequestData.PromotionsRecord.ID==0)
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Promotions Master");
                    }
                    else
                    {
                        ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Promotions Master");
                    }
                    
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Promotions Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotions Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Promotions Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var PromotionsRecord = new PromotionsMaster();
            var RequestData = (DeletePromotionsRequest)RequestObj;
            var ResponseData = new DeletePromotionsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from PromotionWithGetItemDetails where PromotionHeaderID={0} ; Delete from PromotionWithBuyItem where PromotionHeaderID={0}; Delete from PromotionsWithStoreDetails where PromotionHeaderID={0};  Delete from PromotionsWithProducts where PromotionHeaderID={0};  Delete from PromotionsWithCustomerDetails where PromotionHeaderID={0}; Delete from PromotionsMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Promotions Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Promotions Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var PromotionsRecord = new PromotionsMaster();
            var RequestData = (SelectByPromotionsIDRequest)RequestObj;
            var ResponseData = new SelectByPromotionsIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Select * from PromotionsMaster with(NoLock) where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        PromotionsMaster objPromotions = new PromotionsMaster();
                        objPromotions.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotions.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        objPromotions.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotions.PromotionType = Convert.ToString(objReader["PromotionType"]);
                        //objPromotions.Active = Convert.ToBoolean(objReader["Active"]);
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;

                        objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["BuyOptionalCount"]) : 0;

                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;

                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                        objPromotions.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objPromotions.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objPromotions.ExculdeDiscountItems = objReader["ExculdeDiscountItems"] != DBNull.Value ? Convert.ToBoolean(objReader["ExculdeDiscountItems"]) : true;
                        objPromotions.GetItematFixedPrice = objReader["GetItematFixedPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["GetItematFixedPrice"]) : 0;
                        objPromotions.GetOptionalCount = objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["GetOptionalCount"]) : 0;
                        objPromotions.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : true;
                        objPromotions.LowestValueWithGroup = objReader["LowestValueWithGroup"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValueWithGroup"]) : true;
                        objPromotions.MinBillAmount = objReader["MinBillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MinBillAmount"]) : 0;
                        objPromotions.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        objPromotions.Prompt = objReader["Prompt"] != DBNull.Value ? Convert.ToBoolean(objReader["Prompt"]) : true;
                        objPromotions.Type = Convert.ToString(objReader["Type"]);
                        objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPromotions.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;
                        objPromotions.MinPromotionQty = objReader["MinPromotionQty"] != DBNull.Value ? Convert.ToInt32(objReader["MinPromotionQty"]) : 0;

                        objPromotions.MaxGiftPerInvoice = objReader["MaxGiftPerInvoice"] != DBNull.Value ? Convert.ToInt32(objReader["MaxGiftPerInvoice"]) : 0;
                        objPromotions.GiftQuantity = objReader["GiftQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GiftQuantity"]) : 0;
                        objPromotions.GiftBillAmount = objReader["GiftBillAmount"] != DBNull.Value ? Convert.ToInt32(objReader["GiftBillAmount"]) : 0;
                        objPromotions.MultiApplyForReceipt = objReader["MultiApplyForReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["MultiApplyForReceipt"]) : true;
                        objPromotions.BuyItemOptionalAmount = objReader["BuyItemOptionalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BuyItemOptionalAmount"]) : 0;

                        SelectByPromotionIDStoreDetailsRequest objRequest = new SelectByPromotionIDStoreDetailsRequest();
                        SelectByPromotionIDStoreDetailsResponse objResponse = new SelectByPromotionIDStoreDetailsResponse();
                        objRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRequest.DetailsType = Enums.PromotionRecordType.Store;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.StoreList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Customer;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.CustomerList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Category;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.ProductTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.BuyItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.BuyItemTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.GetItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.GetItemTypeList = objResponse.DetailsRecord;
                        }

                        ResponseData.PromotionsRecord = objPromotions;
                        ResponseData.ResponseDynamicData = objPromotions;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
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
            var PromotionsList = new List<PromotionsMaster>();
            var RequestData = (SelectAllPromotionsRequest)RequestObj;
            var ResponseData = new SelectAllPromotionsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "select distinct pm.*,pp.ID as PromotionPriorityID,pp.PriorityNo from PromotionsMaster pm left join PromotionPriority pp on pm.ID=pp.PromotionID JOIN PromotionsWithStoreDetails psd on pm.ID = psd.PromotionHeaderID";

                //if (_RequestFrom == Enums.RequestFrom.StoreSales && RequestData.RequestedProcess != null && RequestData.RequestedProcess == "SalesInvoice")
                //{
                //    sSql = sSql + " where pm.Active='True' and psd.Code = (Select StoreCode from StoreMaster where ID = " + RequestData.StoreIDs + ") order by pp.PriorityNo";                    
                //}
                //else 
                //{
                //    sSql = sSql + " and psd.Code = (Select StoreCode from StoreMaster where ID = "+RequestData.StoreIDs+") order by pp.PriorityNo";
                //}

                if (_RequestFrom == Enums.RequestFrom.StoreSales && RequestData.RequestedProcess != null && RequestData.RequestedProcess == "SalesInvoice")
                {
                    sSql = sSql + " where pm.Active='True' order by pp.PriorityNo";
                }
                else
                {

                    sSql = sSql + /*" and psd.Code = (Select StoreCode from StoreMaster where ID = "+RequestData.StoreIDs+")*/ " order by pp.PriorityNo";
                }

                //sSql.Append("left outer join PromotionsGroupMaster VGM on VM.PromotionsGroupMasterID=VGM.ID    ");
                //sSql.Append("left outer join CompanySettings CS on VM.CompanyID=CS.ID   ");
                //sSql.Append("left outer join CountryMaster CM on VM.CountryID=CM.ID  ");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotions = new PromotionsMaster();
                        objPromotions.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotions.PromotionPriorityID = objReader["PromotionPriorityID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionPriorityID"]) : 0;
                        objPromotions.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        objPromotions.pricelistcode = Convert.ToString(objReader["pricelistcode"]);
                        objPromotions.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotions.PromotionType = Convert.ToString(objReader["PromotionType"]);
                        objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objPromotions.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        objPromotions.MinBillAmount = Convert.ToDecimal(objReader["MinBillAmount"]);
                        objPromotions.Type = Convert.ToString(objReader["Type"]);
                        objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["BuyOptionalCount"]) : 0;
                        objPromotions.GetOptionalCount = objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["GetOptionalCount"]) : 0;
                        objPromotions.GetItematFixedPrice = objReader["GetItematFixedPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["GetItematFixedPrice"]) : 0;
                        //objPromotions.GetItematFixedPrice = Convert.ToDecimal(objReader["GetItematFixedPrice"]);
                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                        objPromotions.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;
                        objPromotions.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objPromotions.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objPromotions.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : false;
                        objPromotions.LowestValueWithGroup = objReader["LowestValueWithGroup"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValueWithGroup"]) : false;
                        
                        objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPromotions.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;

                        objPromotions.MaxGiftPerInvoice = objReader["MaxGiftPerInvoice"] != DBNull.Value ? Convert.ToInt32(objReader["MaxGiftPerInvoice"]) : 0;
                        objPromotions.GiftQuantity = objReader["GiftQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GiftQuantity"]) : 0;
                        objPromotions.GiftBillAmount = objReader["GiftBillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["GiftBillAmount"]) : 0;
                        objPromotions.MultiApplyForReceipt = objReader["MultiApplyForReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["MultiApplyForReceipt"]) : false;
                        objPromotions.BuyItemOptionalAmount = objReader["BuyItemOptionalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BuyItemOptionalAmount"]) : 0;

                        SelectByPromotionIDStoreDetailsRequest objRequest = new SelectByPromotionIDStoreDetailsRequest();
                        SelectByPromotionIDStoreDetailsResponse objResponse = new SelectByPromotionIDStoreDetailsResponse();
                        objRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRequest.DetailsType = Enums.PromotionRecordType.Store;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.StoreList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Customer;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.CustomerList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Category;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.ProductTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.BuyItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.BuyItemTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.GetItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.GetItemTypeList = objResponse.DetailsRecord;
                        }

                        PromotionsList.Add(objPromotions);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionsList = PromotionsList;
                    ResponseData.ResponseDynamicData = PromotionsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
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

        public override SelectAllPromotionsResponse SelectAllStorePromotions(SelectAllPromotionsRequest RequestObj)
        {
            var PromotionsList = new List<PromotionsMaster>();
            var RequestData = (SelectAllPromotionsRequest)RequestObj;
            var ResponseData = new SelectAllPromotionsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_StorePromotions", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StoreID;

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotions = new PromotionsMaster();
                        objPromotions.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objPromotions.PromotionPriorityID = objReader["PromotionPriorityID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionPriorityID"]) : 0;
                        objPromotions.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        //objPromotions.pricelistcode = Convert.ToString(objReader["pricelistcode"]);
                        objPromotions.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotions.PromotionType = Convert.ToString(objReader["PromotionType"]);
                        //objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objPromotions.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        objPromotions.MinBillAmount = Convert.ToDecimal(objReader["MinBillAmount"]);
                        objPromotions.Type = Convert.ToString(objReader["Type"]);
                        objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["BuyOptionalCount"]) : 0;
                        objPromotions.GetOptionalCount = objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["GetOptionalCount"]) : 0;
                        objPromotions.GetItematFixedPrice = Convert.ToDecimal(objReader["GetItematFixedPrice"]);
                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                        objPromotions.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;
                        objPromotions.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objPromotions.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objPromotions.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : false;
                        objPromotions.LowestValueWithGroup = objReader["LowestValueWithGroup"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValueWithGroup"]) : false;

                        objPromotions.MinPromotionQty = objReader["MinPromotionQty"] != DBNull.Value ? Convert.ToInt32(objReader["MinPromotionQty"]) : 0;

                        objPromotions.MaxGiftPerInvoice = objReader["MaxGiftPerInvoice"] != DBNull.Value ? Convert.ToInt32(objReader["MaxGiftPerInvoice"]) : 0;
                        objPromotions.GiftQuantity = objReader["GiftQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GiftQuantity"]) : 0;
                        objPromotions.GiftBillAmount = objReader["GiftBillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["GiftBillAmount"]) : 0;
                        objPromotions.MultiApplyForReceipt = objReader["MultiApplyForReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["MultiApplyForReceipt"]) : false;

                        objPromotions.BuyItemOptionalAmount = objReader["BuyItemOptionalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BuyItemOptionalAmount"]) : 0;

                        //objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPromotions.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;

                        objPromotions.ExculdeDiscountItems = objReader["ExculdeDiscountItems"] != DBNull.Value ? Convert.ToBoolean(objReader["ExculdeDiscountItems"]) : false;

                        SelectByPromotionIDStoreDetailsRequest objRequest = new SelectByPromotionIDStoreDetailsRequest();
                        SelectByPromotionIDStoreDetailsResponse objResponse = new SelectByPromotionIDStoreDetailsResponse();
                        objRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRequest.DetailsType = Enums.PromotionRecordType.Store;
                        objRequest.Type = "";
                        objResponse = SelectAllStorePromotionDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.StoreList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Customer;
                        objRequest.Type = "";
                        objResponse = SelectAllStorePromotionDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.CustomerList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Category;
                        objRequest.Type = "";
                        objResponse = SelectAllStorePromotionDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.ProductTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.BuyItem;
                        objRequest.Type = "";
                        objResponse = SelectAllStorePromotionDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.BuyItemTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.GetItem;
                        objRequest.Type = "";
                        objResponse = SelectAllStorePromotionDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.GetItemTypeList = objResponse.DetailsRecord;
                        }

                        PromotionsList.Add(objPromotions);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionsList = PromotionsList;
                    //ResponseData.ResponseDynamicData = PromotionsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
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
            var PromotionsList = new List<PromotionsMaster>();
            var RequestData = (SelectByPromotionsIDsRequest)RequestObj;
            var ResponseData = new SelectByPromotionsIDsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from PromotionsMaster with(NoLock) where  ID in  ('{0}') ";
                sSql = string.Format(sSql, RequestData.IDs);


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotions = new PromotionsMaster();
                        objPromotions.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotions.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        objPromotions.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotions.PromotionType = Convert.ToString(objReader["PromotionType"]);
                        objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objPromotions.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        objPromotions.MinBillAmount = Convert.ToDecimal(objReader["MinBillAmount"]);
                        //objPromotions.Periority = Convert.ToInt32(objReader["Periority"]);
                        objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["BuyOptionalCount"]) : 0;
                        objPromotions.GetOptionalCount = objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToInt32 (objReader["GetOptionalCount"]) : 0;
                        objPromotions.GetItematFixedPrice = Convert.ToDecimal(objReader["GetItematFixedPrice"]);
                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                        //objPromotions.CollectionCode = Convert.ToString(objReader["CollectionCode"]);
                        //objPromotions.GenderCode = Convert.ToString(objReader["GenderCode"]);
                        //objPromotions.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        //objPromotions.YearCode = Convert.ToInt32(objReader["YearCode"]);
                        objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotions.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;
                        objPromotions.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : false;
                        objPromotions.LowestValueWithGroup = objReader["LowestValueWithGroup"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValueWithGroup"]) : false;
                        objPromotions.MinPromotionQty = objReader["MinPromotionQty"] != DBNull.Value ? Convert.ToInt32(objReader["MinPromotionQty"]) : 0;

                        objPromotions.MaxGiftPerInvoice = objReader["MaxGiftPerInvoice"] != DBNull.Value ? Convert.ToInt32(objReader["MaxGiftPerInvoice"]) : 0;
                        objPromotions.GiftQuantity = objReader["GiftQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GiftQuantity"]) : 0;
                        objPromotions.GiftBillAmount = objReader["GiftBillAmount"] != DBNull.Value ? Convert.ToInt32(objReader["GiftBillAmount"]) : 0;
                        objPromotions.MultiApplyForReceipt = objReader["MultiApplyForReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["MultiApplyForReceipt"]) : true;

                        objPromotions.BuyItemOptionalAmount = objReader["BuyItemOptionalAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BuyItemOptionalAmount"]) : 0;

                        SelectByPromotionIDStoreDetailsRequest objRequest = new SelectByPromotionIDStoreDetailsRequest();
                        SelectByPromotionIDStoreDetailsResponse objResponse = new SelectByPromotionIDStoreDetailsResponse();
                        objRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRequest.DetailsType = Enums.PromotionRecordType.Store;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.StoreList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Customer;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.CustomerList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Category;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.ProductTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.BuyItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.BuyItemTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.GetItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.GetItemTypeList = objResponse.DetailsRecord;
                        }

                        PromotionsList.Add(objPromotions);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionsList = PromotionsList;
                    ResponseData.ResponseDynamicData = PromotionsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
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
        public override EasyBizResponse.Masters.PromotionsMasterResponse.SelectPromotionsLookUpResponse SelectPromotionsLookUp(EasyBizRequest.Masters.PromotionsMasterRequest.SelectPromotionsLookUpRequest ObjRequest)
        {
            throw new NotImplementedException();
        }
        public string StoreTypeListXML(List<CommonUtil> StoreTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objStoreTypeDetails in StoreTypeList)
            {
                sSql.Append("<StoreTypeData>");

                sSql.Append("<ID>" + (objStoreTypeDetails.ID) + "</ID>");
                sSql.Append("<DocumentID>" + (objStoreTypeDetails.DocumentID) + "</DocumentID>");
                sSql.Append("<Code>" + objStoreTypeDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objStoreTypeDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + objStoreTypeDetails.TypeName + "</Type>");
                //sSql.Append("<VisualOrder>" + objScaleDetailMasterDetails.VisualOrder + "</VisualOrder>");
                //sSql.Append("<SCN>" + objScaleDetailMasterDetails.SCN + "</SCN>");              
                sSql.Append("<Active>" + objStoreTypeDetails.Active + "</Active>");
                //sSql.Append("<DocumentID>" + objStoreTypeDetails.DocumentID + "</DocumentID>");
                //sSql.Append("<CreateBy>" + objScaleDetailMasterDetails.CreateBy + "</CreateBy>");
                sSql.Append("</StoreTypeData>");
            }
            return sSql.ToString();
        }
        public string ProductTypeListXML(List<CommonUtil> ProductTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            if (ProductTypeList != null)
            {
                foreach (CommonUtil objProductTypeMasterDetails in ProductTypeList)
                {
                    sSql.Append("<ProductTypeData>");

                    sSql.Append("<ID>" + (objProductTypeMasterDetails.ID) + "</ID>");
                    sSql.Append("<Code>" + objProductTypeMasterDetails.DocumentCode + "</Code>");
                    sSql.Append("<Name>" + (objProductTypeMasterDetails.DocumentName) + "</Name>");
                    sSql.Append("<StyleCode>" + objProductTypeMasterDetails.StyleCode + "</StyleCode>");
                    sSql.Append("<Type>" + objProductTypeMasterDetails.TypeName + "</Type>");
                    sSql.Append("<Active>" + objProductTypeMasterDetails.Active + "</Active>");
                    sSql.Append("<DocumentID>" + objProductTypeMasterDetails.DocumentID + "</DocumentID>");

                    sSql.Append("</ProductTypeData>");
                }
            }
            return sSql.ToString();
        }
        public string CustomerTypeListXML(List<CommonUtil> CustomerTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            if (CustomerTypeList != null)
            {
                foreach (CommonUtil objCustomerMasterDetails in CustomerTypeList)
                {
                    sSql.Append("<CustomerTypeData>");
                    sSql.Append("<ID>" + (objCustomerMasterDetails.ID) + "</ID>");
                    sSql.Append("<Code>" + objCustomerMasterDetails.DocumentCode + "</Code>");
                    sSql.Append("<Name>" + (objCustomerMasterDetails.DocumentName) + "</Name>");
                    sSql.Append("<Type>" + objCustomerMasterDetails.TypeName + "</Type>");
                    sSql.Append("<Active>" + objCustomerMasterDetails.Active + "</Active>");
                    sSql.Append("<DocumentID>" + objCustomerMasterDetails.DocumentID + "</DocumentID>");

                    sSql.Append("</CustomerTypeData>");
                }
            }
            return sSql.ToString();
        }
        public string BuyItemTypeXML(List<CommonUtil> BuyItemTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            if (BuyItemTypeList != null)
            {
                foreach (CommonUtil objBuyItemTypeDetails in BuyItemTypeList)
                {
                    sSql.Append("<BuyItemTypeData>");
                    sSql.Append("<ID>" + (objBuyItemTypeDetails.ID) + "</ID>");
                    sSql.Append("<Code>" + objBuyItemTypeDetails.DocumentCode + "</Code>");
                    sSql.Append("<Name>" + (objBuyItemTypeDetails.DocumentName) + "</Name>");
                    sSql.Append("<Type>" + objBuyItemTypeDetails.TypeName + "</Type>");
                    sSql.Append("<StyleCode>" + objBuyItemTypeDetails.StyleCode + "</StyleCode>");
                    sSql.Append("<Active>" + objBuyItemTypeDetails.Active + "</Active>");
                    sSql.Append("<Quantity>" + objBuyItemTypeDetails.Quantity + "</Quantity>");
                    sSql.Append("<Amount>" + objBuyItemTypeDetails.Amount + "</Amount>");
                    sSql.Append("<DocumentID>" + objBuyItemTypeDetails.DocumentID + "</DocumentID>");
                    sSql.Append("<IsMandatory>" + objBuyItemTypeDetails.IsMandatory + "</IsMandatory>");
                    sSql.Append("</BuyItemTypeData>");
                }
            }
                return sSql.ToString();
            
        }
        public string GetItemTypeXML(List<CommonUtil> GetItemTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            if (GetItemTypeList == null)
            {
                /*sSql.Append("<GetItemTypeData>");
                sSql.Append("<ID>NULL</ID>");
                sSql.Append("<Code>NULL</Code>");
                sSql.Append("<Name>NULL</Name>");
                sSql.Append("<StyleCode>NULL</StyleCode>");
                sSql.Append("<Type>NULL</Type>");
                sSql.Append("<Active>NULL</Active>");
                sSql.Append("<Quantity>NULL</Quantity>");
                sSql.Append("<Amount>NULL</Amount>");
                sSql.Append("<DiscountValue>NULL</DiscountValue>");
                sSql.Append("<Prompt>NULL</Prompt>");
                sSql.Append("<DocumentID>NULL</DocumentID>");
                sSql.Append("<DiscountType>NULL</DiscountType>");
                sSql.Append("</GetItemTypeData>");*/
            }
            else
            {
                foreach (CommonUtil objGetItemTypeDetails in GetItemTypeList)
                {
                    sSql.Append("<GetItemTypeData>");
                    sSql.Append("<ID>" + (objGetItemTypeDetails.ID) + "</ID>");
                    sSql.Append("<Code>" + objGetItemTypeDetails.DocumentCode + "</Code>");
                    sSql.Append("<Name>" + (objGetItemTypeDetails.DocumentName) + "</Name>");
                    sSql.Append("<StyleCode>" + objGetItemTypeDetails.StyleCode + "</StyleCode>");
                    sSql.Append("<Type>" + objGetItemTypeDetails.TypeName + "</Type>");
                    sSql.Append("<Active>" + objGetItemTypeDetails.Active + "</Active>");
                    sSql.Append("<Quantity>" + objGetItemTypeDetails.Quantity + "</Quantity>");
                    sSql.Append("<Amount>" + objGetItemTypeDetails.Amount + "</Amount>");
                    sSql.Append("<DiscountValue>" + objGetItemTypeDetails.DiscountValue + "</DiscountValue>");
                    sSql.Append("<Prompt>" + objGetItemTypeDetails.Prompt + "</Prompt>");
                    sSql.Append("<DocumentID>" + objGetItemTypeDetails.DocumentID + "</DocumentID>");
                    sSql.Append("<DiscountType>" + objGetItemTypeDetails.DiscountType + "</DiscountType>");
                    sSql.Append("</GetItemTypeData>");
                }
            }
            return sSql.ToString();
        }

        public override SelectByPromotionIDStoreDetailsResponse SelectByPromotionWithStoreDetails(SelectByPromotionIDStoreDetailsRequest ObjRequest)
        {
            var DetailsRecord = new List<CommonUtil>();
            var RequestData = (SelectByPromotionIDStoreDetailsRequest)ObjRequest;
            var ResponseData = new SelectByPromotionIDStoreDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.DetailsType == Enums.PromotionRecordType.Store)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID from PromotionsWithStoreDetails with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.Customer)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID from PromotionsWithCustomerDetails with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.Category)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,StyleCode,Name,Type,UpdateFlag,DocumentID,PromotionFrom from PromotionsWithProducts with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.BuyItem)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,StyleCode,Name,Type,UpdateFlag,DocumentID,Quantity,Amount,PromotionFrom,IsMandatory from PromotionWithBuyItem with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.GetItem)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,StyleCode,Name,Type,UpdateFlag,DocumentID,Quantity,Discount,DiscountValue,Prompt,Amount,PromotionFrom from PromotionWithGetItemDetails with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCategoryMaster = new CommonUtil();
                        objCategoryMaster.PromotionHeaderID = objReader["PromotionHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionHeaderID"]) : 0;
                        objCategoryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCategoryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCategoryMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objCategoryMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objCategoryMaster.TypeName = Convert.ToString(objReader["Type"]);
                        objCategoryMaster.DocumentID = objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) : 0;

                        if (RequestData.DetailsType == Enums.PromotionRecordType.Category)
                        {
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                            objCategoryMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        }
                        if (RequestData.DetailsType == Enums.PromotionRecordType.GetItem)
                        {
                            objCategoryMaster.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                            objCategoryMaster.DiscountType = Convert.ToString(objReader["Discount"]);
                            objCategoryMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                            objCategoryMaster.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToInt32(objReader["Amount"]) : 0;
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                            objCategoryMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        }
                        if (RequestData.DetailsType == Enums.PromotionRecordType.BuyItem)
                        {
                            objCategoryMaster.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                            objCategoryMaster.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToInt32(objReader["Amount"]) : 0;
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                            objCategoryMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                            objCategoryMaster.IsMandatory = objReader["IsMandatory"] != DBNull.Value ? Convert.ToBoolean(objReader["IsMandatory"]) : false;
                        }

                        objCategoryMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;

                        DetailsRecord.Add(objCategoryMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DetailsRecord = DetailsRecord;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Type Details");
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

        public override SelectPromotionCriteriaResponse SelectPromotionCriteriaDetails(SelectPromotionCriteriaRequest ObjRequest)
        {
            var PromotionCriteria = new List<PromotionCriteria>();
            var RequestData = (SelectPromotionCriteriaRequest)ObjRequest;
            var ResponseData = new SelectPromotionCriteriaResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.PromotionCode != "")
                {
                    //sSql.Append("Select pm.PromotionCode,pm.PromotionName,pm.PromotionType,pm.MinBillAmount,pm.MinQuantity,pm.AllowMultiPromotion,pm.LowestValue,pm.Discount As PromotionHeaderDiscountType,pm.DiscountValue as PromotionHeaderDiscountValue,Buy.[Type] as BuyType,Buy.DocumentID as BuyDocumentID,Buy.Name as BuyName,Buy.Quantity as BuyQty,  ");
                    //sSql.Append("Buy.Amount as BuyAmount,GetItm.[Type] as GetItemType,GetItm.DocumentID as GetItemDocumentID,GetItm.Name as GetItemName,GetItm.Quantity as GetItemQuantity,GetItm.Discount as DiscountType,GetItm.DiscountValue,   ");
                    //sSql.Append("GetItm.Amount as GetItemAmount,GetItm.Prompt,PP.PriorityNo,pm.Color,pm.StartDate,pm.EndDate from PromotionsMaster pm  ");
                    //sSql.Append("left outer join PromotionWithBuyItem Buy on pm.ID = Buy.PromotionHeaderID    ");
                    //sSql.Append("left outer join PromotionWithGetItemDetails GetItm on Buy.PromotionHeaderID=GetItm.PromotionHeaderID  ");
                    //sSql.Append("left outer join PromotionPriority PP on pm.ID=PP.PromotionID  ");
                    //sSql.Append("where pm.ID in (" + RequestData.PromotionCode + ") order by  PP.PriorityNo");


                    sSql.Append("select * from PromotionCriteriaView1 ");
                    sSql.Append("where ID in (" + RequestData.PromotionCode + ") order by PriorityNo");

                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            PromotionCriteria objPromotionCriteria = new PromotionCriteria();
                            objPromotionCriteria.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objPromotionCriteria.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                            objPromotionCriteria.PromotionName = Convert.ToString(objReader["PromotionName"]);
                            objPromotionCriteria.PromotionType = Convert.ToString(objReader["PromotionType"]);
                            objPromotionCriteria.BuyType = objReader["BuyType"] != DBNull.Value ? Convert.ToString(objReader["BuyType"]) : string.Empty;
                            objPromotionCriteria.BuyDocumentID = objReader["BuyDocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["BuyDocumentID"]) : 0;
                            objPromotionCriteria.BuyName = objReader["BuyName"] != DBNull.Value ? Convert.ToString(objReader["BuyName"]) : string.Empty;
                            objPromotionCriteria.BuyQty = objReader["BuyQty"] != DBNull.Value ? Convert.ToInt32(objReader["BuyQty"]) : 0;
                            objPromotionCriteria.BuyAmount = objReader["BuyAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BuyAmount"]) : 0;
                            objPromotionCriteria.GetItemType = objReader["GetItemType"] != DBNull.Value ? Convert.ToString(objReader["GetItemType"]) : string.Empty;
                            objPromotionCriteria.GetItematFixedPrice = objReader["GetItematFixedPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["GetItematFixedPrice"]) : 0;
                            objPromotionCriteria.GetItemDocumentID = objReader["GetItemDocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["GetItemDocumentID"]) : 0;
                            objPromotionCriteria.GetItemName = objReader["GetItemName"] != DBNull.Value ? Convert.ToString(objReader["GetItemName"]) : string.Empty;
                            objPromotionCriteria.GetItemQuantity = objReader["GetItemQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GetItemQuantity"]) : 0;
                            objPromotionCriteria.GetItemAmount = objReader["GetItemAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["GetItemAmount"]) : 0;
                            objPromotionCriteria.DiscountType = objReader["DiscountType"] != DBNull.Value ? Convert.ToString(objReader["DiscountType"]) : string.Empty;
                            objPromotionCriteria.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;
                            objPromotionCriteria.Prompt = objReader["Prompt"] != DBNull.Value ? Convert.ToBoolean(objReader["Prompt"]) : false;
                            objPromotionCriteria.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                            objPromotionCriteria.MinBillAmount = objReader["MinBillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MinBillAmount"]) : 0;
                            objPromotionCriteria.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                            objPromotionCriteria.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : true;
                            objPromotionCriteria.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;
                            objPromotionCriteria.Color = objReader["Color"] != DBNull.Value ? Convert.ToString(objReader["Color"]) : string.Empty;
                            objPromotionCriteria.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                            objPromotionCriteria.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                            objPromotionCriteria.PromotionHeaderDiscountType = objReader["PromotionHeaderDiscountType"] != DBNull.Value ? Convert.ToString(objReader["PromotionHeaderDiscountType"]) : string.Empty;
                            objPromotionCriteria.PromotionHeaderDiscountValue = objReader["PromotionHeaderDiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionHeaderDiscountValue"]) : 0;
                            //objPromotionCriteria.ListType = objReader["ListType"] != DBNull.Value ? Convert.ToString(objReader["ListType"]) : string.Empty;
                            objPromotionCriteria.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;
                            PromotionCriteria.Add(objPromotionCriteria);
                        }
                        ResponseData.PromotionCriteriaList = PromotionCriteria;
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
                    }
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

        public override SelectByPromotionIDStoreDetailsResponse SelectAllStorePromotionDetails(SelectByPromotionIDStoreDetailsRequest ObjRequest)
        {
            var DetailsRecord = new List<CommonUtil>();
            var RequestData = (SelectByPromotionIDStoreDetailsRequest)ObjRequest;
            var ResponseData = new SelectByPromotionIDStoreDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                if (RequestData.DetailsType == Enums.PromotionRecordType.Store)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID from PromotionsWithStoreDetails with(NoLock)";
                    sQuery = sQuery + " where PromotionHeaderID=" + RequestData.ID + "  and isnull(Code,'') <> ''";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.Customer)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID from PromotionsWithCustomerDetails with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + "  and isnull(Code,'') <> ''";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.Category)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,StyleCode,Name,Type,UpdateFlag,DocumentID,PromotionFrom from PromotionsWithProducts with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + "  and isnull(Code,'') <> ''";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.BuyItem)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,StyleCode,Name,Type,UpdateFlag,DocumentID,Quantity,Amount,PromotionFrom,IsMandatory from PromotionWithBuyItem with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + "  and isnull(Code,'') <> ''";
                }
                else if (RequestData.DetailsType == Enums.PromotionRecordType.GetItem)
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,StyleCode,Name,Type,UpdateFlag,DocumentID,Quantity,Discount,DiscountValue,Prompt,Amount,PromotionFrom from PromotionWithGetItemDetails with(NoLock)";
                    sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + "  and isnull(Code,'') <> ''";
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCategoryMaster = new CommonUtil();
                        objCategoryMaster.PromotionHeaderID = objReader["PromotionHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionHeaderID"]) : 0;
                        objCategoryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCategoryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCategoryMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objCategoryMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objCategoryMaster.TypeName = Convert.ToString(objReader["Type"]);
                        objCategoryMaster.DocumentID = objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) : 0;

                        if (RequestData.DetailsType == Enums.PromotionRecordType.Category)
                        {
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                            objCategoryMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        }
                        if (RequestData.DetailsType == Enums.PromotionRecordType.GetItem)
                        {
                            objCategoryMaster.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                            objCategoryMaster.DiscountType = Convert.ToString(objReader["Discount"]);
                            objCategoryMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                            objCategoryMaster.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToInt32(objReader["Amount"]) : 0;
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                            objCategoryMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        }
                        if (RequestData.DetailsType == Enums.PromotionRecordType.BuyItem)
                        {
                            objCategoryMaster.Quantity = objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) : 0;
                            objCategoryMaster.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToInt32(objReader["Amount"]) : 0;
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                            objCategoryMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                            objCategoryMaster.IsMandatory = objReader["IsMandatory"] != DBNull.Value ? Convert.ToBoolean(objReader["IsMandatory"]) : false;
                        }

                        objCategoryMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;

                        DetailsRecord.Add(objCategoryMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DetailsRecord = DetailsRecord;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Type Details");
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

        public override SelectAllPromotionsResponse API_SelectALL(SelectAllPromotionsRequest requestData)
        {
            var PromotionsList = new List<PromotionsMaster>();
            var RequestData = (SelectAllPromotionsRequest)requestData;
            var ResponseData = new SelectAllPromotionsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //string sSql = "select distinct pm.ID,pm.PromotionCode,pm.PromotionName,pm.Active, RecordCount = COUNT(*) OVER() from PromotionsMaster pm left join PromotionPriority pp on pm.ID=pp.PromotionID JOIN PromotionsWithStoreDetails psd on pm.ID = psd.PromotionHeaderID";
                string sSql = "select distinct pm.ID,pm.PromotionCode,pm.PromotionName,pm.Active, RC.TOTAL_CNT [RecordCount] from PromotionsMaster pm with(NoLock) " +
                   "LEFT JOIN(Select  count(PM1.ID) As TOTAL_CNT From PromotionsMaster PM1 with(NoLock) ";
                sSql = sSql + " where PM1.Active = " + RequestData.IsActive + " ";
                sSql = sSql + "and (isnull('" + RequestData.SearchString + "','') = '' ";
                sSql = sSql + "or PM1.PromotionCode like isnull('%" + RequestData.SearchString + "%','') ";
                sSql = sSql + "or PM1.PromotionName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ";

                sSql = sSql + " where pm.Active = " + RequestData.IsActive + " ";
                sSql = sSql + "and (isnull('" + RequestData.SearchString + "','') = '' ";
                sSql = sSql + "or pm.PromotionCode like isnull('%" + RequestData.SearchString + "%','') ";
                sSql = sSql + "or pm.PromotionName like isnull('%" + RequestData.SearchString + "%','')) ";
                //sSql = sSql + "or Description like isnull('%" + RequestData.SearchString + "%','')) ";
                sSql = sSql + "order by pm.ID asc ";
                sSql = sSql + "offset " + RequestData.Offset + " rows ";
                sSql = sSql + "fetch first " + RequestData.Limit + " rows only";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotions = new PromotionsMaster();
                        objPromotions.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objPromotions.PromotionPriorityID = objReader["PromotionPriorityID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionPriorityID"]) : 0;
                        objPromotions.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        //objPromotions.pricelistcode = Convert.ToString(objReader["pricelistcode"]);
                        objPromotions.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        /*objPromotions.PromotionType = Convert.ToString(objReader["PromotionType"]);
                        objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objPromotions.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        objPromotions.MinBillAmount = Convert.ToDecimal(objReader["MinBillAmount"]);
                        objPromotions.Type = Convert.ToString(objReader["Type"]);
                        objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["BuyOptionalCount"]) : 0;
                        objPromotions.GetOptionalCount = objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["GetOptionalCount"]) : 0;
                        objPromotions.GetItematFixedPrice = Convert.ToDecimal(objReader["GetItematFixedPrice"]);
                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                        objPromotions.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;
                        objPromotions.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objPromotions.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objPromotions.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : false;
                        objPromotions.LowestValueWithGroup = objReader["LowestValueWithGroup"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValueWithGroup"]) : false;

                        objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;*/
                        objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objPromotions.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;

                        SelectByPromotionIDStoreDetailsRequest objRequest = new SelectByPromotionIDStoreDetailsRequest();
                        SelectByPromotionIDStoreDetailsResponse objResponse = new SelectByPromotionIDStoreDetailsResponse();
                        /*objRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objRequest.DetailsType = Enums.PromotionRecordType.Store;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.StoreList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Customer;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.CustomerList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.Category;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.ProductTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.BuyItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.BuyItemTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = Enums.PromotionRecordType.GetItem;
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.GetItemTypeList = objResponse.DetailsRecord;
                        }*/

                        PromotionsList.Add(objPromotions);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionsList = PromotionsList;
                    ResponseData.ResponseDynamicData = PromotionsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
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

        public override SelectAllPromotionsResponse SelectPromotionWithPriorityRecords(SelectAllPromotionsRequest RequestObj)
        {
            var PromotionsList = new List<PromotionsMaster>();
            var RequestData = (SelectAllPromotionsRequest)RequestObj;
            var ResponseData = new SelectAllPromotionsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                //var sSql = new StringBuilder();
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "select distinct pm.*,pp.ID as PromotionPriorityID,pp.PriorityNo from PromotionsMaster pm left join PromotionPriority pp on pm.ID=pp.PromotionID JOIN PromotionsWithStoreDetails psd on pm.ID = psd.PromotionHeaderID";

                //if (_RequestFrom == Enums.RequestFrom.StoreSales && RequestData.RequestedProcess != null && RequestData.RequestedProcess == "SalesInvoice")
                //{
                //    sSql = sSql + " where pm.Active='True' and psd.Code = (Select StoreCode from StoreMaster where ID = " + RequestData.StoreIDs + ") order by pp.PriorityNo";                    
                //}
                //else 
                //{
                //    sSql = sSql + " and psd.Code = (Select StoreCode from StoreMaster where ID = "+RequestData.StoreIDs+") order by pp.PriorityNo";
                //}

                if (_RequestFrom == Enums.RequestFrom.StoreSales && RequestData.RequestedProcess != null && RequestData.RequestedProcess == "SalesInvoice")
                {
                    sSql = sSql + " where pm.Active='True' order by pp.PriorityNo";
                }
                else
                {

                    sSql = sSql + /*" and psd.Code = (Select StoreCode from StoreMaster where ID = "+RequestData.StoreIDs+")*/ " order by pp.PriorityNo";
                }

                //sSql.Append("left outer join PromotionsGroupMaster VGM on VM.PromotionsGroupMasterID=VGM.ID    ");
                //sSql.Append("left outer join CompanySettings CS on VM.CompanyID=CS.ID   ");
                //sSql.Append("left outer join CountryMaster CM on VM.CountryID=CM.ID  ");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objPromotions = new PromotionsMaster();
                        objPromotions.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objPromotions.PromotionPriorityID = objReader["PromotionPriorityID"] != DBNull.Value ? Convert.ToInt32(objReader["PromotionPriorityID"]) : 0;
                        objPromotions.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                        objPromotions.pricelistcode = Convert.ToString(objReader["pricelistcode"]);
                        objPromotions.PromotionName = Convert.ToString(objReader["PromotionName"]);
                        objPromotions.PromotionType = Convert.ToString(objReader["PromotionType"]);
                        objPromotions.PriorityNo = objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) : 0;

                        //objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        //objPromotions.MinQuantity = objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        //objPromotions.MinBillAmount = Convert.ToDecimal(objReader["MinBillAmount"]);
                        //objPromotions.Type = Convert.ToString(objReader["Type"]);
                        //objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) : 0;
                        //objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        //objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["BuyOptionalCount"]) : 0;
                        //objPromotions.GetOptionalCount = objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToInt32(objReader["GetOptionalCount"]) : 0;
                        //objPromotions.GetItematFixedPrice = objReader["GetItematFixedPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["GetItematFixedPrice"]) : 0;
                        ////objPromotions.GetItematFixedPrice = Convert.ToDecimal(objReader["GetItematFixedPrice"]);
                        //objPromotions.Color = Convert.ToString(objReader["Color"]);
                        //objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                      
                        //objPromotions.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        //objPromotions.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        //objPromotions.LowestValue = objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : false;
                        //objPromotions.LowestValueWithGroup = objReader["LowestValueWithGroup"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValueWithGroup"]) : false;

                        //objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        //objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        //objPromotions.AppliedType = objReader["AppliedType"] != DBNull.Value ? Convert.ToString(objReader["AppliedType"]) : string.Empty;

                        //objPromotions.MaxGiftPerInvoice = objReader["MaxGiftPerInvoice"] != DBNull.Value ? Convert.ToInt32(objReader["MaxGiftPerInvoice"]) : 0;
                        //objPromotions.GiftQuantity = objReader["GiftQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GiftQuantity"]) : 0;
                        //objPromotions.GiftBillAmount = objReader["GiftBillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["GiftBillAmount"]) : 0;
                        //objPromotions.MultiApplyForReceipt = objReader["MultiApplyForReceipt"] != DBNull.Value ? Convert.ToBoolean(objReader["MultiApplyForReceipt"]) : false;


                        PromotionsList.Add(objPromotions);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.PromotionsList = PromotionsList;
                    ResponseData.ResponseDynamicData = PromotionsList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Promotions Master");
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



        //public override BaseResponseType InsertPaymentProcessorRecord(BaseRequestType RequestObj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
