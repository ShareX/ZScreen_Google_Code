using System;

namespace ZUploader
{
    public static class FastDateTime
    {
        public static TimeSpan LocalUtcOffset { get; private set; }

        public static DateTime Now
        {
            get { return DateTime.UtcNow + LocalUtcOffset; }
        }

        static FastDateTime()
        {
            LocalUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        }
    }
}