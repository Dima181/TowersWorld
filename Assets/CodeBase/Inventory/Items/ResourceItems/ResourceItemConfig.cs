using Core;
using Inventory.Items.Core;
using UnityEngine;

namespace Inventory.Items.ResourceItems
{
    [CreateAssetMenu(menuName = "Inventory/Resource item config")]
    public class ResourceItemConfig : StackableItemConfig
    {
        [SerializeField] private EResource _resourceType;
        [SerializeField] private int _resourceValue;

        public EResource ResourceType  => _resourceType;
        public int ResourceValue => _resourceValue;

        public override StackableItemModel CreateItem(int count) => 
            new ResourceItemModel(this, count);
    }
}