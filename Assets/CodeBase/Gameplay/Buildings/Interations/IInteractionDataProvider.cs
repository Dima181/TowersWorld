using System.Collections.Generic;

namespace Gameplay.Buildings.Interations
{
    public interface IInteractionDataProvider
    {
        IReadOnlyList<IInteractionData> InteractionData { get; }
    }
}
