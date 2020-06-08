using Microsoft.Extensions.DependencyInjection;
using Pd.Core.DAL;
using Pd.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.Builder
{
    public  class MessageBuilder
    {
        public static IServiceCollection _services { get; set; }
        public   MessageBuilder(IServiceCollection service)
        {
            _services = service;
        } 
        public  void Tengxun()
        {
            _services.AddSingleton<IMessage, Message>();
        }
    }
}
