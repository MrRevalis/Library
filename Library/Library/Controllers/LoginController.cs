using Library.Domain.Abstract;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        private IAccountRepository repository;
        public LoginController(IAccountRepository repositoryParam)
        {
            repository = repositoryParam;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginData)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}