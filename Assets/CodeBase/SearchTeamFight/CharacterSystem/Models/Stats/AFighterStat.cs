using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;

namespace SearchTeamFight.CharacterSystem.Models.Stats
{
    public abstract class AFighterStat<T>
        where T : struct
    {
        public IReadOnlyReactiveProperty<T> BaseValue => _baseValue;
        public IReadOnlyReactiveProperty<T> Value => _baseValue.CombineLatest(_multiplier, Mul).ToReadOnlyReactiveProperty();


        protected IReadOnlyReactiveProperty<T> Multiplier => _multiplier;


        private readonly ReactiveProperty<T> _baseValue;
        private readonly ReactiveProperty<T> _multiplier;

        protected AFighterStat(T baseValue, T multiplier)
        {
            _baseValue = new ReactiveProperty<T>(baseValue);
            _multiplier = new ReactiveProperty<T>(multiplier);
        }

        public async UniTaskVoid BuffMul(T value, TimeSpan duration, CancellationToken cancellationToken)
        {
            _multiplier.Value = Add(_multiplier.Value, value);
            await UniTask.Delay(duration, cancellationToken: cancellationToken);
            _multiplier.Value = Add(_multiplier.Value, Neg(value));
        }

        public void BuffMul(T value)
            => _multiplier.Value = Add(_multiplier.Value, value);

        protected abstract T Add(T a, T b);
        protected abstract T Mul(T a, T b);
        protected abstract T Neg(T value);
    }
}
