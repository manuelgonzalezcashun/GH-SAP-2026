using UnityEngine;
using UnityEngine.UI;
using CraftingSystem;
using SlotObject;

namespace InventorySystem
{
    [RequireComponent(typeof(Image))]
    public class ItemUnit : SlotUnit
    {
        [SerializeField] SO_Item so_item = null;
        Image itemImage = null;
        public SO_Item SO_Item => so_item;

        protected override void Awake()
        {
            base.Awake();
            itemImage = GetComponent<Image>();

            SetUnitSprite();
        }

        private void SetUnitSprite()
        {
            if (so_item == null) return;
            itemImage.sprite = so_item.Item.Sprite;
        }

        public void SetSOItem(SO_Item so_item)
        {
            this.so_item = so_item;
            SetUnitSprite();
        }
    }
}

