using System;
using UnityEngine;
public interface InkTag
{
    void ExecuteTag(string value);
}

public class SpeakerTag : InkTag
{
    public static event Action<string> onNameUpdate;
    public void ExecuteTag(string value)
    {
        onNameUpdate?.Invoke(value);
    }
}