using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace CodeBase.Core
{
    public static class GameExtentions
    {
        public static void DestroyAndClear<T>(this ICollection<T> items)
            where T : MonoBehaviour
        {
            foreach (T item in items)
                Object.Destroy(item.gameObject);

            items.Clear();
        }

        public static Color WithAlpha(this Color color, float alpha) =>
            new Color(color.r, color.g, color.b, alpha);

        public static string ToDisplayedString(this int num)
        {
            var numFormat = new CultureInfo(CultureInfo.CurrentCulture.Name).NumberFormat;

            numFormat.NumberDecimalDigits = 0;
            numFormat.NumberGroupSeparator = "n";
            numFormat.NumberGroupSizes = new[] { 3 };

            return num.ToString(numFormat);
        }

        public static void SetTextFormated(this TextMeshProUGUI uiText, int arg) =>
            uiText.text = arg.ToDisplayedString();
    }
}
