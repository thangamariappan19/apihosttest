using EasyBizAbsDAL.Common;
using EasyBizRequest.Masters.TaxMasterRequest;
using EasyBizResponse.Masters.TaxMasterResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizAbsDAL.Masters
{
    public abstract class BaseTaxMasterDAL : BaseDAL
    {
        public abstract SelectTaxLookUpResponse SelectTaxLookUp(SelectTaxLookUpRequest ObjRequest);
        public abstract SelectTaxDetailsByCountryIDResponse SelectTaxDetailsByCountryID(SelectTaxDetailsByCountryIDRequest ObjRequest);
        public abstract SelectAllTaxResponse API_SelectALL(SelectAllTaxRequest requestData);
        public abstract SelectAllTaxResponse API_SelectTaxLookUp(SelectAllTaxRequest objRequest);
    }
}
