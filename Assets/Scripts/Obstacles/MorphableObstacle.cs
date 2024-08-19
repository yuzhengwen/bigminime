using PrimeTween;
using System;
using UnityEngine;

public class MorphableObstacle : MorphableBehaviour
{
    public int damage = 1;
    private Rigidbody2D rb;
    private bool canMorph = true;

    public event Action OnShrink, OnGrow;
    protected virtual void Start()
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
        if (!canMorph) return;

        canMorph = false;
        OnGrow?.Invoke();
        if (state == MorphState.Small)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 5f, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Big;
            Tween.Scale(transform, 10, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
        }
    }

    public override void Shrink()
    {
        if (!canMorph) return;
        OnShrink?.Invoke();
        if (state == MorphState.Big)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 5, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Small;
            Tween.Scale(transform, 1f, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
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
