using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerManage;
using WebAppIMaster.Models.NewManagerModels;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WebAppIMaster.Controllers.AppController
{
    public class NewsController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "rKTja1mSTCgwIQ0gix83P6v2ocaT67v5Jz4qx7uQ",
            BasePath = "https://plasma-climber-231012.firebaseio.com/",
        };

        IFirebaseClient client;
        // GET: News
        public ActionResult Index(int? pageNumber)
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
        public  ActionResult Create(NewsCreateMdl model)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                ApplicationDbContext db = new ApplicationDbContext();
                NewsManager repository = new NewsManager(db);
                PushNotificationManager pushNotificationManager = new PushNotificationManager();
                int newsId = repository.Create(model, this);
                var result = pushNotificationManager.SendPushNotification(null, model.Title_ru, model.ShortText_ru, DateTime.Now);
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
        public async Task<ActionResult> PushNotification()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            NewsManager repository = new NewsManager(db);
            //var model = repository.SelectNotification();

            var firebaseClient = new FirebaseClient(config);
            FirebaseResponse obj1 = await firebaseClient.GetTaskAsync("News/");
            var myJson = obj1.Body;
            dynamic data = JsonConvert.DeserializeObject<dynamic>(myJson);
            var shorter = ((IDictionary<string, JToken>)data).Select(k =>
    JsonConvert.DeserializeObject<Data>(k.Value.ToString())).ToList();
            return PartialView(shorter);
        }

    }
}
