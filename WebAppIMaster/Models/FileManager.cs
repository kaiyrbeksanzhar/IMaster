using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppIMaster.Models
{
    public class FileManager
    {

        static string CategoryImagesUrl = "/Images/Category";
        static string CustomerOrderUrl = "/Images/CustomerOrder";
        static string ImagesSave = "/Images";
        static string AvatarPath = "/Images/Avatar";
        public static string DefaulAvatarUrl = "/Images/Default/NoPhoto.png";
        private static bool isFilesPathReady;

        private static void PrepareFilesPath( Controller controller )
        {
            if (!Directory.Exists(controller.Server.MapPath(CategoryImagesUrl)))
            {
                Directory.CreateDirectory(controller.Server.MapPath(CategoryImagesUrl));
            }

            isFilesPathReady = true;
        }

        public static byte[] ImageToByteArray( System.Drawing.Image imageIn )
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        public static string SavePhoto1( Controller controller, HttpPostedFileBase postedFile )
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

        public static string SaveCustomerOrder( HttpPostedFileBase postedFile )
        {
            string savedLogoUrl = "";
            if (postedFile != null)
            {
                savedLogoUrl = CustomerOrderUrl + "/" + postedFile.FileName.Split('\\').Last();
                postedFile.SaveAs(HttpContext.Current.Server.MapPath(savedLogoUrl));
            }
            else
            {
                savedLogoUrl = DefaulAvatarUrl;
            }
            return savedLogoUrl;
        }

        public static string SavePhoto( Image img )
        {
            string savedLogoUrl = "";
            savedLogoUrl = CategoryImagesUrl + "/" + img;
            img.Save(HttpContext.Current.Server.MapPath(savedLogoUrl));
            return savedLogoUrl;
        }

        public static string SavePhoto2( string img, string clientId )
        {
            string Type = img.Substring(img.LastIndexOf(".") + 1);
            string savedLogoUrl = CategoryImagesUrl + "/" + clientId + Type;
            HttpContext.Current.Server.MapPath(savedLogoUrl);
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

        public static bool SaveAvatarPhoto( byte[] file, string fileType, string userId )
        {

            return true;
        }

        public static void SendMessage()
        {

        }
    }
}