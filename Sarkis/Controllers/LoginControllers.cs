using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BussinesLogic;
using BussinesLogic.Interfaces;
using Domain.Entities.User;
using Sarkis.Models;


namespace Sarkis.Controllers
{

  
          public class LoginController : Controller
          {
               private readonly ISession _session;
               public LoginController()
               {
               var bl = new BussinesLogic();
                    _session = bl.GetSessionBL();
               }


               // GET: Login
               public ActionResult Index()
               {
                    return View();
               }

               [HttpPost]
               [ValidateAntiForgeryToken]
               public ActionResult Index(ULoginData login)
               {
                    if (ModelState.IsValid)
                    {
                    var data = Mapper.Map<ULoginData>(login);

                    

                         var userLogin = _session.UserLogin(data);
                         if (userLogin.Status)
                         {
                              HttpCookie cookie = _session.GenCookie(login.Username);
                              ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                              return RedirectToAction("Index", "Reservare");
                         }
                         else
                         {
                              ModelState.AddModelError("Nume de utilizator sau parola incorecta.", userLogin.StatusMsg);
                              return View();
                         }

                    }

                    return View();
               }
               public ActionResult Logout()
               {
                    if (Request.Cookies["X-KEY"] != null)
                    {
                         var cookie = new HttpCookie("X-KEY")
                         {
                              Expires = DateTime.Now.AddDays(-1)
                         };
                         Response.Cookies.Add(cookie);
                    }

                    Session.Clear();
                    return RedirectToAction("Index", "Home");
               }
          }
     }