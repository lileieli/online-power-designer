using System;
using System.Collections.Generic;

namespace Pd.Core.Areas.Pd.Models.BaseModel
{
    public partial class CommonType
    {
        public string CommonId { get; set; }
        public string CommonName { get; set; }
        public string CommonBrlong { get; set; }
        public string CommonValue { get; set; }
        public string Remake { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? State { get; set; }
    }
}
