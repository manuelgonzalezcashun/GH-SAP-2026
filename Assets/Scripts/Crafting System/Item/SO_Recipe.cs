using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(menuName = "Crafting System/New Recipe", fileName = "New Recipe")]
    public class SO_Recipe : ScriptableObject
    {
        [SerializeField] SO_Item[] so_RecipeItems = null;
        [SerializeField] SO_Item so_Output_Item;

        private Recipe _recipe = null;
        public Recipe Recipe
        {
            get
            {
                _recipe ??= new Recipe(Items(), so_Output_Item.Item);
                return _recipe;
            }
        }

        public SO_Item GetOutputItem(SO_Item[] inputItems)
        {
            if (inputItems == null || so_RecipeItems == null) return null;

            var filteredItems = inputItems.Where(item => item != null).ToArray();

            bool isMatchingItems = filteredItems.Length == so_RecipeItems.Length &&
            !filteredItems.Except(so_RecipeItems).Any();

            return isMatchingItems ? so_Output_Item : null;
        }
        private Item[] Items()
        {
            Item[] items = new Item[so_RecipeItems.Length];
            for (int i = 0; i < so_RecipeItems.Length; i++)
            {
                if (so_RecipeItems[i]?.Item != null)
                    items[i] = so_RecipeItems[i].Item;
            }
            return items;
        }
    }
}
