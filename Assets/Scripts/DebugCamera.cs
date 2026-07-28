using UnityEngine;

public class DebugCamera : MonoBehaviour
{
    void Update()
    {
        if (Camera.main != null) gameObject.SetActive(false);
    }
}
