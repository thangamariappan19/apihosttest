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
    public class DesignMasterTypes:BaseType
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string DesignCode { get; set; }

        [DataMember]
        public string DesignName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ForeignDescription { get; set; }

        [DataMember]
        public int SegamentationID { get; set; }

        [DataMember]
        public int StyleStatusID { get; set; }

        [DataMember]
        public int ProductLineID { get; set; }

        [DataMember]
        public int ProductGroupID { get; set; }

        [DataMember]
        public int SeasonID { get; set; }

        [DataMember]
        public int CollectionID { get; set; }

        [DataMember]
        public int SubCollectionID { get; set; }

        [DataMember]
        public string Composition { get; set; }

        [DataMember]
        public string SimbolGroup { get; set; }

        [DataMember]
        public int BrandID { get; set; }      

        [DataMember]
        public int DesignerID { get; set; }

        [DataMember]
        public int DivisionID { get; set; }

        [DataMember]
        public int DropID { get; set; }

        [DataMember]
        public string ProductDepartmentCode { get; set; }
        [DataMember]
        public string AFSegamationName { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public string ProductLineName { get; set; }

        [DataMember]
        public string ProductGroupName { get; set; }

        [DataMember]
        public string SeasonName { get; set; }

        [DataMember]
        public string DropName { get; set; }

        [DataMember]
        public string CollectionName { get; set; }

        [DataMember]
        public string SubCollectionName { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

            
        [DataMember]
        public string DivisionName { get; set; }


        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string DevelopmentOffice { get; set; }
        [DataMember]
        public string ShortDescription { get; set; }
        [DataMember]
        public int YearID { get; set; }

        [DataMember]
        public string YearName { get; set; }


        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Grade { get; set; }
        [DataMember]
        public List<ItemImageMaster> DesignWithItemImageList { get; set; }
        [DataMember]
        public List<DesignMasterTypes> ImportExcelList { get; set; }
        [DataMember]
        public string SegamentationCode { get; set; }
        [DataMember]
        public string StyleStatusCode { get; set; }
        [DataMember]
        public string ProductLineCode { get; set; }
        [DataMember]
        public string ProductGroupCode { get; set; }
        [DataMember]
        public string SeasonCode { get; set; }
        [DataMember]
        public string DropCode { get; set; }
        [DataMember]
        public string CollectionCode { get; set; }
        [DataMember]
        public string SubCollectionCode { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string DesignerCode { get; set; }
        [DataMember]
        public string DivisionCode { get; set; }
        [DataMember]
        public string YearCode { get; set; }
 
    }
}
