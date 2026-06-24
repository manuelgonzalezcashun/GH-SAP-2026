using UnityEngine;

namespace CraftingSystem
{
    [CreateAssetMenu(menuName = "Crafting System/New Recipe", fileName = "New Recipe")]
    public class SO_Recipe : ScriptableObject
    {
        [SerializeField] SO_Item so_Component_A;
        [SerializeField] SO_Item so_Component_B;
        [SerializeField] SO_Item so_Output_Item;

        private Recipe _recipe = null;
        public Recipe Recipe
        {
            get
            {
                _recipe ??= new Recipe(so_Component_A.Item, so_Component_B.Item, so_Output_Item.Item);
                return _recipe;
            }
        }
        private bool ValidateRecipeInputs(SO_Item input_a, SO_Item input_b)
        {
            if (so_Component_A == input_a && so_Component_B == input_b) return true;
            if (so_Component_A == input_b && so_Component_B == input_a) return true;
            return false;
        }

        public SO_Item GetOutputItem(SO_Item a, SO_Item b)
        {
            return !ValidateRecipeInputs(a, b) ? null : so_Output_Item;
        }
    }
}
