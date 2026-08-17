using System;
using UnityEngine;
using UnityEngine.EventSystems;
using SlotObject;
using InventorySystem;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventorySlot : Slot
    {
        public static event Action<string, bool> onHoverSlot;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            var itemUnit = eventData.pointerEnter.GetComponentInChildren<ItemUnit>();
            if (itemUnit == null) return;

            string flavorText = itemUnit.SO_Item.Item.Description;
            onHoverSlot?.Invoke(flavorText, true);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            string flavorText = string.Empty;
            onHoverSlot?.Invoke(flavorText, false);
        }
    }
}
