using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float interactDistance = 2f;
    [SerializeField] float speed = 3;
    private bool canMove = true;
    private static float SpawnpointX;
    private static float SpawnpointY;
    private Animator animator;

    #region Player Animation States
    private readonly int Horizontal = Animator.StringToHash("Horizontal");
    private readonly int Vertical = Animator.StringToHash("Vertical");
    private readonly int LastVert = Animator.StringToHash("LastVertical");
    private readonly int LastHor = Animator.StringToHash("LastHorizontal");
    public readonly int Idle = Animator.StringToHash("Idle");
    public readonly int Movement = Animator.StringToHash("Movement");
    #endregion
    void OnEnable()
    {
        EventBus.Subscribe<SceneTransition>(EntryPoint);
        EventBus.Subscribe<PlayerMoveEvent>(CanPlayerMove);
    }

    void OnDisable()
    {
        EventBus.UnSubscribe<SceneTransition>(EntryPoint);
        EventBus.UnSubscribe<PlayerMoveEvent>(CanPlayerMove);
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
        transform.position = new Vector2(SpawnpointX, SpawnpointY);
    }
    void Update()
    {
        if (InputHandler.InteractPressed) PlayerInteract();

        EventBus.Raise(new ItemSearchEvent { _interactDistance = interactDistance, _interactPosition = transform.position });

        ChangePlayerAnimationState();
    }
    void FixedUpdate()
    {
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        if (canMove)
            rb.linearVelocity = InputHandler.Movement.normalized * speed;
    }
    private void PlayerFlip()
    {
        int flip_X = 1;
        if (InputHandler.Movement.x > 0) flip_X = -1;
        if (InputHandler.Movement.x < 0) flip_X = 1;

        transform.localScale = new Vector3(flip_X, transform.localScale.y);
    }
    private void ChangePlayerAnimationState()
    {
        float velX = InputHandler.Movement.x;
        float velY = InputHandler.Movement.y;

        if (InputHandler.Movement != Vector2.zero)
        {
            // Sets the last horizontal/vertical velocity for idle animation
            animator.SetFloat(LastHor, velX);
            animator.SetFloat(LastVert, velY);

            // Set the current movement velocity to play walking animation
            animator.SetFloat(Horizontal, velX);
            animator.SetFloat(Vertical, velY);

            PlayerFlip();
            animator.CrossFade(Movement, 0, 0);
        }
        else
        {
            PlayerFlip();
            animator.CrossFade(Idle, 0, 0);
        }
    }
    void PlayerInteract()
    {
        EventBus.Raise(new PlayerInteractEvent());
    }
    private void EntryPoint(SceneTransition data)
    {
        SpawnpointX = data._X;
        SpawnpointY = data._Y;

        transform.position = new Vector2(SpawnpointX, SpawnpointY);
    }
    private void CanPlayerMove(PlayerMoveEvent data)
    {
        canMove = data.canMove;
        rb.linearVelocity = Vector2.zero;
    }
}