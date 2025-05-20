using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sarkis.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
          public ActionResult Meniu()
          {
               return View();
          }
          public ActionResult About()
          {
               return View();
          }
          public ActionResult Sali ()
          {
               return View();
          }
          public ActionResult Galerie()
          {
               return View();
          }
          public ActionResult Contact()
          {
               return View();
          }
          public ActionResult Rezervare()
          {
               return View();
          }
          public ActionResult profile()
          {
               return View();
          }
        
     }
}