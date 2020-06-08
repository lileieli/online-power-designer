using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Helper;
using Pd.Core.Helper.Filter;
using Pd.Core.Models;


namespace Pd.Core.Areas.Pd.Controllers
{
    public partial class TableController : BaseController
    {
        [Authorize]
        public IActionResult TableTable()
        {
            return View();
        }
        /// <summary>
        /// 根据项目ID查询table及详细（包含模糊查询）
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="tableName">table名称</param>
        /// <returns></returns>
        //[LogFilter(LogsOperation = 3, TableName = "TableTable")]
        public JsonResult GetTableTable(string projectId, string tableName)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                return Json(new { Rows = "" });
            }
            var Result = (from a in _pDBaseContext.Set<TableTable>().Where(p => p.ProjectId == projectId)
                          join b in _pDBaseContext.Set<TableField>() on a.TableId equals b.TableId into joing
                          from b in joing.DefaultIfEmpty()
                          select new { a.TableId, a.TableName, b }).ToList().GroupBy(p => new { p.TableId, p.TableName }).Select(q => new { TableId = q.Key.TableId, TableName = q.Key.TableName, data = q.ToList() });
            if (!string.IsNullOrEmpty(tableName))
            {
                Result = Result.Where(p => p.TableName.Contains(tableName));
            }
            return Json(new { Rows = Result });
        }


        [LogFilter(LogsOperation = 1, TableName = "TableTable")]
        public JsonResult DeleteTableTable(string idlist)
        {
            string[] str = idlist.Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in str)
            {
                //删除table
                var data = _pDBaseContext.TableTable.Where(p => p.TableId == item).FirstOrDefault();
                _pDBaseContext.TableTable.Remove(data);
                //删除table里字段
                var dataField = _pDBaseContext.TableField.Where(p => p.TableId == item).ToList();
                if (dataField.Count > 0)
                {
                    foreach (var item1 in dataField)
                    {
                        _pDBaseContext.TableField.Remove(item1);
                    }
                }

            }
            _pDBaseContext.SaveChanges();
            return Json(new { success = 1, message = "OK" });
        }
        [LogFilter(LogsOperation = 0, TableName = "TableTable")]
        public JsonResult AddTableTable(string tableName, string projectId)
        {
            string Name = User.FindFirstValue(ClaimTypes.Name);
            _pDBaseContext.TableTable.Add(new TableTable { TableId = Guid.NewGuid().ToString("N"), TableName = tableName, ProjectId = projectId, State = 1, Creater = Name, CreateTime = DateTime.Now });
            _pDBaseContext.SaveChanges();
            return Json(new { success = 1, message = "OK" });
        }

        public JsonResult CreateSqls(List<string> tableIds)
        {
            string sqlText = @"";
            var Result = (from a in _pDBaseContext.Set<TableTable>()
                          join b in _pDBaseContext.Set<TableField>() on a.TableId equals b.TableId into joing
                          from b in joing.DefaultIfEmpty()
                          select new { a.TableId, a.TableName, b }).ToList().Where(p => tableIds.Contains(p.TableId))
            .GroupBy(p => new { p.TableId, p.TableName }).Select(q => new { TableId = q.Key.TableId, TableName = q.Key.TableName, data = q.ToList() });
            foreach (var item in Result)
            {
                string sqlTextItem = @"";
                var List = new List<TableField>();
                //要把所有TableField取出来
                foreach (var item1 in item.data)
                {
                    if (item1.b != null)
                    {
                        List.Add(item1.b);
                    }
                }
                int Count = List.Where(p => p.FieldlKey == "1").Count();
                if (Count < 1)
                {
                    return Json(new { success = 1, message = $"{item.TableName}未设置主键" });
                }

                sqlTextItem += CreateSql(item.TableName, List);
                sqlText += sqlTextItem + "\r\n";
            }
            return Json(new { success = 1, message = sqlText });
        }
        /// <summary>
        /// 生成SQL语句
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="list">表字段</param>
        /// <returns></returns>
        private string CreateSql(string name, List<TableField> list)
        {
            string Result = @"CREATE TABLE " + name + "(" + "\r\n";
            string Filed = @"";
            string Key = @"";
            string Defult = @"";
            string Explain = @"";
            //遍历字段     
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item.FieldName))
                {
                    string Type = GetType(item);
                    string Isnull = item.FieldIsNull == "1" ? " NULL" : "NOT NULL";
                    Filed += item.FieldName + "  " + Type +"   "+ Isnull + "," + "\r\n";
                }

                if (item.FieldlKey == "1")
                {
                    string Str = "PK_" + name;
                    Key = " CONSTRAINT " + Str + " PRIMARY KEY CLUSTERED (" + item.FieldName + ")" + "\r\n";
                }
                if (!string.IsNullOrWhiteSpace(item.FieldDefultValue))
                {
                    string Str = name + "_" + item.FieldName;
                    Defult += "ALTER TABLE " + name + " ADD  CONSTRAINT " + Str + " DEFAULT ('" + item.FieldDefultValue + "') FOR " + item.FieldName + "" + "\r\n";
                    Defult += "GO" + "\r\n";
                }
                if (!string.IsNullOrWhiteSpace(item.FieldExplain))
                {
                    Explain += "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + item.FieldExplain + "' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + name + "', @level2type=N'COLUMN',@level2name=N'" + item.FieldName + "'" + "\r\n";
                    Explain += "GO" + "\r\n";
                }
            }
            Result += Filed + Key + ")" + "\r\n" + Defult + "\r\n" + Explain;

            return Result;
        }
        
        private string GetType(TableField tableField)
        {
            switch (tableField.FieldType)
            {
                case "varchar":
                    return tableField.FieldType + "(" + tableField.FieldLength + ")";
                case "nvarchar":
                    return tableField.FieldType + "(" + tableField.FieldLength + ")";
                case "int":
                    return tableField.FieldType;
                case "datetime":
                    return tableField.FieldType;
                case "blob":
                    return tableField.FieldType;
                case "tinyint":
                    return tableField.FieldType;
                case "text":
                    return tableField.FieldType;
                default:
                    return "varchar(50)";
            }

        }


    }
}
