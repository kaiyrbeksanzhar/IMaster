using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Migrations;
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
                            PopulationCategoryName = pq.PopulationCategory.Langs.Where(l => l.LangCode == langcode).Select(l => l.Name).FirstOrDefault(),
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

        public List<PopulationSelectVmMdl> SelectPopulationQuestionListForPagination(int? CurrentPage = null, int? PageSize = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            if (PageSize == null)
            {
                PageSize = 5;
            }
            List<PopulationSelectVmMdl> news = new List<PopulationSelectVmMdl>();
            string langcode = LanguageController.CurrentCultureCode;
            var query = db.populationQuestions;

            int allPageCount = (int)Math.Ceiling(query.Count() / (double)PageSize);
            if (allPageCount < CurrentPage) CurrentPage = 1;

            var sortedQuery = query.Select(u => new
            {
                PopulationCategoryId = u.PopulationCategoryId,
                PopulationCategoryName = u.PopulationCategory.Langs.Where(l => l.LangCode == langcode).Select(l => l.Name).FirstOrDefault(),
                populationQuestionList = new List<PopulationSelectVmMdl.PopulationQuestionList>()
                            {
                                new PopulationSelectVmMdl.PopulationQuestionList
                                {
                                    Id = u.Id,
                                    Description = u.Langs.Where(l=>l.LangCode== langcode).Select(l=>l.Description).FirstOrDefault(),
                                    Name = u.Langs.Where(l=>l.LangCode == langcode).Select(l=>l.Title).FirstOrDefault()
                                }
                            }
            }).Select(m => new PopulationSelectVmMdl
            {
                PopulationCategoryId = m.PopulationCategoryId,
                PopulationCategoryName = m.PopulationCategoryName,
                populationQuestionList = m.populationQuestionList,
            }).OrderByDescending(u => u.PopulationCategoryName).Skip(((int)CurrentPage - 1) * (int)PageSize).Take((int)PageSize).ToList();

            return sortedQuery;
        }

        public PopulationSelect Select(int populationCategoryId)
        {
            List<PopulationSelect.PopulationList> result = new List<PopulationSelect.PopulationList>();
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;
            var item = (from pq in db.populationQuestions

                        where pq.PopulationCategoryId == populationCategoryId

                        select new
                        {
                            Name = pq.Langs.Where(l => l.LangCode == langcode).Select(l => l.Title).FirstOrDefault(),
                            Description = pq.Langs.Where(l => l.LangCode == langcode).Select(l => l.Description).FirstOrDefault(),
                            Id = pq.Id
                        }).ToList();


            return new PopulationSelect
            {
                PopulationCategoryName = db.populationCategoryLangs.Where(l => l.LangCode == langcode).Where(l => l.PopulationCategoryId == populationCategoryId).Select(l => l.Name).FirstOrDefault(),
                populationQuestionList = item.Select(p => new PopulationSelect.PopulationList
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                }).ToList(),
            };
        }

        public bool SaveRateHowItWork(string userId, int populationQuestionId, Enitities.Enums.Estimate estimate)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;

            try
            {
                RateHowItWorks rateHowItWorks = new RateHowItWorks()
                {
                    UserId = userId,
                    PopulationQuestionId = populationQuestionId,
                    Estimate = estimate,
                    CreatedAt_Date = DateTime.Now,
                };
                db.rateHowItWorks.Add(rateHowItWorks);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}