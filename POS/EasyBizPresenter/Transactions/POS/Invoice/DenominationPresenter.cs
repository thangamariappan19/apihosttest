using EasyBizBLL.Transactions.POS;
using EasyBizDBTypes.Transactions.POS;
using EasyBizIView.Transactions.IPOS.IInvoice;
using EasyBizRequest.Transactions.POS.DenominationRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizPresenter.Transactions.POS.Invoice
{
    public class DenominationPresenter
    {
        IDenominationView _IDenominationView;
        DenominationBLL _DenominationBLL = new DenominationBLL();
        public DenominationPresenter(IDenominationView ViewObj)
       {
           _IDenominationView = ViewObj;
       }
        public void SaveDenominations()
        {
            try
            {
                var RequestData = new SaveDenominationRequest();
                RequestData.ReceivedDenominationData = new ReceivedDenomination();
                RequestData.ReceivedDenominationData.ID = _IDenominationView.ReceivedDenominationRecord.ID;
                RequestData.ReceivedDenominationData.DenominationType = _IDenominationView.ReceivedDenominationRecord.DenominationType;
                RequestData.ReceivedDenominationData.DenominationValue = _IDenominationView.ReceivedDenominationRecord.DenominationValue;
                RequestData.ReceivedDenominationData.DenominationNumber = _IDenominationView.ReceivedDenominationRecord.DenominationNumber;
                RequestData.ReceivedDenominationData.TotalDenominationValue = _IDenominationView.ReceivedDenominationRecord.TotalDenominationValue;
                //RequestData.ReceivedDenominationData.CreateBy = 0;
                RequestData.ReceivedDenominationData = _IDenominationView.ReceivedDenominationRecord;

                var ResponseData = _DenominationBLL.SaveDenomination(RequestData);
                _IDenominationView.Message = ResponseData.DisplayMessage;
                _IDenominationView.ProcessStatus = ResponseData.StatusCode;
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
