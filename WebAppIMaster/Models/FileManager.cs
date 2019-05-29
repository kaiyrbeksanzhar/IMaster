using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppIMaster.Models
{
    public class FileManager
    {

        static string CategoryImagesUrl = "/Images/Category";
        static string DefaulAvatarUrl = "/Images/Default/NoPhoto.png";
        private static bool isFilesPathReady;

        private static void PrepareFilesPath( Controller controller )
        {
            if (!Directory.Exists(controller.Server.MapPath(CategoryImagesUrl)))
            {
                Directory.CreateDirectory(controller.Server.MapPath(CategoryImagesUrl));
            }

            isFilesPathReady = true;
        }


        public static string SavePhoto( Controller controller, HttpPostedFileBase postedFile )
        {
            if (!isFilesPathReady)
                PrepareFilesPath(controller);
            string savedLogoUrl = "";
            if (postedFile != null)
            {
                savedLogoUrl = CategoryImagesUrl + "/" + postedFile.FileName.Split('\\').Last();
                postedFile.SaveAs(controller.Server.MapPath(savedLogoUrl));
            }
            else
            {
                savedLogoUrl = DefaulAvatarUrl;
            }
            return savedLogoUrl;
        }

        public static string GetUrlPhoto( string UrlPhoto )
        {
            if (UrlPhoto == null || UrlPhoto.Length == 0)
            {
                return DefaulAvatarUrl;
            }

            return UrlPhoto;
        }
    }
}