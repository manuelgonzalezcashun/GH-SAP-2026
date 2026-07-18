using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void OnEnable()
    {
        InteractableManager.Instance.AddInteractable(this);
    }
    public virtual void OnDisable()
    {
        InteractableManager.Instance.RemoveInteractables(this);
    }
    public abstract void OnInteract();
}