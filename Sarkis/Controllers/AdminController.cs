using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sarkis.Controllers
{
     public class AdminController : Controller
     {
          protected override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var cookie = Request.Cookies["X-KEY"];
               if (cookie == null)
               {
                    filterContext.Result = RedirectToAction("Authentification", "Auth");
                    return;
               }

               var user = new BussinesLogic.LogicBussines().GetSessionBL().GetUserByCookie(cookie.Value);
               if (user == null || user.Level != LevelAcces.Admin)
               {
                    filterContext.Result = RedirectToAction("Index", "Home");
               }
          }

          public ActionResult Index()
          {
               return View();
          }

          public ActionResult AddProduct()
          {
               return View();
          }
     }

}