using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;
using WebAppIMaster.Models.NewManagerModels;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Controllers.AppController
{
    public class UserAgreementController : Controller
    {
        // GET: UserAgreement
        public ActionResult Index(string inf)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserAgreementService repository = new UserAgreementService();
            var model = repository.SelectList();
            ViewBag.Inf = inf;
            return View(model);
        }

        // GET: UserAgreement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAgreement/Create
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if(db.userAgreements.Count() >= 2)
            {
                string inf = "Не возможно создать больше <Пользовательских соглашении>";
                return RedirectToAction("Index", new { inf });
            }
            return View(new UserAgreementGeneral());
        }

        // POST: UserAgreement/Create
        [HttpPost]
        public ActionResult Create( UserAgreementGeneral model )
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                UserAgreementService repository = new UserAgreementService();
                repository.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserAgreement/Edit/5
        public ActionResult Edit(int id)
        {
            UserAgreementService repository = new UserAgreementService();
            var result = repository.UserAgreementEditSelect();

            return View(result);
        }

        // POST: UserAgreement/Edit/5
        [HttpPost]
        public ActionResult Edit(UserAgreementEdit model)
        {
            if(model.Description_kz != null && model.Description_ru != null)
            {
                UserAgreementService repository = new UserAgreementService();
                repository.UserAgreementEditManager(model);
            }
            
            return RedirectToAction("Index");
        }

        // GET: UserAgreement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAgreement/Delete/5
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
