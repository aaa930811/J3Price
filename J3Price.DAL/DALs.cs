using J3PriceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace J3Price.DAL
{
    public class DALQuiz
    {
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
            string RegionID = str.Split(',')[0];
            string ServiceID = str.Split(',')[1];
            using (var db = new J3PriceEntities())
            {
                var query = from q in db.Quotes
                            join p in db.Products on q.ProductID equals p.ProductID
                            join r in db.RegionMst on q.RegionID equals r.RegionID
                            join service in db.ServiceMst on q.ServiceID equals service.ServiceID
                            join sale in db.SaleTypeMst on q.SaleTypeCode equals sale.SaleTypeCode
                            where q.RegionID == RegionID
                            && q.ServiceID == ServiceID
                            && p.ProductName == model.ProductName
                            && q.DealTime == model.DealTime
                            select new QuotesModel
                            {
                                ID = q.ID,
                                RegionName = r.RegionName,
                                ServiceName = service.ServiceName,
                                SaleTypeName = sale.SaleTypeName,
                                ProductName = p.ProductName,
                                ProducPrice = q.ProducPrice,
                                DealTime = q.DealTime,
                                Bidder = q.Bidder,
                                QuotationTime = q.QuotationTime
                            };
                return query.ToList();
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
        public void CreateQuote(string regionid, string serviceid, string saletype, string productname, string productprice, DateTime dealtime, string dealimageurl, string bidder)
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
                    ProductID = q.ToString(),
                    ProducPrice = productprice,
                    DealTime = dealtime,
                    DealImageUrl = dealimageurl,
                    Bidder = bidder,
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
    }
}
