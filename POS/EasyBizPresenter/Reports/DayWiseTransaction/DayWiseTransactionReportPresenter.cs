using EasyBizBLL.Masters;
using EasyBizDBTypes.Common;
using EasyBizIView.Reports;
using EasyBizRequest.Masters.StoreMasterRequest;
using EasyBizResponse.Masters.StoreMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Reports.DayWiseTransaction
{
    public class DayWiseTransactionReportPresenter
    {
        IDayWiseTransactionReportView _IDaywiseTransactionReportView;
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public DayWiseTransactionReportPresenter(IDayWiseTransactionReportView ViewObj)
        {
            _IDaywiseTransactionReportView = ViewObj;
        }
        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDaywiseTransactionReportView.StoreMasterList = ResponseData.StoreMasterList;
            }
        }
    }

}
