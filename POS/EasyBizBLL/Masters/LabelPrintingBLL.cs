using EasyBizFactory;
using EasyBizRequest.Masters.LabelPrintingRequest;
using EasyBizResponse.Masters.LabelPrintingResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizBLL.Masters
{
  public class LabelPrintingBLL 
    {
      public CommonLabelPrintingReportResponse GetLabelPrintingReportData(CommonLabelReportRequest ObjRequest)
        {
            CommonLabelPrintingReportResponse objResponse = null;
            var objFactory = new DALFactory();
            try
            {
                var objLabelPrintReportDAL = objFactory.GetDALRepository().GetLabelPrintingReportDAL();
                objResponse = (CommonLabelPrintingReportResponse)objLabelPrintReportDAL.GetLabelPrintingReport(ObjRequest);
            }
            catch (Exception ex)
            {
                objResponse = new CommonLabelPrintingReportResponse();
                objResponse.ExceptionMessage = ex.Message;
                objResponse.StackTrace = ex.StackTrace;
            }
            return objResponse;
        }
    }
}
