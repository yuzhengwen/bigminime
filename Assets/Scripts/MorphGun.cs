using PrimeTween;
using System.Collections.Generic;
using UnityEngine;

public class MorphGun : MonoBehaviour
{
    public MorphBullet bulletPrefab;
    public float fireInterval = 1.5f;
    private float nextFireTime = 0;

    public void Shoot(MorphType type)
    {
        if (Time.time >= nextFireTime)
        {
            Quaternion rotate = Quaternion.LookRotation(Vector3.forward, GetVectorToMouse(transform.position));
            Instantiate(bulletPrefab, transform.position, rotate);
            bulletPrefab.type = type;
            nextFireTime = Time.time + fireInterval;
        }
    }
    public static Vector2 GetVectorToMouse(Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
    }
}
public class MorphableObstacle : MorphableBehaviour
{
    public int damage = 1;
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
        }
    }
}
public static class MorphValues
{
    public static Dictionary<MorphState, int> DamageChart = new()
    {
        { MorphState.Small, 1 },
        { MorphState.Normal, 5 },
        { MorphState.Big, 10 }
    };
}
