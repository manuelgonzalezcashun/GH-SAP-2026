using System.Collections.Generic;
using UnityEngine;

public class DialogueTagHandler
{
    const string SPEAKER = "Speaker";
    Dictionary<string, InkTag> tagRegistry = new Dictionary<string, InkTag>
    {
        {SPEAKER , new SpeakerTag()}
    };
    public void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
            {
                Debug.Log("Tag could not be parsed in correctly.");
                return;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            ProcessTag(tagKey, tagValue);
        }
    }
    void ProcessTag(string key, string value)
    {
        if (tagRegistry.TryGetValue(key, out var tag))
        {
            tag.ExecuteTag(value);
        }
    }
}
