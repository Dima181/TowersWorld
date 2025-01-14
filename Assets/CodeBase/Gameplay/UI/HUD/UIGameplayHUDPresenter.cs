using UI.Core;
using Cysharp.Threading.Tasks;
using System;
using UI.GameScreens.ScreenHud;
using UniRx;
using Zenject;
using Inventory;
using System.ComponentModel.Design;
using Core.GameResources;
using Core;
using UnityEngine;

namespace Gameplay.UI.HUD
{
    public class UIGameplayHUDPresenter : UIScreenPresenter<UIGameplayHUDView>
    {
        [Inject] private InventoryModel _inventoryModel;
        [Inject] private ResourceService _resources;
        [Inject] private readonly ResourceService _resourceService;

        public IObservable<EHudButton> OnButtonClick => _view.OnHudButtonClick;        

        protected override UniTask BeforeShow(CompositeDisposable disposables)
        {
            SetResourceText();

            /*_inventoryModel.OnItemCountChanged.Subscribe(_ =>
            {
                SetResourceText();
            }).AddTo(disposables);*/

            _resources.ChangedRespurcesCount
                .Subscribe(_ =>
                {
                    SetResourceText();
                }).AddTo(disposables);
            return Complited;
        }

        private void SetResourceText()
        {
            _view.SetValueResourcesText(_resourceService.Amount(EResource.Gems), _resourceService.Amount(EResource.Meat), _resourceService.Amount(EResource.Iron));
        }
    }
}
