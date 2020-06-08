using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.Models
{
    public class RequestModel
    {
        /// <summary>
        /// 一页几行
        /// </summary>
        public int limit { get; set; } = 10;
        /// <summary>
        /// 第几页
        /// </summary>
        public int offset { get; set; } = 0;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string search { get; set; } ="";
    }
}
