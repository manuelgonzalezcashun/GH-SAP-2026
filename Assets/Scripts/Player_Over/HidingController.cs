using UnityEngine;

public class HidingController : MonoBehaviour
{
    
    public bool hidden = false;

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


    public bool getHidden()
    {
        return hidden;
    }

    public void hide()
    {
        hidden = true;
    }
    public void unhide()
    {
        hidden = false;
    }
}
