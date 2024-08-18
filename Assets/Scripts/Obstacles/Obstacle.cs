using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
    private Rigidbody2D rb;
    public float speed = 5;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.left * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
