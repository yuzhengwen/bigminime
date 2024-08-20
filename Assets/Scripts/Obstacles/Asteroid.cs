﻿using System;
using UnityEngine;

public class Asteroid : MorphableObstacle, IDamageable
{
    public int health = 15;
    private SpriteRenderer spriteRenderer;
    private DamageFlash damageFlash;
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject popupObj;
    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageFlash = GetComponent<DamageFlash>();
    }
    protected override void Start()
    {
        base.Start();
        UpdateHealth();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        damageFlash.Flash();
        UpdateIndicator();
        Instantiate(popupObj, transform.position, Quaternion.identity).GetComponent<PopupScript>().SetText($"{damage}!");
        if (health <= 0)
        {
            DestroySelf();
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
            if (health != 20)
            {
                health = 20;
            }
        }
        else if (state == MorphState.Big)
        {
            if (health < 40)
            {
                health = 40;
            }
        }
    }
    protected override void DestroySelf()
    {
        Destroy(gameObject);
        GameObject hit = Instantiate(hitFx, transform.position, Quaternion.identity);
        AudioHandler.Instance.PlayAudio("AsteroidExplosion");
        float duration = hit.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Destroy(hit, duration);
    }
}
