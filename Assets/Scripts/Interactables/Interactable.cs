using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] GameObject interactIcon = null;
    public virtual void OnEnable()
    {
        InteractableManager.Instance.AddInteractable(this);
        EventBus.Subscribe<InteractionWithinRangeEvent>(EnableInteractIcon);
    }
    public virtual void OnDisable()
    {
        InteractableManager.Instance.RemoveInteractables(this);
        EventBus.UnSubscribe<InteractionWithinRangeEvent>(EnableInteractIcon);
    }
    public void EnableInteractIcon(InteractionWithinRangeEvent data)
    {
        if (interactIcon == null) return;
        interactIcon.SetActive(data.enableIcon);
    }
    public abstract void OnInteract();
}