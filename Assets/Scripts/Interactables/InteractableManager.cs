using System.Collections.Generic;
using UnityEngine;

public class InteractableManager
{
    #region Singleton Code 
    private static InteractableManager _instance;
    public static InteractableManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new InteractableManager();

            return _instance;
        }
    }
    InteractableManager()
    {
        EventBus.Subscribe<ItemSearchEvent>(SearchForNearestInteractable);
        EventBus.Subscribe<PlayerInteractEvent>(Interact);
    }

    // ~InteractableManager()
    // {
    //     EventBus.UnSubscribe<ItemSearchEvent>(SearchForNearestInteractable);
    //     EventBus.UnSubscribe<PlayerInteractEvent>(Interact);
    // }
    #endregion

    List<Interactable> interactables = new List<Interactable>();
    private Interactable closestInteractable;

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }
    public void RemoveInteractables(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    public void SearchForNearestInteractable(ItemSearchEvent data)
    {
        float closestDistance = data._interactDistance;
        closestInteractable = null;

        foreach (var item in interactables)
        {
            float currentDistance = Vector2.Distance(data._interactPosition, item.transform.position);
            if (currentDistance <= closestDistance)
            {
                closestDistance = currentDistance;
                closestInteractable = item;
            }
        }

        if (closestInteractable != null) HandleInteraction(closestInteractable);
    }
    public void HandleInteraction(Interactable closest)
    {
        if (closest == null) return;
    }
    public void Interact(PlayerInteractEvent data)
    {
        if (closestInteractable == null) return;

        closestInteractable.OnInteract();
    }

}
