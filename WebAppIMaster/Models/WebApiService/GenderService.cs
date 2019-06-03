using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Models.IWebApiService;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.WebApiService
{
    public class GenderService : IGenderService
    {
        public GenderServiceMdl.GenderNameView Get( int id )
        {
            if (id == 1)
            {
                return new GenderServiceMdl.GenderNameView
                {
                    Id = 1,
                    Name = "Мужской"
                };
            }
            else if (id == 2)
            {
                return new GenderServiceMdl.GenderNameView
                {
                    Id = 2,
                    Name = "Женский"
                };
            }
            return new GenderServiceMdl.GenderNameView()
            {
                Id = -1,
                Name = "не такого Гендера"
            };
        }

        public List<GenderServiceMdl.GenderNameView> GetList()
        {
            return new List<GenderServiceMdl.GenderNameView>
           {
               new GenderServiceMdl.GenderNameView
               {
                   Id=1,
                   Name="Мужской"
               },
               new GenderServiceMdl.GenderNameView
               {
                   Id =2,
                   Name="Мужской"
               }
           };
        }
    }
}