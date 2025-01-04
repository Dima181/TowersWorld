using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.GameResources.Configs
{
    [CreateAssetMenu(fileName = nameof(ResourcesConfig), menuName = "TowersWorld/Resources", order = 0)]
    public class ResourcesConfig : ScriptableObject
    {
        public List<ResourceConfig> Resources;

        public ResourceConfig Get(EResource res) => Resources.FirstOrDefault(r => r.Res == res);
    }

    [Serializable]
    public class ResourceConfig
    {
        public EResource Res;
        public Sprite Icon;
        public string Name;
        //...
    }
}
