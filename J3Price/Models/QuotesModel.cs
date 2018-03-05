using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace J3Price.Models
{
    public class QuotesModel
    {
        public int ID { get; set; }
        public string RegionName { get; set; }
        public string ServiceName { get; set; }
        public string SaleTypeName { get; set; }
        public string ProductName { get; set; }
        public string ProducPrice { get; set; }
        public Nullable<System.DateTime> DealTime { get; set; }
        public string Bidder { get; set; }
        public Nullable<System.DateTime> QuotationTime { get; set; }
    }
}