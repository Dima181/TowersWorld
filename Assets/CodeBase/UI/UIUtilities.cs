namespace UI
{
    public static class UIUtilities
    {
        public static string ToNiceString(this int number)
        {
            switch (number)
            {
                case < 1000:
                    return number.ToString();

                case >= 1000 and < 1000000:
                    var roundedToThousands = number / 1000;
                    return roundedToThousands.ToString() + "K";

                case >= 1000000 and < 1000000000:
                    var roundedToMillions = number / 1000000;
                    return roundedToMillions.ToString() + "M";

                case >= 1000000000:
                    var roundedToBillions = number / 1000000000;
                    return roundedToBillions.ToString() + "B";

                default:
                    return number.ToString();
            }
        }
    }
}
