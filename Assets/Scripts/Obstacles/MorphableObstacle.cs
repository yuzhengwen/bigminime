using PrimeTween;
using UnityEngine;

public class MorphableObstacle : MorphableBehaviour
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
    public override void Grow()
    {
        if (state == MorphState.Small)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 0.25f, 1.5f, Ease.InOutQuad);
        }
        if (state == MorphState.Normal)
        {
            state = MorphState.Big;
            Tween.Scale(transform, 4, 1.5f, Ease.InOutQuad);
        }
    }

    public override void Shrink()
    {
        if (state == MorphState.Big)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 2, 1.5f, Ease.InOutQuad);
        }
        if (state == MorphState.Normal)
        {
            state = MorphState.Small;
            Tween.Scale(transform, 0.5f, 1.5f, Ease.InOutQuad);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.TakeDamage(damage * MorphValues.DamageChart[state]);
            Destroy(gameObject);
        }
    }
}
