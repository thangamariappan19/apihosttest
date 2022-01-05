using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.SegmentMasterRequest;
using EasyBizResponse.Masters.SegmentationMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseSegmentationMasterDAL : BaseDAL
    {
        public abstract SelectAllSegmentResponse API_SelectALL(SelectAllSegmentRequest requestData);
    }
}
