
using CraftingSystem;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] SO_Item itemData = null;
    public override void OnInteract()
    {
        if (itemData == null) return;

        EventBus.Raise(new AddItemEvent { item = itemData }); // If Item isn't null, add it to the inventory
        Destroy(gameObject); // Remove Item from location
    }
}
