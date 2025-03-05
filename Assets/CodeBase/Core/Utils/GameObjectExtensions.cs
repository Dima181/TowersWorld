using UnityEngine;

namespace Core.Utils
{
    public static class GameObjectExtensions
    {
        public static void PlayParticleSystems(this GameObject go)
        {
            if (go.TryGetComponent<ParticleSystem>(out var particles))
            {
                particles.Play();
            }
            else
            {
                var particleSystems = go.GetComponentsInChildren<ParticleSystem>();
                if (particleSystems != null)
                {
                    foreach (ParticleSystem particle in particleSystems)
                    {
                        particle.Play();
                    }
                }
            }
        }
    }
}
