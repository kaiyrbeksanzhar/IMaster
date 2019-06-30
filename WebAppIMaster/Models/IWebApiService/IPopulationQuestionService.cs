using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebAppIMaster.Models.NewManagerModels.PopulationQuestion;
using static WebAppIMaster.Models.WebApiModel.PopulationQuestionServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface IPopulationQuestionService
    {
        List<PopulationSelectVmMdl> SelectList();
        PopulationSelect Select( int populationCategoryId );
    }
}
