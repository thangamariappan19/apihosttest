using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.DocumentNumberingMasterRequest;
using EasyBizResponse.Masters.DocumentNumberingMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseDocumentNumberingMasterDAL : BaseDAL
    {
        public abstract SelectDocumentNumberingMasterLookUpResponse SelecDocumentNumberingMasterLookUp(SelectDocumentNumberingMasterLookUpRequest ObjRequest);

        public abstract SelectDocumentNumberingDetailsResponse SelecDocumentNumberingDetails(SelectDocumentNumberingDetailsRequest ObjRequest);
        public abstract SelectDocumentNumberingDetailsResponse SelectAutoIncrementID(SelectDocumentNumberingDetailsRequest ObjRequest);
        public abstract SelectDocumentNumberingBillNoDetailsResponse SelectDocumentNumberingBillNoDetails(SelectDocumentNumberingBillNoDetailsRequest ObjRequest);
        //public abstract SelectDocumentNumberingBillNoDetailsResponse SelectDocumentNumberingCustomerDetails(SelectDocumentNumberingBillNoDetailsRequest ObjRequest);
        public abstract SelectByIDDocumentNumberingMasterResponse SelectHeaderID(SelectByIDDocumentNumberingMasterRequest ObjRequest);
        public abstract UpdateRunningNumResponse UpdateRunningNum(UpdateRunningNumRequest ObjRequest);
        public abstract SelectByIDDocumentNumberingMasterResponse DateValidation(SaveDocumentNumberingMasterRequest ObjRequest);

        public abstract SelectDocumentNumberingBillNoDetailsResponse SelectDocumentNumberingDetailsAPI(SelectDocumentNumberingBillNoDetailsRequest requestObj);
        public abstract SelectAllDocumentNumberingMasterResponse API_SelectALL(SelectAllDocumentNumberingMasterRequest requestData);
    }
}
