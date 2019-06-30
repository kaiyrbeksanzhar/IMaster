using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebAppIMaster.Models.WebApiModel.SupportServiceMdl;

namespace WebAppIMaster.Models.IWebApiService
{
    public interface ISupportService
    {
        int Create( SupportInsertMdl model);
        List<SupportSelectList> SelectList();
        Select Select( int id );

    }
}
