using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using UnityEngine.SceneManagement;

public class Player : MorphableBehaviour, IDamageable
{
    public PlayerStats stats = new();
    [HideInInspector] public Rigidbody2D rb;
    private MorphGun morphGun;
    [SerializeField] private PlayerGun playerGun;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private HealthbarController healthbarController;

    public bool playing = false;
    public bool dead = false;
    private bool canMorph = true;
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
        if (!canMorph) return;

        canMorph = false;
        state = MorphState.Big;
        playerGun.damageMultiplier = 4;
        Tween.Scale(transform, 4, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
        Invoke(nameof(ReturnNormal), 2);
    }

    public override void Shrink()
    {
        if (!canMorph) return;

        canMorph = false;
        state = MorphState.Small;
        playerGun.damageMultiplier = 1;
        Tween.Scale(transform, 0.5f, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
        Invoke(nameof(ReturnNormal), 2);
    }
    private void ReturnNormal()
    {
        state = MorphState.Normal;
        playerGun.damageMultiplier = 2;
        Tween.Scale(transform, 1, .8f, Ease.InOutQuad).OnComplete(() => canMorph = true);
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

        if (Input.GetKeyDown(KeyCode.Q) && state == MorphState.Normal) Grow();
        if (Input.GetKeyDown(KeyCode.E) && state == MorphState.Normal) Shrink();

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
