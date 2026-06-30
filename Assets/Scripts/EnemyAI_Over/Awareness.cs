using UnityEngine;

public class Awareness : MonoBehaviour
{
    public bool aggro {get; private set;}

    public Vector2 playerLocation {get; private set;}

    [SerializeField]
    private float _detectionRange;

    private Transform player;


    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        Vector2 attackVect = player.position - transform.position;
        playerLocation = attackVect.normalized;

        if (attackVect.magnitude <= _detectionRange)
        {
            aggro = true;
        }
        else
        {
            aggro = false;
        }
    }
}
