using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizDBTypes.Transactions.Pricing;
using EasyBizRequest;
using EasyBizRequest.Masters.SKUMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.SKUMasterRequest;
using EasyBizResponse.Masters.SKUMasterResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MsSqlDAL.Masters
{
    public class SKUMasterDAL : BaseSKUMasterDAL
    {

        //Test
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        public bool checknontrade = false;

        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            SaveSKUMasterRequest RequestData = (SaveSKUMasterRequest)RequestObj;
            SaveSKUMasterResponse ResponseData = new SaveSKUMasterResponse();
            List<SKUMasterTypes> SKUMasterTypesList = RequestData.SKUMasterTypesRecord;


            StringBuilder sSql = new StringBuilder();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                int DetailID = RequestData.BarCodeID;
                long RunningNo = RequestData.BarCodeRunningNo;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                //transaction = _ConnectionObj.BeginTransaction();

                _CommandObj = new SqlCommand("API_InsertOrUpdateSKUMaster1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                //_CommandObj.CommandTimeout = 300000;
                SqlParameter SkuCode = _CommandObj.Parameters.Add("@SKUCode", SqlDbType.NVarChar);
                SkuCode.Direction = ParameterDirection.Input;
                SkuCode.Value = RequestData.SKUCode;

                SqlParameter SalePriceListID = _CommandObj.Parameters.Add("@SalePriceListID", SqlDbType.Int);
                SalePriceListID.Direction = ParameterDirection.Input;
                SalePriceListID.Value = RequestData.SalePriceListID;

                SqlParameter SKUID = _CommandObj.Parameters.Add("@SKUID1", SqlDbType.Int);
                SKUID.Direction = ParameterDirection.Input;
                SKUID.Value = RequestData.ID;

                string IDs = RequestData.DocumentIDs;

                string[] IDArray = null;
                if (IDs != null)
                {
                    IDArray = IDs.Split(',');
                }
                bool IDUpdate = false;
                if (IDArray != null && IDArray.Length == SKUMasterTypesList.Count)
                {
                    IDUpdate = true;
                }
                int Index = 0;

                foreach (SKUMasterTypes objSKUMasterTypes in SKUMasterTypesList)
                {

                    sSql.Append("<SKUMasterTypes>");
                    if (IDUpdate)
                    {
                        sSql.Append("<ID>" + Convert.ToInt64(IDArray[Index]) + "</ID>");
                    }
                    else
                    {
                        sSql.Append("<ID>" + (objSKUMasterTypes.ID) + "</ID>");
                    }
                    //sSql.Append("<ID>" + (objSKUMasterTypes.ID) + "</ID>");
                    sSql.Append("<SKUCode>" + (objSKUMasterTypes.SKUCode) + "</SKUCode>");
                    sSql.Append("<SKUName>" + (objSKUMasterTypes.SKUName) + "</SKUName>");
                    sSql.Append("<BarCode>" + (objSKUMasterTypes.BarCode) + "</BarCode>");
                    sSql.Append("<StyleID>" + (objSKUMasterTypes.StyleID) + "</StyleID>");
                    sSql.Append("<StyleCode>" + (objSKUMasterTypes.StyleCode) + "</StyleCode>");
                    sSql.Append("<DesignID>" + (objSKUMasterTypes.DesignID) + "</DesignID>");
                    sSql.Append("<BrandID>" + (objSKUMasterTypes.BrandID) + "</BrandID>");
                    sSql.Append("<SubBrandID>" + (objSKUMasterTypes.SubBrandID) + "</SubBrandID>");
                    sSql.Append("<CollectionID>" + (objSKUMasterTypes.CollectionID) + "</CollectionID>");
                    sSql.Append("<ArmadaCollectionID>" + (objSKUMasterTypes.ArmadaCollectionID) + "</ArmadaCollectionID>");
                    sSql.Append("<DivisionID>" + (objSKUMasterTypes.DivisionID) + "</DivisionID>");
                    sSql.Append("<ProductGroupID>" + (objSKUMasterTypes.ProductGroupID) + "</ProductGroupID>");
                    sSql.Append("<ProductSubGroupID>" + (objSKUMasterTypes.ProductSubGroupID) + "</ProductSubGroupID>");
                    sSql.Append("<SeasonID>" + (objSKUMasterTypes.SeasonID) + "</SeasonID>");
                    sSql.Append("<YearID>" + (objSKUMasterTypes.YearID) + "</YearID>");
                    sSql.Append("<ProductLineID>" + (objSKUMasterTypes.ProductLineID) + "</ProductLineID>");
                    sSql.Append("<StyleStatusID>" + (objSKUMasterTypes.StyleStatusID) + "</StyleStatusID>");
                    sSql.Append("<DesignerID>" + (objSKUMasterTypes.DesignerID) + "</DesignerID>");
                    sSql.Append("<PurchasePriceListID>" + (objSKUMasterTypes.PurchasePriceListID) + "</PurchasePriceListID>");
                    sSql.Append("<PurchasePrice>" + (objSKUMasterTypes.PurchasePrice) + "</PurchasePrice>");
                    sSql.Append("<PurchaseCurrencyID>" + (objSKUMasterTypes.PurchaseCurrencyID) + "</PurchaseCurrencyID>");
                    sSql.Append("<RRPPrice>" + (objSKUMasterTypes.RRPPrice) + "</RRPPrice>");
                    sSql.Append("<RRPCurrencyID>" + (objSKUMasterTypes.RRPCurrencyID) + "</RRPCurrencyID>");
                    sSql.Append("<ScaleID>" + (objSKUMasterTypes.ScaleID) + "</ScaleID>");
                    sSql.Append("<ColorID>" + (objSKUMasterTypes.ColorID) + "</ColorID>");
                    sSql.Append("<SizeID>" + (objSKUMasterTypes.SizeID) + "</SizeID>");
                    sSql.Append("<SupplierBarcode>" + (objSKUMasterTypes.SupplierBarcode) + "</SupplierBarcode>");
                    sSql.Append("<ArabicSKU>" + (objSKUMasterTypes.ArabicSKU) + "</ArabicSKU>");
                    sSql.Append("<Remarks>" + (objSKUMasterTypes.Remarks) + "</Remarks>");
                    sSql.Append("<CreateBy>" + (objSKUMasterTypes.CreateBy) + "</CreateBy>");
                    sSql.Append("<UpdateBy>" + (objSKUMasterTypes.UpdateBy) + "</UpdateBy>");
                    sSql.Append("<SizeCode>" + (objSKUMasterTypes.SizeCode) + "</SizeCode>");
                    sSql.Append("<ColorCode>" + (objSKUMasterTypes.ColorCode) + "</ColorCode>");
                    sSql.Append("<ExchangeRate>" + (objSKUMasterTypes.ExchangeRate) + "</ExchangeRate>");
                    sSql.Append("<Active>" + (objSKUMasterTypes.Active) + "</Active>");
                    sSql.Append("<SegamentationID>" + (objSKUMasterTypes.SegamentationID) + "</SegamentationID>");
                    sSql.Append("<SCN>" + (objSKUMasterTypes.SCN) + "</SCN>");
                    sSql.Append("<BrandCode>" + (objSKUMasterTypes.BrandCode) + "</BrandCode>");
                    sSql.Append("<DivisionCode>" + (objSKUMasterTypes.DivisionCode) + "</DivisionCode>");
                    sSql.Append("<ProductGroupCode>" + (objSKUMasterTypes.ProductGroupCode) + "</ProductGroupCode>");
                    sSql.Append("<YearCode>" + (objSKUMasterTypes.YearCode) + "</YearCode>");
                    sSql.Append("<DesignCode>" + (objSKUMasterTypes.DesignCode) + "</DesignCode>");
                    sSql.Append("<SubBrandCode>" + (objSKUMasterTypes.SubBrandCode) + "</SubBrandCode>");
                    sSql.Append("<CollectionCode>" + (objSKUMasterTypes.CollectionCode) + "</CollectionCode>");
                    sSql.Append("<ArmadaCollectionCode>" + (objSKUMasterTypes.ArmadaCollectionCode) + "</ArmadaCollectionCode>");
                    sSql.Append("<SeasonCode>" + (objSKUMasterTypes.SeasonCode) + "</SeasonCode>");
                    sSql.Append("<ProductLineCode>" + (objSKUMasterTypes.ProductLineCode) + "</ProductLineCode>");
                    sSql.Append("<StyleStatusCode>" + (objSKUMasterTypes.StyleStatusCode) + "</StyleStatusCode>");
                    sSql.Append("<DesignerCode>" + (objSKUMasterTypes.DesignerCode) + "</DesignerCode>");
                    sSql.Append("<PurchasePriceListCode>" + (objSKUMasterTypes.PurchasePriceListCode) + "</PurchasePriceListCode>");
                    sSql.Append("<PurchasePriceCurrencyCode>" + (objSKUMasterTypes.PurchasePriceCurrencyCode) + "</PurchasePriceCurrencyCode>");
                    sSql.Append("<RRPCurrencyCode>" + (objSKUMasterTypes.RRPCurrencyCode) + "</RRPCurrencyCode>");
                    sSql.Append("<ScaleCode>" + (objSKUMasterTypes.ScaleCode) + "</ScaleCode>");
                    sSql.Append("<SegmentationCode>" + (objSKUMasterTypes.SegmentationCode) + "</SegmentationCode>");
                    sSql.Append("<ProductSubGroupCode>" + (objSKUMasterTypes.ProductSubGroupCode) + "</ProductSubGroupCode>");
                    sSql.Append("<ProductCode>" + (objSKUMasterTypes.ProductCode) + "</ProductCode>");
                    sSql.Append("</SKUMasterTypes>");
                    Index++;
                }

                SqlParameter CollectionData = _CommandObj.Parameters.Add("@CollectionData", SqlDbType.Xml);
                CollectionData.Direction = ParameterDirection.Input;
                CollectionData.Value = sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");


                SqlParameter StylePricingData = _CommandObj.Parameters.Add("@StylePricingData", SqlDbType.Xml);
                StylePricingData.Direction = ParameterDirection.Input;
                if (RequestData.StylePricingList != null && RequestData.StylePricingList.Count > 0)
                {
                    StylePricingData.Value = GenerateStylePricingXML(RequestData.StylePricingList);
                }
                else
                {
                    StylePricingData.Value = String.Empty;
                }

                var SKUID1 = _CommandObj.Parameters.Add("@SKUID", SqlDbType.Int);
                SKUID1.Direction = ParameterDirection.Output;

                var SKUIDs = _CommandObj.Parameters.Add("@SKUIDs", SqlDbType.VarChar, int.MaxValue);
                SKUIDs.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                //_CommandObj.CommandTimeout = 300000;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    int SkuID = 0;
                    //var SkuID1 = SKUIDs.Value.ToString();
                    //if (SkuID1 != null)
                    //{
                    //    SkuID = Convert.ToInt32(SkuID1);
                    //}
                    //SkuID = Convert.ToInt32(SKUID1);
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "SKU Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;

                    if (SKUIDs.Value != DBNull.Value)
                    {
                        ResponseData.ReturnIDs = SKUIDs.Value.ToString();
                        //ResponseData.IDs = SKUIDs.Value.ToString();
                        //ResponseData.DocumentIDs = SKUIDs.Value.ToString();
                    }
                    else
                    {
                        //var SkuID1 = SKUID1.Value.ToString();
                        // SkuID = Convert.ToInt32(SkuID1);
                    }
                    if (RequestData.BaseEntry == "SKUMaster")
                    {
                        SKUImageInsert(RequestData.ItemImageMasterList, SkuID);
                    }
                    else
                    {
                        SKUImageInsert(RequestData.ItemImageMasterList, SkuID);
                    }
                    if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
                    {
                        if (RunningNo > 0)
                        {
                            UpdateRunningNo(DetailID, RunningNo);
                        }
                    }
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "SKU Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                }
            }

            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public bool UpdateRunningNo(int DetailID, long RunningNo)
        {

            bool IsSuccess = false;
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
            try
            {
                string sSql = "update BarcodeSettings set RunningNo= " + RunningNo + " where ID=" + DetailID + " ";
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return IsSuccess;

        }
        public bool SKUImageInsert(List<ItemImageMaster> ItemImageMasterList, int ID)
        {
            bool objBool = false;

            var sqlCommon = new MsSqlCommon();
            sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
            try
            {
                if (ID > 0)
                {
                    string ssql = "Delete  SKUImages where SKUID='" + ID + "'";
                    _CommandObj = new SqlCommand(ssql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    _CommandObj.ExecuteNonQuery();
                    _CommandObj.Dispose();
                }

                if (ItemImageMasterList != null)
                {
                    foreach (ItemImageMaster objItemImageMaster in ItemImageMasterList)
                    {
                        _CommandObj = new SqlCommand("InsertSKUImages", _ConnectionObj);

                        SqlParameter DesignID = _CommandObj.Parameters.Add("@DesignID", SqlDbType.Int);
                        DesignID.Direction = ParameterDirection.Input;
                        DesignID.Value = objItemImageMaster.DesignID;

                        SqlParameter StyleID = _CommandObj.Parameters.Add("@StyleID", SqlDbType.Int);
                        StyleID.Direction = ParameterDirection.Input;
                        StyleID.Value = objItemImageMaster.StyleID;

                        SqlParameter SKUID = _CommandObj.Parameters.Add("@SKUID", SqlDbType.Int);
                        SKUID.Direction = ParameterDirection.Input;
                        SKUID.Value = ID;

                        SqlParameter Stylecode = _CommandObj.Parameters.Add("@StyleCode", SqlDbType.NVarChar);
                        Stylecode.Direction = ParameterDirection.Input;
                        Stylecode.Value = objItemImageMaster.StyleCode;

                        //SqlParameter SkuCode = _CommandObj.Parameters.Add("@SkuCode", SqlDbType.NVarChar);
                        //SkuCode.Direction = ParameterDirection.Input;
                        //SkuCode.Value = objItemImageMaster.SkuCode;

                        SqlParameter SKUImage = _CommandObj.Parameters.Add("@SKUImage", SqlDbType.Image);
                        SKUImage.Direction = ParameterDirection.Input;
                        SKUImage.Value = objItemImageMaster.SKUImage;

                        SqlParameter IsDefaultImage = _CommandObj.Parameters.Add("@IsDefaultImage", SqlDbType.Bit);
                        IsDefaultImage.Direction = ParameterDirection.Input;
                        IsDefaultImage.Value = objItemImageMaster.IsDefaultImage;

                        _CommandObj.CommandType = CommandType.StoredProcedure;
                        _CommandObj.ExecuteNonQuery();
                    }
                    objBool = true;
                }
            }
            catch
            {

            }
            finally
            {
                _CommandObj.Dispose();
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return objBool;
        }
        //public string ItemImageMasterDetailsListXML(List<ItemImageMaster> ItemImageMasterList)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    foreach (ItemImageMaster objItemImageMaster in ItemImageMasterList)
        //    {
        //        sSql.Append("<SKUWithItemImageData>");
        //        sSql.Append("<ID>" + (objItemImageMaster.ID) + "</ID>");
        //        //sSql.Append("<SKUID>" + (objItemImageMaster.SKUID) + "</SKUID>");
        //        sSql.Append("<ItemImage>" + objItemImageMaster.ItemImage + "</ItemImage>");

        //        sSql.Append("</SKUWithItemImageData>");
        //    }
        //    return sSql.ToString();
        //} 
        public override BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateSKUMasterRequest)RequestObj;
            var ResponseData = new UpdateSKUMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateSKUMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                //_CommandObj.Parameters.AddWithValue("@ID", RequestData.SKUMasterTypesRecord.ID);
                //_CommandObj.Parameters.AddWithValue("@ItemNo", RequestData.SKUMasterTypesRecord.ItemNo);
                //_CommandObj.Parameters.AddWithValue("@ItemDescription", RequestData.SKUMasterTypesRecord.ItemDescription);
                //_CommandObj.Parameters.AddWithValue("@ForeignName", RequestData.SKUMasterTypesRecord.ForeignName);
                //_CommandObj.Parameters.AddWithValue("@ItemTypeID", RequestData.SKUMasterTypesRecord.ItemTypeID);
                //_CommandObj.Parameters.AddWithValue("@ItemGroupID", RequestData.SKUMasterTypesRecord.ItemGroupID);
                //_CommandObj.Parameters.AddWithValue("@ModelNo", RequestData.SKUMasterTypesRecord.ModelNo);
                //_CommandObj.Parameters.AddWithValue("@Barcode", RequestData.SKUMasterTypesRecord.Barcode);
                //_CommandObj.Parameters.AddWithValue("@InventoryItem", RequestData.SKUMasterTypesRecord.InventoryItem);
                //_CommandObj.Parameters.AddWithValue("@SalesItem", RequestData.SKUMasterTypesRecord.SalesItem);
                //_CommandObj.Parameters.AddWithValue("@PurchaseItem", RequestData.SKUMasterTypesRecord.PurchaseItem);
                //_CommandObj.Parameters.AddWithValue("@SCN", RequestData.SKUMasterTypesRecord.SCN);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "SKU Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "SKU Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
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

        public override BaseResponseType DeleteRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SKUMasterTypesData = new SKUMasterTypes();
            var RequestData = (DeleteSKUMasterRequest)RequestObj;
            var ResponseData = new DeleteSKUMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);



                string sSql = "Delete from SKUMaster where ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);


                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "SKU Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "SKU Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var SKUMasterTypes = new SKUMasterTypes();
            var SKUMasterTypesList = new List<SKUMasterTypes>();
            var RequestData = (SelectByIDSKUMasterRequest)RequestObj;
            var ResponseData = new SelectByIDSKUMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.Source == "Sales")
                {
                    sSql.Append("Select top 1 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName FROM SKUMaster  SKU with(NoLock)  ");
                    sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
                    sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
                    sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
                    sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
                    sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
                    sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID  ");
                    sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
                    sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
                    sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
                    sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
                    sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
                    sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ");
                    sSql.Append("where SKUCode='" + RequestData.SkuCode + "' ");
                }
                else
                {
                    sSql.Append("Select top 1 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName,SM2.ID As StyleIDBYStyle FROM SKUMaster  SKU with(NoLock) ");
                    sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
                    sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
                    sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
                    sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
                    sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
                    sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID   ");
                    sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
                    sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
                    sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
                    sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
                    sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
                    sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ");
                    sSql.Append("LEFT JOIN StyleMaster SM2 with(NoLock) ON SKU.StyleCode=SM2.StyleCode  ");

                    sSql.Append("where sku.ID='" + RequestData.ID + "' ");
                }
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.BarCode = objReader["BarCode"].ToString();
                        if (objReader["StyleID"].ToString() == "0")
                        {
                            objSKUMasterTypes.StyleID = Convert.ToInt32(objReader["StyleIDBYStyle"].ToString());
                        }
                        else
                        {
                            objSKUMasterTypes.StyleID = Convert.ToInt32(objReader["StyleID"].ToString());
                        }
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : string.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : string.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.SegamentationID = objReader["SegamentationID"] != DBNull.Value ? Convert.ToInt32(objReader["SegamentationID"].ToString()) : 0;
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        //objSKUMasterTypes.ItemImage = objReader["ItemImage"].ToString();
                        objSKUMasterTypes.ExchangeRate = Convert.ToDecimal(objReader["ExchangeRate"]);



                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.Remarks = objReader["Remarks"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        ResponseData.SKUMasterTypesData = objSKUMasterTypes;

                        SKUMasterTypesList.Add(objSKUMasterTypes);
                    }
                    ResponseData.SKUMasterTypesList = SKUMasterTypesList;
                    ResponseData.ResponseDynamicData = SKUMasterTypesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)RequestObj;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;

                if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
                {
                    sSql.Append("Select TOP 100 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName,AFM.UseSeperator FROM SKUMaster  SKU with(NoLock)  ");
                    sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
                    sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
                    sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
                    sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
                    sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
                    sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID  ");
                    sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
                    sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
                    sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
                    sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
                    sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
                    sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ORDER BY SKU.ID DESC");

                    sCommand = sSql.ToString();

                    _CommandObj = new SqlCommand(sCommand, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }
                else if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    _CommandObj = new SqlCommand("API_SearchProducts", _ConnectionObj);
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                    if (RequestData.Mode != null && RequestData.Mode != string.Empty)
                    {
                        _CommandObj.Parameters.AddWithValue("@Mode", RequestData.Mode);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@Mode", DBNull.Value);
                    }
                    if (RequestData.Count == 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@Count", 100);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@Count", RequestData.Count);
                    }
                    _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                    _CommandObj.CommandType = CommandType.StoredProcedure;

                }
                else
                {
                    sSql.Append("Select top 10 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName,AFM.UseSeperator FROM SKUMaster  SKU with(NoLock)  ");
                    sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
                    sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
                    sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
                    sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
                    sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
                    sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID  ");
                    sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
                    sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
                    sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
                    sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
                    sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
                    sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ");

                    sCommand = sSql.ToString();

                    _CommandObj = new SqlCommand(sCommand, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }



                //_CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        //if (RequestObj.RequestFrom == Enums.RequestFrom.StoreSales)
                        //{
                        //    var objSKUMasterTypes = new SKUMasterTypes();

                        //    objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //    objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        //    objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        //    objSKUMasterTypes.SKUName = objReader["SKUName"].ToString(); 
                        //    objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        //    objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        //    objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        //    objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        //    objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        //    objSKUMasterTypes.Year = objReader["Year"].ToString();
                        //    objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        //    objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        //    objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();

                        //    SKUMasterTypes.Add(objSKUMasterTypes);                            
                        //}
                        //else
                        //{
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        if (RequestData.StoreIDs != null)
                        {
                            SqlConnection con = new SqlConnection();
                            //con = RequestData.ConnectionString;
                            sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                            //con.Open();
                            var sql = "SELECT Isnull(SUM(InQty),0) - Isnull(SUM(OutQty),0) FROM TransactionLog WHERE SKUCode = '" + SKUCode + "' and StoreID=" + RequestData.StoreIDs + "";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            objSKUMasterTypes.Stock = (int)cmd.ExecuteScalar();
                            con.Close();
                        }
                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                            objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                            objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        }
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();                        
                        objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        
                        checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        if (RequestData.StoreIDs != null)
                        {
                            objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);
                        }

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                            objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                                objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                            }
                        }
                        SKUMasterTypes.Add(objSKUMasterTypes);                      
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;                   
                }
                else
                {
                    if (checknontrade == true)
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.DisplayMessage = "You can't sell Non-Trading Items";                   
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        //public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        //{
        //    string SKUCode = "";
        //    var SKUMasterTypes = new List<SKUMasterTypes>();
        //    var _ImageProcess = new DataBaseImageProcess();

        //    var RequestData = (SelectAllSKUMasterRequest)RequestObj;
        //    var ResponseData = new SelectAllSKUMasterResponse();

        //    SqlDataReader objReader;

        //    var sqlCommon = new MsSqlCommon();
        //    try
        //    {
        //        _ConnectionString = RequestData.ConnectionString;
        //        _RequestFrom = RequestData.RequestFrom;

        //        sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
        //        var sSql = new StringBuilder();
        //        var sCommand = string.Empty;

        //        if (RequestData.RequestFrom == Enums.RequestFrom.DefaultLoad)
        //        {
        //            sSql.Append("Select TOP 100 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName,AFM.UseSeperator FROM SKUMaster  SKU with(NoLock)  ");
        //            sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
        //            sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
        //            sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
        //            sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
        //            sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
        //            sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID  ");
        //            sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
        //            sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
        //            sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
        //            sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
        //            sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
        //            sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ORDER BY SKU.ID DESC");

        //            sCommand = sSql.ToString();

        //            _CommandObj = new SqlCommand(sCommand, _ConnectionObj);
        //            _CommandObj.CommandType = CommandType.Text;
        //        }
        //        else if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
        //        {
        //            _CommandObj = new SqlCommand("API_SearchProducts", _ConnectionObj);
        //            _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
        //            if (RequestData.Mode != null && RequestData.Mode != string.Empty)
        //            {
        //                _CommandObj.Parameters.AddWithValue("@Mode", RequestData.Mode);
        //            }
        //            else
        //            {
        //                _CommandObj.Parameters.AddWithValue("@Mode", DBNull.Value);
        //            }
        //            if (RequestData.Count == 0)
        //            {
        //                _CommandObj.Parameters.AddWithValue("@Count", 100);
        //            }
        //            else
        //            {
        //                _CommandObj.Parameters.AddWithValue("@Count", RequestData.Count);
        //            }
        //            _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
        //            _CommandObj.CommandType = CommandType.StoredProcedure;

        //        }
        //        else
        //        {
        //            sSql.Append("Select SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName,AFM.UseSeperator FROM SKUMaster  SKU with(NoLock)  ");
        //            sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
        //            sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
        //            sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
        //            sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
        //            sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
        //            sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID  ");
        //            sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
        //            sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
        //            sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
        //            sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
        //            sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
        //            sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ");

        //            sCommand = sSql.ToString();

        //            _CommandObj = new SqlCommand(sCommand, _ConnectionObj);
        //            _CommandObj.CommandType = CommandType.Text;
        //        }



        //        //_CommandObj.CommandType = CommandType.Text;
        //        objReader = _CommandObj.ExecuteReader();
        //        if (objReader.HasRows)
        //        {
        //            while (objReader.Read())
        //            {
        //                //if (RequestObj.RequestFrom == Enums.RequestFrom.StoreSales)
        //                //{
        //                //    var objSKUMasterTypes = new SKUMasterTypes();

        //                //    objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
        //                //    objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
        //                //    objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
        //                //    objSKUMasterTypes.SKUName = objReader["SKUName"].ToString(); 
        //                //    objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
        //                //    objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
        //                //    objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
        //                //    objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
        //                //    objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
        //                //    objSKUMasterTypes.Year = objReader["Year"].ToString();
        //                //    objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
        //                //    objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
        //                //    objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();

        //                //    SKUMasterTypes.Add(objSKUMasterTypes);                            
        //                //}
        //                //else
        //                //{
        //                var objSKUMasterTypes = new SKUMasterTypes();

        //                objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
        //                objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
        //                objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
        //                SKUCode = objReader["SKUCode"].ToString();
        //                SqlConnection con = new SqlConnection();
        //                //con = RequestData.ConnectionString;
        //                sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
        //                //con.Open();
        //                var sql = "SELECT SUM(InQty) - SUM(OutQty) FROM TransactionLog WHERE SKUCode = '" + SKUCode + "' and StoreID=" + RequestData.StoreIDs + "";
        //                SqlCommand cmd = new SqlCommand(sql, con);
        //                objSKUMasterTypes.Stock = (int)cmd.ExecuteScalar();
        //                con.Close();
        //                if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
        //                {
        //                    objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
        //                    objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
        //                    objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
        //                }
        //                objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
        //                objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
        //                objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
        //                objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
        //                objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
        //                objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
        //                objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

        //                objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
        //                objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
        //                objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

        //                objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
        //                objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
        //                objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

        //                objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
        //                objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
        //                objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


        //                objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
        //                objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
        //                objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

        //                objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
        //                objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
        //                objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

        //                objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
        //                objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
        //                objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
        //                objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
        //                objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
        //                objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
        //                objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
        //                objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
        //                //objSKUMasterTypes.ItemImage = objReader["ItemImage"].ToString();
        //                objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


        //                objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
        //                objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
        //                objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
        //                objSKUMasterTypes.Year = objReader["Year"].ToString();
        //                objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
        //                objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
        //                objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
        //                objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
        //                objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
        //                objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
        //                objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
        //                objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

        //                objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
        //                objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
        //                objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
        //                objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
        //                objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
        //                objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


        //                //objSKUMasterTypes.IsNonTrading = Convert.ToBoolean(objReader["IsNonTrading"] != DBNull.Value ? Convert.ToBoolean(objReader["IsNonTrading"]) : false);
        //                checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

        //                objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
        //                objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);

        //                if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
        //                {
        //                    objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

        //                    SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
        //                    objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
        //                    objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
        //                    objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

        //                    SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
        //                    objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


        //                    if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
        //                    {
        //                        objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
        //                        System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
        //                        objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
        //                    }
        //                }



        //                SKUMasterTypes.Add(objSKUMasterTypes);
        //                //}
        //            }


        //            /*if (checknontrade == true)
        //            {
        //                ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //                ResponseData.DisplayMessage = "You can't sell Non-Trading Items";
        //                //ResponseData.SKUMasterTypesList = SKUMasterTypes;
        //                //ResponseData.ResponseDynamicData = SKUMasterTypes;
        //            }
        //            else
        //            {*/
        //            ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //            ResponseData.SKUMasterTypesList = SKUMasterTypes;
        //            ResponseData.ResponseDynamicData = SKUMasterTypes;
        //            //}


        //        }
        //        else
        //        {
        //            if (checknontrade == true)
        //            {
        //                ResponseData.StatusCode = Enums.OpStatusCode.Success;
        //                ResponseData.DisplayMessage = "You can't sell Non-Trading Items";
        //                //ResponseData.SKUMasterTypesList = SKUMasterTypes;
        //                //ResponseData.ResponseDynamicData = SKUMasterTypes;
        //            }
        //            else
        //            {

        //                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
        //                ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
        //        ResponseData.DisplayMessage = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlCommon.CloseConnection(_ConnectionObj);

        //    }
        //    return ResponseData;
        //}
        public override SelectAllSKUImagesResponse SelectAllSKUImages(SelectByALLSKUImagesRequest RequestObj)
        {
            var SKUMasterTypes = new List<ItemImageMaster>();
            var RequestData = (SelectByALLSKUImagesRequest)RequestObj;
            var ResponseData = new SelectAllSKUImagesResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;

                if (RequestData.RequestFrom == Enums.RequestFrom.StoreSales || RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sSql = "exec GetDefaultSKUImage " + RequestData.SKUID + "," + RequestData.StyleID;
                }
                else
                {
                    sSql = "exec GetSKUImages " + RequestData.SKUID;
                }
                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objItemImage = new ItemImageMaster();
                        objItemImage.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objItemImage.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;
                        objItemImage.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objItemImage.SKUImage = objReader["SKUImage"] != DBNull.Value ? (byte[])objReader["SKUImage"] : null;
                        objItemImage.IsDefaultImage = objReader["IsDefaultImage"] != DBNull.Value ? Convert.ToBoolean(objReader["IsDefaultImage"]) : false;

                        SKUMasterTypes.Add(objItemImage);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUImageList = SKUMasterTypes;

                    ResponseData.ResponseDynamicData = SKUMasterTypes;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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
            var SKUMasterTypes = new SKUMasterTypes();
            var SKUMasterTypesList = new List<SKUMasterTypes>();
            var RequestData = (SelectByIDsSKUMasterRequest)RequestObj;
            var ResponseData = new SelectByIDsSKUMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                //if (RequestData.Source == "Sales")
                //{
                //    sSql.Append("Select top 1 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName FROM SKUMaster  SKU with(NoLock)  ");
                //    sSql.Append("LEFT  JOIN  DesignMaster DM ON SKU.DesignID=DM.ID   ");
                //    sSql.Append("LEFT  JOIN  StyleMaster SM ON SKU.StyleID=SM.ID  ");
                //    sSql.Append("LEFT JOIN AFSegamationMaster AFM ON SKU.SegamentationID=AFM.ID  ");
                //    sSql.Append("LEFT JOIN YearMaster YM ON SKU.YearID=YM.ID  ");
                //    sSql.Append("LEFT JOIN BrandMaster BM ON SKU.BrandID =BM.ID    ");
                //    sSql.Append("LEFT JOIN SeasonMaster SEM1 ON SKU.SeasonID=SEM1.ID  ");
                //    sSql.Append("LEFT JOIN ProductGroupMaster PGM ON SKU.ProductGroupID =PGM.ID    ");
                //    sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM ON SKU.ProductSubGroupID =PSGM.ID    ");
                //    sSql.Append("LEFT JOIN SubBrandMaster SBM ON SKU.SubBrandID =SBM.ID    ");
                //    sSql.Append("LEFT JOIN CollectionMaster CM ON SKU.CollectionID=CM.ID  ");
                //    sSql.Append("LEFT JOIN DivisionMaster DM1 ON SKU.DivisionID=DM1.ID   ");
                //    sSql.Append("LEFT JOIN ScaleMaster SM1 ON SKU.ScaleID=SM1.ID  ");
                //    sSql.Append("where SKUCode='" + RequestData.SkuCode + "' ");
                //}
                //else
                //{
                sSql.Append("Select top 1 SKU.*,DM.DesignCode,SM.StyleCode,BM.BrandName,AFM.AFSegamationName,YM.Year,SEM1.SeasonName,PGM.ProductGroupName,PSGM.ProductSubGroupName,SBM.SubBrandName,CM.CollectionName,DM1.DivisionName,SM1.ScaleName,SM2.ID As StyleIDBYStyle FROM SKUMaster  SKU with(NoLock) ");
                sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
                sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
                sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
                sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
                sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
                sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID   ");
                sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
                sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
                sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
                sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
                sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
                sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ");
                sSql.Append("LEFT JOIN StyleMaster SM2 with(NoLock) ON SKU.StyleCode=SM2.StyleCode  ");

                sSql.Append("where sku.ID in (" + RequestData.IDs + ")'");
                //}
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        if (objReader["StyleID"].ToString() == "0")
                        {
                            objSKUMasterTypes.StyleID = objReader["StyleIDBYStyle"] != DBNull.Value ? Convert.ToInt32(objReader["StyleIDBYStyle"].ToString()) : 0;
                        }
                        else
                        {
                            objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        }
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.SegamentationID = objReader["SegamentationID"] != DBNull.Value ? Convert.ToInt32(objReader["SegamentationID"].ToString()) : 0;
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        //objSKUMasterTypes.ItemImage = objReader["ItemImage"].ToString();
                        objSKUMasterTypes.ExchangeRate = Convert.ToDecimal(objReader["ExchangeRate"]);



                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;



                        SKUMasterTypesList.Add(objSKUMasterTypes);

                    }
                    ResponseData.SKUMasterTypesList = SKUMasterTypesList;
                    ResponseData.ResponseDynamicData = SKUMasterTypesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        private string GenerateStylePricingXML(List<StylePricing> StylePricingList)
        {
            StringBuilder sXml = new StringBuilder();
            foreach (StylePricing objStylePricing in StylePricingList)
            {
                sXml.Append("<StylePricingData>");
                sXml.Append("<ID>" + objStylePricing.ID + "</ID>");
                sXml.Append("<SKUID>" + objStylePricing.SKUID + "</SKUID>");
                sXml.Append("<SKUCode>" + objStylePricing.SKUCode + "</SKUCode>");
                sXml.Append("<PriceListID>" + objStylePricing.PriceListID + "</PriceListID>");
                sXml.Append("<PriceListCurrency>" + objStylePricing.PriceListCurrency + "</PriceListCurrency>");
                sXml.Append("<Price>" + objStylePricing.Price + "</Price>");
                sXml.Append("<IsManualEntry>" + objStylePricing.IsManualEntry + "</IsManualEntry>");
                sXml.Append("<CreateBy>" + objStylePricing.CreateBy + "</CreateBy>");
                sXml.Append("<AppVersion>" + objStylePricing.AppVersion + "</AppVersion>");
                sXml.Append("</StylePricingData>");
            }
            return sXml.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override GetStylePricingBySKUCodeResponse SelectGetStylePricingBySKUCode(GetStylePricingBySKUCodeRequest RequestObj)
        {
            var SKUMasterTypes = new SKUMasterTypes();
            var StylePricingList = new List<StylePricing>();
            var RequestData = (GetStylePricingBySKUCodeRequest)RequestObj;
            var ResponseData = new GetStylePricingBySKUCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                sSql.Append("Select *  FROM StylePricing with(NoLock)  ");
                sSql.Append("where SKUCode='" + RequestData.SKUCode + "' ");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                string PriceListIDs = string.Empty;

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStylePricing = new StylePricing();

                        objStylePricing.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStylePricing.SKUCode = objReader["SKUCode"].ToString();
                        objStylePricing.PriceListID = objReader["PriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListID"].ToString()) : 0;
                        objStylePricing.PriceListCurrency = objReader["PriceListCurrency"] != DBNull.Value ? Convert.ToInt32(objReader["PriceListCurrency"]) : 0;
                        objStylePricing.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        StylePricingList.Add(objStylePricing);

                    }
                    ResponseData.StylePricingList = StylePricingList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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
        public override SelectSKUByStyleIDResponse SelectByStyleID(SelectSKUByStyleIDRequest RequestObj)
        {
            var SKUMasterTypes = new SKUMasterTypes();
            var SKUMasterTypesList = new List<SKUMasterTypes>();
            var RequestData = (SelectSKUByStyleIDRequest)RequestObj;
            var ResponseData = new SelectSKUByStyleIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sSql.Append("Select *  FROM SKUMaster with(NoLock)  ");
                sSql.Append("where StyleID='" + RequestData.StyleID + "' ");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                string PriceListIDs = string.Empty;

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.BarCode = objReader["BarCode"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        SKUMasterTypesList.Add(objSKUMasterTypes);

                    }
                    ResponseData.SKUMasterTypesList = SKUMasterTypesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SaveSKUMasterResponse ImportExcelInsert(SaveSKUMasterRequest RequestObj)
        {
            SaveSKUMasterRequest RequestData = (SaveSKUMasterRequest)RequestObj;
            SaveSKUMasterResponse ResponseData = new SaveSKUMasterResponse();


            StringBuilder sSql = new StringBuilder();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertImportSkuMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ImportExcelDetails = _CommandObj.Parameters.Add("@ImportDetails", SqlDbType.Xml);
                ImportExcelDetails.Direction = ParameterDirection.Input;
                ImportExcelDetails.Value = ImportDetailXML(RequestData.ImportExcelList);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "SKU Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "SKU Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
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
        public string ImportDetailXML(List<SKUMasterTypes> ImportDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            //int Index = 0;
            //string[] IDArray = null;
            //if (IDs != null)
            //{
            //    IDArray = IDs.Split(',');
            //}
            //bool IDUpdate = false;
            //if (IDArray != null && IDArray.Length == ImportDetailList.Count)
            //{
            //    IDUpdate = true;
            //}

            foreach (SKUMasterTypes objImportExcelDetail in ImportDetailList)
            {//SKUCode	SKUName	BarCode	StyleID	DesignID	BrandID	SubBrandID	CollectionID	ArmadaCollectionID	DivisionID	ProductGroupID	ProductSubGroupID	SeasonID	YearID	ProductLineID	StyleStatusID	DesignerID	PurchasePriceListID	PurchasePrice	PurchaseCurrencyID	RRPPrice	RRPCurrencyID	ScaleID	ColorID	SizeID	ColorCode	SizeCode	Remarks	SegamentationID	Active	IsStoreSync	IsCountrySync	Origin	StyleCode	DesignCode	DefaultPrice	ExchangeRate

                sSql.Append("<SKUMaster>");
                //if (IDUpdate)
                //{
                //    sSql.Append("<ID>" + Convert.ToInt64(IDArray[Index]) + "</ID>");
                //}
                //else
                //{
                //    sSql.Append("<ID>0</ID>");
                //}     
                sSql.Append("<ID>0</ID>");
                sSql.Append("<SKUCode>" + (objImportExcelDetail.SKUCode) + "</SKUCode>");
                sSql.Append("<SKUName>" + (objImportExcelDetail.SKUName) + "</SKUName>");
                sSql.Append("<BarCode>" + (objImportExcelDetail.BarCode) + "</BarCode>");
                sSql.Append("<StyleID>" + (objImportExcelDetail.StyleID) + "</StyleID>");
                sSql.Append("<DesignID>" + (objImportExcelDetail.DesignID) + "</DesignID>");
                sSql.Append("<BrandID>" + (objImportExcelDetail.BrandID) + "</BrandID>");
                sSql.Append("<SubBrandID>" + (objImportExcelDetail.SubBrandID) + "</SubBrandID>");
                sSql.Append("<CollectionID>" + (objImportExcelDetail.CollectionID) + "</CollectionID>");
                sSql.Append("<ArmadaCollectionID>" + (objImportExcelDetail.ArmadaCollectionID) + "</ArmadaCollectionID>");
                sSql.Append("<DivisionID>" + (objImportExcelDetail.DivisionID) + "</DivisionID>");
                sSql.Append("<ProductGroupID>" + (objImportExcelDetail.ProductGroupID) + "</ProductGroupID>");
                sSql.Append("<ProductSubGroupID>" + (objImportExcelDetail.ProductSubGroupID) + "</ProductSubGroupID>");
                sSql.Append("<SeasonID>" + (objImportExcelDetail.SeasonID) + "</SeasonID>");
                sSql.Append("<YearID>" + (objImportExcelDetail.YearID) + "</YearID>");
                sSql.Append("<ProductLineID>" + (objImportExcelDetail.ProductLineID) + "</ProductLineID>");
                sSql.Append("<StyleStatusID>" + (objImportExcelDetail.StyleStatusID) + "</StyleStatusID>");
                sSql.Append("<DesignerID>" + (objImportExcelDetail.DesignerID) + "</DesignerID>");
                sSql.Append("<PurchasePriceListID>" + (objImportExcelDetail.PurchasePriceListID) + "</PurchasePriceListID>");
                sSql.Append("<PurchasePrice>" + (objImportExcelDetail.PurchasePrice) + "</PurchasePrice>");
                sSql.Append("<PurchaseCurrencyID>" + (objImportExcelDetail.PurchaseCurrencyID) + "</PurchaseCurrencyID>");
                sSql.Append("<RRPPrice>" + (objImportExcelDetail.RRPPrice) + "</RRPPrice>");
                sSql.Append("<RRPCurrencyID>" + (objImportExcelDetail.RRPCurrencyID) + "</RRPCurrencyID>");
                sSql.Append("<ScaleID>" + (objImportExcelDetail.ScaleID) + "</ScaleID>");
                sSql.Append("<ColorID>" + (objImportExcelDetail.ColorID) + "</ColorID>");
                sSql.Append("<SizeID>" + (objImportExcelDetail.SizeID) + "</SizeID>");
                sSql.Append("<ColorCode>" + (objImportExcelDetail.ColorCode) + "</ColorCode>");
                sSql.Append("<SizeCode>" + (objImportExcelDetail.SizeCode) + "</SizeCode>");
                sSql.Append("<Remarks>" + (objImportExcelDetail.Remarks) + "</Remarks>");
                sSql.Append("<SegamentationID>" + (objImportExcelDetail.SegamentationID) + "</SegamentationID>");
                sSql.Append("<Active>" + (objImportExcelDetail.Active) + "</Active>");
                sSql.Append("<IsStoreSync>" + (objImportExcelDetail.IsStoreSync) + "</IsStoreSync>");
                sSql.Append("<IsCountrySync>" + (objImportExcelDetail.IsCountrySync) + "</IsCountrySync>");
                sSql.Append("<Origin>" + (objImportExcelDetail.Origin) + "</Origin>");
                sSql.Append("<StyleCode>" + (objImportExcelDetail.StyleCode) + "</StyleCode>");
                sSql.Append("<DesignCode>" + (objImportExcelDetail.DesignCode) + "</DesignCode>");
                sSql.Append("<DefaultPrice>" + (objImportExcelDetail.DefaultPrice) + "</DefaultPrice>");
                sSql.Append("<ExchangeRate>" + (objImportExcelDetail.ExchangeRate) + "</ExchangeRate>");
                sSql.Append("</SKUMaster>");
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");
        }

        public override SaveSKUMasterResponse UpdateImportBarCode(SaveSKUMasterRequest RequestObj)
        {
            SaveSKUMasterRequest RequestData = (SaveSKUMasterRequest)RequestObj;
            SaveSKUMasterResponse ResponseData = new SaveSKUMasterResponse();

            SqlDataReader objReader;
            StringBuilder sSql = new StringBuilder();
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateImportBarCode", _ConnectionObj);


                var ImportExcelDetails = _CommandObj.Parameters.Add("@ImportDetails", SqlDbType.Xml);
                ImportExcelDetails.Direction = ParameterDirection.Input;
                ImportExcelDetails.Value = ImportBarCodeDetailXML(RequestData.ImportExcelList);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;
                //_CommandObj.CommandTimeout = 300000;
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();
                objReader = _CommandObj.ExecuteReader();

                //string strStatusCode = StatusCode.Value.ToString();
                int sCode = 2;
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        ResponseData.ReturnIDs = objReader["IDS"] != DBNull.Value ? Convert.ToString(objReader["IDS"]) : "";
                        sCode = objReader["StatusCode"] != DBNull.Value ? Convert.ToInt32(objReader["StatusCode"]) : 0;
                        ResponseData.DisplayMessage = objReader["StatusMsg"] != DBNull.Value ? Convert.ToString(objReader["StatusMsg"]) : "";
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                if (sCode.ToString() == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Style Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.ReturnIDs = ReturnIDs.Value.ToString();
                }
                else if (sCode.ToString() == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "SKU Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "SKU Master");
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

        public string ImportBarCodeDetailXML(List<SKUMasterTypes> ImportExcelList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (SKUMasterTypes objImportExcelDetail in ImportExcelList)
            {
                sSql.Append("<SKUMaster>");
                sSql.Append("<ID>0</ID>");
                sSql.Append("<SKUCode>" + (objImportExcelDetail.SKUCode) + "</SKUCode>");

                sSql.Append("<BarCode>" + (objImportExcelDetail.BarCode) + "</BarCode>");
                sSql.Append("<SupplierBarcode>" + (objImportExcelDetail.SupplierBarcode) + "</SupplierBarcode>");
                sSql.Append("</SKUMaster>");
            }

            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;");

        }

        public override SelectColorCodeResponse SelectColorCodeSKUCode(SelectColorCodeRequest RequestObj)
        {
            var SKUMasterTypes = new SKUMasterTypes();
            var SKUMasterTypesList = new List<SKUMasterTypes>();
            var RequestData = (SelectColorCodeRequest)RequestObj;
            var ResponseData = new SelectColorCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sSql.Append("select * from skumaster ");
                sSql.Append("where stylecode='" + RequestData.Department + "-" + RequestData.Productcode + "'");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                string PriceListIDs = string.Empty;

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.StyleCode = objReader["StyleCode"].ToString();
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.BarCode = objReader["BarCode"].ToString();
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        // objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();                     
                        SKUMasterTypesList.Add(objSKUMasterTypes);

                    }
                    ResponseData.SKUMasterTypesList = SKUMasterTypesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectSizeCodeResponse SelectZizeCodeSKUCode(SelectSizeCodeRequest RequestObj)
        {
            var SKUMasterTypes = new SKUMasterTypes();
            var SKUMasterTypesList = new List<SKUMasterTypes>();
            var RequestData = (SelectSizeCodeRequest)RequestObj;
            var ResponseData = new SelectSizeCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sSql.Append("select * from skumaster ");
                sSql.Append("where stylecode='" + RequestData.Department + "-" + RequestData.Productcode + "'");

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                string PriceListIDs = string.Empty;

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.StyleCode = objReader["StyleCode"].ToString();
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.BarCode = objReader["BarCode"].ToString();
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        SKUMasterTypesList.Add(objSKUMasterTypes);

                    }
                    ResponseData.SKUMasterTypesList = SKUMasterTypesList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override GetStylePricingBySKUCodeResponse GetPriceBySKUCode(GetStylePricingBySKUCodeRequest RequestObj)
        {
            //var SKUMasterTypes = new SKUMasterTypes();
            var PriceRecord = new StylePricing();
            var RequestData = (GetStylePricingBySKUCodeRequest)RequestObj;
            var ResponseData = new GetStylePricingBySKUCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                sSql.Append("Select price  FROM StylePricing with(NoLock)  ");
                sSql.Append("where SKUCode='" + RequestData.Department + "-" + RequestData.ProductCode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "' and PriceListID='" + RequestData.PriceListID + "' ");

                //sSql.Append("Select price  FROM StylePricing with(NoLock)  ");
                //sSql.Append("where SKUCode='" + RequestData.Department+"-" + RequestData.ProductCode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "' ");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStylePricing = new StylePricing();
                        objStylePricing.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        ResponseData.PriceRecord = objStylePricing;
                        ResponseData.ResponseDynamicData = objStylePricing;

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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


        public override GetBarCodeBySKUResponse GetBarCodeBySKU(GetBarCodeBySKURequest RequestObj)
        {
            //var SKUMasterTypes = new SKUMasterTypes();
            var BarCodeRecord = new SKUMasterTypes();
            var RequestData = (GetBarCodeBySKURequest)RequestObj;
            var ResponseData = new GetBarCodeBySKUResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                if (RequestData.ChkSupplierBarcode == false)
                {

                    sSql.Append("select barcode from skumaster with(NoLock) where SKUCode='" + RequestData.Department + "-" + RequestData.ProductCode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "'");
                }
                else
                {
                    sSql.Append("select SupplierBarcode as barcode from skumaster with(NoLock) where SKUCode='" + RequestData.Department + "-" + RequestData.ProductCode + "-" + RequestData.ColorCode + "-" + RequestData.SizeCode + "'");
                }


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objBarcode = new SKUMasterTypes();
                        objBarcode.BarCode = objReader["BarCode"].ToString();
                        ResponseData.BarCodeData = objBarcode;
                        ResponseData.ResponseDynamicData = objBarcode;

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectAllSKUMasterResponse GetSKUWithoutStoreID(SelectAllSKUMasterRequest RequestObj)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)RequestObj;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;
                _CommandObj = new SqlCommand("API_SearchProductsWOStore", _ConnectionObj);
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.CommandType = CommandType.StoredProcedure;


            //_CommandObj = new SqlCommand(sCommand, _ConnectionObj);
                //_CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        SqlConnection con = new SqlConnection();
                        sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                        objSKUMasterTypes.SizeName = objReader["sizename"].ToString();

                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        //objSKUMasterTypes.ItemImage = objReader["ItemImage"].ToString();
                        objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        //objSKUMasterTypes.IsNonTrading = Convert.ToBoolean(objReader["IsNonTrading"] != DBNull.Value ? Convert.ToBoolean(objReader["IsNonTrading"]) : false);
                        checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);
                        SKUMasterTypes.Add(objSKUMasterTypes);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                    ResponseData.ResponseDynamicData = SKUMasterTypes;
                }
                else
                {

                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectSKUOnCountryResponse SelectSKUOnCountry(SelectSKUOnCountryRequest request)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectSKUOnCountryRequest)request;
            var ResponseData = new SelectSKUOnCountryResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;
                
            {
                    _CommandObj = new SqlCommand("[API_SearchProductsOnCountry]", _ConnectionObj);
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.Searchstring);
                    _CommandObj.Parameters.AddWithValue("@CountryID", RequestData.CountryID);                 
                  
                  
                    _CommandObj.CommandType = CommandType.StoredProcedure;

                }
               


                //_CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {                    
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        //SqlConnection con = new SqlConnection();                       
                        //sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);                  
                        //var sql = "SELECT SUM(InQty) - SUM(OutQty) FROM TransactionLog WHERE SKUCode = '" + SKUCode + "' and StoreID=" + RequestData.StoreIDs + "";
                        //SqlCommand cmd = new SqlCommand(sql, con);
                        //objSKUMasterTypes.Stock = (int)cmd.ExecuteScalar();
                        //con.Close();
                        //if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        //{
                            objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                            objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                            objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        //}
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        //objSKUMasterTypes.ItemImage = objReader["ItemImage"].ToString();
                        objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;


                        //objSKUMasterTypes.IsNonTrading = Convert.ToBoolean(objReader["IsNonTrading"] != DBNull.Value ? Convert.ToBoolean(objReader["IsNonTrading"]) : false);
                        checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                            objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                                objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                            }
                        }



                        SKUMasterTypes.Add(objSKUMasterTypes);
                        //}
                    }


                  
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                    ResponseData.ResponseDynamicData = SKUMasterTypes;
                  

                }
                else
                {
                    //if (checknontrade == true)
                    //{
                    //    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //    ResponseData.DisplayMessage = "You can't sell Non-Trading Items";
                    //    //ResponseData.SKUMasterTypesList = SKUMasterTypes;
                    //    //ResponseData.ResponseDynamicData = SKUMasterTypes;
                    //}
                    //else
                    //{

                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectAllSKUMasterResponse GetSKUDetails(SelectAllSKUMasterRequest objRequest)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)objRequest;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;

                
                if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    _CommandObj = new SqlCommand("API_SearchProducts", _ConnectionObj);
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                    if (RequestData.Mode != null && RequestData.Mode != string.Empty)
                    {
                        _CommandObj.Parameters.AddWithValue("@Mode", RequestData.Mode);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@Mode", DBNull.Value);
                    }
                    if (RequestData.Count == 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@Count", 100);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@Count", RequestData.Count);
                    }
                    _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                    _CommandObj.CommandType = CommandType.StoredProcedure;

                }
                
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();                    
                        objSKUMasterTypes.Stock = objReader["Stock"] != DBNull.Value ? Convert.ToInt32(objReader["Stock"]) : 0;

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                            objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                            objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        }
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                            objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                                objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                            }
                        }
                        SKUMasterTypes.Add(objSKUMasterTypes);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                }
                else
                {
                    if (checknontrade == true)
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.DisplayMessage = "You can't sell Non-Trading Items";
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectAllSKUMasterResponse GetSKUSearchForSales(SelectAllSKUMasterRequest objRequest)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)objRequest;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;
              
                    _CommandObj = new SqlCommand("API_SKUSearchForSales", _ConnectionObj);
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);             
                    _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                    _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.PriceListID);
                    _CommandObj.CommandType = CommandType.StoredProcedure;
                   
                

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.Stock = objReader["Stock"] != DBNull.Value ? Convert.ToInt32(objReader["Stock"]) : 0;

                        //if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        //{
                        //    objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                        objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        //}
                        //objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        //objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        //objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;
                        //objSKUMasterTypes.SKUName= objReader["SKUName"].ToString();
                        //objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        //objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        //objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        //objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        //objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        //objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        //objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        //objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        //objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        //objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        //objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        //objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        //objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        //objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        //objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        //objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        //objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        //objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        //objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        //objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        //objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        //objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        //objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        //objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        //objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        //objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        //objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        //objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();
                        objSKUMasterTypes.IsComboItem = objReader["IsComboItem"] != DBNull.Value ? Convert.ToBoolean(objReader["IsComboItem"]) : false;
                        //objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        //objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);


                        objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                        SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                        objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                        objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                        objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                        SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                        objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                        if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                            System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                            objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                        }
                        if (objSKUMasterTypes.IsComboItem == true)
                        {
                            objSKUMasterTypes.IsHeaderItem = true;
                        }
                        else
                        {
                            objSKUMasterTypes.IsHeaderItem = false;
                        }
                        SKUMasterTypes.Add(objSKUMasterTypes);
                        if (objSKUMasterTypes.IsComboItem==true)
                        {
                            SelectAllSKUMasterRequest objGetComboListRequest = new SelectAllSKUMasterRequest();
                            objGetComboListRequest.SearchString = RequestData.SearchString;
                            objGetComboListRequest.StoreIDs = RequestData.StoreIDs;
                            objGetComboListRequest.PriceListID = RequestData.PriceListID;

                            SelectAllSKUMasterResponse objGetComboListResponse = new SelectAllSKUMasterResponse();
                            objGetComboListResponse = GetSKUSearchForSalesCombo(objGetComboListRequest);


                            if (objGetComboListResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                foreach(SKUMasterTypes obj in objGetComboListResponse.SKUMasterTypesList)
                                {
                                    SKUMasterTypes.Add(obj);
                                }
                                /*objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                                objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);*/
                            }
                        }

                        //SKUMasterTypes.Add(objSKUMasterTypes);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                }

                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectAllSKUMasterResponse API_SelectALL(SelectAllSKUMasterRequest requestData)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)requestData;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;                
                
               
                    sSql.Append("Select SKU.ID,SKU.SKUCode,SKU.SKUName,SKU.Active,  RC.TOTAL_CNT [RecordCount]  FROM SKUMaster  SKU with(NoLock)  ");
                /*sSql.Append("LEFT  JOIN  DesignMaster DM with(NoLock) ON SKU.DesignID=DM.ID   ");
                sSql.Append("LEFT  JOIN  StyleMaster SM with(NoLock) ON SKU.StyleID=SM.ID  ");
                sSql.Append("LEFT JOIN AFSegamationMaster AFM with(NoLock) ON SKU.SegamentationID=AFM.ID  ");
                sSql.Append("LEFT JOIN YearMaster YM with(NoLock) ON SKU.YearID=YM.ID  ");
                sSql.Append("LEFT JOIN BrandMaster BM with(NoLock) ON SKU.BrandID =BM.ID    ");
                sSql.Append("LEFT JOIN SeasonMaster SEM1 with(NoLock) ON SKU.SeasonID=SEM1.ID  ");
                sSql.Append("LEFT JOIN ProductGroupMaster PGM with(NoLock) ON SKU.ProductGroupID =PGM.ID    ");
                sSql.Append("LEFT JOIN ProductSubGroupMaster PSGM with(NoLock) ON SKU.ProductSubGroupID =PSGM.ID    ");
                sSql.Append("LEFT JOIN SubBrandMaster SBM with(NoLock) ON SKU.SubBrandID =SBM.ID    ");
                sSql.Append("LEFT JOIN CollectionMaster CM with(NoLock) ON SKU.CollectionID=CM.ID  ");
                sSql.Append("LEFT JOIN DivisionMaster DM1 with(NoLock) ON SKU.DivisionID=DM1.ID   ");
                sSql.Append("LEFT JOIN ScaleMaster SM1 with(NoLock) ON SKU.ScaleID=SM1.ID  ");*/

                sSql.Append("LEFT JOIN(Select  count(SKU1.ID) As TOTAL_CNT From SKUMaster SKU1 with(NoLock)  ");
                sSql.Append("where SKU1.Active = " + RequestData.IsActive + " ");
                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or SKU1.SKUCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SKU1.SKUName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1 ");

                sSql.Append("where Active = " + RequestData.IsActive + " ");
                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or SKU.SKUCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SKU.SKUName like isnull('%" + RequestData.SearchString + "%','')) ");
                //sSql.Append("or Description = isnull('%" + RequestData.SearchString + "%','')) ");
                sSql.Append("order by ID asc ");
                sSql.Append("offset " + RequestData.Offset + " rows ");
                sSql.Append("fetch first " + RequestData.Limit + " rows only");

                sCommand = sSql.ToString();

                    _CommandObj = new SqlCommand(sCommand, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                



                //_CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        //objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        /*if (RequestData.StoreIDs != null)
                        {
                            SqlConnection con = new SqlConnection();
                            //con = RequestData.ConnectionString;
                            sqlCommon.InitializeDataComponents(ref con, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                            //con.Open();
                            var sql = "SELECT SUM(InQty) - SUM(OutQty) FROM TransactionLog WHERE SKUCode = '" + SKUCode + "' and StoreID=" + RequestData.StoreIDs + "";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            objSKUMasterTypes.Stock = (int)cmd.ExecuteScalar();
                            con.Close();
                        }
                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                            objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                            objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        }*/
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        /*objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;*/
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                       /* objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        if (RequestData.StoreIDs != null)
                        {
                            objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);
                        }

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                            objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                                objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                            }
                        }*/
                        SKUMasterTypes.Add(objSKUMasterTypes);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                }
                else
                {
                    if (checknontrade == true)
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.DisplayMessage = "You can't sell Non-Trading Items";
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override GetStylePricingBySKUCodeResponse SelectCurrencyStylePricingBySKUCode(GetStylePricingBySKUCodeRequest objRequest)
        {
            var SKUMasterTypes = new SKUMasterTypes();
            var StylePricingList = new List<StylePricing>();
            var RequestData = (GetStylePricingBySKUCodeRequest)objRequest;
            var ResponseData = new GetStylePricingBySKUCodeResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                sSql.Append("select distinct(ccm.countrycode) CountryCode, sp.price Price,cm.currencycode CurrencyCode,ccm.countryname CountryName from StylePricing sp join currencymaster cm on cm.id = sp.pricelistcurrency join countrymaster ccm on ccm.currencyid = cm.id where cm.active = 1 and ccm.active = 1 and sp.Active = 1 and sp.Price>0 and ");
                sSql.Append("sp.SKUCode like '" + RequestData.SKUCode + "%'");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                string PriceListIDs = string.Empty;

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStylePricing = new StylePricing();

                        objStylePricing.CountryCode = objReader["CountryCode"].ToString();
                        objStylePricing.Price = objReader["Price"] != DBNull.Value ? Convert.ToDecimal(objReader["Price"]) : 0;
                        objStylePricing.CurrencyCode = objReader["CurrencyCode"].ToString();
                        //objStylePricing.SKUCode = objReader["SKUCode"].ToString();
                        objStylePricing.CountryName = objReader["CountryName"].ToString();
                        StylePricingList.Add(objStylePricing);                       
                    }
                    ResponseData.StylePricingList = StylePricingList;
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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

        public override SelectAllSKUMasterResponse GetSKUDetailsByBin(SelectAllSKUMasterRequest objRequest)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)objRequest;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;


                if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                {
                    _CommandObj = new SqlCommand("API_SearchBINProducts", _ConnectionObj);
                    _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                    if (RequestData.Mode != null && RequestData.Mode != string.Empty)
                    {
                        _CommandObj.Parameters.AddWithValue("@Mode", RequestData.Mode);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@Mode", DBNull.Value);
                    }
                    if (RequestData.Count == 0)
                    {
                        _CommandObj.Parameters.AddWithValue("@Count", 100);
                    }
                    else
                    {
                        _CommandObj.Parameters.AddWithValue("@Count", RequestData.Count);
                    }
                    _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                    _CommandObj.Parameters.AddWithValue("@BINCode", RequestData.BINCode);
                    _CommandObj.CommandType = CommandType.StoredProcedure;

                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.Stock = objReader["Stock"] != DBNull.Value ? Convert.ToInt32(objReader["Stock"]) : 0;

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                            objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                            objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        }
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;

                        objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objSKUMasterTypes.BinCode = objReader["BinCode"].ToString();
                        checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);

                        if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        {
                            objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                            SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                            objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                            objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                            objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                            SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                            objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                            if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                            {
                                objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                                System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                                objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                            }
                        }
                        SKUMasterTypes.Add(objSKUMasterTypes);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                }
                else
                {
                    if (checknontrade == true)
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.DisplayMessage = "You can't sell Non-Trading Items";
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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

        public override SelectSKUDetailsResponse SelectSKUDetails(SelectAllSKUMasterRequest RequestObj)
        {
            var SKUMasterTypes = new List<ComboOfferDetails>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)RequestObj;
            var ResponseData = new SelectSKUDetailsResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;

                if (RequestData.RequestFrom == Enums.RequestFrom.MainServer)
                {
                    sSql.Append("Select * from SKUMaster with(NoLock) WHERE (SKUCode ='" + RequestObj.SearchString + "' OR BarCode ='" + RequestObj.SearchString + "')");

                    sCommand = sSql.ToString();

                    _CommandObj = new SqlCommand(sCommand, _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                }

                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new ComboOfferDetails();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.Barcode = objReader["BarCode"].ToString();
                        objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.Stylecode = Convert.ToString(objReader["StyleCode"].ToString());

                        objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;







                        SKUMasterTypes.Add(objSKUMasterTypes);

                    }



                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ComboOfferDetailsList = SKUMasterTypes;
                    ResponseData.ResponseDynamicData = SKUMasterTypes;



                }
                else
                {

                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");

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

        public override SelectAllSKUMasterResponse GetSKUSearchForSalesCombo(SelectAllSKUMasterRequest objRequest)
        {
            string SKUCode = "";
            var SKUMasterTypes = new List<SKUMasterTypes>();
            var _ImageProcess = new DataBaseImageProcess();

            var RequestData = (SelectAllSKUMasterRequest)objRequest;
            var ResponseData = new SelectAllSKUMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                var sCommand = string.Empty;

                _CommandObj = new SqlCommand("API_SKUSearchForSales_Combo_Item", _ConnectionObj);
                _CommandObj.Parameters.AddWithValue("@SearchString", RequestData.SearchString);
                _CommandObj.Parameters.AddWithValue("@StoreID", Convert.ToInt32(RequestData.StoreIDs));
                _CommandObj.Parameters.AddWithValue("@PriceListID", RequestData.PriceListID);
                _CommandObj.CommandType = CommandType.StoredProcedure;



                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objSKUMasterTypes = new SKUMasterTypes();

                        objSKUMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSKUMasterTypes.SKUCode = objReader["SKUCode"].ToString();
                        SKUCode = objReader["SKUCode"].ToString();
                        objSKUMasterTypes.Stock = objReader["Stock"] != DBNull.Value ? Convert.ToInt32(objReader["Stock"]) : 0;

                        //if (RequestData.RequestFrom == Enums.RequestFrom.Search || RequestData.RequestFrom == Enums.RequestFrom.StoreSales)
                        //{
                        //    objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.ColorName = objReader["colorname"].ToString();
                        objSKUMasterTypes.SizeName = objReader["sizename"].ToString();
                        //}
                        //objSKUMasterTypes.SKUName = objReader["SKUName"].ToString();
                        objSKUMasterTypes.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"].ToString()) : 0;
                        objSKUMasterTypes.StyleCode = Convert.ToString(objReader["StyleCode"].ToString());
                        //objSKUMasterTypes.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        objSKUMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objSKUMasterTypes.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"].ToString()) : 0;
                        //objSKUMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"].ToString()) : 0;
                        //objSKUMasterTypes.SKUName= objReader["SKUName"].ToString();
                        //objSKUMasterTypes.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        //objSKUMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"].ToString()) : 0;
                        //objSKUMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"].ToString()) : 0;

                        //objSKUMasterTypes.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        //objSKUMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"].ToString()) : 0;
                        //objSKUMasterTypes.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"].ToString()) : 0;

                        //objSKUMasterTypes.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        //objSKUMasterTypes.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"].ToString()) : 0;
                        //objSKUMasterTypes.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"].ToString()) : 0;


                        //objSKUMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        //objSKUMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"].ToString()) : 0;
                        //objSKUMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;

                        //objSKUMasterTypes.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        //objSKUMasterTypes.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"].ToString()) : 0;
                        //objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;

                        //objSKUMasterTypes.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        //objSKUMasterTypes.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"].ToString()) : 0;
                        objSKUMasterTypes.SupplierBarcode = objReader["SupplierBarcode"] != DBNull.Value ? Convert.ToString(objReader["SupplierBarcode"].ToString()) : String.Empty;
                        //objSKUMasterTypes.ArabicSKU = objReader["ArabicSKU"] != DBNull.Value ? Convert.ToString(objReader["ArabicSKU"].ToString()) : String.Empty;
                        objSKUMasterTypes.BarCode = objReader["BarCode"] != DBNull.Value ? Convert.ToString(objReader["BarCode"].ToString()) : String.Empty;
                        //objSKUMasterTypes.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"].ToString()) : 0;
                        //objSKUMasterTypes.ColorCode = objReader["ColorCode"].ToString();
                        //objSKUMasterTypes.SizeCode = objReader["SizeCode"].ToString();
                        //objSKUMasterTypes.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;


                        //objSKUMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objSKUMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objSKUMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objSKUMasterTypes.Year = objReader["Year"].ToString();
                        objSKUMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        //objSKUMasterTypes.ProductSubGroupName = objReader["ProductSubGroupName"].ToString();
                        //objSKUMasterTypes.SubBrandName = objReader["SubBrandName"].ToString();
                        //objSKUMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        //objSKUMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        //objSKUMasterTypes.ScaleName = objReader["ScaleName"].ToString();
                        objSKUMasterTypes.BrandCode = objReader["BrandCode"].ToString();
                        objSKUMasterTypes.SubBrandCode = objReader["SubBrandCode"].ToString();
                        objSKUMasterTypes.IsComboItem = true;
                        //objSKUMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        //objSKUMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        //objSKUMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        //objSKUMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        //objSKUMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        //objSKUMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        //checknontrade = Convert.ToBoolean(objSKUMasterTypes.IsNonTrading);

                        //objSKUMasterTypes.UseSeperator = objReader["UseSeperator"] != DBNull.Value ? Convert.ToString(objReader["UseSeperator"]) : String.Empty;
                        objSKUMasterTypes.StylePrice = Convert.ToDecimal(objReader["stylePrice"]);


                        objSKUMasterTypes.Tag_Id = objReader["Tag_Id"] != DBNull.Value ? Convert.ToString(objReader["Tag_Id"]) : String.Empty;

                        SelectByALLSKUImagesRequest objSelectByALLSKUImagesRequest = new SelectByALLSKUImagesRequest();
                        objSelectByALLSKUImagesRequest.RequestFrom = Enums.RequestFrom.StoreSales;
                        objSelectByALLSKUImagesRequest.SKUID = objSKUMasterTypes.ID;
                        objSelectByALLSKUImagesRequest.StyleID = objSKUMasterTypes.StyleID;

                        SelectAllSKUImagesResponse objSelectAllSKUImagesResponse = new SelectAllSKUImagesResponse();
                        objSelectAllSKUImagesResponse = SelectAllSKUImages(objSelectByALLSKUImagesRequest);


                        if (objSelectAllSKUImagesResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objSKUMasterTypes.SKUImage = objSelectAllSKUImagesResponse.SKUImageList.FirstOrDefault().SKUImage;
                            System.Drawing.Image image = _ImageProcess.byteArrayToImage(objSKUMasterTypes.SKUImage);
                            objSKUMasterTypes.SKUImageSource = (dynamic)_ImageProcess.GetImageStream(image);
                        }
                        objSKUMasterTypes.IsHeaderItem = false;
                        SKUMasterTypes.Add(objSKUMasterTypes);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.SKUMasterTypesList = SKUMasterTypes;
                }

                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "SKU Master");
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


