using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;

namespace WebAppIMaster.Controllers.AppController
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ReviewManagerModel repository = new ReviewManagerModel(db);
            var result = repository.Select();

            return View(result);
        }

        public ActionResult ReviewDetails(int Id)
        {
            if(Id != null)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                ReviewManagerModel repository = new ReviewManagerModel(db);
                var result = repository.Details(Id);
                return View(result);
            }
            return RedirectToAction("Index");
        }
    }
}