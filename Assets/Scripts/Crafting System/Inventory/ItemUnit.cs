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
        public SO_Item SO_Item => so_item;
        Image itemImage => GetComponent<Image>();

        protected override void Awake()
        {
            base.Awake();
            if (so_item != null)
            {
                SetUnitSprite();
            }
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

