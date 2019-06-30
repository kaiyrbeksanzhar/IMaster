using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using static WebAppIMaster.Models.NewManagerModels.PopulationQuestion;
using static WebAppIMaster.Models.WebApiModel.PopulationQuestionServiceMdl;

namespace WebAppIMaster.Models.WebApiService
{
    public class PopulationQuestionService
    {
        public List<PopulationSelectVmMdl> SelectList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from pq in db.populationQuestions

                        select new PopulationSelectVmMdl
                        {
                            PopulationCategoryId = pq.PopulationCategoryId,
                            PopulationCategoryName = pq.PopulationCategory.Langs.Where(l=>l.LangCode == langcode).Select(l=>l.Name).FirstOrDefault(),
                            populationQuestionList = new List<PopulationSelectVmMdl.PopulationQuestionList>()
                            {
                                new PopulationSelectVmMdl.PopulationQuestionList
                                {
                                    Id = pq.Id,
                                    Description = pq.Langs.Where(l=>l.LangCode== langcode).Select(l=>l.Description).FirstOrDefault(),
                                    Name = pq.Langs.Where(l=>l.LangCode == langcode).Select(l=>l.Title).FirstOrDefault()
                                }
                            }
                        }).ToList();

            return item;
        }

        public PopulationSelect Select( int populationCategoryId )
        {
            List<PopulationSelect.PopulationList> result = new List<PopulationSelect.PopulationList>();
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from pq in db.populationQuestions

                        where pq.PopulationCategoryId == populationCategoryId

                        select new
                        {
                            Name = pq.Langs.Where(l=>l.LangCode == langcode).Select(l=>l.Title).FirstOrDefault(),
                            Description = pq.Langs.Where(l=>l.LangCode==langcode).Select(l=>l.Description).FirstOrDefault(),
                            Id = pq.Id
                        }).ToList();

            
            return new PopulationSelect
            {
                PopulationCategoryName = db.populationCategoryLangs.Where(l=>l.LangCode == langcode).Where(l=>l.PopulationCategoryId == populationCategoryId).Select(l=>l.Name).FirstOrDefault(),
                populationQuestionList = item.Select(p=> new PopulationSelect.PopulationList
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                }).ToList(),
            };
        }
    }
}