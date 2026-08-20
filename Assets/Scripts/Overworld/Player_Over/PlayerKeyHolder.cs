using UnityEngine;
using System.Collections.Generic;

public class PlayerKeyHolder : MonoBehaviour
{
    public List<string> KeysHeld = new List<string> { };
    void OnEnable()
    {
        EventBus.Subscribe<SceneTransition>(OutputKeys);
        EventBus.Subscribe<AddKeyEvent>(InputKeys);
    }

    void OnDisable()
    {
        EventBus.UnSubscribe<SceneTransition>(OutputKeys);
        EventBus.Subscribe<AddKeyEvent>(InputKeys);
    }
    public void OutputKeys(SceneTransition data)
    {
        EventBus.Raise(new GetKeyEvent { PlayerKeys = KeysHeld });
    }
    public void InputKeys(AddKeyEvent data)
    {
        KeysHeld.Add(data.AddedKey);
    }

}
