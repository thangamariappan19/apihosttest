using EasyBizAbsDAL.Import;
using EasyBizDBTypes.Common;
using EasyBizRequest.Import.StylePricingRequest;
using EasyBizResponse.Import.StylePricingResponse;
using MsSqlDAL.Common;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDAL.Import
{
    public class ImportStylePricingDAL : BaseImportStylePricingDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;        
        string _ConnectionString; 
        Enums.RequestFrom _RequestFrom;
        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveStylePricingMasterRequest)RequestObj;
            var ResponseData = new SaveStylePricingMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("ImportInsertStylePricingMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                var ImportStylePricingDetails = _CommandObj.Parameters.Add("@ImportStylePricingDetails", SqlDbType.Xml);
                ImportStylePricingDetails.Direction = ParameterDirection.Input;
                ImportStylePricingDetails.Value = ImportStylePricingXML(RequestData.ImportStylePricingExcelList);

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;
                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Style Pricing Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Style Pricing Master");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Style Pricing Master");
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

        public string ImportStylePricingXML(List<EasyBizDBTypes.Transactions.Pricing.StylePricing> ImportStylePricingExcelList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (EasyBizDBTypes.Transactions.Pricing.StylePricing objImportExcelDetail in ImportStylePricingExcelList)
            {
                sSql.Append("<StylePricing>");
                sSql.Append("<ID>0</ID>");
                sSql.Append("<SKUID>" + objImportExcelDetail.SKUID + "</SKUID>");
                sSql.Append("<SKUCode>" + objImportExcelDetail.SKUCode + "</SKUCode>");
                sSql.Append("<PriceListID>" + (objImportExcelDetail.PriceListID) + "</PriceListID>");
                sSql.Append("<PriceListCurrency>" + (objImportExcelDetail.PriceListCurrency) + "</PriceListCurrency>");
                sSql.Append("<Price>" + objImportExcelDetail.Price + "</Price>");
                sSql.Append("<IsManualEntry>" + objImportExcelDetail.IsManualEntry + "</IsManualEntry>");
                sSql.Append("<CreateBy>" + objImportExcelDetail.CreateBy + "</CreateBy>");
                sSql.Append("<CreateOn>" + objImportExcelDetail.CreateOn + "</CreateOn>");
                sSql.Append("<UpdateBy>" + objImportExcelDetail.UpdateBy + "</UpdateBy>");
                sSql.Append("<UpdateOn>" + objImportExcelDetail.UpdateOn + "</UpdateOn>");
                sSql.Append("<SCN>" + objImportExcelDetail.SCN + "</SCN>");
                sSql.Append("<Active>" + objImportExcelDetail.Active + "</Active>");
                sSql.Append("<AppVersion>" + objImportExcelDetail.AppVersion + "</AppVersion>");
                sSql.Append("<IsStoreSync>" + objImportExcelDetail.IsStoreSync + "</IsStoreSync>");
                sSql.Append("<IsCountrySync>" + objImportExcelDetail.IsCountrySync + "</IsCountrySync>");
                sSql.Append("<IsServerSync>" + objImportExcelDetail.IsServerSync + "</IsServerSync>");
                sSql.Append("</StylePricing>");
            }
            return sSql.ToString().Replace("&", "&#38;"); 
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
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectAll(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType SelectByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override EasyBizResponse.BaseResponseType DeleteByIDs(EasyBizRequest.BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }
    }
}
