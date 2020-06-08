using Pd.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace Pd.Core.Com
{
    public class ReponseHelper
    {
        /// <summary>
        /// 统一返回类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageModel"></param>
        /// <param name="Content"></param>
        /// <param name="State"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string ReponseParam<T>( T Content, PageModel pageModel = null, int State = 1, string Message = "Success")
        {

            if (pageModel == null)
            {
                pageModel = new PageModel { PageIndex = 1, PageSize = 10, PageTotal = 10 };
            }
            return JsonConvert.SerializeObject(new ReponseModel<T>()
            {
                State = State,
                Message = Message,
                pageModel = pageModel,
                Content = Content

            });
        }
    }
}
