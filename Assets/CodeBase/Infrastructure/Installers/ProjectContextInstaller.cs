using Core;
using Core.GameResources;
using Infrastructure.Scenes;
using Infrastructure.TimeHelp;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Install<ScenesFlowInstaller>();
            Install<BootAllSceneInstaller>();
            Install<TimeInstaller>();
            Install<ResourcesProjectInstaller>();
        }

        private void Install<T>() where T : Installer<T>
        {
            Installer<T>.Install(Container);
            Debug.Log($"[PROJECT INSTALLER] Install: <b>{typeof(T).Name}</b>");
        }
    }
}