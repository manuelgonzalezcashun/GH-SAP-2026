using UnityEngine;

public class KeyGiverInWorld : MonoBehaviour
{
    public string KeyGiven;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventBus.Raise(new AddKeyEvent { AddedKey = KeyGiven });
        }
    }
}
