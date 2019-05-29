using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppIMaster.Controllers.Base
{
    public class LanguageController : Controller
    {
        private static string currentCultureCode;

        public static string CurrentCultureCode { get => currentCultureCode; set => currentCultureCode = value; }

        public static void setCurrentCultureCode( string code )
        {
            CurrentCultureCode = code;
        }

        public ActionResult ChangeCulture( string lang )
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;

            List<string> cultures = new List<string>() { "ru-RU", "kk-KZ" };
            if (!cultures.Contains(lang))
            {
                lang = "ru-RU";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public PartialViewResult _Generate()
        {
            ViewBag.CCC = currentCultureCode;
            return PartialView();
        }

        public static string GetCurrentLanguageCode()
        {
            return CurrentCultureCode;
        }
        public static string GetKzCode()
        {
            return "kk-KZ";
        }
        public static string GetRuCode()
        {
            return "ru-RU";
        }
    }
}