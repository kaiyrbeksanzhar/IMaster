using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models;
using WebAppIMaster.Models.NewManagerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebAppIMaster.Models.NewManagerModels.SpecilizationModels;
using WebAppIMaster.Models.Enitities;
using System.Threading.Tasks;

namespace WebAppIMaster.Controllers.AppController
{
    public class SpecilizationController : Controller
    {
        // GET: Specilization
        public ActionResult Index( int categoryId )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            SpecilizationManager repository = new SpecilizationManager(db);
            var result = repository.Select(categoryId);
            ViewBag.CategoryName = db.CategoryLangs.Where(cl => cl.CategoryId == categoryId && cl.Langcode == LanguageController.CurrentCultureCode).Select(cl => cl.Name).FirstOrDefault();
            ViewBag.CategiryId = db.Categories.Find(categoryId).Id;
            return View(result);
        }

        // GET: Specilization/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        // GET: Specilization/Create
        public ActionResult Create( int categoryId )
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.CategoryName = db.CategoryLangs.Where(cl => cl.CategoryId == categoryId && cl.Langcode == LanguageController.CurrentCultureCode).Select(cl => cl.Name).FirstOrDefault();
            return View(new SpecilizationCreate()
            {
                CategoryId = categoryId
            });
        }

        // POST: Specilization/Create
        [HttpPost]
        public ActionResult Create( SpecilizationCreate model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.CategoryName = db.CategoryLangs.Where(cl => cl.CategoryId == model.CategoryId && cl.Langcode == LanguageController.CurrentCultureCode).Select(cl => cl.Name).FirstOrDefault();
            SpecilizationManager repository = new SpecilizationManager(db);
            bool result = repository.Insert(this, model);
            if (result)
            {
                return RedirectToAction("Index", "Specilization", new { categoryId = model.CategoryId });
            }
            else
            {
                return View(model);
            }
        }

        // GET: Specilization/Edit/5
        public ActionResult Edit( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SpecilizationManager repository = new SpecilizationManager(db);
            var result = repository.SelectEdit(id);
            return View(result);
        }

        // POST: Specilization/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(SpecilizationEdit model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SpecilizationManager repository = new SpecilizationManager(db);
            int specilizationId = repository.Edit(this, model);
            if (specilizationId != -1)
            {
                var catId = await db.Specializations.FindAsync(specilizationId);
                return RedirectToAction("Index", "Specilization",new { catId.CategoryId });
            }
            else
            {
                return View();
            }
        }

        // GET: Specilization/Delete/5
        public ActionResult Delete( int id, int categoryId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            SpecilizationManager repository = new SpecilizationManager(db);
            repository.Delete(id);
            return RedirectToAction("Index", "Specilization", new { categoryId = categoryId });
        }

    }
}
