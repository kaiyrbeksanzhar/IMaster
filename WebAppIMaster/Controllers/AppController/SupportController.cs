using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;

namespace WebAppIMaster.Controllers.AppController
{
    public class SupportController : Controller
    {
        // GET: Support
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportManager repository = new SupportManager();
            var result = repository.Select();
            return View(result);
        }

        // GET: Support/Details/5
        public ActionResult Details(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SupportManager repository = new SupportManager();
            var result = repository.SelectDetails(id);
            return View(result);
        }
        
    }
}
