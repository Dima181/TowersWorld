using Core;
using UniRx;

namespace Core.GameResources
{
    public interface IProtectedResource
    {
        IReadOnlyReactiveProperty<int> GetProtectedResource(EResource res);

        bool TryProtect(EResource res, int amount, bool needSave = true);
        bool TrySpendUnprotected(EResource res, int amount, bool needSave = true);
    }
}
