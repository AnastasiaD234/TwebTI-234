using Sarkis.Models;
using System.Web.Mvc;
using BussinesLogic.Interfaces;
using Sarkis.BussinesLogic;
using Domain.Entities.User;
using System;
using System.Web.UI.WebControls;
using System.Web;
using Domain.Enums;

namespace Sarkis.Controllers
{
     public class AuthController : Controller
     {
          private readonly ISession _session;

          public AuthController()
          {
               var bl = new LogicBussines();
               _session = bl.GetSessionBL();
          }

          // GET: Authentification
          public ActionResult Authentification()
          {
               var model = new AuthViewModel
               {
                    LoginModel = new UserLogin(),
                    RegisterModel = new UserRegister()
               };

               return View(model);
          }

          // POST: Register
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Register(AuthViewModel model)
          {
               if (ModelState.IsValid)
               {
                    
                    var data = new URegisterData
                    {
                         Username = model.RegisterModel.Username,
                         Email = model.RegisterModel.Email,
                         Password = model.RegisterModel.Password,
                         RegisterIp = Request.UserHostAddress,
                         Level = model.RegisterModel.IsAdmin ? LevelAcces.Admin : LevelAcces.User
                    };
                   ;

                    var result = _session.UserRegister(data);

                    if (result.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(data.Username);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                         Response.Cookies.Add(cookie);


                         var user = _session.GetUserByCookie(cookie.Value);

                         if (model.RegisterModel.IsAdmin)
                         {

                              return RedirectToAction("Index", "Admin");
                         }
                         
                         return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", result.StatusMsg);
               }

               return View("Authentification", model);
          }

          // POST: Login
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Login(AuthViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var data = new ULoginData
                    {
                         Credential = model.LoginModel.Credential,
                         Password = model.LoginModel.Password,
                         LoginIp = Request.UserHostAddress,
                         LoginDateTime = DateTime.Now
                    };

                    var result = _session.UserLogin(data);
                    if (result.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(model.LoginModel.Credential);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                         var user = _session.GetUserByCookie(cookie.Value);

                        
                         if (user.Level == LevelAcces.Admin)
                         {
                              return RedirectToAction("Index", "Admin");
                         }
                         return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", result.StatusMsg);
               }

               return View("Authentification", model);
          }
     }
}
