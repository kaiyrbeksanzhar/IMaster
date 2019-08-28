using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;
using static WebAppIMaster.Models.NewManagerModels.PopulationQuestion;

namespace WebAppIMaster.Controllers.AppController
{
    public class PopulationQuestionController : Controller
    {
        // GET: PopulationQuestion
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            WebAppIMaster.Models.NewManagerManage.PopulationQuestion  repository = new WebAppIMaster.Models.NewManagerManage.PopulationQuestion();
            var result = repository.Select();
            return View(result);
        }

        // GET: PopulationQuestion/Details/5
        public ActionResult Details( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            WebAppIMaster.Models.NewManagerManage.PopulationQuestion repository = new WebAppIMaster.Models.NewManagerManage.PopulationQuestion();
            var result = repository.SelectDetails(id);
            return View(result);
        }

        // GET: PopulationQuestion/Create
        public ActionResult Create(int PcategoryId )
        {

            return View(new PopulationCreateMdl()
            {
                PopulationCategoryId = PcategoryId,
            });
        }

        // POST: PopulationQuestion/Create
        [HttpPost]
        public ActionResult Create(int PopulationCategoryId, PopulationCreateMdl model)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                WebAppIMaster.Models.NewManagerManage.PopulationQuestion repository = new WebAppIMaster.Models.NewManagerManage.PopulationQuestion();
                repository.Create(PopulationCategoryId, model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //--------------------b
        public ActionResult ReQuestion(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            WebAppIMaster.Models.NewManagerManage.PopulationQuestion repository = new WebAppIMaster.Models.NewManagerManage.PopulationQuestion();
            var result = repository.GetQuestion(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult ReQuestion(PopulationQuesEditMdl model)
        {
            WebAppIMaster.Models.NewManagerManage.PopulationQuestion repository = new WebAppIMaster.Models.NewManagerManage.PopulationQuestion();
            repository.EditQuestion(model);
            return RedirectToAction("Details", new { model.Id });
        }

        public ActionResult CreatePCategory()
        {
            return View(new PopulationCategoryMdl());
        }

        [HttpPost]
        public ActionResult CreatePCategory( PopulationCategoryMdl model)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                WebAppIMaster.Models.NewManagerManage.PopulationQuestion repository = new WebAppIMaster.Models.NewManagerManage.PopulationQuestion();
                repository.PopulationCreate(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: PopulationQuestion/Edit/5
        public ActionResult Edit( int id )
        {
            return View();
        }

        // POST: PopulationQuestion/Edit/5
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

        // GET: PopulationQuestion/Delete/5
        public ActionResult Delete( int id )
        {
            return View();
        }

        // POST: PopulationQuestion/Delete/5
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
