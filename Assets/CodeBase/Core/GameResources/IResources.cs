using Core;
using Inventory.Items.ResourceItems;
using UniRx;

namespace Core.GameResources
{
    public interface IResources
    {
        IReadOnlyReactiveProperty<int> Get(EResource res);

        int Amount(EResource res);

        void Add(EResource res, int amount, bool needSave = true);

        bool TrySpend(EResource res, int amount, bool needSave = true);

        void UnpackResourceItem(ResourceItemModel resourceItem, int amount);
    }
}
