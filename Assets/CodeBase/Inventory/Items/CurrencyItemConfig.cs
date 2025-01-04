using Inventory.Items.Core;
using UnityEngine;

namespace Inventory.Items
{
    [CreateAssetMenu(menuName = "Inventory/Currency")]
    public class CurrencyItemConfig : StackableItemConfig
    {
        public override StackableItemModel CreateItem(int count)
        {
            return new CurrencyItemModel(this, count);
        }
    }
}
