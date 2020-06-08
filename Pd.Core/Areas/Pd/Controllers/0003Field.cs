using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Areas.Pd.Models.BaseModel;
using Pd.Core.Helper.Filter;
using Pd.Core.Models;

namespace Pd.Core.Areas.Pd.Controllers
{
    public partial class TableController : BaseController
    {
        [Authorize]
        public IActionResult Field(string tableId)
        {
            ViewData["tableId"] = tableId;
            ViewData["Result"] = GetTableByld(tableId);
            ViewData["CommonType"] = CommonType(new List<string> { "TrueOrFalse", "FiledType" });
            return View();
        }
        public List<TableField> GetTableByld(string tableId)
        {
            var Result = _pDBaseContext.TableField.Where(p => p.TableId == tableId).ToList();
            return Result;
        }
        [LogFilter(LogsOperation = 1, TableName = "TableField")]
        public JsonResult DeleteField(string field)
        {
            var data = _pDBaseContext.TableField.Where(p => p.FieldId == field).FirstOrDefault();
            if(data==null)
                return Json(new { success = 1, message = "OK" });
            _pDBaseContext.TableField.Remove(data);
            _pDBaseContext.SaveChanges();
            return Json(new { success = 1, message = "OK" });
        }
        [LogFilter(LogsOperation = 3, TableName = "TableField")]
        public JsonResult ModifyField(TableField tableField)
        {
          var  field=  _pDBaseContext.TableField.Where(p => p.FieldId == tableField.FieldId).FirstOrDefault();
            field.FieldlKey = tableField.FieldlKey;
            field.FieldName = tableField.FieldName;
            field.FieldExplain = tableField.FieldExplain;
            field.FieldType = tableField.FieldType;
            field.FieldLength = tableField.FieldLength;
            field.FieldDefultValue = tableField.FieldDefultValue;
            field.FieldIsNull = tableField.FieldIsNull;
            field.FieldContentId = tableField.FieldContentId;
            _pDBaseContext.SaveChanges();
            return Json(new { success = 1, message = "OK" });
        }
        [LogFilter(LogsOperation = 0, TableName = "TableField")]
        public JsonResult AddField(TableField filedModel)
        {
            if(filedModel.FieldlKey=="1")
            {
                filedModel.FieldIsNull = "0";
            }
            string Name = User.FindFirstValue(ClaimTypes.Name);
            filedModel.FieldId = Guid.NewGuid().ToString("N");
            filedModel.State = 1;
            filedModel.CreateTime = DateTime.Now;
            filedModel.Creater = Name;
            _pDBaseContext.TableField.Add(filedModel);
            _pDBaseContext.SaveChanges();
            return Json(new { success = 1, message = "OK" });
        }
    }
}
