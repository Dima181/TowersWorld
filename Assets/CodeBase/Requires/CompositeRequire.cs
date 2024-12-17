using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Requires
{
    public class CompositeRequire : IRequire
    {
        public IObservable<bool> Ready => _requires.Select(r => r.Ready).CombineLatestValuesAreAllTrue();

        public IReadOnlyList<IRequire> Requires => _requires;

        private List<IRequire> _requires;

        public CompositeRequire(IEnumerable<IRequire> requires)
        {
            _requires = new(requires);
        }

        public CompositeRequire() => 
            _requires = new();

        public IDisposable Accept(IRequireVisitor visitor, Transform container) => 
            visitor.Visit(_requires, container);

        public void AddRange(IEnumerable<IRequire> items) => 
            _requires.AddRange(items);

        public void Add(IRequire item) =>
            ((ICollection<IRequire>)_requires).Add(item);

        public void Remove(IRequire item) =>
            ((ICollection<IRequire>)_requires).Remove(item);
    }
}