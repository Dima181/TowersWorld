using CodeBase.Core;

namespace Inventory.Items.Core
{
    public class StackableItemModel : ItemModelBase
    {
        public override ItemConfigBase BaseConfig { get; }
        public StackableItemConfig StacableConfig { get; }

        public int Count { get; set; }

        public StackableItemModel(StackableItemConfig config, int count = 0)
        {
            BaseConfig = config;
            StacableConfig = config;
            Count = count;
        }
    }
}