using System.Collections.Generic;

namespace Extensions
{
    public static class CollectionsExtensions
    {
        public static bool InBounds<T>(
            this int index, 
            ICollection<T> collection)
        {
            if(collection == null)
                return false;

            return index >= 0 && index < collection.Count;
        }
    }
}
