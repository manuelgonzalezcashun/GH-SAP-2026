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
            if (data.item != null)
            {
                ItemUnit unit = Instantiate(unitPrefab, slots[itemCount].transform);
                unit.SetSOItem(data.item);
                AddItem(data.item.Item);
            }
            else
            {
                AddItem();
            }

        }
        private void RemoveItem(RemoveItemEvent data)
        {
            if (data.item.Item == null) return;

            RemoveItem(data.item.Item);
        }
        private void AddItem(Item item)
        {
            if (itemCount >= slots.Length) return;

            items.Add(item);
            itemCount++;
            Debug.Log(itemCount);
        }
        private void AddItem()
        {
            if (itemCount >= slots.Length) return;
            itemCount++;
            Debug.Log(itemCount);
        }
        private void RemoveItem(Item item)
        {
            if (itemCount <= 0) return;

            items.Remove(item);
            itemCount--;
            Debug.Log(itemCount);
        }
        private void ShowItemDescription(string description, bool show)
        {
            flavorText.text = description;
            flavorTextContainer.gameObject.SetActive(show);
        }
    }
}
