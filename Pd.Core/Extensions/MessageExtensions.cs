using Microsoft.Extensions.DependencyInjection;
using Pd.Core.DAL;
using Pd.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pd.Core.Builder;

namespace Pd.Core.Extensions
{
    public static class MessageExtensions
    {
        /// <summary>
        /// 封装服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configer"></param>
        public static void AddMessage(this IServiceCollection services,Action<MessageBuilder> configer)
        {
            var builder = new MessageBuilder(services);
            configer(builder);
        }
    }
}
