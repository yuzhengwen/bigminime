using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
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
