using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppIMaster.Controllers.Base;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.NewManagerModels;
using static WebAppIMaster.Models.NewManagerModels.PopulationQuestion;

namespace WebAppIMaster.Models.NewManagerManage
{
    public class PopulationQuestion
    {
        public List<PopulationSelectVmMdl> Select()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from pc in db.populationCategories

                        select new PopulationSelectVmMdl
                        {
                            PopulationCategoryId = pc.Id,
                            PopulationCategoryName = pc.Langs.Where(l => l.LangCode == langcode).Select(l => l.Name).FirstOrDefault(),
                            populationQuestionList = (from pq in pc.populationQuestions

                                                      select new PopulationSelectVmMdl.PopulationQuestionList
                                                      {
                                                          Description = pq.Langs.Where(l=>l.LangCode == langcode).Select(l=>l.Description).FirstOrDefault(),
                                                          Name = pq.Langs.Where(l => l.LangCode == langcode).Select(l => l.Title).FirstOrDefault(),
                                                          Id = pq.Id,
                                                      }).ToList(),
                        }).ToList();
            return item;
        }

        public PopulationDetailsVmMdl SelectDetails(int id )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string langcode = LanguageController.CurrentCultureCode;

            var item = (from pq in db.populationQuestions

                        where pq.Id == id

                        select new PopulationDetailsVmMdl
                        {
                            Id = pq.Id,
                            Name = pq.Langs.Where(l=>l.LangCode == langcode).Select(l=>l.Title).FirstOrDefault(),
                            Description = pq.Langs.Where(l => l.LangCode == langcode).Select(l => l.Description).FirstOrDefault(),
                        }).SingleOrDefault();
            return item;
        }

        public void Create( int CategoryId, PopulationCreateMdl model )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            Models.Enitities.PopulationQuestion PopulationQuestion = new Models.Enitities.PopulationQuestion()
            {
                PopulationCategoryId = model.PopulationCategoryId,
                Langs = new List<PopulationQuestionLangs>()
                {
                    new PopulationQuestionLangs
                    {
                        LangCode =lang_kz,
                        Description = model.Description_kz,
                        Title = model.Name_kz,
                    },
                    new PopulationQuestionLangs
                    {
                        LangCode = lang_ru,
                        Description = model.Description_ru,
                        Title = model.Name_ru
                    }
                },
            };
            db.populationQuestions.Add(PopulationQuestion);
            db.SaveChanges();
        }

        public int Edit( PopulationEditMdl model )
        {
            try
            {
                string lang_kz = LanguageController.GetKzCode();
                string lang_ru = LanguageController.GetRuCode();
                ApplicationDbContext db = new ApplicationDbContext();
                var population = db.populationQuestions.Where(p => p.Id == model.Id).SingleOrDefault();
                if (population == null)
                {
                    return -1;
                }
                population.Langs = db.PopulationQuestionLangs.Where(pl => pl.PopulationQuestionId == model.Id).ToList();
                population.Langs.Where(l => l.LangCode == lang_kz).FirstOrDefault().Title = model.Name_kz;
                population.Langs.Where(l => l.LangCode == lang_ru).FirstOrDefault().Title = model.Name_ru;
                population.Langs.Where(l => l.LangCode == lang_kz).FirstOrDefault().Description = model.Description_kz;
                population.Langs.Where(l => l.LangCode == lang_ru).FirstOrDefault().Description = model.Description_ru;
                db.Entry(population).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return population.Id;
            }
            catch
            {
                return -1;
            }

        }

        public void PopulationCreate( PopulationCategoryMdl model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            PopulationCategory populationCategory = new PopulationCategory()
            {
                Property = model.Property,
                Langs = new List<PopulationCategoryLangs>()
                {
                    new PopulationCategoryLangs
                    {
                        LangCode = lang_kz,
                        Name = model.Name_kz,
                    },
                    new PopulationCategoryLangs
                    {
                        LangCode = lang_ru,
                        Name = model.Name_ru,
                    },
                }
            };
            db.populationCategories.Add(populationCategory);
            db.SaveChanges();
        }

        public void Delete( int populationQuestionId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var models = db.populationQuestions.Find(populationQuestionId);
            db.populationQuestions.Remove(models);
            db.SaveChanges();
        }

        public PopulationQuesEditMdl GetQuestion(int Id)
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            ApplicationDbContext db = new ApplicationDbContext();
            var ques = db.PopulationQuestionLangs.Where(p => p.PopulationQuestionId == Id).ToList();

            PopulationQuesEditMdl mdl = new PopulationQuesEditMdl();
            foreach(var t in ques)
            {
                if(t.LangCode == lang_kz)
                {
                    mdl.Description_kz = t.Description;
                    mdl.Name_kz = t.Title;
                }
                else
                {
                    mdl.Description_ru = t.Description;
                    mdl.Name_ru = t.Title;
                }
                mdl.Id = Id;
            }

            return mdl;
        }

        public void EditQuestion(PopulationQuesEditMdl model)
        {
            string lang_kz = LanguageController.GetKzCode();
            string lang_ru = LanguageController.GetRuCode();

            ApplicationDbContext db = new ApplicationDbContext();
            var ques = db.PopulationQuestionLangs.Where(p => p.PopulationQuestionId == model.Id).ToList();

            WebAppIMaster.Models.Enitities.PopulationQuestion pq = db.populationQuestions.Find(model.Id);
            pq.Langs.Where(l => l.LangCode == LanguageController.GetKzCode()).FirstOrDefault().Description = model.Description_kz;
            pq.Langs.Where(l => l.LangCode == LanguageController.GetRuCode()).FirstOrDefault().Description = model.Description_ru;
            pq.Langs.Where(l => l.LangCode == LanguageController.GetKzCode()).FirstOrDefault().Title = model.Name_kz;
            pq.Langs.Where(l => l.LangCode == LanguageController.GetRuCode()).FirstOrDefault().Title = model.Name_ru;

            db.Entry(pq).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}