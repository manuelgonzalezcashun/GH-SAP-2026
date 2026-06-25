using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueView : MonoBehaviour
{
    public event Action onChoiceMade;
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
    bool isTyping = false;
    Story _story = null;

    void OnEnable()
    {
        SpeakerTag.onNameUpdate += SetNameTag;
    }
    void OnDisable()
    {
        SpeakerTag.onNameUpdate -= SetNameTag;
    }

    public void SetStory(Story story)
    {
        _story = story;
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
    void SetNameTag(string name)
    {
        nameTagText.text = name;
    }

    void DisplayStoryChoices()
    {
        var choices = _story.currentChoices;
        choiceContainer.gameObject.SetActive(true);

        foreach (var choice in choices)
        {
            Button choiceButton = GetChoiceButton();
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();

            choiceText.text = choice.text;

            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() => MakeChoice(choice.index));
        }
    }

    #region Choice Helper Methods
    private void MakeChoice(int choiceIndex)
    {
        _story.ChooseChoiceIndex(choiceIndex);
        ClearStoryChoices();

        onChoiceMade?.Invoke();
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
