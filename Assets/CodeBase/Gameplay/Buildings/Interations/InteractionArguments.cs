using Gameplay.Model;
using Gameplay.View;
using System.Collections.Generic;

namespace Gameplay.Buildings.Interations
{
    public struct InteractionArguments
    {
        public readonly BuildingModel Model;
        public readonly BuildingView View;
        public readonly IReadOnlyList<IInteractionData> ActionOptions;

        public InteractionArguments(
            BuildingModel model, 
            BuildingView view, 
            IReadOnlyList<IInteractionData> actionOptions)
        {
            Model = model;
            View = view;
            ActionOptions = actionOptions;
        }
    }
}
