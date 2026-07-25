using UnityEngine;

public class HidingController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hidingSpot"))
        {
            hide();
        }
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("hidingSpot"))
        {
            unhide();
        }
    }
    public void hide()
    {
        EventBus.Raise(new PlayerHideEvent { _hidingMode = true });
    }
    public void unhide()
    {
        EventBus.Raise(new PlayerHideEvent { _hidingMode = false });
    }
}
