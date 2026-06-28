using Ink.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    // MVC // 
    [SerializeField] DialogueView view;
    [SerializeField] DialogueModel model;

    void Start()
    {
        view.onChoiceMade += StepThroughDialogue;
        model.Initialize();
        view.SetStory(model.Story);
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
        model.TagHandler.HandleTags(model.Story.currentTags);
        StartCoroutine(view.TypeWriterAnimation(line));
    }
    public void EndStory()
    {
        view.onChoiceMade -= StepThroughDialogue;
        view.ShowDialogue(false);
    }
}

public class DialogueEventObserver { }
public class DialogueExternalFunctionHandler { }
