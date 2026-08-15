
using CraftingSystem;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] SO_Item itemData = null;
    void Awake()
    {
        if (itemData == null) return;
    }
    public override void OnInteract()
    {
        // If Item isn't null, add it to the inventory
        // Remove Item from location
    }
}
