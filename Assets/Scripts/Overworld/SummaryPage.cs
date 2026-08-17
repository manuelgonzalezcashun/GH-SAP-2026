using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

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
    [SerializeField] TMP_Text aptitudeLabel = null;
    [SerializeField] TMP_Text[] moveLabels = null;
    [Header("Team Display")]
    [SerializeField] Image[] memberSprites = null;
    private int pneumaIndex = 0;
    void Start()
    {
        DisplayMembers();
    }
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
        aptitudeLabel.text = $"Aptitude: {currentPneuma.Aptitude}";

        // Display Move Description
        DisplayPneumasMoves(currentPneuma);
    }
    // Helper Method
    private void DisplayPneumasMoves(Battler currentPneuma)
    {
        // Clear objects whenever player changes the pneuma in the summary page
        foreach (var label in moveLabels) label.gameObject.SetActive(false);

        for (int i = 0; i < currentPneuma.Moves.Length; i++)
        {
            var currentMove = currentPneuma.Moves[i];
            moveLabels[i].text = $"{currentMove.Name} ({currentMove.Type})";
            moveLabels[i].gameObject.SetActive(true);
        }
    }
    private void DisplayMembers()
    {
        foreach (var sprite in memberSprites) sprite.gameObject.SetActive(false);

        var members = playerParty.Battlers;
        for (int i = 0; i < members.Count; i++)
        {
            memberSprites[i].gameObject.SetActive(true);
            memberSprites[i].sprite = members[i].Sprite;
            memberSprites[i].color = Color.white;
        }
    }
}
