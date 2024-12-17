using System;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;
using Zenject;

namespace Extensions
{
    public static class ZenjectExtensions
    {
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder FromRootComponent<T>(
            this ConcreteBinderGeneric<T> binder, MonoBehaviour targer) =>
            binder.FromInstance(targer.GetComponent<T>());

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder FromChildComponent<T>(
            this ConcreteBinderGeneric<T> binder, MonoBehaviour target) =>
            binder.FromInstance(target.GetComponentInChildren<T>());

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder FromChildComponent<T>(
            this FromBinderNonGeneric binder, MonoBehaviour target) =>
            binder.FromInstance(target.GetComponentInChildren<T>());

        public static Type[] GetArgumentsOfInheritedOpenGenericClass(this Type type, Type openGenericType)
        {
            var currentType = type;
            while(currentType.BaseType != null)
            {
                currentType = currentType.BaseType;
                if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == openGenericType)
                    return currentType.GetGenericArguments();
            }

            return new Type[0];
        }

        public static string SplitPascalCase(this string str)
        {
            StringBuilder builder = new(str.Length);

            builder.Append(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                if(char.IsUpper(str[i]) && !char.IsUpper(str[i - 1]))
                    builder.Append(" ");
            }

            return builder.ToString();
        }
    }
}
