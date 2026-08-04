using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SummaryPage : MonoBehaviour
{
    [Header("External Data")]
    [SerializeField] TrainerParty playerParty = null;

    [Header("Pneuma Display")]
    [SerializeField] Image pneumaProfileImage = null;
    [SerializeField] TMP_Text pneumaNameLabel = null;
    [SerializeField] TMP_Text pneumaTypeLabel = null;
    [Header("Moves/Stats Display")]
    [SerializeField] TMP_Text healthLabel = null;
    [SerializeField] TMP_Text initiativeLabel = null;
    // Aptitude Label
    [SerializeField] TMP_Text[] moveLabels = null;
    private int pneumaIndex = 0;

    void Update()
    {
        if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
            UpdateCreatureIndex();

        ShowCreatureInfo();
    }
    void UpdateCreatureIndex()
    {
        pneumaIndex++;
        pneumaIndex %= playerParty.Battlers.Count;
    }
    void ShowCreatureInfo()
    {
        var currentPneuma = playerParty.Battlers[pneumaIndex];

        // Pneuma Display
        pneumaProfileImage.sprite = currentPneuma.Sprite;
        pneumaNameLabel.text = $"Name: {currentPneuma.Name}";
        pneumaTypeLabel.text = $"Type: {currentPneuma.FirstType} / {currentPneuma.SecondType}";

        // Pneuma Move List / Stat Display
        healthLabel.text = $"Health: {currentPneuma.Health} / {currentPneuma.MaxHealth}";
        initiativeLabel.text = $"Initiative: {currentPneuma.Initiative}";

        for (int i = 0; i < currentPneuma.Moves.Length; i++)
        {
            var currentMove = currentPneuma.Moves[i];
            moveLabels[i].text = $"{currentMove.Name} ({currentMove.Type})";
        }
        // Display Move Description
    }
}
