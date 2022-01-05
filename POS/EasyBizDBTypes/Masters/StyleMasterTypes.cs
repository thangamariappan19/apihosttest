using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyBizDBTypes.Masters
{

    [DataContract]
    [Serializable]
    public class StyleMasterTypes:BaseType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string DocumentNumbering { get; set; }

        [DataMember]
        public string StyleCode { get; set; }

        [DataMember]
        public string StyleDescription { get; set; }



        [DataMember]
        public string ProductDescription { get; set; }

        [DataMember]
        public string Status { get; set; }


        [DataMember]
        public int ProductLine { get; set; }

        [DataMember]
        public int ProductGroup { get; set; }


        [DataMember]
        public string ApparelGroup1 { get; set; }


        [DataMember]
        public int SAPItemGroup { get; set; }

        [DataMember]
        public int Season { get; set; }


        [DataMember]
        public int SubBrandID { get; set; }


        [DataMember]
        public string SubCollection { get; set; }


        [DataMember]
        public string Composition { get; set; }


        [DataMember]
        public string CareInstructionGroup { get; set; }

        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public int Designer { get; set; }


        [DataMember]
        public string Division { get; set; }


        [DataMember]
        public string Year { get; set; }


        [DataMember]
        public string Owner { get; set; }


        [DataMember]
        public string StyleDrop { get; set; }


     

       

      


        [DataMember]
        public string Barcode { get; set; }


        [DataMember]
        public bool InventoryItem { get; set; }


        [DataMember]
        public bool SalesItem { get; set; }


        [DataMember]
        public bool PurchaseItem { get; set; }


        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string SubBrandName { get; set; }



    }
}
