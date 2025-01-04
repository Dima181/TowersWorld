using Inventory.Items.Core;

namespace Inventory.Items
{
    public class CurrencyItemModel : StackableItemModel
    {
        public CurrencyItemModel(CurrencyItemConfig config, int count = 0) : base(config, count)
        {
        }
    }
}
