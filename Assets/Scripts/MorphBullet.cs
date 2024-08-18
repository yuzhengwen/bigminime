using UnityEngine;

public class MorphBullet : MonoBehaviour
{
    public MorphType type;
    public int damage = 0;
    public float speed = 10;
    public float lifeTime = 5;
    private Rigidbody2D rb;

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
        if (collision.TryGetComponent(out MorphableBehaviour morphable))
        {
            if (type == MorphType.Grow)
                morphable.Grow();
            if (type == MorphType.Shrink)
                morphable.Shrink();
            Destroy(gameObject);
        }
    }
}
public enum MorphType
{
    Shrink, Grow
}
