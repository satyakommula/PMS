using AutoMapper;
using PMS.Models;
using PMS.Services;
using PMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                
                var user = _userService.Get(model.UserID);
                if (user == null)
                {
                    ViewBag.Error = "invalidUser";

                    return View(model);
                }
                else
                {
                    string password = CryptoServiceProvider.EncMD5(model.Password);
                    if (user.Password != password)
                    {
                        ViewBag.Error = "invalidPassword";

                        return View(model);
                    }
                }

                FormsAuthentication.SetAuthCookie(model.UserID, false);//RememberMe
                if (this.Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    // return RedirectToAction("Index", "PMS/ProjectMaster");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = "error";

                return View(model);

            }
        }

        // GET: Account
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SignUpUser(SignUpModel signUpModel)
        {
            int result = 0;
            signUpModel.Password = CryptoServiceProvider.EncMD5(signUpModel.Password);

            //tblUser user = new tblUser
            //{
            //    Email= signUpModel.Email,
            //    FirstName= signUpModel.FirstName,
            //    LastName= signUpModel.LastName,
            //    Password = signUpModel.Password,

            //};

            tblUser user = _mapper.Map<tblUser>(signUpModel);

            result = _userService.Insert(user);

            if (result == 1)
            {
                FormsAuthentication.SetAuthCookie(signUpModel.Email, false);//RememberMe
               // return RedirectToAction("Index", "Home");
            }
            else
            {
               // return RedirectToAction("SignUp", "Account");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}