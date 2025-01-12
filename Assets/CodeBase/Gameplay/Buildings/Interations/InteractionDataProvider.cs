using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Buildings.Interations
{
    [RequireComponent(typeof(InteractionsInstaller))]
    public sealed class InteractionDataProvider : MonoBehaviour, IInteractionDataProvider
    {
        public IReadOnlyList<IInteractionData> InteractionData => _interactionData;

        [SerializeReference]
        [SubclassSelector]
        private IInteractionData[] _interactionData = Array.Empty<IInteractionData>();
    }
}