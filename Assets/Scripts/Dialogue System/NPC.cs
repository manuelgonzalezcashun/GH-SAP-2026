using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] string KnotName = string.Empty;
    public override void OnInteract()
    {
        if (KnotName == string.Empty) return;
        EventBus.Raise(new InitiateDialogueEvent { knotName = KnotName });
    }
}
