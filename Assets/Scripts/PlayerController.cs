using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public Rigidbody2D rb;
    float horiz;
    float vert;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = new Vector2(horiz * speed, vert * speed);
        rb.linearVelocityY = new Vector2(horiz * speed, vert * speed);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        horiz = context.ReadValue<Vector2>().x;
        vert = context.ReadValue<Vector2>().y;
    }
}
