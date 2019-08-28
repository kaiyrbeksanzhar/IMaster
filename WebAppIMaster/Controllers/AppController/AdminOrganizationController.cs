using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models;
using WebAppIMaster.Models.Enitities;
using static WebAppWebAppIMaster.SmsService;

namespace WebAppIMaster.Controllers.AppController
{
    
    public class AdminOrganizationController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminOrganizationController()
        {
        }

        public AdminOrganizationController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        [Authorize]
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("Profil");
        }

        [Authorize]
        public async Task<ActionResult> Profil(string Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = await db.Users.SingleAsync(p => p.UserName == User.Identity.Name);

            ViewBag.User = user;

            return View();
        }
    }
}