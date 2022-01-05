using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.Transactions.StockReturn;
using EasyBizDBTypes.Transactions.Stocks.StockReturn;
using EasyBizFactory;
using EasyBizRequest.Transactions.Stocks.StockReturn;
using EasyBizRequest.Transactions.Stocks.StockReceiptRequest;
using EasyBizResponse.Transactions.Stocks.StockReturn;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using EasyBizIView.Transactions.IStockReturn;
using Newtonsoft.Json;

namespace EasyBizBLL.Transactions.Stocks
{
    public class StockReturnBLL
    {

        IStockReturnView _IStockReturnView;
        string us = "";
        string ps = "";
        public SaveStockReturnResponse SaveStockReturn(SaveStockReturnRequest objRequest)
        {
            SaveStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                us = objRequest.Username;
                ps = objRequest.Password;
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    
                    var objStockReturn = new StockReturnHeader();
                    objStockReturn = (StockReturnHeader)objRequest.RequestDynamicData;
                    objRequest.StockReturnHeaderRecord = objStockReturn;
                    objRequest.StockReturnDetailsList = objStockReturn.StockReturnDetailsList;
                    objRequest.TransactionLogList = objStockReturn.TransactionLogList;
                }
                objResponse = (SaveStockReturnResponse)objBaseStockReturnDAL.InsertRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRETURN;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkNew;
                    StockReturnHeader SRH = new StockReturnHeader();
                    SRH = objRequest.StockReturnHeaderRecord;
                    SRH.TransactionLogList = objRequest.TransactionLogList;
                    //BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReturnBLL", "SaveStockReturn");
                    //Test(SRH);
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public async void Test(StockReturnHeader _Order)
        {
            try
            {
                HttpClient client = CreateHttpClient();
                //string token = await _ApiToken.GetToken(UserInfo.UserName, UserInfo.Password);
             
                string body = "grant_type=password&username=" +  us + "&password=" + Decrypt(ps);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:49273/auth");
                request.Content = new StringContent(body,
                                                    Encoding.UTF8,
                                                    "application/json");//CONTENT-TYPE header
                                                                        //"application/x-www-form-urlencoded");//CONTENT-TYPE header

                var response = await client.SendAsync(request);
                string msg = await response.Content.ReadAsStringAsync();
                var quoted = JsonConvert.DeserializeObject(msg);
                Newtonsoft.Json.Linq.JObject rss = Newtonsoft.Json.Linq.JObject.Parse(msg);

                string token = (string)rss["access_token"];
                var header = response.Headers;
                string responsed = Convert.ToString(response);
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string resourceUrl = "http://localhost:49273/api/StockReturn";
                //var r = await client.GetAsync(resourceUrl);

                string json = "";

                json = JsonConvert.SerializeObject(_Order);

                HttpWebRequest requestdata = (HttpWebRequest)WebRequest.Create(resourceUrl);
                UTF8Encoding encoding = new UTF8Encoding();
                requestdata.Method = "POST";
                requestdata.ContentType = "application/json";
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.DefaultRequestHeaders.Authorization

                requestdata.Headers.Add("Authorization", "Bearer " + token);
                byte[] data = encoding.GetBytes(json);
                if (data.Length > 0)
                {
                    requestdata.ContentLength = data.Length;
                    using (Stream stream = requestdata.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                Boolean status = false;
                HttpWebResponse responsedata = (HttpWebResponse)requestdata.GetResponse();
                StreamReader reader = new StreamReader(responsedata.GetResponseStream());
                string responseString = reader.ReadToEnd();

                if (responsedata.StatusCode == HttpStatusCode.OK || responsedata.StatusCode == HttpStatusCode.Created)
                {

                }
                //status = true;
                int i = 0;
            }
            catch (Exception ex)
            { }
        }

        private HttpClient CreateHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri("http://localhost:49273/auth");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            return client;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        public SaveStockReturnResponse Saveint_stock(SaveStockReturnRequest objRequest)
        {
            SaveStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.EnterpriseToStore;
                var objTransactionLogsDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                if (objRequest.RequestDynamicData != null)
                {
                    var Objint_stock = new int_stockreturn();
                    Objint_stock = (int_stockreturn)objRequest.RequestDynamicData;
                    objRequest.int_stockreturnList = Objint_stock.int_stockreturnList;
                }
                objResponse = (SaveStockReturnResponse)objTransactionLogsDAL.Saveint_stock(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRETURN;
                    objRequest.ProcessMode = Enums.ProcessMode.New;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReturnBLL", "Saveint_stock");
                }
            }
            catch (Exception ex)
            {
                objResponse = new SaveStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "int_stock");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public UpdateStockReturnResponse UpdateStockReturn(UpdateStockReturnRequest objRequest)
        {
            UpdateStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (UpdateStockReturnResponse)objBaseStockReturnDAL.UpdateRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public DeleteStockReturnResponse DeleteStockReturn(DeleteStockReturnRequest objRequest)
        {
            DeleteStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (DeleteStockReturnResponse)objBaseStockReturnDAL.DeleteRecord(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRETURN;
                    objRequest.ProcessMode = Enums.ProcessMode.Delete;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReturnBLL", "DeleteStockReturn");
                }
            }
            catch (Exception ex)
            {
                objResponse = new DeleteStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.DeleteErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockReturnResponse SelectAllStockReturn(SelectAllStockReturnRequest objRequest)
        {
            SelectAllStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var _UpdateStockReturnRequest = new UpdateStockReturnRequest();
                _UpdateStockReturnRequest.StoreID = objRequest.StoreID;
                _UpdateStockReturnRequest.StoreCode = objRequest.StoreCode;
                //CloseOpenDocuments(_UpdateStockReturnRequest);

                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (SelectAllStockReturnResponse)objBaseStockReturnDAL.SelectAll(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectAllStockReturnResponse API_SelectALL(SelectAllStockReturnRequest objRequest)
        {
            SelectAllStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var _UpdateStockReturnRequest = new UpdateStockReturnRequest();
                _UpdateStockReturnRequest.StoreID = objRequest.StoreID;
                _UpdateStockReturnRequest.StoreCode = objRequest.StoreCode;
                //CloseOpenDocuments(_UpdateStockReturnRequest);

                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (SelectAllStockReturnResponse)objBaseStockReturnDAL.API_SelectALL(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByStockReturnIDResponse SelectStockReturnRecord(SelectByStockReturnIDRequest objRequest)
        {
            SelectByStockReturnIDResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                if(objRequest.ID == 0)
                {
                    int doc_id;
                    int.TryParse(objRequest.DocumentIDs, out doc_id);
                    objRequest.ID = doc_id;
                }
                objResponse = (SelectByStockReturnIDResponse)objBaseStockReturnDAL.SelectRecord(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockReturnIDResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
        public SelectByStockReturnDetailsResponse SelectStockReturnDetails(SelectByStockReturnDetailsRequest objRequest)
        {
            SelectByStockReturnDetailsResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReturnDetailsDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (SelectByStockReturnDetailsResponse)objBaseStockReturnDetailsDAL.SelectByStockReturnDetails(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectByStockReturnDetailsResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStockReturnResponse GetStockReturnHeaderReport(SelectAllStockReturnRequest objRequest)
        {
            SelectAllStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (SelectAllStockReturnResponse)objBaseStockReturnDAL.GetStockReturnHeaderReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public SelectAllStockReturnResponse GetStockReturnDetailsReport(SelectAllStockReturnRequest objRequest)
        {
            SelectAllStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = (SelectAllStockReturnResponse)objBaseStockReturnDAL.GetStockReturnDetailsReport(objRequest);
            }
            catch (Exception ex)
            {
                objResponse = new SelectAllStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.UpdateErrorMessage.Replace("{}", "StockReceipt");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }

        public void CloseOpenDocuments(UpdateStockReturnRequest objRequest)
        {
            UpdateStockReturnResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                objRequest.SyncMode = Enums.SyncMode.StoreToEnterprise;
                var objBaseStockReturnDAL = objFactory.GetDALRepository().GetStockReturnDAL();
                objResponse = objBaseStockReturnDAL.CloseOpenDocuments(objRequest);
                if (objResponse.StatusCode == Enums.OpStatusCode.Success && objRequest.DataSync == false)
                {
                    objRequest.RequestFrom = objRequest.RequestFrom;
                    objRequest.DocumentIDs = Convert.ToString(objResponse.IDs);
                    objRequest.DocumentType = Enums.DocumentType.STOCKRETURN;
                    objRequest.ProcessMode = Enums.ProcessMode.BulkEdit;

                    BackgroundServices _BackgroundServices = new BackgroundServices(objRequest, objResponse, "EasyBizBLL.Transactions.Stocks.StockReturnBLL", "CloseOpenDocuments");
                }
            }
            catch (Exception ex)
            {
                objResponse = new UpdateStockReturnResponse();
                objResponse.DisplayMessage = CommonStrings.SaveErrorMessage.Replace("{}", "StockReturn");
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
        }
    }
}
