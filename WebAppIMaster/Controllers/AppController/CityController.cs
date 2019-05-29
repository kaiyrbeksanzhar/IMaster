using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models;
using WebAppIMaster.Models.NewManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;

namespace WebAppIMaster.Controllers.AppController
{
    public class CityController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: City
        string lang_kz = LanguageController.GetKzCode();
        string lang_ru = LanguageController.GetRuCode();
        public ActionResult Index()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from c in db.Cities

                        select new CityVmMdl
                        {
                            Id = c.Id,
                            Name = c.Langs.Where(cl => cl.Langcode == langcode).Select(cl => cl.Name).FirstOrDefault(),
                            Latitude = c.Latitude,
                            Longitudey = c.Longitudey,
                            Priroty = c.Preority
                        }).Distinct().OrderBy(c => c.Priroty).ToList();
            //List<CityVmMdl> result = new List<CityVmMdl>();
            //foreach (var item in model)
            //{
            //    result.Add(new CityVmMdl

            //    {
            //        Id = item.Id,
            //        Latitude = item.Lan,
            //        Longitudey = item.Lng,
            //        Name = item.Name,
            //        Priroty = item.Prority
            //    });
            //}
            return View(item);
        }

        // GET: City/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        // GET: City/Create
        public ActionResult Create()
        {
            return View(new CityInsMdl());
        }

        // POST: City/Create
        [HttpPost]
        public ActionResult Create( CityInsMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                City city = new City()
                {
                    Latitude = model.Latitude,
                    Longitudey = model.Longitudey,
                    Preority = model.Preority,
                    Langs = new List<CityLang>()
                    {
                        new CityLang
                        {
                            Name = model.Name_kz,
                            Langcode = lang_kz
                        },
                        new CityLang
                        {
                            Name = model.Name_ru,
                            Langcode = lang_ru
                        }
                    }
                };
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: City/Edit/5
        public ActionResult Edit( int id )
        {
            var model = (from c in db.Cities
                         where c.Id == id

                         select new CityEditMdl
                         {
                             Id = c.Id,
                             Latitude = c.Latitude,
                             Longitudey = c.Longitudey,
                             Preority = c.Preority,
                             Name_kz = c.Langs.Where(cl => cl.Langcode == lang_kz).FirstOrDefault().Name,
                             Namr_ru = c.Langs.Where(cl => cl.Langcode == lang_ru).FirstOrDefault().Name
                         }).SingleOrDefault();
            return View(model);
        }

        // POST: City/Edit/5
        [HttpPost]
        public ActionResult Edit( CityEditMdl model )
        {
            try
            {
                City city = db.Cities.Find(model.Id);
                city.Latitude = model.Latitude;
                city.Longitudey = model.Longitudey;
                city.Preority = model.Preority;
                city.Langs = db.CityLangs.Where(cl => cl.CityId == city.Id).ToList();

                city.Langs.Where(cl => cl.Langcode == lang_kz).Single().Name = model.Name_kz;
                city.Langs.Where(cl => cl.Langcode == lang_ru).Single().Name = model.Namr_ru;


                db.Entry(city).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: City/Delete/5
        public ActionResult Delete( int id )
        {
            var model = db.Cities.Find(id);
            if (model != null)
            {
                db.Cities.Remove(model);
                db.SaveChanges();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }


    }
}
