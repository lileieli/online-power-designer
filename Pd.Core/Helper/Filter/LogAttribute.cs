using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Pd.Core.Helper.Filter
{

    public class LogFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 0新增 1 删除 2修改 3 修改 4查询
        /// </summary>
        public int LogsOperation { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        public string TypeId { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Action 之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            #region 
            string Param = "";
            if (filterContext.HttpContext.Request.Method.ToUpper() == "POST")
            {
                try
                {
                    var forms = filterContext.HttpContext.Request.Form;
                    if (forms.Keys.Count > 0)
                    {
                        foreach (var item in forms.Keys)
                        {
                            var Value = forms[item];
                            Param += item + ":" + Value + ";";
                        }
                    }
                }
                catch (Exception)
                {
                }


            }
            else if (filterContext.HttpContext.Request.Method.ToUpper() == "GET")
            {
                var queryString = filterContext.HttpContext.Request.QueryString;
                Param = queryString.ToString();
            }
            string Action = filterContext.HttpContext.Request.Path;
            using (PDBaseContext dBaseContext = new PDBaseContext())
            {
                TableLogs tableLogs = new TableLogs();
                tableLogs.LogsId = Guid.NewGuid().ToString("N");
                tableLogs.OperateId = TypeId;
                tableLogs.MendName = Action;
                tableLogs.IngOrEd = "OnActionExecuting";
                tableLogs.TableName = TableName;
                tableLogs.Param = Param;
                tableLogs.LogsOperation = LogsOperation;
                tableLogs.CreateTime = DateTime.Now;
                tableLogs.Creater = filterContext.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
                dBaseContext.Add(tableLogs);
                dBaseContext.SaveChanges();
            }
            #endregion

        }
        /// <summary>
        /// Action 之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            #region MyRegion
            string Action = filterContext.HttpContext.Request.Path;
            string Param = "";
            try
            {
                Param = JsonConvert.SerializeObject(filterContext.Result);
            }
            catch (Exception)
            {
            }
            using (PDBaseContext dBaseContext = new PDBaseContext())
            {
                TableLogs tableLogs = new TableLogs();
                tableLogs.LogsId = Guid.NewGuid().ToString("N");
                tableLogs.OperateId = TypeId;
                tableLogs.MendName = Action;
                tableLogs.IngOrEd = "OnActionExecuted";
                tableLogs.TableName = TableName;
                tableLogs.Param = Param;
                tableLogs.LogsOperation = LogsOperation;
                tableLogs.CreateTime = DateTime.Now;
                tableLogs.Creater = filterContext.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
                dBaseContext.Add(tableLogs);
                dBaseContext.SaveChanges();
            }
            #endregion
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }

    }
}
