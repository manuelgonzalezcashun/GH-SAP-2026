using UnityEngine;
using Ink.Runtime;
using System;

[Serializable]
public class DialogueModel
{
    [SerializeField] TextAsset _storyJson;
    public Story Story { get; private set; }
    private static string _loadedState;
    public void Initialize()
    {
        Story = new Story(_storyJson.text);
        if (!string.IsNullOrEmpty(_loadedState))
        {
            Story.state.LoadJson(_loadedState);
            _loadedState = null;
        }
    }

    #region Story State
    public string GetStoryState()
    {
        return Story.state.ToJson();
    }
    public static void LoadState(string state) => _loadedState = state;

    #endregion
}
