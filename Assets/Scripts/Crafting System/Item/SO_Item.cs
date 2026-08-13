using UnityEngine;
using InventorySystem;

namespace CraftingSystem
{
    [CreateAssetMenu(menuName = "Crafting System/New Item", fileName = "New Item")]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] string description;
        [SerializeField] Sprite sprite;

        private Item _item = null;
        public Item Item
        {
            get
            {
                _item ??= new Item(name, description, sprite);
                return _item;
            }
        }
    }
}

