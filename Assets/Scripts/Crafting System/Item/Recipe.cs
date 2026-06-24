using UnityEngine;
using InventorySystem;


namespace CraftingSystem
{
    public class Recipe
    {
        private Item component_A;
        private Item component_B;
        public Item OutputItem { get; private set; }
        public Recipe(Item component_A, Item component_B, Item outputItem)
        {
            this.component_A = component_A;
            this.component_B = component_B;
            OutputItem = outputItem;
        }
        private bool _Validate(Item a, Item b)
        {
            return component_A.Name == a.Name
            && component_B.Name == b.Name;
        }
        public Item Craft(Item a, Item b)
        {
            if (!_Validate(a, b)) return null;

            return OutputItem;
        }
        public override string ToString()
        {
            return $"Component A: {component_A.Name}, Component B: {component_B.Name}, Output: {OutputItem.Name}";
        }
    }
}

