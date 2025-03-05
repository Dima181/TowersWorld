using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Utils;
using Cysharp.Threading.Tasks;
using SearchTeamFight.CharacterSystem.Views;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace SearchTeamFight.CharacterSystem.Views.WorldMap
{
    public class LauncherStaticProjectile
    {
        private ObjectPool<GameObject> _staticProjectilePool;

        private readonly FighterView _fighterView;

        public LauncherStaticProjectile(FighterView fighterView, GameObject projectilePoolGameObject)
        {
            _fighterView = fighterView;

            var parentGameObject = new GameObject(fighterView.name);

            if(projectilePoolGameObject != null)
                parentGameObject.transform.SetParent(projectilePoolGameObject.transform, false);

            _staticProjectilePool = new ObjectPool<GameObject>(
                createFunc: () => Object.Instantiate(fighterView.ProjectilePrefab, fighterView.ShootPoint.position, Quaternion.identity, parentGameObject.transform),
                actionOnGet: projectile =>
                {
                    projectile.transform.position = fighterView.ShootPoint.position;
                    projectile.gameObject.SetActive(true);
                },
                actionOnRelease: projectile => projectile.gameObject.SetActive(false),
                actionOnDestroy: projectile => Object.Destroy(projectile.gameObject),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 20
            );
        }

        public async UniTask Launch(Transform target, CancellationToken token)
        {
            GameObject projectile = _staticProjectilePool.Get();
            projectile.transform.LookAt(target);

            projectile.PlayParticleSystems();

            await Task.Delay(TimeSpan.FromSeconds(_fighterView.ProjectileSpeed), token);
            _staticProjectilePool.Release(projectile);
        }
    }
}