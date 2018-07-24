using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming_Tool
{
    public static class Extensions
    {
        public static string ClockDisplay(this TimeSpan span)
        {
            int minutes = span.Minutes;
            int seconds = span.Seconds;
            minutes = minutes + (span.Hours * 60);
            string minutesStr = minutes.ToString("D2");
            string secondsStr = seconds.ToString("D2");
            return minutesStr + ":" + secondsStr;
        }
    }
}
