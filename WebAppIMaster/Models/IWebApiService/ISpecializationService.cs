using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppIMaster.Models.WebApiModel;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface ISpecializationService
    {
        SpecializationServiceMdl Get( int id );
        List<SpecializationServiceMdl> GetList();
        List<SpecializationServiceMdl> GetList( int categoryId );
    }
}
