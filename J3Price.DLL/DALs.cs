using System;
using System.Collections.Generic;
using System.Linq;

namespace J3Price.DAL
{
    public class DALs
    {
        /// <summary>
        /// 获取报价信息
        /// </summary>
        /// <param name="region"></param>
        /// <param name="server"></param>
        /// <param name="productname"></param>
        /// <param name="dealtime"></param>
        /// <returns></returns>
        public List<Quotes> GetQuotesList(string region ,string server,string productname,DateTime dealtime)
        {
            using (var db = new J3PriceEntities()) {
                var q = from s in db.Quotes
                        join p in db.Products
                        on s.ProductID equals p.ProductID
                        where s.Region == region
                        && s.Server == server
                        && p.ProductName == productname
                        && s.DealTime == dealtime
                        select s;
                return q.ToList();
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
        public void CreateQuote(string region, string server, string saletype,string productname,string productprice, DateTime dealtime,string dealimageurl,string bidder)
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

                var quote = new Quotes {
                    Region=region,
                    Server =server,
                    SaleType =saletype,
                    ProductID = q.ToString(),
                    ProducPrice = productprice,
                    DealTime = dealtime,
                    DealImageUrl = dealimageurl,
                    Bidder =bidder,
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
