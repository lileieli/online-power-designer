using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Pd.Core.Areas.Pd.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pd.Core.Helper.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        // 1. 需要实现一个构造函数,参数为 RequestDelegate
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // 2. 需要实现一个叫做 InvokeAsync 方法
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleError(context, ex);

            }
        }
        private async Task HandleError(HttpContext context, Exception ex)
        {
            LogHelper.WriteLogFile("错误", ex);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            context.Response.Redirect("/Home/Error");
            await context.Response.WriteAsync("抱歉，服务端出错了");

        }
    }
}
