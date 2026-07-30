using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _sprintMod;
    private float _runSpeed;
    private float _speed;

    [SerializeField]
    private float _rotationSpd;

    [SerializeField]
    private float _obstacleCheckRadius;
    [SerializeField]
    private float _obstacleCheckRange;

    [SerializeField]
    private LayerMask _obstacleLayerMask;

    private Rigidbody2D _rigidbody;
    private Awareness _Awareness;
    private Vector2 _target;
    private float rotCD;
    private RaycastHit2D[] _obstacleCollisions;
    private float _obCD;
    private Vector2 _obstacleAvoidanceDirection;

    TrainerParty party => GetComponent<TrainerParty>();
    TrainerParty playerParty => _Awareness.Player.GetComponent<TrainerParty>();


    private void Awake()
    {
        _runSpeed=_walkSpeed+_sprintMod;
        _rigidbody = GetComponent<Rigidbody2D>();
        _Awareness = GetComponent<Awareness>();
        _target = transform.up;
        _obstacleCollisions = new RaycastHit2D[10];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateToPlayer();
        SetVelocity();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BattleSystem._instance.EnterBattle(playerParty, party);
            gameObject.SetActive(false);
        }
    }
    private void UpdateTargetDirection()
    {
        RandomDirection();
        PlayerTargeting();
        HandleObstacles();
    }
    

    private void PlayerTargeting()
    {
        if (_Awareness.aggro)
        {
            _speed=_runSpeed;
            _target = _Awareness.playerLocation;
        }
        else
        {
            _speed=_walkSpeed;
        }
    }

    private void RandomDirection()
    {
        rotCD -= Time.deltaTime;
        if (rotCD <= 0)
        {
            float angle = Random.Range(-130f,130f);
            Quaternion rotation = Quaternion.AngleAxis(angle,transform.forward);
            _target = rotation*_target;

            rotCD = Random.Range(1f,4f);
        }
    }

    private void HandleObstacles()
    {
        _obCD -= Time.deltaTime;

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(
            transform.position,
            _obstacleCheckRadius,
            transform.up,
            contactFilter,
            _obstacleCollisions,
            _obstacleCheckRange);
        for (int i = 0; i<numberOfCollisions; i++)
        {
            var obstacleCollision = _obstacleCollisions[i];
            if(obstacleCollision.collider.gameObject == gameObject)
            {
                continue;
            }

            if (_obCD <= 0)
            {
                _obstacleAvoidanceDirection = obstacleCollision.normal;
                _obCD = 0.5f;
            }
            

            var targetRotation = Quaternion.LookRotation(transform.forward, _obstacleAvoidanceDirection);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpd *Time.deltaTime);


            _target = rotation * Vector2.up;
            break;
        }
    }

    private void RotateToPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _target);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpd * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        
        _rigidbody.linearVelocity = transform.up * _speed;
        
    }
}
