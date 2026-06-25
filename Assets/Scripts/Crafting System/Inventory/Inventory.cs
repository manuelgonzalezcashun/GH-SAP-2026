using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] RectTransform flavorTextContainer;
        [SerializeField] TMP_Text flavorText;
        [SerializeField] ItemUnit[] units;
        List<Item> items = new List<Item>();

        void OnEnable()
        {
            InventorySlot.onHoverSlot += ShowItemDescription;
        }



        void OnDisable()
        {
            InventorySlot.onHoverSlot -= ShowItemDescription;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
        private void ShowItemDescription(string description, bool show)
        {
            flavorText.text = description;
            flavorTextContainer.gameObject.SetActive(show);
        }
    }
}
