using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Promotion;
using EasyBizRequest.Masters.PromotionsMasterRequest;
using EasyBizRequest.Transactions.Promotions.PromotionCriteria;
using EasyBizResponse.Masters.PromotionsMasterResponse;
using EasyBizResponse.Transactions.Promotions.PromotionCriteriaResponse;
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
        SqlTransaction transaction = null;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;  

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SavePromotionsRequest)RequestObj;
            var ResponseData = new SavePromotionsResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertOrUpdatePromotionsMaster", _ConnectionObj);
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


                SqlParameter ExculdeDiscountItems = _CommandObj.Parameters.Add("@ExculdeDiscountItems", SqlDbType.Bit);
                ExculdeDiscountItems.Direction = ParameterDirection.Input;
                ExculdeDiscountItems.Value = RequestData.PromotionsRecord.ExculdeDiscountItems;

                SqlParameter Prompt = _CommandObj.Parameters.Add("@Prompt", SqlDbType.Bit);
                Prompt.Direction = ParameterDirection.Input;
                Prompt.Value = RequestData.PromotionsRecord.Prompt;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.PromotionsRecord.Active;

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

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Promotions Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
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

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Update PromotionsMaster set Active='false' where ID={0}";
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

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
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
                        objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                        objPromotions.BuyOptionalCount = objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToDouble(objReader["BuyOptionalCount"]) : 0;
                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) : 0;
                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDouble(objReader["DiscountValue"]) : 0;
                        objPromotions.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objPromotions.ExculdeDiscountItems =objReader["ExculdeDiscountItems"] != DBNull.Value ? Convert.ToBoolean(objReader["ExculdeDiscountItems"]) : true;
                        objPromotions.GetItematFixedPrice =objReader["GetItematFixedPrice"] != DBNull.Value ? Convert.ToDouble(objReader["GetItematFixedPrice"]) : 0;
                        objPromotions.GetOptionalCount =objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToDouble(objReader["GetOptionalCount"]) :0;
                        objPromotions.LowestValue =objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : true;
                        objPromotions.MinBillAmount =objReader["MinBillAmount"] != DBNull.Value ? Convert.ToDouble(objReader["MinBillAmount"]):0;
                        objPromotions.MinQuantity =objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) : 0;
                        objPromotions.Prompt =objReader["Prompt"] != DBNull.Value ? Convert.ToBoolean(objReader["Prompt"]) : true;
                        objPromotions.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objPromotions.Type = Convert.ToString(objReader["Type"]);                        
                        objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objPromotions.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.PromotionsRecord = objPromotions;
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
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql="select * from PromotionsMaster ";
                //sSql.Append("left outer join PromotionsGroupMaster VGM on VM.PromotionsGroupMasterID=VGM.ID    ");
                //sSql.Append("left outer join CompanySettings CS on VM.CompanyID=CS.ID   ");
                //sSql.Append("left outer join CountryMaster CM on VM.CountryID=CM.ID  ");

                if (!RequestData.ShowInActiveRecords)
                {
                    sSql =sSql +" where Active='True'";
                }
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
                        objPromotions.PriceListID =objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"]) :0;
                        objPromotions.MinQuantity =objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) :0;
                        objPromotions.MinBillAmount =objReader["MinBillAmount"] != DBNull.Value ? Convert.ToDouble(objReader["MinBillAmount"]) :0;
                        //objPromotions.Periority =objReader["Periority"] != DBNull.Value ? Convert.ToInt32(objReader["Periority"]) :0;
                        objPromotions.DiscountValue =objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) :0;
                        objPromotions.Discount = Convert.ToString(objReader["Discount"]);
                        objPromotions.BuyOptionalCount =objReader["BuyOptionalCount"] != DBNull.Value ? Convert.ToDouble(objReader["BuyOptionalCount"]):0;
                        objPromotions.GetOptionalCount =objReader["GetOptionalCount"] != DBNull.Value ? Convert.ToDouble(objReader["GetOptionalCount"]) :0;
                        objPromotions.GetItematFixedPrice =objReader["GetItematFixedPrice"] != DBNull.Value ? Convert.ToDouble(objReader["GetItematFixedPrice"]) :0;
                        objPromotions.Color = Convert.ToString(objReader["Color"]);
                        objPromotions.AllowMultiPromotion = objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                        //objPromotions.CollectionCode = Convert.ToString(objReader["CollectionCode"]);
                        //objPromotions.GenderCode = Convert.ToString(objReader["GenderCode"]);
                        //objPromotions.ProductGroupCode = Convert.ToString(objReader["ProductGroupCode"]);
                        //objPromotions.YearCode =objReader["YearCode"] != DBNull.Value ? Convert.ToInt32(objReader["YearCode"]) :0;
                        objPromotions.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objPromotions.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objPromotions.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objPromotions.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objPromotions.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;


                        SelectByPromotionIDStoreDetailsRequest objRequest = new SelectByPromotionIDStoreDetailsRequest();
                        SelectByPromotionIDStoreDetailsResponse objResponse = new SelectByPromotionIDStoreDetailsResponse();
                        objRequest.ID = Convert.ToInt32(objReader["ID"]);
                        objRequest.DetailsType = "Store";
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.StoreList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = "Customer";
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.CustomerList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = "ProductType";
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.ProductTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = "BuyItemType";
                        objRequest.Type = "";
                        objResponse = SelectByPromotionWithStoreDetails(objRequest);
                        if (objResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objPromotions.BuyItemTypeList = objResponse.DetailsRecord;
                        }

                        objRequest.DetailsType = "GetItemType";
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
                sSql.Append("<Code>" + objStoreTypeDetails.Code + "</Code>");
                sSql.Append("<Name>" + (objStoreTypeDetails.Name) + "</Name>");
                //sSql.Append("<VisualOrder>" + objScaleDetailMasterDetails.VisualOrder + "</VisualOrder>");
                //sSql.Append("<SCN>" + objScaleDetailMasterDetails.SCN + "</SCN>");
                sSql.Append("<Type>" + objStoreTypeDetails.Type + "</Type>");
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
            foreach (CommonUtil objProductTypeMasterDetails in ProductTypeList)
            {
                sSql.Append("<ProductTypeData>");

                sSql.Append("<ID>" + (objProductTypeMasterDetails.ID) + "</ID>");
                sSql.Append("<Code>" + objProductTypeMasterDetails.Code + "</Code>");
                sSql.Append("<Name>" + (objProductTypeMasterDetails.Name) + "</Name>");
                sSql.Append("<Type>" + objProductTypeMasterDetails.Type + "</Type>");
                sSql.Append("<Active>" + objProductTypeMasterDetails.Active + "</Active>");
                sSql.Append("<DocumentID>" + objProductTypeMasterDetails.DocumentID + "</DocumentID>");

                sSql.Append("</ProductTypeData>");
            }
            return sSql.ToString();
        }
        public string CustomerTypeListXML(List<CommonUtil> CustomerTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objCustomerMasterDetails in CustomerTypeList)
            {
                sSql.Append("<CustomerTypeData>");
                sSql.Append("<ID>" + (objCustomerMasterDetails.ID) + "</ID>");
                sSql.Append("<Code>" + objCustomerMasterDetails.Code + "</Code>");
                sSql.Append("<Name>" + (objCustomerMasterDetails.Name) + "</Name>");
                sSql.Append("<Type>" + objCustomerMasterDetails.Type + "</Type>");
                sSql.Append("<Active>" + objCustomerMasterDetails.Active + "</Active>");
                sSql.Append("<DocumentID>" + objCustomerMasterDetails.DocumentID + "</DocumentID>");

                sSql.Append("</CustomerTypeData>");
            }
            return sSql.ToString();
        }
        public string BuyItemTypeXML(List<CommonUtil> BuyItemTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objBuyItemTypeDetails in BuyItemTypeList)
            {
                sSql.Append("<BuyItemTypeData>");
                sSql.Append("<ID>" + (objBuyItemTypeDetails.ID) + "</ID>");
                sSql.Append("<Code>" + objBuyItemTypeDetails.Code + "</Code>");
                sSql.Append("<Name>" + (objBuyItemTypeDetails.Name) + "</Name>");
                sSql.Append("<Type>" + objBuyItemTypeDetails.Type + "</Type>");
                sSql.Append("<Active>" + objBuyItemTypeDetails.Active + "</Active>");
                sSql.Append("<Quantity>" + objBuyItemTypeDetails.Quantity + "</Quantity>");
                sSql.Append("<Amount>" + objBuyItemTypeDetails.Amount + "</Amount>");
                sSql.Append("<DocumentID>" + objBuyItemTypeDetails.DocumentID + "</DocumentID>");

                sSql.Append("</BuyItemTypeData>");
            }
            return sSql.ToString();
        }
        public string GetItemTypeXML(List<CommonUtil> GetItemTypeList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objGetItemTypeDetails in GetItemTypeList)
            {
                sSql.Append("<GetItemTypeData>");
                sSql.Append("<ID>" + (objGetItemTypeDetails.ID) + "</ID>");
                sSql.Append("<Code>" + objGetItemTypeDetails.Code + "</Code>");
                sSql.Append("<Name>" + (objGetItemTypeDetails.Name) + "</Name>");
                sSql.Append("<Type>" + objGetItemTypeDetails.Type + "</Type>");
                sSql.Append("<Active>" + objGetItemTypeDetails.Active + "</Active>");
                sSql.Append("<Quantity>" + objGetItemTypeDetails.Quantity + "</Quantity>");
                sSql.Append("<Amount>" + objGetItemTypeDetails.Amount + "</Amount>");
                sSql.Append("<DiscountValue>" + objGetItemTypeDetails.DiscountValue + "</DiscountValue>");
                sSql.Append("<Prompt>" + objGetItemTypeDetails.Prompt + "</Prompt>");
                sSql.Append("<DocumentID>" + objGetItemTypeDetails.DocumentID + "</DocumentID>");
                sSql.Append("<Discount>" + objGetItemTypeDetails.Discount + "</Discount>");
                sSql.Append("</GetItemTypeData>");
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
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                if (RequestData.DetailsType == "Store")
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID from PromotionsWithStoreDetails with(NoLock)";
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        if (RequestData.Type == "")
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                        }
                        else
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " and  Type='" + RequestData.Type + "'";
                        }
                    }
                }
                else if (RequestData.DetailsType == "Customer")
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID from PromotionsWithCustomerDetails with(NoLock)";
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        if (RequestData.Type == "")
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                        }
                        else
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " and  Type='" + RequestData.Type + "'";
                        }
                    }
                }
                else if (RequestData.DetailsType == "ProductType")
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID,PromotionFrom from PromotionsWithProducts with(NoLock)";
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        if (RequestData.Type == "")
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                        }
                        else
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " and  Type='" + RequestData.Type + "'";
                        }
                    }
                }
                 else if (RequestData.DetailsType == "BuyItemType")
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID,Quantity,Amount,PromotionFrom from PromotionWithBuyItem with(NoLock)";
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        if (RequestData.Type == "")
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                        }
                        else
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " and  Type='" + RequestData.Type + "'";
                        }
                    }
                }
                else if (RequestData.DetailsType == "GetItemType")
                {
                    sQuery = "Select PromotionHeaderID,ID,Active,Code,Name,Type,UpdateFlag,DocumentID,Quantity,Discount,DiscountValue,Prompt,Amount,PromotionFrom from PromotionWithGetItemDetails with(NoLock)";
                    if (RequestData.ShowInActiveRecords == false)
                    {
                        if (RequestData.Type == "")
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " ";
                        }
                        else
                        {
                            sQuery = sQuery + " where  PromotionHeaderID=" + RequestData.ID + " and  Type='" + RequestData.Type + "'";
                        }
                    }
                }
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objCategoryMaster = new CommonUtil();
                        objCategoryMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt16(objReader["ID"]) : 0;
                        objCategoryMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objCategoryMaster.Code = Convert.ToString(objReader["Code"]);
                        objCategoryMaster.Name = Convert.ToString(objReader["Name"]);
                        objCategoryMaster.Type = Convert.ToString(objReader["Type"]);
                        objCategoryMaster.DocumentID = objReader["DocumentID"] != DBNull.Value ? Convert.ToInt16(objReader["DocumentID"]) : 0;

                        if (RequestData.DetailsType == "ProductType")
                        {                        
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                        }
                        if (RequestData.DetailsType == "GetItemType")
                        {
                            objCategoryMaster.Quantity =objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) :0;
                            objCategoryMaster.Discount = Convert.ToString(objReader["Discount"]);
                            objCategoryMaster.DiscountValue =objReader["DiscountValue"] != DBNull.Value ? Convert.ToInt32(objReader["DiscountValue"]) :0;
                            objCategoryMaster.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToInt32(objReader["Amount"]) : 0;
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
                        }
                        if (RequestData.DetailsType == "BuyItemType")
                        {
                            objCategoryMaster.Quantity =objReader["Quantity"] != DBNull.Value ? Convert.ToInt32(objReader["Quantity"]) :0;
                            objCategoryMaster.Amount = objReader["Amount"] != DBNull.Value ? Convert.ToInt32(objReader["Amount"]) : 0;
                            objCategoryMaster.PromotionFrom = Convert.ToString(objReader["PromotionFrom"]);
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

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                if (RequestData.PromotionCode != "")
                {
                    sSql.Append("Select pm.PromotionCode,pm.PromotionName,pm.MinBillAmount,pm.MinQuantity,pm.AllowMultiPromotion,pm.LowestValue,pm.Discount As PromotionHeaderDiscountType,pm.DiscountValue as PromotionHeaderDiscountValue,Buy.[Type] as BuyType,Buy.DocumentID as BuyDocumentID,Buy.Name as BuyName,Buy.Quantity as BuyQty,  ");
                    sSql.Append("Buy.Amount as BuyAmount,GetItm.[Type] as GetItemType,GetItm.DocumentID as GetItemDocumentID,GetItm.Name as GetItemName,GetItm.Quantity as GetItemQuantity,GetItm.Discount as DiscountType,GetItm.DiscountValue,   ");
                    sSql.Append("GetItm.Amount as GetItemAmount,GetItm.Prompt,PP.PriorityNo,pm.Color,pm.StartDate,pm.EndDate from PromotionsMaster pm  ");
                    sSql.Append("left outer join PromotionWithBuyItem Buy on pm.ID = Buy.PromotionHeaderID    ");
                    sSql.Append("left outer join PromotionWithGetItemDetails GetItm on Buy.PromotionHeaderID=GetItm.PromotionHeaderID  ");
                    sSql.Append("left outer join PromotionPriority PP on pm.ID=PP.PromotionID  ");
                    sSql.Append("where pm.PromotionCode in (" + RequestData.PromotionCode + ") order by  PP.PriorityNo");



                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            PromotionCriteria objPromotionCriteria = new PromotionCriteria();
                            objPromotionCriteria.PromotionCode = Convert.ToString(objReader["PromotionCode"]);
                            objPromotionCriteria.PromotionName = Convert.ToString(objReader["PromotionName"]);
                            objPromotionCriteria.BuyType = Convert.ToString(objReader["BuyType"]);


                            objPromotionCriteria.BuyDocumentID = objReader["BuyDocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["BuyDocumentID"]) : 0;
                            objPromotionCriteria.BuyName = Convert.ToString(objReader["BuyName"]);
                            objPromotionCriteria.BuyQty = objReader["BuyQty"] != DBNull.Value ? Convert.ToInt32(objReader["BuyQty"]) : 0;

                            objPromotionCriteria.BuyAmount = objReader["BuyAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["BuyAmount"]) : 0;
                            objPromotionCriteria.GetItemType = Convert.ToString(objReader["GetItemType"]);

                            objPromotionCriteria.GetItemDocumentID = objReader["GetItemDocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["GetItemDocumentID"]) : 0;
                            objPromotionCriteria.GetItemName = Convert.ToString(objReader["GetItemName"]);

                            objPromotionCriteria.GetItemQuantity = objReader["GetItemQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["GetItemQuantity"]) : 0;

                            objPromotionCriteria.GetItemAmount = objReader["GetItemAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["GetItemAmount"]) : 0;
                            objPromotionCriteria.DiscountType = Convert.ToString(objReader["DiscountType"]);
                            objPromotionCriteria.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["DiscountValue"]) : 0;

                            objPromotionCriteria.Prompt = objReader["Prompt"] != DBNull.Value ? Convert.ToBoolean(objReader["Prompt"]) : false;
                            objPromotionCriteria.AllowMultiPromotion =objReader["AllowMultiPromotion"] != DBNull.Value ? Convert.ToBoolean(objReader["AllowMultiPromotion"]) : true;
                            objPromotionCriteria.MinBillAmount =objReader["MinBillAmount"] != DBNull.Value ? Convert.ToDecimal(objReader["MinBillAmount"]) : 0;
                            objPromotionCriteria.MinQuantity =objReader["MinQuantity"] != DBNull.Value ? Convert.ToInt32(objReader["MinQuantity"]) :0;
                            objPromotionCriteria.LowestValue =objReader["LowestValue"] != DBNull.Value ? Convert.ToBoolean(objReader["LowestValue"]) : true;
                            objPromotionCriteria.PriorityNo =objReader["PriorityNo"] != DBNull.Value ? Convert.ToInt32(objReader["PriorityNo"]) :0;
                            objPromotionCriteria.Color = Convert.ToString(objReader["Color"]);
                            objPromotionCriteria.StartDate =objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                            objPromotionCriteria.EndDate =objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) :DateTime.Now;;
                            objPromotionCriteria.PromotionHeaderDiscountType = Convert.ToString(objReader["PromotionHeaderDiscountType"]);
                            objPromotionCriteria.PromotionHeaderDiscountValue = objReader["PromotionHeaderDiscountValue"] != DBNull.Value ? Convert.ToDecimal(objReader["PromotionHeaderDiscountValue"]) : 0;

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
            return ResponseData;
        }
    }
}
