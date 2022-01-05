using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.LabelPrintingRequest;
using EasyBizResponse.Masters.LabelPrintingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
   public abstract class BaseLabelPrintingDAL : BaseDAL
    {
       public abstract CommonLabelPrintingReportResponse GetLabelPrintingReport(CommonLabelReportRequest ObjRequest);
    }
}
