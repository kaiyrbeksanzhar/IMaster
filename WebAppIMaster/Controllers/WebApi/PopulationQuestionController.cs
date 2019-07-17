using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppIMaster.Models.Enitities;
using WebAppIMaster.Models.WebApiService;
using static WebAppIMaster.Models.NewManagerModels.PopulationQuestion;
using static WebAppIMaster.Models.WebApiModel.PopulationQuestionServiceMdl;

namespace WebAppIMaster.Controllers.WebApi
{   
    /// <summary>
    ///  Как это работает
    /// </summary>
    public class PopulationQuestionController : ApiController
    {
        /// <summary>
        /// (api/GetPopulationList) возращает лист элементов
        /// </summary>
        [System.Web.Http.Route("api/GetPopulationList")]
        public List<PopulationSelectVmMdl> GetPopulationList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            PopulationQuestionService repository = new PopulationQuestionService();
            var model = repository.SelectList();
            return model;
        }

        /// <summary>
        /// api/GetPopulationListForPagination/{currentPage,pageSize} возвращает List - Как это работает по pageSize
        /// </summary>
        /// <param name="currentPage">Принимает параметр currentPage(int).</param>
        /// <param name="pageSize">Принимает параметр pageSize(int).</param>
        [System.Web.Http.Route("api/GetPopulationListForPagination")]
        public List<PopulationSelectVmMdl> GetPopulationListForPagination(int? currentPage = null, int? pageSize = null)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            PopulationQuestionService repository = new PopulationQuestionService();
            var model = repository.SelectPopulationQuestionListForPagination(currentPage,pageSize);
            return model;
        }

        /// <summary>
        /// передайте populationCategoryId(api/Support/5) возращает один элемент
        /// </summary>
        /// <param name="populationCategoryId">Принимает параметр (populationCategoryId).(populationCategoryId)</param>
        [System.Web.Http.Route("api/GetPopulationQuestion")]
        public PopulationSelect GetPopulationQuestion( int populationCategoryId )
        {
            ApplicationDbContext db = new ApplicationDbContext();
            PopulationQuestionService repository = new PopulationQuestionService();
            var model = repository.Select(populationCategoryId);
            return model;
        }
    }
}
