using Gameplay.UI.Buildings.CannonTower;
using System;
using UI.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Buildings.CannonTower.UI
{
    public class UICannonTowerScreenView : UIScreenView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _infoButton;


        [SerializeField] private InfoPanel _infoPanel;
        public IObservable<Unit> OnCloseClick => _closeButton.OnClickAsObservable();
        public IObservable<Unit> OnInfoClick => _infoButton.OnClickAsObservable();

        public InfoPanel InfoPanel => _infoPanel;
    }
}
