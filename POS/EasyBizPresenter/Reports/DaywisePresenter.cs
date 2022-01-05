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

namespace EasyBizPresenter.Reports
{
    public class DaywisePresenter
    {
        IDaywiseReportView _IDaywiseReportView;
        StoreMasterBLL _StoreMasterBLL = new StoreMasterBLL();
        public DaywisePresenter(IDaywiseReportView ViewObj)
        {
            _IDaywiseReportView = ViewObj;
        }
        public void GetStoreMasterLookUP()
        {
            SelectStoreMasterLookUpRequest RequestData = new SelectStoreMasterLookUpRequest();
            RequestData.ShowInActiveRecords = false;
            SelectStoreMasterLookUpResponse ResponseData = _StoreMasterBLL.SelectStoreMasterLookUp(RequestData);
            if (ResponseData.StatusCode == Enums.OpStatusCode.Success)
            {
                _IDaywiseReportView.StoreMasterList = ResponseData.StoreMasterList;
            }
        }
    }
}
