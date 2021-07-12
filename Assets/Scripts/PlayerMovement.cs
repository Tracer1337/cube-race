using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2000f;
    public float controlSpeed = 500f;

    private float direction = 0f;
    
    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
    }
    
    private void FixedUpdate()
    {
        rb.AddForce(0, 0, speed * Time.deltaTime);
        rb.AddForce(direction * controlSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        if (rb.position.y < -1)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
