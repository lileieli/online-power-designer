using Pd.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.DAL
{
    public class Message : IMessage
    {
        public string QQ()
        {
            return "QQ";
        }

        public string Weixin()
        {
            return "Weixin";
        }
    }
}
