using UnityEngine;
using System.Collections.Generic;

public class PlayerKeyHolder : MonoBehaviour
{
    public List<string> KeysHeld = new List<string> { };
    void OnEnable()
    {
        EventBus.Subscribe<SetCameraBoundsEvent>(OutputKeys);
        EventBus.Subscribe<AddKeyEvent>(InputKeys);
    }

    void OnDisable()
    {
        EventBus.UnSubscribe<SetCameraBoundsEvent>(OutputKeys);
        EventBus.Subscribe<AddKeyEvent>(InputKeys);
    }
    public void OutputKeys(SetCameraBoundsEvent data)
    {
        EventBus.Raise(new GetKeyEvent { PlayerKeys = KeysHeld });
    }
    public void InputKeys(AddKeyEvent data)
    {
        if (!KeysHeld.Contains(data.AddedKey))
        {
            KeysHeld.Add(data.AddedKey);
        }
    }

}
