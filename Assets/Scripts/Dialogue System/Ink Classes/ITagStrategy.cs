using System;
using UnityEngine;
public interface ITagStrategy
{
    void ExecuteTag(string value);
}

public class SpeakerTagStrategy : ITagStrategy
{
    public static event Action<string> onNameUpdate;
    public void ExecuteTag(string value)
    {
        onNameUpdate?.Invoke(value);
    }
}
