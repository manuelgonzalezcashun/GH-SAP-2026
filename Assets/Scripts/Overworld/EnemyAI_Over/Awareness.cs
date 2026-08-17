using UnityEngine;

public class Awareness : MonoBehaviour
{
    public bool aggro { get; private set; }

    public Vector2 playerLocation { get; private set; }

    [SerializeField]
    private float _detectionRange;
    private PlayerController player;
    private bool aggroToggle = true;
    public PlayerController Player => player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        Vector2 attackVect = player.transform.position - transform.position;
        playerLocation = attackVect.normalized;

        if (attackVect.magnitude <= _detectionRange)
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
