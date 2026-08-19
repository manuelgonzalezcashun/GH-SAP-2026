using UnityEngine;

public class PlayerKeyHolder : MonoBehaviour
{
    public string[] KeysHeld;
    void OnEnable()
    {
        EventBus.Subscribe<SceneTransition>(OutputKeys);
    }

    void OnDisable()
    {
        EventBus.UnSubscribe<SceneTransition>(OutputKeys);
    }
    public void OutputKeys(SceneTransition data)
    {
        EventBus.Raise(new GetKeyEvent { PlayerKeys = KeysHeld });
    }
}
