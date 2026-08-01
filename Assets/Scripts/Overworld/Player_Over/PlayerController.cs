using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float interactDistance = 1.5f;
    [SerializeField] float speed = 3;
    private bool canMove = true;
    private static float SpawnpointX;
    private static float SpawnpointY;
    private Animator animator;
    private int currentState;

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

        int state = GetState();
        if (currentState == state) return;

        animator.CrossFade(state, 0, 0);
        currentState = state;
    }
    void FixedUpdate()
    {
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        if (canMove)
            rb.linearVelocity = InputHandler.Movement.normalized * speed;

        PlayerFlip();
    }
    private void PlayerFlip()
    {
        int flip_X = 1;
        if (InputHandler.Movement.x > 0) flip_X = -1;
        if (InputHandler.Movement.x < 0) flip_X = 1;

        transform.localScale = new Vector3(flip_X, transform.localScale.y);
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

    private int GetState()
    {
        if (InputHandler.Movement.y < 0) return AnimationState.WalkSouth;
        if (InputHandler.Movement.y > 0) return AnimationState.WalkNorth;
        if (InputHandler.Movement.x != 0) return AnimationState.WalkSide;
        if (InputHandler.Movement == Vector2.zero) return AnimationState.Idle;

        return -1;
    }
}

public struct AnimationState
{
    public static readonly int WalkNorth = Animator.StringToHash("PlayerWalkNorth");
    public static readonly int WalkSouth = Animator.StringToHash("PlayerWalkSouth");
    public static readonly int WalkSide = Animator.StringToHash("PlayerWalkSide");
    public static readonly int Idle = Animator.StringToHash("PlayerIdle");
}