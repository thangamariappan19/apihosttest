using EasyBizDBTypes.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizIView.Masters.IDocumentNumberingMaster
{
    public interface IDocumentNumberingView : IBaseView
    {
        int ID { get; set; }
        int CountryID { get; set; }
        int StateID { get; set; }
        int StoreID { get; set; }
        //int PosID { get; set; }
        int DocumentTypeID { get; set; }            

        List<DocumentNumberingDetails> DocumentNumberingDetailsList { get; set; }

        List<CountryMaster> CountryMasterLookUp { set; }

        List<StoreMaster> StoreMasterLookUp { set; }
        List<PosMaster> PosMasterLookUp { set; }
        List<DocumentTypes> DocumentTypeMasterLookUp { set; }

        List<StateMaster> StateMasterLookUp { set; }

        string MaxDate { get; set; }

        string CountryCode { get; }
        string StateCode { get; }
        //string PosCode { get; }
        string StoreCode { get; }

        bool IsActive { get; set; }
    }
}
