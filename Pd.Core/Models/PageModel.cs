using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.Models
{
    public class PageModel
    {
        /// <summary>
        /// 显示数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 显示索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总数据
        /// </summary>
        public int PageTotal { get; set; }
    }
}
