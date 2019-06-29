using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Controllers.AppController
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(int? pageNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsManager repository = new NewsManager(db);
            var result = repository.Select(pageNumber);

            return View(result);
        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View(new NewsCreateMdl());
        }

        // POST: News/Create
        [HttpPost]
        public ActionResult Create( NewsCreateMdl model )
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                NewsManager repository = new NewsManager(db);
                repository.Create(model, this);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
