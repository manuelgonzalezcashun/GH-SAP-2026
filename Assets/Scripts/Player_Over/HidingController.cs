using UnityEngine;

public class HidingController : MonoBehaviour
{
    
    public bool hidden = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hidingSpot")
        {
            hide();
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "hidingSpot")
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
        Debug.Log("hide");
    }
    public void unhide()
    {
        hidden = false;
        Debug.Log("unhide");
    }
}
