using System;
using System.Collections.Generic;

namespace Pd.Core.Areas.Pd.Models.BaseModel
{
    public partial class Project
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? State { get; set; }
        public string Remake { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateYear { get; set; }
    }
}
