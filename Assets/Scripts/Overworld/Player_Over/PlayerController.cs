using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float interactDistance = 1.5f;
    [SerializeField] float speed = 3;
    private bool canMove = true;

    private static float SpawnpointX;
    private static float SpawnpointY;

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

    void Start()
    {
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
        transform.position = new Vector2(SpawnpointX, SpawnpointY);
    }
    void Update()
    {
        if (InputHandler.InteractPressed) PlayerInteract();

        EventBus.Raise(new ItemSearchEvent { _interactDistance = interactDistance, _interactPosition = transform.position });
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