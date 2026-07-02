using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    [SerializeField] string tagFilter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!String.IsNullOrEmpty(tagFilter)&&!other.gameObject.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        if (!String.IsNullOrEmpty(tagFilter)&&!other.gameObject.CompareTag(tagFilter)) return;
        onTriggerExit.Invoke();
    }
}
