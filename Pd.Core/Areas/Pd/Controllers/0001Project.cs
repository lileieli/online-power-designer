using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Helper.Filter;
using Pd.Core.Models;

namespace Pd.Core.Areas.Pd.Controllers
{
    public partial class TableController : BaseController
    {
        public ActionResult Project()
        {

            return View();
        }
        /// <summary>
        /// 根据年份取出项目列表
        /// </summary>
        /// <returns></returns>
       // [LogFilter(LogsOperation=3, TableName= "Project")]
        public JsonResult GetProject()
        {
            var data = GetLikeT(_pDBaseContext.Project, null);
            var Total = data.Keys.First();
            var Rows = data[data.Keys.First()];

            var ProjectList = Rows.GroupBy(p => p.CreateYear).Select(q=>new { 
            year=q.Key,
            data=q.ToList()
            });
            return Json(new { Rows = ProjectList });
        }
        [LogFilter(LogsOperation = 0, TableName = "Project")]
        public JsonResult AddProject(string name)
        {
            string Name = User.FindFirstValue(ClaimTypes.Name);
            _pDBaseContext.Add(new Project {ProjectId=Guid.NewGuid().ToString("N"),ProjectName=name,State=1,Creater= Name, CreateTime=DateTime.Now,CreateYear=DateTime.Now.Year.ToString() });
            _pDBaseContext.SaveChanges();
            return Json(new { success = 1, message = "OK" });
        }
    }
}
