using EasyBizAbsDAL.Common;
using EasyBizRequest.Common;
using EasyBizRequest.Masters.ShiftRequest;
using EasyBizRequest.Transactions.POS.Invoice;
using EasyBizResponse.Masters.ShiftMasterResponse;
using EasyBizResponse.Masters.ShiftResponse;
using EasyBizResponse.Transactions.POS.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBizResponse.Common;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseShiftMasterDAL : BaseDAL
    {
        public abstract SelectByCountryIDResponse SelectCountryByID(SelectByCountryIDRequest ObjRequest);
        public abstract SelectShiftLogResponse SelectShiftLogRecordbyID(SelectShiftLogRequest ObjRequest);
        public abstract SelectShiftLogResponse SelectAllShiftLog(SelectShiftLogRequest ObjRequest);
        public abstract SelectShiftLogResponse SelectJoinShiftMasterandLog(SelectShiftLogRequest ObjRequest);
        public abstract SelectShiftLogResponse SelectShiftInEnabled(SelectShiftLogRequest ObjRequest);
        public abstract SelectShiftLogResponse SelectMaxShiftInEnabled(SelectShiftLogRequest ObjRequest);
        public abstract SelectShiftLogResponse SelectJoinShiftAmount(SelectShiftLogRequest ObjRequest);
        public abstract SelectShiftLookUpResponse SelectShiftLookUp(SelectShiftLookUpRequest ObjRequest);
        public abstract SelectShiftListForCategoryResponse SelectShiftListByCountry(SelectShiftListForCategoryRequest RequestObj);
        public abstract SelectXReportByDetailsResponse GetXReceipt(SelectXReportByDetailsRequest RequestObj);
        public abstract SelectXReportByDetailsResponse GetXReceipt1(SelectXReportByDetailsRequest RequestObj);
        public abstract SelectZReportByDetailsResponse GetZReceipt(SelectZReportByDetailsRequest RequestObj);
        public abstract SelectZReportByDetailsResponse GetZReceipt1(SelectZReportByDetailsRequest RequestObj);
        public abstract SelectZReportByDetailsResponse GetZReceipt2(SelectZReportByDetailsRequest RequestObj);
        public abstract SelectNewZReportByDetailsResponse GetZReceiptdetails(SelectNewZReportByDetailsRequest RequestObj);
        public abstract SelectXReportByDetailsResponse GetXReceiptDetails(SelectXReportByDetailsRequest RequestObj);
        public abstract SelectNewXReportByDetailsReponse GetNewXReceiptDetails(SelectNewXReportByDetailsRequest RequestObj);
        public abstract SelectDayInResponse GetDayIn(SelectDayInRequest objRequest);

        public abstract SelectDayInResponse UpdateShift(SelectDayInRequest objRequest);
        public abstract SelectAllShiftResponse API_SelectALL(SelectAllShiftRequest requestData);
    }
}
