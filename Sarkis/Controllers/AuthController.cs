using BussinesLogic.Interfaces;
using Domain.Model.User;
using Sarkis.Models.Auth;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Sarkis.Controllers
{
     public class AuthController : Controller
    {
          public ActionResult Authentification()
          {
               return View();
          }
          private readonly IAuth _auth;
          public AuthController() 
          {
               var bl = new BussinesLogic.BussinesLogic();
               _auth=bl.GetAuthBL();
          }

          [HttpPost]
          public ActionResult Authentification(UserDataLogin login)
          {
               var data = new UserLoginDTD { 


                    Password = login.Password,
                    Username = login.Username
                    };
               string token = _auth.UserAuthLogic(data);

            return View();
        }
          
    }
}