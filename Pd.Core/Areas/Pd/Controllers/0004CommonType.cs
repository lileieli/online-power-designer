using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Models;

namespace Pd.Core.Areas.Pd.Controllers
{
    public partial class TableController : BaseController
    {
        
        public JsonResult GetCommonType(List<string> CommonBrlong)
        {
            var Result = CommonType(CommonBrlong);
            return Json(new { Rows = Result });
        }
        public List<CommonType> CommonType(List<string > CommonBrlong)
        {
            var Result = _pDBaseContext.CommonType.Where(p => CommonBrlong.Contains(p.CommonBrlong)).ToList();
            return Result;
        }
    }
}
