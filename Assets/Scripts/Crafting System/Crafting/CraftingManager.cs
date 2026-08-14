using UnityEngine;
using System.Linq;
using InventorySystem;

namespace CraftingSystem
{
    public class CraftingManager : MonoBehaviour
    {
        [SerializeField] CraftingSlot[] slots;
        [SerializeField] CraftingSlot outputSlot = null;
        [SerializeField] SO_Recipe[] recipes;
        [SerializeField] ItemUnit outputItemUnitPrefab;
        private SO_Item[] Items
        {
            get
            {
                SO_Item[] items = new SO_Item[slots.Length];
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i]?.Unit != null)
                    {
                        items[i] = slots[i].Unit.SO_Item;
                    }
                }
                return items;
            }
        }

        public void CraftRecipe()
        {
            if (slots.Length <= 0 || recipes.Length <= 0) return;
            if (outputSlot == null) return;

            var outputItem = recipes
            .Select(recipe => recipe.GetOutputItem(Items))
            .FirstOrDefault(item => item != null);

            if (outputItem == null) return;

            var outputUnit = Instantiate(outputItemUnitPrefab, outputSlot.transform);
            outputUnit.SetSOItem(outputItem);

            outputSlot.SetUnitInSlot(outputUnit);

            DestroyUnits();
        }

        private void DestroyUnits()
        {
            foreach (var slot in slots)
            {
                if (slot.Unit == null) return;
                Destroy(slot.Unit.gameObject);
            }
        }
    }

}

