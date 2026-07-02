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

}
