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
            ApplicationDbContext db = new ApplicationDbContext();
            OrganizationManagersModel repository = new OrganizationManagersModel(db);
            List<OrganizationListVMMdl> model = repository.SelectList();
            return View(model);
        }

        // GET: Organization/Details/5
        public ActionResult Details( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            OrganizationManagersModel repository = new OrganizationManagersModel(db);
            OrganizationVmMdl model = repository.SelectOrganization(id);
            return View(model);
        }

        public ActionResult IndexOrganizationPrice( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            OrganizationManagersModel repository = new OrganizationManagersModel(db);
            List<IndexOrganizationPrice> model = repository.SelectOrganizationPrice(id);
            return View(model);
        }


        public ActionResult CreateOrganizationPrice( int organizationId )
        {
            return View(new CreateOrganizationPrice()
            {
                OrganizationId = organizationId,
            });
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult CreateOrganizationPrice( CreateOrganizationPrice model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                OrganizationManagersModel repository = new OrganizationManagersModel(db);
                repository.InsertOrganizationPrice(model);
                return RedirectToAction("IndexOrganizationPrice", new { id = model.OrganizationId });
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }


        // GET: Organization/Create
        public ActionResult CreatePhotoOrganization( int organizationId )
        {
            return View(new PhotoCreateOrganizationMdl()
            {
                OrganizationId = organizationId,
            });
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult CreatePhotoOrganization( PhotoCreateOrganizationMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                OrganizationManagersModel repository = new OrganizationManagersModel(db);
                repository.InsertPhotoOrganization(model);
                return RedirectToAction("Details", new { id = model.OrganizationId });
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            ViewBag.City = CitySelectList.SelectListForSelectList();
            ViewBag.CategoryMarket = CategoryMarketSelectList.SelectListForSelectList();
            return View(new OrganizationCreateMdl());
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult Create( OrganizationCreateMdl model )
        {
            ViewBag.City = CitySelectList.SelectListForSelectList();
            ViewBag.CategoryMarket = CategoryMarketSelectList.SelectListForSelectList();
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

        public ActionResult IndexCategoryMarket()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            OrganizationManagersModel repository = new OrganizationManagersModel(db);
            List<CategoryMarketVmMdl> result = repository.SelectListCategoryMarket();
            if (result != null)
            {
                return View(result);
            }
            return View(new List<CategoryMarketVmMdl>()
            {
                new CategoryMarketVmMdl
                {
                    CategoryMarketName = "нет данных",
                    Id = 1,
                    CatgoryNames = new List<CategoryMarketVmMdl.CatgoryName>()
                    {
                        new CategoryMarketVmMdl.CatgoryName


                        {
                            Id =  1,
                            Name = "нет данных",
                        }
                    },
                }
            });
        }
        // GET: Organization/Create
        public ActionResult CreateCategoryMarket()
        {
            ViewBag.Category = CategorySelectList.SelectListForSelectList();
            return View(new CategoryMarketInsert());
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult CreateCategoryMarket( CategoryMarketInsert model )
        {
            ViewBag.Category = CategorySelectList.SelectListForSelectList();
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                OrganizationManagersModel repository = new OrganizationManagersModel(db);
                repository.OrganizationCategoryInsert(model);
                return RedirectToAction("IndexCategoryMarket");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult IndexPromotionAndDiscount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            OrganizationManagersModel repository = new OrganizationManagersModel(db);
            List<PromotionAndDiscountVmMdl> result = repository.SelectListPromotionAndDiscount();
            return View(result);
        }

        public ActionResult CreatePromotionAndDiscount( int organizationId )
        {
            return View(new PromotionAndDiscountCreate()
            {
                OrganizationId = organizationId,
            });
        }

        // POST: Organization/Create
        [HttpPost]
        public ActionResult CreatePromotionAndDiscount( PromotionAndDiscountCreate model )
        {
            ViewBag.Category = CategorySelectList.SelectListForSelectList();
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                model.CreatedAt = DateTime.Now;
                OrganizationManagersModel repository = new OrganizationManagersModel(db);
                repository.CreatePromotionAndDiscount(model);
                return RedirectToAction("IndexPromotionAndDiscount");
            }
            catch
            {
                return View(model);
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

        public ActionResult DeletePromotionAndDiscount( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var model = db.OrganizationPromotionAndDiscounts.Where(op => op.OrganizationCardId == id).SingleOrDefault();
            db.OrganizationPromotionAndDiscounts.Remove(model);
            db.SaveChanges();
            return RedirectToAction("IndexPromotionAndDiscount");
        }
    }
}
