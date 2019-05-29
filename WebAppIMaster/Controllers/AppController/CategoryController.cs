using WebAppIMaster.Models;
using WebAppIMaster.Models.NewManagerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebAppIMaster.Models.NewManagerModels.CategoryModels;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Controllers.AppController
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryManagerModels repository = new CategoryManagerModels(db);
            var result = repository.Select();
            
            return View(result);
        }

        // GET: Category/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View(new CategoryCreate());
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create( CategoryCreate model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryManagerModels repository = new CategoryManagerModels(db);
            bool result = repository.Insert(this,model);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryManagerModels repository = new CategoryManagerModels(db);
            var result = repository.SelectEdit(id);
            return View(result);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryEdit model )
        {
                ApplicationDbContext db = new ApplicationDbContext();
                CategoryManagerModels repository = new CategoryManagerModels(db);
                int categoryId = repository.Edit(this,model);
            if(categoryId != -1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
           
        }

        // GET: Category/Delete/5
        public ActionResult Delete( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoryManagerModels repository = new CategoryManagerModels(db);
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
