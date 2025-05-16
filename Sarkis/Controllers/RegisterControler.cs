using AutoMapper;
using BussinesLogic.Interfaces;
using Domain.Entities.User;
using Sarkis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sarkis.Controllers
{
     public class RegisterControler
     {
          public class RegisterController : Controller
          {
               private readonly ISession _session;

               public RegisterController()
               {
                    var bl = new BussinesLogic();
                    _session = bl.GetSessionBL();
               }

               public ActionResult Index()
               {
                    return View();
               }

               [HttpPost]
               [ValidateAntiForgeryToken]
               public ActionResult Index(UserRegister register)
               {
                    if (ModelState.IsValid)
                    {
                         var data = Mapper.Map<URegisterData>(register);
                        

                         var registerResult = _session.UserRegister(data);
                         if (registerResult.Status)
                         {
                              return RedirectToAction("Index", "Rezervare");
                         }
                         else
                         {
                              ModelState.AddModelError("", registerResult.StatusMsg);
                         }
                    }

                    return View();
               }
          }
     }
}
}