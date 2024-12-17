using System;

namespace Infrastructure.TimeHelper
{
    public enum ETimeFormat
    {
        Sec,
        Min,
        Hours,
        Days,
    }

    public class TimeHelper
    {
        public string TimeSpanToString(TimeSpan time, bool minimizeDigits = false, ETimeFormat format = ETimeFormat.Min)
        {
            string df = @"%d'd. 'hh\:mm\:ss";
            string hf = minimizeDigits ? @"h\:mm\:ss" : @"hh\:mm\:ss";
            string mf = minimizeDigits ? @"m\:ss" : @"mm\:ss";

            string f;

            if (time.TotalHours >= 24)
            {
                f = df;
            }
            else if (time.TotalMinutes >= 60)
            {
                f = hf;
            }
            else
            {
                f = mf;
            }

            //string f = time.TotalMinutes >= 60 ? hf : mf;

            return time.ToString(f);
        }
    }
}
