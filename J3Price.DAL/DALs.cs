using J3PriceModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace J3Price.DAL
{
    public class DALQuiz
    {
        private J3PriceEntities db = new J3PriceEntities();
        /// <summary>
        /// 获取报价信息
        /// </summary>
        /// <param name="region"></param>
        /// <param name="server"></param>
        /// <param name="productname"></param>
        /// <param name="dealtime"></param>
        /// <returns></returns>
        public IEnumerable<QuotesModel> GetQuotesList(QuotesQueryModel model)
        {
            string str = model.Server;
            str = str.Substring(1, str.Length - 2);
            int RegionID = int.Parse(str.Split(',')[0]);
            int ServiceID = int.Parse(str.Split(',')[1]);
            int Count = 5;//前N条

            var _query = from q in db.Quotes
                         join p in db.Products on q.ProductID equals p.ProductID
                         join e in db.Exteriors on p.ExteriorID equals e.ExteriorID
                         join r in db.RegionMst on q.RegionID equals r.RegionID
                         join service in db.ServiceMst on q.ServiceID equals service.ServiceID
                         join sale in db.SaleTypeMst on q.SaleTypeCode equals sale.SaleTypeCode
                         where q.RegionID == RegionID
                         orderby p.ProductName, q.DealTime descending
                         select new
                         {
                             q.ID,
                             r.RegionName,
                             service.ServiceID,
                             service.ServiceName,
                             service.ServiceNickName,
                             sale.SaleTypeName,
                             e.ExteriorID,
                             p.ProductName,
                             p.ProductNickName1,
                             p.ProductNickName2,
                             p.ProductNickName3,
                             p.ProductNickName4,
                             p.ProductNickName5,
                             q.ProducPrice,
                             q.DealTime,
                             q.Bidder,
                             q.IsAnonymous,
                             q.QuotationTime
                         };
            switch (ServiceID)
            {
                case 0:
                    //全部服务器
                    if (model.ProductName == null)
                    {
                        //按外观类型查询
                        string sqlQuery = "select * from(select SN = ROW_NUMBER()over(PARTITION by productid order by QuotationTime desc),* from quotes where RegionID=@RegionID)tmp where tmp.SN <= @Count";
                        var list = db.Database.SqlQuery<Quotes>(sqlQuery, new SqlParameter("@RegionID", RegionID), new SqlParameter("@Count", Count));
                        var query = from q in list
                                    join p in db.Products on q.ProductID equals p.ProductID
                                    join e in db.Exteriors on p.ExteriorID equals e.ExteriorID
                                    join r in db.RegionMst on q.RegionID equals r.RegionID
                                    join service in db.ServiceMst on q.ServiceID equals service.ServiceID
                                    join sale in db.SaleTypeMst on q.SaleTypeCode equals sale.SaleTypeCode
                                    where e.ExteriorID == model.ExteriorID
                                    select new QuotesModel
                                    {
                                        ID = q.ID,
                                        RegionName = r.RegionName,
                                        ServiceName = service.ServiceName,
                                        ServiceNickName = service.ServiceNickName,
                                        SaleTypeName = sale.SaleTypeName,
                                        ProductName = p.ProductName,
                                        ProducPrice = q.ProducPrice,
                                        DealTime = q.DealTime,
                                        Bidder = q.Bidder,
                                        IsAnonymous = q.IsAnonymous,
                                        QuotationTime = q.QuotationTime
                                    };
                        return query.ToList();
                    }
                    else
                    {
                        //按物品名称查询(30条)
                        return _query.Where(x =>
                        x.ProductName == model.ProductName
                        || x.ProductNickName1 == model.ProductName
                        || x.ProductNickName2 == model.ProductName
                        || x.ProductNickName3 == model.ProductName
                        || x.ProductNickName4 == model.ProductName
                        || x.ProductNickName5 == model.ProductName
                        ).Select(x => new QuotesModel
                        {
                            ID = x.ID,
                            RegionName = x.RegionName,
                            ServiceName = x.ServiceName,
                            ServiceNickName = x.ServiceNickName,
                            SaleTypeName = x.SaleTypeName,
                            ProductName = x.ProductName,
                            ProducPrice = x.ProducPrice,
                            DealTime = x.DealTime,
                            Bidder = x.Bidder,
                            IsAnonymous = x.IsAnonymous,
                            QuotationTime = x.QuotationTime
                        }).Take(30).ToList();
                    }
                default:
                    //指定服务器
                    if (model.ProductName == null)
                    {
                        //按外观类型查询
                        string sqlQuery = "select * from(select SN = ROW_NUMBER()over(PARTITION by productid order by DealTime desc),* from quotes where RegionID=@RegionID and ServiceID=@ServiceID)tmp where tmp.SN <= @Count";
                        var list = db.Database.SqlQuery<Quotes>(sqlQuery,new SqlParameter("@RegionID", RegionID),new SqlParameter("@ServiceID", ServiceID),new SqlParameter("@Count", Count));
                        var query = from q in list
                                    join p in db.Products on q.ProductID equals p.ProductID
                                    join e in db.Exteriors on p.ExteriorID equals e.ExteriorID
                                    join r in db.RegionMst on q.RegionID equals r.RegionID
                                    join service in db.ServiceMst on q.ServiceID equals service.ServiceID
                                    join sale in db.SaleTypeMst on q.SaleTypeCode equals sale.SaleTypeCode
                                    where e.ExteriorID == model.ExteriorID
                                    select new QuotesModel
                                    {
                                        ID = q.ID,
                                        RegionName = r.RegionName,
                                        ServiceName = service.ServiceName,
                                        ServiceNickName = service.ServiceNickName,
                                        SaleTypeName = sale.SaleTypeName,
                                        ProductName = p.ProductName,
                                        ProducPrice = q.ProducPrice,
                                        DealTime = q.DealTime,
                                        Bidder = q.Bidder,
                                        IsAnonymous = q.IsAnonymous,
                                        QuotationTime = q.QuotationTime
                                    };
                        return query.ToList();
                    }
                    else
                    {
                        //按物品名称查询
                        return _query.Where(x =>
                        x.ServiceID == ServiceID
                        && (x.ProductName == model.ProductName
                        || x.ProductNickName1 == model.ProductName
                        || x.ProductNickName2 == model.ProductName
                        || x.ProductNickName3 == model.ProductName
                        || x.ProductNickName4 == model.ProductName
                        || x.ProductNickName5 == model.ProductName
                        )
                        ).Select(x => new QuotesModel
                        {
                            ID = x.ID,
                            RegionName = x.RegionName,
                            ServiceName = x.ServiceName,
                            ServiceNickName = x.ServiceNickName,
                            SaleTypeName = x.SaleTypeName,
                            ProductName = x.ProductName,
                            ProducPrice = x.ProducPrice,
                            DealTime = x.DealTime,
                            Bidder = x.Bidder,
                            IsAnonymous = x.IsAnonymous,
                            QuotationTime = x.QuotationTime
                        }).Take(30).ToList();
                    }
            }
        }



        /// <summary>
        /// 创建报价信息
        /// </summary>
        /// <param name="region"></param>
        /// <param name="server"></param>
        /// <param name="saletype"></param>
        /// <param name="productname"></param>
        /// <param name="productprice"></param>
        /// <param name="dealtime"></param>
        /// <param name="dealimageurl"></param>
        /// <param name="bidder"></param>
        public void CreateQuote(int regionid, int serviceid, int saletype, string productname, string productprice, DateTime dealtime, string dealimageurl, string bidder, bool IsAnonymous)
        {
            using (var db = new J3PriceEntities())
            {
                var q = from s in db.Products
                        where s.ProductName == productname
                        || s.ProductNickName1 == productname
                        || s.ProductNickName2 == productname
                        || s.ProductNickName3 == productname
                        || s.ProductNickName4 == productname
                        || s.ProductNickName5 == productname
                        select s.ProductID;

                var quote = new Quotes
                {
                    RegionID = regionid,
                    ServiceID = serviceid,
                    SaleTypeCode = saletype,
                    ProductID = q.FirstOrDefault(),
                    ProducPrice = productprice,
                    DealTime = dealtime,
                    DealImageUrl = dealimageurl,
                    Bidder = bidder,
                    IsAnonymous = IsAnonymous,
                    QuotationTime = new DateTime()
                };
                try
                {
                    db.Quotes.Add(quote);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Products GetProductByName(string productName)
        {
            var query = from p in db.Products
                        where p.ProductName == productName
                        || p.ProductNickName1 == productName
                        || p.ProductNickName2 == productName
                        || p.ProductNickName3 == productName
                        || p.ProductNickName4 == productName
                        || p.ProductNickName5 == productName
                        select p;
            return query.FirstOrDefault();
        }

        public List<ProductModel> GetProduts() {
            var query = from s in db.Products
                        join e in db.Exteriors on s.ExteriorID equals e.ExteriorID
                        orderby e.ExteriorName
                        select new ProductModel
                        {
                            ProductID = s.ProductID,
                            ProductName = s.ProductName,
                            ProductNickName1 = s.ProductNickName1,
                            ProductNickName2 = s.ProductNickName2,
                            ProductNickName3 = s.ProductNickName3,
                            ProductNickName4= s.ProductNickName4,
                            ProductNickName5 = s.ProductNickName5,
                            ProductImageUrl = s.ProductImageUrl,
                            ExteriorName = e.ExteriorName,
                        };
            return query.ToList();
        }

        public bool CreateProduct(Products product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<RegionModel> GetRegionList()
        {
            var query = from r in db.RegionMst
                        select new RegionModel
                        {
                            region_id = r.RegionID,
                            region_name = r.RegionName
                        };
            return query.ToList();
        }

        public List<ServiceModel> GerServiceList(int region_id)
        {
            var query = from r in db.ServiceMst
                        where r.RegionID == region_id
                        select new ServiceModel
                        {
                            service_id = r.ServiceID,
                            region_id = r.RegionID,
                            service_name = r.ServiceName,
                            service_nickname = r.ServiceNickName
                        };
            return query.ToList();
        }

        public string[] GetAdImageUrl() {
            var query = from s in db.Advertisement
                        select s.AdImageUrl;
            return query.ToArray();
        }
    }
}
