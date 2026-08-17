using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CraftingSystem;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] RectTransform flavorTextContainer;
        [SerializeField] TMP_Text flavorText;
        [SerializeField] InventorySlot[] slots;
        [SerializeField] ItemUnit unitPrefab;
        List<Item> items = new List<Item>();
        int itemCount = 0;
        void OnEnable()
        {
            EventBus.Subscribe<AddItemEvent>(AddItem);
            EventBus.Subscribe<RemoveItemEvent>(RemoveItem);
            InventorySlot.onHoverSlot += ShowItemDescription;
        }
        void OnDisable()
        {
            EventBus.UnSubscribe<AddItemEvent>(AddItem);
            EventBus.UnSubscribe<RemoveItemEvent>(RemoveItem);
            InventorySlot.onHoverSlot -= ShowItemDescription;
        }
        private void AddItem(AddItemEvent data)
        {
            ItemUnit unit = Instantiate(unitPrefab, slots[itemCount].transform);
            AddItem(data.item.Item);
            unit.SetSOItem(data.item);
        }
        private void RemoveItem(RemoveItemEvent data)
        {
            RemoveItem(data.item.Item);
        }
        private void AddItem(Item item)
        {
            if (itemCount >= slots.Length) return;

            items.Add(item);
            itemCount++;
        }
        private void RemoveItem(Item item)
        {
            if (itemCount <= 0) return;

            items.Remove(item);
            itemCount--;
        }
        private void ShowItemDescription(string description, bool show)
        {
            flavorText.text = description;
            flavorTextContainer.gameObject.SetActive(show);
        }
    }
}
