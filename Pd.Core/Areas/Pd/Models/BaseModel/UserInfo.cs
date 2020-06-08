using System;
using System.Collections.Generic;

namespace Pd.Core.Areas.Pd.Models.BaseModel
{
    public partial class UserInfo
    {
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public int? UserState { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string Remake { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
