using Inventory.Items;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Database")]
    public class InventoryItems : ScriptableObject
    {
        public CurrencyItemConfig Gem => _gem;

        [SerializeField] private CurrencyItemConfig _gem;
    }
}
