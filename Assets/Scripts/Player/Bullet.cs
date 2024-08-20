using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10;
    public float lifeTime = 5;
    private Rigidbody2D rb;
    [SerializeField] private GameObject hitEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
            HitEffect();
            Destroy(gameObject);
        }
    }
    private void HitEffect()
    {
        GameObject fx = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(fx, 0.5f);
    }

}
