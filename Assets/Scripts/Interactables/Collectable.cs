
using CraftingSystem;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] SO_Item itemData = null;

    SpriteRenderer spriteRenderer = null;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.Item.Sprite;
    }
    public override void OnInteract()
    {
        // If Item isn't null, add it to the inventory
        // Remove Item from location
    }
}
