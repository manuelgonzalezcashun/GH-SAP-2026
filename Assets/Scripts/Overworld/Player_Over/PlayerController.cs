using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public Rigidbody2D rb;
    private Vector2 moveInput;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput.normalized * speed;
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnEnable()
    {
        EventBus.Subscribe<SceneTransition>(EntryPoint);
    }

    void OnDisable()
    {
        EventBus.UnSubscribe<SceneTransition>(EntryPoint);
    }

    private void EntryPoint(SceneTransition data)
    {
        transform.position = new Vector2(data._X,data._Y);
    }

}
