using System;
using System.Collections.Generic;
using UnityEngine;

namespace Requires
{
    public interface IRequireVisitor
    {
        IDisposable Visit(BuildingRequire buildingRequire, Transform container);
        IDisposable Visit(ResourceRequire resourceRequire, Transform container);
        IDisposable Visit(IEnumerable<IRequire> requires, Transform container);
    }
}