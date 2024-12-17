using System;

namespace Currencies
{
    public static class CurrencyExtension
    {
        public static int GetSkipHardCost(this TimeSpan duration) =>
            (int) (duration.TotalSeconds / 3);
    }
}
