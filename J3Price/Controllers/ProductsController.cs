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
        public List<ProductModel> GetProducts() {

            DALQuiz DAL = new DALQuiz();
            return DAL.GetProduts();
        }
    }
}
