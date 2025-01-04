using AYellowpaper.SerializedCollections;
using Inventory.Items.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Items
{
    [Serializable]
    public class ItemsCollectionBase<TEnum, TConfig>
        where TEnum : Enum
        where TConfig : ItemConfigBase
    {
        [SerializeField] private SerializedDictionary<TEnum, List<TConfig>> _items;

        public IReadOnlyCollection<TConfig> GetItemsTypes(TEnum enumType) =>
            _items.TryGetValue(enumType, out var items) ? items : new List<TConfig>();

        public IEnumerable<TConfig> GetAllItems() =>
            _items.Values.SelectMany(items => items);

        public TConfig GetRandom()
        {
            var allItems = GetAllItems().ToArray();

            if(allItems.Length == 0)
            {
                throw new InvalidOperationException("Collection is empty");
            }

            return allItems[UnityEngine.Random.Range(0, allItems.Length)];
        }
    }
}
