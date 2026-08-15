using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueController : MonoBehaviour
{
    // MVC // 
    [SerializeField] DialogueView view;
    [SerializeField] DialogueModel model;

    void OnEnable()
    {
        EventBus.Subscribe<InitiateDialogueEvent>(StartDialogueAtKnot);
        view.onChoiceMade += HandleChoiceSelected;

        model.Initialize();
        model.Observer.StartListening(model.Story);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<InitiateDialogueEvent>(StartDialogueAtKnot);
        view.onChoiceMade -= HandleChoiceSelected;

        model.Observer.StopListening(model.Story);
    }

    void StartDialogueAtKnot(InitiateDialogueEvent data)
    {
        StartDialogueAtKnot(data.knotName);
    }

    void StartDialogueAtKnot(string knotName)
    {
        if (knotName == null || knotName == string.Empty) return;

        if (model.Story.KnotContainerWithName(knotName))
        {
            view.ShowDialogue(true);
            model.Story.ChoosePathString(knotName);
            InputHandler.ChangeActionMaps(InputHandler.dialogueInput);
        }

        NextDialogueLine();
    }

    void Start()
    {
        StartDialogueAtKnot(string.Empty);
    }
    void Update()
    {
        StepThroughDialogue();
    }
    private void StepThroughDialogue()
    {
        if (!InputHandler.SubmitPressed) return;
        if (view.isTyping)
        {
            view.DisplayCompletedDialogueLine();
            return;
        }
        if (!model.HasChoices && !view.isTyping)
        {
            NextDialogueLine();
        }
    }

    void NextDialogueLine()
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
        view.DisplayDialogueLine(line);
    }
    void HandleChoiceSelected(int index)
    {
        model.Story.ChooseChoiceIndex(index);
        NextDialogueLine();
    }
    public void EndStory()
    {
        EventBus.Raise(new PlayerMoveEvent { canMove = true });
        view.ShowDialogue(false);
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
    }
}
public class DialogueExternalFunctionHandler { }
