using UnityEngine;
using System.Linq;
using InventorySystem;

namespace CraftingSystem
{
    public class CraftingManager : MonoBehaviour
    {
        [SerializeField] CraftingSlot slot_a = null;
        [SerializeField] CraftingSlot slot_b = null;
        [SerializeField] CraftingSlot outputSlot = null;
        [SerializeField] SO_Recipe[] recipes;
        [SerializeField] ItemUnit outputItemUnitPrefab;

        public void CraftRecipe()
        {
            if (slot_a.Unit == null || slot_b.Unit == null || recipes.Length <= 0) return;
            if (outputSlot == null) return;

            var outputItem = recipes
            .Select(recipe => recipe.GetOutputItem(slot_a.Unit.SO_Item, slot_b.Unit.SO_Item))
            .FirstOrDefault(item => item != null);

            if (outputItem == null) return;

            var outputUnit = Instantiate(outputItemUnitPrefab, outputSlot.transform);
            outputUnit.SetSOItem(outputItem);

            outputSlot.SeUnitInSlot(outputUnit);

            Destroy(slot_a.Unit.gameObject);
            Destroy(slot_b.Unit.gameObject);
        }
    }

}

