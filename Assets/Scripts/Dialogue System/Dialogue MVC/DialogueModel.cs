using UnityEngine;
using Ink.Runtime;
using System;

[Serializable]
public class DialogueModel
{
    // Ink Story Data //
    [SerializeField] TextAsset _storyJson;
    public Story Story { get; private set; }
    private static string _loadedState;
    public bool HasChoices => Story.currentChoices.Count > 0;

    // Ink Classes //
    DialogueTagHandler tagHandler;
    DialogueVariableObserver observer;
    public DialogueTagHandler TagHandler => tagHandler;
    public DialogueVariableObserver Observer => observer;
    public void Initialize()
    {
        Story = new Story(_storyJson.text);
        if (!string.IsNullOrEmpty(_loadedState))
        {
            Story.state.LoadJson(_loadedState);
            _loadedState = null;
        }

        tagHandler = new DialogueTagHandler();
        observer = new DialogueVariableObserver();
    }

    #region Story State
    public string GetStoryState()
    {
        return Story.state.ToJson();
    }
    public static void LoadState(string state) => _loadedState = state;

    #endregion
}
