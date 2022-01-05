using EasyBizDBTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace EasyBizDBTypes.Dashboard
{
    public class Dashboard_AreaChart
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Dashboard_AreaChart_Child> Series { get; set; }
    }

    public class Dashboard_AreaChart_Child
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Value { get; set; }
    }

    public class Dashboard_AreaChart_AllData
    {


        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string MonthName { get; set; }
        [DataMember]
        public decimal Sales { get; set; }
        [DataMember]
        public int MonthNo { get; set; }

    }

    public class Dashboard_Purchase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Value { get; set; }
    }

    public class Dashboard_Product
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
