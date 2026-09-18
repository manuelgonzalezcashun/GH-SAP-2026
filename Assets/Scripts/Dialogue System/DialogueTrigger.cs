using UnityEngine;
/// <summary>
/// This script is attached to Game Objects 
/// that is interactable and trigger Dialogue
/// </summary>
public class DialogueTrigger : Interactable
{
    [SerializeField] string KnotName = string.Empty; // Knots are in the Ink Files. They are the keys to the dialogue
    public override void OnInteract()
    {
        if (KnotName == string.Empty) return;
        EventBus.Raise(new InitiateDialogueEvent { knotName = KnotName });
    }
}
