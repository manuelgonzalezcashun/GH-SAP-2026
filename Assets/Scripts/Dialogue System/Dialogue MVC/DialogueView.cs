using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueView : MonoBehaviour
{
    public event Action<int> onChoiceMade;
    Queue<Button> choicePool = new Queue<Button>();

    // Dialogue UI //
    [SerializeField] RectTransform dialogueContainer;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text nameTagText;
    [SerializeField] RectTransform choiceContainer;
    [SerializeField] Button choicePrefab;

    // Runtime Variables //
    [Range(0.01f, 0.05f)]
    [SerializeField] float dialogueWaitTime = 0.02f;

    List<string> currentChoices = new List<string>();
    bool isTyping = false;

    void OnEnable()
    {
        SpeakerTagStrategy.onNameUpdate += SetNameTag;
    }
    void OnDisable()
    {
        SpeakerTagStrategy.onNameUpdate -= SetNameTag;
    }

    public IEnumerator TypeWriterAnimation(string dialogueLine)
    {
        dialogueText.text = dialogueLine; // Set UI Text to dialogueLine
        dialogueText.maxVisibleCharacters = 0; // Hide All Characters

        // Show Characters one by until text is shown
        while (dialogueText.maxVisibleCharacters < dialogueLine.Length)
        {
            isTyping = true;
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(dialogueWaitTime);
        }

        DisplayStoryChoices();
        isTyping = false;
    }
    public void ShowDialogue(bool visible)
    {
        dialogueText.text = string.Empty;
        dialogueContainer.gameObject.SetActive(visible);
        ClearStoryChoices();
    }

    public void HandleStoryChoices(List<string> choices)
    {
        currentChoices = choices;
    }
    void SetNameTag(string name)
    {
        nameTagText.text = name;
    }

    void DisplayStoryChoices()
    {
        choiceContainer.gameObject.SetActive(true);

        for (int i = 0; i < currentChoices.Count; i++)
        {
            Button choiceButton = GetChoiceButton();
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();

            choiceText.text = currentChoices[i];
            choiceButton.onClick.RemoveAllListeners();

            int index = i;
            choiceButton.onClick.AddListener(() => MakeChoice(index));
        }
    }

    #region Choice Helper Methods
    private void MakeChoice(int choiceIndex)
    {
        ClearStoryChoices();
        onChoiceMade?.Invoke(choiceIndex);
    }
    private Button GetChoiceButton()
    {
        Button choiceButton;
        if (choicePool.Count > 0)
        {
            choiceButton = choicePool.Dequeue();
            choiceButton.gameObject.SetActive(true);
        }
        else
        {
            var clone = Instantiate(choicePrefab, choiceContainer);
            choiceButton = clone.GetComponent<Button>();
        }

        return choiceButton;
    }

    private void ClearStoryChoices()
    {
        currentChoices.Clear();

        foreach (Transform child in choiceContainer)
        {
            Button childButton = child.gameObject.GetComponent<Button>();
            childButton.gameObject.SetActive(false);
            childButton.onClick.RemoveAllListeners();

            choicePool.Enqueue(childButton);
        }
    }
    #endregion

}
