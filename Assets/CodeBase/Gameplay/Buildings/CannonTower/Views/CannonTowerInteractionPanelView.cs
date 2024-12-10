using System;
using UI.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.CannonTower.Views
{
    public class CannonTowerInteractionPanelView : UIScreenView
    {
        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable();
        public IObservable<Unit> OnDescriptionClicked => _description.OnClickAsObservable();
        public IObservable<Unit> OnInformationClicked => _information.OnClickAsObservable();

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _description;
        [SerializeField] private Button _information;

    }
}
