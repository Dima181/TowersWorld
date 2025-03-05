using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public static class TowersWorldGameExtentions
    {
        public static Vector2 xz(this Vector3 v) => new Vector2(v.x, v.z);

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if(source == null) 
                new ArgumentNullException(nameof(source));

            if(action == null)
                new ArgumentNullException(nameof(action));

            int index = 0;

            foreach(T element in source)
            {
                action(element, index);
                index++;
            }
        }
    }
}
