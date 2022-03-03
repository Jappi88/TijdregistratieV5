using System;
using Rpm.Misc;
using System.Collections.Generic;
using Polenter.Serialization;
using ProductieManager.Rpm.Misc;
using Rpm.SqlLite;

namespace ProductieManager.Rpm.SqlLite
{
    public class IDbLogging
    {
        public List<LogEntry> Logs { get; set; } = new List<LogEntry>();
    }
}
