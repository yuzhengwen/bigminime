using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class Player : MorphableBehaviour, IDamageable
{
    public PlayerStats stats = new();
    [HideInInspector] public Rigidbody2D rb;
    private MorphGun morphGun;
    private DamageFlash damageFlash;
    [SerializeField] private HealthbarController healthbarController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        morphGun = GetComponentInChildren<MorphGun>();
        damageFlash = GetComponent<DamageFlash>();
    }

    private void Start()
    {
        healthbarController.SetMaxHealth(stats.maxHealth);
    }

    public override void Grow()
    {
        if (state == MorphState.Small)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 1f, 1.5f, Ease.InOutQuad);
        }
        else if (state == MorphState.Normal)
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
            Tween.Scale(transform, 1, 1.5f, Ease.InOutQuad);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Small;
            Tween.Scale(transform, 0.5f, 1.5f, Ease.InOutQuad);
        }
    }

    public void TakeDamage(int damage)
    {
        stats.currentHealth -= damage;
        healthbarController.SetHealth(stats.currentHealth);
        damageFlash.Flash();

        if (stats.currentHealth <= 0)
        {
            Debug.Log("Player died");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveX * stats.speed, moveY * stats.speed);

        if (Input.GetKeyDown(KeyCode.Q)) Grow();
        if (Input.GetKeyDown(KeyCode.E)) Shrink();

        if (Input.GetMouseButtonDown(0)) morphGun.Shoot(MorphType.Shrink);
        if (Input.GetMouseButtonDown(1)) morphGun.Shoot(MorphType.Grow);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            NotificationHandler.Instance.ShowNotification("Hello World!");
        }
    }
}

[System.Serializable]
public class PlayerStats
{
    public int maxHealth = 20;
    public int currentHealth = 20;
    public int speed = 8;
    public int damage = 1;

    public int level = 1;
}
public interface IDamageable
{
    void TakeDamage(int damage);
}
public abstract class MorphableBehaviour : MonoBehaviour
{
    public MorphState state = MorphState.Normal;
    public abstract void Grow();
    public abstract void Shrink();
}
public enum MorphState
{
    Normal,
    Big,
    Small
}
