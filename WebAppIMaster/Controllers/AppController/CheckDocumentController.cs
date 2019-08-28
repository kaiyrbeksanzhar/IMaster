using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;

namespace WebAppIMaster.Controllers.AppController
{
    public class CheckDocumentController : Controller
    {
        // GET: CheckDocument
        public ActionResult DocumentList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CheckDocumentManager repository = new CheckDocumentManager(db);
            var result = repository.DocumentList();
            return View(result);
        }

        public ActionResult Document(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CheckDocumentManager repository = new CheckDocumentManager(db);
            var result = repository.DocumentDetails(Id);
            return View(result);
        }
        public ActionResult Details(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CheckDocumentManager repository = new CheckDocumentManager(db);
            var result = repository.DocumentDetails(Id);
            if(result == null)
            {
                return RedirectToAction("Index", "Category");
            }
            return View(result);
        }
    }
}