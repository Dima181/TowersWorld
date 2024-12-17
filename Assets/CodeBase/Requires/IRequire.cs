using System;
using UnityEngine;

namespace Requires
{
    public interface IRequire
    {
        IObservable<bool> Ready { get; }

        IDisposable Accept(IRequireVisitor visitor, Transform container);
    }
}