using J3Price.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace J3Price.Controllers
{
    public class AdController : ApiController
    {
        public string[] GetAdImageUrl() {
            DALQuiz DAL = new DALQuiz();
            return DAL.GetAdImageUrl();
        }
    }
}
