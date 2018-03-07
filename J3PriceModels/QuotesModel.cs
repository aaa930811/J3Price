using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace J3PriceModels
{
    public class QuotesModel
    {
        public int ID { get; set; }
        public string RegionName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceNickName { get; set; }
        public string SaleTypeName { get; set; }
        public string ProductName { get; set; }
        public string ProducPrice { get; set; }
        public Nullable<System.DateTime> DealTime { get; set; }
        public string Bidder { get; set; }
        public Nullable<System.DateTime> QuotationTime { get; set; }
    }
    public class QuotesQueryModel
    {
        public string Server { get; set; }
        public string ProductName { get; set; }
        public Nullable<System.DateTime> DealTime { get; set; }
    }
    public class QuotesShowModel
    {
        public int ID { get; set; }
        public string RegionName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceNickName { get; set; }
        public string SaleTypeName { get; set; }
        public string ProductName { get; set; }
        public string ProducPrice { get; set; }
        public string DealTime { get; set; }
        public string Bidder { get; set; }
        public string QuotationTime { get; set; }
    }
}