using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireInterval = .8f;
    private float nextFireTime = 0;
    public bool canShoot = true;
    public int damageMultiplier = 2;
    private void Update()
    {
        if (Time.time >= nextFireTime && canShoot)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.damage *= damageMultiplier;
        AudioHandler.Instance.PlayAudio("BulletFire");
    }
}
