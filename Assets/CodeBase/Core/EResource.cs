using UnityEngine;

namespace Core
{
    public enum EResource
    {
        [HideInInspector]
        None = 0,
        Iron = 1,
        Meat = 2,
        Gems = 3
    }

    public static class ResourceExt
    {
        public static bool IsProtectable(this EResource resource) =>
            resource is
                EResource.Iron or
                EResource.Meat;
    }
}
