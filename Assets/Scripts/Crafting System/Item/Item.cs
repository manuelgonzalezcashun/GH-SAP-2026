using UnityEngine;

namespace InventorySystem
{
    public class Item
    {
        private string name;
        private string description;
        private Sprite sprite;

        public string Name => name;
        public string Description => description;
        public Sprite Sprite => sprite;

        public Item(string name, string description, Sprite sprite)
        {
            this.name = name;
            this.description = description;
            this.sprite = sprite;
        }

    }
}
