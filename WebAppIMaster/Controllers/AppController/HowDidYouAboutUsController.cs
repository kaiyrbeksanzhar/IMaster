using WebAppIMaster.Models;
using WebAppIMaster.Models.NewManagerManage;
using WebAppIMaster.Models.NewManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Controllers.AppController
{
    public class HowDidYouAboutUsController : Controller
    {
        public ActionResult IndexMain( string sourceName, int? pageNumber )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            var result = repository.Select(sourceName, pageNumber);
            return View(result);
        }
        // GET: HowDidYouAboutUs
        public ActionResult Index(string sourceName, int? pageNumber)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            var result = repository.Select(sourceName,pageNumber);
            return View(result);
        }

        // GET: HowDidYouAboutUs/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        // GET: HowDidYouAboutUs/Create
        public ActionResult Create()
        {
            return View(new HowDidYouAboutUsCreate());
        }

        // POST: HowDidYouAboutUs/Create
        [HttpPost]
        public ActionResult Create( HowDidYouAboutUsCreate model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            model.DateTime = DateTime.Now;
            bool result = repository.Insert(model);
            if (result)
            {
                return RedirectToAction("IndexMain");
            }
            else
            {
                return View(model);
            }
        }

        // GET: HowDidYouAboutUs/Edit/5
        public ActionResult Edit( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            var result = repository.SelectEdit(id);
            return View(result);
        }

        // POST: HowDidYouAboutUs/Edit/5
        [HttpPost]
        public ActionResult Edit( HowDidYouAboutUsEdit model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            model.DateTime = DateTime.Now;
            int id = repository.Edit(model);
            if (id != -1)
            {
                return RedirectToAction("IndexMain");
            }
            else
            {
                return View(model);
            }
        }

        // GET: HowDidYouAboutUs/Delete/5
        public ActionResult Delete( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            bool delete = repository.Delete(id);
            if (delete)
            {
                return RedirectToAction("IndexMain");
            }
            return View();
        }

        public PartialViewResult Population(string sourceName1, int? pageNumber1)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HowDidYouAboutUsModel repository = new HowDidYouAboutUsModel(db);
            var result = repository.SelectPopulation(sourceName1, pageNumber1);
            return PartialView(result);
        }


    }
}
