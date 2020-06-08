using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.Models
{
    public class ReponseModel<T> 
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 页
        /// </summary>
        public PageModel pageModel { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
    }
}
