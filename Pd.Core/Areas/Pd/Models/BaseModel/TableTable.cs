using System;
using System.Collections.Generic;

namespace Pd.Core.Areas.Pd.Models.BaseModel
{
    public partial class TableTable
    {
        public string TableId { get; set; }
        public string TableName { get; set; }
        public string TableExplain { get; set; }
        public string TableRemake { get; set; }
        public string TableSortOut { get; set; }
        public string ProjectId { get; set; }
        public int? State { get; set; }
        public string Remake { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
