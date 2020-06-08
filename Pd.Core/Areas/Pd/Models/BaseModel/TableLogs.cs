using System;
using System.Collections.Generic;

namespace Pd.Core.Areas.Pd.Models.BaseModel
{
    public partial class TableLogs
    {
        public string LogsId { get; set; }
        public string OperateId { get; set; }
        public string MendName { get; set; }
        public string IngOrEd { get; set; }
        public string TableName { get; set; }
        public string Param { get; set; }
        public string LogsImport { get; set; }
        public int? LogsOperation { get; set; }
        public int? State { get; set; }
        public string Remake { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
