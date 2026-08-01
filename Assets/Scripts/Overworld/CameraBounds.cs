using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [Header("Camera Position")]
    [SerializeField] int posX = 0;
    [SerializeField] int posY = 0;
    [SerializeField] int posZ = 0;

    [Header("Camera Boundries")]
    [SerializeField] int boundX = 0;
    [SerializeField] int boundY = 0;

    void Awake()
    {
        Vector3 newPos = new Vector3(posX, posY, posZ);
        Vector2 newBounds = new Vector2(boundX, boundY);

        EventBus.Raise(new SetCameraBoundsEvent { camBounds = newBounds, camPos = newPos });
    }
}
