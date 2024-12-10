using System.Collections.Generic;
using UniRx;

namespace Gameplay.Model
{
    public class BuildingStateHandler<THandler> : IBuildingInitializable, IBuildingDisposable
        where THandler : IBuildingStateHandler
    {
        private readonly BuildingModel _model;
        private readonly EBuildingState _handleState;
        private readonly List<THandler> _handlers;

        private CompositeDisposable _disposables;
        private bool _isActive;

        public BuildingStateHandler(
            BuildingModel model, 
            EBuildingState handleState, 
            List<THandler> handlers)
        {
            _model = model;
            _handleState = handleState;
            _handlers = handlers;
        }

        public void Initialize()
        {
            _disposables = new();
            _model.State
                .SkipLatestValueOnSubscribe()
                .Select(state => state == _handleState)
                .DistinctUntilChanged()
                .Subscribe(isActive =>
                {
                    _isActive = isActive;
                    if (_isActive)
                    {
                        foreach (var handler in _handlers)
                            handler?.OnEnterState();
                    }
                    else
                    {
                        foreach (var handler in _handlers)
                            handler?.OnExitState();
                    }

                })
                .AddTo(_disposables);

            if(_handleState == _model.State.Value)
            {
                foreach (var handler in _handlers)
                    handler?.OnEnterState();
            }
        }

        public void Dispose()
        {
            if (_isActive)
            {
                foreach (var handler in _handlers)
                    handler?.OnExitState();
            }
            _disposables.Dispose();
        }
    }
}
