using EasyBizBLL.Common;
using EasyBizDBTypes.Common;
using EasyBizDBTypes.SyncSettings;
using EasyBizFactory;
using EasyBizRequest;
using EasyBizRequest.SyncSettings;
using EasyBizResponse;
using EasyBizResponse.SyncSettings;
using MsSqlDAL.SyncSettings;
using ResourceStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.SyncSettings
{
  public  class MasterDataSyncBLL
    {
      public MasterDataSyncResponse SyncStorePriceBLL(MasterDataSyncRequest objRequest)
      {
          MasterDataSyncResponse objResponse = null;
          var objFactory = new DALFactory();
          try
          {
              var objSyncMD = objFactory.GetDALRepository().GetSyncStorePrice();
              objResponse = (MasterDataSyncResponse)objSyncMD.SelectAll(objRequest);
          }
          catch (Exception ex)
          {
              objResponse = new MasterDataSyncResponse();
              objResponse.DisplayMessage = CommonStrings.RetrievalErrorMessage.Replace("{}", "Master data Sync");
              objResponse.ExceptionMessage = ex.Message;
              objResponse.StackTrace = ex.StackTrace;
          }
          return objResponse;
      }

    }
}
