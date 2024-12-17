using CodeBase.Core;
using Inventory.Items.Core;

namespace Inventory.Items.ResourceItems
{
    public class ResourceItemModel : StackableItemModel
    {
        public EResource ResourceType => _config.ResourceType;
        public int ResourceValue => _config.ResourceValue;

        private readonly ResourceItemConfig _config;
        public ResourceItemModel(ResourceItemConfig config, int count = 0) : base(config, count)
        {
            _config = config;
        }
    }
}