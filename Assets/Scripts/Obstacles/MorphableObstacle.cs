using PrimeTween;
using System;
using UnityEngine;

public class MorphableObstacle : MorphableBehaviour
{
    public int damage = 1;
    private Rigidbody2D rb;
    public float speed = 5;

    public event Action OnShrink, OnGrow;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }
    private void Start()
    {
        if (state == MorphState.Small)
        {
            transform.localScale = Vector3.one;
        }
        else if (state == MorphState.Normal)
        {
            transform.localScale = Vector3.one * 5;
        }
        else if (state == MorphState.Big)
        {
            transform.localScale = Vector3.one * 10;
        }
    }
    public override void Grow()
    {
        OnGrow?.Invoke();
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
        OnShrink?.Invoke();
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
