using UnityEngine;

namespace Inventory.Items.Core
{
    public abstract class ItemConfigBase : ScriptableObject
    {
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public EInventoryTab Tab => _tab;

        [SerializeField] private string _name;
        [Multiline]
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private EInventoryTab _tab;

        public ItemConfigBase SetName(string name)
        {
            _name = name;
            return this;
        }

    }
}