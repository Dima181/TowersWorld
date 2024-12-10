using System.Runtime.Serialization;
using System;
using UniRx;
using UnityEngine;

namespace Gameplay.Acceleration
{
    [Serializable]
    public class BaseProgressTaskModel
    {
        [NonSerialized] public ReactiveCommand OnTicked = new();
        [NonSerialized] public ReactiveCommand<bool> OnFinished = new();
        [NonSerialized] public ReactiveCommand OnAccelerated = new();
        [NonSerialized] public ReactiveCommand OnDurationChanged = new();

        public DateTime StartTime => _startTime;
        public DateTime EndTime => _endTime;
        public bool IsConfigured => _isConfigured;
        public bool IsFinished { get; private set; }
        public bool? IsSuccessFinished { get; private set; }
        public string AccelerationType => _accelerationType;

        [SerializeField] private DateTime _startTime;
        [SerializeField] private DateTime _endTime;
        [SerializeField] private bool _isConfigured;
        [SerializeField] private string _accelerationType;

        [OnDeserialized]
        public virtual void OnDeserialized()
        {
            OnTicked = new();
            OnFinished = new();
            OnAccelerated = new();
            OnDurationChanged = new();
        }

        public void SetAccelerationType(string accelerationTypes)
        {
            _accelerationType = accelerationTypes;
        }

        public void SetupExecutionTime(DateTime startTime, DateTime endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
            _isConfigured = true;
        }

        public void AccelerateProgress(TimeSpan accelerationTimeSpan)
        {
            _endTime -= accelerationTimeSpan;
            _startTime -= accelerationTimeSpan;
            OnAccelerated.Execute();
        }

        public void ChangeDuration(TimeSpan durationValue)
        {
            _endTime += durationValue;
            OnDurationChanged.Execute();
        }

        public void Tick()
        {
            if (IsFinished)
                return;

            OnTicked.Execute();
        }

        public void FinishTask(bool result)
        {
            if (IsFinished)
                return;

            IsFinished = true;
            IsSuccessFinished = result;
            OnFinished.Execute(result);
        }
    }
}
