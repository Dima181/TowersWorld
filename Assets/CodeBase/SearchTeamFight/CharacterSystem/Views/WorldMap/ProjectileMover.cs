using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class ProjectileMover : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _startPosition;
        private float _flightDuration;
        private float _currentFlightTime;
        private float _finishDelayTime;
        private bool _isLookAtTarget;

        private IDisposable _fixedUpdateSubscription;
        private CancellationToken _cancellationToken;


        public event OnPositionUpdatedDelegate OnPositionUpdated;
        public IObservable<Vector3> OnTargetReaced => _onTargetReaced;
        public IObservable<ProjectileMover> OnFinish => _onFinish;


        public delegate Vector3 OnPositionUpdatedDelegate(Vector3 position, Vector3 startPosition, float flightProgress);
        private Subject<Vector3> _onTargetReaced = new();
        private Subject<ProjectileMover> _onFinish;

        public void Initialize(
            Transform target, 
            float speed, 
            float disableDelayTime, 
            bool lookAtTarget,
            CancellationToken cancellationToken) 
        {
            _target = target;
            _finishDelayTime = disableDelayTime;
            _isLookAtTarget = lookAtTarget;
            _startPosition = transform.position;
            _flightDuration = Vector3.Distance(_startPosition, target.position) / speed;
            _currentFlightTime = 0f;

            _cancellationToken = cancellationToken;

            _fixedUpdateSubscription = this.FixedUpdateAsObservable()
                .Subscribe(_ => UpdateProjectile())
                .AddTo(this);
        }

        private void UpdateProjectile()
        {
            if (_target == null)
            {
                _onFinish.OnNext(this);
                return;
            }

            _currentFlightTime += Time.fixedDeltaTime + Time.timeScale;
            var flightProgress = _currentFlightTime / _flightDuration;

            var targetPosition = _target.position;

            Vector3 newPosition = Vector3.Lerp(_startPosition, targetPosition, flightProgress);

            if(OnPositionUpdated != null)
                newPosition = OnPositionUpdated.Invoke(newPosition, _startPosition, flightProgress);

            if (_isLookAtTarget)
                transform.LookAt(targetPosition);

            transform.position = newPosition;

            if (flightProgress >= 0.95f)
            {
                _fixedUpdateSubscription.Dispose();

                _onTargetReaced?.OnNext(transform.position);

                DelayedOnFinish().Forget();
            }
        }

        private async UniTaskVoid DelayedOnFinish()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_finishDelayTime), cancellationToken: _cancellationToken);
            _onFinish?.OnNext(this);
        }
    }
}
