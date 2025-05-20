using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sarkis.Controllers
{
     public class ReservationController : Controller
     {
          [HttpPost]
          public ActionResult Buy()
          {

               if (!User.Identity.IsAuthenticated)
               {
                    return RedirectToAction("Authentification","Auth");
               }


               return RedirectToAction("Reservare");
          }

          public ActionResult Confirm()
          {
               return View();
          }
     }
}