using System;
using UnityEngine;
using UnityEngine.EventSystems;
using InventorySystem;
using SlotObject;


namespace CraftingSystem
{
    public class CraftingSlot : Slot
    {
        [SerializeField] bool isOutputSlot = false;
        private ItemUnit _unit;
        public ItemUnit Unit => _unit;

        public override void OnDrop(PointerEventData eventData)
        {
            if (isOutputSlot) return;

            base.OnDrop(eventData);
            _unit = eventData.pointerDrag.GetComponent<ItemUnit>();
        }

        public void SeUnitInSlot(ItemUnit itemUnit)
        {
            if (!isOutputSlot) return;

            _unit = itemUnit;
            _unit.transform.SetParent(transform);
            _unit.transform.position = transform.position;
        }
    }
}

