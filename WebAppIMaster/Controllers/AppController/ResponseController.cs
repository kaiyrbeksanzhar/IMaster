using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;

namespace WebAppIMaster.Controllers.AppController
{
    public class ResponseController : Controller
    {
        // GET: Response
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ResponseManager repositiry = new ResponseManager(db);
            var result = repositiry.GetList();
            return View(result);
        }

        public ActionResult Details(int OrderId, string ExecutorId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ResponseManager repositiry = new ResponseManager(db);
            var result = repositiry.GetSelect(OrderId, ExecutorId);
            return View(result);
        }
    }
}