using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using UnityEngine.SceneManagement;

public class Player : MorphableBehaviour, IDamageable
{
    public PlayerStats stats = new();
    [HideInInspector] public Rigidbody2D rb;
    private MorphGun morphGun;
    private PlayerGun playerGun;
    private DamageFlash damageFlash;
    [SerializeField] private HealthbarController healthbarController;

    public bool playing = false;
    public bool dead = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        morphGun = GetComponentInChildren<MorphGun>();
        playerGun = GetComponentInChildren<PlayerGun>();
        damageFlash = GetComponent<DamageFlash>();
    }

    private void Start()
    {
        healthbarController.SetMaxHealth(stats.maxHealth, stats.maxHealth);
    }
    public void EnablePlay()
    {
        playerGun.canShoot = true;
        playing = true;
        morphGun.canShoot = true;
    }
    public void DisablePlay()
    {
        playerGun.canShoot = false;
        playing = false;
        morphGun.canShoot = false;
    }
    public override void Grow()
    {
        if (state == MorphState.Small)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 1f, .8f, Ease.InOutQuad);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Big;
            Tween.Scale(transform, 4, .8f, Ease.InOutQuad);
        }
    }

    public override void Shrink()
    {
        if (state == MorphState.Big)
        {
            state = MorphState.Normal;
            Tween.Scale(transform, 1, .8f, Ease.InOutQuad);
        }
        else if (state == MorphState.Normal)
        {
            state = MorphState.Small;
            Tween.Scale(transform, 0.5f, .8f, Ease.InOutQuad);
        }
    }

    public void TakeDamage(int damage)
    {
        stats.currentHealth -= damage * MorphValues.SelfDamage[state];
        healthbarController.SetHealth(stats.currentHealth);
        damageFlash.Flash();

        if (stats.currentHealth <= 0)
        {
            dead = true;
            StartMenuHandler.Instance.Restart();
        }
    }

    void Update()
    {
        if (!playing) return;

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
