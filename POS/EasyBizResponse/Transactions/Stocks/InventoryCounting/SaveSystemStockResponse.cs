using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizResponse.Transactions.Stocks.InventoryCounting
{
    [DataContract]
    [Serializable]
    public class SaveSystemStockResponse : BaseResponseType
    {

    }

    [DataContract]
    [Serializable]
    public class SaveManualStockResponse : BaseResponseType
    {

    }

    [DataContract]
    [Serializable]
    public class InventoryFinalizeResponse : BaseResponseType
    {

    }

    [DataContract]
    [Serializable]
    public class InventorySyncResponse : BaseResponseType
    {

    }
}
