using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpd;

    private Rigidbody2D _rigidbody;
    private Awareness _Awareness;
    private Vector2 _target;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _Awareness = GetComponent<Awareness>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateToPlayer();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_Awareness.aggro)
        {
            _target = _Awareness.playerLocation;
        }
        else
        {
            _target = Vector2.zero;
        }
    }

    private void RotateToPlayer()
    {
        if (_target == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _target);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpd*Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_target == Vector2.zero)
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            _rigidbody.linearVelocity = transform.up * _speed;
        }
    }
}
