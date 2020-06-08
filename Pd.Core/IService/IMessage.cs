using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pd.Core.IService
{
    public interface IMessage
    {
        String Weixin();
        String QQ();
    }
}
