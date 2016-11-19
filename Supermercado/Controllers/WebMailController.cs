using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supermercado.Controllers
{
    [AllowAnonymous]
    public class WebMailController : Controller
    {
        // GET: Index view  
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult SendEmailView()
        {
            //call SendEmailView view to invoke webmail  
            return View();
        }
    }
}