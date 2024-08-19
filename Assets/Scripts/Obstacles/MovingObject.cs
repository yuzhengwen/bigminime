using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }
}
