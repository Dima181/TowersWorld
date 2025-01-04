using Inventory.Items.Core;
using System.Collections.Generic;
using System;
using UniRx;
using System.Linq;
using Inventory.Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel
    {
        public IReadOnlyCollection<ItemModelBase> Items => _items;
        public IObservable<(StackableItemModel Item, int Count)> OnItemCountChanged => _onItemCountChanged;
        public IObservable<ItemModelBase> OnAddItem => _onAddItem;
        public IObservable<ItemModelBase> OnRemoveItem => _onRemoveItem;

        public InventoryItems Database => _database;

        private ReactiveCollection<ItemModelBase> _items = new();
        private ReactiveCommand<(StackableItemModel Item, int Count)> _onItemCountChanged = new();
        private ReactiveCommand<ItemModelBase> _onAddItem = new();
        private ReactiveCommand<ItemModelBase> _onRemoveItem = new();

        private InventoryItems _database;

        private bool _tempTestResourceMock = true;

        private InventoryModel(InventoryItems database)
        {
            _database = database;

            if (_tempTestResourceMock)
            {
                AddItem(Database.Gem, 10);
                Debug.Log(GetItemCount(Database.Gem));
            }
        }
        
        private void AddItem(StackableItemConfig key, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (count == 0) return;

            foreach (var i in _items)
            {
                if(i.BaseConfig == key)
                {
                    var item = (StackableItemModel)i;
                    item.Count += count;
                    _onItemCountChanged.Execute((item, item.Count));
                }
            }

            var newItem = key.CreateItem(count);
            _items.Add(newItem);
            _onAddItem.Execute(newItem);
            _onItemCountChanged.Execute((newItem, newItem.Count));
        }

        public int GetItemCount(StackableItemConfig key)
        {
            var item = (StackableItemModel)_items.FirstOrDefault(i => i.BaseConfig == key);
            if (item == null)
                return 0;
            return item.Count;
        }
    }
}
