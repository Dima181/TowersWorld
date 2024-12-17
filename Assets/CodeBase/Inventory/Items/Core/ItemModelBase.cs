using UnityEngine;

namespace Inventory.Items.Core
{
    public abstract class ItemModelBase
    {
        public string Name => BaseConfig.Name;
        public string Description => BaseConfig.Description;
        public Sprite Icon => BaseConfig.Icon;
        public EInventoryTab Tab => BaseConfig.Tab;

        public abstract ItemConfigBase BaseConfig { get; }
    }
}