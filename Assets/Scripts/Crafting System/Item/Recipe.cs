using UnityEngine;
using InventorySystem;
using System.Linq;


namespace CraftingSystem
{
    public class Recipe
    {
        private Item[] recipeItems = null;
        public Item OutputItem { get; private set; }
        public Recipe(Item[] recipeItems, Item outputItem)
        {
            this.recipeItems = recipeItems;
            OutputItem = outputItem;
        }
        private bool _Validate(Item[] items)
        {
            if (items == null || recipeItems == null) return false;

            var filteredItems = items.Where(item => item != null).ToArray();

            bool isMatchingItems = filteredItems.Length == recipeItems.Length &&
            !filteredItems.Except(recipeItems).Any();

            return isMatchingItems;
        }
        public Item Craft(Item[] items)
        {
            return _Validate(items) ? OutputItem : null;
        }
    }
}

