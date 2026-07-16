using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField] string KnotName = string.Empty;
    private float _distance = 1.5f;
    private PlayerController player => FindAnyObjectByType<PlayerController>();

    bool playerInRange => Vector2.Distance(player.transform.position, transform.position) <= _distance;

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (!playerInRange) return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            EventBus.Raise(new InitiateDialogueEvent { knotName = KnotName });
        }
    }
}
