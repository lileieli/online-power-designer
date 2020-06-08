using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.Helper.Attribute
{
    public class LoginAttribute: System.Attribute
    {
        public LoginAttribute(bool _CacheEnable)
        {
            CacheEnable = _CacheEnable;
        }
        /// <summary>
        /// 是否启用
        /// 默认不启用
        /// </summary>
        public bool CacheEnable { get; set; } = false;
    }
}
