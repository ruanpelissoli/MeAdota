using Adopcat.Mobile.Util;
using System;

namespace Adopcat.Mobile.Models
{
    public class SystemLog
    {
        public string Text { get; set; }
        public ELogType LogType { get; set; }
        public EPlatform Platform { get; set; }
        public DateTime LogDate { get; set; }
    }
}
