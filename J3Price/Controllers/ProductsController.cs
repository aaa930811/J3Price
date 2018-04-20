using J3Price.DAL;
using J3PriceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace J3Price.Controllers
{
    public class ProductsController : ApiController
    {
        public List<ProductModel> GetProducts()
        {

            DALQuiz DAL = new DALQuiz();
            var list = DAL.GetProduts();
            List<ProductModel> plist = new List<ProductModel>();
            foreach (var item in list)
            {
                ProductModel model = new ProductModel();
                model.ProductID = item.ProductID;
                model.ProductName = item.ProductName;
                if (string.IsNullOrEmpty(item.ProductNickName1))
                {
                    model.ProductNickName1 = "";
                }
                else
                {
                    model.ProductNickName1 = item.ProductNickName1;
                }
                if (string.IsNullOrEmpty(item.ProductNickName2))
                {
                    model.ProductNickName2 = "";
                }
                else
                {
                    model.ProductNickName2 = item.ProductNickName2;
                }
                if (string.IsNullOrEmpty(item.ProductNickName3))
                {
                    model.ProductNickName3 = "";
                }
                else
                {
                    model.ProductNickName3 = item.ProductNickName3;
                }
                if (string.IsNullOrEmpty(item.ProductNickName4))
                {
                    model.ProductNickName4 = "";
                }
                else
                {
                    model.ProductNickName4 = item.ProductNickName4;
                }
                if (string.IsNullOrEmpty(item.ProductNickName5))
                {
                    model.ProductNickName5 = "";
                }
                else
                {
                    model.ProductNickName5 = item.ProductNickName5;
                }
                model.ProductImageUrl = item.ProductImageUrl;
                model.ExteriorName = item.ExteriorName;
                plist.Add(model);
            }
            return plist;
        }
    }
}
