using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] string KnotName = string.Empty;
    public override void OnInteract()
    {
        EventBus.Raise(new InitiateDialogueEvent { knotName = KnotName });
    }
}
