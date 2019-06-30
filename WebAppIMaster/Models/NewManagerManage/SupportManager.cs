using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class SupportManager
    {
        public List<SupportVmMdl> Select()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from s in db.Supports

                        select new SupportVmMdl
                        {
                            Id = s.Id,
                            Desciption = s.Description,
                            FIO = s.LastName + " " + s.FirstName,
                            CityName = s.City.Langs.Where(l => l.Langcode == langcode).Select(l => l.Name).FirstOrDefault(),
                            PhoneNumber = s.PhoneNumber,
                            Type = s.TypeMessage,
                        }).ToList();
            return item;
        }

        public SupportVmMdl SelectDetails( int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var item = (from s in db.Supports

                        where s.Id == id
                        select new SupportVmMdl
                        {
                            FIO = s.LastName + " " + s.FirstName,
                            Desciption = s.Description,
                            PhoneNumber = s.PhoneNumber,
                            Type = s.TypeMessage,
                        }).SingleOrDefault();
            return item;
        }
    }
}