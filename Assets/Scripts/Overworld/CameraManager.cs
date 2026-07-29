using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineConfiner2D followCameraConstraints;
    private BoxCollider2D camBounds => GetComponentInChildren<BoxCollider2D>();

    void OnEnable()
    {
        EventBus.Subscribe<SetCameraBoundsEvent>(SetCameraBounds);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<SetCameraBoundsEvent>(SetCameraBounds);
    }
    void SetCameraBounds(SetCameraBoundsEvent data)
    {
        followCameraConstraints.InvalidateBoundingShapeCache();

        camBounds.size = data.camBounds;
        transform.position = data.camPos;
    }
}


