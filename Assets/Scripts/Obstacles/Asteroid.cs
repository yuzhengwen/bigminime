using System;
using UnityEngine;

public class Asteroid : MorphableObstacle, IDamageable
{
    public int health = 15;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject hitFx;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateIndicator();
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject hit = Instantiate(hitFx, transform.position, Quaternion.identity);
            Destroy(hit, 0.05f);
        }
    }

    private void UpdateIndicator()
    {
        if (health <= 5)
        {
            spriteRenderer.color = Color.red;
        }
        else if (health <= 10)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnEnable()
    {
        OnShrink += UpdateHealth;
        OnGrow += UpdateHealth;
    }
    private void OnDisable()
    {
        OnShrink -= UpdateHealth;
        OnGrow -= UpdateHealth;
    }

    private void UpdateHealth()
    {
        if (state == MorphState.Small)
        {
            if (health > 5)
            {
                health = 5;
            }

        }
        else if (state == MorphState.Normal)
        {
            if (health < 15)
            {
                health = 15;
            }
        }
        else if (state == MorphState.Big)
        {
            if (health < 25)
            {
                health = 25;
            }
        }
    }
}
