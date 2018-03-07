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
    public class ServiceController : ApiController
    {
        DALQuiz DAL = new DALQuiz();
        
        public IHttpActionResult GetService(int region_id) {
            var servicelist = DAL.GerServiceList(region_id);
            return Json(servicelist);
        }
    }
}
