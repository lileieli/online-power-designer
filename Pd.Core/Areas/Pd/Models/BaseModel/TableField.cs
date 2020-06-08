using System;
using System.Collections.Generic;

namespace Pd.Core.Areas.Pd.Models.BaseModel
{
    public partial class TableField
    {
        public string FieldId { get; set; }
        public string  FieldlKey { get; set; }
        public string FieldName { get; set; }
        public string FieldExplain { get; set; }
        public string FieldType { get; set; }
        public string FieldLength { get; set; }
        public string FieldDefultValue { get; set; }
        public string  FieldIsNull { get; set; }
        public string TableId { get; set; }
        public string FieldContentId { get; set; }
        public int? State { get; set; }
        public string Remake { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
        
    }
}
