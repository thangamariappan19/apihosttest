using BarcodeLib;
using CommonRoutines;
using EasyBizDBTypes.Common;
using EasyBizRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MsSqlDAL.Common
{
    public class MsSqlCommon
    {
        //public string ConString = ConfigurationManager.AppSettings["ConString"];  
        public string ConString = string.Empty;
        private void GetSettings()
        {
            try
            {
                string AppType = FSRetailResource.ResourceManager.GetString("ApplicationType");
                string FilePath = System.Windows.Forms.Application.StartupPath + "\\" + AppType + "SetUp.xml";

                if (File.Exists(FilePath))
                {
                    DataSet objDataTable = new DataSet();
                    objDataTable.ReadXml(FilePath);
                    if (objDataTable.Tables.Count > 0 && objDataTable.Tables[0].Rows.Count > 0)
                    {
                        var EnterpriseConnection = Convert.ToString(objDataTable.Tables[0].Rows[0]["MainServerConnection"]);
                        var LocalConnection = Convert.ToString(objDataTable.Tables[0].Rows[0]["LocalServerConnection"]);

                        ConString = EncrypterDecrypter.Decrypt(LocalConnection);
                    }
                }
                else
                {
                    ConString = ConfigurationManager.AppSettings["ConString"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InitializeDataComponents(ref SqlConnection ConnectionObj, ref SqlCommand CommandObj, ref string ConnectionString, ref Enums.RequestFrom RequestFrom)
        {
            ConnectionObj = new SqlConnection();
            CommandObj = new SqlCommand();
            CommandObj.CommandTimeout = 0;
            try
            {
                if (ConnectionString != null && ConnectionString != string.Empty)
                {
                    ConnectionObj.ConnectionString = EncrypterDecrypter.Decrypt(ConnectionString);
                }
                else
                {
                    GetSettings();
                    ConnectionObj.ConnectionString = ConString;
                }
                if (ConnectionObj.State == System.Data.ConnectionState.Closed)
                {
                    ConnectionObj.Open();
                }


                CommandObj.Connection = ConnectionObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void CloseConnection(SqlConnection ConnectionObj)
        {
            //if (ConnectionObj.State == System.Data.ConnectionState.Open)
            if(ConnectionObj != null)
            {
                ConnectionObj.Close();
            }
        }
        public string GetSQLServerDateString(DateTime DateValue)
        {
            return DateValue.ToString("yyyy-MM-dd");
        }

        public string GetSQLServerDateTimeString(DateTime DateValue)
        {
            return DateValue.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public DateTime SetSQLServerDateTimeString(DateTime DateValue)
        {
            return Convert.ToDateTime(DateValue.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        public string SearchByDate(string SearchString)
        {
            string sDate = string.Empty;
            try
            {
                DateTime temp = DateTime.ParseExact(SearchString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (temp != DateTime.MinValue && temp != DateTime.MaxValue)
                {                    
                    sDate = GetSQLServerDateString(temp);
                }
                else
                {
                    sDate = SearchString;
                }
            }
            catch
            {
                try
                {
                    DateTime temp = DateTime.ParseExact(SearchString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    if (temp != DateTime.MinValue && temp != DateTime.MaxValue)
                    {
                        sDate = GetSQLServerDateString(temp);
                    }
                    else
                    {
                        sDate = SearchString;
                    }
                }
                catch
                {
                    sDate = SearchString;
                }                
            }
            return sDate;
        }

        public string GetBarcode_Base64(string barcode_value)
        {
            string return_str = "";
            try
            {
                int imageWidth = 200;
                int imageHeight = 50;
                Color foreColor = Color.Black;
                Color backColor = Color.White;
                string NumericString = barcode_value;

                if (!string.IsNullOrWhiteSpace(NumericString))
                {
                    using (Barcode barcodeLib = new Barcode())
                    {
                        var bcode = barcodeLib;
                        bcode.Height = 100;
                        bcode.BarWidth = 1;
                        bcode.IncludeLabel = true;
                        bcode.LabelPosition = LabelPositions.BOTTOMCENTER;
                        bcode.RotateFlipType = RotateFlipType.RotateNoneFlipNone;

                        Image barcodeImage = bcode.Encode(TYPE.CODE128, NumericString, foreColor, backColor, imageWidth, imageHeight);
                        //barcodeImage.Save(@"E:\Barcode.png", ImageFormat.Png);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            barcodeImage.Save(ms, ImageFormat.Png);
                            return_str = Convert.ToBase64String(ms.ToArray());
                        }
                    };
                }
            }
            catch (Exception ex)
            {

            }
            return return_str;

        }
   
    //private void MyCallbackFunction(IAsyncResult result)
    //{
    //    try
    //    {
    //        //un-box the AsynState back to the SqlCommand  
    //        SqlCommand cmd = (SqlCommand)result.AsyncState;
    //        SqlDataReader reader = cmd.EndExecuteReader(result);
    //        while (reader.Read())
    //        {
    //            Dispatcher.BeginInvoke(new delegateAddTextToListbox(AddTextToListbox),
    //            reader.GetString(0));
    //        }

    //        if (cmd.Connection.State.Equals(ConnectionState.Open))
    //        {
    //            cmd.Connection.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //ToDo : Swallow exception log  
    //    }
    //}  
}

}
