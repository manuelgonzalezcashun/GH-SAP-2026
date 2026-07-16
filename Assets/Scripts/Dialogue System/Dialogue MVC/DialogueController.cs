using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class DialogueController : MonoBehaviour
{
    // MVC // 
    [SerializeField] DialogueView view;
    [SerializeField] DialogueModel model;
    void OnEnable()
    {
        EventBus.Subscribe<InitiateDialogueEvent>(StartDialogueAtKnot);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<InitiateDialogueEvent>(StartDialogueAtKnot);
    }

    void StartDialogueAtKnot(InitiateDialogueEvent data)
    {
        StartDialogueAtKnot(data.knotName);
    }

    void StartDialogueAtKnot(string knotName)
    {
        if (knotName == null || knotName == string.Empty)
        {
            Debug.Log($"Knot Name came back empty! Continuing without staring dialogue");
            return;
        }

        view.ShowDialogue(true);
        model.Story.ChoosePathString(knotName);

        StepThroughDialogue();
    }

    void Start()
    {
        model.Initialize();
        model.Observer.StartListening(model.Story);
        view.onChoiceMade += HandleChoiceSelected;

        StepThroughDialogue();
    }
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StepThroughDialogue();
        }
    }
    void StepThroughDialogue()
    {
        if (!model.Story.canContinue)
        {
            EndStory();
            return;
        }
        string line = model.Story.Continue();

        if (model.HasChoices)
        {
            List<string> choicesText = (from choice in model.Story.currentChoices select choice.text).ToList();
            view.HandleStoryChoices(choicesText);
        }

        model.TagHandler.HandleTags(model.Story.currentTags);
        StartCoroutine(view.TypeWriterAnimation(line));
    }
    void HandleChoiceSelected(int index)
    {
        model.Story.ChooseChoiceIndex(index);
        StepThroughDialogue();
    }
    public void EndStory()
    {
        model.Observer.StopListening(model.Story);
        view.onChoiceMade -= HandleChoiceSelected;
        view.ShowDialogue(false);
    }
}
public class DialogueExternalFunctionHandler { }
