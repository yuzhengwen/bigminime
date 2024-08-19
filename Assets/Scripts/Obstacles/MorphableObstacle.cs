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
            Tween.Scale(transform, 1f, 1f, Ease.InOutQuad);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Big;
            Tween.Scale(transform, 10, 1f, Ease.InOutQuad);
        }
    }

    public override void Shrink()
    {
        if (state == MorphState.Big)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 5, 1f, Ease.InOutQuad);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Small;
            Tween.Scale(transform, 1f, 1f, Ease.InOutQuad);
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
