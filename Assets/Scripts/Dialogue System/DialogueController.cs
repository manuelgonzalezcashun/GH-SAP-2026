using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    // MVC // 
    [SerializeField] DialogueView view;
    [SerializeField] DialogueModel model;


    void OnEnable()
    {
        view.onChoiceMade += StepThroughDialogue;
    }
    void OnDisable()
    {
        view.onChoiceMade -= StepThroughDialogue;
    }

    void Start()
    {
        model.Initialize();
        view.SetStory(model.Story);
        StartCoroutine(view.TypeWriterAnimation(model.Story.Continue()));
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
        StartCoroutine(view.TypeWriterAnimation(line));
    }
    public void EndStory()
    {
        view.ShowDialogue(false);
    }
}

public class DialogueTagHandler { }
public class DialogueEventObserver { }
public class DialogueExternalFunctionHandler { }
