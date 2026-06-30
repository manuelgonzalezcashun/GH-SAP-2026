using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 vectCheck = new Vector2(horiz,vert);
       
        rb.linearVelocity = vectCheck.normalized * speed;
        
        
        
    }
}
