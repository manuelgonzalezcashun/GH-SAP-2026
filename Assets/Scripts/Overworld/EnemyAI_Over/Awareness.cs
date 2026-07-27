using UnityEngine;

public class Awareness : MonoBehaviour
{
    public bool aggro {get; private set;}

    public Vector2 playerLocation {get; private set;}

    [SerializeField]
    private float _detectionRange;

    private Transform player;
    private bool aggroToggle = true;


    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        Vector2 attackVect = player.position - transform.position;
        playerLocation = attackVect.normalized;

        if (attackVect.magnitude <= _detectionRange )
        {
            if (aggroToggle == false)
            {
                aggro = false;
            }
            else
            {
                aggro = true;
            }
            
        }
        else
        {
            aggro = false;
        }
    }
    void OnEnable()
    {
        EventBus.Subscribe<PlayerHideEvent>(HidingUpdate);
    }

    void OnDisable()
    {
        EventBus.UnSubscribe<PlayerHideEvent>(HidingUpdate);
    }

    private void HidingUpdate(PlayerHideEvent data)
    {
        if (data._hidingMode)
        {
            aggroToggle = false;
        }
        else
        {
            aggroToggle = true;
        }
    }
}
