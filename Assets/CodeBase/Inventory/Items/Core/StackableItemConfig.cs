namespace Inventory.Items.Core
{
    public abstract class StackableItemConfig : ItemConfigBase
    {
        public abstract StackableItemModel CreateItem(int count); 
    }
}