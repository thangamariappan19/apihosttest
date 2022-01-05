using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest;
using EasyBizRequest.Masters.CouponMasterRequest;
using EasyBizResponse;
using EasyBizResponse.Masters.CouponMasterResponse;
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
    public class CouponMasterDAL : BaseCouponMasterDAL
    {
        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        string _ConnectionString;Enums.RequestFrom _RequestFrom;

        public override BaseResponseType InsertRecord(BaseRequestType RequestObj)
        {
            var RequestData = (SaveCouponMasterRequest)RequestObj;
            var ResponseData = new SaveCouponMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("[InsertCouponMaster]", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CouponMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@CouponCode", RequestData.CouponMasterData.CouponCode);
                _CommandObj.Parameters.AddWithValue("@Coupondescription", RequestData.CouponMasterData.Coupondescription);
                _CommandObj.Parameters.AddWithValue("@BarCode", RequestData.CouponMasterData.BarCode);
                _CommandObj.Parameters.AddWithValue("@Country", RequestData.CouponMasterData.Country);
                _CommandObj.Parameters.AddWithValue("@CouponType", RequestData.CouponMasterData.CouponType);
                _CommandObj.Parameters.AddWithValue("@StartDate", RequestData.CouponMasterData.StartDate);
                _CommandObj.Parameters.AddWithValue("@EndDate", RequestData.CouponMasterData.EndDate);
                _CommandObj.Parameters.AddWithValue("@DiscountType", RequestData.CouponMasterData.DiscountType);
                _CommandObj.Parameters.AddWithValue("@DiscountValue", RequestData.CouponMasterData.DiscountValue);
                _CommandObj.Parameters.AddWithValue("@IssuableAtPOS", RequestData.CouponMasterData.IssuableAtPOS);
                _CommandObj.Parameters.AddWithValue("@Serial", RequestData.CouponMasterData.Serial);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CouponMasterData.Remarks);               
                _CommandObj.Parameters.AddWithValue("@CouponSerialCode", RequestData.CouponMasterData.CouponSerialCode);
                _CommandObj.Parameters.AddWithValue("@Issuedstatus", RequestData.CouponMasterData.Issuedstatus);
                _CommandObj.Parameters.AddWithValue("@PhysicalStore", RequestData.CouponMasterData.PhysicalStore);
                _CommandObj.Parameters.AddWithValue("@Remainingamount", RequestData.CouponMasterData.Remainingamount);
                _CommandObj.Parameters.AddWithValue("@Redeemedstatus", RequestData.CouponMasterData.Redeemedstatus);

                var StoreDataDetails = _CommandObj.Parameters.Add("@StoreDataDetails", SqlDbType.Xml);
                StoreDataDetails.Direction = ParameterDirection.Input;
                StoreDataDetails.Value = StoreMasterMasterXML(RequestData.StoreCommonUtilData);


                var CustomerDataDetails = _CommandObj.Parameters.Add("@CustomerUtilDataDetails", SqlDbType.Xml);
                CustomerDataDetails.Direction = ParameterDirection.Input;
                CustomerDataDetails.Value = CustomerMasterMasterXML(RequestData.CustomerCommonUtilData);

                var TotalDataDetails = _CommandObj.Parameters.Add("@TotalUtilDataDetails", SqlDbType.Xml);
                TotalDataDetails.Direction = ParameterDirection.Input;
                TotalDataDetails.Value = TotalMasterMasterXML(RequestData.TotalMasterCommonUtilData);


                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CouponMasterData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public override BaseResponseType UpdateRecord(BaseRequestType RequestObj)
        {
            var RequestData = (UpdateCouponMasterRequest)RequestObj;
            var ResponseData = new UpdateCouponMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("[UpdateCouponMaster1]", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;

                _CommandObj.Parameters.AddWithValue("@ID", RequestData.CouponMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@CouponCode", RequestData.CouponMasterData.CouponCode);
                _CommandObj.Parameters.AddWithValue("@Coupondescription", RequestData.CouponMasterData.Coupondescription);
                _CommandObj.Parameters.AddWithValue("@BarCode", RequestData.CouponMasterData.BarCode);
                _CommandObj.Parameters.AddWithValue("@Country", RequestData.CouponMasterData.Country);
                _CommandObj.Parameters.AddWithValue("@CouponType", RequestData.CouponMasterData.CouponType);
                _CommandObj.Parameters.AddWithValue("@StartDate", RequestData.CouponMasterData.StartDate);
                _CommandObj.Parameters.AddWithValue("@EndDate", RequestData.CouponMasterData.EndDate);
                _CommandObj.Parameters.AddWithValue("@DiscountType", RequestData.CouponMasterData.DiscountType);
                _CommandObj.Parameters.AddWithValue("@DiscountValue", RequestData.CouponMasterData.DiscountValue);
                _CommandObj.Parameters.AddWithValue("@IssuableAtPOS", RequestData.CouponMasterData.IssuableAtPOS);
                _CommandObj.Parameters.AddWithValue("@Serial", RequestData.CouponMasterData.Serial);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.CouponMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@CouponSerialCode", RequestData.CouponMasterData.CouponSerialCode);
                _CommandObj.Parameters.AddWithValue("@Issuedstatus", RequestData.CouponMasterData.Issuedstatus);
                _CommandObj.Parameters.AddWithValue("@PhysicalStore", RequestData.CouponMasterData.PhysicalStore);
                _CommandObj.Parameters.AddWithValue("@Remainingamount", RequestData.CouponMasterData.Remainingamount);
                _CommandObj.Parameters.AddWithValue("@Redeemedstatus", RequestData.CouponMasterData.Redeemedstatus);

                var StoreDataDetails = _CommandObj.Parameters.Add("@StoreDataDetails", SqlDbType.Xml);
                StoreDataDetails.Direction = ParameterDirection.Input;
                StoreDataDetails.Value = StoreMasterMasterXML(RequestData.StoreCommonUtilData);


                var CustomerDataDetails = _CommandObj.Parameters.Add("@CustomerUtilDataDetails", SqlDbType.Xml);
                CustomerDataDetails.Direction = ParameterDirection.Input;
                CustomerDataDetails.Value = CustomerMasterMasterXML(RequestData.CustomerCommonUtilData);

                var TotalDataDetails = _CommandObj.Parameters.Add("@TotalUtilDataDetails", SqlDbType.Xml);
                TotalDataDetails.Direction = ParameterDirection.Input;
                TotalDataDetails.Value = TotalMasterMasterXML(RequestData.TotalMasterCommonUtilData);


                var CreateBy = _CommandObj.Parameters.Add("@CreateBy", SqlDbType.Int);
                CreateBy.Direction = ParameterDirection.Input;
                CreateBy.Value = RequestData.CouponMasterData.CreateBy;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;


                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.ExecuteNonQuery();

                var strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Coupon Master");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Coupon Master");
                }
                else
                {
                    try
                    {
                        ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Coupon Master");
                ResponseData.ExceptionMessage = ex.Message;
                ResponseData.StackTrace = ex.StackTrace;
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }

            return ResponseData;
        }

        public string StoreMasterMasterXML(List<CommonUtil> StoreMasterMasterList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objScaleDetailMasterDetails in StoreMasterMasterList)
            {
                sSql.Append("<StoreDetailsMasterData>");
                sSql.Append("<ID>" + (objScaleDetailMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objScaleDetailMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objScaleDetailMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objScaleDetailMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + (objScaleDetailMasterDetails.TypeName) + "</Type>");
                sSql.Append("<Active>" + objScaleDetailMasterDetails.Active + "</Active>");
                sSql.Append("</StoreDetailsMasterData>");
            }
            return sSql.ToString();
        }

        public string CustomerMasterMasterXML(List<CommonUtil> CommonUtilList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objCustomerMasterDetails in CommonUtilList)
            {
                sSql.Append("<CustomerMasterData>");
                sSql.Append("<ID>" + (objCustomerMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objCustomerMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objCustomerMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objCustomerMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + objCustomerMasterDetails.TypeName + "</Type>");
                sSql.Append("<Active>" + objCustomerMasterDetails.Active + "</Active>");
              
                sSql.Append("</CustomerMasterData>");
            }
            return sSql.ToString();
        }


        public string TotalMasterMasterXML(List<CommonUtil> CommonUtilList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (CommonUtil objTotalMasterMasterDetails in CommonUtilList)
            {
                sSql.Append("<TotalMasterMasterXML>");
                sSql.Append("<ID>" + (objTotalMasterMasterDetails.ID) + "</ID>");
                sSql.Append("<TypeID>" + (objTotalMasterMasterDetails.DocumentID) + "</TypeID>");
                sSql.Append("<Code>" + objTotalMasterMasterDetails.DocumentCode + "</Code>");
                sSql.Append("<Name>" + (objTotalMasterMasterDetails.DocumentName) + "</Name>");
                sSql.Append("<Type>" + objTotalMasterMasterDetails.TypeName + "</Type>");
                sSql.Append("<Active>" + objTotalMasterMasterDetails.Active + "</Active>");
               
                sSql.Append("</TotalMasterMasterXML>");
            }
            return sSql.ToString();
        }

        public override BaseResponseType DeleteRecord(BaseRequestType RequestObj)
        {
            var CouponMasterRecord = new CouponMaster();

            var RequestData = (DeleteCouponMasterRequest)RequestObj;
            var ResponseData = new DeleteCouponMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Update CouponMaster set Active='false' where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Coupon Master");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Coupon Master");
            }
            return ResponseData;
        }

        public override BaseResponseType SelectRecord(BaseRequestType RequestObj)
        {
            var CouponMasterRecord = new CouponMaster();
            var RequestData = (SelectByIDCouponMasterRequest)RequestObj;
            var ResponseData = new SelectByIDCouponMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from CouponMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        objCouponMaster.StartDate = objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponMaster.EndDate = objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCouponMaster.DiscountValue = objReader["DiscountValue"] != DBNull.Value ? Convert.ToDouble(objReader["DiscountValue"]) : 0;
                        objCouponMaster.IssuableAtPOS = objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;
                        objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        ResponseData.CouponMasterRecord = objCouponMaster;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;

        }

        public override BaseResponseType SelectAll(BaseRequestType RequestObj)
        {
            var CouponMasterList = new List<CouponMaster>();
            var RequestData = (SelectAllCouponMasterRequest)RequestObj;
            var ResponseData = new SelectAllCouponMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from CouponMaster with(NoLock)  ";
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        objCouponMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objCouponMaster.CouponCode = Convert.ToString(objReader["CouponCode"]);
                        objCouponMaster.Coupondescription = Convert.ToString(objReader["Coupondescription"]);
                        objCouponMaster.BarCode = Convert.ToString(objReader["BarCode"]);
                        objCouponMaster.Country = Convert.ToString(objReader["Country"]);
                        objCouponMaster.CouponType = Convert.ToString(objReader["CouponType"]);
                        objCouponMaster.StartDate =objReader["StartDate"] != DBNull.Value ? Convert.ToDateTime(objReader["StartDate"]) : DateTime.Now;
                        objCouponMaster.EndDate =objReader["EndDate"] != DBNull.Value ? Convert.ToDateTime(objReader["EndDate"]) : DateTime.Now;
                        objCouponMaster.DiscountType = Convert.ToString(objReader["DiscountType"]);
                        objCouponMaster.DiscountValue =objReader["DiscountValue"] != DBNull.Value ? Convert.ToDouble(objReader["DiscountValue"]) :0;
                        objCouponMaster.IssuableAtPOS =objReader["IssuableAtPOS"] != DBNull.Value ? Convert.ToBoolean(objReader["IssuableAtPOS"]) : true;
                        objCouponMaster.Serial = objReader["Serial"] != DBNull.Value ? Convert.ToBoolean(objReader["Serial"]) : true;
                        objCouponMaster.CouponStoreType = Convert.ToString(objReader["CouponStoreType"]);
                        objCouponMaster.CouponCustomerType = Convert.ToString(objReader["CouponCustomerType"]);
                        objCouponMaster.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objCouponMaster.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objCouponMaster.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objCouponMaster.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objCouponMaster.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objCouponMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        CouponMasterList.Add(objCouponMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponMasterList = CouponMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override BaseResponseType SelectByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override BaseResponseType DeleteByIDs(BaseRequestType RequestObj)
        {
            throw new NotImplementedException();
        }

        public override SelectCouponMasterLookUpResponse SelectCouponMasterLookUp(SelectCouponMasterLookUpRequest RequestObj)
        {
            var CouponMasterList = new List<CouponMaster>();


            SelectCouponMasterLookUpRequest RequestData = new SelectCouponMasterLookUpRequest();
            SelectCouponMasterLookUpResponse ResponseData = new SelectCouponMasterLookUpResponse();
            SqlDataReader objReader;
            MsSqlCommon sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,CouponName from CouponMaster with(NoLock) where Active='True' ";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objCouponMaster = new CouponMaster();
                        //objCouponMaster.ID = Convert.ToInt16(objReader["ID"]);
                        //objCouponMaster.CouponName = Convert.ToString(objReader["CouponName"]);
                        CouponMasterList.Add(objCouponMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CouponMasterList = CouponMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Coupon Master");
                }
            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.GeneralError;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override SelectCouponStoreDetailsResponse SelectCouponMasterStoreType(SelectCouponStoreDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new List<CommonUtil>();
          
            var RequestData = (SelectCouponStoreDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponStoreDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();

                if (RequestData.CouponStoreType != "")
                {
                    sSql.Append("select * from  CouponStoreMasterDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " and  Type='" + RequestData.CouponStoreType + "'");
                }
                else
                {
                    sSql.Append("select * from  CouponStoreMasterDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " ");
                }
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

               
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var objStyleWithColorDetailMaster = new CommonUtil();
                            objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                            objStyleWithColorDetailMaster.DocumentID =objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) :0;
                            objStyleWithColorDetailMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                            objStyleWithColorDetailMaster.DocumentName = Convert.ToString(objReader["Name"]);
                            objStyleWithColorDetailMaster.TypeName = Convert.ToString(objReader["Type"]);                       
                            objStyleWithColorDetailMaster.Active =objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                            objStyleWithColorDetailMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                            StoreMasterDetailsMasterList.Add(objStyleWithColorDetailMaster);
                        }
                        ResponseData.StatusCode = Enums.OpStatusCode.Success;
                        ResponseData.StoreCommonUtil = StoreMasterDetailsMasterList;
                    }
                    else
                    {
                        ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                        ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
                    }

                
               


            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }



        public override SelectCouponCustomerDetailsResponse SelectCouponMasterCustomerType(SelectCouponCustomerDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new List<CommonUtil>();           
            var RequestData = (SelectCouponCustomerDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponCustomerDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                if (RequestData.CouponCustomerType != "")
                {
                    sSql.Append("select * from  CouponCustomerDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " and  Type='" + RequestData.CouponCustomerType + "'");
                }
                else
                {
                    sSql.Append("select * from  CouponCustomerDetails ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " ");
                }
              
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new CommonUtil();
                        objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleWithColorDetailMaster.DocumentID =objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]) :0;
                        objStyleWithColorDetailMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objStyleWithColorDetailMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objStyleWithColorDetailMaster.TypeName = Convert.ToString(objReader["Type"]);                      
                        objStyleWithColorDetailMaster.Active =objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleWithColorDetailMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                        StoreMasterDetailsMasterList.Add(objStyleWithColorDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.CustomerCommonUtil = StoreMasterDetailsMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
                }

               


            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override SelectCouponProductCategoryDetailsResponse SelectCouponMasterProductType(SelectCouponProductCategoryDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new List<CommonUtil>();        
            var RequestData = (SelectCouponProductCategoryDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponProductCategoryDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                if (RequestData.CouponProductCategoryType!="")
                {
                    sSql.Append("select * from  CouponApplicablelist ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + " and  Type='" + RequestData.CouponProductCategoryType + "'");
                }
                else
                {
                    sSql.Append("select * from  CouponApplicablelist ");
                    sSql.Append("where  CouponID=" + RequestData.CouponID + "");
                }
              

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new CommonUtil();
                        objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleWithColorDetailMaster.DocumentID = objReader["DocumentID"] != DBNull.Value ? Convert.ToInt32(objReader["DocumentID"]):0;
                        objStyleWithColorDetailMaster.DocumentCode = Convert.ToString(objReader["Code"]);
                        objStyleWithColorDetailMaster.DocumentName = Convert.ToString(objReader["Name"]);
                        objStyleWithColorDetailMaster.TypeName = Convert.ToString(objReader["Type"]);                      
                        objStyleWithColorDetailMaster.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;
                        objStyleWithColorDetailMaster.UpdateFlag = objReader["UpdateFlag"] != DBNull.Value ? Convert.ToBoolean(objReader["UpdateFlag"]) : true;
                        StoreMasterDetailsMasterList.Add(objStyleWithColorDetailMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.ProductCategoryCommonUtil = StoreMasterDetailsMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
                }




            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }

        public override SelectCouponCouponListDetailsResponse SelectCouponMasterList(SelectCouponCouponListDetailsRequest RequestObj)
        {
            var StoreMasterDetailsMasterList = new CouponMaster();
            var RequestData = (SelectCouponCouponListDetailsRequest)RequestObj;
            var ResponseData = new SelectCouponCouponListDetailsResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                var sSql = new StringBuilder();
                sSql.Append("select * from  CouponListDetails ");
                sSql.Append("where  CouponID=" + RequestData.CouponID + "");

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objStyleWithColorDetailMaster = new CouponMaster();
                        objStyleWithColorDetailMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objStyleWithColorDetailMaster.CouponID = objReader["CouponID"] != DBNull.Value ? Convert.ToInt32(objReader["CouponID"]) : 0;
                        //objScaleDetailMaster.ScaleHeaderID =objReader["ScaleHeaderID"] != DBNull.Value ? Convert.ToInt32(objReader["ScaleHeaderID"]) :0;
                        objStyleWithColorDetailMaster.CouponSerialCode = Convert.ToString(objReader["CouponSerialCode"]);
                        objStyleWithColorDetailMaster.Issuedstatus = Convert.ToString(objReader["Issuedstatus"]);
                        objStyleWithColorDetailMaster.PhysicalStore = Convert.ToString(objReader["PhysicalStore"]);
                        objStyleWithColorDetailMaster.Remainingamount = objReader["Remainingamount"] != DBNull.Value ? Convert.ToDouble(objReader["Remainingamount"]) : 0;
                        objStyleWithColorDetailMaster.Redeemedstatus = Convert.ToString(objReader["Redeemedstatus"]);
                        ResponseData.CouponMasterListDetails = objStyleWithColorDetailMaster;
                       
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    
                   
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Store Master Data");
                }




            }
            catch (Exception ex)
            {
                ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                ResponseData.DisplayMessage = ex.Message;
            }
            return ResponseData;
        }
    }
}
