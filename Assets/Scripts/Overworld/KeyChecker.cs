using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyChecker : MonoBehaviour
{
    public bool Unload;
    public string Key;
    public string SuperUnloadKey = "none";

    void OnEnable()
    {
        EventBus.Subscribe<GetKeyEvent>(PlayerKeys);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<GetKeyEvent>(PlayerKeys);
    }
    public void PlayerKeys(GetKeyEvent data)
    {
        if (Unload)
        {
            if (data.PlayerKeys.Contains(Key))
            {
                Debug.Log(gameObject+" died");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log(gameObject+" lived");
            }
        }
        else
        {
            if (!data.PlayerKeys.Contains(Key))
            {
                Debug.Log(gameObject+" died");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log(gameObject+" lived");
            }
        }
        if (!data.PlayerKeys.Contains(SuperUnloadKey))
        {
            Debug.Log(gameObject+" died");

            Destroy(gameObject);
        }
        //marco notes because I need to comment more:
        // the way i have this set up is that any gameobject can be given this script
        // the unload value determines whether having the right key makes the creature unload or if a key is needed to make it load at all
        //the player will gain a new script called playerkeyholder that will hold keys (represented by strings) and broadcast them when a new scene is loaded
        // the super unload key is to just check if the player has another key that will automatically unload it (ask me if i wasnt clear)
    }

    
}
