using System.Collections.Generic;
using UnityEngine;

namespace Imagy_VFX.VFX_Resources.Scripts.ParticleSystem
{
    [RequireComponent(typeof(UnityEngine.ParticleSystem))]
    public sealed class HeadParticleCollision : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.ParticleSystem _viewParticle;

        private readonly List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();
        private UnityEngine.ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<UnityEngine.ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            int collisionEventsCount = _particle.GetCollisionEvents(other, _collisionEvents);
            if (collisionEventsCount <= 0)
                return;

            _viewParticle.gameObject.SetActive(false);
        }
    }
}
