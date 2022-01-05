using EasyBizAbsDAL.Masters;
using EasyBizRequest.Masters.StyleMasterRequest;
using EasyBizResponse.Masters.StyleMasterResponse;
using MsSqlDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ResourceStrings;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizRequest.Masters.ScaleRequest;
using EasyBizResponse.Masters.ScaleMasterResponse;
using EasyBizResponse.Transactions.Stocks.StockRequest;
using EasyBizRequest.Transactions.Stocks.StockAdjustment;
using EasyBizResponse.Masters.DesignMasterResponse;
using EasyBizRequest.Masters.DesignMasterRequest;
namespace MsSqlDAL.Masters
{
    public class StyleMasterDAL : BaseStyleMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString; Enums.RequestFrom _RequestFrom;

        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStyleRequest)RequestObj;
            var ResponseData = new SaveStyleResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("API_InsertOrUpdateStyleMaster1", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StyleRecord.ID;

                SqlParameter StyleCode = _CommandObj.Parameters.Add("@StyleCode", SqlDbType.NVarChar);
                StyleCode.Direction = ParameterDirection.Input;
                StyleCode.Value = RequestData.StyleRecord.StyleCode;

                SqlParameter StyleName = _CommandObj.Parameters.Add("@StyleName", SqlDbType.NVarChar);
                StyleName.Direction = ParameterDirection.Input;
                StyleName.Value = RequestData.StyleRecord.StyleName;

                SqlParameter ShortDesignName = _CommandObj.Parameters.Add("@ShortDesignName", SqlDbType.NVarChar);
                ShortDesignName.Direction = ParameterDirection.Input;
                ShortDesignName.Value = RequestData.StyleRecord.ShortDesignName;

                SqlParameter DesignID = _CommandObj.Parameters.Add("@DesignID", SqlDbType.Int);
                DesignID.Direction = ParameterDirection.Input;
                DesignID.Value = RequestData.StyleRecord.DesignID;

                SqlParameter DesignName = _CommandObj.Parameters.Add("@DesignName", SqlDbType.NVarChar);
                DesignName.Direction = ParameterDirection.Input;
                DesignName.Value = RequestData.StyleRecord.DesignName;

                SqlParameter PurchasePrice = _CommandObj.Parameters.Add("@PurchasePrice", SqlDbType.Decimal);
                PurchasePrice.Direction = ParameterDirection.Input;
                PurchasePrice.Value = RequestData.StyleRecord.PurchasePrice;

                SqlParameter RRPPrice = _CommandObj.Parameters.Add("@RRPPrice", SqlDbType.Decimal);
                RRPPrice.Direction = ParameterDirection.Input;
                RRPPrice.Value = RequestData.StyleRecord.RRPPrice;

                SqlParameter SalesType = _CommandObj.Parameters.Add("@SalesType", SqlDbType.NVarChar);
                SalesType.Direction = ParameterDirection.Input;
                SalesType.Value = RequestData.StyleRecord.SalesType;

                SqlParameter ExchangeRate = _CommandObj.Parameters.Add("@ExchangeRate", SqlDbType.Money);
                ExchangeRate.Direction = ParameterDirection.Input;
                ExchangeRate.Value = RequestData.StyleRecord.ExchangeRate;

                SqlParameter ProductDepartmentCode = _CommandObj.Parameters.Add("@ProductDepartmentCode", SqlDbType.NVarChar);
                ProductDepartmentCode.Direction = ParameterDirection.Input;
                ProductDepartmentCode.Value = RequestData.StyleRecord.ProductDepartmentCode;

                SqlParameter DevelopmentOffice = _CommandObj.Parameters.Add("@DevelopmentOffice", SqlDbType.NVarChar);
                DevelopmentOffice.Direction = ParameterDirection.Input;
                DevelopmentOffice.Value = RequestData.StyleRecord.DevelopmentOffice;

                SqlParameter Composition = _CommandObj.Parameters.Add("@Composition", SqlDbType.NVarChar);
                Composition.Direction = ParameterDirection.Input;
                Composition.Value = RequestData.StyleRecord.Composition;

                SqlParameter ArabicStyle = _CommandObj.Parameters.Add("@ArabicStyle", SqlDbType.NVarChar);
                ArabicStyle.Direction = ParameterDirection.Input;
                ArabicStyle.Value = RequestData.StyleRecord.ArabicStyle;

                SqlParameter SymbolGroup = _CommandObj.Parameters.Add("@SymbolGroup", SqlDbType.NVarChar);
                SymbolGroup.Direction = ParameterDirection.Input;
                SymbolGroup.Value = RequestData.StyleRecord.SymbolGroup;

                SqlParameter Owner = _CommandObj.Parameters.Add("@Owner", SqlDbType.NVarChar);
                Owner.Direction = ParameterDirection.Input;
                Owner.Value = RequestData.StyleRecord.Owner;

                SqlParameter CountryOfOrigin = _CommandObj.Parameters.Add("@CountryOfOrigin", SqlDbType.NVarChar);
                CountryOfOrigin.Direction = ParameterDirection.Input;
                CountryOfOrigin.Value = RequestData.StyleRecord.CountryOfOrigin;

                SqlParameter ShortDescriptionn = _CommandObj.Parameters.Add("@ShortDescriptionn", SqlDbType.NVarChar);
                ShortDescriptionn.Direction = ParameterDirection.Input;
                ShortDescriptionn.Value = RequestData.StyleRecord.ShortDescriptionn;

                SqlParameter BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                BrandID.Direction = ParameterDirection.Input;
                BrandID.Value = RequestData.StyleRecord.BrandID;

                SqlParameter DropID = _CommandObj.Parameters.Add("@DropID", SqlDbType.Int);
                DropID.Direction = ParameterDirection.Input;
                DropID.Value = RequestData.StyleRecord.DropID;

                SqlParameter SubBrandID = _CommandObj.Parameters.Add("@SubBrandID", SqlDbType.Int);
                SubBrandID.Direction = ParameterDirection.Input;
                SubBrandID.Value = RequestData.StyleRecord.SubBrandID;

                SqlParameter PurchasePriceListID = _CommandObj.Parameters.Add("@PurchasePriceListID", SqlDbType.Int);
                PurchasePriceListID.Direction = ParameterDirection.Input;
                PurchasePriceListID.Value = RequestData.StyleRecord.PurchasePriceListID;

                SqlParameter SalesPriceListID = _CommandObj.Parameters.Add("@SalesPriceListID", SqlDbType.Int);
                SalesPriceListID.Direction = ParameterDirection.Input;
                SalesPriceListID.Value = RequestData.StyleRecord.SalesPriceListID;

                SqlParameter SalesPrice = _CommandObj.Parameters.Add("@SalesPrice", SqlDbType.Decimal);
                SalesPrice.Direction = ParameterDirection.Input;
                SalesPrice.Value = RequestData.StyleRecord.SalesPrice;

                SqlParameter CollectionID = _CommandObj.Parameters.Add("@CollectionID", SqlDbType.Int);
                CollectionID.Direction = ParameterDirection.Input;
                CollectionID.Value = RequestData.StyleRecord.CollectionID;


                SqlParameter DivisionID = _CommandObj.Parameters.Add("@DivisionID", SqlDbType.Int);
                DivisionID.Direction = ParameterDirection.Input;
                DivisionID.Value = RequestData.StyleRecord.DivisionID;

                SqlParameter ProductGroupID = _CommandObj.Parameters.Add("@ProductGroupID", SqlDbType.Int);
                ProductGroupID.Direction = ParameterDirection.Input;
                ProductGroupID.Value = RequestData.StyleRecord.ProductGroupID;

                SqlParameter ProductSubGroupID = _CommandObj.Parameters.Add("@ProductSubGroupID", SqlDbType.Int);
                ProductSubGroupID.Direction = ParameterDirection.Input;
                ProductSubGroupID.Value = RequestData.StyleRecord.ProductSubGroupID;

                SqlParameter SeasonID = _CommandObj.Parameters.Add("@SeasonID", SqlDbType.Int);
                SeasonID.Direction = ParameterDirection.Input;
                SeasonID.Value = RequestData.StyleRecord.SeasonID;

                SqlParameter ProductLineID = _CommandObj.Parameters.Add("@ProductLineID", SqlDbType.Int);
                ProductLineID.Direction = ParameterDirection.Input;
                ProductLineID.Value = RequestData.StyleRecord.ProductLineID;
                SqlParameter DesignerID = _CommandObj.Parameters.Add("@DesignerID", SqlDbType.Int);
                DesignerID.Direction = ParameterDirection.Input;
                DesignerID.Value = RequestData.StyleRecord.DesignerID;

                SqlParameter SalesCurrencyID = _CommandObj.Parameters.Add("@RRPCurrencyID", SqlDbType.Int);
                SalesCurrencyID.Direction = ParameterDirection.Input;
                SalesCurrencyID.Value = RequestData.StyleRecord.RRPCurrencyID;

                SqlParameter PurchaseCurrencyID = _CommandObj.Parameters.Add("@PurchaseCurrencyID", SqlDbType.Int);
                PurchaseCurrencyID.Direction = ParameterDirection.Input;
                PurchaseCurrencyID.Value = RequestData.StyleRecord.PurchaseCurrencyID;

                SqlParameter YearCode = _CommandObj.Parameters.Add("@YearCode", SqlDbType.Int);
                YearCode.Direction = ParameterDirection.Input;
                YearCode.Value = RequestData.StyleRecord.YearCode;

                SqlParameter Grade = _CommandObj.Parameters.Add("@Grade", SqlDbType.VarChar);
                Grade.Direction = ParameterDirection.Input;
                Grade.Value = RequestData.StyleRecord.Grade;


                SqlParameter SegmentationID = _CommandObj.Parameters.Add("@SegmentationID", SqlDbType.Int);
                SegmentationID.Direction = ParameterDirection.Input;
                SegmentationID.Value = RequestData.StyleRecord.StyleSegmentation;

                SqlParameter StyleStatusID = _CommandObj.Parameters.Add("@StyleStatusID", SqlDbType.Int);
                StyleStatusID.Direction = ParameterDirection.Input;
                StyleStatusID.Value = RequestData.StyleRecord.StyleStatusID;

                SqlParameter ArmadaCollectionID = _CommandObj.Parameters.Add("@ArmadaCollectionID", SqlDbType.Int);
                ArmadaCollectionID.Direction = ParameterDirection.Input;
                ArmadaCollectionID.Value = RequestData.StyleRecord.ArmadaCollectionID;

                SqlParameter CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.StyleRecord.CreateBy;

                SqlParameter DocumentID = _CommandObj.Parameters.Add("@DocumentID", SqlDbType.Int);
                DocumentID.Direction = ParameterDirection.Input;
                DocumentID.Value = RequestData.StyleRecord.DocumentID;

                SqlParameter ScaleID = _CommandObj.Parameters.Add("@ScaleID", SqlDbType.Int);
                ScaleID.Direction = ParameterDirection.Input;
                ScaleID.Value = RequestData.StyleRecord.ScaleID;

                SqlParameter CountryID = _CommandObj.Parameters.Add("@CountryID", SqlDbType.Int);
                CountryID.Direction = ParameterDirection.Input;
                CountryID.Value = RequestData.StyleRecord.CountryID;

                SqlParameter StoreID = _CommandObj.Parameters.Add("@StoreID", SqlDbType.Int);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = RequestData.StyleRecord.StoreID;

                SqlParameter Active = _CommandObj.Parameters.Add("@Active", SqlDbType.Bit);
                Active.Direction = ParameterDirection.Input;
                Active.Value = RequestData.StyleRecord.Active;

                SqlParameter Franchise = _CommandObj.Parameters.Add("@Franchise", SqlDbType.Bit);
                Franchise.Direction = ParameterDirection.Input;
                Franchise.Value = RequestData.StyleRecord.Franchise;

                var StyleWithScaleDetails = _CommandObj.Parameters.Add("@StyleWithScaleDetails", SqlDbType.Xml);
                StyleWithScaleDetails.Direction = ParameterDirection.Input;
                StyleWithScaleDetails.Value = StyleWithScaleDetailMasterXML(RequestData.StyleWithScaleDetailsList);

                var StyleWithColorDetails = _CommandObj.Parameters.Add("@StyleWithColorDetails", SqlDbType.Xml);
                StyleWithColorDetails.Direction = ParameterDirection.Input;
                //StyleWithColorDetails.Value = StyleWithColorDetailMasterXML(RequestData.StyleWithColorDetailsList);
                StyleWithColorDetails.Value = StyleWithColorDetailMasterXML(RequestData.StyleWithColorDetailsList).Replace("&", "&#38;").Replace("'", "&apos;");


                var StyleID = _CommandObj.Parameters.Add("@StyleID", SqlDbType.Int);
                StyleID.Direction = ParameterDirection.Output;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID2 = _CommandObj.Parameters.Add("@ID2", SqlDbType.Int);
                ID2.Direction = ParameterDirection.Output;

                SqlParameter SKUID = _CommandObj.Parameters.Add("@SKUID", SqlDbType.Int);
                SKUID.Direction = ParameterDirection.Output;

                SqlParameter BrandCode = _CommandObj.Parameters.Add("@BrandCode", SqlDbType.VarChar);
                BrandCode.Direction = ParameterDirection.Input;
                BrandCode.Value = RequestData.StyleRecord.BrandCode;

                SqlParameter SubBrandCode = _CommandObj.Parameters.Add("@SubBrandCode", SqlDbType.VarChar);
                SubBrandCode.Direction = ParameterDirection.Input;
                SubBrandCode.Value = RequestData.StyleRecord.SubBrandCode;

                SqlParameter CollectionCode = _CommandObj.Parameters.Add("@CollectionCode", SqlDbType.VarChar);
                CollectionCode.Direction = ParameterDirection.Input;
                CollectionCode.Value = RequestData.StyleRecord.CollectionCode;

                SqlParameter ArmadaCollectionCode = _CommandObj.Parameters.Add("@ArmadaCollectionCode", SqlDbType.VarChar);
                ArmadaCollectionCode.Direction = ParameterDirection.Input;
                ArmadaCollectionCode.Value = RequestData.StyleRecord.Grade;

                SqlParameter DivisionCode = _CommandObj.Parameters.Add("@DivisionCode", SqlDbType.VarChar);
                DivisionCode.Direction = ParameterDirection.Input;
                DivisionCode.Value = RequestData.StyleRecord.DivisionCode;

                SqlParameter ProductGroupCode = _CommandObj.Parameters.Add("@ProductGroupCode", SqlDbType.VarChar);
                ProductGroupCode.Direction = ParameterDirection.Input;
                ProductGroupCode.Value = RequestData.StyleRecord.ProductGroupCode;

                SqlParameter SeasonCode = _CommandObj.Parameters.Add("@SeasonCode", SqlDbType.VarChar);
                SeasonCode.Direction = ParameterDirection.Input;
                SeasonCode.Value = RequestData.StyleRecord.SeasonCode;

                SqlParameter ProductLineCode = _CommandObj.Parameters.Add("@ProductLineCode", SqlDbType.VarChar);
                ProductLineCode.Direction = ParameterDirection.Input;
                ProductLineCode.Value = RequestData.StyleRecord.ProductLineCode;

                SqlParameter StyleStatusCode = _CommandObj.Parameters.Add("@StyleStatusCode", SqlDbType.VarChar);
                StyleStatusCode.Direction = ParameterDirection.Input;
                StyleStatusCode.Value = RequestData.StyleRecord.Grade;

                SqlParameter DesignerCode = _CommandObj.Parameters.Add("@DesignerCode", SqlDbType.VarChar);
                DesignerCode.Direction = ParameterDirection.Input;
                DesignerCode.Value = RequestData.StyleRecord.DesignerCode;

                SqlParameter PurchasePriceListCode = _CommandObj.Parameters.Add("@PurchasePriceListCode", SqlDbType.VarChar);
                PurchasePriceListCode.Direction = ParameterDirection.Input;
                PurchasePriceListCode.Value = RequestData.StyleRecord.PurchasePriceListCode;

                SqlParameter PurchaseCurrencyCode = _CommandObj.Parameters.Add("@PurchaseCurrencyCode", SqlDbType.VarChar);
                PurchaseCurrencyCode.Direction = ParameterDirection.Input;
                PurchaseCurrencyCode.Value = RequestData.StyleRecord.PurchaseCurrencyCode;

                SqlParameter RRPCurrencyCode = _CommandObj.Parameters.Add("@RRPCurrencyCode", SqlDbType.VarChar);
                RRPCurrencyCode.Direction = ParameterDirection.Input;
                RRPCurrencyCode.Value = RequestData.StyleRecord.Grade;

                SqlParameter ScaleCode = _CommandObj.Parameters.Add("@ScaleCode", SqlDbType.VarChar);
                ScaleCode.Direction = ParameterDirection.Input;
                ScaleCode.Value = RequestData.StyleRecord.ScaleCode;

                SqlParameter SegmentationCode = _CommandObj.Parameters.Add("@SegmentationCode", SqlDbType.VarChar);
                SegmentationCode.Direction = ParameterDirection.Input;
                SegmentationCode.Value = RequestData.StyleRecord.SegmentationCode;

                SqlParameter DesignCode = _CommandObj.Parameters.Add("@DesignCode", SqlDbType.VarChar);
                DesignCode.Direction = ParameterDirection.Input;
                DesignCode.Value = RequestData.StyleRecord.DesignCode;

                SqlParameter ProductSubGroupCode = _CommandObj.Parameters.Add("@ProductSubGroupCode", SqlDbType.VarChar);
                ProductSubGroupCode.Direction = ParameterDirection.Input;
                ProductSubGroupCode.Value = RequestData.StyleRecord.ProductSubGroupCode;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID2.Value.ToString();

                    StyleImageInsert(RequestData.ItemImageMasterDetailsList, (int)ID2.Value);
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }
        public bool StyleImageInsert(List<ItemImageMaster> ItemImageMasterList, int ID)
        {
            bool objBool = false;

            var sqlCommon = new MsSqlCommon();
            sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
            try
            {
                if (ID > 0)
                {
                    string ssql = "Delete  SKUImages where StyleID='" + ID + "'";
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
                        StyleID.Value = ID; // style ID                                           

                        SqlParameter SKUID = _CommandObj.Parameters.Add("@SKUID", SqlDbType.Int);
                        SKUID.Direction = ParameterDirection.Input;
                        SKUID.Value = objItemImageMaster.SKUID;

                        SqlParameter StyleCode = _CommandObj.Parameters.Add("@StyleCode", SqlDbType.VarChar);
                        StyleCode.Direction = ParameterDirection.Input;
                        StyleCode.Value = objItemImageMaster.StyleCode;

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
            catch (Exception es)
            {
                throw (es);
            }
            finally
            {
                _CommandObj.Dispose();
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return objBool;
        }
        public string StyleWithScaleDetailMasterXML(List<ScaleDetailMaster> ScaleDetailMasterList)
        {
            StringBuilder sSql = new StringBuilder();

            if (ScaleDetailMasterList != null)
            {
                foreach (ScaleDetailMaster objScaleDetailMasterDetails in ScaleDetailMasterList)
                {
                    if (objScaleDetailMasterDetails.Active == true || objScaleDetailMasterDetails.SSID != 0)
                    {
                        sSql.Append("<StyleWithScaleMasterData>");
                        sSql.Append("<ID>" + (objScaleDetailMasterDetails.SSID) + "</ID>");
                        sSql.Append("<SizeID>" + (objScaleDetailMasterDetails.SizeID) + "</SizeID>");
                        sSql.Append("<SizeCode>" + objScaleDetailMasterDetails.SizeCode + "</SizeCode>");
                        sSql.Append("<SizeName>" + objScaleDetailMasterDetails.Description + "</SizeName>");
                        //sSql.Append("<Description>" + (objScaleDetailMasterDetails.Description) + "</Description>");
                        sSql.Append("<Active>" + objScaleDetailMasterDetails.Active + "</Active>");
                        sSql.Append("</StyleWithScaleMasterData>");
                    }
                }
            }
            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); ;
        }
        public string StyleWithColorDetailMasterXML(List<ColorMaster> ColorMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            if (ColorMasterList != null)
            {
                foreach (ColorMaster objColorMasterDetails in ColorMasterList)
                {
                    if (objColorMasterDetails.Active == true || objColorMasterDetails.ID != 0)
                    {
                        sSql.Append("<StyleWithColorMasterData>");
                        sSql.Append("<ID>" + (objColorMasterDetails.SCID) + "</ID>");
                        sSql.Append("<ColorID>" + (objColorMasterDetails.ColorID) + "</ColorID>");
                        sSql.Append("<ColorCode>" + objColorMasterDetails.ColorCode + "</ColorCode>");
                        sSql.Append("<ColorName>" + (objColorMasterDetails.Description) + "</ColorName>");
                        sSql.Append("<Active>" + objColorMasterDetails.Active + "</Active>");
                        sSql.Append("</StyleWithColorMasterData>");
                    }
                }
            }

            return sSql.ToString().Replace("&", "&#38;").Replace("'", "&apos;"); ;
        }
        //public string ItemImageMasterDetailsListXML(List<ItemImageMaster> ItemImageMasterList)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    foreach (ItemImageMaster objItemImageMaster in ItemImageMasterList)
        //    {
        //        sSql.Append("<StyleWithItemImageData>");
        //        sSql.Append("<ID>" + (objItemImageMaster.ID) + "</ID>");
        //        sSql.Append("<ItemImage>" + objItemImageMaster.ItemImage + "</ItemImage>");              
        //        sSql.Append("</StyleWithItemImageData>");
        //    }
        //    return sSql.ToString();
        //} 
        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateStyleRequest)RequestObj;
            var ResponseData = new UpdateStyleResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("UpdateStyleMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID", SqlDbType.Int);
                ID.Direction = ParameterDirection.Input;
                ID.Value = RequestData.StyleRecord.ID;

                SqlParameter StyleCode = _CommandObj.Parameters.Add("@StyleCode", SqlDbType.NVarChar);
                StyleCode.Direction = ParameterDirection.Input;
                StyleCode.Value = RequestData.StyleRecord.StyleCode;



                SqlParameter StyleName = _CommandObj.Parameters.Add("@StyleName", SqlDbType.NVarChar);
                StyleName.Direction = ParameterDirection.Input;
                StyleName.Value = RequestData.StyleRecord.StyleName;

                SqlParameter ShortDesignName = _CommandObj.Parameters.Add("@ShortDesignName", SqlDbType.NVarChar);
                ShortDesignName.Direction = ParameterDirection.Input;
                ShortDesignName.Value = RequestData.StyleRecord.ShortDesignName;

                SqlParameter DesignID = _CommandObj.Parameters.Add("@DesignID", SqlDbType.NVarChar);
                DesignID.Direction = ParameterDirection.Input;
                DesignID.Value = RequestData.StyleRecord.DesignID;

                SqlParameter DesignName = _CommandObj.Parameters.Add("@DesignName", SqlDbType.NVarChar);
                DesignName.Direction = ParameterDirection.Input;
                DesignName.Value = RequestData.StyleRecord.DesignName;

                SqlParameter BrandID = _CommandObj.Parameters.Add("@BrandID", SqlDbType.Int);
                BrandID.Direction = ParameterDirection.Input;
                BrandID.Value = RequestData.StyleRecord.BrandID;

                SqlParameter SubBrandID = _CommandObj.Parameters.Add("@SubBrandID", SqlDbType.Int);
                SubBrandID.Direction = ParameterDirection.Input;
                SubBrandID.Value = RequestData.StyleRecord.SubBrandID;

                SqlParameter CollectionID = _CommandObj.Parameters.Add("@CollectionID", SqlDbType.Int);
                CollectionID.Direction = ParameterDirection.Input;
                CollectionID.Value = RequestData.StyleRecord.CollectionID;


                SqlParameter SalesType = _CommandObj.Parameters.Add("@SalesType", SqlDbType.NVarChar);
                SalesType.Direction = ParameterDirection.Input;
                SalesType.Value = RequestData.StyleRecord.SalesType;


                SqlParameter DivisionID = _CommandObj.Parameters.Add("@DivisionID", SqlDbType.Int);
                DivisionID.Direction = ParameterDirection.Input;
                DivisionID.Value = RequestData.StyleRecord.DivisionID;

                SqlParameter ProductGroupID = _CommandObj.Parameters.Add("@ProductGroupID", SqlDbType.Int);
                ProductGroupID.Direction = ParameterDirection.Input;
                ProductGroupID.Value = RequestData.StyleRecord.BrandID;

                SqlParameter ProductSubGroupID = _CommandObj.Parameters.Add("@ProductSubGroupID", SqlDbType.Int);
                ProductSubGroupID.Direction = ParameterDirection.Input;
                ProductSubGroupID.Value = RequestData.StyleRecord.ProductSubGroupID;

                SqlParameter SeasonID = _CommandObj.Parameters.Add("@SeasonID", SqlDbType.Int);
                SeasonID.Direction = ParameterDirection.Input;
                SeasonID.Value = RequestData.StyleRecord.SeasonID;

                SqlParameter ProductLineID = _CommandObj.Parameters.Add("@ProductLineID", SqlDbType.Int);
                ProductLineID.Direction = ParameterDirection.Input;
                ProductLineID.Value = RequestData.StyleRecord.ProductLineID;

                SqlParameter DesignerID = _CommandObj.Parameters.Add("@DesignerID", SqlDbType.Int);
                DesignerID.Direction = ParameterDirection.Input;
                DesignerID.Value = RequestData.StyleRecord.DesignerID;

                SqlParameter CurrencyID = _CommandObj.Parameters.Add("@RRPCurrencyID", SqlDbType.Int);
                CurrencyID.Direction = ParameterDirection.Input;
                CurrencyID.Value = RequestData.StyleRecord.RRPCurrencyID;

                SqlParameter YearCode = _CommandObj.Parameters.Add("@YearCode", SqlDbType.Int);
                YearCode.Direction = ParameterDirection.Input;
                YearCode.Value = RequestData.StyleRecord.YearCode;


                SqlParameter UpdateBy = _CommandObj.Parameters.Add("@UpdateBy", SqlDbType.Int);
                UpdateBy.Direction = ParameterDirection.Input;
                UpdateBy.Value = RequestData.StyleRecord.UpdateBy;

                SqlParameter SCN = _CommandObj.Parameters.Add("@SCN", SqlDbType.Int);
                SCN.Direction = ParameterDirection.Input;
                SCN.Value = RequestData.StyleRecord.SCN;

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Style Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
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
            var StyleRecord = new StyleMaster();
            var RequestData = (DeleteStyleRequest)RequestObj;
            var ResponseData = new DeleteStyleResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                string sSql = "Delete from StyleMaster where ID={0}";
                sSql = string.Format(sSql, RequestData.ID);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Style Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Style Master");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StyleRecord = new StyleMaster();
            var RequestData = (SelectByStyleIDRequest)RequestObj;
            var ResponseData = new SelectByStyleIDResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = string.Empty;
                if (RequestData.StyleCode != null && RequestData.StyleCode != string.Empty)
                {
                    //sSql = "select SM.*,SES.SeasonName from StyleMaster SM JOIN SeasonMaster SES ON SES.SeasonCode = SM.SeasonCode where SM.StyleCode='{0}'";
                    sSql = "select SM.*,SES.SeasonName from StyleMaster SM JOIN SeasonMaster SES ON SES.ID = SM.SeasonID where SM.StyleCode='{0}'";
                    sSql = string.Format(sSql, RequestData.StyleCode);
                }
                else
                {
                    sSql = "Select * from StyleMaster with(NoLock) where ID='{0}'";
                    sSql = string.Format(sSql, RequestData.ID);
                }

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StyleMaster objStyle = new StyleMaster();
                        objStyle.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyle.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyle.StyleName = Convert.ToString(objReader["StyleName"]);
                        objStyle.ItemImage = Convert.ToString(objReader["ItemImage"]);
                        objStyle.ItemImage = Convert.ToString(objReader["SalesType"]);
                        objStyle.ShortDesignName = Convert.ToString(objReader["ShortDesignName"]);
                        objStyle.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        //objStyle.DesignID =objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) :0;
                        objStyle.DesignName = Convert.ToString(objReader["DesignName"]);
                        objStyle.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objStyle.DropID = objReader["DropID"] != DBNull.Value ? Convert.ToInt32(objReader["DropID"]) : 0;
                        objStyle.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"]) : 0;
                        objStyle.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objStyle.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) : 0;
                        objStyle.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objStyle.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objStyle.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) : 0;
                        objStyle.ProductDepartmentCode = Convert.ToString(objReader["ProductDepartmentCode"]);
                        objStyle.DevelopmentOffice = Convert.ToString(objReader["DevelopmentOffice"]);
                        objStyle.Grade = Convert.ToString(objReader["Grade"]);
                        objStyle.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objStyle.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"]) : 0;
                        objStyle.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"]) : 0;
                        objStyle.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"]) : 0;
                        objStyle.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"]) : 0;
                        objStyle.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objStyle.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objStyle.YearCode = objReader["YearCode"] != DBNull.Value ? Convert.ToInt32(objReader["YearCode"]) : 0;
                        objStyle.StyleSegmentation = objReader["StyleSegmentation"] != DBNull.Value ? Convert.ToInt32(objReader["StyleSegmentation"]) : 0;
                        objStyle.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"]) : 0;
                        objStyle.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objStyle.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;
                        objStyle.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;

                        objStyle.SalesPriceListID = objReader["SalesPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesPriceListID"]) : 0;
                        objStyle.SalesPrice = objReader["SalesPrice"] != DBNull.Value ? Convert.ToInt32(objReader["SalesPrice"]) : 0;
                        objStyle.Composition = Convert.ToString(objReader["Composition"]);
                        objStyle.ArabicStyle = Convert.ToString(objReader["ArabicStyle"]);
                        objStyle.SymbolGroup = Convert.ToString(objReader["SymbolGroup"]);
                        objStyle.Owner = Convert.ToString(objReader["Owner"]);
                        objStyle.CountryOfOrigin = Convert.ToString(objReader["CountryOfOrigin"]);
                        objStyle.ShortDescriptionn = Convert.ToString(objReader["ShortDescriptionn"]);


                        objStyle.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStyle.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStyle.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStyle.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStyle.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStyle.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyle.Franchise = objReader["Franchise"] != DBNull.Value ? Convert.ToBoolean(objReader["Franchise"]) : false;
                        //objStyle.SeasonCode = Convert.ToString(objReader["SeasonCode"]);

                        objStyle.SalesType = objReader["SalesType"] != DBNull.Value ? Convert.ToString(objReader["SalesType"]) : "";
                        objStyle.SeasonCode = objReader["SeasonCode"] != DBNull.Value ? Convert.ToString(objReader["SeasonCode"]) : "";

                        if (RequestData.StyleCode != null && RequestData.StyleCode != string.Empty)
                        {
                            objStyle.SeasonName = Convert.ToString(objReader["SeasonName"]);
                        }

                        objStyle.ScaleDetailMasterList = new List<ScaleDetailMaster>();

                        SelectScaleDetailsRequest objSelectScaleDetailsRequest = new SelectScaleDetailsRequest();
                        SelectScaleDetailsResponse objSelectScaleDetailsResponse = new SelectScaleDetailsResponse();
                        objSelectScaleDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectScaleDetailsRequest.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objSelectScaleDetailsResponse = SelectStyleWithScaleDetails(objSelectScaleDetailsRequest);
                        if (objSelectScaleDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ScaleDetailMasterList = objSelectScaleDetailsResponse.ScaleDetailMasterRecord;
                        }

                        objStyle.ColorMasterList = new List<ColorMaster>();

                        SelectColorDetailsRequest objSelectColorDetailsRequest = new SelectColorDetailsRequest();
                        SelectColorDetailsResponse objSelectColorDetailsResponse = new SelectColorDetailsResponse();
                        objSelectColorDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectColorDetailsResponse = SelectStyleWithColorDetails(objSelectColorDetailsRequest);
                        if (objSelectColorDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ColorMasterList = objSelectColorDetailsResponse.StyleWithColorDetailsRecord;
                        }

                        objStyle.ItemImageMasterList = new List<ItemImageMaster>();

                        SelectItemImageRequest objSelectItemImageRequest = new SelectItemImageRequest();
                        SelectItemImageResponse objSelectItemImageResponse = new SelectItemImageResponse();
                        objSelectItemImageRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectItemImageRequest.FormName = "Style";
                        objSelectItemImageResponse = SelectStyleWithItemImageDetails(objSelectItemImageRequest);
                        if (objSelectItemImageResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ItemImageMasterList = objSelectItemImageResponse.ItemImageMaster;
                        }

                        ResponseData.StyleRecord = objStyle;
                        ResponseData.ResponseDynamicData = objStyle;
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

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            var StyleList = new List<StyleMaster>();
            var RequestData = (SelectAllStyleRequest)RequestObj;
            var ResponseData = new SelectAllStyleResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "select top 20 SM.*,S.SeasonName from StyleMaster AS SM with(NoLock) JOIN SeasonMaster AS S with(NoLock) on SM.SeasonID = S.ID ";

                if (RequestData.ShowInActiveRecords == false && RequestData.StyleCode != null && RequestData.StyleCode != string.Empty)
                {
                    sQuery = sQuery + "where SM.Active='True' and StyleCode='" + RequestData.StyleCode + "'";
                }

                sQuery = sQuery + "order by id desc";

                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StyleMaster objStyle = new StyleMaster();

                        objStyle.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyle.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyle.StyleName = Convert.ToString(objReader["StyleName"]);
                        objStyle.SalesType = Convert.ToString(objReader["SalesType"]);
                        objStyle.ItemImage = Convert.ToString(objReader["ItemImage"]);
                        objStyle.ShortDesignName = Convert.ToString(objReader["ShortDesignName"]);
                        objStyle.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        //objStyle.DesignID =objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) :0;
                        objStyle.DesignName = Convert.ToString(objReader["DesignName"]);
                        objStyle.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objStyle.DropID = objReader["DropID"] != DBNull.Value ? Convert.ToInt32(objReader["DropID"]) : 0;
                        objStyle.SalesPrice = objReader["SalesPrice"] != DBNull.Value ? Convert.ToInt32(objReader["SalesPrice"]) : 0;
                        objStyle.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePrice"]) : 0;
                        objStyle.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objStyle.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) : 0;
                        objStyle.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objStyle.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objStyle.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) : 0;
                        objStyle.ProductDepartmentCode = Convert.ToString(objReader["ProductDepartmentCode"]);
                        objStyle.DevelopmentOffice = Convert.ToString(objReader["DevelopmentOffice"]);
                        objStyle.Grade = Convert.ToString(objReader["Grade"]);
                        objStyle.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objStyle.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"]) : 0;
                        objStyle.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"]) : 0;
                        objStyle.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"]) : 0;
                        objStyle.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"]) : 0;
                        objStyle.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objStyle.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objStyle.YearCode = objReader["YearCode"] != DBNull.Value ? Convert.ToInt32(objReader["YearCode"]) : 0;
                        objStyle.StyleSegmentation = objReader["StyleSegmentation"] != DBNull.Value ? Convert.ToInt32(objReader["StyleSegmentation"]) : 0;
                        objStyle.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"]) : 0;
                        objStyle.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objStyle.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;
                        objStyle.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objStyle.SeasonName = Convert.ToString(objReader["SeasonName"]);

                        objStyle.SalesPriceListID = objReader["SalesPriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["SalesPriceListID"]) : 0;
                        objStyle.SalesPrice = objReader["SalesPrice"] != DBNull.Value ? Convert.ToInt32(objReader["SalesPrice"]) : 0;
                        objStyle.Composition = Convert.ToString(objReader["Composition"]);
                        objStyle.ArabicStyle = Convert.ToString(objReader["ArabicStyle"]);
                        objStyle.SymbolGroup = Convert.ToString(objReader["SymbolGroup"]);
                        objStyle.Owner = Convert.ToString(objReader["Owner"]);
                        objStyle.CountryOfOrigin = Convert.ToString(objReader["CountryOfOrigin"]);
                        objStyle.ShortDescriptionn = Convert.ToString(objReader["ShortDescriptionn"]);

                        objStyle.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStyle.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStyle.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStyle.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStyle.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStyle.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyle.Franchise = objReader["Franchise"] != DBNull.Value ? Convert.ToBoolean(objReader["Franchise"]) : false;

                        objStyle.ScaleDetailMasterList = new List<ScaleDetailMaster>();

                        SelectScaleDetailsRequest objSelectScaleDetailsRequest = new SelectScaleDetailsRequest();
                        SelectScaleDetailsResponse objSelectScaleDetailsResponse = new SelectScaleDetailsResponse();
                        objSelectScaleDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectScaleDetailsRequest.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objSelectScaleDetailsResponse = SelectStyleWithScaleDetails(objSelectScaleDetailsRequest);
                        if (objSelectScaleDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ScaleDetailMasterList = objSelectScaleDetailsResponse.ScaleDetailMasterRecord;
                        }

                        objStyle.ColorMasterList = new List<ColorMaster>();

                        SelectColorDetailsRequest objSelectColorDetailsRequest = new SelectColorDetailsRequest();
                        SelectColorDetailsResponse objSelectColorDetailsResponse = new SelectColorDetailsResponse();
                        objSelectColorDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectColorDetailsResponse = SelectStyleWithColorDetails(objSelectColorDetailsRequest);
                        if (objSelectColorDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ColorMasterList = objSelectColorDetailsResponse.StyleWithColorDetailsRecord;
                        }

                        objStyle.ItemImageMasterList = new List<ItemImageMaster>();

                        SelectItemImageRequest objSelectItemImageRequest = new SelectItemImageRequest();
                        SelectItemImageResponse objSelectItemImageResponse = new SelectItemImageResponse();
                        objSelectItemImageRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectItemImageRequest.FormName = "Style";
                        objSelectItemImageResponse = SelectStyleWithItemImageDetails(objSelectItemImageRequest);
                        if (objSelectItemImageResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ItemImageMasterList = objSelectItemImageResponse.ItemImageMaster;
                        }
                        StyleList.Add(objStyle);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleList = StyleList;
                    ResponseData.ResponseDynamicData = StyleList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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
            var StyleList = new List<StyleMaster>();
            var RequestData = (SelectByStyleIDsRequest)RequestObj;
            var ResponseData = new SelectByStyleIDsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from StyleMaster with(NoLock) where ID in ({0})";
                sSql = string.Format(sSql, RequestData.IDs);

                _CommandObj = new SqlCommand(sSql, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StyleMaster objStyle = new StyleMaster();
                        objStyle.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyle.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyle.StyleName = Convert.ToString(objReader["StyleName"]);
                        objStyle.ItemImage = Convert.ToString(objReader["ItemImage"]);
                        objStyle.ShortDesignName = Convert.ToString(objReader["ShortDesignName"]);
                        objStyle.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                        //objStyle.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) :0;
                        objStyle.DesignName = Convert.ToString(objReader["DesignName"]);
                        objStyle.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objStyle.DropID = objReader["DropID"] != DBNull.Value ? Convert.ToInt32(objReader["DropID"]) : 0;
                        objStyle.SubBrandID = objReader["SubBrandID"] != DBNull.Value ? Convert.ToInt32(objReader["SubBrandID"]) : 0;
                        objStyle.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objStyle.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) : 0;
                        objStyle.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objStyle.ProductSubGroupID = objReader["ProductSubGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductSubGroupID"]) : 0;
                        objStyle.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) : 0;
                        objStyle.ProductDepartmentCode = Convert.ToString(objReader["ProductDepartmentCode"]);
                        objStyle.DevelopmentOffice = Convert.ToString(objReader["DevelopmentOffice"]);
                        objStyle.Grade = Convert.ToString(objReader["Grade"]);
                        objStyle.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objStyle.DesignerID = objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"]) : 0;
                        objStyle.RRPCurrencyID = objReader["RRPCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["RRPCurrencyID"]) : 0;
                        objStyle.PurchaseCurrencyID = objReader["PurchaseCurrencyID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchaseCurrencyID"]) : 0;
                        objStyle.PurchasePrice = objReader["PurchasePrice"] != DBNull.Value ? Convert.ToDecimal(objReader["PurchasePrice"]) : 0;
                        objStyle.RRPPrice = objReader["RRPPrice"] != DBNull.Value ? Convert.ToDecimal(objReader["RRPPrice"]) : 0;
                        objStyle.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objStyle.YearCode = objReader["YearCode"] != DBNull.Value ? Convert.ToInt32(objReader["YearCode"]) : 0;
                        objStyle.StyleSegmentation = objReader["StyleSegmentation"] != DBNull.Value ? Convert.ToInt32(objReader["StyleSegmentation"]) : 0;
                        objStyle.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"]) : 0;
                        objStyle.ArmadaCollectionID = objReader["ArmadaCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["ArmadaCollectionID"]) : 0;
                        objStyle.ExchangeRate = objReader["ExchangeRate"] != DBNull.Value ? Convert.ToDecimal(objReader["ExchangeRate"]) : 0;
                        objStyle.PurchasePriceListID = objReader["PurchasePriceListID"] != DBNull.Value ? Convert.ToInt32(objReader["PurchasePriceListID"]) : 0;
                        objStyle.Composition = Convert.ToString(objReader["Composition"]);
                        objStyle.ArabicStyle = Convert.ToString(objReader["ArabicStyle"]);
                        objStyle.SymbolGroup = Convert.ToString(objReader["SymbolGroup"]);
                        objStyle.Owner = Convert.ToString(objReader["Owner"]);
                        objStyle.CountryOfOrigin = Convert.ToString(objReader["CountryOfOrigin"]);
                        objStyle.ShortDescriptionn = Convert.ToString(objReader["ShortDescriptionn"]);


                        objStyle.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objStyle.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objStyle.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objStyle.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objStyle.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objStyle.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        objStyle.ScaleDetailMasterList = new List<ScaleDetailMaster>();

                        SelectScaleDetailsRequest objSelectScaleDetailsRequest = new SelectScaleDetailsRequest();
                        SelectScaleDetailsResponse objSelectScaleDetailsResponse = new SelectScaleDetailsResponse();
                        objSelectScaleDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectScaleDetailsRequest.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objSelectScaleDetailsRequest.RequestFrom = RequestData.RequestFrom;
                        objSelectScaleDetailsResponse = SelectStyleWithScaleDetails(objSelectScaleDetailsRequest);
                        if (objSelectScaleDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ScaleDetailMasterList = objSelectScaleDetailsResponse.ScaleDetailMasterRecord;
                        }

                        objStyle.ColorMasterList = new List<ColorMaster>();

                        SelectColorDetailsRequest objSelectColorDetailsRequest = new SelectColorDetailsRequest();
                        SelectColorDetailsResponse objSelectColorDetailsResponse = new SelectColorDetailsResponse();
                        objSelectColorDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectColorDetailsRequest.RequestFrom = RequestData.RequestFrom;
                        objSelectColorDetailsResponse = SelectStyleWithColorDetails(objSelectColorDetailsRequest);
                        if (objSelectColorDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ColorMasterList = objSelectColorDetailsResponse.StyleWithColorDetailsRecord;
                        }
                        StyleList.Add(objStyle);
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleMasterList = StyleList;
                    ResponseData.ResponseDynamicData = StyleList;
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

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
        public override EasyBizResponse.Masters.ScaleMasterResponse.SelectScaleDetailsResponse SelectStyleWithScaleDetails(EasyBizRequest.Masters.ScaleRequest.SelectScaleDetailsRequest ObjRequest)
        {
            var ScaleDetailMasterList = new List<ScaleDetailMaster>();
            var RequestData = (SelectScaleDetailsRequest)ObjRequest;
            var ResponseData = new SelectScaleDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                //sSql.Append("select * from  StyleWithScaleDetails ");
                //sSql.Append("where  StyleID=" + RequestData.ID + " and active = 'true' ");   

                sSql.Append("Select ssd.ID,smd.ID as SizeID,smd.SizeCode,smd.ScaleHeaderID,smd.Description,ssd.Active  from StyleWithScaleDetails ssd ");
                sSql.Append("right join ScaleMasterDetails smd on smd.ID = ssd.SizeID and ssd.StyleID=" + RequestData.ID + " where smd.ScaleHeaderID=" + RequestData.ScaleID);

                if (RequestData.RequestFrom == Enums.RequestFrom.Upload)
                {
                    sSql.Append(" and ssd.Active='True'");
                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScaleDetailMaster = new ScaleDetailMaster();
                        objScaleDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SSID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"]) : 0;
                        objScaleDetailMaster.SizeCode = Convert.ToString(objReader["SizeCode"]);
                        objScaleDetailMaster.Description = Convert.ToString(objReader["Description"]);
                        objScaleDetailMaster.ScaleHeaderID = objReader["ScaleHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleHeaderID"]) : 0;
                        objScaleDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        ScaleDetailMasterList.Add(objScaleDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleDetailMasterRecord = ScaleDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style");
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

        public override SelectColorDetailsResponse SelectStyleWithColorDetails(SelectColorDetailsRequest ObjRequest)
        {
            var StyleWithColorDetailsMasterList = new List<ColorMaster>();
            var RequestData = (SelectColorDetailsRequest)ObjRequest;
            var ResponseData = new SelectColorDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                //sSql.Append("select scd.ID AS SCID,cm.ID as ColorID,CM.ColorCode,scd.Active,cm.Description from  StyleWithColorDetails scd ");
                //sSql.Append("right join ColorMaster cm on scd.ColorID=cm.ID  and scd.StlyeID=" + RequestData.ID);

                sSql.Append("Select Distinct scd.ID AS SCID,scd.ColorID,scd.ColorCode,scd.Active,scd.StyleCode,scd.ColorName as Description,scd.StlyeID,CM.Colors as Color from StyleWithColorDetails scd ");
                sSql.Append("join StyleMaster sm on sm.ID=scd.StlyeID join ColorMaster AS CM on scd.ColorID=cm.ID ");

                if (RequestData.RequestFrom == Enums.RequestFrom.Upload)
                {
                    if (RequestData.ID > 0)
                    {
                        sSql.Append("where scd.StlyeID=" + RequestData.ID + " and scd.Active='True'");
                    }
                    else
                    {
                        sSql.Append("where sm.StyleCode='" + RequestData.StyleCode + "' and scd.Active='True'");
                    }
                }
                else
                {
                    if (RequestData.ID > 0)
                    {
                        sSql.Append("where scd.StlyeID=" + RequestData.ID + " and scd.Active='True'");
                    }
                    else
                    {
                        sSql.Append("where sm.StyleCode='" + RequestData.StyleCode + "' and scd.Active='True'");
                    }
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new ColorMaster();
                        objStyleWithColorDetailMaster.SCID = objReader["SCID"] != DBNull.Value ? Convert.ToInt32(objReader["SCID"]) : 0;
                        objStyleWithColorDetailMaster.ID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objStyleWithColorDetailMaster.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objStyleWithColorDetailMaster.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objStyleWithColorDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyleWithColorDetailMaster.Description = Convert.ToString(objReader["Description"]);
                        objStyleWithColorDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        objStyleWithColorDetailMaster.Colors = objReader["Color"] != DBNull.Value ? Convert.ToInt32(objReader["Color"]) : 0;
                        StyleWithColorDetailsMasterList.Add(objStyleWithColorDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleWithColorDetailsRecord = StyleWithColorDetailsMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style");
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

        public override SelectStyleLookUpResponse SelectStyleLookUp(SelectStyleLookUpRequest ObjRequest)
        {
            var StyleLookUpList = new List<StyleMaster>();
            var RequestData = (SelectStyleLookUpRequest)ObjRequest;
            var ResponseData = new SelectStyleLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select top 20 ID,[StyleName],StyleCode from StyleMaster with(NoLock)";
                if (RequestData.ShowInActiveRecords == false)
                {
                    sQuery = sQuery + " where Active='True'";
                }
                sQuery = sQuery + " Order by Id Desc";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleMaster = new StyleMaster();
                        objStyleMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleMaster.StyleName = Convert.ToString(objReader["StyleName"]);
                        objStyleMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        StyleLookUpList.Add(objStyleMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleMasterList = StyleLookUpList;
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

        public override SelectItemImageResponse SelectStyleWithItemImageDetails(SelectItemImageRequest ObjRequest)
        {
            var ItemImageMasterList = new List<ItemImageMaster>();
            var RequestData = (SelectItemImageRequest)ObjRequest;
            var ResponseData = new SelectItemImageResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                if (RequestData.FormName == "Style")
                {
                    sSql.Append("select * from  SKUImages ");
                    sSql.Append("where  StyleID=" + RequestData.ID + " ");


                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        if (ItemImageMasterList != null)
                        {
                            while (objReader.Read())
                            {
                                var objItemImageMaster = new ItemImageMaster();
                                objItemImageMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                                objItemImageMaster.SKUImage = objReader["SKUImage"] != DBNull.Value ? (byte[])objReader["SKUImage"] : null;
                                objItemImageMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                                //objItemImageMaster.StyleCode = objReader["StyleCode"] != DBNull.Value ? (byte[])objReader["StyleCode"] : null;
                                //objItemImageMaster.ItemImage = Convert.ToString(objReader["ItemImage"]);
                                objItemImageMaster.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;

                                ItemImageMasterList.Add(objItemImageMaster);
                            }
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ItemImageMaster = ItemImageMasterList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Image");
                    }
                }
                if (RequestData.FormName == "SKU")
                {
                    sSql.Append("select * from  SKUImages ");
                    sSql.Append("where  SKUID=" + RequestData.ID + " ");


                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        if (ItemImageMasterList != null)
                        {
                            while (objReader.Read())
                            {
                                var objItemImageMaster = new ItemImageMaster();
                                objItemImageMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                                objItemImageMaster.SKUImage = objReader["SKUImage"] != DBNull.Value ? (byte[])objReader["SKUImage"] : null;
                                //objItemImageMaster.ItemImage = Convert.ToString(objReader["ItemImage"]);
                                objItemImageMaster.SKUID = objReader["SKUID"] != DBNull.Value ? Convert.ToInt32(objReader["SKUID"]) : 0;

                                ItemImageMasterList.Add(objItemImageMaster);
                            }
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ItemImageMaster = ItemImageMasterList;
                    }

                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Image");
                    }
                }
                if (RequestData.FormName == "Design")
                {
                    sSql.Append("select * from  SKUImages ");
                    sSql.Append("where  DesignID=" + RequestData.ID + " ");


                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        if (ItemImageMasterList != null)
                        {
                            while (objReader.Read())
                            {
                                var objItemImageMaster = new ItemImageMaster();
                                objItemImageMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                                //objItemImageMaster.ItemImage = Convert.ToString(objReader["ItemImage"]);
                                objItemImageMaster.DesignID = objReader["DesignID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignID"]) : 0;
                                objItemImageMaster.SKUImage = objReader["SKUImage"] != DBNull.Value ? (byte[])objReader["SKUImage"] : null;
                                ItemImageMasterList.Add(objItemImageMaster);
                            }
                            ResponseData.StatusCode = Enums.OpStatusCode.Success;
                            ResponseData.ItemImageMaster = ItemImageMasterList;
                        }
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Image");
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

        public override StyleCodeGeneratingResponse SelectStyleCode(StyleCodeGeneratingRequest ObjRequest)
        {
            var StyleRecord = new StyleMaster();
            var RequestData = (StyleCodeGeneratingRequest)ObjRequest;
            var ResponseData = new StyleCodeGeneratingResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                var sSql = new StringBuilder();
                sSql.Append(" select StyleCode,SUBSTRING(StyleCode, LEN(StyleCode)-2, 3) as LastDocNo from [StyleMaster] where LEFT(StyleCode, LEN(StyleCode)-3)='" + RequestData.StyleCode + "'");

                //sSql.Append("select scd.ID AS SCID,cm.ID as ColorID,CM.ColorCode,scd.Active from  StyleWithColorDetails scd ");
                //sSql.Append("right join ColorMaster cm on scd.ColorID=cm.ID and scd.StlyeID=" + RequestData.);

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleCode = new StyleMaster();
                        objStyleCode.StyleCode = objReader["StyleCode"] != DBNull.Value ? Convert.ToString(objReader["StyleCode"]) : null;
                        objStyleCode.StyleRunningNum = objReader["LastDocNo"] != DBNull.Value ? Convert.ToInt64(objReader["LastDocNo"]) : 0;
                        ResponseData.Autonumbering = objStyleCode.StyleRunningNum;

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StyleWithColorMaster");
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

        public override EasyBizResponse.Transactions.Stocks.StockRequest.SelectStyleWithScaleforStockResponse SelectStyleWithScaleForStock(EasyBizRequest.Transactions.Stocks.StockAdjustment.SelectStyleWithScaleforStockRequest ObjRequest)
        {
            var ScaleDetailMasterList = new List<ScaleDetailMaster>();
            var RequestData = (SelectStyleWithScaleforStockRequest)ObjRequest;
            var ResponseData = new SelectStyleWithScaleforStockResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                //sSql.Append("Select ssd.*,sm.ScaleCode from StyleWithScaleDetails ssd join ScaleMaster sm on ssd.ScaleID=sm.ID ");
                //sSql.Append("where  ssd.StyleID=" + RequestData.ID + " and ssd.active = 'true' ");   

                sSql.Append("Select ssd.*,sm.ScaleCode from StyleWithScaleDetails ssd join ScaleMaster sm on ssd.ScaleID=sm.ID ");
                sSql.Append("join StyleMaster st on st.ID=ssd.StyleID ");

                if (RequestData.ID > 0)
                {
                    sSql.Append("where st.ID=" + RequestData.ID + " and ssd.active = 'true' ");
                }
                else
                {
                    sSql.Append("where  st.StyleCode='" + RequestData.StyleCode + "' and ssd.active = 'true' ");
                }

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScaleDetailMaster = new ScaleDetailMaster();
                        objScaleDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SSID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"]) : 0;
                        objScaleDetailMaster.SizeCode = Convert.ToString(objReader["SizeCode"]);
                        objScaleDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        objScaleDetailMaster.ScaleCode = Convert.ToString(objReader["ScaleCode"]);
                        ScaleDetailMasterList.Add(objScaleDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleDetailMasterRecordForStock = ScaleDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StyleWithScaleMaster");
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

        public override SaveStyleResponse ImportExcelStyleInsert(SaveStyleRequest ObjRequest)
        {

            var RequestData = (SaveStyleRequest)ObjRequest;
            var ResponseData = new SaveStyleResponse();
            var sqlCommon = new MsSqlCommon();
            SqlDataReader objReader;
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertImportStyleMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ImportExcelDetails = _CommandObj.Parameters.Add("@ImportDetails", SqlDbType.Xml);
                ImportExcelDetails.Direction = ParameterDirection.Input;
                ImportExcelDetails.Value = ImportDetailStyleXML(RequestData.ImportExcelList, RequestData.DocumentIDs);

                var ImportExcelWithColorDetails = _CommandObj.Parameters.Add("@ImportColorDetails", SqlDbType.Xml);
                ImportExcelWithColorDetails.Direction = ParameterDirection.Input;
                ImportExcelWithColorDetails.Value = ImportDetailStyleWithColorXML(RequestData.ImportcolorExcelList);

                var ImportExcelWithScaleDetails = _CommandObj.Parameters.Add("@ImportScaleDetails", SqlDbType.Xml);
                ImportExcelWithScaleDetails.Direction = ParameterDirection.Input;
                ImportExcelWithScaleDetails.Value = ImportDetailStyleWithScaleXML(RequestData.ImportScaleExcelList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ReturnIDs = _CommandObj.Parameters.Add("@InsertID", SqlDbType.VarChar, 8000);
                ReturnIDs.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.CommandTimeout = 0;
                objReader = _CommandObj.ExecuteReader();
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
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "StyleWithColorMaster");
                    ResponseData.ReturnIDs = "";
                }

                //string strStatusCode = StatusCode.Value.ToString();
                if (sCode.ToString() == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData.ReturnIDs = ReturnIDs.Value.ToString();


                }

                else if (sCode.ToString() == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public string ImportDetailStyleXML(List<StyleMaster> ImportDetailList, string IDs)
        {
            StringBuilder sSql = new StringBuilder();
            int Index = 0;

            string[] IDArray = null;
            //if (IDs != null)
            //{
            //    IDArray = IDs.Split(',');
            //}

            //  var IDArray = IDs.Split(',');

            // string[] IDArray = null;
            if (IDs != null && IDs != string.Empty)
            {
                IDArray = IDs.Split(',');
            }

            bool IDUpdate = false;
            if (IDArray != null && IDArray.Length == ImportDetailList.Count)
            {
                IDUpdate = true;
                foreach (string ReturnValue in IDArray)
                {
                    if (ReturnValue.Split('|') != null && ReturnValue.Split('|').Length == 2)
                    {
                        int ID = Convert.ToInt32(ReturnValue.Split('|')[0]);
                        string StyleCode = ReturnValue.Split('|')[1];
                        ImportDetailList.Where(x => x.StyleCode == StyleCode).ToList().ForEach(x => x.ID = ID);
                    }
                }

            }


            foreach (StyleMaster objImportExcelDetail in ImportDetailList)
            {
                sSql.Append("<StyleMaster>");

                //if (IDUpdate)
                //{                    
                //    sSql.Append("<ID>" + Convert.ToInt64(IDArray[Index]) + "</ID>");
                //}
                //else
                //{
                //    sSql.Append("<ID>0</ID>");
                //}
                sSql.Append("<ID>" + (objImportExcelDetail.ID) + "</ID>");
                sSql.Append("<StyleCode>" + (objImportExcelDetail.StyleCode).Replace("&", "&#38;") + "</StyleCode>");
                sSql.Append("<StyleName>" + (objImportExcelDetail.StyleName).Replace("&", "&#38;") + "</StyleName>");
                sSql.Append("<ShortDesignName>" + (objImportExcelDetail.ShortDesignName).Replace("&", "&#38;") + "</ShortDesignName>");
                sSql.Append("<StyleSegmentation>" + (objImportExcelDetail.StyleSegmentation) + "</StyleSegmentation>");
                sSql.Append("<DesignID>" + (objImportExcelDetail.DesignID) + "</DesignID>");
                sSql.Append("<DesignName>" + (objImportExcelDetail.DesignName).Replace("&", "&#38;") + "</DesignName>");
                sSql.Append("<ProductDepartmentCode>" + (objImportExcelDetail.ProductDepartmentCode).Replace("&", "&#38;") + "</ProductDepartmentCode>");
                sSql.Append("<BrandID>" + (objImportExcelDetail.BrandID) + "</BrandID>");
                sSql.Append("<SubBrandID>" + (objImportExcelDetail.SubBrandID) + "</SubBrandID>");
                sSql.Append("<CollectionID>" + (objImportExcelDetail.CollectionID) + "</CollectionID>");
                sSql.Append("<ArmadaCollectionID>" + (objImportExcelDetail.ArmadaCollectionID) + "</ArmadaCollectionID>");
                sSql.Append("<DivisionID>" + (objImportExcelDetail.DivisionID) + "</DivisionID>");
                sSql.Append("<ProductGroupID>" + (objImportExcelDetail.ProductGroupID) + "</ProductGroupID>");
                sSql.Append("<ProductSubGroupID>" + (objImportExcelDetail.ProductSubGroupID) + "</ProductSubGroupID>");
                sSql.Append("<SeasonID>" + (objImportExcelDetail.SeasonID) + "</SeasonID>");
                sSql.Append("<YearCode>" + (objImportExcelDetail.YearCode) + "</YearCode>");
                sSql.Append("<ProductLineID>" + (objImportExcelDetail.ProductLineID) + "</ProductLineID>");
                sSql.Append("<StyleStatusID>" + (objImportExcelDetail.StyleStatusID) + "</StyleStatusID>");
                sSql.Append("<DesignerID>" + (objImportExcelDetail.DesignerID) + "</DesignerID>");
                sSql.Append("<PurchasePriceListID>" + (objImportExcelDetail.PurchasePriceListID) + "</PurchasePriceListID>");
                sSql.Append("<PurchasePrice>" + (objImportExcelDetail.PurchasePrice) + "</PurchasePrice>");
                sSql.Append("<SalesPriceListID>" + (objImportExcelDetail.SalesPriceListID) + "</SalesPriceListID>");
                sSql.Append("<SalesPrice>" + (objImportExcelDetail.SalesPrice) + "</SalesPrice>");
                sSql.Append("<PurchaseCurrencyID>" + (objImportExcelDetail.PurchaseCurrencyID) + "</PurchaseCurrencyID>");
                sSql.Append("<RRPPrice>" + (objImportExcelDetail.RRPPrice) + "</RRPPrice>");
                sSql.Append("<RRPCurrencyID>" + (objImportExcelDetail.RRPCurrencyID) + "</RRPCurrencyID>");
                sSql.Append("<CountryID>" + (objImportExcelDetail.CountryID) + "</CountryID>");
                sSql.Append("<StoreID>" + (objImportExcelDetail.StoreID) + "</StoreID>");
                sSql.Append("<ScaleID>" + (objImportExcelDetail.ScaleID) + "</ScaleID>");
                sSql.Append("<Remarks>" + (objImportExcelDetail.Remarks) + "</Remarks>");
                sSql.Append("<Active>" + (objImportExcelDetail.Active) + "</Active>");
                sSql.Append("<IsStoreSync>" + (objImportExcelDetail.IsStoreSync) + "</IsStoreSync>");
                sSql.Append("<IsCountrySync>" + (objImportExcelDetail.IsStoreSync) + "</IsCountrySync>");
                sSql.Append("<Franchise>" + (objImportExcelDetail.Franchise) + "</Franchise>");
                sSql.Append("<ExchangeRate>" + (objImportExcelDetail.ExchangeRate) + "</ExchangeRate>");
                sSql.Append("<ArabicStyle>" + (objImportExcelDetail.ArabicStyle) + "</ArabicStyle>");
                //sSql.Append("<SAP_StyleDocEntry>" + (objImportExcelDetail.SAP_StyleDocEntry) + "</SAP_StyleDocEntry>");
                sSql.Append("</StyleMaster>");
                Index++;
            }

            return sSql.ToString().Replace("'", "&apos;").Replace("&", "&#38;");
        }
        public string ImportDetailStyleWithColorXML(List<StyleMaster> ImportDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (StyleMaster objImportExcelDetail in ImportDetailList)
            {

                sSql.Append("<StyleMaster>");
                sSql.Append("<ID>" + (objImportExcelDetail.ID) + "</ID>");
                sSql.Append("<StyleCode>" + (objImportExcelDetail.StyleCode).Replace("&", "&#38;") + "</StyleCode>");
                sSql.Append("<ColorID>" + (objImportExcelDetail.ColorID) + "</ColorID>");
                sSql.Append("<ColorCode>" + (objImportExcelDetail.ColorCode).Replace("&", "&#38;") + "</ColorCode>");
                sSql.Append("<ColorName>" + (objImportExcelDetail.ColorName).Replace("&", "&#38;") + "</ColorName>");
                sSql.Append("<Active>" + (objImportExcelDetail.Active) + "</Active>");
                sSql.Append("</StyleMaster>");
            }
            return sSql.ToString().Replace("'", "&apos;").Replace("&", "&#38;");
        }
        public string ImportDetailStyleWithScaleXML(List<StyleMaster> ImportDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (StyleMaster objImportExcelDetail in ImportDetailList)
            {

                sSql.Append("<StyleMaster>");
                sSql.Append("<ID>" + (objImportExcelDetail.ID) + "</ID>");
                sSql.Append("<StyleCode>" + (objImportExcelDetail.StyleCode).Replace("&", "&#38;") + "</StyleCode>");
                sSql.Append("<ScaleID>" + (objImportExcelDetail.ScaleID) + "</ScaleID>");
                sSql.Append("<SizeID>" + (objImportExcelDetail.SizeID) + "</SizeID>");
                sSql.Append("<SizeCode>" + (objImportExcelDetail.SizeCode).Replace("&", "&#38;") + "</SizeCode>");
                sSql.Append("<VisualOrder>" + (objImportExcelDetail.VisualOrder) + "</VisualOrder>");
                sSql.Append("<Active>" + (objImportExcelDetail.Active) + "</Active>");
                sSql.Append("<SizeName>" + (objImportExcelDetail.SizeName).Replace("&", "&#38;") + "</SizeName>");
                sSql.Append("</StyleMaster>");

            }
            return sSql.ToString().Replace("'", "&apos;").Replace("&", "&#38;");
        }


        public override SelectDesignGradeLookUpResponse SelectDesignGradeLookUp(SelectDesignGradeLookUpRequest ObjRequest)
        {
            var StyleMasterList = new List<DesignGradeTypes>();
            var RequestData = (SelectDesignGradeLookUpRequest)ObjRequest;
            var ResponseData = new SelectDesignGradeLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,Grade from DesignGrade with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleMaster = new DesignGradeTypes();
                        objStyleMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleMaster.Grade = Convert.ToString(objReader["Grade"]);
                        StyleMasterList.Add(objStyleMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignGradeList = StyleMasterList;
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

        public override SelectDesignDevelopmentOfficeLookUpResponse SelectDesignDevelopmentOfficeLookUp(SelectDesignDevelopmentOfficeLookUpRequest ObjRequest)
        {
            var StyleMasterList = new List<DesignDevelopmentOfficeTypes>();
            var RequestData = (SelectDesignDevelopmentOfficeLookUpRequest)ObjRequest;
            var ResponseData = new SelectDesignDevelopmentOfficeLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select ID,DevelopmentOffice from DesignDevelopmentOffice with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleMaster = new DesignDevelopmentOfficeTypes();
                        objStyleMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleMaster.DevelopmentOffice = Convert.ToString(objReader["DevelopmentOffice"]);
                        StyleMasterList.Add(objStyleMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignDevelopmentOfficeList = StyleMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design Master");
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
        public override InserSKUImageResponse InsertSKUImages(InserSKUImageRequest ObjRequest)
        {
            var RequestData = (InserSKUImageRequest)ObjRequest;
            var ResponseData = new InserSKUImageResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertBulkSKUImages", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                SqlParameter SKUImageData = _CommandObj.Parameters.Add("@SKUImageData", SqlDbType.Xml);
                SKUImageData.Direction = ParameterDirection.Input;
                SKUImageData.Value = ImageXml(RequestData.ImageList);

                SqlParameter StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                SqlParameter StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter SKUImageIDs = _CommandObj.Parameters.Add("@SKUImageIDs", SqlDbType.VarChar, int.MaxValue);
                SKUImageIDs.Direction = ParameterDirection.Output;

                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.IDs = Convert.ToString(SKUImageIDs.Value);
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "SKU Images");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "SKU Images");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "Style Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }
        public string ImageXml(List<ItemImageMaster> ImageList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (ItemImageMaster objItemImageMaster in ImageList)
            {
                sSql.Append("<SKUImageData>");
                sSql.Append("<ID>" + (objItemImageMaster.ID) + "</ID>");
                sSql.Append("<DesignID>" + (objItemImageMaster.DesignID) + "</DesignID>");
                sSql.Append("<StyleID>" + (objItemImageMaster.StyleID) + "</StyleID>");
                sSql.Append("<StyleCode>" + (objItemImageMaster.StyleCode).Replace("&", "&#38;") + "</StyleCode>");
                sSql.Append("<SKUID>" + (objItemImageMaster.SKUID) + "</SKUID>");

                sSql.Append("<SKUImage>" + (Convert.ToBase64String(objItemImageMaster.SKUImage)) + "</SKUImage>");
                // sSql.Append("<SKUImage>" + (objItemImageMaster.SKUImage) + "</SKUImage>");
                sSql.Append("</SKUImageData>");
            }
            return sSql.ToString().Replace("'", "&apos;").Replace("&", "&#38;");
        }




        public override GetStyleNameResponse GetStyleName(GetStyleNameRequest RequestObj)
        {
            //var SKUMasterTypes = new SKUMasterTypes();
            var StyleRecord = new StyleMaster();
            var RequestData = (GetStyleNameRequest)RequestObj;
            var ResponseData = new GetStyleNameResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            var sSql = new StringBuilder();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);


                sSql.Append("select Stylename from stylemaster where stylecode='" + RequestData.Department + "-" + RequestData.ProductCode + "'");



                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyle = new StyleMaster();
                        objStyle.StyleName = objReader["StyleName"].ToString();
                        ResponseData.StyleData = objStyle;
                        ResponseData.ResponseDynamicData = objStyle;

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

        public override SelectAllStyleResponse API_SelectAll(SelectAllStyleRequest objRequest)
        {
            var StyleList = new List<StyleMaster>();
            var RequestData = (SelectAllStyleRequest)objRequest;
            var ResponseData = new SelectAllStyleResponse();


            SelectColorDetailsRequest objSelectColorDetailsRequest = new SelectColorDetailsRequest();
            SelectColorDetailsResponse objSelectColorDetailsResponse = new SelectColorDetailsResponse();
            objSelectColorDetailsRequest.Limit = objRequest.Limit;
            objSelectColorDetailsRequest.Offset = objRequest.Offset;
            objSelectColorDetailsResponse = SelectStyleWithColorDetailsNew(objSelectColorDetailsRequest);


            SelectScaleDetailsRequest objSelectScaleDetailsRequest = new SelectScaleDetailsRequest();
            SelectScaleDetailsResponse objSelectScaleDetailsResponse = new SelectScaleDetailsResponse();
            objSelectScaleDetailsRequest.Limit = objRequest.Limit;
            objSelectScaleDetailsRequest.Offset = objRequest.Offset;
            objSelectScaleDetailsResponse = SelectStyleWithScaleDetailsNew(objSelectScaleDetailsRequest);



            SelectItemImageRequest objSelectItemImageRequest = new SelectItemImageRequest();
            SelectItemImageResponse objSelectItemImageResponse = new SelectItemImageResponse();
            objSelectItemImageRequest.FormName = "Style";
            objSelectItemImageRequest.Limit = objRequest.Limit;
            objSelectItemImageRequest.Offset = objRequest.Offset;
            objSelectItemImageResponse = SelectStyleWithItemImageDetailsNew(objSelectItemImageRequest);


            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "select SM.ID,SM.StyleCode,SM.StyleName,SM.ScaleID,SM.ShortDesignName,SM.Active,SM.StyleName,S.SeasonName,RC.TOTAL_CNT [RecordCount] " +
                    "from StyleMaster AS SM with(NoLock) JOIN SeasonMaster AS S with(NoLock) on SM.SeasonID = S.ID "+
                    "LEFT JOIN(Select  count(SM1.ID) As TOTAL_CNT From StyleMaster SM1 with(NoLock)" +
                    "JOIN SeasonMaster AS S1 with(NoLock) on SM1.SeasonID = S1.ID ";
                sQuery = sQuery + "where SM1.Active = " + RequestData.IsActive + " ";
                sQuery = sQuery + "and (isnull('" + RequestData.SearchString + "','') = '' ";
                sQuery = sQuery + "or SM1.StyleCode like isnull('%" + RequestData.SearchString + "%','') ";
                sQuery = sQuery + "or S1.SeasonName like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ";

                sQuery = sQuery + "where SM.Active = " + RequestData.IsActive + " ";
                sQuery = sQuery + "and (isnull('" + RequestData.SearchString + "','') = '' ";
                sQuery = sQuery + "or SM.StyleCode like isnull('%" + RequestData.SearchString + "%','') ";
                sQuery = sQuery + "or S.SeasonName like isnull('%" + RequestData.SearchString + "%','')) ";
                sQuery = sQuery + "order by ID asc ";
                sQuery = sQuery + "offset " + RequestData.Offset + " rows ";
                sQuery = sQuery + "fetch first " + RequestData.Limit + " rows only";


                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StyleMaster objStyle = new StyleMaster();

                        objStyle.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyle.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyle.StyleName = Convert.ToString(objReader["StyleName"]);
                        objStyle.ShortDesignName = Convert.ToString(objReader["ShortDesignName"]);
                                   
                        objStyle.SeasonName = Convert.ToString(objReader["SeasonName"]);

                    
                        objStyle.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
          

                        objStyle.ScaleDetailMasterList = new List<ScaleDetailMaster>();

                       
                       // objSelectScaleDetailsRequest.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objSelectScaleDetailsRequest.ScaleID = objReader["ScaleID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleID"]) : 0;
                        objSelectScaleDetailsResponse = SelectStyleWithScaleDetails(objSelectScaleDetailsRequest);
                        if (objSelectScaleDetailsResponse.StatusCode == Enums.OpStatusCode.Success)
                        {
                            objStyle.ScaleDetailMasterList = objSelectScaleDetailsResponse.ScaleDetailMasterRecord.Where(x=>x.ScaleHeaderID==objSelectScaleDetailsRequest.ScaleID).ToList();
                        }

                        objStyle.ColorMasterList = new List<ColorMaster>();
                        objSelectColorDetailsRequest.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        if (objSelectColorDetailsResponse.StyleWithColorDetailsRecord != null)
                        {
                            List<ColorMaster> colorlist = new List<ColorMaster>();
                            objStyle.ColorMasterList = objSelectColorDetailsResponse.StyleWithColorDetailsRecord.Where(x => x.StyleCode == objSelectColorDetailsRequest.StyleCode).ToList();
                            //foreach (var item in itemcolorlist)
                            //{
                            //    colorlist.Add(item);
                            //}
                            //objStyle.ColorMasterList = colorlist;
                        }
                    

                        objStyle.ItemImageMasterList = new List<ItemImageMaster>();

                        int  styleimage = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        
                        if (objSelectItemImageResponse.ItemImageMaster != null)
                        {

                            //List<ItemImageMaster> itemimagemasterlist = new List<ItemImageMaster>();
                            objStyle.ItemImageMasterList = objSelectItemImageResponse.ItemImageMaster.Where(x => x.StyleID == styleimage).ToList();
                            //foreach(var item in itemimagelist)
                            //{
                            //    itemimagemasterlist.Add(item);
                            //}
                            //objStyle.ItemImageMasterList = itemimagelist;
                        }
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                        StyleList.Add(objStyle);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleList = StyleList;
                    //ResponseData.ResponseDynamicData = StyleList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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

        public override SelectAllStyleResponse API_GetStyleColorScale(SelectAllStyleRequest objRequest)
        {
            var StyleList = new List<StyleColorSizeType>();
            var RequestData = (SelectAllStyleRequest)objRequest;
            var ResponseData = new SelectAllStyleResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                sQuery = "Select SCD.ColorCode,SM.StyleCode, 'Color' [Type] from StyleMAster SM JOIN StyleWithColorDetails SCD ON SM.ID = SCD.StlyeID where SM.StyleCode = '" + objRequest.StyleCode + "' Union Select SCD.SizeCode as ColorCode,SM.StyleCode, 'Size'[Type] from StyleMAster SM JOIN StyleWithScaleDetails SCD ON SM.ID = SCD.StyleID where SM.StyleCode = '" + objRequest.StyleCode + "' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        StyleColorSizeType objStyle = new StyleColorSizeType();

                        objStyle.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyle.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objStyle.Type = Convert.ToString(objReader["Type"]);


                        StyleList.Add(objStyle);

                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.styleColorSizeTypesList = StyleList;
                    ResponseData.ResponseDynamicData = StyleList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style Master");
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

        public override SelectItemImageResponse SelectStyleWithItemImageDetailsNew(SelectItemImageRequest ObjRequest)
        {
            var ItemImageMasterList = new List<ItemImageMaster>();
            var RequestData = (SelectItemImageRequest)ObjRequest;
            var ResponseData = new SelectItemImageResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                if (RequestData.FormName == "Style")
                {
                    sSql.Append("select * from SKUImages where styleid in (select SM.ID from StyleMaster AS SM with(NoLock) ");
                    sSql.Append("JOIN SeasonMaster AS S with(NoLock) on SM.SeasonID = S.ID where SM.Active = 1 and(isnull('', '') = '' or SM.StyleCode = isnull('', '') or S.SeasonName = isnull('', '')) order by ID asc offset " + RequestData.Offset + " rows fetch first " + ObjRequest.Limit+" rows only)");

                    sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                    _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                    _CommandObj.CommandType = CommandType.Text;
                    objReader = _CommandObj.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        if (ItemImageMasterList != null)
                        {
                            while (objReader.Read())
                            {
                                var objItemImageMaster = new ItemImageMaster();
                                objItemImageMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                                objItemImageMaster.SKUImage = objReader["SKUImage"] != DBNull.Value ? (byte[])objReader["SKUImage"] : null;
                                objItemImageMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                                //objItemImageMaster.StyleCode = objReader["StyleCode"] != DBNull.Value ? (byte[])objReader["StyleCode"] : null;
                                //objItemImageMaster.ItemImage = Convert.ToString(objReader["ItemImage"]);
                                objItemImageMaster.StyleID = objReader["StyleID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleID"]) : 0;

                                ItemImageMasterList.Add(objItemImageMaster);
                            }
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.ItemImageMaster = ItemImageMasterList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Image");
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

        public override SelectScaleDetailsResponse SelectStyleWithScaleDetailsNew(SelectScaleDetailsRequest ObjRequest)
        {
            var ScaleDetailMasterList = new List<ScaleDetailMaster>();
            var RequestData = (SelectScaleDetailsRequest)ObjRequest;
            var ResponseData = new SelectScaleDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                //sSql.Append("select * from  StyleWithScaleDetails ");
                //sSql.Append("where  StyleID=" + RequestData.ID + " and active = 'true' ");   

                sSql.Append(";with Style as( select Distinct SM.ID,SM.ScaleID from StyleMaster AS SM with(NoLock) JOIN SeasonMaster AS S with(NoLock) on SM.SeasonID = S.ID where SM.Active = 1 and (isnull('','') = '' or SM.StyleCode = isnull('','') or S.SeasonName = isnull('','')) order by ID asc offset "+ObjRequest.Offset +" rows fetch first "+ ObjRequest.Limit +" rows only),");
                sSql.Append("StyleScale as(select ID, StyleID, ScaleID, SizeID, Active from StyleWithScaleDetails ssd where ssd.StyleID in (select distinct ID from Style) and ssd.Active = 1)");
                sSql.Append("select ssd.ID,smd.ID as SizeID,smd.SizeCode,smd.ScaleHeaderID,smd.Description,ssd.Active  from ScaleMasterDetails smd join StyleScale ssd on smd.ScaleHeaderID = ssd.ScaleID and smd.ID = ssd.SizeID");

                if (RequestData.RequestFrom == Enums.RequestFrom.Upload)
                {
                    sSql.Append(" and ssd.Active='True'");
                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objScaleDetailMaster = new ScaleDetailMaster();
                        objScaleDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SSID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objScaleDetailMaster.SizeID = objReader["SizeID"] != DBNull.Value ? Convert.ToInt32(objReader["SizeID"]) : 0;
                        objScaleDetailMaster.SizeCode = Convert.ToString(objReader["SizeCode"]);
                        objScaleDetailMaster.Description = Convert.ToString(objReader["Description"]);
                        objScaleDetailMaster.ScaleHeaderID = objReader["ScaleHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleHeaderID"]) : 0;
                        objScaleDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        ScaleDetailMasterList.Add(objScaleDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ScaleDetailMasterRecord = ScaleDetailMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style");
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

        public override SelectColorDetailsResponse SelectStyleWithColorDetailsNew(SelectColorDetailsRequest ObjRequest)
        {
            var StyleWithColorDetailsMasterList = new List<ColorMaster>();
            var RequestData = (SelectColorDetailsRequest)ObjRequest;
            var ResponseData = new SelectColorDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                sSql.Append(";with Style as (select Distinct SM.ID from StyleMaster AS SM with(NoLock) JOIN SeasonMaster AS S with(NoLock) on SM.SeasonID = S.ID where SM.Active = 1 and(isnull('', '') = '' or SM.StyleCode = isnull('', '') or S.SeasonName = isnull('', '')) order by ID asc offset "+ObjRequest.Offset+" rows fetch first "+ObjRequest.Limit+" rows only) ");
                sSql.Append("Select Distinct scd.ID AS SCID,scd.ColorID,scd.ColorCode,scd.Active,scd.StyleCode,scd.ColorName as Description,scd.StlyeID,CM.Colors as Color from StyleWithColorDetails scd join Style sm on sm.ID = scd.StlyeID join ColorMaster AS CM on scd.ColorID = cm.ID  ");


                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new ColorMaster();
                        objStyleWithColorDetailMaster.SCID = objReader["SCID"] != DBNull.Value ? Convert.ToInt32(objReader["SCID"]) : 0;
                        objStyleWithColorDetailMaster.ID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objStyleWithColorDetailMaster.ColorID = objReader["ColorID"] != DBNull.Value ? Convert.ToInt32(objReader["ColorID"]) : 0;
                        objStyleWithColorDetailMaster.ColorCode = Convert.ToString(objReader["ColorCode"]);
                        objStyleWithColorDetailMaster.StyleCode = Convert.ToString(objReader["StyleCode"]);
                        objStyleWithColorDetailMaster.Description = Convert.ToString(objReader["Description"]);
                        objStyleWithColorDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : false;
                        objStyleWithColorDetailMaster.Colors = objReader["Color"] != DBNull.Value ? Convert.ToInt32(objReader["Color"]) : 0;
                        StyleWithColorDetailsMasterList.Add(objStyleWithColorDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.StyleWithColorDetailsRecord = StyleWithColorDetailsMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Style");
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
