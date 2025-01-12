using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Infrastructure.Pipeline.Services
{
    public interface ILocalService : IDisposable
    {
        Type ServiceType { get; }
        UniTask Initialize(DiContainer di);
    }

    public interface ILocalService<T> : ILocalService
    {
        Type ILocalService.ServiceType => typeof(T);
    }
}
