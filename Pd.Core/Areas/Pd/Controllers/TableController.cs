using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Com;
using Pd.Core.Models;

namespace Pd.Core.Areas.Pd.Controllers
{
    [Area("Pd")]
    [Route("Pd/[controller]/[action]")]
    public partial class TableController : BaseController
    {
        private readonly PDBaseContext _pDBaseContext;
        public TableController(PDBaseContext pDBaseContext)
        {
            _pDBaseContext = pDBaseContext;
        }

    }




}