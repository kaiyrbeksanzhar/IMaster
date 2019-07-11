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
    public class OrganizationController : Controller
    {
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        // GET: Organization/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            ViewBag.City = CitySelectList.SelectListForSelectList();
            return View(new OrganizationCreateMdl());
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult Create( OrganizationCreateMdl model )
        {
            ViewBag.City = CitySelectList.SelectListForSelectList();
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                OrganizationManagersModel repository = new OrganizationManagersModel(db);
                var result = repository.OrganizationInsert(model);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        // GET: Organization/Edit/5
        public ActionResult Edit( int id )
        {
            return View();
        }

        // POST: Organization/Edit/5
        [HttpPost]
        public ActionResult Edit( int id, FormCollection collection )
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

        // GET: Organization/Delete/5
        public ActionResult Delete( int id )
        {
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        public ActionResult Delete( int id, FormCollection collection )
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
