using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.BinTransferMasterRequest;
using EasyBizResponse.Masters.BinTransferResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseBinTransferMasterDAL : BaseDAL
    {
        public abstract SaveBinTransferResponse UpdateRecord(SaveBinTransferRequest objRequest);
    }
}
