using Microsoft.Extensions.Configuration;
using Pd.Core.Areas.Pd.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.Helper
{
    public class Setting
    {
        public static SysSetting APPSett;
        public void Initial()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            APPSett = new SysSetting
            {
                Sqlconn = configuration["ConnectionStrings:Sqlconn"],
                Address = configuration["ConnectionStrings:Address"],
            };
        }
    }

    /// <summary>
    /// 系统设置
    /// </summary>
    public class SysSetting
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Sqlconn { get; set; }
        /// <summary>
        /// 日志路径
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 是否打开缓存1是0否
        /// </summary>
        public string IsOpenCache { get; set; } 
    }
}
