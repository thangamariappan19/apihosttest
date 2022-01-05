using EasyBizAbsDAL.Masters;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Masters;
using EasyBizRequest.Masters.DesignMasterRequest;
using EasyBizRequest.Masters.DesignMasterResponse;
using EasyBizResponse.Masters.DesignMasterResponse;
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
     
    public class DesignMasterDAL : BaseDesignMasterDAL
    {

        SqlConnection _ConnectionObj;
        SqlCommand _CommandObj;
        
        string _ConnectionString;Enums.RequestFrom _RequestFrom;


        public override EasyBizResponse.BaseResponseType InsertRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (SaveDesignMasterRequest)RequestObj;
            var ResponseData = new SaveDesignMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertDesginMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@ID", RequestData.DesignMasterData.ID);
                _CommandObj.Parameters.AddWithValue("@DesignCode", RequestData.DesignMasterData.DesignCode);
                _CommandObj.Parameters.AddWithValue("@Description", RequestData.DesignMasterData.Description);
                _CommandObj.Parameters.AddWithValue("@ForeignDescription", RequestData.DesignMasterData.ForeignDescription);
                _CommandObj.Parameters.AddWithValue("@SegamentationID", RequestData.DesignMasterData.SegamentationID);
                _CommandObj.Parameters.AddWithValue("@StyleStatusID", RequestData.DesignMasterData.StyleStatusID);
                _CommandObj.Parameters.AddWithValue("@ProductLineID", RequestData.DesignMasterData.ProductLineID);
                _CommandObj.Parameters.AddWithValue("@ProductGroupID", RequestData.DesignMasterData.ProductGroupID);
                _CommandObj.Parameters.AddWithValue("@SeasonID", RequestData.DesignMasterData.SeasonID);
                _CommandObj.Parameters.AddWithValue("@DropID", RequestData.DesignMasterData.DropID);
                _CommandObj.Parameters.AddWithValue("@Grade", RequestData.DesignMasterData.Grade);
                _CommandObj.Parameters.AddWithValue("@CollectionID", RequestData.DesignMasterData.CollectionID);
                _CommandObj.Parameters.AddWithValue("@SubCollectionID", RequestData.DesignMasterData.SubCollectionID);
                _CommandObj.Parameters.AddWithValue("@Composition", RequestData.DesignMasterData.Composition);
                _CommandObj.Parameters.AddWithValue("@SimbolGroup", RequestData.DesignMasterData.SimbolGroup);
                _CommandObj.Parameters.AddWithValue("@BrandID", RequestData.DesignMasterData.BrandID);
                _CommandObj.Parameters.AddWithValue("@DesignerID", RequestData.DesignMasterData.DesignerID);
                _CommandObj.Parameters.AddWithValue("@DivisionID", RequestData.DesignMasterData.DivisionID);
                _CommandObj.Parameters.AddWithValue("@ProductDepartmentCode", RequestData.DesignMasterData.ProductDepartmentCode);              
                _CommandObj.Parameters.AddWithValue("@CreateBy", RequestData.DesignMasterData.CreateBy);
                _CommandObj.Parameters.AddWithValue("@DevelopmentOffice", RequestData.DesignMasterData.DevelopmentOffice);
                _CommandObj.Parameters.AddWithValue("@ShortDescription", RequestData.DesignMasterData.ShortDescription);
                _CommandObj.Parameters.AddWithValue("@YearID", RequestData.DesignMasterData.YearID);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.DesignMasterData.Remarks);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.DesignMasterData.Active);
                _CommandObj.Parameters.AddWithValue("@DesignName", RequestData.DesignMasterData.DesignName);

                var SegamentationCode = _CommandObj.Parameters.Add("@SegamentationCode", SqlDbType.NVarChar);
                SegamentationCode.Direction = ParameterDirection.Input;
                SegamentationCode.Value = RequestData.DesignMasterData.SegamentationCode;

                var StyleStatusCode = _CommandObj.Parameters.Add("@StyleStatusCode", SqlDbType.NVarChar);
                StyleStatusCode.Direction = ParameterDirection.Input;
                StyleStatusCode.Value = RequestData.DesignMasterData.StyleStatusCode;

                var ProductLineCode = _CommandObj.Parameters.Add("@ProductLineCode", SqlDbType.NVarChar);
                ProductLineCode.Direction = ParameterDirection.Input;
                ProductLineCode.Value = RequestData.DesignMasterData.ProductLineCode;

                var ProductGroupCode = _CommandObj.Parameters.Add("@ProductGroupCode", SqlDbType.NVarChar);
                ProductGroupCode.Direction = ParameterDirection.Input;
                ProductGroupCode.Value = RequestData.DesignMasterData.ProductGroupCode;

                var SeasonCode = _CommandObj.Parameters.Add("@SeasonCode", SqlDbType.NVarChar);
                SeasonCode.Direction = ParameterDirection.Input;
                SeasonCode.Value = RequestData.DesignMasterData.SeasonCode;

                var DropCode = _CommandObj.Parameters.Add("@DropCode", SqlDbType.NVarChar);
                DropCode.Direction = ParameterDirection.Input;
                DropCode.Value = RequestData.DesignMasterData.DropCode;

                var CollectionCode = _CommandObj.Parameters.Add("@CollectionCode", SqlDbType.NVarChar);
                CollectionCode.Direction = ParameterDirection.Input;
                CollectionCode.Value = RequestData.DesignMasterData.CollectionCode;

                var SubCollectionCode = _CommandObj.Parameters.Add("@SubCollectionCode", SqlDbType.NVarChar);
                SubCollectionCode.Direction = ParameterDirection.Input;
                SubCollectionCode.Value = RequestData.DesignMasterData.SubCollectionCode;

                var BrandCode = _CommandObj.Parameters.Add("@BrandCode", SqlDbType.NVarChar);
                BrandCode.Direction = ParameterDirection.Input;
                BrandCode.Value = RequestData.DesignMasterData.BrandCode;

                var DesignerCode = _CommandObj.Parameters.Add("@DesignerCode", SqlDbType.NVarChar);
                DesignerCode.Direction = ParameterDirection.Input;
                DesignerCode.Value = RequestData.DesignMasterData.DesignerCode;

                var DivisionCode = _CommandObj.Parameters.Add("@DivisionCode", SqlDbType.NVarChar);
                DivisionCode.Direction = ParameterDirection.Input;
                DivisionCode.Value = RequestData.DesignMasterData.DivisionCode;

                var YearCode = _CommandObj.Parameters.Add("@YearCode", SqlDbType.NVarChar);
                YearCode.Direction = ParameterDirection.Input;
                YearCode.Value = RequestData.DesignMasterData.YearCode; 

                var DesignID = _CommandObj.Parameters.Add("@DesignID", SqlDbType.Int);
                DesignID.Direction = ParameterDirection.Output;               

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                SqlParameter ID = _CommandObj.Parameters.Add("@ID1", SqlDbType.Int);
                ID.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Design");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.IDs = ID.Value.ToString();

                    DesignImageInsert(RequestData.DesignWithItemImageList, (int)DesignID.Value);

                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Design");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design");
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
        

        public bool  DesignImageInsert(List<ItemImageMaster> ItemImageMasterList , int ID)
        {
            bool objBool = false;
          
            var sqlCommon = new MsSqlCommon();

            sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
            try
            {
                if (ID > 0)
                {
                    string ssql = "Delete  SKUImages where DesignID='" + ID + "'";
                    _CommandObj = new SqlCommand(ssql.ToString(), _ConnectionObj);

                    _CommandObj.CommandType = CommandType.Text;
                    _CommandObj.ExecuteNonQuery();
                    _CommandObj.Dispose();
                }
                if (ItemImageMasterList!= null)
                {
                    foreach (ItemImageMaster objItemImageMaster in ItemImageMasterList)
                    {
                        _CommandObj = new SqlCommand("InsertSKUImages", _ConnectionObj);                       

                        SqlParameter DesignID = _CommandObj.Parameters.Add("@DesignID", SqlDbType.Int);
                        DesignID.Direction = ParameterDirection.Input;
                        DesignID.Value = ID;

                        SqlParameter StyleID = _CommandObj.Parameters.Add("@StyleID", SqlDbType.Int);
                        StyleID.Direction = ParameterDirection.Input;
                        StyleID.Value = objItemImageMaster.StyleID;

                        SqlParameter SKUID = _CommandObj.Parameters.Add("@SKUID", SqlDbType.Int);
                        SKUID.Direction = ParameterDirection.Input;
                        SKUID.Value = objItemImageMaster.SKUID;

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
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {               
                _CommandObj.Dispose();
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return objBool;
        } 


        public override EasyBizResponse.BaseResponseType UpdateRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var RequestData = (UpdateDesignMasterRequest)RequestObj;
            var ResponseData = new UpdateDesignMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                _CommandObj = new SqlCommand("UpdateDesginMaster", _ConnectionObj);
                _CommandObj.CommandType = CommandType.StoredProcedure;
                _CommandObj.Parameters.AddWithValue("@ID", RequestData.DesignMasterData.ID);
               // _CommandObj.Parameters.AddWithValue("@DesignCode", RequestData.DesignMasterData.DesignCode);
                _CommandObj.Parameters.AddWithValue("@DesignName", RequestData.DesignMasterData.DesignName);
                _CommandObj.Parameters.AddWithValue("@Description", RequestData.DesignMasterData.Description);
                _CommandObj.Parameters.AddWithValue("@ForeignDescription", RequestData.DesignMasterData.ForeignDescription);
                _CommandObj.Parameters.AddWithValue("@SegamentationID", RequestData.DesignMasterData.SegamentationID);
                _CommandObj.Parameters.AddWithValue("@StyleStatusID", RequestData.DesignMasterData.StyleStatusID);
                _CommandObj.Parameters.AddWithValue("@ProductLineID", RequestData.DesignMasterData.ProductLineID);
                _CommandObj.Parameters.AddWithValue("@ProductGroupID", RequestData.DesignMasterData.ProductGroupID);
                _CommandObj.Parameters.AddWithValue("@SeasonID", RequestData.DesignMasterData.SeasonID);
                _CommandObj.Parameters.AddWithValue("@DropID", RequestData.DesignMasterData.DropID);
                _CommandObj.Parameters.AddWithValue("@Grade", RequestData.DesignMasterData.Grade);
                _CommandObj.Parameters.AddWithValue("@CollectionID", RequestData.DesignMasterData.CollectionID);
                _CommandObj.Parameters.AddWithValue("@SubCollectionID", RequestData.DesignMasterData.SubCollectionID);
                _CommandObj.Parameters.AddWithValue("@Composition", RequestData.DesignMasterData.Composition);
                _CommandObj.Parameters.AddWithValue("@SimbolGroup", RequestData.DesignMasterData.SimbolGroup);
                _CommandObj.Parameters.AddWithValue("@BrandID", RequestData.DesignMasterData.BrandID);
                _CommandObj.Parameters.AddWithValue("@DesignerID", RequestData.DesignMasterData.DesignerID);
                _CommandObj.Parameters.AddWithValue("@DivisionID", RequestData.DesignMasterData.DivisionID);
                _CommandObj.Parameters.AddWithValue("@ProductDepartmentCode", RequestData.DesignMasterData.ProductDepartmentCode);
                _CommandObj.Parameters.AddWithValue("@YearID", RequestData.DesignMasterData.YearID);
                _CommandObj.Parameters.AddWithValue("@Active", RequestData.DesignMasterData.Active);

                _CommandObj.Parameters.AddWithValue("@UpdateBy", RequestData.DesignMasterData.UpdateBy);
                _CommandObj.Parameters.AddWithValue("@SCN", RequestData.DesignMasterData.SCN);          
               
                _CommandObj.Parameters.AddWithValue("@DevelopmentOffice", RequestData.DesignMasterData.DevelopmentOffice);
                _CommandObj.Parameters.AddWithValue("@ShortDescription", RequestData.DesignMasterData.ShortDescription);
                _CommandObj.Parameters.AddWithValue("@Remarks", RequestData.DesignMasterData.Remarks);

                // Changed by Senthamil @ 07.09.2018
                var SegamentationCode = _CommandObj.Parameters.Add("@SegamentationCode", SqlDbType.NVarChar);
                SegamentationCode.Direction = ParameterDirection.Input;
                SegamentationCode.Value = RequestData.DesignMasterData.SegamentationCode;

                var StyleStatusCode = _CommandObj.Parameters.Add("@StyleStatusCode", SqlDbType.NVarChar);
                StyleStatusCode.Direction = ParameterDirection.Input;
                StyleStatusCode.Value = RequestData.DesignMasterData.StyleStatusCode;

                var ProductLineCode = _CommandObj.Parameters.Add("@ProductLineCode", SqlDbType.NVarChar);
                ProductLineCode.Direction = ParameterDirection.Input;
                ProductLineCode.Value = RequestData.DesignMasterData.ProductLineCode;

                var ProductGroupCode = _CommandObj.Parameters.Add("@ProductGroupCode", SqlDbType.NVarChar);
                ProductGroupCode.Direction = ParameterDirection.Input;
                ProductGroupCode.Value = RequestData.DesignMasterData.ProductGroupCode;

                var SeasonCode = _CommandObj.Parameters.Add("@SeasonCode", SqlDbType.NVarChar);
                SeasonCode.Direction = ParameterDirection.Input;
                SeasonCode.Value = RequestData.DesignMasterData.SeasonCode;

                var DropCode = _CommandObj.Parameters.Add("@DropCode", SqlDbType.NVarChar);
                DropCode.Direction = ParameterDirection.Input;
                DropCode.Value = RequestData.DesignMasterData.DropCode;

                var CollectionCode = _CommandObj.Parameters.Add("@CollectionCode", SqlDbType.NVarChar);
                CollectionCode.Direction = ParameterDirection.Input;
                CollectionCode.Value = RequestData.DesignMasterData.CollectionCode;

                var SubCollectionCode = _CommandObj.Parameters.Add("@SubCollectionCode", SqlDbType.NVarChar);
                SubCollectionCode.Direction = ParameterDirection.Input;
                SubCollectionCode.Value = RequestData.DesignMasterData.SubCollectionCode;

                var BrandCode = _CommandObj.Parameters.Add("@BrandCode", SqlDbType.NVarChar);
                BrandCode.Direction = ParameterDirection.Input;
                BrandCode.Value = RequestData.DesignMasterData.BrandCode;

                var DesignerCode = _CommandObj.Parameters.Add("@DesignerCode", SqlDbType.NVarChar);
                DesignerCode.Direction = ParameterDirection.Input;
                DesignerCode.Value = RequestData.DesignMasterData.DesignerCode;

                var DivisionCode = _CommandObj.Parameters.Add("@DivisionCode", SqlDbType.NVarChar);
                DivisionCode.Direction = ParameterDirection.Input;
                DivisionCode.Value = RequestData.DesignMasterData.DivisionCode;

                var YearCode = _CommandObj.Parameters.Add("@YearCode", SqlDbType.NVarChar);
                YearCode.Direction = ParameterDirection.Input;
                YearCode.Value = RequestData.DesignMasterData.YearCode; 

                //var SegamentationCode = _CommandObj.Parameters.Add("@SegamentationCode", SqlDbType.NVarChar);
                //SegamentationCode.Direction = ParameterDirection.Input;
                //SegamentationCode.Value = RequestData.DesignMasterData.SegamentationCode;

                //var StyleStatusCode = _CommandObj.Parameters.Add("@StyleStatusCode", SqlDbType.NVarChar);
                //StyleStatusCode.Direction = ParameterDirection.Input;
                //StyleStatusCode.Value = RequestData.DesignMasterData.StyleStatusCode;

                //var ProductLineCode = _CommandObj.Parameters.Add("@ProductLineCode", SqlDbType.NVarChar);
                //ProductLineCode.Direction = ParameterDirection.Input;
                //ProductLineCode.Value = RequestData.DesignMasterData.ProductLineCode;

                //var ProductGroupCode = _CommandObj.Parameters.Add("@ProductGroupCode", SqlDbType.NVarChar);
                //ProductGroupCode.Direction = ParameterDirection.Input;
                //ProductGroupCode.Value = RequestData.DesignMasterData.ProductGroupCode;

                //var SeasonCode = _CommandObj.Parameters.Add("@SeasonCode", SqlDbType.NVarChar);
                //SeasonCode.Direction = ParameterDirection.Input;
                //SeasonCode.Value = RequestData.DesignMasterData.SeasonCode;

                //var DropCode = _CommandObj.Parameters.Add("@DropCode", SqlDbType.NVarChar);
                //DropCode.Direction = ParameterDirection.Input;
                //DropCode.Value = RequestData.DesignMasterData.DropCode;

                //var CollectionCode = _CommandObj.Parameters.Add("@CollectionCode", SqlDbType.NVarChar);
                //CollectionCode.Direction = ParameterDirection.Input;
                //CollectionCode.Value = RequestData.DesignMasterData.CollectionCode;

                //var SubCollectionCode = _CommandObj.Parameters.Add("@SubCollectionCode", SqlDbType.NVarChar);
                //SubCollectionCode.Direction = ParameterDirection.Input;
                //SubCollectionCode.Value = RequestData.DesignMasterData.SubCollectionCode;

                //var BrandCode = _CommandObj.Parameters.Add("@BrandCode", SqlDbType.NVarChar);
                //BrandCode.Direction = ParameterDirection.Input;
                //BrandCode.Value = RequestData.DesignMasterData.BrandCode;

                //var DesignerCode = _CommandObj.Parameters.Add("@DesignerCode", SqlDbType.NVarChar);
                //DesignerCode.Direction = ParameterDirection.Input;
                //DesignerCode.Value = RequestData.DesignMasterData.DesignerCode;

                //var DivisionCode = _CommandObj.Parameters.Add("@DivisionCode", SqlDbType.NVarChar);
                //DivisionCode.Direction = ParameterDirection.Input;
                //DivisionCode.Value = RequestData.DesignMasterData.DivisionCode;

                //var YearCode = _CommandObj.Parameters.Add("@YearCode", SqlDbType.NVarChar);
                //YearCode.Direction = ParameterDirection.Input;
                //YearCode.Value = RequestData.DesignMasterData.YearCode; 

                var DesignID = _CommandObj.Parameters.Add("@DesignID", SqlDbType.Int);
                DesignID.Direction = ParameterDirection.Output;

                var StatusCode = _CommandObj.Parameters.Add("@OpStatusCode", SqlDbType.Int);
                StatusCode.Direction = ParameterDirection.Output;

                var StatusMsg = _CommandObj.Parameters.Add("@OpStatusMessage", SqlDbType.NVarChar, 500);
                StatusMsg.Direction = ParameterDirection.Output;

                _CommandObj.ExecuteNonQuery();

                string strStatusCode = StatusCode.Value.ToString();
                if (strStatusCode == "1")
                {
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulUpdateMessage.Replace("{}", "Design");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    DesignImageInsert(RequestData.DesignWithItemImageList, (int)DesignID.Value);
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Design");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design");
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
            var DesignationMasterRecord = new DesignMasterTypes();

            var RequestData = (DeleteDesignMasterRequest)RequestObj;
            var ResponseData = new DeleteDesignMasterResponse();
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                string sSql = "delete from SKUImages where DesignID='{0}';delete from DesignMaster where  ID='{0}'";
               
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                _CommandObj.ExecuteNonQuery();
                ResponseData.StatusCode = Enums.OpStatusCode.Success;
                ResponseData.DisplayMessage = CommonStrings.SuccessfulDeleteMessage.Replace("{}", "Design");
            }
            catch
            {
                ResponseData.StatusCode = Enums.OpStatusCode.DeleteRecordFailed;
                ResponseData.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "Design");
            }
            finally
            {
                sqlCommon.CloseConnection(_ConnectionObj);
            }
            return ResponseData;
        }

        public override EasyBizResponse.BaseResponseType SelectRecord(EasyBizRequest.BaseRequestType RequestObj)
        {
            var DesignMasterTypesRecord = new DesignMasterTypes();
            var RequestData = (SelectByIDDesignMasterRequest)RequestObj;
            var ResponseData = new SelectByIDDesignMasterResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                string sSql = "Select * from DesignMaster with(NoLock) where  ID='{0}'";
                sSql = string.Format(sSql, RequestData.ID);
                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDesignMasterTypes = new DesignMasterTypes();

                        objDesignMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMasterTypes.DesignCode = objReader["DesignCode"].ToString();
                        objDesignMasterTypes.DesignName = objReader["DesignName"].ToString();
                        objDesignMasterTypes.Description = objReader["Description"].ToString();
                        objDesignMasterTypes.ForeignDescription = objReader["ForeignDescription"].ToString();
                        objDesignMasterTypes.SegamentationID =objReader["SegamentationID"] != DBNull.Value ? Convert.ToInt32(objReader["SegamentationID"]) :0;
                        objDesignMasterTypes.StyleStatusID =objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"]) :0;
                        objDesignMasterTypes.ProductLineID =objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) :0;
                        objDesignMasterTypes.ProductGroupID =objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) :0;
                        objDesignMasterTypes.SeasonID =objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) :0;
                        objDesignMasterTypes.DropID =objReader["DropID"] != DBNull.Value ? Convert.ToInt32(objReader["DropID"]) :0;
                        objDesignMasterTypes.Grade = objReader["Grade"].ToString();
                        objDesignMasterTypes.CollectionID =objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) :0;
                        objDesignMasterTypes.SubCollectionID =objReader["SubCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["SubCollectionID"]) :0;
                        objDesignMasterTypes.Composition = objReader["Composition"].ToString();
                        objDesignMasterTypes.SimbolGroup = objReader["SimbolGroup"].ToString();
                        objDesignMasterTypes.BrandID =objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) :0;
                        objDesignMasterTypes.DesignerID =objReader["DesignerID"] != DBNull.Value ? Convert.ToInt32(objReader["DesignerID"]) :0;
                        objDesignMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) : 0;
                        objDesignMasterTypes.ProductDepartmentCode = objReader["ProductDepartmentCode"].ToString();
                        objDesignMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;
                        objDesignMasterTypes.DevelopmentOffice = objReader["DevelopmentOffice"].ToString();
                        objDesignMasterTypes.ShortDescription = objReader["ShortDescription"].ToString();
                        objDesignMasterTypes.Remarks =objReader["Remarks"].ToString();
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"].ToString()) : true;

                        objDesignMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDesignMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDesignMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDesignMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objDesignMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        // Changed by Senthamil @ 07.09.2018
                        objDesignMasterTypes.SegamentationCode = objReader["SegamentationCode"] != DBNull.Value ? Convert.ToString(objReader["SegamentationCode"]) : "";
                        objDesignMasterTypes.StyleStatusCode = objReader["StyleStatusCode"] != DBNull.Value ? Convert.ToString(objReader["StyleStatusCode"]) : "";
                        objDesignMasterTypes.ProductLineCode = objReader["ProductLineCode"] != DBNull.Value ? Convert.ToString(objReader["ProductLineCode"]) : "";
                        objDesignMasterTypes.ProductGroupCode = objReader["ProductGroupCode"] != DBNull.Value ? Convert.ToString(objReader["ProductGroupCode"]) : "";
                        objDesignMasterTypes.SeasonCode = objReader["SeasonCode"] != DBNull.Value ? Convert.ToString(objReader["SeasonCode"]) : "";
                        objDesignMasterTypes.DropCode = objReader["DropCode"] != DBNull.Value ? Convert.ToString(objReader["DropCode"]) : "";
                        objDesignMasterTypes.CollectionCode = objReader["CollectionCode"] != DBNull.Value ? Convert.ToString(objReader["CollectionCode"]) : "";
                        objDesignMasterTypes.SubCollectionCode = objReader["SubCollectionCode"] != DBNull.Value ? Convert.ToString(objReader["SubCollectionCode"]) : "";
                        objDesignMasterTypes.BrandCode = objReader["BrandCode"] != DBNull.Value ? Convert.ToString(objReader["BrandCode"]) : "";
                        objDesignMasterTypes.DesignerCode = objReader["DesignerCode"] != DBNull.Value ? Convert.ToString(objReader["DesignerCode"]) : "";
                        objDesignMasterTypes.DivisionCode = objReader["DivisionCode"] != DBNull.Value ? Convert.ToString(objReader["DivisionCode"]) : "";
                        objDesignMasterTypes.YearCode = objReader["YearCode"] != DBNull.Value ? Convert.ToString(objReader["YearCode"]) : "";

                        ResponseData.DesignMasterTypesData = objDesignMasterTypes;
                        ResponseData.ResponseDynamicData = objDesignMasterTypes;
                        
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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
            var DesignMasterTypesList = new List<DesignMasterTypes>();

            var RequestData = new SelectAllDesignMasterRequest();
            var ResponseData = new SelectAllDesignMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);
                RequestData.ShowInActiveRecords = true;
                var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                sSql.Append("Select DM.*,AFS.AFSegamationName,SSM.StatusName,PLM.ProductLineName,PGM.ProductGroupName,SM.SeasonName,DM1.DropName,CM.CollectionName,SCM.SubCollectionName,EM.EmployeeName,DM2.DivisionName,BM.BrandName,YM.Year from DesignMaster DM  with(NoLock)  ");
                sSql.Append("left OUTER JOIN  AFSegamationMaster AFS ON  DM.SegamentationID=AFS.ID ");
                sSql.Append("left OUTER JOIN  StyleStatusMaster SSM ON  DM.StyleStatusID=SSM.ID ");
                sSql.Append("left OUTER JOIN  ProductLineMaster PLM ON  DM.ProductLineID=PLM.ID ");
                sSql.Append("left OUTER JOIN  ProductGroupMaster PGM ON  DM.ProductGroupID=PGM.ID ");
                sSql.Append("left OUTER JOIN  SeasonMaster SM ON  DM.SeasonID=SM.ID ");
                sSql.Append("left OUTER JOIN  DropMaster DM1 ON  DM.DropID=DM1.ID ");
                sSql.Append("left OUTER JOIN  CollectionMaster CM ON  DM.CollectionID=CM.ID ");
                sSql.Append("left OUTER JOIN  SubCollectionMaster SCM ON  DM.SubCollectionID=SCM.ID ");
                sSql.Append("left OUTER JOIN  BrandMaster BM ON  DM.BrandID=BM.ID ");
                sSql.Append("left OUTER JOIN  EmployeeMaster EM ON  DM.DesignerID=EM.ID ");
                sSql.Append("left OUTER JOIN  DivisionMaster DM2 ON  DM.DivisionID=DM2.ID ");
                sSql.Append("left OUTER JOIN  YearMaster YM ON  DM.YearID=YM.ID order by id  asc");
                
                

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objDesignMasterTypes = new DesignMasterTypes();

                        objDesignMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMasterTypes.DesignCode = objReader["DesignCode"].ToString();
                        objDesignMasterTypes.DesignName = objReader["DesignName"].ToString();
                        objDesignMasterTypes.Description = objReader["Description"].ToString();
                        objDesignMasterTypes.ForeignDescription = objReader["ForeignDescription"].ToString();
                        objDesignMasterTypes.SegamentationID =objReader["SegamentationID"] != DBNull.Value ? Convert.ToInt32(objReader["SegamentationID"]) :0;
                        objDesignMasterTypes.StyleStatusID =objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"]) :0;
                        objDesignMasterTypes.ProductLineID =objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) :0;
                        objDesignMasterTypes.ProductGroupID =objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) :0;
                        objDesignMasterTypes.SeasonID =objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) :0;
                        objDesignMasterTypes.DropID =objReader["DropID"] != DBNull.Value ? Convert.ToInt32(objReader["DropID"]) :0;
                        objDesignMasterTypes.CollectionID =objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) :0;
                        objDesignMasterTypes.SubCollectionID =objReader["SubCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["SubCollectionID"]) :0;
                        objDesignMasterTypes.Composition = objReader["Composition"].ToString();
                        objDesignMasterTypes.SimbolGroup = objReader["SimbolGroup"].ToString();
                        objDesignMasterTypes.BrandID =objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) :0;
                        objDesignMasterTypes.DesignerID =objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) :0;
                        objDesignMasterTypes.DivisionID =objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) :0;
                        objDesignMasterTypes.ProductDepartmentCode = objReader["ProductDepartmentCode"].ToString();
                        objDesignMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objDesignMasterTypes.StatusName = objReader["StatusName"].ToString();
                        objDesignMasterTypes.ProductLineName = objReader["ProductLineName"].ToString();
                        objDesignMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();
                        objDesignMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        objDesignMasterTypes.DropName = objReader["DropName"].ToString();
                        objDesignMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        objDesignMasterTypes.SubCollectionName = objReader["SubCollectionName"].ToString();
                        objDesignMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objDesignMasterTypes.EmployeeName = objReader["EmployeeName"].ToString();
                        objDesignMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objDesignMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objDesignMasterTypes.YearName = objReader["Year"].ToString();
                        objDesignMasterTypes.YearID =objReader["YearID"] != DBNull.Value ?  Convert.ToInt32(objReader["YearID"].ToString()) :0;
                        objDesignMasterTypes.Remarks = objReader["Remarks"].ToString();
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"].ToString()) : true;


                        objDesignMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDesignMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDesignMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDesignMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objDesignMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        DesignMasterTypesList.Add(objDesignMasterTypes);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignMasterTypesList = DesignMasterTypesList;
                    ResponseData.ResponseDynamicData = DesignMasterTypesList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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
            var DesignMasterTypesList = new List<DesignMasterTypes>();

            var RequestData = (SelectByIDsDesignMasterRequest)RequestObj;
            var ResponseData = new SelectByIDsDesignMasterResponse();

            var DesignMasterTypesdata = new DesignMasterTypes();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);

                string sSql = "Select * from DesignMaster with(NoLock) where ID in '{0}'";
                sSql = string.Format(sSql, RequestData.IDs);

                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);

                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objDesignMasterTypes = new DesignMasterTypes();

                        objDesignMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMasterTypes.DesignCode = objReader["DesignCode"].ToString();
                        objDesignMasterTypes.DesignName = objReader["DesignName"].ToString();
                        objDesignMasterTypes.Remarks = objReader["Remarks"].ToString();

                        objDesignMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDesignMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDesignMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDesignMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0; ;
                        objDesignMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        DesignMasterTypesList.Add(objDesignMasterTypes);

                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignMasterTypesList = DesignMasterTypesList;


                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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

        public override SelectDesignMasterLookUpResponse DesignResponseLookUp(SelectDesignMasterLookUpRequest ObjRequest)
        {
            var DesignMasterList = new List<DesignMasterTypes>();
            var RequestData = (SelectDesignMasterLookUpRequest)ObjRequest;
            var ResponseData = new SelectDesignMasterLookUpResponse();
            SqlDataReader objReader;
            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                string sQuery = string.Empty;
                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString,ref _RequestFrom);

                sQuery = "Select ID,DesignCode,ProductDepartmentCode from DesignMaster with(NoLock) where Active='True'";
                _CommandObj = new SqlCommand(sQuery, _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        var objDesignMaster= new DesignMasterTypes();
                        objDesignMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMaster.DesignCode = Convert.ToString(objReader["DesignCode"]);
                        objDesignMaster.ProductDepartmentCode = Convert.ToString(objReader["ProductDepartmentCode"]);
                        DesignMasterList.Add(objDesignMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignMasterTypesList = DesignMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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
        public override SelectDesignGradeLookUpResponse SelectDesignGradeLookUp(SelectDesignGradeLookUpRequest ObjRequest)
        {
            var DesignMasterList = new List<DesignGradeTypes>();
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
                        var objDesignMaster = new DesignGradeTypes();
                        objDesignMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMaster.Grade = Convert.ToString(objReader["Grade"]);                       
                        DesignMasterList.Add(objDesignMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignGradeList = DesignMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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
            var DesignMasterList = new List<DesignDevelopmentOfficeTypes>();
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
                        var objDesignMaster = new DesignDevelopmentOfficeTypes();
                        objDesignMaster.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMaster.DevelopmentOffice = Convert.ToString(objReader["DevelopmentOffice"]);
                        DesignMasterList.Add(objDesignMaster);
                    }
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignDevelopmentOfficeList = DesignMasterList;
                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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
       
        public override SaveDesignMasterResponse ImportExcelInsert(SaveDesignMasterRequest ObjRequest)
        {
            var RequestData = (SaveDesignMasterRequest)ObjRequest;
            var ResponseData = new SaveDesignMasterResponse();

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                _CommandObj = new SqlCommand("InsertImportDesginMaster", _ConnectionObj);
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
                    ResponseData.DisplayMessage = CommonStrings.SuccessfulSaveMessage.Replace("{}", "Design");
                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    //ResponseData
                }
                else if (strStatusCode == "2")
                {
                    ResponseData.DisplayMessage = CommonStrings.AlreadyExists.Replace("{}", "Design");
                }
                else
                {
                    ResponseData.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "Design");
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
        public string ImportDetailXML(List<DesignMasterTypes> ImportDetailList)
        {
            StringBuilder sSql = new StringBuilder();
            foreach (DesignMasterTypes objImportExcelDetail in ImportDetailList)
            {
                sSql.Append("<DesignMaster>");
                sSql.Append("<ID>0</ID>");
                sSql.Append("<DesignCode>" + objImportExcelDetail.DesignCode + "</DesignCode>");
                sSql.Append("<DesignName>" + objImportExcelDetail.DesignName + "</DesignName>");
                sSql.Append("<Description>" + (objImportExcelDetail.Description) + "</Description>");
                sSql.Append("<ForeignDescription>" + (objImportExcelDetail.ForeignDescription) + "</ForeignDescription>");
                sSql.Append("<SegamentationID>" + objImportExcelDetail.SegamentationID + "</SegamentationID>");
                sSql.Append("<StyleStatusID>" + objImportExcelDetail.StyleStatusID + "</StyleStatusID>");
                sSql.Append("<ProductLineID>" + objImportExcelDetail.ProductLineID + "</ProductLineID>");
                sSql.Append("<ProductGroupID>" + objImportExcelDetail.ProductGroupID + "</ProductGroupID>");
                sSql.Append("<SeasonID>" + objImportExcelDetail.SeasonID + "</SeasonID>");
                sSql.Append("<DropID>" + objImportExcelDetail.DropID + "</DropID>");
                sSql.Append("<CollectionID>" + objImportExcelDetail.CollectionID + "</CollectionID>");
                sSql.Append("<SubCollectionID>" + objImportExcelDetail.SubCollectionID + "</SubCollectionID>");
                sSql.Append("<Composition>" + objImportExcelDetail.Composition + "</Composition>");
                sSql.Append("<SimbolGroup>" + objImportExcelDetail.SimbolGroup + "</SimbolGroup>");
                sSql.Append("<BrandID>" + objImportExcelDetail.BrandID + "</BrandID>");
                sSql.Append("<DesignerID>" + objImportExcelDetail.DesignerID + "</DesignerID>");
                sSql.Append("<DivisionID>" + objImportExcelDetail.DivisionID + "</DivisionID>");
                sSql.Append("<Grade>" + objImportExcelDetail.Grade + "</Grade>");
                sSql.Append("<ProductDepartmentCode>" + objImportExcelDetail.ProductDepartmentCode + "</ProductDepartmentCode>");
                sSql.Append("<DevelopmentOffice>" + objImportExcelDetail.DevelopmentOffice + "</DevelopmentOffice>");
                sSql.Append("<ShortDescription>" + objImportExcelDetail.ShortDescription + "</ShortDescription>");
                sSql.Append("<YearID>" + objImportExcelDetail.YearID + "</YearID>");
                sSql.Append("<Active>" + objImportExcelDetail.Active + "</Active>");
                sSql.Append("<IsStoreSync>" + objImportExcelDetail.IsStoreSync + "</IsStoreSync>");
                sSql.Append("<IsCountrySync>" + objImportExcelDetail.IsCountrySync + "</IsCountrySync>");
                sSql.Append("<Remarks>" + objImportExcelDetail.Remarks + "</Remarks>");
                sSql.Append("</DesignMaster>");
            }
            return sSql.ToString().Replace("&", "&#38;"); 
        }

        public override SelectAllDesignMasterResponse API_SelectALL(SelectAllDesignMasterRequest requestData)
        {
            var DesignMasterTypesList = new List<DesignMasterTypes>();

            var RequestData = (SelectAllDesignMasterRequest)requestData;
            var ResponseData = new SelectAllDesignMasterResponse();

            SqlDataReader objReader;

            var sqlCommon = new MsSqlCommon();
            try
            {
                _ConnectionString = RequestData.ConnectionString;
                _RequestFrom = RequestData.RequestFrom;

                sqlCommon.InitializeDataComponents(ref _ConnectionObj, ref _CommandObj, ref _ConnectionString, ref _RequestFrom);
                RequestData.ShowInActiveRecords = true;
                var sSql = new StringBuilder();
                //sSql.Append("Select  ROW_NUMBER() OVER(ORDER BY CustomerName asc) AS RowNumber, ");
                sSql.Append("Select DM.ID,DM.DesignCode,DM.DesignName,DM.Active,SM.SeasonName,CM.CollectionName,BM.BrandName,YM.Year, RC.TOTAL_CNT [RecordCount] from DesignMaster DM  with(NoLock)  ");                                
                sSql.Append("left OUTER JOIN  SeasonMaster SM with(NoLock) ON  DM.SeasonID=SM.ID ");                
                sSql.Append("left OUTER JOIN  CollectionMaster CM with(NoLock) ON  DM.CollectionID=CM.ID ");                
                sSql.Append("left OUTER JOIN  BrandMaster BM with(NoLock) ON  DM.BrandID=BM.ID ");                
                sSql.Append("left OUTER JOIN  YearMaster YM with(NoLock) ON  DM.YearID=YM.ID ");

                sSql.Append("LEFT JOIN(Select  count(DM1.ID) As TOTAL_CNT From DesignMaster DM1 with(NoLock) ");
                sSql.Append("left OUTER JOIN  SeasonMaster SM1 with(NoLock) ON  DM1.SeasonID=SM1.ID ");
                sSql.Append("left OUTER JOIN  CollectionMaster CM1 with(NoLock) ON  DM1.CollectionID=CM1.ID ");
                sSql.Append("left OUTER JOIN  BrandMaster BM1 with(NoLock) ON  DM1.BrandID=BM1.ID ");
                sSql.Append("left OUTER JOIN  YearMaster YM1 with(NoLock) ON  DM1.YearID=YM1.ID ");
                sSql.Append("where DM1.Active = " + RequestData.IsActive + " ");
                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or DM1.DesignCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or DM1.DesignName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM1.SeasonName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CM1.CollectionName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or BM1.BrandName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or YM1.Year like isnull('%" + RequestData.SearchString + "%',''))) As RC ON 1 = 1  ");

                sSql.Append("where DM.Active = " + RequestData.IsActive + " ");
                sSql.Append("and (isnull('" + RequestData.SearchString + "','') = '' ");
                sSql.Append("or DM.DesignCode like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or DM.DesignName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or SM.SeasonName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or CM.CollectionName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or BM.BrandName like isnull('%" + RequestData.SearchString + "%','') ");
                sSql.Append("or YM.Year like isnull('%" + RequestData.SearchString + "%','')) ");
                sSql.Append("order by DM.ID asc ");
                sSql.Append("offset " + RequestData.Offset + " rows ");
                sSql.Append("fetch first " + RequestData.Limit + " rows only");


                _CommandObj = new SqlCommand(sSql.ToString(), _ConnectionObj);
                _CommandObj.CommandType = CommandType.Text;
                objReader = _CommandObj.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {

                        var objDesignMasterTypes = new DesignMasterTypes();

                        objDesignMasterTypes.ID = objReader["ID"] != DBNull.Value ? Convert.ToInt32(objReader["ID"]) : 0;
                        objDesignMasterTypes.DesignCode = objReader["DesignCode"].ToString();
                        objDesignMasterTypes.DesignName = objReader["DesignName"].ToString();
                        /*objDesignMasterTypes.Description = objReader["Description"].ToString();
                        objDesignMasterTypes.ForeignDescription = objReader["ForeignDescription"].ToString();
                        objDesignMasterTypes.SegamentationID = objReader["SegamentationID"] != DBNull.Value ? Convert.ToInt32(objReader["SegamentationID"]) : 0;
                        objDesignMasterTypes.StyleStatusID = objReader["StyleStatusID"] != DBNull.Value ? Convert.ToInt32(objReader["StyleStatusID"]) : 0;
                        objDesignMasterTypes.ProductLineID = objReader["ProductLineID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductLineID"]) : 0;
                        objDesignMasterTypes.ProductGroupID = objReader["ProductGroupID"] != DBNull.Value ? Convert.ToInt32(objReader["ProductGroupID"]) : 0;
                        objDesignMasterTypes.SeasonID = objReader["SeasonID"] != DBNull.Value ? Convert.ToInt32(objReader["SeasonID"]) : 0;
                        objDesignMasterTypes.DropID = objReader["DropID"] != DBNull.Value ? Convert.ToInt32(objReader["DropID"]) : 0;
                        objDesignMasterTypes.CollectionID = objReader["CollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["CollectionID"]) : 0;
                        objDesignMasterTypes.SubCollectionID = objReader["SubCollectionID"] != DBNull.Value ? Convert.ToInt32(objReader["SubCollectionID"]) : 0;
                        objDesignMasterTypes.Composition = objReader["Composition"].ToString();
                        objDesignMasterTypes.SimbolGroup = objReader["SimbolGroup"].ToString();
                        objDesignMasterTypes.BrandID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objDesignMasterTypes.DesignerID = objReader["BrandID"] != DBNull.Value ? Convert.ToInt32(objReader["BrandID"]) : 0;
                        objDesignMasterTypes.DivisionID = objReader["DivisionID"] != DBNull.Value ? Convert.ToInt32(objReader["DivisionID"]) : 0;
                        objDesignMasterTypes.ProductDepartmentCode = objReader["ProductDepartmentCode"].ToString();
                        objDesignMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        objDesignMasterTypes.StatusName = objReader["StatusName"].ToString();
                        objDesignMasterTypes.ProductLineName = objReader["ProductLineName"].ToString();
                        objDesignMasterTypes.ProductGroupName = objReader["ProductGroupName"].ToString();*/
                        objDesignMasterTypes.SeasonName = objReader["SeasonName"].ToString();
                        //objDesignMasterTypes.DropName = objReader["DropName"].ToString();
                        objDesignMasterTypes.CollectionName = objReader["CollectionName"].ToString();
                        //objDesignMasterTypes.SubCollectionName = objReader["SubCollectionName"].ToString();
                        //objDesignMasterTypes.AFSegamationName = objReader["AFSegamationName"].ToString();
                        //objDesignMasterTypes.EmployeeName = objReader["EmployeeName"].ToString();
                        //objDesignMasterTypes.DivisionName = objReader["DivisionName"].ToString();
                        objDesignMasterTypes.BrandName = objReader["BrandName"].ToString();
                        objDesignMasterTypes.YearName = objReader["Year"].ToString();
                        //objDesignMasterTypes.YearID = objReader["YearID"] != DBNull.Value ? Convert.ToInt32(objReader["YearID"].ToString()) : 0;
                        //objDesignMasterTypes.Remarks = objReader["Remarks"].ToString();
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"].ToString()) : true;


                        /*objDesignMasterTypes.CreateOn = objReader["CreateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["CreateOn"]) : DateTime.Now;
                        objDesignMasterTypes.CreateBy = objReader["CreateBy"] != DBNull.Value ? Convert.ToInt32(objReader["CreateBy"]) : 0;
                        objDesignMasterTypes.UpdateOn = objReader["UpdateOn"] != DBNull.Value ? Convert.ToDateTime(objReader["UpdateOn"]) : DateTime.Now;
                        objDesignMasterTypes.UpdateBy = objReader["UpdateBy"] != DBNull.Value ? Convert.ToInt32(objReader["UpdateBy"]) : 0;
                        objDesignMasterTypes.SCN = objReader["SCN"] != DBNull.Value ? Convert.ToInt32(objReader["SCN"]) : 0;*/
                        objDesignMasterTypes.Active = objReader["Active"] != DBNull.Value ? Convert.ToBoolean(objReader["Active"]) : true;

                        DesignMasterTypesList.Add(objDesignMasterTypes);
                        ResponseData.RecordCount = objReader["RecordCount"] != DBNull.Value ? Convert.ToInt32(objReader["RecordCount"]) : 0;
                    }

                    ResponseData.StatusCode = Enums.OpStatusCode.Success;
                    ResponseData.DesignMasterTypesList = DesignMasterTypesList;
                    ResponseData.ResponseDynamicData = DesignMasterTypesList;

                }
                else
                {
                    ResponseData.StatusCode = Enums.OpStatusCode.RecordNotFound;
                    ResponseData.DisplayMessage = CommonStrings.NoRecordFound.Replace("{}", "Design");
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
